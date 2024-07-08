namespace QuanLyNhanSu
{
    partial class SpecializedReport
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
            this.reportViewerSpecialized = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerSpecialized
            // 
            this.reportViewerSpecialized.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerSpecialized.Location = new System.Drawing.Point(0, 0);
            this.reportViewerSpecialized.Name = "reportViewerSpecialized";
            this.reportViewerSpecialized.ServerReport.BearerToken = null;
            this.reportViewerSpecialized.Size = new System.Drawing.Size(800, 450);
            this.reportViewerSpecialized.TabIndex = 0;
            // 
            // SpecializedReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerSpecialized);
            this.Name = "SpecializedReport";
            this.Text = "Danh sách chuyên ngành";
            this.Load += new System.EventHandler(this.SpecializedReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerSpecialized;
    }
}