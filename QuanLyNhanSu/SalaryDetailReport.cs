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
    public partial class SalaryDetailReport : Form
    {
        public DataTable SalaryDetailData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public SalaryDetailReport()
        {
            InitializeComponent();
        }

        private void SalaryDetailReport_Load(object sender, EventArgs e)
        {
            if (SalaryDetailData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("SalaryDetailDataSet", SalaryDetailData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerSalaryDetail.LocalReport.DataSources.Clear();
                this.reportViewerSalaryDetail.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerSalaryDetail.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerSalaryDetail.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\SalaryDetailReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerSalaryDetail.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerSalaryDetail.ZoomMode = ZoomMode.Percent;
                this.reportViewerSalaryDetail.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerSalaryDetail.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
