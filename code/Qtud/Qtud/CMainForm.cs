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
using System.IO;

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

        private int iOld = -1; //患者选择项

        private tbl_patient_checknum_link_Manager patient_checknum_link_Manager = new tbl_patient_checknum_link_Manager();
        private tbl_patient_checknum_file_info_Manager patient_checknum_file_info_Manager = new tbl_patient_checknum_file_info_Manager();  //文件存储管理


        private ReportInfoModel m_ReportInfoModel = new ReportInfoModel();
        private ReportInfoManager m_ReportInfoManager = new ReportInfoManager();  //报告

        private tbl_curve_info_Model curve_info_Model = new tbl_curve_info_Model();
        private tbl_curve_info_Manager m_curve_info_Manager = new tbl_curve_info_Manager(); //曲线

        private TestDatas m_TestDatas = new TestDatas();

        System.Timers.Timer t = new System.Timers.Timer(60 * 60 * 1000);//60 * 60 * 1000 实例化Timer类，设置间隔时间为1小时  ；
        private int m_nUseAbleSpace = 20;
        private string strIniFile = "config.ini"; 
        private string m_strFloder = @"D:\qtud_data\";

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
        
        ~CMainForm() 
        {
            t.Close();
        }

        #region 事件处理函数

        private void CMainFoam_Load(object sender, EventArgs e)
        {  
            this.WindowState = FormWindowState.Maximized;

            int nleft = (int)(this.panel_mid.Width - panel1.Width) / 2;
            if (nleft < 1)
                nleft = 1;
            panel1.Location = new Point(nleft, panel1.Top);

            label_userName.Text = CurrentUser._CurUserModel.user_name;
            if (label_userName.Text == "")
                label_userName.Text = "我的信息";

            //---------------------------------------------------
            strIniFile = Directory.GetCurrentDirectory() + "\\" + strIniFile;
         
            string UseAbleSpace = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "UseAbleSpace", null);
            if (string.IsNullOrEmpty(UseAbleSpace))
            {
                UseAbleSpace = "20";
            }
            try
            {
                m_nUseAbleSpace = int.Parse(UseAbleSpace);
            }
            catch (System.Exception ex)
            {
            	
            }

            string strDataDisk = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "DataDisk", null);
            if ( string.IsNullOrEmpty(strDataDisk))
                m_strFloder = @"D:\qtud_data\";    //备份文件夹 
            else
                m_strFloder = strDataDisk.Substring(0, 1) + @":\qtud_data\";   //备份文件夹 

            //---------------------------------------------------


            UpdateListView("");

            //----------------------------------------------------
            f();
            //定时器
            //System.Timers.Timer t = new System.Timers.Timer(60 * 60* 1000);//实例化Timer类，设置间隔时间为1000毫秒 就是1秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            //----------------------------------------------------

        }
         
        #endregion


        //定时器处理函数
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new TextOption(f));//invok 委托实现跨线程的调用
        }

        delegate void TextOption();//定义一个委托

        void f()
        {
            //label1.Text = DateTime.Now.ToString();//调用内容，并用lable1显示出来。。。
            bool ishaveSpace = false;
            string strFirstUseableDisk = string.Empty;
            string strDiskInfo = GetDriver(ref ishaveSpace, ref strFirstUseableDisk);
            if (!ishaveSpace)  //没有空间提示。
            {
                strDiskInfo = "磁盘空间不足，可能会影响到数据导出。\r\n\r\n" + strDiskInfo;
                MessageBox.Show(strDiskInfo);
            }
            else
            {
                if (strFirstUseableDisk != "" && m_strFloder.Substring(0, 3).ToUpper() != strFirstUseableDisk.ToUpper())
                {
                    try
                    {
                        INIOperationClass.INIWriteValue(strIniFile, "Setting", "DataDisk", strFirstUseableDisk);
                    }
                    catch (System.Exception ex)
                    {
                        //MessageBox.Show(ex.ToString()); 
                    }

                }
            }
        }

        //ishaveSpace 是否有可用空间， strFirstUseableDisk 第一个可用磁盘
        public string GetDriver(ref bool ishaveSpace,ref string strFirstUseableDisk )
        {
            long lsum = 0, ldr = 0;
            long gb = 1024 * 1024 * 1024;

            //系统盘
            String str = string.Empty;
            String nl = Environment.NewLine;
            //String query = "My system drive is %SystemDrive% and my system root is %SystemRoot%";
            //str = Environment.ExpandEnvironmentVariables(query);
            String query = "%SystemDrive%";
            str = Environment.ExpandEnvironmentVariables(query);


            strFirstUseableDisk = string.Empty;
            string strRes = string.Empty;
            ishaveSpace = false;  // 可用空间 是否到超过阈值，出发预警
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                //判断是否是固定磁盘  
                if (drive.DriveType == DriveType.Fixed)
                {
                    lsum = drive.TotalSize / gb;
                    ldr = drive.TotalFreeSpace / gb;
                    long per = 0;
                    if(drive.TotalSize > 0  && drive.TotalFreeSpace>0)
                        per= (long)drive.TotalFreeSpace *100 /drive.TotalSize;
                    if(per>0)
                        strRes += drive.Name + "  总空间 " + lsum.ToString() + "G   剩余空间 " + ldr.ToString() + "G   剩余空间占比 " + per.ToString() + "%\r\n";
                    else
                        strRes += drive.Name + "  总空间 " + lsum.ToString() + "G   剩余空间 " + ldr.ToString() + "G\r\n";


                    //判断是否是系统盘 
                    if (str != "" && drive.Name.IndexOf(str) > -1)  //系统盘
                    {
                        continue;
                    } 
                     
                    if(per >= m_nUseAbleSpace )
                    {
                        ishaveSpace = true;

                        if (strFirstUseableDisk == "")
                        {
                            strFirstUseableDisk = drive.Name;
                        }
                    }
                }
            }
            //progressBar1.Value = int.Parse((lsum - ldr).ToString());  
            //progressBar1.Maximum = int.Parse(lsum.ToString());  
            //lbMsg.Text = "磁盘" + disksrc + "的可用空间为" + ldr + "GB！";  

            return strRes;
        }  

        #region 功能函数

        //更新病人列表
        public void UpdateListView(string strUserId )
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            listView_patList.Items.Clear();
            listPatientInfo.Clear();
             

            m_CurSelPatientInfo = null;
            listView_report.Items.Clear();
            iOld = -1;

            listView_patList.Focus();

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

                        if (strUserId != "" && strUserId == data.id)
                        {
                            listView_patList.Items[i-1].Selected = true;
                        }
                        else if (i == 1)
                        {
                            listView_patList.Items[0].Selected = true; 
                        } 

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

            m_CurSelPatientInfo = null;
            listView_report.Items.Clear();
            listView_patList.Focus();

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

                        if (i == 1)
                        {
                            listView_patList.Items[0].Selected = true;
                        } 
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

            listView_report.Focus();
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

                        if (i == 1)
                        {
                            listView_report.Items[0].Selected = true;
                        } 

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
               UpdateListView(m_PatientInfoDlg.GetUserId());

            }
        }
         

        private void button_query_Click(object sender, EventArgs e)
        {
            if (textBox_queryWhere.Text == "")
            {
                UpdateListView("");
            }
            else
            {
                UpdateListViewWhere(textBox_queryWhere.Text.ToString().Trim());
            }
        }

        private void CMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ret = MessageBox.Show("确定退出系统吗？ " ,"退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
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
                UpdateListView("");
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
            UserMgrForm m_UserMgrForm = new UserMgrForm();
            DialogResult dlgResult1 = m_UserMgrForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK)
            { 
            }
            
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
            if ( m_CurSelPatientInfo == null || m_CurSelPatientInfo.id == "" ) // listView_patList.SelectedItems.Count < 1
            {
                DialogResult ret = MessageBox.Show("请选择需要修改的患者", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                //string site = listView_patList.SelectedItems[0].Text;
                string strid = m_CurSelPatientInfo.id; //  listView_patList.SelectedItems[0].SubItems[1].Text;


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

        #region 删除这个目录下的所有子目录和文件
        //删除这个目录下的所有文件及文件夹
        private void deleteTmpFiles(string strPath)
        {
            //删除这个目录下的所有子目录
            if (Directory.GetDirectories(strPath).Length > 0)
            {
                foreach (string var in Directory.GetDirectories(strPath))
                {
                    //DeleteDirectory(var);
                    if (!string.IsNullOrEmpty(var) && Directory.Exists(var))
                        Directory.Delete(var, true);
                    //DeleteDirectory(var);
                }
            }
            //删除这个目录下的所有文件
            if (Directory.GetFiles(strPath).Length > 0)
            {
                foreach (string var in Directory.GetFiles(strPath))
                {
                    if (!string.IsNullOrEmpty(var) && File.Exists(var))
                        File.Delete(var);
                }
            }
        }
        #endregion

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
                DialogResult ret = MessageBox.Show("删除患者后，相关的检查数据都会删除。\r\n确定删除患者 \"" + m_CurSelPatientInfo.name + "\" 吗?", "删除确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (ret == DialogResult.OK)
                {

                    //------------------------------------------------
                    string strWhere = string.Empty;
                    string sRet = string.Empty;
                    try
                    { 
                        //===================================
                        //删除患者的其它信息
                        strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' ";
                        List<tbl_patient_checknum_link_Model> tempModelist = patient_checknum_link_Manager.GetModelList(strWhere);

                        string strpatientPath = string.Empty;
                        foreach (tbl_patient_checknum_link_Model tempmodel in tempModelist)
                        {
                            string[] pathArr = tempmodel.txtPath.Split('\\');

                            //删除文件记录
                            strWhere = @" check_uuid in (select uuid from tbl_patient_checknum_link where patient_uuid='" + m_CurSelPatientInfo.uuid + @"' and  checkNum='" + pathArr[3] + @"' ) ";
                            patient_checknum_file_info_Manager.Delete(strWhere);

                            //删除文件关联记录
                            strWhere = @" patient_uuid='" + m_CurSelPatientInfo.uuid + @"' and  checkNum='" + pathArr[3] + @"' ";
                            patient_checknum_link_Manager.Delete(strWhere);

                            //删除文件夹下文件
                            string  strPath = pathArr[0] + "\\" + pathArr[1] + "\\" + pathArr[2] + "\\" + pathArr[3];

                            //删除文件夹
                            if (!string.IsNullOrEmpty(strPath) && Directory.Exists(strPath))
                            {
                                deleteTmpFiles(strPath); 
                                Directory.Delete(strPath, true);
                            }

                            strpatientPath = pathArr[0] + "\\" + pathArr[1] + "\\" + pathArr[2];

                            
                        }
                        if (!string.IsNullOrEmpty(strpatientPath) && Directory.Exists(strpatientPath))
                        {
                            Directory.Delete(strpatientPath, true);
                        }
                        //===================================

                        tbl_curve_file_link_Manager m_file_link_manager = new tbl_curve_file_link_Manager();
                        strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' ";
                        List< ReportInfoModel> m_ReportList = m_ReportInfoManager.GetModelList(strWhere);
                        foreach (ReportInfoModel tempmodel in m_ReportList)
                        {
                            //删除之前的曲线与文件的连接关系
                            //tbl_curve_file_link
                            strWhere = @" curve_uuid in ( Select uuid from tbl_curve_info where  report_uuid='" + tempmodel.uuid + @"' )";
                            m_file_link_manager.Delete(strWhere);

                            //删除之前的曲线信息
                            strWhere = @" report_uuid='" + tempmodel.uuid + @"' ";
                            m_curve_info_Manager.Delete(strWhere);
                        }
                        strWhere = @"patient_uuid='" + m_CurSelPatientInfo.uuid + @"' ";
                        m_ReportInfoManager.Delete(strWhere);
                         
                       
                        //===================================

                        pim.Delete(m_CurSelPatientInfo.uuid);  //删除病人信息
                        textBox_queryWhere.Text = string.Empty;

                        m_CurSelPatientInfo = null;
                        listView_report.Items.Clear();
                        listReportInfo.Clear();
                        UpdateListView("");
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
            //高亮显示
            //e.Item.ForeColor = Color.Black;
            //e.Item.BackColor = SystemColors.Window;

            //if (listView_patList.FocusedItem != null)
            //{
            //    listView_patList.FocusedItem.Selected = true;
            //}
            if (listView_patList.SelectedIndices.Count > 0) //若有选中项 
            {
                if (iOld == -1)
                {
                    listView_patList.Items[listView_patList.SelectedIndices[0]].BackColor = Color.FromArgb(49, 106, 197); //设置选中项的背景颜色 
                    listView_patList.Items[listView_patList.SelectedIndices[0]].ForeColor = Color.White; //设置选中项的背景颜色 
                    iOld = listView_patList.SelectedIndices[0]; //设置当前选中项索引 
                }
                else
                {
                    if (listView_patList.SelectedIndices[0] != iOld)
                    {
                        listView_patList.Items[listView_patList.SelectedIndices[0]].BackColor = Color.FromArgb(49, 106, 197); //设置选中项的背景颜色 
                        listView_patList.Items[listView_patList.SelectedIndices[0]].ForeColor = Color.White; //设置选中项的背景颜色 

                        listView_patList.Items[iOld].BackColor = SystemColors.Window; //恢复默认背景色 
                        listView_patList.Items[iOld].ForeColor = Color.Black; //恢复默认背景色 
                        
                        iOld = listView_patList.SelectedIndices[0]; //设置当前选中项索引 
                    }
                }
            }
            else //若无选中项 
            {
                listView_patList.Items[iOld].BackColor = SystemColors.Window; // Color.FromArgb(239, 248, 250); //恢复默认背景色 
                listView_patList.Items[iOld].ForeColor = Color.Black;  
                iOld = -1; //设置当前处于无选中项状态 
            } 

            //------------------------------------------------
            Thread.Sleep(200);

            if (listView_patList.SelectedItems.Count < 1)
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

        private void CMainForm_SizeChanged(object sender, EventArgs e)
        {
            int nleft = (int)(this.panel_mid.Width - panel1.Width) / 2;
            if (nleft < 1)
                nleft =   1;
            panel1.Location = new Point(nleft, panel1.Top);

        }

        private void label_userName_Click(object sender, EventArgs e)
        {
            UserForm m_UserForm = new UserForm(CurrentUser._CurUserModel, true );
            DialogResult dlgResult1 = m_UserForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK)
            {
                if (CurrentUser._CurUserModel.user_name == null || CurrentUser._CurUserModel.user_name == string.Empty)
                    label_userName.Text = "我的信息";
                else
                    label_userName.Text = CurrentUser._CurUserModel.user_name;
            }
        }
 

       
    }
}
