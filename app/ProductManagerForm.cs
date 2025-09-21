using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class ProductManagerForm : Form
    {
        private BindingList<Product> _binding;
        private BindingSource _bs;
        private bool _dirty = false;


        public ProductManagerForm()
        {
            InitializeComponent();

            this.AutoValidate = AutoValidate.EnableAllowFocusChange; // 關閉時不被驗證卡住
            this.KeyPreview = true;                                  // 可做 Ctrl+S 快捷

            _binding = new BindingList<Product>(
                DataStore.Products.Select(p => new Product
                {
                    Code = p.Code,
                    Name = p.Name,
                    BasePrice = p.BasePrice,
                    Kind = p.Kind
                }).ToList());

            _bs = new BindingSource { DataSource = _binding };

            // 欄位（你也可用 AutoGenerateColumns=true）
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "編號",
                DataPropertyName = "Code",
                Width = 120
            });
            // ★ 型態（碼數/捲簾(才)）
            var colKind = new DataGridViewComboBoxColumn
            {
                HeaderText = "型態",
                DataPropertyName = "Kind",
                Width = 100,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
            };
            colKind.DataSource = new[]
            {
                new { Text = "窗簾(碼)", Val = ProductKind.Yard },
                new { Text = "捲簾(才)", Val = ProductKind.Roller },
                new { Text = "車工(寬高)", Val = ProductKind.Labor }
            };
            colKind.DisplayMember = "Text";
            colKind.ValueMember = "Val";
            dgvProducts.Columns.Add(colKind);
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "單價",
                DataPropertyName = "BasePrice",
                Width = 120
            });

            dgvProducts.DataSource = _bs;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = false;

            // 任何改動都視為「未存檔」
            _bs.ListChanged += (_, __) => _dirty = true;
            dgvProducts.CellValueChanged += (_, __) => _dirty = true;
            dgvProducts.UserAddedRow += (_, __) => _dirty = true;
            dgvProducts.UserDeletedRow += (_, __) => _dirty = true;

            // Ctrl+S 快速存檔
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    if (DoSave()) MessageBox.Show("已存檔。");
                    e.Handled = true;
                }
            };
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            dgvProducts.EndEdit();
            _bs.EndEdit();

            _binding.Add(new Product { Code = "", BasePrice = 0, Kind = ProductKind.Yard });

            // 避免立刻卡在編輯狀態
            dgvProducts.EndEdit();
            _bs.EndEdit();
            dgvProducts.ClearSelection();
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            dgvProducts.EndEdit();
            _bs.EndEdit();
            this.Validate();

            if (dgvProducts.SelectedRows.Count > 0 &&
                dgvProducts.SelectedRows[0].DataBoundItem is Product p)
                _binding.Remove(p);
            else if (dgvProducts.CurrentRow?.DataBoundItem is Product p2)
                _binding.Remove(p2);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DoSave()) MessageBox.Show("已存檔。");
        }

        // === 核心：用你的存檔流程包成方法，並把 _dirty 重設 ===
        private bool DoSave()
        {
            // 你的三步收尾
            dgvProducts.EndEdit();
            _bs.EndEdit();
            this.Validate();

            // 你的檢查（加上 null 安全）
            var dup = _binding.GroupBy(p => (p.Code ?? "").Trim())
                              .FirstOrDefault(g => g.Key == "" || g.Count() > 1);
            if (dup != null)
            {
                MessageBox.Show("商品序號不可空白或重複。");
                return false;
            }

            // 你的存檔流程
            DataStore.Products.Clear();
            foreach (var p in _binding) DataStore.Products.Add(p);
            DataStore.SaveAll();

            _dirty = false; // 存檔成功，重設「髒」狀態
            return true;
        }

        // 關閉前攔截：未存檔就提醒（Yes=存後關；No=不存關；Cancel=不關）
        private void ProductManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 先把正在編輯的值寫回，確保 _dirty 正確
            dgvProducts.EndEdit();
            _bs.EndEdit();
            this.Validate();

            if (_dirty)
            {
                var r = MessageBox.Show("你有尚未存檔的變更，要儲存後再關閉嗎？",
                                        "提醒", MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    if (!DoSave()) e.Cancel = true; // 存失敗就不關
                }
                else if (r == DialogResult.Cancel)
                {
                    e.Cancel = true;                 // 留在畫面
                }
                // r == No：不存也關（若要「一定要存才可關」，這裡改成 e.Cancel = true）
            }
        }
    }
}
