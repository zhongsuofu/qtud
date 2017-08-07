using System;
using System.Collections.Generic;
using System.Text;
 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;


namespace Qtud.SystemCommon
{
    /// <summary>
    /// c#  MD5������ 
    /// </summary>
    class MD5Cls
    {

        public MD5Cls()
     {
         //
         // TODO: �ڴ˴���ӹ��캯���߼�
         //
     }
     //MD5���ʹ��

      #region MD5���ʹ��
     /**//// <summary>
     /// ���ܷ���
     /// </summary>
     /// <param name="input">Ҫת�����ַ���</param>
     /// <returns>ת�����MD5</returns>
 
     public string GetMD5(string input)
     {
         MD5 md5 = MD5.Create();
         string result = "";
         byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
         for (int i = 0; i < data.Length; i++)
         {
            result += data[i].ToString("x2");
         }
         return result;
     }


     /**//// <summary>
     /// MD5�Ƚ�
     /// </summary>
     /// <param name="input">������ַ���</param>
     /// <param name="data">�Ƚϵ��ַ���</param>
     /// <returns>�Ƿ���ͬ</returns>
     /// 
     public bool passWordCheck(string input, string data)
     {
         string hashInput = GetMD5(input);
         if (hashInput.Equals(data))
         {
             return true;
         }
         else
         {
             return false;
         }
     }
     #endregion
    }
}
