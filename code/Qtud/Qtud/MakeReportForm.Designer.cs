namespace Qtud.Qtud
{
    partial class MakeReportForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeReportForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Retmainform = new System.Windows.Forms.Button();
            this.button_print = new System.Windows.Forms.Button();
            this.button_PageSetup = new System.Windows.Forms.Button();
            this.button_PrintPreview = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.panel_Report = new System.Windows.Forms.Panel();
            this.panel_print = new System.Windows.Forms.Panel();
            this.panel_Context = new MyPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.report_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel_Report.SuspendLayout();
            this.panel_print.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.button_Retmainform);
            this.panel1.Controls.Add(this.button_print);
            this.panel1.Controls.Add(this.button_PageSetup);
            this.panel1.Controls.Add(this.button_PrintPreview);
            this.panel1.Controls.Add(this.button_Back);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 79);
            this.panel1.TabIndex = 1;
            // 
            // button_Retmainform
            // 
            this.button_Retmainform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Retmainform.Location = new System.Drawing.Point(875, 12);
            this.button_Retmainform.Name = "button_Retmainform";
            this.button_Retmainform.Size = new System.Drawing.Size(118, 44);
            this.button_Retmainform.TabIndex = 5;
            this.button_Retmainform.Text = "返回首页";
            this.button_Retmainform.UseVisualStyleBackColor = true;
            this.button_Retmainform.Click += new System.EventHandler(this.button_Retmainform_Click);
            // 
            // button_print
            // 
            this.button_print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_print.Location = new System.Drawing.Point(627, 12);
            this.button_print.Name = "button_print";
            this.button_print.Size = new System.Drawing.Size(118, 44);
            this.button_print.TabIndex = 4;
            this.button_print.Text = "打  印";
            this.button_print.UseVisualStyleBackColor = true;
            this.button_print.Click += new System.EventHandler(this.button_print_Click);
            // 
            // button_PageSetup
            // 
            this.button_PageSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PageSetup.Location = new System.Drawing.Point(379, 12);
            this.button_PageSetup.Name = "button_PageSetup";
            this.button_PageSetup.Size = new System.Drawing.Size(118, 44);
            this.button_PageSetup.TabIndex = 3;
            this.button_PageSetup.Text = "页面设置";
            this.button_PageSetup.UseVisualStyleBackColor = true;
            this.button_PageSetup.Click += new System.EventHandler(this.button_PageSetup_Click);
            // 
            // button_PrintPreview
            // 
            this.button_PrintPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_PrintPreview.Location = new System.Drawing.Point(503, 12);
            this.button_PrintPreview.Name = "button_PrintPreview";
            this.button_PrintPreview.Size = new System.Drawing.Size(118, 44);
            this.button_PrintPreview.TabIndex = 2;
            this.button_PrintPreview.Text = "打印预览";
            this.button_PrintPreview.UseVisualStyleBackColor = true;
            this.button_PrintPreview.Click += new System.EventHandler(this.button_PrintPreview_Click);
            // 
            // button_Back
            // 
            this.button_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Back.Location = new System.Drawing.Point(751, 12);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(118, 44);
            this.button_Back.TabIndex = 1;
            this.button_Back.Text = "返回上一页";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // panel_Report
            // 
            this.panel_Report.AutoScroll = true;
            this.panel_Report.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_Report.BackgroundImage")));
            this.panel_Report.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Report.Controls.Add(this.panel_print);
            this.panel_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Report.Location = new System.Drawing.Point(0, 79);
            this.panel_Report.Name = "panel_Report";
            this.panel_Report.Size = new System.Drawing.Size(1006, 644);
            this.panel_Report.TabIndex = 4;
            // 
            // panel_print
            // 
            this.panel_print.BackColor = System.Drawing.Color.Transparent;
            this.panel_print.Controls.Add(this.panel_Context);
            this.panel_print.Location = new System.Drawing.Point(316, 6);
            this.panel_print.Name = "panel_print";
            this.panel_print.Size = new System.Drawing.Size(395, 635);
            this.panel_print.TabIndex = 0;
            // 
            // panel_Context
            // 
            this.panel_Context.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Context.BackColor = System.Drawing.Color.White;
            this.panel_Context.ContextMenuStrip = this.contextMenuStrip1;
            this.panel_Context.Location = new System.Drawing.Point(2, 4);
            this.panel_Context.Name = "panel_Context";
            this.panel_Context.Size = new System.Drawing.Size(392, 560);
            this.panel_Context.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.report_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 28);
            // 
            // report_ToolStripMenuItem
            // 
            this.report_ToolStripMenuItem.Name = "report_ToolStripMenuItem";
            this.report_ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.report_ToolStripMenuItem.Text = "不打印报告页";
            this.report_ToolStripMenuItem.Visible = false;
            this.report_ToolStripMenuItem.Click += new System.EventHandler(this.report_ToolStripMenuItem_Click);
            // 
            // MakeReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 723);
            this.Controls.Add(this.panel_Report);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MakeReportForm";
            this.Text = "检查报告";
            this.Load += new System.EventHandler(this.MakeReportForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel_Report.ResumeLayout(false);
            this.panel_print.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_PrintPreview;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Panel panel_Report;
        private System.Windows.Forms.Button button_PageSetup;
        private System.Windows.Forms.Button button_print;
        private System.Windows.Forms.Panel panel_print;
        //private System.Windows.Forms.Panel panel_Context;
        private System.Windows.Forms.Button button_Retmainform;

        private MyPanel panel_Context;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem report_ToolStripMenuItem;

    }
}