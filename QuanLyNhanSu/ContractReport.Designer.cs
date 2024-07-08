namespace QuanLyNhanSu
{
    partial class ContractReport
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
            this.reportViewerContract = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerContract
            // 
            this.reportViewerContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerContract.Location = new System.Drawing.Point(0, 0);
            this.reportViewerContract.Name = "reportViewerContract";
            this.reportViewerContract.ServerReport.BearerToken = null;
            this.reportViewerContract.Size = new System.Drawing.Size(800, 450);
            this.reportViewerContract.TabIndex = 0;
            this.reportViewerContract.Load += new System.EventHandler(this.reportViewerContract_Load);
            // 
            // ContractReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewerContract);
            this.Name = "ContractReport";
            this.Text = "Danh sách hợp đồng";
            this.Load += new System.EventHandler(this.ContractReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerContract;
    }
}