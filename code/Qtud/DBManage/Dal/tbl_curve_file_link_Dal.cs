using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.DBUtility;//请先添加引用
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

using Qtud.DBManage.Model;

namespace Qtud.DBManage.DAL
{
    public class tbl_curve_file_link_Dal
    {
        public tbl_curve_file_link_Dal()
		{}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string cardid)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from tb_patient_info");
            strSql.Append(" where cardid='" + cardid + "' ");

            //MySqlParameter[] parameters = {
            //        new MySqlParameter("@cardid", MySqlDbType.VarChar,128)};
            //parameters[0].Value = cardid;

			return DbHelperMySQL.Exists(strSql.ToString());
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(Qtud.DBManage.Model.tbl_curve_file_link_Model model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into tbl_curve_file_link(");
            strSql.Append(" curve_uuid,file_uuid,nindex)");
			strSql.Append(" values (");
            strSql.Append(@"'" + model.curve_uuid + @"', ");
            strSql.Append(@"'" + model.file_uuid + @"', "); 
			strSql.Append( model.nindex +@" ); ");
			 
             
            DbHelperMySQL.ExecuteSql(strSql.ToString() );
              
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Qtud.DBManage.Model.PatientInfoModel model)
		{ 
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update tb_patient_info set ");

            strSql.Append(@"name ='" + model.name + @"',");
            strSql.Append(@"sex =" + model.sex + @",");
            strSql.Append(@"phone ='" + model.phone + @"',");
            strSql.Append(@"meno ='" + model.meno + @"' ");
			 
			strSql.Append(@" where cardid='"+ model.cardid + @"' ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());
              
            //OleDbParameter[] parameters = { 
            //        new OleDbParameter("@UserUUID", OleDbType.VarChar,64),
            //        new OleDbParameter("@CreateDate", OleDbType.Date),
            //        new OleDbParameter("@OutDate", OleDbType.Date),
            //        new OleDbParameter("@Message", OleDbType.VarChar,255), 
            //        new OleDbParameter("@UUID", OleDbType.VarChar,64)
            //};
             
			
            //parameters[0].Value = model.UserUUID;
            //parameters[1].Value = model.CreateDate;
            //parameters[2].Value = model.OutDate;
            //parameters[3].Value = model.Message;
            //parameters[4].Value = model.UUID; 

            //DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(string strWhere)
		{ 
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from tbl_curve_file_link  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            //OleDbParameter[] parameters = {
            //        new OleDbParameter("@UUID", OleDbType.VarChar,64)};
            //parameters[0].Value = UUID;

            DbHelperMySQL.ExecuteSql(strSql.ToString());
		}


        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Updatebs(PatientInfoModel model)
		{ 
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update   tb_patient_info set bs='" + model.bs+ @"'  ");
            strSql.Append(@" where uuid='" + model.uuid + @"' ");

            //OleDbParameter[] parameters = {
            //        new OleDbParameter("@UUID", OleDbType.VarChar,64)};
            //parameters[0].Value = UUID;

            DbHelperMySQL.ExecuteSql(strSql.ToString());
		}
        

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Qtud.DBManage.Model.PatientInfoModel GetModel(string UUID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  UUID,UserUUID,CreateDate,OutDate,Message from SysMenTbl ");
			strSql.Append(" where UUID=@UUID ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UUID", OleDbType.VarChar,64)};
			parameters[0].Value = UUID;

			Qtud.DBManage.Model.PatientInfoModel model=new Qtud.DBManage.Model.PatientInfoModel();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
                //test

                //model.UUID=ds.Tables[0].Rows[0]["UUID"].ToString();
                //model.UserUUID=ds.Tables[0].Rows[0]["UserUUID"].ToString();
                //model.CreateDate= DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                //model.OutDate= DateTime.Parse(ds.Tables[0].Rows[0]["OutDate"].ToString());
                //model.Message=ds.Tables[0].Rows[0]["Message"].ToString();
				 
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  uuid,cardid,name ,sex ,phone,createtime,lastchecktime,bs,meno ");
            strSql.Append(" from tb_patient_info ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OleDbParameter[] parameters = {
					new OleDbParameter("@tblName", OleDbType.VarChar, 255),
					new OleDbParameter("@fldName", OleDbType.VarChar, 255),
					new OleDbParameter("@PageSize", OleDbType.Integer),
					new OleDbParameter("@PageIndex", OleDbType.Integer),
					new OleDbParameter("@IsReCount", OleDbType.Bit),
					new OleDbParameter("@OrderType", OleDbType.Bit),
					new OleDbParameter("@strWhere", OleDbType.VarChar,1000),
					};
			parameters[0].Value = "SysMenTbl";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOleDb.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  成员方法
    }
}
