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
	public class UserManager
	{
        private readonly Qtud.DBManage.DAL.UserDal dal = new Qtud.DBManage.DAL.UserDal();
		public UserManager()
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
		public void Add(Qtud.DBManage.Model.UserModel model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Qtud.DBManage.Model.UserModel model)
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
		public List<Qtud.DBManage.Model.UserModel> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Qtud.DBManage.Model.UserModel> DataTableToList(DataTable dt)
		{
			List<Qtud.DBManage.Model.UserModel> modelList = new List<Qtud.DBManage.Model.UserModel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Qtud.DBManage.Model.UserModel model;
				for (int n = 0; n < rowsCount; n++)
				{
                    

                    model = new Qtud.DBManage.Model.UserModel();
                    model.user_id = dt.Rows[n]["user_id"].ToString();
                    model.user_name = dt.Rows[n]["user_name"].ToString();
                    model.user_passwd = dt.Rows[n]["user_passwd"].ToString();
                    if (dt.Rows[n]["user_status"].ToString() != "")
                       model.user_status =int.Parse(dt.Rows[n]["user_status"].ToString()) ;
                    if (dt.Rows[n]["user_class"].ToString() != "")
                       model.user_class =int.Parse(dt.Rows[n]["user_class"].ToString()) ;

                    model.user_phone = dt.Rows[n]["user_phone"].ToString();
                    
                    if (dt.Rows[n]["user_createtime"].ToString() != "")
                    {
                        model.user_createtime = DateTime.Parse(dt.Rows[n]["user_createtime"].ToString());
                    }
                    if (dt.Rows[n]["user_lastlogintime"].ToString() != "")
                    {
                        model.user_lastlogintime = DateTime.Parse(dt.Rows[n]["user_lastlogintime"].ToString());
                    } 
                    model.user_meno = dt.Rows[n]["user_meno"].ToString();
                    model.user_loginName = dt.Rows[n]["user_loginName"].ToString();
                     


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

