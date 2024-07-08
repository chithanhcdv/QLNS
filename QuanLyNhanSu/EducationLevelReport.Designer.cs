namespace QuanLyNhanSu
{
    partial class EducationLevelReport
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
            this.reportViewerEducationLevel = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerEducationLevel
            // 
            this.reportViewerEducationLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerEducationLevel.Location = new System.Drawing.Point(0, 0);
            this.reportViewerEducationLevel.Name = "reportViewerEducationLevel";
            this.reportViewerEducationLevel.ServerReport.BearerToken = null;
            this.reportViewerEducationLevel.Size = new System.Drawing.Size(800, 450);
            this.reportViewerEducationLevel.TabIndex = 0;
            this.reportViewerEducationLevel.Load += new System.EventHandler(this.reportViewerEducationLevel_Load);
            // 
            // EducationLevelReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerEducationLevel);
            this.Name = "EducationLevelReport";
            this.Text = "Danh sách trình độ học vấn";
            this.Load += new System.EventHandler(this.EducationLevelReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerEducationLevel;
    }
}