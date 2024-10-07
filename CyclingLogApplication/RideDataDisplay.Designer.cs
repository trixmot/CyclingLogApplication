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
            this.btPrint = new System.Windows.Forms.Button();
            this.cbUpdateValues = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1079, 583);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1156, 629);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 33);
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
            this.cbFilterField.SelectedIndexChanged += new System.EventHandler(this.CbFilterFieldChanged);
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
            this.bFilter.Location = new System.Drawing.Point(1001, 25);
            this.bFilter.Name = "bFilter";
            this.bFilter.Size = new System.Drawing.Size(75, 23);
            this.bFilter.TabIndex = 6;
            this.bFilter.Text = "Run";
            this.bFilter.UseVisualStyleBackColor = true;
            this.bFilter.Click += new System.EventHandler(this.BFilter_Click);
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
            this.cbLogYearFilter.SelectedIndexChanged += new System.EventHandler(this.CbLogYearFilter_SelectedIndexChanged);
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(775, 25);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 9;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.BtClear_Click);
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
            "Moving Time",
            "Ride Distance",
            "Avg Speed",
            "Bike",
            "Ride Type",
            "Wind",
            "Temperature",
            "Avg Cadence",
            "Max Cadence",
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
            "Max Power",
            "Comfort",
            "Custom1",
            "Custom2"});
            this.checkedListBox.Location = new System.Drawing.Point(1124, 54);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(146, 379);
            this.checkedListBox.TabIndex = 12;
            // 
            // btUpdateFields
            // 
            this.btUpdateFields.Location = new System.Drawing.Point(1163, 541);
            this.btUpdateFields.Name = "btUpdateFields";
            this.btUpdateFields.Size = new System.Drawing.Size(75, 23);
            this.btUpdateFields.TabIndex = 13;
            this.btUpdateFields.Text = "Update";
            this.btUpdateFields.UseVisualStyleBackColor = true;
            this.btUpdateFields.Click += new System.EventHandler(this.BtUpdateFields_Click);
            // 
            // btSelectAll
            // 
            this.btSelectAll.Location = new System.Drawing.Point(1163, 483);
            this.btSelectAll.Name = "btSelectAll";
            this.btSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btSelectAll.TabIndex = 14;
            this.btSelectAll.Text = "Select All";
            this.btSelectAll.UseVisualStyleBackColor = true;
            this.btSelectAll.Click += new System.EventHandler(this.BtSelectAll_Click);
            // 
            // btDeselectAll
            // 
            this.btDeselectAll.Location = new System.Drawing.Point(1163, 512);
            this.btDeselectAll.Name = "btDeselectAll";
            this.btDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btDeselectAll.TabIndex = 15;
            this.btDeselectAll.Text = "Deselect All";
            this.btDeselectAll.UseVisualStyleBackColor = true;
            this.btDeselectAll.Click += new System.EventHandler(this.BtDeselectAll_Click);
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(694, 25);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(75, 23);
            this.btPrint.TabIndex = 16;
            this.btPrint.Text = "Print";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.BtPrint_Click);
            // 
            // cbUpdateValues
            // 
            this.cbUpdateValues.AutoSize = true;
            this.cbUpdateValues.Location = new System.Drawing.Point(869, 27);
            this.cbUpdateValues.Name = "cbUpdateValues";
            this.cbUpdateValues.Size = new System.Drawing.Size(96, 17);
            this.cbUpdateValues.TabIndex = 17;
            this.cbUpdateValues.Text = "Update Values";
            this.cbUpdateValues.UseVisualStyleBackColor = true;
            this.cbUpdateValues.CheckedChanged += new System.EventHandler(this.cbUpdateValues_CheckedChanged);
            // 
            // RideDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 679);
            this.Controls.Add(this.cbUpdateValues);
            this.Controls.Add(this.btPrint);
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
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.CheckBox cbUpdateValues;
    }
}