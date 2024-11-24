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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btSavePlanner = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanner)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPlanner
            // 
            this.dataGridViewPlanner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanner.Location = new System.Drawing.Point(48, 147);
            this.dataGridViewPlanner.Name = "dataGridViewPlanner";
            this.dataGridViewPlanner.Size = new System.Drawing.Size(904, 517);
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
            this.cbPlannerMonth.Location = new System.Drawing.Point(231, 111);
            this.cbPlannerMonth.Name = "cbPlannerMonth";
            this.cbPlannerMonth.Size = new System.Drawing.Size(121, 21);
            this.cbPlannerMonth.TabIndex = 18;
            this.cbPlannerMonth.SelectedIndexChanged += new System.EventHandler(this.RunPlanner);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(228, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Month";
            // 
            // cbPlannerLogs
            // 
            this.cbPlannerLogs.FormattingEnabled = true;
            this.cbPlannerLogs.Location = new System.Drawing.Point(48, 111);
            this.cbPlannerLogs.Name = "cbPlannerLogs";
            this.cbPlannerLogs.Size = new System.Drawing.Size(161, 21);
            this.cbPlannerLogs.TabIndex = 20;
            this.cbPlannerLogs.SelectedIndexChanged += new System.EventHandler(this.cbPlannerLogs_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Log";
            // 
            // brRefreshPlanner
            // 
            this.brRefreshPlanner.Location = new System.Drawing.Point(412, 109);
            this.brRefreshPlanner.Name = "brRefreshPlanner";
            this.brRefreshPlanner.Size = new System.Drawing.Size(75, 23);
            this.brRefreshPlanner.TabIndex = 23;
            this.brRefreshPlanner.Text = "Refresh";
            this.brRefreshPlanner.UseVisualStyleBackColor = true;
            this.brRefreshPlanner.Click += new System.EventHandler(this.brRefreshPlanner_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(273, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 20);
            this.textBox1.TabIndex = 24;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(344, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(65, 20);
            this.textBox2.TabIndex = 25;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(415, 36);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(65, 20);
            this.textBox3.TabIndex = 26;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(486, 36);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(65, 20);
            this.textBox4.TabIndex = 27;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(557, 36);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(65, 20);
            this.textBox5.TabIndex = 28;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(628, 36);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(65, 20);
            this.textBox6.TabIndex = 29;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(202, 36);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(65, 20);
            this.textBox7.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Day1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Day2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Day3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Day4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Day5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(574, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Day6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(646, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Day7";
            // 
            // btSavePlanner
            // 
            this.btSavePlanner.Location = new System.Drawing.Point(739, 33);
            this.btSavePlanner.Name = "btSavePlanner";
            this.btSavePlanner.Size = new System.Drawing.Size(75, 23);
            this.btSavePlanner.TabIndex = 38;
            this.btSavePlanner.Text = "Save";
            this.btSavePlanner.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.btSavePlanner);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(45, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(907, 65);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(43, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 39;
            // 
            // Planner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 736);
            this.Controls.Add(this.groupBox1);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btSavePlanner;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}