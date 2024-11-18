using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Data.Common;
using System.Security.Policy;
//using System.Threading;
//using System.Text.RegularExpressions;
//using System.Runtime.Remoting.Metadata.W3cXsd2001;
//using System.Xml.Linq;
//using static System.Net.WebRequestMethods;
//using static DGVPrinterHelper.DGVPrinter;
//using System.Windows.Forms.VisualStyles;



namespace CyclingLogApplication
{
    public partial class MainForm : Form
    {
        //private static Mutex mutex = null;
        Boolean formloading = false;

        private static string logVersion = "0.9.3";
        private static int logLevel = 0;
        private static string gridOrder;
        private static int lastLogSelected = 0;
        private static int lastBikeSelected = 0;
        private static int lastLogFilterSelected = 0;
        private static int lastLogYearChart = 0;
        private static int lastRouteChart = 0;
        private static int lastTypeChart = 0;
        private static int lastTypeTimeChart = 0;
        private static int lastMonthlyLogSelected = 0;
        private static int lastLogYearWeekly = 0;
        private static int lastLogSelectedDataEntry = 0;
        private static string idColumn = "0";
        private static string firstDayOfWeek;
        private static string license;
        public static string customField1;
        public static string customField2;
        private static int heightCLB;
        private static string gridMaintColor;
        private static string gridWeeklyColor;
        private static string gridMonthlyColor;
        private static string gridYearlyColor;
        private static string gridDisplayDataColor;
        private static string gridBikeColor;
        private static string gridRouteColor;
        private static string gridCalendarColor;

        private static string gridMaintTextColor;
        private static string gridWeeklyTextColor;
        private static string gridMonthlyTextColor;
        private static string gridYearlyTextColor;
        private static string gridDisplayTextColor;
        private static string gridBikeTextColor;
        private static string gridRouteTextColor;
        private static string gridCalendarTextColor;

        private static Dictionary<string, string> fieldNameDict = new Dictionary<string, string>();
        private static Dictionary<string, string> logNameIDDict = new Dictionary<string, string>();
        private static List<string> fieldNamesList = new List<string>();
        private static List<string> routeNamesList = new List<string>();

        private static SqlConnection sqlConnection;             // = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=False");
        private static DatabaseConnection databaseConnection;   // = new DatabaseConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=False");

        //connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\workdir\Cycling_Log\CyclingLogApplication\CyclingLogDatabase.mdf;Integrated Security=False"
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

            tbWeekCount.Text = GetCurrentWeekCount().ToString(); //For current year only:
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
                
                ConfigurationFile configfile = new ConfigurationFile();
                ConfigurationFile.ReadConfigFile();

                SetLogVersion(logVersion);

                if (GetLicenseAgreement() == null || GetLicenseAgreement().Equals("false"))
                {
                    DialogResult result = MessageBox.Show("Do you agree with the License Agreement?\n\nThe MIT License Copyright (c) 2024, John T Flynn Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the Software), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.", "License Agreement", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        SetLicenseAgreement("true");
                        ConfigurationFile.WriteConfigFile();
                    }
                }

                tabControl1.SelectedTab = tabControl1.TabPages["Main"];
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

                string idColumn = GetIDColumnValue();
                if (idColumn.Equals("0"))
                {
                    rbShowIDColumn.Checked = false;
                    rbHideIDColumn.Checked = true;
                }
                else
                {
                    rbShowIDColumn.Checked = true;
                    rbHideIDColumn.Checked = false;
                }

                if (GetTextMaint().Equals("True"))
                {
                    cbMaintTextColor.Checked = true;
                    tbColorMaint.ForeColor = Color.Black;
                }
                else
                {
                    cbMaintTextColor.Checked = false;
                    tbColorMaint.ForeColor = Color.White;
                }

                if (GetTextWeekly().Equals("True"))
                {
                    cbWeeklyTextColor.Checked = true;
                    tbColorWeekly.ForeColor = Color.Black;
                }
                else
                {
                    cbWeeklyTextColor.Checked = false;
                    tbColorWeekly.ForeColor = Color.White;
                }

                if (GetTextMonthly().Equals("True"))
                {
                    cbMonthlyTextColor.Checked = true;
                    tbColorMonthly.ForeColor = Color.Black;
                }
                else
                {
                    cbMonthlyTextColor.Checked = false;
                    tbColorMonthly.ForeColor = Color.White;
                }

                if (GetTextYearly().Equals("True"))
                {
                    cbYearlyTextColor.Checked = true;
                    tbColorYearly.ForeColor = Color.Black;
                }
                else
                {
                    cbYearlyTextColor.Checked = false;
                    tbColorYearly.ForeColor = Color.White;
                }

                if (GetTextDisplay().Equals("True"))
                {
                    cbDisplayDataTextColor.Checked = true;
                    tbColorDisplayData.ForeColor = Color.Black;
                }
                else
                {
                    cbDisplayDataTextColor.Checked = false;
                    tbColorDisplayData.ForeColor = Color.White;
                }

                if (GetTextBike().Equals("True"))
                {
                    cbBikeTextColor.Checked = true;
                    tbBikeColor.ForeColor = Color.Black;
                }
                else
                {
                    cbBikeTextColor.Checked = false;
                    tbBikeColor.ForeColor = Color.White;
                }

                if (GetTextRoute().Equals("True"))
                {
                    cbRouteTextColor.Checked = true;
                    tbRouteColor.ForeColor = Color.Black;
                }
                else
                {
                    cbRouteTextColor.Checked = false;
                    tbRouteColor.ForeColor = Color.White;
                }

                if (GetTextCalendar().Equals("True"))
                {
                    cbCalendarTextColor.Checked = true;
                    tbCalendarColor.ForeColor = Color.Black;
                }
                else
                {
                    cbCalendarTextColor.Checked = false;
                    tbCalendarColor.ForeColor = Color.White;
                }

                //Get all values and load the comboboxes:
                List<string> logYearList = ReadDataNamesDESC("Table_Log_year", "Name");
                List<string> routeList = ReadDataNames("Table_Routes", "Name");
                SetRoutes(routeList);
                List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

                SetLogNameIDDictionary(logYearList);
                ChartForm chartForm = new ChartForm();

                cbLogYearConfig.Items.Add("--Select Value--");

                RideDataEntry rideDataEntryForm = new RideDataEntry();

                //Load LogYear values:
                foreach (string val in logYearList)
                {
                    cbLogYearConfig.Items.Add(val);
                    Logger.Log("Data Loading: Log Year: " + val, logSetting, 1);
                }

                cbLogYearConfig.SelectedIndex = 0;
                cbBikeMaint.Items.Add("--Select Value--");

                //Load Bike values:
                foreach (var val in bikeList)
                {
                    cbBikeMaint.Items.Add(val);
                    Logger.Log("Data Loading: Bikes: " + val, logSetting, 1);
                }

                cbBikeMaint.SelectedIndex = 0;

                int currentYear = DateTime.Now.Year;

                cbLogYear.Items.Add("--Select Value--");
                //cbLogYear
                for (int i = 2010; i <= currentYear; i++)
                {
                    cbLogYear.Items.Add(i.ToString());
                }
                cbLogYear.SelectedIndex = 0;                

                cbMaintColors.SelectedIndex = cbMaintColors.FindStringExact(gridMaintColor);
                cbWeeklyColors.SelectedIndex = cbWeeklyColors.FindStringExact(gridWeeklyColor);
                cbMonthlyColors.SelectedIndex = cbMonthlyColors.FindStringExact(gridMonthlyColor);
                cbYearlyColors.SelectedIndex = cbYearlyColors.FindStringExact(gridYearlyColor);
                cbDisplayDataColors.SelectedIndex = cbDisplayDataColors.FindStringExact(gridDisplayDataColor);
                cbBikeColors.SelectedIndex = cbBikeColors.FindStringExact(gridBikeColor);
                cbRouteColors.SelectedIndex = cbRouteColors.FindStringExact(gridRouteColor);
                cbCalendarColors.SelectedIndex = cbCalendarColors.FindStringExact(gridCalendarColor);

                formloading = true;

                cbStatMonthlyLogYear.Items.Add("--Select Value--");
                cbLogYearWeekly.Items.Add("--Select Value--");
                //Load LogYear Monthly values:
                foreach (string val in logYearList)
                {
                    cbStatMonthlyLogYear.Items.Add(val);
                    cbLogYearWeekly.Items.Add(val);
                }

                cbStatMonthlyLogYear.SelectedIndex = GetLastMonthlyLogSelected();
                cbLogYearWeekly.SelectedIndex = GetLastLogYearWeeklySelected();

                RefreshRoutes();

                cbCalendarLogs.Items.Add("--Select Value--");
                if (logYearList.Count > 0)
                {
                    string logName = GetLogNameByYear(DateTime.Now.Year);
                    cbCalendarLogs.SelectedIndex = cbCalendarLogs.Items.IndexOf(logName);
                    string sMonth = DateTime.Now.ToString("MM");
                    cbCalendarMonth.SelectedIndex = int.Parse(sMonth);
                    lbMonth.Text = cbCalendarMonth.SelectedItem.ToString();
                }
                else
                {
                    cbCalendarLogs.SelectedIndex = 0;
                    cbCalendarMonth.SelectedIndex = 0;
                }

                if (logYearList.Count == 0)
                {
                    MessageBox.Show("No Yearly Logs have been added to the database. Please add an entry before continuing.");
                    tabControl1.SelectedTab = tabControl1.TabPages["Settings"];
                    formloading = false;
                    return;
                }

                if (routeList.Count == 0)
                {
                    MessageBox.Show("No Routes have been created yet.");
                    tabControl1.SelectedTab = tabControl1.TabPages["Routes"];
                    formloading = false;
                    return;
                }

                if (bikeList.Count == 0)
                {
                    MessageBox.Show("No Bikes have been added yet. Please add an entry for a Bike.");
                    tabControl1.SelectedTab = tabControl1.TabPages["Bikes"];
                    formloading = false;
                    return;
                }

                RunYearlyStatisticsGrid();
                if (cbStatMonthlyLogYear.SelectedIndex > 0)
                {
                    int logYearIndex = GetLogYearIndex_ByName(cbStatMonthlyLogYear.SelectedItem.ToString());
                    RunMonthlyStatisticsGrid(logYearIndex);
                }

                foreach (string val in logYearList)
                {
                    cbCalendarLogs.Items.Add(val);
                    Logger.Log("Data Loading: Log Year: " + val, logSetting, 1);
                }

                //int calLogIndex = GetLogIndexByYear(DateTime.Now.Year);
                

                
                
                tbCustomDataField1.Text = GetCustomField1();
                tbCustomDataField2.Text = GetCustomField2();

                RefreshWeekly();
                RefreshBikes();
                GetMaintLog();
                UpdateStatsAllLogs(); //Main page stats:
                RunCalendar();

                tbMaintAddUpdate.Text = "Add";
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to load Main form. " + ex.Message.ToString());
            }

            formloading = false;
        }

        private void CloseForm(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ConfigurationFile.WriteConfigFile();
                //this.Dispose();
                Application.Exit();
            }
        }

        static void GetConnectionStrings()
        {
            string conStr = ConfigurationManager.ConnectionStrings["CyclingLogApplication.Properties.Settings.CyclingLogDatabaseConnectionString"].ConnectionString;
            //Logger.Log("conStr Name: " + conStr, 1, 1);
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    if (cs.Name.Equals("CyclingLogApplication.Properties.Settings.CyclingLogDatabaseConnectionString"))
                    {
                        //Logger.Log("ConnectionStringSettingsCollection Name: " + cs.Name, 1, 1);
                        //Logger.Log("ConnectionStringSettingsCollection ProviderName: " + cs.ProviderName, 1, 1);
                        //Logger.Log("ConnectionStringSettingsCollection ConnectionString: " + cs.ConnectionString, 1, 1);

                        sqlConnection = new SqlConnection(cs.ConnectionString);
                        databaseConnection = new DatabaseConnection(cs.ConnectionString);

                        break;
                    }
                }
            }
        }

        public static SqlConnection GetsqlConnectionString()
        {
            return sqlConnection;
        }

        public static DatabaseConnection GetsDatabaseConnectionString()
        {
            return databaseConnection;
        }

        //private Mutex GetMutex()
        //{
        //    return mutex;
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //        mutex.ReleaseMutex();
        //    base.Dispose(disposing);
        //}
        public static void SetTextMaint(string text)
        {
            gridMaintTextColor = text;
        }

        public static void SetTextWeekly(string text)
        {
            gridWeeklyTextColor = text;
        }

        public static void SetTextMonthly(string text)
        {
            gridMonthlyTextColor = text;
        }

        public static void SetTextYearly(string text)
        {
            gridYearlyTextColor = text;
        }

        public static void SetTextDisplay(string text)
        {
            gridDisplayTextColor = text;
        }

        public static void SetTextBike(string text)
        {
            gridBikeTextColor = text;
        }

        public static void SetTextRoute(string text)
        {
            gridRouteTextColor = text;
        }

        public static void SetTextCalendar(string text)
        {
            gridCalendarTextColor = text;
        }

        public static string GetTextMaint()
        {
            return gridMaintTextColor;
        }

        public static string GetTextWeekly()
        {
            return gridWeeklyTextColor;
        }

        public static string GetTextMonthly()
        {
            return gridMonthlyTextColor;
        }

        public static string GetTextYearly()
        {
            return gridYearlyTextColor;
        }

        public static string GetTextDisplay()
        {
            return gridDisplayTextColor;
        }

        public static string GetTextBike()
        {
            return gridBikeTextColor;
        }

        public static string GetTextRoute()
        {
            return gridRouteTextColor;
        }

        public static string GetTextCalendar()
        {
            return gridCalendarTextColor;
        }

        public static void SetMaintColor(string color)
        {
            gridMaintColor = color;
        }

        public static string GetMaintColor()
        {
            return gridMaintColor;
        }

        public static void SetWeeklyColor(string color)
        {
            gridWeeklyColor = color;
        }

        public static string GetMonthlyColor()
        {
            return gridMonthlyColor;
        }

        public static void SetMonthlyColor(string color)
        {
            gridMonthlyColor = color;
        }

        public static string GetYearlyColor()
        {
            return gridYearlyColor;
        }

        public static void SetYearlyColor(string color)
        {
            gridYearlyColor = color;
        }

        public static string GetWeeklyColor()
        {
            return gridWeeklyColor;
        }

        public static void SetDisplayDataColor(string color)
        {
            gridDisplayDataColor = color;
        }

        public static string GetDisplayDataColor()
        {
            return gridDisplayDataColor;
        }

        public static void SetBikeColor(string color)
        {
            gridBikeColor = color;
        }

        public static string GetBikeColor()
        {
            return gridBikeColor;
        }

        public static void SetRouteColor(string color)
        {
            gridRouteColor = color;
        }

        public static string GetRouteColor()
        {
            return gridRouteColor;
        }

        public static void SetCalendarColor(string color)
        {
            gridCalendarColor = color;
        }

        public static string GetCalendarColor()
        {
            return gridCalendarColor;
        }

        public static void SetHeightCLB(int heightCLBInt)
        {
            heightCLB = heightCLBInt;
        }

        public static int GetHeightCLB()
        {
            return heightCLB;
        }

        public static string GetLogVersion()
        {
            return logVersion;
        }

        public static void SetLogVersion(string version)
        {
            logVersion = version;
        }

        public static string GetGridOrder()
        {
            return gridOrder;
        }

        public static void SetGridOrder(string gridOrderString)
        {
            gridOrder = gridOrderString;
        }

        public static string GetIDColumnValue()
        {
            return idColumn;
        }

        public static void SetIDColumn(string idColumnValue)
        { 
            idColumn = idColumnValue; 
        }    

        public static int GetLogLevel()
        {
            return logLevel;
        }

        public static void SetLogLevel(int logLevelFromConfig)
        {
            logLevel = logLevelFromConfig;
        }
        public static void SetLicenseAgreement(string licenseAgreement)
        {
            license = licenseAgreement;
        }

        public static int GetLastBikeSelected()
        {
            return lastBikeSelected;
        }

        public static void SetLastBikeSelected(int bikeIndex)
        {
            lastBikeSelected = bikeIndex;
        }

        public static int GetLastLogSelected()
        {
            return lastLogSelected;
        }

        public static void SetLastLogSelected(int logIndex)
        {
            lastLogSelected = logIndex;
        }

        public static int GetLastMonthlyLogSelected()
        {
            return lastMonthlyLogSelected;
        }

        public static void SetLastMonthlyLogSelected(int logIndex)
        {
            lastMonthlyLogSelected = logIndex;
        }

        public static int GetLastLogSelectedDataEntry()
        {
            return lastLogSelectedDataEntry;
        }

        public static void SetLastLogSelectedDataEntry(int logIndex)
        {
            lastLogSelectedDataEntry = logIndex;
        }

        public static void SetLastLogFilterSelected(int logIndex)
        {
            lastLogFilterSelected = logIndex;
        }

        public static int GetLastLogFilterSelected()
        {
            return lastLogFilterSelected;
        }

        public static void SetLastLogYearChartSelected(int logIndex)
        {
            lastLogYearChart = logIndex;
        }

        public static int GetLastLogYearChartSelected()
        {
            return lastLogYearChart;
        }

        public static int GetLastLogYearWeeklySelected()
        {
            return lastLogYearWeekly;
        }

        public static void SetLastLogYearWeeklySelected(int logIndex)
        {
            lastLogYearWeekly = logIndex;
        }

        public static void SetLastRouteChartSelected(int logIndex)
        {
            lastRouteChart = logIndex;
        }

        public static int GetLastRouteChartSelected()
        {
            return lastRouteChart;
        }

        public static void SetLastTypeChartSelected(int logIndex)
        {
            lastTypeChart = logIndex;
        }

        public static int GetLastTypeChartSelected()
        {
            return lastTypeChart;
        }

        public static void SetLastTypeTimeChartSelected(int logIndex)
        {
            lastTypeTimeChart = logIndex;
        }

        public static int GetLastTypeTimeChartSelected()
        {
            return lastTypeTimeChart;
        }

        public static string GetFirstDayOfWeek()
        {
            return firstDayOfWeek;
        }

        public static string GetLicenseAgreement()
        {
            return license;
        }

        public static string GetCustomField1()
        {
            return customField1;
        }

        public static string GetCustomField2()
        {
            return customField2;
        }

        public static void SetFieldDictionary(Dictionary<string, string> fieldNames)
        {
            //Clear out current values:
            fieldNameDict.Clear();

            for (int i = 0; i < fieldNames.Count; i++)
            {
                fieldNameDict.Add(fieldNames.Keys.ElementAt(i), fieldNames.Values.ElementAt(i));
            }
        }

        public static void SetLogNameIDDictionary(List<string> logYearsList)
        {
            //List<string> logYearsList = MainForm.ReadDataNames("Table_Log_year", "Name");

            //Clear out current values:
            logNameIDDict.Clear();

            for (int i = 0; i < logYearsList.Count; i++)
            {
                List<object> objectValues = new List<object>
                {
                    logYearsList[i]
                };

                int logIndex = 0;

                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index_Name", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            logIndex = int.Parse(results[0].ToString());
                            logNameIDDict.Add(logYearsList[i], logIndex.ToString());
                        }
                    }
                }
            }
        }

        public static Dictionary<string, string> GetLogNameIDDictionary()
        {
            return logNameIDDict;
        }

        public static Dictionary<string, string> GetFieldsDictionary()
        {
            return fieldNameDict;
        }

        public static void SetFirstDayOfWeek(string firstdayString)
        {
            firstDayOfWeek = firstdayString;
        }

        public static void SetCustomField1(string customDataField1)
        {
            customField1 = customDataField1;
        }

        public static void SetCustomField2(string customDataField2)
        {
            customField2 = customDataField2;
        }

        public static List<string> GetLogYears()
        {
            MainForm mainform = new MainForm();
            List<string> logYearsList = MainForm.ReadDataNamesDESC("Table_Log_year", "Name");

            for (int i = 0; i < mainform.cbLogYearConfig.Items.Count; i++)
            {
                logYearsList.Add(logYearsList[i]);
            }

            return logYearsList;
        }

        public static List<string> GetRoutes()
        {
            return routeNamesList;
        }

        public static void SetRoutes(List<string> routeList)
        {
            routeNamesList.Clear();

            for (int i = 0; i < routeList.Count; i++)
            {
                routeNamesList.Add(routeList[i]);
            }

        }

        public static void removeRoute(string route)
        {
            routeNamesList.Remove(route);
        }

        public static List<string> ReadDataNames(string tableName, string columnName)
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
                    reader = cmd.ExecuteReader();
                }

                // write each record
                while (reader.Read())
                {
                    string returnValue = reader[0].ToString();
                    nameList.Add(returnValue);
                    Logger.Log("Reading data from the database: columnName:" + returnValue, logSetting, 1);
                }
            }
            finally
            {
                // close reader
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return nameList;
        }

        public static List<string> ReadDataNamesDESC(string tableName, string columnName)
        {
            SqlDataReader reader = null;
            List<string> nameList = new List<string>();
            int logSetting = GetLogLevel();

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("SELECT " + columnName + " FROM " + tableName + " ORDER BY " + columnName + " DESC", sqlConnection))
                {
                    reader = cmd.ExecuteReader();
                }

                // write each record
                while (reader.Read())
                {
                    string returnValue = reader[0].ToString();
                    nameList.Add(returnValue);
                    Logger.Log("Reading data from the database: columnName:" + returnValue, logSetting, 1);
                }
            }
            finally
            {
                // close reader
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return nameList;
        }

        private void LogTitleSave()
        {
            string logYearTitle = tbLogYearConfig.Text;
            string logType = "Add";

            if (logYearTitle.Equals(""))
            {
                MessageBox.Show("No title entered. Enter a unique name for the log.");
                return;
            }

            //Needs to be selected for new or updated entry:
            if (cbLogYear.SelectedIndex < 1)
            {
                MessageBox.Show("Log Year not selected. Select a year from the dropdown list.");
                return;
            }

            //Get list of Log Titles to determine if the title already exists:
            List<string> logList = ReadDataNames("Table_Log_year", "Name");

            if (logList.Contains(logYearTitle) || cbLogYearConfig.SelectedIndex > 0)
            {
                logType = "Update";
                DialogResult result = MessageBox.Show("Do you really want to update the Log Title?", "Update Log", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else
                {
                    return;
                }
            } else
            { 
                logType = "Add";
                DialogResult result = MessageBox.Show("Do you really want to Add the Log Title?", "Add Log", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                }
                else
                {
                    return;
                }
            }

            string logYear = cbLogYear.SelectedItem.ToString();
            int logSetting = GetLogLevel();

            // New Log created:
            if (logType.Equals("Add"))
            {
                //Add new entry to the LogYear Table:
                List<object> objectValues = new List<object>();
                objectValues.Add(logYearTitle);
                objectValues.Add(Convert.ToInt32(logYear));
                RunStoredProcedure(objectValues, "Log_Year_Add");

                cbLogYearConfig.Items.Add(logYearTitle);
                cbLogYearConfig.SelectedIndex = cbLogYearConfig.Items.Count - 1;
                cbStatMonthlyLogYear.Items.Add(logYearTitle);
                cbLogYearWeekly.Items.Add(logYearTitle);
                cbCalendarLogs.Items.Add(logYearTitle);
            }
            // Update to an existing log:
            else
            {
                string newValue = logYearTitle;
                string oldValue;

                // check if a title has been selected from the combobox:
                if (cbLogYearConfig.SelectedIndex < 1)
                {
                    oldValue = logYearTitle;
                } 
                else
                {
                    oldValue = cbLogYearConfig.SelectedItem.ToString();
                }

                List<object> objectValues = new List<object>();
                objectValues.Add(newValue);
                objectValues.Add(oldValue);
                objectValues.Add(logYear);

                RunStoredProcedure(objectValues, "Log_Year_Update");

                List<string> tempList = new List<string>();

                int cbLogYearConfigIndex = cbLogYearConfig.SelectedIndex;
                int cbStatMonthlyLogYearIndex = cbStatMonthlyLogYear.SelectedIndex;

                // Skip first item:
                for (int i = 1; i < cbLogYearConfig.Items.Count; i++)
                {
                    tempList.Add(cbLogYearConfig.Items[i].ToString());
                }

                for (int i = 0; i < tempList.Count; i++)
                {
                    // -1 since do not want to include the --not select-- option:
                    if (cbLogYearConfigIndex -1 == i)
                    {
                        cbLogYearConfig.Items.Remove(oldValue);
                        cbLogYearConfig.Items.Add(newValue);
                        cbStatMonthlyLogYear.Items.Remove(oldValue);
                        cbStatMonthlyLogYear.Items.Add(newValue);

                        break;
                    }

                }

                cbLogYearConfig.Sorted = true;
                cbStatMonthlyLogYear.Sorted = true;
                cbLogYearConfig.SelectedIndex = cbLogYearConfigIndex;
                cbStatMonthlyLogYear.SelectedIndex = cbStatMonthlyLogYearIndex;
            }

            List<string> namesList = new List<string>();    
            for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
            {
                namesList.Add(cbLogYearConfig.Items[i].ToString());
            }

            SetLogNameIDDictionary(namesList);
            MessageBox.Show("Log Title save is complete.");
        }
            
        private void BtAddLogYearConfig(object sender, EventArgs e)
        {
            string logYearTitle;
            RideDataEntry rideDataEntryForm = new RideDataEntry();

            if (!tbLogYearConfig.Text.Equals(""))
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

            if (cbLogYear.SelectedIndex < 1)
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
            ChartForm chartForm = new ChartForm();
            cbLogYearConfig.Items.Add(logYearTitle);
            cbLogYearConfig.SelectedIndex = cbLogYearConfig.Items.Count - 1;
            rideDataEntryForm.AddLogYearDataEntry(logYearTitle);
            rideDataDisplayForm.AddLogYearFilter(logYearTitle);
            chartForm.cbLogYearChart.Items.Add(logYearTitle);
            cbStatMonthlyLogYear.Items.Add(logYearTitle);

            Logger.Log("Adding a Log Year entry to the Configuration:" + logYearTitle, logSetting, 0);
        }

        private void RemoveLogYearConfig(object sender, EventArgs e)
        {
            if (tbLogYearConfig.Text.Equals(""))
            {
                MessageBox.Show("No Log Title entered. Select a Title from the dropdown list.");
                return;
            }
            //Get list of Log Titles to determine if the title already exists:
            List<string> logList = ReadDataNames("Table_Log_year", "Name");
            string logName = tbLogYearConfig.Text;

            if (!logList.Contains(logName))
            {
                MessageBox.Show("The entered log Title does not exist.  Select a title from the dropdown list.");
                return;
            }

            DialogResult result = MessageBox.Show("Do you really want to delete the Log and all its data?", "Delete Log", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                int logYearIndex = GetLogYearIndex_ByName(logName);
                RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm();
                string deleteLogName = tbLogYearConfig.Text;
                int deleteLogIndex = cbLogYearConfig.Items.IndexOf(deleteLogName);

                //Get initial Log comboboxes index:
                int chartIndex = chartForm.cbLogYearChart.SelectedIndex;
                int rideDataEntry = rideDataEntryForm.cbLogYearDataEntry.SelectedIndex;
                int rideDisplay = rideDataDisplayForm.cbLogYearFilter.SelectedIndex;
                int monthStat = cbStatMonthlyLogYear.SelectedIndex;

                cbLogYearConfig.Items.Remove(logName);
                cbStatMonthlyLogYear.Items.Remove(logName);
                cbLogYearWeekly.Items.Remove(logName);
                cbCalendarLogs.Items.Remove(logName);

                //Remove logyear from the Log year table:
                List<object> objectValues = new List<object>();
                objectValues.Add(logYearIndex);
                RunStoredProcedure(objectValues, "Log_Year_Remove");

                //Need to remove all data for this log from the database:
                RemoveLogYearData(logYearIndex);
                cbLogYearConfig.SelectedIndex = 0;
                tbLogYearConfig.Text = "";

                //Update log comboboxes index to 0 that match the one being deleted:
                if (chartIndex == deleteLogIndex)
                {
                    chartForm.cbLogYearChart.SelectedIndex = 0;
                }
                if (rideDataEntry == deleteLogIndex)
                {
                    rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = 0;
                }
                if (rideDisplay == deleteLogIndex)
                {
                    rideDataDisplayForm.cbLogYearFilter.SelectedIndex = 0;
                }
                
                if (monthStat == deleteLogIndex)
                {
                    cbStatMonthlyLogYear.SelectedIndex = 0;
                }
            }

            List<string> namesList = new List<string>();
            for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
            {
                namesList.Add(cbLogYearConfig.Items[i].ToString());
            }

            SetLogNameIDDictionary(namesList);
            MessageBox.Show("Log Title removal is complete.");
        }

        public static int GetLogYearIndex_ByName(string logYearName)
        {
            int logYearIndex = 0;
            Dictionary<string, string> nameIDdict = GetLogNameIDDictionary();

            for (int i = 0; i < nameIDdict.Count; i++)
            {
                string logName = nameIDdict.Keys.ElementAt(i);
                //string keyValue = nameIDdict.Keys.ElementAt(i);
                if (logName.Equals(logYearName))
                {
                    string test = nameIDdict.Values.ElementAt(i);
                    logYearIndex = int.Parse(nameIDdict.Values.ElementAt(i)); 
                    break;
                }
            }

            return logYearIndex;
        }

        public static int GetLogYearByIndex(int logYearIndex)
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
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return returnValue;
        }

        private static int RunStoredProcedure(List<object> objectValues, string procedureName)
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

        //Remove all records that match the LogYearID:
        private static void RemoveLogYearData(int logIndex)
        {
            List<object> objectValues = new List<object>
            {
                logIndex
            };

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("DeleteLogTitleFromRideInformation", objectValues))
            {
            }
        }

        private void OpenRideDataForm(object sender, EventArgs e)
        {
            RideDataDisplay rideDataDisplayForm = new RideDataDisplay();

            //rideDataDisplayForm.SetCustomValues();
            rideDataDisplayForm.SetLogYearFilterIndex(GetLastLogFilterSelected());
            rideDataDisplayForm.ShowDialog();
        }

        private void OpenRideDataEntry(object sender, EventArgs e)
        {
            //Need to check that there is a least 1 LogYear value entered:
            if (cbLogYearConfig.Items.Count <= 1)
            {
                MessageBox.Show("You must add at least 1 log Entry before entering data.  Add a new Log Year entry in the Settings tab.");
            }
            else
            {
                // Should not be needed since the Misc route entry is added by default:
                //if (routeNamesList.Count 0)
                //{
                //    //Give a warning if no additional routed have been entered:
                //    MessageBox.Show("Reminder: No Routes have been entered. Add a new Route in the Settings tab.");
                //}
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                rideDataEntryForm.cbBikeDataEntrySelection.SelectedIndex = Convert.ToInt32(GetLastBikeSelected());
                rideDataEntryForm.ShowDialog();
            }

            //thread = new Thread(openRideDataEntryForm);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
        }

        private void BTRouteSave_Click(object sender, EventArgs e)
        {
            string routeName = tbRouteConfig.Text;
            string routeOldName = tbRouteOldName.Text;
            List<string> routeList = GetRoutes();
            string routeType = "Add";

            if (tbRouteConfig.Text.Equals(""))
            {
                MessageBox.Show("A Route name must be entered into the field.");
                return;
            }

            if (tbRouteOldName.Text.Equals(""))
            {
                routeOldName = tbRouteConfig.Text;
            }

            //Check to see if the string has already been entered to eliminate duplicates:
            if (routeList.Contains(routeOldName))
            {
                DialogResult result = MessageBox.Show("The Route already exists. Do you want to continue with the update?", "Update Route", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    routeType = "Update";
                }
                else
                {
                    return;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("The Route does not exist. Do you want to continue with the adding the Route?", "Add Route", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    routeType = "Add";
                }
                else
                {
                    return;
                }
            }

            // Add new route:
            if (routeType.Equals("Add"))
            {
                List<object> objectValues = new List<object>();
                objectValues.Add(routeName);
                RunStoredProcedure(objectValues, "Route_Add");
            }
            // Update an existing Route:
            else
            {
                List<object> objectValues = new List<object>();
                objectValues.Add(routeName);
                objectValues.Add(routeOldName);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Route_Update", objectValues))
                    {

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to update Route." + ex.Message.ToString());
                }

                //Update the route name in the database for each row:
                try
                {
                    sqlConnection.Open();

                    // declare command object with parameter
                    using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET Route=@NewValue WHERE [Route]=@OldValue", sqlConnection))
                    {
                        SqlDataReader reader = null;
                        cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = routeName;
                        cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = routeOldName;

                        // get data stream
                        reader = cmd.ExecuteReader();
                    }
                }
                finally
                {
                    // close connection
                    sqlConnection?.Close();
                }

                tbRouteOldName.Text = routeName;
            }

            //Update Routes list:
            SetRoutes(ReadDataNames("Table_Routes", "Name"));
            RefreshRoutes();
        }

        private void AddDefautRoute()
        {
            RideDataEntry rideDataEntryForm = new RideDataEntry();
            ChartForm chartForm = new ChartForm();
            string routeString1 = "--Select Value--";
            string routeString2 = "Miscellaneous Route";
            string routeString3 = "*** Indoor Training ***";
            int logSetting = GetLogLevel();

            Logger.Log("Adding a Route entry:  --Select Value--:", 0, 0);
            Logger.Log("Adding a Route entry:  Miscellaneous Route:", 0, 0);
            Logger.Log("Adding a Route entry:  *** Indoor Training ***:", 0, 0);

            //Route1:
            List<object> objectValues1 = new List<object>
            {
                routeString1
            };
            RunStoredProcedure(objectValues1, "Route_Add");

            //Route2:
            List<object> objectValues2 = new List<object>
            {
                routeString2
            };
            RunStoredProcedure(objectValues2, "Route_Add");
            
            //Route3:
            List<object> objectValues3 = new List<object>
            {
                routeString3
            };
            RunStoredProcedure(objectValues3, "Route_Add");



            rideDataEntryForm.AddRouteDataEntry(routeString1);
            rideDataEntryForm.AddRouteDataEntry(routeString2);
            rideDataEntryForm.AddRouteDataEntry(routeString3);

            chartForm.cbRoutesChart.Items.Add(routeString1);
            chartForm.cbRoutesChart.Items.Add(routeString2);
            chartForm.cbRoutesChart.Items.Add(routeString3);

            SetRoutes(ReadDataNames("Table_Routes", "Name"));
            RefreshRoutes();
        }

        private void BtRemoveRouteConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Route option?", "Delete Route Option", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string deleteValue = tbRouteConfig.Text;

                //Remove the Route from the database table:
                List<object> objectValues = new List<object>();
                objectValues.Add(deleteValue);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Route_Remove", objectValues))
                    {

                    }

                    removeRoute(deleteValue);
                    RefreshRoutes();
                    tbRouteOldName.Text = "";
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete the Route name entry." + ex.Message.ToString());
                }
            }
        }

        private void BtSaveBikeConfig_Click(object sender, EventArgs e)
        {
            string bikeOldName = tbBikeOldName.Text;
            string bikeName = tbBikeConfig.Text;
            float notInMiles = float.Parse(tbConfigMilesNotInLog.Text);
            float logMiles = 0;
            float totalMiles = 0;
            string bikeType = "Add";

            if (tbBikeConfig.Text.Equals(""))
            {
                MessageBox.Show("A Bike name must be entered into the field.");
                return;
            }

            if (tbBikeOldName.Text.Equals(""))
            {
                bikeOldName = tbBikeConfig.Text;
            }

            //Verify Miles is entered and in the correct format:
            if (!float.TryParse(tbConfigMilesNotInLog.Text.ToString(), out _))
            {
                MessageBox.Show("The miles for the Bike must be in numeric format. Enter 0 if unknown.");
                return;
            }

            List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

            //Check to see if the string has already been entered to eliminate duplicates:
            if (bikeList.Contains(bikeOldName))
            {
                DialogResult result = MessageBox.Show("The bike already exists. Do you want to continue with the update?", "Update Bike", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bikeType = "Update";
                } else
                {
                    return;
                }
            } else
            {
                DialogResult result = MessageBox.Show("The bike does not exist. Do you want to continue with the adding the bike?", "Add Bike", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bikeType = "Add";
                }
                else
                {
                    return;
                }
            }

            List<object> objectBikes = new List<object>();           

            // Add a new bike:
            if (bikeType.Equals("Add"))
            {
                cbBikeMaint.Items.Add(bikeName);
                totalMiles = notInMiles + logMiles;

                List<object> objectBikesTotals = new List<object>();
                objectBikesTotals.Add(bikeName);
                objectBikesTotals.Add(notInMiles);
                objectBikesTotals.Add(logMiles);    //should be 0
                objectBikesTotals.Add(totalMiles);  //should be the same as notinmiles
                RunStoredProcedure(objectBikesTotals, "Bike_Totals_Add");
            } 
            // Update an existing bike:
            else
            {
                logMiles = float.Parse(tbBikeLogMiles.Text);
                totalMiles = notInMiles + logMiles;

                List<object> objectBikeTotals = new List<object>();
                objectBikeTotals.Add(bikeName);
                objectBikeTotals.Add(bikeOldName);
                objectBikeTotals.Add(notInMiles);
                objectBikeTotals.Add(logMiles);
                objectBikeTotals.Add(totalMiles);
                RunStoredProcedure(objectBikeTotals, "Bike_Totals_Update");

                int bikeIndex = cbBikeMaint.Items.IndexOf(bikeOldName);
                cbBikeMaint.Items.RemoveAt(bikeIndex);
                cbBikeMaint.Items.Add(bikeName);
                cbBikeMaint.Sorted = true;

                // Check if the bike names have changed:
                if (!bikeOldName.Equals(bikeName))
                {
                    //Update the route name in the database for each row:
                    try
                    {
                        sqlConnection.Open();

                        // declare command object with parameter
                        using (SqlCommand cmd = new SqlCommand("UPDATE Table_Ride_Information SET Bike=@NewValue WHERE [Bike]=@OldValue", sqlConnection))
                        {
                            SqlDataReader reader = null;
                            cmd.Parameters.Add("@NewValue", SqlDbType.NVarChar).Value = bikeName;
                            cmd.Parameters.Add("@OldValue", SqlDbType.NVarChar).Value = bikeOldName;

                            // get data stream
                            reader = cmd.ExecuteReader();
                        }
                    }
                    finally
                    {
                        // close connection
                        sqlConnection?.Close();
                    }
                }

                tbBikeOldName.Text = bikeName;
            }

            RefreshBikes();
        }

        private void BtRemoveBikeConfig_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the bike option?", "Delete Bike Option", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string bikeName = tbBikeConfig.Text.ToString();

                //Note: only removing value as an option, all records using this value are unchanged:
                cbBikeMaint.Items.Remove(bikeName);

                //Clear entires:
                tbConfigMilesNotInLog.Text = "";
                tbBikeLogMiles.Text = "";
                tbBikeConfig.Text = "";
                tbBikeOldName.Text = "";
                tbBikeTotalMiles.Text = "";

                //Remove the Bike from the database table:
                List<object> objectValues = new List<object>();
                objectValues.Add(bikeName);

                try
                {
                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Bike_Remove_Totals", objectValues))
                    {
                        //databaseConnection.CloseConnection();
                    }

                    RefreshBikes();
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception while trying to Delete Bike name entry." + ex.Message.ToString());
                }
            }
        }


        //=============================================================================
        //Start Statistics Section
        //=============================================================================

        public static SqlDataReader ExecuteSimpleQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";

            for (int i = 0; i < _Parameters.Count; i++)
            {
                tmpProcedureName += "@" + i.ToString() + ",";
            }

            tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
            SqlDataReader ToReturn = databaseConnection.ExecuteQueryConnection(tmpProcedureName, _Parameters);
            //databaseConnection.CloseConnection();

            return ToReturn;
        }

        //Get total of miles for the selected log:
        //SELECT SUM(RideDistance) FROM Table_Ride_Information;
        private static string GetTotalMilesForSelectedLog(int logIndex)
        {
            List<object> objectValues = new List<object>
            {
                logIndex
            };
            string returnValue = "0";
            double miles;

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
            returnValue = miles.ToString();

            return returnValue;
        }

        private static string GetElevGain_Yearly(int logIndex)
        {
            List<object> objectValues = new List<object>
            {
                logIndex
            };
            string returnValue = "0";
            int elevgain;

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
            returnValue = elevgain.ToString();

            return returnValue;
        }

        private static float GetTotalMilesForAllLogs()
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

        private static int GetMostElevationAllLogs()
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

        private static string GetLongestRideTimeAllLogs()
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

        private static string GetTotalElevGainForAllLogs()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "0";
            int elevgain;

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
        private static int GetTotalRidesForSelectedLog(int logIndex)
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
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return returnValue;
        }

        //Get average rides per week value:
        //Total rides/weeks
        private static float GetAverageRidesPerWeek(int logIndex)
        {
            int rides = GetTotalRidesForSelectedLog(logIndex);
            int weekValue;
            float avgRides = 0;

            //Need to determine if the current year matches the log year and if not use 52 as the weekValue:
            //Old logs need to include the entire year.  For the current year, only up to the current week #:
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
        private static float GetAverageMilesPerWeek(int logIndex)
        {
            float totalMiles = float.Parse(GetTotalMilesForSelectedLog(logIndex));
            float avgMiles = 0;
            //DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            //Calendar cal = dfi.Calendar;
            int weekValue;

            //Need to determine if the current year matches the log year and if not use 52 as the weekValue:
            //Old logs need to include the entire year.  For the current year, only up to the current week #:
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
        private static float GetAverageMilesPerRide(int logIndex)
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
        private static double GetHighMileageWeekNumber(int logIndex)
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

            // Check last weekly total to see if max:
            if (weekMilesTotal > weeklyMax)
            {
                weeklyMax = weekMilesTotal;
            }

            return weeklyMax;
        }

        private static double GetHighMileageMonthNumber(int logIndex)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            double maxMonthlyMiles = 0;
            double temp = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalMiles_MonthlyForYear", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        temp = double.Parse(results[0].ToString());
                        string testTemp = results[1].ToString();
                        if (temp > maxMonthlyMiles)
                        {
                            maxMonthlyMiles = temp;
                        }
                    }
                }
            }

            return maxMonthlyMiles;
        }

        //Get the highest ascent for a week value:
        private static int GetHighAscentWeekNumber(int logIndex)
        {
            int weekNumber;
            int weekNumberTmp = 0;
            int weekAscentTotal = 0;
            int weeklyMax = 0;

            try
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                string query = "SELECT TotalAscent,WeekNumber FROM Table_Ride_Information WHERE " + logIndex + "=[LogYearID] ORDER BY [WeekNumber] ASC";
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
                                if (weekAscentTotal > weeklyMax)
                                {
                                    weeklyMax = weekAscentTotal;
                                }

                                // Onto a new week, so reset weekly total:
                                weekAscentTotal = int.Parse(reader["TotalAscent"].ToString());
                            }
                            else
                            {
                                weekAscentTotal += int.Parse(reader["TotalAscent"].ToString());
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

            // Check last weekly total to see if max:
            if (weekAscentTotal > weeklyMax)
            {
                weeklyMax = weekAscentTotal;
            }

            return weeklyMax;
        }

        //Get the highest milelage for a day value:
        //SELECT MAX(RideDistance) FROM Table_Ride_Information;
        private static float GetHighMileageDay(int logIndex)
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
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return returnValue;
        }

        private static int GetMaxElevYearly(int logIndex)
        {
            SqlDataReader reader = null;
            int returnValue = 0;

            try
            {
                sqlConnection.Open();

                // 1. declare command object with parameter
                using (SqlCommand cmd = new SqlCommand("select MAX(TotalAscent) from Table_Ride_Information WHERE @Id=[LogYearID]", sqlConnection))
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
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return returnValue;
        }

        private static double GetDaysToNextTimeChange()
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
            else if (year == 2028)
            {
                DateTime changeDate = new DateTime(2028, 3, 12);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2028, 11, 5);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2029, 3, 11);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2029)
            {
                DateTime changeDate = new DateTime(2029, 3, 11);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2029, 11, 4);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2030, 3, 10);
                        dayCount = (changeDate3 - date).TotalDays;
                    }
                    else
                    {
                        dayCount = (changeDate2 - date).TotalDays;
                    }
                }
            }
            else if (year == 2030)
            {
                DateTime changeDate = new DateTime(2030, 3, 10);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(2030, 11, 3);
                    if (date > changeDate2)
                    {
                        DateTime changeDate3 = new DateTime(2031, 3, 9);
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
                DateTime changeDate = new DateTime(Convert.ToInt32(year), 3, 9);
                if ((changeDate - date).TotalDays > 0)
                {
                    dayCount = (changeDate - date).TotalDays;
                }
                else
                {
                    DateTime changeDate2 = new DateTime(Convert.ToInt32(year), 11, 2);
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

        public static int GetCurrentWeekCount()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue;
            string firstDay = GetFirstDayOfWeek() ?? "Monday";
            if (firstDay.Equals("Sunday"))
            {
                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Sunday);
            }
            else
            {
                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Monday);
            }

            //Seen cases where week 53 is returned which is only for leap years?
            if (weekValue == 53)
            {
                if (!DateTime.IsLeapYear(DateTime.Now.Year))
                {
                    weekValue = 1;
                }
            }

            return weekValue;
        }

        private static int GetCurrentWeekCountByYear(DateTime dateValue)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue;
            string firstDay = GetFirstDayOfWeek() ?? "Monday";
            if (firstDay.Equals("Sunday"))
            {
                weekValue = cal.GetWeekOfYear(dateValue, dfi.CalendarWeekRule, DayOfWeek.Sunday);
            }
            else
            {
                weekValue = cal.GetWeekOfYear(dateValue, dfi.CalendarWeekRule, DayOfWeek.Monday);
            }

            //Seen cases where week 53 is returned which is only for leap years?
            if (weekValue == 53)
            {
                if (!DateTime.IsLeapYear(dateValue.Year))
                {
                    weekValue = 1;
                }
            }

            return weekValue;
        }

        private static int GetCurrentDayCount()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            int weekValue = cal.GetDayOfYear(DateTime.Now);

            return weekValue;
        }

        private void UpdateStatsAllLogs()
        {
            //    //Get total miles for all logs:
            double totalMiles = GetTotalMilesForAllLogs();
            totalMiles = Math.Round(totalMiles, 1);
            tbStatisticsTotalMiles.Text = totalMiles.ToString("N0");
            tbLongestRide.Text = Convert.ToString(GetLongestRide());
            tbTotalRides.Text = Convert.ToString(GetTotalRides());
            tbTotalElevGain.Text = Convert.ToString(GetTotalElevGainForAllLogs());
            tbTotalTime.Text = Convert.ToString(GetTotalMovingTimeAllLogs());
            tbMostElevationAll.Text = GetMostElevationAllLogs().ToString("N0");
            tbLongestTimeAll.Text = GetLongestRideTimeAllLogs();
            tbHighWeekAll.Text = GetMonthlyHighMileageWeekNumberAll().ToString();
            tbHighAscentWeekAll.Text = GetHighAscentWeekAll().ToString("N0");
            tbMaxYearlyMilesAllLogs.Text = GetMaxYearlyMilesAll().ToString("N0");
        }

        private double GetMaxYearlyMilesAll()
        {
            double maxMilesTemp = 0;
            double miles = 0;

            //TODO:
            //Get list of logs to iterate over
            List<string> logIDList = ReadDataNames("Table_Log_year", "LogYearID");

            for (int i = 0; i < logIDList.Count; i++)
            {
                List<object> objectValues = new List<object>();
                objectValues.Add(logIDList[i]);

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
                                maxMilesTemp = 0;
                            }
                            else
                            {
                                maxMilesTemp = double.Parse(temp);
                            }
                        }
                    }

                    if (maxMilesTemp > miles)
                    {
                        miles = maxMilesTemp;
                    }
                }
            }

            return miles;
        }

        private static double GetLongestRide()
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

        private static double GetFastestAvg()
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

        private static double GetMaxSpeed()
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

        private static string GetTotalRides()
        {
            List<object> objectValues = new List<object>();
            string returnValue = "0";
            int elevgain;

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

        private static int GetMaintCount()
        {
            List<object> objectValues = new List<object>();
            int count = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetTotalRides_AllLogs", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        count = int.Parse(results[0].ToString());
                    }
                }
            }

            return count;
        }

        private void GetMaintLog()
        {
            lbMaintError.Text = "";

            if (GetMaintCount() < 1)
            {
                Logger.Log("[WARNING] No Maintenance items recorded. ", 0, 0);
                return;
            }

            try
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = null;

                using (sqlDataAdapter = new SqlDataAdapter())
                {
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [Date],[Bike],[Miles],[Comments] FROM Table_Bike_Maintenance", sqlConnection);

                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    
                    dgvMaint.DataSource = dataTable;
                    dgvMaint.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    dgvMaint.EnableHeadersVisualStyles = false;
                    dgvMaint.Sort(dgvMaint.Columns["Date"], ListSortDirection.Descending);
                    dgvMaint.AllowUserToResizeRows = false;
                    dgvMaint.AllowUserToResizeColumns = false;
                    dgvMaint.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvMaint.AllowUserToAddRows = false;
                    dgvMaint.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMaintColor());

                    dgvMaint.Columns[2].DefaultCellStyle.Format = "0.00";

                    //dgvMaint.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    // dgvMaint.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


                    string testValue = GetTextMaint();
                    int rowCount = dgvMaint.Rows.Count;
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (i % 2 == 0)
                        {
                            //is even
                            dgvMaint.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            //is odd
                            if (testValue.Equals("True"))
                            {
                                dgvMaint.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                            }
                            else
                            {
                                dgvMaint.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }
                    }

                    string test = dgvMaint.Rows[1].DefaultCellStyle.ForeColor.ToString();
                    //dgvMaint.Refresh();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to Get Maintenance Log entry. " + ex.Message.ToString());
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                // close connection
                sqlConnection?.Close();
            }
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

        private void BtMaintRetrieve_Run(string date, string bike)
        {
            lbMaintError.Text = "";

            List<object> objectValues = new List<object>();
            objectValues.Add(date);
            objectValues.Add(bike);

            string comments;
            string mainID;

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
                            double milesD = double.Parse(results[2].ToString());
                            milesD = Math.Round(milesD, 2);

                            //Load maintenance data page:
                            rtbMaintComments.Text = comments;
                            tbMaintID.Text = mainID;
                            tbMaintMiles.Text = milesD.ToString();
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
            // Clear out Table_Log_year:
            // Clear out Table_Bike_Mainenace:
            // Clear out Table_Ride_Information:
            List<object> objectBlank = new List<object>();
            RunStoredProcedure(objectBlank, "DeleteTable");


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

            SetCustomField1("");
            SetCustomField2("");

            SetLastLogSelectedDataEntry(0);
            SetLastLogFilterSelected(0);
            SetLastBikeSelected(0);
            SetLastLogSelected(0);
            SetLastLogYearChartSelected(0);
            SetLastMonthlyLogSelected(0);
            SetLicenseAgreement("false");

            AddDefautRoute();

            MessageBox.Show("Close the program and reopen before entering any data.");
        }

        public static int GetLogYearIndexByName(string logName)
        {
            int logIndex = 0;
            List<object> objectValues = new List<object>
            {
                logName
            };

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
            if (cbLogYearConfig.SelectedItem == null || cbLogYearConfig.SelectedIndex == 0)
            {
                tbLogYearConfig.Text = "";

                if (cbLogYear.Items.Count > 0)
                {
                    cbLogYear.SelectedIndex = 0;
                }
                
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

        private void RunMonthlyStatisticsGrid(int logYearIndex)
        {

            double totalMiles;
            int totalRides;
            double avgRidesPerWeek;
            double avgMilesPerWeek;
            double avgMilesPerRide;
            double hightMileageWeek;
            double longestRide;
            int totalElev;          
            int maxElev;        
            string movingTime;

            try
            {
                dataGridViewMonthly.DataSource = null;
                dataGridViewMonthly.Rows.Clear();
                dataGridViewMonthly.ColumnCount = 10;
                //dataGridViewMonthly.RowCount = 12;
                dataGridViewMonthly.Name = "Monthly Stats";

                dataGridViewMonthly.Columns[0].Name = "Total Miles";
                dataGridViewMonthly.Columns[1].Name = "Total Rides";
                dataGridViewMonthly.Columns[2].Name = "Avg Rides/week";
                dataGridViewMonthly.Columns[3].Name = "Avg Miles/week";
                dataGridViewMonthly.Columns[4].Name = "Avg Miles/Ride";
                dataGridViewMonthly.Columns[5].Name = "High Week Miles";
                dataGridViewMonthly.Columns[6].Name = "Longest Ride";
                dataGridViewMonthly.Columns[7].Name = "Total Ascent";
                dataGridViewMonthly.Columns[8].Name = "Max Ascent";
                dataGridViewMonthly.Columns[9].Name = "Moving Time";

                dataGridViewMonthly.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewMonthly.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewMonthly.ReadOnly = true;
                dataGridViewMonthly.EnableHeadersVisualStyles = false;

                dataGridViewMonthly.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewMonthly.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewMonthly.AutoResizeColumns();
                dataGridViewMonthly.AllowUserToOrderColumns = false;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewMonthly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewMonthly.AllowUserToAddRows = false;
                //dataGridViewMonthly.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                //dataGridViewMonthly.DefaultCellStyle.SelectionForeColor = Color.White;
                dataGridViewMonthly.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                //dataGridViewMonthly.RowHeadersVisible = false;

                dataGridViewMonthly.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewMonthly.ColumnHeadersHeight = 40;
                dataGridViewMonthly.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewMonthly.RowHeadersVisible = true;

                dataGridViewMonthly.Columns[0].ValueType = typeof(double);
                dataGridViewMonthly.Columns[1].ValueType = typeof(int);
                dataGridViewMonthly.Columns[2].ValueType = typeof(double);
                dataGridViewMonthly.Columns[3].ValueType = typeof(double);
                dataGridViewMonthly.Columns[4].ValueType = typeof(double);
                dataGridViewMonthly.Columns[5].ValueType = typeof(double);
                dataGridViewMonthly.Columns[6].ValueType = typeof(double);
                dataGridViewMonthly.Columns[7].ValueType = typeof(int);
                dataGridViewMonthly.Columns[8].ValueType = typeof(int);
                dataGridViewMonthly.Columns[9].ValueType = typeof(string);

                //Loop through each month for the logindex:
                for (int i = 1; i < 13; i++)
                {
                    totalMiles = double.Parse(GetTotalMilesMonthlyForSelectedLog(logYearIndex, i).ToString());
                    totalRides = int.Parse(GetTotalRidesMonthlyForSelectedLog(logYearIndex, i).ToString());
                    avgRidesPerWeek = double.Parse(GetAvgMonthlyRidesForSelectedLog(logYearIndex, i).ToString());
                    avgMilesPerWeek = double.Parse(GetAverageMonthlyMilesPerWeek(logYearIndex, i).ToString());
                    avgMilesPerRide = double.Parse(GetAverageMonthlyMilesPerRide(logYearIndex, i).ToString());
                    hightMileageWeek = double.Parse(GetMonthlyHighMileageWeekNumber(logYearIndex, i).ToString());
                    longestRide = double.Parse(GetMaxHighMileageMonthlyForSelectedLog(logYearIndex, i).ToString());
                    totalElev = int.Parse(GetTotalElevGainMonthly(logYearIndex, i).ToString());
                    maxElev = int.Parse(GetMaxElevMonthlyForSelectedLog(logYearIndex, i).ToString());
                    movingTime = GetTotalMovingTimeMonthly(logYearIndex, i).ToString();

                    dataGridViewMonthly.Rows.Add(totalMiles, totalRides, avgRidesPerWeek, avgMilesPerWeek, avgMilesPerRide, hightMileageWeek, longestRide, totalElev, maxElev, movingTime);
                }

                dataGridViewMonthly.Rows[0].Height = 32;
                dataGridViewMonthly.Rows[1].Height = 32;
                dataGridViewMonthly.Rows[2].Height = 32;
                dataGridViewMonthly.Rows[3].Height = 32;
                dataGridViewMonthly.Rows[4].Height = 32;
                dataGridViewMonthly.Rows[5].Height = 32;
                dataGridViewMonthly.Rows[6].Height = 32;
                dataGridViewMonthly.Rows[7].Height = 32;
                dataGridViewMonthly.Rows[8].Height = 32;
                dataGridViewMonthly.Rows[9].Height = 32;
                dataGridViewMonthly.Rows[10].Height = 32;
                dataGridViewMonthly.Rows[11].Height = 32;

                dataGridViewMonthly.Rows[0].HeaderCell.Value = "January";
                dataGridViewMonthly.Rows[1].HeaderCell.Value = "February";
                dataGridViewMonthly.Rows[2].HeaderCell.Value = "March";
                dataGridViewMonthly.Rows[3].HeaderCell.Value = "April";
                dataGridViewMonthly.Rows[4].HeaderCell.Value = "May";
                dataGridViewMonthly.Rows[5].HeaderCell.Value = "June";
                dataGridViewMonthly.Rows[6].HeaderCell.Value = "July";
                dataGridViewMonthly.Rows[7].HeaderCell.Value = "August";
                dataGridViewMonthly.Rows[8].HeaderCell.Value = "September";
                dataGridViewMonthly.Rows[9].HeaderCell.Value = "October";
                dataGridViewMonthly.Rows[10].HeaderCell.Value = "November";
                dataGridViewMonthly.Rows[11].HeaderCell.Value = "December";

                dataGridViewMonthly.AllowUserToResizeRows = false;
                dataGridViewMonthly.AllowUserToResizeColumns = false;
                //dataGridViewMonthly.CurrentCell = dataGridViewMonthly.Rows[0].Cells[4];
                dataGridViewMonthly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMonthlyColor());

                string textValue = GetTextMonthly();
                int rowCount = dataGridViewMonthly.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        //is even
                    }
                    else
                    {
                        //is odd
                        if (textValue.Equals("True"))
                        {
                            dataGridViewMonthly.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridViewMonthly.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Monthly Stats: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Monthly Stats.  Review the log for more information.");
            }
        }

        private void RunYearlyStatisticsGrid()
        {
            try
            {
                dataGridViewYearly.DataSource = null;
                dataGridViewYearly.Rows.Clear();
                dataGridViewYearly.ColumnCount = 12;
                //dataGridViewYearly.RowCount = 12;
                dataGridViewYearly.Name = "Yearly Stats";
                dataGridViewYearly.Columns[0].Name = "Total Miles";
                dataGridViewYearly.Columns[1].Name = "Total Rides";
                dataGridViewYearly.Columns[2].Name = "Avg Rides/week";
                dataGridViewYearly.Columns[3].Name = "Avg Miles/week";
                dataGridViewYearly.Columns[4].Name = "Avg Miles/Ride";
                dataGridViewYearly.Columns[5].Name = "High Month Miles";
                dataGridViewYearly.Columns[6].Name = "High Week Miles";
                dataGridViewYearly.Columns[7].Name = "Longest Ride";
                dataGridViewYearly.Columns[8].Name = "Total Ascent";
                dataGridViewYearly.Columns[9].Name = "Max Ascent";
                dataGridViewYearly.Columns[10].Name = "High Week Ascent";
                dataGridViewYearly.Columns[11].Name = "Moving Time";

                dataGridViewYearly.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewYearly.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewYearly.ReadOnly = true;
                dataGridViewYearly.EnableHeadersVisualStyles = false;

                dataGridViewYearly.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewYearly.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewYearly.AutoResizeColumns();
                dataGridViewYearly.AllowUserToOrderColumns = false;
                dataGridViewYearly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewYearly.AllowUserToAddRows = false;
                dataGridViewYearly.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewYearly.ColumnHeadersHeight = 44;
                dataGridViewYearly.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewYearly.RowHeadersVisible = true;
                dataGridViewYearly.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;


                dataGridViewYearly.Columns[0].ValueType = typeof(double);
                dataGridViewYearly.Columns[1].ValueType = typeof(int);
                dataGridViewYearly.Columns[2].ValueType = typeof(double);
                dataGridViewYearly.Columns[3].ValueType = typeof(double);
                dataGridViewYearly.Columns[4].ValueType = typeof(double);
                dataGridViewYearly.Columns[5].ValueType = typeof(double);
                dataGridViewYearly.Columns[6].ValueType = typeof(double);
                dataGridViewYearly.Columns[7].ValueType = typeof(double);
                dataGridViewYearly.Columns[8].ValueType = typeof(int);
                dataGridViewYearly.Columns[9].ValueType = typeof(int);
                dataGridViewYearly.Columns[10].ValueType = typeof(int);
                dataGridViewYearly.Columns[11].ValueType = typeof(string);

                double totalMiles;
                int totalRides;
                double avgRidesPerWeek;
                double avgMilesPerWeek;
                double avgMilesPerRideYearly;
                double highMilesForMonth;
                double highMilesForWeek;
                double highMilesForDay;
                int totalElevGain;
                int maxElev;
                int totalElevForWeek;
                string totalMovingTime;

                //Get list of logs
                List<string> logYearNameList = ReadDataNames("Table_Log_year", "Name");

                int rowIndex = 0;

                //Loop through each log:
                for (int i = logYearNameList.Count -1; i > -1; i--)
                {
                    int logIndex = GetLogYearIndexByName(logYearNameList[i]);

                    totalMiles = double.Parse(GetTotalMilesForSelectedLog(logIndex));
                    totalRides = GetTotalRidesForSelectedLog(logIndex);
                    avgRidesPerWeek = Math.Round(GetAverageRidesPerWeek(logIndex), 2);
                    avgMilesPerWeek = Math.Round(GetAverageMilesPerWeek(logIndex), 2);
                    avgMilesPerRideYearly = Math.Round(GetAverageMilesPerRide(logIndex), 2);
                    highMilesForMonth = Math.Round(GetHighMileageMonthNumber(logIndex), 2);
                    highMilesForWeek = Math.Round(GetHighMileageWeekNumber(logIndex), 2);
                    highMilesForDay = Math.Round(GetHighMileageDay(logIndex), 2);
                    totalElevGain = int.Parse(GetElevGain_Yearly(logIndex));              
                    maxElev = GetMaxElevYearly(logIndex);
                    totalElevForWeek = GetHighAscentWeekNumber(logIndex);
                    totalMovingTime = GetTotalMovingTimeYearly(logIndex).ToString();

                    dataGridViewYearly.Rows.Add(totalMiles, totalRides, avgRidesPerWeek, avgMilesPerWeek, avgMilesPerRideYearly, highMilesForMonth, highMilesForWeek, highMilesForDay, totalElevGain, maxElev, totalElevForWeek, totalMovingTime);
                    dataGridViewYearly.Rows[rowIndex].HeaderCell.Value = logYearNameList[i];
                    dataGridViewYearly.Rows[rowIndex].Height = 34;
                    rowIndex++;
                }

                dataGridViewYearly.AllowUserToResizeRows = false;
                dataGridViewYearly.AllowUserToResizeColumns = false;
                //dataGridViewYearly.CurrentCell = dataGridViewYearly.Rows[0].Cells[4];
                dataGridViewYearly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetYearlyColor());

                string textValue = GetTextYearly();
                int rowCount = dataGridViewYearly.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        //is even
                    }
                    else
                    {
                        //is odd
                        if (textValue.Equals("True"))
                        {
                            dataGridViewYearly.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridViewYearly.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Yearly Stats: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Yearly Stats.  Review the log for more information.");
            }
        }

        private void CbStatMonthlyLogYear_changed(object sender, EventArgs e)
        {
            if (cbStatMonthlyLogYear.SelectedIndex == 0)
            {
                return;
            }

            int logYearIndex = GetLogYearIndex_ByName(cbStatMonthlyLogYear.SelectedItem.ToString());
            MainForm.SetLastMonthlyLogSelected(cbStatMonthlyLogYear.SelectedIndex);

            if (!formloading)
            {
               // using (RefreshingForm refreshingForm = new RefreshingForm())
                //{
                    // Display form modelessly
                   // refreshingForm.Show();
                    //  ALlow main UI thread to properly display please wait form.
                   // Application.DoEvents();
                    //this.ShowDialog();
                    RunMonthlyStatisticsGrid(logYearIndex);
                    //refreshingForm.Hide();
               // }
            }
        }

        //Get total of miles for the selected log:
        //SELECT SUM(RideDistance) FROM Table_Ride_Information;
        private static float GetTotalMilesMonthlyForSelectedLog(int logIndex, int month)
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
        private static int GetTotalRidesMonthlyForSelectedLog(int logIndex, int month)
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
        private static double GetAvgMonthlyRidesForSelectedLog(int logIndex, int month)
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

        private static double GetAverageMonthlyMilesPerWeek(int logIndex, int month)
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
        private static float GetAverageMonthlyMilesPerRide(int logIndex, int month)
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

        private static double GetMaxHighMileageMonthlyForSelectedLog(int logIndex, int month)
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

        private static int GetMaxElevMonthlyForSelectedLog(int logIndex, int month)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logIndex);
            objectValues.Add(month);
            int returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMaxElevation_Monthly", objectValues))
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

        public static double GetMonthlyHighMileageWeekNumber(int LogYearID, int Month)
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
                sqlConnection?.Close();
            }

            // Check last weekly total to see if max:
            if (weekMilesTotal > weeklyMax)
            {
                weeklyMax = weekMilesTotal;
            }

            return weeklyMax;
        }

        public static double GetMonthlyHighMileageWeekNumberAll()
        {
            int weekNumber;
            int weekNumberTmp = 0;
            double weekMilesTotal = 0;
            double weeklyMax = 0;

            //Loop through each log year:
            //Get list of log year index's:
            
            try
            {
                List<string> logYearList = ReadDataNames("Table_Log_year", "LogYearID");

                for (int i = 0; i < logYearList.Count; i++)
                {
                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    string query = "SELECT RideDistance,WeekNumber FROM Table_Ride_Information WHERE " + logYearList[i] + "=[LogYearID] ORDER BY [WeekNumber] ASC";
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

                    // Check last weekly total to see if max:
                    if (weekMilesTotal > weeklyMax)
                    {
                        weeklyMax = weekMilesTotal;
                    }
                    weekMilesTotal = 0;
                    weekNumberTmp = 1;
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

           

            return weeklyMax;
        }

        public static double GetHighAscentWeekAll()
        {
            int weekNumber;
            int weekNumberTmp = 0;
            int weekAscentTotal = 0;
            int weeklyMax = 0;

            //Loop through each log year:
            //Get list of log year index's:

            try
            {
                List<string> logYearList = ReadDataNames("Table_Log_year", "LogYearID");

                for (int i = 0; i < logYearList.Count; i++)
                {
                    if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
                    {
                        sqlConnection.Open();
                    }

                    string query = "SELECT TotalAscent,WeekNumber FROM Table_Ride_Information WHERE " + logYearList[i] + "=[LogYearID] ORDER BY [WeekNumber] ASC";
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
                                    if (weekAscentTotal > weeklyMax)
                                    {
                                        weeklyMax = weekAscentTotal;
                                    }

                                    // Onto a new week, so reset weekly total:
                                    weekAscentTotal = int.Parse(reader["TotalAscent"].ToString());
                                }
                                else
                                {
                                    weekAscentTotal += int.Parse(reader["TotalAscent"].ToString());
                                }
                            }

                            //reader.Close();
                        }
                        command.Cancel();
                    }

                    // Check last weekly total to see if max:
                    if (weekAscentTotal > weeklyMax)
                    {
                        weeklyMax = weekAscentTotal;
                    }
                    weekAscentTotal = 0;
                    weekNumberTmp = 1;
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



            return weeklyMax;
        }

        public static int GetTEST(int LogYearID)
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
                reader?.Close();

                // close connection
                sqlConnection?.Close();
            }

            return returnValue;
        }

        //=============================================================================
        // Start Monthly Statistics Section
        //=============================================================================

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetMaintLog();
            tabControl1.Refresh();
            Calendar.Refresh();
        }

        private void BtRefreshStatisticsData_Click(object sender, EventArgs e)
        {
            RunYearlyStatisticsGrid();
        }

        private void BtCharts_Click(object sender, EventArgs e)
        {
            ChartForm chartForm = new ChartForm();
            chartForm.Show();
            lbMaintError.Text = "";
        }

        private static string GetTotalElevGainMonthly(int logIndex, int month)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                month
            };
            string returnValue = "0";
            int elevgain;

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
            returnValue = elevgain.ToString();

            return returnValue;
        }

        private static string GetTotalMovingTimeMonthly(int logIndex, int month)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                month
            };
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
                    seconds %= 60;
                }

                minutes += min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes %= 60;
                }

                hours += hr;
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

        private static string GetTotalMovingTimeYearly(int logIndex)
        {
            List<object> objectValues = new List<object>
            {
                logIndex
            };
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
                    seconds %= 60;
                }

                minutes += min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes %= 60;
                }

                hours += hr;
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

        private static string GetTotalMovingTimeAllLogs()
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
                    seconds %= 60;
                }

                minutes += min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes %= 60;
                }

                hours += hr;
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

        private static float GetTotalMilesWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
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

        private static float GetMilesByDate(int logIndex, DateTime rideDate)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                rideDate
            };
            float returnValue = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMiles_ByDate", objectValues))
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

        private static double GetLongestRideWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
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

        private static string GetTotalElevGainWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
            string returnValue = "0";
            int elevgain;

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

        private static string GetHighestElevWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
            string returnValue = "0";
            int elevgain;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("GetMaxHighElev_Weekly", objectValues))
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

        private static string GetTotalRidesWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
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

        private static double GetAvgSpeedWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
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

        private static string GetTotalMovingTimeWeekly(int logIndex, int weekNumber)
        {
            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
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
                    seconds %= 60;
                }

                minutes += min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes %= 60;
                }

                hours += hr;
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

        private static double GetAveragePaceWeekly(int logIndex, int weekNumber, string totalMiles)
        {

            if (totalMiles == "0")
            {
                return 0;
            }

            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };

            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int hr = 0;
            int min = 0;
            double avgPace;

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
                            return 0;
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
                    seconds %= 60;
                }

                minutes += min;

                if (minutes != 0)
                {
                    hr = minutes / 60;
                    minutes %= 60;
                }

                hours += hr;

                //Calculate total time in minutes:
                double secToMin = seconds / 60.0;
                double hrsToMin = hours * 60;
                double totalMin = minutes + secToMin + hrsToMin;

                //Divide total minutes by total miles:
                avgPace = totalMin / double.Parse(totalMiles);
                avgPace = Math.Round(avgPace, 2);
                //splitAsTime = avgPace.ToString().Split('.');
            }

            //if (splitAsTime.Length == 1)
            //{
            //    returnValue = splitAsTime[0] + ":0/mile";
            //}
            //else
            //{
            //    returnValue = splitAsTime[0] + ":" + splitAsTime[1];// + "/mile";
            //}

            return avgPace;
        }

        private static int GetLogYearIndex(int year)
        {
            int logIndex = 0;
            //Get week number
            List<object> objectValues = new List<object>
            {
                year
            };
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

        private static int GetRouteCount(string routeName)
        {
            int count = 0;
            List<object> objectValues = new List<object>
            {
                routeName
            };
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

        private static int GetBikeCount(string routeName)
        {
            int count = 0;
            List<object> objectValues = new List<object>
            {
                routeName
            };
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

        public static DateTime GetDateFromWeekNumber(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;

            string firstDay = GetFirstDayOfWeek();
            int firstWeek;

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

        private void BtFirstDay_Click(object sender, EventArgs e)
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

        private void Button2_Click(object sender, EventArgs e)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string pathFile = strWorkPath + "\\Cycling_Log_User's_Guide.docx";

            System.Diagnostics.Process.Start(pathFile);
        }

        public void RefreshData()
        {
            // Run Refresh for all data fields:
            RunYearlyStatisticsGrid();
            int logYearIndex = GetLogYearIndex_ByName(cbStatMonthlyLogYear.SelectedItem.ToString());
            RunMonthlyStatisticsGrid(logYearIndex);
            RefreshWeekly();
            RefreshBikes();
            RefreshRoutes();
            GetMaintLog();
            UpdateStatsAllLogs();
        }

        private void BtRefreshData_Click(object sender, EventArgs e)
        {
            //Check if any logs yet:
            List<string> logYearList = ReadDataNamesDESC("Table_Log_year", "Name");

            if (logYearList.Count < 1)
            {
                return;
            }
            RefreshData();
            MessageBox.Show("All data fields have been updated.");
        }

        //Update custom fields:
        private void BtCustomDataField1_Click(object sender, EventArgs e)
        {
            string customNEW1 = tbCustomDataField1.Text;
            string customNEW2 = tbCustomDataField2.Text;

            int heightCLB = 394;
            int numberRemoved = 0;

            Boolean changesMade = false;

            // Remove custom1 value:
            if (customNEW1.Equals(""))
            {
                numberRemoved++;
                changesMade = true;
                SetCustomField1("");
            }
            // Update custom1 value to a different value:
            else
            {
                SetCustomField1(customNEW1);
                changesMade = true;
            }

            // Remove custom1 value:
            if (customNEW2.Equals(""))
            {
                numberRemoved++;
                changesMade = true;
                SetCustomField2("");
            }
            // Update custom1 value to a different value:
            else
            {
                SetCustomField2(customNEW2);
                changesMade = true;
            }

            if (numberRemoved == 1)
            {
                heightCLB = 379;
            }
            else if (numberRemoved == 2)
            {
                heightCLB = 364;
            }

            SetHeightCLB(heightCLB);

            if (changesMade)
            {
                MessageBox.Show("Custom data fields have been updated.");
            } else
            {
                MessageBox.Show("No changes saved.");
            }

            ConfigurationFile.WriteConfigFile();
        }

        private void RefreshRoutes()
        {
            int count;

            try
            {
                List<string> routeList = GetRoutes();

                dataGridViewRoutes.ColumnCount = 2;
                dataGridViewRoutes.Name = "Route Listing And Counts";
                dataGridViewRoutes.Columns[0].Name = "Route Name";
                dataGridViewRoutes.Columns[1].Name = "Count";
                dataGridViewRoutes.Columns[0].ValueType = typeof(string);
                dataGridViewRoutes.Columns[1].ValueType = typeof(int);
                dataGridViewRoutes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
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
                dataGridViewRoutes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetRouteColor());
                
                this.dataGridViewRoutes.Rows.Clear();

                if (routeList.Contains("--Select Value--"))
                {
                    routeList.Remove("--Select Value--");
                }

                //MessageBox.Show("Route count: " + routeList.Count);
                for (int i = 0; i < routeList.Count; i++)
                {
                    count = GetRouteCount(routeList[i]);
                    this.dataGridViewRoutes.Rows.Add(routeList[i], count);
                    if (i % 2 == 0)
                    {
                        //is even
                    }
                    else
                    {
                        //is odd
                        if (GetTextRoute().Equals("True"))
                        {
                            this.dataGridViewRoutes.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        } else
                        {
                            this.dataGridViewRoutes.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }      
                    }
                }

                dataGridViewRoutes.Refresh();
                tbTotalRoutes.Text = routeList.Count.ToString("N0");
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Routes: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Routes.  Review the log for more information.");
            }
        }

        private void RefreshBikes()
        {
            int count;
            double milesNotInLog = 0;
            double logMiles = 0;
            double totalMiles = 0;
            double sumMiles = 0; //running total of all miles from all bikes:

            try
            {
                List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

                dataGridViewBikes.DataSource = null;
                dataGridViewBikes.Rows.Clear();
                dataGridViewBikes.ColumnCount = 5;
                dataGridViewBikes.Name = "Bike Listing And Miles";
                dataGridViewBikes.Columns[0].Name = "Bike Name";
                dataGridViewBikes.Columns[1].Name = "Rides";
                dataGridViewBikes.Columns[2].Name = "Total Miles";
                dataGridViewBikes.Columns[3].Name = "Log Miles";
                dataGridViewBikes.Columns[4].Name = "Miles Not In Log";
                //dataGridViewBikes.Columns[0].ValueType = typeof(string);
                //dataGridViewBikes.Columns[0].ValueType = typeof(int);
                //dataGridViewBikes.Columns[0].ValueType = typeof(double);
                //dataGridViewBikes.Columns[0].ValueType = typeof(double);
                dataGridViewBikes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewBikes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewBikes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewBikes.ReadOnly = true;
                dataGridViewBikes.EnableHeadersVisualStyles = false;
                dataGridViewBikes.Columns["Rides"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Log Miles"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Total Miles"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles Not In Log"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                dataGridViewBikes.AutoResizeColumns();
                dataGridViewBikes.AllowUserToOrderColumns = true;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewBikes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridViewBikes.Columns["Rides"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Total Miles"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Log Miles"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.Columns["Miles Not In Log"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewBikes.AllowUserToAddRows = false;
                dataGridViewBikes.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewBikes.AllowUserToResizeRows = false;
                dataGridViewBikes.AllowUserToResizeColumns = false;
                dataGridViewBikes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridViewBikes.ColumnHeadersHeight = 40;
                dataGridViewBikes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewBikes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetBikeColor());

                int bikeCount = bikeList.Count;
                int height = 355;
                int heightNew = (bikeCount * 23) + 40;
                if (heightNew < height)
                {
                    height = heightNew;
                }
                Size startingSize = new Size(467, height);
                dataGridViewBikes.Size = startingSize;
                dataGridViewBikes.Refresh();

                for (int i = 0; i < bikeList.Count; i++)
                {
                    try
                    {
                        List<object> objectValues = new List<object>
                        {
                            bikeList[i]
                        };

                        //Sum up all miles logged for each bike:
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
                                        logMiles = 0;
                                    }
                                    else
                                    {
                                        logMiles = float.Parse(temp);
                                    }
                                }
                                sumMiles += logMiles;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to total miles for each bike." + ex.Message.ToString());
                    }

                    //Get NotInMiles for each bike:
                    try
                    {
                        List<object> objectValues = new List<object>
                        {
                            bikeList[i]
                        };

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
                                Logger.LogError("[WARNING: RefreshBikes() No entry found for the selected Bike and Date.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to get bike miles." + ex.Message.ToString());
                    }

                    totalMiles = milesNotInLog + logMiles;
                    totalMiles = Math.Round(totalMiles, 1);
                    logMiles = Math.Round(logMiles, 1);
                    count = GetBikeCount(bikeList[i]);
                    this.dataGridViewBikes.Rows.Add(bikeList[i], count, totalMiles, logMiles, milesNotInLog);
                    if (i % 2 == 0)
                    {
                        //is even
                    }
                    else
                    {
                        //is odd
                        if (GetTextBike().Equals("True"))
                        {
                            this.dataGridViewBikes.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            this.dataGridViewBikes.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }

                    //Update bike table with total miles:
                    try
                    {
                        List<object> objectValues = new List<object>
                        {
                            bikeList[i],
                            milesNotInLog,
                            logMiles,
                            totalMiles
                        };

                        //ExecuteScalarFunction
                        using (var results = ExecuteSimpleQueryConnection("Bike_TotalMiles_Update", objectValues))
                        {
                            //if (results.HasRows)
                            //{
                            //    while (results.Read())
                            //    {
                                    
                            //    }
                            //}
                            //else
                            //{
                            //    //lbMaintError.Text = "No entry found for the selected Bike.";
                            //    Logger.LogError("[WARNING: No entry found for the selected Bike.");
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("[ERROR]: Exception while trying to update bike total miles." + ex.Message.ToString());
                    }
                }

                tbBikeAllMilesTotal.Text = sumMiles.ToString("N0");
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Bikes: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Bikes.  Review the log for more information.");
            }
        }

        private string GetAvgMilesPerRide(string distance, string rides)
        {
            if (int.Parse(rides) == 0)
            {
                return "0";
            }

            double avg_miles;
            avg_miles = double.Parse(distance)/double.Parse(rides);
            avg_miles = Math.Round(avg_miles, 1);

            return avg_miles.ToString();
        }

        private int GetLog_Year(string logYearName)
        {
            List<object> objectValues = new List<object>();
            objectValues.Add(logYearName);
            int year = 0;

            try
            {
                //ExecuteScalarFunction
                using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValues))
                {
                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            year = int.Parse(results[0].ToString());
                        }
                    }
                    else
                    {
                        // lbMaintError.Text = "No entry found for the selected Bike and Date.";
                        return 0;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
            }

            return year;
        }

        private void RefreshWeekly()
        {
            string logYearName = cbLogYearWeekly.SelectedItem.ToString();
            if (cbLogYearWeekly.SelectedIndex == 0 || logYearName.Equals("--Select Value--"))
            {
                return;
            }

            int logYearIndex = GetLogYearIndex_ByName(logYearName);
            int currentWeekNumber = GetCurrentWeekCount();
            int logYear = GetLog_Year(logYearName);
            int currentYear = DateTime.Now.Year;
            Boolean bCurrentYear = false;

            if (logYear == currentYear)
            {
                bCurrentYear = true;
            }

            try
            {
                dataGridViewWeekly.DataSource = null;
                dataGridViewWeekly.Rows.Clear();
                dataGridViewWeekly.ColumnCount = 10;
                //dataGridViewWeekly.RowCount = 7;
                dataGridViewWeekly.Name = "Weekly Stats";

                dataGridViewWeekly.Columns[0].Name = "Week #";
                dataGridViewWeekly.Columns[1].Name = "Total Miles";
                dataGridViewWeekly.Columns[2].Name = "Total Rides";
                dataGridViewWeekly.Columns[3].Name = "Avg Miles/Ride";
                dataGridViewWeekly.Columns[4].Name = "Longest Ride";
                dataGridViewWeekly.Columns[5].Name = "Total Ascent";
                dataGridViewWeekly.Columns[6].Name = "Max Ascent";
                dataGridViewWeekly.Columns[7].Name = "Moving Time";
                dataGridViewWeekly.Columns[8].Name = "Avg Speed";
                dataGridViewWeekly.Columns[9].Name = "Pace min/mile";

                dataGridViewWeekly.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewWeekly.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewWeekly.ReadOnly = true;
                dataGridViewWeekly.EnableHeadersVisualStyles = false;

                dataGridViewWeekly.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewWeekly.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dataGridViewWeekly.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridViewWeekly.AllowUserToOrderColumns = false;
                dataGridViewWeekly.AllowUserToAddRows = false;
                dataGridViewWeekly.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridViewWeekly.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewWeekly.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewWeekly.ColumnHeadersHeight = 40;
                dataGridViewWeekly.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewWeekly.RowHeadersVisible = true;

                dataGridViewWeekly.Columns[0].ValueType = typeof(int);
                dataGridViewWeekly.Columns[1].ValueType = typeof(double);
                dataGridViewWeekly.Columns[2].ValueType = typeof(double);
                dataGridViewWeekly.Columns[3].ValueType = typeof(double);
                dataGridViewWeekly.Columns[4].ValueType = typeof(double);
                dataGridViewWeekly.Columns[5].ValueType = typeof(double);
                dataGridViewWeekly.Columns[6].ValueType = typeof(double);
                dataGridViewWeekly.Columns[7].ValueType = typeof(double);
                dataGridViewWeekly.Columns[8].ValueType = typeof(double);
                dataGridViewWeekly.Columns[9].ValueType = typeof(double);

                //If on the current year, get current week:

                //Loop through each week:
                for (int weekNumber = 1; weekNumber < 54; weekNumber++)
                {
                    //Hide future
                    if (bCurrentYear && currentWeekNumber < weekNumber)
                    {
                        dataGridViewWeekly.CurrentCell = dataGridViewWeekly.Rows[weekNumber-2].Cells[0];
                        break;
                    } else
                    {
                        if (weekNumber == 53)
                        {
                            if (!DateTime.IsLeapYear(logYear))
                            {
                                break;
                            }
                            
                            DateTime currentDate = GetDateFromWeekNumber(logYear, weekNumber);
                            int currentYearValue = currentDate.Year;

                            if (currentYearValue > logYear)
                            {
                                break;
                            }
                        }
                        
                        string tbDistanceWeek0 = GetTotalMilesWeekly(logYearIndex, weekNumber).ToString();
                        string tbNumRidesWeek0 = GetTotalRidesWeekly(logYearIndex, weekNumber).ToString();
                        double avgMilesPerRide = double.Parse(GetAvgMilesPerRide(tbDistanceWeek0, tbNumRidesWeek0));
                        double longestRideWeekly = GetLongestRideWeekly(logYearIndex, weekNumber);
                        double totalElevGainWeekly = double.Parse(GetTotalElevGainWeekly(logYearIndex, weekNumber));
                        double hightestElevWeekly = double.Parse(GetHighestElevWeekly(logYearIndex, weekNumber));
                        string totalMovingTimeWeekly = GetTotalMovingTimeWeekly(logYearIndex, weekNumber).ToString();
                        double avgSpeedWeekly = GetAvgSpeedWeekly(logYearIndex, weekNumber);
                        double avgPaceWeekly = GetAveragePaceWeekly(logYearIndex, weekNumber, tbDistanceWeek0);
                        dataGridViewWeekly.Rows.Add(weekNumber, double.Parse(tbDistanceWeek0), double.Parse(tbNumRidesWeek0), avgMilesPerRide, longestRideWeekly, totalElevGainWeekly, hightestElevWeekly, totalMovingTimeWeekly, avgSpeedWeekly, avgPaceWeekly);
                        dataGridViewWeekly.Rows[weekNumber - 1].HeaderCell.Value = GetDateFromWeekNumber(logYear, weekNumber).ToString("MM/dd/yyyy");
                        dataGridViewWeekly.Rows[weekNumber - 1].Height = 34;
                    }                  
                }

                dataGridViewWeekly.AllowUserToResizeRows = false;
                dataGridViewWeekly.AllowUserToResizeColumns = false;  
                dataGridViewWeekly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetWeeklyColor());

                string textValue = GetTextWeekly();
                int rowCount = dataGridViewWeekly.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        //is even
                    }
                    else
                    {
                        //is odd
                        if (textValue.Equals("True"))
                        {
                            dataGridViewWeekly.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridViewWeekly.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Weekly Stats: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Weekly Stats.  Review the log for more information.");
            }

        }

        public static void Backup()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(strExeFilePath);
            string DBFileName = path + "\\CyclingLogDatabase.mdf";

            using (SqlCommand cmd = new SqlCommand("backup database [" + DBFileName + "] to disk=@path WITH NOFORMAT", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@path", path + "\\database\\CyclingLogDatabase_backup.bak");
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }

        }

        public static void Restore()
        {
            //try { 
            ////using (SqlConnection cn = new SqlConnection())
            ////{
            //sqlConnection.Open();
            //    #region step 1 SET SINGLE_USER WITH ROLLBACK
            //    string sql = "IF DB_ID('CyclingLogDatabase') IS NOT NULL ALTER DATABASE [CyclingLogDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            //    using (var command = new SqlCommand(sql, sqlConnection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //    #endregion
            //    #region step 2 InstanceDefaultDataPath

            //    sql = "SELECT ServerProperty(N'InstanceDefaultDataPath') AS default_file";
            //    string default_file = "NONE";
            //    using (var command = new SqlCommand(sql, sqlConnection))
            //    {
            //        using (var reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                default_file = reader.GetString(reader.GetOrdinal("default_file"));
            //            }
            //        }
            //    }
            //    sql = "SELECT ServerProperty(N'InstanceDefaultLogPath') AS default_log";
            //    string default_log = "NONE";
            //    using (var command = new SqlCommand(sql, sqlConnection))
            //    {
            //        using (var reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                default_log = reader.GetString(reader.GetOrdinal("default_log"));
            //            }
            //        }
            //    }
            //    #endregion
            //    #region step 3 Restore
            //    //sql = "USE MASTER RESTORE DATABASE [K:\\TESTING\\CYCLING_LOG_TESTING\\CYCLINGLOGDATABASE.MDF] FROM DISK='K:\\testing\\Cycling_Log_Testing\\database\\CyclingLogDatabase_backup.bak' WITH  FILE = 1, MOVE N'CyclingLogDatabase' TO '" + default_file + "CyclingLogDatabase.mdf', MOVE N'CyclingLogDatabase_Log' TO '" + default_log + "CyclingLogDatabase_Log.ldf', NOUNLOAD,  REPLACE,  STATS = 1;";
            //    sql = "RESTORE FILELISTONLY FROM DISK = 'C:\\Program Files\\Microsoft SQL Server\\MSSQL16.SQLEXPRESS02\\MSSQL\\Backup\\CyclingLogDatabase_backup.bak'";
            //    using (var command = new SqlCommand(sql, sqlConnection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //    #endregion
            //    #region step 4 SET MULTI_USER
            //    sql = "ALTER DATABASE [CyclingLogDatabase] SET MULTI_USER";
            //    using (var command = new SqlCommand(sql, sqlConnection))
            //    {
            //        command.ExecuteNonQuery();
            //    }
            //    #endregion
            //}

            try
            {
                sqlConnection.Open();
                string sqlStmt = string.Format("RESTORE DATABASE [test123] from disk=N'F:\\1_test_cycling\\test123.mdf'", "F:\\1_test_cycling\\database\\CyclingLogDatabase_backup.bak");
                using (SqlCommand bu2 = new SqlCommand(sqlStmt, sqlConnection))

                {
                    bu2.Connection = sqlConnection;
                    //bu2.CommandText = sqlStmt;

                    bu2.ExecuteNonQuery();
                }

                MessageBox.Show("Restore done Sucessfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restore error");
                MessageBox.Show(ex.ToString());
            }

            sqlConnection.Close();
        }

        public static void Restore_OLD1()
        {
            using (SqlCommand cmd = new SqlCommand("drop database CyclingLogDatabase", sqlConnection))
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(strExeFilePath);
            string DBFileName = path + "\\CyclingLogDatabase.mdf";
            string query = "USE [master]; RESTORE DATABASE [" + DBFileName + "] FROM DISK = N'" + path + "\\database\\CyclingLogDatabase_backup.bak" + " ' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }

        }

        //public void backupDB()
        //{
        //    // Connect to the local, default instance of SQL Server.   
        //    Server srv = new Server();
        //    // Reference the CyclingLogDatabase database.   
        //    Database db = default(Database);
        //    db = srv.Databases["CyclingLogDatabase"];

        //    // Store the current recovery model in a variable.   
        //    int recoverymod;
        //    recoverymod = (int)db.DatabaseOptions.RecoveryModel;
        //    setRecoverymod(recoverymod);

        //    // Define a Backup object variable.   
        //    Backup bk = new Backup();

        //    // Specify the type of backup, the description, the name, and the database to be backed up.   
        //    bk.Action = BackupActionType.Database;
        //    bk.BackupSetDescription = "Full backup of CyclingLogDatabase";
        //    bk.BackupSetName = "CyclingLogDatabase Backup";
        //    bk.Database = "CyclingLogDatabase";

        //    // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.   
        //    BackupDeviceItem bdi = default(BackupDeviceItem);
        //    bdi = new BackupDeviceItem("Test_Full_Backup1", DeviceType.File);

        //    // Add the device to the Backup object.   
        //    bk.Devices.Add(bdi);
        //    // Set the Incremental property to False to specify that this is a full database backup.   
        //    bk.Incremental = false;

        //    // Specify that the log must be truncated after the backup is complete.   
        //    bk.LogTruncation = BackupTruncateLogType.Truncate;

        //    // Run SqlBackup to perform the full database backup on the instance of SQL Server.   
        //    bk.SqlBackup(srv);

        //    // Inform the user that the backup has been completed.   
        //    System.Console.WriteLine("Full Backup complete.");

        //    // Remove the backup device from the Backup object.   
        //    bk.Devices.Remove(bdi);
        //}

        //public void restoreDB()
        //{
        //    // Connect to the local, default instance of SQL Server.   
        //    Server srv = new Server();
        //    // Reference the CyclingLogDatabase database.   
        //    Database db = default(Database);
        //    db = srv.Databases["CyclingLogDatabase"];

        //    // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.   
        //    BackupDeviceItem bdi = default(BackupDeviceItem);
        //    bdi = new BackupDeviceItem("Test_Full_Backup1", DeviceType.File);

        //    // Define a Restore object variable.  
        //    Restore rs = new Restore();

        //    // Set the NoRecovery property to true, so the transactions are not recovered.   
        //    rs.NoRecovery = true;

        //    // Add the device that contains the full database backup to the Restore object.   
        //    rs.Devices.Add(bdi);

        //    // Specify the database name.   
        //    rs.Database = "CyclingLogDatabase";

        //    // Restore the full database backup with no recovery.   
        //    rs.SqlRestore(srv);

        //    // Inform the user that the Full Database Restore is complete.   
        //    Console.WriteLine("Full Database Restore complete.");

        //    // reacquire a reference to the database  
        //    db = srv.Databases["CyclingLogDatabase"];

        //    // Remove the device from the Restore object.  
        //    rs.Devices.Remove(bdi);

        //    // Set the NoRecovery property to False.   
        //    rs.NoRecovery = false;

        //    // Set the database recovery model back to its original value.  
        //    int recoverymodOut = getRecoverymodIn();
        //    db.RecoveryModel = (RecoveryModel)recoverymodOut;

        //    // Remove the backup files from the hard disk.  
        //    // This location is dependent on the installation of SQL Server  
        //    //System.IO.File.Delete("C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\Test_Full_Backup1");
        //}

        private void BtDBBackup_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This function will create a copy of the database. Do you want to continue?", "Backup Database", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(strExeFilePath);

            string sourceFile1 = path + "\\CyclingLogDatabase.mdf";
            string sourceFile2 = path + "\\CyclingLogDatabase_log.ldf";

            // Source file to be renamed  
            // Create a FileInfo  
            System.IO.FileInfo fi = new System.IO.FileInfo(sourceFile1);
            // Check if file is there  
            if (fi.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(path + "\\dbBackup\\CyclingLogDatabase_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".mdf");
            }

            fi = new System.IO.FileInfo(sourceFile2);
            // Check if file is there  
            if (fi.Exists)
            {
                // Move file with a new name. Hence renamed.  
                fi.MoveTo(path + "\\dbBackup\\CyclingLogDatabase_log_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".ldf");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Backup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void dgvMaint_Click(object sender, EventArgs e)
        {
            int rowindex = dgvMaint.CurrentCell.RowIndex;
            int columnindex = dgvMaint.CurrentCell.ColumnIndex;

            string date = dgvMaint.Rows[rowindex].Cells[0].Value.ToString();
            string bike = dgvMaint.Rows[rowindex].Cells[1].Value.ToString();
            double milesD = double.Parse(dgvMaint.Rows[rowindex].Cells[2].Value.ToString());
            milesD = Math.Round(milesD, 2);
            int bikeIndex = -1; 

            BtMaintRetrieve_Run(date, bike);

            List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

            for (int i = 0; i < bikeList.Count; i++)
            {
                if (bikeList[i].Equals(bike))
                {
                    bikeIndex = i;
                    break;
                }
            }

            //dgvMaint.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dgvMaint.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tbMaintDateCheck.Text = dgvMaint.Rows[rowindex].Cells[0].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dgvMaint.Rows[rowindex].Cells[0].Value.ToString());
            cbBikeMaint.SelectedIndex = bikeIndex + 1; // Add 1 to account for --select value--:
            tbMaintMiles.Text = milesD.ToString();
            tbMaintAddUpdate.Text = "Update";
        }

        private void btHideShowIDColumn_Click(object sender, EventArgs e)
        {
            if (rbShowIDColumn.Checked)
            {
                SetIDColumn("1");
                MessageBox.Show("Setting to show the ID Column.");
            }
            else
            {
                SetIDColumn("0");
                MessageBox.Show("Setting to hide the ID Column.");
            }
        }

        private void btSetColors_Click(object sender, EventArgs e)
        {
            if (cbMaintTextColor.Checked)
            {
                SetTextMaint("True");
            }
            else
            {
                SetTextMaint("False");
            }

            if (cbWeeklyTextColor.Checked)
            {
                SetTextWeekly("True");
            }
            else
            {
                SetTextWeekly("False");
            }

            if (cbMonthlyTextColor.Checked)
            {
                SetTextMonthly("True");
            }
            else
            {
                SetTextMonthly("False");
            }

            if (cbYearlyTextColor.Checked)
            {
                SetTextYearly("True");
            }
            else
            {
                SetTextYearly("False");
            }

            if (cbDisplayDataTextColor.Checked)
            {
                SetTextDisplay("True");
            }
            else
            {
                SetTextDisplay("False");
            }

            if (cbBikeTextColor.Checked)
            {
                SetTextBike("True");
            }
            else
            {
                SetTextBike("False");
            }

            if (cbRouteTextColor.Checked)
            {
                SetTextRoute("True");
            }
            else
            {
                SetTextRoute("False");
            }

            if (cbCalendarTextColor.Checked)
            {
                SetTextCalendar("True");
            }
            else
            {
                SetTextCalendar("False");
            }

            //Maintenance Grid:
            SetMaintColor(cbMaintColors.SelectedItem.ToString());
            dgvMaint.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMaintColor());
            dgvMaint.Refresh();

            //Weekly Stat Grid:
            SetWeeklyColor(cbWeeklyColors.SelectedItem.ToString());
            dataGridViewWeekly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetWeeklyColor());
            dataGridViewWeekly.Refresh();

            //Monthly Stat Grid:
            SetMonthlyColor(cbMonthlyColors.SelectedItem.ToString());
            dataGridViewMonthly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMonthlyColor());
            dataGridViewMonthly.Refresh();

            //Yearly Stat Grid:
            SetYearlyColor(cbYearlyColors.SelectedItem.ToString());
            dataGridViewYearly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetYearlyColor());
            dataGridViewYearly.Refresh();

            //Display Data Grid:
            SetDisplayDataColor(cbDisplayDataColors.SelectedItem.ToString());

            //Bike Grid:
            SetBikeColor(cbBikeColors.SelectedItem.ToString());
            dataGridViewBikes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetBikeColor());
            dataGridViewBikes.Refresh();

            //Route Grid:
            SetRouteColor(cbRouteColors.SelectedItem.ToString());
            dataGridViewRoutes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetRouteColor());
            dataGridViewRoutes.Refresh();

            //Calendar Grid:
            SetCalendarColor(cbCalendarColors.SelectedItem.ToString());
            RunCalendar();

            MessageBox.Show("Color save complete.");
        }

        private void cbMaintColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbMaintColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbColorMaint.BackColor = Color.FromName(color);
        }

        private void cbWeeklyColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbWeeklyColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbColorWeekly.BackColor = Color.FromName(color);
        }

        private void cbMonthlyColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbMonthlyColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbColorMonthly.BackColor = Color.FromName(color);
        }

        private void cbYearlyColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbYearlyColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbColorYearly.BackColor = Color.FromName(color);
        }

        private void cbDisplayDataColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbDisplayDataColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbColorDisplayData.BackColor = Color.FromName(color);
        }

        private void cbBikeColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbBikeColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbBikeColor.BackColor = Color.FromName(color);
        }

        private void cbRouteColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbRouteColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbRouteColor.BackColor = Color.FromName(color);
        }

        private void cbMaintTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMaintTextColor.Checked)
            {
                tbColorMaint.ForeColor = Color.Black;
            } else
            {
                tbColorMaint.ForeColor= Color.White;
            }
        }

        private void cbWeeklyTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWeeklyTextColor.Checked)
            {
                tbColorWeekly.ForeColor = Color.Black;
            }
            else
            {
                tbColorWeekly.ForeColor = Color.White;
            }
        }

        private void cbMonthlyTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMonthlyTextColor.Checked)
            {
                tbColorMonthly.ForeColor = Color.Black;
            }
            else
            {
                tbColorMonthly.ForeColor = Color.White;
            }
        }

        private void cbYearlyTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbYearlyTextColor.Checked)
            {
                tbColorYearly.ForeColor = Color.Black;
            }
            else
            {
                tbColorYearly.ForeColor = Color.White;
            }
        }
        private void cbDisplayDataTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDisplayDataTextColor.Checked)
            {
                tbColorDisplayData.ForeColor = Color.Black;
            }
            else
            {
                tbColorDisplayData.ForeColor = Color.White;
            }
        }

        private void cbBikeTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBikeTextColor.Checked)
            {
                tbBikeColor.ForeColor = Color.Black;
            }
            else
            {
                tbBikeColor.ForeColor = Color.White;
            }
        }

        private void cbRouteTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRouteTextColor.Checked)
            {
                tbRouteColor.ForeColor = Color.Black;
            }
            else
            {
                tbRouteColor.ForeColor = Color.White;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tbMaintAddUpdate.Text = "Add";
        }

        private void cbBikeMaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBikeMaint.SelectedIndex < 1)
            {
                return;
            }

            if (tbMaintAddUpdate.Text.Equals("Add"))
            {
                string bikeName = cbBikeMaint.SelectedItem.ToString();
                double miles;

                try
                {
                    List<object> objectValues = new List<object>
                        {
                            bikeName
                        };

                    //ExecuteScalarFunction
                    using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
                    {
                        if (results.HasRows)
                        {
                            while (results.Read())
                            {
                                miles = double.Parse(results[1].ToString());
                                miles = Math.Round(miles, 1);
                                tbMaintMiles.Text = miles.ToString();
                            }
                        }
                        else
                        {
                            //lbMaintError.Text = "No entry found for the selected Bik.";
                            Logger.LogError("[WARNING: cbBikeMaint_SelectedIndexChanged - No entry found for the selected Bike.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError("[ERROR]: Exception cbBikeMaint_SelectedIndexChanged." + ex.Message.ToString());
                }
                
            }
        }

        private void BtMaintSave_Click(object sender, EventArgs e)
        {
            lbMaintError.Text = "";

            if (!tbMaintID.Text.Equals("") && tbMaintDateCheck.Text.Equals(dateTimePicker1.Value) && tbMaintAddUpdate.Text.Equals("Update"))
            {
                //MessageBox.Show("The selected date already has an entry. The entry can be updated but not added.");
                lbMaintError.Text = "The selected date and bike already have an entry. Select the Update option.";
                return;
            }

            if (cbBikeMaint.SelectedIndex < 1)
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
            if (!double.TryParse(tbMaintMiles.Text, out _))
            {
                lbMaintError.Text = "The miles value must be in numeric format. Enter 0 if unknown.";
                return;
            }

            List<object> objectValues = new List<object>();

            //Check if adding a new entry or updating one:
            if (tbMaintDateCheck.Text.Equals(dateTimePicker1.Value.ToString()) && tbMaintAddUpdate.Text.Equals("Update"))
            {
                objectValues.Add(tbMaintID.Text);
                objectValues.Add(cbBikeMaint.SelectedItem.ToString());
                objectValues.Add(rtbMaintComments.Text);
                objectValues.Add(dateTimePicker1.Value);
                objectValues.Add(float.Parse(tbMaintMiles.Text));
                RunStoredProcedure(objectValues, "Maintenance_Update");
            }
            else
            {
                objectValues.Add(cbBikeMaint.SelectedItem.ToString());
                objectValues.Add(rtbMaintComments.Text);
                objectValues.Add(dateTimePicker1.Value);
                objectValues.Add(float.Parse(tbMaintMiles.Text));
                RunStoredProcedure(objectValues, "Maintenance_Add");
            }

            //tbMaintID.Text = "";
            //tbMaintMiles.Text = "";
            //rtbMaintComments.Text = "";
            GetMaintLog();

            MessageBox.Show("Maintenance item has been saved.");
        }

        private void dataGridViewBikes_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewBikes.CurrentCell.RowIndex;
            int columnindex = dataGridViewBikes.CurrentCell.ColumnIndex;

            tbBikeConfig.Text = dataGridViewBikes.Rows[rowindex].Cells[0].Value.ToString();
            tbBikeTotalMiles.Text = dataGridViewBikes.Rows[rowindex].Cells[2].Value.ToString();
            tbBikeLogMiles.Text = dataGridViewBikes.Rows[rowindex].Cells[3].Value.ToString();
            tbConfigMilesNotInLog.Text = dataGridViewBikes.Rows[rowindex].Cells[4].Value.ToString();

            tbBikeOldName.Text = dataGridViewBikes.Rows[rowindex].Cells[0].Value.ToString();
        }

        private void BtBikeClear_Click(object sender, EventArgs e)
        {
            tbBikeOldName.Text = "";
            tbBikeConfig.Text = "";
            tbConfigMilesNotInLog.Text = "";
            tbBikeLogMiles.Text = "";
            tbBikeTotalMiles.Text = "";
        }

        private void BtRouteClear_Click(object sender, EventArgs e)
        {
            tbRouteConfig.Text = "";
            tbRouteOldName.Text = "";
        }

        private void dataGridViewRoutes_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridViewRoutes.CurrentCell.RowIndex;
            int columnindex = dataGridViewRoutes.CurrentCell.ColumnIndex;

            tbRouteConfig.Text = dataGridViewRoutes.Rows[rowindex].Cells[0].Value.ToString();
            tbRouteOldName.Text = dataGridViewRoutes.Rows[rowindex].Cells[0].Value.ToString();


        }

        private void btLogTitleSave_Click(object sender, EventArgs e)
        {
            LogTitleSave();
        }

        private void btLogTitleClear_Click(object sender, EventArgs e)
        {
            cbLogYearConfig.SelectedIndex = 0;
            tbLogYearConfig.Text = string.Empty;
            cbLogYear.SelectedIndex = 0;
        }

        private void RefreshWeekly(object sender, EventArgs e)
        {
            if (cbLogYearWeekly.SelectedIndex == 0)
            {
                return;
            }
            SetLastLogYearWeeklySelected(cbLogYearWeekly.SelectedIndex);
            RefreshWeekly();
        }

        private void btMonthlyStatReset_Click(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex_ByName(cbStatMonthlyLogYear.SelectedItem.ToString());
            RunMonthlyStatisticsGrid(logYearIndex);
        }

        private void btYearlyStatReset_Click(object sender, EventArgs e)
        {
            RunYearlyStatisticsGrid();
        }

        private void btPlanner_Click(object sender, EventArgs e)
        {
            Planner planner = new Planner(); 
            planner.Show();
        }

        private static string GetFirstDayForMonth(int month)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, month, 1);
            // 'Friday, November 1, 2024'

            return firstDay.DayOfWeek.ToString();
        }

        public static int GetLogIndexByName(string logName)
        {
            List<object> objectValues = new List<object>
            {
                logName
            };

            int logIndex = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index_Name", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        logIndex = int.Parse(results[0].ToString());
                    }
                }
            }

            return logIndex;
        }

        public static int GetLogYearByName(string logName)
        {
            List<object> objectValues = new List<object>
            {
                logName
            };

            int logIndex = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Get_LogYear_YearByName", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        logIndex = int.Parse(results[0].ToString());
                    }
                }
            }

            return logIndex;
        }

        public static int GetLogIndexByYear(int logYear)
        {
            List<object> objectValues = new List<object>
            {
                logYear
            };

            int logIndex = 0;

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Get_LogIndex_ByYear", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        logIndex = int.Parse(results[0].ToString());
                    }
                }
            }

            return logIndex;
        }

        public static string GetLogNameByYear(int logYear)
        {
            List<object> objectValues = new List<object>
            {
                logYear
            };

            string logName = "";

            //ExecuteScalarFunction
            using (var results = ExecuteSimpleQueryConnection("Get_LogName_ByYear", objectValues))
            {
                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        logName = results[0].ToString();
                    }
                }
            }

            return logName;
        }

        private void RunCalendar()
        {
            if (cbCalendarMonth.SelectedIndex < 1 || cbCalendarLogs.SelectedIndex < 1)
            {
                return;
            }

            int monthIndex = cbCalendarMonth.SelectedIndex;
            int previousMonthIndex = monthIndex - 1;
            int daysInMonthPrevious = 1;
            Boolean startOfYear = false;
            
            
            int nextMonthIndex = monthIndex + 1;
            string firstDay = GetFirstDayForMonth(monthIndex);
            string logName = cbCalendarLogs.SelectedItem.ToString();
            int logIndex = GetLogIndexByName(logName);
            int logYear = GetLogYearByName(logName);
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

            if (currentYearNumber >= logYear && monthIndex > currentMonthNumber) {
                futureDays = true;
            }

            int day1 = 0;
            int day2 = 0;
            int day3 = 0;
            int day4 = 0;
            int day5 = 0;
            int day6 = 0;
            int day7 = 0;

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

            string textValue = GetTextCalendar();
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
                dataGridViewCalendar.DataSource = null;
                dataGridViewCalendar.Rows.Clear();
                dataGridViewCalendar.ColumnCount = 7;
                //dataGridViewPlanner.RowCount = 12;
                dataGridViewCalendar.Name = "Calendar View";
                dataGridViewCalendar.Columns[0].Name = "Sunday";
                dataGridViewCalendar.Columns[1].Name = "Monday";
                dataGridViewCalendar.Columns[2].Name = "Tuesday";
                dataGridViewCalendar.Columns[3].Name = "Wednesday";
                dataGridViewCalendar.Columns[4].Name = "Thursday";
                dataGridViewCalendar.Columns[5].Name = "Friday";
                dataGridViewCalendar.Columns[6].Name = "Saturday";

                dataGridViewCalendar.ColumnHeadersDefaultCellStyle.BackColor = Color.Silver;
                dataGridViewCalendar.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridViewCalendar.RowHeadersDefaultCellStyle.BackColor = Color.Silver;

                foreach (DataGridViewColumn column in dataGridViewCalendar.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                }

                dataGridViewCalendar.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewCalendar.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Resize the master DataGridView columns to fit the newly loaded data.
                //dataGridViewMonthly.AutoResizeColumns();
                dataGridViewCalendar.ReadOnly = true;
                dataGridViewCalendar.EnableHeadersVisualStyles = false;
                dataGridViewCalendar.AllowUserToOrderColumns = false;
                dataGridViewCalendar.AllowUserToAddRows = false;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridViewCalendar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewCalendar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                dataGridViewCalendar.ColumnHeadersHeight = 40;
                dataGridViewCalendar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridViewCalendar.RowHeadersVisible = true;

                dataGridViewCalendar.Columns[0].ValueType = typeof(string);
                dataGridViewCalendar.Columns[1].ValueType = typeof(string);
                dataGridViewCalendar.Columns[2].ValueType = typeof(string);
                dataGridViewCalendar.Columns[3].ValueType = typeof(string);
                dataGridViewCalendar.Columns[4].ValueType = typeof(string);
                dataGridViewCalendar.Columns[5].ValueType = typeof(string);
                dataGridViewCalendar.Columns[6].ValueType = typeof(string);

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

                //First Week of the month:
                if (day1 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (1 == currentDayNumber)
                        {
                            cellNumber = 0;
                            rowNumber = 0;
                            futureDays = true;
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
                        
                        miles1 = GetMilesByDate(logIndex, dateTime1).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime2).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime3).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime4).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime5).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                }
                else if (day2 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (2 == currentDayNumber)
                        {
                            cellNumber = 1;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime2 = new DateTime(logYear, monthIndex, 1);
                    DateTime dateTime3 = new DateTime(logYear, monthIndex, 2);
                    DateTime dateTime4 = new DateTime(logYear, monthIndex, 3);
                    DateTime dateTime5 = new DateTime(logYear, monthIndex, 4);
                    DateTime dateTime6 = new DateTime(logYear, monthIndex, 5);
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 6);
                    
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
                    } else
                    {
                        temp1 = daysInMonthPrevious.ToString();
                        DateTime dateTime2b = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        miles1 = GetMilesByDate(logIndex, dateTime2b).ToString();
                    }
                    
                    if (futureDays)
                    {
                        miles2 = "";
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    } else
                    {
                        miles2 = GetMilesByDate(logIndex, dateTime2).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime3).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime4).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime5).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                    

                }
                else if (day3 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (3 == currentDayNumber)
                        {
                            cellNumber = 2;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime3 = new DateTime(logYear, monthIndex, 1);
                    DateTime dateTime4 = new DateTime(logYear, monthIndex, 2);
                    DateTime dateTime5 = new DateTime(logYear, monthIndex, 3);
                    DateTime dateTime6 = new DateTime(logYear, monthIndex, 4);
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 5);
                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        miles1 = "";
                        miles2 = "";
                    }
                    else
                    {
                        DateTime dateTime31 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime32 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        miles1 = GetMilesByDate(logIndex, dateTime32).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime31).ToString();
                        temp1 = (daysInMonthPrevious - 1).ToString();
                        temp2 = (daysInMonthPrevious).ToString();
                    }
                    temp3 = "1";
                    temp4 = "2";
                    temp5 = "3";
                    temp6 = "4";
                    temp7 = "5";
                    
                    if (futureDays)
                    {
                        miles3 = "";
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    } else
                    {
                        miles3 = GetMilesByDate(logIndex, dateTime3).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime4).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime5).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                    

                }
                else if (day4 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (4 == currentDayNumber)
                        {
                            cellNumber = 3;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime4 = new DateTime(logYear, monthIndex, 1);
                    DateTime dateTime5 = new DateTime(logYear, monthIndex, 2);
                    DateTime dateTime6 = new DateTime(logYear, monthIndex, 3);
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 4);
                    if (startOfYear)
                    {
                        temp1 = "";
                        temp2 = "";
                        temp3 = "";
                        miles1 = "";
                        miles2 = "";
                        miles3 = "";
                    }
                    else
                    {
                        temp1 = (daysInMonthPrevious - 2).ToString();
                        temp2 = (daysInMonthPrevious - 1).ToString();
                        temp3 = (daysInMonthPrevious).ToString();
                        DateTime dateTime41 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime42 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime43 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        miles1 = GetMilesByDate(logIndex, dateTime43).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime42).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime41).ToString();
                    }
                    temp4 = "1";
                    temp5 = "2";
                    temp6 = "3";
                    temp7 = "4";
                    
                    if (futureDays)
                    {
                        miles4 = "";
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    } else
                    {
                        miles4 = GetMilesByDate(logIndex, dateTime4).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime5).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                    

                }
                else if (day5 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (5 == currentDayNumber)
                        {
                            cellNumber = 4;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime5 = new DateTime(logYear, monthIndex, 1);
                    DateTime dateTime6 = new DateTime(logYear, monthIndex, 2);
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 3);
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
                    else
                    {
                        temp1 = (daysInMonthPrevious - 3).ToString();
                        temp2 = (daysInMonthPrevious - 2).ToString();
                        temp3 = (daysInMonthPrevious - 1).ToString();
                        temp4 = (daysInMonthPrevious).ToString();
                        DateTime dateTime51 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime52 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime53 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime54 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        miles1 = GetMilesByDate(logIndex, dateTime54).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime53).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime52).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime51).ToString();
                    }
                    temp5 = "1";
                    temp6 = "2";
                    temp7 = "3";

                    if (futureDays)
                    {
                        miles5 = "";
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        miles5 = GetMilesByDate(logIndex, dateTime5).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }

                }
                else if (day6 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (6 == currentDayNumber)
                        {
                            cellNumber = 5;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime6 = new DateTime(logYear, monthIndex, 1);
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 2);
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
                    else
                    {
                        temp1 = (daysInMonthPrevious - 4).ToString();
                        temp2 = (daysInMonthPrevious - 3).ToString();
                        temp3 = (daysInMonthPrevious - 2).ToString();
                        temp4 = (daysInMonthPrevious - 1).ToString();
                        temp5 = (daysInMonthPrevious).ToString();
                        DateTime dateTime61 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime62 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime63 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime64 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        DateTime dateTime65 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 4);
                        miles1 = GetMilesByDate(logIndex, dateTime65).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime64).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime63).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime62).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime61).ToString();
                    }
                    temp6 = "1";
                    temp7 = "2";

                    if (futureDays)
                    {
                        miles6 = "";
                        miles7 = "";
                    }
                    else
                    {
                        miles6 = GetMilesByDate(logIndex, dateTime6).ToString();
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                }
                else if (day7 == 1)
                {
                    if (currentYearMonth)
                    {
                        if (7 == currentDayNumber)
                        {
                            cellNumber = 6;
                            rowNumber = 0;
                            futureDays = true;
                        }
                    }
                    DateTime dateTime7 = new DateTime(logYear, monthIndex, 1);
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
                    else
                    {
                        DateTime dateTime71 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious);
                        DateTime dateTime72 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 1);
                        DateTime dateTime73 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 2);
                        DateTime dateTime74 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 3);
                        DateTime dateTime75 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 4);
                        DateTime dateTime76 = new DateTime(logYear, previousMonthIndex, daysInMonthPrevious - 5);
                        miles1 = GetMilesByDate(logIndex, dateTime76).ToString();
                        miles2 = GetMilesByDate(logIndex, dateTime75).ToString();
                        miles3 = GetMilesByDate(logIndex, dateTime74).ToString();
                        miles4 = GetMilesByDate(logIndex, dateTime73).ToString();
                        miles5 = GetMilesByDate(logIndex, dateTime72).ToString();
                        miles6 = GetMilesByDate(logIndex, dateTime71).ToString();
                    }

                    if (futureDays)
                    {
                        miles7 = "";
                    }
                    else
                    {
                        miles7 = GetMilesByDate(logIndex, dateTime7).ToString();
                    }
                }

                if (miles1.Equals("0")){
                    miles1 = "OFF";
                }
                if (miles2.Equals("0"))
                {
                    miles2 = "OFF";
                }
                if (miles3.Equals("0"))
                {
                    miles3 = "OFF";
                }
                if (miles4.Equals("0"))
                {
                    miles4 = "OFF";
                }
                if (miles5.Equals("0"))
                {
                    miles5 = "OFF";
                }
                if (miles6.Equals("0"))
                {
                    miles6 = "OFF";
                }
                if (miles7.Equals("0"))
                {
                    miles7 = "OFF";
                }
                dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7);
                dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                dayCount = int.Parse(temp7);
                
                if (miles1.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                } else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles2.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles3.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles4.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles5.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles6.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.FromName(GetCalendarColor());
                }
                if (miles7.Equals(""))
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.White;
                }
                else
                {
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.FromName(GetCalendarColor());
                }

                rowCount++;
                dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.ForeColor = textColor;
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
                            miles1 = GetMilesByDate(logIndex, dateTime1a).ToString();
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
                        } else
                        {
                            DateTime dateTime1b = new DateTime(logYear, nextMonthIndex, 1);
                            DateTime dateTime1c = new DateTime(logYear, nextMonthIndex, 2);
                            DateTime dateTime1d = new DateTime(logYear, nextMonthIndex, 3);
                            DateTime dateTime1e = new DateTime(logYear, nextMonthIndex, 4);
                            DateTime dateTime1f = new DateTime(logYear, nextMonthIndex, 5);
                            DateTime dateTime1g = new DateTime(logYear, nextMonthIndex, 6);
                            miles2 = GetMilesByDate(logIndex, dateTime1b).ToString();
                            miles3 = GetMilesByDate(logIndex, dateTime1c).ToString();
                            miles4 = GetMilesByDate(logIndex, dateTime1d).ToString();
                            miles5 = GetMilesByDate(logIndex, dateTime1e).ToString();
                            miles6 = GetMilesByDate(logIndex, dateTime1f).ToString();
                            miles7 = GetMilesByDate(logIndex, dateTime1g).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                        miles1 = GetMilesByDate(logIndex, dateTime1a).ToString();
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
                        }
                        else
                        {
                            miles2 = GetMilesByDate(logIndex, dateTime2a).ToString();
                        }
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
                            miles3 = GetMilesByDate(logIndex, dateTime2b).ToString();
                            miles4 = GetMilesByDate(logIndex, dateTime2c).ToString();
                            miles5 = GetMilesByDate(logIndex, dateTime2d).ToString();
                            miles6 = GetMilesByDate(logIndex, dateTime2e).ToString();
                            miles7 = GetMilesByDate(logIndex, dateTime2f).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                        miles2 = GetMilesByDate(logIndex, dateTime2a).ToString();
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
                        }
                        else
                        {
                            miles3 = GetMilesByDate(logIndex, dateTime3a).ToString();
                        }
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
                            miles4 = GetMilesByDate(logIndex, dateTime3b).ToString();
                            miles5 = GetMilesByDate(logIndex, dateTime3c).ToString();
                            miles6 = GetMilesByDate(logIndex, dateTime3d).ToString();
                            miles7 = GetMilesByDate(logIndex, dateTime3e).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                        }
                        else { 
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                        miles3 = GetMilesByDate(logIndex, dateTime3a).ToString();
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
                        }
                        else
                        {
                            miles4 = GetMilesByDate(logIndex, dateTime4a).ToString();
                        }
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
                            miles5 = GetMilesByDate(logIndex, dateTime4b).ToString();
                            miles6 = GetMilesByDate(logIndex, dateTime4c).ToString();
                            miles7 = GetMilesByDate(logIndex, dateTime4d).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                        miles4 = GetMilesByDate(logIndex, dateTime4a).ToString();
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
                        }
                        else
                        {
                            miles5 = GetMilesByDate(logIndex, dateTime5a).ToString();
                        }
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
                            miles6 = GetMilesByDate(logIndex, dateTime5b).ToString();
                            miles7 = GetMilesByDate(logIndex, dateTime5c).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                        miles5 = GetMilesByDate(logIndex, dateTime5a).ToString();
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
                        }
                        else
                        {
                            miles6 = GetMilesByDate(logIndex, dateTime6a).ToString();
                        }
                        if (nextMonthIndex == 13)
                        {
                            //Just post a blank entry for the next year Jan days:
                            miles7 = "";
                        }
                        else
                        {
                            DateTime dateTime6b = new DateTime(logYear, nextMonthIndex, 1);
                            miles7 = GetMilesByDate(logIndex, dateTime6b).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.LightGray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.ForeColor = textColor;
                        if (futureDays)
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.White;
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.LightGray;

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
                    } else
                    {
                        miles6 = GetMilesByDate(logIndex, dateTime6a).ToString();
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
                            miles7 = GetMilesByDate(logIndex, dateTime7a).ToString();
                        }
                        if (miles1.Equals("0"))
                        {
                            miles1 = "OFF";
                        }
                        if (miles2.Equals("0"))
                        {
                            miles2 = "OFF";
                        }
                        if (miles3.Equals("0"))
                        {
                            miles3 = "OFF";
                        }
                        if (miles4.Equals("0"))
                        {
                            miles4 = "OFF";
                        }
                        if (miles5.Equals("0"))
                        {
                            miles5 = "OFF";
                        }
                        if (miles6.Equals("0"))
                        {
                            miles6 = "OFF";
                        }
                        if (miles7.Equals("0"))
                        {
                            miles7 = "OFF";
                        }
                        dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                        dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);
                        dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.ForeColor = textColor;
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.ForeColor = textColor;
                        
                        if (miles1.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        
                        if (miles2.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                        }

                        if (miles3.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                        }

                        if (miles4.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        
                        if (miles5.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                        }                   
                        
                        if (miles6.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.White;
                        } else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.FromName(GetCalendarColor());
                        }
                        
                        if (miles7.Equals(""))
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.FromName(GetCalendarColor());
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
                    } else
                    {
                        miles7 = GetMilesByDate(logIndex, dateTime7a).ToString();
                    }
                    if (miles1.Equals("0"))
                    {
                        miles1 = "OFF";
                    }
                    if (miles2.Equals("0"))
                    {
                        miles2 = "OFF";
                    }
                    if (miles3.Equals("0"))
                    {
                        miles3 = "OFF";
                    }
                    if (miles4.Equals("0"))
                    {
                        miles4 = "OFF";
                    }
                    if (miles5.Equals("0"))
                    {
                        miles5 = "OFF";
                    }
                    if (miles6.Equals("0"))
                    {
                        miles6 = "OFF";
                    }
                    if (miles7.Equals("0"))
                    {
                        miles7 = "OFF";
                    }
                    dataGridViewCalendar.Rows.Add(temp1, temp2, temp3, temp4, temp5, temp6, temp7, "");
                    dataGridViewCalendar.Rows.Add(miles1, miles2, miles3, miles4, miles5, miles6, miles7);                
                    
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.ForeColor = textColor;
                    dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.ForeColor = textColor;

                    dataGridViewCalendar.Rows[rowCount].DefaultCellStyle.BackColor = Color.Gray;

                    if (miles1.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[0].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[0].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[0].Style.BackColor = Color.Gray;
                    }
                    if (miles2.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[1].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[1].Style.BackColor = Color.Gray;
                    }
                    if (miles3.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[2].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[2].Style.BackColor = Color.Gray;
                    }
                    if (miles4.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[3].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[3].Style.BackColor = Color.Gray;
                    }
                    if (miles5.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[4].Style.BackColor = Color.Gray;
                    }
                    if (miles6.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[5].Style.BackColor = Color.Gray;
                    }
                    if (miles7.Equals(""))
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.White;
                        //dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.FromName(GetCalendarColor());
                        //dataGridViewCalendar.Rows[rowCount].Cells[6].Style.BackColor = Color.Gray;
                    }
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[1].Style.BackColor = Color.FromName(GetCalendarColor());
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[2].Style.BackColor = Color.FromName(GetCalendarColor());
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[3].Style.BackColor = Color.FromName(GetCalendarColor());
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[4].Style.BackColor = Color.FromName(GetCalendarColor());
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[5].Style.BackColor = Color.FromName(GetCalendarColor());
                    //dataGridViewCalendar.Rows[rowCount + 1].Cells[6].Style.BackColor = Color.FromName(GetCalendarColor());

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
                    rowCount++;
                    weekNumber++;
                }

                //Highlight the current day if on the curernt month and year:
                if (currentYearMonth)
                {
                    dataGridViewCalendar.CurrentCell = dataGridViewCalendar.Rows[rowNumber].Cells[cellNumber];
                }

                //First Week of the month:
                if (day1 == 1)
                {
                    //no changes:
                }
                else if (day2 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;

                }
                else if (day3 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[1].Style.BackColor = Color.LightGray;

                }
                else if (day4 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[2].Style.BackColor = Color.LightGray;

                }
                else if (day5 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[3].Style.BackColor = Color.LightGray;

                }
                else if (day6 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[4].Style.BackColor = Color.LightGray;
                }
                else if (day7 == 1)
                {
                    dataGridViewCalendar.Rows[0].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[0].Cells[5].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[0].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[1].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[2].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[3].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[4].Style.BackColor = Color.LightGray;
                    dataGridViewCalendar.Rows[1].Cells[5].Style.BackColor = Color.LightGray;
                }

                //dataGridViewPlanner.Columns[0].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[8].DefaultCellStyle.BackColor = Color.Khaki;
                //dataGridViewPlanner.Rows[0].Cells[7].Style.BackColor = Color.Khaki;
                //dataGridViewPlanner.Columns[0].Width = 30;
                // Specify a larger font for the "Date" row. 
                using (Font font = new Font(
                    dataGridViewCalendar.DefaultCellStyle.Font.FontFamily, 23, FontStyle.Bold))
                {
                    dataGridViewCalendar.Rows[0].DefaultCellStyle.Font = font;
                    dataGridViewCalendar.Rows[2].DefaultCellStyle.Font = font;
                    dataGridViewCalendar.Rows[4].DefaultCellStyle.Font = font;
                    dataGridViewCalendar.Rows[6].DefaultCellStyle.Font = font;
                    dataGridViewCalendar.Rows[8].DefaultCellStyle.Font = font;
                    if (sixRow)
                    {
                        dataGridViewCalendar.Rows[10].DefaultCellStyle.Font = font;
                    }
                }

                dataGridViewCalendar.Rows[0].Height = 35;
                dataGridViewCalendar.Rows[1].Height = 35;
                dataGridViewCalendar.Rows[2].Height = 35;
                dataGridViewCalendar.Rows[3].Height = 35;
                dataGridViewCalendar.Rows[4].Height = 35;
                dataGridViewCalendar.Rows[5].Height = 35;
                dataGridViewCalendar.Rows[6].Height = 35;
                dataGridViewCalendar.Rows[7].Height = 35;
                dataGridViewCalendar.Rows[8].Height = 35;
                dataGridViewCalendar.Rows[9].Height = 35;

                //Changes if a 6th week is needed:
                if (sixRow)
                {
                    dataGridViewCalendar.Rows[10].Height = 35;
                    dataGridViewCalendar.Rows[11].Height = 35;
                    Size gridSize = new Size(830, 470);
                    dataGridViewCalendar.Size = gridSize;
                } else
                {
                    Size gridSize = new Size(830, 400);
                    dataGridViewCalendar.Size = gridSize;
                }

                dataGridViewCalendar.AllowUserToResizeRows = false;
                dataGridViewCalendar.AllowUserToResizeColumns = false;
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query Calendar: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering Calendar.  Review the log for more information.");
            }

            dataGridViewCalendar.Refresh();
        }

        private void cbCalendarMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCalendarMonth.SelectedIndex < 1)
            {
                lbMonth.Text = "";
            } else
            {
                lbMonth.Text = cbCalendarMonth.SelectedItem.ToString();
            }
            
            RunCalendar();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            RunCalendar();
        }

        private void cbCalendarColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string color = cbCalendarColors.SelectedItem.ToString();
            color = color.Replace("\t", "");
            tbCalendarColor.BackColor = Color.FromName(color);
        }

        private void cbCalendarTextColor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCalendarTextColor.Checked)
            {
                tbCalendarColor.ForeColor = Color.Black;
            }
            else
            {
                tbCalendarColor.ForeColor = Color.White;
            }
        }
    }
}
