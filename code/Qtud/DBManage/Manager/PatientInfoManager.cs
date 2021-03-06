using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using LTP.Common;
using Qtud.DBManage.Model;

namespace Qtud.DBManage.Manager
{
    /// <summary>
    /// 业务逻辑类SysMenTbl 的摘要说明。
    /// </summary>
    public class PatientInfoManager
    {
        private readonly Qtud.DBManage.DAL.PatientInfoDal dal = new Qtud.DBManage.DAL.PatientInfoDal();
        public PatientInfoManager()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UUID)
        {
            return dal.Exists(UUID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Qtud.DBManage.Model.PatientInfoModel model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Qtud.DBManage.Model.PatientInfoModel model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string UUID)
        {

            dal.Delete(UUID);
        }

        /// <summary>
        /// 更新最后登陆时间
        /// </summary>
        public void Updatebs(Qtud.DBManage.Model.PatientInfoModel model)
        {
            dal.Updatebs(model);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Qtud.DBManage.Model.PatientInfoModel GetModel(string UUID)
        {

            return dal.GetModel(UUID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Qtud.DBManage.Model.PatientInfoModel GetModelByCache(string UUID)
        {

            string CacheKey = "SysCounterTblModel-" + UUID;
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
                catch { }
            }
            return (Qtud.DBManage.Model.PatientInfoModel)objModel;
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
        public List<Qtud.DBManage.Model.PatientInfoModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Qtud.DBManage.Model.PatientInfoModel> DataTableToList(DataTable dt)
        {
            List<Qtud.DBManage.Model.PatientInfoModel> modelList = new List<Qtud.DBManage.Model.PatientInfoModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Qtud.DBManage.Model.PatientInfoModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Qtud.DBManage.Model.PatientInfoModel();
                    model.uuid = dt.Rows[n]["uuid"].ToString();
                    model.cardid = dt.Rows[n]["cardid"].ToString();
                    model.name = dt.Rows[n]["name"].ToString();
                    model.phone = dt.Rows[n]["phone"].ToString();
                    model.sex = int.Parse( dt.Rows[n]["sex"].ToString());
                    model.meno = dt.Rows[n]["meno"].ToString();
                    model.createtime = DateTime.Parse(dt.Rows[n]["createtime"].ToString());
                    model.lastchecktime = DateTime.Parse(dt.Rows[n]["lastchecktime"].ToString());
                    model.bs = dt.Rows[n]["bs"].ToString();
                    model.id = dt.Rows[n]["id"].ToString();
                    if (dt.Rows[n]["birth"].ToString() != null && dt.Rows[n]["birth"].ToString() != "")
                        model.birth = DateTime.Parse(dt.Rows[n]["birth"].ToString());
                     
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
