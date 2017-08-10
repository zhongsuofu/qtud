using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类PatientInfoModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PatientInfoModel
	{
		public PatientInfoModel()
		{}
		#region Model
        private string _uuid = string.Empty;
        private string _name = string.Empty;
        private string _cardid = string.Empty;
        private int _sex = 0;     // 性别 0女 1男
        private string _phone = string.Empty;
        private DateTime _createtime = DateTime.Now;
        private DateTime _lastchecktime = DateTime.Now;
        private string _meno = string.Empty;
        private string _bs = string.Empty;  //病史
        private DateTime _birth = DateTime.Now;  //病史
        private string _id = string.Empty;  //病史

         

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
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
		/// <summary>
		/// 
		/// </summary>
        public string name
		{
            set { _name = value; }
            get { return _name; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string cardid
		{
            set { _cardid = value; }
            get { return _cardid; }
		} 
		/// <summary>
		/// 
		/// </summary>
        public int sex
		{
            set { _sex = value; }
            get { return _sex; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string phone
		{
            set { _phone = value; }
            get { return _phone; }
		}
		 
		 
        
		/// <summary>
		/// 
		/// </summary>
        public DateTime createtime
		{
            set { _createtime = value; }
            get { return _createtime; }
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime lastchecktime
		{
            set { _lastchecktime = value; }
            get { return _lastchecktime; }
		}

        /// <summary>
		/// 
		/// </summary>
        public DateTime birth
		{
            set { _birth = value; }
            get { return _birth; }
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
        public string bs
        {
            set { _bs = value; }
            get { return _bs; }
        }
        
		#endregion Model

	}
}

