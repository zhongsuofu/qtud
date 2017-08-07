using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

//C#�Զ������ļ����м��ܽ���
namespace Qtud.MyCryptoHelp
{
    /// <summary>
    /// �쳣������
    /// </summary>
    public class CryptoHelpException : ApplicationException
    {
        public CryptoHelpException(string msg) : base(msg) { }
    }

    /// <summary>
    /// CryptHelp
    /// </summary>
    public class CryptoHelp
    {
        private const ulong FC_TAG = 0xFC010203040506CF;



        private const int BUFFER_SIZE = 128 * 1024;

        /// <summary>
        /// ��������Byte�����Ƿ���ͬ
        /// </summary>
        /// <param name="b1">Byte����</param>
        /// <param name="b2">Byte����</param>
        /// <returns>true�����</returns>
        private static bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// ����Rijndael SymmetricAlgorithm
        /// </summary>
        /// <param name="password">����</param>
        /// <param name="salt"></param>
        /// <returns>���ܶ���</returns>
        private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);

            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }

        /// <summary>
        /// �����ļ����������
        /// </summary>
        private static RandomNumberGenerator rand = new RNGCryptoServiceProvider();

        /// <summary>
        /// ����ָ�����ȵ����Byte����
        /// </summary>
        /// <param name="count">Byte���鳤��</param>
        /// <returns>���Byte����</returns>
        private static byte[] GenerateRandomBytes(int count)
        {
            byte[] bytes = new byte[count];
            rand.GetBytes(bytes);
            return bytes;
        }


        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="inFile">�������ļ�</param>
        /// <param name="outFile">���ܺ������ļ�</param>
        /// <param name="password">��������</param>
        public static void EncryptFile(string inFile, string outFile, string password)
        {
            using (FileStream fin = File.OpenRead(inFile),
                fout = File.OpenWrite(outFile))
            {
                long lSize = fin.Length; // �����ļ�����
                int size = (int)lSize;
                byte[] bytes = new byte[BUFFER_SIZE]; // ����
                int read = -1; // �����ļ���ȡ����
                int value = 0;

                // ��ȡIV��salt 
                byte[] IV = GenerateRandomBytes(16);
                byte[] salt = GenerateRandomBytes(16);

                // �������ܶ���
                SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
                sma.IV = IV;

                // ������ļ���ʼ����д��IV��salt
                fout.Write(IV, 0, IV.Length);
                fout.Write(salt, 0, salt.Length);

                // ����ɢ�м���
                HashAlgorithm hasher = SHA256.Create();
                using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write),
                    chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    BinaryWriter bw = new BinaryWriter(cout);
                    bw.Write(lSize);

                    bw.Write(FC_TAG);



                    // ��д�ֽڿ鵽������������
                    while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        cout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                    }
                    // �رռ�����
                    chash.Flush();
                    chash.Close();
         

                    // ��ȡɢ��
                    byte[] hash = hasher.Hash;

                    // �����ļ�д��ɢ��
                    cout.Write(hash, 0, hash.Length);


                    // �ر��ļ���
                    cout.Flush();
                    cout.Close();
                }
            }
        }



        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="inFile">�������ļ�</param>
        /// <param name="outFile">���ܺ�����ļ�</param>
        /// <param name="password">��������</param>
        public static void DecryptFile(string inFile, string outFile, string password)
        { 
            // �������ļ���
            using (FileStream fin = File.OpenRead(inFile),
                fout = File.OpenWrite(outFile))
            {
                int size = (int)fin.Length;
                byte[] bytes = new byte[BUFFER_SIZE];
                int read = -1;
                int value = 0;
                int outValue = 0;

                byte[] IV = new byte[16];
                fin.Read(IV, 0, 16);
                byte[] salt = new byte[16];
                fin.Read(salt, 0, 16);

                SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
                sma.IV = IV;



                value = 32;
                long lSize = -1;

                // ����ɢ�ж���, У���ļ�
                HashAlgorithm hasher = SHA256.Create();




                using (CryptoStream cin = new CryptoStream(fin, sma.CreateDecryptor(), CryptoStreamMode.Read),
                    chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    // ��ȡ�ļ�����
                    BinaryReader br = new BinaryReader(cin);
                    lSize = br.ReadInt64();
                    ulong tag = br.ReadUInt64();

                    if (FC_TAG != tag)
                        throw new CryptoHelpException("�ļ����ƻ�");

                    long numReads = lSize / BUFFER_SIZE;

                    long slack = (long)lSize % BUFFER_SIZE;

                    for (int i = 0; i < numReads; ++i)
                    {
                        read = cin.Read(bytes, 0, bytes.Length);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outValue += read;
                    }

                    if (slack > 0)
                    {
                        read = cin.Read(bytes, 0, (int)slack);
                        fout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                        outValue += read;
                    }

                    chash.Flush();
                    chash.Close();

                    fout.Flush();
                    fout.Close();



                    byte[] curHash = hasher.Hash;


                    // ��ȡ�ȽϺ;ɵ�ɢ�ж���
                    byte[] oldHash = new byte[hasher.HashSize / 8];
                    read = cin.Read(oldHash, 0, oldHash.Length);
                    if ((oldHash.Length != read) || (!CheckByteArrays(oldHash, curHash)))
                        throw new CryptoHelpException("�ļ����ƻ�");
                }

                if (outValue != lSize)
                    throw new CryptoHelpException("�ļ���С��ƥ��");
            }
        }
    }
}
 