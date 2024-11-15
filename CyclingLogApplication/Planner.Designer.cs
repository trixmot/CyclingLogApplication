namespace CyclingLogApplication
{
    partial class Planner
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
            this.dataGridViewPlanner = new System.Windows.Forms.DataGridView();
            this.btClosePlanner = new System.Windows.Forms.Button();
            this.cbPlannerMonth = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPlannerLogs = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.brRefreshPlanner = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanner)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPlanner
            // 
            this.dataGridViewPlanner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanner.Location = new System.Drawing.Point(48, 75);
            this.dataGridViewPlanner.Name = "dataGridViewPlanner";
            this.dataGridViewPlanner.Size = new System.Drawing.Size(904, 589);
            this.dataGridViewPlanner.TabIndex = 0;
            this.dataGridViewPlanner.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPlanner_CellContentClick);
            // 
            // btClosePlanner
            // 
            this.btClosePlanner.Location = new System.Drawing.Point(877, 693);
            this.btClosePlanner.Name = "btClosePlanner";
            this.btClosePlanner.Size = new System.Drawing.Size(75, 23);
            this.btClosePlanner.TabIndex = 16;
            this.btClosePlanner.Text = "Close";
            this.btClosePlanner.UseVisualStyleBackColor = true;
            this.btClosePlanner.Click += new System.EventHandler(this.btClosePlanner_Click);
            // 
            // cbPlannerMonth
            // 
            this.cbPlannerMonth.FormattingEnabled = true;
            this.cbPlannerMonth.Items.AddRange(new object[] {
            "--Select Value--",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cbPlannerMonth.Location = new System.Drawing.Point(231, 35);
            this.cbPlannerMonth.Name = "cbPlannerMonth";
            this.cbPlannerMonth.Size = new System.Drawing.Size(121, 21);
            this.cbPlannerMonth.TabIndex = 18;
            this.cbPlannerMonth.SelectedIndexChanged += new System.EventHandler(this.RunPlanner);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Month";
            // 
            // cbPlannerLogs
            // 
            this.cbPlannerLogs.FormattingEnabled = true;
            this.cbPlannerLogs.Location = new System.Drawing.Point(48, 35);
            this.cbPlannerLogs.Name = "cbPlannerLogs";
            this.cbPlannerLogs.Size = new System.Drawing.Size(161, 21);
            this.cbPlannerLogs.TabIndex = 20;
            this.cbPlannerLogs.SelectedIndexChanged += new System.EventHandler(this.cbPlannerLogs_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Log";
            // 
            // brRefreshPlanner
            // 
            this.brRefreshPlanner.Location = new System.Drawing.Point(412, 33);
            this.brRefreshPlanner.Name = "brRefreshPlanner";
            this.brRefreshPlanner.Size = new System.Drawing.Size(75, 23);
            this.brRefreshPlanner.TabIndex = 23;
            this.brRefreshPlanner.Text = "Refresh";
            this.brRefreshPlanner.UseVisualStyleBackColor = true;
            this.brRefreshPlanner.Click += new System.EventHandler(this.brRefreshPlanner_Click);
            // 
            // Planner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 736);
            this.Controls.Add(this.brRefreshPlanner);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbPlannerLogs);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbPlannerMonth);
            this.Controls.Add(this.btClosePlanner);
            this.Controls.Add(this.dataGridViewPlanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Planner";
            this.Text = "Planner";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPlanner;
        private System.Windows.Forms.Button btClosePlanner;
        private System.Windows.Forms.ComboBox cbPlannerMonth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPlannerLogs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button brRefreshPlanner;
    }
}