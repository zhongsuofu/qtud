using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using Qtud.DBManage.Model;

namespace Qtud.DBManage.Manager 
{
	/// <summary>
	/// 业务逻辑类SysUserTbl 的摘要说明。
	/// </summary>
	public class ReportInfoManager
	{
        private readonly Qtud.DBManage.DAL.ReportInfoDal dal = new Qtud.DBManage.DAL.ReportInfoDal();
        public ReportInfoManager()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UUID)
		{
			return dal.Exists(UUID);
		}

        /// <summary>
        /// 插入图片1
        /// </summary>
        public void InsertPic1(string UUID, byte[] imagebytes1)
        {
            dal.InsertPic1(UUID, imagebytes1);
        }

        /// <summary>
        /// 得到图片1
        /// </summary>
        public byte[] GetPic1(string strSql)
        {
            return dal.GetPic1(strSql);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(Qtud.DBManage.Model.ReportInfoModel model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public void Update(Qtud.DBManage.Model.ReportInfoModel model)
		{
			dal.Update(model);
		}

        /// <summary>
        /// 更新最后登陆时间
        /// </summary>
        public void UpdateLastLogTime(Qtud.DBManage.Model.UserModel model)
        {
            dal.UpdateLastLogTime(model);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string UUID)
		{
			
			dal.Delete(UUID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Qtud.DBManage.Model.UserModel GetModel(string UUID)
		{
			
			return dal.GetModel(UUID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Qtud.DBManage.Model.UserModel GetModelByCache(string UUID)
		{
			
			string CacheKey = "SysUserTblModel-" + UUID;
			object objModel = LTP.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(UUID);
					if (objModel != null)
					{
						int ModelCache = LTP.Common.ConfigHelper.GetConfigInt("ModelCache");
						LTP.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Qtud.DBManage.Model.UserModel)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Qtud.DBManage.Model.ReportInfoModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Qtud.DBManage.Model.ReportInfoModel> DataTableToList(DataTable dt)
		{
            List<Qtud.DBManage.Model.ReportInfoModel> modelList = new List<Qtud.DBManage.Model.ReportInfoModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Qtud.DBManage.Model.ReportInfoModel model;
				for (int n = 0; n < rowsCount; n++)
				{
                     
                    model = new Qtud.DBManage.Model.ReportInfoModel();
                    model.uuid = dt.Rows[n]["uuid"].ToString();
                    model.name = dt.Rows[n]["name"].ToString();
                    model.patient_uuid = dt.Rows[n]["patient_uuid"].ToString();
                    if (dt.Rows[n]["CreateDate"].ToString() != "")
                    {
                        model.CreateDate = DateTime.Parse(dt.Rows[n]["CreateDate"].ToString());
                    } 
                    model.ks = dt.Rows[n]["ks"].ToString();
                    model.ch = dt.Rows[n]["ch"].ToString();
                    model.nlljcjg = float.Parse(dt.Rows[n]["nlljcjg"].ToString());
                    model.nlljcjg_nl = float.Parse(dt.Rows[n]["pnl"].ToString());
                    model.pgrlylcd = float.Parse(dt.Rows[n]["pgrlylcd"].ToString());
                    model.pgrl_cg = float.Parse(dt.Rows[n]["pgrl_cg"].ToString());
                    model.pgrl_zc = float.Parse(dt.Rows[n]["pgrl_zc"].ToString());
                    model.pgrl_zd = float.Parse(dt.Rows[n]["pgrl_zd"].ToString());

                    model.pgsyx = dt.Rows[n]["pgsyx"].ToString();
                    model.pgwdx = dt.Rows[n]["pgwdx"].ToString();
                    model.tsjc = dt.Rows[n]["tsjc"].ToString();

                    model.vlpp = float.Parse(dt.Rows[n]["vlpp"].ToString());
                    model.dlpp = float.Parse(dt.Rows[n]["dlpp"].ToString());
                    model.clpp = float.Parse(dt.Rows[n]["clpp"].ToString());
                    model.pgaqrl = float.Parse(dt.Rows[n]["pgaqrl"].ToString());
                    model.otherInfo = dt.Rows[n]["otherInfo"].ToString();
                    model.testresult = dt.Rows[n]["testresult"].ToString();
                     

                    modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

