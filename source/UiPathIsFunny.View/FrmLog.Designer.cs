namespace UiPathIsFunny.View
{
    partial class FrmLog
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
            this.lsvStatus = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lsvStatus
            // 
            this.lsvStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvStatus.FullRowSelect = true;
            this.lsvStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvStatus.Location = new System.Drawing.Point(0, 0);
            this.lsvStatus.Name = "lsvStatus";
            this.lsvStatus.Size = new System.Drawing.Size(800, 450);
            this.lsvStatus.TabIndex = 32;
            this.lsvStatus.UseCompatibleStateImageBehavior = false;
            this.lsvStatus.View = System.Windows.Forms.View.Details;
            this.lsvStatus.Resize += new System.EventHandler(this.lsvStatus_Resize);
            // 
            // FrmLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lsvStatus);
            this.MinimizeBox = false;
            this.Name = "FrmLog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Logs";
            this.Load += new System.EventHandler(this.FrmLog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvStatus;
    }
}