using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D; // 畫虛線用

namespace ProductCalculatorApp
{
    public partial class MonthlyStatementForm : Form
    {
        private ComboBox cboCustomer;
        private DateTimePicker dtMonth;
        private Button btnPreview, btnPrint;

        private List<Row> _rows = new List<Row>();
        private int _printIndex = 0;
        private Customer _cust;
        private DateTime _month;
        private double _sumSubtotal, _sumWage;

        private readonly PrintDocument _doc = new PrintDocument();
        private readonly PrintPreviewDialog _preview = new PrintPreviewDialog();
        private readonly PrintDialog _printDlg = new PrintDialog();

        private bool _printedHeader = false;
        private DateTime? _lastDatePrinted = null;

        // 放在類別裡
        private bool _isPreview = false;
        // 0=印明細；1=畫總計上方的橫線；2=總計三欄；3=左下第1行；4=左下第2行；5=左下第3行；6=注意事項；7=全部完成
        private int _footerStage = 0;
        private int _notePrintIndex = 0;


        private class Row
        {
            public DateTime Date { get; set; }
            public string CodeOrLabor { get; set; } = "";
            public string Spec { get; set; } = "";
            public double Qty { get; set; }
            public string Unit { get; set; } = "";
            public double UnitPrice { get; set; }
            public double Subtotal { get; set; }
            public double Wage { get; set; }
            public string Note { get; set; } = "";
        }

        public MonthlyStatementForm()
        {
            Text = "每月請款單";
            Width = 920;
            Height = 120;

            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = new Font("Microsoft JhengHei UI", 12F);

            var lblCustomer = new Label { Left = 10, Top = 12, Text = "客戶：", AutoSize = true };
            Controls.Add(lblCustomer);

            cboCustomer = new ComboBox
            {
                Left = lblCustomer.Right + 8,
                Top = lblCustomer.Top - 3,
                Width = 240,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboCustomer);

            var lblMonth = new Label { Left = cboCustomer.Right + 16, Top = lblCustomer.Top, Text = "月份：", AutoSize = true };
            Controls.Add(lblMonth);

            dtMonth = new DateTimePicker
            {
                Left = lblMonth.Right + 8,
                Top = lblCustomer.Top - 3,
                Width = 120,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy/MM",
                ShowUpDown = true
            };
            Controls.Add(dtMonth);

            btnPreview = new Button { Text = "列印預覽", AutoSize = true };
            btnPreview.Left = dtMonth.Right + 16;
            btnPreview.Top = lblCustomer.Top - 6;

            btnPrint = new Button { Text = "直接列印", AutoSize = true };
            btnPrint.Left = btnPreview.Right + 10;
            btnPrint.Top = btnPreview.Top;

            Controls.AddRange(new Control[] { btnPreview, btnPrint });

            var list = DataStore.Customers.Where(c => c.Active).OrderBy(c => c.Name).ToList();
            cboCustomer.DisplayMember = "Name"; cboCustomer.ValueMember = "Id";
            cboCustomer.DataSource = new BindingList<Customer>(list);
            if (list.Count > 0) cboCustomer.SelectedIndex = 0;

            // 連續紙：取消邊界，由 0,0 起筆
            _doc.OriginAtMargins = false;
            _doc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            _doc.PrintPage += Doc_PrintPage;

            _preview.Document = _doc;
            _preview.UseAntiAlias = true;

            // 讓預覽預設用 100% 顯示、最大化視窗
            _preview.Shown += (s, e) =>
            {
                // PrintPreviewDialog 裡面有個 PrintPreviewControl
                var ctl = _preview.Controls.OfType<PrintPreviewControl>().FirstOrDefault();
                if (ctl != null)
                {
                    ctl.AutoZoom = false;   // 關掉「自動縮放以整頁顯示」
                    ctl.Zoom = 1.0;         // 100% 顯示（可改 1.25、1.5 等）
                    ctl.UseAntiAlias = true;
                }
                _preview.WindowState = FormWindowState.Maximized;
            };
    

            _printDlg.UseEXDialog = true;
            _printDlg.Document = _doc;

            btnPreview.Click += (_, __) =>
            {
                if (!BuildData()) return;
                if (!PickPrinterAndApply()) return;
                _isPreview = true;               // 預覽：啟用分頁邏輯
                _preview.ShowDialog(this);
            };

            btnPrint.Click += (_, __) =>
            {
                if (!BuildData()) return;
                if (!PickPrinterAndApply()) return;
                _isPreview = false;              // 列印：關閉分頁，連續紙一直印
                _doc.Print();
            };

        }

        private bool PickPrinterAndApply()
        {
            if (_printDlg.ShowDialog(this) != DialogResult.OK)
                return false;

            _doc.PrinterSettings = _printDlg.PrinterSettings;  // 用選到的那台
            ForceHalfContinuousPaper();                        // 先套 9.5" 固定寬（高度稍後會被動態覆蓋）
            return true;
        }

        // 固定 9.5 吋寬（高度之後用 SetDynamicLongPaper 取代）
        private void ForceHalfContinuousPaper()
        {
            const int W = 950, H = 550;
            var names = new[] { "9.5x5.5", "9.5 x 5.5", "241x140", "Half", "Half Form" };
            PaperSize ps = FindPaperSize(_doc.PrinterSettings, W, H, names);
            if (ps == null)
            {
                try { ps = new PaperSize("9.5x5.5", W, H); } catch { }
            }
            if (ps != null)
                _doc.DefaultPageSettings.PaperSize = ps;
        }

        private static PaperSize FindPaperSize(PrinterSettings s, int w, int h, IEnumerable<string> names)
        {
            foreach (PaperSize p in s.PaperSizes)
            {
                bool nameMatch = names.Any(n => p.PaperName.Equals(n, StringComparison.OrdinalIgnoreCase));
                if (nameMatch || (p.Width == w && p.Height == h))
                    return p;
            }
            return null;
        }

        private bool BuildData()
        {
            if (cboCustomer.SelectedItem is not Customer cust)
            {
                MessageBox.Show("請先選擇客戶。"); return false;
            }

            _cust = cust;
            _month = new DateTime(dtMonth.Value.Year, dtMonth.Value.Month, 1);
            var next = _month.AddMonths(1);

            // 如果 Order.CustomerId 是 string：o.CustomerId == cust.Id.ToString()
            var orders = DataStore.Orders
                .Where(o => o.CustomerId == cust.Id && o.CreatedAt >= _month && o.CreatedAt < next)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            _rows = orders
    .SelectMany(
        o => o.Lines,                          // collectionSelector：每張訂單的明細
        (o, l) => new Row                      // resultSelector：把 o(訂單) 與 l(明細) 投影成 Row
        {
            Date = o.CreatedAt,
            CodeOrLabor = string.IsNullOrWhiteSpace(l.Code) ? l.Name : l.Code,
            Spec = (l.Width > 0 || l.Length > 0) ? $"{l.Width:0.##}*{l.Length:0.##}" : "",
            Qty = l.Quantity,
            Unit = l.Unit,
            UnitPrice = l.UnitPrice,
            Subtotal = l.Subtotal,
            Wage = l.Wage,
            Note = l.Note
        })
    .OrderBy(r => r.Date)
    .ToList();


            if (_rows.Count == 0)
            {
                MessageBox.Show("本月無資料。"); return false;
            }

            _sumSubtotal = Math.Round(_rows.Sum(r => r.Subtotal), 2);
            _sumWage = Math.Round(_rows.Sum(r => r.Wage), 2);
            _printIndex = 0;
            _printedHeader = false;
            _lastDatePrinted = null;
            _doc.DocumentName = $"月結請款單_{cust.Name}_{_month:yyyyMM}";
            return true;
        }

        // ROC年月：114/06
        private static string ToRocYM(DateTime dt)
        {
            int roc = dt.Year - 1911;
            if (roc < 1) roc = 1;
            return $"{roc}/{dt:MM}";
        }

        // 列印頁面（連續紙，一頁到底）
        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;

            // 從實體紙張的 (0,0) 開始（消除各機型硬邊界）
            //g.TranslateTransform(-e.PageSettings.HardMarginX, -e.PageSettings.HardMarginY);

            // 不用 MarginBounds，自己給一點很小的安全邊
            const float PAD_L = 75f, PAD_R = 12f, PAD_T = 12f, PAD_B = 20f;
            var page = e.PageBounds;
            float left = page.Left + PAD_L;
            float top = page.Top + PAD_T;
            float right = page.Right - PAD_R;
            float bottom = page.Bottom - PAD_B;
            float width = right - left;

            using var fTitle = new Font("PMingLiU", 18, FontStyle.Bold);
            using var fHead1 = new Font("PMingLiU", 10, FontStyle.Regular);
            using var fHeadBold = new Font("PMingLiU", 10, FontStyle.Bold);
            using var fText = new Font("PMingLiU", 10, FontStyle.Regular);

            // 欄寬（品名規格縮一些，留空給備註）
            float cDate = 70, cCode = 120, cSpec = 100, cQty = 60, cUnit = 40, cUP = 80, cSub = 100, cWage = 100;
            float cNote = width - (cDate + cCode + cSpec + cQty + cUnit + cUP + cSub + cWage) - 4;

            float y = top;

            // ===== 只在第一頁印抬頭 + 欄名 =====
            if (!_printedHeader)
            {
                string title = "拓丞實業有限公司－應收帳款請款單";
                var szTitle = g.MeasureString(title, fTitle);
                g.DrawString(title, fTitle, Brushes.Black, (left + (width - szTitle.Width) / 2)-30, y);
                y += szTitle.Height + 6;

                g.DrawString("台中市北屯區大連路一段26號", fHead1, Brushes.Black, left + 290, y); y += 18;
                g.DrawString("TEL：04-22950721   FAX：04-22950723", fHead1, Brushes.Black, left + 260, y); y += 20;

                g.DrawString($"若帳月份：{ToRocYM(_month)}", fHead1, Brushes.Black, left, y); y += 18;
                g.DrawString($"客戶名稱：{_cust?.Name ?? ""}", fHead1, Brushes.Black, left, y); y += 18;
                g.DrawString("公司地址：", fHead1, Brushes.Black, left, y); y += 18;

                // 欄名
                g.DrawLine(Pens.Black, left, y, left + width-75, y); y += 6;
                float cx = left;
                g.DrawString("日期", fHead1, Brushes.Black, cx, y); cx += cDate;
                g.DrawString("產品編號", fHead1, Brushes.Black, cx, y); cx += cCode;
                g.DrawString("品名規格", fHead1, Brushes.Black, cx, y); cx += cSpec;
                g.DrawString("數量", fHead1, Brushes.Black, cx, y); cx += cQty;
                g.DrawString("單位", fHead1, Brushes.Black, cx, y); cx += cUnit;
                g.DrawString("單價", fHead1, Brushes.Black, cx, y); cx += cUP;
                g.DrawString("小計", fHead1, Brushes.Black, cx, y); cx += cSub;
                g.DrawString("工資", fHead1, Brushes.Black, cx, y); cx += cWage;
                g.DrawString("備註", fHead1, Brushes.Black, cx, y);
                y += 18;
                g.DrawLine(Pens.Black, left, y, left + width-75, y); y += 4;

                _printedHeader = true;
            }

            // ===== 明細列（不預留任何頁尾空間，填滿即可）=====
            float rowH = 18f;
            while (_printIndex < _rows.Count && (y + rowH) <= bottom)
            {
                var r = _rows[_printIndex];

                // 同一天只印一次日期
                string dateText = (_lastDatePrinted == null || r.Date.Date != _lastDatePrinted.Value)
                                  ? r.Date.ToString("yyyy/M/d") : "";
                _lastDatePrinted = r.Date.Date;

                float cx = left;
                g.DrawString(dateText, fText, Brushes.Black, cx, y); cx += cDate;
                g.DrawString(r.CodeOrLabor ?? "", fText, Brushes.Black, cx, y); cx += cCode;
                g.DrawString(r.Spec ?? "", fText, Brushes.Black, cx, y); cx += cSpec;
                g.DrawString(r.Qty == 0 ? "" : r.Qty.ToString("0.##"), fText, Brushes.Black, cx, y); cx += cQty;
                g.DrawString(r.Unit ?? "", fText, Brushes.Black, cx, y); cx += cUnit;
                g.DrawString(r.UnitPrice == 0 ? "" : r.UnitPrice.ToString("0.##"), fText, Brushes.Black, cx, y); cx += cUP;
                g.DrawString(r.Subtotal == 0 ? "" : r.Subtotal.ToString("0.##"), fText, Brushes.Black, cx, y); cx += cSub;
                g.DrawString(r.Wage == 0 ? "" : r.Wage.ToString("0.##"), fText, Brushes.Black, cx, y); cx += cWage;

                // 備註簡截
                string note = r.Note ?? "";
                if (note.Length > 0)
                {
                    int maxChars = Math.Max(1, (int)(cNote / 10));
                    if (note.Length > maxChars) note = note.Substring(0, maxChars - 1) + "…";
                }
                g.DrawString(note, fText, Brushes.Black, cx, y);

                y += rowH;
                _printIndex++;
            }

            // ===== 是否已經印完所有明細？=====
            bool allRowsPrinted = (_printIndex >= _rows.Count);

            // ===== 只有在「所有明細都印完」的那一頁，才畫總計＋注意事項 =====
            if (allRowsPrinted)
            {
                // 總計列
                y += 6f;
                g.DrawLine(Pens.Black, left, y, left + width - 75, y);
                y += 4f;

                float xSubtotalCol = left + cDate + cCode + cSpec + cQty + cUnit + cUP;
                float xWageCol = xSubtotalCol + cSub;

                g.DrawString("總  計：", fHeadBold, Brushes.Black, left + 5, y);
                g.DrawString(_sumSubtotal.ToString("#,0.##"), fHeadBold, Brushes.Black, xSubtotalCol, y);
                g.DrawString(_sumWage.ToString("#,0.##"), fHeadBold, Brushes.Black, xWageCol, y);
                y += 22f;

                // 左下三行
                g.DrawString($"本期應收：{_sumSubtotal:#,0.##}", fHead1, Brushes.Black, left, y); y += 18f;
                g.DrawString($"工資合計：{_sumWage:#,0.##}", fHead1, Brushes.Black, left, y); y += 18f;
                g.DrawString($"應收合計：{(_sumSubtotal + _sumWage):#,0.##}", fHeadBold, Brushes.Black, left, y); y += 24f;

                // 注意事項（放在右側）
                string[] notes =
                {
            "※現金折扣為3%，勿尾折",
            "※貨款未滿3,000元請勿折扣，並請惠付現金(票)",
            "※支票抬頭請開 \"拓丞實業有限公司\"",
            "※開立期票尾數請勿折扣",
            "※工資、稅額請勿折扣",
            "※車工將於114/8/1調漲"
        };

                float noteX = left + 360f;
                foreach (var line in notes)
                {
                    if (y + 16f > bottom) break; // 不擠；真的不夠就讓驅動再開一頁
                    g.DrawString(line, fHead1, Brushes.Black, noteX, y);
                    y += 18f;
                }
            }

            // 如果還有未印完的明細或（最後一頁）注意事項沒塞進去，就請求另一頁；
            // 否則結束列印。
            e.HasMorePages = !_printIndex.Equals(_rows.Count) || (y + 10f > bottom ? true : false);
            // 註：這裡的判斷很保守——最後一段注意事項如果被截到，印表機會再喚起一次 PrintPage，
            // 我們不會重畫抬頭（_printedHeader 已為 true），僅把剩餘內容補完。
        }


    }
}
