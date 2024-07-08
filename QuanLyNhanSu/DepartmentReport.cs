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
    public partial class DepartmentReport : Form
    {
        public DataTable DepartmentData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public DepartmentReport()
        {
            InitializeComponent();
        }

        private void DepartmentReport_Load(object sender, EventArgs e)
        {
            if (DepartmentData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("DepartmentDataSet", DepartmentData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerDepartment.LocalReport.DataSources.Clear();
                this.reportViewerDepartment.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerDepartment.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerDepartment.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\DepartmentReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerDepartment.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerDepartment.ZoomMode = ZoomMode.Percent;
                this.reportViewerDepartment.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerDepartment.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }         
        }

        private void reportViewerDepartment_Load(object sender, EventArgs e)
        {

        }
    }
}
