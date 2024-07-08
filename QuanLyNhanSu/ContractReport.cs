using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhanSu
{
    public partial class ContractReport : Form
    {
        public DataTable ContractData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public ContractReport()
        {
            InitializeComponent();
        }

        private void ContractReport_Load(object sender, EventArgs e)
        {
            if (ContractData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("ContractDataSet", ContractData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerContract.LocalReport.DataSources.Clear();
                this.reportViewerContract.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerContract.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerContract.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\ContractReport.rdlc";

                this.reportViewerContract.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerContract.ZoomMode = ZoomMode.Percent;
                this.reportViewerContract.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerContract.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }

        private void reportViewerContract_Load(object sender, EventArgs e)
        {

        }
    }
}
