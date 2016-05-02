using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingLogApplication
{
    class Logger
    {
        private static string logPath = "c:\\CyclingLogApplication\\logs\\";
        private static string fileName = "";

        private static string getLogPath()
        {
            return logPath;
        }

        public static void setLogFilePath()
        {
            string path = getLogPath();

            try
            {
                // Determine whether the directory exists.
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    //ServiceLogger.Log("The directory was created successfully at " + Directory.GetCreationTime(path));
               }
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR] Exception setting Log File path: " + ex.Message.ToString());

                return;
            }
            finally
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                fileName = path + "Connector_";
                fileName += DateTime.Now.ToString("yyyy_MM_dd") + ".log";
            }
        }

        public static void Log(string logMessage, int logLevel, int logLevelSetting)
        {
            string logType = "";

            if (logLevel == 0)
            {
                logType = "VERB";
            }
            else if (logLevel == 1)
            {
                logType = "INFO";
            }

            //0=verbose:
            //1=info:
            //2=error only
            // logLevel-logLevelSetting
            // 0-0 yes
            // 0-1 no
            // 0-2 no
            // 1-0 yes
            // 1-1 yes
            // 1-2 no

            if (logLevel == logLevelSetting || logLevel > logLevelSetting)
            {
                setLogFilePath();
                using (StreamWriter w = File.AppendText(fileName))
                {
                    w.WriteLine("[{1}] : {2} : {0}", logMessage, DateTime.Now.ToLongTimeString(), logType);
                }
            }
        }

        public static void LogError(string logMessage)
        {
            //Error messages are written for all log levels:
            setLogFilePath();

            if (!logMessage.Contains("ERROR : "))
            {
                logMessage = "ERROR : "+ logMessage;
            }
            using (StreamWriter w = File.AppendText(fileName))
            {
                w.WriteLine("###############################");
                w.WriteLine("[{1}] : [ERROR] : {0}", logMessage, DateTime.Now.ToLongTimeString());
                w.WriteLine("###############################");
            }
        }

        public static void CleanLogs()
        {
            //Read config for value:
            int daysToKeep = 360;
            int logLevel = 0;
            string path = getLogPath();
            setLogFilePath();
            string[] logFiles = Directory.GetFiles(path);

            foreach (string logFile in logFiles)
            {
                FileInfo fInfo = new FileInfo(logFile);

                if (fInfo.LastWriteTime < DateTime.Now.AddDays(-daysToKeep))
                {
                    try
                    {
                        fInfo.Delete();
                        Log("Cleaning up logs older than " + daysToKeep + " days old.", 1, logLevel);
                        Log("Logs dir : " + path, 0, logLevel);
                    }
                    catch (Exception e)
                    {
                        LogError("Error attempting to remove file : " + logFile + " : " + e.Message);
                    }
                }
            }
        }
    }
}
