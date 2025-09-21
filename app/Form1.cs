using System;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //（可選）視窗外觀
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void btnQuickCalc_Click(object sender, EventArgs e)
        {
            using (var f = new QuickCalcForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);   // 用 ShowDialog 讓子視窗在前面，關閉後回到封面
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            using (var f = new ProductManagerForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (var f = new HistoryForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            using var f = new CustomerManagerForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void 標題_Click(object sender, EventArgs e)
        {

        }

        private void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            using var f = new MonthlyReportForm { StartPosition = FormStartPosition.CenterParent };
            f.ShowDialog(this);
        }

    }
}
