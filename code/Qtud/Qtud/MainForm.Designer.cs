namespace Qtud.Qtud
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_ADDP = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBox_Check_date = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox_checkMode = new System.Windows.Forms.ComboBox();
            this.listBox_checkTime = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox_usb_Dev = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox_checkNO = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_nll = new System.Windows.Forms.CheckBox();
            this.checkBox_nl = new System.Windows.Forms.CheckBox();
            this.checkBox_Pdet = new System.Windows.Forms.CheckBox();
            this.checkBox_Pabd = new System.Windows.Forms.CheckBox();
            this.checkBox_Pves = new System.Windows.Forms.CheckBox();
            this.panel_11 = new System.Windows.Forms.Panel();
            //this.panel_draw = new System.Windows.Forms.Panel();
            this.panel_draw = new MyPanel();

            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.parient_list = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_Searchp = new System.Windows.Forms.Button();
            this.textBox_SearchParient = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.病人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel_11.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(232)))));
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1623, 723);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_ADDP});
            this.toolStrip1.Location = new System.Drawing.Point(308, 39);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1315, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_ADDP
            // 
            this.toolStripButton_ADDP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_ADDP.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_ADDP.Image")));
            this.toolStripButton_ADDP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ADDP.Name = "toolStripButton_ADDP";
            this.toolStripButton_ADDP.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_ADDP.Text = "ADDP";
            this.toolStripButton_ADDP.Click += new System.EventHandler(this.toolStripButton_ADDP_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel_11, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(308, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.989522F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.59649F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.43275F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1315, 684);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox5, 4, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 37);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1309, 257);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listBox_Check_date);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(460, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(268, 251);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "检测日期";
            // 
            // listBox_Check_date
            // 
            this.listBox_Check_date.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_Check_date.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_Check_date.ForeColor = System.Drawing.Color.DarkBlue;
            this.listBox_Check_date.FormattingEnabled = true;
            this.listBox_Check_date.ItemHeight = 23;
            this.listBox_Check_date.Location = new System.Drawing.Point(12, 26);
            this.listBox_Check_date.Name = "listBox_Check_date";
            this.listBox_Check_date.Size = new System.Drawing.Size(247, 211);
            this.listBox_Check_date.TabIndex = 0;
            this.listBox_Check_date.SelectedValueChanged += new System.EventHandler(this.listBox_Check_time_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox_checkMode);
            this.groupBox3.Controls.Add(this.listBox_checkTime);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(734, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 251);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "检测时间";
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
            this.comboBox_checkMode.Location = new System.Drawing.Point(11, 26);
            this.comboBox_checkMode.Name = "comboBox_checkMode";
            this.comboBox_checkMode.Size = new System.Drawing.Size(247, 25);
            this.comboBox_checkMode.TabIndex = 1;
            this.comboBox_checkMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_checkMode_SelectedIndexChanged);
            this.comboBox_checkMode.SelectedValueChanged += new System.EventHandler(this.comboBox_checkMode_SelectedValueChanged);
            // 
            // listBox_checkTime
            // 
            this.listBox_checkTime.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_checkTime.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_checkTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.listBox_checkTime.FormattingEnabled = true;
            this.listBox_checkTime.ItemHeight = 23;
            this.listBox_checkTime.Location = new System.Drawing.Point(11, 72);
            this.listBox_checkTime.Name = "listBox_checkTime";
            this.listBox_checkTime.Size = new System.Drawing.Size(247, 165);
            this.listBox_checkTime.TabIndex = 0;
            this.listBox_checkTime.SelectedValueChanged += new System.EventHandler(this.listBox_checkTime_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.listBox_usb_Dev);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 251);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "检测设备";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(16, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox_usb_Dev
            // 
            this.listBox_usb_Dev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_usb_Dev.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_usb_Dev.ForeColor = System.Drawing.Color.DarkBlue;
            this.listBox_usb_Dev.FormattingEnabled = true;
            this.listBox_usb_Dev.ItemHeight = 23;
            this.listBox_usb_Dev.Location = new System.Drawing.Point(16, 26);
            this.listBox_usb_Dev.Name = "listBox_usb_Dev";
            this.listBox_usb_Dev.Size = new System.Drawing.Size(163, 165);
            this.listBox_usb_Dev.TabIndex = 0;
            this.listBox_usb_Dev.SelectedValueChanged += new System.EventHandler(this.listBox_usb_Dev_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox_checkNO);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(199, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "检测编号";
            // 
            // listBox_checkNO
            // 
            this.listBox_checkNO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_checkNO.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_checkNO.ForeColor = System.Drawing.Color.DarkBlue;
            this.listBox_checkNO.FormattingEnabled = true;
            this.listBox_checkNO.ItemHeight = 23;
            this.listBox_checkNO.Location = new System.Drawing.Point(12, 26);
            this.listBox_checkNO.Name = "listBox_checkNO";
            this.listBox_checkNO.Size = new System.Drawing.Size(230, 211);
            this.listBox_checkNO.TabIndex = 0;
            this.listBox_checkNO.SelectedValueChanged += new System.EventHandler(this.listBox_check_Data_SelectedValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_nll);
            this.groupBox5.Controls.Add(this.checkBox_nl);
            this.groupBox5.Controls.Add(this.checkBox_Pdet);
            this.groupBox5.Controls.Add(this.checkBox_Pabd);
            this.groupBox5.Controls.Add(this.checkBox_Pves);
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(1008, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(264, 168);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "曲线通道";
            // 
            // checkBox_nll
            // 
            this.checkBox_nll.AutoSize = true;
            this.checkBox_nll.Checked = true;
            this.checkBox_nll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_nll.Location = new System.Drawing.Point(47, 140);
            this.checkBox_nll.Name = "checkBox_nll";
            this.checkBox_nll.Size = new System.Drawing.Size(115, 21);
            this.checkBox_nll.TabIndex = 4;
            this.checkBox_nll.Text = "尿流率曲线";
            this.checkBox_nll.UseVisualStyleBackColor = true;
            // 
            // checkBox_nl
            // 
            this.checkBox_nl.AutoSize = true;
            this.checkBox_nl.Checked = true;
            this.checkBox_nl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_nl.Location = new System.Drawing.Point(47, 113);
            this.checkBox_nl.Name = "checkBox_nl";
            this.checkBox_nl.Size = new System.Drawing.Size(98, 21);
            this.checkBox_nl.TabIndex = 3;
            this.checkBox_nl.Text = "尿量曲线";
            this.checkBox_nl.UseVisualStyleBackColor = true;
            // 
            // checkBox_Pdet
            // 
            this.checkBox_Pdet.AutoSize = true;
            this.checkBox_Pdet.Checked = true;
            this.checkBox_Pdet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Pdet.Location = new System.Drawing.Point(47, 86);
            this.checkBox_Pdet.Name = "checkBox_Pdet";
            this.checkBox_Pdet.Size = new System.Drawing.Size(185, 21);
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
            this.checkBox_Pabd.Location = new System.Drawing.Point(47, 59);
            this.checkBox_Pabd.Name = "checkBox_Pabd";
            this.checkBox_Pabd.Size = new System.Drawing.Size(168, 21);
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
            this.checkBox_Pves.Location = new System.Drawing.Point(47, 32);
            this.checkBox_Pves.Name = "checkBox_Pves";
            this.checkBox_Pves.Size = new System.Drawing.Size(168, 21);
            this.checkBox_Pves.TabIndex = 0;
            this.checkBox_Pves.Text = "Pves膀胱压力曲线";
            this.checkBox_Pves.UseVisualStyleBackColor = true;
            this.checkBox_Pves.CheckedChanged += new System.EventHandler(this.checkBox_Pves_CheckedChanged);
            // 
            // panel_11
            // 
            this.panel_11.Controls.Add(this.panel_draw);
            this.panel_11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_11.Location = new System.Drawing.Point(3, 300);
            this.panel_11.Name = "panel_11";
            this.panel_11.Size = new System.Drawing.Size(1309, 381);
            this.panel_11.TabIndex = 2;
            // 
            // panel_draw
            // 
            this.panel_draw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_draw.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_draw.Location = new System.Drawing.Point(30, 20);
            this.panel_draw.Name = "panel_draw";
            this.panel_draw.Size = new System.Drawing.Size(951, 352);
            this.panel_draw.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.parient_list, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.47369F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(308, 684);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // parient_list
            // 
            this.parient_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parient_list.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.parient_list.ForeColor = System.Drawing.Color.DarkBlue;
            this.parient_list.FormattingEnabled = true;
            this.parient_list.ItemHeight = 30;
            this.parient_list.Location = new System.Drawing.Point(3, 75);
            this.parient_list.Name = "parient_list";
            this.parient_list.Size = new System.Drawing.Size(302, 606);
            this.parient_list.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_Searchp);
            this.panel2.Controls.Add(this.textBox_SearchParient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 66);
            this.panel2.TabIndex = 2;
            // 
            // button_Searchp
            // 
            this.button_Searchp.Location = new System.Drawing.Point(224, 12);
            this.button_Searchp.Name = "button_Searchp";
            this.button_Searchp.Size = new System.Drawing.Size(75, 38);
            this.button_Searchp.TabIndex = 2;
            this.button_Searchp.Text = "查询";
            this.button_Searchp.UseVisualStyleBackColor = true;
            // 
            // textBox_SearchParient
            // 
            this.textBox_SearchParient.Location = new System.Drawing.Point(12, 20);
            this.textBox_SearchParient.Name = "textBox_SearchParient";
            this.textBox_SearchParient.Size = new System.Drawing.Size(206, 27);
            this.textBox_SearchParient.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.病人ToolStripMenuItem,
            this.打印ToolStripMenuItem,
            this.系统用户ToolStripMenuItem,
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1623, 39);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 病人ToolStripMenuItem
            // 
            this.病人ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem});
            this.病人ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.病人ToolStripMenuItem.Name = "病人ToolStripMenuItem";
            this.病人ToolStripMenuItem.Size = new System.Drawing.Size(122, 35);
            this.病人ToolStripMenuItem.Text = "病人信息";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(136, 36);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 打印ToolStripMenuItem
            // 
            this.打印ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            this.打印ToolStripMenuItem.Size = new System.Drawing.Size(74, 35);
            this.打印ToolStripMenuItem.Text = "打印";
            // 
            // 系统用户ToolStripMenuItem
            // 
            this.系统用户ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.系统用户ToolStripMenuItem.Name = "系统用户ToolStripMenuItem";
            this.系统用户ToolStripMenuItem.Size = new System.Drawing.Size(122, 35);
            this.系统用户ToolStripMenuItem.Text = "系统用户";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数配置ToolStripMenuItem,
            this.版本ToolStripMenuItem});
            this.系统ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(74, 35);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 参数配置ToolStripMenuItem
            // 
            this.参数配置ToolStripMenuItem.Name = "参数配置ToolStripMenuItem";
            this.参数配置ToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.参数配置ToolStripMenuItem.Text = "参数配置";
            // 
            // 版本ToolStripMenuItem
            // 
            this.版本ToolStripMenuItem.Name = "版本ToolStripMenuItem";
            this.版本ToolStripMenuItem.Size = new System.Drawing.Size(184, 36);
            this.版本ToolStripMenuItem.Text = "版本";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 723);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainForm";
            this.Text = "尿动力数据管理系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel_11.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox parient_list;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 病人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 版本ToolStripMenuItem;
        private System.Windows.Forms.Button button_Searchp;
        private System.Windows.Forms.TextBox textBox_SearchParient;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_ADDP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox_Check_date;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox_Pdet;
        private System.Windows.Forms.CheckBox checkBox_Pabd;
        private System.Windows.Forms.CheckBox checkBox_Pves;
        private System.Windows.Forms.CheckBox checkBox_nl;
        private System.Windows.Forms.CheckBox checkBox_nll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox_checkNO;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox_usb_Dev;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox_checkTime;
        private System.Windows.Forms.Panel panel_11;
        private System.Windows.Forms.ComboBox comboBox_checkMode;
        //private System.Windows.Forms.Panel panel_draw;
        private MyPanel panel_draw;

    }
}

