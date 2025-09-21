using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProductCalculatorApp   // <== 確認和另一個 .cs 檔的 namespace 一樣
{
    partial class DailyStatementForm : Form   // <== 一定要繼承 Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void DailyStatementForm_Load(object sender, EventArgs e) { }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DailyStatementForm));
            SuspendLayout();
            // 
            // DailyStatementForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(900, 120);
            Font = new Font("Microsoft JhengHei UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DailyStatementForm";
            Text = "出貨單";
            Load += DailyStatementForm_Load;
            ResumeLayout(false);
        }
        #endregion
    }
}
