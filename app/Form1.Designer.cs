namespace ProductCalculatorApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            標題 = new Label();
            btnQuickCalc = new Button();
            btnInventory = new Button();
            btnHistory = new Button();
            btnCustomers = new Button();
            label1 = new Label();
            label2 = new Label();
            btnMonthlyReport = new Button();
            SuspendLayout();
            // 
            // 標題
            // 
            標題.Anchor = AnchorStyles.None;
            標題.Font = new Font("Microsoft JhengHei UI", 40F);
            標題.ImageAlign = ContentAlignment.BottomCenter;
            標題.Location = new Point(0, 95);
            標題.Name = "標題";
            標題.Size = new Size(1184, 68);
            標題.TabIndex = 0;
            標題.Text = "拓丞實業";
            標題.TextAlign = ContentAlignment.MiddleCenter;
            標題.Click += 標題_Click;
            // 
            // btnQuickCalc
            // 
            btnQuickCalc.Font = new Font("Microsoft JhengHei UI", 14F);
            btnQuickCalc.Location = new Point(501, 408);
            btnQuickCalc.Name = "btnQuickCalc";
            btnQuickCalc.Size = new Size(180, 48);
            btnQuickCalc.TabIndex = 1;
            btnQuickCalc.Text = "出貨單";
            btnQuickCalc.UseVisualStyleBackColor = true;
            btnQuickCalc.Click += btnQuickCalc_Click;
            // 
            // btnInventory
            // 
            btnInventory.Font = new Font("Microsoft JhengHei UI", 14F);
            btnInventory.Location = new Point(501, 213);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(180, 48);
            btnInventory.TabIndex = 2;
            btnInventory.Text = "新增樣品";
            btnInventory.UseVisualStyleBackColor = true;
            btnInventory.Click += btnInventory_Click;
            // 
            // btnHistory
            // 
            btnHistory.Font = new Font("Microsoft JhengHei UI", 14F);
            btnHistory.Location = new Point(501, 508);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(180, 48);
            btnHistory.TabIndex = 3;
            btnHistory.Text = "應收帳請款單";
            btnHistory.UseVisualStyleBackColor = true;
            btnHistory.Click += btnHistory_Click;
            // 
            // btnCustomers
            // 
            btnCustomers.Font = new Font("Microsoft JhengHei UI", 14F);
            btnCustomers.Location = new Point(501, 312);
            btnCustomers.Name = "btnCustomers";
            btnCustomers.Size = new Size(180, 48);
            btnCustomers.TabIndex = 4;
            btnCustomers.Text = "客戶管理";
            btnCustomers.UseVisualStyleBackColor = true;
            btnCustomers.Click += btnCustomers_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 14F);
            label1.Location = new Point(687, 420);
            label1.Name = "label1";
            label1.Size = new Size(60, 24);
            label1.TabIndex = 5;
            label1.Text = "(填寫)";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 14F);
            label2.Location = new Point(687, 520);
            label2.Name = "label2";
            label2.Size = new Size(197, 24);
            label2.TabIndex = 6;
            label2.Text = "(列印出貨單,月請款單)";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnMonthlyReport
            // 
            btnMonthlyReport.Font = new Font("Microsoft JhengHei UI", 14F);
            btnMonthlyReport.Location = new Point(501, 613);
            btnMonthlyReport.Name = "btnMonthlyReport";
            btnMonthlyReport.Size = new Size(180, 48);
            btnMonthlyReport.TabIndex = 7;
            btnMonthlyReport.Text = "月報表";
            btnMonthlyReport.UseVisualStyleBackColor = true;
            btnMonthlyReport.Click += btnMonthlyReport_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(btnMonthlyReport);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCustomers);
            Controls.Add(btnHistory);
            Controls.Add(btnInventory);
            Controls.Add(btnQuickCalc);
            Controls.Add(標題);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "首頁";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label 標題;
        private Button btnQuickCalc;
        private Button btnInventory;
        private Button btnHistory;
        private Button btnCustomers;
        private Label label1;
        private Label label2;
        private Button btnMonthlyReport;
    }
}
