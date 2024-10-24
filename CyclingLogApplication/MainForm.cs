﻿using System;
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

        private static readonly string logVersion = "0.9.1";
        private static int logLevel = 0;
        private static string gridOrder;
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
        private static string idColumn = "0";
        private static string firstDayOfWeek;
        private static string license;
        public static string customField1;
        public static string customField2;
        private static int heightCLB;
        private static string gridMaintColor;
        private static string gridWeeklyColor;
        private static string gridDisplayDataColor;
        private static string gridBikeColor;
        private static string gridRouteColor;

        private static string gridMaintTextColor;
        private static string gridWeeklyTextColor;
        private static string gridDisplayTextColor;
        private static string gridBikeTextColor;
        private static string gridRouteTextColor;

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
                //ConfigurationFile.ReadConfigFile(true);

                if (GetLicenseAgreement().Equals("False"))
                {
                    DialogResult result = MessageBox.Show("Do you agree with the License Agreement?\n\nThe MIT License Copyright (c) 2024, John T Flynn Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the Software), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. THE SOFTWARE IS PROVIDED AS IS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.", "License Agreement", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        SetLicenseAgreement("True");
                        //ConfigurationFile.WriteConfigFile();
                    }
                }

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

                //Get all values and load the comboboxes:
                List<string> logYearList = ReadDataNames("Table_Log_year", "Name");
                List<string> routeList = ReadDataNames("Table_Routes", "Name");
                SetRoutes(routeList);
                List<string> bikeList = ReadDataNames("Table_Bikes", "Name");

                SetLogNameIDDictionary(logYearList);

                //RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                ChartForm chartForm = new ChartForm();

                //Set first option of 'None':
                cbLogYear1.Items.Add("--Select Value--");
                cbLogYear2.Items.Add("--Select Value--");
                cbLogYear3.Items.Add("--Select Value--");
                cbLogYear4.Items.Add("--Select Value--");
                cbLogYear5.Items.Add("--Select Value--");

                cbLogYearConfig.Items.Add("--Select Value--");
                //rideDataDisplayForm.cbLogYearFilter.Items.Add("--Select Value--");

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
                    //rideDataDisplayForm.cbLogYearFilter.Items.Add(val);
                    chartForm.cbLogYearChart.Items.Add(val);
                    Logger.Log("Data Loading: Log Year: " + val, logSetting, 1);
                }

                cbLogYearConfig.SelectedIndex = 0;

                if (GetLastLogSelectedDataEntry() != 0)
                {
                    rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = GetLastLogSelectedDataEntry();
                }

                //Load Route values:
                foreach (var val in routeList)
                {
                    rideDataEntryForm.cbRouteDataEntry.Items.Add(val);
                    chartForm.cbRoutesChart.Items.Add(val);
                    Logger.Log("Data Loading: Route: " + val, logSetting, 1);
                }

                cbBikeMaint.Items.Add("--Select Value--");
                //Load Bike values:
                foreach (var val in bikeList)
                {
                    cbBikeMaint.Items.Add(val);
                    Logger.Log("Data Loading: Bikes: " + val, logSetting, 1);
                }

                cbBikeMaint.SelectedIndex = 0;

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

                formloading = true;

                //Set first option of 'None':
                cbStatMonthlyLogYear.Items.Add("--Select Value--");
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

                int currentYear = DateTime.Now.Year;

                cbLogYear.Items.Add("--Select Value--");
                //cbLogYear
                for (int i = 2010; i <= currentYear; i++)
                {
                    cbLogYear.Items.Add(i.ToString());
                }
                cbLogYear.SelectedIndex = 0;

                //tabControl1.SelectedTab = tabControl1.TabPages["Main"];
                cbMaintColors.SelectedIndex = cbMaintColors.FindStringExact(gridMaintColor);
                cbWeeklyColors.SelectedIndex = cbWeeklyColors.FindStringExact(gridWeeklyColor);
                cbDisplayDataColors.SelectedIndex = cbDisplayDataColors.FindStringExact(gridDisplayDataColor);
                cbBikeColors.SelectedIndex = cbBikeColors.FindStringExact(gridBikeColor);
                cbRouteColors.SelectedIndex = cbRouteColors.FindStringExact(gridRouteColor);

                if (GetTextMaint().Equals("True")){
                    cbMaintTextColor.Checked = true;
                    tbColorMaint.ForeColor = Color.Black;
                } else
                {
                    cbMaintTextColor.Checked = false;
                    tbColorMaint.ForeColor = Color.White;
                }

                if (GetTextMaint().Equals("True"))
                {
                    cbWeeklyTextColor.Checked = true;
                    tbColorWeekly.ForeColor = Color.Black;
                }
                else
                {
                    cbWeeklyTextColor.Checked = false;
                    tbColorWeekly.ForeColor = Color.White;
                }

                if (GetTextMaint().Equals("True"))
                {
                    cbDisplayDataTextColor.Checked = true;
                    tbColorDisplayData.ForeColor = Color.Black;
                }
                else
                {
                    cbDisplayDataTextColor.Checked = false;
                    tbColorDisplayData.ForeColor = Color.White;
                }

                if (GetTextMaint().Equals("True"))
                {
                    cbBikeTextColor.Checked = true;
                    tbBikeColor.ForeColor = Color.Black;
                }
                else
                {
                    cbBikeTextColor.Checked = false;
                    tbBikeColor.ForeColor = Color.White;
                }

                if (GetTextMaint().Equals("True"))
                {
                    cbRouteTextColor.Checked = true;
                    tbRouteColor.ForeColor = Color.Black;
                }
                else
                {
                    cbRouteTextColor.Checked = false;
                    tbRouteColor.ForeColor = Color.White;
                }

                RefreshData();
                formloading = false;

                tbMaintAddUpdate.Text = "Add";

            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to load form. " + ex.Message.ToString());
            }

        }

        private void CloseForm(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit Application", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
                //ChartForm chartForm = new ChartForm();
                //chartForm.Close();
                //RideDataEntry rideDataEntryForm = new RideDataEntry();
                ConfigurationFile.WriteConfigFile();
                //rideDataDisplayForm.Dispose();
                //chartForm.Dispose();
                //rideDataEntryForm.Dispose();
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

        public static string GetTextMaint()
        {
            return gridMaintTextColor;
        }

        public static string GetTextWeekly()
        {
            return gridWeeklyTextColor;
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
        public static string GetcbStatistic1()
        {
            return cbStatistic1;
        }
        public static string GetcbStatistic2()
        {
            return cbStatistic2;
        }
        public static string GetcbStatistic3()
        {
            return cbStatistic3;
        }
        public static string GetcbStatistic4()
        {
            return cbStatistic4;
        }
        public static string GetcbStatistic5()
        {
            return cbStatistic5;
        }

        public static void SetcbStatistic1(string setcbStatistic1Config)
        {
            cbStatistic1 = setcbStatistic1Config;
        }
        public static void SetcbStatistic2(string setcbStatistic2Config)
        {
            cbStatistic2 = setcbStatistic2Config;
        }
        public static void SetcbStatistic3(string setcbStatistic3Config)
        {
            cbStatistic3 = setcbStatistic3Config;
        }
        public static void SetcbStatistic4(string setcbStatistic4Config)
        {
            cbStatistic4 = setcbStatistic4Config;
        }
        public static void SetcbStatistic5(string setcbStatistic5Config)
        {
            cbStatistic5 = setcbStatistic5Config;
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

        //public static void SetFieldNameList(List<string> fieldNames)
        //{
        //    for (int i = 0; i < fieldNames.Count; i++)
        //    {
        //        fieldNamesList.Add(fieldNames.ElementAt(i));
        //    }
        //}

        public static Dictionary<string, string> GetFieldsDictionary()
        {
            return fieldNameDict;
        }

        //public static List<string> GetFieldNamesList()
        //{
        //    return fieldNamesList;
        //}

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
            List<string> logYearsList = MainForm.ReadDataNames("Table_Log_year", "Name");

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

        //Disable x close option:
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle |= CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}

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

        private void LogTitleSave()
        {
            string logYearTitle = tbLogYearConfig.Text;
            string logType = "Add";
            RideDataEntry rideDataEntryForm = new RideDataEntry();
            RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
            ChartForm chartForm = new ChartForm();

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

                // Skip first item:
                for (int i = 1; i < cbLogYearConfig.Items.Count; i++)
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
                cbLogYear1.Items.Add("--Select Value--");
                cbLogYear2.Items.Add("--Select Value--");
                cbLogYear3.Items.Add("--Select Value--");
                cbLogYear4.Items.Add("--Select Value--");
                cbLogYear5.Items.Add("--Select Value--");

                for (int i = 0; i < tempList.Count; i++)
                {
                    // -1 since do not want to include the --not select-- option:
                    if (cbLogYearConfigIndex -1 == i)
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

                        break;
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

            //Update combo's on stat tab:
            cbLogYear1.Items.Add(logYearTitle);
            cbLogYear2.Items.Add(logYearTitle);
            cbLogYear3.Items.Add(logYearTitle);
            cbLogYear4.Items.Add(logYearTitle);
            cbLogYear5.Items.Add(logYearTitle);

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
                int yearStat1 = cbLogYear1.SelectedIndex;
                int yearStat2 = cbLogYear2.SelectedIndex;
                int yearStat3 = cbLogYear3.SelectedIndex;
                int yearStat4 = cbLogYear4.SelectedIndex;
                int yearStat5 = cbLogYear5.SelectedIndex;
                int monthStat = cbStatMonthlyLogYear.SelectedIndex;

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
                if (yearStat1 == deleteLogIndex)
                {
                    cbLogYear1.SelectedIndex = 0;
                }
                if (yearStat2 == deleteLogIndex)
                {
                    cbLogYear2.SelectedIndex = 0;
                }
                if (yearStat3 == deleteLogIndex)
                {
                    cbLogYear3.SelectedIndex = 0;
                }
                if (yearStat4 == deleteLogIndex)
                {
                    cbLogYear4.SelectedIndex = 0;
                }
                if (yearStat5 == deleteLogIndex)
                {
                    cbLogYear5.SelectedIndex = 0;
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

        //public static int GetLogYearIndex(string logYearName)
        //{
        //    List<object> objectValues = new List<object>
        //    {
        //        logYearName
        //    };

        //    int logIndex = 0;

        //    //ExecuteScalarFunction
        //    using (var results = ExecuteSimpleQueryConnection("Get_LogYear_Index_Name", objectValues))
        //    {
        //        if (results.HasRows)
        //        {
        //            while (results.Read())
        //            {
        //                logIndex = int.Parse(results[0].ToString());
        //            }
        //        }
        //    }

        //    return logIndex;
        //}

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
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                rideDataEntryForm.AddRouteDataEntry(routeName);
                ChartForm chartForm = new ChartForm();
                chartForm.cbRoutesChart.Items.Add(routeName);
            }
            // Update an existing Route:
            else
            {
                RideDataEntry rideDataEntryForm = new RideDataEntry();
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

                int routeIndex = rideDataEntryForm.cbRouteDataEntry.Items.IndexOf(routeOldName);
                ChartForm chartForm = new ChartForm();

                rideDataEntryForm.cbRouteDataEntry.Items.Remove(routeOldName);
                rideDataEntryForm.cbRouteDataEntry.Items.Add(routeName);
                rideDataEntryForm.cbRouteDataEntry.Sorted = true;

                chartForm.cbRoutesChart.Items.Remove(routeOldName);
                chartForm.cbRoutesChart.Items.Add(routeName);
                chartForm.cbRoutesChart.Sorted = true;

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
            string routeString1 = "Miscellaneous Route";
            string routeString2 = "--Indoor Training--";
            int logSetting = GetLogLevel();

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

            rideDataEntryForm.AddRouteDataEntry(routeString1);
            rideDataEntryForm.AddRouteDataEntry(routeString2);

            chartForm.cbRoutesChart.Items.Add(routeString1);
            chartForm.cbRoutesChart.Items.Add(routeString2);

            SetRoutes(ReadDataNames("Table_Routes", "Name"));
            RefreshRoutes();
        }

        private void BtRemoveRouteConfig(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete the Route option?", "Delete Route Option", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string deleteValue = tbRouteConfig.Text;
                RideDataEntry rideDataEntryForm = new RideDataEntry();
                //Note: only removing value as an option, all records using this value are unchanged:
                rideDataEntryForm.RemoveRouteDataEntry(deleteValue);

                ChartForm chartForm = new ChartForm();
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

                    RefreshRoutes();
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

            RideDataEntry rideDataEntryForm = new RideDataEntry();

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
                cbBikeMaint.Items.Add(tbConfigMilesNotInLog.Text);
                rideDataEntryForm.AddBikeDataEntry(bikeName);
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

                rideDataEntryForm.cbBikeDataEntrySelection.Items.RemoveAt(bikeIndex);
                rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(bikeName);
                rideDataEntryForm.cbBikeDataEntrySelection.Sorted = true;

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
                RideDataEntry rideDataEntryForm = new RideDataEntry();

                //Note: only removing value as an option, all records using this value are unchanged:
                cbBikeMaint.Items.Remove(bikeName);
                rideDataEntryForm.RemoveBikeDataEntry(bikeName);

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
            returnValue = miles.ToString("N0");

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
            returnValue = elevgain.ToString("N0");

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

        private static int GetCurrentWeekCount()
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

            return weekValue;
        }

        private static int GetCurrentDayCount()
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
                logYearIndex = GetLogYearIndex_ByName(cbLogYear1.SelectedItem.ToString());
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
                logYearIndex = GetLogYearIndex_ByName(cbLogYear2.SelectedItem.ToString());

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
                logYearIndex = GetLogYearIndex_ByName(cbLogYear3.SelectedItem.ToString());

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
                logYearIndex = GetLogYearIndex_ByName(cbLogYear4.SelectedItem.ToString());

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
                logYearIndex = GetLogYearIndex_ByName(cbLogYear5.SelectedItem.ToString());

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
            tbHighWeekAll.Text = GetMonthlyHighMileageWeekNumberAll().ToString();
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

        private void Cb1LogYear_changed(object sender, EventArgs e)
        {
            int logYearIndex = GetLogYearIndex_ByName(cbLogYear1.SelectedItem.ToString());
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
            int logYearIndex = GetLogYearIndex_ByName(cbLogYear2.SelectedItem.ToString());
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
            int logYearIndex = GetLogYearIndex_ByName(cbLogYear3.SelectedItem.ToString());
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
            int logYearIndex = GetLogYearIndex_ByName(cbLogYear4.SelectedItem.ToString());
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
            int logYearIndex = GetLogYearIndex_ByName(cbLogYear5.SelectedItem.ToString());
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

        //TODO: NEEDS TO BE UPDATED BEFORE IT CAN BE RUN:
        //NEED TO REENABLE - this.button11.Click += new System.EventHandler(this.ImportFromExcelLog) in designer.cs
        //This option is currently not visable:

        //public void ImportFromExcelLog(object sender, EventArgs e)
        //{
        //    int logIndex;

        //    //window to selct the index:
        //    using (LegacyImport legacyImport = new LegacyImport())
        //    {
        //        legacyImport.ShowDialog();

        //        logIndex = LegacyImport.GetLegacyIndexSelection() + 1;

        //        if (logIndex < 1)
        //        {
        //            return;
        //        }
        //    }

        //    List<object> objectValues = new List<object>();
        //    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        //    Calendar cal = dfi.Calendar;

        //    using (OpenFileDialog openfileDialog = new OpenFileDialog() { Filter = "CSV|*.csv", Multiselect = false })
        //    {
        //        if (openfileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            string line;
        //            //Check if the file is used by another process
        //            try
        //            {
        //                using (StreamReader file = new StreamReader(openfileDialog.FileName))
        //                {
        //                    int rowCount = 0;

        //                    while ((line = file.ReadLine()) != null)
        //                    {
        //                        var tempList = line.Split(',');

        //                        if (rowCount == 0)
        //                        {
        //                            //Line 1 is the headings                  
        //                            //MessageBox.Show(headingList[0]);
        //                        }
        //                        else
        //                        {
        //                            //MessageBox.Show(line);
        //                            objectValues.Clear();
        //                            string[] splitList = line.Split(',');

        //                            objectValues.Add(splitList[1]);     //Moving Time:
        //                            objectValues.Add(splitList[2]);     //Ride Distance:
        //                            objectValues.Add(splitList[3]);     //Average Speed:
        //                            objectValues.Add(splitList[4]);     //Bike:
        //                            objectValues.Add(splitList[5]);     //Ride Type:                            
        //                            objectValues.Add(splitList[7]);     //Wind:
        //                            objectValues.Add(splitList[8]);     //Temp:
        //                            objectValues.Add(splitList[0]);     //Date:
        //                            objectValues.Add(splitList[9]);     //Average Cadence:
        //                            objectValues.Add(splitList[10]);     //Average Heart Rate:
        //                            objectValues.Add(splitList[11]);     //Max Heart Rate:
        //                            objectValues.Add(splitList[15]);     //Calories:
        //                            objectValues.Add(splitList[12]);     //Total Ascent:
        //                            objectValues.Add(splitList[13]);     //Total Descent:
        //                            objectValues.Add(splitList[16]);     //Max Speed:
        //                            objectValues.Add(null);              //Average Power:
        //                            objectValues.Add(null);              //Max Power:
        //                            objectValues.Add(splitList[17]);     //Route:

        //                            string comment = "";
        //                            if (splitList.Length > 19)
        //                            {
        //                                //Get the total:
        //                                int arraySize = splitList.Length;
        //                                for (int index = 18; index < arraySize; index++)
        //                                {
        //                                    comment += splitList[index];
        //                                }
        //                            }

        //                            objectValues.Add(comment);     //Comments:
        //                            objectValues.Add(logIndex);         //LogYear index:

        //                            //Need to figure out the week from the ride date:
        //                            DateTime rideDate = Convert.ToDateTime(splitList[0]);
        //                            string firstDay = GetFirstDayOfWeek();
        //                            int weekValue = 0;

        //                            if (firstDay.Equals("Sunday"))
        //                            {
        //                                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Sunday);
        //                            }
        //                            else
        //                            {
        //                                weekValue = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, DayOfWeek.Monday);
        //                            }
        //                            objectValues.Add(weekValue);        //Week number:
        //                            objectValues.Add(splitList[14]);     //Location:
        //                            objectValues.Add(null);     //Windchill:
        //                            objectValues.Add(splitList[6]);     //Effort:

        //                            using (var results = ExecuteSimpleQueryConnection("Ride_Information_Add", objectValues))
        //                            {
        //                                //string ToReturn = "";
        //                                //if (results.HasRows)
        //                                //    while (results.Read())
        //                                //        ToReturn = results.GetString(results.GetOrdinal("field1"));
        //                            }
        //                        }

        //                        rowCount++;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.LogError("[ERROR]: Exception while trying to read the .CVS file." + ex.Message.ToString());
        //                MessageBox.Show("[ERROR] An error occured while reading the .CVS file. \n\n");
        //                return;
        //            }
        //        }
        //    }

        //    //Go through list of Routes and see if any of them need to be added to the Routes table:
        //    List<string> currentRouteList = new List<string>();
        //    for (int index = 0; index < cbRouteConfig.Items.Count; index++)
        //    {
        //        currentRouteList.Add(cbRouteConfig.GetItemText(cbRouteConfig.Items[index]));
        //    }

        //    List<string> routeList = ReadDataNames("Table_Ride_Information", "Route");
        //    foreach (var route in routeList)
        //    {
        //        if (!currentRouteList.Contains(route))
        //        {
        //            currentRouteList.Add(route);
        //            cbRouteConfig.Items.Add(route);
        //            RideDataEntry rideDataEntryForm = new RideDataEntry();
        //            rideDataEntryForm.cbRouteDataEntry.Items.Add(route);

        //            //Add new entry to the Route Table:
        //            List<object> routeObjectValues = new List<object>();
        //            routeObjectValues.Add(route);
        //            RunStoredProcedure(routeObjectValues, "Route_Add");
        //        }
        //    }

        //    //Now go through the list of Bikes and see if any of them need to be added to the Bike table:
        //    List<string> currentBikeList = new List<string>();
        //    for (int index = 0; index < cbBikeConfig.Items.Count; index++)
        //    {
        //        currentBikeList.Add(cbBikeConfig.GetItemText(cbBikeConfig.Items[index]));
        //    }

        //    List<string> bikeList = ReadDataNames("Table_Ride_Information", "Bike");
        //    foreach (var bike in bikeList)
        //    {
        //        if (!currentBikeList.Contains(bike))
        //        {
        //            currentBikeList.Add(bike);
        //            cbBikeConfig.Items.Add(bike);
        //            RideDataEntry rideDataEntryForm = new RideDataEntry();
        //            rideDataEntryForm.cbBikeDataEntrySelection.Items.Add(bike);

        //            //Add new entry to the Route Table:
        //            List<object> bikeObjectValues = new List<object>();
        //            bikeObjectValues.Add(bike);
        //            RunStoredProcedure(bikeObjectValues, "Bike_Add");
        //        }
        //    }

        //    MessageBox.Show("Data Import successful.");
        //}

        //private void BRenameLogYear_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("Do you really want to Rename the Log Title?", "Rename Log", MessageBoxButtons.YesNo);
        //    if (result == DialogResult.Yes)
        //    {
        //        string newValue = tbLogYearConfig.Text;
        //        string oldValue;

        //        if (cbLogYearConfig.SelectedItem != null)
        //        {
        //            oldValue = cbLogYearConfig.SelectedItem.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Invalid Log Year selected.");
        //            return;
        //        }


        //        if (cbLogYear.SelectedItem == null)
        //        {
        //            MessageBox.Show("Invalid Log Year selected.");
        //            return;
        //        }

        //        string logYear = "";
        //        List<object> objectValues1 = new List<object>();
        //        objectValues1.Add(oldValue);
        //        try
        //        {
        //            //ExecuteScalarFunction
        //            using (var results = ExecuteSimpleQueryConnection("Log_Year_Get", objectValues1))
        //            {
        //                if (results.HasRows)
        //                {
        //                    while (results.Read())
        //                    {
        //                        logYear = results[0].ToString();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Logger.LogError("[ERROR]: Exception while trying to get Log Year." + ex.Message.ToString());
        //        }

        //        List<object> objectValues = new List<object>();
        //        objectValues.Add(newValue);
        //        objectValues.Add(oldValue);
        //        objectValues.Add(logYear);

        //        RunStoredProcedure(objectValues, "Log_Year_Update");

        //        List<string> tempList = new List<string>();
        //        RideDataEntry rideDataEntryForm = new RideDataEntry();
        //        RideDataDisplay rideDataDisplayForm = new RideDataDisplay();
        //        ChartForm chartForm = new ChartForm();

        //        int statIndex1 = cbLogYear1.SelectedIndex;
        //        int statIndex2 = cbLogYear2.SelectedIndex;
        //        int statIndex3 = cbLogYear3.SelectedIndex;
        //        int statIndex4 = cbLogYear4.SelectedIndex;
        //        int statIndex5 = cbLogYear5.SelectedIndex;

        //        int cbLogYearConfigIndex = cbLogYearConfig.SelectedIndex;
        //        int cbStatMonthlyLogYearIndex = cbStatMonthlyLogYear.SelectedIndex;
        //        int rideDataEntryIndex = rideDataEntryForm.cbLogYearDataEntry.SelectedIndex;
        //        int rideDataDisplayFormIndex = rideDataDisplayForm.cbLogYearFilter.SelectedIndex;
        //        int rideDataChartIndex = chartForm.cbLogYearChart.SelectedIndex;

        //        for (int i = 0; i < cbLogYearConfig.Items.Count; i++)
        //        {
        //            tempList.Add(cbLogYearConfig.Items[i].ToString());
        //        }

        //        cbLogYear1.DataSource = null;
        //        cbLogYear1.Items.Clear();
        //        cbLogYear2.DataSource = null;
        //        cbLogYear2.Items.Clear();
        //        cbLogYear3.DataSource = null;
        //        cbLogYear3.Items.Clear();
        //        cbLogYear4.DataSource = null;
        //        cbLogYear4.Items.Clear();
        //        cbLogYear5.DataSource = null;
        //        cbLogYear5.Items.Clear();

        //        //Set first option of 'None':
        //        cbLogYear1.Items.Add("--None--");
        //        cbLogYear2.Items.Add("--None--");
        //        cbLogYear3.Items.Add("--None--");
        //        cbLogYear4.Items.Add("--None--");
        //        cbLogYear5.Items.Add("--None--");

        //        for (int i = 0; i < tempList.Count; i++)
        //        {
        //            if (cbLogYearConfigIndex == i)
        //            {
        //                cbLogYearConfig.Items.Remove(oldValue);
        //                cbLogYearConfig.Items.Add(newValue);

        //                rideDataEntryForm.cbLogYearDataEntry.Items.Remove(oldValue);
        //                rideDataEntryForm.cbLogYearDataEntry.Items.Add(newValue);

        //                rideDataDisplayForm.cbLogYearFilter.Items.Remove(oldValue);
        //                rideDataDisplayForm.cbLogYearFilter.Items.Add(newValue);

        //                chartForm.cbLogYearChart.Items.Remove(oldValue);
        //                chartForm.cbLogYearChart.Items.Add(newValue);

        //                cbStatMonthlyLogYear.Items.Remove(oldValue);
        //                cbStatMonthlyLogYear.Items.Add(newValue);

        //                cbLogYear1.Items.Add(newValue);
        //                cbLogYear2.Items.Add(newValue);
        //                cbLogYear3.Items.Add(newValue);
        //                cbLogYear4.Items.Add(newValue);
        //                cbLogYear5.Items.Add(newValue);
        //            }
        //            else
        //            {
        //                cbLogYear1.Items.Add(tempList[i]);
        //                cbLogYear2.Items.Add(tempList[i]);
        //                cbLogYear3.Items.Add(tempList[i]);
        //                cbLogYear4.Items.Add(tempList[i]);
        //                cbLogYear5.Items.Add(tempList[i]);
        //            }
        //        }

        //        cbLogYearConfig.Sorted = true;
        //        rideDataEntryForm.cbLogYearDataEntry.Sorted = true;
        //        chartForm.cbLogYearChart.Sorted = true;
        //        cbStatMonthlyLogYear.Sorted = true;
        //        rideDataDisplayForm.cbLogYearFilter.Sorted = true;

        //        cbLogYearConfig.SelectedIndex = cbLogYearConfigIndex;
        //        rideDataEntryForm.cbLogYearDataEntry.SelectedIndex = rideDataEntryIndex;
        //        rideDataDisplayForm.cbLogYearFilter.SelectedIndex = rideDataDisplayFormIndex;
        //        chartForm.cbLogYearChart.SelectedIndex = rideDataChartIndex;
        //        cbStatMonthlyLogYear.SelectedIndex = cbStatMonthlyLogYearIndex;

        //        cbLogYear1.Sorted = true;
        //        cbLogYear2.Sorted = true;
        //        cbLogYear3.Sorted = true;
        //        cbLogYear4.Sorted = true;
        //        cbLogYear5.Sorted = true;

        //        cbLogYear1.SelectedIndex = statIndex1;
        //        cbLogYear2.SelectedIndex = statIndex2;
        //        cbLogYear3.SelectedIndex = statIndex3;
        //        cbLogYear4.SelectedIndex = statIndex4;
        //        cbLogYear5.SelectedIndex = statIndex5;
        //    }

        //    //NOTE: The Table_Ride_Information only contains the LogYearID and not the name:
        //}

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
                    
                    dgvMaint.DataSource = dataTable;
                    dgvMaint.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                    dgvMaint.EnableHeadersVisualStyles = false;
                    dgvMaint.Sort(dgvMaint.Columns["Date"], ListSortDirection.Descending);
                    dgvMaint.AllowUserToResizeRows = false;
                    dgvMaint.AllowUserToResizeColumns = false;
                    dgvMaint.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                    dgvMaint.AllowUserToAddRows = false;
                    dgvMaint.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMaintColor());

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
                Logger.LogError("[ERROR]: Exception while trying to Get Maintenance Log entry." + ex.Message.ToString());
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
        //Replaced by Grid click:
        //private void BtMaintRetrieve_Click(object sender, EventArgs e)
        //{
        //    lbMaintError.Text = "";

        //    if (cbBikeMaint.SelectedIndex == -1)
        //    {
        //        //MessageBox.Show("A Bike option must be selected before continuing.");
        //        lbMaintError.Text = "A Bike option must be selected before continuing.";
        //        return;
        //    }

        //    List<object> objectValues = new List<object>();
        //    objectValues.Add(dateTimePicker1.Value);
        //    objectValues.Add(cbBikeMaint.SelectedItem.ToString());

        //    string comments;
        //    string mainID;
        //    string miles;

        //    try
        //    {
        //        //ExecuteScalarFunction
        //        using (var results = ExecuteSimpleQueryConnection("Maintenance_Get", objectValues))
        //        {
        //            if (results.HasRows)
        //            {
        //                while (results.Read())
        //                {
        //                    //MessageBox.Show(String.Format("{0}", results[0]));
        //                    //lbErrorMessage.Hide();
        //                    comments = results[0].ToString();
        //                    mainID = results[1].ToString();
        //                    miles = results[2].ToString();

        //                    //Load maintenance data page:
        //                    rtbMaintComments.Text = comments;
        //                    tbMaintID.Text = mainID;
        //                    tbMaintMiles.Text = miles;
        //                }
        //            }
        //            else
        //            {
        //                //lbErrorMessage.Show();
        //                //lbErrorMessage.Text = "No ride data found for the selected date.";
        //                //tbRecordID.Text = "0";
        //                //lbMaintError.Text = "No entry found for the selected Bike and Date.";
        //                Logger.LogError("WARNING: No entry found for the selected Bike and Date.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("[ERROR]: Exception while trying to retrive maintenance data." + ex.Message.ToString());
        //    }
        //}

        private void BtMaintRetrieve_Run(string date, string bike)
        {
            lbMaintError.Text = "";

            List<object> objectValues = new List<object>();
            objectValues.Add(date);
            objectValues.Add(bike);

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

        //private void CbBikeConfig_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    double notInMiles = 0;
        //    double logMiles = 0;
        //    double totalMiles = 0;

        //    //Load miles from the database:
        //    try
        //    {
        //        List<object> objectValues = new List<object>();

        //        if (cbBikeConfig.SelectedItem == null)
        //        {
        //            cbBikeConfig.SelectedIndex = 0;
        //            //bikeName = cbBikeConfig.SelectedItem.ToString();

        //            objectValues.Add(tbBikeConfig.Text);

        //        }
        //        else
        //        {
        //            objectValues.Add(cbBikeConfig.SelectedItem.ToString());
        //            tbBikeConfig.Text = cbBikeConfig.SelectedItem.ToString();
        //        }

        //        //ExecuteScalarFunction
        //        //Get Notinmiles
        //        using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
        //        {
        //            if (results.HasRows)
        //            {
        //                while (results.Read())
        //                {
        //                    notInMiles = float.Parse(results[0].ToString());
        //                    logMiles = float.Parse(results[1].ToString());
        //                    totalMiles = float.Parse(results[2].ToString());

        //                    notInMiles = Math.Round(notInMiles, 1);
        //                    logMiles = Math.Round(logMiles, 1);
        //                    totalMiles = Math.Round(totalMiles, 1);

        //                    tbConfigMilesNotInLog.Text = notInMiles.ToString();
        //                    tbBikeLogMiles.Text = logMiles.ToString();
        //                    tbBikeTotalMiles.Text = totalMiles.ToString();
        //                }
        //            }
        //            else
        //            {
        //                //MessageBox.Show("\"No entry found for the selected Bike and Date.");
        //                Logger.LogError("WARNING: No entry found for the selected Bike and Date.");
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogError("[ERROR]: Exception while trying to retrive Bike miles data." + ex.Message.ToString());
        //    }
        //}

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

            AddDefautRoute();
            Logger.Log("Adding a Route entry to the Configuration:" + "Miscellaneous Route:", 0, 0);
            Logger.Log("Adding a Route entry to the Configuration:" + "-- Indoor Training --:", 0, 0);

            SetCustomField1("");
            SetCustomField2("");

            SetLastLogSelectedDataEntry(0);
            SetcbStatistic1("0");
            SetcbStatistic2("0");
            SetcbStatistic3("0");
            SetcbStatistic4("0");
            SetcbStatistic5("0");
            SetLastLogFilterSelected(0);
            SetLastBikeSelected(-1);
            SetLastLogSelected(0);
            SetLastLogYearChartSelected(0);
            SetLastMonthlyLogSelected(0);
            SetLicenseAgreement("0");

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

            int logYearIndex = GetLogYearIndex_ByName(cbStatMonthlyLogYear.SelectedItem.ToString());

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
            MainForm.SetLastMonthlyLogSelected(cbStatMonthlyLogYear.SelectedIndex);
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
            GetMaintLog();
        }

        private void BtRefreshStatisticsData_Click(object sender, EventArgs e)
        {
            RefreshStatisticsData();
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
            returnValue = elevgain.ToString("N0");

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

        //private static string GetTotalElevGainYearly(int logIndex, int month)
        //{
        //    List<object> objectValues = new List<object>
        //    {
        //        logIndex,
        //        month
        //    };
        //    string returnValue = "0";
        //    int elevgain;

        //    //ExecuteScalarFunction
        //    using (var results = ExecuteSimpleQueryConnection("GetTotalElevGain_Monthly", objectValues))
        //    {
        //        if (results.HasRows)
        //        {
        //            while (results.Read())
        //            {
        //                string temp = results[0].ToString();

        //                if (temp.Equals(""))
        //                {
        //                    returnValue = "0";
        //                }
        //                else
        //                {
        //                    returnValue = temp;
        //                }
        //            }
        //        }
        //    }

        //    elevgain = Int32.Parse(returnValue);
        //    returnValue = elevgain.ToString("N0");

        //    return returnValue;
        //}

        //*******************************************
        //WEEKLY
        //*******************************************

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

        private static string GetAveragePaceWeekly(int logIndex, int weekNumber, string totalMiles)
        {

            if (totalMiles == "0")
            {
                return "0";
            }

            List<object> objectValues = new List<object>
            {
                logIndex,
                weekNumber
            };
            string returnValue;
            string[] splitValues;

            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            int hr = 0;
            int min = 0;
            double avgPace;
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
            RefreshStatisticsData();
            RunMonthlyStatistics();
            RefreshWeekly();
            RefreshBikes();
            RefreshRoutes();
            GetMaintLog();
        }

        private void BtRefreshData_Click(object sender, EventArgs e)
        {
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
                dataGridViewRoutes.Columns[1].Name = "Count";
                dataGridViewRoutes.Columns[0].Name = "Route Name";
                dataGridViewRoutes.Columns[1].ValueType = typeof(int);
                dataGridViewRoutes.Columns[0].ValueType = typeof(string);
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

        private void RefreshWeekly()
        {
            int logIndex;
            int logIndexPrevious;

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

            string tbDistanceWeek2;
            string lbweek2;
            string tbLongestRideWeek2;
            string tbElevGainWeek2;
            string tbNumRidesWeek2;
            string tbAvgSpeedWeek2;
            string tbTotalTimeWeekly2;
            string tbAvgPace2;

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
                    lbweek2 = "";
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
                    lbweek2 = GetDateFromWeekNumber(DateTime.Now.Year, weekNumber).ToString("MM/dd/yyyy");
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
                dataGridViewWeekly.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
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
            string miles = dgvMaint.Rows[rowindex].Cells[2].Value.ToString();
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

            tbMaintDateCheck.Text = dgvMaint.Rows[rowindex].Cells[0].Value.ToString();
            dateTimePicker1.Value = DateTime.Parse(dgvMaint.Rows[rowindex].Cells[0].Value.ToString());
            cbBikeMaint.SelectedIndex = bikeIndex + 1; // Add 1 to account for --select value--:
            tbMaintMiles.Text = miles;
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
            //Maintenance Grid:
            SetMaintColor(cbMaintColors.SelectedItem.ToString());
            dgvMaint.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetMaintColor());
            dgvMaint.Refresh();

            //Weekly Stat Grid:
            SetWeeklyColor(cbWeeklyColors.SelectedItem.ToString());
            dataGridViewWeekly.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(GetWeeklyColor());
            dataGridViewWeekly.Refresh();

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

            if (cbMaintTextColor.Checked)
            {
                SetTextMaint("True");
            } else
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

            MessageBox.Show("Color save complete.");
        }

        private void cbMaintColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbColorMaint.BackColor = Color.FromName(cbMaintColors.SelectedItem.ToString());
        }

        private void cbWeeklyColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbColorWeekly.BackColor = Color.FromName(cbWeeklyColors.SelectedItem.ToString());
        }

        private void cbDisplayDataColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbColorDisplayData.BackColor = Color.FromName(cbDisplayDataColors.SelectedItem.ToString());
        }

        private void cbBikeColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbBikeColor.BackColor = Color.FromName(cbBikeColors.SelectedItem.ToString());
        }

        private void cbRouteColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbRouteColor.BackColor = Color.FromName(cbRouteColors.SelectedItem.ToString());
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
            if (!int.TryParse(tbMaintMiles.Text, out _))
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

            //double notInMiles = 0;
            //double logMiles = 0;
            //double totalMiles = 0;

            //Load miles from the database:
            //try
            //{
            //    List<object> objectValues = new List<object>();

            //    if (cbBikeConfig.SelectedItem == null)
            //    {
            //        cbBikeConfig.SelectedIndex = 0;
            //        //bikeName = cbBikeConfig.SelectedItem.ToString();

            //        objectValues.Add(tbBikeConfig.Text);

            //    }
            //    else
            //    {
            //        objectValues.Add(cbBikeConfig.SelectedItem.ToString());
            //        tbBikeConfig.Text = cbBikeConfig.SelectedItem.ToString();
            //    }

            //    //ExecuteScalarFunction
            //    //Get Notinmiles
            //    using (var results = ExecuteSimpleQueryConnection("Bike_GetMiles", objectValues))
            //    {
            //        if (results.HasRows)
            //        {
            //            while (results.Read())
            //            {
            //                notInMiles = float.Parse(results[0].ToString());
            //                logMiles = float.Parse(results[1].ToString());
            //                totalMiles = float.Parse(results[2].ToString());

            //                notInMiles = Math.Round(notInMiles, 1);
            //                logMiles = Math.Round(logMiles, 1);
            //                totalMiles = Math.Round(totalMiles, 1);

            //                tbConfigMilesNotInLog.Text = notInMiles.ToString();
            //                tbBikeLogMiles.Text = logMiles.ToString();
            //                tbBikeTotalMiles.Text = totalMiles.ToString();
            //            }
            //        }
            //        else
            //        {
            //            //MessageBox.Show("\"No entry found for the selected Bike and Date.");
            //            Logger.LogError("WARNING: No entry found for the selected Bike and Date.");
            //            return;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.LogError("[ERROR]: Exception while trying to retrive Bike miles data." + ex.Message.ToString());
            //}
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
    }
}
