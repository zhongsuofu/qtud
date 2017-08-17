namespace Qtud.Qtud
{
    partial class MainFrom_Curve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom_Curve));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Add_Report = new System.Windows.Forms.Button();
            this.button_make_Report = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_show_history = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Export = new System.Windows.Forms.Button();
            this.comboBox_checkMode = new System.Windows.Forms.ComboBox();
            this.treeView_File = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.All_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem0 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.Export_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Allcancel_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.refresh_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listBox_SelSeg = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_deleteItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_nll = new System.Windows.Forms.CheckBox();
            this.checkBox_nl = new System.Windows.Forms.CheckBox();
            this.checkBox_Pdet = new System.Windows.Forms.CheckBox();
            this.checkBox_Pabd = new System.Windows.Forms.CheckBox();
            this.checkBox_Pves = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip_Curve = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_AddReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.srcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Range_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_Draw = new  MyPanel();
            this.label_tip = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip_TreeView.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip_deleteItem.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.contextMenuStrip_Curve.SuspendLayout();
            this.panel_Draw.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.button_Save);
            this.panel1.Controls.Add(this.button_Add_Report);
            this.panel1.Controls.Add(this.button_make_Report);
            this.panel1.Controls.Add(this.button_Back);
            this.panel1.Controls.Add(this.label_Info);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1258, 79);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button_Add_Report
            // 
            this.button_Add_Report.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Add_Report.Location = new System.Drawing.Point(881, 12);
            this.button_Add_Report.Name = "button_Add_Report";
            this.button_Add_Report.Size = new System.Drawing.Size(118, 44);
            this.button_Add_Report.TabIndex = 3;
            this.button_Add_Report.Text = "加入报告";
            this.button_Add_Report.UseVisualStyleBackColor = true;
            this.button_Add_Report.Click += new System.EventHandler(this.button_Add_Report_Click);
            // 
            // button_make_Report
            // 
            this.button_make_Report.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_make_Report.Location = new System.Drawing.Point(1005, 12);
            this.button_make_Report.Name = "button_make_Report";
            this.button_make_Report.Size = new System.Drawing.Size(118, 44);
            this.button_make_Report.TabIndex = 2;
            this.button_make_Report.Text = "生成报告";
            this.button_make_Report.UseVisualStyleBackColor = true;
            this.button_make_Report.Click += new System.EventHandler(this.button_make_Report_Click);
            // 
            // button_Back
            // 
            this.button_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Back.Location = new System.Drawing.Point(1129, 12);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(118, 44);
            this.button_Back.TabIndex = 1;
            this.button_Back.Text = "返 回";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // label_Info
            // 
            this.label_Info.AutoSize = true;
            this.label_Info.BackColor = System.Drawing.Color.Transparent;
            this.label_Info.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Info.ForeColor = System.Drawing.Color.DarkOrange;
            this.label_Info.Location = new System.Drawing.Point(41, 32);
            this.label_Info.Name = "label_Info";
            this.label_Info.Size = new System.Drawing.Size(85, 24);
            this.label_Info.TabIndex = 0;
            this.label_Info.Text = "患者：";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.button_show_history);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.Export);
            this.panel4.Controls.Add(this.comboBox_checkMode);
            this.panel4.Controls.Add(this.treeView_File);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 79);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(354, 885);
            this.panel4.TabIndex = 0;
            // 
            // button_show_history
            // 
            this.button_show_history.Location = new System.Drawing.Point(113, 18);
            this.button_show_history.Name = "button_show_history";
            this.button_show_history.Size = new System.Drawing.Size(102, 44);
            this.button_show_history.TabIndex = 5;
            this.button_show_history.Text = "历史数据";
            this.button_show_history.UseVisualStyleBackColor = true;
            this.button_show_history.Click += new System.EventHandler(this.button_show_history_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "检测模式";
            // 
            // Export
            // 
            this.Export.Location = new System.Drawing.Point(21, 18);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(86, 44);
            this.Export.TabIndex = 4;
            this.Export.Text = "导 出";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // comboBox_checkMode
            // 
            this.comboBox_checkMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_checkMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_checkMode.ForeColor = System.Drawing.Color.DarkBlue;
            this.comboBox_checkMode.FormattingEnabled = true;
            this.comboBox_checkMode.Items.AddRange(new object[] {
            "全  部",
            "0-畅通模式",
            "1-尿潴留模式",
            "2-定时模式",
            "3-定压模式",
            "4-定时定压模式",
            "5-分段定压模式",
            "6-尿动力检测模式"});
            this.comboBox_checkMode.Location = new System.Drawing.Point(111, 83);
            this.comboBox_checkMode.Name = "comboBox_checkMode";
            this.comboBox_checkMode.Size = new System.Drawing.Size(231, 28);
            this.comboBox_checkMode.TabIndex = 2;
            this.comboBox_checkMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_checkMode_SelectedIndexChanged);
            // 
            // treeView_File
            // 
            this.treeView_File.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView_File.ContextMenuStrip = this.contextMenuStrip_TreeView;
            this.treeView_File.Location = new System.Drawing.Point(12, 124);
            this.treeView_File.Name = "treeView_File";
            this.treeView_File.Size = new System.Drawing.Size(330, 727);
            this.treeView_File.TabIndex = 0;
            this.treeView_File.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_File_AfterCheck);
            this.treeView_File.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView_File_DrawNode);
            // 
            // contextMenuStrip_TreeView
            // 
            this.contextMenuStrip_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.All_ToolStripMenuItem,
            this.ToolStripMenuItem0,
            this.ToolStripMenuItem1,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3,
            this.ToolStripMenuItem4,
            this.ToolStripMenuItem5,
            this.ToolStripMenuItem6,
            this.Export_ToolStripMenuItem,
            this.toolStripSeparator2,
            this.Allcancel_ToolStripMenuItem,
            this.toolStripSeparator4,
            this.refresh_ToolStripMenuItem});
            this.contextMenuStrip_TreeView.Name = "contextMenuStrip_TreeView";
            this.contextMenuStrip_TreeView.Size = new System.Drawing.Size(199, 280);
            // 
            // All_ToolStripMenuItem
            // 
            this.All_ToolStripMenuItem.Name = "All_ToolStripMenuItem";
            this.All_ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.All_ToolStripMenuItem.Text = "全 部";
            this.All_ToolStripMenuItem.Click += new System.EventHandler(this.All_ToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem0
            // 
            this.ToolStripMenuItem0.Name = "ToolStripMenuItem0";
            this.ToolStripMenuItem0.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem0.Text = "0-畅通模式";
            this.ToolStripMenuItem0.Click += new System.EventHandler(this.ToolStripMenuItem0_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem1.Text = "1-尿潴留模式";
            this.ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // ToolStripMenuItem2
            // 
            this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
            this.ToolStripMenuItem2.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem2.Text = "2-定时模式";
            this.ToolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // ToolStripMenuItem3
            // 
            this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
            this.ToolStripMenuItem3.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem3.Text = "3-定压模式";
            this.ToolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // ToolStripMenuItem4
            // 
            this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
            this.ToolStripMenuItem4.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem4.Text = "4-定时定压模式";
            this.ToolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem4_Click);
            // 
            // ToolStripMenuItem5
            // 
            this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
            this.ToolStripMenuItem5.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem5.Text = "5-分段定压模式";
            this.ToolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
            // 
            // ToolStripMenuItem6
            // 
            this.ToolStripMenuItem6.Name = "ToolStripMenuItem6";
            this.ToolStripMenuItem6.Size = new System.Drawing.Size(198, 24);
            this.ToolStripMenuItem6.Text = "6-尿动力检测模式";
            this.ToolStripMenuItem6.Click += new System.EventHandler(this.ToolStripMenuItem6_Click);
            // 
            // Export_ToolStripMenuItem
            // 
            this.Export_ToolStripMenuItem.Name = "Export_ToolStripMenuItem";
            this.Export_ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.Export_ToolStripMenuItem.Text = "导 出";
            this.Export_ToolStripMenuItem.Click += new System.EventHandler(this.Export_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // Allcancel_ToolStripMenuItem
            // 
            this.Allcancel_ToolStripMenuItem.Name = "Allcancel_ToolStripMenuItem";
            this.Allcancel_ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.Allcancel_ToolStripMenuItem.Text = "全部取消";
            this.Allcancel_ToolStripMenuItem.Click += new System.EventHandler(this.全部取消ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(195, 6);
            // 
            // refresh_ToolStripMenuItem
            // 
            this.refresh_ToolStripMenuItem.Name = "refresh_ToolStripMenuItem";
            this.refresh_ToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.refresh_ToolStripMenuItem.Text = "刷 新";
            this.refresh_ToolStripMenuItem.Click += new System.EventHandler(this.refresh_ToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.listBox_SelSeg);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(973, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(285, 885);
            this.panel3.TabIndex = 2;
            // 
            // listBox_SelSeg
            // 
            this.listBox_SelSeg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_SelSeg.ContextMenuStrip = this.contextMenuStrip_deleteItem;
            this.listBox_SelSeg.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_SelSeg.ForeColor = System.Drawing.Color.DarkBlue;
            this.listBox_SelSeg.FormattingEnabled = true;
            this.listBox_SelSeg.ItemHeight = 23;
            this.listBox_SelSeg.Location = new System.Drawing.Point(6, 265);
            this.listBox_SelSeg.Name = "listBox_SelSeg";
            this.listBox_SelSeg.Size = new System.Drawing.Size(276, 579);
            this.listBox_SelSeg.TabIndex = 6;
            // 
            // contextMenuStrip_deleteItem
            // 
            this.contextMenuStrip_deleteItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7,
            this.删除全部ToolStripMenuItem,
            this.toolStripSeparator1,
            this.menoToolStripMenuItem});
            this.contextMenuStrip_deleteItem.Name = "contextMenuStrip_TreeView";
            this.contextMenuStrip_deleteItem.Size = new System.Drawing.Size(139, 82);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem7.Text = "删除选项";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // 删除全部ToolStripMenuItem
            // 
            this.删除全部ToolStripMenuItem.Name = "删除全部ToolStripMenuItem";
            this.删除全部ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.删除全部ToolStripMenuItem.Text = "删除全部";
            this.删除全部ToolStripMenuItem.Click += new System.EventHandler(this.删除全部ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // menoToolStripMenuItem
            // 
            this.menoToolStripMenuItem.Name = "menoToolStripMenuItem";
            this.menoToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.menoToolStripMenuItem.Text = "编辑备注";
            this.menoToolStripMenuItem.Click += new System.EventHandler(this.menoToolStripMenuItem_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_nll);
            this.groupBox5.Controls.Add(this.checkBox_nl);
            this.groupBox5.Controls.Add(this.checkBox_Pdet);
            this.groupBox5.Controls.Add(this.checkBox_Pabd);
            this.groupBox5.Controls.Add(this.checkBox_Pves);
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(7, 31);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(275, 212);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "曲线通道";
            // 
            // checkBox_nll
            // 
            this.checkBox_nll.AutoSize = true;
            this.checkBox_nll.Checked = true;
            this.checkBox_nll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_nll.ForeColor = System.Drawing.Color.White;
            this.checkBox_nll.Location = new System.Drawing.Point(37, 160);
            this.checkBox_nll.Name = "checkBox_nll";
            this.checkBox_nll.Size = new System.Drawing.Size(131, 24);
            this.checkBox_nll.TabIndex = 4;
            this.checkBox_nll.Text = "尿流率曲线";
            this.checkBox_nll.UseVisualStyleBackColor = true;
            this.checkBox_nll.CheckedChanged += new System.EventHandler(this.checkBox_nll_CheckedChanged);
            // 
            // checkBox_nl
            // 
            this.checkBox_nl.AutoSize = true;
            this.checkBox_nl.Checked = true;
            this.checkBox_nl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_nl.ForeColor = System.Drawing.Color.White;
            this.checkBox_nl.Location = new System.Drawing.Point(37, 133);
            this.checkBox_nl.Name = "checkBox_nl";
            this.checkBox_nl.Size = new System.Drawing.Size(111, 24);
            this.checkBox_nl.TabIndex = 3;
            this.checkBox_nl.Text = "尿量曲线";
            this.checkBox_nl.UseVisualStyleBackColor = true;
            this.checkBox_nl.CheckedChanged += new System.EventHandler(this.checkBox_nl_CheckedChanged);
            // 
            // checkBox_Pdet
            // 
            this.checkBox_Pdet.AutoSize = true;
            this.checkBox_Pdet.Checked = true;
            this.checkBox_Pdet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Pdet.ForeColor = System.Drawing.Color.White;
            this.checkBox_Pdet.Location = new System.Drawing.Point(37, 106);
            this.checkBox_Pdet.Name = "checkBox_Pdet";
            this.checkBox_Pdet.Size = new System.Drawing.Size(211, 24);
            this.checkBox_Pdet.TabIndex = 2;
            this.checkBox_Pdet.Text = "Pdet逼尿肌压力曲线";
            this.checkBox_Pdet.UseVisualStyleBackColor = true;
            this.checkBox_Pdet.CheckedChanged += new System.EventHandler(this.checkBox_Pdet_CheckedChanged);
            // 
            // checkBox_Pabd
            // 
            this.checkBox_Pabd.AutoSize = true;
            this.checkBox_Pabd.Checked = true;
            this.checkBox_Pabd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Pabd.ForeColor = System.Drawing.Color.White;
            this.checkBox_Pabd.Location = new System.Drawing.Point(37, 79);
            this.checkBox_Pabd.Name = "checkBox_Pabd";
            this.checkBox_Pabd.Size = new System.Drawing.Size(191, 24);
            this.checkBox_Pabd.TabIndex = 1;
            this.checkBox_Pabd.Text = "Pabd直肠压力曲线";
            this.checkBox_Pabd.UseVisualStyleBackColor = true;
            this.checkBox_Pabd.CheckedChanged += new System.EventHandler(this.checkBox_Pabd_CheckedChanged);
            // 
            // checkBox_Pves
            // 
            this.checkBox_Pves.AutoSize = true;
            this.checkBox_Pves.Checked = true;
            this.checkBox_Pves.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Pves.ForeColor = System.Drawing.Color.White;
            this.checkBox_Pves.Location = new System.Drawing.Point(37, 52);
            this.checkBox_Pves.Name = "checkBox_Pves";
            this.checkBox_Pves.Size = new System.Drawing.Size(191, 24);
            this.checkBox_Pves.TabIndex = 0;
            this.checkBox_Pves.Text = "Pves膀胱压力曲线";
            this.checkBox_Pves.UseVisualStyleBackColor = true;
            this.checkBox_Pves.CheckedChanged += new System.EventHandler(this.checkBox_Pves_CheckedChanged);
            // 
            // contextMenuStrip_Curve
            // 
            this.contextMenuStrip_Curve.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AddReport,
            this.toolStripSeparator3,
            this.srcToolStripMenuItem,
            this.Range_ToolStripMenuItem});
            this.contextMenuStrip_Curve.Name = "contextMenuStrip_TreeView";
            this.contextMenuStrip_Curve.Size = new System.Drawing.Size(139, 82);
            // 
            // toolStripMenuItem_AddReport
            // 
            this.toolStripMenuItem_AddReport.Name = "toolStripMenuItem_AddReport";
            this.toolStripMenuItem_AddReport.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem_AddReport.Text = "加入报告";
            this.toolStripMenuItem_AddReport.Click += new System.EventHandler(this.toolStripMenuItem_AddReport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(135, 6);
            // 
            // srcToolStripMenuItem
            // 
            this.srcToolStripMenuItem.Name = "srcToolStripMenuItem";
            this.srcToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.srcToolStripMenuItem.Text = "查看原图";
            this.srcToolStripMenuItem.Click += new System.EventHandler(this.srcToolStripMenuItem_Click);
            // 
            // Range_ToolStripMenuItem
            // 
            this.Range_ToolStripMenuItem.Name = "Range_ToolStripMenuItem";
            this.Range_ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.Range_ToolStripMenuItem.Text = "数据范围";
            this.Range_ToolStripMenuItem.Click += new System.EventHandler(this.Range_ToolStripMenuItem_Click);
            // 
            // panel_Draw
            // 
            this.panel_Draw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Draw.BackColor = System.Drawing.Color.Transparent;
            this.panel_Draw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel_Draw.ContextMenuStrip = this.contextMenuStrip_Curve;
            this.panel_Draw.Controls.Add(this.label_tip);
            this.panel_Draw.Location = new System.Drawing.Point(360, 85);
            this.panel_Draw.Name = "panel_Draw";
            this.panel_Draw.Size = new System.Drawing.Size(607, 867);
            this.panel_Draw.TabIndex = 3;
            this.panel_Draw.Click += new System.EventHandler(this.panel_Draw_Click);
            this.panel_Draw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_Draw_MouseDown);
            this.panel_Draw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_Draw_MouseMove);
            this.panel_Draw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_Draw_MouseUp);
            this.panel_Draw.Resize += new System.EventHandler(this.panel_Draw_Resize);
            // 
            // label_tip
            // 
            this.label_tip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_tip.AutoSize = true;
            this.label_tip.BackColor = System.Drawing.Color.Transparent;
            this.label_tip.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tip.ForeColor = System.Drawing.Color.DarkBlue;
            this.label_tip.Location = new System.Drawing.Point(12, 12);
            this.label_tip.Name = "label_tip";
            this.label_tip.Size = new System.Drawing.Size(75, 24);
            this.label_tip.TabIndex = 0;
            this.label_tip.Text = "label";
            this.label_tip.Visible = false;
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Location = new System.Drawing.Point(757, 12);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(118, 44);
            this.button_Save.TabIndex = 4;
            this.button_Save.Text = "保 存";
            this.button_Save.UseVisualStyleBackColor = true;
            // 
            // MainFrom_Curve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1258, 964);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel_Draw);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrom_Curve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "尿动力数据管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrom2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.contextMenuStrip_TreeView.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip_deleteItem.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.contextMenuStrip_Curve.ResumeLayout(false);
            this.panel_Draw.ResumeLayout(false);
            this.panel_Draw.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView treeView_File;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox_nll;
        private System.Windows.Forms.CheckBox checkBox_nl;
        private System.Windows.Forms.CheckBox checkBox_Pdet;
        private System.Windows.Forms.CheckBox checkBox_Pabd;
        private System.Windows.Forms.CheckBox checkBox_Pves;
        private System.Windows.Forms.ListBox listBox_SelSeg;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_make_Report;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TreeView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem0;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem All_ToolStripMenuItem;
        private System.Windows.Forms.Button button_Add_Report;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Curve;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AddReport;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_deleteItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem 删除全部ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Allcancel_ToolStripMenuItem;
        private System.Windows.Forms.Label label_tip;
        //private System.Windows.Forms.Panel panel_Draw;
        private MyPanel panel_Draw;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem srcToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem refresh_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Range_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Export_ToolStripMenuItem;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.ComboBox comboBox_checkMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_show_history;
        private System.Windows.Forms.Button button_Save;
        
    }
}