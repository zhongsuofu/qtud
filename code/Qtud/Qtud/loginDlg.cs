﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Qtud.DBManage.Manager;
using Qtud.DBManage.Model;

namespace Qtud.Qtud
{
    public partial class loginDlg : Form
    {
        #region 变量
        private UserManager um = new UserManager();
        #endregion

        #region 构造函数
        public loginDlg()
        {
            InitializeComponent();
        }
        #endregion

        #region 界面操作

        private void loginDlg_Load(object sender, EventArgs e)
        {
            textBox_UserName.Text = GetLastLoginUserName();
            this.Show();
            if (textBox_UserName.Text.Trim() != "")
            {
                textBox_UserPasswd.Focus();

            }
        }

        private void loginDlg_KeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽ALT+F4组合。
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Submit();
            }
        }

        private void textBox_UserPasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Submit(); 
        }

        #endregion

        #region 功能模块

        /// <summary>
        /// 校验用户名、密码
        /// </summary>
        /// <returns></returns>
        private bool VerifyUserPWD()
        {
            bool bRet = false;
            string strWhere = string.Empty;
            try
            {
                //对密码做DEs加密和Base64 编码 
                string PswStr = textBox_UserName.Text.Trim() + "*@*" + this.textBox_UserPasswd.Text;  //密码由用户名加密码组成

                //string _Des = DES.DESEncoder(PswStr, Encoding.Default, null, null);  //DES加密 
                //string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码


                strWhere += " user_name='" + textBox_UserName.Text.Trim() + "'";
                //strWhere += " and UserPassword='" + EBase64 + "'";
                strWhere += " and user_passwd='" + this.textBox_UserPasswd.Text + "'";
                strWhere += " and user_status=1";
                List<UserModel> liUserInfo = um.GetModelList(strWhere);
                if (0 == liUserInfo.Count)
                {
                    bRet = false;
                    return bRet;
                }

                //更新最后登录日期
                UserModel data = liUserInfo[0];
                data.user_lastlogintime = DateTime.Now;
                um.UpdateLastLogTime(data);

                CurrentUser._CurUserModel = data;

                bRet = true;

            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                GC.Collect();
            }
            return bRet;
        }


        /// <summary>
        /// 获得最后一次登陆的用户名
        /// </summary>
        /// <returns></returns>
        private string GetLastLoginUserName()
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            try
            {
                strWhere += " user_status=1";//非冻结
                strWhere += " order by user_lastlogintime desc ";
                List<UserModel> liUserInfo = um.GetModelList(strWhere);
                if (0 == liUserInfo.Count)//没找到数据
                {
                    return "";
                }
                sRet = liUserInfo[0].user_name;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            return sRet;
        }

      

        /// <summary>
        /// 登录
        /// </summary>
        private void Submit()
        {
            if (!VerifyUserPWD())
            {
                MessageBox.Show("用户名或密码错误，请重新输入！");
                textBox_UserPasswd.Focus();

                return;
            }

            textBox_UserPasswd.Text = "";
            this.DialogResult = DialogResult.OK;
               
        }

        #endregion
        
    }
}