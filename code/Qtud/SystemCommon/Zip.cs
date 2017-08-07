using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

using ICSharpCode;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression; 
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

namespace Qtud.SystemCommon
{
    /// <summary>   
    /// Zip ѹ���ļ�   
    /// </summary>   
    public class Zip
    {
        public Zip()
        {

        }

        #region ѹ���ļ�����
        /// <summary>   
        /// ���ܣ�ѹ���ļ�����ʱֻѹ���ļ�����һ��Ŀ¼�е��ļ����ļ��м����Ӽ������ԣ�   
        /// </summary>   
        /// <param name="dirPath">��ѹ�����ļ��м�·��</param>   
        /// <param name="zipFilePath">����ѹ���ļ���·����Ϊ����Ĭ���뱻ѹ���ļ���ͬһ��Ŀ¼������Ϊ���ļ�����+.zip</param>   
        /// <param name="err">������Ϣ</param>   
        /// <returns>�Ƿ�ѹ���ɹ�</returns>   
        public static bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "Ҫѹ�����ļ��в���Ϊ�գ�";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "Ҫѹ�����ļ��в����ڣ�";
                return false;
            }
            //ѹ���ļ���Ϊ��ʱʹ���ļ�������.zip   
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("//"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }
        #endregion

        #region ��ѹ�ļ�����
        /// <summary>   
        /// ���ܣ���ѹzip��ʽ���ļ���   
        /// </summary>   
        /// <param name="zipFilePath">ѹ���ļ�·��</param>   
        /// <param name="unZipDir">��ѹ�ļ����·��,Ϊ��ʱĬ����ѹ���ļ�ͬһ��Ŀ¼�£���ѹ���ļ�ͬ�����ļ���</param>   
        /// <param name="err">������Ϣ</param>   
        /// <returns>��ѹ�Ƿ�ɹ�</returns>   
        public static bool UnZipFile(string zipFilePath, string unZipDir, out string err)
        {
            err = "";
            if (zipFilePath == string.Empty)
            {
                err = "ѹ���ļ�����Ϊ�գ�";
                return false;
            }
            if (!File.Exists(zipFilePath))
            {
                err = "ѹ���ļ������ڣ�";
                return false;
            }
            //��ѹ�ļ���Ϊ��ʱĬ����ѹ���ļ�ͬһ��Ŀ¼�£���ѹ���ļ�ͬ�����ļ���   
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("//"))
                unZipDir += "//";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);

            try
            {
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {

                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (!directoryName.EndsWith("//"))
                            directoryName += "//";
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }//while   
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }//��ѹ����  
        #endregion


        /// <summary>
        /// �ַ���ѹ�����ֽ�����
        /// ���أ���ѹ�����ֽ�����
        /// </summary>
        /// <param name="stringToCompress">��ѹ�����ַ���</param>
        /// <returns></returns>
        public   byte[] Compress(string stringToCompress)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
            byte[] compressedData = CompressBytes(bytData);
            return compressedData;
        }

        /// <summary>
        /// �ֽ������ѹ�����ַ���
        /// ���أ���ѹ�����ַ���
        /// </summary>
        /// <param name="bytData">����ѹ�����ֽ�����</param>
        /// <returns></returns>
        public   string DeCompress(byte[] bytData)
        {
            byte[] decompressedData = DecompressBytes(bytData);
            return Encoding.UTF8.GetString(decompressedData);
        }

        /// <summary>
        /// �ַ���ѹ��
        /// ���أ���ѹ�����ַ���
        /// </summary>
        /// <param name="stringToCompress">��ѹ�����ַ���</param>
        /// <returns></returns>
        public   string CompressString(string stringToCompress)
        {
            byte[] bytData = Encoding.UTF8.GetBytes(stringToCompress);
            byte[] compressedData = CompressBytes(bytData);
            return Convert.ToBase64String(compressedData);
        }

        /// <summary>
        /// �ַ�����ѹ��
        /// ���أ���ѹ�����ַ���
        /// </summary>
        /// <param name="CompressTostring">����ѹ�����ַ���</param>
        /// <returns></returns>
        public   string DeCompressString(string CompressTostring)
        {
            byte[] bytData = Convert.FromBase64String(CompressTostring);
            byte[] decompressedData = DecompressBytes(bytData);
            return Encoding.UTF8.GetString(decompressedData);
        }

        /// <summary>
        /// �ֽ�����ѹ��
        /// ���أ���ѹ�����ֽ�����
        /// </summary>
        /// <param name="data">��ѹ�����ֽ�����</param>
        /// <returns></returns>
        public   byte[] CompressBytes(byte[] data)
        {
            ICSharpCode.SharpZipLib.Zip.Compression.Deflater f = new ICSharpCode.SharpZipLib.Zip.Compression.Deflater(ICSharpCode.SharpZipLib.Zip.Compression.Deflater.BEST_COMPRESSION);
            f.SetInput(data);
            f.Finish();

            MemoryStream o = new MemoryStream(data.Length);

            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Deflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }
            return o.ToArray();
        }

        /// <summary>
        /// �ֽ������ѹ��
        /// ���أ��ѽ�ѹ�����ֽ�����
        /// </summary>
        /// <param name="data">����ѹ�����ֽ�����</param>
        /// <returns></returns>
        public   byte[] DecompressBytes(byte[] data)
        {
            Inflater f = new Inflater();
            f.SetInput(data);

            MemoryStream o = new MemoryStream(data.Length);
            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Inflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }
            return o.ToArray();
        } 
     
    }
}
