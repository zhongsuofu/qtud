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
        #region 公共枚举定义
        #region 银行卡类型
        /// <summary>
        /// 银行卡类型
        /// </summary>
        public enum BankCardType
        {
            /// <summary>
            /// 借记卡
            /// </summary>
            DebitCard = 0x00,
            /// <summary>
            /// 信用卡
            /// </summary>
            CreditCard,
            /// <summary>
            /// 存折
            /// </summary>
            Passbook
        }
        #endregion

        #region 银行卡状态
        /// <summary>
        /// 银行卡类型
        /// </summary>
        public enum BankCardStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            Work = 0x00,
            /// <summary>
            /// 挂失
            /// </summary>
            guashi,
            /// <summary>
            /// 过期
            /// </summary>
            OutTime,
            /// <summary>
            /// 注销
            /// </summary>
            Unreg,
            /// <summary>
            /// 冻结
            /// </summary>
            OutWork
        }
        #endregion

        #region 统计类型
        /// <summary>
        /// 统计类型
        /// </summary>
        public enum StatisticsType
        {
            /// <summary>
            /// 年度收支统计
            /// </summary>
            YearBalance = 0x00,
            /// <summary>
            /// 收入统计
            /// </summary>
            Income,
            /// <summary>
            ///支出统计
            /// </summary>
            Pay,
            /// <summary>
            /// 银行卡单独统计
            /// </summary>
            BankCard,
        }
        #endregion

        #region 财务类型
        /// <summary>
        /// 财务类型
        /// </summary>
        public enum FinanceType
        {
            /// <summary>
            /// 收入
            /// </summary>
            Income = 0,
            /// <summary>
            ///支出
            /// </summary>
            Pay,
        }
        #endregion

        #region 图表类型
        /// <summary>
        /// 图表类型
        /// </summary>
        public enum ChartIndex
        {
            ColChart = 0,
            LineChart,
            PieChart,
        }
        #endregion

        #region 主界面标签
        /// <summary>
        /// 主界面标签
        /// </summary>
        public enum MFTabIndex
        {
            /// <summary>
            /// 个人信息
            /// </summary>
            UserInfo = 0,

            /// <summary>
            /// 银行信息
            /// </summary>
            BankInfo ,

            /// <summary>
            /// 财务信息
            /// </summary>
            FinanceInfo ,

            /// <summary>
            /// 证件信息
            /// </summary>
            CertificationInfo,

            /// <summary>
            /// 注册信息
            /// </summary>
            RegInfo,

            /// <summary>
            /// 客户信息
            /// </summary>
            CounterInfo,

            /// <summary>
            /// 日程信息
            /// </summary>
            MemtoInfo,
 
            /// <summary>
            /// 数据备份信息
            /// </summary>
            SysBakInfo,
        }
        #endregion
        #endregion

        #region 公共结构体

        #region 服务器版本_参考

        /// <summary>
        /// 服务器版本
        /// </summary>
        //[StructLayout(LayoutKind.Sequential, Pack = 2, CharSet = CharSet.Ansi)]
        //public struct Info2009Version
        //{
        //    public short MajorNum;
        //    public short MinorNum;
        //    public short Language;
        //    public short Country;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 34)]
        //    public string Info;
        //}
        #endregion

        #endregion

        #region 公共常量定义

        #endregion

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
