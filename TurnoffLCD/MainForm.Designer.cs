namespace CloseLCD
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
            this.btnSetMenuItem = new System.Windows.Forms.Button();
            this.txtMenuText = new System.Windows.Forms.TextBox();
            this.btnTurnoff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSetMenuItem
            // 
            this.btnSetMenuItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetMenuItem.Location = new System.Drawing.Point(12, 41);
            this.btnSetMenuItem.Name = "btnSetMenuItem";
            this.btnSetMenuItem.Size = new System.Drawing.Size(121, 23);
            this.btnSetMenuItem.TabIndex = 1;
            this.btnSetMenuItem.Text = "加入桌面右键菜单";
            this.btnSetMenuItem.UseVisualStyleBackColor = true;
            this.btnSetMenuItem.Click += new System.EventHandler(this.btnSetMenuItem_Click);
            // 
            // txtMenuText
            // 
            this.txtMenuText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMenuText.Location = new System.Drawing.Point(139, 41);
            this.txtMenuText.Name = "txtMenuText";
            this.txtMenuText.Size = new System.Drawing.Size(144, 22);
            this.txtMenuText.TabIndex = 2;
            this.txtMenuText.Text = "关闭显示器(&M)";
            // 
            // btnTurnoff
            // 
            this.btnTurnoff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTurnoff.Location = new System.Drawing.Point(12, 12);
            this.btnTurnoff.Name = "btnTurnoff";
            this.btnTurnoff.Size = new System.Drawing.Size(271, 23);
            this.btnTurnoff.TabIndex = 0;
            this.btnTurnoff.Text = "关闭显示器";
            this.btnTurnoff.UseVisualStyleBackColor = true;
            this.btnTurnoff.Click += new System.EventHandler(this.btnTurnoff_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 76);
            this.Controls.Add(this.btnTurnoff);
            this.Controls.Add(this.txtMenuText);
            this.Controls.Add(this.btnSetMenuItem);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "楚人一键关闭显示器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetMenuItem;
        private System.Windows.Forms.TextBox txtMenuText;
        private System.Windows.Forms.Button btnTurnoff;
    }
}

