namespace QuanLyNhanSu
{
    partial class DepartmentReport
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
            this.reportViewerDepartment = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerDepartment
            // 
            this.reportViewerDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerDepartment.Location = new System.Drawing.Point(0, 0);
            this.reportViewerDepartment.Name = "reportViewerDepartment";
            this.reportViewerDepartment.ServerReport.BearerToken = null;
            this.reportViewerDepartment.Size = new System.Drawing.Size(800, 450);
            this.reportViewerDepartment.TabIndex = 0;
            this.reportViewerDepartment.Load += new System.EventHandler(this.reportViewerDepartment_Load);
            // 
            // DepartmentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerDepartment);
            this.Name = "DepartmentReport";
            this.Text = "Danh sách phòng ban";
            this.Load += new System.EventHandler(this.DepartmentReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerDepartment;
    }
}