using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CyclingLogApplication
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();

            // Average Speed Chart
            chart1.Series["Series1"].XValueMember = "Date";
            chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            chart1.Series["Series1"].YValueMembers = "AvgSpeed";
            chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            //DataManipulator myDataManip = chart1.DataManipulator;
            //myDataManip.Filter(CompareMethod.EqualTo, 0, "LogYearID");
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // NOTE: This line of code loads data into the 'cyclingLogDatabaseDataSet.Table_Ride_Information' table. You can move, or remove it, as needed.
            this.table_Ride_InformationTableAdapter.Fill(this.cyclingLogDatabaseDataSet.Table_Ride_Information);

        }
    }
}
