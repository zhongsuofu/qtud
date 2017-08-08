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
    public partial class CMainForm : Form
    {

        #region 变量
        private UserManager um = new UserManager();
        private PatientInfoManager pim = new PatientInfoManager();

        private List<PatientInfoModel> listPatientInfo;  //病人数据列表
        private PatientInfoModel m_CurSelPatientInfo;    //当前选择的病人
        private int CurSelIndex  = -1;  //当前选择的病人列表序号
  
        #endregion

        public CMainForm()
        {
            InitializeComponent();

            //----------------------------------------------------
            //登录界面
            this.Hide();
            loginDlg login = new loginDlg();

            DialogResult dlgResult = login.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                this.Show(); //显示  
            }
            else
            {
                System.Environment.Exit(0);  //关闭程序
                Close();

            }
            //----------------------------------------------------
        }

        #region 事件处理函数

        private void CMainFoam_Load(object sender, EventArgs e)
        {  
            this.WindowState = FormWindowState.Maximized;
            panel1.Location = new Point((int)(this.Width - panel1.Width) / 2, panel1.Top);

            UpdateListBox(); 

        }

        private void parient_list_SelectedValueChanged(object sender, EventArgs e)
        {
            GetSelPrtientInfo();

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            SavePatientInfo();
        }


        private void parient_list_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GetSelPrtientInfo();
            this.Hide();

            ReportListView m_ReportListView = new ReportListView(m_CurSelPatientInfo);
            DialogResult dlgResult = m_ReportListView.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {

            }
            

            //MainFrom2 m_MainFrom2 = new MainFrom2(m_CurSelPatientInfo);
            //DialogResult dlgResult = m_MainFrom2.ShowDialog();
            //if (dlgResult == DialogResult.OK)
            //{

            //}
            this.Show(); //显示  
        }


        #endregion



        #region 功能函数

        private void GetSelPrtientInfo()
        {
            CurSelIndex = parient_list.SelectedIndex;
            string str = parient_list.Items[parient_list.SelectedIndex].ToString();

            int i = 0;
            foreach (PatientInfoModel data in listPatientInfo)
            {
                if (parient_list.SelectedIndex == i && str.IndexOf(data.name) > -1)
                {
                    m_CurSelPatientInfo = data;   //当前选择的病人信息

                    UpdatePatientInfoEdit(m_CurSelPatientInfo);
                    break;
                }
                i++;
            }
        }

        public void UpdateListBoxWhere(string strQuery)
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            parient_list.Items.Clear();
            //listPatientInfo.Clear();
            try
            {
                //string _Des = DES.DESEncoder(strQuery.Trim(), Encoding.Default, null, null);  //DES加密 
                //string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码



                strWhere += @" cardid<>'' and ( cardid like '%" + strQuery.Trim() + @"%'  or  name like '%" + strQuery + @"%' ) ";//非冻结
                strWhere += "   order by lastchecktime desc  ";

                listPatientInfo = pim.GetModelList(strWhere);
                if (0 < listPatientInfo.Count)//没找到数据
                {
                    int i = 0;
                    foreach (PatientInfoModel data in listPatientInfo)
                    {
                        i++;

                        //解密
                        //string DBase64 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(data.cardid));  //Base解码
                        //string strcardid = DES.DESDecoder(DBase64, Encoding.Default, null, null);  //DES解码 
                        //data.cardid = strcardid;
                         
                        
                        string str = data.name;
                        if (data.name.Length < 5)
                        {
                            str += "         ";
                            str = str.Substring(0, 5);
                        }
                        if (data.cardid.Length > 9)
                        {
                            str += " (" + data.cardid.Substring(0, 4) + @"***" + data.cardid.Substring(data.cardid.Length - 5) + ")";

                        }
                        else
                            str += " (" + data.cardid + ")";

                        //判断是否添加到listbox1
                        if (!this.parient_list.Items.Contains(str))
                        {
                            this.parient_list.Items.Add(str);
                        }

                    }

                    if (CurSelIndex == -1)
                        CurSelIndex = 0;
                    this.parient_list.SelectedIndex = CurSelIndex;
                }
                else
                {
                    DialogResult ret = MessageBox.Show("病人信息不存在，是否现在添加", "添加", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    if (ret == DialogResult.OK)
                    {
                        ADDPatientInfo();
                        textBox_queryWhere.Text = "";

                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        //更新病人列表
        public void UpdateListBox()
        {
            CurSelIndex  = -1;
            string strWhere = string.Empty;
            string sRet = string.Empty;
            parient_list.Items.Clear();
            //listPatientInfo.Clear();
            try
            {
                strWhere += " cardid<>''";//非冻结
                strWhere += "   order by lastchecktime desc limit 0,50 ";

                listPatientInfo = pim.GetModelList(strWhere);
                if (0 < listPatientInfo.Count)//没找到数据
                {
                    int i = 0;
                    foreach (PatientInfoModel data in listPatientInfo)
                    {
                        i++;

                        //解密
                        //string DBase64 = System.Text.Encoding.Default.GetString(Convert.FromBase64String(data.cardid));  //Base解码
                        //string strcardid = DES.DESDecoder(DBase64, Encoding.Default, null, null);  //DES解码 
                        //data.cardid = strcardid;
                        data.cardid = data.cardid;

                        string str = data.name;
                        if (data.name.Length < 5)
                        {
                            str += "         ";
                            str = str.Substring(0, 5);
                        }
                        if (data.cardid.Length > 9)
                        {
                            str += " (" + data.cardid.Substring(0, 4) + @"***" + data.cardid.Substring(data.cardid.Length - 5) + ")";

                        }
                        else
                            str += " (" + data.cardid + ")";

                        //判断是否添加到listbox1
                        if (!this.parient_list.Items.Contains(str))
                        {
                            this.parient_list.Items.Add(str);
                        }

                    }

                    if (CurSelIndex == -1)
                        CurSelIndex = 0;
                    this.parient_list.SelectedIndex = CurSelIndex;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        private void UpdatePatientInfoEdit(PatientInfoModel CurSelPatientInfo)
        {
            textBox_cardid.Text = CurSelPatientInfo.cardid;
            textBox_cardid.Enabled = false; 
            textBox_name.Text = CurSelPatientInfo.name;
            textBox_phone.Text = CurSelPatientInfo.phone;
            textBox_meno.Text = CurSelPatientInfo.meno;
            textBox1_bs.Text = CurSelPatientInfo.bs;
            
            if (CurSelPatientInfo.sex == 1)
                radioBtn_man.Checked = true;
            else
                radioBTN_woman.Checked = true;
        }


        private void SavePatientInfo()
        {
            try
            {
                if (this.textBox_cardid.Text.Trim() == "")
                {
                    MessageBox.Show("请输入身份证号!", "输入提示");
                    textBox_cardid.Focus();
                    return;
                }

                if (this.textBox_name.Text.Trim() == "")
                {
                    MessageBox.Show("请输入姓名!", "输入提示");
                    textBox_name.Focus();
                    return;
                }

                PatientInfoModel model = new PatientInfoModel();
                model.name = this.textBox_name.Text.Trim();

                // 身份证号做DEs加密和Base64 编码  保存
                //string _Des = DES.DESEncoder(this.textBox_cardid.Text.Trim(), Encoding.Default, null, null);  //DES加密 
                //string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码
                model.cardid = this.textBox_cardid.Text.Trim(); // EBase64;

                model.sex = 0;
                if (radioBtn_man.Checked)
                    model.sex = 1;

                model.phone = this.textBox_phone.Text.Trim();
                model.meno = this.textBox_meno.Text.Trim();

                PatientInfoManager pim = new PatientInfoManager();
                if (textBox_cardid.Enabled)  //新增
                {
                    if (pim.Exists(model.cardid))
                    {
                        MessageBox.Show("身份证号已经存在,不能重复添加", "输入提示");
                        textBox_cardid.Focus();
                        return;
                    }
                    model.createtime = DateTime.Now;
                    model.lastchecktime = DateTime.Now;
                    pim.Add(model);
                    MessageBox.Show(" 添加成功！ ");


                }
                else  //修改
                {
                    pim.Update(model);

                    MessageBox.Show(" 修改成功！ ");
                }

                UpdateListBox();  //更新病人列表
            }
            catch (System.Exception e)
            {
                MessageBox.Show(" 保存失败！ ");
            }
        }

        #endregion



        private void ADDPatientInfo(PatientInfoModel _model = null)
        {
            PatientInfoDlg m_PatientInfoDlg = new PatientInfoDlg(_model);
            DialogResult dlgResult = m_PatientInfoDlg.ShowDialog();
            if (dlgResult == DialogResult.OK)  //添加完成
            {
                UpdateListBox();

            }
        }
         

        private void button_query_Click(object sender, EventArgs e)
        {
            if (textBox_queryWhere.Text == "")
            {
                UpdateListBox();
            }
            else
            {
                UpdateListBoxWhere(textBox_queryWhere.Text.ToString().Trim());
            }
        }

        private void CMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("确认退出系统吗 " ,"退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (ret == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            
        }

        private void textBox_queryWhere_TextChanged(object sender, EventArgs e)
        {
            label_msg.Visible = textBox_queryWhere.Text.Trim().Length < 1;

            if (textBox_queryWhere.Text.Trim() == "")
            {
                UpdateListBox();
            }
        }

        private void label_msg_Click(object sender, EventArgs e)
        {
            textBox_queryWhere.Focus();
        }

        private void new_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADDPatientInfo();

        }

        private void edit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_CurSelPatientInfo != null && m_CurSelPatientInfo.uuid != "")
            {
                ADDPatientInfo(m_CurSelPatientInfo);
            }
        }

        private void delete_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.parient_list.SelectedIndex > -1)
            {
                DialogResult ret = MessageBox.Show("删除病人后，相关的检查数据都会删除。\r\n确定删除病人 " + m_CurSelPatientInfo.name + " 吗?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (ret == DialogResult.OK)
                {

                    //------------------------------------------------
                    string strWhere = string.Empty;
                    string sRet = string.Empty;
                    try
                    {
                        pim.Delete(m_CurSelPatientInfo.uuid);
                        CurSelIndex = -1;
                        UpdateListBox();
                        textBox_queryWhere.Text = string.Empty;

                    }
                    catch (System.Exception er)
                    {
                        throw er;
                    }
                    //------------------------------------------------

                }
            }
        }

        private void button_patient_manage_Click(object sender, EventArgs e)
        {
            this.Hide(); //显示  
            PatientManageDlg m_ReportListView = new PatientManageDlg();
            DialogResult dlgResult = m_ReportListView.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {

            }
            UpdateListBox();

            this.Show(); //显示  
            
        }

        private void button_user_manage_Click(object sender, EventArgs e)
        {

        }

        private void button_sys_Setting_Click(object sender, EventArgs e)
        {

        }
 

       
    }
}
