using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类UserModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class UserModel
	{
		public UserModel()
		{}
		#region Model
        private string _user_id = string.Empty;
        private string _user_name = string.Empty;
        private string _user_passwd = string.Empty;
        private int _user_status = 0;
        private int _user_class = 0;
        private string _user_phone = string.Empty;
        private DateTime _user_createtime = DateTime.Now;
        private DateTime _user_lastlogintime = DateTime.Now;
        private string _user_meno = string.Empty; 
        private string _user_loginName = string.Empty;

		/// <summary>
		/// 
		/// </summary>
        public string user_id
		{
            set { _user_id = value; }
            get { return _user_id; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string user_name
		{
            set { _user_name = value; }
            get { return _user_name; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string user_passwd
		{
            set { _user_passwd = value; }
            get { return _user_passwd; }
		}
		/// <summary>
		/// 
		/// </summary>
        public string user_phone
		{
            set { _user_phone = value; }
            get { return _user_phone; }
		}
		
		 
		/// <summary>
		/// 
		/// </summary>
        public int user_status
		{
            set { _user_status = value; }
            get { return _user_status ; }
		}

        /// <summary>
        /// 
        /// </summary>
        public int user_class
        {
            set { _user_class = value; }
            get { return _user_class; }
        }
		/// <summary>
		/// 
		/// </summary>
        public DateTime user_createtime
		{
            set { _user_createtime = value; }
            get { return _user_createtime; }
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime user_lastlogintime
		{
            set { _user_lastlogintime = value; }
            get { return _user_lastlogintime; }
		}

        /// <summary>
        /// 
        /// </summary>
        public string user_meno
        {
            set { _user_meno = value; }
            get { return _user_meno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string user_loginName
        {
            set { _user_loginName = value; }
            get { return _user_loginName; }
        }
       

        
		#endregion Model

	}
}

