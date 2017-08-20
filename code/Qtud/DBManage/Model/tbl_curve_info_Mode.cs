using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类UserModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class tbl_curve_info_Model
	{
		public tbl_curve_info_Model()
		{}
		#region Model
        private string _uuid = string.Empty;
        private string _report_uuid = string.Empty;
        private DateTime _starttime = DateTime.Now;
        private DateTime _endtime = DateTime.Now;
        private string _meno = string.Empty;
        private string _rangs = string.Empty;
        private string _strmode = string.Empty;
        private int _nindex = 0;
     

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
        public string report_uuid
		{
            set { _report_uuid = value; }
            get { return _report_uuid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string meno
		{
            set { _meno = value; }
            get { return _meno; }
		}

        /// <summary>
		/// 
		/// </summary>
        public DateTime starttime
		{
            set { _starttime = value; }
            get { return _starttime; }
		}
        /// <summary>
		/// 
		/// </summary>
        public DateTime endtime
		{
            set { _endtime = value; }
            get { return _endtime; }
		}
		 
        /// <summary>
		/// 
		/// </summary>
        public string rangs
		{
            set { _rangs = value; }
            get { return _rangs; }
		}
		  
        /// <summary>
		/// 
		/// </summary>
        public string strmode
		{
            set { _strmode = value; }
            get { return _strmode; }
		}
		  
        /// <summary>
		/// 
		/// </summary>
        public int nindex
		{
            set { _nindex = value; }
            get { return _nindex; }
		}
		  
        
		#endregion Model

	}
}

