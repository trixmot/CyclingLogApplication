using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Printing;
using DGVPrinterHelper;
using static DGVPrinterHelper.DGVPrinter;
using System.IO;
using System.Xml.Linq;

namespace CyclingLogApplication
{
    public partial class RideDataDisplay : Form
    {
        //private static int logYearFilterIndex;
        private readonly SqlConnection sqlConnection;// = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");

        public RideDataDisplay()
        {
            InitializeComponent();
            cbFilterField.SelectedIndex = 0;
            //MainForm mainForm = new MainForm();
            sqlConnection = MainForm.GetsqlConnectionString();

            //MainForm mainform = new MainForm();
            List<string> logList = MainForm.ReadDataNames("Table_Log_year", "Name");

            for (int i = 0; i < logList.Count; i++)
            {
                cbLogYearFilter.Items.Add(logList[i]);
            }

            sqlConnection.Close();
        }

        private void CloseForm(object sender, EventArgs e)
        {
            cbFilterField.SelectedIndex = 0;
            cbFilterValue.Items.Clear();
            cbFilterValue.Items.Add("");
            cbFilterValue.SelectedIndex = 0;
            dataGridView1.DataSource = null;

            sqlConnection.Close();

            this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }

        public void AddLogYearFilter(string item)
        {
            cbLogYearFilter.Items.Add(item);
        }

        public void RemoveLogYearFilter(string item)
        {
            cbLogYearFilter.Items.Remove(item);
        }

        public void SetLogYearFilterIndex(int index)
        {
            try
            {
                cbLogYearFilter.SelectedIndex = index;
            }
            catch
            {
                cbLogYearFilter.SelectedIndex = 0;
            }
        }

        //public static int GetLogYearFilterIndex()
        //{
        //    return logYearFilterIndex;
        //}

        public void SetCustomValues()
        {
            //MainForm mainForm = new MainForm();
            string customValue1 = MainForm.GetCustomField1();
            string customValue2 = MainForm.GetCustomField2();

            if (customValue1.Equals(""))
            {
                this.checkedListBox.Items[25] = "Custom1";
            }
            else
            {
                this.checkedListBox.Items[25] = customValue1;
            }

            if (customValue2.Equals(""))
            {
                this.checkedListBox.Items[26] = "Custom2";
            }
            else
            {
                this.checkedListBox.Items[26] = customValue2;
            }
        }

        public void SetCheckedValues()
        {
            //MainForm mainForm = new MainForm();

            // Set CheckedListBox values:
            if (MainForm.GetCheckedListBoxItem0().Equals("1"))
            {
                checkedListBox.SetItemCheckState(0, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem1().Equals("1"))
            {
                checkedListBox.SetItemCheckState(1, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(1, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem2().Equals("1"))
            {
                checkedListBox.SetItemCheckState(2, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(2, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem3().Equals("1"))
            {
                checkedListBox.SetItemCheckState(3, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(3, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem4().Equals("1"))
            {
                checkedListBox.SetItemCheckState(4, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(4, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem5().Equals("1"))
            {
                checkedListBox.SetItemCheckState(5, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(5, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem6().Equals("1"))
            {
                checkedListBox.SetItemCheckState(6, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(6, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem7().Equals("1"))
            {
                checkedListBox.SetItemCheckState(7, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(7, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem8().Equals("1"))
            {
                checkedListBox.SetItemCheckState(8, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(8, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem9().Equals("1"))
            {
                checkedListBox.SetItemCheckState(9, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(9, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem10().Equals("1"))
            {
                checkedListBox.SetItemCheckState(10, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(10, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem11().Equals("1"))
            {
                checkedListBox.SetItemCheckState(11, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(11, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem12().Equals("1"))
            {
                checkedListBox.SetItemCheckState(12, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(12, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem13().Equals("1"))
            {
                checkedListBox.SetItemCheckState(13, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(13, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem14().Equals("1"))
            {
                checkedListBox.SetItemCheckState(14, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(14, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem15().Equals("1"))
            {
                checkedListBox.SetItemCheckState(15, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(15, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem16().Equals("1"))
            {
                checkedListBox.SetItemCheckState(16, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(16, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem17().Equals("1"))
            {
                checkedListBox.SetItemCheckState(17, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(17, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem18().Equals("1"))
            {
                checkedListBox.SetItemCheckState(18, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(18, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem19().Equals("1"))
            {
                checkedListBox.SetItemCheckState(19, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(19, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem20().Equals("1"))
            {
                checkedListBox.SetItemCheckState(20, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(20, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem21().Equals("1"))
            {
                checkedListBox.SetItemCheckState(21, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(21, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem22().Equals("1"))
            {
                checkedListBox.SetItemCheckState(22, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(22, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem23().Equals("1"))
            {
                checkedListBox.SetItemCheckState(23, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(23, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem24().Equals("1"))
            {
                checkedListBox.SetItemCheckState(24, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(24, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem25().Equals("1"))
            {
                checkedListBox.SetItemCheckState(25, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(25, CheckState.Unchecked);
            }

            if (MainForm.GetCheckedListBoxItem26().Equals("1"))
            {
                checkedListBox.SetItemCheckState(26, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(26, CheckState.Unchecked);
            }
        }

        private void BFilter_Click(object sender, EventArgs e)
        {
            //MainForm mainForm = new MainForm();
            bool customDataField1 = false;
            bool customDataField2 = false;

            string fieldString = "";
            if (checkedListBox.GetItemChecked(0))
            {
                fieldString += "[WeekNumber]";
            }

            if (checkedListBox.GetItemChecked(1))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Id]";
                }
                else
                {
                    fieldString += ",[Id]";
                }
            }

            if (checkedListBox.GetItemChecked(2))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Date]";
                }
                else
                {
                    fieldString += ",[Date]";
                }
            }

            if (checkedListBox.GetItemChecked(3))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[MovingTime]";
                }
                else
                {
                    fieldString += ",[MovingTime]";
                }
            }

            if (checkedListBox.GetItemChecked(4))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[RideDistance]";
                }
                else
                {
                    fieldString += ",[RideDistance]";
                }
            }

            if (checkedListBox.GetItemChecked(5))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[AvgSpeed]";
                }
                else
                {
                    fieldString += ",[AvgSpeed]";
                }
            }

            if (checkedListBox.GetItemChecked(6))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Bike]";
                }
                else
                {
                    fieldString += ",[Bike]";
                }
            }

            if (checkedListBox.GetItemChecked(7))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[RideType]";
                }
                else
                {
                    fieldString += ",[RideType]";
                }
            }

            if (checkedListBox.GetItemChecked(8))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Wind]";
                }
                else
                {
                    fieldString += ",[Wind]";
                }
            }

            if (checkedListBox.GetItemChecked(9))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Temperature]";
                }
                else
                {
                    fieldString += ",[Temperature]";
                }
            }

            if (checkedListBox.GetItemChecked(10))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[AvgCadence]";
                }
                else
                {
                    fieldString += ",[AvgCadence]";
                }
            }

            if (checkedListBox.GetItemChecked(11))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[MaxCadence]";
                }
                else
                {
                    fieldString += ",[MaxCadence]";
                }
            }


            if (checkedListBox.GetItemChecked(12))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[AvgHeartRate]";
                }
                else
                {
                    fieldString += ",[AvgHeartRate]";
                }
            }

            if (checkedListBox.GetItemChecked(13))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[MaxHeartRate]";
                }
                else
                {
                    fieldString += ",[MaxHeartRate]";
                }
            }

            if (checkedListBox.GetItemChecked(14))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Calories]";
                }
                else
                {
                    fieldString += ",[Calories]";
                }
            }

            if (checkedListBox.GetItemChecked(15))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[TotalAscent]";
                }
                else
                {
                    fieldString += ",[TotalAscent]";
                }
            }

            if (checkedListBox.GetItemChecked(16))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[TotalDescent]";
                }
                else
                {
                    fieldString += ",[TotalDescent]";
                }
            }

            if (checkedListBox.GetItemChecked(17))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Route]";
                }
                else
                {
                    fieldString += ",[Route]";
                }
            }

            if (checkedListBox.GetItemChecked(18))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Location]";
                }
                else
                {
                    fieldString += ",[Location]";
                }
            }

            if (checkedListBox.GetItemChecked(19))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Comments]";
                }
                else
                {
                    fieldString += ",[Comments]";
                }
            }

            if (checkedListBox.GetItemChecked(20))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Effort]";
                }
                else
                {
                    fieldString += ",[Effort]";
                }
            }

            if (checkedListBox.GetItemChecked(21))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[MaxSpeed]";
                }
                else
                {
                    fieldString += ",[MaxSpeed]";
                }
            }

            if (checkedListBox.GetItemChecked(22))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[AveragePower]";
                }
                else
                {
                    fieldString += ",[AveragePower]";
                }
            }

            if (checkedListBox.GetItemChecked(23))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[MaxPower]";
                }
                else
                {
                    fieldString += ",[MaxPower]";
                }
            }

            if (checkedListBox.GetItemChecked(24))
            {
                if (fieldString.Equals(""))
                {
                    fieldString += "[Comfort]";
                }
                else
                {
                    fieldString += ",[Comfort]";
                }
            }

            if (checkedListBox.GetItemChecked(25))
            {
                customDataField1 = true;

                if (fieldString.Equals(""))
                {
                    fieldString += "[Custom1]";
                }
                else
                {
                    fieldString += ",[Custom1]";
                }
            }

            if (checkedListBox.GetItemChecked(26))
            {
                customDataField2 = true;

                if (fieldString.Equals(""))
                {
                    fieldString += "[Custom2]";
                }
                else
                {
                    fieldString += ",[Custom2]";
                }
            }

            try
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = null;
                int logYearID = -1;
                string logYearIDQuery = "";

                //Get the Logyear ID:
                if (cbLogYearFilter.SelectedIndex == 0)
                {
                    logYearID = 0;
                }
                else
                {
                    
                    //logYearID = mainForm.GetLogYearIndex(cbLogYearFilter.SelectedItem.ToString());

                    logYearID = MainForm.GetLogYearIndexByName(cbLogYearFilter.SelectedItem.ToString());

                    logYearIDQuery = " and [LogYearID]=@logyearID";
                }

                //WeekNumber, Bike, RideType, route
                if (cbFilterField.Text.Equals("WeekNumber"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @WeekNumber LIKE WeekNumber" + logYearIDQuery, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@WeekNumber", SqlDbType.BigInt, 5);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WeekNumber", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }

                    //sqlDataAdapter = new SqlDataAdapter("select [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Comments] from Table_Ride_Information WHERE WeekNumber LIKE " + cbFilter.Text + "%", conn);
                }
                else if (cbFilterField.Text.Equals("Bike"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Bike LIKE Bike" + logYearIDQuery, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Bike", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Bike", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("RideType"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @RideType LIKE RideType" + logYearIDQuery, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@RideType", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RideType", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("Route"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Route LIKE Route" + logYearIDQuery, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Route", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("Location"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Location LIKE Location" + logYearIDQuery, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Location", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("Longest"))
                {
                    sqlDataAdapter = new SqlDataAdapter();

                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY[RideDistance] DESC", sqlConnection);
                    }
                    else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information ORDER BY[RideDistance] DESC", sqlConnection);
                    }
                }
                else if (cbFilterField.Text.Equals("Temperature"))
                {
                    sqlDataAdapter = new SqlDataAdapter();

                    //index 0 = "Below 30":
                    //index 1 = "30-49":
                    //index 2 = "50-69":
                    //index 3 = "70-89":
                    //index 4 = "Above 90":

                    if (cbFilterValue.SelectedIndex == 0)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] < " + 30 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 1)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 30 + " and [Temperature] < " + 50 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 2)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 50 + " and [Temperature] < " + 70 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 3)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 70 + " and [Temperature] < " + 90 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 4)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 90 + " " + logYearIDQuery, sqlConnection);
                    }
                    //sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] > " + temp_value + " " + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    //sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Temperature", temp_value);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    if (logYearID != 0)
                    {
                        //sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Id],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] ASC", sqlConnection);
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] ASC", sqlConnection);
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                    else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information ORDER BY [Date] ASC", sqlConnection);
                    }
                }

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Item", typeof(int)));
                //Set AutoIncrement True for the First Column.
                dataTable.Columns["Item"].AutoIncrement = true;
                //Set the Starting or Seed value.
                dataTable.Columns["Item"].AutoIncrementSeed = 1;
                //Set the Increment value.
                dataTable.Columns["Item"].AutoIncrementStep = 1;
                sqlDataAdapter.Fill(dataTable);

                string customValue1 = MainForm.GetCustomField1();
                string customValue2 = MainForm.GetCustomField2();

                if (customDataField1)
                {
                    if (customValue1.Equals(""))
                    {
                        dataTable.Columns["Custom1"].ColumnName = "Custom1";
                    }
                    else
                    {
                        dataTable.Columns["Custom1"].ColumnName = customValue1;
                    }
                }

                if (customDataField2)
                {
                    if (customValue2.Equals(""))
                    {
                        dataTable.Columns["Custom2"].ColumnName = "Custom2";
                    }
                    else
                    {
                        dataTable.Columns["Custom2"].ColumnName = customValue2;
                    }
                }

                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["AvgSpeed"].DefaultCellStyle.Format = "0.00";
                dataGridView1.Columns["RideDistance"].DefaultCellStyle.Format = "0.0";

                // Resize the master DataGridView columns to fit the newly loaded data.
                dataGridView1.AutoResizeColumns();
                dataGridView1.AllowUserToOrderColumns = true;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.ReadOnly = true;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridView1.ColumnHeadersHeight = 30;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query ride data: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering ride data.  Review the log for more information.");
            }
            finally
            {
                // close connection
                sqlConnection?.Close();
            }
        }

        private void BtClear_Click(object sender, EventArgs e)
        {
            cbFilterField.SelectedIndex = 0;
            cbFilterValue.Items.Clear();
            cbFilterValue.Items.Add("");
            cbFilterValue.SelectedIndex = 0;

            SqlConnection conn = null;

            try
            {
                //conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""\\mac\home\documents\visual studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");
                //conn.Open();
                //SqlDataAdapter sqlDataAdapter = null;

                //sqlDataAdapter = new SqlDataAdapter();
                //sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information", conn);

                //DataTable dataTable = new DataTable();
                //sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = null;
                //dataGridView1.Rows.Clear();
                // dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to Clear ride data." + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred.  Review the log for more information.");
            }
            finally
            {
                // close connection
                conn?.Close();
            }
        }

        private void CbLogYearFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MainForm mainForm = new MainForm("");
            MainForm.SetLastLogFilterSelected(cbLogYearFilter.SelectedIndex);
        }

        private void CbFilterFieldChanged(object sender, EventArgs e)
        {
            //MainForm mainForm = new MainForm("");
            cbFilterValue.Items.Clear();

            //NONE          - 0
            //Bike          - 1
            //Location      - 2
            //Longest       - 3
            //RideType      - 4
            //Route         - 5
            //Temperature   - 6
            //WeekNumber    - 7

            if (cbFilterField.SelectedIndex == 0)
            {
                //NONE
                cbFilterValue.Items.Add("");
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 1)
            {
                //Load Bike values:
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                List<string> bikeList = new List<string>();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");

                foreach (var val in bikeList)
                {
                    cbFilterValue.Items.Add(val);
                }
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 2)
            {
                //Location:
                cbFilterValue.Items.Add("Road");
                cbFilterValue.Items.Add("Rollers");
                cbFilterValue.Items.Add("Trail");
                cbFilterValue.Items.Add("Trainer");
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 3)
            {
                //Longest:
                cbFilterValue.Items.Add("");
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 4)
            {
                //RideType:
                cbFilterValue.Items.Add("Recovery");
                cbFilterValue.Items.Add("Base");
                cbFilterValue.Items.Add("Distance");
                cbFilterValue.Items.Add("Speed");
                cbFilterValue.Items.Add("Race");
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 5)
            {
                //Route:
                List<string> routeList = MainForm.ReadDataNames("Table_Routes", "Name");
                for (int i = 1; i < routeList.Count; i++)
                {
                    cbFilterValue.Items.Add(routeList.ElementAt(i));
                }
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 6)
            {
                //Temperature:
                cbFilterValue.Items.Add("Below 30");
                cbFilterValue.Items.Add("30-49");
                cbFilterValue.Items.Add("50-69");
                cbFilterValue.Items.Add("70-89");
                cbFilterValue.Items.Add("Above 90");
                cbFilterValue.SelectedIndex = 0;
            }
            else if (cbFilterField.SelectedIndex == 7)
            {
                //WeekNumber:
                for (int i = 1; i < 53; i++)
                {
                    cbFilterValue.Items.Add(i);
                }
                cbFilterValue.SelectedIndex = 0;
            }
        }

        private void BtUpdateFields_Click(object sender, EventArgs e)
        {
            //MainForm mainForm = new MainForm();

            if (checkedListBox.GetItemChecked(0))
            {
                MainForm.SetCheckedListBoxItem0("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem0("0");
            }

            if (checkedListBox.GetItemChecked(1))
            {
                MainForm.SetCheckedListBoxItem1("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem1("0");
            }

            if (checkedListBox.GetItemChecked(2))
            {
                MainForm.SetCheckedListBoxItem2("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem2("0");
            }

            if (checkedListBox.GetItemChecked(3))
            {
                MainForm.SetCheckedListBoxItem3("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem3("0");
            }

            if (checkedListBox.GetItemChecked(4))
            {
                MainForm.SetCheckedListBoxItem4("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem4("0");
            }

            if (checkedListBox.GetItemChecked(5))
            {
                MainForm.SetCheckedListBoxItem5("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem5("0");
            }

            if (checkedListBox.GetItemChecked(6))
            {
                MainForm.SetCheckedListBoxItem6("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem6("0");
            }

            if (checkedListBox.GetItemChecked(7))
            {
                MainForm.SetCheckedListBoxItem7("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem7("0");
            }

            if (checkedListBox.GetItemChecked(8))
            {
                MainForm.SetCheckedListBoxItem8("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem8("0");
            }

            if (checkedListBox.GetItemChecked(9))
            {
                MainForm.SetCheckedListBoxItem9("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem9("0");
            }

            if (checkedListBox.GetItemChecked(10))
            {
                MainForm.SetCheckedListBoxItem10("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem10("0");
            }

            if (checkedListBox.GetItemChecked(11))
            {
                MainForm.SetCheckedListBoxItem11("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem11("0");
            }

            if (checkedListBox.GetItemChecked(12))
            {
                MainForm.SetCheckedListBoxItem12("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem12("0");
            }

            if (checkedListBox.GetItemChecked(13))
            {
                MainForm.SetCheckedListBoxItem13("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem13("0");
            }

            if (checkedListBox.GetItemChecked(14))
            {
                MainForm.SetCheckedListBoxItem14("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem14("0");
            }

            if (checkedListBox.GetItemChecked(15))
            {
                MainForm.SetCheckedListBoxItem15("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem15("0");
            }

            if (checkedListBox.GetItemChecked(16))
            {
                MainForm.SetCheckedListBoxItem16("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem16("0");
            }

            if (checkedListBox.GetItemChecked(17))
            {
                MainForm.SetCheckedListBoxItem17("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem17("0");
            }

            if (checkedListBox.GetItemChecked(18))
            {
                MainForm.SetCheckedListBoxItem18("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem18("0");
            }

            if (checkedListBox.GetItemChecked(19))
            {
                MainForm.SetCheckedListBoxItem19("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem19("0");
            }

            if (checkedListBox.GetItemChecked(20))
            {
                MainForm.SetCheckedListBoxItem20("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem20("0");
            }

            if (checkedListBox.GetItemChecked(21))
            {
                MainForm.SetCheckedListBoxItem21("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem21("0");
            }

            if (checkedListBox.GetItemChecked(22))
            {
                MainForm.SetCheckedListBoxItem22("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem22("0");
            }

            if (checkedListBox.GetItemChecked(23))
            {
                MainForm.SetCheckedListBoxItem23("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem23("0");
            }

            if (checkedListBox.GetItemChecked(24))
            {
                MainForm.SetCheckedListBoxItem24("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem24("0");
            }

            if (checkedListBox.GetItemChecked(25))
            {
                MainForm.SetCheckedListBoxItem25("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem25("0");
            }

            if (checkedListBox.GetItemChecked(26))
            {
                MainForm.SetCheckedListBoxItem26("1");
            }
            else
            {
                MainForm.SetCheckedListBoxItem26("0");
            }

            MessageBox.Show("The Display field options have been updated.");
        }

        private void BtSelectAll_Click(object sender, EventArgs e)
        {
            checkedListBox.SetItemCheckState(0, CheckState.Checked);
            checkedListBox.SetItemCheckState(1, CheckState.Checked);
            checkedListBox.SetItemCheckState(2, CheckState.Checked);
            checkedListBox.SetItemCheckState(3, CheckState.Checked);
            checkedListBox.SetItemCheckState(4, CheckState.Checked);
            checkedListBox.SetItemCheckState(5, CheckState.Checked);
            checkedListBox.SetItemCheckState(6, CheckState.Checked);
            checkedListBox.SetItemCheckState(7, CheckState.Checked);
            checkedListBox.SetItemCheckState(8, CheckState.Checked);
            checkedListBox.SetItemCheckState(9, CheckState.Checked);
            checkedListBox.SetItemCheckState(10, CheckState.Checked);
            checkedListBox.SetItemCheckState(11, CheckState.Checked);
            checkedListBox.SetItemCheckState(12, CheckState.Checked);
            checkedListBox.SetItemCheckState(13, CheckState.Checked);
            checkedListBox.SetItemCheckState(14, CheckState.Checked);
            checkedListBox.SetItemCheckState(15, CheckState.Checked);
            checkedListBox.SetItemCheckState(16, CheckState.Checked);
            checkedListBox.SetItemCheckState(17, CheckState.Checked);
            checkedListBox.SetItemCheckState(18, CheckState.Checked);
            checkedListBox.SetItemCheckState(19, CheckState.Checked);
            checkedListBox.SetItemCheckState(20, CheckState.Checked);
            checkedListBox.SetItemCheckState(21, CheckState.Checked);
            checkedListBox.SetItemCheckState(22, CheckState.Checked);
            checkedListBox.SetItemCheckState(23, CheckState.Checked);
            checkedListBox.SetItemCheckState(24, CheckState.Checked);
            checkedListBox.SetItemCheckState(25, CheckState.Checked);
            checkedListBox.SetItemCheckState(26, CheckState.Checked);
        }

        private void BtDeselectAll_Click(object sender, EventArgs e)
        {
            checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(1, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(2, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(3, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(4, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(5, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(6, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(7, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(8, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(9, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(10, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(11, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(12, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(13, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(14, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(15, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(16, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(17, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(18, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(19, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(20, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(21, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(22, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(23, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(24, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(25, CheckState.Unchecked);
            checkedListBox.SetItemCheckState(26, CheckState.Unchecked);
        }

        private void BtPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter
            {
                Title = "Cycling Log Report",
                //printer.SubTitle = "An Easy to Use DataGridView Printing Object";
                SubTitleFormatFlags = StringFormatFlags.LineLimit |
                StringFormatFlags.NoClip,
                PageNumbers = true,
                PageNumberInHeader = false,
                //printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
                ColumnWidth = DGVPrinter.ColumnWidthSetting.CellWidth,
                HeaderCellAlignment = StringAlignment.Near,
                //printer.Footer = "";
                FooterSpacing = 15
            };
            printer.PageNumberFormat.Alignment = StringAlignment.Center;

            if (DialogResult.OK == printer.DisplayPrintDialog()) {  // you may replace 


                // print without redisplaying the printdialog 
                printer.PrintNoDisplay(dataGridView1);
                //printer.PrintPreviewDataGridView(dataGridView1);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //if (!cbUpdateValues.Checked)
            //{
            //    return;
            //}

            string setCommand = "";

            //Create a new dictionary of strings for the parameters to use in the sql command:
            Dictionary<string, string> sqlPrameters = new Dictionary<string, string>();

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            int columnindex = dataGridView1.CurrentCell.ColumnIndex;

            RideDataEntry rideDataEntryForm = new RideDataEntry();
            //rideDataEntryForm.cbBikeDataEntrySelection.SelectedIndex = Convert.ToInt32(GetLastBikeSelected());

            //int logIndex = -1;
            int bikeIndex = -1;
            int routeIndex = -1;
            int logNameIndex = cbLogYearFilter.SelectedIndex;


            string weekNumber = "";
            string id = "";
            string date = "";
            string movingTime = "";
            string rideDistance = "";
            string avgSpeed = "";
            string bike = "";
            string rideType = "";
            string wind = "";
            string temp = "";
            string avgCadence = "";
            string maxCadence = "";
            string avgHeartRate = "";
            string maxHeartRate = "";
            string calories = "";
            string totalAscent = "";
            string totalDescent = "";
            string route = "";
            string location = "";
            string comments = "";
            string effort = "";
            string maxSpeed = "";
            string avgPower = "";
            string maxPower = "";
            string comfort = "";
            string custom1 = "";
            string custom2 = "";


            int index;

            if (checkedListBox.GetItemChecked(0))
            {
                var dataGridViewColumn = dataGridView1.Columns["WeekNumber"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                weekNumber = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "WeekNumber=@weekNumber,";
                sqlPrameters.Add("@weekNumber", weekNumber);
            }

            if (checkedListBox.GetItemChecked(1))
            {
                var dataGridViewColumn = dataGridView1.Columns["Id"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                id = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Id=@id ";
                sqlPrameters.Add("@id", id);
            }

            if (checkedListBox.GetItemChecked(2))
            {
                var dataGridViewColumn = dataGridView1.Columns["Date"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                date = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Date=@date,";
                sqlPrameters.Add("@date", date);
            }

            if (checkedListBox.GetItemChecked(3))
            {
                var dataGridViewColumn = dataGridView1.Columns["MovingTime"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                movingTime = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "MovingTime=@movingTime,";
                sqlPrameters.Add("@movingTime", movingTime);
            }

            if (checkedListBox.GetItemChecked(4))
            {
                var dataGridViewColumn = dataGridView1.Columns["RideDistance"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                rideDistance = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "RideDistance=@rideDistance,";
                sqlPrameters.Add("@rideDistance", rideDistance);
            }

            if (checkedListBox.GetItemChecked(5))
            {
                var dataGridViewColumn = dataGridView1.Columns["AvgSpeed"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                avgSpeed = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "AvgSpeed=@avgSpeed,";
                sqlPrameters.Add("@avgSpeed", avgSpeed);
            }

            if (checkedListBox.GetItemChecked(6))
            {
                var dataGridViewColumn = dataGridView1.Columns["Bike"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                bike = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Bike=@bike,";
                sqlPrameters.Add("@bike", bike);
            }

            if (checkedListBox.GetItemChecked(7))
            {
                var dataGridViewColumn = dataGridView1.Columns["RideType"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                rideType = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "RideType=@rideType,";
                sqlPrameters.Add("@rideType", rideType);
            }

            if (checkedListBox.GetItemChecked(8))
            {
                var dataGridViewColumn = dataGridView1.Columns["Wind"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                wind = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Wind=@wind,";
                sqlPrameters.Add("@wind", wind);
            }

            if (checkedListBox.GetItemChecked(9))
            {
                var dataGridViewColumn = dataGridView1.Columns["Temp"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                temp = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Temp=@temp,";
                sqlPrameters.Add("@temp", temp);
            }

            if (checkedListBox.GetItemChecked(10))
            {
                var dataGridViewColumn = dataGridView1.Columns["AvgCadence"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                avgCadence = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "AvgCadence=@avgCadence,";
                sqlPrameters.Add("@avgCadence", avgCadence);
            }

            if (checkedListBox.GetItemChecked(11))
            {
                var dataGridViewColumn = dataGridView1.Columns["MaxCadence"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                maxCadence = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "MaxCadence=@maxCadence,";
                sqlPrameters.Add("@maxCadence", maxCadence);
            }

            if (checkedListBox.GetItemChecked(12))
            {
                var dataGridViewColumn = dataGridView1.Columns["AvgHeartRate"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                avgHeartRate = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "AvgHeartRate=@avgHeartRate,";
                sqlPrameters.Add("@avgHeartRate", avgHeartRate);
            }

            if (checkedListBox.GetItemChecked(13))
            {
                var dataGridViewColumn = dataGridView1.Columns["MaxHeartRate"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                maxHeartRate = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "MaxHeartRate=@maxHeartRate,";
                sqlPrameters.Add("@maxHeartRate", maxHeartRate);
            }

            if (checkedListBox.GetItemChecked(14))
            {
                var dataGridViewColumn = dataGridView1.Columns["Calories"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                calories = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Calories=@calories,";
                sqlPrameters.Add("@calories", calories);
            }

            if (checkedListBox.GetItemChecked(15))
            {
                var dataGridViewColumn = dataGridView1.Columns["TotalAscent"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                totalAscent = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "TotalAscent=@totalAscent,";
                sqlPrameters.Add("@totalAscent", totalAscent);
            }

            if (checkedListBox.GetItemChecked(16))
            {
                var dataGridViewColumn = dataGridView1.Columns["TotalDescent"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                totalDescent = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "TotalDescent=@totalDescent,";
                sqlPrameters.Add("@totalDescent", totalDescent);
            }

            if (checkedListBox.GetItemChecked(17))
            {
                var dataGridViewColumn = dataGridView1.Columns["Route"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                route = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Route=@route,";
                sqlPrameters.Add("@route", route);
            }

            if (checkedListBox.GetItemChecked(18))
            {
                var dataGridViewColumn = dataGridView1.Columns["Location"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                location = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Location=@location,";
                sqlPrameters.Add("@location", location);
            }

            if (checkedListBox.GetItemChecked(19))
            {
                var dataGridViewColumn = dataGridView1.Columns["Comments"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                comments = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Comments=@comments,";
                sqlPrameters.Add("@comments", comments);
            }

            if (checkedListBox.GetItemChecked(20))
            {
                var dataGridViewColumn = dataGridView1.Columns["Effort"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                effort = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Effort=@effort,";
                sqlPrameters.Add("@effort", effort);
            }

            if (checkedListBox.GetItemChecked(21))
            {
                var dataGridViewColumn = dataGridView1.Columns["MaxSpeed"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                maxSpeed = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "MaxSpeed=@maxSpeed,";
                sqlPrameters.Add("@maxSpeed", maxSpeed);
            }

            if (checkedListBox.GetItemChecked(22))
            {
                var dataGridViewColumn = dataGridView1.Columns["AvgPower"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                avgPower = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "AvgPower=@avgPower,";
                sqlPrameters.Add("@avgPower", avgPower);
            }

            if (checkedListBox.GetItemChecked(23))
            {
                var dataGridViewColumn = dataGridView1.Columns["MaxPower"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                maxPower = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "MaxPower=@maxPower,";
                sqlPrameters.Add("@maxPower", maxPower);
            }

            if (checkedListBox.GetItemChecked(24))
            {
                var dataGridViewColumn = dataGridView1.Columns["Comfort"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                comfort = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Comfort=@comfort,";
                sqlPrameters.Add("@comfort", comfort);
            }

            if (checkedListBox.GetItemChecked(25))
            {
                var dataGridViewColumn = dataGridView1.Columns["Custom1"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                custom1 = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Custom1=@custom1,";
                sqlPrameters.Add("@custom1", custom1);
            }

            if (checkedListBox.GetItemChecked(26))
            {
                var dataGridViewColumn = dataGridView1.Columns["Custom2"];
                index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
                custom2 = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
                setCommand += "Custom2=@custom2,";
                sqlPrameters.Add("@custom2", custom2);
            }

            rideDataEntryForm.SetSqlParameters(sqlPrameters);
            setCommand = setCommand.TrimEnd(',');
            rideDataEntryForm.SetSetCommand(setCommand);

            List<string> bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");

            for (int i = 0; i < bikeList.Count; i++)
            {
                if (bikeList[i].Equals(bike))
                {
                    bikeIndex = i;
                    break;
                }
            }

            List<string> routeList = MainForm.ReadDataNames("Table_Routes", "Name");

            for (int i = 0; i < routeList.Count; i++)
            {
                if (routeList[i].Equals(bike))
                {
                    routeIndex = i;
                    break;
                }
            }

            //Populate fields in the entry form:
            rideDataEntryForm.SetcbLogYearDataEntryIndex(logNameIndex - 1);
            rideDataEntryForm.SettbWeekCountRDE(weekNumber);
            rideDataEntryForm.SetDate(DateTime.Parse(date));
            rideDataEntryForm.SetTime(DateTime.Parse(movingTime));
            rideDataEntryForm.SetDistance(decimal.Parse(rideDistance));
            rideDataEntryForm.SetRoute(routeIndex);
            rideDataEntryForm.SetBike(bikeIndex);
            rideDataEntryForm.SetCalories(calories);

            rideDataEntryForm.ShowDialog();
        }

        private void cbUpdateValues_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUpdateValues.Checked)
            {
                checkedListBox.SetItemCheckState(0, CheckState.Checked);
                checkedListBox.SetItemCheckState(1, CheckState.Checked);
                checkedListBox.SetItemCheckState(2, CheckState.Checked);
                checkedListBox.SetItemCheckState(3, CheckState.Checked);
                checkedListBox.SetItemCheckState(4, CheckState.Checked);
                checkedListBox.SetItemCheckState(5, CheckState.Checked);
                checkedListBox.SetItemCheckState(6, CheckState.Checked);
                checkedListBox.SetItemCheckState(7, CheckState.Checked);
                checkedListBox.SetItemCheckState(8, CheckState.Checked);
                checkedListBox.SetItemCheckState(9, CheckState.Checked);
                checkedListBox.SetItemCheckState(10, CheckState.Checked);
                checkedListBox.SetItemCheckState(11, CheckState.Checked);
                checkedListBox.SetItemCheckState(12, CheckState.Checked);
                checkedListBox.SetItemCheckState(13, CheckState.Checked);
                checkedListBox.SetItemCheckState(14, CheckState.Checked);
                checkedListBox.SetItemCheckState(15, CheckState.Checked);
                checkedListBox.SetItemCheckState(16, CheckState.Checked);
                checkedListBox.SetItemCheckState(17, CheckState.Checked);
                checkedListBox.SetItemCheckState(18, CheckState.Checked);
                checkedListBox.SetItemCheckState(19, CheckState.Checked);
                checkedListBox.SetItemCheckState(20, CheckState.Checked);
                checkedListBox.SetItemCheckState(21, CheckState.Checked);
                checkedListBox.SetItemCheckState(22, CheckState.Checked);
                checkedListBox.SetItemCheckState(23, CheckState.Checked);
                checkedListBox.SetItemCheckState(24, CheckState.Checked);
                checkedListBox.SetItemCheckState(25, CheckState.Checked);
                checkedListBox.SetItemCheckState(26, CheckState.Checked);
            }
        }


       
    }
}
