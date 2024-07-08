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
    public partial class SpecializedReport : Form
    {
        public DataTable SpecializedData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public SpecializedReport()
        {
            InitializeComponent();
        }

        private void SpecializedReport_Load(object sender, EventArgs e)
        {
            if (SpecializedData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("SpecializedDataSet", SpecializedData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerSpecialized.LocalReport.DataSources.Clear();
                this.reportViewerSpecialized.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerSpecialized.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerSpecialized.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\SpecializedReport.rdlc";

                this.reportViewerSpecialized.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerSpecialized.ZoomMode = ZoomMode.Percent;
                this.reportViewerSpecialized.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerSpecialized.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }       
    }
}
