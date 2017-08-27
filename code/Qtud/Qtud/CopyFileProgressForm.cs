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
    public partial class CopyFileProgressForm : Form
    {
        public CopyFileProgressForm()
        {
            InitializeComponent();
        }

        private void CopyFileProgressForm_Load(object sender, EventArgs e)
        {
        }

        public void SetProgressMaximumValue(int value)
        {
            progressBar1.Maximum = value;

        }

        public void SetProgressValue(int value)
        {
            this.progressBar1.Value = value;
            this.label_file.Text = "进度 :" + value.ToString() + "%";

            // 这里关闭，比较好，呵呵！  
            if (value == this.progressBar1.Maximum - 1 || value<0) 
                this.Close();
        }  
    }
}
