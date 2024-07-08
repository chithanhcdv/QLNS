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
    public partial class EmployeeSalaryReport : Form
    {
        public DataTable EmployeeSalaryData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public EmployeeSalaryReport()
        {
            InitializeComponent();
        }

        private void EmployeeSalaryReport_Load(object sender, EventArgs e)
        {
            if (EmployeeSalaryData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("EmployeeSalaryDataSet", EmployeeSalaryData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerEmployeeSalary.LocalReport.DataSources.Clear();
                this.reportViewerEmployeeSalary.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerEmployeeSalary.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerEmployeeSalary.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\EmployeeSalaryReport.rdlc";

                this.reportViewerEmployeeSalary.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerEmployeeSalary.ZoomMode = ZoomMode.Percent;
                this.reportViewerEmployeeSalary.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerEmployeeSalary.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
