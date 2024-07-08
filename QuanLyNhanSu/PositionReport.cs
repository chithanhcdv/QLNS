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
    public partial class PositionReport : Form
    {
        public DataTable PositionData { get; set; }
        public DataTable UnitUsedData { get; set; }
        public PositionReport()
        {
            InitializeComponent();
        }

        private void PositionReport_Load(object sender, EventArgs e)
        {
            if (PositionData != null && UnitUsedData != null)
            {
                ReportDataSource reportDataSource1 = new ReportDataSource("PositionDataSet", PositionData);
                ReportDataSource reportDataSource2 = new ReportDataSource("UnitUsedDataSet", UnitUsedData);

                this.reportViewerPosition.LocalReport.DataSources.Clear();
                this.reportViewerPosition.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewerPosition.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewerPosition.LocalReport.ReportPath = "C:\\Users\\ADMIN\\source\\repos\\QuanLyNhanSu\\QuanLyNhanSu\\PositionReport.rdlc";

                this.reportViewerPosition.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewerPosition.ZoomMode = ZoomMode.Percent;
                this.reportViewerPosition.ZoomPercent = 100;
                this.WindowState = FormWindowState.Maximized;
                this.reportViewerPosition.RefreshReport();
            }
            else
            {
                MessageBox.Show("No data to display.");
            }
        }       
    }
}
