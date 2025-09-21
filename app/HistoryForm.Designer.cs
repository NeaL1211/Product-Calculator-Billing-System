namespace ProductCalculatorApp
{
    partial class HistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            dgvOrders = new DataGridView();
            dgvLines = new DataGridView();
            label1 = new Label();
            numAmtMin = new NumericUpDown();
            label2 = new Label();
            numAmtMax = new NumericUpDown();
            label3 = new Label();
            dtFrom = new DateTimePicker();
            label4 = new Label();
            dtTo = new DateTimePicker();
            rdoAll = new RadioButton();
            rdoDay = new RadioButton();
            rdoWeek = new RadioButton();
            rdoMonth = new RadioButton();
            btnSearch = new Button();
            btnReset = new Button();
            rdoYear = new RadioButton();
            label5 = new Label();
            cboCustomerFilter = new ComboBox();
            btnMonthly = new Button();
            btnDaily = new Button();
            btnClearAll = new Button();
            label6 = new Label();
            txtCodeFilter = new TextBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAmtMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAmtMax).BeginInit();
            SuspendLayout();
            // 
            // dgvOrders
            // 
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new Point(447, 23);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersWidth = 40;
            dgvOrders.Size = new Size(725, 340);
            dgvOrders.TabIndex = 0;
            // 
            // dgvLines
            // 
            dgvLines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLines.Location = new Point(171, 380);
            dgvLines.Name = "dgvLines";
            dgvLines.Size = new Size(1001, 360);
            dgvLines.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F);
            label1.Location = new Point(27, 158);
            label1.Name = "label1";
            label1.Size = new Size(45, 20);
            label1.TabIndex = 2;
            label1.Text = "金額:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numAmtMin
            // 
            numAmtMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numAmtMin.Font = new Font("Microsoft JhengHei UI", 12F);
            numAmtMin.Location = new Point(80, 155);
            numAmtMin.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numAmtMin.Name = "numAmtMin";
            numAmtMin.Size = new Size(120, 28);
            numAmtMin.TabIndex = 3;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 12F);
            label2.Location = new Point(206, 159);
            label2.Name = "label2";
            label2.Size = new Size(21, 20);
            label2.TabIndex = 4;
            label2.Text = "~";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numAmtMax
            // 
            numAmtMax.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numAmtMax.Font = new Font("Microsoft JhengHei UI", 12F);
            numAmtMax.Location = new Point(233, 155);
            numAmtMax.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numAmtMax.Name = "numAmtMax";
            numAmtMax.Size = new Size(120, 28);
            numAmtMax.TabIndex = 5;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 12F);
            label3.Location = new Point(27, 206);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.TabIndex = 6;
            label3.Text = "日期:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtFrom
            // 
            dtFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtFrom.Checked = false;
            dtFrom.Location = new Point(79, 205);
            dtFrom.Name = "dtFrom";
            dtFrom.ShowCheckBox = true;
            dtFrom.Size = new Size(200, 23);
            dtFrom.TabIndex = 7;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 12F);
            label4.Location = new Point(285, 207);
            label4.Name = "label4";
            label4.Size = new Size(21, 20);
            label4.TabIndex = 8;
            label4.Text = "~";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dtTo
            // 
            dtTo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtTo.Checked = false;
            dtTo.Location = new Point(79, 234);
            dtTo.Name = "dtTo";
            dtTo.ShowCheckBox = true;
            dtTo.Size = new Size(200, 23);
            dtTo.TabIndex = 9;
            // 
            // rdoAll
            // 
            rdoAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoAll.AutoSize = true;
            rdoAll.Font = new Font("Microsoft JhengHei UI", 12F);
            rdoAll.Location = new Point(27, 76);
            rdoAll.Name = "rdoAll";
            rdoAll.Size = new Size(59, 24);
            rdoAll.TabIndex = 10;
            rdoAll.TabStop = true;
            rdoAll.Text = "全部";
            rdoAll.TextAlign = ContentAlignment.MiddleCenter;
            rdoAll.UseVisualStyleBackColor = true;
            // 
            // rdoDay
            // 
            rdoDay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoDay.AutoSize = true;
            rdoDay.Font = new Font("Microsoft JhengHei UI", 12F);
            rdoDay.Location = new Point(92, 76);
            rdoDay.Name = "rdoDay";
            rdoDay.Size = new Size(59, 24);
            rdoDay.TabIndex = 11;
            rdoDay.TabStop = true;
            rdoDay.Text = "今天";
            rdoDay.TextAlign = ContentAlignment.MiddleCenter;
            rdoDay.UseVisualStyleBackColor = true;
            // 
            // rdoWeek
            // 
            rdoWeek.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoWeek.AutoSize = true;
            rdoWeek.Font = new Font("Microsoft JhengHei UI", 12F);
            rdoWeek.Location = new Point(157, 76);
            rdoWeek.Name = "rdoWeek";
            rdoWeek.Size = new Size(59, 24);
            rdoWeek.TabIndex = 12;
            rdoWeek.TabStop = true;
            rdoWeek.Text = "本週";
            rdoWeek.TextAlign = ContentAlignment.MiddleCenter;
            rdoWeek.UseVisualStyleBackColor = true;
            // 
            // rdoMonth
            // 
            rdoMonth.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoMonth.AutoSize = true;
            rdoMonth.Font = new Font("Microsoft JhengHei UI", 12F);
            rdoMonth.Location = new Point(222, 76);
            rdoMonth.Name = "rdoMonth";
            rdoMonth.Size = new Size(59, 24);
            rdoMonth.TabIndex = 13;
            rdoMonth.TabStop = true;
            rdoMonth.Text = "本月";
            rdoMonth.TextAlign = ContentAlignment.MiddleCenter;
            rdoMonth.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.AutoSize = true;
            btnSearch.Font = new Font("Microsoft JhengHei UI", 12F);
            btnSearch.Location = new Point(218, 34);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 30);
            btnSearch.TabIndex = 14;
            btnSearch.Text = "搜尋";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnReset
            // 
            btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnReset.AutoSize = true;
            btnReset.Font = new Font("Microsoft JhengHei UI", 12F);
            btnReset.Location = new Point(305, 34);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(83, 30);
            btnReset.TabIndex = 15;
            btnReset.Text = "清除篩選";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // rdoYear
            // 
            rdoYear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            rdoYear.AutoSize = true;
            rdoYear.Font = new Font("Microsoft JhengHei UI", 12F);
            rdoYear.Location = new Point(287, 76);
            rdoYear.Name = "rdoYear";
            rdoYear.Size = new Size(59, 24);
            rdoYear.TabIndex = 16;
            rdoYear.TabStop = true;
            rdoYear.Text = "本年";
            rdoYear.TextAlign = ContentAlignment.MiddleCenter;
            rdoYear.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 12F);
            label5.Location = new Point(23, 39);
            label5.Name = "label5";
            label5.Size = new Size(45, 20);
            label5.TabIndex = 17;
            label5.Text = "客戶:";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboCustomerFilter
            // 
            cboCustomerFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCustomerFilter.Font = new Font("Microsoft JhengHei UI", 12F);
            cboCustomerFilter.FormattingEnabled = true;
            cboCustomerFilter.Location = new Point(74, 34);
            cboCustomerFilter.Name = "cboCustomerFilter";
            cboCustomerFilter.Size = new Size(121, 28);
            cboCustomerFilter.TabIndex = 18;
            // 
            // btnMonthly
            // 
            btnMonthly.AutoSize = true;
            btnMonthly.Font = new Font("Microsoft JhengHei UI", 12F);
            btnMonthly.Location = new Point(122, 305);
            btnMonthly.Name = "btnMonthly";
            btnMonthly.Size = new Size(99, 30);
            btnMonthly.TabIndex = 19;
            btnMonthly.Text = "月結請款單";
            btnMonthly.UseVisualStyleBackColor = true;
            btnMonthly.Click += btnMonthly_Click;
            // 
            // btnDaily
            // 
            btnDaily.AutoSize = true;
            btnDaily.Font = new Font("Microsoft JhengHei UI", 12F);
            btnDaily.Location = new Point(27, 305);
            btnDaily.Name = "btnDaily";
            btnDaily.Size = new Size(75, 30);
            btnDaily.TabIndex = 20;
            btnDaily.Text = "出貨單";
            btnDaily.UseVisualStyleBackColor = true;
            btnDaily.Click += btnDaily_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(11, 717);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(75, 23);
            btnClearAll.TabIndex = 21;
            btnClearAll.Text = "清除所有";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 12F);
            label6.Location = new Point(23, 117);
            label6.Name = "label6";
            label6.Size = new Size(77, 20);
            label6.TabIndex = 22;
            label6.Text = "產品編號:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtCodeFilter
            // 
            txtCodeFilter.Font = new Font("Microsoft JhengHei UI", 12F);
            txtCodeFilter.Location = new Point(106, 114);
            txtCodeFilter.Name = "txtCodeFilter";
            txtCodeFilter.Size = new Size(200, 28);
            txtCodeFilter.TabIndex = 23;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 12F);
            label7.Location = new Point(307, 117);
            label7.Name = "label7";
            label7.Size = new Size(131, 20);
            label7.TabIndex = 24;
            label7.Text = "(輸入完要按搜尋)";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(label7);
            Controls.Add(txtCodeFilter);
            Controls.Add(label6);
            Controls.Add(btnClearAll);
            Controls.Add(btnDaily);
            Controls.Add(btnMonthly);
            Controls.Add(cboCustomerFilter);
            Controls.Add(label5);
            Controls.Add(rdoYear);
            Controls.Add(btnReset);
            Controls.Add(btnSearch);
            Controls.Add(rdoMonth);
            Controls.Add(rdoWeek);
            Controls.Add(rdoDay);
            Controls.Add(rdoAll);
            Controls.Add(dtTo);
            Controls.Add(label4);
            Controls.Add(dtFrom);
            Controls.Add(label3);
            Controls.Add(numAmtMax);
            Controls.Add(label2);
            Controls.Add(numAmtMin);
            Controls.Add(label1);
            Controls.Add(dgvLines);
            Controls.Add(dgvOrders);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HistoryForm";
            Text = "歷史紀錄";
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAmtMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAmtMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvOrders;
        private DataGridView dgvLines;
        private Label label1;
        private NumericUpDown numAmtMin;
        private Label label2;
        private NumericUpDown numAmtMax;
        private Label label3;
        private DateTimePicker dtFrom;
        private Label label4;
        private DateTimePicker dtTo;
        private RadioButton rdoAll;
        private RadioButton rdoDay;
        private RadioButton rdoWeek;
        private RadioButton rdoMonth;
        private Button btnSearch;
        private Button btnReset;
        private RadioButton rdoYear;
        private Label label5;
        private ComboBox cboCustomerFilter;
        private Button btnMonthly;
        private Button btnDaily;
        private Button btnClearAll;
        private Label label6;
        private TextBox txtCodeFilter;
        private Label label7;
    }
}