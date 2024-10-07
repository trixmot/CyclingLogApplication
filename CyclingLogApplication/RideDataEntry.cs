using System;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;


//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static DGVPrinterHelper.DGVPrinter;
using System.Xml.Linq;

namespace CyclingLogApplication
{
    public partial class RideDataEntry : Form
    {
        private static string setCommand;
        private static Dictionary<string, string> sqlParameters;
        private static int formLoad = 1;
        private static int formClosing = 0;
        private SqlConnection sqlConnection;// = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
        private DatabaseConnection databaseConnection;// = new DatabaseConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");

        //MainForm mainForm;
        public RideDataEntry()
        {
            InitializeComponent();
            //MainForm mainForm = new MainForm();
            sqlConnection = MainForm.GetsqlConnectionString();
            databaseConnection = MainForm.GetsDatabaseConnectionString();

            //Hidden field to store the record id of the current displaying record that has been loaded on the page:
            tbRecordID.Hide();
            //tbWeekNumber.Hide();
            tbRecordID.Text = "0";
            lbRideDataEntryError.Hide();
            lbRideDataEntryError.Text = "";

            // Set the Minimum, Maximum, and initial Value.
            numericUpDown1.Value = 0;
            numericUpDown1.Maximum = 200;
            numericUpDown1.Minimum = 0;
            numericUpDown1.DecimalPlaces = 2;
            numericUpDown1.Increment = 0.10M;

            numDistanceRideDataEntry.Value = 0;
            numDistanceRideDataEntry.Maximum = 200;
            numDistanceRideDataEntry.Minimum = 0;
            numDistanceRideDataEntry.DecimalPlaces = 2;
            numDistanceRideDataEntry.Increment = 0.01M;

            //tbComments.ScrollBars = ScrollBars.Horizontal;

            dtpTimeRideDataEntry.Format = DateTimePickerFormat.Custom;
            //For 24 H format
            dtpTimeRideDataEntry.CustomFormat = "HH:mm:ss";
            dtpTimeRideDataEntry.ShowUpDown = true;
            dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            //Update Ride Type cbRideTypeDataEntry:
            //cbRideTypeDataEntry.Items.Add("Recovery");
            //cbRideTypeDataEntry.Items.Add("Base");
            //cbRideTypeDataEntry.Items.Add("Distance");
            //cbRideTypeDataEntry.Items.Add("Speed");
            //cbRideTypeDataEntry.Items.Add("Race");

            List<string> routeList = MainForm.GetRoutes();
            for (int i = 0; i < routeList.Count; i++)
            {
                cbRouteDataEntry.Items.Add(routeList.ElementAt(i));
            }

            List<string> logYearList = MainForm.GetLogYears();
            for (int i = 0; i < logYearList.Count; i++)
            {
                cbLogYearDataEntry.Items.Add(logYearList.ElementAt(i));
            }

            cbLogYearDataEntry.SelectedIndex = MainForm.GetLastLogSelectedDataEntry();

            //Set index for the LogYear:
            int logYearIndex = Convert.ToInt32(MainForm.GetLastLogSelectedDataEntry());

            if (logYearIndex == -1)
            {
                lbRideDataEntryError.Show();
                lbRideDataEntryError.Text = "No Log Year selected.";
            }
            else
            {
                //Note: the cbLogYearDataEntry is loaded from the mainForm load:
                lbRideDataEntryError.Hide();
            }

            formLoad = 1;
            numericUpDown2.Value = 1;
            numericUpDown2.Maximum = 1;
            numericUpDown2.Minimum = 1;
            numericUpDown2.Enabled = false;

            ConfigurationFile configurationFile = new ConfigurationFile();
            ConfigurationFile.ReadConfigFile(false);
            string customField1 = MainForm.GetCustomField1();
            string customField2 = MainForm.GetCustomField2();

            if (customField1.Equals(""))
            {
                //lbCustom1.Text = "Custom1";
                lbCustom1.Visible = false;
                tbCustom1.Visible = false;
            }
            else
            {
                lbCustom1.Text = MainForm.GetCustomField1();
                lbCustom1.Visible = true;
                tbCustom1.Visible = true;
            }

            if (customField2.Equals(""))
            {
                //lbCustom2.Text = "Custom2";
                lbCustom2.Visible = false;
                tbCustom2.Visible = false;
            }
            else
            {
                lbCustom2.Text = MainForm.GetCustomField2();
                lbCustom2.Visible = true;
                tbCustom2.Visible = true;
            }

            List<string> bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");
            //Load Bike values:
            foreach (var val in bikeList)
            {
                cbBikeDataEntrySelection.Items.Add(val);
            }

            lbRideDataEntryError.Text = "";
        }

        public void AddLogYearDataEntry(string item)
        {
            cbLogYearDataEntry.Items.Add(item);
        }

        public void RemoveLogYearDataEntry(string item)
        {
            cbLogYearDataEntry.Items.Remove(item);
        }

        public void AddRouteDataEntry(string item)
        {
            cbRouteDataEntry.Items.Add(item);
        }

        public void RemoveRouteDataEntry(string item)
        {
            cbRouteDataEntry.Items.Remove(item);
        }

        public void AddBikeDataEntry(string item)
        {
            cbBikeDataEntrySelection.Items.Add(item);
        }

        public void RemoveBikeDataEntry(string item)
        {
            cbBikeDataEntrySelection.Items.Remove(item);
        }

        public void SetLastLogYearSelected(int index)
        {
            cbLogYearDataEntry.SelectedIndex = index;
        }

        public void SetcbLogYearDataEntryIndex(int index)
        {
            cbLogYearDataEntry.SelectedIndex = index;
        }

        //logyear date route bike time distance avg_speed wind temp type location effort comfort
        public void SettbWeekCountRDE(string weekNumber)
        {
            tbWeekCountRDE.Text = weekNumber;
        }

        public void SetDate(DateTime date)
        {
            dtpRideDate.Value = date;
        }

        public void SetTime(DateTime time)
        {
            dtpTimeRideDataEntry.Value = time;
        }

        public void SetDistance(decimal distance)
        {
            numDistanceRideDataEntry.Value = distance;
        }

        public void SetCalories(string caloriesValue)
        {
            calories.Text = caloriesValue;
        }

        public void SetRoute(int routeIndex)
        {
            cbRouteDataEntry.SelectedIndex=routeIndex;
        }

        public void SetBike(int bikeIndex)
        {
            cbBikeDataEntrySelection.SelectedIndex = bikeIndex;
        }

        //avg_cadence tbMaxCadence avg_heart_rate max_heart_rate total_ascent total_descent max_speed avg_power max_power custom1 custom2 comments

        //Diable x close option:
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (formClosing == 0)
            {
                if (cbLogYearDataEntry.SelectedIndex == -1)
                {
                    lbRideDataEntryError.Show();
                    lbRideDataEntryError.Text = "No Log Year selected";

                    return;
                }
                formLoad = 0;

                // Look up to see if there is an entry by this date:
                //GetRideData(dtpRideDate.Value.Date, 1);
            }
        }

        private void GetRideData(DateTime date, int recordDateIndex)
        {
            if (formLoad == 1)
            {
                return;
            }

            List<object> objectValues = new List<object>
            {
                date
            };
            //int logLevel;

            int logID = 0;
            List<object> objectValuesLogID = new List<object>
            {
                dtpRideDate.Value.Year
            };
            dtpRideDate.Format = DateTimePickerFormat.Custom;
            // Display the date as "Mon 27 Feb 2012".  
            dtpRideDate.CustomFormat = "ddd dd MMM yyyy";
            //dtpRideDate.MaximumSize(20);

            using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index", objectValuesLogID))
            {
                if (results != null && results.HasRows)
                {
                    while (results.Read())
                    {
                        logID = Int32.Parse(results[0].ToString());
                    }
                }
                else
                {
                    //No matching date found
                }
            }
            //using (MainForm mainForm = new MainForm(""))
            //{
            //    logID = mainForm.GetLogYearIndex(cbLogYearDataEntry.SelectedItem.ToString());
            //    logLevel = mainForm.GetLogLevel();
            //}
            objectValues.Add(Convert.ToInt32(logID));

            string movingTime;
            string rideDistance;
            string avgSpeed;
            string bike;
            string rideType;
            string wind;
            string temp;
            string avgCadence;
            string maxCadence;
            string avgHeartRate;
            string maxHeartRate;
            string caloriesField;
            string totalAscent;
            string totalDescent;
            string maxSpeed;
            string avgPower;
            string maxPower;
            string route;
            string comments;
            string location;
            string recordID;
            string weekNumber;
            string effort;
            string comfort;
            string custom1;
            string custom2;

            int recordIndex = 0;

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("GetRideData", objectValues))
                {
                    //Logger.Log("Results: " + results.FieldCount, 0, logLevel);
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            recordIndex++;
                            if (recordDateIndex == recordIndex)
                            {
                                //MessageBox.Show(String.Format("{0}", results[0]));
                                lbRideDataEntryError.Hide();

                                movingTime = results[0].ToString();
                                rideDistance = results[1].ToString();
                                avgSpeed = results[2].ToString();
                                bike = results[3].ToString();
                                rideType = results[4].ToString();
                                wind = results[5].ToString();
                                temp = results[6].ToString();
                                avgCadence = results[7].ToString();
                                maxCadence = results[8].ToString();
                                avgHeartRate = results[9].ToString();
                                maxHeartRate = results[10].ToString();
                                caloriesField = results[11].ToString();
                                totalAscent = results[12].ToString();
                                totalDescent = results[13].ToString();
                                double maxSpeedDouble = double.Parse(results[14].ToString());
                                maxSpeed = (Math.Round(maxSpeedDouble, 1)).ToString();
                                avgPower = results[15].ToString();
                                maxPower = results[16].ToString();
                                route = results[17].ToString();
                                comments = results[18].ToString();
                                location = results[19].ToString();
                                recordID = results[20].ToString();
                                weekNumber = results[21].ToString();
                                effort = results[22].ToString();
                                comfort = results[23].ToString();
                                custom1 = results[24].ToString();
                                custom2 = results[25].ToString();

                                //Load ride data page:
                                dtpTimeRideDataEntry.Value = Convert.ToDateTime(movingTime);
                                numDistanceRideDataEntry.Value = Convert.ToDecimal(rideDistance);
                                numericUpDown1.Value = Convert.ToDecimal(avgSpeed);
                                cbBikeDataEntrySelection.SelectedIndex = cbBikeDataEntrySelection.Items.IndexOf(bike);
                                cbRideTypeDataEntry.SelectedIndex = cbRideTypeDataEntry.Items.IndexOf(rideType);
                                numericUpDown4.Value = Convert.ToDecimal(wind);
                                numericUpDown3.Value = Convert.ToDecimal(temp);
                                avg_cadence.Text = avgCadence;
                                tbMaxCadence.Text = maxCadence;
                                avg_heart_rate.Text = avgHeartRate;
                                max_heart_rate.Text = maxHeartRate;
                                calories.Text = caloriesField;
                                total_ascent.Text = totalAscent;
                                total_descent.Text = totalDescent;
                                max_speed.Text = maxSpeed;
                                avg_power.Text = avgPower;
                                max_power.Text = maxPower;
                                cbRouteDataEntry.SelectedIndex = cbRouteDataEntry.Items.IndexOf(route);
                                tbComments.Text = comments;
                                tbRecordID.Text = recordID;
                                //tbWeekNumber.Text = weekNumber;
                                cbLocationDataEntry.SelectedIndex = cbLocationDataEntry.Items.IndexOf(location);
                                cbEffortRideDataEntry.SelectedIndex = cbEffortRideDataEntry.Items.IndexOf(effort);
                                cbComfortRideDataEntry.SelectedIndex = cbComfortRideDataEntry.Items.IndexOf(comfort);
                                tbCustom1.Text = custom1;
                                tbCustom2.Text = custom2;
                            }
                            else
                            {
                                Logger.LogError("Ride data for an unselected date index was selected.");
                            }
                        }
                    }
                    else
                    {
                        lbRideDataEntryError.Show();
                        lbRideDataEntryError.Text = "No ride data found for the selected date.";
                        tbRecordID.Text = "0";
                        ClearDataEntryFields();
                        recordIndex = 1;
                    }

                    numericUpDown2.Maximum = recordIndex;
                    numericUpDown2.Minimum = 1;
                }

                if (recordIndex == 1)
                {
                    numericUpDown2.Enabled = false;
                }
                else
                {
                    numericUpDown2.Enabled = true;
                    
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive ride data." + ex.Message.ToString());
            }
        }

        private void SubmitData(object sender, EventArgs e)
        {
            lbRideDataEntryError.Text = "";
            lbRideDataEntryError.Hide();

            RideInformationChange("Add", "Ride_Information_Add");
        }

        private void CloseRideDataEntry(object sender, EventArgs e)
        {
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastBikeSelected(cbBikeDataEntrySelection.SelectedIndex);
                MainForm.SetLastLogSelectedDataEntry(cbLogYearDataEntry.SelectedIndex);
                //MainForm mainForm2 = new MainForm();
                //mainForm2.refreshData();
                //Close();
                //this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
                //DialogResult result = MessageBox.Show("Any unsaved changes will be lost, do you want to continue?", "Exit Data Entry Form", MessageBoxButtons.YesNo);
                //if (result == DialogResult.Yes)
                // {
                //Close();
                
                formClosing = 1;
                this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
                //}
            }
        }

        //private void textBox1_Validating(object sender, CancelEventArgs e)
        //{
        //    TextBox box = sender as TextBox;
        //    string pattern = "\\d{1,2}:\\d{2}\\s*(AM|PM)";
        //    //textBox1.Text = string.Format("{00:00:00}", 55);
        //    if (box != null)
        //    {
        //        if (!Regex.IsMatch(box.Text, pattern, RegexOptions.CultureInvariant))
        //        {
        //            MessageBox.Show("Not a valid time format ('hh:mm AM|PM').");
        //            e.Cancel = true;
        //            box.Select(0, box.Text.Length);
        //        }
        //    }
        //}

        //private void RideDataEntry_FormClosing(object sender, FormClosingEventArgs e)
        //{

        //    DialogResult result = MessageBox.Show("Do you really want to exit?", "Dialog Title", MessageBoxButtons.YesNo);
        //    if (result == DialogResult.Yes)
        //    {
        //        //Close();
        //        //this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        //        RideDataEntry.ActiveForm.Close();
        //    }
        //    else
        //    {
        //        e.Cancel = true;
        //    }
        //}

        private void RideInformationChange(string changeType, string procedureName)
        {
            lbRideDataEntryError.Hide();

            try
            {
                //Make sure certain required fields are filled in:
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                if (dtpTimeRideDataEntry.Value == dt)
                {
                    lbRideDataEntryError.Text = "The Ride Time must be greater than 0:00:00.";
                    lbRideDataEntryError.Show();
                    return;
                }
                decimal avgspeed = numericUpDown1.Value;
                if (avgspeed == 0)
                {
                    lbRideDataEntryError.Text = "The Average Speed must be greater than 0.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbLogYearDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "A Log year must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbRouteDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "A Route must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbBikeDataEntrySelection.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "A Bike must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbRideTypeDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "A Ride Type must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbLocationDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "A Ride Location must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbEffortRideDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "An Effort option must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbComfortRideDataEntry.SelectedIndex < 0)
                {
                    lbRideDataEntryError.Text = "An Comfort option must be selected.";
                    lbRideDataEntryError.Show();
                    return;
                }

                //===============================
                string year = "";
                List<object> objectValuesLogYear = new List<object>();
                objectValuesLogYear.Add(cbLogYearDataEntry.SelectedItem);

                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValuesLogYear))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            year = results[0].ToString();

                        }
                    }
                    else
                    {
                        // lbMaintError.Text = "No entry found for the selected Bike and Date.";
                        MessageBox.Show("No entry found for the selected Bike and Date.");
                        return;
                    }
                }

                DateTime rideDate = dtpRideDate.Value;
                if (!year.Equals(rideDate.Year.ToString()))
                {
                    lbRideDataEntryError.Text = "The Log year does not match up with the current ride date.";
                    lbRideDataEntryError.Show();
                    return;
                }

                //===============================
                int logSetting;
                int logIndex;

                using (MainForm mainForm = new MainForm(""))
                {
                    logSetting = MainForm.GetLogLevel();
                    logIndex = MainForm.GetLogYearIndex(cbLogYearDataEntry.SelectedItem.ToString());
                }

                //TODO: Run check to see if a record exists for this date:
                List<object> objectValuesRideDate = new List<object>();
                objectValuesRideDate.Add(dtpRideDate.Value);
                objectValuesRideDate.Add(logIndex);
                string entryID = "0";
                using (var results = ExecuteSimpleQueryConnection("CheckRideDate", objectValuesRideDate))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            entryID = results[0].ToString();
                        }
                    }
                    else
                    {
                        //No matching date found
                    }
                }

                string recordID = tbRecordID.Text;

                //MessageBox.Show("Record ID value: " + recordID);

                if (changeType.Equals("Update"))
                {
                    if (tbRecordID.Text.Equals("0"))
                    {
                        MessageBox.Show("The current ride can not be updated since it was not loaded from the database.");
                        Logger.Log("The current ride can not be updated since it was not loaded from the database." + recordID, logSetting, 0);

                        return;
                    }
                    DialogResult result = MessageBox.Show("Updating the ride in the log. Do you want to continue?", "Update Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                // Check recordID value:
                if (!entryID.Equals("0") && changeType.Equals("Add"))
                {
                    DialogResult result = MessageBox.Show("Detected that the current date was retrieved from a record that was already saved to the database. Do you want to continue adding the record?", "Add Ride Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        Logger.Log("Detected that the current data was retrieved from a record that was already saved to the database. RecordID" + recordID, logSetting, 0);

                        return;
                    }
                }
                else
                {
                    if (changeType.Equals("Add"))
                    {
                        DialogResult result = MessageBox.Show("Adding the ride to the log. Do you want to continue?", "Add Data", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }

                }

                List<object> objectValues = new List<object>();
                objectValues.Add(dtpTimeRideDataEntry.Value);                           //Moving Time:
                objectValues.Add(numDistanceRideDataEntry.Value);                       //Ride Distance:
                objectValues.Add(numericUpDown1.Value);                                 //Average Speed:
                objectValues.Add(cbBikeDataEntrySelection.SelectedItem.ToString());     //Bike:
                objectValues.Add(cbRideTypeDataEntry.SelectedItem.ToString());          //Ride Type:
                float windspeed = (float)numericUpDown4.Value;                          //----
                objectValues.Add(windspeed);                                            //Wind:
                float temp = (float)numericUpDown3.Value;                               //--
                objectValues.Add(Math.Round(temp, 1));                                   //Temp:
                objectValues.Add(dtpRideDate.Value);                                    //Date:

                if (avg_cadence.Text.Equals("") || avg_cadence.Text.Equals("--"))       //Average Cadence:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_cadence.Text));
                }

                if (tbMaxCadence.Text.Equals("") || tbMaxCadence.Text.Equals("--"))       //Max Cadence:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_cadence.Text));
                }

                if (avg_heart_rate.Text.Equals("") || avg_heart_rate.Text.Equals("--")) //Average Heart Rate:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_heart_rate.Text));
                }

                if (max_heart_rate.Text.Equals("") || max_heart_rate.Text.Equals("--")) //Max Heart Rate:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(max_heart_rate.Text));
                }

                if (calories.Text.Equals("") || calories.Text.Equals("--"))             //Calories:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(calories.Text));
                }

                if (total_ascent.Text.Equals("") || total_ascent.Text.Equals("--"))     //Total Ascent:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(total_ascent.Text));
                }

                if (total_descent.Text.Equals("") || total_descent.Text.Equals("--"))   //Total Descent:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(total_descent.Text));
                }

                if (max_speed.Text.Equals("") || max_speed.Text.Equals("--"))           //Max Speed:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(max_speed.Text));
                }

                if (avg_power.Text.Equals("") || avg_power.Text.Equals("--"))           //Average Power:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_power.Text));
                }

                if (max_power.Text.Equals("") || max_power.Text.Equals("--"))           //Max Power:
                {
                    objectValues.Add(0);
                }
                else
                {
                    objectValues.Add(float.Parse(max_power.Text));
                }

                objectValues.Add(cbRouteDataEntry.SelectedItem.ToString());             //Route:
                objectValues.Add(tbComments.Text);                                      //Comments:
                objectValues.Add(logIndex);                                             //LogYear index:

                //DateTime date = new DateTime();
                DayOfWeek firstDay = DayOfWeek.Monday;

                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                int weekValue = cal.GetWeekOfYear(dtpRideDate.Value, dfi.CalendarWeekRule, firstDay);

                if (changeType.Equals("Update"))                                        //Week number:
                {
                    objectValues.Add(Int32.Parse(tbWeekCountRDE.Text));
                }
                else
                {
                    objectValues.Add(weekValue);
                }

                objectValues.Add(cbLocationDataEntry.SelectedItem.ToString());          //Location:
                double winchill = 0;

                if (windspeed > 3 && temp > 50)                                          //Winchill:
                {
                    winchill = 35.74 + (0.6215) * (temp) - (35.75) * (Math.Pow(windspeed, 0.16)) + (0.4275) * (Math.Pow(windspeed, 0.16));
                    objectValues.Add(winchill.ToString());
                }
                else
                {
                    objectValues.Add("");
                }

                objectValues.Add(cbEffortRideDataEntry.SelectedItem.ToString());         //Effort:
                objectValues.Add(cbComfortRideDataEntry.SelectedItem.ToString());         //Comfort:
                objectValues.Add(tbCustom1.Text);                                                //Custom1
                objectValues.Add(tbCustom2.Text);                                                //Custom2

                if (changeType.Equals("Update"))
                {
                    objectValues.Add(entryID);                                         //Record ID:
                }

                using (var results = ExecuteSimpleQueryConnection(procedureName, objectValues))
                {
                    if (results == null)
                    {
                        if (changeType.Equals("Update"))
                        {
                            MessageBox.Show("[ERROR] There was a problem updating the ride.");
                        }
                        else
                        {
                            MessageBox.Show("[ERROR] There was a problem adding the ride.");
                        }
                    }
                    else
                    {
                        if (changeType.Equals("Update"))
                        {
                            MessageBox.Show("The ride has been updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("The ride has been added successfully.");
                        }
                    }

                    return;
                }

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to update ride information." + ex.Message.ToString());
            }
        }

        public SqlDataReader ExecuteSimpleQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";
            SqlDataReader ToReturn = null;

            try
            {
                for (int i = 0; i < _Parameters.Count; i++)
                {
                    tmpProcedureName += "@" + i.ToString() + ",";
                }

                tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
                ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName, _Parameters);

            }
            catch (Exception ex)
            {
                Logger.Log("[ERROR] SQL Query - Exception occurred:  " + ex.Message.ToString(), 0, 0);
            }

            return ToReturn;
        }

        private void RideDataEntryLoad(object sender, EventArgs e)
        {
            // NOTE: This line of code loads data into the 'cyclingLogDatabaseDataSet.Table_Ride_Information' table. You can move, or remove it, as needed.
            this.table_Ride_InformationTableAdapter.Fill(this.cyclingLogDatabaseDataSet.Table_Ride_Information);
            MainForm mainForm = new MainForm("");
            cbBikeDataEntrySelection.SelectedIndex = MainForm.GetLastBikeSelected();
            // cbLogYearDataEntry.SelectedIndex = cbRouteDataEntry.FindStringExact("");
        }

        private void RideDataEntryFormClosed(object sender, FormClosedEventArgs e)
        {
            //clearDataEntryFields();
        }

        private void ImportData(object sender, EventArgs e)
        {
            //Clear values on form:
            avg_cadence.Text = "";                                                                                      //Average Cadence:
            avg_heart_rate.Text = "";                                                                                   //Average Heart Rate:
            max_heart_rate.Text = "";                                                                                   //Max Heart Rate:
            calories.Text = "";                                                                                         //Calories:
            total_ascent.Text = "";                                                                                     //Total Ascent:
            total_descent.Text = "";                                                                                    //Total Descent:
            max_speed.Text = "";                                                                                        //Max Speed:
            avg_power.Text = "";                                                                                        //Average Power:
            max_power.Text = "";
            tbComments.Text = "";
            tbMaxCadence.Text = "";
            tbCustom1.Text = "";
            tbCustom2.Text = "";

            string[] headingList = new string[16];
            string[] splitList = new string[16];
            Dictionary<string, string> garminDataDictionary = new Dictionary<string, string>();

            lbRideDataEntryError.Text = "";
            lbRideDataEntryError.Hide();

            // Only the Summary line is required:
            try
            {
                using (OpenFileDialog openfileDialog = new OpenFileDialog() { Filter = "CSV|*.csv", Multiselect = false })
                {
                    if (openfileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string line;
                        using (StreamReader file = new StreamReader(openfileDialog.FileName))
                        {
                            int rowCount = 0;

                            while ((line = file.ReadLine()) != null)
                            {
                                var tempList = line.Split(',');
                                string summaryCheck = tempList[0];

                                if (rowCount == 0)
                                {
                                    //Line 1 is the headings
                                    headingList = line.Split(',');

                                    for (int i = 0; i < headingList.Length; i++)
                                    {
                                        garminDataDictionary.Add(headingList[i], "");
                                    }
                                }
                                else if (summaryCheck.Contains("Summary"))
                                {
                                    var sep = new string[] { "\",\"" };
                                    splitList = line.Split(sep, StringSplitOptions.None);

                                    for (int i = 0; i < headingList.Length; i++)
                                    {
                                        string dataValue = splitList[i];
                                        dataValue = dataValue.Replace("\"", string.Empty);
                                        garminDataDictionary[headingList[i]] = dataValue;
                                    }
                                }
                                rowCount++;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }

                //--------------------------------------------------
                //0 Laps:
                //1 Time
                //2 Cumulative Time
                //3 Distance
                //4 Avg Speed
                //5 Avg HR
                //6 Max HR
                //7 Total Ascent
                //8 Total Descent
                //9 Calories
                //10 Avg Temperature
                //11 Max Speed
                //12 Moving Time
                //13 Avg Moving Speed
                //11Avg Bike Cadence
                //12Max Bike Cadence

                string temp = "";

                //Split time and enter hours-min-sec
                if (garminDataDictionary.TryGetValue("\"Moving Time\"", out string value))
                {
                    temp = value;
                }
                else
                {
                    if (garminDataDictionary.TryGetValue("\"Time\"", out value))
                    {
                        temp = value;
                    }
                }

                string[] temp2 = temp.Split(':');

                if (temp2.Length == 3)
                {
                    temp2[2] = temp2[2].Split('.')[0];
                }

                //Check size to see if hours is included:
                //hh:mm:ss
                if (temp2.Length == 0)
                {
                    //MessageBox.Show("Count is 0");
                    dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                }
                else if (temp2.Length == 1)
                {
                    //MessageBox.Show("Count is 1");
                    dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, Int32.Parse(temp2[0]));
                }
                else if (temp2.Length == 2)
                {
                    //MessageBox.Show("Count is 2");
                    dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, Int32.Parse(temp2[0]), Int32.Parse(temp2[1]));
                }
                else if (temp2.Length == 3)
                {
                    //MessageBox.Show("Count is 3");
                    dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Int32.Parse(temp2[0]), Int32.Parse(temp2[1]), Int32.Parse(temp2[2]));
                }

                numDistanceRideDataEntry.Maximum = (200);
                numDistanceRideDataEntry.Value = System.Convert.ToDecimal(garminDataDictionary["\"Distance\""]);

                if (garminDataDictionary.TryGetValue("\"Total Ascent\"", out value))
                {
                    total_ascent.Text = value;
                }

                if (garminDataDictionary.TryGetValue("\"Total Descent\"", out value))
                {
                    total_descent.Text = value;
                }

                if (garminDataDictionary.TryGetValue("\"Avg Moving Speed\"", out value))
                {
                    numericUpDown1.Value = System.Convert.ToDecimal(value);
                }
                else
                {
                    numericUpDown1.Value = System.Convert.ToDecimal(garminDataDictionary["\"Avg Speed\""]);
                }

                if (garminDataDictionary.TryGetValue("\"Max Speed\"", out value))
                {
                    max_speed.Text = value;
                }

                if (garminDataDictionary.TryGetValue("\"Avg HR\"", out value))
                {
                    avg_heart_rate.Text = value;
                    max_heart_rate.Text = garminDataDictionary["\"Max HR\""];
                }

                if (garminDataDictionary.TryGetValue("\"Avg Bike Cadence\"", out value))
                {
                    avg_cadence.Text = value;
                }

                if (garminDataDictionary.TryGetValue("\"Max Bike Cadence\"", out value))
                {
                    tbMaxCadence.Text = value;
                }

                if (garminDataDictionary.TryGetValue("\"Avg Temperature\"", out value))
                {
                    numericUpDown3.Value = decimal.Round(System.Convert.ToDecimal(value), 2, MidpointRounding.AwayFromZero);
                }

                calories.Text = garminDataDictionary["\"Calories\""];

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive ride data. " + ex.Message.ToString());
                MessageBox.Show("[ERROR] Exception occurred. Refer to the log for more information. ");
            }

            //tbRecordID.Text = "import";
        }

        // Used to clear the form on date changes:
        private void ClearDataEntryFields()
        {
            //Reset and clear values:
            dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);//Moving Time:
            numDistanceRideDataEntry.Value = 0;                                                                         //Ride Distance:
            numericUpDown1.Value = 0;                                                                                   //Average Speed:
            cbBikeDataEntrySelection.SelectedIndex = cbBikeDataEntrySelection.FindStringExact(""); ;                    //Bike:
            cbRideTypeDataEntry.SelectedIndex = cbRideTypeDataEntry.FindStringExact(""); ;                              //Ride Type:
            numericUpDown4.Value = 0;                                                                                   //Wind:
            numericUpDown3.Value = 0;                                                                                   //Temp:
            //dtpRideDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0); ;     //Date:
            avg_cadence.Text = "";                                                                                      //Average Cadence:
            avg_heart_rate.Text = "";                                                                                   //Average Heart Rate:
            max_heart_rate.Text = "";                                                                                   //Max Heart Rate:
            calories.Text = "";                                                                                         //Calories:
            total_ascent.Text = "";                                                                                     //Total Ascent:
            total_descent.Text = "";                                                                                    //Total Descent:
            max_speed.Text = "";                                                                                        //Max Speed:
            avg_power.Text = "";                                                                                        //Average Power:
            max_power.Text = "";                                                                                        //Max Power:
            cbRouteDataEntry.SelectedIndex = cbRouteDataEntry.FindStringExact(""); ;                                    //Route:
            tbComments.Text = "";                                                                                       //Comments:
            //cbLogYearDataEntry.SelectedIndex = cbLogYearDataEntry.FindStringExact("");                                //LogYear index:
            cbLocationDataEntry.SelectedIndex = cbLocationDataEntry.FindStringExact("");
            cbEffortRideDataEntry.SelectedIndex = cbEffortRideDataEntry.FindStringExact("");
            //tbWeekNumber.Text = "0";
            tbRecordID.Text = "0";
            cbComfortRideDataEntry.SelectedIndex = cbComfortRideDataEntry.FindStringExact("");
            tbCustom1.Text = "";
            tbCustom2.Text = "";
            tbMaxCadence.Text = "";
        }

        private void ClearDataEntryFields_click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Clearing all fields. Do you want to continue?", "Clear Fields", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                lbRideDataEntryError.Text = "";
                lbRideDataEntryError.Hide();

                ClearDataEntryFields();
            }
        }

        private void CbLogYearDataEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MainForm mainForm = new MainForm(""))
            {
                MainForm.SetLastLogSelected(cbLogYearDataEntry.SelectedIndex);
                if (cbLogYearDataEntry.SelectedIndex == -1)
                {
                    lbRideDataEntryError.Show();
                    lbRideDataEntryError.Text = "No Log Year selected.";
                }
                else
                {
                    lbRideDataEntryError.Hide();

                    //Get current log year:
                    int logYear = 0;
                    List<object> objectValuesLogID = new List<object>();
                    string logName = cbLogYearDataEntry.SelectedItem.ToString();
                    objectValuesLogID.Add(logName);

                    using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValuesLogID))
                    {
                        if (results != null && results.HasRows)
                        {
                            while (results.Read())
                            {
                                logYear = Int32.Parse(results[0].ToString());
                            }
                        }
                        else
                        {
                            //No matching date found
                        }
                    }

                    //Update ride date to the year that matches the log:
                    //If current year, then also match current date:
                    int currentYear = DateTime.Now.Year;
                    if (currentYear == logYear)
                    {
                        dtpRideDate.Value = new DateTime(logYear, DateTime.Now.Month, DateTime.Now.Day);
                    } else
                    {
                        dtpRideDate.Value = new DateTime(logYear, 01, 01);
                    }
                    
                }
            }
        }

        private void DtpRideDate_ValueChanged(object sender, EventArgs e)
        {
            //Get current date and then obtain and set week number:
            //var rideDataSate = dtpTimeRideDataEntry.Value;
            //DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            //Logger.LogError("Calendar value: " + dtpRideDate.Value.Date);
            int weekValue = myCal.GetWeekOfYear(dtpRideDate.Value.Date, myCWR, myFirstDOW);
            tbWeekCountRDE.Text = weekValue.ToString();
        }

        private void BtUpdateRideDateEntry_Click(object sender, EventArgs e)
        {
            RideInformationChange("Update", "Ride_Information_Update");
        }

        private void BtDeleteRideDataEntry_Click(object sender, EventArgs e)
        {
            //Get ride recordID:
            string rideRecordID = tbRecordID.Text;

            DialogResult result = MessageBox.Show("Do you really want to delete the Ride and all its data?", "Delete Ride From Database", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // conn and reader declared outside try block for visibility in finally block
                //SqlConnection conn = null;
                //SqlDataReader reader = null;

                int returnValue;

                try
                {

                    List<object> objectValuesRideDate = new List<object>
                    {
                        rideRecordID
                    };

                    using (var results = ExecuteSimpleQueryConnection("DeleteRideByID", objectValuesRideDate))
                    {
                        if (results != null && results.HasRows)
                        {
                            while (results.Read())
                            {
                                returnValue = Int32.Parse(results[0].ToString());
                            }
                        }
                        else
                        {
                            //No matching date found
                        }
                    }
                    //// instantiate and open connection
                    ////conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                    //sqlConnection.Open();

                    //// 1. declare command object with parameter
                    //using (SqlCommand cmd = new SqlCommand("DELETE FROM Table_Ride_Information WHERE @Id=[Id]", sqlConnection))
                    //{
                    //    // 2. define parameters used in command object
                    //    SqlParameter param = new SqlParameter
                    //    {
                    //        ParameterName = "@Id",
                    //        Value = rideRecordID
                    //    };

                    //    // 3. add new parameter to command object
                    //    cmd.Parameters.Add(param);

                    //    // get data stream
                    //    reader = cmd.ExecuteReader();
                    //}

                    //// write each record
                    //while (reader.Read())
                    //{
                    //    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //    //Console.WriteLine(String.Format("{0}", reader[0]));
                    //    string temp = reader[0].ToString();

                    //    if (temp.Equals(""))
                    //    {
                    //        returnValue = 0;
                    //    }
                    //    else
                    //    {
                    //        returnValue = int.Parse(temp);
                    //    }
                    //}

                    if (numericUpDown2.Enabled == true)
                    {
                        // Run to update the form with the current existing data:
                        Retrieve_run();
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to remove a Ride year from the database. rideRecordID:" + rideRecordID + " : " + ex.Message.ToString());
                }
                finally
                {
                    // close reader
                    //if (reader != null)
                    //{
                    //    reader.Close();
                   // }

                    // close connection
                    sqlConnection?.Close();
                }
            }
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (formLoad == 0)
            {
                NumericUpDown num = (NumericUpDown)sender;
                if (Convert.ToInt32(num.Text) > num.Value)
                {
                    //MessageBox.Show("Value decreased");
                    GetRideData(dtpRideDate.Value.Date, Convert.ToInt16(numericUpDown2.Value));
                }
                else
                {
                    //MessageBox.Show("Value increased");
                    GetRideData(dtpRideDate.Value.Date, Convert.ToInt16(numericUpDown2.Value));
                }
            }
        }

        private void DtpTimeRideDataEntry_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BtRetrieve_Click(object sender, EventArgs e)
        {
            formLoad = 0;
            Retrieve_run();
        }

        private void Retrieve_run()
        {
            if (cbLogYearDataEntry.SelectedIndex == -1)
            {
                MessageBox.Show("A Log Year must be selected.");

                return;
            }

            // Look up to see if there is an entry by this date:
            GetRideData(dtpRideDate.Value.Date, 1);

            int logID = 0;
            List<object> objectValuesLogID = new List<object>
            {
                dtpRideDate.Value.Year
            };

            using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index", objectValuesLogID))
            {
                if (results != null && results.HasRows)
                {
                    while (results.Read())
                    {
                        logID = Int32.Parse(results[0].ToString());
                    }
                }
                else
                {
                    //No matching date found
                }
            }

            List<object> objectValuesRideDate = new List<object>
            {
                dtpRideDate.Value,
                logID
            };
            int resultsCount = 0;
            using (var results = ExecuteSimpleQueryConnection("CheckRideDateCount", objectValuesRideDate))
            {
                if (results != null && results.HasRows)
                {
                    while (results.Read())
                    {
                        resultsCount = Int32.Parse(results[0].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No data found for the selected date.");
                }
            }

            if (resultsCount > 1)
            {
                //Alert user that multiple dates were found:
                MessageBox.Show("Multiple Rides were found for the selected date. Use the 'Multiple Rides' selecter to select the desired ride.");
            }

        }

        public void SetSetCommand(string setCommandString)
        {
            setCommand = setCommandString;
        }

        public string GetSetCommand()
        {
            return setCommand;
        }

        public void SetSqlParameters(Dictionary<string, string> sqlParametersDict)
        {
            sqlParameters = sqlParametersDict;
        }

        public Dictionary<string, string> GetSqlParameters() {
            return sqlParameters; 
        }

        public void UpdateRideInformation()
        {
            //Need list of fields to update:
         //   @MovingTime time,
         //   @RideDistance float,
         //   @AvgSpeed float,
         //   @Bike nvarchar(25),
	        //@RideType nvarchar(25),
	        //@Wind float,
         //   @Temperature float,
         //   @Date date,
	        //@AvgCadence float,
         //   @MaxCadence float,
         //   @AvgHeartRate float,
         //   @MaxHeartRate float,
         //   @Calories float,
         //   @TotalAscent float,
         //   @TotalDescent float,
         //   @MaxSpeed float,
         //   @AveragePower float,
         //   @MaxPower float,
         //   @Route nvarchar(50),
	        //@Comments nvarchar(200),
	        //@LogYearIndex bigint,
         //   @WeekNumber bigint,
	        //@Location nvarchar(25),
	        //@Windchill float,
         //   @Effort nvarchar(25),
	        //@Comfort nvarchar(25),
	        //@Custom1 nvarchar(25),
	        //@Custom2 nvarchar(25),
	        //@Id bigint

            SqlDataReader reader = null;
            //int idValue = 1550;

            string setCommand = GetSetCommand();

            Dictionary<string, string> sqlParametersDictionary = GetSqlParameters();

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET " + setCommand + " WHERE Id=@idValue", sqlConnection))
                {
                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter();
                    // 3. add new parameter to command object
                    for (int i = 0; i < sqlParametersDictionary.Count; i++)
                    {
                        cmd.Parameters.AddWithValue(sqlParametersDictionary.ElementAt(i).Key, sqlParametersDictionary.ElementAt(i).Value);
                    }
                    //cmd.Parameters.AddWithValue("@calories", "12345");
                    //cmd.Parameters.AddWithValue("@idValue", "1550");
                    // get data stream
                    reader = cmd.ExecuteReader();
                }

                // write each record
                while (reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying update ride data entry." + ex.Message.ToString());
            }
            finally
            {
                reader?.Close();
                sqlConnection?.Close();
            }
        }

        private void btUpdateEntry_Click(object sender, EventArgs e)
        {
            UpdateRideInformation();
        }

        //private void tbCustom1_TextChanged(object sender, EventArgs e)
        //{
        //    if (!tbCustom1.Text.Equals(""))
        //    {
        //        tbCustom2.Visible = true;
        //    }
        //    else
        //    {
        //        tbCustom2.Visible = false;
        //    }
        //}
    }
}
