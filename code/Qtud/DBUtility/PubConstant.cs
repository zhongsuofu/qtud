using System;
using System.Configuration;
using System.Xml;

namespace Maticsoft.DBUtility
{

    public class PubConstant
    {
        internal static string configPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\SqlCon.xml";
        #region ����
        /// <summary>
        /// ��ȡ�����ַ���
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
        /// �õ�web.config������������ݿ������ַ�����
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
