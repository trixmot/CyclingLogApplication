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

        public bool readConfigFile()
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

                if (!Directory.Exists(path) || !File.Exists(pathFile))
                {
                    writeConfigFile();
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
            List<string> configList = new List<string>();

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

            string lastLogYearSelected = nodes.Item(0).SelectSingleNode("LastLogSelected").InnerText;
            string lastBikeSelected = nodes.Item(0).SelectSingleNode("LastBikeSelected").InnerText;
            string lastLogYearFilterSelected = nodes.Item(0).SelectSingleNode("LastLogFilterSelected").InnerText;

            string chartLogYearSelected = nodes.Item(0).SelectSingleNode("ChartLogYear").InnerText;
            string chartRouteSelected = nodes.Item(0).SelectSingleNode("ChartRoute").InnerText;
            string chartTypeSelected = nodes.Item(0).SelectSingleNode("ChartType").InnerText;
            string chartTimeTypeSelected = nodes.Item(0).SelectSingleNode("ChartTimeType").InnerText;

            string lastMonthlyLogYearSelected = nodes.Item(0).SelectSingleNode("LastMonthlyLogSelected").InnerText;
            string lastLogYearSelectedDataEntry = nodes.Item(0).SelectSingleNode("LastLogSelectedDataEntry").InnerText;

            MainForm mainForm = new MainForm("");
            mainForm.SetLogLevel(logLevel);
            mainForm.SetcbStatistic1(cbStatistic1);
            mainForm.SetcbStatistic2(cbStatistic2);
            mainForm.SetcbStatistic3(cbStatistic3);
            mainForm.SetcbStatistic4(cbStatistic4);
            mainForm.SetcbStatistic5(cbStatistic5);
            mainForm.SetLastLogFilterSelected(Convert.ToInt32(lastLogYearFilterSelected));
            mainForm.SetLastBikeSelected(Convert.ToInt32(lastBikeSelected));
            mainForm.SetLastLogSelected(Convert.ToInt32(lastLogYearSelected));

            mainForm.SetLastLogYearChartSelected(Convert.ToInt32(chartLogYearSelected));
            mainForm.SetLastRouteChartSelected(Convert.ToInt32(chartRouteSelected));
            mainForm.SetLastTypeChartSelected(Convert.ToInt32(chartTypeSelected));
            mainForm.SetLastTypeTimeChartSelected(Convert.ToInt32(chartTimeTypeSelected));

            mainForm.SetLastMonthlyLogSelected(Convert.ToInt32(lastMonthlyLogYearSelected));
            mainForm.SetLastLogSelectedDataEntry(Convert.ToInt32(lastLogYearSelectedDataEntry));

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

            returnStatus = true;

            return returnStatus;
        }

        public void writeConfigFile()
        {
            //This will give us the full name path of the executable file:
            //i.e. C:\Program Files\MyApplication\MyApplication.exe
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //This will strip just the working path name:
            //C:\Program Files\MyApplication
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            string path = strWorkPath + "\\settings";
            string pathFile = strWorkPath + "\\settings\\CyclingLogConfig.xml";
            MainForm mainForm = new MainForm("");
            int logSetting = mainForm.GetLogLevel();

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
                    XmlNode rootNode = xmlDoc.CreateElement("Config");
                    xmlDoc.AppendChild(rootNode);
                    //XmlNode commentNode1 = xmlDoc.CreateComment("This file was created by the ExtraViewToRallyConnector application. Do not modify.");
                    //rootNode.AppendChild(commentNode1);
                    //XmlNode commentNode2 = xmlDoc.CreateComment("The Time stamp value is used to compare Last modified times of records for updating.");
                    //rootNode.AppendChild(commentNode2);
                    XmlNode logDaysNode = xmlDoc.CreateElement("DAYSTOKEEPLOGS");
                    logDaysNode.InnerText = "90";
                    rootNode.AppendChild(logDaysNode);

                    XmlNode versionNode = xmlDoc.CreateElement("VERSION");
                    versionNode.InnerText = mainForm.GetLogVersion();
                    rootNode.AppendChild(versionNode);

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

                string cbStatistic1 = mainForm.GetcbStatistic1();
                string cbStatistic2 = mainForm.GetcbStatistic2();
                string cbStatistic3 = mainForm.GetcbStatistic3();
                string cbStatistic4 = mainForm.GetcbStatistic4();
                string cbStatistic5 = mainForm.GetcbStatistic5();

                int lastLogSelected = mainForm.GetLastLogSelected();
                int lastBikeSelected = mainForm.GetLastBikeSelected();
                int lastLogFilterSelected = mainForm.GetLastLogFilterSelected();

                int lastLogYearChartSelected = mainForm.GetLastLogYearChartSelected();
                int lastRouteChartSelected = mainForm.GetLastRouteChartSelected();
                int lastTypeChartSelected = mainForm.GetLastTypeChartSelected();
                int lastTypeTimeChartSelected = mainForm.GetLastTypeTimeChartSelected();

                int lastMonthlyLogSelected = mainForm.GetLastMonthlyLogSelected();
                int lastLogSelectedDataEntry = mainForm.GetLastLogSelectedDataEntry();

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

        public void ReadConfigFile_backup()
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


        public void WriteConfigFile_backup()
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
