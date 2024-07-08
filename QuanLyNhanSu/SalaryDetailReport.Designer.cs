namespace QuanLyNhanSu
{
    partial class SalaryDetailReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewerSalaryDetail = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerSalaryDetail
            // 
            this.reportViewerSalaryDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerSalaryDetail.Location = new System.Drawing.Point(0, 0);
            this.reportViewerSalaryDetail.Name = "reportViewerSalaryDetail";
            this.reportViewerSalaryDetail.ServerReport.BearerToken = null;
            this.reportViewerSalaryDetail.Size = new System.Drawing.Size(800, 450);
            this.reportViewerSalaryDetail.TabIndex = 0;
            // 
            // SalaryDetailReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerSalaryDetail);
            this.Name = "SalaryDetailReport";
            this.Text = "Danh sách chi tiết lương";
            this.Load += new System.EventHandler(this.SalaryDetailReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerSalaryDetail;
    }
}