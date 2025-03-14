﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;
using System.IdentityModel.Claims;
using System.Diagnostics;
using System.Net.PeerToPeer.Collaboration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Diagnostics.Eventing.Reader;

namespace CyclingLogApplication
{
    public partial class RideDataEntry : Form
    {
        private static int formLoad = 1;
        private static int formClosing = 0;
        private SqlConnection sqlConnection;
        private DatabaseConnection databaseConnection;
        private int id;
        private int logYearID;
        private DateTime date;
        private int bikeIndex;
        private string bike;
        private string route;


        public RideDataEntry()
        {
            InitializeComponent();
            sqlConnection = MainForm.GetsqlConnectionString();
            databaseConnection = MainForm.GetsDatabaseConnectionString();

            //Hidden field to store the record id of the current displaying record that has been loaded on the page:
            //tbRecordID.Hide();
            //tbWeekNumber.Hide();
            tbRecordID.Text = "0";
            lbRideDataEntryError.Hide();
            lbRideDataEntryError.Text = "";

            // Set the Minimum, Maximum, and initial Value.
            //numericUpDown1.Value = 0;
            //numericUpDown1.Maximum = 200;
            //numericUpDown1.Minimum = 0;
            //numericUpDown1.DecimalPlaces = 2;
            //numericUpDown1.Increment = 0.10M;

            //numDistanceRideDataEntry.Value = 0;
            //numDistanceRideDataEntry.Maximum = 200;
            //numDistanceRideDataEntry.Minimum = 0;
            //numDistanceRideDataEntry.DecimalPlaces = 2;
            //numDistanceRideDataEntry.Increment = 1.01M;

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
            cbRouteDataEntry.Items.Add("--Select Value--");
            for (int i = 0; i < routeList.Count; i++)
            {
                cbRouteDataEntry.Items.Add(routeList.ElementAt(i));
            }

            List<string> bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");
            cbBikeDataEntrySelection.Items.Add("--Select Value--");
            //Load Bike values:
            foreach (var val in bikeList)
            {
                cbBikeDataEntrySelection.Items.Add(val);
            }

            List<string> logYearList = MainForm.GetLogYears();
            cbLogYearDataEntry.Items.Add("--Select Value--");
            for (int i = 0; i < logYearList.Count; i++)
            {
                cbLogYearDataEntry.Items.Add(logYearList.ElementAt(i));
            }

            cbLogYearDataEntry.SelectedIndex = MainForm.GetLastLogSelectedDataEntry();

            //Set index for the LogYear:
            int logYearIndex = Convert.ToInt32(cbLogYearDataEntry.SelectedIndex);

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

            checkBoxCloneEntry.Enabled = false;
            checkBoxCloneEntry.Checked = false;

            if (cbComfortRideDataEntry.Items.Count < 3)
            {
                cbComfortRideDataEntry.Items.Add("--Select Value--");
                cbComfortRideDataEntry.Items.Add("Weak/Tight");
                cbComfortRideDataEntry.Items.Add("Average");
                cbComfortRideDataEntry.Items.Add("Strong");
            }

            if (cbRideTypeDataEntry.Items.Count < 6)
            {
                cbRideTypeDataEntry.Items.Add("--Select Value--");
                cbRideTypeDataEntry.Items.Add("Base");
                cbRideTypeDataEntry.Items.Add("Distance");
                cbRideTypeDataEntry.Items.Add("Race");
                cbRideTypeDataEntry.Items.Add("Recovery");
                cbRideTypeDataEntry.Items.Add("Speed");
                cbRideTypeDataEntry.Items.Add("Tour");
            }

            if (cbLocationDataEntry.Items.Count < 5)
            {
                cbLocationDataEntry.Items.Add("--Select Value--");
                cbLocationDataEntry.Items.Add("Road");
                cbLocationDataEntry.Items.Add("Rollers");
                cbLocationDataEntry.Items.Add("Trail");
                cbLocationDataEntry.Items.Add("Trainer");
            }

            if (cbEffortRideDataEntry.Items.Count < 5)
            {
                cbEffortRideDataEntry.Items.Add("--Select Value--");
                cbEffortRideDataEntry.Items.Add("Easy / Spin");
                cbEffortRideDataEntry.Items.Add("Moderate");
                cbEffortRideDataEntry.Items.Add("Hard");
                cbEffortRideDataEntry.Items.Add("Race");
            }

            cbRouteDataEntry.SelectedIndex = 0;
            //cbBikeDataEntrySelection.SelectedIndex = 0;
            cbRideTypeDataEntry.SelectedIndex = 0;
            cbLocationDataEntry.SelectedIndex = 0;
            cbEffortRideDataEntry.SelectedIndex = 0;
            cbComfortRideDataEntry.SelectedIndex = 0;

            ConfigurationFile configurationFile = new ConfigurationFile();
            ConfigurationFile.ReadConfigFile();
            string customField1 = MainForm.GetCustomField1();
            string customField2 = MainForm.GetCustomField2();

            if (customField1 == null || customField1.Equals(""))
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

            if (customField2 == null || customField2.Equals(""))
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

            lbRideDataEntryError.Text = "";
            btRideDisplayUpdate.Visible = false;
        }

        public void SetDateValue(DateTime dateValue)
        {
            date = dateValue;
        }

        public DateTime GetDateValue()
        {
            return date;
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
            tbRideDataEntryDistance.Text = distance.ToString();
        }

        public void SetEntryID(int idValue)
        {
            id = idValue;
        }

        public int GetEntryID()
        {
            return id;
        }

        public int GetLogYearID()
        {
            return logYearID;
        }

        public void SetLogYearID(int logYearIDValue)
        {
            logYearID = logYearIDValue;
        }

        public void SetCalories(string caloriesValue)
        {
            if (caloriesValue.Equals("") || caloriesValue.Equals("0")) {
                calories.Text = "- -";
            }
            else
            {
                calories.Text = caloriesValue;
            }
        }

        public void SetRouteIndex(int routeIndex)
        {
            cbRouteDataEntry.SelectedIndex = routeIndex;
        }

        public void SetRoute(string routeValue)
        {
            route = routeValue;
        }

        public void SetBike(string bikeValue)
        {
            bike = bikeValue;
        }

        public string GetBike()
        {
            return bike;
        }

        //Used to load display data into entry form:
        public void SetBikeIndex(int bikeIndexValue)
        {
            cbBikeDataEntrySelection.SelectedIndex = bikeIndexValue;
            //bikeIndex = bikeIndexValue;
        }

        public void SetAvgSpeed(double avgSpeed)
        {
            double speed = Math.Round(double.Parse(avgSpeed.ToString()), 1);
            tbRideDataEntryAvgSpeed.Text = speed.ToString();
        }

        public void SetWind(string windIndex)
        {
            if (windIndex.Equals("") || windIndex.Equals("- -"))
            {
                tbRideDataEntryWind.Text = "- -";
            } else
            {
                double wind = Math.Round(double.Parse(windIndex), 1);
                tbRideDataEntryWind.Text = wind.ToString();
            }
        }

        public void SetTemp(string temp)
        {
            if (temp.Equals("") || temp.Equals("- -")) { 
                tbRideEntryTemp.Text = "- -";
            } else 
            {
                tbRideEntryTemp.Text = temp;
            }
        }


        public void SetType(int typeIndex)
        {
            cbRideTypeDataEntry.SelectedIndex = typeIndex;
        }

        public void SetLocation(int locationIndex)
        {
            cbLocationDataEntry.SelectedIndex = locationIndex;
        }

        public void SetEffort(int effortIndex)
        {
            cbEffortRideDataEntry.SelectedIndex = effortIndex;
        }

        public void SetComfort(int comfortIndex)
        {
            cbComfortRideDataEntry.SelectedIndex = comfortIndex;
        }


        public void SetAvgCadence(string avgCadence)
        {
            if (avgCadence.Equals("0") || avgCadence.Equals(""))
            {
                avg_cadence.Text = "- -";
            } else
            {
                avg_cadence.Text = avgCadence;
            }
            
        }

        public void SetMaxCadence(string maxCadence)
        {
            if (maxCadence.Equals("0") || maxCadence.Equals(""))
            {
                tbMaxCadence.Text = "- -";
            } else
            {
                tbMaxCadence.Text = maxCadence;
            }
            
        }

        public void SetAvgHeartRate(string avgHeartRate)
        {
            if (avgHeartRate.Equals("0") || avgHeartRate.Equals(""))
            {
                avg_heart_rate.Text = "- -";
            } else
            {
                avg_heart_rate.Text = avgHeartRate;
            }
            
        }

        public void SetMaxHeartRate(string maxHeartRate)
        {
            if (maxHeartRate.Equals("0") || maxHeartRate.Equals(""))
            {
                max_heart_rate.Text = "- -";
            } else
            {
                max_heart_rate.Text = maxHeartRate;
            }
            
        }

        public void SetTotalAscent(string totalAscent)
        {
            if (totalAscent.Equals(""))
            {
                total_ascent.Text = "- -";
            } else
            {
                total_ascent.Text = totalAscent;
            }
            
        }

        public void SetTotalDescent(string totalDescent)
        {
            if (totalDescent.Equals(""))
            {
                total_descent.Text = "- -";
            }
            else
            {
                total_descent.Text = totalDescent;
            }
            
        }

        public void SetMaxSpeed(string maxSpeed)
        {
            if (maxSpeed.Equals(""))
            {
                max_speed.Text = "- -";
            } else
            {
                max_speed.Text = maxSpeed;
            }
            
        }

        public void SetAvgPower(string avgPower)
        {
            if (avgPower.Equals("0") || avgPower.Equals(""))
            {
                avg_power.Text = "- -";
            }
            else
            {
                avg_power.Text = avgPower;
            }
            
        }

        public void SetMaxPower(string maxPower)
        {
            if (maxPower.Equals("0") || maxPower.Equals(""))
            {
                max_power.Text = "- -";
            }
            else
            {
                max_power.Text = maxPower;
            }
            
        }

        public void SetCustom1(string custom1)
        {
            if (custom1.Equals("0") || custom1.Equals(""))
            {
                tbCustom1.Text = "- -";
            }
            else
            {
                tbCustom1.Text = custom1;
            }
        }

        public void SetCustom2(string custom2)
        {
            if (custom2.Equals("0") || custom2.Equals(""))
            {
                tbCustom2.Text = "- -";
            }
            else
            {
                tbCustom2.Text = custom2;
            }
        }

        public void SetComments(string comments)
        {
            tbComments.Text = comments;
        }

        public void SetWindChill(string wind_Chill)
        {
            if (wind_Chill.Equals(""))
            {
                tbRideEntryWindChill.Text = "- -";
            }
            else
            {
                tbRideEntryWindChill.Text = wind_Chill;
            }
        }

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
                if (cbLogYearDataEntry.SelectedIndex < 1)
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
            checkBoxCloneEntry.Enabled = false;

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
            string windChill;

            int recordIndex = 0;

            try
            {
                //Get ride data using the date:
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
                                if (string.IsNullOrEmpty(results[5].ToString()))
                                {
                                    tbRideDataEntryWind.Text = "- -";
                                } else
                                {
                                    double windTemp = double.Parse(results[5].ToString());
                                    windTemp = Math.Round(windTemp, 1);
                                    wind = windTemp.ToString();
                                    tbRideDataEntryWind.Text = wind;
                                }
                                if (string.IsNullOrEmpty(results[6].ToString()) || results[6].ToString().Equals("0"))
                                {
                                    tbRideEntryTemp.Text = "- -";
                                } else
                                {
                                    temp = results[6].ToString();
                                    tbRideEntryTemp.Text = temp;
                                }
                                if (string.IsNullOrEmpty(results[7].ToString()) || results[7].ToString().Equals("0"))
                                {
                                    avg_cadence.Text = "- -";
                                } else
                                {
                                    avgCadence = results[7].ToString();
                                    avg_cadence.Text = avgCadence;
                                }
                                if (string.IsNullOrEmpty(results[8].ToString()) || results[8].ToString().Equals("0"))
                                {
                                    tbMaxCadence.Text = "- -";
                                } else
                                {
                                    maxCadence = results[8].ToString();
                                    tbMaxCadence.Text = maxCadence;
                                }
                                if (string.IsNullOrEmpty(results[9].ToString()) || results[9].ToString().Equals("0"))
                                {
                                    avg_heart_rate.Text = "- -";
                                } else
                                {
                                    avgHeartRate = results[9].ToString();
                                    avg_heart_rate.Text = avgHeartRate;                               
                                }
                                if (string.IsNullOrEmpty(results[10].ToString()) || results[10].ToString().Equals("0"))
                                {
                                    max_heart_rate.Text = "- -";
                                } else
                                {
                                    maxHeartRate = results[10].ToString();
                                    max_heart_rate.Text = maxHeartRate;
                                }
                                if (string.IsNullOrEmpty(results[11].ToString()) || results[11].ToString().Equals("0"))
                                {
                                    calories.Text = "- -";
                                }
                                else
                                {
                                    caloriesField = results[11].ToString();
                                    calories.Text = caloriesField;
                                }
                                if (string.IsNullOrEmpty(results[12].ToString()))
                                {
                                    total_ascent.Text = "- -";
                                }
                                else
                                {
                                    totalAscent = results[12].ToString(); //allow 0
                                    total_ascent.Text = totalAscent;
                                }
                                if (string.IsNullOrEmpty(results[13].ToString()))
                                {
                                    total_descent.Text = "- -";
                                } else
                                {
                                    totalDescent = results[13].ToString(); //allow 0
                                    total_descent.Text = totalDescent;
                                }
                                if (string.IsNullOrEmpty(results[14].ToString()) || results[14].ToString().Equals("0"))
                                {
                                    max_speed.Text = "- -";
                                } else {
                                    double maxSpeedDouble = double.Parse(results[14].ToString());
                                    maxSpeed = (Math.Round(maxSpeedDouble, 1)).ToString();
                                    max_speed.Text = maxSpeed;
                                }
                                if (string.IsNullOrEmpty(results[15].ToString()) || results[15].ToString().Equals("0"))
                                {
                                    avg_power.Text = "- -";
                                } else
                                {
                                    avgPower = results[15].ToString();
                                    avg_power.Text = avgPower;
                                }
                                if (string.IsNullOrEmpty(results[16].ToString()) || results[16].ToString().Equals("0"))
                                {
                                    max_power.Text = "- -";
                                } else
                                {
                                    maxPower = results[16].ToString();
                                    max_power.Text = maxPower;
                                }
                                route = results[17].ToString();
                                comments = results[18].ToString();
                                location = results[19].ToString();
                                recordID = results[20].ToString();
                                weekNumber = results[21].ToString();
                                effort = results[22].ToString();
                                comfort = results[23].ToString();
                                if (string.IsNullOrEmpty(results[24].ToString()) || results[24].ToString().Equals("0"))
                                {
                                    tbCustom1.Text = "- -";
                                } else
                                {
                                    custom1 = results[24].ToString();
                                    tbCustom1.Text = custom1;
                                }
                                if (string.IsNullOrEmpty(results[25].ToString()) || results[25].ToString().Equals("0"))
                                {
                                    tbCustom2.Text = "- -";
                                }
                                else
                                {
                                    custom2 = results[25].ToString();
                                    tbCustom2.Text = custom2;
                                }
                                if (string.IsNullOrEmpty(results[26].ToString()) || results[26].ToString().Equals("0"))
                                {
                                    tbRideEntryWindChill.Text = "- -";
                                } else
                                {
                                    windChill = results[26].ToString();
                                    tbRideEntryWindChill.Text = Math.Round(double.Parse(windChill), 1).ToString();
                                }                               

                                //Load ride data page:
                                dtpTimeRideDataEntry.Value = Convert.ToDateTime(movingTime);
                                tbRideDataEntryDistance.Text = rideDistance;
                                tbRideDataEntryAvgSpeed.Text = avgSpeed;
                                cbBikeDataEntrySelection.SelectedIndex = cbBikeDataEntrySelection.Items.IndexOf(bike);
                                cbRideTypeDataEntry.SelectedIndex = cbRideTypeDataEntry.Items.IndexOf(rideType);                                                                 
                                cbRouteDataEntry.SelectedIndex = cbRouteDataEntry.Items.IndexOf(route);
                                tbComments.Text = comments;
                                tbRecordID.Text = recordID;
                                //tbWeekNumber.Text = weekNumber;
                                cbLocationDataEntry.SelectedIndex = cbLocationDataEntry.Items.IndexOf(location);
                                cbEffortRideDataEntry.SelectedIndex = cbEffortRideDataEntry.Items.IndexOf(effort);
                                cbComfortRideDataEntry.SelectedIndex = cbComfortRideDataEntry.Items.IndexOf(comfort.TrimEnd());                                
                            }
                            else
                            {
                                Logger.LogError("Ride data for an unselected date index was selected.");
                            }

                            checkBoxCloneEntry.Enabled = true;
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
                Logger.LogError("[ERROR]: GetRideData() Exception while trying to retrive ride data. " + ex.Message.ToString());
            }
        }

        private void SubmitData(object sender, EventArgs e)
        {
            lbRideDataEntryError.Text = "";
            lbRideDataEntryError.Hide();

            //RideInformationChange("Add", "Ride_Information_Add");
        }

        private void CloseRideDataEntry(object sender, EventArgs e)
        {
            using (MainForm mainForm = new MainForm(""))
            {
                //MainForm.SetLastBikeSelected(cbBikeDataEntrySelection.SelectedIndex);
                //MainForm.SetLastLogSelectedDataEntry(cbLogYearDataEntry.SelectedIndex);
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

        private void RideInformationChange(Boolean rideDisplayChange)
        {
            lbRideDataEntryError.Hide();
            string changeType = "";
            string procedureName = "";

            //Make sure certain required fields are filled in:
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            if (dtpTimeRideDataEntry.Value == currentDate)
            {
                lbRideDataEntryError.Text = "The Ride Time must be greater than 0:00:00.";
                lbRideDataEntryError.Show();

                return;
            }

            //Make sure the selected date is not a future date:
            DateTime selectedDate = DateTime.Parse(dtpRideDate.Value.ToString());
            if (selectedDate > currentDate)
            {
                MessageBox.Show("The selected Date is past the current date.");

                return;
            }        

            //decimal avgspeed = decimal.Parse(tbRideDataEntryAvgSpeed.Text);
            //if (avgspeed == 0)
            //{
            //    lbRideDataEntryError.Text = "The Average Speed must be greater than 0.";
            //    lbRideDataEntryError.Show();
            //    return;
            //}
            if (cbLogYearDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "A Log year must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbRouteDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "A Route must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbBikeDataEntrySelection.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "A Bike must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbRideTypeDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "A Ride Type must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbLocationDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "A Ride Location must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbEffortRideDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "An Effort option must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (cbComfortRideDataEntry.SelectedIndex < 1)
            {
                lbRideDataEntryError.Text = "An Comfort option must be selected.";
                lbRideDataEntryError.Show();
                return;
            }
            if (tbRideDataEntryDistance.Text.Equals("") || tbRideDataEntryDistance.Text.Equals("0"))
            {
                lbRideDataEntryError.Text = "A Ride Distance value must be entered.";
                lbRideDataEntryError.Show();
                return;
            }
            //if (tbRideDataEntryAvgSpeed.Text.Equals("") || tbRideDataEntryAvgSpeed.Text.Equals("0"))
            //{
            //    lbRideDataEntryError.Text = "An Average Speed value must be entered.";
            //    lbRideDataEntryError.Show();
            //    return;
            //}

            //***********************************************************************
            //Check the entry type:
            //***********************************************************************
            Boolean addUpdateEntry = false;
            Boolean plannedEntry = false;

            if (rideDisplayChange)
            {
                changeType = "DisplayUpdate";
                procedureName = "Ride_Information_Update";
            }
            else if (checkBoxCloneEntry.Checked)
            {
                changeType = "clone";
                procedureName = "Ride_Information_Add";
                DialogResult result = MessageBox.Show("Do you really want to Clone the current ride data entry?", "Clone Ride Entry", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                addUpdateEntry = true;
            }

            try
            {
                string plannedEntryID = "";
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
                    logIndex = MainForm.GetLogYearIndex_ByName(cbLogYearDataEntry.SelectedItem.ToString());
                }

                // Run check to see if a record exists for this date:
                List<object> objectValuesRideDate = new List<object>();
                objectValuesRideDate.Add(dtpRideDate.Value);
                objectValuesRideDate.Add(logIndex);
                int recordCount = 0;

                using (var results = ExecuteSimpleQueryConnection("CheckRideDateCount", objectValuesRideDate)) //and RideDistance IS NOT NULL
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            recordCount = int.Parse(results[0].ToString());
                        }
                    }
                }

                //TODO: Check if a plan entry exists:
                if (addUpdateEntry)
                {
                    List<object> objectValuesPlanEntry = new List<object>
                    {
                        logIndex,
                        dtpRideDate.Value
                    };                  
                    
                    if (recordCount == 0)
                    {
                        using (var results = ExecuteSimpleQueryConnection("GetPlannedValueEntryID", objectValuesPlanEntry))
                        {
                            if (results.HasRows)
                            {
                                while (results.Read())
                                {
                                    plannedEntryID = results[0].ToString();
                                }
                            }
                        }

                        //Record Count is 0 and an ID is found, this means a planner entry was found:
                        if (plannedEntryID.Equals("") || plannedEntryID.Equals("0"))
                        {
                            changeType = "Add";
                            procedureName = "Ride_Information_Add";
                            DialogResult result = MessageBox.Show("Do you really want to Add a new ride entry?", "Add Ride Entry", MessageBoxButtons.YesNo);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            changeType = "Update";
                            plannedEntry = true;
                            procedureName = "Ride_Information_Update";
                            DialogResult result = MessageBox.Show("Do you really want to Update the ride entry?", "Update Ride Entry", MessageBoxButtons.YesNo);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                    } else
                    {
                        //Determine if Add or Update:
                        //ID field = 0 -> Add otherwise Update
                        if (tbRecordID.Text.Equals("0"))
                        {
                            changeType = "Add";
                            procedureName = "Ride_Information_Add";
                            DialogResult result = MessageBox.Show("Do you really want to Add a new ride entry?", "Add Ride Entry", MessageBoxButtons.YesNo);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            changeType = "Update";
                            procedureName = "Ride_Information_Update";
                            DialogResult result = MessageBox.Show("Do you really want to Update the ride entry?", "Update Ride Entry", MessageBoxButtons.YesNo);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }

                string recordID = tbRecordID.Text;

                //MessageBox.Show("Record ID value: " + recordID);

                if (changeType.Equals("Update") && recordCount > 0)
                {
                    if (tbRecordID.Text.Equals("0"))
                    {
                        MessageBox.Show("The current ride can not be updated since it was not loaded from the database.");
                        Logger.Log("The current ride can not be updated since it was not loaded from the database." + recordID, logSetting, 0);

                        return;
                    }
                }

                // Check recordID value:
                if (recordCount > 0 && changeType.Equals("Add"))
                {
                    DialogResult result = MessageBox.Show("Detected that the selected date already has an entry saved to the database. Do you want to continue adding this entry?", "Add Ride Data", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        Logger.Log("Detected that the selected date already has an entry saved to the database. RecordID" + recordID, logSetting, 0);

                        return;
                    }
                }

                double averageSpeed = 0;
                //if (tbRideDataEntryAvgSpeed.Text.Equals(""))
                //{
                    //Value missing, need to calculate it (miles/time:
                    double miles = double.Parse(tbRideDataEntryDistance.Text);
                    int timeHours = DateTime.Parse(dtpTimeRideDataEntry.Value.ToString()).Hour;
                    int timeMin = DateTime.Parse(dtpTimeRideDataEntry.Value.ToString()).Minute;                
                    int timeSec = DateTime.Parse(dtpTimeRideDataEntry.Value.ToString()).Second;
                    string timeString = timeHours.ToString() + ":" + timeMin.ToString() + ":" + timeSec.ToString();
                    double timeInHours = TimeSpan.Parse(timeString).TotalHours;
                    averageSpeed = miles / timeInHours;
                    tbRideDataEntryAvgSpeed.Text = Math.Round(averageSpeed, 1).ToString();
                //}

                //*****************************************************************************
                //*************  VERIFY INPUT DATA IS IN CORRECT FORMAT ***********************
                //*****************************************************************************
                double doubleValue = 0;
                int intValue = 0;
                if (!double.TryParse(tbRideDataEntryDistance.Text, out doubleValue))
                {
                    lbRideDataEntryError.Text = "The Ride Distance value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!tbRideDataEntryAvgSpeed.Text.Equals("- - ") && !double.TryParse(tbRideDataEntryAvgSpeed.Text, out doubleValue))
                {
                    lbRideDataEntryError.Text = "The Average Speed value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!max_speed.Text.Equals("") && !double.TryParse(tbRideDataEntryAvgSpeed.Text, out doubleValue))
                {
                    lbRideDataEntryError.Text = "The Max Speed value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!avg_cadence.Text.Equals("- -") && !avg_cadence.Text.Equals("") && !int.TryParse(avg_cadence.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Average Cadenace value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!tbMaxCadence.Text.Equals("- -") && !tbMaxCadence.Text.Equals("") && !int.TryParse(tbMaxCadence.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Max Cadenace value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!calories.Text.Equals("- -") && !calories.Text.Equals("") && !int.TryParse(calories.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Calories value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!tbRideDataEntryWind.Text.Equals("- -") && !tbRideDataEntryWind.Text.Equals("") && !double.TryParse(tbRideDataEntryWind.Text, out doubleValue))
                {
                    lbRideDataEntryError.Text = "The Wind value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!tbRideEntryTemp.Text.Equals("- -") && !tbRideEntryTemp.Text.Equals("") && !double.TryParse(tbRideEntryTemp.Text, out doubleValue))
                {
                    lbRideDataEntryError.Text = "The Temperture value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!avg_heart_rate.Text.Equals("- -") && !avg_heart_rate.Text.Equals("") && !int.TryParse(avg_heart_rate.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Average Heart Rate value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!max_heart_rate.Text.Equals("- -") && !max_heart_rate.Text.Equals("") && !int.TryParse(max_heart_rate.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Max Heart Rate value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!total_ascent.Text.Equals("- -") && !total_ascent.Text.Equals("") && !int.TryParse(total_ascent.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Total Ascent value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!total_descent.Text.Equals("- -") && !total_descent.Text.Equals("") && !int.TryParse(total_descent.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Total Descent value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!avg_power.Text.Equals("- -") && !avg_power.Text.Equals("") && !int.TryParse(avg_power.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Average Power value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (!max_power.Text.Equals("- -") && !max_power.Text.Equals("") && !int.TryParse(max_power.Text, out intValue))
                {
                    lbRideDataEntryError.Text = "The Max Power value is incorrect.";
                    lbRideDataEntryError.Show();
                    return;
                }


                //*****************************************************************************
                //*****************************************************************************

                List<object> objectValues = new List<object>();
                objectValues.Add(dtpTimeRideDataEntry.Value);                           //Moving Time:
                objectValues.Add(tbRideDataEntryDistance.Text);                         //Ride Distance:
                if (tbRideDataEntryAvgSpeed.Text.Equals(""))
                {
                    objectValues.Add(averageSpeed);                         //Average Speed:
                } else
                {
                    objectValues.Add(tbRideDataEntryAvgSpeed.Text);                         //Average Speed:
                }
                
                objectValues.Add(cbBikeDataEntrySelection.SelectedItem.ToString());     //Bike:
                MainForm.SetLastBikeSelected(cbBikeDataEntrySelection.SelectedIndex);
                objectValues.Add(cbRideTypeDataEntry.SelectedItem.ToString());          //Ride Type:
                float windspeed;
                Boolean noTemp = false;
                //Boolean noWind = false;
                if (tbRideDataEntryWind.Text.Equals("") || tbRideDataEntryWind.Text.Equals("- -"))
                {
                    objectValues.Add(null);                                            //Wind:
                    //noWind = true;
                }
                else
                {
                    windspeed = float.Parse(tbRideDataEntryWind.Text);
                    objectValues.Add(windspeed);                                            //Wind:
                }
                
                float temp = 0;
                if (tbRideEntryTemp.Text.Equals("") || tbRideEntryTemp.Text.Equals("- -"))
                {
                    objectValues.Add(null);                                  //Temp:
                    noTemp = true;
                }
                else
                {
                    temp = float.Parse(tbRideEntryTemp.Text);
                    objectValues.Add(Math.Round(temp, 1));                                  //Temp:
                }
                
                objectValues.Add(dtpRideDate.Value);                                    //Date:

                if (avg_cadence.Text.Equals("") || avg_cadence.Text.Equals("- -"))       //Average Cadence:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_cadence.Text));
                }

                if (tbMaxCadence.Text.Equals("") || tbMaxCadence.Text.Equals("- -"))     //Max Cadence:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(tbMaxCadence.Text));
                }

                if (avg_heart_rate.Text.Equals("") || avg_heart_rate.Text.Equals("- -"))     //Average Heart Rate:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_heart_rate.Text));
                }

                if (max_heart_rate.Text.Equals("") || max_heart_rate.Text.Equals("- -")) //Max Heart Rate:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(max_heart_rate.Text));
                }

                if (calories.Text.Equals("") || calories.Text.Equals("- -"))             //Calories:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(calories.Text));
                }

                if (total_ascent.Text.Equals("") || total_ascent.Text.Equals("- -"))     //Total Ascent:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(total_ascent.Text));
                }

                if (total_descent.Text.Equals("") || total_descent.Text.Equals("- -"))   //Total Descent:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(total_descent.Text));
                }

                if (max_speed.Text.Equals("") || max_speed.Text.Equals("- -"))           //Max Speed:
                {
                    objectValues.Add(null);
                }
                else
                {
                    double maxSpeed1 = double.Parse(max_speed.Text);
                    objectValues.Add(Math.Round(maxSpeed1,1));
                }

                if (avg_power.Text.Equals("") || avg_power.Text.Equals("- -"))           //Average Power:
                {
                    objectValues.Add(null);
                }
                else
                {
                    objectValues.Add(float.Parse(avg_power.Text));
                }

                if (max_power.Text.Equals("") || max_power.Text.Equals("- -"))           //Max Power:
                {
                    objectValues.Add(null);
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
                double windChill = 0;
                if (max_speed.Text == "" || noTemp)
                {
                    objectValues.Add(null);                                                 //Winchill:
                } else
                {
                    double maxSpeed = double.Parse(max_speed.Text);
                    if (maxSpeed > 0 && temp > 0)                                          //Winchill:
                    {
                        windChill = 35.74 + 0.6215 * temp + (0.4275 * temp - 35.75) * Math.Pow(maxSpeed, 0.16);
                        windChill = Math.Round(windChill, 1);
                        objectValues.Add(windChill.ToString());
                    } else
                    {
                        objectValues.Add(temp);                                                 //Winchill:
                    }
                }

                objectValues.Add(cbEffortRideDataEntry.SelectedItem.ToString());         //Effort:
                objectValues.Add(cbComfortRideDataEntry.SelectedItem.ToString());         //Comfort:
                if (tbCustom1.Text.Equals("") || tbCustom1.Text.Equals("- -")){
                    objectValues.Add(null);                                                //Custom1
                } else
                {
                    objectValues.Add(tbCustom1.Text);                                                //Custom1
                }

                if (tbCustom2.Text.Equals("") || tbCustom2.Text.Equals("- -"))
                {
                    objectValues.Add(null);                                                //Custom2
                }
                else
                {
                    objectValues.Add(tbCustom2.Text);                                                //Custom2
                }


                if (plannedEntry)
                {
                    objectValues.Add(plannedEntryID);                                         //Record ID:
                }
                else if (changeType.Equals("Update") || changeType.Equals("DisplayUpdate"))
                {
                    objectValues.Add(GetEntryID().ToString());                                         //Record ID:
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
                        if (changeType.Equals("Update") || changeType.Equals("DisplayUpdate"))
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
                Logger.LogError("[ERROR]: Exception while trying to update ride information. " + ex.Message.ToString());
                MessageBox.Show("[ERROR] There was a problem adding or updating the ride.");
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
            //cbBikeDataEntrySelection.SelectedIndex = MainForm.GetLastBikeSelected();
            // cbLogYearDataEntry.SelectedIndex = cbRouteDataEntry.FindStringExact("");
        }

        private void RideDataEntryFormClosed(object sender, FormClosedEventArgs e)
        {
            //clearDataEntryFields();
        }

        private void ImportData(object sender, EventArgs e)
        {
            //Clear some values on form to prepdare for import:
            ClearDataEntryFieldsImport();

            string[] headingList = new string[16];
            string[] splitList = new string[16];
            Dictionary<string, string> garminDataDictionary = new Dictionary<string, string>();

            lbRideDataEntryError.Text = "";
            lbRideDataEntryError.Hide();
            double avgSpeed = 0;

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
                                        dataValue = dataValue.Replace(",", "");
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

                tbRideDataEntryDistance.Text = garminDataDictionary["\"Distance\""];

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
                    tbRideDataEntryAvgSpeed.Text = value;
                }
                else
                {
                    tbRideDataEntryAvgSpeed.Text = garminDataDictionary["\"Avg Speed\""];
                }

                avgSpeed = double.Parse(tbRideDataEntryAvgSpeed.Text);

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
                    tbRideEntryTemp.Text = decimal.Round(System.Convert.ToDecimal(value), 2, MidpointRounding.AwayFromZero).ToString();
                }

                if (garminDataDictionary.ContainsKey("\"Calories\""))
                {
                    calories.Text = garminDataDictionary["\"Calories\""];
                } else
                {
                    calories.Text = "";
                }
                
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to import ride data. " + ex.Message.ToString());
                MessageBox.Show("[ERROR] Exception occurred during data import. " + ex.Message.ToString());
            }

            double windChill;
            double maxSpeed;

            if (max_speed.Text.Equals(""))
            {
                maxSpeed = avgSpeed;
            } else
            {
                maxSpeed = double.Parse(max_speed.Text);
            }

            if (tbRideEntryTemp.Text.Equals(""))
            {
                tbRideEntryWindChill.Text = "";
            } else
            {
                double temperature = double.Parse(tbRideEntryTemp.Text.ToString());

                if (maxSpeed > 0 && temperature > 0)
                {
                    windChill = 35.74 + 0.6215 * temperature + (0.4275 * temperature - 35.75) * Math.Pow(maxSpeed, 0.16);
                }
                else
                {
                    windChill = temperature;
                }

                windChill = Math.Round(windChill, 1);
                tbRideEntryWindChill.Text = windChill.ToString();
            }                    
        }

        // Used to clear the form on date changes:
        private void ClearDataEntryFields()
        {
            //Reset and clear values:
            dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);//Moving Time:
            tbRideDataEntryDistance.Text = "";                                                                         //Ride Distance:
            tbRideDataEntryAvgSpeed.Text = "";                                                                                   //Average Speed:
            cbBikeDataEntrySelection.SelectedIndex = 0;                     //Bike:
            cbRideTypeDataEntry.SelectedIndex = 0;                              //Ride Type:
            tbRideDataEntryWind.Text = "";                                                                                   //Wind:
            tbRideEntryTemp.Text = "";                                                                                   //Temp:
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
            cbRouteDataEntry.SelectedIndex = 0; ;                                    //Route:
            tbComments.Text = "";                                                                                       //Comments:
            //cbLogYearDataEntry.SelectedIndex = cbLogYearDataEntry.FindStringExact("");                                //LogYear index:
            cbLocationDataEntry.SelectedIndex = 0;
            cbEffortRideDataEntry.SelectedIndex = 0;
            //tbWeekNumber.Text = "0";
            tbRecordID.Text = "0";
            cbComfortRideDataEntry.SelectedIndex = 0;
            tbCustom1.Text = "";
            tbCustom2.Text = "";
            tbMaxCadence.Text = "";
            tbRideEntryWindChill.Text = "";
        }

        private void ClearDataEntryFieldsImport()
        {
            //Reset and clear values:
            dtpTimeRideDataEntry.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);//Moving Time:
            tbRideDataEntryDistance.Text = "";                                                                          //Ride Distance:
            tbRideDataEntryAvgSpeed.Text = "";                                                                          //Average Speed:
            tbRideDataEntryWind.Text = "";                                                                              //Wind:
            tbRideEntryTemp.Text = "";                                                                                  //Temp:
            avg_cadence.Text = "";                                                                                      //Average Cadence:
            avg_heart_rate.Text = "";                                                                                   //Average Heart Rate:
            max_heart_rate.Text = "";                                                                                   //Max Heart Rate:
            calories.Text = "";                                                                                         //Calories:
            total_ascent.Text = "";                                                                                     //Total Ascent:
            total_descent.Text = "";                                                                                    //Total Descent:
            max_speed.Text = "";                                                                                        //Max Speed:
            avg_power.Text = "";                                                                                        //Average Power:
            max_power.Text = "";                                                                                        //Max Power:
            tbComments.Text = "";                                                                                       //Comments:          
            tbRecordID.Text = "0";
            tbCustom1.Text = "";
            tbCustom2.Text = "";
            tbMaxCadence.Text = "";
            tbRideEntryWindChill.Text = "";
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
            //MainForm mainForm = new MainForm("")
            //using (MainForm mainForm = new MainForm(""))
            //{
                MainForm.SetLastLogSelected(cbLogYearDataEntry.SelectedIndex);
                if (cbLogYearDataEntry.SelectedIndex < 1)
                {
                    lbRideDataEntryError.Show();
                    lbRideDataEntryError.Text = "No Log Year selected.";
                    dtpRideDate.Enabled = false;
                }
                else
                {
                    dtpRideDate.Enabled = true;
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
            //}
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

            checkBoxCloneEntry.Enabled = false;
            checkBoxCloneEntry.Checked = false;
        }

        private void BtUpdateRideDateEntry_Click(object sender, EventArgs e)
        {
            //RideInformationChange("Update", "Ride_Information_Update");
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
            if (cbLogYearDataEntry.SelectedIndex < 1)
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

            //The Entry ID will need to be used on the update:
            SetEntryID(int.Parse(tbRecordID.Text));

            MessageBox.Show("Ride data retrieved successfully.");
        }

        //public void SetSqlParameters(Dictionary<string, string> sqlParametersDict)
        //{
        //    sqlParameters = sqlParametersDict;
        //}

        //public Dictionary<string, string> GetSqlParameters() {
        //    return sqlParameters; 
        //}

        //public void UpdateRideInformation()
        //{
        //    //Need list of fields to update:
        // //   @MovingTime time,
        // //   @RideDistance float,
        // //   @AvgSpeed float,
        // //   @Bike nvarchar(25),
	       // //@RideType nvarchar(25),
	       // //@Wind float,
        // //   @Temperature float,
        // //   @Date date,
	       // //@AvgCadence float,
        // //   @MaxCadence float,
        // //   @AvgHeartRate float,
        // //   @MaxHeartRate float,
        // //   @Calories float,
        // //   @TotalAscent float,
        // //   @TotalDescent float,
        // //   @MaxSpeed float,
        // //   @AveragePower float,
        // //   @MaxPower float,
        // //   @Route nvarchar(50),
	       // //@Comments nvarchar(200),
	       // //@LogYearID bigint,
        //    //@WeekNumber bigint,
	       // //@Location nvarchar(25),
	       // //@Windchill float,
        //    //@Effort nvarchar(25),
	       // //@Comfort nvarchar(25),
	       // //@Custom1 nvarchar(25),
	       // //@Custom2 nvarchar(25),

        //    SqlDataReader reader = null;
        //    int id = GetID();

        //    string setCommand = "MovingTime=@movingTime,RideDistance=@rideDistance,AvgSpeed=@avgSpeed,Bike=@bike,RideType=@rideType,Wind=@wind,Temperature=@temperature,Date=@date,AvgCadence=@avgCadence,MaxCadence=maxCadence,AvgHeartRate=@avgHeartRate,MaxHeartRate=@maxHeartRate,Calories=@calories,TotalAscent=@totalAscent,TotalDescent=@totalDescent,MaxSpeed=@maxSpeed,AveragePower=@averagePower,MaxPower=@maxPower,Route=@route,Comments=@comments,LogYearID=@logYearID,WeekNumber=@weekNumber,Location=@location,Effort=@effort,Comfort=@comfort,Custom1=@custom1,Custom2=@custom2";
        //    Dictionary<string, string> sqlParametersDictionary = GetSqlParameters();
        //    string keyValue;

        //    try
        //    {
        //        sqlConnection.Open();

        //        // 1. declare command object with parameter
        //        using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET " + setCommand + " WHERE Id=@id", sqlConnection))
        //        {
        //            // 2. define parameters used in command object
        //            SqlParameter param = new SqlParameter();
        //            // 3. add new parameter to command object
        //            for (int i = 0; i < sqlParametersDictionary.Count; i++)
        //            {
        //                //TODO: convert certain fields to correct type:
        //                keyValue = sqlParametersDictionary.ElementAt(i).Key;
        //                //if (keyValue.Equals("@RideDistance") || keyValue.Equals("@AvgSpeed") || keyValue.Equals("@Wind") || keyValue.Equals("@Temperature") || keyValue.Equals("@AvgCadence") || keyValue.Equals("@MaxCadence") || keyValue.Equals("@AvgHeartRate") || keyValue.Equals("@MaxHeartRate") || keyValue.Equals("@TotalAscent") || keyValue.Equals("@TotalDescent") || keyValue.Equals("@MaxSpeed") || keyValue.Equals("@AveragePower") || keyValue.Equals("@MaxPower"))
        //                //{
        //                //    cmd.Parameters.AddWithValue(keyValue, sqlParametersDictionary.ElementAt(i).Value);
        //                //} else if ()
        //                //{
        //                //    cmd.Parameters.AddWithValue(float.Parse(keyValue), sqlParametersDictionary.ElementAt(i).Value);
        //                //} else
        //                //{
        //                    cmd.Parameters.AddWithValue(keyValue, sqlParametersDictionary.ElementAt(i).Value);
        //                //}
                        
        //            }
        //            //cmd.Parameters.AddWithValue("@calories", "12345");

        //            reader = cmd.ExecuteReader();
        //        }

        //        // write each record
        //        while (reader.Read())
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("[ERROR]: Exception while trying update ride data entry." + ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        reader?.Close();
        //        sqlConnection?.Close();
        //    }
        //}

        private void btUpdateEntry_Click(object sender, EventArgs e)
        {
            //UpdateRideDisplayInformation();
            DeleteRecordByID(GetEntryID());
            UpdateRideDisplayInformationAdd();
        }

        public void RideDisplayDataQuery(string id, string date, int logYearID)
        {
            SetDate(DateTime.Parse(date));
            SetLogYearID(logYearID);

            //Hide items not used from this location:
            groupBoxRetrieveDate.Visible = false;
            btImportDataEntry.Visible = false;
            btDeleteRideDataEntry.Visible = false;
            btClearDataEntry.Visible = false;
            btRideDisplayUpdate.Visible = true;
            btLogEntrySave.Visible = false;

            List<object> objectValues = new List<object>
            {
                id
            };

            SetEntryID(int.Parse(id));
            tbRecordID.Text = id;

            string movingTime;
            string rideDistance;
            string avgSpeed;
            string bike;
            string rideType;
            string wind;
            string temperature;
            string avgCadence;
            string maxCadence;
            string avgHeartRate;
            string maxHeartRate;
            string calories;
            string totalAscent;
            string totalDescent;
            string maxSpeed;
            string averagePower;
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
            string windChill;
            int routeIndex = 0;
            int bikeIndex = 0;
            int rideTypeIndex = 0;
            int locationIndex = 0;
            int effortIndex = 0;
            int comfortIndex = 0;

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("GetRideDataByID", objectValues))
                {
                    //Logger.Log("Results: " + results.FieldCount, 0, logLevel);
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            movingTime = results[0].ToString();
                            rideDistance = results[1].ToString();
                            avgSpeed = results[2].ToString();
                            bike = results[3].ToString();
                            rideType = results[4].ToString();
                            wind = results[5].ToString();
                            temperature = results[6].ToString();
                            avgCadence = results[7].ToString();
                            maxCadence = results[8].ToString();
                            avgHeartRate = results[9].ToString();
                            maxHeartRate = results[10].ToString();
                            calories = results[11].ToString();
                            totalAscent = results[12].ToString();
                            totalDescent = results[13].ToString();
                            if (string.IsNullOrEmpty(results[14].ToString()) || results[14].ToString().Equals("- -"))
                            {
                                maxSpeed = "- -";
                            }
                            else
                            {
                                double maxSpeedDouble = double.Parse(results[14].ToString());
                                maxSpeed = (Math.Round(maxSpeedDouble, 1)).ToString();
                            }
                            
                            averagePower = results[15].ToString();
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
                            windChill = results[26].ToString();

                            MainForm mainform = new MainForm();
                            List<string> routeList = MainForm.ReadDataNames("Table_Routes", "Name");

                            for (int i = 0; i < routeList.Count; i++)
                            {
                                if (routeList[i].Equals(route))
                                {
                                    routeIndex = i;
                                    break;
                                }
                            }

                            List<string> bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");

                            for (int i = 0; i < bikeList.Count; i++)
                            {
                                if (bikeList[i].Equals(bike))
                                {
                                    bikeIndex = i;
                                    break;
                                }
                            }

                            if (rideType.Equals("Base"))
                            {
                                rideTypeIndex = 1;
                            }
                            else if (rideType.Equals("Distance"))
                            {
                                rideTypeIndex = 2;
                            }
                            else if (rideType.Equals("Race"))
                            {
                                rideTypeIndex = 3;
                            }
                            else if (rideType.Equals("Recovery"))
                            {
                                rideTypeIndex = 4;
                            }
                            else if (rideType.Equals("Speed"))
                            {
                                rideTypeIndex = 5;
                            }
                            else if (rideType.Equals("Tour"))
                            {
                                rideTypeIndex = 6;
                            }

                            if (location.Equals("Road"))
                            {
                                locationIndex = 1;
                            }
                            else if (location.Equals("Rollers"))
                            {
                                locationIndex = 2;
                            }
                            else if (location.Equals("Trail"))
                            {
                                locationIndex = 3;
                            }
                            else if (location.Equals("Trainer"))
                            {
                                locationIndex = 4;
                            }

                            if (effort.Contains("Easy"))
                            {
                                effortIndex = 1;
                            }
                            else if (effort.Equals("Moderate"))
                            {
                                effortIndex = 2;
                            }
                            else if (effort.Equals("Hard"))
                            {
                                effortIndex = 3;
                            }
                            else if (effort.Equals("Race"))
                            {
                                effortIndex = 4;
                            }

                            if (comfort.Contains("Weak"))
                            {
                                comfortIndex = 1;
                            }
                            else if (comfort.Contains("Average"))
                            {
                                comfortIndex = 2;
                            }
                            else if (comfort.Contains("Strong"))
                            {
                                comfortIndex = 3;
                            }

                            //Populate fields in the entry form:
                            SetcbLogYearDataEntryIndex(logYearID);
                            SetDate(DateTime.Parse(date));
                            SettbWeekCountRDE(weekNumber);
                            SetRouteIndex(routeIndex+1); //+1 to account for '--Select Value--'
                            SetRoute(route);
                            //Thread.Sleep(2000); // 1000 milliseconds i.e 1sec
                            SetTime(DateTime.Parse(movingTime));
                            SetDistance(decimal.Parse(rideDistance));
                            SetAvgSpeed(double.Parse(avgSpeed));
                            SetWind(wind);
                            SetTemp(temperature);
                            SetType(rideTypeIndex);
                            SetLocation(locationIndex);
                            SetEffort(effortIndex);
                            SetComfort(comfortIndex);
                            SetCalories(calories);
                            SetAvgCadence(avgCadence);
                            SetMaxCadence(maxCadence);
                            SetAvgHeartRate(avgHeartRate);
                            SetMaxHeartRate(maxHeartRate);
                            SetTotalAscent(totalAscent);
                            SetTotalDescent(totalDescent);
                            SetMaxSpeed(maxSpeed);
                            SetMaxPower(maxPower);
                            SetAvgPower(averagePower);
                            SetCustom1(custom1);
                            SetCustom2(custom2);
                            SetBike(bike);
                            SetBikeIndex(bikeIndex+1);//+1 to account for '--Select Value--'
                            SetComments(comments);
                            SetWindChill(windChill);

         
                            //Dictionary<string, string> sqlParameters = new Dictionary<string, string>();
                            //sqlParameters.Add("@Id", id);
                            //sqlParameters.Add("@MovingTime", movingTime);
                            //sqlParameters.Add("@RideDistance", rideDistance);
                            //sqlParameters.Add("@AvgSpeed", avgSpeed);
                            //sqlParameters.Add("@Bike", bike);
                            //sqlParameters.Add("@RideType", rideType);
                            //sqlParameters.Add("@Wind", wind);
                            //sqlParameters.Add("@Temperature", temperature);
                            //sqlParameters.Add("@Date", date);
                            //sqlParameters.Add("@AvgCadence", avgCadence);
                            //sqlParameters.Add("@MaxCadence", maxCadence);
                            //sqlParameters.Add("@AvgHeartRate", avgHeartRate);
                            //sqlParameters.Add("@MaxHeartRate", maxHeartRate);
                            //sqlParameters.Add("@Calories", calories);
                            //sqlParameters.Add("@TotalAscent", totalAscent);
                            //sqlParameters.Add("@TotalDescent", totalDescent);
                            //sqlParameters.Add("@MaxSpeed", maxSpeed);
                            //sqlParameters.Add("@AveragePower", averagePower);
                            //sqlParameters.Add("@MaxPower", maxPower);
                            //sqlParameters.Add("@Route", route);
                            //sqlParameters.Add("@Comments", comments);
                            //sqlParameters.Add("@LogYearID", logYearID.ToString());
                            //sqlParameters.Add("@WeekNumber", weekNumber);
                            //sqlParameters.Add("@Location", location);
                            ////sqlParameters.Add("@Windchill", windchill);
                            //sqlParameters.Add("@Effort", effort);
                            //sqlParameters.Add("@Comfort", comfort);
                            //sqlParameters.Add("@Custom1", custom1);
                            //sqlParameters.Add("@Custom2", custom2);

                            //SetSqlParameters(sqlParameters);
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: RideDisplayDataQuery: Exception while trying to retrive ride data. " + ex.Message.ToString());
            }

            //this.cbBikeDataEntrySelection.SelectedIndex = cbBikeDataEntrySelection.Items.IndexOf(GetBike());
            //cbBikeDataEntrySelection.SelectedText = GetBike();
        }

        //private void btCancelDisplayUpdate_Click(object sender, EventArgs e)
        //{
        //    formClosing = 1;
        //    this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        //}

        //static double CalculateWCI(double temperature, double windSpeed)
        //{
        //    // Calculating Wind Chill Index (Twc)
        //    double wci = 13.12 + 0.6215 * temperature - 11.37 * Math.Pow(windSpeed, 0.16) + 0.3965 * temperature * Math.Pow(windSpeed, 0.16);

        //    return wci;
        //}

        public void DeleteRecordByID(int recordID)
        {
            //Get ride recordID:
            string rideRecordID = tbRecordID.Text;

            DialogResult result = MessageBox.Show("Do you really want to delete the Ride and all its data?", "Delete Ride From Database", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
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
                    // close connection
                    sqlConnection?.Close();
                }
            }
        }

        public void UpdateRideDisplayInformationAdd()
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
                decimal avgspeed = decimal.Parse(tbRideDataEntryAvgSpeed.Text);
                if (avgspeed == 0)
                {
                    lbRideDataEntryError.Text = "The Average Speed must be greater than 0.";
                    lbRideDataEntryError.Show();
                    return;
                }
                if (cbLogYearDataEntry.SelectedIndex < 1)
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

                DialogResult result = MessageBox.Show("Adding the ride in the log. Do you want to continue?", "Update Data", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }

                List<object> objectValues = new List<object>();
                objectValues.Add(dtpTimeRideDataEntry.Value);                           //Moving Time:
                objectValues.Add(tbRideDataEntryDistance.Text);                       //Ride Distance:
                objectValues.Add(tbRideDataEntryAvgSpeed.Text);                                 //Average Speed:
                objectValues.Add(cbBikeDataEntrySelection.SelectedItem.ToString());     //Bike:
                objectValues.Add(cbRideTypeDataEntry.SelectedItem.ToString());          //Ride Type:
                float windspeed = float.Parse(tbRideDataEntryWind.Text);                          //----
                objectValues.Add(windspeed);                                            //Wind:
                float temp = float.Parse(tbRideEntryTemp.Text);                               //--
                objectValues.Add(Math.Round(temp, 1));                                   //Temp:
                objectValues.Add(dtpRideDate.Value);                                                 //Date:

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
                    objectValues.Add(float.Parse(tbMaxCadence.Text));
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
                objectValues.Add(logYearID);                                             //LogYear index:

                //DateTime date = new DateTime();
                DayOfWeek firstDay = DayOfWeek.Monday;

                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                int weekValue = cal.GetWeekOfYear(dtpRideDate.Value, dfi.CalendarWeekRule, firstDay);

                objectValues.Add(Int32.Parse(tbWeekCountRDE.Text));

                objectValues.Add(cbLocationDataEntry.SelectedItem.ToString());          //Location:
                double winchill = 0;

                if (windspeed > 3 && temp < 50)                                          //Winchill:
                {
                    winchill = 35.74 + (0.6215) * (temp) - (35.75) * (Math.Pow(windspeed, 0.16)) + (0.4275) * (Math.Pow(windspeed, 0.16));
                    objectValues.Add(winchill.ToString());
                }
                else
                {
                    objectValues.Add(temp);
                }

                objectValues.Add(cbEffortRideDataEntry.SelectedItem.ToString());         //Effort:
                objectValues.Add(cbComfortRideDataEntry.SelectedItem.ToString());         //Comfort:
                objectValues.Add(tbCustom1.Text);                                                //Custom1
                objectValues.Add(tbCustom2.Text);                                                //Custom2

                //objectValues.Add(id);                                         //Record ID:

                using (var results = ExecuteSimpleQueryConnection("Ride_Information_Add", objectValues))
                {
                    if (results == null)
                    {

                        MessageBox.Show("[ERROR] There was a problem adding the ride.");

                    }
                    else
                    {

                        MessageBox.Show("The ride entry has been added successfully.");

                    }

                    return;
                }

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to add the ride display information." + ex.Message.ToString());
            }
        }

        private void btRideDisplayUpdate_Click(object sender, EventArgs e)
        {
            RideInformationChange(true);
        }

        private void btLogEntrySave_Click(object sender, EventArgs e)
        {
            RideInformationChange(false);
            MainForm.SetLastLogSelectedDataEntry(cbLogYearDataEntry.SelectedIndex);
            ConfigurationFile.WriteConfigFile();
        }
    }
}
