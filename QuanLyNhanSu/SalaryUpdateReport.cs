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
    public partial class SalaryUpdateReport : Form
    {
        public DataTable SalaryUpdateData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public SalaryUpdateReport()
        {
            InitializeComponent();
        }

        private void SalaryUpdateReport_Load(object sender, EventArgs e)
        {
            if (SalaryUpdateData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("SalaryUpdateDataSet", SalaryUpdateData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerSalaryUpdate.LocalReport.DataSources.Clear();
                this.reportViewerSalaryUpdate.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerSalaryUpdate.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerSalaryUpdate.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\SalaryUpdateReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerSalaryUpdate.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerSalaryUpdate.ZoomMode = ZoomMode.Percent;
                this.reportViewerSalaryUpdate.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerSalaryUpdate.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
