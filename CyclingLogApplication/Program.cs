using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace CyclingLogApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //https://social.technet.microsoft.com/wiki/contents/articles/940.how-to-embed-sql-server-express-in-an-application.aspx
            //WebPlatformInstaller.exe /id SQLExpress
            //System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft\Web Platform Installer\webplatforminstaller.exe"," /id SQLExpress");
            //System.Diagnostics.Process.Start("http://www.microsoft.com/web/gallery/install.aspx?appsxml=&appid=SQLExpress");

            //Process process = Process.Start(@"sc", " query MSSQLSERVER");
            Process compiler = new Process();
            compiler.StartInfo.FileName = "sc";
            compiler.StartInfo.Arguments = "query MSSQLSERVER";
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.Start();

            //Console.WriteLine(compiler.StandardOutput.ReadToEnd());
            Logger.Log("[DEBUG] " + compiler.StandardOutput.ReadToEnd(), 0, 0);
            compiler.WaitForExit();

            //System.Diagnostics.Process.Start(@"sc", " query MSSQLSERVER");
            //Command line:
            //sc query MSSQLSERVER
            //Wmic service where (PathName like '%Binn\\sqlservr%') get caption, name, startmode, state, PathName, ProcessId

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool EnumerateSQLInstances()
        {
            string correctNamespace = GetCorrectWmiNameSpace();

            if (string.Equals(correctNamespace, string.Empty))
            {
                return false;
            }

            string query = string.Format("select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and PropertyName = 'instanceID'");
            ManagementObjectSearcher getSqlEngine = new ManagementObjectSearcher(correctNamespace, query);

            if (getSqlEngine.Get().Count == 0)
            {
                return false;
            }

            Console.WriteLine("SQL Server database instances discovered :");
            string instanceName = string.Empty;
            string serviceName = string.Empty;
            string version = string.Empty;
            string edition = string.Empty;
            Console.WriteLine("Instance Name \t ServiceName \t Edition \t Version \t");

            foreach (ManagementObject sqlEngine in getSqlEngine.Get())
            {
                serviceName = sqlEngine["ServiceName"].ToString();
                instanceName = GetInstanceNameFromServiceName(serviceName);
                version = GetWmiPropertyValueForEngineService(serviceName, correctNamespace, "Version");
                edition = GetWmiPropertyValueForEngineService(serviceName, correctNamespace, "SKUNAME");
                Console.Write("{0} \t", instanceName);
                Console.Write("{0} \t", serviceName);
                Console.Write("{0} \t", edition);
                Console.WriteLine("{0} \t", version);
            }

            return true;
        }

        /// <summary>
        /// Method returns the correct SQL namespace to use to detect SQL Server instances.
        /// </summary>
        /// <returns>namespace to use to detect SQL Server instances</returns>
        public static string GetCorrectWmiNameSpace()
        {
            String wmiNamespaceToUse = "root\\Microsoft\\sqlserver";
            List<string> namespaces = new List<string>();

            try
            {
                // Enumerate all WMI instances of
                // __namespace WMI class.
                ManagementClass nsClass =
                    new ManagementClass(
                    new ManagementScope(wmiNamespaceToUse),
                    new ManagementPath("__namespace"),
                    null);

                foreach (ManagementObject ns in nsClass.GetInstances())
                {
                    namespaces.Add(ns["Name"].ToString());
                }
            }

            catch (ManagementException e)
            {
                Console.WriteLine("Exception = " + e.Message);
            }

            if (namespaces.Count > 0)
            {
                if (namespaces.Contains("ComputerManagement10"))
                {
                    //use katmai+ namespace
                    wmiNamespaceToUse = wmiNamespaceToUse + "\\ComputerManagement10";
                }
                else if (namespaces.Contains("ComputerManagement"))
                {
                    //use yukon namespace
                    wmiNamespaceToUse = wmiNamespaceToUse + "\\ComputerManagement";
                }
                else
                {
                    wmiNamespaceToUse = string.Empty;
                }
            }
            else
            {
                wmiNamespaceToUse = string.Empty;
            }

            return wmiNamespaceToUse;
        }

        /// <summary>
        /// method extracts the instance name from the service name
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static string GetInstanceNameFromServiceName(string serviceName)
        {
            if (!string.IsNullOrEmpty(serviceName))
            {
                if (string.Equals(serviceName, "MSSQLSERVER", StringComparison.OrdinalIgnoreCase))
                {
                    return serviceName;
                }
                else
                {
                    return serviceName.Substring(serviceName.IndexOf('$') + 1, serviceName.Length - serviceName.IndexOf('$') - 1);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the WMI property value for a given property name for a particular SQL Server service Name
        /// </summary>
        /// <param name="serviceName">The service name for the SQL Server engine service to query for</param>
        /// <param name="wmiNamespace">The wmi namespace to connect to </param>
        /// <param name="propertyName">The property name whose value is required</param>
        /// <returns></returns>
        public static string GetWmiPropertyValueForEngineService(string serviceName, string wmiNamespace, string propertyName)
        {
            string propertyValue = string.Empty;
            string query = String.Format("select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and PropertyName = '{0}' and ServiceName = '{1}'", propertyName, serviceName);
            ManagementObjectSearcher propertySearcher = new ManagementObjectSearcher(wmiNamespace, query);

            foreach (ManagementObject sqlEdition in propertySearcher.Get())
            {
                propertyValue = sqlEdition["PropertyStrValue"].ToString();
            }

            return propertyValue;
        }
    }
}
