using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class MonthlyReportForm : Form
    {
        private DateTimePicker dtFrom, dtTo;
        private DataGridView dgv;
        private Label lblSumSubtotal, lblSumWage, lblSumTotal;

        private readonly BindingList<Row> _rows = new();

        private class Row
        {
            public string CustomerId { get; set; } = "";
            public string CustomerName { get; set; } = "";
            public double PeriodSubtotal { get; set; }   // 本期應收（小計合計）
            public double PeriodWage { get; set; }       // 工資合計
            public double PeriodTotal => PeriodSubtotal + PeriodWage;
        }

        public MonthlyReportForm()
        {
            InitializeComponent();

            // 規格：視窗大小、全介面字型 12pt
            Text = "月報表（期間彙總）";
            ClientSize = new Size(1200, 800);
            Font = new Font("Microsoft JhengHei UI", 12F);

            // ---- 上方條（起/迄，日期框緊貼在文字後面）----
            var topMargin = 14;
            var leftMargin = 12;

            var lblFrom = new Label { Left = leftMargin, Top = topMargin, AutoSize = true, Text = "起：" };
            dtFrom = new DateTimePicker
            {
                Left = lblFrom.Right-58,  // ← 貼著
                Top = lblFrom.Top - 3,
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy/MM/dd"
            };

            var lblTo = new Label { Left = dtFrom.Right + 16, Top = lblFrom.Top, AutoSize = true, Text = "迄：" };
            dtTo = new DateTimePicker
            {
                Left = lblTo.Right -58,    // ← 貼著
                Top = lblFrom.Top - 3,
                Width = 150,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy/MM/dd"
            };

            Controls.AddRange(new Control[] { lblFrom, dtFrom, lblTo, dtTo });

            // 預設：本月一號 ~ 今天
            var now = DateTime.Now;
            dtFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtTo.Value = now.Date;

            // ---- Grid ----
            dgv = new DataGridView
            {
                Left = leftMargin,
                Top = lblFrom.Bottom + 10,
                Width = ClientSize.Width - leftMargin * 2,
                Height = ClientSize.Height - 220,  // 預留下面合計區
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = _rows
            };
            // Grid 也使用 12pt
            dgv.Font = this.Font;
            dgv.ColumnHeadersDefaultCellStyle.Font = this.Font;
            dgv.RowHeadersDefaultCellStyle.Font = this.Font;
            // 整個 DataGridView 的標頭自動調整
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "客戶名稱",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = nameof(Row.CustomerName),
                MinimumWidth = 260
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "本期應收",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = nameof(Row.PeriodSubtotal),
                Width = 160,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,0.##" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "工資合計",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = nameof(Row.PeriodWage),
                Width = 160,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,0.##" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "合計應收",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataPropertyName = nameof(Row.PeriodTotal),
                Width = 160,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "#,0.##" }
            });

            Controls.Add(dgv);

            // ---- 底部合計 ----
            var bottomTop = dgv.Bottom + 12;
            lblSumSubtotal = new Label { Left = leftMargin, Top = bottomTop, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Bottom };
            lblSumWage = new Label { Left = leftMargin, Top = lblSumSubtotal.Bottom + 6, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Bottom };
            lblSumTotal = new Label { Left = leftMargin, Top = lblSumWage.Bottom + 6, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Bottom };
            Controls.AddRange(new Control[] { lblSumSubtotal, lblSumWage, lblSumTotal });

            // 事件：改日期就即時查（已移除查詢/關閉按鈕）
            dtFrom.ValueChanged += (_, __) => RefreshData();
            dtTo.ValueChanged += (_, __) => RefreshData();

            // 首次載入
            RefreshData();
        }

        // 你的 RefreshData() 原本內容保持不變
        private void RefreshData()
        {
            _rows.Clear();

            var from = dtFrom.Value.Date;
            var toExclusive = dtTo.Value.Date.AddDays(1);

            var orders = DataStore.Orders
                .Where(o => o.CreatedAt >= from && o.CreatedAt < toExclusive)
                .ToList();

            var grouped = orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    CustomerName = g.Select(x => x.CustomerName).FirstOrDefault() ?? "",
                    Subtotal = g.Sum(o => o.Lines?.Sum(l => l.Subtotal) ?? 0),
                    Wage = g.Sum(o => o.Lines?.Sum(l => l.Wage) ?? 0)
                })
                .Where(x => (x.Subtotal > 0) || (x.Wage > 0))
                .OrderBy(x => x.CustomerName, StringComparer.CurrentCulture)
                .ToList();

            foreach (var x in grouped)
            {
                _rows.Add(new Row
                {
                    CustomerId = x.CustomerId ?? "",
                    CustomerName = x.CustomerName ?? "",
                    PeriodSubtotal = Math.Round(x.Subtotal, 2),
                    PeriodWage = Math.Round(x.Wage, 2)
                });
            }

            var sumSub = _rows.Sum(r => r.PeriodSubtotal);
            var sumWage = _rows.Sum(r => r.PeriodWage);
            var sumTotal = sumSub + sumWage;

            lblSumSubtotal.Text = $"本期應收合計：{sumSub:#,0.##}";
            lblSumWage.Text = $"工資合計：{sumWage:#,0.##}";
            lblSumTotal.Text = $"應收合計：{sumTotal:#,0.##}";
        }
    }
}
