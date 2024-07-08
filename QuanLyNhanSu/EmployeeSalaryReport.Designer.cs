namespace QuanLyNhanSu
{
    partial class EmployeeSalaryReport
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
            this.reportViewerEmployeeSalary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerEmployeeSalary
            // 
            this.reportViewerEmployeeSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerEmployeeSalary.Location = new System.Drawing.Point(0, 0);
            this.reportViewerEmployeeSalary.Name = "reportViewerEmployeeSalary";
            this.reportViewerEmployeeSalary.ServerReport.BearerToken = null;
            this.reportViewerEmployeeSalary.Size = new System.Drawing.Size(800, 450);
            this.reportViewerEmployeeSalary.TabIndex = 0;
            // 
            // EmployeeSalaryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerEmployeeSalary);
            this.Name = "EmployeeSalaryReport";
            this.Text = "Thông tin lương cơ bản";
            this.Load += new System.EventHandler(this.EmployeeSalaryReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerEmployeeSalary;
    }
}