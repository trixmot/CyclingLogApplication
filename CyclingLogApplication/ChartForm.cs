using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            //chart1.Series["Series1"].XValueMember = "Date";
            //chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            //chart1.Series["Series1"].YValueMembers = "AvgSpeed";
            //chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;

            //DataManipulator myDataManip = chart1.DataManipulator;
            //myDataManip.Filter(CompareMethod.EqualTo, 0, "LogYearID");

            checkBoxRouteOption.Checked = false;
            cbRoutesChart.Enabled = false;
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
            
            chart1.Series[0].XValueType = ChartValueType.DateTime;

            chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            HitTestResult result = chart1.HitTest(e.X, e.Y);
            
            if (result.PointIndex > -1 && result.ChartArea != null)
            {
                double xValue = result.Series.Points[result.PointIndex].YValues[0];
                tbYAxis.Text = Math.Round(xValue, 2).ToString();
                tbXAxis.Text = result.Series.Points[result.PointIndex].AxisLabel;
            }
        }

        private void btCloseChart_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
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

        public void chartTest()
        {
            MainForm mainForm = new MainForm();
            int logSetting = mainForm.getLogLevel();
            int logIndex = 1;


            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
            conn.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex, conn);
            //adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Series1"].XValueMember = "Date";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Series1"].YValueMembers = "AvgSpeed";

            chart1.Series["Series1"].Points.AddXY("1/10/16", "15");
            chart1.Series["Series1"].Points.AddXY("2/10/16", "18");

            chart1.Titles.Add("Average Speed Chart");
        }

        private void btRunChart_Click(object sender, EventArgs e)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<string> nameList = new List<string>();
            MainForm mainForm = new MainForm();
            int logSetting = mainForm.getLogLevel();

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();
                SqlCommand cmd = null;
                int logIndex = mainForm.getLogYearIndex(cbLogYearChart.SelectedItem.ToString());

                //Daily:
                if (cbTypeChart.SelectedIndex == 0)
                {
                    if (!checkBoxRouteOption.Checked) {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex, conn);
                    } else
                    {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex, conn);
                    }
                }
                //Weekly:
                else if (cbTypeChart.SelectedIndex == 1)
                {
                    if (!checkBoxRouteOption.Checked)
                    {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex, conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex, conn);
                    }
                }
                //Monthly:
                else if (cbTypeChart.SelectedIndex == 2)
                {
                    if (!checkBoxRouteOption.Checked)
                    {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex, conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex, conn);
                    }
                }

                reader = cmd.ExecuteReader();
                int weekCount = 1;
                int monthCount = 1;
                int recordCount = 0;
                double avg_avgSpeed = 0;
                double total_avgSpeed = 0;
                string recordDate = "";

                chart1.Series[0].Points.Clear();

                // write each record
                while (reader.Read())
                {
                    string date = reader[0].ToString();
                    date = Convert.ToDateTime(date).ToShortDateString();
                    double avgSpeed = Convert.ToDouble(reader[1].ToString());
                    int weekValue = Convert.ToInt32(reader[2].ToString());                
                    //this.chart1.Series["Series1"].Points.AddXY(reader[0].ToString(), reader[1].ToString());

                    //Daily
                    if (cbTypeChart.SelectedIndex == 0)
                    {
                        if (!date.Equals(""))
                        {
                            chart1.Series["Series1"].Points.AddXY(date, avgSpeed.ToString());
                            Logger.Log("Chart Testing: Daily avgSpeed: " + avgSpeed + "::" + date + "::" + date, 1, 0);
                        }
                    }
                    //Weekly:
                    else if (cbTypeChart.SelectedIndex == 1)
                    {
                        if (weekCount != weekValue)
                        {
                            //Run chart series data on previous values:
                            avg_avgSpeed = total_avgSpeed / recordCount;
                            
                            if (!recordDate.Equals(""))
                            {
                                chart1.Series["Series1"].Points.AddXY(recordDate, avg_avgSpeed.ToString());
                                Logger.Log("Chart Testing: Weekly current_avgSpeed: " + avg_avgSpeed + "::" + recordDate + "::" + date, 1, 0);
                            }

                            //Restart values over since starting a new week:
                            recordCount = 1;
                            recordDate = date;
                            total_avgSpeed = avgSpeed;
                            Logger.Log("Chart Testing: Weekly total_avgSpeed: " + total_avgSpeed + "::" + date, 1, 0);
                            weekCount++;
                        }
                        else
                        {
                            if (recordDate.Equals(""))
                            {
                                recordDate = date;
                            }
                            total_avgSpeed = total_avgSpeed + avgSpeed;
                            recordCount++;
                            Logger.Log("Chart Testing: Weekly total_avgSpeed: " + total_avgSpeed + "::" + date, 1, 0);
                        }
                    }
                    //Monthly:
                    else if (cbTypeChart.SelectedIndex == 2)
                    {
                        DateTime datetime = Convert.ToDateTime(date);
                        int month = datetime.Month;

                        if (monthCount != month)
                        {
                            //Run chart series data on previous values:
                            avg_avgSpeed = total_avgSpeed / recordCount;

                            if (!recordDate.Equals(""))
                            {
                                chart1.Series["Series1"].Points.AddXY(recordDate, avg_avgSpeed.ToString());
                                Logger.Log("Chart Testing: Monthly current_avgSpeed: " + avg_avgSpeed + "::" + recordDate + "::" + date, 1, 0);
                            }

                            //Restart values over since starting a new month:
                            recordCount = 1;
                            recordDate = date;
                            total_avgSpeed = avgSpeed;
                            Logger.Log("Chart Testing: Monthly total_avgSpeed: " + total_avgSpeed + "::" + date, 1, 0);
                            monthCount++;
                        } else
                        {
                            Logger.Log("Chart Testing: Monthly total_avgSpeed: " + total_avgSpeed + "::" + date, 1, 0);
                        }
                    }
                }
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            //---------------------
            if (cbTypeChart.SelectedIndex == 0)
            {

            } else if (cbTypeChart.SelectedIndex == 1)
            {

            } else if (cbTypeChart.SelectedIndex == 2)
            {
                // Average Speed Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "AvgSpeed";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            
        }

        private void checkBoxRouteOption_Click(object sender, EventArgs e)
        {
            if (checkBoxRouteOption.Checked == true)
            {
                cbRoutesChart.Enabled = true;
            } else
            {
                cbRoutesChart.Enabled = false;
                cbRoutesChart.SelectedIndex = -1;
            }
        }
    }
}
