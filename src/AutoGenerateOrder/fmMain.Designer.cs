namespace AutoGenerateOrder
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCustomerInfo = new System.Windows.Forms.Button();
            this.btnProductInfo = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lsDetails = new System.Windows.Forms.ListBox();
            this.llShow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lsRigth = new System.Windows.Forms.ListBox();
            this.btnNote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomerInfo
            // 
            this.btnCustomerInfo.Location = new System.Drawing.Point(60, 45);
            this.btnCustomerInfo.Name = "btnCustomerInfo";
            this.btnCustomerInfo.Size = new System.Drawing.Size(85, 26);
            this.btnCustomerInfo.TabIndex = 0;
            this.btnCustomerInfo.Text = "设置用户信息";
            this.btnCustomerInfo.UseVisualStyleBackColor = true;
            this.btnCustomerInfo.Click += new System.EventHandler(this.btnCustomerInfo_Click);
            // 
            // btnProductInfo
            // 
            this.btnProductInfo.Location = new System.Drawing.Point(250, 45);
            this.btnProductInfo.Name = "btnProductInfo";
            this.btnProductInfo.Size = new System.Drawing.Size(85, 26);
            this.btnProductInfo.TabIndex = 1;
            this.btnProductInfo.Text = "设置商品信息";
            this.btnProductInfo.UseVisualStyleBackColor = true;
            this.btnProductInfo.Click += new System.EventHandler(this.btnProductInfo_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(748, 45);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(85, 26);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "生成订单";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lsDetails
            // 
            this.lsDetails.FormattingEnabled = true;
            this.lsDetails.ItemHeight = 12;
            this.lsDetails.Location = new System.Drawing.Point(55, 112);
            this.lsDetails.Name = "lsDetails";
            this.lsDetails.Size = new System.Drawing.Size(439, 340);
            this.lsDetails.TabIndex = 3;
            // 
            // llShow
            // 
            this.llShow.AutoSize = true;
            this.llShow.Location = new System.Drawing.Point(56, 94);
            this.llShow.Name = "llShow";
            this.llShow.Size = new System.Drawing.Size(89, 12);
            this.llShow.TabIndex = 4;
            this.llShow.Text = "错误信息提示栏";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(543, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "正确信息提示栏";
            // 
            // lsRigth
            // 
            this.lsRigth.FormattingEnabled = true;
            this.lsRigth.ItemHeight = 12;
            this.lsRigth.Location = new System.Drawing.Point(543, 112);
            this.lsRigth.Name = "lsRigth";
            this.lsRigth.Size = new System.Drawing.Size(439, 340);
            this.lsRigth.TabIndex = 5;
            // 
            // btnNote
            // 
            this.btnNote.Location = new System.Drawing.Point(547, 45);
            this.btnNote.Name = "btnNote";
            this.btnNote.Size = new System.Drawing.Size(85, 26);
            this.btnNote.TabIndex = 7;
            this.btnNote.Text = "使用说明";
            this.btnNote.UseVisualStyleBackColor = true;
            this.btnNote.Click += new System.EventHandler(this.btnNote_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 508);
            this.Controls.Add(this.btnNote);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsRigth);
            this.Controls.Add(this.llShow);
            this.Controls.Add(this.lsDetails);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnProductInfo);
            this.Controls.Add(this.btnCustomerInfo);
            this.Name = "frmMain";
            this.Text = "订单生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCustomerInfo;
        private System.Windows.Forms.Button btnProductInfo;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ListBox lsDetails;
        private System.Windows.Forms.Label llShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsRigth;
        private System.Windows.Forms.Button btnNote;
    }
}

