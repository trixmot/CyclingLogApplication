using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;

namespace CyclingLogApplication
{
    class ConfigurationFile
    {

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
            //List<string> configList = new List<string>();

            //=================================
            // Read from config file
            //=================================
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Config");
            string daysToKeepLogs = nodes.Item(0).SelectSingleNode("DAYSTOKEEPLOGS").InnerText;
            string verison = nodes.Item(0).SelectSingleNode("VERSION").InnerText;
            int logLevel = Convert.ToInt32(nodes.Item(0).SelectSingleNode("LOGLEVEL").InnerText);

            string cbStatistic1 = nodes.Item(0).SelectSingleNode("cbStatistic1").InnerText;
            string cbStatistic2 = nodes.Item(0).SelectSingleNode("cbStatistic2").InnerText;
            string cbStatistic3 = nodes.Item(0).SelectSingleNode("cbStatistic3").InnerText;
            string cbStatistic4 = nodes.Item(0).SelectSingleNode("cbStatistic4").InnerText;
            string cbStatistic5 = nodes.Item(0).SelectSingleNode("cbStatistic5").InnerText;
            string firstDayOfWeek = nodes.Item(0).SelectSingleNode("FIRSTDAY").InnerText;
            string license = nodes.Item(0).SelectSingleNode("LICENSE").InnerText;
            string customDataField1 = nodes.Item(0).SelectSingleNode("CUSTOMFIELD1").InnerText;
            string customDataField2 = nodes.Item(0).SelectSingleNode("CUSTOMFIELD2").InnerText;

            string checkListBoxItem0 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX0").InnerText;
            string checkListBoxItem1 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX1").InnerText;
            string checkListBoxItem2 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX2").InnerText;
            string checkListBoxItem3 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX3").InnerText;
            string checkListBoxItem4 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX4").InnerText;
            string checkListBoxItem5 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX5").InnerText;
            string checkListBoxItem6 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX6").InnerText;
            string checkListBoxItem7 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX7").InnerText;
            string checkListBoxItem8 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX8").InnerText;
            string checkListBoxItem9 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX9").InnerText;
            string checkListBoxItem10 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX10").InnerText;
            string checkListBoxItem11 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX11").InnerText;
            string checkListBoxItem12 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX12").InnerText;
            string checkListBoxItem13 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX13").InnerText;
            string checkListBoxItem14 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX14").InnerText;
            string checkListBoxItem15 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX15").InnerText;
            string checkListBoxItem16 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX16").InnerText;
            string checkListBoxItem17 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX17").InnerText;
            string checkListBoxItem18 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX18").InnerText;
            string checkListBoxItem19 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX19").InnerText;
            string checkListBoxItem20 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX20").InnerText;
            string checkListBoxItem21 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX21").InnerText;
            string checkListBoxItem22 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX22").InnerText;
            string checkListBoxItem23 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX23").InnerText;
            string checkListBoxItem24 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX24").InnerText;
            string checkListBoxItem25 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX25").InnerText;
            string checkListBoxItem26 = nodes.Item(0).SelectSingleNode("CHECKEDLISTBOX26").InnerText;

            string lastLogYearSelected = nodes.Item(0).SelectSingleNode("LastLogSelected").InnerText;
            string lastBikeSelected = nodes.Item(0).SelectSingleNode("LastBikeSelected").InnerText;
            string lastLogYearFilterSelected = nodes.Item(0).SelectSingleNode("LastLogFilterSelected").InnerText;

            string chartLogYearSelected = nodes.Item(0).SelectSingleNode("ChartLogYear").InnerText;
            string chartRouteSelected = nodes.Item(0).SelectSingleNode("ChartRoute").InnerText;
            string chartTypeSelected = nodes.Item(0).SelectSingleNode("ChartType").InnerText;
            string chartTimeTypeSelected = nodes.Item(0).SelectSingleNode("ChartTimeType").InnerText;

            string lastMonthlyLogYearSelected = nodes.Item(0).SelectSingleNode("LastMonthlyLogSelected").InnerText;
            string lastLogYearSelectedDataEntry = nodes.Item(0).SelectSingleNode("LastLogSelectedDataEntry").InnerText;

            //MainForm mainForm = new MainForm();
            MainForm.SetLogLevel(logLevel);
            MainForm.SetLicenseAgreement(license);
            MainForm.SetFirstDayOfWeek(firstDayOfWeek);
            MainForm.SetCustomField1(customDataField1);
            MainForm.SetCustomField2(customDataField2);
            MainForm.SetcbStatistic1(cbStatistic1);
            MainForm.SetcbStatistic2(cbStatistic2);
            MainForm.SetcbStatistic3(cbStatistic3);
            MainForm.SetcbStatistic4(cbStatistic4);
            MainForm.SetcbStatistic5(cbStatistic5);
            MainForm.SetLastLogFilterSelected(Convert.ToInt32(lastLogYearFilterSelected));
            MainForm.SetLastBikeSelected(Convert.ToInt32(lastBikeSelected));
            MainForm.SetLastLogSelected(Convert.ToInt32(lastLogYearSelected));

            MainForm.SetLastLogYearChartSelected(Convert.ToInt32(chartLogYearSelected));
            MainForm.SetLastRouteChartSelected(Convert.ToInt32(chartRouteSelected));
            MainForm.SetLastTypeChartSelected(Convert.ToInt32(chartTypeSelected));
            MainForm.SetLastTypeTimeChartSelected(Convert.ToInt32(chartTimeTypeSelected));

            MainForm.SetLastMonthlyLogSelected(Convert.ToInt32(lastMonthlyLogYearSelected));
            MainForm.SetLastLogSelectedDataEntry(Convert.ToInt32(lastLogYearSelectedDataEntry));

            MainForm.SetCheckedListBoxItem0(checkListBoxItem0);
            MainForm.SetCheckedListBoxItem1(checkListBoxItem1);
            MainForm.SetCheckedListBoxItem2(checkListBoxItem2);
            MainForm.SetCheckedListBoxItem3(checkListBoxItem3);
            MainForm.SetCheckedListBoxItem4(checkListBoxItem4);
            MainForm.SetCheckedListBoxItem5(checkListBoxItem5);
            MainForm.SetCheckedListBoxItem6(checkListBoxItem6);
            MainForm.SetCheckedListBoxItem7(checkListBoxItem7);
            MainForm.SetCheckedListBoxItem8(checkListBoxItem8);
            MainForm.SetCheckedListBoxItem9(checkListBoxItem9);
            MainForm.SetCheckedListBoxItem10(checkListBoxItem10);
            MainForm.SetCheckedListBoxItem11(checkListBoxItem11);
            MainForm.SetCheckedListBoxItem12(checkListBoxItem12);
            MainForm.SetCheckedListBoxItem13(checkListBoxItem13);
            MainForm.SetCheckedListBoxItem14(checkListBoxItem14);
            MainForm.SetCheckedListBoxItem15(checkListBoxItem15);
            MainForm.SetCheckedListBoxItem16(checkListBoxItem16);
            MainForm.SetCheckedListBoxItem17(checkListBoxItem17);
            MainForm.SetCheckedListBoxItem18(checkListBoxItem18);
            MainForm.SetCheckedListBoxItem19(checkListBoxItem19);
            MainForm.SetCheckedListBoxItem20(checkListBoxItem20);
            MainForm.SetCheckedListBoxItem21(checkListBoxItem21);
            MainForm.SetCheckedListBoxItem22(checkListBoxItem22);
            MainForm.SetCheckedListBoxItem23(checkListBoxItem23);
            MainForm.SetCheckedListBoxItem24(checkListBoxItem24);
            MainForm.SetCheckedListBoxItem25(checkListBoxItem25);
            MainForm.SetCheckedListBoxItem25(checkListBoxItem26);

            //NOTE: If the dateTime value is blank then a force update will be run and a new timestamp will be written at end of run:
            Logger.Log("Configuration Read: DAYSTOKEEPLOGS: " + daysToKeepLogs, 1, 0);
            Logger.Log("Configuration Read: LOGLEVEL : " + logLevel, 1, 0);
            Logger.Log("Configuration Read: VERSION : " + verison, 1, 0);
            Logger.Log("Configuration Read: cbStatistic1 : " + cbStatistic1, 1, 0);
            Logger.Log("Configuration Read: cbStatistic2 : " + cbStatistic2, 1, 0);
            Logger.Log("Configuration Read: cbStatistic3 : " + cbStatistic3, 1, 0);
            Logger.Log("Configuration Read: cbStatistic4 : " + cbStatistic4, 1, 0);
            Logger.Log("Configuration Read: cbStatistic5 : " + cbStatistic5, 1, 0);
            Logger.Log("Configuration Read: lastLogYearFilterSelected : " + lastLogYearFilterSelected, 1, 0);
            Logger.Log("Configuration Read: lastLogYearSelected : " + lastLogYearSelected, 1, 0);
            Logger.Log("Configuration Read: lastBikeSelected : " + lastBikeSelected, 1, 0);

            Logger.Log("Configuration Read: chartLogYearSelected : " + chartLogYearSelected, 1, 0);
            Logger.Log("Configuration Read: chartRouteSelected : " + chartRouteSelected, 1, 0);
            Logger.Log("Configuration Read: chartTypeSelected : " + chartTypeSelected, 1, 0);
            Logger.Log("Configuration Read: chartTimeTypeSelected : " + chartTimeTypeSelected, 1, 0);

            Logger.Log("Configuration Read: lastMonthlyLogYearSelected : " + lastMonthlyLogYearSelected, 1, 0);
            Logger.Log("Configuration Read: lastLogYearSelectedDataEntry : " + lastLogYearSelectedDataEntry, 1, 0);

            Logger.Log("Configuration Read: license : " + license, 1, 0);
            Logger.Log("Configuration Read: custom1 : " + customDataField1, 1, 0);
            Logger.Log("Configuration Read: custom2 : " + customDataField2, 1, 0);

            returnStatus = true;

            return returnStatus;
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
                    //XmlNode commentNode1 = xmlDoc.CreateComment("This file was created by the ExtraViewToRallyConnector application. Do not modify.");
                    //rootNode.AppendChild(commentNode1);
                    //XmlNode commentNode2 = xmlDoc.CreateComment("The Time stamp value is used to compare Last modified times of records for updating.");
                    //rootNode.AppendChild(commentNode2);
                    XmlNode logDaysNode = xmlDoc.CreateElement("DAYSTOKEEPLOGS");
                    logDaysNode.InnerText = "90";
                    rootNode.AppendChild(logDaysNode);

                    XmlNode versionNode = xmlDoc.CreateElement("VERSION");
                    versionNode.InnerText = MainForm.GetLogVersion();
                    rootNode.AppendChild(versionNode);

                    XmlNode firstDayNode = xmlDoc.CreateElement("FIRSTDAY");
                    firstDayNode.InnerText = MainForm.GetFirstDayOfWeek();
                    rootNode.AppendChild(firstDayNode);

                    XmlNode licesneNode = xmlDoc.CreateElement("LICENSE");
                    licesneNode.InnerText = MainForm.GetLicenseAgreement().ToString();
                    rootNode.AppendChild(licesneNode);

                    XmlNode customField1Node = xmlDoc.CreateElement("CUSTOMFIELD1");
                    customField1Node.InnerText = MainForm.GetCustomField1();
                    rootNode.AppendChild(customField1Node);

                    XmlNode customField2Node = xmlDoc.CreateElement("CUSTOMFIELD2");
                    customField2Node.InnerText = MainForm.GetCustomField2();
                    rootNode.AppendChild(customField2Node);

                    XmlNode checkedItemNode0 = xmlDoc.CreateElement("CHECKEDLISTBOX0");
                    checkedItemNode0.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode0);

                    XmlNode checkedItemNode1 = xmlDoc.CreateElement("CHECKEDLISTBOX1");
                    checkedItemNode1.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode1);

                    XmlNode checkedItemNode2 = xmlDoc.CreateElement("CHECKEDLISTBOX2");
                    checkedItemNode2.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode2);

                    XmlNode checkedItemNode3 = xmlDoc.CreateElement("CHECKEDLISTBOX3");
                    checkedItemNode3.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode3);

                    XmlNode checkedItemNode4 = xmlDoc.CreateElement("CHECKEDLISTBOX4");
                    checkedItemNode4.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode4);

                    XmlNode checkedItemNode5= xmlDoc.CreateElement("CHECKEDLISTBOX5");
                    checkedItemNode5.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode5);

                    XmlNode checkedItemNode6 = xmlDoc.CreateElement("CHECKEDLISTBOX6");
                    checkedItemNode6.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode6);

                    XmlNode checkedItemNode7 = xmlDoc.CreateElement("CHECKEDLISTBOX7");
                    checkedItemNode7.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode7);

                    XmlNode checkedItemNode8 = xmlDoc.CreateElement("CHECKEDLISTBOX8");
                    checkedItemNode8.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode8);

                    XmlNode checkedItemNode9 = xmlDoc.CreateElement("CHECKEDLISTBOX9");
                    checkedItemNode9.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode9);

                    XmlNode checkedItemNode10 = xmlDoc.CreateElement("CHECKEDLISTBOX10");
                    checkedItemNode10.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode10);

                    XmlNode checkedItemNode11 = xmlDoc.CreateElement("CHECKEDLISTBOX11");
                    checkedItemNode11.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode11);

                    XmlNode checkedItemNode12 = xmlDoc.CreateElement("CHECKEDLISTBOX12");
                    checkedItemNode12.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode12);

                    XmlNode checkedItemNode13 = xmlDoc.CreateElement("CHECKEDLISTBOX13");
                    checkedItemNode13.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode13);

                    XmlNode checkedItemNode14 = xmlDoc.CreateElement("CHECKEDLISTBOX14");
                    checkedItemNode14.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode14);

                    XmlNode checkedItemNode15 = xmlDoc.CreateElement("CHECKEDLISTBOX15");
                    checkedItemNode15.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode15);

                    XmlNode checkedItemNode16 = xmlDoc.CreateElement("CHECKEDLISTBOX16");
                    checkedItemNode16.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode16);

                    XmlNode checkedItemNode17 = xmlDoc.CreateElement("CHECKEDLISTBOX17");
                    checkedItemNode17.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode17);

                    XmlNode checkedItemNode18 = xmlDoc.CreateElement("CHECKEDLISTBOX18");
                    checkedItemNode18.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode18);

                    XmlNode checkedItemNode19 = xmlDoc.CreateElement("CHECKEDLISTBOX19");
                    checkedItemNode19.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode19);

                    XmlNode checkedItemNode20 = xmlDoc.CreateElement("CHECKEDLISTBOX20");
                    checkedItemNode20.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode20);

                    XmlNode checkedItemNode21 = xmlDoc.CreateElement("CHECKEDLISTBOX21");
                    checkedItemNode21.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode21);

                    XmlNode checkedItemNode22 = xmlDoc.CreateElement("CHECKEDLISTBOX22");
                    checkedItemNode22.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode22);

                    XmlNode checkedItemNode23 = xmlDoc.CreateElement("CHECKEDLISTBOX23");
                    checkedItemNode23.InnerText = "1";
                    rootNode.AppendChild(checkedItemNode23);

                    XmlNode checkedItemNode24 = xmlDoc.CreateElement("CHECKEDLISTBOX24");
                    checkedItemNode24.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode24);

                    XmlNode checkedItemNode25 = xmlDoc.CreateElement("CHECKEDLISTBOX25");
                    checkedItemNode25.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode25);

                    XmlNode checkedItemNode26 = xmlDoc.CreateElement("CHECKEDLISTBOX26");
                    checkedItemNode26.InnerText = "0";
                    rootNode.AppendChild(checkedItemNode26);

                    XmlNode logLevelNode = xmlDoc.CreateElement("LOGLEVEL");
                    logLevelNode.InnerText = "0";
                    rootNode.AppendChild(logLevelNode);

                    XmlNode cbStatistic1Node = xmlDoc.CreateElement("cbStatistic1");
                    cbStatistic1Node.InnerText = "-1";
                    rootNode.AppendChild(cbStatistic1Node);

                    XmlNode cbStatistic2Node = xmlDoc.CreateElement("cbStatistic2");
                    cbStatistic2Node.InnerText = "-1";
                    rootNode.AppendChild(cbStatistic2Node);

                    XmlNode cbStatistic3Node = xmlDoc.CreateElement("cbStatistic3");
                    cbStatistic3Node.InnerText = "-1";
                    rootNode.AppendChild(cbStatistic3Node);

                    XmlNode cbStatistic4Node = xmlDoc.CreateElement("cbStatistic4");
                    cbStatistic4Node.InnerText = "-1";
                    rootNode.AppendChild(cbStatistic4Node);

                    XmlNode cbStatistic5Node = xmlDoc.CreateElement("cbStatistic5");
                    cbStatistic5Node.InnerText = "-1";
                    rootNode.AppendChild(cbStatistic5Node);

                    XmlNode lastBikeSelectedNode = xmlDoc.CreateElement("LastBikeSelected");
                    lastBikeSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastBikeSelectedNode);

                    XmlNode lastLogSelectedNode = xmlDoc.CreateElement("LastLogSelected");
                    lastLogSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastLogSelectedNode);

                    XmlNode lastLogFilterSelectedNode = xmlDoc.CreateElement("LastLogFilterSelected");
                    lastLogFilterSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastLogFilterSelectedNode);

                    XmlNode lastLogYearChartSelectedNode = xmlDoc.CreateElement("ChartLogYear");
                    lastLogYearChartSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastLogYearChartSelectedNode);

                    XmlNode lastRouteChartSelectedNode = xmlDoc.CreateElement("ChartRoute");
                    lastRouteChartSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastRouteChartSelectedNode);

                    XmlNode lastTypeChartSelectedNode = xmlDoc.CreateElement("ChartType");
                    lastTypeChartSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastTypeChartSelectedNode);

                    XmlNode lastTypeTimeChartSelectedNode = xmlDoc.CreateElement("ChartTimeType");
                    lastTypeTimeChartSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastTypeTimeChartSelectedNode);

                    XmlNode lastMonthlyLogYearSelectedNode = xmlDoc.CreateElement("LastMonthlyLogSelected");
                    lastMonthlyLogYearSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(lastMonthlyLogYearSelectedNode);

                    XmlNode lastLogYearSelectedDataEntryNode = xmlDoc.CreateElement("LastLogSelectedDataEntry");
                    lastLogYearSelectedDataEntryNode.InnerText = "-1";
                    rootNode.AppendChild(lastLogYearSelectedDataEntryNode);

                    xmlDoc.Save(pathFile);
                }
            }
            catch (Exception e)
            {
                Logger.LogError("[ERROR]: Exception while trying to write the Config File" + e.Message.ToString());

                return;
            }
            finally
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(pathFile);

                string cbStatistic1 = MainForm.GetcbStatistic1();
                string cbStatistic2 = MainForm.GetcbStatistic2();
                string cbStatistic3 = MainForm.GetcbStatistic3();
                string cbStatistic4 = MainForm.GetcbStatistic4();
                string cbStatistic5 = MainForm.GetcbStatistic5();

                string firstDay = MainForm.GetFirstDayOfWeek();
                string license = MainForm.GetLicenseAgreement();
                string customField1 = MainForm.GetCustomField1();
                string customField2 = MainForm.GetCustomField2();

                string checkedItem0 = MainForm.GetCheckedListBoxItem0();
                string checkedItem1 = MainForm.GetCheckedListBoxItem1();
                string checkedItem2 = MainForm.GetCheckedListBoxItem2();
                string checkedItem3 = MainForm.GetCheckedListBoxItem3();
                string checkedItem4 = MainForm.GetCheckedListBoxItem4();
                string checkedItem5 = MainForm.GetCheckedListBoxItem5();
                string checkedItem6 = MainForm.GetCheckedListBoxItem6();
                string checkedItem7 = MainForm.GetCheckedListBoxItem7();
                string checkedItem8 = MainForm.GetCheckedListBoxItem8();
                string checkedItem9 = MainForm.GetCheckedListBoxItem9();
                string checkedItem10 = MainForm.GetCheckedListBoxItem10();
                string checkedItem11 = MainForm.GetCheckedListBoxItem11();
                string checkedItem12 = MainForm.GetCheckedListBoxItem12();
                string checkedItem13 = MainForm.GetCheckedListBoxItem13();
                string checkedItem14 = MainForm.GetCheckedListBoxItem14();
                string checkedItem15 = MainForm.GetCheckedListBoxItem15();
                string checkedItem16 = MainForm.GetCheckedListBoxItem16();
                string checkedItem17 = MainForm.GetCheckedListBoxItem17();
                string checkedItem18 = MainForm.GetCheckedListBoxItem18();
                string checkedItem19 = MainForm.GetCheckedListBoxItem19();
                string checkedItem20 = MainForm.GetCheckedListBoxItem20();
                string checkedItem21 = MainForm.GetCheckedListBoxItem21();
                string checkedItem22 = MainForm.GetCheckedListBoxItem22();
                string checkedItem23 = MainForm.GetCheckedListBoxItem23();
                string checkedItem24 = MainForm.GetCheckedListBoxItem24();
                string checkedItem25 = MainForm.GetCheckedListBoxItem25();
                string checkedItem26 = MainForm.GetCheckedListBoxItem26();

                int lastLogSelected = MainForm.GetLastLogSelected();
                int lastBikeSelected = MainForm.GetLastBikeSelected();
                int lastLogFilterSelected = MainForm.GetLastLogFilterSelected();

                int lastLogYearChartSelected = MainForm.GetLastLogYearChartSelected();
                int lastRouteChartSelected = MainForm.GetLastRouteChartSelected();
                int lastTypeChartSelected = MainForm.GetLastTypeChartSelected();
                int lastTypeTimeChartSelected = MainForm.GetLastTypeTimeChartSelected();

                int lastMonthlyLogSelected = MainForm.GetLastMonthlyLogSelected();
                int lastLogSelectedDataEntry = MainForm.GetLastLogSelectedDataEntry();

                xmlDoc.SelectSingleNode("/Config/FIRSTDAY").InnerText = firstDay;
                xmlDoc.SelectSingleNode("/Config/LICENSE").InnerText = license;
                xmlDoc.SelectSingleNode("/Config/CUSTOMFIELD1").InnerText = customField1;
                xmlDoc.SelectSingleNode("/Config/CUSTOMFIELD2").InnerText = customField2;

                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX0").InnerText = checkedItem0;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX1").InnerText = checkedItem1;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX2").InnerText = checkedItem2;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX3").InnerText = checkedItem3;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX4").InnerText = checkedItem4;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX5").InnerText = checkedItem5;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX6").InnerText = checkedItem6;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX7").InnerText = checkedItem7;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX8").InnerText = checkedItem8;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX9").InnerText = checkedItem9;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX10").InnerText = checkedItem10;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX11").InnerText = checkedItem11;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX12").InnerText = checkedItem12;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX13").InnerText = checkedItem13;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX14").InnerText = checkedItem14;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX15").InnerText = checkedItem15;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX16").InnerText = checkedItem16;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX17").InnerText = checkedItem17;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX18").InnerText = checkedItem18;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX19").InnerText = checkedItem19;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX20").InnerText = checkedItem20;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX21").InnerText = checkedItem21;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX22").InnerText = checkedItem22;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX23").InnerText = checkedItem23;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX24").InnerText = checkedItem24;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX25").InnerText = checkedItem25;
                xmlDoc.SelectSingleNode("/Config/CHECKEDLISTBOX26").InnerText = checkedItem26;

                xmlDoc.SelectSingleNode("/Config/cbStatistic1").InnerText = cbStatistic1;
                xmlDoc.SelectSingleNode("/Config/cbStatistic2").InnerText = cbStatistic2;
                xmlDoc.SelectSingleNode("/Config/cbStatistic3").InnerText = cbStatistic3;
                xmlDoc.SelectSingleNode("/Config/cbStatistic4").InnerText = cbStatistic4;
                xmlDoc.SelectSingleNode("/Config/cbStatistic5").InnerText = cbStatistic5;

                xmlDoc.SelectSingleNode("/Config/LastLogSelected").InnerText = lastLogSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/LastBikeSelected").InnerText = lastBikeSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/LastLogFilterSelected").InnerText = lastLogFilterSelected.ToString();

                xmlDoc.SelectSingleNode("/Config/ChartLogYear").InnerText = lastLogYearChartSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/ChartRoute").InnerText = lastRouteChartSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/ChartType").InnerText = lastTypeChartSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/ChartTimeType").InnerText = lastTypeTimeChartSelected.ToString();

                xmlDoc.SelectSingleNode("/Config/LastMonthlyLogSelected").InnerText = lastMonthlyLogSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/LastLogSelectedDataEntry").InnerText = lastLogSelectedDataEntry.ToString();

                Logger.Log("Write Config Values: FIRSTDAY written:" + firstDay, 0, logSetting);
                Logger.Log("Write Config Values: LICENSE written:" + license, 0, logSetting);
                Logger.Log("Write Config Values: CUSTOMFIELD1 written:" + customField1, 0, logSetting);
                Logger.Log("Write Config Values: CUSTOMFIELD2 written:" + customField2, 0, logSetting);

                Logger.Log("Write Config Values: cbStatistic1 written:" + cbStatistic1, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic2 written:" + cbStatistic2, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic3 written:" + cbStatistic3, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic4 written:" + cbStatistic4, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic5 written:" + cbStatistic5, 0, logSetting);
                Logger.Log("Write Config Values: lastLogSelected written:" + lastLogSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastBikeSelected written:" + lastBikeSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastLogFilterSelected written:" + lastLogFilterSelected, 0, logSetting);

                Logger.Log("Write Config Values: lastLogYearChartSelected written:" + lastLogYearChartSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastRouteChartSelected written:" + lastRouteChartSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastTypeChartSelected written:" + lastTypeChartSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastTypeTimeChartSelected written:" + lastTypeTimeChartSelected, 0, logSetting);

                Logger.Log("Write Config Values: lastMonthlyLogSelected written:" + lastMonthlyLogSelected, 0, logSetting);
                Logger.Log("Write Config Values: lastLogSelectedDataEntry written:" + lastLogSelectedDataEntry, 0, logSetting);

                xmlDoc.Save(pathFile);
            }
        }

        //******************************************************************
        //
        // Methods not used:
        //
        //******************************************************************

        public static void ReadConfigFile_backup()
        {
            XmlReader reader = XmlReader.Create("c:\\CyclingLogApplication\\CyclingLogConfig.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Version")
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                        //if (reader.Name == "value")
                        //{
                        //    while (reader.NodeType != XmlNodeType.EndElement)
                        //    {
                        //        reader.Read();
                        //        if (reader.NodeType == XmlNodeType.Text)
                        //        {
                        //            Console.WriteLine("Price = {0:C}", Double.Parse(reader.Value));
                        //        }
                        //    }
                        //    reader.Read();
                        //} //end if

                        if (reader.Name == "Defaults")
                        {
                            while (reader.NodeType != XmlNodeType.EndElement)
                            {
                                reader.Read();
                                if (reader.Name == "LogYear")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            Console.WriteLine("LogYear = " + reader.Value);
                                        }
                                    }
                                    reader.Read();
                                } //end if

                                if (reader.Name == "Bike")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            Console.WriteLine("Bike = " + reader.Value);
                                        }
                                    }
                                    reader.Read();
                                } //end if
                                if (reader.Name == "Route")
                                {
                                    while (reader.NodeType != XmlNodeType.EndElement)
                                    {
                                        reader.Read();
                                        if (reader.NodeType == XmlNodeType.Text)
                                        {
                                            Console.WriteLine("Route = " + reader.Value);
                                        }
                                    }

                                } //end if
                            }
                        } //end if
                    } //end while
                } //end if

            } //end while
        }


        public static void WriteConfigFile_backup()
        {
            string path = @"c:\CyclingLogApplication";

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));
                } 
            }
            catch (Exception e)
            {
                //Console.WriteLine("The process failed: {0}", e.ToString());
                Logger.LogError("[ERROR]: Exception while trying to Clear ride data." + e.Message.ToString());
                MessageBox.Show("[ERROR] Unable to write the Configuration file. ");

                return;
            }
            finally
            {

            }

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true
            };

            XmlWriter writer = XmlWriter.Create("c:\\CyclingLogApplication\\CyclingLogConfig.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("Do not edit this file. It was generated by the CyclingLogApplicaiton.");
            writer.WriteStartElement("Version");
            writer.WriteAttributeString("ver", "0.0.1");
            //writer.WriteAttributeString("Name", "x");
            //writer.WriteElementString("value", "10.00");

            //Defaults section:
            writer.WriteStartElement("Defaults");
            writer.WriteElementString("LogYear", "1");
            writer.WriteElementString("Bike", "1");
            writer.WriteElementString("Route", "1");
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }
    }
}
