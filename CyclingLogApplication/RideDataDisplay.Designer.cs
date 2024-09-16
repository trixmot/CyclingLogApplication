namespace CyclingLogApplication
{
    partial class RideDataDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RideDataDisplay));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cbFilterField = new System.Windows.Forms.ComboBox();
            this.Field = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLogYearFilter = new System.Windows.Forms.ComboBox();
            this.btClear = new System.Windows.Forms.Button();
            this.cbFilterValue = new System.Windows.Forms.ComboBox();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.btUpdateFields = new System.Windows.Forms.Button();
            this.btSelectAll = new System.Windows.Forms.Button();
            this.btDeselectAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(993, 492);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(944, 561);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CloseForm);
            // 
            // cbFilterField
            // 
            this.cbFilterField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterField.FormattingEnabled = true;
            this.cbFilterField.Items.AddRange(new object[] {
            "NONE",
            "Bike",
            "Location",
            "Longest",
            "RideType",
            "Route",
            "Temperature",
            "WeekNumber"});
            this.cbFilterField.Location = new System.Drawing.Point(181, 27);
            this.cbFilterField.Name = "cbFilterField";
            this.cbFilterField.Size = new System.Drawing.Size(121, 21);
            this.cbFilterField.TabIndex = 2;
            this.cbFilterField.SelectedIndexChanged += new System.EventHandler(this.cbFilterFieldChanged);
            // 
            // Field
            // 
            this.Field.AutoSize = true;
            this.Field.Location = new System.Drawing.Point(178, 9);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(54, 13);
            this.Field.TabIndex = 4;
            this.Field.Text = "Filter Field";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter Value";
            // 
            // bFilter
            // 
            this.bFilter.Location = new System.Drawing.Point(694, 24);
            this.bFilter.Name = "bFilter";
            this.bFilter.Size = new System.Drawing.Size(75, 23);
            this.bFilter.TabIndex = 6;
            this.bFilter.Text = "Run";
            this.bFilter.UseVisualStyleBackColor = true;
            this.bFilter.Click += new System.EventHandler(this.bFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Log Year";
            // 
            // cbLogYearFilter
            // 
            this.cbLogYearFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLogYearFilter.FormattingEnabled = true;
            this.cbLogYearFilter.Items.AddRange(new object[] {
            "All Logs"});
            this.cbLogYearFilter.Location = new System.Drawing.Point(23, 27);
            this.cbLogYearFilter.Name = "cbLogYearFilter";
            this.cbLogYearFilter.Size = new System.Drawing.Size(131, 21);
            this.cbLogYearFilter.TabIndex = 7;
            this.cbLogYearFilter.SelectedIndexChanged += new System.EventHandler(this.cbLogYearFilter_SelectedIndexChanged);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(775, 25);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 9;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // cbFilterValue
            // 
            this.cbFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterValue.FormattingEnabled = true;
            this.cbFilterValue.Items.AddRange(new object[] {
            "NONE",
            "Bike",
            "Location",
            "RideType",
            "Route",
            "WeekNumber"});
            this.cbFilterValue.Location = new System.Drawing.Point(338, 28);
            this.cbFilterValue.Name = "cbFilterValue";
            this.cbFilterValue.Size = new System.Drawing.Size(316, 21);
            this.cbFilterValue.TabIndex = 10;
            // 
            // checkedListBox
            // 
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Items.AddRange(new object[] {
            "Week Number",
            "ID",
            "Date",
            "Moving Time",
            "Ride Distance",
            "Avg Speed",
            "Bike",
            "Ride Type",
            "Wind",
            "Temperature",
            "Avg Cadence",
            "Avg Heart Rate",
            "Max Heart Rate",
            "Calories",
            "Total Ascent",
            "Total Descent",
            "Route",
            "Location",
            "Comments",
            "Effort",
            "Max Speed",
            "Avg Power",
            "Max Power"});
            this.checkedListBox.Location = new System.Drawing.Point(1033, 41);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(146, 349);
            this.checkedListBox.TabIndex = 12;
            // 
            // btUpdateFields
            // 
            this.btUpdateFields.Location = new System.Drawing.Point(1074, 497);
            this.btUpdateFields.Name = "btUpdateFields";
            this.btUpdateFields.Size = new System.Drawing.Size(75, 23);
            this.btUpdateFields.TabIndex = 13;
            this.btUpdateFields.Text = "Update";
            this.btUpdateFields.UseVisualStyleBackColor = true;
            this.btUpdateFields.Click += new System.EventHandler(this.btUpdateFields_Click);
            // 
            // btSelectAll
            // 
            this.btSelectAll.Location = new System.Drawing.Point(1074, 402);
            this.btSelectAll.Name = "btSelectAll";
            this.btSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btSelectAll.TabIndex = 14;
            this.btSelectAll.Text = "Select All";
            this.btSelectAll.UseVisualStyleBackColor = true;
            this.btSelectAll.Click += new System.EventHandler(this.btSelectAll_Click);
            // 
            // btDeselectAll
            // 
            this.btDeselectAll.Location = new System.Drawing.Point(1074, 431);
            this.btDeselectAll.Name = "btDeselectAll";
            this.btDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btDeselectAll.TabIndex = 15;
            this.btDeselectAll.Text = "Deselect All";
            this.btDeselectAll.UseVisualStyleBackColor = true;
            this.btDeselectAll.Click += new System.EventHandler(this.btDeselectAll_Click);
            // 
            // RideDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 596);
            this.Controls.Add(this.btDeselectAll);
            this.Controls.Add(this.btSelectAll);
            this.Controls.Add(this.btUpdateFields);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.cbFilterValue);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLogYearFilter);
            this.Controls.Add(this.bFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.cbFilterField);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RideDataDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ride Data Information";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbFilterField;
        private System.Windows.Forms.Label Field;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bFilter;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbLogYearFilter;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.ComboBox cbFilterValue;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.Button btUpdateFields;
        private System.Windows.Forms.Button btSelectAll;
        private System.Windows.Forms.Button btDeselectAll;
    }
}