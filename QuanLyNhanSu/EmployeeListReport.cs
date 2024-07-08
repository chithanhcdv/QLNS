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
    public partial class EmployeeListReport : Form
    {
        public DataTable EmployeeListData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public EmployeeListReport()
        {
            InitializeComponent();
        }

        private void EmployeeListReport_Load(object sender, EventArgs e)
        {
            if (EmployeeListData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("EmployeeListDataSet", EmployeeListData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerEmployeeList.LocalReport.DataSources.Clear();
                this.reportViewerEmployeeList.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerEmployeeList.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerEmployeeList.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\EmployeeListReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerEmployeeList.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerEmployeeList.ZoomMode = ZoomMode.Percent;
                this.reportViewerEmployeeList.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerEmployeeList.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }      
    }
}
