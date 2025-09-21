using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ProductCalculatorApp
{
    public partial class HistoryForm : Form
    {
        private BindingList<Order> _orders = new BindingList<Order>();
        // ★ 改成直接綁真正的明細
        private BindingList<OrderLine> _lines = new BindingList<OrderLine>();

        public HistoryForm()
        {
            InitializeComponent();

            // ===== 單頭（上方）=====
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "日期",
                DataPropertyName = "CreatedAt",
                Width = 160,
                DefaultCellStyle = { Format = "yyyy/MM/dd" }
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "客戶",
                DataPropertyName = "CustomerName",  // 要在 Order 類別加一個屬性
                Width = 100
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "件數",
                DataPropertyName = "LineCount",
                Width = 60
            });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "總金額",
                DataPropertyName = "Total",
                Width = 100,
                DefaultCellStyle = { Format = "0.##" }
            });
            dgvOrders.DataSource = _orders;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;

            // 上面：填滿
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ScrollBars = ScrollBars.Vertical;
            dgvOrders.Columns[0].FillWeight = 150;
            dgvOrders.Columns[1].FillWeight = 50;
            dgvOrders.Columns[2].FillWeight = 100;

            // ===== 明細（下方）=====
            dgvLines.AutoGenerateColumns = false;
            dgvLines.Columns.Clear();

            // 未繫結欄：產品編號(商品顯示 Code，車工顯示名稱)
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCodeOrLabor",
                HeaderText = "產品編號",
                //ReadOnly = true,
                Width = 120
            });

            // ★ 品名規格：改成未繫結，顯示時再依規則決定「長×寬」或「寬×高」
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSpec",
                HeaderText = "品名規格",
                //ReadOnly = true,
                Width = 160
            });

            // 可編輯欄
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "數量",
                DataPropertyName = "Quantity",
                Width = 70,
                DefaultCellStyle = { Format = "0.##" }
            });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "單位",
                DataPropertyName = "Unit",
                Width = 60,
                ReadOnly = true
            });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "單價",
                DataPropertyName = "UnitPrice",
                Width = 90,
                DefaultCellStyle = { Format = "0.##" }
            });

            // 計算欄：唯讀
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "小計",
                DataPropertyName = "Subtotal",
                Width = 90,
                //ReadOnly = true,
                DefaultCellStyle = { Format = "0.##" }
            });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "工資",
                DataPropertyName = "Wage",
                Width = 90,
                //ReadOnly = true,
                DefaultCellStyle = { Format = "0.##" }
            });
            dgvLines.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "備註",
                DataPropertyName = "Note",
                Width = 280
            });

            dgvLines.DataSource = _lines;
            dgvLines.ReadOnly = false;
            dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLines.MultiSelect = false;
            dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvLines.ScrollBars = ScrollBars.Both;
            dgvLines.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // ===== 篩選元件 =====
            dtFrom.Format = DateTimePickerFormat.Custom; dtFrom.CustomFormat = "yyyy/MM/dd"; dtFrom.ShowCheckBox = true;
            dtTo.Format = DateTimePickerFormat.Custom; dtTo.CustomFormat = "yyyy/MM/dd"; dtTo.ShowCheckBox = true;
            rdoAll.Checked = true;

            LoadCustomerFilter();
            cboCustomerFilter.SelectedIndexChanged += (_, __) => ApplyFilter();
            numAmtMin.ValueChanged += (_, __) => ApplyFilter();
            numAmtMax.ValueChanged += (_, __) => ApplyFilter();
            dtFrom.ValueChanged += (_, __) => ApplyFilter();
            dtTo.ValueChanged += (_, __) => ApplyFilter();
            rdoAll.CheckedChanged += (_, __) => { if (rdoAll.Checked) ApplyFilter(); };
            rdoDay.CheckedChanged += (_, __) => { if (rdoDay.Checked) ApplyFilter(); };
            rdoWeek.CheckedChanged += (_, __) => { if (rdoWeek.Checked) ApplyFilter(); };
            rdoMonth.CheckedChanged += (_, __) => { if (rdoMonth.Checked) ApplyFilter(); };
            btnSearch.Click += (_, __) => ApplyFilter();
            btnReset.Click += (_, __) => ResetFilters();

            // 換單時載入明細
            dgvOrders.SelectionChanged += (_, __) => ShowCurrentOrderLines();
            dgvOrders.CellClick += (_, __) => ShowCurrentOrderLines();

            // ===== 明細：顯示用格式化 & 即時重算 =====
            dgvLines.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= _lines.Count) return;
                var l = _lines[e.RowIndex];
                var colName = dgvLines.Columns[e.ColumnIndex].Name;

                if (colName == "colCodeOrLabor")
                {
                    e.Value = string.IsNullOrWhiteSpace(l.Code) ? l.Name : l.Code;
                    e.FormattingApplied = true;
                }
                else if (colName == "colSpec")
                {
                    e.Value = FormatSpec(l);   // ← 依 Product.Kind 套用 長×寬 / 寬×高
                    e.FormattingApplied = true;
                }
            };

            // 讓編輯中的 cell 立刻觸發 CellValueChanged
            dgvLines.CurrentCellDirtyStateChanged += (_, __) =>
            {
                if (dgvLines.IsCurrentCellDirty)
                    dgvLines.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dgvLines.CellValueChanged += dgvLines_CellValueChanged;

            // 初次載入
            ApplyFilter();
        }

        // === 顯示規則：捲簾顯示「長×寬(=高×寬)」，其餘維持「寬×高」 ===
        private static string FormatSpec(OrderLine l)
        {
            decimal w = (decimal)l.Width;
            decimal h = (decimal)l.Length;
            return $"{w:0.##}*{h:0.##}";     // 其他：寬×高
        }

        // 用產品代碼到 DataStore.Products 找商品 → 判斷 Kind
        private static bool IsRoller(OrderLine l)
        {
            if (string.IsNullOrWhiteSpace(l?.Code)) return false;
            var p = DataStore.Products.FirstOrDefault(x =>
                string.Equals(x.Code?.Trim(), l.Code?.Trim(), StringComparison.OrdinalIgnoreCase));
            return p?.Kind == ProductKind.Roller;
        }

        // ====== 篩選與載入 ======
        private void LoadCustomerFilter()
        {
            var list = new List<Customer> { new Customer { Id = "*", Name = "全部" } };
            list.AddRange(DataStore.Customers.Where(c => c.Active).OrderBy(c => c.Name));
            cboCustomerFilter.DisplayMember = "Name";
            cboCustomerFilter.ValueMember = "Id";
            cboCustomerFilter.DataSource = list;
            cboCustomerFilter.SelectedValue = "*";
        }

        private void ApplyFilter()
        {
            var q = DataStore.Orders.AsEnumerable();

            var sel = cboCustomerFilter.SelectedValue?.ToString();
            if (!string.IsNullOrWhiteSpace(sel) && sel != "*")
            {
                var cid = sel.Trim();
                q = q.Where(o => string.Equals(o.CustomerId?.Trim(), cid, StringComparison.OrdinalIgnoreCase));
            }

            var (qs, qe) = GetQuickRange();
            DateTime? start = qs, end = qe;
            if (dtFrom.Checked) start = dtFrom.Value.Date;
            if (dtTo.Checked) end = dtTo.Value.Date.AddDays(1);

            if (start.HasValue) q = q.Where(o => o.CreatedAt >= start.Value);
            if (end.HasValue) q = q.Where(o => o.CreatedAt < end.Value);
            string codeFilter = txtCodeFilter.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(codeFilter))
            {
                q = q.Where(o => o.Lines.Any(l =>
                    (!string.IsNullOrWhiteSpace(l.Code) &&
                     l.Code.IndexOf(codeFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                ));
            }

            double min = (double)numAmtMin.Value, max = (double)numAmtMax.Value;
            if (min > 0 || max > 0)
            {
                if (max > 0 && min > max)
                {
                    var t = min; min = max; max = t;
                }
                q = q.Where(o => o.Total >= (min > 0 ? min : double.MinValue) &&
                                 o.Total <= (max > 0 ? max : double.MaxValue));
            }

            _orders = new BindingList<Order>(q.OrderByDescending(o => o.CreatedAt).ToList());
            dgvOrders.DataSource = _orders;
            dgvOrders.ClearSelection();

            if (_orders.Count > 0)
            {
                dgvOrders.Rows[0].Selected = true;
                ShowCurrentOrderLines();
            }
            else
            {
                _lines = new BindingList<OrderLine>();
                dgvLines.DataSource = _lines;
            }
        }

        private (DateTime? start, DateTime? end) GetQuickRange()
        {
            if (rdoDay.Checked)
            {
                var s = DateTime.Today;
                return (s, s.AddDays(1));
            }
            if (rdoWeek.Checked)
            {
                var today = DateTime.Today;
                int dow = (int)today.DayOfWeek;
                var monday = today.AddDays(dow == 0 ? -6 : (1 - dow));
                return (monday, monday.AddDays(7));
            }
            if (rdoMonth.Checked)
            {
                var s = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                return (s, s.AddMonths(1));
            }
            return (null, null);
        }

        private void ResetFilters()
        {
            numAmtMin.Value = 0;
            numAmtMax.Value = 0;
            dtFrom.Checked = false;
            dtTo.Checked = false;
            if (cboCustomerFilter.Items.Count > 0) cboCustomerFilter.SelectedIndex = 0;
            rdoAll.Checked = true;
            ApplyFilter();
        }

        // ====== 明細載入（直接綁原始 Lines，才能寫回） ======
        private void ShowCurrentOrderLines()
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order od)
            {
                _lines = new BindingList<OrderLine>(od.Lines); // 不要 ToList()
                dgvLines.DataSource = _lines;
                dgvLines.Refresh();
            }
            else
            {
                _lines = new BindingList<OrderLine>();
                dgvLines.DataSource = _lines;
            }
        }

        // ====== 即時重算 + 存檔 ======
        private void dgvLines_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _lines.Count) return;
            var l = _lines[e.RowIndex];

            // 判斷商品/工資類：商品通常有 Code 或 Unit=="碼"
            bool isProduct = !string.IsNullOrWhiteSpace(l.Code) ||
                             string.Equals(l.Unit, "碼", StringComparison.Ordinal);

            if (isProduct)
            {
                l.Subtotal = Math.Round(l.UnitPrice * l.Quantity, 2);
                l.Wage = 0;
            }
            else
            {
                l.Wage = Math.Round(l.UnitPrice * l.Quantity, 2);
                l.Subtotal = 0;
            }

            // 更新單頭總額 & 存檔
            if (dgvOrders.CurrentRow?.DataBoundItem is Order od)
            {
                od.Total = Math.Round(od.Lines.Sum(x => x.Subtotal + x.Wage), 2);
                dgvOrders.Refresh();
            }

            dgvLines.InvalidateRow(e.RowIndex);
            DataStore.SaveAll();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ShowCurrentOrderLines();
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            using var f = new MonthlyStatementForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            using var f = new DailyStatementForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("確定要清除所有歷史紀錄嗎？此操作無法復原！",
                                    "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                DataStore.ClearAllOrders();
                ApplyFilter();   // 重新整理顯示（清空 DataGridView）
                MessageBox.Show("所有紀錄已清除。");
            }
        }

    }
}
