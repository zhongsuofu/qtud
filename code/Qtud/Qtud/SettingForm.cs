using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Qtud.Qtud
{
    public partial class SettingForm : Form
    {
        private string strIniFile = "config.ini";
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            strIniFile = Directory.GetCurrentDirectory() + "\\" +strIniFile; 
              //获取指定KEY的值  
            string ValueUnit = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "strUnit", null);
            if (ValueUnit == null || ValueUnit == string.Empty || ValueUnit == "cmH2O")
            {
                comboBox_unit.SelectedIndex = 0;
            }
            else  
            {
                comboBox_unit.SelectedIndex = 1; 
            }

            
            string pvetX = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "pvetX", null);
            if (pvetX == null || pvetX == string.Empty)
            {
                textBox_pvetX.Text = "-100";
            }
            else
                textBox_pvetX.Text = pvetX;

            string pvetY = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "pvetY", null);
            if (pvetY == null || pvetY == string.Empty)
            {
                textBox_pvetY.Text = "300";
            }
            else
                textBox_pvetY.Text = pvetY;

            string nlX = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "nlX", null);
            if (nlX == null || nlX == string.Empty)
            {
                textBox_nlX.Text = "0";
            }
            else
                textBox_nlX.Text = nlX;

            string nlY = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "nlY", null);
            if (nlY == null || nlY == string.Empty)
            {
                textBox_nlY.Text = "3000";
            }
            else
                textBox_nlY.Text = nlY;

            string nllX = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "nllX", null);
            if (nllX == null || nllX == string.Empty)
            {
                textBox_nllX.Text = "0";
            }
            else
                textBox_nllX.Text = nllX;

            string nllY = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "nllY", null);
            if (nllY == null || nllY == string.Empty)
            {
                textBox_nllY.Text = "300";
            }
            else
                textBox_nllY.Text = nllY;

            string ShowFirstPage = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "ShowFirstPage", null);
            if (ShowFirstPage == null || ShowFirstPage == string.Empty)
            {
                ShowFirstPage = "1";
            } 
            if (ShowFirstPage == "1")
                checkBox_printFirst.Checked = true;


            string UseAbleSpace = INIOperationClass.INIGetStringValue(strIniFile, "Setting", "UseAbleSpace", null);
            if (string.IsNullOrEmpty(UseAbleSpace))
            {
                UseAbleSpace = "20";
            }
            textBox_UseAbleSpace.Text = UseAbleSpace;

        }



        protected bool isNumberic(string message)  //, out int result
        {
            int result = 0;
            //判断是否为整数字符串

            //是的话则将其转换为数字并将其设为out类型的输出值、返回true, 否则为false

            result = -1;   //result 定义为out 用来输出值

            try
            {

                //当数字字符串的为是少于4时，以下三种都可以转换，任选一种

                //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()



                result = int.Parse(message);

                //result = Convert.ToInt16(message);

                //result = Convert.ToInt32(message);

                return true;

            }

            catch
            {

                return false;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ValueUnit =string.Empty; 
            if (comboBox_unit.SelectedIndex == 0)
            {
                ValueUnit = "cmH2O";
            }
            else 
            {
                ValueUnit = "mmHg";
            }
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "strUnit", ValueUnit);


            string strTempValue = string.Empty;
            if (textBox_pvetX.Text.Trim() == "" || !isNumberic(textBox_pvetX.Text.Trim()))
            {
                MessageBox.Show("压力值 下限设置错误，请重新设置", "输入错误");
                textBox_pvetX.Focus();
                return;
            }

            if (textBox_pvetY.Text.Trim() == "" || !isNumberic(textBox_pvetY.Text.Trim()))
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
            if (textBox_nlX.Text.Trim() == "" || !isNumberic(textBox_nlX.Text.Trim()))
            {
                MessageBox.Show("尿量 下限设置错误，请重新设置", "输入错误");
                textBox_nlX.Focus();
                return;
            }

            if (textBox_nlY.Text.Trim() == "" || !isNumberic(textBox_nlY.Text.Trim()))
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
            if (textBox_nllX.Text.Trim() == "" || !isNumberic(textBox_nllX.Text.Trim()))
            {
                MessageBox.Show("尿流率 下限设置错误，请重新设置", "输入错误");
                textBox_nllX.Focus();
                return;
            }

            if (textBox_nllY.Text.Trim() == "" || !isNumberic(textBox_nllY.Text.Trim()))
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
            if (textBox_UseAbleSpace.Text.Trim() == "" || !isNumberic(textBox_UseAbleSpace.Text.Trim()))
            {
                MessageBox.Show("磁盘可用空间剩余量预警阈值 设置错误，请重新设置", "输入错误");
                textBox_UseAbleSpace.Focus();
                return;
            }
            if (int.Parse(textBox_UseAbleSpace.Text.Trim()) < 1 || int.Parse(textBox_UseAbleSpace.Text.Trim()) > 100)
            {
                MessageBox.Show("磁盘可用空间剩余量预警阈值范围(1,100)，请重新设置", "输入错误");
                textBox_UseAbleSpace.Focus();
                return;
            }
            //-------------------------------------------
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "pvetX", textBox_pvetX.Text.Trim());
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "pvetY", textBox_pvetY.Text.Trim());
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "nlX", textBox_nlX.Text.Trim());
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "nlY", textBox_nlY.Text.Trim());
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "nllX", textBox_nllX.Text.Trim());
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "nllY", textBox_nllY.Text.Trim());

            //-------------------------------------------
            if (checkBox_printFirst.Checked)
            {
                strTempValue = "1";
            }
            else 
            {
                strTempValue = "0";　
            }
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "ShowFirstPage", strTempValue);

            //-------------------------------------------
             
            INIOperationClass.INIWriteValue(strIniFile, "Setting", "UseAbleSpace", textBox_UseAbleSpace.Text.Trim());
             
            //-------------------------------------------
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
