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
using System.IO;

namespace Qtud.Qtud
{
    public partial class TestResultDlg : Form
    {
        private PatientInfoManager pim = new PatientInfoManager();
        public PatientInfoModel m_CurSelPatientInfo = null;    //当前选择的病人
        private ReportInfoModel m_ReportInfoModel = null;    //当前报告

        public TestDatas m_TestDatas = new TestDatas
        {
            uuid = string.Empty,
            strKS = string.Empty,
            strCH = string.Empty,
            strBS = string.Empty,
            strNLL = string.Empty,
            strNL = string.Empty,

            str_RJ_YL = string.Empty,
            str_nl_cg = string.Empty,
            str_nl_zc = string.Empty,
            str_nl_zd = string.Empty,

            str_syx = string.Empty,
            str_wdx = string.Empty,

            str_tsjc = string.Empty,
            str_vlpp = string.Empty,
            str_dlpp = string.Empty,
            str_clpp = string.Empty,
            str_aqrl = string.Empty,
            str_qtms = string.Empty,
            str_ndlxzd = string.Empty
        };
         

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
            string strIniFile = "config.ini";
            strIniFile = Directory.GetCurrentDirectory() + "\\" +strIniFile; 
             //获取指定KEY的值  
            string ValueUnit = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "strUnit", null);
            if (ValueUnit != "")
            {
                label_vlpp.Text = ValueUnit;
                label_dlpp.Text = ValueUnit;
                label_alpp.Text = ValueUnit;
            }
            //---------------------------------------------------- 

            textBox_name.Text = m_CurSelPatientInfo.name;
            textBox_cardid.Text = m_CurSelPatientInfo.cardid;
            if(m_CurSelPatientInfo.sex == 0)
                textBox_sex.Text = "女";
            else
                textBox_sex.Text = "男";

            string strTemp = string.Empty; 
            if (m_CurSelPatientInfo.birth != null && m_CurSelPatientInfo.birth.ToString() != "")
            {
                DateTime dt = DateTime.Now;
                int year = dt.Year - m_CurSelPatientInfo.birth.Year;
                strTemp = year.ToString() + @" 岁";

            }

            label_Info.Text = "患者：" + m_CurSelPatientInfo.name + @"  " + textBox_sex.Text + @"  " + m_CurSelPatientInfo.id;

            //---------------------------------------------------- 
            if (m_ReportInfoModel != null)
            {
                m_TestDatas.uuid = m_ReportInfoModel.uuid;
                m_TestDatas.strKS = m_ReportInfoModel.ks;
                m_TestDatas.strCH = m_ReportInfoModel.ch;
                m_TestDatas.strNLL = m_ReportInfoModel.nlljcjg ;
                m_TestDatas.strNL = m_ReportInfoModel.nlljcjg_nl ;

                m_TestDatas.str_RJ_YL = m_ReportInfoModel.pgrlylcd ;
                 

                m_TestDatas.str_nl_cg = m_ReportInfoModel.pgrl_cg ;
                m_TestDatas.str_nl_zc = m_ReportInfoModel.pgrl_zc ;
                m_TestDatas.str_nl_zd = m_ReportInfoModel.pgrl_zd ;

                m_TestDatas.str_syx = m_ReportInfoModel.pgsyx  ;
                m_TestDatas.str_wdx = m_ReportInfoModel.pgwdx ;
                m_TestDatas.str_tsjc = m_ReportInfoModel.tsjc ;

                m_TestDatas.str_vlpp = m_ReportInfoModel.vlpp ;
                m_TestDatas.str_dlpp = m_ReportInfoModel.dlpp ;
                m_TestDatas.str_clpp = m_ReportInfoModel.clpp ;
                m_TestDatas.str_aqrl = m_ReportInfoModel.pgaqrl ;

                m_TestDatas.str_qtms = m_ReportInfoModel.otherInfo ;
                m_TestDatas.str_ndlxzd = m_ReportInfoModel.testresult ;
            }

            if (m_CurSelPatientInfo != null)
            {
                m_TestDatas.strBS = m_CurSelPatientInfo.bs;
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
                checkBox_syx_zc.Checked = true;
            }
            else if (m_TestDatas.str_syx == "高顺应性")
            {
                checkBox_syx_g.Checked = true;
            }
            else if (m_TestDatas.str_syx == "低顺应性")
            {
                checkBox_syx_d.Checked = true;
            }

            if (m_TestDatas.str_wdx == "正常")
            {
                checkBox_wdx_z.Checked = true;
            }
            else if (m_TestDatas.str_wdx == "逼尿肌活动过度")
            {
                checkBox_wdx_b.Checked = true;
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

        private void  bindValue()
        {
            m_TestDatas.strKS = textBox_ks.Text.Trim();
            m_TestDatas.strCH = textBox_ch.Text.Trim();
            m_TestDatas.strBS = textBox_bs.Text.Trim();
            m_TestDatas.strNLL = textBox_nlljcjg.Text.Trim();
            m_TestDatas.strNL = textBox1_nl.Text.Trim();
            m_TestDatas.str_RJ_YL = textBox_cyqpgrj.Text.Trim();

            m_TestDatas.str_nl_cg = textBox_rlcg.Text.Trim();
            m_TestDatas.str_nl_zc = textBox_rlzc.Text.Trim();
            m_TestDatas.str_nl_zd = textBox_rlzd.Text.Trim();

            m_TestDatas.str_syx = "";
            if (checkBox_syx_zc.Checked)
                m_TestDatas.str_syx = "正常";
            else if (checkBox_syx_g.Checked)
                m_TestDatas.str_syx = "高顺应性";
            else if (checkBox_syx_d.Checked)
                m_TestDatas.str_syx = "低顺应性";

            m_TestDatas.str_wdx = "";
            if (checkBox_wdx_z.Checked)
                m_TestDatas.str_wdx = "正常";
            else if (checkBox_wdx_b.Checked)
                m_TestDatas.str_wdx = "逼尿肌活动过度";

            m_TestDatas.str_tsjc = textBox_tsjc.Text.Trim();
            m_TestDatas.str_vlpp = textBox_vlpp.Text.Trim();
            m_TestDatas.str_dlpp = textBox_dlpp.Text.Trim();
            m_TestDatas.str_clpp = textBox_clpp.Text.Trim();

            m_TestDatas.str_aqrl = textBox_pgaqrl.Text.Trim();

            m_TestDatas.str_qtms = textBox_qtms.Text.Trim();
            m_TestDatas.str_ndlxzd = textBox_ndlxzd.Text.Trim();


            //---------------------------------------------------- 
           

            m_ReportInfoModel.ks = m_TestDatas.strKS;
            m_ReportInfoModel.ch = m_TestDatas.strCH;
            m_ReportInfoModel.nlljcjg = (m_TestDatas.strNLL);
            m_ReportInfoModel.nlljcjg_nl = (m_TestDatas.strNL);
            m_ReportInfoModel.pgrlylcd = (m_TestDatas.str_RJ_YL);

            m_ReportInfoModel.pgrl_cg = (m_TestDatas.str_nl_cg);
            m_ReportInfoModel.pgrl_zc = (m_TestDatas.str_nl_zc);
            m_ReportInfoModel.pgrl_zd = (m_TestDatas.str_nl_zd);

            m_ReportInfoModel.pgsyx = m_TestDatas.str_syx;
            m_ReportInfoModel.pgwdx = m_TestDatas.str_wdx;
            m_ReportInfoModel.tsjc = m_TestDatas.str_tsjc;

            m_ReportInfoModel.vlpp = (m_TestDatas.str_vlpp);  //StrToFloat
            m_ReportInfoModel.dlpp = (m_TestDatas.str_dlpp);
            m_ReportInfoModel.clpp = (m_TestDatas.str_clpp);
            m_ReportInfoModel.pgaqrl = (m_TestDatas.str_aqrl);

            m_ReportInfoModel.otherInfo = m_TestDatas.str_qtms;
            m_ReportInfoModel.testresult = m_TestDatas.str_ndlxzd;


            m_TestDatas.uuid = m_ReportInfoModel.uuid;
        }

        private void UpdateData()
        {
            bindValue();

            try
            {
                m_CurSelPatientInfo.bs = textBox_bs.Text;
                pim.Updatebs(m_CurSelPatientInfo);

                ReportInfoManager rim = new ReportInfoManager();

                rim.Update(m_ReportInfoModel);
                MessageBox.Show(" 保存成功！ ");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" 保存失败！ ");

            }
        }
 
        private void sava( )
        {
            m_ReportInfoModel = new ReportInfoModel();
            m_ReportInfoModel.uuid = PublicConst.GenerateUUID();
            m_ReportInfoModel.patient_uuid = m_CurSelPatientInfo.uuid;
            bindValue();
             
            try
            {

                m_CurSelPatientInfo.bs = textBox_bs.Text;
                pim.Updatebs(m_CurSelPatientInfo);
                
                ReportInfoManager rim = new ReportInfoManager();
                rim.Add(m_ReportInfoModel);
                MessageBox.Show(" 保存成功！ ");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(" 保存失败！ ");
            	
            }
        }

        //是否修改了
        private bool isEdit()
        {
            bool isbool = true;

            if (m_TestDatas.strKS != textBox_ks.Text)
            {
                return isbool;
            }
            if (m_TestDatas.strCH != textBox_ch.Text)
            {
                return isbool;
            }
            if (m_TestDatas.strBS != textBox_bs.Text)
            {
                return isbool;
            }
            if (m_TestDatas.strNLL != textBox_nlljcjg.Text)
            {
                return isbool;
            }
            if (m_TestDatas.strNL != textBox1_nl.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_RJ_YL != textBox_cyqpgrj.Text)
            {
                return isbool;
            }

            if (m_TestDatas.str_nl_cg != textBox_rlcg.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_nl_zc != textBox_rlzc.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_nl_zd != textBox_rlzd.Text)
            {
                return isbool;
            }

            string temp = "";
            if (checkBox_syx_zc.Checked)
                temp = "正常";
            else if (checkBox_syx_g.Checked)
                temp = "高顺应性";
            else if (checkBox_syx_d.Checked)
                temp = "低顺应性";
            if (m_TestDatas.str_syx != temp)
            {
                return isbool;
            }

            temp = "";
            if (checkBox_wdx_z.Checked)
                temp = "正常";
            else if (checkBox_wdx_b.Checked)
                temp = "逼尿肌活动过度";

            if (m_TestDatas.str_wdx != temp)
            {
                return isbool;
            }

            if (m_TestDatas.str_tsjc != textBox_tsjc.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_vlpp != textBox_vlpp.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_dlpp != textBox_dlpp.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_clpp != textBox_clpp.Text)
            {
                return isbool;
            }

            if (m_TestDatas.str_aqrl != textBox_pgaqrl.Text)
            {
                return isbool;
            }

            if (m_TestDatas.str_qtms != textBox_qtms.Text)
            {
                return isbool;
            }
            if (m_TestDatas.str_ndlxzd != textBox_ndlxzd.Text)
            {
                return isbool;
            }
            isbool = false;
            return isbool;
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_ReportInfoModel == null || m_ReportInfoModel.uuid == "")
            {
                sava();
            }
            else
                UpdateData();

             

        }

        private void TestResultDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isEdit())
            {
                return;
            }
            DialogResult res = MessageBox.Show("报告已修改，是否保存吗？", "保存提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
             
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
            if (isEdit())
            {
                DialogResult res = MessageBox.Show("报告已修改，是否保存吗？", "保存提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (res == DialogResult.OK)  //保存 
                {
                    //-----------------------------------
                    if (m_ReportInfoModel == null || m_ReportInfoModel.uuid == "")
                        sava();
                    else
                        UpdateData();
                    //-----------------------------------
                }
            }
          

            Hide();
            MainFrom_Curve m_MainFrom2 = new MainFrom_Curve(m_CurSelPatientInfo, m_TestDatas);
            DialogResult dlgResult = m_MainFrom2.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {

            }
            m_TestDatas.uuid = m_MainFrom2.m_TestDatas.uuid;
            if (m_TestDatas.uuid != "")
            {
                if (m_ReportInfoModel == null || m_ReportInfoModel.uuid == "")
                {
                    m_ReportInfoModel = new ReportInfoModel();
                    m_ReportInfoModel.uuid = m_MainFrom2.m_TestDatas.uuid;
                }
            }
            Show();
        }

        private void checkBox_syx_zc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_syx_zc.Checked)
            {
                checkBox_syx_g.Checked = false;
                checkBox_syx_d.Checked = false;
            }
        }

        private void checkBox_syx_g_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_syx_g.Checked)
            {
                checkBox_syx_zc.Checked = false;
                checkBox_syx_d.Checked = false;
            }
        }

        private void checkBox_syx_d_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_syx_d.Checked)
            {
                checkBox_syx_zc.Checked = false;
                checkBox_syx_g.Checked = false;
            }
        }

        private void checkBox_wdx_z_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_wdx_z.Checked)
            {
                checkBox_wdx_b.Checked = false; 
            }
        }

        private void checkBox_wdx_b_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_wdx_b.Checked)
            {
                checkBox_wdx_z.Checked = false;
            }
        }

    }
}
