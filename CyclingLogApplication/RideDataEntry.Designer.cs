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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RideDataEntry));
            this.cbLogYearDataEntry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRideDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btLogEntrySave = new System.Windows.Forms.Button();
            this.btRideDisplayUpdate = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBoxRetrieveDate = new System.Windows.Forms.GroupBox();
            this.checkBoxCloneEntry = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btRetrieve = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.tbWeekCountRDE = new System.Windows.Forms.TextBox();
            this.tbRecordID = new System.Windows.Forms.TextBox();
            this.btDeleteRideDataEntry = new System.Windows.Forms.Button();
            this.lbRideDataEntryError = new System.Windows.Forms.Label();
            this.btImportDataEntry = new System.Windows.Forms.Button();
            this.btClearDataEntry = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.lbNoLogYearSelected = new System.Windows.Forms.Label();
            this.btRIdeDataEntryClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbRideDataEntryDistance = new System.Windows.Forms.TextBox();
            this.tbRideDataEntryAvgSpeed = new System.Windows.Forms.TextBox();
            this.tbRideDataEntryWind = new System.Windows.Forms.TextBox();
            this.tbRideEntryTemp = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lbCustom1 = new System.Windows.Forms.Label();
            this.tbRideEntryWindChill = new System.Windows.Forms.TextBox();
            this.tbCustom2 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbMaxCadence = new System.Windows.Forms.TextBox();
            this.lbCustom2 = new System.Windows.Forms.Label();
            this.tbCustom1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpTimeRideDataEntry = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
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
            this.cbBikeDataEntrySelection = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cbComfortRideDataEntry = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cbEffortRideDataEntry = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbLocationDataEntry = new System.Windows.Forms.ComboBox();
            this.cbRouteDataEntry = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbRideTypeDataEntry = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableRideInformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cyclingLogDatabaseDataSet = new CyclingLogApplication.CyclingLogDatabaseDataSet();
            this.table_Ride_InformationTableAdapter = new CyclingLogApplication.CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxRetrieveDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLogYearDataEntry
            // 
            this.cbLogYearDataEntry.FormattingEnabled = true;
            this.cbLogYearDataEntry.Location = new System.Drawing.Point(20, 38);
            this.cbLogYearDataEntry.Name = "cbLogYearDataEntry";
            this.cbLogYearDataEntry.Size = new System.Drawing.Size(182, 21);
            this.cbLogYearDataEntry.TabIndex = 0;
            this.cbLogYearDataEntry.SelectedIndexChanged += new System.EventHandler(this.CbLogYearDataEntry_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log Title*";
            // 
            // dtpRideDate
            // 
            this.dtpRideDate.Location = new System.Drawing.Point(20, 80);
            this.dtpRideDate.Name = "dtpRideDate";
            this.dtpRideDate.Size = new System.Drawing.Size(182, 20);
            this.dtpRideDate.TabIndex = 1;
            this.dtpRideDate.ValueChanged += new System.EventHandler(this.DtpRideDate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btLogEntrySave);
            this.groupBox1.Controls.Add(this.btRideDisplayUpdate);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.groupBoxRetrieveDate);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.tbWeekCountRDE);
            this.groupBox1.Controls.Add(this.tbRecordID);
            this.groupBox1.Controls.Add(this.btDeleteRideDataEntry);
            this.groupBox1.Controls.Add(this.lbRideDataEntryError);
            this.groupBox1.Controls.Add(this.btImportDataEntry);
            this.groupBox1.Controls.Add(this.btClearDataEntry);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.cbLogYearDataEntry);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpRideDate);
            this.groupBox1.Location = new System.Drawing.Point(19, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btLogEntrySave
            // 
            this.btLogEntrySave.Location = new System.Drawing.Point(420, 35);
            this.btLogEntrySave.Name = "btLogEntrySave";
            this.btLogEntrySave.Size = new System.Drawing.Size(75, 23);
            this.btLogEntrySave.TabIndex = 5;
            this.btLogEntrySave.Text = "Save";
            this.btLogEntrySave.UseVisualStyleBackColor = true;
            this.btLogEntrySave.Click += new System.EventHandler(this.btLogEntrySave_Click);
            // 
            // btRideDisplayUpdate
            // 
            this.btRideDisplayUpdate.Location = new System.Drawing.Point(356, 64);
            this.btRideDisplayUpdate.Name = "btRideDisplayUpdate";
            this.btRideDisplayUpdate.Size = new System.Drawing.Size(85, 34);
            this.btRideDisplayUpdate.TabIndex = 9;
            this.btRideDisplayUpdate.Text = "Update";
            this.btRideDisplayUpdate.UseVisualStyleBackColor = true;
            this.btRideDisplayUpdate.Click += new System.EventHandler(this.btRideDisplayUpdate_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(100, 115);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 13);
            this.label29.TabIndex = 49;
            this.label29.Text = "Record#";
            // 
            // groupBoxRetrieveDate
            // 
            this.groupBoxRetrieveDate.Controls.Add(this.checkBoxCloneEntry);
            this.groupBoxRetrieveDate.Controls.Add(this.label28);
            this.groupBoxRetrieveDate.Controls.Add(this.btRetrieve);
            this.groupBoxRetrieveDate.Controls.Add(this.label24);
            this.groupBoxRetrieveDate.Controls.Add(this.numericUpDown2);
            this.groupBoxRetrieveDate.Location = new System.Drawing.Point(230, 14);
            this.groupBoxRetrieveDate.Name = "groupBoxRetrieveDate";
            this.groupBoxRetrieveDate.Size = new System.Drawing.Size(158, 138);
            this.groupBoxRetrieveDate.TabIndex = 48;
            this.groupBoxRetrieveDate.TabStop = false;
            // 
            // checkBoxCloneEntry
            // 
            this.checkBoxCloneEntry.AutoSize = true;
            this.checkBoxCloneEntry.Location = new System.Drawing.Point(45, 71);
            this.checkBoxCloneEntry.Name = "checkBoxCloneEntry";
            this.checkBoxCloneEntry.Size = new System.Drawing.Size(80, 17);
            this.checkBoxCloneEntry.TabIndex = 50;
            this.checkBoxCloneEntry.Text = "Clone Entry";
            this.checkBoxCloneEntry.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(18, 19);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(125, 13);
            this.label28.TabIndex = 49;
            this.label28.Text = "Retrieve Data From Date";
            // 
            // btRetrieve
            // 
            this.btRetrieve.Location = new System.Drawing.Point(45, 40);
            this.btRetrieve.Name = "btRetrieve";
            this.btRetrieve.Size = new System.Drawing.Size(75, 23);
            this.btRetrieve.TabIndex = 4;
            this.btRetrieve.Text = "Retrieve";
            this.btRetrieve.UseVisualStyleBackColor = true;
            this.btRetrieve.Click += new System.EventHandler(this.BtRetrieve_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(61, 97);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(73, 13);
            this.label24.TabIndex = 45;
            this.label24.Text = "Multiple Rides";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(21, 95);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(34, 20);
            this.numericUpDown2.TabIndex = 43;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2_ValueChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(20, 115);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(43, 13);
            this.label25.TabIndex = 47;
            this.label25.Text = "Week#";
            // 
            // tbWeekCountRDE
            // 
            this.tbWeekCountRDE.Location = new System.Drawing.Point(72, 112);
            this.tbWeekCountRDE.Name = "tbWeekCountRDE";
            this.tbWeekCountRDE.ReadOnly = true;
            this.tbWeekCountRDE.Size = new System.Drawing.Size(22, 20);
            this.tbWeekCountRDE.TabIndex = 2;
            this.tbWeekCountRDE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRecordID
            // 
            this.tbRecordID.Location = new System.Drawing.Point(155, 112);
            this.tbRecordID.Name = "tbRecordID";
            this.tbRecordID.ReadOnly = true;
            this.tbRecordID.Size = new System.Drawing.Size(47, 20);
            this.tbRecordID.TabIndex = 3;
            // 
            // btDeleteRideDataEntry
            // 
            this.btDeleteRideDataEntry.Location = new System.Drawing.Point(420, 93);
            this.btDeleteRideDataEntry.Name = "btDeleteRideDataEntry";
            this.btDeleteRideDataEntry.Size = new System.Drawing.Size(75, 23);
            this.btDeleteRideDataEntry.TabIndex = 7;
            this.btDeleteRideDataEntry.Text = "Delete";
            this.btDeleteRideDataEntry.UseVisualStyleBackColor = true;
            this.btDeleteRideDataEntry.Click += new System.EventHandler(this.BtDeleteRideDataEntry_Click);
            // 
            // lbRideDataEntryError
            // 
            this.lbRideDataEntryError.AutoSize = true;
            this.lbRideDataEntryError.ForeColor = System.Drawing.Color.Crimson;
            this.lbRideDataEntryError.Location = new System.Drawing.Point(20, 139);
            this.lbRideDataEntryError.Name = "lbRideDataEntryError";
            this.lbRideDataEntryError.Size = new System.Drawing.Size(107, 13);
            this.lbRideDataEntryError.TabIndex = 26;
            this.lbRideDataEntryError.Text = "Ride Data Entry Error";
            // 
            // btImportDataEntry
            // 
            this.btImportDataEntry.Location = new System.Drawing.Point(420, 64);
            this.btImportDataEntry.Name = "btImportDataEntry";
            this.btImportDataEntry.Size = new System.Drawing.Size(75, 23);
            this.btImportDataEntry.TabIndex = 6;
            this.btImportDataEntry.Text = "Garmin CSV";
            this.btImportDataEntry.UseVisualStyleBackColor = true;
            this.btImportDataEntry.Click += new System.EventHandler(this.ImportData);
            // 
            // btClearDataEntry
            // 
            this.btClearDataEntry.Location = new System.Drawing.Point(420, 122);
            this.btClearDataEntry.Name = "btClearDataEntry";
            this.btClearDataEntry.Size = new System.Drawing.Size(75, 23);
            this.btClearDataEntry.TabIndex = 8;
            this.btClearDataEntry.Text = "Clear";
            this.btClearDataEntry.UseVisualStyleBackColor = true;
            this.btClearDataEntry.Click += new System.EventHandler(this.ClearDataEntryFields_click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(20, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Ride Date*";
            // 
            // lbNoLogYearSelected
            // 
            this.lbNoLogYearSelected.AutoSize = true;
            this.lbNoLogYearSelected.ForeColor = System.Drawing.Color.Crimson;
            this.lbNoLogYearSelected.Location = new System.Drawing.Point(275, 595);
            this.lbNoLogYearSelected.Name = "lbNoLogYearSelected";
            this.lbNoLogYearSelected.Size = new System.Drawing.Size(0, 13);
            this.lbNoLogYearSelected.TabIndex = 28;
            // 
            // btRIdeDataEntryClose
            // 
            this.btRIdeDataEntryClose.Location = new System.Drawing.Point(467, 673);
            this.btRIdeDataEntryClose.Name = "btRIdeDataEntryClose";
            this.btRIdeDataEntryClose.Size = new System.Drawing.Size(85, 35);
            this.btRIdeDataEntryClose.TabIndex = 35;
            this.btRIdeDataEntryClose.Text = "Close";
            this.btRIdeDataEntryClose.UseVisualStyleBackColor = true;
            this.btRIdeDataEntryClose.Click += new System.EventHandler(this.CloseRideDataEntry);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbRideDataEntryDistance);
            this.groupBox2.Controls.Add(this.tbRideDataEntryAvgSpeed);
            this.groupBox2.Controls.Add(this.tbRideDataEntryWind);
            this.groupBox2.Controls.Add(this.tbRideEntryTemp);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.lbCustom1);
            this.groupBox2.Controls.Add(this.tbRideEntryWindChill);
            this.groupBox2.Controls.Add(this.tbCustom2);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.tbMaxCadence);
            this.groupBox2.Controls.Add(this.lbCustom2);
            this.groupBox2.Controls.Add(this.tbCustom1);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.tbComments);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.dtpTimeRideDataEntry);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
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
            this.groupBox2.Location = new System.Drawing.Point(19, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(534, 310);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // tbRideDataEntryDistance
            // 
            this.tbRideDataEntryDistance.Location = new System.Drawing.Point(107, 44);
            this.tbRideDataEntryDistance.Name = "tbRideDataEntryDistance";
            this.tbRideDataEntryDistance.Size = new System.Drawing.Size(100, 20);
            this.tbRideDataEntryDistance.TabIndex = 17;
            this.tbRideDataEntryDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRideDataEntryAvgSpeed
            // 
            this.tbRideDataEntryAvgSpeed.Location = new System.Drawing.Point(107, 70);
            this.tbRideDataEntryAvgSpeed.Name = "tbRideDataEntryAvgSpeed";
            this.tbRideDataEntryAvgSpeed.ReadOnly = true;
            this.tbRideDataEntryAvgSpeed.Size = new System.Drawing.Size(100, 20);
            this.tbRideDataEntryAvgSpeed.TabIndex = 18;
            this.tbRideDataEntryAvgSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRideDataEntryWind
            // 
            this.tbRideDataEntryWind.Location = new System.Drawing.Point(107, 146);
            this.tbRideDataEntryWind.Name = "tbRideDataEntryWind";
            this.tbRideDataEntryWind.Size = new System.Drawing.Size(100, 20);
            this.tbRideDataEntryWind.TabIndex = 21;
            this.tbRideDataEntryWind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRideEntryTemp
            // 
            this.tbRideEntryTemp.Location = new System.Drawing.Point(107, 170);
            this.tbRideEntryTemp.Name = "tbRideEntryTemp";
            this.tbRideEntryTemp.Size = new System.Drawing.Size(100, 20);
            this.tbRideEntryTemp.TabIndex = 22;
            this.tbRideEntryTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(42, 200);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(63, 13);
            this.label30.TabIndex = 53;
            this.label30.Text = "Wind Chill ^";
            // 
            // lbCustom1
            // 
            this.lbCustom1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbCustom1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbCustom1.Location = new System.Drawing.Point(6, 225);
            this.lbCustom1.Name = "lbCustom1";
            this.lbCustom1.Size = new System.Drawing.Size(96, 17);
            this.lbCustom1.TabIndex = 47;
            this.lbCustom1.Text = "User Defined 1";
            this.lbCustom1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbRideEntryWindChill
            // 
            this.tbRideEntryWindChill.Location = new System.Drawing.Point(107, 195);
            this.tbRideEntryWindChill.Name = "tbRideEntryWindChill";
            this.tbRideEntryWindChill.ReadOnly = true;
            this.tbRideEntryWindChill.Size = new System.Drawing.Size(100, 20);
            this.tbRideEntryWindChill.TabIndex = 23;
            this.tbRideEntryWindChill.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbCustom2
            // 
            this.tbCustom2.Location = new System.Drawing.Point(394, 224);
            this.tbCustom2.Name = "tbCustom2";
            this.tbCustom2.Size = new System.Drawing.Size(100, 20);
            this.tbCustom2.TabIndex = 33;
            this.tbCustom2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(318, 45);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(73, 13);
            this.label27.TabIndex = 50;
            this.label27.Text = "Max Cadence";
            // 
            // tbMaxCadence
            // 
            this.tbMaxCadence.Location = new System.Drawing.Point(394, 42);
            this.tbMaxCadence.Name = "tbMaxCadence";
            this.tbMaxCadence.Size = new System.Drawing.Size(100, 20);
            this.tbMaxCadence.TabIndex = 26;
            this.tbMaxCadence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbCustom2
            // 
            this.lbCustom2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbCustom2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbCustom2.Location = new System.Drawing.Point(272, 227);
            this.lbCustom2.Name = "lbCustom2";
            this.lbCustom2.Size = new System.Drawing.Size(115, 17);
            this.lbCustom2.TabIndex = 51;
            this.lbCustom2.Text = "User Defined 2";
            this.lbCustom2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCustom1
            // 
            this.tbCustom1.Location = new System.Drawing.Point(107, 222);
            this.tbCustom1.Name = "tbCustom1";
            this.tbCustom1.Size = new System.Drawing.Size(100, 20);
            this.tbCustom1.TabIndex = 24;
            this.tbCustom1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(38, 265);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Comments";
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(103, 250);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.Size = new System.Drawing.Size(392, 42);
            this.tbComments.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(70, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Time*";
            // 
            // dtpTimeRideDataEntry
            // 
            this.dtpTimeRideDataEntry.Location = new System.Drawing.Point(107, 19);
            this.dtpTimeRideDataEntry.Name = "dtpTimeRideDataEntry";
            this.dtpTimeRideDataEntry.Size = new System.Drawing.Size(100, 20);
            this.dtpTimeRideDataEntry.TabIndex = 16;
            this.dtpTimeRideDataEntry.ValueChanged += new System.EventHandler(this.DtpTimeRideDataEntry_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(70, 173);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(34, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Temp";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(49, 149);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Max Wind";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(51, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 13);
            this.label15.TabIndex = 29;
            this.label15.Text = "Distance*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(36, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Avg Speed ^";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(327, 201);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Max Power";
            // 
            // max_power
            // 
            this.max_power.Location = new System.Drawing.Point(394, 198);
            this.max_power.Name = "max_power";
            this.max_power.Size = new System.Drawing.Size(100, 20);
            this.max_power.TabIndex = 32;
            this.max_power.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(329, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Avg Power";
            // 
            // avg_power
            // 
            this.avg_power.Location = new System.Drawing.Point(394, 172);
            this.avg_power.Name = "avg_power";
            this.avg_power.Size = new System.Drawing.Size(100, 20);
            this.avg_power.TabIndex = 31;
            this.avg_power.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Max Speed";
            // 
            // max_speed
            // 
            this.max_speed.Location = new System.Drawing.Point(107, 96);
            this.max_speed.Name = "max_speed";
            this.max_speed.Size = new System.Drawing.Size(100, 20);
            this.max_speed.TabIndex = 19;
            this.max_speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(314, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Descent";
            // 
            // total_descent
            // 
            this.total_descent.Location = new System.Drawing.Point(394, 148);
            this.total_descent.Name = "total_descent";
            this.total_descent.Size = new System.Drawing.Size(100, 20);
            this.total_descent.TabIndex = 30;
            this.total_descent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(321, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Total Ascent";
            // 
            // total_ascent
            // 
            this.total_ascent.Location = new System.Drawing.Point(394, 122);
            this.total_ascent.Name = "total_ascent";
            this.total_ascent.Size = new System.Drawing.Size(100, 20);
            this.total_ascent.TabIndex = 29;
            this.total_ascent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Calories";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Max Heart Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Avg Heart Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Avg Cadence";
            // 
            // calories
            // 
            this.calories.Location = new System.Drawing.Point(107, 120);
            this.calories.Name = "calories";
            this.calories.Size = new System.Drawing.Size(100, 20);
            this.calories.TabIndex = 20;
            this.calories.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // max_heart_rate
            // 
            this.max_heart_rate.Location = new System.Drawing.Point(394, 93);
            this.max_heart_rate.Name = "max_heart_rate";
            this.max_heart_rate.Size = new System.Drawing.Size(100, 20);
            this.max_heart_rate.TabIndex = 28;
            this.max_heart_rate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_heart_rate
            // 
            this.avg_heart_rate.Location = new System.Drawing.Point(394, 67);
            this.avg_heart_rate.Name = "avg_heart_rate";
            this.avg_heart_rate.Size = new System.Drawing.Size(100, 20);
            this.avg_heart_rate.TabIndex = 27;
            this.avg_heart_rate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // avg_cadence
            // 
            this.avg_cadence.Location = new System.Drawing.Point(394, 16);
            this.avg_cadence.Name = "avg_cadence";
            this.avg_cadence.Size = new System.Drawing.Size(100, 20);
            this.avg_cadence.TabIndex = 25;
            this.avg_cadence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbBikeDataEntrySelection
            // 
            this.cbBikeDataEntrySelection.FormattingEnabled = true;
            this.cbBikeDataEntrySelection.Location = new System.Drawing.Point(141, 19);
            this.cbBikeDataEntrySelection.Name = "cbBikeDataEntrySelection";
            this.cbBikeDataEntrySelection.Size = new System.Drawing.Size(287, 21);
            this.cbBikeDataEntrySelection.TabIndex = 10;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(270, 116);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 13);
            this.label26.TabIndex = 45;
            this.label26.Text = "Comfort*";
            // 
            // cbComfortRideDataEntry
            // 
            this.cbComfortRideDataEntry.FormattingEnabled = true;
            this.cbComfortRideDataEntry.Location = new System.Drawing.Point(327, 113);
            this.cbComfortRideDataEntry.Name = "cbComfortRideDataEntry";
            this.cbComfortRideDataEntry.Size = new System.Drawing.Size(100, 21);
            this.cbComfortRideDataEntry.TabIndex = 15;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(281, 89);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(36, 13);
            this.label23.TabIndex = 42;
            this.label23.Text = "Effort*";
            // 
            // cbEffortRideDataEntry
            // 
            this.cbEffortRideDataEntry.FormattingEnabled = true;
            this.cbEffortRideDataEntry.Location = new System.Drawing.Point(327, 86);
            this.cbEffortRideDataEntry.Name = "cbEffortRideDataEntry";
            this.cbEffortRideDataEntry.Size = new System.Drawing.Size(100, 21);
            this.cbEffortRideDataEntry.TabIndex = 14;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(78, 114);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 13);
            this.label21.TabIndex = 39;
            this.label21.Text = "Location*";
            // 
            // cbLocationDataEntry
            // 
            this.cbLocationDataEntry.FormattingEnabled = true;
            this.cbLocationDataEntry.Location = new System.Drawing.Point(141, 111);
            this.cbLocationDataEntry.Name = "cbLocationDataEntry";
            this.cbLocationDataEntry.Size = new System.Drawing.Size(100, 21);
            this.cbLocationDataEntry.TabIndex = 13;
            // 
            // cbRouteDataEntry
            // 
            this.cbRouteDataEntry.FormattingEnabled = true;
            this.cbRouteDataEntry.Location = new System.Drawing.Point(141, 46);
            this.cbRouteDataEntry.Name = "cbRouteDataEntry";
            this.cbRouteDataEntry.Size = new System.Drawing.Size(287, 21);
            this.cbRouteDataEntry.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(95, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Type*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(98, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Bike*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(90, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Route*";
            // 
            // cbRideTypeDataEntry
            // 
            this.cbRideTypeDataEntry.FormattingEnabled = true;
            this.cbRideTypeDataEntry.Location = new System.Drawing.Point(141, 84);
            this.cbRideTypeDataEntry.Name = "cbRideTypeDataEntry";
            this.cbRideTypeDataEntry.Size = new System.Drawing.Size(100, 21);
            this.cbRideTypeDataEntry.TabIndex = 12;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(59, 668);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "* Required Fields";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbLocationDataEntry);
            this.groupBox3.Controls.Add(this.cbComfortRideDataEntry);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.cbEffortRideDataEntry);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.cbRouteDataEntry);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cbRideTypeDataEntry);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cbBikeDataEntrySelection);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(21, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(532, 147);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
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
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(57, 690);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(133, 13);
            this.label31.TabIndex = 41;
            this.label31.Text = "^ Auto calcualted on Save";
            // 
            // RideDataEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 720);
            this.ControlBox = false;
            this.Controls.Add(this.label31);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btRIdeDataEntryClose);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lbNoLogYearSelected);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RideDataEntry";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ride Data Entry";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RideDataEntryFormClosed);
            this.Load += new System.EventHandler(this.RideDataEntryLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxRetrieveDate.ResumeLayout(false);
            this.groupBoxRetrieveDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRideDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btRIdeDataEntryClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbRideTypeDataEntry;
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
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btClearDataEntry;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.DateTimePicker dtpTimeRideDataEntry;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btImportDataEntry;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbLocationDataEntry;
        public System.Windows.Forms.ComboBox cbLogYearDataEntry;
        public System.Windows.Forms.ComboBox cbRouteDataEntry;
        private CyclingLogDatabaseDataSet cyclingLogDatabaseDataSet;
        private System.Windows.Forms.BindingSource tableRideInformationBindingSource;
        private CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter table_Ride_InformationTableAdapter;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbRideDataEntryError;
        private System.Windows.Forms.TextBox tbRecordID;
        private System.Windows.Forms.Label lbNoLogYearSelected;
        private System.Windows.Forms.Button btDeleteRideDataEntry;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cbEffortRideDataEntry;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tbWeekCountRDE;
        private System.Windows.Forms.Button btRetrieve;
        private System.Windows.Forms.Label lbCustom1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cbComfortRideDataEntry;
        private System.Windows.Forms.TextBox tbCustom1;
        private System.Windows.Forms.TextBox tbCustom2;
        private System.Windows.Forms.Label lbCustom2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbMaxCadence;
        private System.Windows.Forms.GroupBox groupBoxRetrieveDate;
        private System.Windows.Forms.Label label28;
        public System.Windows.Forms.ComboBox cbBikeDataEntrySelection;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btRideDisplayUpdate;
        private System.Windows.Forms.Button btLogEntrySave;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbRideEntryWindChill;
        private System.Windows.Forms.TextBox tbRideEntryTemp;
        private System.Windows.Forms.TextBox tbRideDataEntryWind;
        private System.Windows.Forms.TextBox tbRideDataEntryAvgSpeed;
        private System.Windows.Forms.TextBox tbRideDataEntryDistance;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxCloneEntry;
        private System.Windows.Forms.Label label31;
    }
}