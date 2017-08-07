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
    /// c#  MD5加密类 
    /// </summary>
    class MD5Cls
    {

        public MD5Cls()
     {
         //
         // TODO: 在此处添加构造函数逻辑
         //
     }
     //MD5类的使用

      #region MD5类的使用
     /**//// <summary>
     /// 加密方法
     /// </summary>
     /// <param name="input">要转换的字符串</param>
     /// <returns>转换后的MD5</returns>
 
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
     /// MD5比较
     /// </summary>
     /// <param name="input">输入的字符串</param>
     /// <param name="data">比较的字符串</param>
     /// <returns>是否相同</returns>
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
