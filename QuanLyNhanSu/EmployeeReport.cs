using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QuanLyNhanSu
{
    public partial class Report : Form
    {
        public DataTable EmployeeInformationData { get; set; }
        public DataTable UnitUsedData { get; set; }

        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            if (EmployeeInformationData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("EmployeeInformationDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UniversityDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource3 = new ReportDataSource("AfterUniversityDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource4 = new ReportDataSource("ForeignLanguageDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource5 = new ReportDataSource("WorkingProcessDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource6 = new ReportDataSource("ScientificWorksDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource7 = new ReportDataSource("ScientificResearchTopicsDataSet", EmployeeInformationData);
                ReportDataSource reportDataSource8 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);
                this.reportViewerEmployee.LocalReport.DataSources.Clear();
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource3);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource4);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource5);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource6);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource7);
                this.reportViewerEmployee.LocalReport.DataSources.Add(reportDataSource8);
                this.reportViewerEmployee.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\EmployeeReport.rdlc"; // Đường dẫn tới file .rdlc của bạn

                this.reportViewerEmployee.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerEmployee.ZoomMode = ZoomMode.Percent;
                this.reportViewerEmployee.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerEmployee.RefreshReport();
            }
            else 
            {
                MessageBox.Show("No data to display.");
            }
        }
    }
}
