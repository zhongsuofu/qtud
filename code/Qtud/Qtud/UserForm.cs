using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Qtud.DBManage.Model;

using Qtud.SystemCommon;
using Qtud.DBManage.Manager;


namespace Qtud.Qtud
{
    public partial class UserForm : Form
    { 
        UserModel m_UserModel = null;
        bool isMyInfo = false;
        public UserForm(UserModel _UserModel, bool _isMyInfo = false)
        {
            InitializeComponent();

            m_UserModel = _UserModel;
            isMyInfo = _isMyInfo;
        }
         
        private void UserForm_Load(object sender, EventArgs e)
        {
            if (isMyInfo)
                button_EditPwd.Visible = true;
            else
                button_EditPwd.Visible = false;

            if (m_UserModel != null)
            {
                textBox_loginName.Text = m_UserModel.user_loginName;
                textBox_name.Text = m_UserModel.user_name;
                textBox_pwd.Text = m_UserModel.user_passwd;
                textBox_repwd.Text = m_UserModel.user_passwd;
                comboBox_usertype.SelectedIndex = m_UserModel.user_class-1;
                textBox_phone.Text = m_UserModel.user_phone;
                textBox_meno.Text = m_UserModel.user_meno;

                textBox_loginName.Enabled = false;

                textBox_pwd.Enabled = false;
                textBox_repwd.Enabled = false;
            }
            else
            {
                comboBox_usertype.SelectedIndex = 0;

            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        { 
            if (textBox_loginName.Text.Trim() == "")
            {
                MessageBox.Show("登录名不能为空, 请重新输入！");
                textBox_loginName.Focus();
                return;
            }
            if (textBox_pwd.Text == "")
            {
                MessageBox.Show("密码不能为空，请重新输入！");
                textBox_pwd.Focus();
                return;
            }

            if (textBox_pwd.Text != textBox_repwd.Text)
            {
                MessageBox.Show("两次密码输入不相同，请重新输入！");
                textBox_repwd.Focus();
                return;
            }

            UserManager m_UserManager = new UserManager();

            if (m_UserModel == null)
            {
                m_UserModel = new UserModel();
            }
            m_UserModel.user_loginName = textBox_loginName.Text.Trim();
            m_UserModel.user_name = textBox_name.Text.Trim();


            //m_UserModel.user_passwd = textBox_pwd.Text; 
            string PswStr = textBox_loginName.Text.Trim() + "---" + this.textBox_pwd.Text;  //密码由用户名加密码组成
            string _Des = DES.DESEncoder(PswStr, Encoding.Default, null, null);  //DES加密 
            string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码
            m_UserModel.user_passwd = EBase64;

            m_UserModel.user_phone = textBox_phone.Text.Trim();
            m_UserModel.user_meno = textBox_meno.Text.Trim();
            m_UserModel.user_class = comboBox_usertype.SelectedIndex + 1 ;
            if (m_UserModel.user_id == "")  //新建
            {
                string strWhere = string.Empty;
                strWhere = @" user_loginName='" + m_UserModel.user_loginName + @"' ";
                List<UserModel> m_userModelList = m_UserManager.GetModelList(strWhere);
                if (m_userModelList.Count > 0)
                { 
                    MessageBox.Show("登录名已存在，请重新输入！");
                    textBox_loginName.Focus();
                    return;
                }

                m_UserModel.user_status = 1;
                m_UserModel.user_createtime = DateTime.Now ;
                m_UserModel.user_lastlogintime = DateTime.Now ;
                m_UserModel.user_id = PublicConst.GenerateUUID();
                m_UserManager.Add(m_UserModel);
            }
            else
            {
                m_UserManager.Update(m_UserModel);

                if (isMyInfo)
                {
                    m_UserModel.user_passwd = textBox_pwd.Text;
                    CurrentUser._CurUserModel = m_UserModel;
                }


            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_EditPwd_Click(object sender, EventArgs e)
        {
            textBox_pwd.Enabled = true;
            textBox_repwd.Enabled = true;
        }
    }
}
