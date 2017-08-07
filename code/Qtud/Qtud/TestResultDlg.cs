using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Qtud.DBManage.Model;
using Qtud.DBManage.Manager;
using Qtud.SystemCommon;

namespace Qtud.Qtud
{
    public partial class TestResultDlg : Form
    {
        private PatientInfoManager pim = new PatientInfoManager();
        public PatientInfoModel m_CurSelPatientInfo;    //当前选择的病人
        private ReportInfoModel m_ReportInfoModel;    //当前报告

        public TestDatas m_TestDatas = new TestDatas();

        private bool isSave = false;

        public TestResultDlg(PatientInfoModel CurSelPatientInfo,   ReportInfoModel _ReportInfoModel)
        {
            InitializeComponent();

            m_CurSelPatientInfo = CurSelPatientInfo;
            m_ReportInfoModel = _ReportInfoModel;
        }

        private void TestResultDlg_Load(object sender, EventArgs e)
        {
            panel1.Location = new Point((int)((Width-panel1.Width )/2), panel1.Top);

            textBox_ks.Focus();
            //---------------------------------------------------- 


            textBox_name.Text = m_CurSelPatientInfo.name;
            textBox_cardid.Text = m_CurSelPatientInfo.cardid;
            if(m_CurSelPatientInfo.sex == 0)
                textBox_sex.Text = "女";
            else
                textBox_sex.Text = "男";

            string strTemp = string.Empty;
            if (m_CurSelPatientInfo.cardid != "")
            {
                string str = m_CurSelPatientInfo.cardid.Substring(6, 4);
                if (str != "")
                {
                    DateTime dt = DateTime.Now;
                    int year = dt.Year - int.Parse(str);
                    strTemp = year.ToString() + " 岁";
                }

            }

            label_Info.Text = "患者：" + m_CurSelPatientInfo.name + @"  " + textBox_sex.Text + @"  " + m_CurSelPatientInfo.cardid;

            //---------------------------------------------------- 
            if (m_ReportInfoModel != null)
            {
                m_TestDatas.uuid = m_ReportInfoModel.uuid;
                m_TestDatas.strKS = m_ReportInfoModel.ks;
                m_TestDatas.strCH = m_ReportInfoModel.ch;
                m_TestDatas.strNLL = m_ReportInfoModel.nlljcjg.ToString();
                m_TestDatas.strNL = m_ReportInfoModel.nlljcjg_nl.ToString();

                m_TestDatas.str_RJ_YL = m_ReportInfoModel.pgrlylcd.ToString();
                 

                m_TestDatas.str_nl_cg = m_ReportInfoModel.pgrl_cg.ToString();
                m_TestDatas.str_nl_zc = m_ReportInfoModel.pgrl_zc.ToString();
                m_TestDatas.str_nl_zd = m_ReportInfoModel.pgrl_zd.ToString();

                m_TestDatas.str_syx = m_ReportInfoModel.pgsyx  ;
                m_TestDatas.str_wdx = m_ReportInfoModel.pgwdx ;
                m_TestDatas.str_tsjc = m_ReportInfoModel.tsjc ;

                m_TestDatas.str_vlpp = m_ReportInfoModel.vlpp.ToString();
                m_TestDatas.str_dlpp = m_ReportInfoModel.dlpp.ToString();
                m_TestDatas.str_clpp = m_ReportInfoModel.clpp.ToString();
                m_TestDatas.str_aqrl = m_ReportInfoModel.pgaqrl.ToString();

                m_TestDatas.str_qtms = m_ReportInfoModel.otherInfo ;
                m_TestDatas.str_ndlxzd = m_ReportInfoModel.testresult ;
            }
           
            //----------------------------------------------------

            textBox_yearold.Text = strTemp; 
            textBox_bs.Text = m_CurSelPatientInfo.bs;


            textBox_ks.Text = m_TestDatas.strKS;
            textBox_ch.Text = m_TestDatas.strCH;
            textBox_bs.Text = m_CurSelPatientInfo.bs;
            textBox_nlljcjg.Text = m_TestDatas.strNLL;
            textBox1_nl.Text = m_TestDatas.strNL;

            textBox_cyqpgrj.Text = m_TestDatas.str_RJ_YL;
             
            textBox_rlcg.Text = m_TestDatas.str_nl_cg;
            textBox_rlzc.Text = m_TestDatas.str_nl_zc;
            textBox_rlzd.Text = m_TestDatas.str_nl_zd;


            if (m_TestDatas.str_syx == "正常")
            {
                radioButton_syqzc.Checked = true;
            }
            else if (m_TestDatas.str_syx == "高顺应性")
            {
                radioButton_syqg.Checked = true;
            }
            else if (m_TestDatas.str_syx == "低顺应性")
            {
                radioButton_syqd.Checked = true;
            }

            if (m_TestDatas.str_wdx == "正常")
            {
                radioButton_wdqzc.Checked = true;
            }
            else if (m_TestDatas.str_wdx == "逼尿肌活动过度")
            {
                radioButton_wdqb.Checked = true;
            }
                

            textBox_tsjc.Text = m_TestDatas.str_tsjc;
            textBox_vlpp.Text = m_TestDatas.str_vlpp;
            textBox_dlpp.Text = m_TestDatas.str_dlpp;
            textBox_clpp.Text = m_TestDatas.str_clpp;

            textBox_pgaqrl.Text = m_TestDatas.str_aqrl;

            textBox_qtms.Text = m_TestDatas.str_qtms;
            textBox_ndlxzd.Text = m_TestDatas.str_ndlxzd;
        }

        public float StrToFloat(object FloatString)
        {
            float result;
            if (FloatString != null)
            {
                if (float.TryParse(FloatString.ToString(), out result))
                    return result;
                else
                {
                    return (float)0.00;
                }
            }
            else
            {
                return (float)0.00;
            }
        }

        private void UpdateData()
        {
            m_TestDatas.strKS = textBox_ks.Text;
            m_TestDatas.strCH = textBox_ch.Text;
            m_TestDatas.strBS = textBox_bs.Text;
            m_TestDatas.strNLL = textBox_nlljcjg.Text;
            m_TestDatas.strNL = textBox1_nl.Text;
            m_TestDatas.str_RJ_YL = textBox_cyqpgrj.Text;

            m_TestDatas.str_nl_cg = textBox_rlcg.Text;
            m_TestDatas.str_nl_zc = textBox_rlzc.Text;
            m_TestDatas.str_nl_zd = textBox_rlzd.Text;

            if (radioButton_syqzc.Checked)
                m_TestDatas.str_syx = "正常";
            else if (radioButton_syqg.Checked)
                m_TestDatas.str_syx = "高顺应性";
            else if (radioButton_syqd.Checked)
                m_TestDatas.str_syx = "低顺应性";

            if (radioButton_wdqzc.Checked)
                m_TestDatas.str_wdx = "正常";
            else if (radioButton_wdqb.Checked)
                m_TestDatas.str_wdx = "逼尿肌活动过度";



            m_TestDatas.str_tsjc = textBox_tsjc.Text;
            m_TestDatas.str_vlpp = textBox_vlpp.Text;
            m_TestDatas.str_dlpp = textBox_dlpp.Text;
            m_TestDatas.str_clpp = textBox_clpp.Text;

            m_TestDatas.str_aqrl = textBox_pgaqrl.Text;

            m_TestDatas.str_qtms = textBox_qtms.Text;
            m_TestDatas.str_ndlxzd = textBox_ndlxzd.Text;


            //---------------------------------------------------- 


            m_ReportInfoModel.ks = m_TestDatas.strKS;
            m_ReportInfoModel.ch = m_TestDatas.strCH;
            m_ReportInfoModel.nlljcjg = StrToFloat(m_TestDatas.strNLL);
            m_ReportInfoModel.nlljcjg_nl = StrToFloat(m_TestDatas.strNL);
            m_ReportInfoModel.pgrlylcd = StrToFloat(m_TestDatas.str_RJ_YL);

            m_ReportInfoModel.pgrl_cg = StrToFloat(m_TestDatas.str_nl_cg);
            m_ReportInfoModel.pgrl_zc = StrToFloat(m_TestDatas.str_nl_zc);
            m_ReportInfoModel.pgrl_zd = StrToFloat(m_TestDatas.str_nl_zd);

            m_ReportInfoModel.pgsyx = m_TestDatas.str_syx;
            m_ReportInfoModel.pgwdx = m_TestDatas.str_wdx;
            m_ReportInfoModel.tsjc = m_TestDatas.str_tsjc;

            m_ReportInfoModel.vlpp = StrToFloat(m_TestDatas.str_vlpp);
            m_ReportInfoModel.dlpp = StrToFloat(m_TestDatas.str_dlpp);
            m_ReportInfoModel.clpp = StrToFloat(m_TestDatas.str_clpp);
            m_ReportInfoModel.pgaqrl = StrToFloat(m_TestDatas.str_aqrl);

            m_ReportInfoModel.otherInfo = m_TestDatas.str_qtms;
            m_ReportInfoModel.testresult = m_TestDatas.str_ndlxzd;


            m_TestDatas.uuid = m_ReportInfoModel.uuid;

            try
            {
                m_CurSelPatientInfo.bs = textBox_bs.Text;
                pim.Updatebs(m_CurSelPatientInfo);

                ReportInfoManager rim = new ReportInfoManager();

                rim.Update(m_ReportInfoModel);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" 保存失败！ ");

            }
        }
 
        private void sava( )
        {
            m_TestDatas.strKS = textBox_ks.Text;
            m_TestDatas.strCH = textBox_ch.Text;
            m_TestDatas.strBS = textBox_bs.Text;
            m_TestDatas.strNLL = textBox_nlljcjg.Text;
            m_TestDatas.strNL = textBox1_nl.Text;
            m_TestDatas.str_RJ_YL = textBox_cyqpgrj.Text;

            m_TestDatas.str_nl_cg = textBox_rlcg.Text;
            m_TestDatas.str_nl_zc = textBox_rlzc.Text;
            m_TestDatas.str_nl_zd = textBox_rlzd.Text;

            if (radioButton_syqzc.Checked)
                m_TestDatas.str_syx = "正常";
            else if (radioButton_syqg.Checked)
                m_TestDatas.str_syx = "高顺应性";
            else if (radioButton_syqd.Checked)
                m_TestDatas.str_syx = "低顺应性";

            if (radioButton_wdqzc.Checked)
                m_TestDatas.str_wdx = "正常";
            else if (radioButton_wdqb.Checked)
                m_TestDatas.str_wdx = "逼尿肌活动过度";
            
             

            m_TestDatas.str_tsjc = textBox_tsjc.Text;
            m_TestDatas.str_vlpp = textBox_vlpp.Text;
            m_TestDatas.str_dlpp = textBox_dlpp.Text;
            m_TestDatas.str_clpp = textBox_clpp.Text;

            m_TestDatas.str_aqrl = textBox_pgaqrl.Text;

            m_TestDatas.str_qtms = textBox_qtms.Text;
            m_TestDatas.str_ndlxzd = textBox_ndlxzd.Text;


            //----------------------------------------------------
            ReportInfoModel model = new ReportInfoModel();

            model.uuid = PublicConst.GenerateUUID();
            model.name = m_CurSelPatientInfo.cardid;
            model.CreateDate = DateTime.Now;
            model.patient_uuid = m_CurSelPatientInfo.uuid;

            model.ks = m_TestDatas.strKS;
            model.ch = m_TestDatas.strCH;
            model.nlljcjg = StrToFloat(m_TestDatas.strNLL);
            model.nlljcjg_nl = StrToFloat(m_TestDatas.strNL);
            model.pgrlylcd = StrToFloat(m_TestDatas.str_RJ_YL);

            model.pgrl_cg = StrToFloat(m_TestDatas.str_nl_cg);
            model.pgrl_zc = StrToFloat(m_TestDatas.str_nl_zc);
            model.pgrl_zd = StrToFloat(m_TestDatas.str_nl_zd);

            model.pgsyx = m_TestDatas.str_syx;
            model.pgwdx = m_TestDatas.str_wdx;
            model.tsjc = m_TestDatas.str_tsjc;

            model.vlpp = StrToFloat(m_TestDatas.str_vlpp);
            model.dlpp = StrToFloat(m_TestDatas.str_dlpp);
            model.clpp = StrToFloat(m_TestDatas.str_clpp);
            model.pgaqrl = StrToFloat(m_TestDatas.str_aqrl);

            model.otherInfo = m_TestDatas.str_qtms;
            model.testresult = m_TestDatas.str_ndlxzd;

            m_ReportInfoModel = model;

            m_TestDatas.uuid = model.uuid;
            try
            {
                ReportInfoManager pim = new ReportInfoManager();

                pim.Add(model);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" 保存失败！ ");
            	
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_ReportInfoModel == null)
                sava();
            else
                UpdateData();

             

        }

        private void TestResultDlg_FormClosing(object sender, FormClosingEventArgs e)
        { 
            DialogResult res = MessageBox.Show("退出前，是否保存吗？", "保存提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
             
            if (res == DialogResult.Cancel)  //不保存
            { 
                return;
            }
            else
            {
                if (m_ReportInfoModel == null)
                    sava();
                else
                    UpdateData();
                DialogResult = DialogResult.OK;
            }
        }

        private void button_Back_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_AddCural_Click(object sender, EventArgs e)
        {
            //-----------------------------------
            if (m_ReportInfoModel == null)
                sava();
            else
                UpdateData();
            //-----------------------------------

            Hide();
            MainFrom_Curve m_MainFrom2 = new MainFrom_Curve(m_CurSelPatientInfo, m_TestDatas);
            DialogResult dlgResult = m_MainFrom2.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {

            }
            Show();
        }

    }
}
