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
            if (ValueUnit == "cmH2O")
            {
                radio_mmh2o.Checked = true;
            }
            else if (ValueUnit == "mmHg")
            {
                radio_mmhg.Checked = true;

            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ValueUnit =string.Empty;
            if(radio_mmh2o.Checked)
            {
                ValueUnit = "cmH2O";
            } 
            else if(radio_mmhg.Checked)
            {
                ValueUnit = "mmHg";
            }

            INIOperationClass.INIWriteValue(strIniFile, "Setting", "strUnit", ValueUnit);

            this.DialogResult   = DialogResult.OK;
            Close();
        }
    }
}
