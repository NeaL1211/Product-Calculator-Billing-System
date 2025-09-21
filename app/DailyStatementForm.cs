using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class DailyStatementForm : Form
    {
        private ComboBox cboCustomer;
        private DateTimePicker dtDate;
        private Button btnPreview, btnPrint;

        private List<Row> _rows = new();
        private int _printIndex = 0;
        private Customer _cust;
        private DateTime _date;
        private double _sumSubtotal, _sumWage;

        private readonly PrintDocument _doc = new PrintDocument();
        private readonly PrintPreviewDialog _preview = new PrintPreviewDialog();
        private readonly PrintDialog _printDlg = new PrintDialog();

        private bool _isFirstPage = true;
        private DateTime? _lastDatePrinted = null;

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

        public DailyStatementForm()
        {
            Text = "日請款單";
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

            var lblD = new Label { Left = cboCustomer.Right + 16, Top = lblCustomer.Top, Text = "日期：", AutoSize = true };
            Controls.Add(lblD);

            dtDate = new DateTimePicker
            {
                Left = lblD.Right + 8,
                Top = lblCustomer.Top - 3,
                Width = 130,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy/MM/dd",
            };
            Controls.Add(dtDate);

            btnPreview = new Button { Text = "列印預覽", AutoSize = true };
            btnPreview.Left = dtDate.Right + 16;
            btnPreview.Top = lblCustomer.Top - 6;

            btnPrint = new Button { Text = "直接列印", AutoSize = true };
            btnPrint.Left = btnPreview.Right + 10;
            btnPrint.Top = btnPreview.Top;

            Controls.AddRange(new Control[] { btnPreview, btnPrint });

            var list = DataStore.Customers.Where(c => c.Active).OrderBy(c => c.Name).ToList();
            cboCustomer.DisplayMember = "Name"; cboCustomer.ValueMember = "Id";
            cboCustomer.DataSource = new BindingList<Customer>(list);
            if (list.Count > 0) cboCustomer.SelectedIndex = 0;

            // 基本 PrintDocument 設定（先設 margin/Origin）
            _doc.OriginAtMargins = false;
            _doc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            _doc.PrintPage += Doc_PrintPage;

            _preview.Document = _doc;
            _preview.UseAntiAlias = true;

            _printDlg.UseEXDialog = true;
            _printDlg.Document = _doc;

            // 先產生資料 → 讓使用者選印表機 → 再套「兩切一刀」紙張 → 預覽/列印
            // ★ 預覽：先選印表機（不列印），再預覽
            btnPreview.Click += (_, __) =>
            {
                if (!BuildData()) return;
                if (!PickPrinterAndApply()) return;  // 這一步確保預覽用的是目標印表機的硬邊界
                _preview.ShowDialog(this);
            };

            // 直接列印：維持原本流程
            btnPrint.Click += (_, __) =>
            {
                if (!BuildData()) return;
                if (!PickPrinterAndApply()) return;
                _doc.Print();
            };
        }

        private bool PickPrinterAndApply()
        {
            if (_printDlg.ShowDialog(this) != DialogResult.OK)
                return false;

            _doc.PrinterSettings = _printDlg.PrinterSettings;  // 用選到的那台
            ForceHalfContinuousPaper();                        // 針對該台套 9.5x5.5
            return true;
        }

        private void ForceHalfContinuousPaper()
        {
            const int W = 950;  // 9.5"
            const int H = 550;  // 5.5"
            var names = new[] { "9.5x5.5", "9.5 x 5.5", "241x140", "Half", "Half Form" };
            PaperSize ps = FindPaperSize(_doc.PrinterSettings, W, H, names);
            if (ps == null)
            {
                try { ps = new PaperSize("9.5x5.5", W, H); } catch { /* 某些驅動不允許程式自建表單 */ }
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

        private static string ToRocYMD(DateTime dt)
        {
            int roc = dt.Year - 1911;
            if (roc < 1) roc = 1;
            return $"{roc}/{dt:MM/dd}";
        }

        private bool BuildData()
        {
            if (cboCustomer.SelectedItem is not Customer cust)
            {
                MessageBox.Show("請先選擇客戶。");
                return false;
            }

            _cust = cust;
            _date = dtDate.Value.Date;
            var next = _date.AddDays(1);
            string custId = cust.Id.ToString();

            var orders = DataStore.Orders
                .Where(o => string.Equals(o.CustomerId, custId, StringComparison.OrdinalIgnoreCase)
                         && o.CreatedAt >= _date
                         && o.CreatedAt < next)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            if (orders.Count == 0)
            {
                MessageBox.Show("當天無資料。");
                return false;
            }

            _rows = orders
                .SelectMany(o => o.Lines.Select(l => new Row
                {
                    Date = o.CreatedAt,
                    CodeOrLabor = string.IsNullOrWhiteSpace(l.Code) ? l.Name : l.Code,
                    Spec = ((l.Width > 0 || l.Length > 0) ? $"{l.Width:0.##}*{l.Length:0.##}" : ""),
                    Qty = l.Quantity,
                    Unit = l.Unit,
                    UnitPrice = l.UnitPrice,
                    Subtotal = l.Subtotal,
                    Wage = l.Wage,
                    Note = l.Note
                }))
                .OrderBy(r => r.Date)
                .ToList();

            _sumSubtotal = Math.Round(_rows.Sum(r => r.Subtotal), 2);
            _sumWage = Math.Round(_rows.Sum(r => r.Wage), 2);
            _printIndex = 0;

            _isFirstPage = true;
            _lastDatePrinted = null;

            _doc.DocumentName = $"日請款單_{cust.Name}_{_date:yyyyMMdd}";
            return true;
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;

            // 用 PageBounds + 自訂 PAD（與月單一致）
            const float PAD_L = 75f, PAD_R = 12f, PAD_T = 12f, PAD_B = 0f;
            var page = e.PageBounds;
            float left = page.Left + PAD_L;
            float top = page.Top + PAD_T;
            float right = page.Right - PAD_R;
            float bottom = page.Bottom - PAD_B;
            float width = right - left;

            using Font fTitle = new Font("PMingLiU", 18, FontStyle.Bold);
            using Font fHead1 = new Font("PMingLiU", 10);
            using Font fText = new Font("PMingLiU", 10);

            float y = top;

            void HLine(float yy, float thickness = 1.5f, float pad = 20f)
            {
                using var p = new Pen(Color.Black, thickness);
                g.DrawLine(p, left, yy, right - pad, yy);
            }

            // ===== 每頁都先畫抬頭 =====
            {
                string company = "拓丞實業有限公司";
                var szCompany = g.MeasureString(company, fTitle);
                g.DrawString(company, fTitle, Brushes.Black, left + (width - szCompany.Width) / 2f, y);
                y += szCompany.Height + 4f;

                using var fShip = new Font("PMingLiU", 14, FontStyle.Bold);
                string shipTitle = "出貨單";
                var szShip = g.MeasureString(shipTitle, fShip);
                float shipX = left + (width - szShip.Width) / 2f;
                float shipY = y;
                g.DrawString(shipTitle, fShip, Brushes.Black, shipX, shipY);
                using (var pen = new Pen(Color.Black, 2))
                {
                    float lineY = shipY + szShip.Height + 1;
                    float pad = 15;
                    g.DrawLine(pen, shipX - pad, lineY, shipX + szShip.Width + pad, lineY);
                }
                y += szShip.Height + 20f;

                float infoRowH = 20f;
                g.DrawString($"客戶名稱：{_cust?.Name ?? ""}", fHead1, Brushes.Black, left, y);
                g.DrawString("頁數：", fHead1, Brushes.Black, right - 260, y);
                y += infoRowH;

                g.DrawString($"訂貨日期：{ToRocYMD(_date)}", fHead1, Brushes.Black, left, y);
                g.DrawString("電話：04-22950721", fHead1, Brushes.Black, right - 260, y);
                y += infoRowH;

                g.DrawString("送貨地址：台中市北屯區大連路一段26號(大榮貨運-大屯站)", fHead1, Brushes.Black, left, y);
                g.DrawString("行動：0935098919", fHead1, Brushes.Black, right - 260, y);
                y += infoRowH + 10f;

                // 欄位表頭
                HLine(y); y += 6f;
                float cx = left;
                g.DrawString("產品編號", fHead1, Brushes.Black, cx, y); cx += 100;
                g.DrawString("品名規格", fHead1, Brushes.Black, cx, y); cx += 100;
                g.DrawString("數量", fHead1, Brushes.Black, cx, y); cx += 60;
                g.DrawString("單位", fHead1, Brushes.Black, cx, y); cx += 40;
                g.DrawString("單價", fHead1, Brushes.Black, cx, y); cx += 80;
                g.DrawString("小計", fHead1, Brushes.Black, cx, y); cx += 100;
                g.DrawString("工資", fHead1, Brushes.Black, cx, y); cx += 100;
                g.DrawString("備註", fHead1, Brushes.Black, cx, y);
                y += 18f;
                HLine(y); y += 4f;
            }

            // 欄位寬度
            float cCode = 100, cSpec = 100, cQty = 60, cUnit = 40, cUP = 80, cSub = 100, cWage = 100;
            float cNote = width - (cCode + cSpec + cQty + cUnit + cUP + cSub + cWage) - 4;

            // 頁尾（注意事項 + 簽名列）
            string notice =
                "產品若規格不符或品質不良，請勿裁剪並於3日內電話通知辦理退換，逾期恕不受理（訂貨品除外）敬請合作";

            float noticeWidth = right - left;
            SizeF noticeSize = g.MeasureString(notice, fHead1, new SizeF(noticeWidth, 1000f));
            float sigLineH = fHead1.GetHeight(g) + 2f;

            // ===== 在這裡增加保留高度 =====
            const float GAP_LINE_TO_NOTICE = 8f;   // 底線 → 注意事項
            const float GAP_NOTICE_TO_SIG = 10f;  // 注意事項 → 簽名列
            const float FOOTER_EXTRA = 70f;  // ★★ 你可以改大/改小，預設 70f

            float footerReserved =
                2f /*底線粗度近似*/ + GAP_LINE_TO_NOTICE + noticeSize.Height + GAP_NOTICE_TO_SIG + sigLineH + FOOTER_EXTRA;

            // 每列高度
            float rowH = 18f;

            // 本頁可以放的列數（一定要預留 footer）
            int capThisPage = Math.Max(0, (int)Math.Floor((bottom - y - footerReserved) / rowH));

            // 印明細
            int printed = 0;
            while (_printIndex < _rows.Count && printed < capThisPage)
            {
                var r = _rows[_printIndex];
                float cx = left;

                g.DrawString(r.CodeOrLabor ?? "", fText, Brushes.Black, cx, y); cx += cCode;
                g.DrawString(r.Spec ?? "", fText, Brushes.Black, cx, y); cx += cSpec;

                g.DrawString(r.Qty == 0 ? "" : r.Qty.ToString("0.##"), fText, Brushes.Black, cx, y); cx += cQty;
                g.DrawString(r.Unit ?? "", fText, Brushes.Black, cx, y); cx += cUnit;

                // 保留欄位寬，但不印數字
                cx += cUP;   // 單價
                cx += cSub;  // 小計
                cx += cWage; // 工資

                string note = r.Note ?? "";
                if (!string.IsNullOrEmpty(note))
                {
                    int maxChars = Math.Max(1, (int)(cNote / 10));
                    if (note.Length > maxChars) note = note[..(maxChars - 1)] + "…";
                }
                g.DrawString(note, fText, Brushes.Black, cx, y);

                y += rowH;
                _printIndex++;
                printed++;
            }

            // 畫頁尾（錨在頁底）
            {
                float footerLineY = bottom - (footerReserved - 2f /*粗度近似*/);
                HLine(footerLineY, thickness: 1.5f, pad: 20f);

                float noticeTop = footerLineY + GAP_LINE_TO_NOTICE;
                var noticeRect = new RectangleF(left, noticeTop, noticeWidth, noticeSize.Height);
                using (var fmt = new StringFormat(StringFormatFlags.LineLimit) { Trimming = StringTrimming.Word })
                    g.DrawString(notice, fHead1, Brushes.Black, noticeRect, fmt);

                float sigY = noticeRect.Bottom + GAP_NOTICE_TO_SIG;
                float sigX = left;
                void Sig(string label, float gapAfter)
                {
                    g.DrawString(label, fHead1, Brushes.Black, sigX, sigY);
                    sigX += g.MeasureString(label, fHead1).Width + gapAfter;
                }
                Sig("主管：", 100f);
                Sig("倉管：", 100f);
                Sig("會計：", 100f);
                Sig("製單人：", 100f);
                g.DrawString("簽收人：", fHead1, Brushes.Black, sigX, sigY);
            }

            e.HasMorePages = (_printIndex < _rows.Count);
        }




    }
}
