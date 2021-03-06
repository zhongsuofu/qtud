using System;
namespace Qtud.DBManage.Model
{
	/// <summary>
	/// 实体类UserModel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReportInfoModel
	{
        public ReportInfoModel()
		{}
        #region Model
        private string _uuid = string.Empty;
        private string _name = string.Empty;
        private DateTime _CreateDate = DateTime.Now;

        private string _patient_uuid = string.Empty;
        private string _ks = string.Empty;
        private string _ch = string.Empty;
        private string _nlljcjg = string.Empty;  //最大尿流率
        private string _nlljcjg_nl = string.Empty;  //尿量

        private string _pgrlylcd = string.Empty;
        private string _pgrl_cg = string.Empty;
        private string _pgrl_zc = string.Empty;
        private string _pgrl_zd = string.Empty;

        private string _pgsyx = string.Empty;
        private string _pgwdx = string.Empty;
        private string _tsjc = string.Empty;
        private string _vlpp = string.Empty;
        private string _dlpp = string.Empty;
        private string _clpp = string.Empty;
        private string _pgaqrl = string.Empty;

        private string _otherInfo = string.Empty;
        private string _testresult = string.Empty;

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
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _CreateDate = value; }
            get { return _CreateDate; }
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
        public string ks
        {
            set { _ks = value; }
            get { return _ks; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ch
        {
            set { _ch = value; }
            get { return _ch; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string nlljcjg
        {
            set { _nlljcjg = value; }
            get { return _nlljcjg; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string nlljcjg_nl
        {
            set { _nlljcjg_nl = value; }
            get { return _nlljcjg_nl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string pgrlylcd
        {
            set { _pgrlylcd = value; }
            get { return _pgrlylcd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pgrl_cg
        {
            set { _pgrl_cg = value; }
            get { return _pgrl_cg; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string pgrl_zc
        {
            set { _pgrl_zc = value; }
            get { return _pgrl_zc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string pgrl_zd
        {
            set { _pgrl_zd = value; }
            get { return _pgrl_zd; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string pgsyx
        {
            set { _pgsyx = value; }
            get { return _pgsyx; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string pgwdx
        {
            set { _pgwdx = value; }
            get { return _pgwdx; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string tsjc
        {
            set { _tsjc = value; }
            get { return _tsjc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string vlpp
        {
            set { _vlpp = value; }
            get { return _vlpp; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string dlpp
        {
            set { _dlpp = value; }
            get { return _dlpp; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string clpp
        {
            set { _clpp = value; }
            get { return _clpp; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string pgaqrl
        {
            set { _pgaqrl = value; }
            get { return _pgaqrl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string otherInfo
        {
            set { _otherInfo = value; }
            get { return _otherInfo; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string testresult
        {
            set { _testresult = value; }
            get { return _testresult; }
        }

        #endregion Model

	}
}

