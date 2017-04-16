namespace MyPacketCapturer
{
    partial class arpCache
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtFirstOctet = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMacAddress = new System.Windows.Forms.TextBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDnsSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSecondOctet = new System.Windows.Forms.TextBox();
            this.txtThirdOctet = new System.Windows.Forms.TextBox();
            this.txtFourthOctet = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFirstOctet
            // 
            this.txtFirstOctet.Location = new System.Drawing.Point(66, 15);
            this.txtFirstOctet.MaxLength = 3;
            this.txtFirstOctet.Name = "txtFirstOctet";
            this.txtFirstOctet.Size = new System.Drawing.Size(39, 20);
            this.txtFirstOctet.TabIndex = 1;
            this.txtFirstOctet.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = "Listen";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMacAddress);
            this.groupBox1.Controls.Add(this.txtClass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDnsSuffix);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(233, 173);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Mac Address:";
            // 
            // txtMacAddress
            // 
            this.txtMacAddress.Location = new System.Drawing.Point(84, 137);
            this.txtMacAddress.Name = "txtMacAddress";
            this.txtMacAddress.Size = new System.Drawing.Size(138, 20);
            this.txtMacAddress.TabIndex = 8;
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(85, 77);
            this.txtClass.Margin = new System.Windows.Forms.Padding(2);
            this.txtClass.Name = "txtClass";
            this.txtClass.ReadOnly = true;
            this.txtClass.Size = new System.Drawing.Size(138, 20);
            this.txtClass.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Class:";
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(85, 107);
            this.txtMask.Margin = new System.Windows.Forms.Padding(2);
            this.txtMask.Name = "txtMask";
            this.txtMask.ReadOnly = true;
            this.txtMask.Size = new System.Drawing.Size(138, 20);
            this.txtMask.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Subnet Mask:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(85, 53);
            this.txtIp.Margin = new System.Windows.Forms.Padding(2);
            this.txtIp.Name = "txtIp";
            this.txtIp.ReadOnly = true;
            this.txtIp.Size = new System.Drawing.Size(138, 20);
            this.txtIp.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ip Address:";
            // 
            // txtDnsSuffix
            // 
            this.txtDnsSuffix.Location = new System.Drawing.Point(85, 28);
            this.txtDnsSuffix.Margin = new System.Windows.Forms.Padding(2);
            this.txtDnsSuffix.Name = "txtDnsSuffix";
            this.txtDnsSuffix.ReadOnly = true;
            this.txtDnsSuffix.Size = new System.Drawing.Size(138, 20);
            this.txtDnsSuffix.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNS Suffix:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFourthOctet);
            this.groupBox2.Controls.Add(this.txtThirdOctet);
            this.groupBox2.Controls.Add(this.txtSecondOctet);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtFirstOctet);
            this.groupBox2.Location = new System.Drawing.Point(11, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 123);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "1st Octet";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "2nd Octet";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "3rd Octet";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "4th Octet";
            // 
            // txtSecondOctet
            // 
            this.txtSecondOctet.Location = new System.Drawing.Point(66, 45);
            this.txtSecondOctet.MaxLength = 3;
            this.txtSecondOctet.Name = "txtSecondOctet";
            this.txtSecondOctet.Size = new System.Drawing.Size(39, 20);
            this.txtSecondOctet.TabIndex = 8;
            // 
            // txtThirdOctet
            // 
            this.txtThirdOctet.Location = new System.Drawing.Point(65, 71);
            this.txtThirdOctet.MaxLength = 3;
            this.txtThirdOctet.Name = "txtThirdOctet";
            this.txtThirdOctet.Size = new System.Drawing.Size(40, 20);
            this.txtThirdOctet.TabIndex = 9;
            // 
            // txtFourthOctet
            // 
            this.txtFourthOctet.Location = new System.Drawing.Point(65, 97);
            this.txtFourthOctet.MaxLength = 3;
            this.txtFourthOctet.Name = "txtFourthOctet";
            this.txtFourthOctet.Size = new System.Drawing.Size(40, 20);
            this.txtFourthOctet.TabIndex = 10;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(250, 39);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(289, 278);
            this.txtOutput.TabIndex = 6;
            // 
            // arpCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 322);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "arpCache";
            this.Text = "arpCache";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFirstOctet;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMacAddress;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDnsSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFourthOctet;
        private System.Windows.Forms.TextBox txtThirdOctet;
        private System.Windows.Forms.TextBox txtSecondOctet;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOutput;
    }
}