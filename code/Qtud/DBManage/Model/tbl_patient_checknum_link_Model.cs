using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// ʵ����UserModel ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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

