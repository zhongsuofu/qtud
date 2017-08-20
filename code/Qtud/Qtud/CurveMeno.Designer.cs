namespace Qtud.Qtud
{
    partial class CurveMeno
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_meno = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(33, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "meno";
            // 
            // textBox_meno
            // 
            this.textBox_meno.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_meno.ForeColor = System.Drawing.Color.Navy;
            this.textBox_meno.Location = new System.Drawing.Point(22, 83);
            this.textBox_meno.MaxLength = 128;
            this.textBox_meno.Multiline = true;
            this.textBox_meno.Name = "textBox_meno";
            this.textBox_meno.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_meno.Size = new System.Drawing.Size(595, 258);
            this.textBox_meno.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "确 定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CurveMeno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_meno);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CurveMeno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据说明";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_meno;
        private System.Windows.Forms.Button button1;
    }
}