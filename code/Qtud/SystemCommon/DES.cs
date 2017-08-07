using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;    

namespace Qtud.SystemCommon
{ 
    /// <summary>
    /// DES���� ���� 
    ///  string Value = "���Ƕ���һ���һ��� һ���"; 
     
    ///     string _Des = Zgke.Test.DES.DESEncoder(Value, Encoding.Default, null, null); 
    ///       string _AAA = Zgke.Test.DES.DESDecoder(_Des, Encoding.Default, null, null); 

    ///      MessageBox.Show(_Des, _AAA);
    /// </summary>
    public class DES
    {
        /// <summary>   
        /// ���ֽڽ���DES����   
        /// </summary>   
        /// <param name="p_Value">�ֽ�����</param>   
        /// <param name="p_Key">Կ�� ����ʹ�� System.Text.Encoding.AscII.GetBytes("ABVD") ע�������8λ </param>   
        /// <param name="p_IV">���� ���ΪNULL ������Կ����һ��</param>   
        /// <returns>���ܺ��BYTE</returns>   
        public static byte[] DESEncoder(byte[] p_Value, byte[] p_Key, byte[] p_IV)
        {
            byte[] _RgbKey = p_Key;
            byte[] _RgbIV = p_IV;

            if (_RgbKey == null || _RgbKey.Length != 8) _RgbKey = new byte[] { 0x7A, 0x67, 0x6B, 0x65, 0x40, 0x73, 0x69, 0x6E };
            if (_RgbIV == null) _RgbIV = _RgbKey;

            DESCryptoServiceProvider _Desc = new DESCryptoServiceProvider();
            ICryptoTransform _ICrypto = _Desc.CreateEncryptor(_RgbKey, _RgbIV);

            return _ICrypto.TransformFinalBlock(p_Value, 0, p_Value.Length);
        }
        /// <summary>   
        /// ���ֽڽ���DES����   
        /// </summary>   
        /// <param name="p_Value">�ֽ�����</param>   
        /// <param name="p_Key">Կ�� ����ʹ�� System.Text.Encoding.AscII.GetBytes("ABVD") ע�������8λ </param>   
        /// <param name="p_IV">���� ���ΪNULL ������Կ����һ��</param>   
        /// <returns>���ܺ��BYTE</returns>   
        public static byte[] DESDecoder(byte[] p_Value, byte[] p_Key, byte[] p_IV)
        {
            byte[] _RgbKey = p_Key;
            byte[] _RgbIV = p_IV;

            if (_RgbKey == null || _RgbKey.Length != 8) _RgbKey = new byte[] { 0x7A, 0x67, 0x6B, 0x65, 0x40, 0x73, 0x69, 0x6E };
            if (_RgbIV == null) _RgbIV = _RgbKey;

            DESCryptoServiceProvider _Desc = new DESCryptoServiceProvider();
            ICryptoTransform _ICrypto = _Desc.CreateDecryptor(_RgbKey, _RgbIV);

            return _ICrypto.TransformFinalBlock(p_Value, 0, p_Value.Length);
        }

        /// <summary>   
        /// DES����   
        /// </summary>   
        /// <param name="enStr">ԭʼ����</param>   
        /// <param name="p_TextEncoding">���ݱ���</param>   
        /// <param name="p_Key">Կ�� ����ʹ�� System.Text.Encoding.AscII.GetBytes("ABVD") ע�������8λ </param>   
        /// <param name="p_IV">���� ���ΪNULL ������Կ����һ��</param>   
        /// <returns>���ܺ���ַ��� 00-00-00</returns>   
        public static string DESEncoder(string p_TextValue, System.Text.Encoding p_TextEncoding, byte[] p_Key, byte[] p_IV)
        {
            byte[] _DataByte = p_TextEncoding.GetBytes(p_TextValue);
            return BitConverter.ToString(DESEncoder(_DataByte, p_Key, p_IV));
        }

        /// <summary>   
        /// DES����   
        /// </summary>   
        /// <param name="p_TextValue">������������</param>   
        /// <param name="p_TextEncoding">���ݱ���</param>   
        /// <param name="p_Key">Կ�� ����ʹ�� System.Text.Encoding.AscII.GetBytes("ABVD") ע�������8λ </param>   
        /// <param name="p_IV">���� ���ΪNULL ������Կ����һ��</param>   
        /// <returns>���ܺ���ַ���</returns>   
        public static string DESDecoder(string p_TextValue, System.Text.Encoding p_TextEncoding, byte[] p_Key, byte[] p_IV)
        {

            string[] _ByteText = p_TextValue.Split('-');
            byte[] _DataByte = new byte[_ByteText.Length];
            for (int i = 0; i != _ByteText.Length; i++)
            {
                _DataByte[i] = Convert.ToByte(_ByteText[i], 16);
            }
            return p_TextEncoding.GetString(DESDecoder(_DataByte, p_Key, p_IV));
        }   
    }
}
