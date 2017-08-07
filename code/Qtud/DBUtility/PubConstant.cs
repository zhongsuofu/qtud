using System;
using System.Configuration;
using System.Xml;

namespace Maticsoft.DBUtility
{

    public class PubConstant
    {
        internal static string configPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\SqlCon.xml";
        #region 属性
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                XmlDocument Xmldoc = new XmlDocument();
                Xmldoc.Load(configPath);

                string _connectionString = Xmldoc.SelectSingleNode("//ConnectionString").InnerText;
                string ConStringEncrypt = Xmldoc.SelectSingleNode("//ConStringEncrypt").InnerText;
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                return _connectionString;
            }
        }
        #endregion

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            XmlDocument Xmldoc = new XmlDocument();
            Xmldoc.Load(configPath);
            string connectionString = Xmldoc.SelectSingleNode("//ConnectionString").InnerText;
            string ConStringEncrypt = Xmldoc.SelectSingleNode("//ConStringEncrypt").InnerText;
            if (ConStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
