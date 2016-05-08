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
        public ChartForm(MainForm mainForm)
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

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);

            chart1.ChartAreas[0].CursorX.Interval = 0;
            chart1.ChartAreas[0].CursorY.Interval = 0;

            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            HitTestResult result = chart1.HitTest(e.X, e.Y);
            

            if (result.PointIndex > -1 && result.ChartArea != null)
            {
                tbXAxis.Text = result.Series.Points[result.PointIndex].YValues[0].ToString();
                tbYAxis.Text = DateTime.FromOADate(result.Series.Points[result.PointIndex].XValue).ToString();
            }
        }

        private void btCloseChart_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }

        private void cbTypeChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.setLastTypeChartSelected(cbTypeChart.SelectedIndex);
        }

        private void cbLogYearChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.setLastLogYearChartSelected(cbLogYearChart.SelectedIndex);
        }

        private void cbRoutesChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.setLastRouteChartSelected(cbRoutesChart.SelectedIndex);
        }

        private void rbAllWeeks_MouseClick(object sender, MouseEventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.setLastTimelineChartSelected(0);
        }

        private void rbCurrentWeek_MouseClick(object sender, MouseEventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.setLastTimelineChartSelected(1);
        }
    }
}
