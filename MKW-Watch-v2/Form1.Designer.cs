namespace MKW_Watch_v2
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkSpdFloat1 = new System.Windows.Forms.CheckBox();
            this.chkAir = new System.Windows.Forms.CheckBox();
            this.chkMT = new System.Windows.Forms.CheckBox();
            this.chkBst = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox1.Location = new System.Drawing.Point(71, 82);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Show Speedometer";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 13);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(124, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect To Dolphin";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(205, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Not Connected";
            // 
            // chkSpdFloat1
            // 
            this.chkSpdFloat1.AutoSize = true;
            this.chkSpdFloat1.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkSpdFloat1.Location = new System.Drawing.Point(196, 82);
            this.chkSpdFloat1.Name = "chkSpdFloat1";
            this.chkSpdFloat1.Size = new System.Drawing.Size(127, 17);
            this.chkSpdFloat1.TabIndex = 4;
            this.chkSpdFloat1.Text = "Show Speed as Float";
            this.chkSpdFloat1.UseVisualStyleBackColor = true;
            this.chkSpdFloat1.Visible = false;
            // 
            // chkAir
            // 
            this.chkAir.AutoSize = true;
            this.chkAir.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkAir.Location = new System.Drawing.Point(71, 105);
            this.chkAir.Name = "chkAir";
            this.chkAir.Size = new System.Drawing.Size(94, 17);
            this.chkAir.TabIndex = 5;
            this.chkAir.Text = "Show Air Time";
            this.chkAir.UseVisualStyleBackColor = true;
            this.chkAir.Visible = false;
            this.chkAir.CheckedChanged += new System.EventHandler(this.chkAir_CheckedChanged);
            // 
            // chkMT
            // 
            this.chkMT.AutoSize = true;
            this.chkMT.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkMT.Location = new System.Drawing.Point(71, 128);
            this.chkMT.Name = "chkMT";
            this.chkMT.Size = new System.Drawing.Size(109, 17);
            this.chkMT.TabIndex = 6;
            this.chkMT.Text = "Show MT Charge";
            this.chkMT.UseVisualStyleBackColor = true;
            this.chkMT.Visible = false;
            this.chkMT.CheckedChanged += new System.EventHandler(this.chkMT_CheckedChanged);
            // 
            // chkBst
            // 
            this.chkBst.AutoSize = true;
            this.chkBst.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkBst.Location = new System.Drawing.Point(71, 151);
            this.chkBst.Name = "chkBst";
            this.chkBst.Size = new System.Drawing.Size(112, 17);
            this.chkBst.TabIndex = 7;
            this.chkBst.Text = "Show Boost Timer";
            this.chkBst.UseVisualStyleBackColor = true;
            this.chkBst.Visible = false;
            this.chkBst.CheckedChanged += new System.EventHandler(this.chkBst_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(391, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "====== Speedometer Options ======";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(71, 175);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(117, 17);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Show Input Display";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 212);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBst);
            this.Controls.Add(this.chkMT);
            this.Controls.Add(this.chkAir);
            this.Controls.Add(this.chkSpdFloat1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "SpeeDolphin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.CheckBox chkSpdFloat1;
        public System.Windows.Forms.CheckBox chkAir;
        public System.Windows.Forms.CheckBox chkMT;
        public System.Windows.Forms.CheckBox chkBst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

