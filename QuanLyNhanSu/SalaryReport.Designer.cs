namespace QuanLyNhanSu
{
    partial class SalaryReport
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
            this.reportViewerSalary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerSalary
            // 
            this.reportViewerSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerSalary.Location = new System.Drawing.Point(0, 0);
            this.reportViewerSalary.Name = "reportViewerSalary";
            this.reportViewerSalary.ServerReport.BearerToken = null;
            this.reportViewerSalary.Size = new System.Drawing.Size(800, 450);
            this.reportViewerSalary.TabIndex = 0;
            // 
            // SalaryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerSalary);
            this.Name = "SalaryReport";
            this.Text = "Danh sách lương";
            this.Load += new System.EventHandler(this.SalaryReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerSalary;
    }
}