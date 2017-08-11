using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//请先添加引用
using Qtud.SystemCommon;   //zip , DES用到
using MySql.Data.MySqlClient;

namespace Qtud.DBManage.DAL
{
    /// <summary>
    /// 数据访问类UserDal。
    /// </summary>
    public class UserDal
    {
        public UserDal()
        {
        }

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SysUserTbl");
            strSql.Append(" where UUID=@UUID ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@UUID", OleDbType.VarChar,64)};
            parameters[0].Value = UUID;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入图片1
        /// </summary>
        public void InsertPic1(string UUID, byte[] imagebytes1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysUserTbl set  ");
            strSql.Append("Pic1=@Pic1 ");
            strSql.Append(" where UUID=@UUID ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Pic1", OleDbType.Binary), 
					new OleDbParameter("@UUID", OleDbType.VarChar,64)
            };

            Zip m_Zip = new Zip();
            byte[] imagebytes = m_Zip.CompressBytes(imagebytes1);

            parameters[0].Value = imagebytes;

            parameters[1].Value = UUID;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到图片1
        /// </summary>
        public byte[] GetPic1(string strSql)
        {
            byte[] picStream = null;
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Pic1"] != DBNull.Value)
                {
                    picStream = (byte[])ds.Tables[0].Rows[0]["Pic1"];
                    Zip m_Zip = new Zip();
                    picStream = m_Zip.DecompressBytes(picStream);

                }
            }
            return picStream;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(Qtud.DBManage.Model.UserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_user(");
            strSql.Append(" user_id,user_name,user_passwd,user_status,user_class,user_phone,user_createtime,user_lastlogintime,user_meno,user_loginName)");   
            strSql.Append("  values(");
            strSql.Append(@"'"+model.user_id+ @"',");
            strSql.Append(@"'"+model.user_name+ @"',");
            strSql.Append(@"'"+model.user_passwd+ @"',");
            strSql.Append( model.user_status+ @",");
            strSql.Append( model.user_class+ @",");
            strSql.Append(@"'"+model.user_phone+ @"',");
            strSql.Append(@"'"+model.user_createtime+ @"',");
            strSql.Append(@"'"+model.user_lastlogintime+ @"',");
            strSql.Append(@"'"+model.user_meno+ @"',");
            strSql.Append(@"'"+model.user_loginName+ @"'");
            strSql.Append(" );");  

             
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Qtud.DBManage.Model.UserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_user set ");
            strSql.Append("user_name='"+model.user_name+@"',");
            strSql.Append("user_passwd='"+model.user_passwd+@"',");
            strSql.Append("user_phone='"+model.user_phone+@"',");
            strSql.Append("user_status="+model.user_status+@",");
            strSql.Append("user_class="+model.user_class+@",");
            strSql.Append("user_createtime='"+model.user_createtime+@"',");
            strSql.Append("user_lastlogintime='"+model.user_lastlogintime+@"',");
            strSql.Append("user_meno='"+model.user_meno+@"',");
            strSql.Append("user_loginName='"+model.user_loginName+@"' ");
          
            strSql.Append(" where user_id='"+model.user_id+@"'");
          
            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateLastLogTime(Qtud.DBManage.Model.UserModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_user set ");
            strSql.Append(@"user_lastlogintime='" + model.user_lastlogintime +"'");
            strSql.Append(@" where user_id='" + model.user_id + "'");
           
            //MySqlParameter[] parameters = { 
            //        new MySqlParameter("@user_lastlogintime", MySqlDbType.Datetime),
            //        new MySqlParameter("@user_id", MySqlDbType.VarChar,64)
            //};

            //parameters[0].Value = model.user_lastlogintime;
            //parameters[1].Value = model.user_id;

            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(string UUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_user ");
            strSql.Append(" where user_id='"+UUID+@"' "); 

            DbHelperMySQL.ExecuteSql(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Qtud.DBManage.Model.UserModel GetModel(string UUID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UUID,UserName,UserFullName,UserPassword,SocialSecurityNO,Email,Dept,Tel,CellPhone,Fax,Status,CreateDate,ModifyDate,LastLogDate,Meno,IsSysUser from SysUserTbl ");
            strSql.Append(" where UUID=? ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@UUID", OleDbType.VarChar,64)};
            parameters[0].Value = UUID;

            Qtud.DBManage.Model.UserModel model = new Qtud.DBManage.Model.UserModel();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //test
                //model.UUID = ds.Tables[0].Rows[0]["UUID"].ToString();
                //model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                //model.UserFullName = ds.Tables[0].Rows[0]["UserFullName"].ToString();
                //model.Password = ds.Tables[0].Rows[0]["UserPassword"].ToString();
 
                //model.SocialSecurityNO = ds.Tables[0].Rows[0]["SocialSecurityNO"].ToString();
                //model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                //model.Dept = ds.Tables[0].Rows[0]["Dept"].ToString();
                //model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                //model.CellPhone = ds.Tables[0].Rows[0]["CellPhone"].ToString();
                //model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                //model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                //if (ds.Tables[0].Rows[0]["CreateDate"].ToString() != "")
                //{
                //    model.CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["ModifyDate"].ToString() != "")
                //{
                //    model.ModifyDate = DateTime.Parse(ds.Tables[0].Rows[0]["ModifyDate"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["LastLogDate"].ToString() != "")
                //{
                //    model.LastLogDate = DateTime.Parse(ds.Tables[0].Rows[0]["LastLogDate"].ToString());
                //}
                //model.Meno = ds.Tables[0].Rows[0]["Meno"].ToString();

                //if (ds.Tables[0].Rows[0]["IsSysUser"].ToString() != "")
                //{
                //    model.IsSysUser = int.Parse(ds.Tables[0].Rows[0]["IsSysUser"].ToString());
                //}

                
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
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select user_id, user_name, user_passwd, user_status,user_class,user_phone,user_createtime,user_lastlogintime,user_meno,user_loginName ");
            strSql.Append(" from tb_user ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            parameters[0].Value = "SysUserTbl";
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

