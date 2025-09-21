namespace ProductCalculatorApp
{
    partial class QuickCalcForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickCalcForm));
            label1 = new Label();
            txtCode = new TextBox();
            btnFetch = new Button();
            label2 = new Label();
            lblName = new Label();
            label4 = new Label();
            lblUnitPrice = new Label();
            lblLTitle = new Label();
            lblWTitle = new Label();
            btnAddToCart = new Button();
            numLength = new NumericUpDown();
            numWidth = new NumericUpDown();
            dgvCart = new DataGridView();
            btnRemove = new Button();
            btnClear = new Button();
            lblGrandTotal = new Label();
            label6 = new Label();
            txtLineNote = new TextBox();
            btnConfirm = new Button();
            label7 = new Label();
            cboCustomer = new ComboBox();
            lblSubtotalTotal = new Label();
            lblWageTotal = new Label();
            cboKind = new ComboBox();
            numYards = new NumericUpDown();
            numQty = new NumericUpDown();
            lblYard = new Label();
            lblQty = new Label();
            label8 = new Label();
            lblLaborName = new Label();
            txtLaborName = new TextBox();
            lblLaborPrice = new Label();
            numLaborPrice = new NumericUpDown();
            lblBonusTitle = new Label();
            numBonus = new NumericUpDown();
            btnSwapSubtotalWage = new Button();
            ((System.ComponentModel.ISupportInitialize)numLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLaborPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numBonus).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F);
            label1.Location = new Point(231, 28);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 0;
            label1.Text = "產品編號:";
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Microsoft JhengHei UI", 12F);
            txtCode.Location = new Point(314, 23);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(157, 28);
            txtCode.TabIndex = 1;
            // 
            // btnFetch
            // 
            btnFetch.Font = new Font("Microsoft JhengHei UI", 12F);
            btnFetch.Location = new Point(492, 23);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new Size(68, 30);
            btnFetch.TabIndex = 2;
            btnFetch.Text = "查單價";
            btnFetch.UseVisualStyleBackColor = true;
            btnFetch.Click += btnFetch_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 12F);
            label2.Location = new Point(231, 70);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 3;
            label2.Text = "產品名稱:";
            // 
            // lblName
            // 
            lblName.Font = new Font("Microsoft JhengHei UI", 12F);
            lblName.Location = new Point(314, 67);
            lblName.Name = "lblName";
            lblName.Size = new Size(157, 23);
            lblName.TabIndex = 4;
            lblName.Text = "-";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Font = new Font("Microsoft JhengHei UI", 12F);
            label4.Location = new Point(231, 105);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 5;
            label4.Text = "單價:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblUnitPrice
            // 
            lblUnitPrice.Font = new Font("Microsoft JhengHei UI", 12F);
            lblUnitPrice.Location = new Point(314, 102);
            lblUnitPrice.Name = "lblUnitPrice";
            lblUnitPrice.Size = new Size(157, 23);
            lblUnitPrice.TabIndex = 6;
            lblUnitPrice.Text = "-";
            lblUnitPrice.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLTitle
            // 
            lblLTitle.AutoSize = true;
            lblLTitle.Font = new Font("Microsoft JhengHei UI", 12F);
            lblLTitle.Location = new Point(599, 119);
            lblLTitle.Name = "lblLTitle";
            lblLTitle.Size = new Size(29, 20);
            lblLTitle.TabIndex = 7;
            lblLTitle.Text = "高:";
            lblLTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblLTitle.Click += lblLTitle_Click;
            // 
            // lblWTitle
            // 
            lblWTitle.AutoSize = true;
            lblWTitle.Font = new Font("Microsoft JhengHei UI", 12F);
            lblWTitle.Location = new Point(599, 73);
            lblWTitle.Name = "lblWTitle";
            lblWTitle.Size = new Size(29, 20);
            lblWTitle.TabIndex = 8;
            lblWTitle.Text = "寬:";
            lblWTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAddToCart
            // 
            btnAddToCart.AutoSize = true;
            btnAddToCart.Font = new Font("Microsoft JhengHei UI", 12F);
            btnAddToCart.Location = new Point(1009, 206);
            btnAddToCart.Name = "btnAddToCart";
            btnAddToCart.Size = new Size(83, 30);
            btnAddToCart.TabIndex = 9;
            btnAddToCart.Text = "加入帳單";
            btnAddToCart.UseVisualStyleBackColor = true;
            btnAddToCart.Click += btnAddToCart_Click;
            // 
            // numLength
            // 
            numLength.DecimalPlaces = 2;
            numLength.Font = new Font("Microsoft JhengHei UI", 12F);
            numLength.Location = new Point(634, 116);
            numLength.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numLength.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numLength.Name = "numLength";
            numLength.Size = new Size(120, 28);
            numLength.TabIndex = 10;
            numLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numWidth
            // 
            numWidth.DecimalPlaces = 2;
            numWidth.Font = new Font("Microsoft JhengHei UI", 12F);
            numWidth.Location = new Point(634, 70);
            numWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numWidth.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(120, 28);
            numWidth.TabIndex = 11;
            numWidth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Location = new Point(23, 206);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(969, 543);
            dgvCart.TabIndex = 12;
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemove.AutoSize = true;
            btnRemove.Font = new Font("Microsoft JhengHei UI", 12F);
            btnRemove.Location = new Point(1009, 320);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(83, 30);
            btnRemove.TabIndex = 13;
            btnRemove.Text = "刪除選取";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.AutoSize = true;
            btnClear.Font = new Font("Microsoft JhengHei UI", 12F);
            btnClear.Location = new Point(1009, 377);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(83, 30);
            btnClear.TabIndex = 14;
            btnClear.Text = "清空帳單";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGrandTotal.Font = new Font("Microsoft JhengHei UI", 13F);
            lblGrandTotal.Location = new Point(998, 714);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.Size = new Size(185, 35);
            lblGrandTotal.TabIndex = 15;
            lblGrandTotal.Text = "總金額:-";
            lblGrandTotal.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 12F);
            label6.Location = new Point(790, 74);
            label6.Name = "label6";
            label6.Size = new Size(45, 20);
            label6.TabIndex = 16;
            label6.Text = "備註:";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLineNote
            // 
            txtLineNote.Font = new Font("Microsoft JhengHei UI", 12F);
            txtLineNote.Location = new Point(841, 70);
            txtLineNote.Name = "txtLineNote";
            txtLineNote.Size = new Size(229, 28);
            txtLineNote.TabIndex = 17;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfirm.AutoSize = true;
            btnConfirm.Font = new Font("Microsoft JhengHei UI", 12F);
            btnConfirm.Location = new Point(1009, 437);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(83, 30);
            btnConfirm.TabIndex = 18;
            btnConfirm.Text = "確認帳單";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 12F);
            label7.Location = new Point(23, 28);
            label7.Name = "label7";
            label7.Size = new Size(45, 20);
            label7.TabIndex = 19;
            label7.Text = "客戶:";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboCustomer
            // 
            cboCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCustomer.Font = new Font("Microsoft JhengHei UI", 12F);
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(74, 23);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(121, 28);
            cboCustomer.TabIndex = 20;
            // 
            // lblSubtotalTotal
            // 
            lblSubtotalTotal.AutoSize = true;
            lblSubtotalTotal.Font = new Font("Microsoft JhengHei UI", 13F);
            lblSubtotalTotal.Location = new Point(998, 639);
            lblSubtotalTotal.Name = "lblSubtotalTotal";
            lblSubtotalTotal.Size = new Size(94, 23);
            lblSubtotalTotal.TabIndex = 21;
            lblSubtotalTotal.Text = "小計合計:-";
            lblSubtotalTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWageTotal
            // 
            lblWageTotal.AutoSize = true;
            lblWageTotal.Font = new Font("Microsoft JhengHei UI", 13F);
            lblWageTotal.Location = new Point(998, 680);
            lblWageTotal.Name = "lblWageTotal";
            lblWageTotal.Size = new Size(94, 23);
            lblWageTotal.TabIndex = 22;
            lblWageTotal.Text = "工資合計:-";
            lblWageTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cboKind
            // 
            cboKind.Font = new Font("Microsoft JhengHei UI", 12F);
            cboKind.FormattingEnabled = true;
            cboKind.Location = new Point(74, 100);
            cboKind.Name = "cboKind";
            cboKind.Size = new Size(121, 28);
            cboKind.TabIndex = 23;
            // 
            // numYards
            // 
            numYards.DecimalPlaces = 1;
            numYards.Font = new Font("Microsoft JhengHei UI", 12F);
            numYards.Location = new Point(634, 25);
            numYards.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numYards.Name = "numYards";
            numYards.Size = new Size(120, 28);
            numYards.TabIndex = 24;
            numYards.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numQty
            // 
            numQty.Font = new Font("Microsoft JhengHei UI", 12F);
            numQty.Location = new Point(634, 162);
            numQty.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numQty.Name = "numQty";
            numQty.Size = new Size(120, 28);
            numQty.TabIndex = 25;
            numQty.ValueChanged += numQty_ValueChanged;
            // 
            // lblYard
            // 
            lblYard.AutoSize = true;
            lblYard.Font = new Font("Microsoft JhengHei UI", 12F);
            lblYard.Location = new Point(583, 28);
            lblYard.Name = "lblYard";
            lblYard.Size = new Size(45, 20);
            lblYard.TabIndex = 26;
            lblYard.Text = "碼數:";
            lblYard.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Font = new Font("Microsoft JhengHei UI", 12F);
            lblQty.Location = new Point(583, 165);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(45, 20);
            lblQty.TabIndex = 27;
            lblQty.Text = "數量:";
            lblQty.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft JhengHei UI", 12F);
            label8.Location = new Point(23, 66);
            label8.Name = "label8";
            label8.Size = new Size(93, 20);
            label8.TabIndex = 28;
            label8.Text = "非布料選擇:";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLaborName
            // 
            lblLaborName.AutoSize = true;
            lblLaborName.Font = new Font("Microsoft JhengHei UI", 12F);
            lblLaborName.Location = new Point(17, 154);
            lblLaborName.Name = "lblLaborName";
            lblLaborName.Size = new Size(99, 40);
            lblLaborName.TabIndex = 29;
            lblLaborName.Text = "名稱:\r\n(特殊車工用)";
            lblLaborName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLaborName
            // 
            txtLaborName.Font = new Font("Microsoft JhengHei UI", 12F);
            txtLaborName.Location = new Point(122, 154);
            txtLaborName.Name = "txtLaborName";
            txtLaborName.Size = new Size(119, 28);
            txtLaborName.TabIndex = 30;
            // 
            // lblLaborPrice
            // 
            lblLaborPrice.AutoSize = true;
            lblLaborPrice.Font = new Font("Microsoft JhengHei UI", 12F);
            lblLaborPrice.Location = new Point(247, 154);
            lblLaborPrice.Name = "lblLaborPrice";
            lblLaborPrice.Size = new Size(99, 40);
            lblLaborPrice.TabIndex = 31;
            lblLaborPrice.Text = "單價:\r\n(特殊車工用)";
            lblLaborPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numLaborPrice
            // 
            numLaborPrice.DecimalPlaces = 1;
            numLaborPrice.Font = new Font("Microsoft JhengHei UI", 12F);
            numLaborPrice.Location = new Point(352, 154);
            numLaborPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numLaborPrice.Name = "numLaborPrice";
            numLaborPrice.Size = new Size(120, 28);
            numLaborPrice.TabIndex = 32;
            // 
            // lblBonusTitle
            // 
            lblBonusTitle.AutoSize = true;
            lblBonusTitle.Font = new Font("Microsoft JhengHei UI", 12F);
            lblBonusTitle.Location = new Point(790, 28);
            lblBonusTitle.Name = "lblBonusTitle";
            lblBonusTitle.Size = new Size(60, 20);
            lblBonusTitle.TabIndex = 33;
            lblBonusTitle.Text = "Bonus:";
            lblBonusTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numBonus
            // 
            numBonus.Font = new Font("Microsoft JhengHei UI", 12F);
            numBonus.Location = new Point(856, 26);
            numBonus.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numBonus.Name = "numBonus";
            numBonus.Size = new Size(120, 28);
            numBonus.TabIndex = 34;
            // 
            // btnSwapSubtotalWage
            // 
            btnSwapSubtotalWage.AutoSize = true;
            btnSwapSubtotalWage.Font = new Font("Microsoft JhengHei UI", 12F);
            btnSwapSubtotalWage.Location = new Point(1009, 264);
            btnSwapSubtotalWage.Name = "btnSwapSubtotalWage";
            btnSwapSubtotalWage.Size = new Size(94, 30);
            btnSwapSubtotalWage.TabIndex = 35;
            btnSwapSubtotalWage.Text = "小計⇄工資";
            btnSwapSubtotalWage.UseVisualStyleBackColor = true;
            btnSwapSubtotalWage.Click += btnSwapSubtotalWage_Click;
            // 
            // QuickCalcForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(btnSwapSubtotalWage);
            Controls.Add(numBonus);
            Controls.Add(lblBonusTitle);
            Controls.Add(numLaborPrice);
            Controls.Add(lblLaborPrice);
            Controls.Add(txtLaborName);
            Controls.Add(lblLaborName);
            Controls.Add(label8);
            Controls.Add(lblQty);
            Controls.Add(lblYard);
            Controls.Add(numQty);
            Controls.Add(numYards);
            Controls.Add(cboKind);
            Controls.Add(lblWageTotal);
            Controls.Add(lblSubtotalTotal);
            Controls.Add(cboCustomer);
            Controls.Add(label7);
            Controls.Add(btnConfirm);
            Controls.Add(txtLineNote);
            Controls.Add(label6);
            Controls.Add(lblGrandTotal);
            Controls.Add(btnClear);
            Controls.Add(btnRemove);
            Controls.Add(dgvCart);
            Controls.Add(numWidth);
            Controls.Add(numLength);
            Controls.Add(btnAddToCart);
            Controls.Add(lblWTitle);
            Controls.Add(lblLTitle);
            Controls.Add(lblUnitPrice);
            Controls.Add(label4);
            Controls.Add(lblName);
            Controls.Add(label2);
            Controls.Add(btnFetch);
            Controls.Add(txtCode);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "QuickCalcForm";
            Text = "出貨單";
            ((System.ComponentModel.ISupportInitialize)numLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)numWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYards).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLaborPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numBonus).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtCode;
        private Button btnFetch;
        private Label label2;
        private Label lblName;
        private Label label4;
        private Label lblUnitPrice;
        private Label lblLTitle;
        private Label lblWTitle;
        private Button btnAddToCart;
        private NumericUpDown numLength;
        private NumericUpDown numWidth;
        private DataGridView dgvCart;
        private Button btnRemove;
        private Button btnClear;
        private Label lblGrandTotal;
        private Label label6;
        private TextBox txtLineNote;
        private Button btnConfirm;
        private Label label7;
        private ComboBox cboCustomer;
        private Label lblSubtotalTotal;
        private Label lblWageTotal;
        private ComboBox cboKind;
        private NumericUpDown numYards;
        private NumericUpDown numQty;
        private Label lblYard;
        private Label lblQty;
        private Label label8;
        private Label lblLaborName;
        private TextBox txtLaborName;
        private Label lblLaborPrice;
        private NumericUpDown numLaborPrice;
        private Label lblBonusTitle;
        private NumericUpDown numBonus;
        private Button btnSwapSubtotalWage;
    }
}