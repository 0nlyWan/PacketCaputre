namespace MyPacketCapturer
{
    partial class FrmSmurf
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDnsSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAttackers = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClass);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMask);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDnsSuffix);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 275);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My Data";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(150, 216);
            this.txtClass.Name = "txtClass";
            this.txtClass.ReadOnly = true;
            this.txtClass.Size = new System.Drawing.Size(272, 31);
            this.txtClass.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Class:";
            // 
            // txtMask
            // 
            this.txtMask.Location = new System.Drawing.Point(150, 163);
            this.txtMask.Name = "txtMask";
            this.txtMask.ReadOnly = true;
            this.txtMask.Size = new System.Drawing.Size(272, 31);
            this.txtMask.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Subnet Mask:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(150, 105);
            this.txtIp.Name = "txtIp";
            this.txtIp.ReadOnly = true;
            this.txtIp.Size = new System.Drawing.Size(272, 31);
            this.txtIp.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ip Address:";
            // 
            // txtDnsSuffix
            // 
            this.txtDnsSuffix.Location = new System.Drawing.Point(150, 50);
            this.txtDnsSuffix.Name = "txtDnsSuffix";
            this.txtDnsSuffix.ReadOnly = true;
            this.txtDnsSuffix.Size = new System.Drawing.Size(272, 31);
            this.txtDnsSuffix.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNS Suffix:";
            // 
            // cboAttackers
            // 
            this.cboAttackers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboAttackers.Enabled = false;
            this.cboAttackers.FormattingEnabled = true;
            this.cboAttackers.Location = new System.Drawing.Point(12, 368);
            this.cboAttackers.Name = "cboAttackers";
            this.cboAttackers.Size = new System.Drawing.Size(442, 550);
            this.cboAttackers.TabIndex = 1;
            this.cboAttackers.Text = "Attackers";
            // 
            // FrmSmurf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 962);
            this.Controls.Add(this.cboAttackers);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmSmurf";
            this.Text = "frmSmurf";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDnsSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboAttackers;
    }
}