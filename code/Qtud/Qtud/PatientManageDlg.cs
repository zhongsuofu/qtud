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

namespace Qtud.Qtud
{
    public partial class PatientManageDlg : Form
    {

        private PatientInfoManager pim = new PatientInfoManager();

        private List<PatientInfoModel> listPatientInfo = new List<PatientInfoModel>();  //病人数据列表

        public PatientManageDlg()
        {
            InitializeComponent();
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

        private void PatientManageDlg_Load(object sender, EventArgs e)
        {
            this.listView_patList.Columns.Add("序 号", 80, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("姓 名", 150, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("身份证号", 200, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("性 别", 100, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("年 龄", 100, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("电 话", 150, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("病 史", 260, HorizontalAlignment.Center); //一步添加 
            this.listView_patList.Columns.Add("备 注", 260, HorizontalAlignment.Center); //一步添加 

            UpdateListView();
        }

        public string GetYearOldByCardid(string cardid)
        {
            string strTemp = string.Empty;
            if ( cardid != "")
            {
                string str =  cardid.Substring(6, 4);
                if (str != "")
                {
                    DateTime dt = DateTime.Now;
                    int year = dt.Year - int.Parse(str);
                    strTemp = year.ToString();
                }

            }

            return strTemp;
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
                        if (data.sex == 0)
                            lvi.SubItems.Add("女");
                        else
                            lvi.SubItems.Add("男");

                        lvi.SubItems.Add(GetYearOldByCardid(data.cardid));
                        lvi.SubItems.Add(data.phone);
                        lvi.SubItems.Add(data.bs);
                        lvi.SubItems.Add(data.meno);
                        this.listView_patList.Items.Add(lvi);
                    } 
                }
                 
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

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
                        data.cardid = data.cardid;

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = i.ToString();
                        lvi.SubItems.Add(data.name );
                        lvi.SubItems.Add(data.cardid);
                        if(data.sex == 0)
                            lvi.SubItems.Add("女");
                        else
                            lvi.SubItems.Add("男");

                        lvi.SubItems.Add(GetYearOldByCardid(data.cardid));
                        lvi.SubItems.Add(data.phone);
                        lvi.SubItems.Add(data.bs );
                        lvi.SubItems.Add(data.meno);
                        this.listView_patList.Items.Add(lvi);

                    }

                   
                }
            }
            catch (System.Exception e)
            {
                throw e;
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

        private void ADDPatientInfo(PatientInfoModel _model = null)
        {
            PatientInfoDlg m_PatientInfoDlg = new PatientInfoDlg(_model);
            DialogResult dlgResult = m_PatientInfoDlg.ShowDialog();
            if (dlgResult == DialogResult.OK)  //添加完成
            {
                UpdateListView();

            }
        }

        private void button_patient_manage_Click(object sender, EventArgs e)
        {
            ADDPatientInfo();
        }
    }
}
