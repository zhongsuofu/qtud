using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
                strWhere += "  id<>''";//非冻结
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
                        lvi.SubItems.Add(data.id);
                        lvi.SubItems.Add(data.name);
                      
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



                strWhere += @" id<>'' and (  id like '%" + strQuery.Trim() + @"%'  or  name like '%" + strQuery + @"%' ) ";//非冻结
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
                        lvi.SubItems.Add(data.id);
                        lvi.SubItems.Add(data.name);
                        
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
                DateTime dt = DateTime.Now;
                int year = dt.Year - CurSelPatientInfo.birth.Year;
                strTemp = year.ToString();

            }
            label_yearold.Text = strTemp + " 岁";
        }

         
        #endregion


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
            SettingForm m_SettingForm = new SettingForm();
            DialogResult dlgResult1 = m_SettingForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK)
            { 
            }
        }
         

        private void button3_Click(object sender, EventArgs e)
        {
            ADDPatientInfo();
        }


        //修改
        private void button2_Click(object sender, EventArgs e)
        {
            if (m_CurSelPatientInfo == null || m_CurSelPatientInfo.id == "")
            {
                DialogResult ret = MessageBox.Show("请选择需要修改的患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                string site = listView_patList.SelectedItems[0].Text;
                string strid = listView_patList.SelectedItems[0].SubItems[1].Text;


                int i = 0;
                foreach (PatientInfoModel data in listPatientInfo)
                {
                    if (strid == data.id)
                    {
                        m_CurSelPatientInfo = data;   //当前选择的病人信息

                        ADDPatientInfo(m_CurSelPatientInfo);
                        break;
                    }
                    i++;
                }
            }
        }


        /// <summary>
        /// 删除患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if ( m_CurSelPatientInfo == null || m_CurSelPatientInfo.id == "")
            {
                DialogResult ret = MessageBox.Show("请选择需要删除的患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
            else
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

                        //===================================
                        //删除患者的其它信息
                        //===================================

                        m_CurSelPatientInfo = null;
                        listView_report.Items.Clear();
                        listReportInfo.Clear();
                    }
                    catch (System.Exception er)
                    {
                        throw er;
                    }
                    //------------------------------------------------

                }
            }
            
        }

        private void listView_patList_Click(object sender, EventArgs e)
        {
            if (listView_patList.SelectedItems.Count < 0)
                return;
            string strID = listView_patList.SelectedItems[0].SubItems[1].Text;

            int i = 0;
            foreach (PatientInfoModel data in listPatientInfo)
            {
                if (strID == data.id)
                {
                    m_CurSelPatientInfo = data;   //当前选择的病人信息
                    UpdateReportListBox();
                    UpdatePatientInfoEdit(m_CurSelPatientInfo);
                    break;
                }
                i++;
            }
        }

        private void button_Create_rep_Click(object sender, EventArgs e)
        { 
            if (m_CurSelPatientInfo == null || m_CurSelPatientInfo.id == "")
            {
                MessageBox.Show("请选择患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;
            }
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
            UpdateReport();
        }

        private void listView_report_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateReport(); 
        }
        private void UpdateReport( )
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
            else
            {
                MessageBox.Show("请选择报告", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
        }
        private void listView_patList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.ForeColor = Color.Black;
            e.Item.BackColor = SystemColors.Window;
            Thread.Sleep(200);
            if (listView_patList.FocusedItem != null)
            {
                listView_patList.FocusedItem.Selected = true;
            }
        }

        private void listView_patList_Validated(object sender, EventArgs e)
        {
            if (listView_patList.FocusedItem != null)
            {
                listView_patList.FocusedItem.BackColor = SystemColors.Highlight;
                listView_patList.FocusedItem.ForeColor = Color.White;
            }
        }

        private void listView_report_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.ForeColor = Color.Black;
            e.Item.BackColor = SystemColors.Window;

            if (listView_report.FocusedItem != null)
            {
                listView_report.FocusedItem.Selected = true;
            }
        }

        private void listView_report_Validated(object sender, EventArgs e)
        {
            if (listView_report.FocusedItem != null)
            {
                listView_report.FocusedItem.BackColor = SystemColors.Highlight;
                listView_report.FocusedItem.ForeColor = Color.White;
            }
        }


        //删除报告
        private void button_del_Click(object sender, EventArgs e)
        {
            if (this.listView_report.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in listView_report.SelectedItems)
                {
                    foreach (ReportInfoModel data in listReportInfo)
                    {
                        if (data.CreateDate.ToString() == listView_report.Items[lvi.Index].SubItems[1].Text)
                        {
                            if (data.uuid != "")
                            {
                                DialogResult ret = MessageBox.Show("\r\n确定删除报告 " + data.CreateDate + " 吗?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                                if (ret != DialogResult.OK)
                                {
                                    return;
                                }

                                try
                                {
                                    //删除曲线与文件的连接关系
                                    //tbl_curve_file_link
                                    tbl_curve_file_link_Manager m_file_link_manager = new tbl_curve_file_link_Manager();
                                    string strWhere = string.Empty;
                                    strWhere = @" curve_uuid in ( Select uuid from tbl_curve_info where  report_uuid='" + data.uuid + @"' )";
                                    m_file_link_manager.Delete(strWhere);

                                    //删除曲线信息
                                    tbl_curve_info_Manager m_curve_info_Manager = new tbl_curve_info_Manager();
                                    strWhere = @" report_uuid='" + data.uuid + @"' ";
                                    m_curve_info_Manager.Delete(strWhere);

                                    //删除报告信息
                                    strWhere = @" uuid='" + data.uuid + @"' ";
                                    Rim.Delete(strWhere);
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show(" 删除失败！ ");
                                }
                            }
                             
                            UpdateReportListBox();
                             
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的报告", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
            
        }

        //查看报告
        private void button_showRep_Click(object sender, EventArgs e)
        {
            if (this.listView_report.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvi in listView_report.SelectedItems)
                {
                    foreach (ReportInfoModel data in listReportInfo)
                    {
                        if (data.CreateDate.ToString() == listView_report.Items[lvi.Index].SubItems[1].Text)
                        {
                            if (data.uuid != "")
                            {
                                this.Hide();


                                Size curve3_Range = new Size(-100, 300);  //3条曲线范围：（最小值 ，最大值）
                                Size nl_Range = new Size(0, 3000);  //尿量范围：（最小值 ，最大值）
                                Size nll_Range = new Size(0, 100);  //尿流率范围：（最小值 ，最大值）

                                //---------------------------------------
                                TestDatas m_TestDatas = new TestDatas();

                                m_TestDatas.uuid = data.uuid;
                                m_TestDatas.strKS = data.ks;
                                m_TestDatas.strCH = data.ch;
                                m_TestDatas.strNLL = data.nlljcjg;
                                m_TestDatas.strNL = data.nlljcjg_nl;

                                m_TestDatas.str_RJ_YL = data.pgrlylcd;


                                m_TestDatas.str_nl_cg = data.pgrl_cg;
                                m_TestDatas.str_nl_zc = data.pgrl_zc;
                                m_TestDatas.str_nl_zd = data.pgrl_zd;

                                m_TestDatas.str_syx = data.pgsyx;
                                m_TestDatas.str_wdx = data.pgwdx;
                                m_TestDatas.str_tsjc = data.tsjc;

                                m_TestDatas.str_vlpp = data.vlpp;
                                m_TestDatas.str_dlpp = data.dlpp;
                                m_TestDatas.str_clpp = data.clpp;
                                m_TestDatas.str_aqrl = data.pgaqrl;

                                m_TestDatas.str_qtms = data.otherInfo;
                                m_TestDatas.str_ndlxzd = data.testresult;

                                //---------------------------------------
                                List<CurveDatas> m_PrintCurveDatas = new List<CurveDatas>();  //加入到打印列表里的
                                MainFrom_Curve m_CurveMainFrom = new MainFrom_Curve(m_CurSelPatientInfo, m_TestDatas);

                                //---------------------------------------
                                tbl_curve_info_Manager m_curve_info_Manager = new tbl_curve_info_Manager(); //曲线
                                string strWhere = string.Empty;
                                strWhere += @"  report_uuid='" + data.uuid + @"'  ORDER BY nindex";//非冻结  
                                List<tbl_curve_info_Model> m_curveInfoList = m_curve_info_Manager.GetModelList(strWhere);
                                foreach (tbl_curve_info_Model curveinfo_Model in m_curveInfoList)
                                {
                                    CurveDatas m_TempCurveDatas = new CurveDatas
                                    {
                                        StartTime = DateTime.Now, //(filename.Replace('.', ':')),
                                        endTime = DateTime.Now,
                                        showMode = new byte[5],  //全部显示
                                        FirstFileEndIndex = -1,
                                        str_range = string.Empty,

                                        list_Pabd = new List<StruData>(),
                                        list_Pdet = new List<StruData>(),
                                        list_Pves = new List<StruData>(),
                                        list_Wights = new List<StruData>(),
                                        list_ufr = new List<StruData>(),

                                        list_Files = new List<string>()

                                    };

                                    m_TempCurveDatas.StartTime = curveinfo_Model.starttime;
                                    m_TempCurveDatas.endTime = curveinfo_Model.endtime;
                                    m_TempCurveDatas.strMeno = curveinfo_Model.meno;
                                    m_TempCurveDatas.str_range = curveinfo_Model.rangs;

                                    string[] m_modeList = curveinfo_Model.strmode.Split(',');
                                    for(int t=0; t<m_modeList.Count() && t<5;t++)
                                    {
                                        if (m_modeList[t].ToString() != "")
                                            m_TempCurveDatas.showMode[t] = byte.Parse(m_modeList[t].ToString());
                                        else
                                            m_TempCurveDatas.showMode[t] =  0; 
                                    }
                                    //---------------------------------------
                                    //读取文件列表
                                    tbl_patient_checknum_file_info_Manager pcfimanager = new tbl_patient_checknum_file_info_Manager();
                                    strWhere = @"   uuid in (select file_uuid from tbl_curve_file_link where curve_uuid='" + curveinfo_Model.uuid + @"' )  ORDER BY path";
                                    List < tbl_patient_checknum_file_info_Model> m_checknum_file_info_list = pcfimanager.GetModelList(strWhere);
                                    foreach (tbl_patient_checknum_file_info_Model checknum_file_info_Model in m_checknum_file_info_list)
                                    {
                                        m_TempCurveDatas.list_Files.Add(checknum_file_info_Model.path);
                                    }
                                    //---------------------------------------
                                    if (m_TempCurveDatas.list_Files.Count > 0)
                                    {
                                        string strDataFile = m_TempCurveDatas.list_Files[0];
                                        int ipos = strDataFile.LastIndexOf(@"\ID");
                                        string strDate = strDataFile.Substring(ipos + 3);  // 2017-07-25 08.36.22.hold
                                        ipos = strDate.LastIndexOf('.');
                                        strDate = strDate.Substring(0, ipos);
                                        DateTime StartTime = DateTime.Parse(strDate.Replace('.', ':'));  // 2017-06-29 16:57:10

                                        System.TimeSpan   ts =m_TempCurveDatas.StartTime -StartTime;
                                        int nstartIndex = (int)(ts.TotalMilliseconds /500);
                                        System.TimeSpan te = m_TempCurveDatas.endTime - StartTime;
                                        int nendIndex = (int)(te.TotalMilliseconds / 500);

                                        m_CurveMainFrom.ReadFiles(m_TempCurveDatas.list_Files, ref m_TempCurveDatas, nstartIndex, nendIndex);
                                        m_PrintCurveDatas.Add(m_TempCurveDatas);
                                    }
                                 
                                } 
                                //---------------------------------------

                                MakeReportForm m_reportForm = new MakeReportForm(m_CurSelPatientInfo, m_PrintCurveDatas, m_TestDatas, curve3_Range, nl_Range, nll_Range);
                                DialogResult dlgResult = m_reportForm.ShowDialog();
                                if (dlgResult == DialogResult.Cancel)
                                {  
                                }

                                this.Show(); //显示  
                            } 
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择报告", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            }
            
            
        }

        private void CMainForm_ResizeEnd(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //panel1.Location = new Point((int)(this.panel_mid.Width - panel1.Width) / 2, panel1.Top);

        }
 

       
    }
}
