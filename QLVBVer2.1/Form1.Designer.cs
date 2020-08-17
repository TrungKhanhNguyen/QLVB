namespace QLVBVer2._1
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
            this.btnCVDi = new System.Windows.Forms.Button();
            this.btnCVDen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblExpiredCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCVDi
            // 
            this.btnCVDi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCVDi.Image = global::QLVBVer2._1.Properties.Resources.if_arrow_right_alt2_118745;
            this.btnCVDi.Location = new System.Drawing.Point(234, 108);
            this.btnCVDi.Margin = new System.Windows.Forms.Padding(5);
            this.btnCVDi.Name = "btnCVDi";
            this.btnCVDi.Size = new System.Drawing.Size(207, 64);
            this.btnCVDi.TabIndex = 3;
            this.btnCVDi.Text = "Danh mục";
            this.btnCVDi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCVDi.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCVDi.UseVisualStyleBackColor = true;
            this.btnCVDi.Click += new System.EventHandler(this.btnCVDi_Click);
            // 
            // btnCVDen
            // 
            this.btnCVDen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCVDen.Image = global::QLVBVer2._1.Properties.Resources.if_51_Coud_Arrow_Down_183464;
            this.btnCVDen.Location = new System.Drawing.Point(14, 108);
            this.btnCVDen.Margin = new System.Windows.Forms.Padding(5);
            this.btnCVDen.Name = "btnCVDen";
            this.btnCVDen.Size = new System.Drawing.Size(211, 64);
            this.btnCVDen.TabIndex = 2;
            this.btnCVDen.Text = "Công văn đến";
            this.btnCVDen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCVDen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCVDen.UseVisualStyleBackColor = true;
            this.btnCVDen.Click += new System.EventHandler(this.btnCVDen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblExpiredCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 60);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Công văn đến hạn";
            // 
            // lblExpiredCount
            // 
            this.lblExpiredCount.AutoSize = true;
            this.lblExpiredCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpiredCount.ForeColor = System.Drawing.Color.Red;
            this.lblExpiredCount.Location = new System.Drawing.Point(118, 22);
            this.lblExpiredCount.Name = "lblExpiredCount";
            this.lblExpiredCount.Size = new System.Drawing.Size(0, 25);
            this.lblExpiredCount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(141, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "công văn sắp hết hạn";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(454, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(40, 21);
            this.configurationToolStripMenuItem.Text = "File";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.configToolStripMenuItem.Text = "Config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(128, 21);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(63, 21);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 186);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCVDi);
            this.Controls.Add(this.btnCVDen);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "QLCV";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCVDi;
        private System.Windows.Forms.Button btnCVDen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        public System.Windows.Forms.Label lblExpiredCount;
    }
}

