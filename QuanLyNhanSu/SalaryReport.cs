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
    public partial class SalaryReport : Form
    {
        public DataTable SalaryData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public SalaryReport()
        {
            InitializeComponent();
        }

        private void SalaryReport_Load(object sender, EventArgs e)
        {
            if (SalaryData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("SalaryDataSet", SalaryData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerSalary.LocalReport.DataSources.Clear();
                this.reportViewerSalary.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerSalary.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerSalary.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\SalaryReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerSalary.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerSalary.ZoomMode = ZoomMode.Percent;
                this.reportViewerSalary.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerSalary.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
