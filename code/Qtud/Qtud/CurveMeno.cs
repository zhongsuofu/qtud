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
    public partial class CurveMeno : Form
    {
        CurveDatas m_CurveDatas = new CurveDatas();
        public CurveMeno(CurveDatas _CurveDatas)
        {
            InitializeComponent();

            m_CurveDatas = _CurveDatas;

            string strTemp = string.Empty;
            strTemp = m_CurveDatas.StartTime.ToString() +" 至 "+ m_CurveDatas.endTime.ToString();
            label1.Text = strTemp;

            textBox_meno.Text = _CurveDatas.strMeno;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            m_CurveDatas.strMeno = textBox_meno.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        public string GetStrMeno()
        {
            return m_CurveDatas.strMeno;
        } 
    }
}
