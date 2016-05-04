namespace CyclingLogApplication
{
    partial class RideDataEntry
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
            this.cbLogYearDataEntry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRideDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btUpdateRideDateEntry = new System.Windows.Forms.Button();
            this.lbErrorMessage = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbRecordID = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cbLocationDataEntry = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpTimeRideDataEntry = new System.Windows.Forms.DateTimePicker();
            this.cbRouteDataEntry = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.max_power = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.avg_power = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.max_speed = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.total_descent = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.total_ascent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.calories = new System.Windows.Forms.TextBox();
            this.max_heart_rate = new System.Windows.Forms.TextBox();
            this.avg_heart_rate = new System.Windows.Forms.TextBox();
            this.avg_cadence = new System.Windows.Forms.TextBox();
            this.nudDistanceRideDataEntry = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cbRideTypeDataEntry = new System.Windows.Forms.ComboBox();
            this.cbBikeDataEntry = new System.Windows.Forms.ComboBox();
            this.tableRideInformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cyclingLogDatabaseDataSet = new CyclingLogApplication.CyclingLogDatabaseDataSet();
            this.table_Ride_InformationTableAdapter = new CyclingLogApplication.CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter();
            this.lbNoLogYearSelected = new System.Windows.Forms.Label();
            this.tbWeekNumber = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceRideDataEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLogYearDataEntry
            // 
            this.cbLogYearDataEntry.FormattingEnabled = true;
            this.cbLogYearDataEntry.Location = new System.Drawing.Point(73, 37);
            this.cbLogYearDataEntry.Name = "cbLogYearDataEntry";
            this.cbLogYearDataEntry.Size = new System.Drawing.Size(121, 21);
            this.cbLogYearDataEntry.TabIndex = 0;
            this.cbLogYearDataEntry.SelectedIndexChanged += new System.EventHandler(this.cbLogYearDataEntry_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log Year*";
            // 
            // dtpRideDate
            // 
            this.dtpRideDate.Location = new System.Drawing.Point(73, 79);
            this.dtpRideDate.Name = "dtpRideDate";
            this.dtpRideDate.Size = new System.Drawing.Size(121, 20);
            this.dtpRideDate.TabIndex = 2;
            this.dtpRideDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.lbNoLogYearSelected);
            this.groupBox1.Controls.Add(this.btUpdateRideDateEntry);
            this.groupBox1.Controls.Add(this.lbErrorMessage);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbLogYearDataEntry);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpRideDate);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 165);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btUpdateRideDateEntry
            // 
            this.btUpdateRideDateEntry.Location = new System.Drawing.Point(384, 47);
            this.btUpdateRideDateEntry.Name = "btUpdateRideDateEntry";
            this.btUpdateRideDateEntry.Size = new System.Drawing.Size(75, 23);
            this.btUpdateRideDateEntry.TabIndex = 27;
            this.btUpdateRideDateEntry.Text = "Update";
            this.btUpdateRideDateEntry.UseVisualStyleBackColor = true;
            this.btUpdateRideDateEntry.Click += new System.EventHandler(this.btUpdateRideDateEntry_Click);
            // 
            // lbErrorMessage
            // 
            this.lbErrorMessage.AutoSize = true;
            this.lbErrorMessage.ForeColor = System.Drawing.Color.Crimson;
            this.lbErrorMessage.Location = new System.Drawing.Point(73, 110);
            this.lbErrorMessage.Name = "lbErrorMessage";
            this.lbErrorMessage.Size = new System.Drawing.Size(205, 13);
            this.lbErrorMessage.TabIndex = 26;
            this.lbErrorMessage.Text = "No Ride Data found for the selected date.";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(384, 76);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Import";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ImportData);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(384, 134);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.clearDataEntryFields_click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(73, 63);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Ride Date*";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.submitData);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(425, 576);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.closeRideDataEntry);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbWeekNumber);
            this.groupBox2.Controls.Add(this.tbRecordID);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.cbLocationDataEntry);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.tbComments);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.dtpTimeRideDataEntry);
            this.groupBox2.Controls.Add(this.cbRouteDataEntry);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.numericUpDown3);
            this.groupBox2.Controls.Add(this.numericUpDown4);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.max_power);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.avg_power);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.max_speed);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.total_descent);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.total_ascent);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.calories);
            this.groupBox2.Controls.Add(this.max_heart_rate);
            this.groupBox2.Controls.Add(this.avg_heart_rate);
            this.groupBox2.Controls.Add(this.avg_cadence);
            this.groupBox2.Controls.Add(this.nudDistanceRideDataEntry);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.cbRideTypeDataEntry);
            this.groupBox2.Controls.Add(this.cbBikeDataEntry);
            this.groupBox2.Location = new System.Drawing.Point(12, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 379);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // tbRecordID
            // 
            this.tbRecordID.Location = new System.Drawing.Point(73, 14);
            this.tbRecordID.Name = "tbRecordID";
            this.tbRecordID.Size = new System.Drawing.Size(54, 20);
            this.tbRecordID.TabIndex = 28;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(372, 363);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "* Required Fields";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 256);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Location*";
            // 
            // cbLocationDataEntry
            // 
            this.cbLocationDataEntry.FormattingEnabled = true;
            this.cbLocationDataEntry.Items.AddRange(new object[] {
            "Road",
            "Rollers",
            "Trail",
            "Trainer"});
            this.cbLocationDataEntry.Location = new System.Drawing.Point(73, 253);
            this.cbLocationDataEntry.Name = "cbLocationDataEntry";
            this.cbLocationDataEntry.Size = new System.Drawing.Size(101, 21);
            this.cbLocationDataEntry.TabIndex = 38;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(55, 302);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Comments";
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(117, 302);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.Size = new System.Drawing.Size(342, 42);
            this.tbComments.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(36, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Time*";
            // 
            // dtpTimeRideDataEntry
            // 
            this.dtpTimeRideDataEntry.Location = new System.Drawing.Point(73, 67);
            this.dtpTimeRideDataEntry.Name = "dtpTimeRideDataEntry";
            this.dtpTimeRideDataEntry.Size = new System.Drawing.Size(101, 20);
            this.dtpTimeRideDataEntry.TabIndex = 35;
            // 
            // cbRouteDataEntry
            // 
            this.cbRouteDataEntry.FormattingEnabled = true;
            this.cbRouteDataEntry.Location = new System.Drawing.Point(73, 40);
            this.cbRouteDataEntry.Name = "cbRouteDataEntry";
            this.cbRouteDataEntry.Size = new System.Drawing.Size(165, 21);
            this.cbRouteDataEntry.TabIndex = 34;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(32, 176);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Temp";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(34, 149);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Wind";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(73, 173);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown3.TabIndex = 31;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(73, 147);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown4.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Distance*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Avg Speed*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(35, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Type*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 202);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Bike*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Route*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(289, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Max Power";
            // 
            // max_power
            // 
            this.max_power.Location = new System.Drawing.Point(359, 249);
            this.max_power.Name = "max_power";
            this.max_power.Size = new System.Drawing.Size(100, 20);
            this.max_power.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(291, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Avg Power";
            // 
            // avg_power
            // 
            this.avg_power.Location = new System.Drawing.Point(359, 223);
            this.avg_power.Name = "avg_power";
            this.avg_power.Size = new System.Drawing.Size(100, 20);
            this.avg_power.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Max Speed";
            // 
            // max_speed
            // 
            this.max_speed.Location = new System.Drawing.Point(359, 197);
            this.max_speed.Name = "max_speed";
            this.max_speed.Size = new System.Drawing.Size(100, 20);
            this.max_speed.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(276, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Descent";
            // 
            // total_descent
            // 
            this.total_descent.Location = new System.Drawing.Point(359, 171);
            this.total_descent.Name = "total_descent";
            this.total_descent.Size = new System.Drawing.Size(100, 20);
            this.total_descent.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(283, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Total Ascent";
            // 
            // total_ascent
            // 
            this.total_ascent.Location = new System.Drawing.Point(359, 145);
            this.total_ascent.Name = "total_ascent";
            this.total_ascent.Size = new System.Drawing.Size(100, 20);
            this.total_ascent.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Calories";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Max Heart Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Avg Heart Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Avg Cadence";
            // 
            // calories
            // 
            this.calories.Location = new System.Drawing.Point(359, 119);
            this.calories.Name = "calories";
            this.calories.Size = new System.Drawing.Size(100, 20);
            this.calories.TabIndex = 10;
            // 
            // max_heart_rate
            // 
            this.max_heart_rate.Location = new System.Drawing.Point(359, 93);
            this.max_heart_rate.Name = "max_heart_rate";
            this.max_heart_rate.Size = new System.Drawing.Size(100, 20);
            this.max_heart_rate.TabIndex = 9;
            // 
            // avg_heart_rate
            // 
            this.avg_heart_rate.Location = new System.Drawing.Point(359, 67);
            this.avg_heart_rate.Name = "avg_heart_rate";
            this.avg_heart_rate.Size = new System.Drawing.Size(100, 20);
            this.avg_heart_rate.TabIndex = 8;
            // 
            // avg_cadence
            // 
            this.avg_cadence.Location = new System.Drawing.Point(359, 41);
            this.avg_cadence.Name = "avg_cadence";
            this.avg_cadence.Size = new System.Drawing.Size(100, 20);
            this.avg_cadence.TabIndex = 7;
            // 
            // nudDistanceRideDataEntry
            // 
            this.nudDistanceRideDataEntry.Location = new System.Drawing.Point(73, 93);
            this.nudDistanceRideDataEntry.Name = "nudDistanceRideDataEntry";
            this.nudDistanceRideDataEntry.Size = new System.Drawing.Size(101, 20);
            this.nudDistanceRideDataEntry.TabIndex = 6;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(73, 119);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(101, 20);
            this.numericUpDown1.TabIndex = 5;
            // 
            // cbRideTypeDataEntry
            // 
            this.cbRideTypeDataEntry.FormattingEnabled = true;
            this.cbRideTypeDataEntry.Items.AddRange(new object[] {
            "Recovery",
            "Base",
            "Distance",
            "Speed",
            "Race"});
            this.cbRideTypeDataEntry.Location = new System.Drawing.Point(73, 226);
            this.cbRideTypeDataEntry.Name = "cbRideTypeDataEntry";
            this.cbRideTypeDataEntry.Size = new System.Drawing.Size(101, 21);
            this.cbRideTypeDataEntry.TabIndex = 1;
            // 
            // cbBikeDataEntry
            // 
            this.cbBikeDataEntry.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.tableRideInformationBindingSource, "Bike", true));
            this.cbBikeDataEntry.FormattingEnabled = true;
            this.cbBikeDataEntry.Location = new System.Drawing.Point(73, 199);
            this.cbBikeDataEntry.Name = "cbBikeDataEntry";
            this.cbBikeDataEntry.Size = new System.Drawing.Size(101, 21);
            this.cbBikeDataEntry.TabIndex = 0;
            this.cbBikeDataEntry.SelectedIndexChanged += new System.EventHandler(this.cbBikeDataEntry_SelectedIndexChanged);
            // 
            // tableRideInformationBindingSource
            // 
            this.tableRideInformationBindingSource.DataMember = "Table_Ride_Information";
            this.tableRideInformationBindingSource.DataSource = this.cyclingLogDatabaseDataSet;
            // 
            // cyclingLogDatabaseDataSet
            // 
            this.cyclingLogDatabaseDataSet.DataSetName = "CyclingLogDatabaseDataSet";
            this.cyclingLogDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // table_Ride_InformationTableAdapter
            // 
            this.table_Ride_InformationTableAdapter.ClearBeforeFill = true;
            // 
            // lbNoLogYearSelected
            // 
            this.lbNoLogYearSelected.AutoSize = true;
            this.lbNoLogYearSelected.ForeColor = System.Drawing.Color.Crimson;
            this.lbNoLogYearSelected.Location = new System.Drawing.Point(216, 24);
            this.lbNoLogYearSelected.Name = "lbNoLogYearSelected";
            this.lbNoLogYearSelected.Size = new System.Drawing.Size(0, 13);
            this.lbNoLogYearSelected.TabIndex = 28;
            // 
            // tbWeekNumber
            // 
            this.tbWeekNumber.Location = new System.Drawing.Point(133, 14);
            this.tbWeekNumber.Name = "tbWeekNumber";
            this.tbWeekNumber.Size = new System.Drawing.Size(41, 20);
            this.tbWeekNumber.TabIndex = 41;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(384, 105);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 29;
            this.button5.Text = "Delete";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // RideDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 611);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Name = "RideDataEntry";
            this.Text = "RideDataEntry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RideDataEntryFormClosed);
            this.Load += new System.EventHandler(this.RideDataEntryLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistanceRideDataEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRideDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRideTypeDataEntry;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown nudDistanceRideDataEntry;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox total_ascent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox calories;
        private System.Windows.Forms.TextBox max_heart_rate;
        private System.Windows.Forms.TextBox avg_heart_rate;
        private System.Windows.Forms.TextBox avg_cadence;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox max_power;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox avg_power;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox max_speed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox total_descent;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.DateTimePicker dtpTimeRideDataEntry;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbLocationDataEntry;
        public System.Windows.Forms.ComboBox cbLogYearDataEntry;
        public System.Windows.Forms.ComboBox cbBikeDataEntry;
        public System.Windows.Forms.ComboBox cbRouteDataEntry;
        private CyclingLogDatabaseDataSet cyclingLogDatabaseDataSet;
        private System.Windows.Forms.BindingSource tableRideInformationBindingSource;
        private CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter table_Ride_InformationTableAdapter;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbErrorMessage;
        private System.Windows.Forms.Button btUpdateRideDateEntry;
        private System.Windows.Forms.TextBox tbRecordID;
        private System.Windows.Forms.Label lbNoLogYearSelected;
        private System.Windows.Forms.TextBox tbWeekNumber;
        private System.Windows.Forms.Button button5;
    }
}