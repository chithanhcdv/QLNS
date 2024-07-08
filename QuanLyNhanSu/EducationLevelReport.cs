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
    public partial class EducationLevelReport : Form
    {
        public DataTable EducationLevelData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public EducationLevelReport()
        {
            InitializeComponent();
        }

        private void EducationLevelReport_Load(object sender, EventArgs e)
        {
            if (EducationLevelData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("EducationLevelDataSet", EducationLevelData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerEducationLevel.LocalReport.DataSources.Clear();
                this.reportViewerEducationLevel.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerEducationLevel.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerEducationLevel.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\EducationLevelReport.rdlc";

                this.reportViewerEducationLevel.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerEducationLevel.ZoomMode = ZoomMode.Percent;
                this.reportViewerEducationLevel.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerEducationLevel.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }

        private void reportViewerEducationLevel_Load(object sender, EventArgs e)
        {

        }
    }
}
