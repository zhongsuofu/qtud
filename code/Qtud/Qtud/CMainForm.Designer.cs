namespace Qtud.Qtud
{
    partial class CMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1_bs = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label_msg = new System.Windows.Forms.Label();
            this.button_query = new System.Windows.Forms.Button();
            this.textBox_queryWhere = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.new_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_meno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_phone = new System.Windows.Forms.TextBox();
            this.textBox_cardid = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioBtn_man = new System.Windows.Forms.RadioButton();
            this.radioBTN_woman = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_sys_Setting = new System.Windows.Forms.Button();
            this.button_user_manage = new System.Windows.Forms.Button();
            this.button_patient_manage = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.parient_list = new System.Windows.Forms.ListBox();
            this.listView_patList = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Create_rep = new System.Windows.Forms.Button();
            this.button_edit_report = new System.Windows.Forms.Button();
            this.listView_report = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.parient_list);
            this.panel1.Controls.Add(this.textBox1_bs);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox_meno);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox_phone);
            this.panel1.Controls.Add(this.textBox_cardid);
            this.panel1.Controls.Add(this.textBox_name);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.radioBtn_man);
            this.panel1.Controls.Add(this.radioBTN_woman);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(460, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(573, 751);
            this.panel1.TabIndex = 0;
            // 
            // textBox1_bs
            // 
            this.textBox1_bs.Enabled = false;
            this.textBox1_bs.Location = new System.Drawing.Point(205, 329);
            this.textBox1_bs.MaxLength = 64;
            this.textBox1_bs.Multiline = true;
            this.textBox1_bs.Name = "textBox1_bs";
            this.textBox1_bs.ReadOnly = true;
            this.textBox1_bs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1_bs.Size = new System.Drawing.Size(286, 106);
            this.textBox1_bs.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(83, 330);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 25);
            this.label7.TabIndex = 31;
            this.label7.Text = "病  史:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.listView_patList);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(460, 751);
            this.panel5.TabIndex = 30;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.button1);
            this.panel7.Controls.Add(this.button2);
            this.panel7.Controls.Add(this.button3);
            this.panel7.Controls.Add(this.label_msg);
            this.panel7.Controls.Add(this.button_query);
            this.panel7.Controls.Add(this.textBox_queryWhere);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(460, 138);
            this.panel7.TabIndex = 31;
            // 
            // label_msg
            // 
            this.label_msg.AutoSize = true;
            this.label_msg.BackColor = System.Drawing.SystemColors.Window;
            this.label_msg.Font = new System.Drawing.Font("华文仿宋", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_msg.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label_msg.Location = new System.Drawing.Point(109, 96);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(181, 21);
            this.label_msg.TabIndex = 31;
            this.label_msg.Text = "患者姓名或身份证号";
            this.label_msg.Click += new System.EventHandler(this.label_msg_Click);
            // 
            // button_query
            // 
            this.button_query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_query.Location = new System.Drawing.Point(355, 84);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(90, 44);
            this.button_query.TabIndex = 6;
            this.button_query.Text = "查 询";
            this.button_query.UseVisualStyleBackColor = true;
            this.button_query.Click += new System.EventHandler(this.button_query_Click);
            // 
            // textBox_queryWhere
            // 
            this.textBox_queryWhere.Location = new System.Drawing.Point(45, 91);
            this.textBox_queryWhere.MaxLength = 24;
            this.textBox_queryWhere.Name = "textBox_queryWhere";
            this.textBox_queryWhere.Size = new System.Drawing.Size(291, 30);
            this.textBox_queryWhere.TabIndex = 31;
            this.textBox_queryWhere.TextChanged += new System.EventHandler(this.textBox_queryWhere_TextChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.listView_report);
            this.panel6.Controls.Add(this.button_edit_report);
            this.panel6.Controls.Add(this.button_Create_rep);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(1033, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(424, 751);
            this.panel6.TabIndex = 31;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new_ToolStripMenuItem,
            this.edit_ToolStripMenuItem,
            this.delete_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 76);
            // 
            // new_ToolStripMenuItem
            // 
            this.new_ToolStripMenuItem.Name = "new_ToolStripMenuItem";
            this.new_ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.new_ToolStripMenuItem.Text = "新建";
            this.new_ToolStripMenuItem.Click += new System.EventHandler(this.new_ToolStripMenuItem_Click);
            // 
            // edit_ToolStripMenuItem
            // 
            this.edit_ToolStripMenuItem.Name = "edit_ToolStripMenuItem";
            this.edit_ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.edit_ToolStripMenuItem.Text = "修改";
            this.edit_ToolStripMenuItem.Click += new System.EventHandler(this.edit_ToolStripMenuItem_Click);
            // 
            // delete_ToolStripMenuItem
            // 
            this.delete_ToolStripMenuItem.Name = "delete_ToolStripMenuItem";
            this.delete_ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.delete_ToolStripMenuItem.Text = "删除";
            this.delete_ToolStripMenuItem.Click += new System.EventHandler(this.delete_ToolStripMenuItem_Click);
            // 
            // textBox_meno
            // 
            this.textBox_meno.Enabled = false;
            this.textBox_meno.Location = new System.Drawing.Point(205, 464);
            this.textBox_meno.MaxLength = 64;
            this.textBox_meno.Multiline = true;
            this.textBox_meno.Name = "textBox_meno";
            this.textBox_meno.ReadOnly = true;
            this.textBox_meno.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_meno.Size = new System.Drawing.Size(286, 106);
            this.textBox_meno.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(83, 465);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 25);
            this.label5.TabIndex = 25;
            this.label5.Text = "备  注:";
            // 
            // textBox_phone
            // 
            this.textBox_phone.Enabled = false;
            this.textBox_phone.Location = new System.Drawing.Point(205, 272);
            this.textBox_phone.MaxLength = 16;
            this.textBox_phone.Name = "textBox_phone";
            this.textBox_phone.ReadOnly = true;
            this.textBox_phone.Size = new System.Drawing.Size(286, 30);
            this.textBox_phone.TabIndex = 24;
            // 
            // textBox_cardid
            // 
            this.textBox_cardid.Enabled = false;
            this.textBox_cardid.Location = new System.Drawing.Point(205, 93);
            this.textBox_cardid.MaxLength = 24;
            this.textBox_cardid.Name = "textBox_cardid";
            this.textBox_cardid.ReadOnly = true;
            this.textBox_cardid.Size = new System.Drawing.Size(286, 30);
            this.textBox_cardid.TabIndex = 23;
            // 
            // textBox_name
            // 
            this.textBox_name.Enabled = false;
            this.textBox_name.Location = new System.Drawing.Point(205, 155);
            this.textBox_name.MaxLength = 32;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.ReadOnly = true;
            this.textBox_name.Size = new System.Drawing.Size(286, 30);
            this.textBox_name.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(83, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 25);
            this.label4.TabIndex = 21;
            this.label4.Text = "电  话:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(83, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "性  别:";
            // 
            // radioBtn_man
            // 
            this.radioBtn_man.AutoSize = true;
            this.radioBtn_man.Enabled = false;
            this.radioBtn_man.ForeColor = System.Drawing.Color.White;
            this.radioBtn_man.Location = new System.Drawing.Point(205, 213);
            this.radioBtn_man.Name = "radioBtn_man";
            this.radioBtn_man.Size = new System.Drawing.Size(60, 24);
            this.radioBtn_man.TabIndex = 19;
            this.radioBtn_man.Text = " 男";
            this.radioBtn_man.UseVisualStyleBackColor = true;
            // 
            // radioBTN_woman
            // 
            this.radioBTN_woman.AutoSize = true;
            this.radioBTN_woman.Checked = true;
            this.radioBTN_woman.Enabled = false;
            this.radioBTN_woman.ForeColor = System.Drawing.Color.White;
            this.radioBTN_woman.Location = new System.Drawing.Point(292, 213);
            this.radioBTN_woman.Name = "radioBTN_woman";
            this.radioBTN_woman.Size = new System.Drawing.Size(60, 24);
            this.radioBTN_woman.TabIndex = 18;
            this.radioBTN_woman.TabStop = true;
            this.radioBTN_woman.Text = " 女";
            this.radioBTN_woman.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(57, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "身份证号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(83, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "姓  名:";
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
            this.panel2.Size = new System.Drawing.Size(1457, 79);
            this.panel2.TabIndex = 1;
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
            this.button_sys_Setting.Enabled = false;
            this.button_sys_Setting.Location = new System.Drawing.Point(1327, 12);
            this.button_sys_Setting.Name = "button_sys_Setting";
            this.button_sys_Setting.Size = new System.Drawing.Size(118, 44);
            this.button_sys_Setting.TabIndex = 5;
            this.button_sys_Setting.Text = "系统配置";
            this.button_sys_Setting.UseVisualStyleBackColor = true;
            this.button_sys_Setting.Click += new System.EventHandler(this.button_sys_Setting_Click);
            // 
            // button_user_manage
            // 
            this.button_user_manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_user_manage.Enabled = false;
            this.button_user_manage.Location = new System.Drawing.Point(1203, 12);
            this.button_user_manage.Name = "button_user_manage";
            this.button_user_manage.Size = new System.Drawing.Size(118, 44);
            this.button_user_manage.TabIndex = 4;
            this.button_user_manage.Text = "用户管理";
            this.button_user_manage.UseVisualStyleBackColor = true;
            this.button_user_manage.Click += new System.EventHandler(this.button_user_manage_Click);
            // 
            // button_patient_manage
            // 
            this.button_patient_manage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_patient_manage.Location = new System.Drawing.Point(1079, 12);
            this.button_patient_manage.Name = "button_patient_manage";
            this.button_patient_manage.Size = new System.Drawing.Size(118, 44);
            this.button_patient_manage.TabIndex = 3;
            this.button_patient_manage.Text = "患者管理";
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1457, 751);
            this.panel3.TabIndex = 22;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(327, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 44);
            this.button1.TabIndex = 34;
            this.button1.Text = "删 除";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(183, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 44);
            this.button2.TabIndex = 33;
            this.button2.Text = "修 改";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(34, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 44);
            this.button3.TabIndex = 32;
            this.button3.Text = "新 建";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // parient_list
            // 
            this.parient_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.parient_list.ContextMenuStrip = this.contextMenuStrip1;
            this.parient_list.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.parient_list.ForeColor = System.Drawing.Color.DarkBlue;
            this.parient_list.FormattingEnabled = true;
            this.parient_list.ItemHeight = 30;
            this.parient_list.Location = new System.Drawing.Point(77, 6);
            this.parient_list.Name = "parient_list";
            this.parient_list.Size = new System.Drawing.Size(243, 34);
            this.parient_list.TabIndex = 2;
            this.parient_list.SelectedValueChanged += new System.EventHandler(this.parient_list_SelectedValueChanged);
            this.parient_list.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.parient_list_MouseDoubleClick);
            // 
            // listView_patList
            // 
            this.listView_patList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_patList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView_patList.FullRowSelect = true;
            this.listView_patList.GridLines = true;
            this.listView_patList.Location = new System.Drawing.Point(21, 155);
            this.listView_patList.MultiSelect = false;
            this.listView_patList.Name = "listView_patList";
            this.listView_patList.Size = new System.Drawing.Size(424, 572);
            this.listView_patList.TabIndex = 32;
            this.listView_patList.UseCompatibleStateImageBehavior = false;
            this.listView_patList.View = System.Windows.Forms.View.Details;
            this.listView_patList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_patList_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓 名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "身份证号";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序 号";
            this.columnHeader1.Width = 80;
            // 
            // button_Create_rep
            // 
            this.button_Create_rep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Create_rep.Location = new System.Drawing.Point(161, 34);
            this.button_Create_rep.Name = "button_Create_rep";
            this.button_Create_rep.Size = new System.Drawing.Size(118, 44);
            this.button_Create_rep.TabIndex = 6;
            this.button_Create_rep.Text = "新建报告";
            this.button_Create_rep.UseVisualStyleBackColor = true;
            // 
            // button_edit_report
            // 
            this.button_edit_report.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_edit_report.Location = new System.Drawing.Point(294, 34);
            this.button_edit_report.Name = "button_edit_report";
            this.button_edit_report.Size = new System.Drawing.Size(118, 44);
            this.button_edit_report.TabIndex = 7;
            this.button_edit_report.Text = "修改报告";
            this.button_edit_report.UseVisualStyleBackColor = true;
            // 
            // listView_report
            // 
            this.listView_report.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_report.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.listView_report.FullRowSelect = true;
            this.listView_report.GridLines = true;
            this.listView_report.Location = new System.Drawing.Point(17, 104);
            this.listView_report.MultiSelect = false;
            this.listView_report.Name = "listView_report";
            this.listView_report.Size = new System.Drawing.Size(397, 623);
            this.listView_report.TabIndex = 8;
            this.listView_report.UseCompatibleStateImageBehavior = false;
            this.listView_report.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "序 号";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "日 期";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 260;
            // 
            // CMainForm
            // 
            this.AcceptButton = this.button_query;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1457, 830);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("新宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "尿动力数据管理系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CMainForm_FormClosing);
            this.Load += new System.EventHandler(this.CMainFoam_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_meno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_phone;
        private System.Windows.Forms.TextBox textBox_cardid;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioBtn_man;
        private System.Windows.Forms.RadioButton radioBTN_woman;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button_patient_manage;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_user_manage;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.Button button_sys_Setting;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox_queryWhere;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem new_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edit_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delete_ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1_bs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox parient_list;
        private System.Windows.Forms.ListView listView_patList;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button_edit_report;
        private System.Windows.Forms.Button button_Create_rep;
        private System.Windows.Forms.ListView listView_report;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}