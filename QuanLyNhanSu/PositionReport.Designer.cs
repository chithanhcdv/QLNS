namespace QuanLyNhanSu
{
    partial class PositionReport
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
            this.reportViewerPosition = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerPosition
            // 
            this.reportViewerPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerPosition.Location = new System.Drawing.Point(0, 0);
            this.reportViewerPosition.Name = "reportViewerPosition";
            this.reportViewerPosition.ServerReport.BearerToken = null;
            this.reportViewerPosition.Size = new System.Drawing.Size(800, 450);
            this.reportViewerPosition.TabIndex = 0;
            // 
            // PositionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerPosition);
            this.Name = "PositionReport";
            this.Text = "Danh sách chức vụ";
            this.Load += new System.EventHandler(this.PositionReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerPosition;
    }
}