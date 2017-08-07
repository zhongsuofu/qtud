namespace Qtud.Qtud
{
    partial class ReportListView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportListView));
            this.listView_report = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_up = new System.Windows.Forms.Button();
            this.button_Create_rep = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_report
            // 
            this.listView_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_report.FullRowSelect = true;
            this.listView_report.Location = new System.Drawing.Point(0, 0);
            this.listView_report.MultiSelect = false;
            this.listView_report.Name = "listView_report";
            this.listView_report.Size = new System.Drawing.Size(424, 401);
            this.listView_report.TabIndex = 0;
            this.listView_report.UseCompatibleStateImageBehavior = false;
            this.listView_report.View = System.Windows.Forms.View.Details;
            this.listView_report.DoubleClick += new System.EventHandler(this.listView_report_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.listView_report);
            this.panel1.Location = new System.Drawing.Point(204, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 401);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.button_up);
            this.panel2.Controls.Add(this.button_Create_rep);
            this.panel2.Controls.Add(this.label_Info);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 79);
            this.panel2.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Location = new System.Drawing.Point(12, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(341, 66);
            this.panel4.TabIndex = 7;
            // 
            // button_up
            // 
            this.button_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_up.Location = new System.Drawing.Point(680, 12);
            this.button_up.Name = "button_up";
            this.button_up.Size = new System.Drawing.Size(118, 44);
            this.button_up.TabIndex = 6;
            this.button_up.Text = "返回上一页";
            this.button_up.UseVisualStyleBackColor = true;
            this.button_up.Click += new System.EventHandler(this.button_up_Click);
            // 
            // button_Create_rep
            // 
            this.button_Create_rep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Create_rep.Location = new System.Drawing.Point(556, 12);
            this.button_Create_rep.Name = "button_Create_rep";
            this.button_Create_rep.Size = new System.Drawing.Size(118, 44);
            this.button_Create_rep.TabIndex = 5;
            this.button_Create_rep.Text = "新建报告";
            this.button_Create_rep.UseVisualStyleBackColor = true;
            this.button_Create_rep.Click += new System.EventHandler(this.button_Create_rep_Click);
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
            // ReportListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(825, 602);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReportListView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportListView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportListView_FormClosing);
            this.Load += new System.EventHandler(this.ReportListView_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_report;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_up;
        private System.Windows.Forms.Button button_Create_rep;
        private System.Windows.Forms.Label label_Info;
    }
}