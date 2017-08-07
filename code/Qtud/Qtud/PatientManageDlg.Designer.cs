namespace Qtud.Qtud
{
    partial class PatientManageDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientManageDlg));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_sys_Setting = new System.Windows.Forms.Button();
            this.button_user_manage = new System.Windows.Forms.Button();
            this.button_patient_manage = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView_patList = new System.Windows.Forms.ListView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label_msg = new System.Windows.Forms.Label();
            this.button_query = new System.Windows.Forms.Button();
            this.textBox_queryWhere = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.button_sys_Setting);
            this.panel2.Controls.Add(this.button_user_manage);
            this.panel2.Controls.Add(this.button_patient_manage);
            this.panel2.Controls.Add(this.label_Info);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(924, 79);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(34, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 66);
            this.panel4.TabIndex = 7;
            // 
            // button_sys_Setting
            // 
            this.button_sys_Setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sys_Setting.Location = new System.Drawing.Point(794, 12);
            this.button_sys_Setting.Name = "button_sys_Setting";
            this.button_sys_Setting.Size = new System.Drawing.Size(118, 44);
            this.button_sys_Setting.TabIndex = 5;
            this.button_sys_Setting.Text = "删 除";
            this.button_sys_Setting.UseVisualStyleBackColor = true;
            // 
            // button_user_manage
            // 
            this.button_user_manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_user_manage.Location = new System.Drawing.Point(670, 12);
            this.button_user_manage.Name = "button_user_manage";
            this.button_user_manage.Size = new System.Drawing.Size(118, 44);
            this.button_user_manage.TabIndex = 4;
            this.button_user_manage.Text = "修 改";
            this.button_user_manage.UseVisualStyleBackColor = true;
            // 
            // button_patient_manage
            // 
            this.button_patient_manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_patient_manage.Location = new System.Drawing.Point(546, 12);
            this.button_patient_manage.Name = "button_patient_manage";
            this.button_patient_manage.Size = new System.Drawing.Size(118, 44);
            this.button_patient_manage.TabIndex = 3;
            this.button_patient_manage.Text = "新 建";
            this.button_patient_manage.UseVisualStyleBackColor = true;
            this.button_patient_manage.Click += new System.EventHandler(this.button_patient_manage_Click);
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.BackColor = System.Drawing.Color.Transparent;
            this.label_Info.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Info.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_Info.Location = new System.Drawing.Point(41, 32);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(0, 24);
            this.label_Info.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.listView_patList);
            this.panel1.Location = new System.Drawing.Point(130, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 414);
            this.panel1.TabIndex = 3;
            // 
            // listView_patList
            // 
            this.listView_patList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_patList.FullRowSelect = true;
            this.listView_patList.Location = new System.Drawing.Point(0, 0);
            this.listView_patList.MultiSelect = false;
            this.listView_patList.Name = "listView_patList";
            this.listView_patList.Size = new System.Drawing.Size(701, 414);
            this.listView_patList.TabIndex = 1;
            this.listView_patList.UseCompatibleStateImageBehavior = false;
            this.listView_patList.View = System.Windows.Forms.View.Details;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.label_msg);
            this.panel7.Controls.Add(this.button_query);
            this.panel7.Controls.Add(this.textBox_queryWhere);
            this.panel7.Location = new System.Drawing.Point(319, 85);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(512, 64);
            this.panel7.TabIndex = 32;
            // 
            // label_msg
            // 
            this.label_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_msg.AutoSize = true;
            this.label_msg.BackColor = System.Drawing.SystemColors.Window;
            this.label_msg.Font = new System.Drawing.Font("华文仿宋", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_msg.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label_msg.Location = new System.Drawing.Point(131, 19);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(181, 21);
            this.label_msg.TabIndex = 31;
            this.label_msg.Text = "病人姓名或身份证号";
            this.label_msg.Click += new System.EventHandler(this.label_msg_Click);
            // 
            // button_query
            // 
            this.button_query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_query.Location = new System.Drawing.Point(419, 5);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(90, 44);
            this.button_query.TabIndex = 6;
            this.button_query.Text = "查 询";
            this.button_query.UseVisualStyleBackColor = true;
            this.button_query.Click += new System.EventHandler(this.button_query_Click);
            // 
            // textBox_queryWhere
            // 
            this.textBox_queryWhere.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_queryWhere.Location = new System.Drawing.Point(76, 14);
            this.textBox_queryWhere.MaxLength = 24;
            this.textBox_queryWhere.Name = "textBox_queryWhere";
            this.textBox_queryWhere.Size = new System.Drawing.Size(323, 30);
            this.textBox_queryWhere.TabIndex = 31;
            this.textBox_queryWhere.TextChanged += new System.EventHandler(this.textBox_queryWhere_TextChanged);
            // 
            // PatientManageDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(924, 591);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatientManageDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PatientManageDlg_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_sys_Setting;
        private System.Windows.Forms.Button button_user_manage;
        private System.Windows.Forms.Button button_patient_manage;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView_patList;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.TextBox textBox_queryWhere;
    }
}