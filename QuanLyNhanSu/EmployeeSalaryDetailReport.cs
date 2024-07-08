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
    public partial class EmployeeSalaryDetailReport : Form
    {
        public DataTable EmployeeSalaryDetailData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public EmployeeSalaryDetailReport()
        {
            InitializeComponent();
        }

        private void EmployeeSalaryDetailReport_Load(object sender, EventArgs e)
        {
            if (EmployeeSalaryDetailData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("EmployeeSalaryDetailDataSet", EmployeeSalaryDetailData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerEmployeeSalaryDetail.LocalReport.DataSources.Clear();
                this.reportViewerEmployeeSalaryDetail.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerEmployeeSalaryDetail.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerEmployeeSalaryDetail.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\EmployeeSalaryDetailReport.rdlc";
                this.reportViewerEmployeeSalaryDetail.RefreshReport();

                this.reportViewerEmployeeSalaryDetail.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerEmployeeSalaryDetail.ZoomMode = ZoomMode.Percent;
                this.reportViewerEmployeeSalaryDetail.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerEmployeeSalaryDetail.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
