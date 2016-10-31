namespace stockassistant
{
    partial class FrmSetting
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmaincaption = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.upoverflow = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.txtlogincaption = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWave = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.upoverflow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(93, 7);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(347, 21);
            this.txtPath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Main Caption:";
            // 
            // txtmaincaption
            // 
            this.txtmaincaption.Location = new System.Drawing.Point(93, 69);
            this.txtmaincaption.Name = "txtmaincaption";
            this.txtmaincaption.Size = new System.Drawing.Size(347, 21);
            this.txtmaincaption.TabIndex = 3;
            this.txtmaincaption.DoubleClick += new System.EventHandler(this.txtlogincaption_DoubleClick);
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(93, 107);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(347, 21);
            this.txtAccount.TabIndex = 4;
            this.txtAccount.DoubleClick += new System.EventHandler(this.txtAccount_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Account:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(93, 144);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(347, 21);
            this.txtPWD.TabIndex = 7;
            this.txtPWD.DoubleClick += new System.EventHandler(this.txtPWD_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "OverFlow:";
            // 
            // upoverflow
            // 
            this.upoverflow.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upoverflow.Location = new System.Drawing.Point(93, 179);
            this.upoverflow.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.upoverflow.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.upoverflow.Name = "upoverflow";
            this.upoverflow.Size = new System.Drawing.Size(91, 21);
            this.upoverflow.TabIndex = 9;
            this.upoverflow.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(118, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(269, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(442, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Log Caption:";
            // 
            // txtlogincaption
            // 
            this.txtlogincaption.Location = new System.Drawing.Point(93, 37);
            this.txtlogincaption.Name = "txtlogincaption";
            this.txtlogincaption.Size = new System.Drawing.Size(347, 21);
            this.txtlogincaption.TabIndex = 14;
            this.txtlogincaption.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "Wave:";
            // 
            // txtWave
            // 
            this.txtWave.Location = new System.Drawing.Point(93, 209);
            this.txtWave.Name = "txtWave";
            this.txtWave.Size = new System.Drawing.Size(91, 21);
            this.txtWave.TabIndex = 16;
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 266);
            this.Controls.Add(this.txtWave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtlogincaption);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.upoverflow);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPWD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtmaincaption);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSetting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.upoverflow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmaincaption;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown upoverflow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtlogincaption;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtWave;
    }
}