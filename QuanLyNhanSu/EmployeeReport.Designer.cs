﻿namespace QuanLyNhanSu
{
    partial class Report
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
            this.reportViewerEmployee = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerEmployee
            // 
            this.reportViewerEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerEmployee.LocalReport.DisplayName = "EmployeeReport.rdlc";
            this.reportViewerEmployee.LocalReport.ReportEmbeddedResource = "QuanLyNhanSu.ReportEmployee.rdlc";
            this.reportViewerEmployee.Location = new System.Drawing.Point(0, 0);
            this.reportViewerEmployee.Name = "reportViewerEmployee";
            this.reportViewerEmployee.ServerReport.BearerToken = null;
            this.reportViewerEmployee.Size = new System.Drawing.Size(1282, 585);
            this.reportViewerEmployee.TabIndex = 0;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 585);
            this.Controls.Add(this.reportViewerEmployee);
            this.Name = "Report";
            this.Text = "Lý lịch khoa học";
            this.Load += new System.EventHandler(this.Report_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerEmployee;
    }
}