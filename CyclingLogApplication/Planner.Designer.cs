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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Planner));
            this.dataGridViewPlanner = new System.Windows.Forms.DataGridView();
            this.btClosePlanner = new System.Windows.Forms.Button();
            this.cbPlannerMonth = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbPlannerLogs = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.brRefreshPlanner = new System.Windows.Forms.Button();
            this.tbDayPlanner2 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner3 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner4 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner5 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner6 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner7 = new System.Windows.Forms.TextBox();
            this.tbDayPlanner1 = new System.Windows.Forms.TextBox();
            this.lbDay1 = new System.Windows.Forms.Label();
            this.lbDay2 = new System.Windows.Forms.Label();
            this.lbDay3 = new System.Windows.Forms.Label();
            this.lbDay4 = new System.Windows.Forms.Label();
            this.lbDay5 = new System.Windows.Forms.Label();
            this.lbDay6 = new System.Windows.Forms.Label();
            this.lbDay7 = new System.Windows.Forms.Label();
            this.btSavePlanner = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbHold = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGoal = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.btDeletePlanner = new System.Windows.Forms.Button();
            this.btClearPlanned = new System.Windows.Forms.Button();
            this.lbPlannerError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPlannerDate = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPlanner
            // 
            this.dataGridViewPlanner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanner.Location = new System.Drawing.Point(27, 168);
            this.dataGridViewPlanner.Name = "dataGridViewPlanner";
            this.dataGridViewPlanner.Size = new System.Drawing.Size(904, 587);
            this.dataGridViewPlanner.TabIndex = 0;
            // 
            // btClosePlanner
            // 
            this.btClosePlanner.Location = new System.Drawing.Point(798, 102);
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
            this.cbPlannerMonth.Location = new System.Drawing.Point(19, 70);
            this.cbPlannerMonth.Name = "cbPlannerMonth";
            this.cbPlannerMonth.Size = new System.Drawing.Size(122, 21);
            this.cbPlannerMonth.TabIndex = 18;
            this.cbPlannerMonth.SelectedIndexChanged += new System.EventHandler(this.cbPlannerMonth_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Month";
            // 
            // cbPlannerLogs
            // 
            this.cbPlannerLogs.FormattingEnabled = true;
            this.cbPlannerLogs.Location = new System.Drawing.Point(20, 29);
            this.cbPlannerLogs.Name = "cbPlannerLogs";
            this.cbPlannerLogs.Size = new System.Drawing.Size(185, 21);
            this.cbPlannerLogs.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Log Title";
            // 
            // brRefreshPlanner
            // 
            this.brRefreshPlanner.Location = new System.Drawing.Point(798, 65);
            this.brRefreshPlanner.Name = "brRefreshPlanner";
            this.brRefreshPlanner.Size = new System.Drawing.Size(75, 23);
            this.brRefreshPlanner.TabIndex = 23;
            this.brRefreshPlanner.Text = "Refresh";
            this.brRefreshPlanner.UseVisualStyleBackColor = true;
            this.brRefreshPlanner.Click += new System.EventHandler(this.brRefreshPlanner_Click);
            // 
            // tbDayPlanner2
            // 
            this.tbDayPlanner2.Location = new System.Drawing.Point(328, 84);
            this.tbDayPlanner2.Name = "tbDayPlanner2";
            this.tbDayPlanner2.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner2.TabIndex = 24;
            this.tbDayPlanner2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner3
            // 
            this.tbDayPlanner3.Location = new System.Drawing.Point(400, 85);
            this.tbDayPlanner3.Name = "tbDayPlanner3";
            this.tbDayPlanner3.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner3.TabIndex = 25;
            this.tbDayPlanner3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner4
            // 
            this.tbDayPlanner4.Location = new System.Drawing.Point(473, 85);
            this.tbDayPlanner4.Name = "tbDayPlanner4";
            this.tbDayPlanner4.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner4.TabIndex = 26;
            this.tbDayPlanner4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner5
            // 
            this.tbDayPlanner5.Location = new System.Drawing.Point(547, 85);
            this.tbDayPlanner5.Name = "tbDayPlanner5";
            this.tbDayPlanner5.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner5.TabIndex = 27;
            this.tbDayPlanner5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner6
            // 
            this.tbDayPlanner6.Location = new System.Drawing.Point(621, 85);
            this.tbDayPlanner6.Name = "tbDayPlanner6";
            this.tbDayPlanner6.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner6.TabIndex = 28;
            this.tbDayPlanner6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner7
            // 
            this.tbDayPlanner7.Location = new System.Drawing.Point(694, 85);
            this.tbDayPlanner7.Name = "tbDayPlanner7";
            this.tbDayPlanner7.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner7.TabIndex = 29;
            this.tbDayPlanner7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbDayPlanner1
            // 
            this.tbDayPlanner1.Location = new System.Drawing.Point(256, 85);
            this.tbDayPlanner1.Name = "tbDayPlanner1";
            this.tbDayPlanner1.Size = new System.Drawing.Size(65, 20);
            this.tbDayPlanner1.TabIndex = 30;
            this.tbDayPlanner1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbDay1
            // 
            this.lbDay1.AutoSize = true;
            this.lbDay1.Location = new System.Drawing.Point(266, 68);
            this.lbDay1.Name = "lbDay1";
            this.lbDay1.Size = new System.Drawing.Size(32, 13);
            this.lbDay1.TabIndex = 31;
            this.lbDay1.Text = "Day1";
            // 
            // lbDay2
            // 
            this.lbDay2.AutoSize = true;
            this.lbDay2.Location = new System.Drawing.Point(337, 68);
            this.lbDay2.Name = "lbDay2";
            this.lbDay2.Size = new System.Drawing.Size(32, 13);
            this.lbDay2.TabIndex = 32;
            this.lbDay2.Text = "Day2";
            // 
            // lbDay3
            // 
            this.lbDay3.AutoSize = true;
            this.lbDay3.Location = new System.Drawing.Point(400, 68);
            this.lbDay3.Name = "lbDay3";
            this.lbDay3.Size = new System.Drawing.Size(32, 13);
            this.lbDay3.TabIndex = 33;
            this.lbDay3.Text = "Day3";
            // 
            // lbDay4
            // 
            this.lbDay4.AutoSize = true;
            this.lbDay4.Location = new System.Drawing.Point(482, 68);
            this.lbDay4.Name = "lbDay4";
            this.lbDay4.Size = new System.Drawing.Size(32, 13);
            this.lbDay4.TabIndex = 34;
            this.lbDay4.Text = "Day4";
            // 
            // lbDay5
            // 
            this.lbDay5.AutoSize = true;
            this.lbDay5.Location = new System.Drawing.Point(559, 68);
            this.lbDay5.Name = "lbDay5";
            this.lbDay5.Size = new System.Drawing.Size(32, 13);
            this.lbDay5.TabIndex = 35;
            this.lbDay5.Text = "Day5";
            // 
            // lbDay6
            // 
            this.lbDay6.AutoSize = true;
            this.lbDay6.Location = new System.Drawing.Point(630, 68);
            this.lbDay6.Name = "lbDay6";
            this.lbDay6.Size = new System.Drawing.Size(32, 13);
            this.lbDay6.TabIndex = 36;
            this.lbDay6.Text = "Day6";
            // 
            // lbDay7
            // 
            this.lbDay7.AutoSize = true;
            this.lbDay7.Location = new System.Drawing.Point(703, 68);
            this.lbDay7.Name = "lbDay7";
            this.lbDay7.Size = new System.Drawing.Size(32, 13);
            this.lbDay7.TabIndex = 37;
            this.lbDay7.Text = "Day7";
            // 
            // btSavePlanner
            // 
            this.btSavePlanner.Location = new System.Drawing.Point(388, 119);
            this.btSavePlanner.Name = "btSavePlanner";
            this.btSavePlanner.Size = new System.Drawing.Size(75, 23);
            this.btSavePlanner.TabIndex = 38;
            this.btSavePlanner.Text = "Save";
            this.btSavePlanner.UseVisualStyleBackColor = true;
            this.btSavePlanner.Click += new System.EventHandler(this.btSavePlanner_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbHold);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbGoal);
            this.groupBox1.Controls.Add(this.label62);
            this.groupBox1.Controls.Add(this.btDeletePlanner);
            this.groupBox1.Controls.Add(this.btClosePlanner);
            this.groupBox1.Controls.Add(this.btClearPlanned);
            this.groupBox1.Controls.Add(this.lbPlannerError);
            this.groupBox1.Controls.Add(this.btSavePlanner);
            this.groupBox1.Controls.Add(this.lbDay3);
            this.groupBox1.Controls.Add(this.brRefreshPlanner);
            this.groupBox1.Controls.Add(this.lbDay1);
            this.groupBox1.Controls.Add(this.cbPlannerMonth);
            this.groupBox1.Controls.Add(this.lbDay2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbDayPlanner1);
            this.groupBox1.Controls.Add(this.cbPlannerLogs);
            this.groupBox1.Controls.Add(this.lbDay4);
            this.groupBox1.Controls.Add(this.cbPlannerDate);
            this.groupBox1.Controls.Add(this.lbDay7);
            this.groupBox1.Controls.Add(this.tbDayPlanner6);
            this.groupBox1.Controls.Add(this.tbDayPlanner3);
            this.groupBox1.Controls.Add(this.lbDay6);
            this.groupBox1.Controls.Add(this.tbDayPlanner7);
            this.groupBox1.Controls.Add(this.tbDayPlanner2);
            this.groupBox1.Controls.Add(this.tbDayPlanner5);
            this.groupBox1.Controls.Add(this.tbDayPlanner4);
            this.groupBox1.Controls.Add(this.lbDay5);
            this.groupBox1.Location = new System.Drawing.Point(27, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 154);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // cbHold
            // 
            this.cbHold.AutoSize = true;
            this.cbHold.Location = new System.Drawing.Point(163, 75);
            this.cbHold.Name = "cbHold";
            this.cbHold.Size = new System.Drawing.Size(48, 17);
            this.cbHold.TabIndex = 67;
            this.cbHold.Text = "Hold";
            this.cbHold.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Goal %";
            // 
            // cbGoal
            // 
            this.cbGoal.FormattingEnabled = true;
            this.cbGoal.Items.AddRange(new object[] {
            ""});
            this.cbGoal.Location = new System.Drawing.Point(163, 111);
            this.cbGoal.Name = "cbGoal";
            this.cbGoal.Size = new System.Drawing.Size(52, 21);
            this.cbGoal.TabIndex = 65;
            this.cbGoal.SelectedIndexChanged += new System.EventHandler(this.cbGoal_SelectedIndexChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(338, 15);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(193, 28);
            this.label62.TabIndex = 64;
            this.label62.Text = "Weekly Planner";
            // 
            // btDeletePlanner
            // 
            this.btDeletePlanner.Location = new System.Drawing.Point(550, 119);
            this.btDeletePlanner.Name = "btDeletePlanner";
            this.btDeletePlanner.Size = new System.Drawing.Size(75, 23);
            this.btDeletePlanner.TabIndex = 43;
            this.btDeletePlanner.Text = "Delete";
            this.btDeletePlanner.UseVisualStyleBackColor = true;
            this.btDeletePlanner.Click += new System.EventHandler(this.btDeletePlanner_Click);
            // 
            // btClearPlanned
            // 
            this.btClearPlanned.Location = new System.Drawing.Point(469, 119);
            this.btClearPlanned.Name = "btClearPlanned";
            this.btClearPlanned.Size = new System.Drawing.Size(75, 23);
            this.btClearPlanned.TabIndex = 42;
            this.btClearPlanned.Text = "Clear";
            this.btClearPlanned.UseVisualStyleBackColor = true;
            this.btClearPlanned.Click += new System.EventHandler(this.btClearPlanned_Click);
            // 
            // lbPlannerError
            // 
            this.lbPlannerError.AutoSize = true;
            this.lbPlannerError.ForeColor = System.Drawing.Color.Red;
            this.lbPlannerError.Location = new System.Drawing.Point(630, 27);
            this.lbPlannerError.Name = "lbPlannerError";
            this.lbPlannerError.Size = new System.Drawing.Size(121, 13);
            this.lbPlannerError.TabIndex = 40;
            this.lbPlannerError.Text = "Planner error message...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Week";
            // 
            // cbPlannerDate
            // 
            this.cbPlannerDate.FormattingEnabled = true;
            this.cbPlannerDate.Location = new System.Drawing.Point(20, 111);
            this.cbPlannerDate.Name = "cbPlannerDate";
            this.cbPlannerDate.Size = new System.Drawing.Size(121, 21);
            this.cbPlannerDate.TabIndex = 39;
            this.cbPlannerDate.SelectedIndexChanged += new System.EventHandler(this.cbPlannerDate_SelectedIndexChanged);
            // 
            // Planner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 777);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewPlanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Planner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Planner";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanner)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPlanner;
        private System.Windows.Forms.Button btClosePlanner;
        private System.Windows.Forms.ComboBox cbPlannerMonth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbPlannerLogs;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button brRefreshPlanner;
        private System.Windows.Forms.TextBox tbDayPlanner2;
        private System.Windows.Forms.TextBox tbDayPlanner3;
        private System.Windows.Forms.TextBox tbDayPlanner4;
        private System.Windows.Forms.TextBox tbDayPlanner5;
        private System.Windows.Forms.TextBox tbDayPlanner6;
        private System.Windows.Forms.TextBox tbDayPlanner7;
        private System.Windows.Forms.TextBox tbDayPlanner1;
        private System.Windows.Forms.Label lbDay1;
        private System.Windows.Forms.Label lbDay2;
        private System.Windows.Forms.Label lbDay3;
        private System.Windows.Forms.Label lbDay4;
        private System.Windows.Forms.Label lbDay5;
        private System.Windows.Forms.Label lbDay6;
        private System.Windows.Forms.Label lbDay7;
        private System.Windows.Forms.Button btSavePlanner;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbPlannerDate;
        private System.Windows.Forms.Label lbPlannerError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btClearPlanned;
        private System.Windows.Forms.Button btDeletePlanner;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbGoal;
        private System.Windows.Forms.CheckBox cbHold;
    }
}