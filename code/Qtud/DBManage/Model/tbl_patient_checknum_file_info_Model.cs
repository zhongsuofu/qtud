using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类UserModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class tbl_patient_checknum_file_info_Model
	{
		public tbl_patient_checknum_file_info_Model()
		{}
		#region Model
        private string _uuid = string.Empty;
        private string _check_uuid = string.Empty;
        private string _path = string.Empty;
        private int _checkmode = 0;
        private DateTime _createtime = DateTime.Now;
     

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
        public string check_uuid
		{
            set { _check_uuid = value; }
            get { return _check_uuid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string path
		{
            set { _path = value; }
            get { return _path; }
		}

        /// <summary>
		/// 
		/// </summary>
        public int checkmode
		{
            set { _checkmode = value; }
            get { return _checkmode; }
		}
        /// <summary>
		/// 
		/// </summary>
        public DateTime createtime
		{
            set { _createtime = value; }
            get { return _createtime; }
		}
		 
		#endregion Model

	}
}

