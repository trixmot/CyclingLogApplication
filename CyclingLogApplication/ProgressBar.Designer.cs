namespace CyclingLogApplication
{
    partial class ProgressBar
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Percent = new System.Windows.Forms.Label();
            this.PBar = new System.Windows.Forms.ProgressBar();
            this.Load = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Percent
            // 
            this.Percent.AutoSize = true;
            this.Percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Percent.Location = new System.Drawing.Point(361, 99);
            this.Percent.Name = "Percent";
            this.Percent.Size = new System.Drawing.Size(18, 20);
            this.Percent.TabIndex = 0;
            this.Percent.Text = "0";
            // 
            // PBar
            // 
            this.PBar.Location = new System.Drawing.Point(33, 96);
            this.PBar.Name = "PBar";
            this.PBar.Size = new System.Drawing.Size(322, 23);
            this.PBar.TabIndex = 1;
            // 
            // Load
            // 
            this.Load.AutoSize = true;
            this.Load.BackColor = System.Drawing.SystemColors.Desktop;
            this.Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Load.Location = new System.Drawing.Point(29, 61);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(78, 20);
            this.Load.TabIndex = 2;
            this.Load.Text = "Loading...";
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(405, 166);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.PBar);
            this.Controls.Add(this.Percent);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProgressBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please wait while the Chart is loading";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label Percent;
        private System.Windows.Forms.ProgressBar PBar;
        private System.Windows.Forms.Label Load;
    }
}