using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类UserModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class tbl_patient_checknum_link_Model
	{
		public tbl_patient_checknum_link_Model()
		{}
		#region Model
        private string _uuid = string.Empty;
        private string _patient_uuid = string.Empty;
        private string _checknum = string.Empty;

        private string _txtPath = string.Empty;

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
        public string patient_uuid
		{
            set { _patient_uuid = value; }
            get { return _patient_uuid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string checknum
		{
            set { _checknum = value; }
            get { return _checknum; }
		}
		 
        /// <summary>
		/// 
		/// </summary>
        public string txtPath
		{
            set { _txtPath = value; }
            get { return _txtPath; }
		}
		 
        
		#endregion Model

	}
}

