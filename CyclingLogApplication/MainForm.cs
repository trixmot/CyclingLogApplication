using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Collections.Specialized;
using System.Threading;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Configuration;


namespace CyclingLogApplication
{
    public partial class MainForm : Form
    {
        private static Mutex mutex;
        RideDataEntry rideDataEntryForm;
        RideDataDisplay rideDataDisplayForm;
        ChartForm chartForm;

        private static string logVersion = "0.1.1";
        private static int logLevel = 0;
        private static string cbStatistic1 = "-1";
        private static string cbStatistic2 = "-1";
        private static string cbStatistic3 = "-1";
        private static string cbStatistic4 = "-1";
        private static string cbStatistic5 = "-1";
        private static int lastLogSelected = -1;
        private static int lastBikeSelected = -1;
        private static int lastLogFilterSelected = -1;
        private static int lastLogYearChart = -1;
        private static int lastRouteChart = -1;
        private static int lastTypeChart = -1;
        private static int lastTypeTimeChart = -1;
        private static int lastMonthlyLogSelected = -1;
        private static int lastLogSelectedDataEntry = -1;

        private static SqlConnection sqlConnection;             // = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
        private static DatabaseConnection databaseConnection;   // = new DatabaseConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");

        //connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\workdir\CylingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf;Integrated Security=True"
        //connectionString="Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|CyclingLogDatabase.mdf;Integrated Security=True"

        public MainForm()
        {
            //Set DataDirectory for the contectionstring in the app.config:
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

            Text = "Single Instance!";
            mutex = new Mutex(false, "SINGLE_INSTANCE_MUTEX");
            if (!mutex.WaitOne(0, false))
            {
                mutex.Close();
                mutex = null;
            }

            InitializeComponent();
            GetConnectionStrings();
            int logSetting = getLogLevel();

            Logger.Log("**********************************************", 1, logSetting);
            Logger.Log("Staring Log Application", 1, 0);

            Logger.Log("**********************************************", 1, logSetting);

            tbWeekCount.Text = GetCurrentWeekCount().ToString();
            tbDayCount.Text = GetCurrentDayCount().ToString();
            tbTimeChange.Text = getDaysToNextTimeChange().ToString();
        }

        public MainForm(string emptyConstructor)
        {
            //Empty consturctor to prevent from running InitializeComponent():
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (getMutex() != null)
            {
                //Application.Run(app);
            }
            else
            {
                Logger.Log("Instance of ExtraViewToRallyConnector is already running", 1, logLevel);
                System.Environment.Exit(0);
            }

            ConfigurationFile configfile = new ConfigurationFile();
            configfile.readConfigFile();
            int logSetting = getLogLevel();

            //Get all values and load the comboboxes:
            List<string> logYearList = new List<string>();
            //List<string> yearList = new List<string>();
            List<string> routeList = new List<string>();
            List<string> bikeList = new List<string>();

            logYearList = readDataNames("Table_Log_year", "Name");
            //yearList = readDataNames("Table_Log_year", "Year");
            routeList = readDataNames("Table_Routes", "Name");
            bikeList = readDataNames("Table_Bikes", "Name");

            rideDataEntryForm = new RideDataEntry(this);
            rideDataDisplayForm = new RideDataDisplay(this);
            chartForm = new ChartForm(this);

            //Set first option of 'None':
            cbLogYear1.Items.Add("--None--");
            cbLogYear2.Items.Add("--None--");
            cbLogYear3.Items.Add("--None--");
            cbLogYear4.Items.Add("--None--");
            cbLogYear5.Items.Add("--None--");

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
                chartForm.cbLogYearChart.Items.Add(val);
                Logger.Log("Data Loading: Log Year: " + val, 0, logSetting);
            }

            rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = getLastLogSelectedDataEntry();

            //Load Route values:
            foreach (var val in routeList)
            {
                cbRouteConfig.Items.Add(val);
                rideDataEntryForm.cbRouteDataEntry.Items.Add(val);
                chartForm.cbRoutesChart.Items.Add(val);
                Logger.Log("Data Loading: Route: " + val, 1, logSetting);
            }

            //Load Bike values:
            foreach (var val in bikeList)
            {
                cbBikeConfig.Items.Add(val);
                cbBikeMaint.Items.Add(val);
                Logger.Log("Data Loading: Bikes: " + val, 1, logSetting);
            }

            if (logYearList.Count == 0)
            {
                MessageBox.Show("No Yearly Logs have been added to the database. Please add an entry before continuing.");
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

            btBikeMilesUpdate_click();
            btGetMaintLog_Click(sender, e);

            //Set first option of 'None':
            cbStatMonthlyLogYear.Items.Add("--None--");

            //Load LogYear Monthly values:
            foreach (string val in logYearList)
            {
                cbStatMonthlyLogYear.Items.Add(val);
            }

            //Load Statistic combo index values:
            cbLogYear1.SelectedIndex = Convert.ToInt32(getcbStatistic1());
            cbLogYear2.SelectedIndex = Convert.ToInt32(getcbStatistic2());
            cbLogYear3.SelectedIndex = Convert.ToInt32(getcbStatistic3());
            cbLogYear4.SelectedIndex = Convert.ToInt32(getcbStatistic4());
            cbLogYear5.SelectedIndex = Convert.ToInt32(getcbStatistic5());

            cbStatMonthlyLogYear.SelectedIndex = Convert.ToInt32(getLastMonthlyLogSelected());

            label2.Text = "App Version: " + getLogVersion();
            lbMaintError.Text = "";
            lbConfigError.Text = "";

            RefreshStatisticsData();
            runMonthlyStatistics();
        }

        private void closeForm(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chartForm.Close();
                int logSetting = getLogLevel();
                ConfigurationFile configurationFile = new ConfigurationFile();
                configurationFile.writeConfigFile();

                Logger.Log("**********************************************", 1, logSetting);
                Logger.Log("Ending Log Application", 1, logSetting);
                Logger.Log("**********************************************", 1, logSetting);

                rideDataDisplayForm.Dispose();
                chartForm.Dispose();
                this.Dispose();
                Application.Exit();
            }
        }

        static void GetConnectionStrings()
        {
            string conStr = ConfigurationManager.ConnectionStrings["CyclingLogApplication.Properties.Settings.CyclingLogDatabaseConnectionString"].ConnectionString;
            Logger.Log("conStr Name: " + conStr, 1, 1);
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.Name.Equals("CyclingLogApplication.Properties.Settings.CyclingLogDatabaseConnectionString"))
                    {
                        Logger.Log("ConnectionStringSettingsCollection Name: " + cs.Name, 1, 1);
                        Logger.Log("ConnectionStringSettingsCollection ProviderName: " + cs.ProviderName, 1, 1);
                        Logger.Log("ConnectionStringSettingsCollection ConnectionString: " + cs.ConnectionString, 1, 1);

                        sqlConnection = new SqlConnection(cs.ConnectionString);
                        databaseConnection = new DatabaseConnection(cs.ConnectionString);

                        break;
                    }
                }
            }
        }

        public SqlConnection getsqlConnectionString()
        {
            return sqlConnection;
        }

        public DatabaseConnection getsDatabaseConnectionString()
        {
            return databaseConnection;
        }

        private Mutex getMutex()
        {
            return mutex;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //        mutex.ReleaseMutex();
        //    base.Dispose(disposing);
        //}

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

        public int getLastBikeSelected()
        {
            return lastBikeSelected;
        }

        public void setLastBikeSelected(int bikeIndex)
        {
            lastBikeSelected = bikeIndex;
        }

        public int getLastLogSelected()
        {
            return lastLogSelected;
        }

        public void setLastLogSelected(int logIndex)
        {
            lastLogSelected = logIndex;
        }

        public int getLastMonthlyLogSelected()
        {
            return lastMonthlyLogSelected;
        }

        public void setLastMonthlyLogSelected(int logIndex)
        {
            lastMonthlyLogSelected = logIndex;
        }

        public int getLastLogSelectedDataEntry()
        {
            return lastLogSelectedDataEntry;
        }

        public void setLastLogSelectedDataEntry(int logIndex)
        {
            lastLogSelectedDataEntry = logIndex;
        }

        public void setLastLogFilterSelected(int logIndex)
        {
            lastLogFilterSelected = logIndex;
        }

        public int getLastLogFilterSelected()
        {
            return lastLogFilterSelected;
        }

        public void setLastLogYearChartSelected(int logIndex)
        {
            lastLogYearChart = logIndex;
        }

        public int getLastLogYearChartSelected()
        {
            return lastLogYearChart;
        }

        public void setLastRouteChartSelected(int logIndex)
        {
            lastRouteChart = logIndex;
        }

        public int getLastRouteChartSelected()
        {
            return lastRouteChart;
        }

        public void setLastTypeChartSelected(int logIndex)
        {
            lastTypeChart = logIndex;
        }

        public int getLastTypeChartSelected()
        {
            return lastTypeChart;
        }

        public void setLastTypeTimeChartSelected(int logIndex)
        {
            lastTypeTimeChart = logIndex;
        }

        public int getLastTypeTimeChartSelected()
        {
            return lastTypeTimeChart;
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

        public List<string> readDataNames(string tableName, string columnName)
        {
            SqlDataReader reader = null;
            List<string> nameList = new List<string>();
            int logSetting = getLogLevel();

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("SELECT " + columnName + " FROM " + tableName + " ORDER BY " + columnName + " ASC", sqlConnection);

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
                    nameList.Add(returnValue);
                    Logger.Log("Reading data from the database: columnName:" + returnValue, 0, logSetting);
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
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }

            return nameList;
        }

        private void btAddLogYearConfig(object sender, EventArgs e)
        {
            string logYearTitle;

            if (cbLogYearConfig.SelectedItem != null)
            {
                logYearTitle = tbLogYearConfig.Text;
                //Check to see if the string has already been entered to eliminate duplicates:
                for (int index = 1; index < cbLogYearConfig.Items.Count; index++)
                {
                    if (cbLogYearConfig.SelectedItem.ToString().Equals(logYearTitle))
                    {
                        MessageBox.Show("Duplicate name entered. Enter a unique name for the log.");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid name entered. Enter a unique name for the log.");
                return;
            }

            if (cbLogYear.SelectedIndex == 0)
            {
                MessageBox.Show("Invalid Log Year selected. Select a valid year.");
                return;
            }

            string logYearValue = cbLogYear.SelectedItem.ToString();
            int logSetting = getLogLevel();

            //Add new entry to the LogYear Table:
            List<object> objectValues = new List<object>();
            objectValues.Add(logYearTitle);
            objectValues.Add(Convert.ToInt32(logYearValue));
            runStoredProcedure(objectValues, "Log_Year_Add");

            cbLogYearConfig.Items.Add(logYearTitle);
            cbLogYearConfig.SelectedIndex = cbLogYearConfig.Items.Count - 1;
            rideDataEntryForm.AddLogYearDataEntry(logYearTitle);
            rideDataDisplayForm.AddLogYearFilter(logYearTitle);
            chartForm.cbLogYearChart.Items.Add(logYearTitle);

            //Update combo's on stat tab:
            cbLogYear1.Items.Add(logYearTitle);
            cbLogYear2.Items.Add(logYearTitle);
            cbLogYear3.Items.Add(logYearTitle);
            cbLogYear4.Items.Add(logYearTitle);
            cbLogYear5.Items.Add(logYearTitle);

            Logger.Log("Adding a Log Year entry to the Configuration:" + logYearTitle, 0, logSetting);
        }

        private void removeLogYearConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Log and all its data?", "Delete Log", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //List<string> tempList = new List<string>();

                //int selectedIndex = cbLogYearConfig.SelectedIndex;
                //int statIndex1 = cbLogYear1.SelectedIndex;
                //int statIndex2 = cbLogYear1.SelectedIndex;
                //int statIndex3 = cbLogYear1.SelectedIndex;
                //int statIndex4 = cbLogYear1.SelectedIndex;
                //int statIndex5 = cbLogYear1.SelectedIndex;

                //TODO: 
                cbLogYearConfig.Items.Remove(cbLogYearConfig.SelectedItem);
                rideDataEntryForm.cbLogYearDataEntry.Items.Remove(cbLogYearConfig.SelectedItem);
                rideDataDisplayForm.cbLogYearFilter.Items.Remove(cbLogYearConfig.SelectedItem);
                chartForm.cbLogYearChart.Items.Remove(cbLogYearConfig.SelectedItem);
                cbLogYear1.Items.Remove(cbLogYearConfig.SelectedItem);
                cbLogYear2.Items.Remove(cbLogYearConfig.SelectedItem);
                cbLogYear3.Items.Remove(cbLogYearConfig.SelectedItem);
                cbLogYear4.Items.Remove(cbLogYearConfig.SelectedItem);
                cbLogYear5.Items.Remove(cbLogYearConfig.SelectedItem);

                //cbLogYearConfig.DataSource = cbLogYearConfig.Items;

                //for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
                //{
                //    tempList.Add(cbLogYearConfig.Items[i].ToString());
                //}

                //cbLogYearConfig.DataSource = null;
                //cbLogYearConfig.Items.Clear();
                //rideDataEntryForm.cbLogYearDataEntry.DataSource = null;
                //rideDataEntryForm.cbLogYearDataEntry.Items.Clear();
                //rideDataDisplayForm.cbLogYearFilter.DataSource = null;
                //rideDataDisplayForm.cbLogYearFilter.Items.Clear();
                //chartForm.cbLogYearChart.Items.Clear();
                //chartForm.cbLogYearChart.DataSource = null;
                //cbLogYear1.DataSource = null;
                //cbLogYear1.Items.Clear();
                //cbLogYear2.DataSource = null;
                //cbLogYear2.Items.Clear();
                //cbLogYear3.DataSource = null;
                //cbLogYear3.Items.Clear();
                //cbLogYear4.DataSource = null;
                //cbLogYear4.Items.Clear();
                //cbLogYear5.DataSource = null;
                //cbLogYear5.Items.Clear();

                ////Set first option of 'None':
                //cbLogYear1.Items.Add("--None--");
                //cbLogYear2.Items.Add("--None--");
                //cbLogYear3.Items.Add("--None--");
                //cbLogYear4.Items.Add("--None--");
                //cbLogYear5.Items.Add("--None--");

                //for (int i = 0; i < tempList.Count; i++)
                //{
                //    cbLogYearConfig.Items.Add(tempList[i]);
                //    rideDataEntryForm.cbLogYearDataEntry.Items.Add(tempList[i]);
                //    rideDataDisplayForm.cbLogYearFilter.Items.Add(tempList[i]);
                //    chartForm.cbLogYearChart.Items.Add(tempList[i]);
                //    cbLogYear1.Items.Add(tempList[i]);
                //    cbLogYear2.Items.Add(tempList[i]);
                //    cbLogYear3.Items.Add(tempList[i]);
                //    cbLogYear4.Items.Add(tempList[i]);
                //    cbLogYear5.Items.Add(tempList[i]);
                //}

                int logYearIndex = getLogYearIndex(cbLogYearConfig.SelectedItem.ToString());

                //Remove logyear from the Log year table:
                List<object> objectValues = new List<object>();
                objectValues.Add(logYearIndex);
                runStoredProcedure(objectValues, "Log_Year_Remove");

                //Need to remove all data for this log from the database:
                removeLogYearData(logYearIndex);
                cbLogYearConfig.Text = "";
            }
        }

        public int getLogYearIndex(string logYearName)
        {
            SqlDataReader reader = null;
            int returnValue = -1;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("SELECT LogYearID FROM Table_Log_Year WHERE @logYearName=[Name]", sqlConnection);

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
                    string temp = reader[0].ToString();

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
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to the Log year Index from the database." + ex.Message.ToString());
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
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
            SqlDataReader reader = null;

            int returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("DELETE FROM Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection);

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
                    string temp = reader[0].ToString();

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
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to remove Log year data from the database." + ex.Message.ToString());
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void openRideDataForm(object sender, EventArgs e)
        {
            rideDataDisplayForm.setLogYearFilterIndex(getLastLogFilterSelected());
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
                
                rideDataEntryForm.cbBikeDataEntrySelection.SelectedIndex = Convert.ToInt32(getLastBikeSelected());
                rideDataEntryForm.ShowDialog();
            }

            //thread = new Thread(openRideDataEntryForm);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        private void btAddRoute_Click(object sender, EventArgs e)
        {
            string routeString = tbRouteConfig.Text;
            int logSetting = getLogLevel();

            if (cbRouteConfig.SelectedItem != null)
            {
                //Check to see if the string has already been entered to eliminate duplicates:
                for (int index = 0; index < cbRouteConfig.Items.Count; index++)
                {
                    if (cbRouteConfig.Items.IndexOf(index).Equals(routeString))
                    {
                        MessageBox.Show("Duplicate name entered. Enter a unique name for the route.");
                        return;
                    }
                }
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(routeString);
            runStoredProcedure(objectValues, "Route_Add");

            cbRouteConfig.Items.Add(routeString);
            cbRouteConfig.SelectedIndex = cbRouteConfig.Items.Count - 1;
            rideDataEntryForm.AddRouteDataEntry(routeString);
            chartForm.cbRoutesChart.Items.Add(routeString);
            Logger.Log("Adding a Route entry to the Configuration:" + routeString, 0, logSetting);
        }

        private void btRemoveRouteConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Route option?", "Delete Route Option", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string deleteValue = cbRouteConfig.SelectedItem.ToString();

                //Note: only removing value as an option, all records using this value are unchanged:
                cbRouteConfig.Items.Remove(cbRouteConfig.SelectedItem);
                rideDataEntryForm.RemoveRouteDataEntry(tbRouteConfig.Text);
                chartForm.cbRoutesChart.Items.Remove(tbRouteConfig.Text);

                //Remove the Route from the database table:
                List<object> objectValues = new List<object>();
                objectValues.Add(deleteValue);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Route_Remove", objectValues))
                    {

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete the Route name entry." + ex.Message.ToString());
                }
            }
        }

        private void btAddBikeConfig_Click(object sender, EventArgs e)
        {
            string bikeString = tbBikeConfig.Text;
            string miles = tbConfigMilesNotInLog.Text;

            if (cbBikeConfig.SelectedItem != null)
            {
                //Check to see if the string has already been entered to eliminate duplicates:
                for (int index = 0; index < cbBikeConfig.Items.Count; index++)
                {
                    if (cbBikeConfig.Items.IndexOf(index).Equals(bikeString))
                    {
                        MessageBox.Show("Duplicate name entered. Enter a unique name for the bike.");
                        return;
                    }
                }
            }

            //Verify Miles is entered and in the correct format:
            int parsedValue;
            if (!int.TryParse(tbConfigMilesNotInLog.Text, out parsedValue))
            {
                lbConfigError.Text = "The miles for the Bike must be in numeric format. Enter 0 if unknown.";
                return;
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(bikeString);
            objectValues.Add(miles);
            runStoredProcedure(objectValues, "Bike_Add");

            cbBikeConfig.Items.Add(bikeString);
            cbBikeMaint.Items.Add(tbConfigMilesNotInLog.Text);
            cbBikeConfig.SelectedIndex = cbBikeConfig.Items.Count - 1;
            rideDataEntryForm.AddBikeDataEntry(bikeString);
        }

        private void btRemoveBikeConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the bike option?", "Delete Bike Option", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string deleteValue = cbBikeConfig.SelectedItem.ToString();

                //Note: only removing value as an option, all records using this value are unchanged:
                cbBikeConfig.Items.Remove(cbBikeConfig.SelectedItem);
                cbBikeMaint.Items.Remove(cbBikeConfig.SelectedItem);
                rideDataEntryForm.RemoveBikeDataEntry(deleteValue);

                //List<string> tempList = new List<string>();

                //int selectedIndex = cbBikeConfig.SelectedIndex;
                //cbBikeConfig.DataSource = cbBikeConfig.Items;

                //for (int i = 0; i < cbBikeConfig.Items.Count; i++)
                //{
                //    tempList.Add(cbBikeConfig.Items[i].ToString());
                //}

                //cbBikeConfig.DataSource = null;
                //cbBikeConfig.Items.Clear();

                //cbBikeMaint.DataSource = null;
                //cbBikeMaint.Items.Clear();

                //rideDataEntryForm.cbBikeDataEntrySelection.DataSource = null;
                //rideDataEntryForm.cbBikeDataEntrySelection.Items.Clear();

                //Clear entires:
                tbConfigMilesNotInLog.Text = "0";
                tbBikeConfig.Text = "";

                //for (int i = 0; i < tempList.Count; i++)
                //{
                //    cbBikeConfig.Items.Add(tempList[i]);
                //    cbBikeMaint.Items.Add(tempList[i]);
                //    rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(tempList[i]);
                //}

                //Remove the Bike from the database table:
                List<object> objectValues = new List<object>();
                objectValues.Add(deleteValue);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Bike_Remove", objectValues))
                    {

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete Bike name entry." + ex.Message.ToString());
                }
            }
        }

        private void cbRouteConfig_SelectedIndexChanged(object sender, EventArgs e)
        {         
            if (cbRouteConfig.SelectedItem != null)
            {
                tbRouteConfig.Text = cbRouteConfig.SelectedItem.ToString();
            } 
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
            SqlDataReader ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName, _Parameters);

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

        private float getTotalMilesForAllLogs()
        {
            List<object> objectValues = new List<object>();
            float returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMiles_AllLogs", objectValues))
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

        //Get total number of rides for the selected log:
        //SELECT Count(LogYearID) FROM Table_Ride_Information;
        private int getTotalRidesForSelectedLog(int logIndex)
        {
            SqlDataReader reader = null;
            int returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select COUNT(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection);

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
                    string temp = reader[0].ToString();

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
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
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
            float avgRides = 0;

            if (rides > 0)
            {
                avgRides = (float)rides / weekValue;
            }
            //MessageBox.Show(Convert.ToString(avgRides));

            return (float)(Math.Round((double)avgRides, 2));
        }

        //Get average miles per week value:
        //Total miles/weeks
        private float getAverageMilesPerWeek(int logIndex)
        {
            float totalMiles = getTotalMilesForSelectedLog(logIndex);
            float avgMiles = 0;
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            if (totalMiles > 0)
            {
                avgMiles = (float)totalMiles / weekValue;
            }
            //MessageBox.Show(Convert.ToString(totalMiles));

            return (float)(Math.Round((double)avgMiles, 2));
        }

        //Get average miles per ride value:
        //Total miles/total rides
        private float getAverageMilesPerRide(int logIndex)
        {
            float miles = getTotalMilesForSelectedLog(logIndex);
            int rides = getTotalRidesForSelectedLog(logIndex);
            float averageMiles = 0;

            if (miles > 0)
            {
                averageMiles = (float)miles / rides;
            }

            //MessageBox.Show(Convert.ToString(averageMiles));
            double avgMiles = Math.Round((double)averageMiles, 2);

            return (float)(avgMiles);
        }

        //Get the highest mileage for a week value:
        private double getHighMileageWeekNumber(int logIndex)
        {
            List<double> rideDistanceList = new List<double>();
            int weekNumber = 1;
            int weekNumberTmp = 0;
            double weekMilesTotal = 0;
            double weeklyMax = 0;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + logIndex + "=[LogYearID]";
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
                                // Compare weekly total to see if max:
                                if (weekMilesTotal > weeklyMax)
                                {
                                    weeklyMax = weekMilesTotal;
                                }

                                // Onto a new week, so reset weekly total:
                                weekMilesTotal = (double)reader["RideDistance"];
                            }
                            else
                            {
                                weekMilesTotal = weekMilesTotal + (double)reader["RideDistance"];
                            }
                        }

                        reader.Close();
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
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }

            // Check last weekly total to see if max:
            if (weekMilesTotal > weeklyMax)
            {
                weeklyMax = weekMilesTotal;
            }

            return weeklyMax;
        }

        //Get the highest milelage for a day value:
        //SELECT MAX(RideDistance) FROM Table_Ride_Information;
        private float getHighMileageDay(int logIndex)
        {
            SqlDataReader reader = null;
            float returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("select MAX(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection);

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
                    string temp = reader[0].ToString();
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
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
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

            //2016  Sun, Mar 13 -,Sun, Nov 6
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
            }
            else if (year == 2017)
            {
                DateTime changeDate = new DateTime(2017, 3, 12);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2017, 11, 5);

                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2018, 3, 11);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }                                 
                }
            }
            else if (year == 2018)
            {
                DateTime changeDate = new DateTime(2018, 3, 11);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2018, 11, 4);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2019, 3, 10);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2019)
            {
                DateTime changeDate = new DateTime(2019, 3, 10);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2019, 11, 3);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2020, 3, 8);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2020)
            {
                DateTime changeDate = new DateTime(2020, 3, 8);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2020, 11, 1);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2021, 3, 14);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2021)
            {
                DateTime changeDate = new DateTime(2021, 3, 14);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2021, 11, 7);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2022, 3, 13);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2022)
            {
                DateTime changeDate = new DateTime(2022, 3, 13);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2022, 11, 6);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2023, 3, 13);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else
            {
                DateTime changeDate = new DateTime(Convert.ToInt32(year), 3, 13);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(Convert.ToInt32(year), 11, 6);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(Convert.ToInt32(year) + 1, 3, 13);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
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
        // Yearly:
        private void RefreshStatisticsData()
        {
            int logYearIndex = -1;

            // Get log index and pass to all the methods:
            if (cbLogYear1.SelectedItem == null)
            {
                logYearIndex = 0;
            }
            else
            {
                logYearIndex = getLogYearIndex(cbLogYear1.SelectedItem.ToString());
            }         

            if (cbLogYear1.SelectedIndex > 0)
            {
                tb1Log1.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = getHighMileageDay(logYearIndex).ToString();
            }

            if (cbLogYear2.SelectedIndex > 0)
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

            if (cbLogYear3.SelectedIndex > 0)
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

            if (cbLogYear4.SelectedIndex > 0)
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

            if (cbLogYear5.SelectedIndex > 0)
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

            //Get total miles for all logs:
            double totalMiles = getTotalMilesForAllLogs();
            totalMiles = Math.Truncate(totalMiles * 100) / 100;
            tbStatisticsTotalMiles.Text = Convert.ToString(Math.Round(totalMiles, 1));
            tbLongestRide.Text = Convert.ToString(getLongestRide());
            tbTotalRides.Text = Convert.ToString(getTotalRides());
        }

        private double getLongestRide()
        {
            List<object> objectValues = new List<object>();
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetLongestRide_AllLogs", objectValues))
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

            return Math.Round(returnValue, 1);
        }

        private double getFastestAvg()
        {
            List<object> objectValues = new List<object>();
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetFastestAvg_AllLogs", objectValues))
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

        private double getMaxSpeed()
        {
            List<object> objectValues = new List<object>();
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMaxSpeed_AllLogs", objectValues))
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

        private double getTotalRides()
        {
            List<object> objectValues = new List<object>();
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalRides_AllLogs", objectValues))
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

        private void cb1LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = getLogYearIndex(cbLogYear1.SelectedItem.ToString());
            setcbStatistic1(cbLogYear1.SelectedIndex.ToString());

            if (cbLogYear1.SelectedIndex > 0)
            {
                tb1Log1.Text = getTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = getTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = getAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = getAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = getAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = getHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = getHighMileageDay(logYearIndex).ToString();
            }
            else
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

            if (cbLogYear2.SelectedIndex > 0)
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

            if (cbLogYear3.SelectedIndex > 0)
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

            if (cbLogYear4.SelectedIndex > 0)
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

            if (cbLogYear5.SelectedIndex > 0)
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
                tb1Log5.Text = "";
                tb2Log5.Text = "";
                tb3Log5.Text = "";
                tb4Log5.Text = "";
                tb5Log5.Text = "";
                tb6Log5.Text = "";
                tb7Log5.Text = "";
            }
        }

        public void importFromExcelLog(object sender, EventArgs e)
        {
            //window to selct the index:
            LegacyImport legacyImport = new LegacyImport();
            legacyImport.ShowDialog();

            int logIndex = legacyImport.getLegacyIndexSelection() + 1;

            if (logIndex < 1)
            {
                return;
            }

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
                    try
                    {
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
                                objectValues.Add(splitList[7]);     //Wind:
                                objectValues.Add(splitList[8]);     //Temp:
                                objectValues.Add(splitList[0]);     //Date:
                                objectValues.Add(splitList[9]);     //Average Cadence:
                                objectValues.Add(splitList[10]);     //Average Heart Rate:
                                objectValues.Add(splitList[11]);     //Max Heart Rate:
                                objectValues.Add(splitList[15]);     //Calories:
                                objectValues.Add(splitList[12]);     //Total Ascent:
                                objectValues.Add(splitList[13]);     //Total Descent:
                                objectValues.Add(splitList[16]);     //Max Speed:
                                objectValues.Add(null);              //Average Power:
                                objectValues.Add(null);              //Max Power:
                                objectValues.Add(splitList[17]);     //Route:

                                //string tmepValue = splitList[18];
                                //if (splitList[18].Contains('"'))
                                //{
                                //   // MessageBox.Show(splitList[18] + "=" + splitList[19]);
                                //    string tempStr = splitList[18];

                                //    splitList[18] = tempStr.Replace("\"", "");
                                //}
                                string comment = "";
                                if (splitList.Length > 19)
                                {
                                    //Get the total:
                                    int arraySize = splitList.Length;
                                    for (int index = 18; index < arraySize; index++)
                                    {
                                        comment = comment + splitList[index];
                                    }
                                }

                                objectValues.Add(comment);     //Comments:
                                objectValues.Add(logIndex);         //LogYear index:

                                //Need to figure out the week from the ride date:
                                DateTime rideDate = Convert.ToDateTime(splitList[0]);
                                int weekValue = cal.GetWeekOfYear(rideDate, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                                objectValues.Add(weekValue);        //Week number:
                                objectValues.Add(splitList[14]);     //Location:
                                objectValues.Add(null);     //Windchill:
                                objectValues.Add(splitList[6]);     //Effort:

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
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to read the .CVS file." + ex.Message.ToString());
                        MessageBox.Show("[ERROR] An error occured while reading the .CVS file. \n\n");
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
                    rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(bike);

                    //Add new entry to the Route Table:
                    List<object> bikeObjectValues = new List<object>();
                    bikeObjectValues.Add(bike);
                    runStoredProcedure(bikeObjectValues, "Bike_Add");
                }
            }

            MessageBox.Show("Data Import successful.");
        }

        private void cbRenameRoute(object sender, EventArgs e)
        {
            //Read selected index and update the value for that index:
            string newValue = tbRouteConfig.Text;
            string oldValue = cbRouteConfig.SelectedItem.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);
            objectValues.Add(oldValue);

            runStoredProcedure(objectValues, "Route_Update");

            List<string> tempList = new List<string>();
            int selectedIndex = cbRouteConfig.SelectedIndex;

            for (int i = 0; i < cbRouteConfig.Items.Count; i++)
            {
                tempList.Add(cbRouteConfig.Items[i].ToString());
            }

            cbRouteConfig.Sorted = true;
            rideDataEntryForm.cbRouteDataEntry.DataSource = null;
            rideDataEntryForm.cbRouteDataEntry.Items.Clear();
            chartForm.cbRoutesChart.Items.Clear();
            chartForm.cbRoutesChart.DataSource = null;

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbRouteConfig.Items.Remove(oldValue);
                    cbRouteConfig.Items.Add(newValue);
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(newValue);
                    chartForm.cbRoutesChart.Items.Add(newValue);
                }
                else
                {
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(tempList[i]);
                    chartForm.cbRoutesChart.Items.Add(tempList[i]);
                }
            }

            cbRouteConfig.Sorted = true;
            cbRouteConfig.SelectedIndex = selectedIndex;

            //Update the route name in the database for each row:
            //Update value in database:
            SqlDataReader reader = null;

            float returnValue = 0;

            try
            {
                sqlConnection.Open();

                // declare command object with parameter
                SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET Route=@NewValue WHERE [Route]=@OldValue", sqlConnection);

                // setcbStatistic1 parameters
                cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = newValue;
                cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = oldValue;

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

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
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void bRenameLogYear_Click(object sender, EventArgs e)
        {
            string newValue = tbLogYearConfig.Text;
            string oldValue = "";

            if (cbLogYearConfig.SelectedItem != null)
            {
                oldValue = cbLogYearConfig.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Invalid Log Year selected.");
                return;
            }


            if (cbLogYear.SelectedItem == null)
            {
                MessageBox.Show("Invalid Log Year selected.");
                return;
            }

            string logYear = cbLogYear.SelectedItem.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);
            objectValues.Add(oldValue);
            objectValues.Add(logYear);

            runStoredProcedure(objectValues, "Log_Year_Update");

            List<string> tempList = new List<string>();

            int selectedIndex = cbLogYearConfig.SelectedIndex;
            int statIndex1 = cbLogYear1.SelectedIndex;
            int statIndex2 = cbLogYear2.SelectedIndex;
            int statIndex3 = cbLogYear3.SelectedIndex;
            int statIndex4 = cbLogYear4.SelectedIndex;
            int statIndex5 = cbLogYear5.SelectedIndex;

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
            chartForm.cbLogYearChart.Items.Clear();
            chartForm.cbLogYearChart.DataSource = null;
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

            //Set first option of 'None':
            cbLogYear1.Items.Add("--None--");
            cbLogYear2.Items.Add("--None--");
            cbLogYear3.Items.Add("--None--");
            cbLogYear4.Items.Add("--None--");
            cbLogYear5.Items.Add("--None--");

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbLogYearConfig.Items.Add(newValue);
                    rideDataEntryForm.cbLogYearDataEntry.Items.Add(newValue);
                    rideDataDisplayForm.cbLogYearFilter.Items.Add(newValue);
                    chartForm.cbLogYearChart.Items.Add(newValue);
                    cbLogYear1.Items.Add(newValue);
                    cbLogYear2.Items.Add(newValue);
                    cbLogYear3.Items.Add(newValue);
                    cbLogYear4.Items.Add(newValue);
                    cbLogYear5.Items.Add(newValue);
                }
                else
                {
                    cbLogYearConfig.Items.Add(tempList[i]);
                    rideDataEntryForm.cbLogYearDataEntry.Items.Add(tempList[i]);
                    rideDataDisplayForm.cbLogYearFilter.Items.Add(tempList[i]);
                    chartForm.cbLogYearChart.Items.Add(tempList[i]);
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
            chartForm.cbLogYearChart.SelectedIndex = selectedIndex;
            cbLogYear1.SelectedIndex = statIndex1;
            cbLogYear2.SelectedIndex = statIndex2;
            cbLogYear3.SelectedIndex = statIndex3;
            cbLogYear4.SelectedIndex = statIndex4;
            cbLogYear5.SelectedIndex = statIndex5;

            //NOTE: The Table_Ride_Information only contains the LogYearID and not the name:
        }

        //=============================================================================
        //End Statistics Section
        //=============================================================================

        //=============================================================================
        //Start Maintenance Section
        //=============================================================================
        private void button1_Click_1(object sender, EventArgs e)
        {
            //ChartForm chartForm = new ChartForm(this);
            chartForm.Show();
            lbMaintError.Text = "";
        }

        private void btGetMaintLog_Click(object sender, EventArgs e)
        {
            lbMaintError.Text = "";
            //SqlConnection conn = null;

            try
            {
                //conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                //conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""c:\LogFiles\CyclingLogDatabase.mdf"";Integrated Security=True");

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = null;

                sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [Date],[Bike],[Miles],[Comments] FROM Table_Bike_Maintenance", sqlConnection);

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                dgvMaint.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dgvMaint.EnableHeadersVisualStyles = false;
                dgvMaint.DataSource = dataTable;
                dgvMaint.Refresh();
                dgvMaint.Sort(dgvMaint.Columns["Date"], ListSortDirection.Descending);
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to Get Maintenance Log entry." + ex.Message.ToString());
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void btMaintAdd_Click(object sender, EventArgs e)
        {
            lbMaintError.Text = "";

            if (!tbMaintID.Text.Equals(""))
            {
                //MessageBox.Show("The selected date already has an entry. The entry can be updated but not added.");
                lbMaintError.Text = "The selected date and bike already have an entry. Select the Update option.";
                return;
            }

            if (cbBikeMaint.SelectedIndex == -1)
            {
                //MessageBox.Show("All the selections are not set.");
                lbMaintError.Text = "The Bike selection is not set.";
                return;
            }

            if (rtbMaintComments.Text.Equals(""))
            {
                //MessageBox.Show("All the selections are not set.");
                lbMaintError.Text = "The Comments are missing.";
                return;
            }

            //Check format of miles:
            int parsedValue;
            if (!int.TryParse(tbMaintMiles.Text, out parsedValue))
            {
                lbMaintError.Text = "The miles value must be in numeric format. Enter 0 if unknown.";
                return;
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(cbBikeMaint.SelectedItem.ToString());
            objectValues.Add(rtbMaintComments.Text);
            objectValues.Add(dateTimePicker1.Value);
            objectValues.Add(tbMaintMiles.Text);
            runStoredProcedure(objectValues, "Maintenance_Add");

            tbMaintID.Text = "";
            //btGetMaintLog_Click(sender, e);
            tbMaintMiles.Text = "";
            rtbMaintComments.Text = "";
            btGetMaintLog_Click(sender, e);
        }

        //private void dgvMaint_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //btGetMaintLog_Click( sender, e);
        }

        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{

        //}

        private void btMaintUpdate_Click(object sender, EventArgs e)
        {
            lbMaintError.Text = "";

            if (tbMaintID.Equals(""))
            {
                //MessageBox.Show("The selected entry has not been saved yet. Select Add Entry instead of Update.");
                lbMaintError.Text = "The selected entry has not been saved yet. Select Add Entry instead of Update.";
                return;
            }

            if (cbBikeMaint.SelectedIndex == -1)
            {
                //MessageBox.Show("All the selections are not set.");
                lbMaintError.Text = "The Bike selection is not set.";
                return;
            }

            if (rtbMaintComments.Text.Equals(""))
            {
                //MessageBox.Show("All the selections are not set.");
                lbMaintError.Text = "The Comments are missing.";
                return;
            }

            //Check format of miles:
            int parsedValue;
            if (!int.TryParse(tbMaintMiles.Text, out parsedValue))
            {
                lbMaintError.Text = "The miles value must be in numeric format. Enter 0 if unknown.";
                return;
            }

            if (cbBikeMaint.SelectedIndex == -1 || rtbMaintComments.Text.Equals(""))
            {
                //MessageBox.Show("All the selections are not set.");
                lbMaintError.Text = "All the selections are not set.";
                return;
            }

            //Check format of miles:
            //int parsedValue;
            if (!int.TryParse(tbMaintMiles.Text, out parsedValue))
            {
                lbMaintError.Text = "The miles value must be in numeric format.";
                return;
            }

            DialogResult result = MessageBox.Show("Updating the Maintenance entry. Do you want to continue?", "Update Maintenance Entry", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(tbMaintID.Text);
            objectValues.Add(cbBikeMaint.SelectedItem.ToString());
            objectValues.Add(rtbMaintComments.Text);
            objectValues.Add(dateTimePicker1.Value);
            objectValues.Add(tbMaintMiles.Text);
            runStoredProcedure(objectValues, "Maintenance_Update");
            btGetMaintLog_Click(sender, e);
        }

        private void btMaintRemove_Click(object sender, EventArgs e)
        {
            if (tbMaintID.Text.Equals(""))
            {
                // MessageBox.Show("A Maintenance entry must be retrieved before trying to delete it.");
                lbMaintError.Text = "A Maintenance entry must be retrieved before trying to delete it.";
                return;
            }

            DialogResult result = MessageBox.Show("Deleting the Maintenance entry. Do you want to continue?", "Delete Maintenance Entry", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(tbMaintID.Text);
            runStoredProcedure(objectValues, "Maintenance_Remove");
            rtbMaintComments.Text = "";
            tbMaintMiles.Text = "";
            btGetMaintLog_Click(sender, e);
        }

        private void btMaintRetrieve_Click(object sender, EventArgs e)
        {
            lbMaintError.Text = "";

            if (cbBikeMaint.SelectedIndex == -1)
            {
                //MessageBox.Show("A Bike option must be selected before continuing.");
                lbMaintError.Text = "A Bike option must be selected before continuing.";
                return;
            }
            List<object> objectValues = new List<object>();
            objectValues.Add(dateTimePicker1.Text);
            objectValues.Add(cbBikeMaint.SelectedItem.ToString());

            string comments;
            string mainID;
            string miles;

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Maintenance_Get", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            //MessageBox.Show(String.Format("{0}", results[0]));
                            //lbErrorMessage.Hide();
                            comments = results[0].ToString();
                            mainID = results[1].ToString();
                            miles = results[2].ToString();

                            //Load maintenance data page:
                            rtbMaintComments.Text = comments;
                            tbMaintID.Text = mainID;
                            tbMaintMiles.Text = miles;
                        }
                    }
                    else
                    {
                        //lbErrorMessage.Show();
                        //lbErrorMessage.Text = "No ride data found for the selected date.";
                        //tbRecordID.Text = "0";
                        lbMaintError.Text = "No entry found for the selected Bike and Date.";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
            }
        }

        //private void btBikeMilesUpdate_Click(object sender, EventArgs e)
        private void btBikeMilesUpdate_click()
        {
            //Load bike names and notinlogmiles from the database:

            tbBikeMiles1.Text = "";
            tbBikeMiles2.Text = "";
            tbBikeMiles3.Text = "";
            tbBikeMiles4.Text = "";
            tbBikeMiles5.Text = "";

            double bikeMilesAdd = 0;
            double runningTotalMiles = 0;

            try
            {
                List<string> bikeList = new List<string>();
                bikeList = readDataNames("Table_Bikes", "Name");
                Dictionary<string, double> bikeMilesDictionary = new Dictionary<string, double>();

                for (int index = 0; index < bikeList.Count; index++)
                {
                    List<object> objectValues = new List<object>();
                    objectValues.Add(bikeList[index]);
                    double returnValue = 0;

                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("GetTotalMiles_AllLogs_ForABike", objectValues))
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
                                else
                                {
                                    returnValue = Convert.ToDouble(temp);
                                }
                            }
                        }
                    }
                    bikeMilesDictionary.Add(bikeList[index], returnValue);
                }

                //Load textboxes with bikes with the highest values:
                int bikeCount;
                if (bikeList.Count >= 5)
                {
                    bikeCount = 5;
                }
                else
                {
                    bikeCount = bikeList.Count;
                }

                for (int i = 1; i <= bikeCount; i++)
                {
                    double bikeMiles = bikeMilesDictionary.Values.Max();
                    string bikeName = "";
                    double totalMiles;

                    foreach (KeyValuePair<string, double> bike in bikeMilesDictionary)
                    {
                        if (bike.Value == bikeMiles)
                        {
                            bikeName = bike.Key;
                            break;
                        }
                    }

                    bikeMilesDictionary.Remove(bikeName);

                    try
                    {
                        List<object> objectValues = new List<object>();
                        objectValues.Add(bikeName);

                        //ExecuteScalarFunction
                        using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
                        {
                            if (results.HasRows)
                            {
                                while (results.Read())
                                {
                                    string mileNotInLog = results[0].ToString();
                                    if (mileNotInLog.Equals(""))
                                    {
                                        mileNotInLog = "0";
                                    }
                                    bikeMilesAdd = Convert.ToDouble(mileNotInLog);
                                }
                            }
                            else
                            {
                                lbMaintError.Text = "No entry found for the selected Bike and Date.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
                    }

                    if (i == 1)
                    {
                        totalMiles = bikeMiles + bikeMilesAdd;
                        runningTotalMiles += totalMiles;
                        tbBikeMilesTotal1.Text = Convert.ToString(totalMiles);
                        tbBikeMiles1.Text = bikeName;
                    }
                    else if (i == 2)
                    {
                        totalMiles = bikeMiles + bikeMilesAdd;
                        runningTotalMiles += totalMiles;
                        tbBikeMilesTotal2.Text = Convert.ToString(totalMiles);
                        tbBikeMiles2.Text = bikeName;
                    }
                    else if (i == 3)
                    {
                        totalMiles = bikeMiles + bikeMilesAdd;
                        runningTotalMiles += totalMiles;
                        tbBikeMilesTotal3.Text = Convert.ToString(totalMiles);
                        tbBikeMiles3.Text = bikeName;
                    }
                    else if (i == 4)
                    {
                        totalMiles = bikeMiles + bikeMilesAdd;
                        runningTotalMiles += totalMiles;
                        tbBikeMilesTotal4.Text = Convert.ToString(totalMiles);
                        tbBikeMiles4.Text = bikeName;
                    }
                    else if (i == 5)
                    {
                        totalMiles = bikeMiles + bikeMilesAdd;
                        runningTotalMiles += totalMiles;
                        tbBikeMilesTotal5.Text = Convert.ToString(totalMiles);
                        tbBikeMiles5.Text = bikeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive Bike names." + ex.Message.ToString());
            }

            tbBikeMilesTotal.Text = runningTotalMiles.ToString();
        }

        private void cbBikeConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            string miles;
            //Load miles from the database:
            try
            {
                List<object> objectValues = new List<object>();
                objectValues.Add(cbBikeConfig.SelectedItem.ToString());
                tbBikeConfig.Text = cbBikeConfig.SelectedItem.ToString();

                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            miles = results[0].ToString();
                            if (miles.Equals(""))
                            {
                                miles = "0";
                            }
                            tbConfigMilesNotInLog.Text = miles;
                        }
                    }
                    else
                    {
                        lbConfigError.Text = "No entry found for the selected Bike and Date.";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive Bike miles data." + ex.Message.ToString());
            }
        }

        private void btDeleteAllData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This function will delete all data and from the Cycling Log Application and database. Are you sure you want to continue? If you have not made a backup copy of the database, select No, and make a copy and then run the function once again.", "Delete All Data From Database", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            //TODO: Reset entire application:
            //List of things to clean up:
            //Clear out all tables:
            //Delete the config file and a default new one will be created:
        }

        private void btRenameBike_Click(object sender, EventArgs e)
        {
            //Verify Miles is entered and in the correct format:
            int parsedValue;
            string miles = tbConfigMilesNotInLog.Text;

            if (!int.TryParse(miles, out parsedValue))
            {
                lbConfigError.Text = "The miles for the Bike must be in numeric format. Enter 0 if unknown.";
                return;
            }

            string newValue = tbBikeConfig.Text;
            string oldValue = cbBikeConfig.SelectedItem.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);
            objectValues.Add(oldValue);
            objectValues.Add(Convert.ToDouble(miles));

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

            cbBikeMaint.DataSource = null;
            cbBikeMaint.Items.Clear();

            rideDataEntryForm.cbBikeDataEntrySelection.DataSource = null;
            rideDataEntryForm.cbBikeDataEntrySelection.Items.Clear();

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbBikeConfig.Items.Add(newValue);
                    cbBikeMaint.Items.Add(newValue);
                    rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(newValue);
                }
                else
                {
                    cbBikeConfig.Items.Add(tempList[i]);
                    cbBikeMaint.Items.Add(tempList[i]);
                    rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(tempList[i]);
                }
            }

            //Update value in database:
            SqlDataReader reader = null;
            float returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET [Bike]=@NewValue WHERE [Bike]=@OldValue", sqlConnection);

                // 2. define parameters used in command object
                //SqlParameter sqlparams = new SqlParameter();
                //sqlparams.ParameterName = "NewValue";
                //sqlparams.Value = newValue;

                //sqlparams.ParameterName = "OldValue";
                //sqlparams.Value = oldValue;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(sqlparams);

                cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = newValue;
                cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = oldValue;

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

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
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to Rename bike name." + ex.Message.ToString());
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void cbLogYearConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            string year = "";
            List<object> objectValues = new List<object>();
            objectValues.Add(cbLogYearConfig.SelectedItem);
            tbLogYearConfig.Text = cbLogYearConfig.SelectedItem.ToString();

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValues))
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
                        return;
                    }
                }

                cbLogYear.SelectedItem = year;
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
            }
        }


        //=============================================================================
        //End Maintenance Section
        //=============================================================================


        //=============================================================================
        // Start Monthly Statistics Section
        //=============================================================================

        private void runMonthlyStatistics()
        {
            if (cbStatMonthlyLogYear.SelectedItem == null)
            {
                return;
            }

            int logYearIndex = getLogYearIndex(cbStatMonthlyLogYear.SelectedItem.ToString());

            month1R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R1.Text = getTotalMilesMonthlyForSelectedLog(logYearIndex, 12).ToString();

            month1R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R2.Text = getTotalRidesMonthlyForSelectedLog(logYearIndex, 12).ToString();

            month1R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 1).ToString();
            month2R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 2).ToString();
            month3R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 3).ToString();
            month4R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 4).ToString();
            month5R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 5).ToString();
            month6R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 6).ToString();
            month7R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 7).ToString();
            month8R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 8).ToString();
            month9R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 9).ToString();
            month10R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 10).ToString();
            month11R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 11).ToString();
            month12R3.Text = getAvgMonthlyRidesForSelectedLog(logYearIndex, 12).ToString();

            month1R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 1).ToString();
            month2R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 2).ToString();
            month3R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 3).ToString();
            month4R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 4).ToString();
            month5R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 5).ToString();
            month6R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 6).ToString();
            month7R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 7).ToString();
            month8R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 8).ToString();
            month9R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 9).ToString();
            month10R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 10).ToString();
            month11R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 11).ToString();
            month12R4.Text = getAverageMonthlyMilesPerWeek(logYearIndex, 12).ToString();

            month1R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 1).ToString();
            month2R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 2).ToString();
            month3R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 3).ToString();
            month4R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 4).ToString();
            month5R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 5).ToString();
            month6R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 6).ToString();
            month7R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 7).ToString();
            month8R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 8).ToString();
            month9R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 9).ToString();
            month10R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 10).ToString();
            month11R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 11).ToString();
            month12R5.Text = getAverageMonthlyMilesPerRide(logYearIndex, 12).ToString();

            month1R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 1).ToString();
            month2R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 2).ToString();
            month3R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 3).ToString();
            month4R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 4).ToString();
            month5R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 5).ToString();
            month6R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 6).ToString();
            month7R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 7).ToString();
            month8R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 8).ToString();
            month9R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 9).ToString();
            month10R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 10).ToString();
            month11R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 11).ToString();
            month12R6.Text = getMonthlyHighMileageWeekNumber(logYearIndex, 12).ToString();

            month1R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R7.Text = getMaxHighMileageMonthlyForSelectedLog(logYearIndex, 12).ToString();
        }

        private void cbStatMonthlyLogYear_changed(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm("");
            mainForm.setLastMonthlyLogSelected(cbStatMonthlyLogYear.SelectedIndex);
            //if (cbStatMonthlyLogYear.SelectedIndex == -1)
            //{
            //    lbRideDataEntryError.Show();
            //    lbRideDataEntryError.Text = "No Log Year selected.";
            //}
            //else
            //{
            //    lbRideDataEntryError.Hide();
            //}
        }

        //Get total of miles for the selected log:
        //SELECT SUM(RideDistance) FROM Table_Ride_Information;
        private float getTotalMilesMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
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

        //Get total number of rides for the selected log:
        //SELECT Count(LogYearID) FROM Table_Ride_Information;
        private int getTotalRidesMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            int returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("getTotalRides_Monthly", objectValues))
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

        //Get total number of rides for the selected log:
        //SELECT Count(LogYearID) FROM Table_Ride_Information;
        private double getAvgMonthlyRidesForSelectedLog(int logIndex, int month)
        {
            double avgRides = 0;
            int rides = getTotalRidesMonthlyForSelectedLog(logIndex, month);

            //31 days Jan-1, Mar-3, May-5, Jul-7, Aug-8, Oct-10, Dec-12 = 4.4286
            if (month == 1 || month == 3 || month == 5 || month == 8 || month == 10 || month == 12)
            {
                avgRides = rides / 4.4286;
            }
            //30 days Apr-4, Jun-6, Sep-9, Nov-11 = 4.2857
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                avgRides = rides / 4.2857;
            }
            //28/29 days Feb-2 = 4/4.1429
            else
            {
                avgRides = rides / 4;
            }

            return Math.Round(avgRides, 2);
        }

        private double getAverageMonthlyMilesPerWeek(int logIndex, int month)
        {
            double totalMiles = getTotalMilesMonthlyForSelectedLog(logIndex, month);
            double avgMiles = 0;
            //31 days Jan-1, Mar-3, May-5, Jul-7, Aug-8, Oct-10, Dec-12 = 4.4286
            if (month == 1 || month == 3 || month == 5 || month == 8 || month == 10 || month == 12)
            {
                avgMiles = totalMiles / 4.4286;
            }
            //30 days Apr-4, Jun-6, Sep-9, Nov-11 = 4.2857
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                avgMiles = totalMiles / 4.2857;
            }
            //28/29 days Feb-2 = 4/4.1429
            else
            {
                avgMiles = totalMiles / 4;
            }

            return (Math.Round((double)avgMiles, 2));
        }

        //Get average miles per ride value:
        //Total miles/total rides
        private float getAverageMonthlyMilesPerRide(int logIndex, int month)
        {
            float miles = getTotalMilesMonthlyForSelectedLog(logIndex, month);
            int rides = getTotalRidesMonthlyForSelectedLog(logIndex, month);
            float averageMiles = 0;

            if (miles > 0)
            {
                averageMiles = (float)miles / rides;
            }

            //MessageBox.Show(Convert.ToString(averageMiles));
            double avgMiles = Math.Round((double)averageMiles, 2);

            return (float)(avgMiles);
        }

        private double getMonthlyHighMileageWeekNumber_backup(int logIndex, int month)
        {
            List<int> weekList = new List<int>();
            if (month == 1)
            {
                weekList.Add(1);
                weekList.Add(2);
                weekList.Add(3);
                weekList.Add(4);
            }
            else if (month == 2)
            {
                weekList.Add(1);
                weekList.Add(2);
                weekList.Add(3);
                weekList.Add(4);
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);

            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalHighMileageWeek_Monthly", objectValues))
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

        private double getMaxHighMileageMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
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

        public double getMonthlyHighMileageWeekNumber(int LogYearID, int Month)
        {
            List<double> rideDistanceList = new List<double>();
            int weekNumber = 1;
            int weekNumberTmp = 0;
            double weekMilesTotal = 0;
            double weeklyMax = 0;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + LogYearID + "=[LogYearID] and " + Month + "=MONTH([Date])";
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
                                // Compare weekly total to see if max:
                                if (weekMilesTotal > weeklyMax)
                                {
                                    weeklyMax = weekMilesTotal;
                                }

                                // Onto a new week, so reset weekly total:
                                weekMilesTotal = (double)reader["RideDistance"];
                            }
                            else
                            {
                                weekMilesTotal = weekMilesTotal + (double)reader["RideDistance"];
                            }
                        }

                        reader.Close();
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
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }

            // Check last weekly total to see if max:
            if (weekMilesTotal > weeklyMax)
            {
                weeklyMax = weekMilesTotal;
            }

            return weeklyMax;
        }

        public int getTEST(int LogYearID, int Month)
        {
            SqlDataReader reader = null;
            int returnValue = -1;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                // 1. declare command object with parameter
                //SqlCommand cmd = new SqlCommand("SELECT LogYearID FROM Table_Log_Year WHERE @logYearName=[Name]", sqlConnection);
                //SqlCommand cmd = new SqlCommand("SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE @LogYearID=[LogYearID] and @Month=MONTH([Date])", sqlConnection);
                SqlCommand cmd = new SqlCommand("SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + LogYearID + "=[LogYearID]", sqlConnection);

                // 2. define parameters used in command object
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@LogYearID";
                //param.Value = LogYearID;
                //param.ParameterName = "@Month";
                //param.Value = Month;

                // 3. add new parameter to command object
                //cmd.Parameters.Add(param);

                // get data stream
                reader = cmd.ExecuteReader();

                // write each record
                while (reader.Read())
                {
                    //Console.WriteLine("{0}, {1}", reader["field1"], reader["field2"]);
                    //MessageBox.Show(String.Format("{0}", reader[0]));
                    string temp = reader[0].ToString();

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
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to the Log year Index from the database." + ex.Message.ToString());
            }
            finally
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }

            return returnValue;
        }

        //=============================================================================
        // Start Monthly Statistics Section
        //=============================================================================

        private void MonthlyStatistics_Click(object sender, EventArgs e)
        {
            runMonthlyStatistics();
        }

    }
}
