using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;

namespace CyclingLogApplication
{
    class ConfigurationFile
    {

        private static int logsRead;

        public static bool ReadConfigFile()
        {            
            //This will give us the full name path of the executable file:
            //i.e. C:\Program Files\MyApplication\MyApplication.exe
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            string path = strWorkPath + "\\settings";
            string pathFile = path + "\\CyclingLogConfig.xml";
            bool returnStatus = false;

            try
            {
                // Determine whether the directory and file exists.
                if (!Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                // Determine whether the directory and file exists.
                if (!Directory.Exists(strWorkPath + "\\database"))
                {
                    System.IO.Directory.CreateDirectory(strWorkPath + "\\database");
                }

                if (!Directory.Exists(path) || !File.Exists(pathFile))
                {
                    WriteConfigFile();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[ERROR] Exception while trying to read the Config file: " + e.ToString());

                return returnStatus;
            }

            XmlDocument doc = new XmlDocument();

            doc.Load(strWorkPath + "\\settings\\CyclingLogConfig.xml");
            //doc.Load(@"C:\\CyclingLogApplication\\CyclingLogConfig.xml");


            //=================================
            // Read from config file
            //=================================
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Config");
            string daysToKeepLogs = nodes.Item(0).SelectSingleNode("DAYSTOKEEPLOGS").InnerText;
            //string verison = nodes.Item(0).SelectSingleNode("VERSION").InnerText;
            int logLevel = Convert.ToInt32(nodes.Item(0).SelectSingleNode("LOGLEVEL").InnerText);

            string cbStatistic1 = nodes.Item(0).SelectSingleNode("cbStatistic1").InnerText;
            string cbStatistic2 = nodes.Item(0).SelectSingleNode("cbStatistic2").InnerText;
            string cbStatistic3 = nodes.Item(0).SelectSingleNode("cbStatistic3").InnerText;
            string cbStatistic4 = nodes.Item(0).SelectSingleNode("cbStatistic4").InnerText;
            string cbStatistic5 = nodes.Item(0).SelectSingleNode("cbStatistic5").InnerText;
            string cbStatistic6 = nodes.Item(0).SelectSingleNode("cbStatistic6").InnerText;
            string cbStatistic7 = nodes.Item(0).SelectSingleNode("cbStatistic7").InnerText;
            string cbStatistic8 = nodes.Item(0).SelectSingleNode("cbStatistic8").InnerText;
            string cbStatistic9 = nodes.Item(0).SelectSingleNode("cbStatistic9").InnerText;
            string cbStatistic10 = nodes.Item(0).SelectSingleNode("cbStatistic10").InnerText;
            string firstDayOfWeek = nodes.Item(0).SelectSingleNode("FIRSTDAY").InnerText;
            string license = nodes.Item(0).SelectSingleNode("LICENSE").InnerText;
            string gridOrder = nodes.Item(0).SelectSingleNode("GRIDORDER").InnerText;
            string idColumnValue = nodes.Item(0).SelectSingleNode("IDCOLUMN").InnerText;
            string customDataField1 = nodes.Item(0).SelectSingleNode("CUSTOMFIELD1").InnerText;
            string customDataField2 = nodes.Item(0).SelectSingleNode("CUSTOMFIELD2").InnerText;
            string colorMaint = nodes.Item(0).SelectSingleNode("COLORMAINT").InnerText;
            string colorWeekly = nodes.Item(0).SelectSingleNode("COLORWEEKLY").InnerText;
            string colorMonthly = nodes.Item(0).SelectSingleNode("COLORMONTHLY").InnerText;
            string colorYearly = nodes.Item(0).SelectSingleNode("COLORYEARLY").InnerText;
            string colorDisplayData = nodes.Item(0).SelectSingleNode("COLORDISPLAYDATA").InnerText;
            string colorBike = nodes.Item(0).SelectSingleNode("COLORBIKE").InnerText;
            string colorRoute = nodes.Item(0).SelectSingleNode("COLORROUTE").InnerText;

            string textMaint = nodes.Item(0).SelectSingleNode("COLORMAINTTEXT").InnerText;
            string textWeekly = nodes.Item(0).SelectSingleNode("COLORWEEKLYTEXT").InnerText;
            string textMonthly = nodes.Item(0).SelectSingleNode("COLORMONTHLYTEXT").InnerText;
            string textYearly = nodes.Item(0).SelectSingleNode("COLORYEARLYTEXT").InnerText;
            string textDisplay = nodes.Item(0).SelectSingleNode("COLORDISPLAYTEXT").InnerText;
            string textBike = nodes.Item(0).SelectSingleNode("COLORBIKETEXT").InnerText;
            string textRoute = nodes.Item(0).SelectSingleNode("COLORROUTETEXT").InnerText;

            Dictionary<string, string> fieldCheckDictionary = new Dictionary<string, string>();  

            string checkListBoxItemNAME0 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME0").InnerText;
            string checkListBoxItemNAME1 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME1").InnerText;
            string checkListBoxItemNAME2 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME2").InnerText;
            string checkListBoxItemNAME3 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME3").InnerText;
            string checkListBoxItemNAME4 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME4").InnerText;
            string checkListBoxItemNAME5 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME5").InnerText;
            string checkListBoxItemNAME6 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME6").InnerText;
            string checkListBoxItemNAME7 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME7").InnerText;
            string checkListBoxItemNAME8 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME8").InnerText;
            string checkListBoxItemNAME9 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME9").InnerText;
            string checkListBoxItemNAME10 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME10").InnerText;
            string checkListBoxItemNAME11 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME11").InnerText;
            string checkListBoxItemNAME12 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME12").InnerText;
            string checkListBoxItemNAME13 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME13").InnerText;
            string checkListBoxItemNAME14 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME14").InnerText;
            string checkListBoxItemNAME15 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME15").InnerText;
            string checkListBoxItemNAME16 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME16").InnerText;
            string checkListBoxItemNAME17 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME17").InnerText;
            string checkListBoxItemNAME18 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME18").InnerText;
            string checkListBoxItemNAME19 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME19").InnerText;
            string checkListBoxItemNAME20 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME20").InnerText;
            string checkListBoxItemNAME21 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME21").InnerText;
            string checkListBoxItemNAME22 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME22").InnerText;
            string checkListBoxItemNAME23 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME23").InnerText;
            string checkListBoxItemNAME24 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME24").InnerText;
            string checkListBoxItemNAME25 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXNAME25").InnerText;

            string checkListBoxItemCHECK0 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK0").InnerText;
            string checkListBoxItemCHECK1 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK1").InnerText;
            string checkListBoxItemCHECK2 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK2").InnerText;
            string checkListBoxItemCHECK3 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK3").InnerText;
            string checkListBoxItemCHECK4 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK4").InnerText;
            string checkListBoxItemCHECK5 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK5").InnerText;
            string checkListBoxItemCHECK6 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK6").InnerText;
            string checkListBoxItemCHECK7 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK7").InnerText;
            string checkListBoxItemCHECK8 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK8").InnerText;
            string checkListBoxItemCHECK9 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK9").InnerText;
            string checkListBoxItemCHECK10 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK10").InnerText;
            string checkListBoxItemCHECK11 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK11").InnerText;
            string checkListBoxItemCHECK12 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK12").InnerText;
            string checkListBoxItemCHECK13 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK13").InnerText;
            string checkListBoxItemCHECK14 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK14").InnerText;
            string checkListBoxItemCHECK15 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK15").InnerText;
            string checkListBoxItemCHECK16 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK16").InnerText;
            string checkListBoxItemCHECK17 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK17").InnerText;
            string checkListBoxItemCHECK18 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK18").InnerText;
            string checkListBoxItemCHECK19 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK19").InnerText;
            string checkListBoxItemCHECK20 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK20").InnerText;
            string checkListBoxItemCHECK21 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK21").InnerText;
            string checkListBoxItemCHECK22 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK22").InnerText;
            string checkListBoxItemCHECK23 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK23").InnerText;
            string checkListBoxItemCHECK24 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK24").InnerText;
            string checkListBoxItemCHECK25 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOXCHECK25").InnerText;

            fieldCheckDictionary.Add(checkListBoxItemNAME0, checkListBoxItemCHECK0);
            fieldCheckDictionary.Add(checkListBoxItemNAME1, checkListBoxItemCHECK1);
            fieldCheckDictionary.Add(checkListBoxItemNAME2, checkListBoxItemCHECK2);
            fieldCheckDictionary.Add(checkListBoxItemNAME3, checkListBoxItemCHECK3);
            fieldCheckDictionary.Add(checkListBoxItemNAME4, checkListBoxItemCHECK4);
            fieldCheckDictionary.Add(checkListBoxItemNAME5, checkListBoxItemCHECK5);
            fieldCheckDictionary.Add(checkListBoxItemNAME6, checkListBoxItemCHECK6);
            fieldCheckDictionary.Add(checkListBoxItemNAME7, checkListBoxItemCHECK7);
            fieldCheckDictionary.Add(checkListBoxItemNAME8, checkListBoxItemCHECK8);
            fieldCheckDictionary.Add(checkListBoxItemNAME9, checkListBoxItemCHECK9);
            fieldCheckDictionary.Add(checkListBoxItemNAME10, checkListBoxItemCHECK10);
            fieldCheckDictionary.Add(checkListBoxItemNAME11, checkListBoxItemCHECK11);
            fieldCheckDictionary.Add(checkListBoxItemNAME12, checkListBoxItemCHECK12);
            fieldCheckDictionary.Add(checkListBoxItemNAME13, checkListBoxItemCHECK13);
            fieldCheckDictionary.Add(checkListBoxItemNAME14, checkListBoxItemCHECK14);
            fieldCheckDictionary.Add(checkListBoxItemNAME15, checkListBoxItemCHECK15);
            fieldCheckDictionary.Add(checkListBoxItemNAME16, checkListBoxItemCHECK16);
            fieldCheckDictionary.Add(checkListBoxItemNAME17, checkListBoxItemCHECK17);
            fieldCheckDictionary.Add(checkListBoxItemNAME18, checkListBoxItemCHECK18);
            fieldCheckDictionary.Add(checkListBoxItemNAME19, checkListBoxItemCHECK19);
            fieldCheckDictionary.Add(checkListBoxItemNAME20, checkListBoxItemCHECK20);
            fieldCheckDictionary.Add(checkListBoxItemNAME21, checkListBoxItemCHECK21);
            fieldCheckDictionary.Add(checkListBoxItemNAME22, checkListBoxItemCHECK22);
            fieldCheckDictionary.Add(checkListBoxItemNAME23, checkListBoxItemCHECK23);
            fieldCheckDictionary.Add(checkListBoxItemNAME24, checkListBoxItemCHECK24);
            fieldCheckDictionary.Add(checkListBoxItemNAME25, checkListBoxItemCHECK25);

            //int heightCLB = 394;
            //int numberRemoved = 0;
            ////Check value of custom1:
            //if (customDataField1.Equals(""))
            //{
            //    fieldCheckDictionary.Remove("Custom1");
            //    numberRemoved++;
            //}
            //if (customDataField2.Equals(""))
            //{
            //    fieldCheckDictionary.Remove("Custom2");
            //    numberRemoved++;
            //}

            //if (numberRemoved == 1)
            //{
            //    heightCLB = 379;
            //} else if (numberRemoved == 2)
            //{
            //    heightCLB = 364;
            //}
            //Set checkedListbox height:
            //MainForm.SetHeightCLB(heightCLB);

            MainForm.SetFieldDictionary(fieldCheckDictionary);

            string lastLogYearSelected = nodes.Item(0).SelectSingleNode("LastLogSelected").InnerText;
            string lastBikeSelected = nodes.Item(0).SelectSingleNode("LastBikeSelected").InnerText;
            string lastLogYearFilterSelected = nodes.Item(0).SelectSingleNode("LastLogFilterSelected").InnerText;

            string chartLogYearSelected = nodes.Item(0).SelectSingleNode("ChartLogYear").InnerText;
            string chartRouteSelected = nodes.Item(0).SelectSingleNode("ChartRoute").InnerText;
            string chartTypeSelected = nodes.Item(0).SelectSingleNode("ChartType").InnerText;
            string chartTimeTypeSelected = nodes.Item(0).SelectSingleNode("ChartTimeType").InnerText;

            string lastMonthlyLogYearSelected = nodes.Item(0).SelectSingleNode("LastMonthlyLogSelected").InnerText;
            string lastLogYearSelectedDataEntry = nodes.Item(0).SelectSingleNode("LastLogSelectedDataEntry").InnerText;

            MainForm.SetLicenseAgreement(license);
            MainForm.SetIDColumn(idColumnValue);
            MainForm.SetLogLevel(logLevel);
            MainForm.SetGridOrder(gridOrder);
            MainForm.SetFirstDayOfWeek(firstDayOfWeek);
            MainForm.SetCustomField1(customDataField1);
            MainForm.SetCustomField2(customDataField2);
            MainForm.SetMaintColor(colorMaint);
            MainForm.SetWeeklyColor(colorWeekly);
            MainForm.SetMonthlyColor(colorMonthly);
            MainForm.SetYearlyColor(colorYearly);
            MainForm.SetDisplayDataColor(colorDisplayData);
            MainForm.SetBikeColor(colorBike);
            MainForm.SetRouteColor(colorRoute);

            MainForm.SetTextMaint(textMaint);
            MainForm.SetTextWeekly(textWeekly);
            MainForm.SetTextMonthly(textMonthly);
            MainForm.SetTextYearly(textYearly);
            MainForm.SetTextDisplay(textDisplay);
            MainForm.SetTextBike(textBike);
            MainForm.SetTextRoute(textRoute);

            MainForm.SetcbStatistic1(cbStatistic1);
            MainForm.SetcbStatistic2(cbStatistic2);
            MainForm.SetcbStatistic3(cbStatistic3);
            MainForm.SetcbStatistic4(cbStatistic4);
            MainForm.SetcbStatistic5(cbStatistic5);
            MainForm.SetcbStatistic6(cbStatistic6);
            MainForm.SetcbStatistic7(cbStatistic7);
            MainForm.SetcbStatistic8(cbStatistic8);
            MainForm.SetcbStatistic9(cbStatistic9);
            MainForm.SetcbStatistic10(cbStatistic10);
            MainForm.SetLastLogFilterSelected(Convert.ToInt32(lastLogYearFilterSelected));
            MainForm.SetLastBikeSelected(Convert.ToInt32(lastBikeSelected));
            MainForm.SetLastLogSelected(Convert.ToInt32(lastLogYearSelected));

            MainForm.SetLastLogYearChartSelected(Convert.ToInt32(chartLogYearSelected));
            MainForm.SetLastRouteChartSelected(Convert.ToInt32(chartRouteSelected));
            MainForm.SetLastTypeChartSelected(Convert.ToInt32(chartTypeSelected));
            MainForm.SetLastTypeTimeChartSelected(Convert.ToInt32(chartTimeTypeSelected));

            MainForm.SetLastMonthlyLogSelected(Convert.ToInt32(lastMonthlyLogYearSelected));
            MainForm.SetLastLogSelectedDataEntry(Convert.ToInt32(lastLogYearSelectedDataEntry));

            //NOTE: If the dateTime value is blank then a force update will be run and a new timestamp will be written at end of run:
            Logger.Log("Configuration Read: DAYSTOKEEPLOGS: " + daysToKeepLogs, logLevel, 0);
            Logger.Log("Configuration Read: LOGLEVEL : " + logLevel, logLevel, 0);
            //Logger.Log("Configuration Read: VERSION : " + verison, logLevel, 0);
            Logger.Log("Configuration Read: GRIDORDER : " + gridOrder, logLevel, 0);
            Logger.Log("Configuration Read: IDCOLUMN : " + idColumnValue, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic1 : " + cbStatistic1, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic2 : " + cbStatistic2, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic3 : " + cbStatistic3, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic4 : " + cbStatistic4, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic5 : " + cbStatistic5, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic6 : " + cbStatistic6, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic7 : " + cbStatistic7, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic8 : " + cbStatistic8, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic9 : " + cbStatistic9, logLevel, 0);
            Logger.Log("Configuration Read: cbStatistic10 : " + cbStatistic10, logLevel, 0);
            Logger.Log("Configuration Read: custom1 : " + customDataField1, logLevel, 0);
            Logger.Log("Configuration Read: custom2 : " + customDataField2, logLevel, 0);
            Logger.Log("Configuration Read: color maint : " + colorMaint, logLevel, 0);
            Logger.Log("Configuration Read: color weekly : " + colorWeekly, logLevel, 0);
            Logger.Log("Configuration Read: color monthly : " + colorMonthly, logLevel, 0);
            Logger.Log("Configuration Read: color yearly : " + colorYearly, logLevel, 0);
            Logger.Log("Configuration Read: color display data : " + colorDisplayData, 0, 0);
            Logger.Log("Configuration Read: color bike : " + colorBike, logLevel, 0);
            Logger.Log("Configuration Read: color route : " + colorRoute, logLevel, 0);

            Logger.Log("Configuration Read: text maint : " + textMaint, logLevel, 0);
            Logger.Log("Configuration Read: text weekly : " + textWeekly, logLevel, 0);
            Logger.Log("Configuration Read: text monthly : " + textMonthly, logLevel, 0);
            Logger.Log("Configuration Read: text yearly : " + textYearly, logLevel, 0);
            Logger.Log("Configuration Read: text display : " + textDisplay, logLevel, 0);
            Logger.Log("Configuration Read: text bike : " + textBike, logLevel, 0);
            Logger.Log("Configuration Read: text route : " + textRoute, logLevel, 0);

            Logger.Log("Configuration Read: lastLogYearFilterSelected : " + lastLogYearFilterSelected, logLevel, 0);
            Logger.Log("Configuration Read: lastLogYearSelected : " + lastLogYearSelected, logLevel, 0);
            Logger.Log("Configuration Read: lastBikeSelected : " + lastBikeSelected, logLevel, 0);

            Logger.Log("Configuration Read: chartLogYearSelected : " + chartLogYearSelected, logLevel, 0);
            Logger.Log("Configuration Read: chartRouteSelected : " + chartRouteSelected, logLevel, 0);
            Logger.Log("Configuration Read: chartTypeSelected : " + chartTypeSelected, logLevel, 0);
            Logger.Log("Configuration Read: chartTimeTypeSelected : " + chartTimeTypeSelected, logLevel, 0);

            Logger.Log("Configuration Read: lastMonthlyLogYearSelected : " + lastMonthlyLogYearSelected, logLevel, 0);
            Logger.Log("Configuration Read: lastLogYearSelectedDataEntry : " + lastLogYearSelectedDataEntry, logLevel, 0);

            Logger.Log("Configuration Read: license : " + license, logLevel, 0);
            Logger.Log("Configuration Read: custom1 : " + customDataField1, logLevel, 0);
            Logger.Log("Configuration Read: custom2 : " + customDataField2, logLevel, 0);


            returnStatus = true;           

            return returnStatus;
        }

        public static void SetLogsRead(int logReadValue)
        {
            logsRead = logReadValue;
        }

        public static int GetLogsRead()
        {
            return logsRead;
        }

        public static void WriteConfigFile()
        {
            //This will give us the full name path of the executable file:
            //i.e. C:\Program Files\MyApplication\MyApplication.exe
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            string path = strWorkPath + "\\settings";
            string pathFile = strWorkPath + "\\settings\\CyclingLogConfig.xml";
            //MainForm mainForm = new MainForm("");
            int logSetting = MainForm.GetLogLevel();

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Logger.Log("Write Config file: The directory was created successfully at {0}." + Directory.GetCreationTime(path), 1, logSetting);
                }

                // Determine whether the file exists.
                if (!File.Exists(pathFile))
                {
                    //Write in the nodes:
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlElement rootNode = xmlDoc.CreateElement("Config");
                    xmlDoc.AppendChild(rootNode);

                    XmlNode logDaysNode = xmlDoc.CreateElement("DAYSTOKEEPLOGS");
                    logDaysNode.InnerText = "90";
                    rootNode.AppendChild(logDaysNode);

                    XmlNode versionNode = xmlDoc.CreateElement("VERSION");
                    versionNode.InnerText = MainForm.GetLogVersion();
                    rootNode.AppendChild(versionNode);

                    XmlNode idNode = xmlDoc.CreateElement("IDCOLUMN");
                    idNode.InnerText = "0";
                    rootNode.AppendChild(idNode);

                    XmlNode firstDayNode = xmlDoc.CreateElement("FIRSTDAY");
                    firstDayNode.InnerText = "Monday";
                    rootNode.AppendChild(firstDayNode);

                    XmlNode licesneNode = xmlDoc.CreateElement("LICENSE");
                    licesneNode.InnerText = "False";
                    rootNode.AppendChild(licesneNode);

                    XmlNode customField1Node = xmlDoc.CreateElement("CUSTOMFIELD1");
                    customField1Node.InnerText = "";
                    rootNode.AppendChild(customField1Node);

                    XmlNode customField2Node = xmlDoc.CreateElement("CUSTOMFIELD2");
                    customField2Node.InnerText = "";
                    rootNode.AppendChild(customField2Node);

                    XmlNode colorMaintNode = xmlDoc.CreateElement("COLORMAINT");
                    colorMaintNode.InnerText = "Beige";
                    rootNode.AppendChild(colorMaintNode);

                    XmlNode colorWeeklyNode = xmlDoc.CreateElement("COLORWEEKLY");
                    colorWeeklyNode.InnerText = "Beige";
                    rootNode.AppendChild(colorWeeklyNode);

                    XmlNode colorMonthlyNode = xmlDoc.CreateElement("COLORMONTHLY");
                    colorMonthlyNode.InnerText = "Beige";
                    rootNode.AppendChild(colorMonthlyNode);

                    XmlNode colorYearlyNode = xmlDoc.CreateElement("COLORYEARLY");
                    colorYearlyNode.InnerText = "Beige";
                    rootNode.AppendChild(colorYearlyNode);

                    XmlNode displayDataColorNode = xmlDoc.CreateElement("COLORDISPLAYDATA");
                    displayDataColorNode.InnerText = "Beige";
                    rootNode.AppendChild(displayDataColorNode);

                    XmlNode colorBikeNode = xmlDoc.CreateElement("COLORBIKE");
                    colorBikeNode.InnerText = "Beige";
                    rootNode.AppendChild(colorBikeNode);

                    XmlNode colorRouteNode = xmlDoc.CreateElement("COLORROUTE");
                    colorRouteNode.InnerText = "Beige";
                    rootNode.AppendChild(colorRouteNode);

                    //----------------
                    XmlNode colorMaintTextNode = xmlDoc.CreateElement("COLORMAINTTEXT");
                    colorMaintTextNode.InnerText = "True";
                    rootNode.AppendChild(colorMaintTextNode);

                    XmlNode colorWeeklyTextNode = xmlDoc.CreateElement("COLORWEEKLYTEXT");
                    colorWeeklyTextNode.InnerText = "True";
                    rootNode.AppendChild(colorWeeklyTextNode);

                    XmlNode colorMonthlyTextNode = xmlDoc.CreateElement("COLORMONTHLYTEXT");
                    colorMonthlyTextNode.InnerText = "True";
                    rootNode.AppendChild(colorMonthlyTextNode);

                    XmlNode colorYearlyTextNode = xmlDoc.CreateElement("COLORYEARLYTEXT");
                    colorYearlyTextNode.InnerText = "True";
                    rootNode.AppendChild(colorYearlyTextNode);

                    XmlNode colorDisplayTextNode = xmlDoc.CreateElement("COLORDISPLAYTEXT");
                    colorDisplayTextNode.InnerText = "True";
                    rootNode.AppendChild(colorDisplayTextNode);

                    XmlNode colorBikeTextNode = xmlDoc.CreateElement("COLORBIKETEXT");
                    colorBikeTextNode.InnerText = "True";
                    rootNode.AppendChild(colorBikeTextNode);

                    XmlNode colorRouteTextNode = xmlDoc.CreateElement("COLORROUTETEXT");
                    colorRouteTextNode.InnerText = "True";
                    rootNode.AppendChild(colorRouteTextNode);
                    //**********************************************************************
                    XmlNode checkedItemNode0 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME0");
                    checkedItemNode0.InnerText = "Week Number";
                    rootNode.AppendChild(checkedItemNode0);

                    XmlNode checkedItemNode1 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME1");
                    checkedItemNode1.InnerText = "Moving Time";
                    rootNode.AppendChild(checkedItemNode1);

                    XmlNode checkedItemNode2 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME2");
                    checkedItemNode2.InnerText = "Ride Distance";
                    rootNode.AppendChild(checkedItemNode2);

                    XmlNode checkedItemNode3 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME3");
                    checkedItemNode3.InnerText = "Avg Speed";
                    rootNode.AppendChild(checkedItemNode3);

                    XmlNode checkedItemNode4 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME4");
                    checkedItemNode4.InnerText = "Bike";
                    rootNode.AppendChild(checkedItemNode4);

                    XmlNode checkedItemNode5= xmlDoc.CreateElement("CHECKEDLISTBOXNAME5");
                    checkedItemNode5.InnerText = "Route";
                    rootNode.AppendChild(checkedItemNode5);

                    XmlNode checkedItemNode6 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME6");
                    checkedItemNode6.InnerText = "Total Ascent";
                    rootNode.AppendChild(checkedItemNode6);

                    XmlNode checkedItemNode7 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME7");
                    checkedItemNode7.InnerText = "Avg Cadence";
                    rootNode.AppendChild(checkedItemNode7);

                    XmlNode checkedItemNode8 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME8");
                    checkedItemNode8.InnerText = "Avg Heart Rate";
                    rootNode.AppendChild(checkedItemNode8);

                    XmlNode checkedItemNode9 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME9");
                    checkedItemNode9.InnerText = "Comments";
                    rootNode.AppendChild(checkedItemNode9);

                    XmlNode checkedItemNode10 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME10");
                    checkedItemNode10.InnerText = "Calories";
                    rootNode.AppendChild(checkedItemNode10);

                    XmlNode checkedItemNode11 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME11");
                    checkedItemNode11.InnerText = "Ride Type";
                    rootNode.AppendChild(checkedItemNode11);

                    XmlNode checkedItemNode12 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME12");
                    checkedItemNode12.InnerText = "Effort";
                    rootNode.AppendChild(checkedItemNode12);

                    XmlNode checkedItemNode13 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME13");
                    checkedItemNode13.InnerText = "Comfort";
                    rootNode.AppendChild(checkedItemNode13);

                    XmlNode checkedItemNode14 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME14");
                    checkedItemNode14.InnerText = "Wind";
                    rootNode.AppendChild(checkedItemNode14);

                    XmlNode checkedItemNode15 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME15");
                    checkedItemNode15.InnerText = "Temperature";
                    rootNode.AppendChild(checkedItemNode15);

                    XmlNode checkedItemNode16 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME16");
                    checkedItemNode16.InnerText = "Wind Chill";
                    rootNode.AppendChild(checkedItemNode16);

                    XmlNode checkedItemNode17 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME17");
                    checkedItemNode17.InnerText = "Location";
                    rootNode.AppendChild(checkedItemNode17);

                    XmlNode checkedItemNode18 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME18");
                    checkedItemNode18.InnerText = "Total Descent";
                    rootNode.AppendChild(checkedItemNode18);

                    XmlNode checkedItemNode19 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME19");
                    checkedItemNode19.InnerText = "Max Cadence";
                    rootNode.AppendChild(checkedItemNode19);

                    XmlNode checkedItemNode20 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME20");
                    checkedItemNode20.InnerText = "Max Heart Rate";
                    rootNode.AppendChild(checkedItemNode20);

                    XmlNode checkedItemNode21 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME21");
                    checkedItemNode21.InnerText = "Max Speed";
                    rootNode.AppendChild(checkedItemNode21);

                    XmlNode checkedItemNode22 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME22");
                    checkedItemNode22.InnerText = "Avg Power";
                    rootNode.AppendChild(checkedItemNode22);

                    XmlNode checkedItemNode23 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME23");
                    checkedItemNode23.InnerText = "Max Power";
                    rootNode.AppendChild(checkedItemNode23);

                    XmlNode checkedItemNode24 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME24");
                    checkedItemNode24.InnerText = "Custom1";
                    rootNode.AppendChild(checkedItemNode24);

                    XmlNode checkedItemNode25 = xmlDoc.CreateElement("CHECKEDLISTBOXNAME25");
                    checkedItemNode25.InnerText = "Custom2";
                    rootNode.AppendChild(checkedItemNode25);
                    //**********************************************************************
                    XmlNode checkedItemNodeCHECK0 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK0");
                    checkedItemNodeCHECK0.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK0);

                    XmlNode checkedItemNodeCHECK1 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK1");
                    checkedItemNodeCHECK1.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK1);

                    XmlNode checkedItemNodeCHECK2 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK2");
                    checkedItemNodeCHECK2.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK2);

                    XmlNode checkedItemNodeCHECK3 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK3");
                    checkedItemNodeCHECK3.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK3);

                    XmlNode checkedItemNodeCHECK4 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK4");
                    checkedItemNodeCHECK4.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK4);

                    XmlNode checkedItemNodeCHECK5 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK5");
                    checkedItemNodeCHECK5.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK5);

                    XmlNode checkedItemNodeCHECK6 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK6");
                    checkedItemNodeCHECK6.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK6);

                    XmlNode checkedItemNodeCHECK7 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK7");
                    checkedItemNodeCHECK7.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK7);

                    XmlNode checkedItemNodeCHECK8 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK8");
                    checkedItemNodeCHECK8.InnerText = "True";
                    rootNode.AppendChild(checkedItemNodeCHECK8);

                    XmlNode checkedItemNodeCHECK9 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK9");
                    checkedItemNodeCHECK9.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK9);

                    XmlNode checkedItemNodeCHECK10 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK10");
                    checkedItemNodeCHECK10.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK10);

                    XmlNode checkedItemNodeCHECK11 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK11");
                    checkedItemNodeCHECK11.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK11);

                    XmlNode checkedItemNodeCHECK12 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK12");
                    checkedItemNodeCHECK12.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK12);

                    XmlNode checkedItemNodeCHECK13 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK13");
                    checkedItemNodeCHECK13.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK13);

                    XmlNode checkedItemNodeCHECK14 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK14");
                    checkedItemNodeCHECK14.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK14);

                    XmlNode checkedItemNodeCHECK15 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK15");
                    checkedItemNodeCHECK15.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK15);

                    XmlNode checkedItemNodeCHECK16 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK16");
                    checkedItemNodeCHECK16.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK16);

                    XmlNode checkedItemNodeCHECK17 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK17");
                    checkedItemNodeCHECK17.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK17);

                    XmlNode checkedItemNodeCHECK18 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK18");
                    checkedItemNodeCHECK18.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK18);

                    XmlNode checkedItemNodeCHECK19 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK19");
                    checkedItemNodeCHECK19.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK19);

                    XmlNode checkedItemNodeCHECK20 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK20");
                    checkedItemNodeCHECK20.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK20);

                    XmlNode checkedItemNodeCHECK21 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK21");
                    checkedItemNodeCHECK21.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK21);

                    XmlNode checkedItemNodeCHECK22 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK22");
                    checkedItemNodeCHECK22.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK22);

                    XmlNode checkedItemNodeCHECK23 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK23");
                    checkedItemNodeCHECK23.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK23);

                    XmlNode checkedItemNodeCHECK24 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK24");
                    checkedItemNodeCHECK24.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK24);

                    XmlNode checkedItemNodeCHECK25 = xmlDoc.CreateElement("CHECKEDLISTBOXCHECK25");
                    checkedItemNodeCHECK25.InnerText = "False";
                    rootNode.AppendChild(checkedItemNodeCHECK25);
                    //**********************************************************************

                    XmlNode logLevelNode = xmlDoc.CreateElement("LOGLEVEL");
                    logLevelNode.InnerText = "0";
                    rootNode.AppendChild(logLevelNode);

                    XmlNode gridOrderNode = xmlDoc.CreateElement("GRIDORDER");
                    gridOrderNode.InnerText = "DESC";
                    rootNode.AppendChild(gridOrderNode);

                    XmlNode cbStatistic1Node = xmlDoc.CreateElement("cbStatistic1");
                    cbStatistic1Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic1Node);

                    XmlNode cbStatistic2Node = xmlDoc.CreateElement("cbStatistic2");
                    cbStatistic2Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic2Node);

                    XmlNode cbStatistic3Node = xmlDoc.CreateElement("cbStatistic3");
                    cbStatistic3Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic3Node);

                    XmlNode cbStatistic4Node = xmlDoc.CreateElement("cbStatistic4");
                    cbStatistic4Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic4Node);

                    XmlNode cbStatistic5Node = xmlDoc.CreateElement("cbStatistic5");
                    cbStatistic5Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic5Node);

                    XmlNode cbStatistic6Node = xmlDoc.CreateElement("cbStatistic6");
                    cbStatistic6Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic6Node);

                    XmlNode cbStatistic7Node = xmlDoc.CreateElement("cbStatistic7");
                    cbStatistic7Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic7Node);

                    XmlNode cbStatistic8Node = xmlDoc.CreateElement("cbStatistic8");
                    cbStatistic8Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic8Node);

                    XmlNode cbStatistic9Node = xmlDoc.CreateElement("cbStatistic9");
                    cbStatistic9Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic9Node);

                    XmlNode cbStatistic10Node = xmlDoc.CreateElement("cbStatistic10");
                    cbStatistic10Node.InnerText = "0";
                    rootNode.AppendChild(cbStatistic10Node);

                    XmlNode lastBikeSelectedNode = xmlDoc.CreateElement("LastBikeSelected");
                    lastBikeSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastBikeSelectedNode);

                    XmlNode lastLogSelectedNode = xmlDoc.CreateElement("LastLogSelected");
                    lastLogSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastLogSelectedNode);

                    XmlNode lastLogFilterSelectedNode = xmlDoc.CreateElement("LastLogFilterSelected"); //Display form logyear
                    lastLogFilterSelectedNode.InnerText = "0";
                    rootNode.AppendChild(lastLogFilterSelectedNode);

                    XmlNode lastLogYearChartSelectedNode = xmlDoc.CreateElement("ChartLogYear");
                    lastLogYearChartSelectedNode.InnerText = "0";
                    rootNode.AppendChild(lastLogYearChartSelectedNode);

                    XmlNode lastRouteChartSelectedNode = xmlDoc.CreateElement("ChartRoute");
                    lastRouteChartSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastRouteChartSelectedNode);

                    XmlNode lastTypeChartSelectedNode = xmlDoc.CreateElement("ChartType");
                    lastTypeChartSelectedNode.InnerText = "0";
                    rootNode.AppendChild(lastTypeChartSelectedNode);

                    XmlNode lastTypeTimeChartSelectedNode = xmlDoc.CreateElement("ChartTimeType");
                    lastTypeTimeChartSelectedNode.InnerText = "0";
                    rootNode.AppendChild(lastTypeTimeChartSelectedNode);

                    XmlNode lastMonthlyLogYearSelectedNode = xmlDoc.CreateElement("LastMonthlyLogSelected");//for cbStatMonthlyLogYear
                    lastMonthlyLogYearSelectedNode.InnerText = "0";
                    rootNode.AppendChild(lastMonthlyLogYearSelectedNode);

                    XmlNode lastLogYearSelectedDataEntryNode = xmlDoc.CreateElement("LastLogSelectedDataEntry");
                    lastLogYearSelectedDataEntryNode.InnerText = "-0";
                    rootNode.AppendChild(lastLogYearSelectedDataEntryNode);

                    MainForm.SetLicenseAgreement("False");

                    xmlDoc.Save(pathFile);
                } else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(pathFile);

                    string cbStatistic1 = MainForm.GetcbStatistic1();
                    string cbStatistic2 = MainForm.GetcbStatistic2();
                    string cbStatistic3 = MainForm.GetcbStatistic3();
                    string cbStatistic4 = MainForm.GetcbStatistic4();
                    string cbStatistic5 = MainForm.GetcbStatistic5();
                    string cbStatistic6 = MainForm.GetcbStatistic6();
                    string cbStatistic7 = MainForm.GetcbStatistic7();
                    string cbStatistic8 = MainForm.GetcbStatistic8();
                    string cbStatistic9 = MainForm.GetcbStatistic9();
                    string cbStatistic10 = MainForm.GetcbStatistic10();

                    string idColumnValue = MainForm.GetIDColumnValue();
                    string version = MainForm.GetLogVersion();
                    string gridOrder = MainForm.GetGridOrder();
                    string firstDay = MainForm.GetFirstDayOfWeek();
                    string license = MainForm.GetLicenseAgreement();
                    string customField1 = MainForm.GetCustomField1();
                    string customField2 = MainForm.GetCustomField2();
                    string colorMaint = MainForm.GetMaintColor();
                    string colorWeekly = MainForm.GetWeeklyColor();
                    string colorMonthly = MainForm.GetMonthlyColor();
                    string colorYearly = MainForm.GetYearlyColor();
                    string colorDisplayData = MainForm.GetDisplayDataColor();
                    string colorBike = MainForm.GetBikeColor();
                    string colorRoute = MainForm.GetRouteColor();
                    string textMaint = MainForm.GetTextMaint();
                    string textWeekly = MainForm.GetTextWeekly();
                    string textMonthly = MainForm.GetTextMonthly();
                    string textYearly = MainForm.GetTextYearly();
                    string textDisplay = MainForm.GetTextDisplay();
                    string textBike = MainForm.GetTextBike();
                    string textRoute = MainForm.GetTextRoute();

                    Dictionary<string, string> fieldDictionary = MainForm.GetFieldsDictionary();

                    int lastLogSelected = MainForm.GetLastLogSelected();
                    int lastBikeSelected = MainForm.GetLastBikeSelected();
                    int lastLogFilterSelected = MainForm.GetLastLogFilterSelected();

                    int lastLogYearChartSelected = MainForm.GetLastLogYearChartSelected();
                    int lastRouteChartSelected = MainForm.GetLastRouteChartSelected();
                    int lastTypeChartSelected = MainForm.GetLastTypeChartSelected();
                    int lastTypeTimeChartSelected = MainForm.GetLastTypeTimeChartSelected();

                    int lastMonthlyLogSelected = MainForm.GetLastMonthlyLogSelected();
                    int lastLogSelectedDataEntry = MainForm.GetLastLogSelectedDataEntry();

                    xmlDoc.SelectSingleNode("/Config/VERSION").InnerText = version;
                    xmlDoc.SelectSingleNode("/Config/IDCOLUMN").InnerText = idColumnValue;
                    xmlDoc.SelectSingleNode("/Config/FIRSTDAY").InnerText = firstDay;
                    xmlDoc.SelectSingleNode("/Config/LICENSE").InnerText = license;
                    xmlDoc.SelectSingleNode("/Config/GRIDORDER").InnerText = gridOrder;
                    xmlDoc.SelectSingleNode("/Config/CUSTOMFIELD1").InnerText = customField1;
                    xmlDoc.SelectSingleNode("/Config/CUSTOMFIELD2").InnerText = customField2;
                    xmlDoc.SelectSingleNode("/Config/COLORMAINT").InnerText = colorMaint;
                    xmlDoc.SelectSingleNode("/Config/COLORWEEKLY").InnerText = colorWeekly;
                    xmlDoc.SelectSingleNode("/Config/COLORMONTHLY").InnerText = colorMonthly;
                    xmlDoc.SelectSingleNode("/Config/COLORYEARLY").InnerText = colorYearly;
                    xmlDoc.SelectSingleNode("/Config/COLORDISPLAYDATA").InnerText = colorDisplayData;
                    xmlDoc.SelectSingleNode("/Config/COLORBIKE").InnerText = colorBike;
                    xmlDoc.SelectSingleNode("/Config/COLORROUTE").InnerText = colorRoute;

                    xmlDoc.SelectSingleNode("/Config/COLORMAINTTEXT").InnerText = textMaint;
                    xmlDoc.SelectSingleNode("/Config/COLORWEEKLYTEXT").InnerText = textWeekly;
                    xmlDoc.SelectSingleNode("/Config/COLORMONTHLYTEXT").InnerText = textMonthly;
                    xmlDoc.SelectSingleNode("/Config/COLORYEARLYTEXT").InnerText = textYearly;
                    xmlDoc.SelectSingleNode("/Config/COLORDISPLAYTEXT").InnerText = textDisplay;
                    xmlDoc.SelectSingleNode("/Config/COLORBIKETEXT").InnerText = textBike;
                    xmlDoc.SelectSingleNode("/Config/COLORROUTETEXT").InnerText = textRoute;

                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK0").InnerText = fieldDictionary.Values.ElementAt(0);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK1").InnerText = fieldDictionary.Values.ElementAt(1);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK2").InnerText = fieldDictionary.Values.ElementAt(2);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK3").InnerText = fieldDictionary.Values.ElementAt(3);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK4").InnerText = fieldDictionary.Values.ElementAt(4);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK5").InnerText = fieldDictionary.Values.ElementAt(5);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK6").InnerText = fieldDictionary.Values.ElementAt(6);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK7").InnerText = fieldDictionary.Values.ElementAt(7);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK8").InnerText = fieldDictionary.Values.ElementAt(8);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK9").InnerText = fieldDictionary.Values.ElementAt(9);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK10").InnerText = fieldDictionary.Values.ElementAt(10);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK11").InnerText = fieldDictionary.Values.ElementAt(11);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK12").InnerText = fieldDictionary.Values.ElementAt(12);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK13").InnerText = fieldDictionary.Values.ElementAt(13);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK14").InnerText = fieldDictionary.Values.ElementAt(14);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK15").InnerText = fieldDictionary.Values.ElementAt(15);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK16").InnerText = fieldDictionary.Values.ElementAt(16);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK17").InnerText = fieldDictionary.Values.ElementAt(17);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK18").InnerText = fieldDictionary.Values.ElementAt(18);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK19").InnerText = fieldDictionary.Values.ElementAt(19);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK20").InnerText = fieldDictionary.Values.ElementAt(20);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK21").InnerText = fieldDictionary.Values.ElementAt(21);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK22").InnerText = fieldDictionary.Values.ElementAt(22);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK23").InnerText = fieldDictionary.Values.ElementAt(23);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK24").InnerText = fieldDictionary.Values.ElementAt(24);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXCHECK25").InnerText = fieldDictionary.Values.ElementAt(25);

                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME0").InnerText = fieldDictionary.Keys.ElementAt(0);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME1").InnerText = fieldDictionary.Keys.ElementAt(1);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME2").InnerText = fieldDictionary.Keys.ElementAt(2);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME3").InnerText = fieldDictionary.Keys.ElementAt(3);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME4").InnerText = fieldDictionary.Keys.ElementAt(4);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME5").InnerText = fieldDictionary.Keys.ElementAt(5);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME6").InnerText = fieldDictionary.Keys.ElementAt(6);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME7").InnerText = fieldDictionary.Keys.ElementAt(7);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME8").InnerText = fieldDictionary.Keys.ElementAt(8);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME9").InnerText = fieldDictionary.Keys.ElementAt(9);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME10").InnerText = fieldDictionary.Keys.ElementAt(10);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME11").InnerText = fieldDictionary.Keys.ElementAt(11);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME12").InnerText = fieldDictionary.Keys.ElementAt(12);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME13").InnerText = fieldDictionary.Keys.ElementAt(13);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME14").InnerText = fieldDictionary.Keys.ElementAt(14);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME15").InnerText = fieldDictionary.Keys.ElementAt(15);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME16").InnerText = fieldDictionary.Keys.ElementAt(16);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME17").InnerText = fieldDictionary.Keys.ElementAt(17);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME18").InnerText = fieldDictionary.Keys.ElementAt(18);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME19").InnerText = fieldDictionary.Keys.ElementAt(19);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME20").InnerText = fieldDictionary.Keys.ElementAt(20);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME21").InnerText = fieldDictionary.Keys.ElementAt(21);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME22").InnerText = fieldDictionary.Keys.ElementAt(22);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME23").InnerText = fieldDictionary.Keys.ElementAt(23);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME24").InnerText = fieldDictionary.Keys.ElementAt(24);
                    xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOXNAME25").InnerText = fieldDictionary.Keys.ElementAt(25);

                    xmlDoc.SelectSingleNode("/Config/cbStatistic1").InnerText = cbStatistic1;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic2").InnerText = cbStatistic2;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic3").InnerText = cbStatistic3;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic4").InnerText = cbStatistic4;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic5").InnerText = cbStatistic5;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic6").InnerText = cbStatistic6;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic7").InnerText = cbStatistic7;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic8").InnerText = cbStatistic8;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic9").InnerText = cbStatistic9;
                    xmlDoc.SelectSingleNode("/Config/cbStatistic10").InnerText = cbStatistic10;

                    xmlDoc.SelectSingleNode("/Config/LastLogSelected").InnerText = lastLogSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/LastBikeSelected").InnerText = lastBikeSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/LastLogFilterSelected").InnerText = lastLogFilterSelected.ToString();

                    xmlDoc.SelectSingleNode("/Config/ChartLogYear").InnerText = lastLogYearChartSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/ChartRoute").InnerText = lastRouteChartSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/ChartType").InnerText = lastTypeChartSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/ChartTimeType").InnerText = lastTypeTimeChartSelected.ToString();

                    xmlDoc.SelectSingleNode("/Config/LastMonthlyLogSelected").InnerText = lastMonthlyLogSelected.ToString();
                    xmlDoc.SelectSingleNode("/Config/LastLogSelectedDataEntry").InnerText = lastLogSelectedDataEntry.ToString();

                    Logger.Log("Write Config Values: VERSION written:" + version, logSetting, 0);
                    Logger.Log("Write Config Values: GRIDORDER written:" + gridOrder, logSetting, 0);
                    Logger.Log("Write Config Values: IDCOLUMN written:" + idColumnValue, logSetting, 0);
                    Logger.Log("Write Config Values: FIRSTDAY written:" + firstDay, logSetting, 0);
                    Logger.Log("Write Config Values: LICENSE written:" + license, logSetting, 0);
                    Logger.Log("Write Config Values: CUSTOMFIELD1 written:" + customField1, logSetting, 0);
                    Logger.Log("Write Config Values: CUSTOMFIELD2 written:" + customField2, logSetting, 0);

                    Logger.Log("Write Config Values: COLORMAINT written:" + colorMaint, logSetting, 0);
                    Logger.Log("Write Config Values: COLORDISPLAYDATA written:" + colorDisplayData, logSetting, 0);
                    Logger.Log("Write Config Values: COLORBIKE written:" + colorBike, logSetting, 0);
                    Logger.Log("Write Config Values: COLORROUTE written:" + colorRoute, logSetting, 0);
                    Logger.Log("Write Config Values: COLORWEEKLY written:" + colorWeekly, logSetting, 0);
                    Logger.Log("Write Config Values: COLORMONTHLY written:" + colorMonthly, logSetting, 0);
                    Logger.Log("Write Config Values: COLORYEARLY written:" + colorYearly, logSetting, 0);

                    Logger.Log("Write Config Values: COLORMAINTTEXT written:" + textMaint, logSetting, 0);
                    Logger.Log("Write Config Values: COLORWEEKLYTEXT written:" + textWeekly, logSetting, 0);
                    Logger.Log("Write Config Values: COLORMONTHLYTEXT written:" + textMonthly, logSetting, 0);
                    Logger.Log("Write Config Values: COLORYEARLYTEXT written:" + textYearly, logSetting, 0);
                    Logger.Log("Write Config Values: COLORDISPLAYTEXT written:" + textDisplay, logSetting, 0);
                    Logger.Log("Write Config Values: COLORBIKETEXT written:" + textBike, logSetting, 0);
                    Logger.Log("Write Config Values: COLORROUTETEXT written:" + textRoute, logSetting, 0);

                    Logger.Log("Write Config Values: cbStatistic1 written:" + cbStatistic1, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic2 written:" + cbStatistic2, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic3 written:" + cbStatistic3, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic4 written:" + cbStatistic4, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic5 written:" + cbStatistic5, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic6 written:" + cbStatistic6, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic7 written:" + cbStatistic7, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic8 written:" + cbStatistic8, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic9 written:" + cbStatistic9, logSetting, 0);
                    Logger.Log("Write Config Values: cbStatistic10 written:" + cbStatistic10, logSetting, 0);
                    Logger.Log("Write Config Values: lastLogSelected written:" + lastLogSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastBikeSelected written:" + lastBikeSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastLogFilterSelected written:" + lastLogFilterSelected, logSetting, 0);

                    Logger.Log("Write Config Values: lastLogYearChartSelected written:" + lastLogYearChartSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastRouteChartSelected written:" + lastRouteChartSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastTypeChartSelected written:" + lastTypeChartSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastTypeTimeChartSelected written:" + lastTypeTimeChartSelected, logSetting, 0);

                    Logger.Log("Write Config Values: lastMonthlyLogSelected written:" + lastMonthlyLogSelected, logSetting, 0);
                    Logger.Log("Write Config Values: lastLogSelectedDataEntry written:" + lastLogSelectedDataEntry, logSetting, 0);

                    xmlDoc.Save(pathFile);
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[ERROR]: Exception while trying to write the Config File" + e.Message.ToString());

                return;
            }
        }
 
    }
}
