namespace CyclingLogApplication
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableRideInformationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cyclingLogDatabaseDataSet = new CyclingLogApplication.CyclingLogDatabaseDataSet();
            this.table_Ride_InformationTableAdapter = new CyclingLogApplication.CyclingLogDatabaseDataSetTableAdapters.Table_Ride_InformationTableAdapter();
            this.lbXAxis = new System.Windows.Forms.Label();
            this.lbYAxis = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelChartError = new System.Windows.Forms.Label();
            this.checkBoxRouteOption = new System.Windows.Forms.CheckBox();
            this.btCloseChart = new System.Windows.Forms.Button();
            this.btRunChart = new System.Windows.Forms.Button();
            this.tbYAxis = new System.Windows.Forms.TextBox();
            this.tbXAxis = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRoutesChart = new System.Windows.Forms.ComboBox();
            this.cbLogYearChart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTypeData = new System.Windows.Forms.ComboBox();
            this.rbChartTypeBar = new System.Windows.Forms.RadioButton();
            this.rbChartTypeColumn = new System.Windows.Forms.RadioButton();
            this.rbChartTypeLine = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            legend1.Title = "Average Speed";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(251, 12);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueMember = "Date";
            series1.YValueMembers = "RideDistance";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(791, 562);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Name = "chart";
            title1.Text = "Log Chart";
            this.chart1.Titles.Add(title1);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
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
            // lbXAxis
            // 
            this.lbXAxis.AutoSize = true;
            this.lbXAxis.Location = new System.Drawing.Point(19, 26);
            this.lbXAxis.Name = "lbXAxis";
            this.lbXAxis.Size = new System.Drawing.Size(36, 13);
            this.lbXAxis.TabIndex = 1;
            this.lbXAxis.Text = "X-Axis";
            // 
            // lbYAxis
            // 
            this.lbYAxis.AutoSize = true;
            this.lbYAxis.Location = new System.Drawing.Point(119, 26);
            this.lbYAxis.Name = "lbYAxis";
            this.lbYAxis.Size = new System.Drawing.Size(36, 13);
            this.lbYAxis.TabIndex = 2;
            this.lbYAxis.Text = "Y-Axis";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.labelChartError);
            this.groupBox1.Controls.Add(this.checkBoxRouteOption);
            this.groupBox1.Controls.Add(this.btCloseChart);
            this.groupBox1.Controls.Add(this.btRunChart);
            this.groupBox1.Controls.Add(this.tbYAxis);
            this.groupBox1.Controls.Add(this.tbXAxis);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbRoutesChart);
            this.groupBox1.Controls.Add(this.cbLogYearChart);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbTypeData);
            this.groupBox1.Controls.Add(this.lbXAxis);
            this.groupBox1.Controls.Add(this.lbYAxis);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 562);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // labelChartError
            // 
            this.labelChartError.AutoSize = true;
            this.labelChartError.ForeColor = System.Drawing.Color.Red;
            this.labelChartError.Location = new System.Drawing.Point(32, 422);
            this.labelChartError.MaximumSize = new System.Drawing.Size(200, 0);
            this.labelChartError.Name = "labelChartError";
            this.labelChartError.Size = new System.Drawing.Size(165, 26);
            this.labelChartError.TabIndex = 15;
            this.labelChartError.Text = "Select a Log Year option from the dropdown list.";
            this.labelChartError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxRouteOption
            // 
            this.checkBoxRouteOption.AutoSize = true;
            this.checkBoxRouteOption.Location = new System.Drawing.Point(22, 202);
            this.checkBoxRouteOption.Name = "checkBoxRouteOption";
            this.checkBoxRouteOption.Size = new System.Drawing.Size(96, 17);
            this.checkBoxRouteOption.TabIndex = 14;
            this.checkBoxRouteOption.Text = "Specific Route";
            this.checkBoxRouteOption.UseVisualStyleBackColor = true;
            this.checkBoxRouteOption.Click += new System.EventHandler(this.checkBoxRouteOption_Click);
            // 
            // btCloseChart
            // 
            this.btCloseChart.Location = new System.Drawing.Point(68, 504);
            this.btCloseChart.Name = "btCloseChart";
            this.btCloseChart.Size = new System.Drawing.Size(75, 23);
            this.btCloseChart.TabIndex = 13;
            this.btCloseChart.Text = "Close";
            this.btCloseChart.UseVisualStyleBackColor = true;
            this.btCloseChart.Click += new System.EventHandler(this.btCloseChart_Click);
            // 
            // btRunChart
            // 
            this.btRunChart.Location = new System.Drawing.Point(68, 464);
            this.btRunChart.Name = "btRunChart";
            this.btRunChart.Size = new System.Drawing.Size(75, 23);
            this.btRunChart.TabIndex = 12;
            this.btRunChart.Text = "Run Chart";
            this.btRunChart.UseVisualStyleBackColor = true;
            this.btRunChart.Click += new System.EventHandler(this.btRunChart_Click);
            // 
            // tbYAxis
            // 
            this.tbYAxis.Location = new System.Drawing.Point(122, 51);
            this.tbYAxis.Name = "tbYAxis";
            this.tbYAxis.ReadOnly = true;
            this.tbYAxis.Size = new System.Drawing.Size(75, 20);
            this.tbYAxis.TabIndex = 11;
            this.tbYAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbXAxis
            // 
            this.tbXAxis.Location = new System.Drawing.Point(22, 51);
            this.tbXAxis.Name = "tbXAxis";
            this.tbXAxis.ReadOnly = true;
            this.tbXAxis.Size = new System.Drawing.Size(75, 20);
            this.tbXAxis.TabIndex = 10;
            this.tbXAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Routes";
            // 
            // cbRoutesChart
            // 
            this.cbRoutesChart.FormattingEnabled = true;
            this.cbRoutesChart.Location = new System.Drawing.Point(22, 251);
            this.cbRoutesChart.Name = "cbRoutesChart";
            this.cbRoutesChart.Size = new System.Drawing.Size(175, 21);
            this.cbRoutesChart.TabIndex = 8;
            this.cbRoutesChart.SelectedIndexChanged += new System.EventHandler(this.cbRoutesChart_SelectedIndexChanged);
            // 
            // cbLogYearChart
            // 
            this.cbLogYearChart.FormattingEnabled = true;
            this.cbLogYearChart.Location = new System.Drawing.Point(22, 160);
            this.cbLogYearChart.Name = "cbLogYearChart";
            this.cbLogYearChart.Size = new System.Drawing.Size(146, 21);
            this.cbLogYearChart.TabIndex = 6;
            this.cbLogYearChart.SelectedIndexChanged += new System.EventHandler(this.cbLogYearChart_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Log Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data";
            // 
            // cbTypeData
            // 
            this.cbTypeData.FormattingEnabled = true;
            this.cbTypeData.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly"});
            this.cbTypeData.Location = new System.Drawing.Point(22, 111);
            this.cbTypeData.Name = "cbTypeData";
            this.cbTypeData.Size = new System.Drawing.Size(146, 21);
            this.cbTypeData.TabIndex = 3;
            this.cbTypeData.SelectedIndexChanged += new System.EventHandler(this.cbTypeChart_SelectedIndexChanged);
            // 
            // rbChartTypeBar
            // 
            this.rbChartTypeBar.AutoSize = true;
            this.rbChartTypeBar.Location = new System.Drawing.Point(32, 23);
            this.rbChartTypeBar.Name = "rbChartTypeBar";
            this.rbChartTypeBar.Size = new System.Drawing.Size(41, 17);
            this.rbChartTypeBar.TabIndex = 16;
            this.rbChartTypeBar.TabStop = true;
            this.rbChartTypeBar.Text = "Bar";
            this.rbChartTypeBar.UseVisualStyleBackColor = true;
            // 
            // rbChartTypeColumn
            // 
            this.rbChartTypeColumn.AutoSize = true;
            this.rbChartTypeColumn.Location = new System.Drawing.Point(32, 46);
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
            this.rbChartTypeLine.Location = new System.Drawing.Point(32, 69);
            this.rbChartTypeLine.Name = "rbChartTypeLine";
            this.rbChartTypeLine.Size = new System.Drawing.Size(45, 17);
            this.rbChartTypeLine.TabIndex = 18;
            this.rbChartTypeLine.TabStop = true;
            this.rbChartTypeLine.Text = "Line";
            this.rbChartTypeLine.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbChartTypeColumn);
            this.groupBox2.Controls.Add(this.rbChartTypeLine);
            this.groupBox2.Controls.Add(this.rbChartTypeBar);
            this.groupBox2.Location = new System.Drawing.Point(22, 294);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 100);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chart Type";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 587);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChartForm";
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.ChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tableRideInformationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cyclingLogDatabaseDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbTypeData;
        private System.Windows.Forms.CheckBox checkBoxRouteOption;
        private System.Windows.Forms.Label labelChartError;
        private System.Windows.Forms.RadioButton rbChartTypeLine;
        private System.Windows.Forms.RadioButton rbChartTypeColumn;
        private System.Windows.Forms.RadioButton rbChartTypeBar;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}