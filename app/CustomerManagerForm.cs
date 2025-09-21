using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class CustomerManagerForm : Form
    {
        private BindingList<Customer> _binding;
        private BindingSource _bs = new BindingSource();
        private bool _dirty = false;

        public CustomerManagerForm()
        {
            InitializeComponent();

            _binding = new BindingList<Customer>(
            DataStore.Customers.Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Mobile = c.Mobile,    // ★ 帶入
                Address = c.Address,  // ★ 帶入
                Active = c.Active,
            }).ToList());

            _bs.DataSource = _binding;

            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "名稱",
                DataPropertyName = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 120
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "電話",
                DataPropertyName = "Phone",
                Width = 160
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "行動電話",        // ★ 新增
                DataPropertyName = "Mobile",
                Width = 160
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "地址",            // ★ 新增
                DataPropertyName = "Address",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 220
            });
            dgvCustomers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "啟用",
                DataPropertyName = "Active",
                Width = 60
            });

            dgvCustomers.DataSource = _bs;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.AllowUserToAddRows = false;

            _bs.ListChanged += (_, __) => _dirty = true;
            dgvCustomers.CellValueChanged += (_, __) => _dirty = true;

            this.FormClosing += CustomerManagerForm_FormClosing;
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            _binding.Add(new Customer { Id = DataStore.NewCustomerId(), Active = true });
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is Customer c)
                _binding.Remove(c);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DoSave()) MessageBox.Show("已存檔。");
        }

        private bool DoSave()
        {
            dgvCustomers.EndEdit(); _bs.EndEdit(); this.Validate();

            if (_binding.Any(x => string.IsNullOrWhiteSpace(x.Name)))
            {
                MessageBox.Show("客戶名稱不可空白。"); return false;
            }
            // 名稱唯一（不分大小寫）
            var dup = _binding.GroupBy(x => x.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                              .FirstOrDefault(g => g.Count() > 1);
            if (dup != null)
            {
                MessageBox.Show($"客戶名稱重複：{dup.Key}"); return false;
            }

            // DoSave() 內，在 ReplaceCustomers 前補齊沒有 Id 的
            foreach (var c in _binding)
                if (string.IsNullOrWhiteSpace(c.Id))
                    c.Id = DataStore.NewCustomerId();

            DataStore.ReplaceCustomers(_binding.ToList());
            _dirty = false;
            return true;
        }

        private void CustomerManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgvCustomers.EndEdit(); _bs.EndEdit(); this.Validate();
            if (_dirty)
            {
                var r = MessageBox.Show("尚未存檔，要儲存後關閉嗎？", "提醒",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes && !DoSave()) e.Cancel = true;
                else if (r == DialogResult.Cancel) e.Cancel = true;
            }
        }
    }
}
