using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text;

namespace CyclingLogApplication
{
    public partial class ChartForm : Form
    {
        private static SqlConnection sqlConnection;// = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");


        public ChartForm()
        {
            InitializeComponent();
            sqlConnection = MainForm.GetsqlConnectionString();
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            // NOTE: This line of code loads data into the 'cyclingLogDatabaseDataSet.Table_Ride_Information' table. You can move, or remove it, as needed.
            //this.table_Ride_InformationTableAdapter.Fill(this.cyclingLogDatabaseDataSet.Table_Ride_Information);

            labelChartError.Hide();
            checkBoxRouteOption.Checked = false;
            cbRoutesChart.Enabled = false;
            rbChartTypeColumn.Checked = true;

            List<string> logList = MainForm.ReadDataNamesDESC("Table_Log_year", "Name");

            cbLogYearChart.Items.Add("--Select Value--");
            for (int i = 0; i < logList.Count; i++)
            {
                cbLogYearChart.Items.Add(logList[i]);
            }

            List<string> routeList = MainForm.GetRoutes();

            cbRoutesChart.Items.Add("--Select Value--");
            for (int i = 0; i < routeList.Count; i++)
            {
                cbRoutesChart.Items.Add(routeList[i]);
            }

            try
            {
                cbLogYearChart.SelectedIndex = MainForm.GetLastLogYearChartSelected();
                cbTypeChartData.SelectedIndex = MainForm.GetLastTypeChartSelected();
                cbTypeTime.SelectedIndex = MainForm.GetLastTypeTimeChartSelected();
            }
            catch
            {
                cbLogYearChart.SelectedIndex = 0;
                cbTypeChartData.SelectedIndex = 0;
                cbTypeTime.SelectedIndex = 0;
            }

            cbRoutesChart.SelectedIndex = 0;
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

        private void BtRunChart_Click(object sender, EventArgs e)
        {
            string chartDataColumn = "";
            string dataType = "";
            bool averageDataType = false;

            //Verify the required values are available before running chart:
            if (cbTypeTime.SelectedIndex < 1)
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
            if (cbRoutesChart.SelectedIndex < 1 && checkBoxRouteOption.Checked)
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
            SqlDataReader reader = null;
            int logIndex;
            int logSetting;

            using (MainForm mainForm = new MainForm(""))
            {
                logSetting = MainForm.GetLogLevel();
                logIndex = MainForm.GetLogYearIndex_ByName(cbLogYearChart.SelectedItem.ToString());
            }

            //Average Speed:
            if (cbTypeChartData.SelectedIndex == 1)
            {
                chartDataColumn = "AvgSpeed";
                averageDataType = true;
                lbYAxis.Text = "AvgSpeed";
                dataType = "avgspeed";
            }
            //Longest:
            else if (cbTypeChartData.SelectedIndex == 2)
            {

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

                lbYAxis.Text = "Miles";

                chartDataColumn = "RideDistance";
                dataType = "longest";
            }
            //Miles:
            else if (cbTypeChartData.SelectedIndex == 3)
            {
                chartDataColumn = "RideDistance";
                lbYAxis.Text = "Miles";
                dataType = "miles";
            }
            //Longest Ascent:
            else if (cbTypeChartData.SelectedIndex == 4)
            {
                chartDataColumn = "TotalAscent";
                lbYAxis.Text = "Feet";
                dataType = "totalascent";
            }
            else if (cbTypeChartData.SelectedIndex == 5)
            {
                chartDataColumn = "Planned";
                lbYAxis.Text = "Miles";
                dataType = "miles";
            }

            SqlCommand cmd = null;
            string timeFreq = "";
            var allowedChartColumns = new HashSet<string> { "AvgSpeed", "RideDistance", "TotalAscent", "Planned" };
            if (!allowedChartColumns.Contains(chartDataColumn))
            {
                throw new ArgumentException("Invalid chart data column selected.");
            }

            if (chartDataColumn.Equals("Planned"))
            {
                try
                {
                    //Daily:
                    if (cbTypeTime.SelectedIndex == 1)
                    {
                        DateTime dateFrom = DateTime.Parse(dtpFromDate.Value.ToString());
                        DateTime dateTo = DateTime.Parse(dtpToDate.Value.ToString());

                        cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber], [RideDistance] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID AND [Date] >= @DateFrom AND [Date] < @DateTo ORDER BY [Date]", sqlConnection);
                        cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                        cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", dateTo);
                        lbXAxis.Text = "Day";
                        timeFreq = "Daily";
                    }
                    //Weekly:
                    else if (cbTypeTime.SelectedIndex == 2)
                    {
                        cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber], [RideDistance] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                        cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                        lbXAxis.Text = "Week";
                        timeFreq = "Weekly";
                    }
                    //Monthly:
                    else if (cbTypeTime.SelectedIndex == 3)
                    {
                        cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber], [RideDistance] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                        cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                        lbXAxis.Text = "Month";
                        timeFreq = "Monthly";
                    }

                    sqlConnection.Open();
                    reader = cmd.ExecuteReader();
                    int weekCount = 1;
                    int monthCount = 1;
                    int recordCount = 1;
                    double total_miles_plan = 0;
                    double total_miles_actual = 0;

                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                    chart1.Series["Series2"].ChartType = SeriesChartType.Line;


                    string date = "";
                    Boolean recordFound = false;

                    // write each record
                    while (reader.Read())
                    {
                        date = reader[0].ToString();
                        date = Convert.ToDateTime(date).ToShortDateString();
                        string pMilesString = reader[1].ToString();

                        double plan_miles = 0;
                        if (string.IsNullOrEmpty(pMilesString))
                        {
                            plan_miles = 0;
                        }
                        else
                        {
                            plan_miles = double.Parse(pMilesString);
                        }

                        int weekValue = Convert.ToInt32(reader[2].ToString());
                        string aMilesString = reader[3].ToString();

                        double actual_miles = 0;
                        if (string.IsNullOrEmpty(aMilesString))
                        {
                            actual_miles = 0;
                        }
                        else
                        {
                            actual_miles = double.Parse(aMilesString);
                        }

                        //Daily
                        if (cbTypeTime.SelectedIndex == 1)
                        {
                            if (!string.IsNullOrEmpty(date))
                            {
                                actual_miles = Math.Round(actual_miles, 1);
                                plan_miles = Math.Round(plan_miles, 1);
                                chart1.Series["Series1"].Points.AddXY(date, actual_miles.ToString());
                                chart1.Series["Series2"].Points.AddXY(date, plan_miles.ToString());

                                Logger.Log("Planner Chart Testing: Daily acutul values: " + actual_miles + "::" + date, logSetting, 1);
                                Logger.Log("Planner Chart Testing: Daily plan values: " + plan_miles + "::" + date, logSetting, 1);
                            }
                        }
                        //Weekly:
                        else if (cbTypeTime.SelectedIndex == 2)
                        {
                            //If not equal, a new week has started:
                            if (weekCount != weekValue)
                            {
                                chart1.Series["Series1"].Points.AddXY(date, total_miles_actual.ToString());
                                chart1.Series["Series2"].Points.AddXY(date, total_miles_plan.ToString());

                                //Restart values over since starting a new week:
                                recordCount = 1;
                                total_miles_actual = actual_miles;
                                total_miles_plan = plan_miles;
                                weekCount = weekValue;
                                recordFound = true;
                            }
                            // Still on the current week:
                            else
                            {
                                total_miles_actual += actual_miles;
                                total_miles_plan += plan_miles;
                                recordCount++;
                                recordFound = true;
                            }

                            Logger.Log("Planner Chart Testing: Weekly actual values: " + actual_miles + "::" + date, logSetting, 1);
                            Logger.Log("Planner Chart Testing: Weekly plan values: " + plan_miles + "::" + date, logSetting, 1);
                        }
                        //Monthly:
                        else if (cbTypeTime.SelectedIndex == 3)
                        {
                            DateTime datetime = Convert.ToDateTime(date);
                            int month = datetime.Month;

                            if (monthCount != month)
                            {
                                chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), total_miles_actual.ToString());
                                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                chart1.Series["Series1"].IsValueShownAsLabel = true;

                                chart1.Series["Series2"].Points.AddXY(monthCount.ToString(), total_miles_plan.ToString());
                                chart1.Series["Series2"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                chart1.Series["Series2"].IsValueShownAsLabel = true;

                                //Restart values over since starting a new month:
                                recordCount = 1;
                                total_miles_actual = actual_miles;
                                total_miles_plan = plan_miles;
                                monthCount = month;
                                recordFound = true;
                            }
                            else
                            {
                                total_miles_actual += actual_miles;
                                total_miles_plan += plan_miles;
                                recordCount++;
                                recordFound = true;
                            }

                            Logger.Log("Planner Chart Testing: Weekly actual values: " + actual_miles + "::" + date, logSetting, 1);
                            Logger.Log("Planner Chart Testing: Weekly plan values: " + plan_miles + "::" + date, logSetting, 1);
                        }
                        chart1.ChartAreas[0].AxisX.Interval = 1;
                    }

                    // Daily not required:
                    //Last Weekly data:
                    if (recordFound && cbTypeTime.SelectedIndex == 2)
                    {
                        chart1.Series["Series1"].Points.AddXY(date, total_miles_actual.ToString());
                        chart1.Series["Series1"].IsValueShownAsLabel = true;

                        chart1.Series["Series2"].Points.AddXY(date, total_miles_plan.ToString());
                        chart1.Series["Series2"].IsValueShownAsLabel = true;
                    }
                    //Last Monthly data:
                    else if (recordFound && cbTypeTime.SelectedIndex == 3)
                    {
                        chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), total_miles_actual.ToString());
                        chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                        chart1.Series["Series1"].IsValueShownAsLabel = true;

                        chart1.Series["Series2"].Points.AddXY(monthCount.ToString(), total_miles_plan.ToString());
                        chart1.Series["Series2"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                        chart1.Series["Series2"].IsValueShownAsLabel = true;
                    }

                    chart1.Series["Series2"].BorderWidth = 5;
                    Title title = new Title();
                    title.Font = new Font("Arial", 14, FontStyle.Bold);
                    title.Text = "Planner: " + timeFreq + " Miles";
                    chart1.Titles.RemoveAt(0);
                    chart1.Titles.Add(title);
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to run a Planner chart." + ex.Message.ToString());
                    MessageBox.Show("Exception while trying to run a Planner chart: " + ex.Message.ToString());
                }
                finally
                {
                    // close reader
                    reader?.Close();

                    // close connection
                    sqlConnection?.Close();

                    cmd?.Dispose();
                }
            }
            else
            {

                // All other than Planner:
                try
                {
                    //Daily:
                    if (cbTypeTime.SelectedIndex == 1)
                    {
                        DateTime dateFrom = DateTime.Parse(dtpFromDate.Value.ToString());
                        DateTime dateTo = DateTime.Parse(dtpToDate.Value.ToString());

                        if (!checkBoxRouteOption.Checked)
                        {
                            cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID AND [Date] >= @DateFrom AND [Date] < @DateTo ORDER BY [Date]", sqlConnection);
                            cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", dateTo);
                        }
                        else
                        {
                            cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [Route]=@Route AND [LogYearID]=@LogYearID AND [Date] >= @DateFrom AND [Date] < @DateTo ORDER BY [Date]", sqlConnection);
                            cmd.Parameters.AddWithValue("@Route", cbRoutesChart.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            cmd.Parameters.AddWithValue("@DateFrom", dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", dateTo);
                        }

                        lbXAxis.Text = "Day";
                        timeFreq = "Daily";
                    }
                    //Weekly:
                    else if (cbTypeTime.SelectedIndex == 2)
                    {
                        if (!checkBoxRouteOption.Checked)
                        {
                            if (cbTypeChartData.SelectedIndex == 2)
                            {
                                cmd = new SqlCommand("SELECT [Date], MAX([RideDistance]), [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID GROUP BY [RideDistance], [WeekNumber], [Date] ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                            else
                            {
                                cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                        }
                        else
                        {
                            if (cbTypeChartData.SelectedIndex == 2)
                            {
                                cmd = new SqlCommand("SELECT [Date], MAX([RideDistance]), [WeekNumber] FROM Table_Ride_Information WHERE [Route]=@Route AND [LogYearID]=@LogYearID GROUP BY [RideDistance], [WeekNumber], [Date] ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@Route", cbRoutesChart.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                            else
                            {
                                cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [Route]=@Route AND [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@Route", cbRoutesChart.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                        }

                        lbXAxis.Text = "Week";
                        timeFreq = "Weekly";
                    }
                    //Monthly:
                    else if (cbTypeTime.SelectedIndex == 3)
                    {
                        if (!checkBoxRouteOption.Checked)
                        {
                            if (cbTypeChartData.SelectedIndex == 2)
                            {
                                cmd = new SqlCommand("SELECT [Date], MAX([RideDistance]), [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID GROUP BY [RideDistance], [WeekNumber], [Date] ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                            else if (cbTypeChartData.SelectedIndex == 5)
                            {
                                cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                            else
                            {
                                cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID AND [RideDistance] > 0 ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                        }
                        else
                        {
                            if (cbTypeChartData.SelectedIndex == 2)
                            {
                                cmd = new SqlCommand("SELECT [Date], MAX([RideDistance]), [WeekNumber] FROM Table_Ride_Information WHERE [Route]=@Route AND [LogYearID]=@LogYearID GROUP BY [RideDistance], [WeekNumber], [Date] ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@Route", cbRoutesChart.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                            else
                            {
                                cmd = new SqlCommand($"SELECT [Date], [{chartDataColumn}], [WeekNumber] FROM Table_Ride_Information WHERE [Route]=@Route AND [LogYearID]=@LogYearID ORDER BY [Date]", sqlConnection);
                                cmd.Parameters.AddWithValue("@Route", cbRoutesChart.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@LogYearID", logIndex);
                            }
                        }

                        lbXAxis.Text = "Month";
                        timeFreq = "Monthly";
                    }

                    sqlConnection.Open();
                    reader = cmd.ExecuteReader();
                    int weekCount = 1;
                    int monthCount = 1;
                    int recordCount = 1;
                    double total_value = 0;
                    double avg_value = 0;

                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();

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
                    double weeklyMax = 0;
                    double monthlyMax = 0;

                    // write each record
                    while (reader.Read())
                    {
                        date = reader[0].ToString();
                        date = Convert.ToDateTime(date).ToShortDateString();

                        double chartDataTypeValue = 0;
                        double.TryParse(reader[1]?.ToString(), out chartDataTypeValue);

                        int weekValue = 0;
                        int.TryParse(reader[2]?.ToString(), out weekValue);


                        //Daily
                        if (cbTypeTime.SelectedIndex == 1)
                        {
                            if (!string.IsNullOrEmpty(date))
                            {
                                chartDataTypeValue = Math.Round(chartDataTypeValue, 1);
                                chart1.Series["Series1"].Points.AddXY(date, chartDataTypeValue.ToString());
                                Logger.Log("Chart Testing: Daily values: " + chartDataTypeValue + "::" + date, logSetting, 1);
                            }
                        }
                        //Weekly:
                        else if (cbTypeTime.SelectedIndex == 2)
                        {
                            if (weekValue == 0)
                            {
                                continue;
                            }

                            //If not equal, a new week has started:
                            if (weekCount != weekValue)
                            {
                                if (averageDataType)
                                {
                                    if (total_value != 0)
                                    {
                                        avg_value = total_value / recordCount;
                                        avg_value = Math.Round(avg_value, 1);
                                        chart1.Series["Series1"].Points.AddXY(date, avg_value.ToString());
                                    }
                                }
                                else if (dataType.Equals("longest") || dataType.Equals("totalascent"))
                                {
                                    if (weeklyMax != 0)
                                    {
                                        chart1.Series["Series1"].Points.AddXY(date, weeklyMax.ToString());
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
                                weeklyMax = chartDataTypeValue;
                                recordFound = true;
                            }
                            // Still on the current week:
                            else
                            {
                                total_value += chartDataTypeValue;
                                recordCount++;
                                recordFound = true;
                                if (chartDataTypeValue > weeklyMax)
                                {
                                    weeklyMax = chartDataTypeValue;
                                }
                            }

                            Logger.Log("Chart Testing: Weekly values: " + chartDataTypeValue + "::" + date, logSetting, 1);
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
                                        avg_value = Math.Round(avg_value, 1);
                                        chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), avg_value.ToString());
                                        chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                        chart1.Series["Series1"].IsValueShownAsLabel = true;
                                    }
                                }
                                else if (dataType.Equals("longest") || dataType.Equals("totalascent"))
                                {
                                    if (monthlyMax != 0)
                                    {
                                        chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), monthlyMax.ToString());
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
                                monthlyMax = chartDataTypeValue;
                                recordFound = true;
                            }
                            else
                            {
                                total_value += chartDataTypeValue;
                                recordCount++;
                                recordFound = true;
                                if (chartDataTypeValue > monthlyMax)
                                {
                                    monthlyMax = chartDataTypeValue;
                                }
                            }

                            Logger.Log("Chart Testing: Monthly values: " + chartDataTypeValue + "::" + date, logSetting, 1);
                        }
                        chart1.ChartAreas[0].AxisX.Interval = 1;
                    }

                    // Daily not required:
                    //Weekly:
                    if (recordFound && cbTypeTime.SelectedIndex == 2)
                    {
                        if (averageDataType)
                        {
                            if (total_value != 0)
                            {
                                avg_value = total_value / recordCount;
                                avg_value = Math.Round(avg_value, 1);
                                chart1.Series["Series1"].Points.AddXY(date, avg_value.ToString());
                                chart1.Series["Series1"].IsValueShownAsLabel = true;
                            }
                        }
                        else if (dataType.Equals("longest") || dataType.Equals("totalascent"))
                        {
                            if (weeklyMax != 0)
                            {
                                chart1.Series["Series1"].Points.AddXY(date, weeklyMax.ToString());
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
                                avg_value = Math.Round(avg_value, 1);
                                chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), avg_value.ToString());
                                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
                                chart1.Series["Series1"].IsValueShownAsLabel = true;
                            }
                        }
                        else if (dataType.Equals("longest") || dataType.Equals("totalascent"))
                        {
                            if (monthlyMax != 0)
                            {
                                chart1.Series["Series1"].Points.AddXY(monthCount.ToString(), monthlyMax.ToString());
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

                    Title title = new Title();
                    title.Font = new Font("Arial", 14, FontStyle.Bold);
                    string chartName = "";
                    if (dataType.Equals("longest"))
                    {
                        chartName = " Longest Ride";
                        title.Text = timeFreq + chartName;
                    }
                    else if (dataType.Equals("totalascent"))
                    {
                        chartName = " Total Ascent";
                        title.Text = timeFreq + chartName;
                    }
                    else if (dataType.Equals("avgspeed"))
                    {
                        chartName = " Average Speed";
                        title.Text = timeFreq + chartName;
                    }
                    else if (dataType.Equals("miles"))
                    {
                        chartName = " Miles";
                        title.Text = timeFreq + chartName;
                    }

                    chart1.Titles.RemoveAt(0);
                    chart1.Titles.Add(title);
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to run a chart." + ex.Message.ToString());
                    MessageBox.Show("Exception while trying to run a chart: " + ex.Message.ToString());
                }
                finally
                {
                    // close reader
                    reader?.Close();

                    // close connection
                    sqlConnection?.Close();

                    cmd?.Dispose();
                }
            }

            //---------------------
            if (cbTypeChartData.SelectedIndex == 1)
            {
                // Average Speed Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "AvgSpeed";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (cbTypeChartData.SelectedIndex == 2)
            {
                // Longest Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "Longest";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (cbTypeChartData.SelectedIndex == 3)
            {
                // Miles Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "Miles";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (cbTypeChartData.SelectedIndex == 4)
            {
                // High Ascent Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "High Ascent";
                chart1.Series["Series1"].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            }
            else if (cbTypeChartData.SelectedIndex == 5)
            {
                // Planner Chart
                chart1.Series["Series1"].XValueMember = "Date";
                chart1.Series["Series1"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Series1"].YValueMembers = "Miles";
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
            chart1.Series[1].Points.Clear();

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
                        if (string.IsNullOrEmpty(temp))
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
                        if (string.IsNullOrEmpty(temp))
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
            int weekNumber;
            int weekNumberTmp = 0;
            double weeklyMaxTmp;
            double weeklyMax = 0;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber,Date FROM Table_Ride_Information WHERE [LogYearID]=@LogYearID";
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@LogYearID", LogYearID);
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
                        if (string.IsNullOrEmpty(temp))
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
            var tmpProcedureName = new StringBuilder();
            tmpProcedureName.Append("EXECUTE ").Append(ProcedureName).Append(" ");

            for (int i = 0; i < _Parameters.Count; i++)
            {
                tmpProcedureName.Append("@").Append(i).Append(",");
            }

            if (_Parameters.Count > 0)
            {
                tmpProcedureName.Length -= 1;
            }

            tmpProcedureName.Append(";");
            DatabaseConnection databaseConnection;

            using (MainForm mainForm = new MainForm())
            {
                databaseConnection = MainForm.GetsDatabaseConnectionString();
            }
            SqlDataReader ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName.ToString(), _Parameters);

            return ToReturn;
        }

        private void CheckBoxRouteOption_Click(object sender, EventArgs e)
        {
            if (checkBoxRouteOption.Checked)
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

            if (cbTypeTime.SelectedIndex == 1)
            {
                gbChartTimeRange.Enabled = true;
            }
            else
            {
                gbChartTimeRange.Enabled = false;
            }
        }

        private void CbTypeChartData_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelChartError.Hide();

            if (cbTypeChartData.SelectedIndex == 5)
            {
                rbChartTypeBar.Checked = false;
                rbChartTypeColumn.Checked = true;
                rbChartTypeLine.Checked = false;
                gbChartType.Enabled = false;
            }
            else if (cbTypeChartData.SelectedIndex == 2 || cbTypeChartData.SelectedIndex == 4)
            {
                gbChartType.Enabled = true;
                checkBoxRouteOption.Checked = false;
                checkBoxRouteOption.Enabled = false;
                cbRoutesChart.Enabled = false;
                cbRoutesChart.SelectedIndex = 0;
            }
            else
            {
                gbChartType.Enabled = true;
                checkBoxRouteOption.Enabled = true;
            }

            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastTypeChartSelected(cbTypeChartData.SelectedIndex);
            }
        }
    }
}