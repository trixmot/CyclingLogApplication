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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cbFilterField = new System.Windows.Forms.ComboBox();
            this.tbFilterText = new System.Windows.Forms.TextBox();
            this.Field = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLogYearFilter = new System.Windows.Forms.ComboBox();
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
            this.cbFilterField.FormattingEnabled = true;
            this.cbFilterField.Items.AddRange(new object[] {
            "Bike",
            "Location",
            "RideType",
            "Route",
            "WeekNumber"});
            this.cbFilterField.Location = new System.Drawing.Point(181, 27);
            this.cbFilterField.Name = "cbFilterField";
            this.cbFilterField.Size = new System.Drawing.Size(121, 21);
            this.cbFilterField.TabIndex = 2;
            // 
            // tbFilterText
            // 
            this.tbFilterText.Location = new System.Drawing.Point(338, 28);
            this.tbFilterText.Name = "tbFilterText";
            this.tbFilterText.Size = new System.Drawing.Size(237, 20);
            this.tbFilterText.TabIndex = 3;
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
            this.bFilter.Location = new System.Drawing.Point(601, 25);
            this.bFilter.Name = "bFilter";
            this.bFilter.Size = new System.Drawing.Size(75, 23);
            this.bFilter.TabIndex = 6;
            this.bFilter.Text = "Filter";
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
            this.cbLogYearFilter.FormattingEnabled = true;
            this.cbLogYearFilter.Items.AddRange(new object[] {
            "All Logs"});
            this.cbLogYearFilter.Location = new System.Drawing.Point(23, 27);
            this.cbLogYearFilter.Name = "cbLogYearFilter";
            this.cbLogYearFilter.Size = new System.Drawing.Size(131, 21);
            this.cbLogYearFilter.TabIndex = 7;
            // 
            // RideDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 596);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbLogYearFilter);
            this.Controls.Add(this.bFilter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.tbFilterText);
            this.Controls.Add(this.cbFilterField);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "RideDataDisplay";
            this.Text = "Ride Data Information";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbFilterField;
        private System.Windows.Forms.TextBox tbFilterText;
        private System.Windows.Forms.Label Field;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bFilter;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbLogYearFilter;
    }
}