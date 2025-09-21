namespace ProductCalculatorApp
{
    partial class CustomerManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerManagerForm));
            dgvCustomers = new DataGridView();
            btnAddRow = new Button();
            btnDeleteRow = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // dgvCustomers
            // 
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(0, 0);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.Size = new Size(924, 762);
            dgvCustomers.TabIndex = 0;
            // 
            // btnAddRow
            // 
            btnAddRow.Font = new Font("Microsoft JhengHei UI", 12F);
            btnAddRow.Location = new Point(1000, 276);
            btnAddRow.Name = "btnAddRow";
            btnAddRow.Size = new Size(100, 30);
            btnAddRow.TabIndex = 1;
            btnAddRow.Text = "新增一列";
            btnAddRow.UseVisualStyleBackColor = true;
            btnAddRow.Click += btnAddRow_Click;
            // 
            // btnDeleteRow
            // 
            btnDeleteRow.Font = new Font("Microsoft JhengHei UI", 12F);
            btnDeleteRow.Location = new Point(1000, 356);
            btnDeleteRow.Name = "btnDeleteRow";
            btnDeleteRow.Size = new Size(100, 30);
            btnDeleteRow.TabIndex = 3;
            btnDeleteRow.Text = "刪除選取列";
            btnDeleteRow.UseVisualStyleBackColor = true;
            btnDeleteRow.Click += btnDeleteRow_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Microsoft JhengHei UI", 12F);
            btnSave.Location = new Point(1000, 447);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 5;
            btnSave.Text = "存檔";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // CustomerManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(btnSave);
            Controls.Add(btnDeleteRow);
            Controls.Add(btnAddRow);
            Controls.Add(dgvCustomers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CustomerManagerForm";
            Text = "客戶清單";
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private Button btnAddRow;
        private Button btnDeleteRow;
        private Button btnSave;
    }
}