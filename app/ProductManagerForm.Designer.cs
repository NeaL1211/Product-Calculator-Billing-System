namespace ProductCalculatorApp
{
    partial class ProductManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductManagerForm));
            dgvProducts = new DataGridView();
            btnAddRow = new Button();
            btnDeleteRow = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(0, -1);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(1057, 761);
            dgvProducts.TabIndex = 0;
            // 
            // btnAddRow
            // 
            btnAddRow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRow.Font = new Font("Microsoft JhengHei UI", 12F);
            btnAddRow.Location = new Point(1075, 275);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(97, 34);
            btnAddRow.TabIndex = 1;
            btnAddRow.Text = "新增一列";
            btnAddRow.UseVisualStyleBackColor = true;
            btnAddRow.Click += btnAddRow_Click;
            // 
            // btnDeleteRow
            // 
            btnDeleteRow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteRow.Font = new Font("Microsoft JhengHei UI", 12F);
            btnDeleteRow.Location = new Point(1075, 335);
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(97, 34);
            btnDeleteRow.TabIndex = 2;
            btnDeleteRow.Text = "刪除選取列";
            btnDeleteRow.UseVisualStyleBackColor = true;
            btnDeleteRow.Click += btnDeleteRow_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Font = new Font("Microsoft JhengHei UI", 12F);
            btnSave.Location = new Point(1075, 395);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(97, 34);
            btnSave.TabIndex = 3;
            btnSave.Text = "存檔";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ProductManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(btnSave);
            Controls.Add(btnDeleteRow);
            Controls.Add(btnAddRow);
            Controls.Add(dgvProducts);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ProductManagerForm";
            Text = "產品清單";
            FormClosing += ProductManagerForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProducts;
        private Button btnAddRow;
        private Button btnDeleteRow;
        private Button btnSave;
    }
}