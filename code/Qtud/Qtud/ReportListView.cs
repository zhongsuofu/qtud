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
    public partial class ReportListView : Form
    {
        private PatientInfoModel m_CurSelPatientInfo;    //当前选择的病人
        private ReportInfoManager pim = new ReportInfoManager();
        private List<ReportInfoModel> listReportInfo = new List<ReportInfoModel>();    //报告数据列表
         private  TestDatas m_TestDatas = new TestDatas();

        public ReportListView(PatientInfoModel _CurSelPatientInfo)
        {
            InitializeComponent();
            m_CurSelPatientInfo = _CurSelPatientInfo;
        }

        private void button_Create_rep_Click(object sender, EventArgs e)
        {
            this.Hide();

            TestResultDlg m_TestResForm = new TestResultDlg(m_CurSelPatientInfo, null );
            DialogResult dlgResult1 = m_TestResForm.ShowDialog();
            if (dlgResult1 == DialogResult.OK  )
            {
                m_TestDatas = m_TestResForm.m_TestDatas;
                m_CurSelPatientInfo = m_TestResForm.m_CurSelPatientInfo;
            }
            UpdateListBox(); 
             

            //MakeReportForm m_reportForm = new MakeReportForm(m_CurSelPatientInfo, m_PrintCurveDatas, m_TestDatas);
            //DialogResult dlgResult = m_reportForm.ShowDialog();
            //if (dlgResult == DialogResult.Cancel)
            //{
            //    dlgResult = DialogResult.Cancel;
            //    Close();
            //    return;
            //}

            this.Show(); //显示  
        }

        private void button_up_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ReportListView_Load(object sender, EventArgs e)
        {
            this.listView_report.Columns.Add("序 号", 80, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("日 期", 260, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("床号", 100, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("最大尿流率", 150, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("排尿量", 100, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("膀胱顺应性", 150, HorizontalAlignment.Center); //一步添加 
            this.listView_report.Columns.Add("膀胱稳定性", 200, HorizontalAlignment.Center); //一步添加 
            
            UpdateListBox();
        }


        public void UpdateListBox()
        {
            string strWhere = string.Empty;
            string sRet = string.Empty;
            listView_report.Items.Clear();
            listReportInfo.Clear();
            try
            {
                strWhere += @" patient_uuid='" + m_CurSelPatientInfo.uuid+ @"' ";//非冻结
                strWhere += @"   order by CreateDate ";

                listReportInfo = pim.GetModelList(strWhere);
                if (0 < listReportInfo.Count)//没找到数据
                {
                    int i = 0;
                    foreach (ReportInfoModel data in listReportInfo)
                    {
                        i++;

                        ListViewItem lvi = new ListViewItem();  
                        lvi.Text =  i.ToString();
                        lvi.SubItems.Add(data.CreateDate.ToString());
                        lvi.SubItems.Add(data.ch);
                        lvi.SubItems.Add(data.nlljcjg.ToString());
                        lvi.SubItems.Add(data.nlljcjg_nl.ToString());
                        lvi.SubItems.Add(data.pgsyx);
                        lvi.SubItems.Add(data.pgwdx);
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

        private void listView_report_DoubleClick(object sender, EventArgs e)
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
                            UpdateListBox();
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

        private void ReportListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
