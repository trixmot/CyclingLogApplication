using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclingLogApplication
{
    public partial class Planner : Form
    {
        public Planner()
        {
            InitializeComponent();
            //RunPlanner();

            int logSetting = MainForm.GetLogLevel();

            List<string> logYearList = MainForm.ReadDataNamesDESC("Table_Log_year", "Name");
            cbPlannerLogs.Items.Add("--Select Value--");
            foreach (string val in logYearList)
            {
                cbPlannerLogs.Items.Add(val);
                Logger.Log("Data Loading: Log Year: " + val, logSetting, 1);
            }

            cbPlannerLogs.SelectedIndex = 0;
            cbPlannerMonth.SelectedIndex = 0;
        }

        private string GetFirstDayForMonth(int month)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, month, 1);
            // 'Friday, November 1, 2024'

            return firstDay.DayOfWeek.ToString();
        }

        //TODO:  First Day of week Monday/Sunday

        private void RunPlanner()
        {
            if (cbPlannerMonth.SelectedIndex < 1)
            {
                return;
            }

            int monthIndex = cbPlannerMonth.SelectedIndex;
            string firstDay = GetFirstDayForMonth(monthIndex); 
            int daysInMonth = System.DateTime.DaysInMonth(DateTime.Now.Year, monthIndex);
            string firstDayOfWeek = MainForm.GetFirstDayOfWeek();
            int logIndex = 0;

            int day1 = 0;
            int day2 = 0;
            int day3 = 0;
            int day4 = 0;
            int day5 = 0;
            int day6 = 0;
            int day7 = 0;

            if (firstDay.Equals("Monday"))
            {
                day1 = 1;
            }
            else if (firstDay.Equals("Tuesday"))
            {
                day2 = 1;
            }
            else if (firstDay.Equals("Wednesday"))
            {
                day3 = 1;
            }
            else if (firstDay.Equals("Thursday"))
            {
                day4 = 1;
            }
            else if (firstDay.Equals("Friday"))
            {
                day5 = 1;
            }
            else if (firstDay.Equals("Saturday"))
            {
                day6 = 1;
            }
            else if (firstDay.Equals("Sunday"))
            {
                day7 = 1;
            }

            int dayInt = 5;
            //Look up date to get goal and actual miles:
            //Create TimeDate from day:
            DateTime dateTime = new DateTime(DateTime.Now.Year, monthIndex, dayInt);

            try
            {
                dataGridViewPlanner.DataSource = null;
                dataGridViewPlanner.Rows.Clear();
                dataGridViewPlanner.ColumnCount = 7;
                //dataGridViewPlanner.RowCount = 12;
                dataGridViewPlanner.Name = "Calendar View";

                if (firstDayOfWeek.Equals("Monday"))
                {
                    dataGridViewPlanner.Columns[0].Name = "Monday";
                    dataGridViewPlanner.Columns[1].Name = "Tuesday";
                    dataGridViewPlanner.Columns[2].Name = "Wednesday";
                    dataGridViewPlanner.Columns[3].Name = "Thursday";
                    dataGridViewPlanner.Columns[4].Name = "Friday";
                    dataGridViewPlanner.Columns[5].Name = "Saturday";
                    dataGridViewPlanner.Columns[6].Name = "Sunday";
                }
                else
                {
                    dataGridViewPlanner.Columns[0].Name = "Sunday";
                    dataGridViewPlanner.Columns[1].Name = "Monday";
                    dataGridViewPlanner.Columns[2].Name = "Tuesday";
                    dataGridViewPlanner.Columns[3].Name = "Wednesday";
                    dataGridViewPlanner.Columns[4].Name = "Thursday";
                    dataGridViewPlanner.Columns[5].Name = "Friday";
                    dataGridViewPlanner.Columns[6].Name = "Saturday";                    
                }
                

                dataGridViewPlanner.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewPlanner.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                dataGridViewPlanner.ReadOnly = true;
                dataGridViewPlanner.EnableHeadersVisualStyles = false;

                dataGridViewPlanner.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewMonthly.AutoResizeColumns();
                dataGridViewPlanner.AllowUserToOrderColumns = false;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewPlanner.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPlanner.AllowUserToAddRows = false;
                //dataGridViewMonthly.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                //dataGridViewMonthly.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridViewPlanner.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                //dataGridViewMonthly.RowHeadersVisible = false;

                dataGridViewPlanner.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewPlanner.ColumnHeadersHeight = 40;
                dataGridViewPlanner.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewPlanner.RowHeadersVisible = true;

                dataGridViewPlanner.Columns[0].ValueType = typeof(string);
                dataGridViewPlanner.Columns[1].ValueType = typeof(string);
                dataGridViewPlanner.Columns[2].ValueType = typeof(string);
                dataGridViewPlanner.Columns[3].ValueType = typeof(string);
                dataGridViewPlanner.Columns[4].ValueType = typeof(string);
                dataGridViewPlanner.Columns[5].ValueType = typeof(string);
                dataGridViewPlanner.Columns[6].ValueType = typeof(string);

                int weekNumber = 2;
                string miles1 = "30.1";
                string miles2 = "30.1";
                string miles3 = "30.1";
                string miles4 = "30.1";
                string miles5 = "30.1";
                string miles6 = "30.1";
                string miles7 = "30.1";
                int rowCount = 0;
                int dayCount = 0;
                string temp1 = "";
                string temp2 = "";
                string temp3 = "";
                string temp4 = "";
                string temp5 = "";
                string temp6 = "";
                string temp7 = "";

                //Get Miles for day1:
                //GetMilesByDate();

                //First Week of the month:
                if (day1 == 1)
                {
                    temp1 = "1";
                    temp2 = "2";
                    temp3 = "3";
                    temp4 = "4";
                    temp5 = "5";
                    temp6 = "6";
                    temp7 = "7";
                }
                else if (day2 == 1)
                {
                    temp1 = "";
                    temp2 = "1";
                    temp3 = "2";
                    temp4 = "3";
                    temp5 = "4";
                    temp6 = "5";
                    temp7 = "6";
                    miles1 = "";
                }
                else if (day3 == 1)
                {
                    temp1 = "";
                    temp2 = "";
                    temp3 = "1";
                    temp4 = "2";
                    temp5 = "3";
                    temp6 = "4";
                    temp7 = "5";
                    miles1 = "";
                    miles2 = "";
                }
                else if (day4 == 1)
                {
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    temp4 = "1";
                    temp5 = "2";
                    temp6 = "3";
                    temp7 = "4";
                    miles1 = "";
                    miles2 = "";
                    miles3 = "";
                }
                else if (day5 == 1)
                {
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    temp4 = "";
                    temp5 = "1";
                    temp6 = "2";
                    temp7 = "3";
                    miles1 = "";
                    miles2 = "";
                    miles3 = "";
                    miles4 = "";
                }
                else if (day6 == 1)
                {
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    temp4 = "";
                    temp5 = "";
                    temp6 = "1";
                    temp7 = "2";
                    miles1 = "";
                    miles2 = "";
                    miles3 = "";
                    miles4 = "";
                    miles5 = "";
                }
                else if (day7 == 1)
                {
                    temp1 = "";
                    temp2 = "";
                    temp3 = "";
                    temp4 = "";
                    temp5 = "";
                    temp6 = "";
                    temp7 = "1";
                    miles1 = "";
                    miles2 = "";
                    miles3 = "";
                    miles4 = "";
                    miles5 = "";
                    miles6 = "";
                }

                dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7);
                dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                dayCount = int.Parse(temp7);
                rowCount++;
                rowCount++;

                //Loop through each month for the logindex:
                for (int i = 0; i < 5; i++)
                {
                    //check to see of over daysInMonth: 30


                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp1 = dayCount.ToString();
                        temp2 = "";
                        temp3 = "";
                        temp4 = "";
                        temp5 = "";
                        temp6 = "";
                        temp7 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }

                    temp1 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp2 = dayCount.ToString();
                        temp3 = "";
                        temp4 = "";
                        temp5 = "";
                        temp6 = "";
                        temp7 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp2 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp3 = dayCount.ToString();
                        temp4 = "";
                        temp5 = "";
                        temp6 = "";
                        temp7 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp3 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp4 = dayCount.ToString();
                        temp5 = "";
                        temp6 = "";
                        temp7 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp4 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp5 = dayCount.ToString();
                        temp6 = "";
                        temp7 = "";
                        miles6 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp5 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp6 = dayCount.ToString();
                        temp7 = "";
                        miles7 = "";
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp6 = dayCount.ToString();
                    dayCount++;
                    if (dayCount == daysInMonth)
                    {
                        temp7 = dayCount.ToString();
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                        break;
                    }
                    temp7 = dayCount.ToString();

                    dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                    dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                    dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                    rowCount++;
                    rowCount++;
                    weekNumber++;
                }

                //dataGridViewPlanner.Columns[0].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[8].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Rows[0].Cells[7].Style.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[0].Width = 30;
                // Specify a larger font for the "Date" row. 
                using (Font font = new Font(
                    dataGridViewPlanner.DefaultCellStyle.Font.FontFamily, 25, FontStyle.Bold))
                {
                    dataGridViewPlanner.Rows[0].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[2].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[4].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[6].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[8].DefaultCellStyle.Font = font;
                }

                dataGridViewPlanner.Rows[0].Height = 35;
                dataGridViewPlanner.Rows[1].Height = 35;
                dataGridViewPlanner.Rows[2].Height = 35;
                dataGridViewPlanner.Rows[3].Height = 35;
                dataGridViewPlanner.Rows[4].Height = 35;
                dataGridViewPlanner.Rows[5].Height = 35;
                dataGridViewPlanner.Rows[6].Height = 35;
                dataGridViewPlanner.Rows[7].Height = 35;
                dataGridViewPlanner.Rows[8].Height = 35;
                dataGridViewPlanner.Rows[9].Height = 35;

                //dataGridViewPlanner.Rows[0].HeaderCell.Value = "Day";//1
                //dataGridViewPlanner.Rows[1].HeaderCell.Value = "Miles";
                //dataGridViewPlanner.Rows[2].HeaderCell.Value = "Day";//2
                //dataGridViewPlanner.Rows[3].HeaderCell.Value = "Miles";
                //dataGridViewPlanner.Rows[4].HeaderCell.Value = "Day";//3
                //dataGridViewPlanner.Rows[5].HeaderCell.Value = "Miles";
                //dataGridViewPlanner.Rows[6].HeaderCell.Value = "Day";//4
                //dataGridViewPlanner.Rows[7].HeaderCell.Value = "Miles";
                //dataGridViewPlanner.Rows[8].HeaderCell.Value = "Day";//5
                //dataGridViewPlanner.Rows[9].HeaderCell.Value = "Miles";

                dataGridViewPlanner.AllowUserToResizeRows = false;
                dataGridViewPlanner.AllowUserToResizeColumns = false;
                //dataGridViewPlanner.CurrentCell = dataGridViewMonthly.Rows[0].Cells[4];
                //dataGridViewPlanner.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite;

            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Planner: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Planner.  Review the log for more information.");
            }
        }

        private void dataGridViewPlanner_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btClosePlanner_Click(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }

        private void RunPlanner(object sender, EventArgs e)
        {
            //RunPlanner();
        }

        private void btPlannerSave_Click(object sender, EventArgs e)
        {
            //Save each day's value to an entry in the database:
            //Will need a date for each day in order to save value
            //Retrieve will need to change fo entry with just a goal

            //Using current year and week number selected, create a date.
        }

        private void cbPlannerLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If log year is not the current year, grey out planner update section:
            //List<string> logYearList = MainForm.ReadDataNamesDESC("Table_Log_year", "Year");
            //int cbIndex = cbPlannerLogs.SelectedIndex;
            //int logyear = 2023;

        }

        private void btClear_Click(object sender, EventArgs e)
        {

        }

        private void brRefreshPlanner_Click(object sender, EventArgs e)
        {
            RunPlanner();
        }
    }
}
