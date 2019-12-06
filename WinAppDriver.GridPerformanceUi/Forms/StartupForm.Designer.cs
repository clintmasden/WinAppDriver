namespace WinAppDriver.GridPerformanceUi
{
    partial class StartupForm
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
            this.PerformanceDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.PerformanceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PerformanceDataGridView
            // 
            this.PerformanceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PerformanceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PerformanceDataGridView.Location = new System.Drawing.Point(0, 0);
            this.PerformanceDataGridView.Name = "PerformanceDataGridView";
            this.PerformanceDataGridView.RowHeadersWidth = 51;
            this.PerformanceDataGridView.RowTemplate.Height = 24;
            this.PerformanceDataGridView.Size = new System.Drawing.Size(882, 453);
            this.PerformanceDataGridView.TabIndex = 0;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 453);
            this.Controls.Add(this.PerformanceDataGridView);
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup Form - WinAppDriver - Performance";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PerformanceDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PerformanceDataGridView;
    }
}

