namespace QuanLyNhanSu
{
    partial class EmployeeListReport
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
            this.reportViewerEmployeeList = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerEmployeeList
            // 
            this.reportViewerEmployeeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerEmployeeList.Location = new System.Drawing.Point(0, 0);
            this.reportViewerEmployeeList.Name = "reportViewerEmployeeList";
            this.reportViewerEmployeeList.ServerReport.BearerToken = null;
            this.reportViewerEmployeeList.Size = new System.Drawing.Size(800, 450);
            this.reportViewerEmployeeList.TabIndex = 0;
            // 
            // EmployeeListReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerEmployeeList);
            this.Name = "EmployeeListReport";
            this.Text = "Danh sách nhân sự";
            this.Load += new System.EventHandler(this.EmployeeListReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerEmployeeList;
    }
}