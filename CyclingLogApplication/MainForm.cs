using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
//using System.Threading;

    //TODO: Prevent multipe runs of app

namespace CyclingLogApplication
{
    public partial class MainForm : Form
    {
        RideDataEntry rideDataEntryForm;
        RideDataDisplay rideDataDisplayForm;
        //private Thread thread;
        private static string logVersion = "0.0.1";
        private static int logLevel = 0;
        private static string cbStatistic1 = "-1";
        private static string cbStatistic2 = "-1";
        private static string cbStatistic3 = "-1";
        private static string cbStatistic4 = "-1";
        private static string cbStatistic5 = "-1";
        private static string LastLogSelected = "-1";
        private static string LastBikeSelected = "-1";

        public MainForm()
        {
            InitializeComponent();
            //rideDataEntryForm = new RideDataEntry(this);
            int logSetting = getLogLevel();

            Logger.Log("**********************************************", 1, logSetting);
            Logger.Log("Staring Log Application", 1, 0);

            Logger.Log("**********************************************", 1, logSetting);

            tbWeekCount.Text = GetCurrentWeekCount().ToString();
            tbDayCount.Text = GetCurrentDayCount().ToString();
            tbTimeChange.Text = getDaysToNextTimeChange().ToString();       

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigurationFile configfile = new ConfigurationFile();
            configfile.readConfigFile();
            int logSetting = getLogLevel();

            //Get all values and load the comboboxes:
            List<string> logYearList = new List<string>();
            List<string> routeList = new List<string>();
            List<string> bikeList = new List<string>();

            logYearList = readDataNames("Table_Log_year", "Name");
            routeList = readDataNames("Table_Routes", "Name");
            bikeList = readDataNames("Table_Bikes", "Name");

            rideDataEntryForm = new RideDataEntry(this);
            rideDataDisplayForm = new RideDataDisplay();

            //Load LogYear values:
            foreach (string val in logYearList)
            {
                cbLogYearConfig.Items.Add(val);
                rideDataEntryForm.cbLogYearDataEntry.Items.Add(val);
                cbLogYear1.Items.Add(val);
                cbLogYear2.Items.Add(val);
                cbLogYear3.Items.Add(val);
                cbLogYear4.Items.Add(val);
                cbLogYear5.Items.Add(val);
                rideDataDisplayForm.cbLogYearFilter.Items.Add(val);
            }
       
            //Load Route values:
            foreach (var val in routeList)
            {
                cbRouteConfig.Items.Add(val);
                rideDataEntryForm.cbRouteDataEntry.Items.Add(val);
            }

            //Load Bike values:
            foreach (var val in bikeList)
            {
                cbBikeConfig.Items.Add(val);
                rideDataEntryForm.cbBikeDataEntry.Items.Add(val);
            }

            if (logYearList.Count == 0)
            {
                MessageBox.Show("No Yearly Logs have been created and no Bikes have been added. Please add an entry for each.");
                tabControl1.SelectedIndex = 2;
                return;
            }

            if (routeList.Count == 0)
            {
                MessageBox.Show("No Routes have been created yet.");
                tabControl1.SelectedIndex = 2;
                return;
            }

            if (bikeList.Count == 0)
            {
                MessageBox.Show("No Bikes have been added yet. Please add an entry for a Bike.");
                tabControl1.SelectedIndex = 2;
                return;
            }

            //Load Statistic combo index values:
            cbLogYear1.SelectedIndex = Convert.ToInt32(getcbStatistic1());
            cbLogYear2.SelectedIndex = Convert.ToInt32(getcbStatistic2());
            cbLogYear3.SelectedIndex = Convert.ToInt32(getcbStatistic3());
            cbLogYear4.SelectedIndex = Convert.ToInt32(getcbStatistic4());
            cbLogYear5.SelectedIndex = Convert.ToInt32(getcbStatistic5());
        }

        private void closeForm(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int logSetting = getLogLevel();
                ConfigurationFile configurationFile = new ConfigurationFile();
                configurationFile.writeConfigFile();

                Logger.Log("**********************************************", 1, logSetting);
                Logger.Log("Ending Log Application", 1, 0);
                Logger.Log("**********************************************", 1, logSetting);

                Application.Exit();
            }
        }

        public string getLogVersion()
        {
            return logVersion;
        }

        public int getLogLevel()
        {
            return logLevel;
        }

        public void setLogLevel(int logLevelFromConfig)
        {
            logLevel = logLevelFromConfig;
        }
        public string getcbStatistic1()
        {
            return cbStatistic1;
        }
        public string getcbStatistic2()
        {
            return cbStatistic2;
        }
        public string getcbStatistic3()
        {
            return cbStatistic3;
        }
        public string getcbStatistic4()
        {
            return cbStatistic4;
        }
        public string getcbStatistic5()
        {
            return cbStatistic5;
        }

        public void setcbStatistic1(string setcbStatistic1Config)
        {
            cbStatistic1 = setcbStatistic1Config;
        }
        public void setcbStatistic2(string setcbStatistic2Config)
        {
            cbStatistic2 = setcbStatistic2Config;
        }
        public void setcbStatistic3(string setcbStatistic3Config)
        {
            cbStatistic3 = setcbStatistic3Config;
        }
        public void setcbStatistic4(string setcbStatistic4Config)
        {
            cbStatistic4 = setcbStatistic4Config;
        }
        public void setcbStatistic5(string setcbStatistic5Config)
        {
            cbStatistic5 = setcbStatistic5Config;
        }

        public string getLastBikeSelected()
        {
            return LastBikeSelected;
        }

        public string getLastLogSelected()
        {
            return LastLogSelected;
        }

        public void setLastBikeSelected(string bikeIndex)
        {
            LastBikeSelected = bikeIndex;
        }

        public void setLastLogSelected(string logIndex)
        {
            LastLogSelected = logIndex;
        }

        public List<string> GetLogYears()
        {
            List<string> logYearsList = new List<string>();

            for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
            {
                logYearsList.Add(cbLogYearConfig.GetItemText(cbLogYearConfig.Items[i]));
            }

            return logYearsList;
        }

        public List<string> GetRoutes()
        {
            List<string> routeList = new List<string>();

            for (int i = 0; i < cbRouteConfig.Items.Count; i++)
            {
                routeList.Add(cbRouteConfig.GetItemText(cbRouteConfig.Items[i]));
            }

            return routeList;
        }

        //Diable x close option:
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //TESTING ONLY
        private void writeData(object sender, EventArgs e)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;

            string input1 = "testvalue1";
            string input2 = "testvalue2";

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // declare command object with parameter
                String query = "INSERT INTO dbo.Table1 (field1,field2) VALUES(@field1,@field2)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@field1", input1);
                cmd.Parameters.AddWithValue("@field2", input2);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                // close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private List<string> readDataNames(string tableName, string columnName)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;
            List<string> nameList = new List<string>();

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select " + columnName + " from " + tableName, conn);

                // 2. define parameters used in command object
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@Id";
                //param.Value = inputValue;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();
                
                // write each record
                while (reader.Read())
                {
                    string returnValue = reader[0].ToString();
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    nameList.Add(returnValue);
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

            return nameList;
        }

        //TESTING only
        private void readData(object sender, EventArgs e)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            int inputValue = 3;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select SUM(RideDistance) from Table_Ride_Information", conn);

                // 2. define parameters used in command object
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@Id";
                //param.Value = inputValue;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
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
        }

        private void btAddLogYearConfig(object sender, EventArgs e)
        {
            string bikeString = tbLogYearConfig.Text;
            //Check to see if the string has already been entered to eliminate duplicates:
            for (int index = 0; index < cbLogYearConfig.Items.Count; index++)
            {
                if (cbLogYearConfig.SelectedItem.ToString().Equals(bikeString))
                {
                    MessageBox.Show("Duplicate name entered. Enter a unique name for the log.");
                    return;
                }
            }

            //Add new entry to the LogYear Table:
            List<object> objectValues = new List<object>();
            objectValues.Add(bikeString);
            runStoredProcedure(objectValues, "Log_Year_Add");

            cbLogYearConfig.Items.Add(bikeString);
            cbLogYearConfig.SelectedIndex = cbLogYearConfig.Items.Count-1;
            rideDataEntryForm.AddLogYearDataEntry(bikeString);
            rideDataDisplayForm.AddLogYearFilter(bikeString);

            //Update combo's on stat tab:
            cbLogYear1.Items.Add(bikeString);
            cbLogYear2.Items.Add(bikeString);
            cbLogYear3.Items.Add(bikeString);
            cbLogYear4.Items.Add(bikeString);
            cbLogYear5.Items.Add(bikeString);
        }

        private void removeLogYearConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Log and all its data?", "Delete Log", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int selectedIndex = cbLogYearConfig.SelectedIndex;
                cbLogYearConfig.Items.Remove(cbLogYearConfig.SelectedItem);
                rideDataEntryForm.RemoveLogYearDataEntry(cbLogYearConfig.SelectedText);
                rideDataDisplayForm.RemoveLogYearFilter(cbLogYearConfig.SelectedText);
                cbLogYear1.Items.Remove(tbLogYearConfig.Text);
                cbLogYear2.Items.Remove(tbLogYearConfig.Text);
                cbLogYear3.Items.Remove(tbLogYearConfig.Text);
                cbLogYear4.Items.Remove(tbLogYearConfig.Text);
                cbLogYear5.Items.Remove(tbLogYearConfig.Text);

                //Need to remove all data for this log from the database:
                removeLogYearData(getLogYearIndex(cbLogYearConfig.SelectedText));
            }
        }

        public int getLogYearIndex(string logYearName)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            int returnValue = -1;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("SELECT LogYearID FROM Table_Log_Year WHERE @logYearName=[Name]", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@logYearName";
                param.Value = logYearName;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = int.Parse(temp);
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

            return returnValue;
        }

        private void runStoredProcedure(List<object> objectValues, string procedureName)
        {
            using (var results = ExecuteSimpleQueryConnection(procedureName, objectValues))
            {
                int ToReturn = -1;

                if (results.HasRows)
                    while (results.Read())
                        ToReturn = (int)results[0];
            }
        }

        private void removeLogYearData(int logIndex)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            int returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("DELETE FROM Table_Ride_Information WHERE @Id=[LogYearID]", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = logIndex;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = int.Parse(temp);
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
        }

        private void openRideDataForm(object sender, EventArgs e)
        {
            rideDataDisplayForm.ShowDialog();
        }

        private void openRideDataEntry(object sender, EventArgs e)
        {
            //Need to check that there is a least 1 LogYear value entered:
            if (cbLogYearConfig.Items.Count == 0)
            {
                MessageBox.Show("You must add at least 1 log Entry before entering data.  Add a new Log Year entry in the Configuration tab.");
            }
            else
            {
                if (cbRouteConfig.Items.Count == 0)
                {
                    //Give a warning if no additional routed have been entered:
                    MessageBox.Show("Reminder: No Routes have been entered. Add a new Route in the Configuration tab.");
                }
                rideDataEntryForm.ShowDialog();
            }

            //thread = new Thread(openRideDataEntryForm);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        private void btAddRoute_Click(object sender, EventArgs e)
        {
            string routeString = tbRouteConfig.Text;
            //Check to see if the string has already been entered to eliminate duplicates:
            for (int index = 0; index < cbRouteConfig.Items.Count; index++)
            {
                if (cbRouteConfig.Items.IndexOf(index).Equals(routeString))
                {
                    MessageBox.Show("Duplicate name entered. Enter a unique name for the route.");
                    return;
                }
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(routeString);
            runStoredProcedure(objectValues, "Route_Add");

            cbRouteConfig.Items.Add(routeString);
            cbRouteConfig.SelectedIndex = cbRouteConfig.Items.Count - 1;
            rideDataEntryForm.AddRouteDataEntry(routeString);
        }

        private void btRemoveRouteConfig(object sender, EventArgs e)
        {
            cbRouteConfig.Items.Remove(cbRouteConfig.SelectedItem);
            rideDataEntryForm.RemoveRouteDataEntry(tbRouteConfig.Text);

            //TODO: Replace value in the database
        }

        private void btAddBikeConfig_Click(object sender, EventArgs e)
        {
            string bikeString = tbBikeConfig.Text;
            //Check to see if the string has already been entered to eliminate duplicates:
            for (int index = 0; index < cbBikeConfig.Items.Count; index++)
            {
                if (cbBikeConfig.Items.IndexOf(index).Equals(bikeString))
                {
                    MessageBox.Show("Duplicate name entered. Enter a unique name for the bike.");
                    return;
                }
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(bikeString);
            runStoredProcedure(objectValues, "Bike_Add");

            cbBikeConfig.Items.Add(bikeString);
            cbBikeConfig.SelectedIndex = cbBikeConfig.Items.Count - 1;
            rideDataEntryForm.AddBikeDataEntry(bikeString);
        }

        private void btRemoveBikeConfig_Click(object sender, EventArgs e)
        {
            cbBikeConfig.Items.Remove(cbBikeConfig.SelectedItem);
            rideDataEntryForm.RemoveBikeDataEntry(tbBikeConfig.Text);

            //TODO: Replace value in the database
        }

        // private void openRideDataEntryForm(object obj)
        // {
        //     Application.Run(new RideDataEntry());
        // }

        //=============================================================================
        //Start Statistics Section
        //=============================================================================

        public SqlDataReader ExecuteSimpleQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";

            for (int i = 0; i < _Parameters.Count; i++)
            {
                tmpProcedureName += "@" + i.ToString() + ",";
            }

            tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
            DatabaseConnection databaseConnection = new DatabaseConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
            //Object ToReturnTest = databaseConnection.ExecuteScalarFunctionTest(ProcedureName, _Parameters);
            //MessageBox.Show(ToReturnTest.ToString());
            SqlDataReader ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName, _Parameters);
            //qlDataReader ToReturn = null;
            return ToReturn;
        }

        //Get total of miles for the selected log:
        //SELECT SUM(RideDistance) FROM Table_Ride_Information;
        private float getTotalMilesForSelectedLog(int logIndex)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            float returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMiles", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        //MessageBox.Show(String.Format("{0}", results[0]));
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = 0;
                        }
                        else {
                            returnValue = float.Parse(temp);
                        }

                    }
                }
            }

            return returnValue;
        }


        //Get total number of rides for the selected log:
        //SELECT Count(LogYearID) FROM Table_Ride_Information;
        private int getTotalRidesForSelectedLog(int logIndex)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            int returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select COUNT(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = logIndex;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = int.Parse(temp);
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

            return returnValue;
        }

        //Get average rides per week value:
        //Total rides/weeks
        private float getAverageRidesPerWeek(int logIndex)
        {
            int rides = getTotalRidesForSelectedLog(logIndex);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            float avgRides = (float)rides / weekValue;
            //MessageBox.Show(Convert.ToString(avgRides));

            return (float)(Math.Round((double)avgRides, 2)); 
        }

        //Get average miles per week value:
        //Total miles/weeks
        private float getAverageMilesPerWeek(int logIndex)
        {
            float totalMiles = getTotalMilesForSelectedLog(logIndex);
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            totalMiles = (float)totalMiles / weekValue;
            //MessageBox.Show(Convert.ToString(totalMiles));

            return (float)(Math.Round((double)totalMiles, 2));
        }

        //Get average miles per ride value:
        //Total miles/total rides
        private float getAverageMilesPerRide(int logIndex)
        {
            float miles = getTotalMilesForSelectedLog(logIndex);
            int rides = getTotalRidesForSelectedLog(logIndex);
            float averageMiles = (float)miles / rides;
            //MessageBox.Show(Convert.ToString(averageMiles));
            double avgMiles = Math.Round((double)averageMiles, 2);


            return (float)(avgMiles);
        }

        //Get the highest mileage for a week value:
        private float getHighMileageWeekNumber(int logIndex)
        {
            //add up miles for each week number
            //SELECT SUM(RideDistance) FROM Table_Ride_Information WHERE @LogYearID=[LogYearID]
            //SELECT RideDistance FROM Table_Ride_Information WHERE @WeekNumber=[WeekNumber]

            //SELECT SUM(RideDistance) FROM Table_Ride_Information WHERE @LogYearID=[LogYearID]

            //loop through each week
            //Call DB query to get total miles for the selected week
            //store week number and miles if its > then current value
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            float returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select SUM(RideDistance) from Table_Ride_Information WHERE @LogYearID=[LogYearID]", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@LogYearID";
                param.Value = logIndex;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();
                    //MessageBox.Show(temp);
                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = float.Parse(temp);
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

            return returnValue;

        }

        //Get the highest milelage for a day value:
        //SELECT MAX(RideDistance) FROM Table_Ride_Information;
        private float getHighMileageDay(int logIndex)
        {
            // conn and reader declared outside try block for visibility in finally block
            SqlConnection conn = null;
            SqlDataReader reader = null;

            float returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select MAX(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@Id";
                param.Value = logIndex;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();
                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = float.Parse(temp);
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

            return returnValue;
        }

        
        private double getDaysToNextTimeChange()
        {
            DateTime date = DateTime.Now;
            double year = date.Year;
            int month = date.Month;
            int day = date.Day;
            double dayCount = 0;
            //DateTime changeDate = new DateTime(moment.Year, moment.Month, moment.Day);
            //Days to time change -  (DateTime.Now - DateTime(Int32 year, Int32 month, Int32 day)).TotalDays {type DateTime}

            //2016    Sun, Mar 13 -,Sun, Nov 6
            //2017	Sun, Mar 12 - Sun, Nov 5,
            //2018	Sun, Mar 11 - Sun, Nov 4
            //2019	Sun, Mar 10 - Sun, Nov 3,

            //Check year
            //check if before or after March
            if (year == 2016)
            {
                DateTime changeDate = new DateTime(2016, 3, 13);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2016, 11, 6);
                    dayCount = (changeDate2 - date).TotalDays;
                }
                
            } else if (year == 2017)
            {
                DateTime changeDate = new DateTime(2017, 3, 12);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2016, 11, 5);
                    dayCount = (changeDate2 - date).TotalDays;
                }

            } else if (year == 2018)
            {
                DateTime changeDate = new DateTime(2018, 3, 11);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2016, 11, 4);
                    dayCount = (changeDate2 - date).TotalDays;
                }
            } else if (year == 2019)
            {
                DateTime changeDate = new DateTime(2019, 3, 10);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2016, 11, 3);
                    dayCount = (changeDate2 - date).TotalDays;
                }
            } else if (year == 2020)
            {
                DateTime changeDate = new DateTime(2020, 3, 9);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2016, 11, 2);
                    dayCount = (changeDate2 - date).TotalDays;
                }
            }

            double result = Math.Ceiling(dayCount);
            return result;
        }


        private int GetCurrentWeekCount()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            return weekValue;
        }

        private int GetCurrentDayCount()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetDayOfYear(DateTime.Now);

            return weekValue;
        }


        //NOTE reference in designer is commented out to not run on tabcontrol1:
        private void RefreshStatisticsData(object sender, EventArgs e)
        {
            int logYearIndex = -1;
            // Get log index and pass to all the methods:
            logYearIndex = getLogYearIndex(cbLogYear1.SelectedItem.ToString());


            if (cbLogYear1.SelectedIndex > -1)
            {
                tb1Log1.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = getHighMileageDay(logYearIndex).ToString();
            }

            if (cbLogYear2.SelectedIndex > -1)
            {
                logYearIndex = getLogYearIndex(cbLogYear2.SelectedItem.ToString());

                tb1Log2.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log2.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log2.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log2.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log2.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log2.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log2.Text = getHighMileageDay(logYearIndex).ToString();
            }

            if (cbLogYear3.SelectedIndex > -1)
            {
                logYearIndex = getLogYearIndex(cbLogYear3.SelectedItem.ToString());

                tb1Log3.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log3.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log3.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log3.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log3.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log3.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log3.Text = getHighMileageDay(logYearIndex).ToString();
            }

            if (cbLogYear4.SelectedIndex > -1)
            {
                logYearIndex = getLogYearIndex(cbLogYear4.SelectedItem.ToString());

                tb1Log4.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log4.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log4.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log4.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log4.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log4.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log4.Text = getHighMileageDay(logYearIndex).ToString();
            }

            if (cbLogYear5.SelectedIndex > -1)
            {
                logYearIndex = getLogYearIndex(cbLogYear5.SelectedItem.ToString());

                tb1Log5.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log5.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log5.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log5.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log5.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log5.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log5.Text = getHighMileageDay(logYearIndex).ToString();
            }
        }

        private void cb1LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear1.SelectedItem.ToString());
            setcbStatistic1(cbLogYear1.SelectedIndex.ToString());

            if (cbLogYear1.SelectedIndex > -1)
            {
                tb1Log1.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = getHighMileageDay(logYearIndex).ToString();
            } else
            {
                tb1Log1.Text = "";
                tb2Log1.Text = "";
                tb3Log1.Text = "";
                tb4Log1.Text = "";
                tb5Log1.Text = "";
                tb6Log1.Text = "";
                tb7Log1.Text = "";
            }
        }

        private void cb2LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear2.SelectedItem.ToString());
            setcbStatistic2(cbLogYear2.SelectedIndex.ToString());

            if (cbLogYear2.SelectedIndex > -1)
            {
                tb1Log2.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log2.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log2.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log2.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log2.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log2.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log2.Text = getHighMileageDay(logYearIndex).ToString();
            }
            else
            {
                tb1Log2.Text = "";
                tb2Log2.Text = "";
                tb3Log2.Text = "";
                tb4Log2.Text = "";
                tb5Log2.Text = "";
                tb6Log2.Text = "";
                tb7Log2.Text = "";
            }
        }

        private void cb3LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear3.SelectedItem.ToString());
            setcbStatistic3(cbLogYear3.SelectedIndex.ToString());

            if (cbLogYear3.SelectedIndex > -1)
            {
                tb1Log3.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log3.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log3.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log3.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log3.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log3.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log3.Text = getHighMileageDay(logYearIndex).ToString();
            }
            else
            {
                tb1Log3.Text = "";
                tb2Log3.Text = "";
                tb3Log3.Text = "";
                tb4Log3.Text = "";
                tb5Log3.Text = "";
                tb6Log3.Text = "";
                tb7Log3.Text = "";
            }
        }

        private void cb4LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear4.SelectedItem.ToString());
            setcbStatistic4(cbLogYear4.SelectedIndex.ToString());

            if (cbLogYear4.SelectedIndex > -1)
            {
                tb1Log4.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log4.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log4.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log4.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log4.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log4.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log4.Text = getHighMileageDay(logYearIndex).ToString();
            }
            else
            {
                tb1Log4.Text = "";
                tb2Log4.Text = "";
                tb3Log4.Text = "";
                tb4Log4.Text = "";
                tb5Log4.Text = "";
                tb6Log4.Text = "";
                tb7Log4.Text = "";
            }
        }

        private void cb5LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear5.SelectedItem.ToString());
            setcbStatistic5(cbLogYear5.SelectedIndex.ToString());

            if (cbLogYear5.SelectedIndex > -1)
            {
                tb1Log5.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log5.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log5.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log5.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log5.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log5.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log5.Text = getHighMileageDay(logYearIndex).ToString();
            }
            else
            {
                tb1Log4.Text = "";
                tb2Log4.Text = "";
                tb3Log4.Text = "";
                tb4Log4.Text = "";
                tb5Log4.Text = "";
                tb6Log4.Text = "";
                tb7Log4.Text = "";
            }
        }

        private void importFromExcelLog(object sender, EventArgs e)
        {
            //TODO: Need to set the index for the log year that is getting imported:
            //Add window to selct the index:
            int logIndex = 1;

            string[] splitList = new string[18];
            List<object> objectValues = new List<object>();
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;            

            using (OpenFileDialog openfileDialog = new OpenFileDialog() { Filter = "CSV|*.csv", Multiselect = false })
            {
                if (openfileDialog.ShowDialog() == DialogResult.OK)
                {
                    string line;
                    //Check if the file is used by another process
                    try {
                        StreamReader file = new StreamReader(openfileDialog.FileName);
                        int rowCount = 0;

                        while ((line = file.ReadLine()) != null)
                        {
                            var tempList = line.Split(',');

                            if (rowCount == 0)
                            {
                                //Line 1 is the headings                  
                                //MessageBox.Show(headingList[0]);
                            }
                            else
                            {
                                //MessageBox.Show(line);
                                objectValues.Clear();
                                splitList = line.Split(',');

                                objectValues.Add(splitList[1]);     //Moving Time:
                                objectValues.Add(splitList[2]);     //Ride Distance:
                                objectValues.Add(splitList[3]);     //Average Speed:
                                objectValues.Add(splitList[4]);     //Bike:
                                objectValues.Add(splitList[5]);     //Ride Type:
                                objectValues.Add(splitList[6]);     //Wind:
                                objectValues.Add(splitList[7]);     //Temp:
                                objectValues.Add(splitList[0]);     //Date:
                                objectValues.Add(splitList[8]);     //Average Cadence:
                                objectValues.Add(splitList[9]);     //Average Heart Rate:
                                objectValues.Add(splitList[10]);     //Max Heart Rate:
                                objectValues.Add(splitList[13]);     //Calories:
                                objectValues.Add(splitList[11]);     //Total Ascent:
                                objectValues.Add(splitList[12]);     //Total Descent:
                                objectValues.Add(splitList[14]);     //Max Speed:
                                objectValues.Add(null);              //Average Power:
                                objectValues.Add(null);              //Max Power:
                                objectValues.Add(splitList[15]);     //Route:
                                

                                //string tmepValue = splitList[17];
                                if (splitList[17].Contains('"'))
                                {
                                   // MessageBox.Show(splitList[17] + "=" + splitList[18]);
                                    string tempStr = splitList[17] + splitList[18];

                                    splitList[17] = tempStr.Replace("\"", "");
                                }


                                objectValues.Add(splitList[17]);     //Comments:
                                objectValues.Add(logIndex);         //LogYear index:

                                //Need to figure out the week from the ride date:
                                DateTime rideDate = Convert.ToDateTime(splitList[0]);
                                int weekValue = cal.GetWeekOfYear(rideDate, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                                objectValues.Add(weekValue);        //Week number:
                                objectValues.Add(splitList[16]);     //Location:
                                objectValues.Add(null);     //Windchill:
                                //MessageBox.Show(splitList[0]);

                                using (var results = ExecuteSimpleQueryConnection("Ride_Information_Add", objectValues))
                                {
                                    //string ToReturn = "";
                                    //if (results.HasRows)
                                    //    while (results.Read())
                                    //        ToReturn = results.GetString(results.GetOrdinal("field1"));
                                }
                            }

                            rowCount++;
                        }
                    } catch (Exception ex)
                    {
                        MessageBox.Show("Check to see if the file is already opened in another process.  If so, close it and try again. \n\n" + ex.Message.ToString());
                        return;
                    }
                }
            }

            //Go through list of Routes and see if any of them need to be added to the Routes table:
            List<string> currentRouteList = new List<string>();
            for (int index = 0; index < cbRouteConfig.Items.Count; index++)
            {
                currentRouteList.Add(cbRouteConfig.GetItemText(cbRouteConfig.Items[index]));               
            }

            List<string> routeList = readDataNames("Table_Ride_Information", "Route");
            foreach (var route in routeList)
            {
                if (!currentRouteList.Contains(route))
                {
                    currentRouteList.Add(route);
                    cbRouteConfig.Items.Add(route);
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(route);

                    //Add new entry to the Route Table:
                    List<object> routeObjectValues = new List<object>();
                    routeObjectValues.Add(route);
                    runStoredProcedure(routeObjectValues, "Route_Add");
                }
            }

            //Now go through the list of Bikes and see if any of them need to be added to the Bike table:
            List<string> currentBikeList = new List<string>();
            for (int index = 0; index < cbBikeConfig.Items.Count; index++)
            {
                currentBikeList.Add(cbBikeConfig.GetItemText(cbBikeConfig.Items[index]));
            }

            List<string> bikeList = readDataNames("Table_Ride_Information", "Bike");
            foreach (var bike in bikeList)
            {
                if (!currentBikeList.Contains(bike))
                {
                    currentBikeList.Add(bike);
                    cbBikeConfig.Items.Add(bike);
                    rideDataEntryForm.cbBikeDataEntry.Items.Add(bike);

                    //Add new entry to the Route Table:
                    List<object> bikeObjectValues = new List<object>();
                    bikeObjectValues.Add(bike);
                    runStoredProcedure(bikeObjectValues, "Bike_Add");
                }
            }

            //TODO: Sort comboboxes:

            MessageBox.Show("Data Import successful.");
        }

        private void cbRenameRoute(object sender, EventArgs e)
        {
            //Read textbox for value
            //Read selected index and update the value for that index:
            string newValue = tbRouteConfig.Text;
            string oldValue = cbRouteConfig.SelectedValue.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);

            runStoredProcedure(objectValues, "Route_Update");          

            List<string> tempList = new List<string>();

            int selectedIndex = cbRouteConfig.SelectedIndex;
            cbRouteConfig.DataSource = cbRouteConfig.Items;

            for (int i = 0; i < cbRouteConfig.Items.Count; i++)
            {
                tempList.Add(cbRouteConfig.Items[i].ToString());
            }

            cbRouteConfig.DataSource = null;
            cbRouteConfig.Items.Clear();
            rideDataEntryForm.cbRouteDataEntry.DataSource = null;
            rideDataEntryForm.cbRouteDataEntry.Items.Clear();

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbRouteConfig.Items.Add(newValue);
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(newValue);
                }
                else {
                    cbRouteConfig.Items.Add(tempList[i]);
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(tempList[i]);
                }
            }
            cbRouteConfig.SelectedIndex = selectedIndex;
            //rideDataEntryForm.cbRouteDataEntry.SelectedIndex = -1;

            //Update the route name in the database for each row:
            //Update value in database:
            SqlConnection conn = null;
            SqlDataReader reader = null;

            float returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information(Route) SET Route=@NewValue WHERE [Route]=@OldValue", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@NewValue";
                param.Value = newValue;
                param.ParameterName = "@OldValue";
                param.Value = oldValue;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();
                    //MessageBox.Show(temp);
                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = float.Parse(temp);
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

        }

        private void bRenameLogYear_Click(object sender, EventArgs e)
        {
            string newValue = tbLogYearConfig.Text;

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);                

            runStoredProcedure(objectValues, "Log_Year_Update");

            List<string> tempList = new List<string>();

            int selectedIndex = cbLogYearConfig.SelectedIndex;
            int statIndex1 = cbLogYear1.SelectedIndex;
            int statIndex2 = cbLogYear1.SelectedIndex;
            int statIndex3 = cbLogYear1.SelectedIndex;
            int statIndex4 = cbLogYear1.SelectedIndex;
            int statIndex5 = cbLogYear1.SelectedIndex;

            cbLogYearConfig.DataSource = cbLogYearConfig.Items;

            for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
            {
                tempList.Add(cbLogYearConfig.Items[i].ToString());
            }

            cbLogYearConfig.DataSource = null;
            cbLogYearConfig.Items.Clear();
            rideDataEntryForm.cbLogYearDataEntry.DataSource = null;
            rideDataEntryForm.cbLogYearDataEntry.Items.Clear();
            rideDataDisplayForm.cbLogYearFilter.DataSource = null;
            rideDataDisplayForm.cbLogYearFilter.Items.Clear();
            cbLogYear1.DataSource = null;
            cbLogYear1.Items.Clear();
            cbLogYear2.DataSource = null;
            cbLogYear2.Items.Clear();
            cbLogYear3.DataSource = null;
            cbLogYear3.Items.Clear();
            cbLogYear4.DataSource = null;
            cbLogYear4.Items.Clear();
            cbLogYear5.DataSource = null;
            cbLogYear5.Items.Clear();

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbLogYearConfig.Items.Add(newValue);
                    rideDataEntryForm.cbLogYearDataEntry.Items.Add(newValue);
                    rideDataDisplayForm.cbLogYearFilter.Items.Add(newValue);
                    cbLogYear1.Items.Add(newValue);
                    cbLogYear2.Items.Add(newValue);
                    cbLogYear3.Items.Add(newValue);
                    cbLogYear4.Items.Add(newValue);
                    cbLogYear5.Items.Add(newValue);
                }
                else {
                    cbLogYearConfig.Items.Add(tempList[i]);
                    rideDataEntryForm.cbLogYearDataEntry.Items.Add(tempList[i]);
                    rideDataDisplayForm.cbLogYearFilter.Items.Add(tempList[i]);
                    cbLogYear1.Items.Add(tempList[i]);
                    cbLogYear2.Items.Add(tempList[i]);
                    cbLogYear3.Items.Add(tempList[i]);
                    cbLogYear4.Items.Add(tempList[i]);
                    cbLogYear5.Items.Add(tempList[i]);
                }
            }
            cbLogYearConfig.SelectedIndex = selectedIndex;
            rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = selectedIndex;
            rideDataDisplayForm.cbLogYearFilter.SelectedIndex = selectedIndex;
            cbLogYear1.SelectedIndex = statIndex1;
            cbLogYear2.SelectedIndex = statIndex2;
            cbLogYear3.SelectedIndex = statIndex3;
            cbLogYear4.SelectedIndex = statIndex4;
            cbLogYear5.SelectedIndex = statIndex5;
        }

        private void bRenameBike_Click(object sender, EventArgs e)
        {
            string newValue = tbBikeConfig.Text;
            string oldValue = cbBikeConfig.SelectedValue.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);

            runStoredProcedure(objectValues, "Bike_Update");

            List<string> tempList = new List<string>();

            int selectedIndex = cbBikeConfig.SelectedIndex;
            cbBikeConfig.DataSource = cbBikeConfig.Items;

            for (int i = 0; i < cbBikeConfig.Items.Count; i++)
            {
                tempList.Add(cbBikeConfig.Items[i].ToString());
            }

            cbBikeConfig.DataSource = null;
            cbBikeConfig.Items.Clear();
            rideDataEntryForm.cbBikeDataEntry.DataSource = null;
            rideDataEntryForm.cbBikeDataEntry.Items.Clear();

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbBikeConfig.Items.Add(newValue);
                    rideDataEntryForm.cbBikeDataEntry.Items.Add(newValue);
                }
                else {
                    cbBikeConfig.Items.Add(tempList[i]);
                    rideDataEntryForm.cbBikeDataEntry.Items.Add(tempList[i]);
                }
            }
            cbBikeConfig.SelectedIndex = selectedIndex;
            rideDataEntryForm.cbBikeDataEntry.SelectedIndex = selectedIndex;

            //Update value in database:
            SqlConnection conn = null;
            SqlDataReader reader = null;

            float returnValue = 0;

            try
            {
                // instantiate and open connection
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                conn.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information(Route) SET [Bike]=@NewValue WHERE [Bike]=@OldValue", conn);

                // 2. define parameters used in command object
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@NewValue";
                param.Value = newValue;
                param.ParameterName = "@OldValue";
                param.Value = oldValue;

                // 3. add new parameter to command object
                cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    //Console.WriteLine(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();
                    //MessageBox.Show(temp);
                    if (temp.Equals(""))
                    {
                        returnValue = 0;
                    }
                    else {
                        returnValue = float.Parse(temp);
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
        }

        //=============================================================================
        //End Statistics Section
        //=============================================================================


        private void button1_Click_1(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.Show();

        }
    }
}
