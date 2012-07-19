namespace OrderStatus
{
    partial class OrderStatusService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Logs = new System.Diagnostics.EventLog();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.Logs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // Logs
            // 
            this.Logs.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.Logs_EntryWritten);
            // 
            // OrderStatusService
            // 
            this.ServiceName = "OrderStatusService";
            ((System.ComponentModel.ISupportInitialize)(this.Logs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog Logs;
        private System.Diagnostics.EventLog eventLog1;
    }
}
