using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// ʵ����UserModel ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class tbl_curve_file_link_Model
	{
        public tbl_curve_file_link_Model()
		{}
		#region Model
        private string _curve_uuid = string.Empty;
        private string _file_uuid = string.Empty;
        private int _nindex = 0;
     

		/// <summary>
		/// 
		/// </summary>
        public string curve_uuid
		{
            set { _curve_uuid = value; }
            get { return _curve_uuid; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string file_uuid
		{
            set { _file_uuid = value; }
            get { return _file_uuid; }
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

