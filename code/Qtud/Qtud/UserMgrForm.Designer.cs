namespace Qtud.Qtud
{
    partial class UserMgrForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMgrForm));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button_del_user = new System.Windows.Forms.Button();
            this.button_edit_user = new System.Windows.Forms.Button();
            this.button_New_user = new System.Windows.Forms.Button();
            this.label_Info = new System.Windows.Forms.Label();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_msg = new System.Windows.Forms.Label();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel7 = new System.Windows.Forms.Panel();
            this.button_query = new System.Windows.Forms.Button();
            this.textBox_queryWhere = new System.Windows.Forms.TextBox();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_userList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "创建日期";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 220;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.button_del_user);
            this.panel2.Controls.Add(this.button_edit_user);
            this.panel2.Controls.Add(this.button_New_user);
            this.panel2.Controls.Add(this.label_Info);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1704, 79);
            this.panel2.TabIndex = 33;
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
            // button_del_user
            // 
            this.button_del_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_del_user.Location = new System.Drawing.Point(1574, 12);
            this.button_del_user.Name = "button_del_user";
            this.button_del_user.Size = new System.Drawing.Size(118, 44);
            this.button_del_user.TabIndex = 5;
            this.button_del_user.Text = "删 除";
            this.button_del_user.UseVisualStyleBackColor = true;
            this.button_del_user.Click += new System.EventHandler(this.button_del_user_Click);
            // 
            // button_edit_user
            // 
            this.button_edit_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_edit_user.Location = new System.Drawing.Point(1450, 12);
            this.button_edit_user.Name = "button_edit_user";
            this.button_edit_user.Size = new System.Drawing.Size(118, 44);
            this.button_edit_user.TabIndex = 4;
            this.button_edit_user.Text = "修 改";
            this.button_edit_user.UseVisualStyleBackColor = true;
            this.button_edit_user.Click += new System.EventHandler(this.button_edit_user_Click);
            // 
            // button_New_user
            // 
            this.button_New_user.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_New_user.Location = new System.Drawing.Point(1326, 12);
            this.button_New_user.Name = "button_New_user";
            this.button_New_user.Size = new System.Drawing.Size(118, 44);
            this.button_New_user.TabIndex = 3;
            this.button_New_user.Text = "新 建";
            this.button_New_user.UseVisualStyleBackColor = true;
            this.button_New_user.Click += new System.EventHandler(this.button_New_user_Click);
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
            // columnHeader5
            // 
            this.columnHeader5.Text = "电 话";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "最后登录日期";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 220;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "类 型";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 150;
            // 
            // label_msg
            // 
            this.label_msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_msg.AutoSize = true;
            this.label_msg.BackColor = System.Drawing.SystemColors.Window;
            this.label_msg.Font = new System.Drawing.Font("华文仿宋", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_msg.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label_msg.Location = new System.Drawing.Point(1278, 19);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(124, 21);
            this.label_msg.TabIndex = 31;
            this.label_msg.Text = "登录名或姓名";
            this.label_msg.Click += new System.EventHandler(this.label_msg_Click);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "备 注";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 260;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.label_msg);
            this.panel7.Controls.Add(this.button_query);
            this.panel7.Controls.Add(this.textBox_queryWhere);
            this.panel7.Location = new System.Drawing.Point(84, 94);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1608, 64);
            this.panel7.TabIndex = 35;
            // 
            // button_query
            // 
            this.button_query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_query.Location = new System.Drawing.Point(1515, 5);
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
            this.textBox_queryWhere.Location = new System.Drawing.Point(1172, 14);
            this.textBox_queryWhere.MaxLength = 24;
            this.textBox_queryWhere.Name = "textBox_queryWhere";
            this.textBox_queryWhere.Size = new System.Drawing.Size(323, 30);
            this.textBox_queryWhere.TabIndex = 31;
            this.textBox_queryWhere.TextChanged += new System.EventHandler(this.textBox_queryWhere_TextChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "姓 名";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "登录名";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序 号";
            this.columnHeader1.Width = 80;
            // 
            // listView_userList
            // 
            this.listView_userList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView_userList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_userList.FullRowSelect = true;
            this.listView_userList.GridLines = true;
            this.listView_userList.Location = new System.Drawing.Point(0, 0);
            this.listView_userList.MultiSelect = false;
            this.listView_userList.Name = "listView_userList";
            this.listView_userList.Size = new System.Drawing.Size(1755, 565);
            this.listView_userList.TabIndex = 1;
            this.listView_userList.UseCompatibleStateImageBehavior = false;
            this.listView_userList.View = System.Windows.Forms.View.Details;
            this.listView_userList.SelectedIndexChanged += new System.EventHandler(this.listView_userList_SelectedIndexChanged);
            this.listView_userList.DoubleClick += new System.EventHandler(this.listView_userList_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.listView_userList);
            this.panel1.Location = new System.Drawing.Point(39, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1755, 565);
            this.panel1.TabIndex = 34;
            // 
            // UserMgrForm
            // 
            this.AcceptButton = this.button_query;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1704, 765);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserMgrForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UserMgrForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button_del_user;
        private System.Windows.Forms.Button button_edit_user;
        private System.Windows.Forms.Button button_New_user;
        private System.Windows.Forms.Label label_Info;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.TextBox textBox_queryWhere;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listView_userList;
        private System.Windows.Forms.Panel panel1;
    }
}