using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Qtud.DBManage.Manager;
using Qtud.DBManage.Model;
using Qtud.SystemCommon; 

namespace Qtud.Qtud
{
    public partial class UserMgrForm : Form
    {
        UserManager m_UserManager = new UserManager();
        List<UserModel> m_userModelList = new List<UserModel>();
        UserModel m_CurUserModel = new UserModel();
        
        public UserMgrForm()
        {
            InitializeComponent();
        }

        private void UserMgrForm_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void button_query_Click(object sender, EventArgs e)
        {
            if (textBox_queryWhere.Text == "")
            {
                UpdateList();
            }
            else
            {
                UpdateListWhere(textBox_queryWhere.Text.ToString().Trim());
            }
        }

        private void label_msg_Click(object sender, EventArgs e)
        {
            textBox_queryWhere.Focus();

        }

        private void textBox_queryWhere_TextChanged(object sender, EventArgs e)
        {
            label_msg.Visible = textBox_queryWhere.Text.Trim().Length < 1;

            if (textBox_queryWhere.Text.Trim() == "")
                UpdateList();              

        }

        private void button_New_user_Click(object sender, EventArgs e)
        {
            UserForm m_UserForm = new UserForm(null);
            DialogResult dlgResult1 = m_UserForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK)
            {
                UpdateList();
            }
             
        }

        private void edit_user_func( )
        {
            if (listView_userList.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要修改的项 ", "提示", MessageBoxButtons.OKCancel);
                return;
            }
            else
            {
                UserForm m_UserForm = new UserForm(m_CurUserModel);
                DialogResult dlgResult1 = m_UserForm.ShowDialog();
                if (dlgResult1 == DialogResult.OK)
                {
                    UpdateList();
                }
            }
        }
        private void button_edit_user_Click(object sender, EventArgs e)
        {
            edit_user_func();
            
        }

        private void button_del_user_Click(object sender, EventArgs e)
        {
            if (listView_userList.SelectedItems.Count > 0)
            {
                if (m_CurUserModel.user_loginName == CurrentUser._CurUserModel.user_loginName)
                {
                    MessageBox.Show("不允许删除自己 " , "提示", MessageBoxButtons.OKCancel );
                    return;
                }
                DialogResult ret = MessageBox.Show("确认删除用户 " + m_CurUserModel.user_name + " 吗?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (ret == DialogResult.OK)
                {

                    //------------------------------------------------
                    string strWhere = string.Empty;
                    string sRet = string.Empty;
                    try
                    {
                        m_UserManager.Delete(m_CurUserModel.user_id);
                        UpdateList();
                        textBox_queryWhere.Text = string.Empty;
                        m_CurUserModel = null;
                    }
                    catch (System.Exception er)
                    {
                        throw er;
                    }
                    //------------------------------------------------

                }
            }
            else
            {
                DialogResult ret = MessageBox.Show("请选择需要删除的项", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
        }

        private void UpdateList()
        {
            m_userModelList.Clear();
            listView_userList.Items.Clear();
            string strWhere = string.Empty;
            strWhere = " user_status <>2 order by user_lastlogintime desc ";
            m_userModelList = m_UserManager.GetModelList(strWhere);
            int i = 0;
            foreach (UserModel model in m_userModelList)
            {
                i++;

                if (model.user_passwd != "")
                {
                    //解密
                    string DBase64 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(model.user_passwd));  //Base解码
                    string strtemp = DES.DESDecoder(DBase64, Encoding.Default, null, null);  //DES解码 
                    strtemp = strtemp.Replace(model.user_loginName + "---", "");
                    model.user_passwd = strtemp;
                } 
                  
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(model.user_loginName);
                lvi.SubItems.Add(model.user_name);

                if (model.user_class == 1)
                    lvi.SubItems.Add("系统管理员");
                else
                    lvi.SubItems.Add("普通用户");

                lvi.SubItems.Add(model.user_phone);
                lvi.SubItems.Add(model.user_createtime.ToString());
                if (model.user_lastlogintime < model.user_createtime)
                    lvi.SubItems.Add("");
                else
                    lvi.SubItems.Add(model.user_lastlogintime.ToString());
                lvi.SubItems.Add(model.user_meno );
                this.listView_userList.Items.Add(lvi);
            } 
        }

        public void UpdateListWhere(string strQuery)
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            m_userModelList.Clear();
            listView_userList.Items.Clear();
            try
            {
                //string _Des = DES.DESEncoder(strQuery.Trim(), Encoding.Default, null, null);  //DES加密 
                //string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码
                 

                strWhere = @"  user_status <>2  and ( user_name like '%" + strQuery.Trim() + @"%'  or  user_loginName like '%" + strQuery + @"%' ) ";//非冻结
                strWhere += "   order by user_lastlogintime desc ";

                m_userModelList = m_UserManager.GetModelList(strWhere);

                if (0 < m_userModelList.Count)//没找到数据
                {
                    int i = 0;
                    foreach (UserModel model in m_userModelList)
                    {
                        i++;

                        if (model.user_passwd != "")
                        {
                            //解密
                            string DBase64 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(model.user_passwd));  //Base解码
                            string strtemp = DES.DESDecoder(DBase64, Encoding.Default, null, null);  //DES解码 
                            strtemp = strtemp.Replace(model.user_loginName + "---", "");
                            model.user_passwd = strtemp;
                        }

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.Add(model.user_loginName);
                        lvi.SubItems.Add(model.user_name);

                        if (model.user_class == 1)
                            lvi.SubItems.Add("系统管理员");
                        else
                            lvi.SubItems.Add("普通用户");

                        lvi.SubItems.Add(model.user_phone);
                        lvi.SubItems.Add(model.user_createtime.ToString());
                        lvi.SubItems.Add(model.user_lastlogintime.ToString());
                        lvi.SubItems.Add(model.user_meno);
                        this.listView_userList.Items.Add(lvi);
                    } 
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        private void listView_userList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_userList.SelectedItems.Count == 0)
                return;
            else
            {
                string site = listView_userList.SelectedItems[0].Text;
                string loginName = listView_userList.SelectedItems[0].SubItems[1].Text;

                int i = 0;
                foreach (UserModel model in m_userModelList)
                {
                    if (loginName == model.user_loginName)
                    {
                        m_CurUserModel = model;   //当前选择的用户信息

                        break;
                    }
                    i++;
                }
            }
        }

        private void listView_userList_DoubleClick(object sender, EventArgs e)
        {
            edit_user_func();
        }

        private void button_return_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        //防止加载闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }
    }
}
