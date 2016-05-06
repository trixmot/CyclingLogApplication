using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CyclingLogApplication
{
    class ConfigurationFile
    {

        public bool readConfigFile()
        {
            string path = "C:\\CyclingLogApplication";
            string pathFile = "C:\\CyclingLogApplication\\CyclingLogConfig.xml";
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
            doc.Load(@"C:\\CyclingLogApplication\\CyclingLogConfig.xml");
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

            MainForm mainForm = new MainForm();
            mainForm.setLogLevel(logLevel);
            mainForm.setcbStatistic1(cbStatistic1);
            mainForm.setcbStatistic2(cbStatistic2);
            mainForm.setcbStatistic3(cbStatistic3);
            mainForm.setcbStatistic4(cbStatistic4);
            mainForm.setcbStatistic5(cbStatistic5);
            mainForm.setLastLogFilterSelected(Convert.ToInt32(lastLogYearFilterSelected));
            mainForm.setLastBikeSelected(Convert.ToInt32(lastBikeSelected));
            mainForm.setLastLogSelected(Convert.ToInt32(lastLogYearSelected));

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

            returnStatus = true;

            return returnStatus;
        }

        public void writeConfigFile()
        {
            string path = "C:\\CyclingLogApplication";
            string pathFile = "C:\\CyclingLogApplication\\CyclingLogConfig.xml";
            MainForm mainForm = new MainForm();
            int logSetting = mainForm.getLogLevel();

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
                    versionNode.InnerText = mainForm.getLogVersion();
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

                    XmlNode LastBikeSelectedNode = xmlDoc.CreateElement("LastBikeSelected");
                    LastBikeSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(LastBikeSelectedNode);

                    XmlNode LastLogSelectedNode = xmlDoc.CreateElement("LastLogSelected");
                    LastLogSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(LastLogSelectedNode);

                    XmlNode LastLogFilterSelectedNode = xmlDoc.CreateElement("LastLogFilterSelected");
                    LastLogFilterSelectedNode.InnerText = "-1";
                    rootNode.AppendChild(LastLogFilterSelectedNode);

                    xmlDoc.Save("C:\\CyclingLogApplication\\CyclingLogConfig.xml");
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
                xmlDoc.Load(@"C:\\CyclingLogApplication\\CyclingLogConfig.xml");

                string cbStatistic1 = mainForm.getcbStatistic1();
                string cbStatistic2 = mainForm.getcbStatistic2();
                string cbStatistic3 = mainForm.getcbStatistic3();
                string cbStatistic4 = mainForm.getcbStatistic4();
                string cbStatistic5 = mainForm.getcbStatistic5();

                int LastLogSelected = mainForm.getLastLogSelected();
                int LastBikeSelected = mainForm.getLastBikeSelected();
                int LastLogFilterSelected = mainForm.getLastLogFilterSelected();

                xmlDoc.SelectSingleNode("/Config/cbStatistic1").InnerText = cbStatistic1;
                xmlDoc.SelectSingleNode("/Config/cbStatistic2").InnerText = cbStatistic2;
                xmlDoc.SelectSingleNode("/Config/cbStatistic3").InnerText = cbStatistic3;
                xmlDoc.SelectSingleNode("/Config/cbStatistic4").InnerText = cbStatistic4;
                xmlDoc.SelectSingleNode("/Config/cbStatistic5").InnerText = cbStatistic5;

                xmlDoc.SelectSingleNode("/Config/LastLogSelected").InnerText = LastLogSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/LastBikeSelected").InnerText = LastBikeSelected.ToString();
                xmlDoc.SelectSingleNode("/Config/LastLogFilterSelected").InnerText = LastLogFilterSelected.ToString();

                Logger.Log("Write Config Values: cbStatistic1 written:" + cbStatistic1, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic2 written:" + cbStatistic2, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic3 written:" + cbStatistic3, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic4 written:" + cbStatistic4, 0, logSetting);
                Logger.Log("Write Config Values: cbStatistic5 written:" + cbStatistic5, 0, logSetting);
                Logger.Log("Write Config Values: LastLogSelected written:" + LastLogSelected, 0, logSetting);
                Logger.Log("Write Config Values: LastBikeSelected written:" + LastBikeSelected, 0, logSetting);
                Logger.Log("Write Config Values: LastLogFilterSelected written:" + LastLogFilterSelected, 0, logSetting);

                xmlDoc.Save(@"C:\\CyclingLogApplication\\CyclingLogConfig.xml");
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
                Console.WriteLine("The process failed: {0}", e.ToString());

                MessageBox.Show("[ERROR] Unable to write the Configuration file. ");

                return;
            }
            finally
            {

            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

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
