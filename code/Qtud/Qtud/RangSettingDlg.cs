using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Qtud.Qtud
{
    public partial class RangSettingDlg : Form
    {
        public Size pvet_range = new Size(0,0);
        public Size nl_range = new Size(0, 0);
        public Size nll_range = new Size(0, 0);

        public RangSettingDlg(Size _pvet_range , Size _nl_range,Size _nll_range)
        {
            InitializeComponent();

            pvet_range = _pvet_range;
            nl_range = _nl_range;
            nll_range = _nll_range;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_pvetX.Text.Trim() == "")
            {
                MessageBox.Show("压力值 下限设置错误，请重新设置", "输入错误");
                textBox_pvetX.Focus();
                return ;
            }

            if (textBox_pvetY.Text.Trim() == "")
            {
                MessageBox.Show("压力值 上限设置错误，请重新设置", "输入错误");
                textBox_pvetY.Focus();
                return;
            }

            if (int.Parse(textBox_pvetX.Text.Trim()) >= int.Parse(textBox_pvetY.Text.Trim()))
            {
                MessageBox.Show("压力值 下限应小于上限，请重新设置", "输入错误");
                textBox_pvetY.Focus();
                return;
            }
            //-------------------------------------------
            if (textBox_nlX.Text.Trim() == "")
            {
                MessageBox.Show("尿量 下限设置错误，请重新设置", "输入错误");
                textBox_nlX.Focus();
                return;
            }

            if (textBox_nlY.Text.Trim() == "")
            {
                MessageBox.Show("尿量 上限设置错误，请重新设置", "输入错误");
                textBox_nlY.Focus();
                return;
            }

            if (int.Parse(textBox_nlX.Text.Trim()) >= int.Parse(textBox_nlY.Text.Trim()))
            {
                MessageBox.Show("尿量 下限应小于上限，请重新设置", "输入错误");
                textBox_nlX.Focus();
                return;
            }
            //-------------------------------------------
            if (textBox_nllX.Text.Trim() == "")
            {
                MessageBox.Show("尿流率 下限设置错误，请重新设置", "输入错误");
                textBox_nllX.Focus();
                return;
            }

            if (textBox_nllY.Text.Trim() == "")
            {
                MessageBox.Show("尿流率 上限设置错误，请重新设置", "输入错误");
                textBox_nllY.Focus();
                return;
            }

            if (int.Parse(textBox_nllX.Text.Trim()) >= int.Parse(textBox_nllY.Text.Trim()))
            {
                MessageBox.Show("尿流率 下限应小于上限，请重新设置", "输入错误");
                textBox_nllX.Focus();
                return;
            }
            //-------------------------------------------

            pvet_range.Width = int.Parse(textBox_pvetX.Text.Trim());
            pvet_range.Height = int.Parse(textBox_pvetY.Text.Trim());

           nl_range.Width =  int.Parse(textBox_nlX.Text.Trim());
           nl_range.Height=  int.Parse(textBox_nlY.Text.Trim());

           nll_range.Width = int.Parse(textBox_nllX.Text.Trim());
           nll_range.Height = int.Parse(textBox_nllY.Text.Trim());

           DialogResult = DialogResult.OK;
           Close();
        }

        private void RangSettingDlg_Load(object sender, EventArgs e)
        {
            textBox_pvetX.Text = pvet_range.Width.ToString();
            textBox_pvetY.Text = pvet_range.Height.ToString();

            textBox_nlX.Text = nl_range.Width.ToString();
            textBox_nlY.Text = nl_range.Height.ToString();

            textBox_nllX.Text = nll_range.Width.ToString();
            textBox_nllY.Text = nll_range.Height.ToString();
        }
    }
}
