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
    public class tbl_patient_checknum_file_info_Manager
    {
        private readonly Qtud.DBManage.DAL.tbl_patient_checknum_file_info_Dal dal= new Qtud.DBManage.DAL.tbl_patient_checknum_file_info_Dal();
        public tbl_patient_checknum_file_info_Manager()
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
        public void Add(Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model model)
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
        public List<Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model> DataTableToList(DataTable dt)
        {
            List<Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model> modelList = new List<Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model();
                    model.uuid = dt.Rows[n]["uuid"].ToString();
                    model.checkmode = dt.Rows[n]["checkmode"].ToString();
                    model.path = dt.Rows[n]["path"].ToString();

                    model.check_uuid = dt.Rows[n]["check_uuid"].ToString();
                     
                    model.createtime = DateTime.Parse(dt.Rows[n]["createtime"].ToString());
                    ;
                     
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
