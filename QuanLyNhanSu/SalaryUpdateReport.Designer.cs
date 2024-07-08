namespace QuanLyNhanSu
{
    partial class SalaryUpdateReport
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
            this.reportViewerSalaryUpdate = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerSalaryUpdate
            // 
            this.reportViewerSalaryUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerSalaryUpdate.Location = new System.Drawing.Point(0, 0);
            this.reportViewerSalaryUpdate.Name = "reportViewerSalaryUpdate";
            this.reportViewerSalaryUpdate.ServerReport.BearerToken = null;
            this.reportViewerSalaryUpdate.Size = new System.Drawing.Size(800, 450);
            this.reportViewerSalaryUpdate.TabIndex = 0;
            // 
            // SalaryUpdateReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerSalaryUpdate);
            this.Name = "SalaryUpdateReport";
            this.Text = "Danh sách cập nhật lương";
            this.Load += new System.EventHandler(this.SalaryUpdateReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerSalaryUpdate;
    }
}