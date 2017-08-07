using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类PatientInfoModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PrintInfoModel
	{
        public PrintInfoModel()
		{}
		#region Model
        private string _uuid = string.Empty;
        private string _useruuid = string.Empty;
        private string _ReportUUId = string.Empty;
        private int _pagecnt = 0;
        private DateTime _printDate = DateTime.Now;
        private int _printcnt = 0;
  
		/// <summary>
		/// 
		/// </summary>
        public string uuid
		{
            set { _uuid = value; }
            get { return _uuid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string useruuid
		{
            set { _useruuid = value; }
            get { return _useruuid; }
		}

        /// <summary>
        /// 
        /// </summary>
        public string ReportUUId
        {
            set { _ReportUUId = value; }
            get { return _ReportUUId; }
        }
         
		/// <summary>
		/// 
		/// </summary>
        public int pagecnt
		{
            set { _pagecnt = value; }
            get { return _pagecnt; }
		}

        /// <summary>
        /// 
        /// </summary>
        public DateTime printDate
        {
            set { _printDate = value; }
            get { return _printDate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int printcnt
        {
            set { _printcnt = value; }
            get { return _printcnt; }
        }
         
        
		#endregion Model

	}
}

