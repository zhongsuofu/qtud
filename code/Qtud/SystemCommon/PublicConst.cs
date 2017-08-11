using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;
using Maticsoft.DBUtility;//请先添加引用
namespace Qtud.SystemCommon
{
    /// <summary>
    /// 公共变量
    /// </summary>
    public class PublicConst
    { 
 
        #region 公共方法
        /// <summary>
        /// 文件大小单位
        /// </summary>
        private enum FileSizeUnit
        {
            B = 0x00,
            KB = 0x01,
            MB = 0x02,
            GB = 0x03,
        }
        /// <summary>
        /// 获得文件大小
        /// </summary>
        /// <param name="FileSize"></param>
        /// <returns></returns>
        public static string GetFileSize(int FileSize)
        {
            try
            {

                Int64 LeftSize = 0;
                string retString;
                int index = 0;
                retString = FileSize.ToString() + " " + ((FileSizeUnit)index).ToString();
                while (FileSize > 1024)
                {
                    LeftSize = FileSize % 1024;
                    FileSize /= 1024;
                    index++;
                    double DispSize = FileSize + LeftSize / 1024.0;

                    retString = string.Format("{0:F}", DispSize) + " " + ((FileSizeUnit)index).ToString();
                }
                return retString;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        /// <summary>
        /// 生成uuid
        /// </summary>
        /// <returns>uuid</returns>
        public static string GenerateUUID()
        {
            try
            {
                return Guid.NewGuid().ToString("N").ToUpper();
            }
            catch (Exception e)
            {
                return ""; 
            }
        }

        
        ///   <summary> 
        ///   判断是否是实数，是返回true   否返回false。可以传入null。 
        ///   </summary> 
        ///   <param   name= "strVal "> 要验证的字符串 </param> 
        ///   <returns> </returns> 
        public static bool IsNumeric(string strVal)
        {
            //判断是否为null   和空字符串 
            if (strVal == null || strVal.Length == 0)
                return false;
            //判断是否只有.、-、   -. 
            if (strVal == ". " || strVal == "- " || strVal == "-. ")
                return false;

            //记录是否有多个小数点 
            bool isPoint = false; //是否有小数点 

            //去掉第一个负号，中间是不可以有负号的 
            if (strVal.Substring(0, 1) == "- ")
                strVal = strVal.TrimStart('-');

            foreach (char c in strVal)
            {
                if (c == '.')
                    if (isPoint)
                        return false;
                    else
                        isPoint = true;

                if ((c < '0' || c > '9') && c != '.')
                    return false;
            }
            return true;
        }

        #endregion
    }
}
