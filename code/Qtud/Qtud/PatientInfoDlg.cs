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
    public partial class PatientInfoDlg : Form
    {

        #region 变量
        private UserManager um = new UserManager();
        private PatientInfoModel model = new PatientInfoModel();
        private int DlgType = 0;//新建0，更新1
         
         
        #endregion

        public PatientInfoDlg(PatientInfoModel _model)
        {
            InitializeComponent();

            if (_model != null)
            {
                model = _model;
                DlgType = 1;
            }
            else
            {
                model.uuid = PublicConst.GenerateUUID();
                DlgType = 0;
            } 
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Submit();
        }
         

        /// <summary>
        /// 添加
        /// </summary>
        private void Submit()
        {
            try
            {
                //if (this.textBox_cardid.Text.Trim() == "")
                //{
                //    MessageBox.Show("请输入身份证号!", "输入提示");
                //    textBox_cardid.Focus();
                //    return;
                //}
                //if (this.textBox_cardid.Text.Trim().Length < 18)
                //{
                //    MessageBox.Show("身份证号错误，请重新输入!", "输入提示");
                //    textBox_cardid.Focus();
                //    return;
                //}

                if (this.textBox_id.Text.Trim() == "")
                {
                    MessageBox.Show("请输入ID号!", "输入提示");
                    textBox_id.Focus();
                    return;
                }
                model.id = this.textBox_id.Text.Trim();

                if (this.textBox_name.Text.Trim() == "")
                {
                    MessageBox.Show("请输入姓名!", "输入提示");
                    textBox_name.Focus();
                    return;
                }

                model.name = this.textBox_name.Text.Trim() ;

                // 身份证号做DEs加密和Base64 编码  保存
                //string _Des = DES.DESEncoder(this.textBox_cardid.Text.Trim(), Encoding.Default, null, null);  //DES加密 
                //string EBase64 = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(_Des));  //Base64编码
                model.cardid = this.textBox_cardid.Text.Trim();  // EBase64;

                model.sex = 0;
                if(radioBtn_man.Checked )
                    model.sex = 1;

                model.bs = textBox_bs.Text.Trim();
                model.phone = this.textBox_phone.Text.Trim();
                model.meno = this.textBox_meno.Text.Trim();
                model.birth = this.dateTimePicker1.Value  ;

                PatientInfoManager pim = new PatientInfoManager();
                if (DlgType == 0)  //新增
                {
                    if (pim.Exists(model.id))
                    {
                        MessageBox.Show("ID号已经存在,不能重复添加", "输入提示");
                        textBox_cardid.Focus();
                        return;
                    }
                    model.createtime = DateTime.Now;
                    model.lastchecktime = DateTime.Now;
                    pim.Add(model);

                     
                }
                else  //修改
                {
                    pim.Update(model);
                     
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception e)
            {
                MessageBox.Show("  保存失败！ ");
            }
            this.Close();
        }

        private void PatientInfoDlg_Load(object sender, EventArgs e)
        {
            if (DlgType == 1 && model != null)
            {
                textBox_id.Enabled = false;
                textBox_name.Enabled = false;
                textBox_id.Text = model.id;

                textBox_cardid.Text = model.cardid;
                textBox_name.Text = model.name;
                if (model.sex == 0)
                    radioBTN_woman.Checked = true;
                else
                    radioBtn_man.Checked = true;
                textBox_phone.Text = model.phone;
                textBox_meno.Text = model.meno;
                dateTimePicker1.Value = model.birth;

                textBox_bs.Text = model.bs;
            }
            else
            {
                textBox_cardid.Enabled = true;
                textBox_name.Enabled = true;
            }
        }
         
    }
}
