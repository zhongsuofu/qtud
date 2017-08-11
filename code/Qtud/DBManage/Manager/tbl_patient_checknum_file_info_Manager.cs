using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using LTP.Common;
using Qtud.DBManage.Model;

namespace Qtud.DBManage.Manager
{
    /// <summary>
    /// ҵ���߼���SysMenTbl ��ժҪ˵����
    /// </summary>
    public class tbl_patient_checknum_file_info_Manager
    {
        private readonly Qtud.DBManage.DAL.tbl_patient_checknum_file_info_Dal dal= new Qtud.DBManage.DAL.tbl_patient_checknum_file_info_Dal();
        public tbl_patient_checknum_file_info_Manager()
        { }
        #region  ��Ա����
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(string UUID)
        {
            return dal.Exists(UUID);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Add(Qtud.DBManage.Model.tbl_patient_checknum_file_info_Model model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public void Update(Qtud.DBManage.Model.PatientInfoModel model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public void Delete(string UUID)
        {

            dal.Delete(UUID);
        }

        /// <summary>
        /// ��������½ʱ��
        /// </summary>
        public void Updatebs(Qtud.DBManage.Model.PatientInfoModel model)
        {
            dal.Updatebs(model);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Qtud.DBManage.Model.PatientInfoModel GetModel(string UUID)
        {

            return dal.GetModel(UUID);
        }

        /// <summary>
        /// �õ�һ������ʵ�壬�ӻ����С�
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
        /// ��������б�
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Qtud.DBManage.Model.PatientInfoModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// ��������б�
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
                     
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  ��Ա����
    }
}
