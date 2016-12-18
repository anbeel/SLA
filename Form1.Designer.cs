namespace stockassistant
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numupnumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtlowestprice = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mtxthighestprice = new System.Windows.Forms.MaskedTextBox();
            this.chkraisedstage = new System.Windows.Forms.CheckBox();
            this.mtxtpreprice = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.mtxtbuy1price = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mtxtsell1price = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mtxtlastbuyprice = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mtxtlastsellprice = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.startNumber = new System.Windows.Forms.NumericUpDown();
            this.chkiswatch = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnCal = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numupnumber)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(10, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(569, 256);
            this.listBox1.TabIndex = 1;
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "NO:";
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(29, 20);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(76, 21);
            this.txtNo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Unit:";
            // 
            // numupnumber
            // 
            this.numupnumber.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numupnumber.Location = new System.Drawing.Point(147, 21);
            this.numupnumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numupnumber.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numupnumber.Name = "numupnumber";
            this.numupnumber.Size = new System.Drawing.Size(56, 21);
            this.numupnumber.TabIndex = 5;
            this.numupnumber.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "LowestPrice:";
            // 
            // mtxtlowestprice
            // 
            this.mtxtlowestprice.HidePromptOnLeave = true;
            this.mtxtlowestprice.Location = new System.Drawing.Point(289, 20);
            this.mtxtlowestprice.Name = "mtxtlowestprice";
            this.mtxtlowestprice.Size = new System.Drawing.Size(58, 21);
            this.mtxtlowestprice.TabIndex = 8;
            this.mtxtlowestprice.DoubleClick += new System.EventHandler(this.mtxtlowestprice_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(352, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "HighestPrice:";
            // 
            // mtxthighestprice
            // 
            this.mtxthighestprice.Location = new System.Drawing.Point(435, 20);
            this.mtxthighestprice.Name = "mtxthighestprice";
            this.mtxthighestprice.Size = new System.Drawing.Size(58, 21);
            this.mtxthighestprice.TabIndex = 10;
            this.mtxthighestprice.DoubleClick += new System.EventHandler(this.mtxthighestprice_DoubleClick);
            // 
            // chkraisedstage
            // 
            this.chkraisedstage.AutoSize = true;
            this.chkraisedstage.Location = new System.Drawing.Point(509, 23);
            this.chkraisedstage.Name = "chkraisedstage";
            this.chkraisedstage.Size = new System.Drawing.Size(60, 16);
            this.chkraisedstage.TabIndex = 11;
            this.chkraisedstage.Text = "Raised";
            this.chkraisedstage.UseVisualStyleBackColor = true;
            // 
            // mtxtpreprice
            // 
            this.mtxtpreprice.Location = new System.Drawing.Point(63, 48);
            this.mtxtpreprice.Name = "mtxtpreprice";
            this.mtxtpreprice.Size = new System.Drawing.Size(58, 21);
            this.mtxtpreprice.TabIndex = 13;
            this.mtxtpreprice.DoubleClick += new System.EventHandler(this.mtxtpreprice_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "PrePrice:";
            // 
            // mtxtbuy1price
            // 
            this.mtxtbuy1price.Location = new System.Drawing.Point(201, 48);
            this.mtxtbuy1price.Name = "mtxtbuy1price";
            this.mtxtbuy1price.Size = new System.Drawing.Size(58, 21);
            this.mtxtbuy1price.TabIndex = 15;
            this.mtxtbuy1price.DoubleClick += new System.EventHandler(this.mtxtbuy1price_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(135, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "Buy1Price:";
            // 
            // mtxtsell1price
            // 
            this.mtxtsell1price.Location = new System.Drawing.Point(339, 48);
            this.mtxtsell1price.Name = "mtxtsell1price";
            this.mtxtsell1price.Size = new System.Drawing.Size(58, 21);
            this.mtxtsell1price.TabIndex = 17;
            this.mtxtsell1price.DoubleClick += new System.EventHandler(this.mtxtsell1price_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Sell1Price:";
            // 
            // mtxtlastbuyprice
            // 
            this.mtxtlastbuyprice.Location = new System.Drawing.Point(492, 48);
            this.mtxtlastbuyprice.Name = "mtxtlastbuyprice";
            this.mtxtlastbuyprice.Size = new System.Drawing.Size(58, 21);
            this.mtxtlastbuyprice.TabIndex = 19;
            this.mtxtlastbuyprice.DoubleClick += new System.EventHandler(this.mtxtlastbuyprice_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(409, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "LastBuyPrice:";
            // 
            // mtxtlastsellprice
            // 
            this.mtxtlastsellprice.Location = new System.Drawing.Point(99, 85);
            this.mtxtlastsellprice.Name = "mtxtlastsellprice";
            this.mtxtlastsellprice.Size = new System.Drawing.Size(58, 21);
            this.mtxtlastsellprice.TabIndex = 21;
            this.mtxtlastsellprice.DoubleClick += new System.EventHandler(this.mtxtlastsellprice_DoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "LastSellPrice:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(477, 83);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btnCal);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.startNumber);
            this.groupBox1.Controls.Add(this.chkiswatch);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.mtxtlastsellprice);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.mtxtlastbuyprice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numupnumber);
            this.groupBox1.Controls.Add(this.mtxtsell1price);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.mtxtlowestprice);
            this.groupBox1.Controls.Add(this.mtxtbuy1price);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.mtxtpreprice);
            this.groupBox1.Controls.Add(this.mtxthighestprice);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkraisedstage);
            this.groupBox1.Location = new System.Drawing.Point(6, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 127);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detail";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(163, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "Methods:";
            // 
            // startNumber
            // 
            this.startNumber.Location = new System.Drawing.Point(216, 85);
            this.startNumber.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.startNumber.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.startNumber.Name = "startNumber";
            this.startNumber.Size = new System.Drawing.Size(60, 21);
            this.startNumber.TabIndex = 24;
            this.startNumber.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // chkiswatch
            // 
            this.chkiswatch.AutoSize = true;
            this.chkiswatch.Location = new System.Drawing.Point(399, 87);
            this.chkiswatch.Name = "chkiswatch";
            this.chkiswatch.Size = new System.Drawing.Size(72, 16);
            this.chkiswatch.TabIndex = 23;
            this.chkiswatch.Text = "Is Watch";
            this.chkiswatch.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 401);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 24;
            this.button2.Text = "Test SMS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(207, 401);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Setting";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(295, 401);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(386, 401);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 24;
            this.button5.Text = "Download";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(481, 401);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 27;
            this.button6.Text = "Upload";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnCal
            // 
            this.btnCal.Location = new System.Drawing.Point(289, 83);
            this.btnCal.Name = "btnCal";
            this.btnCal.Size = new System.Drawing.Size(75, 23);
            this.btnCal.TabIndex = 26;
            this.btnCal.Text = "Calculate";
            this.btnCal.UseVisualStyleBackColor = true;
            this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 435);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = " Assistant V4.5";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numupnumber)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numupnumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtxtlowestprice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtxthighestprice;
        private System.Windows.Forms.CheckBox chkraisedstage;
        private System.Windows.Forms.MaskedTextBox mtxtpreprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mtxtbuy1price;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mtxtsell1price;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox mtxtlastbuyprice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox mtxtlastsellprice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chkiswatch;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown startNumber;
        private System.Windows.Forms.Button btnCal;
    }
}

