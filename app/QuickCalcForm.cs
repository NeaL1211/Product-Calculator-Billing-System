using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class QuickCalcForm : Form
    {
        // 目前查到的商品
        private Product _current;

        // 購物車資料源
        private BindingList<CartRow> _cart = new BindingList<CartRow>();

        // 只保留兩個模式
        private const string MODE_PRODUCT = "產品";
        private const string MODE_SPECIAL = "特殊車工";

        private readonly BindingSource _cartSource = new BindingSource();

        public QuickCalcForm()
        {
            InitializeComponent();

            // 下拉只能選、支援自動完成
            cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;

            // 數值控制
            numLength.DecimalPlaces = 2; numLength.Minimum = 0.01M; numLength.Maximum = 1000000M; numLength.Value = 1M;
            numWidth.DecimalPlaces = 2; numWidth.Minimum = 0.01M; numWidth.Maximum = 1000000M; numWidth.Value = 1M;

            InitCartGrid();

            lblGrandTotal.Text = "總金額：—";

            // 序號 Enter 直接查
            txtCode.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnFetch_Click(s, EventArgs.Empty);
                    e.SuppressKeyPress = true;
                }
            };

            ReloadCustomers();
            SetupKindSwitcher();
        }

        private void SetupKindSwitcher()
        {
            cboKind.Items.Clear();
            cboKind.Items.AddRange(new object[] { MODE_PRODUCT, MODE_SPECIAL });
            cboKind.SelectedIndex = 0;

            numYards.DecimalPlaces = 2; numYards.Minimum = 0; numYards.Maximum = 999999;
            numQty.DecimalPlaces = 0; numQty.Minimum = 1; numQty.Maximum = 999999;
            numLength.DecimalPlaces = 2;
            numWidth.DecimalPlaces = 2;

            // ★ Bonus 欄位設定
            numBonus.DecimalPlaces = 2;
            numBonus.Minimum = 0;           // 只加價（若要可折扣改成負的：Minimum = -999999）
            numBonus.Maximum = 999999;
            numBonus.Value = 0;

            cboKind.SelectedIndexChanged += (_, __) => { ApplyKindUI(); UpdatePreviewLabels(); };
            numLength.ValueChanged += (_, __) => UpdatePreviewLabels();
            numBonus.ValueChanged += (_, __) => UpdatePreviewLabels();   // ★ Bonus 變動即時更新
            txtLaborName.TextChanged += (_, __) => UpdatePreviewLabels();
            numLaborPrice.ValueChanged += (_, __) => UpdatePreviewLabels();

            ApplyKindUI();
        }

        /// <summary>依目前模式(產品/特殊車工) + 商品Kind 切換 UI</summary>
        private void ApplyKindUI()
        {
            string mode = cboKind.SelectedItem?.ToString() ?? MODE_PRODUCT;

            if (mode == MODE_SPECIAL)
            {
                // 特殊車工：隱藏產品相關，顯示特殊車工欄位；Bonus 隱藏
                numYards.Visible = false;
                numWidth.Visible = false;
                numLength.Visible = false;
                lblQty.Visible = numQty.Visible = true;

                lblWTitle.Text = "寬：";
                lblLTitle.Text = "高：";

                lblLaborName.Visible = txtLaborName.Visible = true;
                lblLaborPrice.Visible = numLaborPrice.Visible = true;

                lblBonusTitle.Visible = false;   // ★
                numBonus.Visible = false;   // ★
                return;
            }

            // 產品模式：隱藏特殊車工欄位
            lblLaborName.Visible = txtLaborName.Visible = false;
            lblLaborPrice.Visible = numLaborPrice.Visible = false;

            // 尚未查到商品 → 先顯示碼數 + 數量
            if (_current == null)
            {
                numYards.Visible = true;
                numWidth.Visible = false;
                numLength.Visible = false;
                lblQty.Visible = numQty.Visible = true;

                lblWTitle.Text = "寬：";
                lblLTitle.Text = "高：";

                lblBonusTitle.Visible = true;    // ★ 讓你也可以先填加價
                numBonus.Visible = true;    // ★
                return;
            }

            bool isYard = _current.Kind == ProductKind.Yard;
            bool isRoller = _current.Kind == ProductKind.Roller;
            bool isLabor = _current.Kind == ProductKind.Labor;

            numYards.Visible = isYard;
            numWidth.Visible = isRoller || isLabor;
            numLength.Visible = isRoller || isLabor;

            lblQty.Visible = numQty.Visible = isYard || isLabor;

            lblWTitle.Text = "寬：";
            lblLTitle.Text = "高：";

            lblBonusTitle.Visible = true;   // ★ 產品皆可加價
            numBonus.Visible = true;   // ★
        }

        // ★ 產品模式用：實際單價 = 基礎單價 + Bonus
        private double EffectiveUnitPrice()
        {
            string mode = cboKind.SelectedItem?.ToString() ?? MODE_PRODUCT;
            if (mode == MODE_SPECIAL)
                return (double)numLaborPrice.Value; // 特殊車工不吃 Bonus

            var basePrice = _current != null ? _current.BasePrice : 0.0; // ★ double
            return basePrice + (double)numBonus.Value;
        }

        // 讀客戶
        private void ReloadCustomers()
        {
            var list = DataStore.Customers.Where(c => c.Active).OrderBy(c => c.Name).ToList();
            cboCustomer.DisplayMember = "Name";
            cboCustomer.ValueMember = "Id";
            cboCustomer.DataSource = new BindingList<Customer>(list);
        }

        private void InitCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "產品編號", DataPropertyName = nameof(CartRow.Code), Width = 120 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "品名規格", DataPropertyName = "Spec", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 160 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "數量", DataPropertyName = "Quantity", Width = 70, DefaultCellStyle = { Format = "0.##" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "單位", DataPropertyName = "Unit", Width = 60 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "單價", DataPropertyName = "UnitPrice", Width = 90, DefaultCellStyle = { Format = "0.##" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "小計", DataPropertyName = "Subtotal", Width = 90, DefaultCellStyle = { Format = "0.##" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "工資", DataPropertyName = "Wage", Width = 90, DefaultCellStyle = { Format = "0.##" } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "備註", DataPropertyName = "Note", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = 140 });

            _cartSource.DataSource = _cart;
            dgvCart.DataSource = _cartSource;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.ReadOnly = true;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.ScrollBars = ScrollBars.Both;
            dgvCart.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                if (dgvCart.Columns[e.ColumnIndex].DataPropertyName == "Subtotal")
                {
                    if (e.Value is double val && val == 0)
                    {
                        e.Value = "";             // 顯示空白
                        e.FormattingApplied = true;
                    }
                }
                else if (dgvCart.Columns[e.ColumnIndex].DataPropertyName == "Wage")
                {
                    if (e.Value is double val && val == 0)
                    {
                        e.Value = "";             // 工資 0 也隱藏
                        e.FormattingApplied = true;
                    }
                }
            };

        }

        // 查單價
        private void btnFetch_Click(object sender, EventArgs e)
        {
            var code = txtCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("請先輸入商品序號。");
                return;
            }

            var p = DataStore.FindProduct(code);
            if (p == null)
            {
                lblName.Text = "查無商品";
                lblUnitPrice.Text = "-";

                var go = MessageBox.Show("查無此序號，是否前往『增添樣品庫』新增？",
                                         "找不到商品", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (go == DialogResult.Yes)
                {
                    using var f = new ProductManagerForm { StartPosition = FormStartPosition.CenterParent };
                    f.ShowDialog(this);
                }
                return;
            }

            SelectKindFromProduct(p);
        }
        private void SelectKindFromProduct(Product p)
        {
            _current = p;
            lblName.Text = p.Name ?? "-";
            lblUnitPrice.Text = p.BasePrice.ToString("0.##");
            ApplyKindUI(); // ← 依商品 Kind 切畫面
        }

        private void UpdatePreviewLabels()
        {
            string mode = cboKind.SelectedItem?.ToString() ?? MODE_PRODUCT;

            if (mode == MODE_SPECIAL)
            {
                lblName.Text = string.IsNullOrWhiteSpace(txtLaborName.Text) ? "特殊車工" : txtLaborName.Text.Trim();
                lblUnitPrice.Text = numLaborPrice.Value.ToString("0.##");
                return;
            }

            // 產品模式：用「基礎單價 + Bonus」顯示
            lblName.Text = _current?.Name ?? "-";
            lblUnitPrice.Text = (_current != null ? EffectiveUnitPrice() : 0).ToString("0.##");
        }

        // 加入購物車
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            string mode = cboKind.SelectedItem?.ToString() ?? MODE_PRODUCT;

            // --- 特殊車工 ---
            if (mode == MODE_SPECIAL)
            {
                string name = txtLaborName.Text.Trim();
                if (string.IsNullOrEmpty(name)) { MessageBox.Show("請輸入『名稱(特殊車工用)』"); return; }

                double unit = (double)numLaborPrice.Value;
                double qty = (double)numQty.Value;
                if (qty <= 0) { MessageBox.Show("數量需大於 0。"); return; }

                _cart.Add(new CartRow
                {
                    Type = MODE_SPECIAL,
                    Code = name,
                    Name = name,
                    Spec = "",
                    Quantity = qty,
                    Unit = "",
                    UnitPrice = unit,
                    Subtotal = 0,
                    Wage = Math.Round(unit * qty, 2),
                    Note = txtLineNote.Text.Trim(),
                    Width = 0,
                    Length = 0
                });

                AfterAdded();
                return;
            }

            // --- 產品模式（必須先查到商品） ---
            if (_current == null)
            {
                MessageBox.Show("請先輸入序號並『查單價』。");
                return;
            }

            switch (_current.Kind)
            {
                case ProductKind.Yard:
                    {
                        double yards = (double)numYards.Value;
                        if (yards <= 0) { MessageBox.Show("碼數需大於 0。"); return; }

                        double unit = EffectiveUnitPrice(); // ★
                        _cart.Add(new CartRow
                        {
                            Type = "窗簾(碼)",
                            Code = _current.Code,
                            Name = _current.Name,
                            Spec = "",
                            Quantity = yards,
                            Unit = "碼",
                            UnitPrice = unit,                               // ★
                            Subtotal = Math.Round(unit * yards, 2),        // ★
                            Wage = 0,
                            Note = txtLineNote.Text.Trim(),
                            Width = 0,
                            Length = 0
                        });
                        break;
                    }

                case ProductKind.Roller:
                    {
                        double w = (double)numWidth.Value;
                        double h = (double)numLength.Value;
                        if (w <= 0 || h <= 0) { MessageBox.Show("請輸入寬與長。"); return; }

                        double cai = Math.Round(w * h, 2);
                        double unit = EffectiveUnitPrice();               // ★

                        _cart.Add(new CartRow
                        {
                            Type = "捲簾(才)",
                            Code = _current.Code,
                            Name = _current.Name,
                            Spec = $"{w:0.##}*{h:0.##}",
                            Quantity = cai,
                            Unit = "才",
                            UnitPrice = unit,                              // ★
                            Subtotal = Math.Round(unit * cai, 2),         // ★
                            Wage = 0,
                            Note = txtLineNote.Text.Trim(),
                            Width = w,
                            Length = h
                        });
                        break;
                    }

                case ProductKind.Labor:
                    {
                        double w = (double)numWidth.Value;
                        double h = (double)numLength.Value;
                        double qty = (double)numQty.Value;
                        if (w <= 0 || h <= 0) { MessageBox.Show("請輸入寬與高。"); return; }
                        if (qty <= 0) { MessageBox.Show("數量需大於 0。"); return; }

                        double unit = EffectiveUnitPrice();               // ★

                        _cart.Add(new CartRow
                        {
                            Type = "車工(寬高)",
                            Code = _current.Code,
                            Name = _current.Name,
                            Spec = $"{w:0.##}*{h:0.##}",
                            Quantity = qty,
                            Unit = "",
                            UnitPrice = unit,                              // ★
                            Subtotal = 0,
                            Wage = Math.Round(unit * qty, 2),             // ★ 金額進工資
                            Note = txtLineNote.Text.Trim(),
                            Width = w,
                            Length = h
                        });
                        break;
                    }
            }
            AfterAdded();
            numBonus.Value = 0;   // 加入後重置
        }

        private void AfterAdded()
        {
            RecalcTotals();
            txtLineNote.Clear();
            UpdatePreviewLabels();
        }

        // 移除選取列
        private void btnRemove_Click(object sender, EventArgs e)
        {
            dgvCart.EndEdit();

            if (dgvCart.SelectedRows.Count > 0 &&
                dgvCart.SelectedRows[0].DataBoundItem is CartRow sel)
            {
                _cart.Remove(sel);
                RecalcTotals();
                return;
            }

            if (dgvCart.CurrentRow?.DataBoundItem is CartRow row)
            {
                _cart.Remove(row);
                RecalcTotals();
                return;
            }

            MessageBox.Show("請先選取要刪除的那一列。");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0) return;
            var r = MessageBox.Show("確定要清空購物車嗎？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                _cart.Clear();
                RecalcTotals();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0) { MessageBox.Show("購物車是空的。"); return; }
            if (cboCustomer.SelectedItem is not Customer cust)
            {
                MessageBox.Show("請先選擇客戶。"); return;
            }

            var now = DateTime.Now;
            var order = new Order
            {
                CreatedAt = now,
                CustomerId = cust.Id,   
                CustomerName = cust.Name,
                OrderNo = DataStore.NextOrderNo(cust.Id, now),
                Lines = _cart.Select(x => new OrderLine
                {
                    Code = x.Code,
                    Name = x.Name,
                    UnitPrice = x.UnitPrice,
                    Length = x.Length,
                    Width = x.Width,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    Note = x.Note,
                    Wage = x.Wage,
                    Subtotal = x.Subtotal
                }).ToList()
            };


            order.TotalSubtotal = order.Lines.Sum(l => l.Subtotal);
            order.TotalWage = order.Lines.Sum(l => l.Wage);
            order.Total = order.TotalSubtotal + order.TotalWage;

            DataStore.AddOrder(order);
            _cart.Clear();
            RecalcTotals();
            MessageBox.Show("已建立帳單。");
        }


        private void RecalcTotals()
        {
            var sumSubtotal = _cart.Sum(x => x.Subtotal);
            var sumWage = _cart.Sum(x => x.Wage);
            lblSubtotalTotal.Text = $"小計合計：{sumSubtotal:0.##}";
            lblWageTotal.Text = $"工資合計：{sumWage:0.##}";
            lblGrandTotal.Text = $"總金額：{(sumSubtotal + sumWage):0.##}";
        }

        private class CartRow
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Spec { get; set; }
            public double Quantity { get; set; }
            public string Unit { get; set; }
            public double UnitPrice { get; set; }
            public double Subtotal { get; set; }
            public double Wage { get; set; }
            public string Note { get; set; }
            public double Length { get; set; }
            public double Width { get; set; }
        }

        private void numQty_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSwapSubtotalWage_Click(object? sender, EventArgs e)
        {
            // 收尾任何編輯狀態
            dgvCart.EndEdit();

            // 取得目標列索引（優先用 SelectedRows，否則用 CurrentRow）
            int rowIndex =
                (dgvCart.SelectedRows.Count > 0) ? dgvCart.SelectedRows[0].Index :
                (dgvCart.CurrentRow != null ? dgvCart.CurrentRow.Index : -1);

            if (rowIndex < 0)
            {
                MessageBox.Show("請先選取要切換的小計/工資那一列。");
                return;
            }

            // 透過 BindingList 直接以索引取得原始資料
            if (rowIndex >= _cart.Count) return;
            var src = _cart[rowIndex];

            // 建立新物件（除小計/工資互換，其餘照舊）
            var swapped = new CartRow
            {
                Type = src.Type,
                Name = src.Name,
                Code = src.Code,
                Spec = src.Spec,
                Quantity = src.Quantity,
                Unit = src.Unit,
                UnitPrice = src.UnitPrice,
                Subtotal = src.Wage,     // ★ 互換
                Wage = src.Subtotal, // ★ 互換
                Note = src.Note,
                Length = src.Length,
                Width = src.Width
            };

            // 用「設定索引」覆蓋：BindingList<T>.SetItem 會送出 ListChanged 事件
            _cart[rowIndex] = swapped;

            // 再精確通知這筆更新，確保畫面刷新
            _cartSource.ResetItem(rowIndex);

            // 維持選取不變
            dgvCart.ClearSelection();
            if (rowIndex >= 0 && rowIndex < dgvCart.Rows.Count)
                dgvCart.Rows[rowIndex].Selected = true;

            // 重算總額
            RecalcTotals();
        }

        private void lblLTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
