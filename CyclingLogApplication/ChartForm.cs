using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CyclingLogApplication
{
    public partial class ChartForm : Form
    {
        private static SqlConnection sqlConnection;// = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
        //BackgroundWorker bgw = new BackgroundWorker();

        public ChartForm()
        {
            InitializeComponent();
            labelChartError.Hide();
            //MainForm mainForm = new MainForm("");
            sqlConnection = MainForm.GetsqlConnectionString();
            //chart1.Series["Series1"].XValueMember = "Date";
            //chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            //chart1.Series["Series1"].YValueMembers = "AvgSpeed";
            //chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;

            //DataManipulator myDataManip = chart1.DataManipulator;
            //myDataManip.Filter(CompareMethod.EqualTo, 0, "LogYearID");

            checkBoxRouteOption.Checked = false;
            cbRoutesChart.Enabled = false;
            rbChartTypeColumn.Checked = true;

            //MainForm mainform = new MainForm();
            List<string> logList = MainForm.ReadDataNames("Table_Log_year", "Name");

            cbLogYearChart.Items.Add("--Select Value--");
            for (int i = 0; i < logList.Count; i++)
            {
                cbLogYearChart.Items.Add(logList[i]);
            }

            List<string> routeList = MainForm.ReadDataNames("Table_Routes", "Name");

            for (int i = 0; i < routeList.Count; i++)
            {
                cbRoutesChart.Items.Add(routeList[i]);
            }
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // NOTE: This line of code loads data into the 'cyclingLogDatabaseDataSet.Table_Ride_Information' table. You can move, or remove it, as needed.
            this.table_Ride_InformationTableAdapter.Fill(this.cyclingLogDatabaseDataSet.Table_Ride_Information);

            //MainForm mainForm = new MainForm();
            try
            {
                cbLogYearChart.SelectedIndex = MainForm.GetLastLogYearChartSelected();
                cbRoutesChart.SelectedIndex = MainForm.GetLastRouteChartSelected();
                cbTypeChartData.SelectedIndex = MainForm.GetLastTypeChartSelected();
                cbTypeTime.SelectedIndex = MainForm.GetLastTypeTimeChartSelected();
            }
            catch
            {
                cbLogYearChart.SelectedIndex = 0;
                cbRoutesChart.SelectedIndex = -1;
                cbTypeChartData.SelectedIndex = -1;
                cbTypeTime.SelectedIndex = -1;
            }
            
        }

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
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

        private void BtCloseChart_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }

        private void CbLogYearChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelChartError.Hide();
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastLogYearChartSelected(cbLogYearChart.SelectedIndex);
            }
        }

        private void CbRoutesChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelChartError.Hide();
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastRouteChartSelected(cbRoutesChart.SelectedIndex);
            }
        }

        public void ChartTest()
        {
            using (MainForm mainForm = new MainForm(""))
            {
                int logSetting = MainForm.GetLogLevel();
            }
            //int logIndex = 1;

            //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
            sqlConnection.Open();
            DataSet ds = new DataSet();
            //SqlDataAdapter adapt = new SqlDataAdapter("SELECT Date, AvgSpeed, WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex, sqlConnection);
            //adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Series1"].XValueMember = "Date";
            //set the member columns of the chart data source used to data bind to the X-values of the series  
            chart1.Series["Series1"].YValueMembers = "AvgSpeed";

            chart1.Series["Series1"].Points.AddXY("1/10/16", "15");
            chart1.Series["Series1"].Points.AddXY("2/10/16", "18");

            chart1.Titles.Add("Average Speed Chart");
            //chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
        }

        private void BtRunChart_Click(object sender, EventArgs e)
        {
            string chartDataType;
            bool averageDataType = false;

            //Verify the required values are available before running chart:
            if (cbTypeTime.SelectedIndex == -1)
            {
                labelChartError.Text = "Select a Chart time option from the dropdown list.";
                labelChartError.Show();

                return;
            }
            if (cbLogYearChart.SelectedIndex < 1)
            {
                labelChartError.Text = "Select a Log Year option from the dropdown list.";
                labelChartError.Show();

                return;
            }
            if (cbRoutesChart.SelectedIndex == -1 && checkBoxRouteOption.Checked)
            {
                labelChartError.Text = "Select a Route option from the dropdown list.";
                labelChartError.Show();

                return;
            }

            if (cbTypeChartData.SelectedIndex < 1)
            {
                labelChartError.Text = "Select a Chart data type from the dropdown list.";
                labelChartError.Show();

                return;
            }

            // conn and reader declared outside try block for visibility in finally block
            //SqlConnection conn = null;
            SqlDataReader reader = null;
            int logIndex;
            int logSetting;

            using (MainForm mainForm = new MainForm(""))
            {
                logSetting = MainForm.GetLogLevel();
                logIndex = MainForm.GetLogYearIndex(cbLogYearChart.SelectedItem.ToString());
            }

            //Average Speed:
            if (cbTypeChartData.SelectedIndex == 1)
            {
                chartDataType = "AvgSpeed";
                averageDataType = true;
                lbYAxis.Text = "AvgSpeed";
            }
            //Longest:
            else if (cbTypeChartData.SelectedIndex == 2)
            {
                //using (ProgressBar progressBar = new ProgressBar())
                //{
                //    progressBar.Show();

                    //Weekly:
                    if (cbTypeTime.SelectedIndex == 2)
                    {
                        GetMonthlyHighMileageWeekNumber(logIndex);
                    }
                    //Monthly:
                    else if (cbTypeTime.SelectedIndex == 3)
                    {
                        RunLongestRideChartMonthly(logIndex);
                    }

                  //  progressBar.Hide();
                //}

                lbYAxis.Text = "Miles";

                return;
                //chartDataType = "RideDistance";
            }
            //Miles:
            else
            {
                chartDataType = "RideDistance";
                lbYAxis.Text = "Miles";
            }

            SqlCommand cmd = null;

            try
            {
                // instantiate and open connection
                //conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");

                //Daily:
                if (cbTypeTime.SelectedIndex == 1)
                {
                    if (!checkBoxRouteOption.Checked)
                    {
                        cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                    }

                    lbXAxis.Text = "Day";
                }
                //Weekly:
                else if (cbTypeTime.SelectedIndex == 2)
                {
                    if (!checkBoxRouteOption.Checked)
                    {
                        if (cbTypeChartData.SelectedIndex == 2)
                        {
                            cmd = new SqlCommand("SELECT Date, MAX(RideDistance), WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                    }
                    else
                    {
                        if (cbTypeChartData.SelectedIndex == 2)
                        {
                            cmd = new SqlCommand("SELECT Date, MAX(RideDistance), WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                    }

                    lbXAxis.Text = "Week";
                }
                //Monthly:
                else if (cbTypeTime.SelectedIndex == 3)
                {
                    if (!checkBoxRouteOption.Checked)
                    {
                        if (cbTypeChartData.SelectedIndex == 2)
                        {
                            cmd = new SqlCommand("SELECT Date, MAX(RideDistance), WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                    }
                    else
                    {
                        if (cbTypeChartData.SelectedIndex == 2)
                        {
                            cmd = new SqlCommand("SELECT Date, MAX(RideDistance), WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                        else
                        {
                            cmd = new SqlCommand("SELECT Date, " + chartDataType + ", WeekNumber FROM Table_Ride_Information WHERE Route='" + cbRoutesChart.SelectedItem + "' and LogYearID=" + logIndex + " ORDER BY Date", sqlConnection);
                        }
                    }

                    lbXAxis.Text = "Month";
                }

                sqlConnection.Open();
                reader = cmd.ExecuteReader();
                int weekCount = 1;
                int monthCount = 1;
                int recordCount = 1;
                double total_value = 0;
                double avg_value = 0;

                chart1.Series[0].Points.Clear();

                if (rbChartTypeBar.Checked)
                {
                    chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
                }
                else if (rbChartTypeColumn.Checked)
                {
                    chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                }
                else
                {
                    chart1.Series["Series1"].ChartType = SeriesChartType.Line;
                }

                string date = "";
                Boolean recordFound = false;

                // write each record
                while (reader.Read())
                {
                    date = reader[0].ToString();
                    date = Convert.ToDateTime(date).ToShortDateString();
                    double chartDataTypeValue = Convert.ToDouble(reader[1].ToString());
                    int weekValue = Convert.ToInt32(reader[2].ToString());
                    //this.chart1.Series["Series1"].Points.AddXY(reader[0].ToString(), reader[1].ToString());

                    //Daily
                    if (cbTypeTime.SelectedIndex == 1)
                    {
                        if (!date.Equals(""))
                        {
                            chart1.Series["Series1"].Points.AddXY(date, chartDataTypeValue.ToString());
                            Logger.Log("Chart Testing: Daily values: " + chartDataTypeValue + "::" + date, logSetting, 1);
                        }
                    }
                    //Weekly:
                    else if (cbTypeTime.SelectedIndex == 2)
                    {
                        if (weekCount != weekValue)
                        {
                            if (averageDataType)
                            {
                                if (total_value != 0)
                                {
                                    avg_value = total_value / recordCount;
                                    chart1.Series["Series1"].Points.AddXY(date, avg_value.ToString());
                                }
                            }
                            else
                            {
                                if (total_value != 0)
                                {
                                    chart1.Series["Series1"].Points.AddXY(date, total_value.ToString());
                                }
                            }

                            //Restart values over since starting a new week:
                            recordCount = 1;
                            total_value = chartDataTypeValue;
                            weekCount = weekValue;
                        }
                        else
                        {
                            total_value += chartDataTypeValue;
                            recordCount++;
                            recordFound = true;
                        }

                        Logger.Log("Chart Testing: Weekly values: " + total_value + "::" + date, logSetting, 1);
                    }
                    //Monthly:
                    else if (cbTypeTime.SelectedIndex == 3)
                    {
                        DateTime datetime = Convert.ToDateTime(date);
                        int month = datetime.Month;

                        if (monthCount != month)
                        {
                            if (averageDataType)
                            {
                                if (total_value != 0)
                                {
                                    avg_value = total_value / recordCount;
                                    chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), avg_value.ToString());
                                    chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                    chart1.Series["Series1"].IsValueShownAsLabel = true;
                                }
                            }
                            else
                            {
                                if (total_value != 0)
                                {
                                    chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), total_value.ToString());
                                    chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                    chart1.Series["Series1"].IsValueShownAsLabel = true;
                                }

                            }
                            //Restart values over since starting a new month:
                            recordCount = 1;
                            total_value = chartDataTypeValue;
                            monthCount = month;
                        }
                        else
                        {
                            total_value += chartDataTypeValue;
                            recordCount++;
                            recordFound = true;
                        }

                        Logger.Log("Chart Testing: Monthly values: " + total_value + "::" + date, logSetting, 1);
                    }
                    chart1.ChartAreas[0].AxisX.Interval = 1;
                }

                //Weekly:
                if (recordFound && cbTypeTime.SelectedIndex == 2)
                {
                    if (averageDataType)
                    {
                        if (total_value != 0)
                        {
                            avg_value = total_value / recordCount;
                            chart1.Series["Series1"].Points.AddXY(date, avg_value.ToString());
                            chart1.Series["Series1"].IsValueShownAsLabel = true;
                        }
                    }
                    else
                    {
                        if (total_value != 0)
                        {
                            chart1.Series["Series1"].Points.AddXY(date, total_value.ToString());
                            chart1.Series["Series1"].IsValueShownAsLabel = true;
                        }
                    }
                }
                //Monthly:
                else if (recordFound && cbTypeTime.SelectedIndex == 3)
                {
                    if (averageDataType)
                    {
                        if (total_value != 0)
                        {
                            avg_value = total_value / recordCount;
                            chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), avg_value.ToString());
                            chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                            chart1.Series["Series1"].IsValueShownAsLabel = true;
                        }
                    }
                    else
                    {
                        if (total_value != 0)
                        {
                            chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), total_value.ToString());
                            chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                            chart1.Series["Series1"].IsValueShownAsLabel = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to run a chart." + ex.Message.ToString());
            }
            finally
            {
                // close reader
                reader?.Close();

                // close connection
                sqlConnection?.Close();

                cmd.Dispose();
            }

            //---------------------
            if (cbTypeTime.SelectedIndex == 1)
            {

            }
            else if (cbTypeTime.SelectedIndex == 2)
            {

            }
            else if (cbTypeTime.SelectedIndex == 3)
            {
                // Average Speed Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "AvgSpeed";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
        }

        private void RunLongestRideChartMonthly(int logYearIndex)
        {
            //Get the logyear from the index:
            int logYear = GetLogYearForSelectedLog(logYearIndex);

            string point1 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 1).ToString();
            string point2 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 2).ToString();
            string point3 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 3).ToString();
            string point4 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 4).ToString();
            string point5 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 5).ToString();
            string point6 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 6).ToString();
            string point7 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 7).ToString();
            string point8 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 8).ToString();
            string point9 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 9).ToString();
            string point10 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 10).ToString();
            string point11 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 11).ToString();
            string point12 = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 12).ToString();

            chart1.Series[0].Points.Clear();

            chart1.Series["Series1"].Points.AddXY("1/1/" + logYear, point1.ToString());
            chart1.Series["Series1"].Points.AddXY("2/1/" + logYear, point2.ToString());
            chart1.Series["Series1"].Points.AddXY("3/1/" + logYear, point3.ToString());
            chart1.Series["Series1"].Points.AddXY("4/1/" + logYear, point4.ToString());
            chart1.Series["Series1"].Points.AddXY("5/1/" + logYear, point5.ToString());
            chart1.Series["Series1"].Points.AddXY("6/1/" + logYear, point6.ToString());
            chart1.Series["Series1"].Points.AddXY("7/1/" + logYear, point7.ToString());
            chart1.Series["Series1"].Points.AddXY("8/1/" + logYear, point8.ToString());
            chart1.Series["Series1"].Points.AddXY("9/1/" + logYear, point9.ToString());
            chart1.Series["Series1"].Points.AddXY("10/1/" + logYear, point10.ToString());
            chart1.Series["Series1"].Points.AddXY("11/1/" + logYear, point11.ToString());
            chart1.Series["Series1"].Points.AddXY("12/1/" + logYear, point12.ToString());

            chart1.ChartAreas[0].AxisX.Interval = 1;
        }

        private static int GetLogYearForSelectedLog(int logIndex)
        {
            List<object> objectValues = new List<object>
            {
                logIndex
            };
            int returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Log_Year_Get_By_Index", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = 0;
                        }
                        else
                        {
                            returnValue = int.Parse(temp);
                        }
                    }
                }
            }

            return returnValue;
        }

        private static double GetMaxHighMileageMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                month
            };
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMaxHighMileage_Monthly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = 0;
                        }
                        else
                        {
                            returnValue = double.Parse(temp);
                        }
                    }
                }
            }

            return returnValue;
        }

        public void GetMonthlyHighMileageWeekNumber(int LogYearID)
        {
            //List<double> rideDistanceList = new List<double>();
            int weekNumber;
            int weekNumberTmp = 0;
            double weeklyMaxTmp;
            double weeklyMax = 0;
            chart1.Series[0].Points.Clear();

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber,Date FROM Table_Ride_Information WHERE " + LogYearID + "=[LogYearID]";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            weekNumber = (int)reader["WeekNumber"];
                            //Check if on a different week:
                            if (weekNumber > weekNumberTmp)
                            {
                                weekNumberTmp = weekNumber;
                                chart1.Series["Series1"].Points.AddXY(reader["Date"], weeklyMax.ToString());

                                // Onto a new week, so reset weekly total:
                                weeklyMax = (double)reader["RideDistance"];
                            }
                            else
                            {
                                weeklyMaxTmp = (double)reader["RideDistance"];

                                if (weeklyMaxTmp > weeklyMax)
                                {
                                    weeklyMax = weeklyMaxTmp;
                                }
                            }
                        }

                        //reader.Close();
                    }
                    command.Cancel();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to the Log year Index from the database." + ex.Message.ToString());
            }
            finally
            {
                // close connection
                sqlConnection?.Close();
            }

            chart1.ChartAreas[0].AxisX.Interval = 5;
        }

        private static float GetTotalMilesMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                month
            };
            float returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMiles_Monthly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = 0;
                        }
                        else
                        {
                            returnValue = float.Parse(temp);
                        }

                    }
                }
            }

            return returnValue;
        }

        public static SqlDataReader ExecuteSimpleQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";

            for (int i = 0; i < _Parameters.Count; i++)
            {
                tmpProcedureName += "@" + i.ToString() + ",";
            }

            tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
            DatabaseConnection databaseConnection;

            using (MainForm mainForm = new MainForm())
            {
                databaseConnection = MainForm.GetsDatabaseConnectionString();
            }
            SqlDataReader ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName, _Parameters);

            return ToReturn;
        }

        private void CheckBoxRouteOption_Click(object sender, EventArgs e)
        {
            if (checkBoxRouteOption.Checked == true)
            {
                cbRoutesChart.Enabled = true;
            }
            else
            {
                cbRoutesChart.Enabled = false;
                cbRoutesChart.SelectedIndex = -1;
            }
        }

        private void CbTypeTimeChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelChartError.Hide();
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastTypeTimeChartSelected(cbTypeTime.SelectedIndex);
            }
        }

        private void CbTypeChartData_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelChartError.Hide();
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastTypeChartSelected(cbTypeChartData.SelectedIndex);
            }
        }
    }
}
