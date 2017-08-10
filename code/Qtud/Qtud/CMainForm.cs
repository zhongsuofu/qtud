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
        private ReportInfoManager Rim = new ReportInfoManager();

        private List<PatientInfoModel> listPatientInfo = new List<PatientInfoModel>();  //病人数据列表
        private List<ReportInfoModel> listReportInfo = new List<ReportInfoModel>();    //报告数据列表
        private PatientInfoModel m_CurSelPatientInfo = new PatientInfoModel();    //当前选择的病人

        private TestDatas m_TestDatas = new TestDatas();

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
            panel1.Location = new Point((int)(this.panel_mid.Width - panel1.Width) / 2, panel1.Top);

            UpdateListView();

        }
         
        #endregion



        #region 功能函数

        //更新病人列表
        public void UpdateListView()
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            listView_patList.Items.Clear();
            listPatientInfo.Clear();
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

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.Add(data.name);
                        lvi.SubItems.Add(data.cardid);
                      
                        this.listView_patList.Items.Add(lvi);

                    }


                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }


        public void UpdateListViewWhere(string strQuery)
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            listView_patList.Items.Clear();
            listPatientInfo.Clear();
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


                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.Add(data.name);
                        lvi.SubItems.Add(data.cardid);
                        
                        this.listView_patList.Items.Add(lvi);
                    }
                }

            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public void UpdateReportListBox()
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            listView_report.Items.Clear();
            listReportInfo.Clear();
            try
            {
                strWhere += @" patient_uuid='" + m_CurSelPatientInfo.uuid + @"' ";//非冻结
                strWhere += @"   order by CreateDate desc ";

                listReportInfo = Rim.GetModelList(strWhere);
                if (0 < listReportInfo.Count)//没找到数据
                {
                    int i = 0;
                    foreach (ReportInfoModel data in listReportInfo)
                    {
                        i++;

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.Add(data.CreateDate.ToString());
                        
                        this.listView_report.Items.Add(lvi);

                    }

                    //if (CurSelIndex == -1)
                    //    CurSelIndex = 0;
                    //this.parient_list.SelectedIndex = CurSelIndex;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
  
        private void UpdatePatientInfoEdit(PatientInfoModel CurSelPatientInfo)
        {
            label_cardid.Text = CurSelPatientInfo.cardid;
           
            label_name.Text = CurSelPatientInfo.name;
            label_phone.Text = CurSelPatientInfo.phone;
            textBox_meno.Text = CurSelPatientInfo.meno;
            
            if (CurSelPatientInfo.sex == 1)
                label_sex.Text = "男";
            else
                label_sex.Text = "女";

            label_birth.Text = CurSelPatientInfo.birth.ToLongDateString();
            label_id.Text = CurSelPatientInfo.id ;

            string strTemp = string.Empty;
            if (CurSelPatientInfo.birth != null)
            {
                string str = CurSelPatientInfo.cardid.Substring(6, 4);
                if (str != "")
                {
                    DateTime dt = DateTime.Now;
                    int year = dt.Year - CurSelPatientInfo.birth.Year;
                    strTemp = year.ToString();
                }

            }
            label_yearold.Text = strTemp + "  岁";
        }

         
        #endregion



        private void ADDPatientInfo(PatientInfoModel _model = null)
        {
            PatientInfoDlg m_PatientInfoDlg = new PatientInfoDlg(_model);
            DialogResult dlgResult = m_PatientInfoDlg.ShowDialog();
            if (dlgResult == DialogResult.OK)  //添加完成
            {
                UpdateListView();

            }
        }
         

        private void button_query_Click(object sender, EventArgs e)
        {
            if (textBox_queryWhere.Text == "")
            {
                UpdateListView();
            }
            else
            {
                UpdateListViewWhere(textBox_queryWhere.Text.ToString().Trim());
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
                UpdateListView();
            }
        }

        private void label_msg_Click(object sender, EventArgs e)
        {
            textBox_queryWhere.Focus();
        }
         
        private void button_patient_manage_Click(object sender, EventArgs e)
        {
            //this.Hide(); //显示  
            //PatientManageDlg m_ReportListView = new PatientManageDlg();
            //DialogResult dlgResult = m_ReportListView.ShowDialog();
            //if (dlgResult == DialogResult.OK)
            //{

            //}
            //UpdateListBox();

            //this.Show(); //显示  
            
        }

        private void button_user_manage_Click(object sender, EventArgs e)
        {

        }

        private void button_sys_Setting_Click(object sender, EventArgs e)
        {

        }
         

        private void button3_Click(object sender, EventArgs e)
        {
            ADDPatientInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView_patList.SelectedItems.Count == 0)
            {
                DialogResult ret = MessageBox.Show("请选择需要修改的患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                string site = listView_patList.SelectedItems[0].Text;
                string cardid = listView_patList.SelectedItems[0].SubItems[2].Text;


                int i = 0;
                foreach (PatientInfoModel data in listPatientInfo)
                {
                    if (cardid == data.cardid)
                    {
                        m_CurSelPatientInfo = data;   //当前选择的病人信息

                        ADDPatientInfo(m_CurSelPatientInfo);
                        break;
                    }
                    i++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView_patList.SelectedItems.Count > 0)
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
                        UpdateListView();
                        textBox_queryWhere.Text = string.Empty;
                        m_CurSelPatientInfo = null;
                        UpdateReportListBox();
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
                DialogResult ret = MessageBox.Show("请选择需要删除的患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
        }

        private void listView_patList_Click(object sender, EventArgs e)
        {
            if (listView_patList.SelectedItems.Count == 0)
                return;
            else
            {
                string cardid = listView_patList.SelectedItems[0].SubItems[2].Text;

                int i = 0;
                foreach (PatientInfoModel data in listPatientInfo)
                {
                    if (cardid == data.cardid)
                    {
                        m_CurSelPatientInfo = data;   //当前选择的病人信息
                        UpdateReportListBox();
                        UpdatePatientInfoEdit(m_CurSelPatientInfo);
                        break;
                    }
                    i++;
                }
            }
        }

        private void button_Create_rep_Click(object sender, EventArgs e)
        { 
            this.Hide();
            TestResultDlg m_TestResForm = new TestResultDlg(m_CurSelPatientInfo, null);
            DialogResult dlgResult1 = m_TestResForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK)
            {
                m_TestDatas = m_TestResForm.m_TestDatas;
                m_CurSelPatientInfo = m_TestResForm.m_CurSelPatientInfo;
            }
            UpdateReportListBox();

            this.Show();
             
        }


        private void button_edit_report_Click(object sender, EventArgs e)
        {

        }

        private void listView_report_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView_report.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in listView_report.SelectedItems)
                {
                    foreach (ReportInfoModel data in listReportInfo)
                    {
                        if (data.CreateDate.ToString() == listView_report.Items[lvi.Index].SubItems[1].Text)
                        {
                            Hide();
                            TestResultDlg m_TestResForm = new TestResultDlg(m_CurSelPatientInfo, data);
                            DialogResult dlgResult1 = m_TestResForm.ShowDialog();
                            if (dlgResult1 == DialogResult.OK)
                            {
                                m_TestDatas = m_TestResForm.m_TestDatas;
                                m_CurSelPatientInfo = m_TestResForm.m_CurSelPatientInfo;
                            }
                            UpdateReportListBox();

                            Show();

                            //MainFrom2 m_MainFrom2 = new MainFrom2(m_CurSelPatientInfo, m_TestDatas);
                            //DialogResult dlgResult = m_MainFrom2.ShowDialog();
                            //if (dlgResult == DialogResult.OK)
                            //{

                            //}


                            break;
                        }
                    }
                }

            }
        }
 

       
    }
}
