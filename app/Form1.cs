using System;
using System.Windows.Forms;

namespace ProductCalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //�]�i��^�����~�[
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
        }

        private void btnQuickCalc_Click(object sender, EventArgs e)
        {
            using (var f = new QuickCalcForm())
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);   // �� ShowDialog ���l�����b�e���A������^��ʭ�
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

        private void ���D_Click(object sender, EventArgs e)
        {

        }

        private void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            using var f = new MonthlyReportForm { StartPosition = FormStartPosition.CenterParent };
            f.ShowDialog(this);
        }

    }
}
