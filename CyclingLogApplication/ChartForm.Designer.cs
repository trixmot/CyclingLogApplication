﻿namespace CyclingLogApplication
{
    partial class ChartForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbXAxis = new System.Windows.Forms.Label();
            this.lbYAxis = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRoutesChart = new System.Windows.Forms.ComboBox();
            this.checkBoxRouteOption = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTypeChartData = new System.Windows.Forms.ComboBox();
            this.gbChartType = new System.Windows.Forms.GroupBox();
            this.rbChartTypeColumn = new System.Windows.Forms.RadioButton();
            this.rbChartTypeLine = new System.Windows.Forms.RadioButton();
            this.rbChartTypeBar = new System.Windows.Forms.RadioButton();
            this.labelChartError = new System.Windows.Forms.Label();
            this.btCloseChart = new System.Windows.Forms.Button();
            this.btRunChart = new System.Windows.Forms.Button();
            this.tbYAxis = new System.Windows.Forms.TextBox();
            this.tbXAxis = new System.Windows.Forms.TextBox();
            this.cbLogYearChart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTypeTime = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbChartTimeRange = new System.Windows.Forms.GroupBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableRideInformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cyclingLogDatabaseDataSet = new CyclingLogApplication.CyclingLogDatabaseDataSet();
            this.table_Ride_InformationTableAdapter = new CyclingLogApplication.CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbChartType.SuspendLayout();
            this.gbChartTimeRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.chart1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            legend1.Title = "Average Speed";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueMember = "Date";
            series1.YValueMembers = "RideDistance";
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(932, 589);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "chart";
            title1.Text = "Ride Charts";
            title1.Visible = false;
            this.chart1.Titles.Add(title1);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart1_MouseMove);
            // 
            // lbXAxis
            // 
            this.lbXAxis.AutoSize = true;
            this.lbXAxis.Location = new System.Drawing.Point(58, 19);
            this.lbXAxis.Name = "lbXAxis";
            this.lbXAxis.Size = new System.Drawing.Size(36, 13);
            this.lbXAxis.TabIndex = 1;
            this.lbXAxis.Text = "X-Axis";
            // 
            // lbYAxis
            // 
            this.lbYAxis.AutoSize = true;
            this.lbYAxis.Location = new System.Drawing.Point(161, 19);
            this.lbYAxis.Name = "lbYAxis";
            this.lbYAxis.Size = new System.Drawing.Size(36, 13);
            this.lbYAxis.TabIndex = 2;
            this.lbYAxis.Text = "Y-Axis";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gbChartTimeRange);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbTypeChartData);
            this.groupBox1.Controls.Add(this.gbChartType);
            this.groupBox1.Controls.Add(this.labelChartError);
            this.groupBox1.Controls.Add(this.btCloseChart);
            this.groupBox1.Controls.Add(this.btRunChart);
            this.groupBox1.Controls.Add(this.tbYAxis);
            this.groupBox1.Controls.Add(this.tbXAxis);
            this.groupBox1.Controls.Add(this.cbLogYearChart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbTypeTime);
            this.groupBox1.Controls.Add(this.lbXAxis);
            this.groupBox1.Controls.Add(this.lbYAxis);
            this.groupBox1.Location = new System.Drawing.Point(973, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 589);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbRoutesChart);
            this.groupBox3.Controls.Add(this.checkBoxRouteOption);
            this.groupBox3.Location = new System.Drawing.Point(17, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 100);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Route Selection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Routes";
            // 
            // cbRoutesChart
            // 
            this.cbRoutesChart.FormattingEnabled = true;
            this.cbRoutesChart.Location = new System.Drawing.Point(9, 69);
            this.cbRoutesChart.Name = "cbRoutesChart";
            this.cbRoutesChart.Size = new System.Drawing.Size(213, 21);
            this.cbRoutesChart.TabIndex = 8;
            this.cbRoutesChart.SelectedIndexChanged += new System.EventHandler(this.CbRoutesChart_SelectedIndexChanged);
            // 
            // checkBoxRouteOption
            // 
            this.checkBoxRouteOption.AutoSize = true;
            this.checkBoxRouteOption.Location = new System.Drawing.Point(18, 24);
            this.checkBoxRouteOption.Name = "checkBoxRouteOption";
            this.checkBoxRouteOption.Size = new System.Drawing.Size(136, 17);
            this.checkBoxRouteOption.TabIndex = 14;
            this.checkBoxRouteOption.Text = "Filter on Specific Route";
            this.checkBoxRouteOption.UseVisualStyleBackColor = true;
            this.checkBoxRouteOption.Click += new System.EventHandler(this.CheckBoxRouteOption_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Data";
            // 
            // cbTypeChartData
            // 
            this.cbTypeChartData.FormattingEnabled = true;
            this.cbTypeChartData.Items.AddRange(new object[] {
            "--Select Value--",
            "Average Speed",
            "Longest",
            "Miles",
            "Highest Ascent",
            "Planner"});
            this.cbTypeChartData.Location = new System.Drawing.Point(60, 186);
            this.cbTypeChartData.Name = "cbTypeChartData";
            this.cbTypeChartData.Size = new System.Drawing.Size(146, 21);
            this.cbTypeChartData.TabIndex = 20;
            this.cbTypeChartData.SelectedIndexChanged += new System.EventHandler(this.CbTypeChartData_SelectedIndexChanged);
            // 
            // gbChartType
            // 
            this.gbChartType.Controls.Add(this.rbChartTypeColumn);
            this.gbChartType.Controls.Add(this.rbChartTypeLine);
            this.gbChartType.Controls.Add(this.rbChartTypeBar);
            this.gbChartType.Location = new System.Drawing.Point(17, 323);
            this.gbChartType.Name = "gbChartType";
            this.gbChartType.Size = new System.Drawing.Size(213, 61);
            this.gbChartType.TabIndex = 19;
            this.gbChartType.TabStop = false;
            this.gbChartType.Text = "Chart Type";
            // 
            // rbChartTypeColumn
            // 
            this.rbChartTypeColumn.AutoSize = true;
            this.rbChartTypeColumn.Location = new System.Drawing.Point(84, 27);
            this.rbChartTypeColumn.Name = "rbChartTypeColumn";
            this.rbChartTypeColumn.Size = new System.Drawing.Size(60, 17);
            this.rbChartTypeColumn.TabIndex = 17;
            this.rbChartTypeColumn.TabStop = true;
            this.rbChartTypeColumn.Text = "Column";
            this.rbChartTypeColumn.UseVisualStyleBackColor = true;
            // 
            // rbChartTypeLine
            // 
            this.rbChartTypeLine.AutoSize = true;
            this.rbChartTypeLine.Location = new System.Drawing.Point(156, 27);
            this.rbChartTypeLine.Name = "rbChartTypeLine";
            this.rbChartTypeLine.Size = new System.Drawing.Size(45, 17);
            this.rbChartTypeLine.TabIndex = 18;
            this.rbChartTypeLine.TabStop = true;
            this.rbChartTypeLine.Text = "Line";
            this.rbChartTypeLine.UseVisualStyleBackColor = true;
            // 
            // rbChartTypeBar
            // 
            this.rbChartTypeBar.AutoSize = true;
            this.rbChartTypeBar.Location = new System.Drawing.Point(19, 27);
            this.rbChartTypeBar.Name = "rbChartTypeBar";
            this.rbChartTypeBar.Size = new System.Drawing.Size(41, 17);
            this.rbChartTypeBar.TabIndex = 16;
            this.rbChartTypeBar.TabStop = true;
            this.rbChartTypeBar.Text = "Bar";
            this.rbChartTypeBar.UseVisualStyleBackColor = true;
            // 
            // labelChartError
            // 
            this.labelChartError.AutoSize = true;
            this.labelChartError.ForeColor = System.Drawing.Color.Red;
            this.labelChartError.Location = new System.Drawing.Point(45, 503);
            this.labelChartError.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelChartError.Name = "labelChartError";
            this.labelChartError.Size = new System.Drawing.Size(165, 26);
            this.labelChartError.TabIndex = 15;
            this.labelChartError.Text = "Select a Log Year option from the dropdown list.";
            this.labelChartError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btCloseChart
            // 
            this.btCloseChart.Location = new System.Drawing.Point(155, 543);
            this.btCloseChart.Name = "btCloseChart";
            this.btCloseChart.Size = new System.Drawing.Size(75, 23);
            this.btCloseChart.TabIndex = 13;
            this.btCloseChart.Text = "Close";
            this.btCloseChart.UseVisualStyleBackColor = true;
            this.btCloseChart.Click += new System.EventHandler(this.BtCloseChart_Click);
            // 
            // btRunChart
            // 
            this.btRunChart.Location = new System.Drawing.Point(48, 543);
            this.btRunChart.Name = "btRunChart";
            this.btRunChart.Size = new System.Drawing.Size(75, 23);
            this.btRunChart.TabIndex = 12;
            this.btRunChart.Text = "Run Chart";
            this.btRunChart.UseVisualStyleBackColor = true;
            this.btRunChart.Click += new System.EventHandler(this.BtRunChart_Click);
            // 
            // tbYAxis
            // 
            this.tbYAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbYAxis.Location = new System.Drawing.Point(142, 36);
            this.tbYAxis.Name = "tbYAxis";
            this.tbYAxis.ReadOnly = true;
            this.tbYAxis.Size = new System.Drawing.Size(88, 26);
            this.tbYAxis.TabIndex = 11;
            this.tbYAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbXAxis
            // 
            this.tbXAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbXAxis.Location = new System.Drawing.Point(35, 36);
            this.tbXAxis.Name = "tbXAxis";
            this.tbXAxis.ReadOnly = true;
            this.tbXAxis.Size = new System.Drawing.Size(88, 26);
            this.tbXAxis.TabIndex = 10;
            this.tbXAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbLogYearChart
            // 
            this.cbLogYearChart.FormattingEnabled = true;
            this.cbLogYearChart.Location = new System.Drawing.Point(60, 90);
            this.cbLogYearChart.Name = "cbLogYearChart";
            this.cbLogYearChart.Size = new System.Drawing.Size(146, 21);
            this.cbLogYearChart.TabIndex = 6;
            this.cbLogYearChart.SelectedIndexChanged += new System.EventHandler(this.CbLogYearChart_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Log Title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Frequency";
            // 
            // cbTypeTime
            // 
            this.cbTypeTime.FormattingEnabled = true;
            this.cbTypeTime.Items.AddRange(new object[] {
            "--Select Value--",
            "Daily",
            "Weekly",
            "Monthly"});
            this.cbTypeTime.Location = new System.Drawing.Point(60, 139);
            this.cbTypeTime.Name = "cbTypeTime";
            this.cbTypeTime.Size = new System.Drawing.Size(146, 21);
            this.cbTypeTime.TabIndex = 3;
            this.cbTypeTime.SelectedIndexChanged += new System.EventHandler(this.CbTypeTimeChart_SelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            // 
            // gbChartTimeRange
            // 
            this.gbChartTimeRange.Controls.Add(this.label6);
            this.gbChartTimeRange.Controls.Add(this.label2);
            this.gbChartTimeRange.Controls.Add(this.dtpToDate);
            this.gbChartTimeRange.Controls.Add(this.dtpFromDate);
            this.gbChartTimeRange.Location = new System.Drawing.Point(17, 390);
            this.gbChartTimeRange.Name = "gbChartTimeRange";
            this.gbChartTimeRange.Size = new System.Drawing.Size(228, 110);
            this.gbChartTimeRange.TabIndex = 23;
            this.gbChartTimeRange.TabStop = false;
            this.gbChartTimeRange.Text = "Date Range";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(19, 33);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(194, 20);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(19, 75);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(194, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "From";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "To";
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
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 613);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.ChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbChartType.ResumeLayout(false);
            this.gbChartType.PerformLayout();
            this.gbChartTimeRange.ResumeLayout(false);
            this.gbChartTimeRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private CyclingLogDatabaseDataSet cyclingLogDatabaseDataSet;
        private System.Windows.Forms.BindingSource tableRideInformationBindingSource;
        private CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter table_Ride_InformationTableAdapter;
        private System.Windows.Forms.Label lbXAxis;
        private System.Windows.Forms.Label lbYAxis;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCloseChart;
        private System.Windows.Forms.Button btRunChart;
        private System.Windows.Forms.TextBox tbYAxis;
        private System.Windows.Forms.TextBox tbXAxis;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbRoutesChart;
        public System.Windows.Forms.ComboBox cbLogYearChart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTypeTime;
        private System.Windows.Forms.CheckBox checkBoxRouteOption;
        private System.Windows.Forms.Label labelChartError;
        private System.Windows.Forms.RadioButton rbChartTypeLine;
        private System.Windows.Forms.RadioButton rbChartTypeColumn;
        private System.Windows.Forms.RadioButton rbChartTypeBar;
        private System.Windows.Forms.GroupBox gbChartType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTypeChartData;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbChartTimeRange;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
    }
}