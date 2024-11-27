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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);

            if (MainForm.GetFirstDayOfWeek().Equals("Sunday")){
                while (dt.DayOfWeek != DayOfWeek.Sunday)
                {
                    dt = dt.AddDays(1);
                }

                lbDay1.Text = "Sunday";
                lbDay2.Text = "Monday";
                lbDay3.Text = "Tuesday";
                lbDay4.Text = "Wednesday";
                lbDay5.Text = "Thursday";
                lbDay6.Text = "Friday";
                lbDay7.Text = "Saturday";
            } else
            {
                while (dt.DayOfWeek != DayOfWeek.Monday)
                {
                    dt = dt.AddDays(1);
                }
              
                lbDay1.Text = "Monday";
                lbDay2.Text = "Tuesday";
                lbDay3.Text = "Wednesday";
                lbDay4.Text = "Thursday";
                lbDay5.Text = "Friday";
                lbDay6.Text = "Saturday";
                lbDay7.Text = "Sunday";
            }

            int index = 1;
            cbPlannerDate.Items.Add("--Select Value--");
            cbPlannerDate.Items.Add(dt.ToString("dd/MM/yyyy"));
            for (int i = 1; i < 53; i++)
            {
                //string dateString = dt.ToString();

                DateTime weekdate = dt.AddDays(index*7);
                index++;
                //list of dates:
                cbPlannerDate.Items.Add(weekdate.ToString("dd/MM/yyyy"));
            }

            cbPlannerDate.SelectedIndex = 0;
            lbPlannerError.Hide();
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

        private void RunPlanner2()
        {
            if (cbPlannerMonth.SelectedIndex < 1 || cbPlannerLogs.SelectedIndex < 1)
            {
                return;
            }

            int monthIndex = cbPlannerMonth.SelectedIndex;
            int previousMonthIndex = monthIndex - 1;
            int daysInMonthPrevious = 1;
            Boolean startOfYear = false;


            int nextMonthIndex = monthIndex + 1;
            string firstDay = GetFirstDayForMonth(monthIndex);
            string logName = cbPlannerLogs.SelectedItem.ToString();
            int logIndex = MainForm.GetLogIndexByName(logName);
            int logYear = MainForm.GetLogYearByName(logName);
            if (monthIndex == 1)
            {
                startOfYear = true;
            }
            else
            {
                daysInMonthPrevious = System.DateTime.DaysInMonth(logYear, previousMonthIndex);
            }
            int daysInMonth = System.DateTime.DaysInMonth(logYear, monthIndex);
            int currentDayNumber = DateTime.Now.Day;
            int currentYearNumber = DateTime.Now.Year;
            int currentMonthNumber = DateTime.Now.Month;
            Boolean currentYearMonth = false;
            Boolean futureDays = false;

            if (logYear == currentYearNumber && monthIndex == currentMonthNumber)
            {
                currentYearMonth = true;
            }

            if (logYear >= currentYearNumber && monthIndex > currentMonthNumber)
            {
                futureDays = true;
            }

            string sqlCommand = "GetMiles_ByDate";

            int day1 = 0;
            int day2 = 0;
            int day3 = 0;
            int day4 = 0;
            int day5 = 0;
            int day6 = 0;
            int day7 = 0;

            string firstDayOfWeek = MainForm.GetFirstDayOfWeek();

            if (firstDayOfWeek.Equals("Sunday"))
            {
                if (firstDay.Equals("Sunday"))
                {
                    day1 = 1;
                }
                else if (firstDay.Equals("Monday"))
                {
                    day2 = 1;
                }
                else if (firstDay.Equals("Tuesday"))
                {
                    day3 = 1;
                }
                else if (firstDay.Equals("Wednesday"))
                {
                    day4 = 1;
                }
                else if (firstDay.Equals("Thursday"))
                {
                    day5 = 1;
                }
                else if (firstDay.Equals("Friday"))
                {
                    day6 = 1;
                }
                else if (firstDay.Equals("Saturday"))
                {
                    day7 = 1;
                }
            } else
            {
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
            }

            string textValue = MainForm.GetTextCalendar();
            System.Drawing.Color textColor;
            if (textValue.Equals("True"))
            {
                textColor = Color.Black;
            }
            else
            {
                textColor = Color.White;
            }

            try
            {
                dataGridViewPlanner.DataSource = null;
                dataGridViewPlanner.Rows.Clear();
                dataGridViewPlanner.ColumnCount = 7;
                //dataGridViewPlanner.RowCount = 12;
                dataGridViewPlanner.Name = "Planner View";

                if (firstDayOfWeek.Equals("Sunday"))
                {
                    dataGridViewPlanner.Columns[0].Name = "Sunday";
                    dataGridViewPlanner.Columns[1].Name = "Monday";
                    dataGridViewPlanner.Columns[2].Name = "Tuesday";
                    dataGridViewPlanner.Columns[3].Name = "Wednesday";
                    dataGridViewPlanner.Columns[4].Name = "Thursday";
                    dataGridViewPlanner.Columns[5].Name = "Friday";
                    dataGridViewPlanner.Columns[6].Name = "Saturday";
                } else
                {                   
                    dataGridViewPlanner.Columns[0].Name = "Monday";
                    dataGridViewPlanner.Columns[1].Name = "Tuesday";
                    dataGridViewPlanner.Columns[2].Name = "Wednesday";
                    dataGridViewPlanner.Columns[3].Name = "Thursday";
                    dataGridViewPlanner.Columns[4].Name = "Friday";
                    dataGridViewPlanner.Columns[5].Name = "Saturday";
                    dataGridViewPlanner.Columns[6].Name = "Sunday";
                }

                dataGridViewPlanner.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
                dataGridViewPlanner.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewPlanner.RowHeadersDefaultCellStyle.BackColor = Color.Silver;

                foreach (DataGridViewColumn column in dataGridViewPlanner.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                dataGridViewPlanner.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewPlanner.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewMonthly.AutoResizeColumns();
                dataGridViewPlanner.ReadOnly = true;
                dataGridViewPlanner.EnableHeadersVisualStyles = false;
                dataGridViewPlanner.AllowUserToOrderColumns = false;
                dataGridViewPlanner.AllowUserToAddRows = false;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewPlanner.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPlanner.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewPlanner.ColumnHeadersHeight = 40;
                //dataGridViewPlanner.RowHeadersWidth = 10;
                dataGridViewPlanner.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewPlanner.RowHeadersVisible = true;

                dataGridViewPlanner.Columns[0].ValueType = typeof(string);
                dataGridViewPlanner.Columns[1].ValueType = typeof(string);
                dataGridViewPlanner.Columns[2].ValueType = typeof(string);
                dataGridViewPlanner.Columns[3].ValueType = typeof(string);
                dataGridViewPlanner.Columns[4].ValueType = typeof(string);
                dataGridViewPlanner.Columns[5].ValueType = typeof(string);
                dataGridViewPlanner.Columns[6].ValueType = typeof(string);

                int cellNumber = 0;
                int rowNumber = 0;
                int weekNumber = 2;
                int rowCount = 0;
                int dayCount = 0;

                string miles1 = "";
                string miles2 = "";
                string miles3 = "";
                string miles4 = "";
                string miles5 = "";
                string miles6 = "";
                string miles7 = "";

                string temp1 = "";
                string temp2 = "";
                string temp3 = "";
                string temp4 = "";
                string temp5 = "";
                string temp6 = "";
                string temp7 = "";

                string pMiles1 = "";
                string pMiles2 = "";
                string pMiles3 = "";
                string pMiles4 = "";
                string pMiles5 = "";
                string pMiles6 = "";
                string pMiles7 = "";

                //First Week of the month:
                //Check which day is the first day of the month:
                if (day1 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            //This is for having the current day in the calendar selected (highlighted in blue):
                            cellNumber = 0;
                            rowNumber = 0;
                        }
                    }

                    temp1 = "1";
                    temp2 = "2";
                    temp3 = "3";
                    temp4 = "4";
                    temp5 = "5";
                    temp6 = "6";
                    temp7 = "7";

                    if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime1 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime2 = new DateTime(logYear, monthIndex, 2);
                        DateTime dateTime3 = new DateTime(logYear, monthIndex, 3);
                        DateTime dateTime4 = new DateTime(logYear, monthIndex, 4);
                        DateTime dateTime5 = new DateTime(logYear, monthIndex, 5);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 6);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 7);

                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime1, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime2, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime3, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day2 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 1;
                            rowNumber = 0;
                        }
                    }

                    temp1 = daysInMonthPrevious.ToString();
                    temp2 = "1";
                    temp3 = "2";
                    temp4 = "3";
                    temp5 = "4";
                    temp6 = "5";
                    temp7 = "6";

                    if (startOfYear)
                    {
                        temp1 = "";
                        miles1 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime2b = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime2b, sqlCommand);
                        DateTime dateTime2 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime3 = new DateTime(logYear, monthIndex, 2);
                        DateTime dateTime4 = new DateTime(logYear, monthIndex, 3);
                        DateTime dateTime5 = new DateTime(logYear, monthIndex, 4);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 5);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 6);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime2, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime3, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day3 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 2;
                            rowNumber = 0;
                        }
                    }

                    temp1 = (daysInMonthPrevious - 1).ToString();
                    temp2 = (daysInMonthPrevious).ToString();
                    temp3 = "1";
                    temp4 = "2";
                    temp5 = "3";
                    temp6 = "4";
                    temp7 = "5";

                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        miles1 = "";
                        miles2 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime31 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime32 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime32, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime31, sqlCommand);
                        DateTime dateTime3 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime4 = new DateTime(logYear, monthIndex, 2);
                        DateTime dateTime5 = new DateTime(logYear, monthIndex, 3);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 4);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 5);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime3, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day4 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 3;
                            rowNumber = 0;
                        }
                    }

                    temp1 = (daysInMonthPrevious - 2).ToString();
                    temp2 = (daysInMonthPrevious - 1).ToString();
                    temp3 = (daysInMonthPrevious).ToString();
                    temp4 = "1";
                    temp5 = "2";
                    temp6 = "3";
                    temp7 = "4";

                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        temp3 = "";
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime41 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime42 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime43 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime43, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime42, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime41, sqlCommand);
                        DateTime dateTime4 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime5 = new DateTime(logYear, monthIndex, 2);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 3);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 4);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day5 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 4;
                            rowNumber = 0;
                        }
                    }

                    temp1 = (daysInMonthPrevious - 3).ToString();
                    temp2 = (daysInMonthPrevious - 2).ToString();
                    temp3 = (daysInMonthPrevious - 1).ToString();
                    temp4 = (daysInMonthPrevious).ToString();
                    temp5 = "1";
                    temp6 = "2";
                    temp7 = "3";

                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        temp3 = "";
                        temp4 = "";
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime51 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime52 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime53 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime54 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime54, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime53, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime52, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime51, sqlCommand);
                        DateTime dateTime5 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 2);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 3);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day6 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 5;
                            rowNumber = 0;
                        }
                    }

                    temp1 = (daysInMonthPrevious - 4).ToString();
                    temp2 = (daysInMonthPrevious - 3).ToString();
                    temp3 = (daysInMonthPrevious - 2).ToString();
                    temp4 = (daysInMonthPrevious - 1).ToString();
                    temp5 = (daysInMonthPrevious).ToString();
                    temp6 = "1";
                    temp7 = "2";

                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        temp3 = "";
                        temp4 = "";
                        temp5 = "";
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime61 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime62 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime63 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime64 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        DateTime dateTime65 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 4);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime65, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime64, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime63, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime62, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime61, sqlCommand);
                        DateTime dateTime6 = new DateTime(logYear, monthIndex, 1);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 2);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6, sqlCommand);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }
                else if (day7 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 6;
                            rowNumber = 0;
                        }
                    }

                    temp1 = (daysInMonthPrevious - 5).ToString();
                    temp2 = (daysInMonthPrevious - 4).ToString();
                    temp3 = (daysInMonthPrevious - 3).ToString();
                    temp4 = (daysInMonthPrevious - 2).ToString();
                    temp5 = (daysInMonthPrevious - 1).ToString();
                    temp6 = (daysInMonthPrevious).ToString();
                    temp7 = "1";

                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        temp3 = "";
                        temp4 = "";
                        temp5 = "";
                        temp6 = "";
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                    }
                    else if (futureDays)
                    {
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        DateTime dateTime71 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime72 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime73 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime74 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        DateTime dateTime75 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 4);
                        DateTime dateTime76 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 5);
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime76, sqlCommand);
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime75, sqlCommand);
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime74, sqlCommand);
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime73, sqlCommand);
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime72, sqlCommand);
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime71, sqlCommand);
                        DateTime dateTime7 = new DateTime(logYear, monthIndex, 1);
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7, sqlCommand);
                    }
                }

                //Date:
                dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7);
                dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                //Planned:
                dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                rowCount++;
                //Actuals
                dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                rowCount++;
                dayCount = int.Parse(temp7);

                if (miles1.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles2.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles3.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles4.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles5.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles6.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }
                if (miles7.Equals(""))
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                }

                dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.ForeColor = textColor;
                rowCount++;
                Boolean sixRow = false;

                //Loop through remaining weeks:
                for (int i = 0; i < 5; i++)
                {
                    if (i == 4)
                    {
                        sixRow = true;
                    }

                    dayCount++;
                    DateTime dateTime1a = new DateTime(logYear, monthIndex, dayCount);
                    //check to see of over daysInMonth: 30
                    if (dayCount == daysInMonth)
                    {
                        temp1 = dayCount.ToString();
                        temp2 = "1";
                        temp3 = "2";
                        temp4 = "3";
                        temp5 = "4";
                        temp6 = "5";
                        temp7 = "6";

                        if (futureDays)
                        {
                            miles1 = "";
                        }
                        else
                        {
                            miles1 = MainForm.GetDataItemByDate(logIndex, dateTime1a, sqlCommand);
                        }
                        if (nextMonthIndex == 13)
                        {
                            //Just post a blank entry for the next year Jan days:
                            miles2 = "";
                            miles3 = "";
                            miles4 = "";
                            miles5 = "";
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            DateTime dateTime1b = new DateTime(logYear, nextMonthIndex, 1);
                            DateTime dateTime1c = new DateTime(logYear, nextMonthIndex, 2);
                            DateTime dateTime1d = new DateTime(logYear, nextMonthIndex, 3);
                            DateTime dateTime1e = new DateTime(logYear, nextMonthIndex, 4);
                            DateTime dateTime1f = new DateTime(logYear, nextMonthIndex, 5);
                            DateTime dateTime1g = new DateTime(logYear, nextMonthIndex, 6);
                            miles2 = MainForm.GetDataItemByDate(logIndex, dateTime1b, sqlCommand);
                            miles3 = MainForm.GetDataItemByDate(logIndex, dateTime1c, sqlCommand);
                            miles4 = MainForm.GetDataItemByDate(logIndex, dateTime1d, sqlCommand);
                            miles5 = MainForm.GetDataItemByDate(logIndex, dateTime1e, sqlCommand);
                            miles6 = MainForm.GetDataItemByDate(logIndex, dateTime1f, sqlCommand);
                            miles7 = MainForm.GetDataItemByDate(logIndex, dateTime1g, sqlCommand);
                        }

                        //Dates
                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        //PLanned
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        //Actuals
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);              
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 0;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles1 = "";
                    }
                    else
                    {
                        miles1 = MainForm.GetDataItemByDate(logIndex, dateTime1a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 0;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }
                    temp1 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime2a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp2 = dayCount.ToString();
                        temp3 = "1";
                        temp4 = "2";
                        temp5 = "3";
                        temp6 = "4";
                        temp7 = "5";
                        if (futureDays)
                        {
                            miles2 = "";
                            miles3 = "";
                            miles4 = "";
                            miles5 = "";
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            miles2 = MainForm.GetDataItemByDate(logIndex, dateTime2a, sqlCommand);
                            if (nextMonthIndex == 13)
                            {
                                //Just post a blank entry for the next year Jan days:
                                miles3 = "";
                                miles4 = "";
                                miles5 = "";
                                miles6 = "";
                                miles7 = "";
                            }
                            else
                            {
                                DateTime dateTime2b = new DateTime(logYear, nextMonthIndex, 1);
                                DateTime dateTime2c = new DateTime(logYear, nextMonthIndex, 2);
                                DateTime dateTime2d = new DateTime(logYear, nextMonthIndex, 3);
                                DateTime dateTime2e = new DateTime(logYear, nextMonthIndex, 4);
                                DateTime dateTime2f = new DateTime(logYear, nextMonthIndex, 5);
                                miles3 = MainForm.GetDataItemByDate(logIndex, dateTime2b, sqlCommand);
                                miles4 = MainForm.GetDataItemByDate(logIndex, dateTime2c, sqlCommand);
                                miles5 = MainForm.GetDataItemByDate(logIndex, dateTime2d, sqlCommand);
                                miles6 = MainForm.GetDataItemByDate(logIndex, dateTime2e, sqlCommand);
                                miles7 = MainForm.GetDataItemByDate(logIndex, dateTime2f, sqlCommand);
                            }
                        }                       

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 1;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles2 = "";
                    }
                    else
                    {
                        miles2 = MainForm.GetDataItemByDate(logIndex, dateTime2a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 1;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }
                    temp2 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime3a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp3 = dayCount.ToString();
                        temp4 = "1";
                        temp5 = "2";
                        temp6 = "3";
                        temp7 = "4";
                        if (futureDays)
                        {
                            miles3 = "";
                            miles4 = "";
                            miles5 = "";
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            miles3 = MainForm.GetDataItemByDate(logIndex, dateTime3a, sqlCommand);
                            if (nextMonthIndex == 13)
                            {
                                //Just post a blank entry for the next year Jan days:
                                miles4 = "";
                                miles5 = "";
                                miles6 = "";
                                miles7 = "";
                            }
                            else
                            {
                                DateTime dateTime3b = new DateTime(logYear, nextMonthIndex, 1);
                                DateTime dateTime3c = new DateTime(logYear, nextMonthIndex, 2);
                                DateTime dateTime3d = new DateTime(logYear, nextMonthIndex, 3);
                                DateTime dateTime3e = new DateTime(logYear, nextMonthIndex, 4);
                                miles4 = MainForm.GetDataItemByDate(logIndex, dateTime3b, sqlCommand);
                                miles5 = MainForm.GetDataItemByDate(logIndex, dateTime3c, sqlCommand);
                                miles6 = MainForm.GetDataItemByDate(logIndex, dateTime3d, sqlCommand);
                                miles7 = MainForm.GetDataItemByDate(logIndex, dateTime3e, sqlCommand);
                            }
                        }                     

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 2;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles3 = "";
                    }
                    else
                    {
                        miles3 = MainForm.GetDataItemByDate(logIndex, dateTime3a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 2;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }
                    temp3 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime4a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp4 = dayCount.ToString();
                        temp5 = "1";
                        temp6 = "2";
                        temp7 = "3";
                        if (futureDays)
                        {
                            miles4 = "";
                            miles5 = "";
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4a, sqlCommand);
                            if (nextMonthIndex == 13)
                            {
                                //Just post a blank entry for the next year Jan days:
                                miles5 = "";
                                miles6 = "";
                                miles7 = "";
                            }
                            else
                            {
                                DateTime dateTime4b = new DateTime(logYear, nextMonthIndex, 1);
                                DateTime dateTime4c = new DateTime(logYear, nextMonthIndex, 2);
                                DateTime dateTime4d = new DateTime(logYear, nextMonthIndex, 3);
                                miles5 = MainForm.GetDataItemByDate(logIndex, dateTime4b, sqlCommand);
                                miles6 = MainForm.GetDataItemByDate(logIndex, dateTime4c, sqlCommand);
                                miles7 = MainForm.GetDataItemByDate(logIndex, dateTime4d, sqlCommand);
                            }
                        }                     

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 3;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles4 = "";
                    }
                    else
                    {
                        miles4 = MainForm.GetDataItemByDate(logIndex, dateTime4a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 3;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }
                    temp4 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime5a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp5 = dayCount.ToString();
                        temp6 = "1";
                        temp7 = "2";
                        if (futureDays)
                        {
                            miles5 = "";
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5a, sqlCommand);
                            if (nextMonthIndex == 13)
                            {
                                //Just post a blank entry for the next year Jan days:
                                miles6 = "";
                                miles7 = "";
                            }
                            else
                            {
                                DateTime dateTime5b = new DateTime(logYear, nextMonthIndex, 1);
                                DateTime dateTime5c = new DateTime(logYear, nextMonthIndex, 2);
                                miles6 = MainForm.GetDataItemByDate(logIndex, dateTime5b, sqlCommand);
                                miles7 = MainForm.GetDataItemByDate(logIndex, dateTime5c, sqlCommand);
                            }
                        }                      

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 4;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles5 = "";
                    }
                    else
                    {
                        miles5 = MainForm.GetDataItemByDate(logIndex, dateTime5a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 4;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }
                    temp5 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime6a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp6 = dayCount.ToString();
                        temp7 = "1";
                        if (futureDays)
                        {
                            miles6 = "";
                            miles7 = "";
                        }
                        else
                        {
                            miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6a, sqlCommand);
                            if (nextMonthIndex == 13)
                            {
                                //Just post a blank entry for the next year Jan days:
                                miles7 = "";
                            }
                            else
                            {
                                DateTime dateTime6b = new DateTime(logYear, nextMonthIndex, 1);
                                miles7 = MainForm.GetDataItemByDate(logIndex, dateTime6b, sqlCommand);
                            }
                        }                        

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                            dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                            dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 5;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }

                    if (futureDays)
                    {
                        miles6 = "";
                    }
                    else
                    {
                        miles6 = MainForm.GetDataItemByDate(logIndex, dateTime6a, sqlCommand);
                    }
                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 5;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }

                    temp6 = dayCount.ToString();
                    //---------------
                    dayCount++;
                    DateTime dateTime7a = new DateTime(logYear, monthIndex, dayCount);
                    if (dayCount == daysInMonth)
                    {
                        temp7 = dayCount.ToString();
                        if (futureDays)
                        {
                            miles7 = "";
                        }
                        else
                        {
                            miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7a, sqlCommand);
                        }

                        dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                        rowCount++;
                        dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        rowCount++;
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.ForeColor = textColor;
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.ForeColor = textColor;

                        if (miles1.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles2.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles3.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles4.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles5.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles6.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (miles7.Equals(""))
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        }

                        if (currentYearMonth)
                        {
                            if (dayCount == currentDayNumber)
                            {
                                cellNumber = 6;
                                rowNumber = rowCount;
                                futureDays = true;
                            }
                        }

                        break;
                    }
                    //---------------
                    temp7 = dayCount.ToString();

                    if (futureDays)
                    {
                        miles7 = "";
                    }
                    else
                    {
                        miles7 = MainForm.GetDataItemByDate(logIndex, dateTime7a, sqlCommand);
                    }

                    dataGridViewPlanner.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                    dataGridViewPlanner.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                    dataGridViewPlanner.Rows.Add(pMiles1, pMiles2, pMiles3, pMiles4, pMiles5, pMiles6, pMiles7);
                    rowCount++;
                    dataGridViewPlanner.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                    rowCount++;
                    dataGridViewPlanner.Rows[rowCount].Cells[0].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[1].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[2].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[3].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[4].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[5].Style.ForeColor = textColor;
                    dataGridViewPlanner.Rows[rowCount].Cells[6].Style.ForeColor = textColor;                    

                    if (miles1.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[0].Style.BackColor = Color.Gray;
                    }
                    if (miles2.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[1].Style.BackColor = Color.Gray;
                    }
                    if (miles3.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[2].Style.BackColor = Color.Gray;
                    }
                    if (miles4.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[3].Style.BackColor = Color.Gray;
                    }
                    if (miles5.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[4].Style.BackColor = Color.Gray;
                    }
                    if (miles6.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewPlanner.Rows[rowCount].Cells[5].Style.BackColor = Color.Gray;
                    }
                    if (miles7.Equals(""))
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                        //dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewPlanner.Rows[rowCount].Cells[6].Style.BackColor = Color.FromName(MainForm.GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.Gray;
                    }

                    if (currentYearMonth)
                    {
                        if (dayCount == currentDayNumber)
                        {
                            cellNumber = 6;
                            rowNumber = rowCount;
                            futureDays = true;
                        }
                    }

                    rowCount++;
                    weekNumber++;
                }

                //Highlight the current day if on the curernt month and year:
                if (currentYearMonth)
                {
                    dataGridViewPlanner.CurrentCell = dataGridViewPlanner.Rows[rowNumber-2].Cells[cellNumber];
                }

                //First Week of the month:
                if (day1 == 1)
                {
                    //no changes:
                }
                else if (day2 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;

                }
                else if (day3 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[1].Style.BackColor = Color.LightGray;

                }
                else if (day4 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[2].Style.BackColor = Color.LightGray;

                }
                else if (day5 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[3].Style.BackColor = Color.LightGray;

                }
                else if (day6 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[4].Style.BackColor = Color.LightGray;
                }
                else if (day7 == 1)
                {
                    dataGridViewPlanner.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[0].Cells[5].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[1].Cells[5].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewPlanner.Rows[2].Cells[5].Style.BackColor = Color.LightGray;
                }

                //dataGridViewPlanner.Columns[0].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[8].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Rows[0].Cells[7].Style.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[0].Width = 30;
                // Specify a larger font for the "Date" row. 
                using (Font font = new Font(
                    dataGridViewPlanner.DefaultCellStyle.Font.FontFamily, 23, FontStyle.Bold))
                {
                    dataGridViewPlanner.Rows[0].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[3].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[6].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[9].DefaultCellStyle.Font = font;
                    dataGridViewPlanner.Rows[12].DefaultCellStyle.Font = font;
                    if (sixRow)
                    {
                        dataGridViewPlanner.Rows[15].DefaultCellStyle.Font = font;
                    }
                }

                dataGridViewPlanner.Rows[0].Height = 33;
                dataGridViewPlanner.Rows[1].Height = 28;
                dataGridViewPlanner.Rows[2].Height = 28;
                dataGridViewPlanner.Rows[3].Height = 33;
                dataGridViewPlanner.Rows[4].Height = 28;
                dataGridViewPlanner.Rows[5].Height = 28;
                dataGridViewPlanner.Rows[6].Height = 33;
                dataGridViewPlanner.Rows[7].Height = 28;
                dataGridViewPlanner.Rows[8].Height = 28;
                dataGridViewPlanner.Rows[9].Height = 33;
                dataGridViewPlanner.Rows[10].Height = 28;
                dataGridViewPlanner.Rows[11].Height = 28;
                dataGridViewPlanner.Rows[12].Height = 33;
                dataGridViewPlanner.Rows[13].Height = 28;
                dataGridViewPlanner.Rows[14].Height = 28;

                //Changes if a 6th week is needed:
                if (sixRow)
                {
                    dataGridViewPlanner.Rows[15].Height = 33;
                    dataGridViewPlanner.Rows[16].Height = 28;
                    dataGridViewPlanner.Rows[17].Height = 28;
                    Size gridSize = new Size(904, 580);
                    dataGridViewPlanner.Size = gridSize;
                }
                else
                {
                    Size gridSize = new Size(904, 493);
                    dataGridViewPlanner.Size = gridSize;
                }

                dataGridViewPlanner.AllowUserToResizeRows = false;
                dataGridViewPlanner.AllowUserToResizeColumns = false;
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Calendar: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Calendar.  Review the log for more information.");
            }

            dataGridViewPlanner.Refresh();
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
            RunPlanner2();
        }

        private void btSavePlanner_Click(object sender, EventArgs e)
        {
            lbPlannerError.Hide();          

            //*****************************************************************************
            //*************  VERIFY INPUT DATA IS IN CORRECT FORMAT ***********************
            //*****************************************************************************
            double doubleValue = 0;

            if (!double.TryParse(tbDayPlanner1.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 1.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner2.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 2.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner3.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 3.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner4.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 4.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner5.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 5.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner6.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 6.";
                lbPlannerError.Show();
                return;
            }

            if (!double.TryParse(tbDayPlanner7.Text, out doubleValue))
            {
                lbPlannerError.Text = "The Ride Distance value is incorrect for Day 7.";
                lbPlannerError.Show();
                return;
            }

            if (cbPlannerDate.SelectedIndex < 1)
            {
                lbPlannerError.Text = "The Week selection has not been selected from the dropdown list.";
                lbPlannerError.Show();
            }

            DialogResult result = MessageBox.Show("Do you really want to Save the current Planning data for the selected week?", "Save Planning Data", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            string dateString = cbPlannerDate.SelectedItem.ToString();
            DateTime firstDay = DateTime.ParseExact(dateString, "dd/MM/yyyy", null);

            //Need to check if an entry exists for the current date:
            //Save each day's plan value to the database:

            List<int> idList = CheckDateExists(firstDay);

            if (idList.Count > 0)
            {
                MessageBox.Show("More than one ride entry found for " + firstDay);
            }

            //If blank, then need to add a new entry:
            //Else update that already exists:

            ExtractAssociatedIconEx();
        }

        private static List<int> CheckDateExists(DateTime dateValue)
        {
            List<int> returnValue = new List<int>();

            try
            {              
                List<object> objectValues = new List<object>
            {
                dateValue
            };
                //ExecuteScalarFunction
                using (var results = MainForm.ExecuteSimpleQueryConnection("Ride_Information_Exists", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            returnValue.Add(int.Parse(results[0].ToString()));
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to run query if date exists: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering if date exists.  Review the log for more information.");
            }

            return returnValue;
        }

        System.Windows.Forms.ListView listView1;
        ImageList imageList1;
        public void ExtractAssociatedIconEx()
        {
            // Initialize the ListView, ImageList and Form.
            listView1 = new System.Windows.Forms.ListView();
            imageList1 = new ImageList();
            listView1.Location = new Point(37, 12);
            listView1.Size = new Size(151, 262);
            listView1.SmallImageList = imageList1;
            listView1.View = View.SmallIcon;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.listView1);
            this.Text = "Form1";

            // Get the c:\ directory.
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\");

            ListViewItem item;
            listView1.BeginUpdate();

            // For each file in the c:\ directory, create a ListViewItem
            // and set the icon to the icon extracted from the file.
            foreach (System.IO.FileInfo file in dir.GetFiles())
            {
                // Set a default icon for the file.
                Icon iconForFile = SystemIcons.WinLogo;

                item = new ListViewItem(file.Name, 1);

                // Check to see if the image collection contains an image
                // for this extension, using the extension as a key.
                if (!imageList1.Images.ContainsKey(file.Extension))
                {
                    // If not, add the image to the image list.
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
                    imageList1.Images.Add(file.Extension, iconForFile);
                }
                item.ImageKey = file.Extension;
                listView1.Items.Add(item);
            }
            listView1.EndUpdate();
        }
    }
}
