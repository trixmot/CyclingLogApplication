using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Linq;
using System.Diagnostics;



namespace CyclingLogApplication
{
    public partial class MainForm : Form
    {
        private static Mutex mutex = null;
        //private RideDataEntry rideDataEntryForm;
        //private RideDataDisplay rideDataDisplayForm;
        //private ChartForm chartForm;
        Boolean formloading = false;

        private static readonly string logVersion = "0.9.0";
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
        private static string firstDayOfWeek;
        public static string customField1;
        public static string customField2;
        private static string checkedListBoxItem0 = "1";
        private static string checkedListBoxItem1 = "1";
        private static string checkedListBoxItem2 = "1";
        private static string checkedListBoxItem3 = "1";
        private static string checkedListBoxItem4 = "1";
        private static string checkedListBoxItem5 = "1";
        private static string checkedListBoxItem6 = "1";
        private static string checkedListBoxItem7 = "1";
        private static string checkedListBoxItem8 = "1";
        private static string checkedListBoxItem9 = "1";
        private static string checkedListBoxItem10 = "1";
        private static string checkedListBoxItem11 = "1";
        private static string checkedListBoxItem12 = "1";
        private static string checkedListBoxItem13 = "1";
        private static string checkedListBoxItem14 = "1";
        private static string checkedListBoxItem15 = "1";
        private static string checkedListBoxItem16 = "1";
        private static string checkedListBoxItem17 = "1";
        private static string checkedListBoxItem18 = "1";
        private static string checkedListBoxItem19 = "1";
        private static string checkedListBoxItem20 = "1";
        private static string checkedListBoxItem21 = "1";
        private static string checkedListBoxItem22 = "1";
        private static string checkedListBoxItem23 = "1";
        private static string checkedListBoxItem24 = "1";
        private static string checkedListBoxItem25 = "0";
        private static string checkedListBoxItem26 = "0";

        private static SqlConnection sqlConnection;             // = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
        private static DatabaseConnection databaseConnection;   // = new DatabaseConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");

        //connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\workdir\Cycling_Log\CyclingLogApplication\CyclingLogDatabase.mdf;Integrated Security=True"
        //connectionString="Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|CyclingLogDatabase.mdf;Integrated Security=True"

        //[STAThread]
        //public static void Main()
        //{
        //    const string appName = "Cycling Log";
        //    bool createdNew;

        //    mutex = new Mutex(true, appName, out createdNew);

        //    if (!createdNew)
        //    {
        //        //app is already running! Exiting the application
        //        return;
        //    }

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new MainForm());
        //}

        public MainForm()
        {
            //if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) return;
            //const string appName = "Cycling Log";
            //bool createdNew;

            //mutex = new Mutex(true, appName, out createdNew);

            //if (!createdNew)
            //{
            //    //app is already running! Exiting the application
            //    return;
            //}
            //Set DataDirectory for the contectionstring in the app.config:
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());

            //Text = "Single Instance!";
            //mutex = new Mutex(false, "SINGLE_INSTANCE_MUTEX");
            //if (!mutex.WaitOne(0, false))
            //{
            //    mutex.Close();
            //    mutex = null;
            //}

            InitializeComponent();
            GetConnectionStrings();
            int logSetting = GetLogLevel();

            Logger.Log("**********************************************", 1, logSetting);
            Logger.Log("Starting Log Application", 1, 0);

            Logger.Log("**********************************************", 1, logSetting);

            tbWeekCount.Text = GetCurrentWeekCount().ToString();
            tbDayCount.Text = GetCurrentDayCount().ToString();
            tbTimeChange.Text = GetDaysToNextTimeChange().ToString();

            textBox1.Text = "The MIT License\r\nCopyright (c) 2024, John T Flynn\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation\r\nfiles (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify,\r\nmerge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom\r\nthe Software is furnished to do so, subject to the following conditions:\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\nAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\nOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
        }

        public MainForm(string emptyConstructor)
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.ProcessName.Equals(Process.GetCurrentProcess().ProcessName) && proc.Id != Process.GetCurrentProcess().Id)
                    {
                        proc.Kill();
                        //break;
                    }
                }
                this.Dispose();
                Application.Exit();
            }
            //Empty consturctor to prevent from running InitializeComponent():
            //Curretnly not used:
            string calledFrom = emptyConstructor;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1) return;

            //if (GetMutex() != null)
            //{
            //    //Application.Run(app);
            //}
            //else
            //{
            //    Logger.Log("Instance of Cycling Log Application is already running", 1, logLevel);
            //    System.Environment.Exit(0);
            //}

            try
            {
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                ConfigurationFile configfile = new ConfigurationFile();
                configfile.readConfigFile();
                int logSetting = GetLogLevel();

                lbVersion.Text = "App Version: " + GetLogVersion();
                lbMaintError.Text = "";

                string firstDay = GetFirstDayOfWeek();
                if (firstDay.Equals("Sunday"))
                {
                    rbFirstDayMonday.Checked = false;
                    rbFirstDaySunday.Checked = true;
                }
                else
                {
                    rbFirstDayMonday.Checked = true;
                    rbFirstDaySunday.Checked = false;
                }

                //Get all values and load the comboboxes:
                List<string> logYearList = ReadDataNames("Table_Log_year", "Name");
                List<string> routeList = ReadDataNames("Table_Routes", "Name");
                List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

                RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm(this);

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

                if (GetLastLogSelectedDataEntry() != 0)
                {
                    rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = GetLastLogSelectedDataEntry();
                }

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
                    tabControl1.SelectedTab = tabControl1.TabPages["Settings"];
                    return;
                }

                if (routeList.Count == 0)
                {
                    MessageBox.Show("No Routes have been created yet.");
                    tabControl1.SelectedTab = tabControl1.TabPages["Routes"];
                    return;
                }

                if (bikeList.Count == 0)
                {
                    MessageBox.Show("No Bikes have been added yet. Please add an entry for a Bike.");
                    tabControl1.SelectedTab = tabControl1.TabPages["Bikes"];
                    return;
                }

                GetMaintLog();

                formloading = true;
                //Set first option of 'None':
                cbStatMonthlyLogYear.Items.Add("--None--");


                //Load LogYear Monthly values:
                foreach (string val in logYearList)
                {
                    cbStatMonthlyLogYear.Items.Add(val);
                }

                //Load Statistic combo index values:
                cbLogYear1.SelectedIndex = Convert.ToInt32(GetcbStatistic1());
                cbLogYear2.SelectedIndex = Convert.ToInt32(GetcbStatistic2());
                cbLogYear3.SelectedIndex = Convert.ToInt32(GetcbStatistic3());
                cbLogYear4.SelectedIndex = Convert.ToInt32(GetcbStatistic4());
                cbLogYear5.SelectedIndex = Convert.ToInt32(GetcbStatistic5());

                cbStatMonthlyLogYear.SelectedIndex = Convert.ToInt32(GetLastMonthlyLogSelected());

                RefreshStatisticsData();
                RunMonthlyStatistics();
                refreshWeekly();
                refreshRoutes();
                refreshBikes();

                tbCustomDataField1.Text = GetCustomField1();
                tbCustomDataField2.Text = GetCustomField2();
                //tabControl1.SelectedTab = tabControl1.TabPages["Main"];
                formloading = false;

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to load form." + ex.Message.ToString());
            }

        }

        private void CloseForm(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm(this);
                chartForm.Close();
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                ConfigurationFile configurationFile = new ConfigurationFile();
                configurationFile.writeConfigFile();
                rideDataDisplayForm.Dispose();
                chartForm.Dispose();
                rideDataEntryForm.Dispose();
                this.Dispose();
                Application.Exit();
            }
        }

        static void GetConnectionStrings()
        {
            string conStr = ConfigurationManager.ConnectionStrings["CyclingLogApplication.Properties.Settings.CyclingLogDatabaseConnectionString"].ConnectionString;
            Logger.Log("**********************************************", 1, 1);
            Logger.Log("Ending Log Application", 1, 1);
            Logger.Log("**********************************************", 1, 1);
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

        public SqlConnection GetsqlConnectionString()
        {
            return sqlConnection;
        }

        public DatabaseConnection GetsDatabaseConnectionString()
        {
            return databaseConnection;
        }

        private Mutex GetMutex()
        {
            return mutex;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //        mutex.ReleaseMutex();
        //    base.Dispose(disposing);
        //}

        public string GetLogVersion()
        {
            return logVersion;
        }

        public int GetLogLevel()
        {
            return logLevel;
        }

        public void SetLogLevel(int logLevelFromConfig)
        {
            logLevel = logLevelFromConfig;
        }
        public string GetcbStatistic1()
        {
            return cbStatistic1;
        }
        public string GetcbStatistic2()
        {
            return cbStatistic2;
        }
        public string GetcbStatistic3()
        {
            return cbStatistic3;
        }
        public string GetcbStatistic4()
        {
            return cbStatistic4;
        }
        public string GetcbStatistic5()
        {
            return cbStatistic5;
        }

        public void SetcbStatistic1(string setcbStatistic1Config)
        {
            cbStatistic1 = setcbStatistic1Config;
        }
        public void SetcbStatistic2(string setcbStatistic2Config)
        {
            cbStatistic2 = setcbStatistic2Config;
        }
        public void SetcbStatistic3(string setcbStatistic3Config)
        {
            cbStatistic3 = setcbStatistic3Config;
        }
        public void SetcbStatistic4(string setcbStatistic4Config)
        {
            cbStatistic4 = setcbStatistic4Config;
        }
        public void SetcbStatistic5(string setcbStatistic5Config)
        {
            cbStatistic5 = setcbStatistic5Config;
        }

        public int GetLastBikeSelected()
        {
            return lastBikeSelected;
        }

        public void SetLastBikeSelected(int bikeIndex)
        {
            lastBikeSelected = bikeIndex;
        }

        public int GetLastLogSelected()
        {
            return lastLogSelected;
        }

        public void SetLastLogSelected(int logIndex)
        {
            lastLogSelected = logIndex;
        }

        public int GetLastMonthlyLogSelected()
        {
            return lastMonthlyLogSelected;
        }

        public void SetLastMonthlyLogSelected(int logIndex)
        {
            lastMonthlyLogSelected = logIndex;
        }

        public int GetLastLogSelectedDataEntry()
        {
            return lastLogSelectedDataEntry;
        }

        public void SetLastLogSelectedDataEntry(int logIndex)
        {
            lastLogSelectedDataEntry = logIndex;
        }

        public void SetLastLogFilterSelected(int logIndex)
        {
            lastLogFilterSelected = logIndex;
        }

        public int GetLastLogFilterSelected()
        {
            return lastLogFilterSelected;
        }

        public void SetLastLogYearChartSelected(int logIndex)
        {
            lastLogYearChart = logIndex;
        }

        public static int GetLastLogYearChartSelected()
        {
            return lastLogYearChart;
        }

        public void SetLastRouteChartSelected(int logIndex)
        {
            lastRouteChart = logIndex;
        }

        public static int GetLastRouteChartSelected()
        {
            return lastRouteChart;
        }

        public void SetLastTypeChartSelected(int logIndex)
        {
            lastTypeChart = logIndex;
        }

        public static int GetLastTypeChartSelected()
        {
            return lastTypeChart;
        }

        public void SetLastTypeTimeChartSelected(int logIndex)
        {
            lastTypeTimeChart = logIndex;
        }

        public static int GetLastTypeTimeChartSelected()
        {
            return lastTypeTimeChart;
        }

        public string GetFirstDayOfWeek()
        {
            return firstDayOfWeek;
        }

        public static string GetCustomField1()
        {
            return customField1;
        }

        public static string GetCustomField2()
        {
            return customField2;
        }

        public void SetFirstDayOfWeek(string firstdayString)
        {
            firstDayOfWeek = firstdayString;
        }

        public void SetCustomField1(string customDataField1)
        {
            customField1 = customDataField1;
        }

        public void SetCustomField2(string customDataField2)
        {
            customField2 = customDataField2;
        }

        public void SetCheckedListBoxItem0(string checkedItem0)
        {
            checkedListBoxItem0 = checkedItem0;
        }

        public void SetCheckedListBoxItem1(string checkedItem1)
        {
            checkedListBoxItem1 = checkedItem1;
        }

        public void SetCheckedListBoxItem2(string checkedItem2)
        {
            checkedListBoxItem2 = checkedItem2;
        }

        public void SetCheckedListBoxItem3(string checkedItem3)
        {
            checkedListBoxItem3 = checkedItem3;
        }

        public void SetCheckedListBoxItem4(string checkedItem4)
        {
            checkedListBoxItem4 = checkedItem4;
        }

        public void SetCheckedListBoxItem5(string checkedItem5)
        {
            checkedListBoxItem5 = checkedItem5;
        }

        public void SetCheckedListBoxItem6(string checkedItem6)
        {
            checkedListBoxItem6 = checkedItem6;
        }

        public void SetCheckedListBoxItem7(string checkedItem7)
        {
            checkedListBoxItem7 = checkedItem7;
        }

        public void SetCheckedListBoxItem8(string checkedItem8)
        {
            checkedListBoxItem8 = checkedItem8;
        }

        public void SetCheckedListBoxItem9(string checkedItem9)
        {
            checkedListBoxItem9 = checkedItem9;
        }

        public void SetCheckedListBoxItem10(string checkedItem10)
        {
            checkedListBoxItem10 = checkedItem10;
        }

        public void SetCheckedListBoxItem11(string checkedItem11)
        {
            checkedListBoxItem11 = checkedItem11;
        }

        public void SetCheckedListBoxItem12(string checkedItem12)
        {
            checkedListBoxItem12 = checkedItem12;
        }

        public void SetCheckedListBoxItem13(string checkedItem13)
        {
            checkedListBoxItem13 = checkedItem13;
        }

        public void SetCheckedListBoxItem14(string checkedItem14)
        {
            checkedListBoxItem14 = checkedItem14;
        }

        public void SetCheckedListBoxItem15(string checkedItem15)
        {
            checkedListBoxItem15 = checkedItem15;
        }

        public void SetCheckedListBoxItem16(string checkedItem16)
        {
            checkedListBoxItem16 = checkedItem16;
        }

        public void SetCheckedListBoxItem17(string checkedItem17)
        {
            checkedListBoxItem17 = checkedItem17;
        }

        public void SetCheckedListBoxItem18(string checkedItem18)
        {
            checkedListBoxItem18 = checkedItem18;
        }

        public void SetCheckedListBoxItem19(string checkedItem19)
        {
            checkedListBoxItem19 = checkedItem19;
        }

        public void SetCheckedListBoxItem20(string checkedItem20)
        {
            checkedListBoxItem20 = checkedItem20;
        }

        public void SetCheckedListBoxItem21(string checkedItem21)
        {
            checkedListBoxItem21 = checkedItem21;
        }

        public void SetCheckedListBoxItem22(string checkedItem22)
        {
            checkedListBoxItem22 = checkedItem22;
        }

        public void SetCheckedListBoxItem23(string checkedItem23)
        {
            checkedListBoxItem23 = checkedItem23;
        }

        public void SetCheckedListBoxItem24(string checkedItem24)
        {
            checkedListBoxItem24 = checkedItem24;
        }

        public void SetCheckedListBoxItem25(string checkedItem25)
        {
            checkedListBoxItem25 = checkedItem25;
        }

        public void SetCheckedListBoxItem26(string checkedItem26)
        {
            checkedListBoxItem26 = checkedItem26;
        }

        public string GetCheckedListBoxItem0()
        {
            return checkedListBoxItem0;
        }

        public string GetCheckedListBoxItem1()
        {
            return checkedListBoxItem1;
        }

        public string GetCheckedListBoxItem2()
        {
            return checkedListBoxItem2;
        }

        public string GetCheckedListBoxItem3()
        {
            return checkedListBoxItem3;
        }

        public string GetCheckedListBoxItem4()
        {
            return checkedListBoxItem4;
        }

        public string GetCheckedListBoxItem5()
        {
            return checkedListBoxItem5;
        }

        public string GetCheckedListBoxItem6()
        {
            return checkedListBoxItem6;
        }

        public string GetCheckedListBoxItem7()
        {
            return checkedListBoxItem7;
        }

        public string GetCheckedListBoxItem8()
        {
            return checkedListBoxItem8;
        }

        public string GetCheckedListBoxItem9()
        {
            return checkedListBoxItem9;
        }

        public string GetCheckedListBoxItem10()
        {
            return checkedListBoxItem10;
        }

        public string GetCheckedListBoxItem11()
        {
            return checkedListBoxItem11;
        }

        public string GetCheckedListBoxItem12()
        {
            return checkedListBoxItem12;
        }

        public string GetCheckedListBoxItem13()
        {
            return checkedListBoxItem13;
        }

        public string GetCheckedListBoxItem14()
        {
            return checkedListBoxItem14;
        }

        public string GetCheckedListBoxItem15()
        {
            return checkedListBoxItem15;
        }

        public string GetCheckedListBoxItem16()
        {
            return checkedListBoxItem16;
        }

        public string GetCheckedListBoxItem17()
        {
            return checkedListBoxItem17;
        }

        public string GetCheckedListBoxItem18()
        {
            return checkedListBoxItem18;
        }

        public string GetCheckedListBoxItem19()
        {
            return checkedListBoxItem19;
        }

        public string GetCheckedListBoxItem20()
        {
            return checkedListBoxItem20;
        }

        public string GetCheckedListBoxItem21()
        {
            return checkedListBoxItem21;
        }

        public string GetCheckedListBoxItem22()
        {
            return checkedListBoxItem22;
        }

        public string GetCheckedListBoxItem23()
        {
            return checkedListBoxItem23;
        }

        public string GetCheckedListBoxItem24()
        {
            return checkedListBoxItem24;
        }

        public string GetCheckedListBoxItem25()
        {
            return checkedListBoxItem25;
        }

        public string GetCheckedListBoxItem26()
        {
            return checkedListBoxItem26;
        }

        public static List<string> GetLogYears()
        {
            MainForm mainform = new MainForm();
            List<string> logYearsList = mainform.ReadDataNames("Table_Log_year", "Name");

            for (int i = 0; i < mainform.cbLogYearConfig.Items.Count; i++)
            {
                logYearsList.Add(logYearsList[i]);
            }

            return logYearsList;
        }

        public static List<string> GetRoutes()
        {
            MainForm mainform = new MainForm();
            List<string> routeList = mainform.ReadDataNames("Table_Routes", "Name");

            for (int i = 0; i < mainform.cbRouteConfig.Items.Count; i++)
            {
                routeList.Add(routeList[i]);
            }

            return routeList;
        }

        //Disable x close option:
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

        public List<string> ReadDataNames(string tableName, string columnName)
        {
            SqlDataReader reader = null;
            List<string> nameList = new List<string>();
            int logSetting = GetLogLevel();

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("SELECT " + columnName + " FROM " + tableName + " ORDER BY " + columnName + " ASC", sqlConnection))
                {
                    // 2. define parameters used in command object
                    //SqlParameter param = new SqlParameter();
                    //param.ParameterName = "@Id";
                    //param.Value = inputValue;

                    // 3. add new parameter to command object
                    //cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                }

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

        private void BtAddLogYearConfig(object sender, EventArgs e)
        {
            string logYearTitle;
            RideDataEntry rideDataEntryForm = new RideDataEntry();

            if (tbLogYearConfig.Text != "")
            {
                logYearTitle = tbLogYearConfig.Text;
                //Check to see if the string has already been entered to eliminate duplicates:
                for (int index = 1; index < cbLogYearConfig.Items.Count; index++)
                {
                    if (cbLogYearConfig.Items.Contains(logYearTitle))
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
            int logSetting = GetLogLevel();

            //Add new entry to the LogYear Table:
            List<object> objectValues = new List<object>();
            objectValues.Add(logYearTitle);
            objectValues.Add(Convert.ToInt32(logYearValue));
            RunStoredProcedure(objectValues, "Log_Year_Add");
            RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
            ChartForm chartForm = new ChartForm(this);
            cbLogYearConfig.Items.Add(logYearTitle);
            cbLogYearConfig.SelectedIndex = cbLogYearConfig.Items.Count - 1;
            rideDataEntryForm.AddLogYearDataEntry(logYearTitle);
            rideDataDisplayForm.AddLogYearFilter(logYearTitle);
            chartForm.cbLogYearChart.Items.Add(logYearTitle);
            cbStatMonthlyLogYear.Items.Add(logYearTitle);

            //Update combo's on stat tab:
            cbLogYear1.Items.Add(logYearTitle);
            cbLogYear2.Items.Add(logYearTitle);
            cbLogYear3.Items.Add(logYearTitle);
            cbLogYear4.Items.Add(logYearTitle);
            cbLogYear5.Items.Add(logYearTitle);

            refreshWeekly();

            Logger.Log("Adding a Log Year entry to the Configuration:" + logYearTitle, 0, logSetting);
        }

        private void RemoveLogYearConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Log and all its data?", "Delete Log", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                string logName = cbLogYearConfig.SelectedItem.ToString();
                int logYearIndex = GetLogYearIndex(logName);
                RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm(this);
                int chartIndexCount = chartForm.cbLogYearChart.Items.Count;
                SetLastLogYearChartSelected(chartIndexCount - 2);

                cbLogYearConfig.Items.Remove(logName);
                rideDataEntryForm.cbLogYearDataEntry.Items.Remove(logName);
                rideDataDisplayForm.cbLogYearFilter.Items.Remove(logName);
                chartForm.cbLogYearChart.Items.Remove(logName);
                cbStatMonthlyLogYear.Items.Remove(logName);


                cbLogYear1.Items.Remove(logName);
                cbLogYear2.Items.Remove(logName);
                cbLogYear3.Items.Remove(logName);
                cbLogYear4.Items.Remove(logName);
                cbLogYear5.Items.Remove(logName);

                //Remove logyear from the Log year table:
                List<object> objectValues = new List<object>();
                objectValues.Add(logYearIndex);
                RunStoredProcedure(objectValues, "Log_Year_Remove");

                //Need to remove all data for this log from the database:
                RemoveLogYearData(logYearIndex);
                cbLogYearConfig.Text = "";
            }
        }

        public int GetLogYearIndex(string logYearName)
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
                using (SqlCommand cmd = new SqlCommand("SELECT LogYearID FROM Table_Log_Year WHERE @logYearName=[Name]", sqlConnection))
                {

                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@logYearName",
                        Value = logYearName
                    };

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                }

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

        public int GetLogYearByIndex(int logYearIndex)
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
                using (SqlCommand cmd = new SqlCommand("SELECT Year FROM Table_Log_Year WHERE @logYearIndex=[LogyearID]", sqlConnection))
                {
                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@logYearIndex",
                        Value = logYearIndex
                    };

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                }

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
                Logger.LogError("[ERROR]: Exception while trying to the Log year value by Index from the database." + ex.Message.ToString());
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

        private int RunStoredProcedure(List<object> objectValues, string procedureName)
        {
            int ToReturn = -1;

            using (var results = ExecuteSimpleQueryConnection(procedureName, objectValues))
            {
                if (results.HasRows)
                    while (results.Read())
                        ToReturn = (int)results[0];
            }

            return ToReturn;
        }

        private void RemoveLogYearData(int logIndex)
        {
            SqlDataReader reader = null;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection))
                {
                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = logIndex
                    };

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

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

        private void OpenRideDataForm(object sender, EventArgs e)
        {
            RideDataDisplay rideDataDisplayForm = new RideDataDisplay();

            rideDataDisplayForm.setCustomValues();
            rideDataDisplayForm.setLogYearFilterIndex(GetLastLogFilterSelected());
            rideDataDisplayForm.setCheckedValues();
            rideDataDisplayForm.ShowDialog();
        }

        private void OpenRideDataEntry(object sender, EventArgs e)
        {
            //Need to check that there is a least 1 LogYear value entered:
            if (cbLogYearConfig.Items.Count == 0)
            {
                MessageBox.Show("You must add at least 1 log Entry before entering data.  Add a new Log Year entry in the Settings tab.");
            }
            else
            {
                if (cbRouteConfig.Items.Count == 0)
                {
                    //Give a warning if no additional routed have been entered:
                    MessageBox.Show("Reminder: No Routes have been entered. Add a new Route in the Settings tab.");
                }
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                rideDataEntryForm.cbBikeDataEntrySelection.SelectedIndex = Convert.ToInt32(GetLastBikeSelected());
                rideDataEntryForm.ShowDialog();
            }

            //thread = new Thread(openRideDataEntryForm);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        private void BtAddRoute_Click(object sender, EventArgs e)
        {
            string routeString = tbRouteConfig.Text;
            int logSetting = GetLogLevel();

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
            RunStoredProcedure(objectValues, "Route_Add");

            cbRouteConfig.Items.Add(routeString);
            cbRouteConfig.SelectedIndex = cbRouteConfig.Items.Count - 1;
            RideDataEntry rideDataEntryForm = new RideDataEntry();
            rideDataEntryForm.AddRouteDataEntry(routeString);
            ChartForm chartForm = new ChartForm(this);
            chartForm.cbRoutesChart.Items.Add(routeString);

            refreshRoutes();
            Logger.Log("Adding a Route entry to the Configuration:" + routeString, 0, logSetting);
        }

        private void addDefautRoute()
        {
            string routeString = "Miscellaneous Route";
            int logSetting = GetLogLevel();

            List<object> objectValues = new List<object>();
            objectValues.Add(routeString);
            RunStoredProcedure(objectValues, "Route_Add");

            RideDataEntry rideDataEntryForm = new RideDataEntry();
            cbRouteConfig.Items.Add(routeString);
            cbRouteConfig.SelectedIndex = cbRouteConfig.Items.Count - 1;
            rideDataEntryForm.AddRouteDataEntry(routeString);

            ChartForm chartForm = new ChartForm(this);
            chartForm.cbRoutesChart.Items.Add(routeString);

            refreshRoutes();
            Logger.Log("Adding a Route entry to the Configuration:" + routeString, 0, logSetting);
        }

        private void BtRemoveRouteConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Route option?", "Delete Route Option", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string deleteValue = cbRouteConfig.SelectedItem.ToString();
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                //Note: only removing value as an option, all records using this value are unchanged:
                cbRouteConfig.Items.Remove(deleteValue);
                rideDataEntryForm.RemoveRouteDataEntry(deleteValue);

                ChartForm chartForm = new ChartForm(this);
                chartForm.cbRoutesChart.Items.Remove(deleteValue);

                //Remove the Route from the database table:
                List<object> objectValues = new List<object>();
                objectValues.Add(deleteValue);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Route_Remove", objectValues))
                    {

                    }

                    refreshRoutes();
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete the Route name entry." + ex.Message.ToString());
                }
            }
        }

        private void BtAddBikeConfig_Click(object sender, EventArgs e)
        {
            string bikeString = tbBikeConfig.Text;
            string miles = tbConfigMilesNotInLog.Text;
            Boolean updateBikeTotals = true;
            RideDataEntry rideDataEntryForm = new RideDataEntry();

            //Check to see if the string has already been entered to eliminate duplicates:
            if (cbBikeConfig.Items.Contains(bikeString))
            {
                MessageBox.Show("Duplicate name entered. Enter a unique name for the bike.");
                return;
            }

            //Verify Miles is entered and in the correct format:
            if (!int.TryParse(tbConfigMilesNotInLog.Text, out _))
            {
                MessageBox.Show("The miles for the Bike must be in numeric format. Enter 0 if unknown.");
                return;
            }

            List<object> objectBikes = new List<object>();
            objectBikes.Add(bikeString);
            RunStoredProcedure(objectBikes, "Bike_Add");

            cbBikeConfig.Items.Add(bikeString);
            cbBikeMaint.Items.Add(tbConfigMilesNotInLog.Text);
            cbBikeConfig.SelectedIndex = cbBikeConfig.Items.Count - 1;
            rideDataEntryForm.AddBikeDataEntry(bikeString);

            if (updateBikeTotals)
            {
                List<object> objectBikesTotals = new List<object>();
                objectBikesTotals.Add(bikeString);
                objectBikesTotals.Add(miles);
                RunStoredProcedure(objectBikesTotals, "Bike_Totals_Add");
            }

            refreshBikes();
        }

        private void BtRemoveBikeConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the bike option?", "Delete Bike Option", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string deleteValue = cbBikeConfig.SelectedItem.ToString();
                RideDataEntry rideDataEntryForm = new RideDataEntry();

                //Note: only removing value as an option, all records using this value are unchanged:
                cbBikeConfig.Items.Remove(deleteValue);
                cbBikeMaint.Items.Remove(deleteValue);
                rideDataEntryForm.RemoveBikeDataEntry(deleteValue);

                //Clear entires:
                tbConfigMilesNotInLog.Text = "0";
                tbBikeConfig.Text = "";

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

                //Clear entires:
                tbConfigMilesNotInLog.Text = "0";
                tbBikeConfig.Text = "";

                //Remove the Bike from the database table:
                List<object> objectValues2 = new List<object>();
                objectValues.Add(deleteValue);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Bike_Remove_Totals", objectValues2))
                    {

                    }

                    refreshBikes();
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete Bike name entry." + ex.Message.ToString());
                }
            }
        }

        private void CbRouteConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRouteConfig.SelectedItem != null)
            {
                tbRouteConfig.Text = cbRouteConfig.SelectedItem.ToString();
            }
        }


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
        private string GetTotalMilesForSelectedLog(int logIndex)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            string returnValue = "0";
            double miles = 0;

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
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            miles = double.Parse(returnValue);
            returnValue = miles.ToString("N0");

            return returnValue;
        }

        private string GetElevGain_Yearly(int logIndex)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            string returnValue = "0";
            int elevgain = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetElevGainYearly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        private float GetTotalMilesForAllLogs()
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

        private int GetMostElevationAllLogs()
        {
            List<object> objectValues = new List<object>();
            int returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMostElevation_AllLogs", objectValues))
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

        private string GetLongestRideTimeAllLogs()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "0";

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetLongestTime_AllLogs", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            return returnValue;
        }

        private string GetTotalElevGainForAllLogs()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "0";
            int elevgain = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalElevGain_AllLogs", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        //Get total number of rides for the selected log:
        //SELECT Count(LogYearID) FROM Table_Ride_Information;
        private int GetTotalRidesForSelectedLog(int logIndex)
        {
            SqlDataReader reader = null;
            int returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("select COUNT(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection))
                {
                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = logIndex
                    };

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                }

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
        private float GetAverageRidesPerWeek(int logIndex)
        {
            int rides = GetTotalRidesForSelectedLog(logIndex);
            int weekValue;
            float avgRides = 0;

            //Need to determine if the current year matches the log year and if not use 52 as the weekValue:
            int logYear = GetLogYearByIndex(logIndex);

            if (logYear == DateTime.Today.Year)
            {
                weekValue = GetCurrentWeekCount();
            }
            else
            {
                weekValue = 52;
            }

            if (rides > 0)
            {
                avgRides = (float)rides / weekValue;
            }
            //MessageBox.Show(Convert.ToString(avgRides));

            return (float)(Math.Round((double)avgRides, 2));
        }

        //Get average miles per week value:
        //Total miles/weeks
        private float GetAverageMilesPerWeek(int logIndex)
        {
            float totalMiles = float.Parse(GetTotalMilesForSelectedLog(logIndex));
            float avgMiles = 0;
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue;

            //Need to determine if the current year matches the log year and if not use 52 (full year) as the weekValue:
            int logYear = GetLogYearByIndex(logIndex);

            if (logYear == DateTime.Today.Year)
            {
                weekValue = GetCurrentWeekCount();
            }
            else
            {
                weekValue = 52;
            }

            if (totalMiles > 0)
            {
                avgMiles = (float)totalMiles / weekValue;
            }
            //MessageBox.Show(Convert.ToString(totalMiles));

            return (float)(Math.Round((double)avgMiles, 2));
        }

        //Get average miles per ride value:
        //Total miles/total rides
        private float GetAverageMilesPerRide(int logIndex)
        {
            float miles = float.Parse(GetTotalMilesForSelectedLog(logIndex));
            int rides = GetTotalRidesForSelectedLog(logIndex);
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
        private double GetHighMileageWeekNumber(int logIndex)
        {
            int weekNumber;
            int weekNumberTmp = 0;
            double weekMilesTotal = 0;
            double weeklyMax = 0;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + logIndex + "=[LogYearID] ORDER BY [WeekNumber] ASC";
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
                                weekMilesTotal += (double)reader["RideDistance"];
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
        private float GetHighMileageDay(int logIndex)
        {
            SqlDataReader reader = null;
            float returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("select MAX(RideDistance) from Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection))
                {
                    // 2. define parameters used in command object
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = logIndex
                    };

                    // 3. add new parameter to command object
                    cmd.Parameters.Add(param);

                    // get data stream
                    reader = cmd.ExecuteReader();
                }

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

        private double GetDaysToNextTimeChange()
        {
            DateTime date = DateTime.Now;
            double year = date.Year;
            //int month = date.Month;
            //int day = date.Day;
            double dayCount;
            //DateTime changeDate = new DateTime(moment.Year, moment.Month, moment.Day);
            //Days to time change -  (DateTime.Now - DateTime(Int32 year, Int32 month, Int32 day)).TotalDays {type DateTime}

            //2016  Sun, Mar 13 -,Sun, Nov 6
            //2017	Sun, Mar 12 - Sun, Nov 5,
            //2018	Sun, Mar 11 - Sun, Nov 4
            //2019	Sun, Mar 10 - Sun, Nov 3,

            //Check year
            //check if before or after March
            if (year == 2024)
            {
                DateTime changeDate = new DateTime(2024, 3, 10);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2024, 11, 3);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2025, 3, 9);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2025)
            {
                DateTime changeDate = new DateTime(2025, 3, 9);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2025, 11, 2);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2026, 3, 8);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2026)
            {
                DateTime changeDate = new DateTime(2026, 3, 8);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2026, 11, 1);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2027, 3, 14);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2027)
            {
                DateTime changeDate = new DateTime(2027, 3, 14);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2027, 11, 7);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2028, 3, 12);
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

            /*Year    DST Begins  DST Ends        Year    DST Begins  DST Ends
              2014    March 9     November 2      2024    March 10    November 3
              2015    March 8     November 1      2025    March 9     November 2
              2016    March 13    November 6      2026    March 8     November 1
              2017    March 12    November 5      2027    March 14    November 7
              2018    March 11    November 4      2028    March 12    November 5
              2019    March 10    November 3      2029    March 11    November 4
              2020    March 8     November 1      2030    March 10    November 3
              2021    March 14    November 7      2031    March 9     November 2
              2022    March 13    November 6      2032    March 14    November 7
              2023    March 12    November 5      2033    March 13    November 6 */

            double result = Math.Ceiling(dayCount);
            return result;
        }

        private int GetCurrentWeekCount()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = 0;
            string firstDay = GetFirstDayOfWeek();

            if (firstDay == null)
            {
                firstDay = "Monday";

            }

            if (firstDay.Equals("Sunday"))
            {
                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Sunday);
            }
            else
            {
                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Monday);
            }

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
            int logYearIndex;

            // Get log index and pass to all the methods:
            if (cbLogYear1.SelectedItem == null)
            {
                logYearIndex = 0;
            }
            else
            {
                logYearIndex = GetLogYearIndex(cbLogYear1.SelectedItem.ToString());
            }

            if (cbLogYear1.SelectedIndex > 0)
            {
                tb1Log1.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly1.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly1.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
            }

            if (cbLogYear2.SelectedIndex > 0)
            {
                logYearIndex = GetLogYearIndex(cbLogYear2.SelectedItem.ToString());

                tb1Log2.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log2.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log2.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log2.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log2.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log2.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log2.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly2.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly2.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
            }

            if (cbLogYear3.SelectedIndex > 0)
            {
                logYearIndex = GetLogYearIndex(cbLogYear3.SelectedItem.ToString());

                tb1Log3.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log3.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log3.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log3.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log3.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log3.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log3.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly3.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly3.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
            }

            if (cbLogYear4.SelectedIndex > 0)
            {
                logYearIndex = GetLogYearIndex(cbLogYear4.SelectedItem.ToString());

                tb1Log4.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log4.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log4.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log4.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log4.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log4.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log4.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly4.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly4.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
            }

            if (cbLogYear5.SelectedIndex > 0)
            {
                logYearIndex = GetLogYearIndex(cbLogYear5.SelectedItem.ToString());

                tb1Log5.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log5.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log5.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log5.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log5.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log5.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log5.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly5.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly5.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
            }

            //Get total miles for all logs:
            double totalMiles = GetTotalMilesForAllLogs();
            totalMiles = Math.Round(totalMiles, 1);
            tbStatisticsTotalMiles.Text = totalMiles.ToString("N0");
            tbLongestRide.Text = Convert.ToString(GetLongestRide());
            tbTotalRides.Text = Convert.ToString(GetTotalRides());
            tbTotalElevGain.Text = Convert.ToString(GetTotalElevGainForAllLogs());
            tbTotalTime.Text = Convert.ToString(GetTotalMovingTimeAllLogs());
            tbMostElevationAll.Text = GetMostElevationAllLogs().ToString("N0");
            tbLongestTimeAll.Text = GetLongestRideTimeAllLogs();
        }

        private double GetLongestRide()
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

        private double GetFastestAvg()
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

        private double GetMaxSpeed()
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

        private string GetTotalRides()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "0";
            int elevgain = 0;

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
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        private void Cb1LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex(cbLogYear1.SelectedItem.ToString());
            SetcbStatistic1(cbLogYear1.SelectedIndex.ToString());

            if (cbLogYear1.SelectedIndex > 0)
            {
                tb1Log1.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log1.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log1.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log1.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log1.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log1.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log1.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly1.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly1.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
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
                tbElevGainYearly1.Text = "";
                tbTimeYearly1.Text = "";
            }
        }

        private void Cb2LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex(cbLogYear2.SelectedItem.ToString());
            SetcbStatistic2(cbLogYear2.SelectedIndex.ToString());

            if (cbLogYear2.SelectedIndex > 0)
            {
                tb1Log2.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log2.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log2.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log2.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log2.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log2.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log2.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly2.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly2.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
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
                tbElevGainYearly2.Text = "";
                tbTimeYearly2.Text = "";
            }
        }

        private void Cb3LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex(cbLogYear3.SelectedItem.ToString());
            SetcbStatistic3(cbLogYear3.SelectedIndex.ToString());

            if (cbLogYear3.SelectedIndex > 0)
            {
                tb1Log3.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log3.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log3.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log3.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log3.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log3.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log3.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly3.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly3.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
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
                tbElevGainYearly3.Text = "";
                tbTimeYearly3.Text = "";
            }
        }

        private void Cb4LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex(cbLogYear4.SelectedItem.ToString());
            SetcbStatistic4(cbLogYear4.SelectedIndex.ToString());

            if (cbLogYear4.SelectedIndex > 0)
            {
                tb1Log4.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log4.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log4.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log4.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log4.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log4.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log4.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly4.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly4.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
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
                tbElevGainYearly4.Text = "";
                tbTimeYearly4.Text = "";
            }
        }

        private void Cb5LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex(cbLogYear5.SelectedItem.ToString());
            SetcbStatistic5(cbLogYear5.SelectedIndex.ToString());

            if (cbLogYear5.SelectedIndex > 0)
            {
                tb1Log5.Text = GetTotalMilesForSelectedLog(logYearIndex).ToString();
                tb2Log5.Text = GetTotalRidesForSelectedLog(logYearIndex).ToString();
                tb3Log5.Text = GetAverageRidesPerWeek(logYearIndex).ToString();
                tb4Log5.Text = GetAverageMilesPerWeek(logYearIndex).ToString();
                tb5Log5.Text = GetAverageMilesPerRide(logYearIndex).ToString();
                tb6Log5.Text = GetHighMileageWeekNumber(logYearIndex).ToString();
                tb7Log5.Text = GetHighMileageDay(logYearIndex).ToString();
                tbElevGainYearly5.Text = GetElevGain_Yearly(logYearIndex).ToString();
                tbTimeYearly5.Text = GetTotalMovingTimeYearly(logYearIndex).ToString();
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
                tbElevGainYearly5.Text = "";
                tbTimeYearly5.Text = "";
            }
        }

        //This option is currently not visable:
        public void ImportFromExcelLog(object sender, EventArgs e)
        {
            int logIndex;

            //window to selct the index:
            using (LegacyImport legacyImport = new LegacyImport())
            {
                legacyImport.ShowDialog();

                logIndex = legacyImport.getLegacyIndexSelection() + 1;

                if (logIndex < 1)
                {
                    return;
                }
            }

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
                        using (StreamReader file = new StreamReader(openfileDialog.FileName))
                        {
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
                                    string[] splitList = line.Split(',');

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

                                    string comment = "";
                                    if (splitList.Length > 19)
                                    {
                                        //Get the total:
                                        int arraySize = splitList.Length;
                                        for (int index = 18; index < arraySize; index++)
                                        {
                                            comment += splitList[index];
                                        }
                                    }

                                    objectValues.Add(comment);     //Comments:
                                    objectValues.Add(logIndex);         //LogYear index:

                                    //Need to figure out the week from the ride date:
                                    DateTime rideDate = Convert.ToDateTime(splitList[0]);
                                    string firstDay = GetFirstDayOfWeek();
                                    int weekValue = 0;

                                    if (firstDay.Equals("Sunday"))
                                    {
                                        weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Sunday);
                                    }
                                    else
                                    {
                                        weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Monday);
                                    }
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

            List<string> routeList = ReadDataNames("Table_Ride_Information", "Route");
            foreach (var route in routeList)
            {
                if (!currentRouteList.Contains(route))
                {
                    currentRouteList.Add(route);
                    cbRouteConfig.Items.Add(route);
                    RideDataEntry rideDataEntryForm = new RideDataEntry();
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(route);

                    //Add new entry to the Route Table:
                    List<object> routeObjectValues = new List<object>();
                    routeObjectValues.Add(route);
                    RunStoredProcedure(routeObjectValues, "Route_Add");
                }
            }

            //Now go through the list of Bikes and see if any of them need to be added to the Bike table:
            List<string> currentBikeList = new List<string>();
            for (int index = 0; index < cbBikeConfig.Items.Count; index++)
            {
                currentBikeList.Add(cbBikeConfig.GetItemText(cbBikeConfig.Items[index]));
            }

            List<string> bikeList = ReadDataNames("Table_Ride_Information", "Bike");
            foreach (var bike in bikeList)
            {
                if (!currentBikeList.Contains(bike))
                {
                    currentBikeList.Add(bike);
                    cbBikeConfig.Items.Add(bike);
                    RideDataEntry rideDataEntryForm = new RideDataEntry();
                    rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(bike);

                    //Add new entry to the Route Table:
                    List<object> bikeObjectValues = new List<object>();
                    bikeObjectValues.Add(bike);
                    RunStoredProcedure(bikeObjectValues, "Bike_Add");
                }
            }

            MessageBox.Show("Data Import successful.");
        }

        private void CbRenameRoute(object sender, EventArgs e)
        {
            RideDataEntry rideDataEntryForm = new RideDataEntry();
            //Read selected index and update the value for that index:
            string newValue = tbRouteConfig.Text;
            string oldValue = cbRouteConfig.SelectedItem.ToString();

            List<object> objectValues = new List<object>();
            objectValues.Add(newValue);
            objectValues.Add(oldValue);

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Route_Update", objectValues))
                {

                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to rename Route." + ex.Message.ToString());
            }

            List<string> tempList = new List<string>();
            int selectedIndex = cbRouteConfig.SelectedIndex;

            for (int i = 0; i < cbRouteConfig.Items.Count; i++)
            {
                tempList.Add(cbRouteConfig.Items[i].ToString());
            }

            ChartForm chartForm = new ChartForm(this);

            for (int i = 0; i < tempList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    cbRouteConfig.Items.Remove(oldValue);
                    cbRouteConfig.Items.Add(newValue);

                    rideDataEntryForm.cbRouteDataEntry.Items.Remove(oldValue);
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(newValue);

                    chartForm.cbRoutesChart.Items.Remove(oldValue);
                    chartForm.cbRoutesChart.Items.Add(newValue);
                }
            }

            cbRouteConfig.Sorted = true;
            chartForm.cbRoutesChart.Sorted = true;
            rideDataEntryForm.cbRouteDataEntry.Sorted = true;
            cbRouteConfig.SelectedIndex = selectedIndex;

            //Update the route name in the database for each row:
            SqlDataReader reader = null;

            try
            {
                sqlConnection.Open();

                // declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET Route=@NewValue WHERE [Route]=@OldValue", sqlConnection))
                {
                    // setcbStatistic1 parameters
                    cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = newValue;
                    cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = oldValue;

                    // get data stream
                    reader = cmd.ExecuteReader();
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

                refreshRoutes();
            }
        }

        private void BRenameLogYear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to Rename the Log Title?", "Rename Log", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string newValue = tbLogYearConfig.Text;
                string oldValue;

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

                string logYear = "";
                List<object> objectValues1 = new List<object>();
                objectValues1.Add(oldValue);
                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValues1))
                    {
                        if (results.HasRows)
                        {
                            while (results.Read())
                            {
                                logYear = results[0].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to get Log Year." + ex.Message.ToString());
                }

                List<object> objectValues = new List<object>();
                objectValues.Add(newValue);
                objectValues.Add(oldValue);
                objectValues.Add(logYear);

                RunStoredProcedure(objectValues, "Log_Year_Update");

                List<string> tempList = new List<string>();
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm(this);

                int statIndex1 = cbLogYear1.SelectedIndex;
                int statIndex2 = cbLogYear2.SelectedIndex;
                int statIndex3 = cbLogYear3.SelectedIndex;
                int statIndex4 = cbLogYear4.SelectedIndex;
                int statIndex5 = cbLogYear5.SelectedIndex;

                int cbLogYearConfigIndex = cbLogYearConfig.SelectedIndex;
                int cbStatMonthlyLogYearIndex = cbStatMonthlyLogYear.SelectedIndex;
                int rideDataEntryIndex = rideDataEntryForm.cbLogYearDataEntry.SelectedIndex;
                int rideDataDisplayFormIndex = rideDataDisplayForm.cbLogYearFilter.SelectedIndex;
                int rideDataChartIndex = chartForm.cbLogYearChart.SelectedIndex;

                for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
                {
                    tempList.Add(cbLogYearConfig.Items[i].ToString());
                }

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
                    if (cbLogYearConfigIndex == i)
                    {
                        cbLogYearConfig.Items.Remove(oldValue);
                        cbLogYearConfig.Items.Add(newValue);

                        rideDataEntryForm.cbLogYearDataEntry.Items.Remove(oldValue);
                        rideDataEntryForm.cbLogYearDataEntry.Items.Add(newValue);

                        rideDataDisplayForm.cbLogYearFilter.Items.Remove(oldValue);
                        rideDataDisplayForm.cbLogYearFilter.Items.Add(newValue);

                        chartForm.cbLogYearChart.Items.Remove(oldValue);
                        chartForm.cbLogYearChart.Items.Add(newValue);

                        cbStatMonthlyLogYear.Items.Remove(oldValue);
                        cbStatMonthlyLogYear.Items.Add(newValue);

                        cbLogYear1.Items.Add(newValue);
                        cbLogYear2.Items.Add(newValue);
                        cbLogYear3.Items.Add(newValue);
                        cbLogYear4.Items.Add(newValue);
                        cbLogYear5.Items.Add(newValue);
                    }
                    else
                    {
                        cbLogYear1.Items.Add(tempList[i]);
                        cbLogYear2.Items.Add(tempList[i]);
                        cbLogYear3.Items.Add(tempList[i]);
                        cbLogYear4.Items.Add(tempList[i]);
                        cbLogYear5.Items.Add(tempList[i]);
                    }
                }

                cbLogYearConfig.Sorted = true;
                rideDataEntryForm.cbLogYearDataEntry.Sorted = true;
                chartForm.cbLogYearChart.Sorted = true;
                cbStatMonthlyLogYear.Sorted = true;
                rideDataDisplayForm.cbLogYearFilter.Sorted = true;

                cbLogYearConfig.SelectedIndex = cbLogYearConfigIndex;
                rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = rideDataEntryIndex;
                rideDataDisplayForm.cbLogYearFilter.SelectedIndex = rideDataDisplayFormIndex;
                chartForm.cbLogYearChart.SelectedIndex = rideDataChartIndex;
                cbStatMonthlyLogYear.SelectedIndex = cbStatMonthlyLogYearIndex;

                cbLogYear1.Sorted = true;
                cbLogYear2.Sorted = true;
                cbLogYear3.Sorted = true;
                cbLogYear4.Sorted = true;
                cbLogYear5.Sorted = true;

                cbLogYear1.SelectedIndex = statIndex1;
                cbLogYear2.SelectedIndex = statIndex2;
                cbLogYear3.SelectedIndex = statIndex3;
                cbLogYear4.SelectedIndex = statIndex4;
                cbLogYear5.SelectedIndex = statIndex5;
            }

            //NOTE: The Table_Ride_Information only contains the LogYearID and not the name:
        }

        //=============================================================================
        //End Statistics Section
        //=============================================================================

        //=============================================================================
        //Start Maintenance Section
        //=============================================================================

        private void GetMaintLog()
        {
            lbMaintError.Text = "";

            try
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = null;

                using (sqlDataAdapter = new SqlDataAdapter())
                {
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [Date],[Bike],[Miles],[Comments] FROM Table_Bike_Maintenance", sqlConnection);

                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    dgvMaint.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                    dgvMaint.EnableHeadersVisualStyles = false;
                    dgvMaint.DataSource = dataTable;
                    dgvMaint.Refresh();
                    dgvMaint.Sort(dgvMaint.Columns["Date"], ListSortDirection.Descending);
                    dgvMaint.AllowUserToResizeRows = false;
                    dgvMaint.AllowUserToResizeColumns = false;
                    dgvMaint.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvMaint.AllowUserToAddRows = false;
                }
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

        private void BtMaintAdd_Click(object sender, EventArgs e)
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
            if (!int.TryParse(tbMaintMiles.Text, out int parsedValue))
            {
                lbMaintError.Text = "The miles value must be in numeric format. Enter 0 if unknown.";
                return;
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(cbBikeMaint.SelectedItem.ToString());
            objectValues.Add(rtbMaintComments.Text);
            objectValues.Add(dateTimePicker1.Value);
            objectValues.Add(tbMaintMiles.Text);
            RunStoredProcedure(objectValues, "Maintenance_Add");

            tbMaintID.Text = "";
            tbMaintMiles.Text = "";
            rtbMaintComments.Text = "";
            GetMaintLog();
        }

        private void BtMaintUpdate_Click(object sender, EventArgs e)
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
            if (!int.TryParse(tbMaintMiles.Text, out int parsedValue))
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
            RunStoredProcedure(objectValues, "Maintenance_Update");
            GetMaintLog();
        }

        private void BtMaintRemove_Click(object sender, EventArgs e)
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
            RunStoredProcedure(objectValues, "Maintenance_Remove");
            rtbMaintComments.Text = "";
            tbMaintMiles.Text = "";
            GetMaintLog();
        }

        private void BtMaintRetrieve_Click(object sender, EventArgs e)
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
                        //lbMaintError.Text = "No entry found for the selected Bike and Date.";
                        Logger.LogError("WARNING: No entry found for the selected Bike and Date.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
            }
        }

        private void CbBikeConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            string miles;
            string bikeName;
            //Load miles from the database:
            try
            {
                List<object> objectValues = new List<object>();

                if (cbBikeConfig.SelectedItem == null)
                {
                    cbBikeConfig.SelectedIndex = 0;
                    //bikeName = cbBikeConfig.SelectedItem.ToString();

                    objectValues.Add(tbBikeConfig.Text);

                }
                else
                {
                    objectValues.Add(cbBikeConfig.SelectedItem.ToString());
                    tbBikeConfig.Text = cbBikeConfig.SelectedItem.ToString();
                }

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
                        //MessageBox.Show("\"No entry found for the selected Bike and Date.");
                        Logger.LogError("WARNING: No entry found for the selected Bike and Date.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive Bike miles data." + ex.Message.ToString());
            }
        }

        private void BtDeleteAllData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This function will delete all data and from the Cycling Log Application's database. Are you sure you want to continue? If you have not made a backup copy of the database, select No, and make a copy and then run the function once again.", "Delete All Data From Database", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            //TODO: Reset entire application:
            //List of things to clean up:
            // Clear out Table_Routes:
            // Clear out Table_Bikes:
            // Clear out Table_Bike_Totals:
            // Clear out Table_Log_year:
            // Clear out Table_Bike_Mainenace:
            // Clear out Table_Ride_Information:
            List<object> objectBlank = new List<object>();

            //Update Table_Bikes Table:
            RunStoredProcedure(objectBlank, "DeleteTable");

            // Clear out all combo boxes:
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

            // TODO: Loop through each combo and delete items:

            //cbLogYearConfig
            //cbStatMonthlyLogYear
            //rideDataEntryForm.cbLogYearDataEntry
            //rideDataDisplayForm.cbLogYearFilter
            //chartForm.cbLogYearChart

            //cbRouteConfig
            //chartForm.cbRoutesChart
            //rideDataEntryForm.cbRouteDataEntry

            //cbBikeConfig
            //cbBikeMaint
            //rideDataEntryForm.cbBikeDataEntrySelection



            //Delete the config file and a default new one will be created:
            //This will give us the full name path of the executable file:
            //i.e. C:\Program Files\MyApplication\MyApplication.exe
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            string path = strWorkPath + "\\settings";
            string sourceFile = path + "\\CyclingLogConfig.xml";

            // Source file to be renamed  
            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile);
            // Check if file is there  
            if (fi.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(path + "\\CyclingLogConfig_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml");
            }

            addDefautRoute();
            Logger.Log("Adding a Route entry to the Configuration:" + "Miscellaneous Route:", 0, 0);

            SetCustomField1("");
            SetCustomField2("");

            SetLastLogSelectedDataEntry(0);
            SetcbStatistic1("0");
            SetcbStatistic2("0");
            SetcbStatistic3("0");
            SetcbStatistic4("0");
            SetcbStatistic5("0");
            SetLastLogFilterSelected(-1);
            SetLastBikeSelected(-1);
            SetLastLogSelected(-1);
            SetLastLogYearChartSelected(-1);
            SetLastMonthlyLogSelected(-1);

            MessageBox.Show("Close the program and reopen before entering any data.");
        }

        //Rename or update miles not in log:
        private void BtRenameBike_Click(object sender, EventArgs e)
        {
            if (cbBikeConfig.SelectedItem != null)
            {
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                //Verify Miles is entered and in the correct format:
                string miles = tbConfigMilesNotInLog.Text;

                if (!int.TryParse(miles, out int parsedValue))
                {
                    MessageBox.Show("\"The miles for the Bike must be in numeric format. Enter 0 if unknown.");
                    return;
                }

                int selectedIndex = cbBikeConfig.SelectedIndex;
                string newValue = tbBikeConfig.Text;
                string oldValue = cbBikeConfig.SelectedItem.ToString();

                List<object> objectBikes = new List<object>();
                objectBikes.Add(newValue);
                objectBikes.Add(oldValue);

                //Update Table_Bikes Table:
                RunStoredProcedure(objectBikes, "Bike_Update");

                List<object> objectBikeTotals = new List<object>();
                objectBikeTotals.Add(newValue);
                objectBikeTotals.Add(oldValue);
                objectBikeTotals.Add(Convert.ToDouble(miles));

                RunStoredProcedure(objectBikeTotals, "Bike_Totals_Update");

                List<string> tempList = new List<string>();

                for (int i = 0; i < cbBikeConfig.Items.Count; i++)
                {
                    tempList.Add(cbBikeConfig.Items[i].ToString());
                }

                for (int i = 0; i < tempList.Count; i++)
                {
                    if (selectedIndex == i)
                    {
                        cbBikeConfig.Items.Remove(oldValue);
                        cbBikeConfig.Items.Add(newValue);

                        cbBikeMaint.Items.Remove(oldValue);
                        cbBikeMaint.Items.Add(newValue);

                        rideDataEntryForm.cbBikeDataEntrySelection.Items.Remove(oldValue);
                        rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(newValue);
                    }
                }


                cbBikeConfig.Sorted = true;
                cbBikeMaint.Sorted = true;
                rideDataEntryForm.cbBikeDataEntrySelection.Sorted = true;

                cbBikeConfig.SelectedIndex = selectedIndex;
                //Update value in all database rows:
                SqlDataReader reader = null;

                try
                {
                    sqlConnection.Open();

                    //  declare command object with parameter
                    using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET [Bike]=@NewValue WHERE [Bike]=@OldValue", sqlConnection))
                    {

                        cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = newValue;
                        cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = oldValue;

                        // get data stream
                        reader = cmd.ExecuteReader();
                    }

                    // write each record
                    while (reader.Read())
                    {
                    }

                    refreshBikes();
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
            else
            {
                MessageBox.Show("Must select a Bike from the Bike Selection list.");
                return;
            }
        }

        public int GetLogYearIndexByName(string logName)
        {
            int logIndex = 0;
            List<object> objectValues = new List<object>();
            objectValues.Add(logName);

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Get_LogYear_IndexByName", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            logIndex = Int32.Parse(results[0].ToString());
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive Log Index." + ex.Message.ToString());
            }

            return logIndex;
        }

        private void CbLogYearConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLogYearConfig.SelectedItem == null)
            {
                return;
            }

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

        private void RunMonthlyStatistics()
        {
            if (cbStatMonthlyLogYear.SelectedItem == null)
            {
                return;
            }

            int logYearIndex = GetLogYearIndex(cbStatMonthlyLogYear.SelectedItem.ToString());

            month1R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R1.Text = GetTotalMilesMonthlyForSelectedLog(logYearIndex, 12).ToString();

            month1R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R2.Text = GetTotalRidesMonthlyForSelectedLog(logYearIndex, 12).ToString();

            month1R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 1).ToString();
            month2R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 2).ToString();
            month3R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 3).ToString();
            month4R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 4).ToString();
            month5R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 5).ToString();
            month6R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 6).ToString();
            month7R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 7).ToString();
            month8R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 8).ToString();
            month9R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 9).ToString();
            month10R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 10).ToString();
            month11R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 11).ToString();
            month12R3.Text = GetAvgMonthlyRidesForSelectedLog(logYearIndex, 12).ToString();

            month1R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 1).ToString();
            month2R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 2).ToString();
            month3R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 3).ToString();
            month4R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 4).ToString();
            month5R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 5).ToString();
            month6R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 6).ToString();
            month7R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 7).ToString();
            month8R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 8).ToString();
            month9R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 9).ToString();
            month10R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 10).ToString();
            month11R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 11).ToString();
            month12R4.Text = GetAverageMonthlyMilesPerWeek(logYearIndex, 12).ToString();

            month1R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 1).ToString();
            month2R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 2).ToString();
            month3R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 3).ToString();
            month4R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 4).ToString();
            month5R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 5).ToString();
            month6R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 6).ToString();
            month7R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 7).ToString();
            month8R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 8).ToString();
            month9R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 9).ToString();
            month10R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 10).ToString();
            month11R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 11).ToString();
            month12R5.Text = GetAverageMonthlyMilesPerRide(logYearIndex, 12).ToString();

            month1R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 1).ToString();
            month2R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 2).ToString();
            month3R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 3).ToString();
            month4R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 4).ToString();
            month5R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 5).ToString();
            month6R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 6).ToString();
            month7R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 7).ToString();
            month8R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 8).ToString();
            month9R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 9).ToString();
            month10R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 10).ToString();
            month11R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 11).ToString();
            month12R6.Text = GetMonthlyHighMileageWeekNumber(logYearIndex, 12).ToString();

            month1R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 1).ToString();
            month2R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 2).ToString();
            month3R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 3).ToString();
            month4R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 4).ToString();
            month5R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 5).ToString();
            month6R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 6).ToString();
            month7R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 7).ToString();
            month8R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 8).ToString();
            month9R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 9).ToString();
            month10R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 10).ToString();
            month11R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 11).ToString();
            month12R7.Text = GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, 12).ToString();

            tbElevGainMonthly1.Text = GetTotalElevGainMonthly(logYearIndex, 1).ToString();
            tbElevGainMonthly2.Text = GetTotalElevGainMonthly(logYearIndex, 2).ToString();
            tbElevGainMonthly3.Text = GetTotalElevGainMonthly(logYearIndex, 3).ToString();
            tbElevGainMonthly4.Text = GetTotalElevGainMonthly(logYearIndex, 4).ToString();
            tbElevGainMonthly5.Text = GetTotalElevGainMonthly(logYearIndex, 5).ToString();
            tbElevGainMonthly6.Text = GetTotalElevGainMonthly(logYearIndex, 6).ToString();
            tbElevGainMonthly7.Text = GetTotalElevGainMonthly(logYearIndex, 7).ToString();
            tbElevGainMonthly8.Text = GetTotalElevGainMonthly(logYearIndex, 8).ToString();
            tbElevGainMonthly9.Text = GetTotalElevGainMonthly(logYearIndex, 9).ToString();
            tbElevGainMonthly10.Text = GetTotalElevGainMonthly(logYearIndex, 10).ToString();
            tbElevGainMonthly11.Text = GetTotalElevGainMonthly(logYearIndex, 11).ToString();
            tbElevGainMonthly12.Text = GetTotalElevGainMonthly(logYearIndex, 12).ToString();

            tbTimeMonthly1.Text = GetTotalMovingTimeMonthly(logYearIndex, 1).ToString();
            tbTimeMonthly2.Text = GetTotalMovingTimeMonthly(logYearIndex, 2).ToString();
            tbTimeMonthly3.Text = GetTotalMovingTimeMonthly(logYearIndex, 3).ToString();
            tbTimeMonthly4.Text = GetTotalMovingTimeMonthly(logYearIndex, 4).ToString();
            tbTimeMonthly5.Text = GetTotalMovingTimeMonthly(logYearIndex, 5).ToString();
            tbTimeMonthly6.Text = GetTotalMovingTimeMonthly(logYearIndex, 6).ToString();
            tbTimeMonthly7.Text = GetTotalMovingTimeMonthly(logYearIndex, 7).ToString();
            tbTimeMonthly8.Text = GetTotalMovingTimeMonthly(logYearIndex, 8).ToString();
            tbTimeMonthly9.Text = GetTotalMovingTimeMonthly(logYearIndex, 9).ToString();
            tbTimeMonthly10.Text = GetTotalMovingTimeMonthly(logYearIndex, 10).ToString();
            tbTimeMonthly11.Text = GetTotalMovingTimeMonthly(logYearIndex, 11).ToString();
            tbTimeMonthly12.Text = GetTotalMovingTimeMonthly(logYearIndex, 12).ToString();
        }

        private void CbStatMonthlyLogYear_changed(object sender, EventArgs e)
        {
            //MainForm mainForm = new MainForm("");
            this.SetLastMonthlyLogSelected(cbStatMonthlyLogYear.SelectedIndex);
            //if (cbStatMonthlyLogYear.SelectedIndex == -1)
            //{
            // Display form modelessly
            //    lbRideDataEntryError.Show();
            //    lbRideDataEntryError.Text = "No Log Year selected.";
            //}
            //else
            //{
            //    lbRideDataEntryError.Hide();
            //}

            if (!formloading)
            {
                using (RefreshingForm refreshingForm = new RefreshingForm())
                {
                    // Display form modelessly
                    refreshingForm.Show();
                    //  ALlow main UI thread to properly display please wait form.
                    Application.DoEvents();
                    //this.ShowDialog();
                    RunMonthlyStatistics();
                    refreshingForm.Hide();
                }
            }
        }

        //Get total of miles for the selected log:
        //SELECT SUM(RideDistance) FROM Table_Ride_Information;
        private float GetTotalMilesMonthlyForSelectedLog(int logIndex, int month)
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
        private int GetTotalRidesMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            int returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalRides_Monthly", objectValues))
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
        private double GetAvgMonthlyRidesForSelectedLog(int logIndex, int month)
        {
            double avgRides;
            int rides = GetTotalRidesMonthlyForSelectedLog(logIndex, month);

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

        private double GetAverageMonthlyMilesPerWeek(int logIndex, int month)
        {
            double totalMiles = GetTotalMilesMonthlyForSelectedLog(logIndex, month);
            double avgMiles;
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
        private float GetAverageMonthlyMilesPerRide(int logIndex, int month)
        {
            float miles = GetTotalMilesMonthlyForSelectedLog(logIndex, month);
            int rides = GetTotalRidesMonthlyForSelectedLog(logIndex, month);
            float averageMiles = 0;

            if (miles > 0)
            {
                averageMiles = (float)miles / rides;
            }

            //MessageBox.Show(Convert.ToString(averageMiles));
            double avgMiles = Math.Round((double)averageMiles, 2);

            return (float)(avgMiles);
        }

        private double GetMaxHighMileageMonthlyForSelectedLog(int logIndex, int month)
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

        public double GetMonthlyHighMileageWeekNumber(int LogYearID, int Month)
        {
            //List<double> rideDistanceList = new List<double>();
            int weekNumber;
            int weekNumberTmp = 0;
            double weekMilesTotal = 0;
            double weeklyMax = 0;

            //TODO: Keep weeks from a Monday to Sunday

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + LogYearID + "=[LogYearID] and " + Month + "=MONTH([Date]) ORDER BY [WeekNumber] ASC";
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
                                weekMilesTotal += (double)reader["RideDistance"];
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

        public int GetTEST(int LogYearID)
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
                using (SqlCommand cmd = new SqlCommand("SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + LogYearID + "=[LogYearID]", sqlConnection))
                {
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
                }

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
            RunMonthlyStatistics();
            //using (RefreshingForm refreshingForm = new RefreshingForm())
            //{
            //    // Display form modelessly
            //    refreshingForm.Show();
            //    //  ALlow main UI thread to properly display please wait form.
            //    Application.DoEvents();
            //    //this.ShowDialog();
            //    RunMonthlyStatistics();
            //    refreshingForm.Hide();
            //}
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtBikeMilesUpdate_Click(object sender, EventArgs e)
        {

        }

        private void BtRefreshStatisticsData_Click(object sender, EventArgs e)
        {
            RefreshStatisticsData();
        }

        private void BtCharts_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm(this);
            chartForm.Show();
            lbMaintError.Text = "";
        }

        private string GetTotalElevGainMonthly(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            string returnValue = "0";
            int elevgain = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalElevGain_Monthly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();

                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        private string GetTotalMovingTimeMonthly(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            string returnValue = "00:00:00";
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int hr = 0;
            int min = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMovingTime_Monthly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals("") || temp.Equals("00:00:00"))
                        {
                            returnValue = "0:0:0";
                        }
                        else
                        {
                            splitValues = temp.Split(':');
                            if (!temp[0].Equals("00"))
                            {
                                hours += Int32.Parse(splitValues[0]);
                            }
                            if (!temp[1].Equals("00"))
                            {
                                minutes += Int32.Parse(splitValues[1]);
                            }
                            if (!temp[2].Equals("00"))
                            {
                                seconds += Int32.Parse(splitValues[2]);
                            }
                        }
                    }
                }

                if (seconds != 0)
                {
                    min = seconds / 60;
                    seconds = seconds % 60;
                }

                minutes = minutes + min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes = minutes % 60;
                }

                hours = hours + hr;
                string string_hours;
                string string_min;
                string string_sec;

                //Add leading 0 if less than 10:
                if (hours < 10)
                {
                    string_hours = "0" + hours.ToString();
                }
                else
                {
                    string_hours = hours.ToString();
                }
                if (minutes < 10)
                {
                    string_min = "0" + minutes.ToString();
                }
                else
                {
                    string_min = minutes.ToString();
                }
                if (seconds < 10)
                {
                    string_sec = "0" + seconds.ToString();
                }
                else
                {
                    string_sec = seconds.ToString();
                }

                //Calculate total time:
                returnValue = string_hours + ":" + string_min + ":" + string_sec;
            }

            return returnValue;
        }

        private string GetTotalMovingTimeYearly(int logIndex)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            string returnValue = "00:00:00";
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int min = 0;
            int hr = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMovingTime_Yearly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals("") || temp.Equals("00:00:00"))
                        {
                            returnValue = "0:0:0";
                        }
                        else
                        {
                            splitValues = temp.Split(':');
                            if (!temp[0].Equals("00"))
                            {
                                hours += Int32.Parse(splitValues[0]);
                            }
                            if (!temp[1].Equals("00"))
                            {
                                minutes += Int32.Parse(splitValues[1]);
                            }
                            if (!temp[2].Equals("00"))
                            {
                                seconds += Int32.Parse(splitValues[2]);
                            }
                        }
                    }
                }

                if (seconds != 0)
                {
                    min = seconds / 60;
                    seconds = seconds % 60;
                }

                minutes = minutes + min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes = minutes % 60;
                }

                hours = hours + hr;
                string string_hours;
                string string_min;
                string string_sec;

                //Add leading 0 if less than 10:
                if (hours < 10)
                {
                    string_hours = "0" + hours.ToString();
                }
                else
                {
                    string_hours = hours.ToString();
                }
                if (minutes < 10)
                {
                    string_min = "0" + minutes.ToString();
                }
                else
                {
                    string_min = minutes.ToString();
                }
                if (seconds < 10)
                {
                    string_sec = "0" + seconds.ToString();
                }
                else
                {
                    string_sec = seconds.ToString();
                }

                //Calculate total time:
                returnValue = string_hours + ":" + string_min + ":" + string_sec;
            }

            return returnValue;
        }

        private string GetTotalMovingTimeAllLogs()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "00:00:00";
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int min = 0;
            int hr = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMovingTime_AllLogs", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals("") || temp.Equals("00:00:00"))
                        {
                            returnValue = "0:0:0";
                        }
                        else
                        {
                            splitValues = temp.Split(':');
                            if (!temp[0].Equals("00"))
                            {
                                hours += Int32.Parse(splitValues[0]);
                            }
                            if (!temp[1].Equals("00"))
                            {
                                minutes += Int32.Parse(splitValues[1]);
                            }
                            if (!temp[2].Equals("00"))
                            {
                                seconds += Int32.Parse(splitValues[2]);
                            }
                        }
                    }
                }

                if (seconds != 0)
                {
                    min = seconds / 60;
                    seconds = seconds % 60;
                }

                minutes = minutes + min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes = minutes % 60;
                }

                hours = hours + hr;
                string string_hours;
                string string_min;
                string string_sec;

                //Add leading 0 if less than 10:
                if (hours < 10)
                {
                    string_hours = "0" + hours.ToString();
                }
                else
                {
                    string_hours = hours.ToString();
                }
                if (minutes < 10)
                {
                    string_min = "0" + minutes.ToString();
                }
                else
                {
                    string_min = minutes.ToString();
                }
                if (seconds < 10)
                {
                    string_sec = "0" + seconds.ToString();
                }
                else
                {
                    string_sec = seconds.ToString();
                }

                //Calculate total time:
                returnValue = string_hours + ":" + string_min + ":" + string_sec;
            }

            return returnValue;
        }

        private string GetTotalElevGainYearly(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            string returnValue = "0";
            int elevgain = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalElevGain_Monthly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();

                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        //*******************************************
        //WEEKLY
        //*******************************************
        //private void RefreshRideDataWeekly()
        //{
        //    int logIndex = 0;
        //    int logIndexPrevious = 0;

        //    int logYear = DateTime.Now.Year;
        //    logIndex = GetLogYearIndex(logYear);
        //    int logYearPrevious = logYear - 1;
        //    logIndexPrevious = GetLogYearIndex(logYearPrevious);

        //    int weekNumber = GetCurrentWeekCount();
        //    //Current week plus 4 prior weeks

        //    //Total Miles Weekly:
        //    //Current week:
        //    tbDistanceWeek1.Text = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
        //    lbweek1.Text = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber).ToString("MM/dd/yyyy");
        //    tbLongestRideWeek1.Text = GetLongestRideWeekly(logIndex, weekNumber).ToString();
        //    tbElevGainWeek1.Text = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
        //    tbNumRidesWeek1.Text = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
        //    tbAvgSpeedWeek1.Text = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
        //    tbTotalTimeWeekly1.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
        //    tbAvgPace1.Text = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek1.Text).ToString();

        //    // This first if handles when only one year log exists:
        //    if (logIndex == 1)
        //    {
        //        //Current week -1(2):
        //        if (weekNumber - 1 <= 0)
        //        {
        //            tbDistanceWeek2.Text = "0";
        //            tbLongestRideWeek2.Text = "0";
        //            tbElevGainWeek2.Text = "0";
        //            tbNumRidesWeek2.Text = "0";
        //            tbAvgSpeedWeek2.Text = "0";
        //            tbTotalTimeWeekly2.Text = "0";
        //            tbAvgPace2.Text = "0";
        //        }
        //        else
        //        {
        //            tbDistanceWeek2.Text = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
        //            tbLongestRideWeek2.Text = GetLongestRideWeekly(logIndex, weekNumber).ToString();
        //            tbElevGainWeek2.Text = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
        //            tbNumRidesWeek2.Text = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
        //            tbAvgSpeedWeek2.Text = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
        //            tbTotalTimeWeekly2.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
        //            tbAvgPace2.Text = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek2.Text).ToString();
        //        }
        //        //Current week -2(3):
        //        if (weekNumber - 2 <= 0)
        //        {
        //            tbDistanceWeek3.Text = "0";
        //            tbLongestRideWeek3.Text = "0";
        //            tbElevGainWeek3.Text = "0";
        //            tbNumRidesWeek3.Text = "0";
        //            tbAvgSpeedWeek3.Text = "0";
        //            tbTotalTimeWeekly3.Text = "0";
        //            tbAvgPace3.Text = "0";
        //        }
        //        else
        //        {
        //            tbDistanceWeek3.Text = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
        //            tbLongestRideWeek3.Text = GetLongestRideWeekly(logIndex, weekNumber).ToString();
        //            tbElevGainWeek3.Text = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
        //            tbNumRidesWeek3.Text = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
        //            tbAvgSpeedWeek3.Text = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
        //            tbTotalTimeWeekly3.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
        //            tbAvgPace3.Text = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek3.Text).ToString();
        //        }
        //        //Current week -3(4):
        //        if (weekNumber - 3 <= 0)
        //        {
        //            tbDistanceWeek4.Text = "0";
        //            tbLongestRideWeek4.Text = "0";
        //            tbElevGainWeek4.Text = "0";
        //            tbNumRidesWeek4.Text = "0";
        //            tbAvgSpeedWeek4.Text = "0";
        //            tbTotalTimeWeekly4.Text = "0";
        //            tbAvgPace4.Text = "0";
        //        }
        //        else
        //        {
        //            tbDistanceWeek4.Text = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
        //            tbLongestRideWeek4.Text = GetLongestRideWeekly(logIndex, weekNumber).ToString();
        //            tbElevGainWeek4.Text = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
        //            tbNumRidesWeek4.Text = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
        //            tbAvgSpeedWeek4.Text = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
        //            tbTotalTimeWeekly4.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
        //            tbAvgPace4.Text = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek4.Text).ToString();
        //        }
        //        //Current week -4(5):
        //        if (weekNumber - 4 <= 0)
        //        {
        //            tbDistanceWeek5.Text = "0";
        //            tbLongestRideWeek5.Text = "0";
        //            tbElevGainWeek5.Text = "0";
        //            tbNumRidesWeek5.Text = "0";
        //            tbAvgSpeedWeek5.Text = "0";
        //            tbTotalTimeWeekly5.Text = "0";
        //            tbAvgPace5.Text = "0";
        //        }
        //        else
        //        {
        //            tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
        //            tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndex, weekNumber).ToString();
        //            tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
        //            tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
        //            tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
        //            tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
        //            tbAvgPace5.Text = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek5.Text).ToString();
        //        }
        //    }
        //    else
        //    {
        //        //Current week -1(2):
        //        if (weekNumber - 1 <= 0)
        //        {
        //            tbDistanceWeek2.Text = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
        //            lbweek2.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
        //            tbLongestRideWeek2.Text = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
        //            tbElevGainWeek2.Text = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
        //            tbNumRidesWeek2.Text = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
        //            tbAvgSpeedWeek2.Text = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
        //            tbTotalTimeWeekly2.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
        //            tbAvgPace2.Text = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek2.Text).ToString();
        //        }
        //        else
        //        {
        //            int weekNumber2 = weekNumber - 1;
        //            tbDistanceWeek2.Text = GetTotalMilesWeekly(logIndex, weekNumber2).ToString();
        //            lbweek2.Text = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber2).ToString("MM/dd/yyyy");
        //            tbLongestRideWeek2.Text = GetLongestRideWeekly(logIndex, weekNumber2).ToString();
        //            tbElevGainWeek2.Text = GetTotalElevGainWeekly(logIndex, weekNumber2).ToString();
        //            tbNumRidesWeek2.Text = GetTotalRidesWeekly(logIndex, weekNumber2).ToString();
        //            tbAvgSpeedWeek2.Text = GetAvgSpeedWeekly(logIndex, weekNumber2).ToString();
        //            tbTotalTimeWeekly2.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber2).ToString();
        //            tbAvgPace2.Text = GetAveragePaceWeekly(logIndex, weekNumber2, tbDistanceWeek2.Text).ToString();
        //        }
        //        //Current week -2(3):
        //        if (weekNumber - 2 <= 0)
        //        {
        //            if (weekNumber == 1)
        //            {
        //                lbweek3.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
        //                tbDistanceWeek3.Text = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
        //                tbLongestRideWeek3.Text = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
        //                tbElevGainWeek3.Text = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
        //                tbNumRidesWeek3.Text = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgSpeedWeek3.Text = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
        //                tbTotalTimeWeekly3.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgPace3.Text = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek3.Text).ToString();
        //            }
        //            else if (weekNumber == 2)
        //            {
        //                lbweek3.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
        //                tbDistanceWeek3.Text = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
        //                tbLongestRideWeek3.Text = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
        //                tbElevGainWeek3.Text = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
        //                tbNumRidesWeek3.Text = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgSpeedWeek3.Text = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
        //                tbTotalTimeWeekly3.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgPace3.Text = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek3.Text).ToString();
        //            }
        //        }
        //        else
        //        {
        //            int weekNumber3 = weekNumber - 2;
        //            tbDistanceWeek3.Text = GetTotalMilesWeekly(logIndex, weekNumber3).ToString();
        //            lbweek3.Text = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber3).ToString("MM/dd/yyyy");
        //            tbLongestRideWeek3.Text = GetLongestRideWeekly(logIndex, weekNumber3).ToString();
        //            tbElevGainWeek3.Text = GetTotalElevGainWeekly(logIndex, weekNumber3).ToString();
        //            tbNumRidesWeek3.Text = GetTotalRidesWeekly(logIndex, weekNumber3).ToString();
        //            tbAvgSpeedWeek3.Text = GetAvgSpeedWeekly(logIndex, weekNumber3).ToString();
        //            tbTotalTimeWeekly3.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber3).ToString();
        //            tbAvgPace3.Text = GetAveragePaceWeekly(logIndex, weekNumber3, tbDistanceWeek3.Text).ToString();
        //        }
        //        //Current week -3(4):
        //        if (weekNumber - 3 <= 0)
        //        {
        //            if (weekNumber == 1)
        //            {
        //                lbweek4.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
        //                tbDistanceWeek4.Text = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
        //                tbLongestRideWeek4.Text = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
        //                tbElevGainWeek4.Text = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
        //                tbNumRidesWeek4.Text = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
        //                tbAvgSpeedWeek4.Text = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
        //                tbTotalTimeWeekly4.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
        //                tbAvgPace4.Text = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek4.Text).ToString();
        //            }
        //            else if (weekNumber == 2)
        //            {
        //                lbweek4.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
        //                tbDistanceWeek4.Text = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
        //                tbLongestRideWeek4.Text = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
        //                tbElevGainWeek4.Text = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
        //                tbNumRidesWeek4.Text = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgSpeedWeek4.Text = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
        //                tbTotalTimeWeekly4.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgPace4.Text = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek4.Text).ToString();
        //            }
        //            else if (weekNumber == 3)
        //            {
        //                lbweek4.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
        //                tbDistanceWeek4.Text = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
        //                tbLongestRideWeek4.Text = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
        //                tbElevGainWeek4.Text = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
        //                tbNumRidesWeek4.Text = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgSpeedWeek4.Text = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
        //                tbTotalTimeWeekly4.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgPace4.Text = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek4.Text).ToString();
        //            }
        //        }
        //        else
        //        {
        //            int weekNumber4 = weekNumber - 3;
        //            tbDistanceWeek4.Text = GetTotalMilesWeekly(logIndex, weekNumber4).ToString();
        //            tbLongestRideWeek4.Text = GetLongestRideWeekly(logIndex, weekNumber4).ToString();
        //            lbweek4.Text = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber4).ToString("MM/dd/yyyy");
        //            tbElevGainWeek4.Text = GetTotalElevGainWeekly(logIndex, weekNumber4).ToString();
        //            tbNumRidesWeek4.Text = GetTotalRidesWeekly(logIndex, weekNumber4).ToString();
        //            tbAvgSpeedWeek4.Text = GetAvgSpeedWeekly(logIndex, weekNumber4).ToString();
        //            tbTotalTimeWeekly4.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber4).ToString();
        //            tbAvgPace4.Text = GetAveragePaceWeekly(logIndex, weekNumber4, tbDistanceWeek4.Text).ToString();
        //        }
        //        //Current week -4(5):
        //        if (weekNumber - 4 <= 0)
        //        {
        //            if (weekNumber == 1)
        //            {
        //                lbweek5.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 49).ToString("MM/dd/yyyy");
        //                tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndexPrevious, 49).ToString();
        //                tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndexPrevious, 49).ToString();
        //                tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndexPrevious, 49).ToString();
        //                tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndexPrevious, 49).ToString();
        //                tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndexPrevious, 49).ToString();
        //                tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 49).ToString();
        //                tbAvgPace5.Text = GetAveragePaceWeekly(logIndexPrevious, 49, tbDistanceWeek5.Text).ToString();
        //            }
        //            else if (weekNumber == 2)
        //            {
        //                lbweek5.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
        //                tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
        //                tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
        //                tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
        //                tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
        //                tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
        //                tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
        //                tbAvgPace5.Text = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek5.Text).ToString();
        //            }
        //            else if (weekNumber == 3)
        //            {
        //                lbweek5.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
        //                tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
        //                tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
        //                tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
        //                tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
        //                tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
        //                tbAvgPace5.Text = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek5.Text).ToString();
        //            }
        //            else if (weekNumber == 4)
        //            {
        //                lbweek5.Text = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
        //                tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
        //                tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
        //                tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
        //                tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
        //                tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
        //                tbAvgPace5.Text = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek5.Text).ToString();
        //            }
        //        }
        //        else
        //        {
        //            int weekNumber5 = weekNumber - 4;
        //            tbDistanceWeek5.Text = GetTotalMilesWeekly(logIndex, weekNumber5).ToString();
        //            lbweek5.Text = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber5).ToString("MM/dd/yyyy");
        //            tbLongestRideWeek5.Text = GetLongestRideWeekly(logIndex, weekNumber5).ToString();
        //            tbElevGainWeek5.Text = GetTotalElevGainWeekly(logIndex, weekNumber5).ToString();
        //            tbNumRidesWeek5.Text = GetTotalRidesWeekly(logIndex, weekNumber5).ToString();
        //            tbAvgSpeedWeek5.Text = GetAvgSpeedWeekly(logIndex, weekNumber5).ToString();
        //            tbTotalTimeWeekly5.Text = GetTotalMovingTimeWeekly(logIndex, weekNumber5).ToString();
        //            tbAvgPace5.Text = GetAveragePaceWeekly(logIndex, weekNumber5, tbDistanceWeek5.Text).ToString();
        //        }
        //    }
        //}

        private void RefreshRideDataWeekly_Backup()
        {
            int logIndex = 0;
            int logIndexPrevious = 0;

            int logYear = DateTime.Now.Year;
            logIndex = GetLogYearIndex(logYear);
            int logYearPrevious = logYear - 1;
            logIndexPrevious = GetLogYearIndex(logYearPrevious);

            int weekNumber = GetCurrentWeekCount();
            //Current week plus 4 prior weeks

            //Total Miles Weekly:
            //Current week:
            string tbDistanceWeek1 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
            string lbweek1 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber).ToString("MM/dd/yyyy");
            string tbLongestRideWeek1 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
            string tbElevGainWeek1 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
            string tbNumRidesWeek1 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
            string tbAvgSpeedWeek1 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
            string tbTotalTimeWeekly1 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
            string tbAvgPace1 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek1).ToString();

            string tbDistanceWeek2 = "";
            string lbweek2 = "";
            string tbLongestRideWeek2 = "";
            string tbElevGainWeek2 = "";
            string tbNumRidesWeek2 = "";
            string tbAvgSpeedWeek2 = "";
            string tbTotalTimeWeekly2 = "";
            string tbAvgPace2 = "";

            string tbDistanceWeek3 = "";
            string lbweek3 = "";
            string tbLongestRideWeek3 = "";
            string tbElevGainWeek3 = "";
            string tbNumRidesWeek3 = "";
            string tbAvgSpeedWeek3 = "";
            string tbTotalTimeWeekly3 = "";
            string tbAvgPace3 = "";

            string tbDistanceWeek4 = "";
            string lbweek4 = "";
            string tbLongestRideWeek4 = "";
            string tbElevGainWeek4 = "";
            string tbNumRidesWeek4 = "";
            string tbAvgSpeedWeek4 = "";
            string tbTotalTimeWeekly4 = "";
            string tbAvgPace4 = "";

            string tbDistanceWeek5 = "";
            string lbweek5 = "";
            string tbLongestRideWeek5 = "";
            string tbElevGainWeek5 = "";
            string tbNumRidesWeek5 = "";
            string tbAvgSpeedWeek5 = "";
            string tbTotalTimeWeekly5 = "";
            string tbAvgPace5 = "";


            // This first if handles when only one year log exists:
            if (logIndex == 1)
            {
                //Current week -1(2):
                if (weekNumber - 1 <= 0)
                {
                    tbDistanceWeek2 = "0";
                    tbLongestRideWeek2 = "0";
                    tbElevGainWeek2 = "0";
                    tbNumRidesWeek2 = "0";
                    tbAvgSpeedWeek2 = "0";
                    tbTotalTimeWeekly2 = "0";
                    tbAvgPace2 = "0";
                }
                else
                {
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek2).ToString();
                }
                //Current week -2(3):
                if (weekNumber - 2 <= 0)
                {
                    tbDistanceWeek3 = "0";
                    tbLongestRideWeek3 = "0";
                    tbElevGainWeek3 = "0";
                    tbNumRidesWeek3 = "0";
                    tbAvgSpeedWeek3 = "0";
                    tbTotalTimeWeekly3 = "0";
                    tbAvgPace3 = "0";
                }
                else
                {
                    tbDistanceWeek3 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek3 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek3 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek3 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace3 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek3).ToString();
                }
                //Current week -3(4):
                if (weekNumber - 3 <= 0)
                {
                    tbDistanceWeek4 = "0";
                    tbLongestRideWeek4 = "0";
                    tbElevGainWeek4 = "0";
                    tbNumRidesWeek4 = "0";
                    tbAvgSpeedWeek4 = "0";
                    tbTotalTimeWeekly4 = "0";
                    tbAvgPace4 = "0";
                }
                else
                {
                    tbDistanceWeek4 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek4 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek4 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek4 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace4 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek4).ToString();
                }
                //Current week -4(5):
                if (weekNumber - 4 <= 0)
                {
                    tbDistanceWeek5 = "0";
                    tbLongestRideWeek5 = "0";
                    tbElevGainWeek5 = "0";
                    tbNumRidesWeek5 = "0";
                    tbAvgSpeedWeek5 = "0";
                    tbTotalTimeWeekly5 = "0";
                    tbAvgPace5 = "0";
                }
                else
                {
                    tbDistanceWeek5 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek5 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek5 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek5 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace5 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek5).ToString();
                }
            }
            else
            {
                //Current week -1(2):
                if (weekNumber - 1 <= 0)
                {
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                    lbweek2 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek2).ToString();
                }
                else
                {
                    int weekNumber2 = weekNumber - 1;
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndex, weekNumber2).ToString();
                    lbweek2 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber2).ToString("MM/dd/yyyy");
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndex, weekNumber2).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndex, weekNumber2).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndex, weekNumber2).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndex, weekNumber2).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndex, weekNumber2).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndex, weekNumber2, tbDistanceWeek2).ToString();
                }
                //Current week -2(3):
                if (weekNumber - 2 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek3 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek3 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek3 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek3 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace3 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek3).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek3 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek3 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek3 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek3 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace3 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek3).ToString();
                    }
                }
                else
                {
                    int weekNumber3 = weekNumber - 2;
                    tbDistanceWeek3 = GetTotalMilesWeekly(logIndex, weekNumber3).ToString();
                    lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber3).ToString("MM/dd/yyyy");
                    tbLongestRideWeek3 = GetLongestRideWeekly(logIndex, weekNumber3).ToString();
                    tbElevGainWeek3 = GetTotalElevGainWeekly(logIndex, weekNumber3).ToString();
                    tbNumRidesWeek3 = GetTotalRidesWeekly(logIndex, weekNumber3).ToString();
                    tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndex, weekNumber3).ToString();
                    tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndex, weekNumber3).ToString();
                    tbAvgPace3 = GetAveragePaceWeekly(logIndex, weekNumber3, tbDistanceWeek3).ToString();
                }
                //Current week -3(4):
                if (weekNumber - 3 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek4).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek4).ToString();
                    }
                    else if (weekNumber == 3)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek4).ToString();
                    }
                }
                else
                {
                    int weekNumber4 = weekNumber - 3;
                    tbDistanceWeek4 = GetTotalMilesWeekly(logIndex, weekNumber4).ToString();
                    tbLongestRideWeek4 = GetLongestRideWeekly(logIndex, weekNumber4).ToString();
                    lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber4).ToString("MM/dd/yyyy");
                    tbElevGainWeek4 = GetTotalElevGainWeekly(logIndex, weekNumber4).ToString();
                    tbNumRidesWeek4 = GetTotalRidesWeekly(logIndex, weekNumber4).ToString();
                    tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndex, weekNumber4).ToString();
                    tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndex, weekNumber4).ToString();
                    tbAvgPace4 = GetAveragePaceWeekly(logIndex, weekNumber4, tbDistanceWeek4).ToString();
                }
                //Current week -4(5):
                if (weekNumber - 4 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 49).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 49).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 49).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 49).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 49).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 49).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 49).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 49, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 3)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 4)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek5).ToString();
                    }
                }
                else
                {
                    int weekNumber5 = weekNumber - 4;
                    tbDistanceWeek5 = GetTotalMilesWeekly(logIndex, weekNumber5).ToString();
                    lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber5).ToString("MM/dd/yyyy");
                    tbLongestRideWeek5 = GetLongestRideWeekly(logIndex, weekNumber5).ToString();
                    tbElevGainWeek5 = GetTotalElevGainWeekly(logIndex, weekNumber5).ToString();
                    tbNumRidesWeek5 = GetTotalRidesWeekly(logIndex, weekNumber5).ToString();
                    tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndex, weekNumber5).ToString();
                    tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndex, weekNumber5).ToString();
                    tbAvgPace5 = GetAveragePaceWeekly(logIndex, weekNumber5, tbDistanceWeek5).ToString();
                }
            }
        }

        private float GetTotalMilesWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            float returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMiles_Weekly", objectValues))
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

        private double GetLongestRideWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMaxHighMileage_Weekly", objectValues))
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

        private string GetTotalElevGainWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            string returnValue = "0";
            int elevgain = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalElevGain_Weekly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();

                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            elevgain = Int32.Parse(returnValue);
            returnValue = elevgain.ToString("N0");

            return returnValue;
        }

        private string GetTotalRidesWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            string returnValue = "0";

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalRides_Weekly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            returnValue = "0";
                        }
                        else
                        {
                            returnValue = temp;
                        }
                    }
                }
            }

            return returnValue;
        }

        private double GetAvgSpeedWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            double returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetAvgSpeed_Weekly", objectValues))
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
                            returnValue = Math.Round(returnValue, 1);
                        }
                    }
                }
            }

            return returnValue;
        }

        private string GetTotalMovingTimeWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            string returnValue = "00:00:00";
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int hr = 0;
            int min = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMovingTime_Weekly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals("") || temp.Equals("00:00:00"))
                        {
                            returnValue = "0:0:0";
                        }
                        else
                        {
                            splitValues = temp.Split(':');
                            if (!temp[0].Equals("00"))
                            {
                                hours += Int32.Parse(splitValues[0]);
                            }
                            if (!temp[1].Equals("00"))
                            {
                                minutes += Int32.Parse(splitValues[1]);
                            }
                            if (!temp[2].Equals("00"))
                            {
                                seconds += Int32.Parse(splitValues[2]);
                            }
                        }
                    }
                }

                if (seconds != 0)
                {
                    min = seconds / 60;
                    seconds = seconds % 60;
                }

                minutes = minutes + min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes = minutes % 60;
                }

                hours = hours + hr;
                string string_hours;
                string string_min;
                string string_sec;

                //Add leading 0 if less than 10:
                if (hours < 10)
                {
                    string_hours = "0" + hours.ToString();
                }
                else
                {
                    string_hours = hours.ToString();
                }
                if (minutes < 10)
                {
                    string_min = "0" + minutes.ToString();
                }
                else
                {
                    string_min = minutes.ToString();
                }
                if (seconds < 10)
                {
                    string_sec = "0" + seconds.ToString();
                }
                else
                {
                    string_sec = seconds.ToString();
                }

                //Calculate total time:
                returnValue = string_hours + ":" + string_min + ":" + string_sec;
            }

            return returnValue;
        }

        private string GetAveragePaceWeekly(int logIndex, int weekNumber, string totalMiles)
        {

            if (totalMiles == "0")
            {
                return "0";
            }

            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(weekNumber);
            string returnValue = "0.0";
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int hr = 0;
            int min = 0;
            double avgPace = 0;
            string[] splitAsTime;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMovingTime_Weekly", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals("") || temp.Equals("00:00:00"))
                        {
                            returnValue = "0:0:0";
                        }
                        else
                        {
                            splitValues = temp.Split(':');
                            if (!temp[0].Equals("00"))
                            {
                                hours += Int32.Parse(splitValues[0]);
                            }
                            if (!temp[1].Equals("00"))
                            {
                                minutes += Int32.Parse(splitValues[1]);
                            }
                            if (!temp[2].Equals("00"))
                            {
                                seconds += Int32.Parse(splitValues[2]);
                            }
                        }
                    }
                }

                if (seconds != 0)
                {
                    min = seconds / 60;
                    seconds = seconds % 60;
                }

                minutes = minutes + min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes = minutes % 60;
                }

                hours = hours + hr;

                //Calculate total time in minutes:
                double secToMin = seconds / 60.0;
                double hrsToMin = hours * 60;
                double totalMin = minutes + secToMin + hrsToMin;

                //Divide total minutes by total miles:
                avgPace = totalMin / double.Parse(totalMiles);
                avgPace = Math.Round(avgPace, 2);
                splitAsTime = avgPace.ToString().Split('.');
            }

            if (splitAsTime.Length == 1)
            {
                returnValue = splitAsTime[0] + ":0/mile";
            }
            else
            {
                returnValue = splitAsTime[0] + ":" + splitAsTime[1] + "/mile";
            }

            return returnValue;
        }

        private int GetLogYearIndex(int year)
        {
            int logIndex = 0;
            //Get week number
            List<object> objectValues = new List<object>();
            objectValues.Add(year);
            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            MessageBox.Show("Unable to obtain the Loge year Index ID");

                            return 0;
                        }
                        else
                        {
                            logIndex = Int32.Parse(temp);
                            break;
                        }
                    }
                }
                return logIndex;
            }
        }

        private int GetRouteCount(string routeName)
        {
            int count = 0;
            List<object> objectValues = new List<object>();
            objectValues.Add(routeName);
            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetRouteCount", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            MessageBox.Show("Unable to find Route by name.");

                            return 0;
                        }
                        else
                        {
                            count = Int32.Parse(temp);
                            break;
                        }
                    }
                }
                return count;
            }
        }

        private int GetBikeCount(string routeName)
        {
            int count = 0;
            List<object> objectValues = new List<object>();
            objectValues.Add(routeName);
            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetBikeCount", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        string temp = results[0].ToString();
                        if (temp.Equals(""))
                        {
                            MessageBox.Show("Unable to find Bike by name.");

                            return 0;
                        }
                        else
                        {
                            count = Int32.Parse(temp);
                            break;
                        }
                    }
                }
                return count;
            }
        }

        private int GetWeekNumber(int weekNumber)
        {
            //Current week -1:
            if (weekNumber - 1 <= 0)
            {
                weekNumber = 52;
            }
            //Current week -2:
            if (weekNumber - 2 <= 0)
            {
                if (weekNumber == 1)
                {
                    weekNumber = 51;
                }
                else if (weekNumber == 2)
                {
                    weekNumber = 52;
                }
            }
            //Current week -3:
            if (weekNumber - 3 <= 0)
            {
                if (weekNumber == 1)
                {
                    weekNumber = 50;
                }
                else if (weekNumber == 2)
                {
                    weekNumber = 51;
                }
                else if (weekNumber == 3)
                {
                    weekNumber = 52;
                }
            }
            //Current week -4:
            if (weekNumber - 4 <= 0)
            {
                if (weekNumber == 1)
                {
                    weekNumber = 49;
                }
                else if (weekNumber == 2)
                {
                    weekNumber = 50;
                }
                else if (weekNumber == 3)
                {
                    weekNumber = 51;
                }
                else if (weekNumber == 4)
                {
                    weekNumber = 52;
                }
            }

            return weekNumber;
        }

        public DateTime GetDateFromWeekNumber(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;

            string firstDay = GetFirstDayOfWeek();
            int firstWeek = 0;

            if (firstDay.Equals("Sunday"))
            {
                firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            }
            else
            {
                firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3).Date;
        }

        private void btFirstDay_Click(object sender, EventArgs e)
        {
            if (rbFirstDaySunday.Checked)
            {
                SetFirstDayOfWeek("Sunday");
                MessageBox.Show("First Day of week set to Sunday.");
            }
            else
            {
                SetFirstDayOfWeek("Monday");
                MessageBox.Show("First Day of week set to Monday.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string pathFile = strWorkPath + "\\Cycling_Log_User's_Guide.docx";

            System.Diagnostics.Process.Start(pathFile);
        }

        public void refreshData()
        {
            // Run Refresh for all data fields:
            RefreshStatisticsData();
            RunMonthlyStatistics();
            refreshWeekly();
            refreshBikes();
            refreshRoutes();

            //using (RefreshingForm refreshingForm = new RefreshingForm())
            //{
            //    // Display form modelessly
            //    refreshingForm.Show();
            //    //  ALlow main UI thread to properly display please wait form.
            //    Application.DoEvents();
            //    //this.ShowDialog();
            //    RunMonthlyStatistics();
            //    refreshingForm.Hide();
            //}
        }

        private void btRefreshData_Click(object sender, EventArgs e)
        {
            refreshData();
            MessageBox.Show("All data fields have been updated.");
        }

        private void btCustomDataField1_Click(object sender, EventArgs e)
        {
            SetCustomField1(tbCustomDataField1.Text);
            SetCustomField2(tbCustomDataField2.Text);
            ConfigurationFile configurationFile = new ConfigurationFile();
            configurationFile.writeConfigFile();
        }

        private void refreshRoutes()
        {
            int count = 0;

            try
            {
                List<string> routeList = ReadDataNames("Table_Routes", "Name");

                dataGridViewRoutes.ColumnCount = 2;
                dataGridViewRoutes.Name = "Route Listing And Counts";
                dataGridViewRoutes.Columns[1].Name = "Count";
                dataGridViewRoutes.Columns[0].Name = "Route Name";
                dataGridViewRoutes.Columns[1].ValueType = typeof(int);
                dataGridViewRoutes.Columns[0].ValueType = typeof(string);
                dataGridViewRoutes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridViewRoutes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewRoutes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewRoutes.ReadOnly = true;
                dataGridViewRoutes.EnableHeadersVisualStyles = false;

                // Resize the master DataGridView columns to fit the newly loaded data.
                dataGridViewRoutes.AutoResizeColumns();
                dataGridViewRoutes.AllowUserToOrderColumns = true;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewRoutes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewRoutes.AllowUserToAddRows = false;
                dataGridViewRoutes.Columns["Count"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dataGridViewRoutes.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                //dataGridViewRoutes.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridViewRoutes.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                //dataGridViewRoutes.RowHeadersVisible = false;
                dataGridViewRoutes.AllowUserToResizeRows = false;
                dataGridViewRoutes.AllowUserToResizeColumns = false;
                dataGridViewRoutes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridViewRoutes.ColumnHeadersHeight = 40;
                dataGridViewRoutes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                for (int i = 0; i < routeList.Count; i++)
                {
                    count = GetRouteCount(routeList[i]);
                    this.dataGridViewRoutes.Rows.Add(routeList[i], count);
                }

                tbTotalRoutes.Text = routeList.Count.ToString("N0");
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Routes: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Routes.  Review the log for more information.");
            }
        }

        private void refreshBikes()
        {
            int count = 0;
            double miles = 0;
            double milesNotInLog = 0;
            double totalMiles = 0;

            try
            {
                List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

                dataGridViewBikes.DataSource = null;
                dataGridViewBikes.Rows.Clear();
                dataGridViewBikes.ColumnCount = 4;
                dataGridViewBikes.Name = "Bike Listing And Miles";
                dataGridViewBikes.Columns[0].Name = "Bike Name";
                dataGridViewBikes.Columns[1].Name = "Rides";
                dataGridViewBikes.Columns[2].Name = "Miles";
                dataGridViewBikes.Columns[3].Name = "Miles Not In Log";
                dataGridViewBikes.Columns[0].ValueType = typeof(string);
                dataGridViewBikes.Columns[0].ValueType = typeof(int);
                dataGridViewBikes.Columns[0].ValueType = typeof(double);
                dataGridViewBikes.Columns[0].ValueType = typeof(double);
                dataGridViewBikes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridViewBikes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewBikes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewBikes.ReadOnly = true;
                dataGridViewBikes.EnableHeadersVisualStyles = false;
                dataGridViewBikes.Columns["Rides"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles Not In Log"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                dataGridViewBikes.AutoResizeColumns();
                dataGridViewBikes.AllowUserToOrderColumns = true;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewBikes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewBikes.Columns["Rides"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles Not In Log"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.AllowUserToAddRows = false;
                dataGridViewBikes.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewBikes.AllowUserToResizeRows = false;
                dataGridViewBikes.AllowUserToResizeColumns = false;
                dataGridViewBikes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridViewBikes.ColumnHeadersHeight = 40;
                dataGridViewBikes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                for (int i = 0; i < bikeList.Count; i++)
                {
                    try
                    {
                        List<object> objectValues = new List<object>();
                        objectValues.Add(bikeList[i]);

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
                                        miles = 0;
                                    }
                                    else
                                    {
                                        miles = Convert.ToDouble(temp);
                                    }
                                }
                                totalMiles += miles;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
                    }

                    try
                    {
                        List<object> objectValues = new List<object>();
                        objectValues.Add(bikeList[i]);

                        //ExecuteScalarFunction
                        using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
                        {
                            if (results.HasRows)
                            {
                                while (results.Read())
                                {
                                    milesNotInLog = Convert.ToInt32(results[0]);
                                }
                            }
                            else
                            {
                                //lbMaintError.Text = "No entry found for the selected Bike and Date.";
                                Logger.LogError("[WARNING: No entry found for the selected Bike and Date.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
                    }

                    count = GetBikeCount(bikeList[i]);
                    this.dataGridViewBikes.Rows.Add(bikeList[i], count, miles, milesNotInLog);
                }

                tbBikeMilesTotal.Text = totalMiles.ToString("N0");
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Bikes: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Bikes.  Review the log for more information.");
            }
        }

        private void refreshWeekly()
        {
            int logIndex = 0;
            int logIndexPrevious = 0;

            int logYear = DateTime.Now.Year;
            logIndex = GetLogYearIndex(logYear);
            int logYearPrevious = logYear - 1;
            logIndexPrevious = GetLogYearIndex(logYearPrevious);

            int weekNumber = GetCurrentWeekCount();
            //Current week plus 4 prior weeks

            //Total Miles Weekly:
            //Current week:
            string tbDistanceWeek1 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
            string lbweek1 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber).ToString("MM/dd/yyyy");
            string tbLongestRideWeek1 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
            string tbElevGainWeek1 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
            string tbNumRidesWeek1 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
            string tbAvgSpeedWeek1 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
            string tbTotalTimeWeekly1 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
            string tbAvgPace1 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek1).ToString();

            string tbDistanceWeek2 = "";
            string lbweek2 = "";
            string tbLongestRideWeek2 = "";
            string tbElevGainWeek2 = "";
            string tbNumRidesWeek2 = "";
            string tbAvgSpeedWeek2 = "";
            string tbTotalTimeWeekly2 = "";
            string tbAvgPace2 = "";

            string tbDistanceWeek3 = "";
            string lbweek3 = "";
            string tbLongestRideWeek3 = "";
            string tbElevGainWeek3 = "";
            string tbNumRidesWeek3 = "";
            string tbAvgSpeedWeek3 = "";
            string tbTotalTimeWeekly3 = "";
            string tbAvgPace3 = "";

            string tbDistanceWeek4 = "";
            string lbweek4 = "";
            string tbLongestRideWeek4 = "";
            string tbElevGainWeek4 = "";
            string tbNumRidesWeek4 = "";
            string tbAvgSpeedWeek4 = "";
            string tbTotalTimeWeekly4 = "";
            string tbAvgPace4 = "";

            string tbDistanceWeek5 = "";
            string lbweek5 = "";
            string tbLongestRideWeek5 = "";
            string tbElevGainWeek5 = "";
            string tbNumRidesWeek5 = "";
            string tbAvgSpeedWeek5 = "";
            string tbTotalTimeWeekly5 = "";
            string tbAvgPace5 = "";


            // This first if handles when only one year log exists:
            if (logIndex == 1)
            {
                //Current week -1(2):
                if (weekNumber - 1 <= 0)
                {
                    tbDistanceWeek2 = "0";
                    tbLongestRideWeek2 = "0";
                    tbElevGainWeek2 = "0";
                    tbNumRidesWeek2 = "0";
                    tbAvgSpeedWeek2 = "0";
                    tbTotalTimeWeekly2 = "0";
                    tbAvgPace2 = "0";
                }
                else
                {
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek2).ToString();
                }
                //Current week -2(3):
                if (weekNumber - 2 <= 0)
                {
                    tbDistanceWeek3 = "0";
                    tbLongestRideWeek3 = "0";
                    tbElevGainWeek3 = "0";
                    tbNumRidesWeek3 = "0";
                    tbAvgSpeedWeek3 = "0";
                    tbTotalTimeWeekly3 = "0";
                    tbAvgPace3 = "0";
                }
                else
                {
                    tbDistanceWeek3 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek3 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek3 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek3 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace3 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek3).ToString();
                }
                //Current week -3(4):
                if (weekNumber - 3 <= 0)
                {
                    tbDistanceWeek4 = "0";
                    tbLongestRideWeek4 = "0";
                    tbElevGainWeek4 = "0";
                    tbNumRidesWeek4 = "0";
                    tbAvgSpeedWeek4 = "0";
                    tbTotalTimeWeekly4 = "0";
                    tbAvgPace4 = "0";
                }
                else
                {
                    tbDistanceWeek4 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek4 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek4 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek4 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace4 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek4).ToString();
                }
                //Current week -4(5):
                if (weekNumber - 4 <= 0)
                {
                    tbDistanceWeek5 = "0";
                    tbLongestRideWeek5 = "0";
                    tbElevGainWeek5 = "0";
                    tbNumRidesWeek5 = "0";
                    tbAvgSpeedWeek5 = "0";
                    tbTotalTimeWeekly5 = "0";
                    tbAvgPace5 = "0";
                }
                else
                {
                    tbDistanceWeek5 = GetTotalMilesWeekly(logIndex, weekNumber).ToString();
                    tbLongestRideWeek5 = GetLongestRideWeekly(logIndex, weekNumber).ToString();
                    tbElevGainWeek5 = GetTotalElevGainWeekly(logIndex, weekNumber).ToString();
                    tbNumRidesWeek5 = GetTotalRidesWeekly(logIndex, weekNumber).ToString();
                    tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndex, weekNumber).ToString();
                    tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndex, weekNumber).ToString();
                    tbAvgPace5 = GetAveragePaceWeekly(logIndex, weekNumber, tbDistanceWeek5).ToString();
                }
            }
            else
            {
                //Current week -1(2):
                if (weekNumber - 1 <= 0)
                {
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                    lbweek2 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek2).ToString();
                }
                else
                {
                    int weekNumber2 = weekNumber - 1;
                    tbDistanceWeek2 = GetTotalMilesWeekly(logIndex, weekNumber2).ToString();
                    lbweek2 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber2).ToString("MM/dd/yyyy");
                    tbLongestRideWeek2 = GetLongestRideWeekly(logIndex, weekNumber2).ToString();
                    tbElevGainWeek2 = GetTotalElevGainWeekly(logIndex, weekNumber2).ToString();
                    tbNumRidesWeek2 = GetTotalRidesWeekly(logIndex, weekNumber2).ToString();
                    tbAvgSpeedWeek2 = GetAvgSpeedWeekly(logIndex, weekNumber2).ToString();
                    tbTotalTimeWeekly2 = GetTotalMovingTimeWeekly(logIndex, weekNumber2).ToString();
                    tbAvgPace2 = GetAveragePaceWeekly(logIndex, weekNumber2, tbDistanceWeek2).ToString();
                }
                //Current week -2(3):
                if (weekNumber - 2 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek3 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek3 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek3 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek3 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace3 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek3).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek3 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek3 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek3 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek3 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace3 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek3).ToString();
                    }
                }
                else
                {
                    int weekNumber3 = weekNumber - 2;
                    tbDistanceWeek3 = GetTotalMilesWeekly(logIndex, weekNumber3).ToString();
                    lbweek3 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber3).ToString("MM/dd/yyyy");
                    tbLongestRideWeek3 = GetLongestRideWeekly(logIndex, weekNumber3).ToString();
                    tbElevGainWeek3 = GetTotalElevGainWeekly(logIndex, weekNumber3).ToString();
                    tbNumRidesWeek3 = GetTotalRidesWeekly(logIndex, weekNumber3).ToString();
                    tbAvgSpeedWeek3 = GetAvgSpeedWeekly(logIndex, weekNumber3).ToString();
                    tbTotalTimeWeekly3 = GetTotalMovingTimeWeekly(logIndex, weekNumber3).ToString();
                    tbAvgPace3 = GetAveragePaceWeekly(logIndex, weekNumber3, tbDistanceWeek3).ToString();
                }
                //Current week -3(4):
                if (weekNumber - 3 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek4).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek4).ToString();
                    }
                    else if (weekNumber == 3)
                    {
                        lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek4 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek4 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek4 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek4 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace4 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek4).ToString();
                    }
                }
                else
                {
                    int weekNumber4 = weekNumber - 3;
                    tbDistanceWeek4 = GetTotalMilesWeekly(logIndex, weekNumber4).ToString();
                    tbLongestRideWeek4 = GetLongestRideWeekly(logIndex, weekNumber4).ToString();
                    lbweek4 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber4).ToString("MM/dd/yyyy");
                    tbElevGainWeek4 = GetTotalElevGainWeekly(logIndex, weekNumber4).ToString();
                    tbNumRidesWeek4 = GetTotalRidesWeekly(logIndex, weekNumber4).ToString();
                    tbAvgSpeedWeek4 = GetAvgSpeedWeekly(logIndex, weekNumber4).ToString();
                    tbTotalTimeWeekly4 = GetTotalMovingTimeWeekly(logIndex, weekNumber4).ToString();
                    tbAvgPace4 = GetAveragePaceWeekly(logIndex, weekNumber4, tbDistanceWeek4).ToString();
                }
                //Current week -4(5):
                if (weekNumber - 4 <= 0)
                {
                    if (weekNumber == 1)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 49).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 49).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 49).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 49).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 49).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 49).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 49).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 49, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 2)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 50).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 50).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 50).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 50).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 50).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 50).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 50).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 50, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 3)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 51).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 51).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 51).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 51).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 51).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 51).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 51).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 51, tbDistanceWeek5).ToString();
                    }
                    else if (weekNumber == 4)
                    {
                        lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year - 1, 52).ToString("MM/dd/yyyy");
                        tbDistanceWeek5 = GetTotalMilesWeekly(logIndexPrevious, 52).ToString();
                        tbLongestRideWeek5 = GetLongestRideWeekly(logIndexPrevious, 52).ToString();
                        tbElevGainWeek5 = GetTotalElevGainWeekly(logIndexPrevious, 52).ToString();
                        tbNumRidesWeek5 = GetTotalRidesWeekly(logIndexPrevious, 52).ToString();
                        tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndexPrevious, 52).ToString();
                        tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndexPrevious, 52).ToString();
                        tbAvgPace5 = GetAveragePaceWeekly(logIndexPrevious, 52, tbDistanceWeek5).ToString();
                    }
                }
                else
                {
                    int weekNumber5 = weekNumber - 4;
                    tbDistanceWeek5 = GetTotalMilesWeekly(logIndex, weekNumber5).ToString();
                    lbweek5 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber5).ToString("MM/dd/yyyy");
                    tbLongestRideWeek5 = GetLongestRideWeekly(logIndex, weekNumber5).ToString();
                    tbElevGainWeek5 = GetTotalElevGainWeekly(logIndex, weekNumber5).ToString();
                    tbNumRidesWeek5 = GetTotalRidesWeekly(logIndex, weekNumber5).ToString();
                    tbAvgSpeedWeek5 = GetAvgSpeedWeekly(logIndex, weekNumber5).ToString();
                    tbTotalTimeWeekly5 = GetTotalMovingTimeWeekly(logIndex, weekNumber5).ToString();
                    tbAvgPace5 = GetAveragePaceWeekly(logIndex, weekNumber5, tbDistanceWeek5).ToString();
                }
            }

            try
            {
                dataGridViewWeekly.DataSource = null;
                dataGridViewWeekly.Rows.Clear();
                dataGridViewWeekly.ColumnCount = 5;
                //dataGridViewWeekly.RowCount = 7;
                dataGridViewWeekly.Name = "Weekly Stats";
                dataGridViewWeekly.Columns[0].Name = lbweek1;
                dataGridViewWeekly.Columns[1].Name = lbweek2;
                dataGridViewWeekly.Columns[2].Name = lbweek3;
                dataGridViewWeekly.Columns[3].Name = lbweek4;
                dataGridViewWeekly.Columns[4].Name = lbweek5;

                dataGridViewWeekly.Columns[0].ValueType = typeof(double);
                dataGridViewWeekly.Columns[1].ValueType = typeof(double);
                dataGridViewWeekly.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridViewWeekly.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                //dataGridViewWeekly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
                //dataGridViewWeekly.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //dataGridViewWeekly.Columns[0].FillWeight = 20;
                //dataGridViewWeekly.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //dataGridViewWeekly.Columns[1].FillWeight = 20;
                //dataGridViewWeekly.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //dataGridViewWeekly.Columns[2].FillWeight = 20;
                //dataGridViewWeekly.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //dataGridViewWeekly.Columns[3].FillWeight = 20;
                //dataGridViewWeekly.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //dataGridViewWeekly.Columns[4].FillWeight = 20;
                dataGridViewWeekly.ReadOnly = true;
                dataGridViewWeekly.EnableHeadersVisualStyles = false;

                dataGridViewWeekly.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridViewWeekly.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewWeekly.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewWeekly.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewWeekly.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridViewWeekly.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewWeekly.AutoResizeColumns();
                dataGridViewWeekly.AllowUserToOrderColumns = false;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                //dataGridViewWeekly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewWeekly.AllowUserToAddRows = false;
                //dataGridViewRoutes.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                //dataGridViewRoutes.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridViewWeekly.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                //dataGridViewRoutes.RowHeadersVisible = false;

                dataGridViewWeekly.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewWeekly.ColumnHeadersHeight = 40;
                dataGridViewWeekly.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewWeekly.RowHeadersVisible = true;

                dataGridViewWeekly.Rows.Add(tbDistanceWeek5, tbDistanceWeek4, tbDistanceWeek3, tbDistanceWeek2, tbDistanceWeek1);
                dataGridViewWeekly.Rows.Add(tbLongestRideWeek5, tbLongestRideWeek4, tbLongestRideWeek3, tbLongestRideWeek2, tbLongestRideWeek1);
                dataGridViewWeekly.Rows.Add(tbElevGainWeek5, tbElevGainWeek4, tbElevGainWeek3, tbElevGainWeek2, tbElevGainWeek1);
                dataGridViewWeekly.Rows.Add(tbNumRidesWeek5, tbNumRidesWeek4, tbNumRidesWeek3, tbNumRidesWeek2, tbNumRidesWeek1);
                dataGridViewWeekly.Rows.Add(tbAvgSpeedWeek5, tbAvgSpeedWeek4, tbAvgSpeedWeek3, tbAvgSpeedWeek2, tbAvgSpeedWeek1);
                dataGridViewWeekly.Rows.Add(tbTotalTimeWeekly5, tbTotalTimeWeekly4, tbTotalTimeWeekly3, tbTotalTimeWeekly2, tbTotalTimeWeekly1);
                dataGridViewWeekly.Rows.Add(tbAvgPace5, tbAvgPace4, tbAvgPace3, tbAvgPace2, tbAvgPace1);

                dataGridViewWeekly.Rows[0].Height = 40;
                dataGridViewWeekly.Rows[1].Height = 40;
                dataGridViewWeekly.Rows[2].Height = 40;
                dataGridViewWeekly.Rows[3].Height = 40;
                dataGridViewWeekly.Rows[4].Height = 40;
                dataGridViewWeekly.Rows[5].Height = 40;
                dataGridViewWeekly.Rows[6].Height = 40;

                dataGridViewWeekly.Rows[0].HeaderCell.Value = "Total Miles";
                dataGridViewWeekly.Rows[1].HeaderCell.Value = "Longest Ride";
                dataGridViewWeekly.Rows[2].HeaderCell.Value = "Elev. Gain";
                dataGridViewWeekly.Rows[3].HeaderCell.Value = "Num Rides";
                dataGridViewWeekly.Rows[4].HeaderCell.Value = "Avg Speed";
                dataGridViewWeekly.Rows[5].HeaderCell.Value = "Moving Time";
                dataGridViewWeekly.Rows[6].HeaderCell.Value = "Avg Pace";

                dataGridViewWeekly.AllowUserToResizeRows = false;
                dataGridViewWeekly.AllowUserToResizeColumns = false;
                dataGridViewWeekly.CurrentCell = dataGridViewWeekly.Rows[0].Cells[4];
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Weekly Stats: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Weekly Stats.  Review the log for more information.");
            }
        }

    }
}
