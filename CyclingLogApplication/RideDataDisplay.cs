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
using System.Threading;

namespace CyclingLogApplication
{
    public partial class RideDataDisplay : Form
    {
        private SqlConnection sqlConnection;
        private DatabaseConnection databaseConnection;


        public RideDataDisplay()
        {
            try
            {
                InitializeComponent();
                sqlConnection = MainForm.GetsqlConnectionString();
                databaseConnection = MainForm.GetsDatabaseConnectionString();

                cbFilterField.SelectedIndex = 0;
                sqlConnection = MainForm.GetsqlConnectionString();

                string custom1 = MainForm.GetCustomField1();
                string custom2 = MainForm.GetCustomField2();
                Boolean custom1Skipped = false;
                Boolean custom2Skipped = false;
                int numberRemoved = 0;

                List<string> logList = MainForm.ReadDataNames("Table_Log_year", "Name");
                int checkListBoxIndex = 0;
                for (int i = 0; i < logList.Count; i++)
                {
                    cbLogYearFilter.Items.Add(logList[i]);
                }

                if (checkedListBox.Items.Count < 20)
                {
                    Dictionary<string, string> fieldDict = MainForm.GetFieldsDictionary();
                    for (int i = 0; i < fieldDict.Count; i++)
                    {
                        string keyValue = fieldDict.Keys.ElementAt(i);

                        if (custom1.Equals("") && keyValue.Equals("Custom1"))
                        {
                            //skip adding
                            custom1Skipped = true;
                            numberRemoved++;
                        } 
                        else if (custom2.Equals("") && keyValue.Equals("Custom2"))
                        {
                            //skip adding
                            custom2Skipped = true;
                            numberRemoved++;
                        }
                        else
                        {
                            if (custom1Skipped)
                            {
                                checkListBoxIndex--;
                            }
                            if (custom2Skipped)
                            {
                                checkListBoxIndex--;
                            }

                            if (keyValue.Equals("Custom1")){
                                checkedListBox.Items.Insert(checkListBoxIndex, custom1);
                            }
                            else if (keyValue.Equals("Custom2"))
                            {
                                checkedListBox.Items.Insert(checkListBoxIndex, custom2);
                            }
                            else
                            {
                                checkedListBox.Items.Insert(checkListBoxIndex, keyValue);
                            }

                            checkedListBox.SetItemChecked(checkListBoxIndex, bool.Parse(fieldDict.Values.ElementAt(i)));
                            checkListBoxIndex++;
                        }                
                    }
                }

                int heightCLB = 394;

                if (numberRemoved == 1)
                {
                    heightCLB = 379;
                }
                else if (numberRemoved == 2)
                {
                    heightCLB = 364;
                }
                
                checkedListBox.Height = heightCLB;
                //sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to load RideDataDisplay form. " + ex.Message.ToString());
            }
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

        //public void SetCustomValues()
        //{
        //    string customValue1 = MainForm.GetCustomField1();
        //    string customValue2 = MainForm.GetCustomField2();

        //    if (customValue1.Equals(""))
        //    {
        //        this.checkedListBox.Items[23] = "Custom1";
        //    }
        //    else
        //    {
        //        this.checkedListBox.Items[23] = customValue1;
        //    }

        //    if (customValue2.Equals(""))
        //    {
        //        this.checkedListBox.Items[24] = "Custom2";
        //    }
        //    else
        //    {
        //        this.checkedListBox.Items[24] = customValue2;
        //    }
        //}

        private void BFilter_Click(object sender, EventArgs e)
        {
            if (cbLogYearFilter.SelectedIndex < 0)
            {
                MessageBox.Show("In order to view data a log must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            runRideDisplayClear();

            string custom1 = MainForm.GetCustomField1();
            string custom2 = MainForm.GetCustomField2();
            bool customDataField1 = false;
            bool customDataField2 = false;

            bool customDataField1Checked = false;
            bool customDataField2Checked = false;

            string fieldName;

            if (!custom1.Equals(""))
            {
                customDataField1 = true;
            }
            if (!custom2.Equals(""))
            {
                customDataField2 = true;
            }

            string fieldString = "[Id],[Date]";

            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                string nameValue = checkedListBox.Items[i].ToString();
                if (nameValue.Equals(custom1))
                {
                    customDataField1Checked = checkedListBox.GetItemChecked(i);
                }
                if (nameValue.Equals(custom2))
                {
                    customDataField2Checked = checkedListBox.GetItemChecked(i);
                }

                if (checkedListBox.GetItemChecked(i))
                {
                    fieldName = checkedListBox.Items[i].ToString();
                    if (fieldName.Equals("Week Number"))
                    {
                        fieldName = "WeekNumber";
                    } 
                    else if (fieldName.Equals("Moving Time"))
                    {
                        fieldName = "MovingTime";
                    }
                    else if (fieldName.Equals("Ride Distance"))
                    {
                        fieldName = "RideDistance";
                    }
                    else if (fieldName.Equals("Avg Speed"))
                    {
                        fieldName = "AvgSpeed";
                    }
                    else if (fieldName.Equals("Ride Type"))
                    {
                        fieldName = "RideType";
                    }
                    else if (fieldName.Equals("Avg Cadence"))
                    {
                        fieldName = "AvgCadence";
                    }
                    else if (fieldName.Equals("Max Cadence"))
                    {
                        fieldName = "MaxCadence";
                    }
                    else if (fieldName.Equals("Avg Heart Rate"))
                    {
                        fieldName = "AvgHeartRate";
                    }
                    else if (fieldName.Equals("Max Heart Rate"))
                    {
                        fieldName = "MaxHeartRate";
                    }
                    else if (fieldName.Equals("Total Ascent"))
                    {
                        fieldName = "TotalAscent";

                    }
                    else if (fieldName.Equals("Total Descent"))
                    {
                        fieldName = "TotalDescent";
                    }
                    else if (fieldName.Equals("Max Speed"))
                    {
                        fieldName = "MaxSpeed";
                    }
                    else if (fieldName.Equals("Avg Power"))
                    {
                        fieldName = "AveragePower";
                    }
                    else if (fieldName.Equals("Max Power"))
                    {
                        fieldName = "MaxPower";
                    }
                    else if (fieldName.Equals("Wind Chill"))
                    {
                        fieldName = "Windchill";
                    }
                    else if (fieldName.Equals(custom1))
                    {
                        fieldName = "Custom1";
                    }
                    else if (fieldName.Equals(custom2))
                    {
                        fieldName = "Custom2";
                    }

                    fieldString += ",[" + fieldName + "]";              
                    
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

                if (customDataField1Checked && customDataField1)
                {
                    if (custom1.Equals(""))
                    {
                        //dataTable.Columns["Custom1"].ColumnName = "Custom1";
                    }
                    else
                    {
                        dataTable.Columns["Custom1"].ColumnName = custom1;
                    }
                }

                if (customDataField2Checked && customDataField2)
                {
                    if (custom2.Equals(""))
                    {
                        //dataTable.Columns["Custom2"].ColumnName = "Custom2";
                    }
                    else
                    {
                        dataTable.Columns["Custom2"].ColumnName = custom2;
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
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.ReadOnly = true;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dataGridView1.ColumnHeadersHeight = 30;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromName(MainForm.GetDisplayDataColor());

                string idColumnValue = MainForm.GetIDColumnValue();
                //Determine if ID field should appear in the Data Display grid output:
                if (idColumnValue.Equals("0"))
                {
                    dataGridView1.Columns["Id"].Visible = false;
                }
                else
                {
                    dataGridView1.Columns["Id"].Visible = true;
                }

                string textValue = MainForm.GetTextDisplay();
                int rowCount = dataGridView1.Rows.Count;
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
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }

                dataGridView1.Refresh();
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
            runRideDisplayClear();
        }

        private void runRideDisplayClear()
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

        private void BtSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void BtDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
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
            if (!cbUpdateValues.Checked)
            {
                return;
            }

            string id;
            string date;

            RideDataEntry rideDataEntry = new RideDataEntry();

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            //int columnindex = dataGridView1.CurrentCell.ColumnIndex;

            var dataGridViewColumn = dataGridView1.Columns["Id"];
            int index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
            id = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();

            dataGridViewColumn = dataGridView1.Columns["Date"];
            index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
            date = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();

            int logNameIndex = cbLogYearFilter.SelectedIndex;
            rideDataEntry.RideDisplayDataQuery(id, date, logNameIndex-1);

            rideDataEntry.ShowDialog();
        }

        private void UpPictureBox_Click(object sender, EventArgs e)
        {
            if (checkedListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item to move", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int newIndex = checkedListBox.SelectedIndex - 1;

                if (newIndex < 0)
                {
                    return;
                }

                //CheckState.Unchecked
                //CheckState.Checked
                string itemChecked = checkedListBox.GetItemCheckState(checkedListBox.SelectedIndex).ToString();
                object selectedItem = checkedListBox.SelectedItem;
                checkedListBox.Items.Remove(selectedItem);
                checkedListBox.Items.Insert(newIndex, selectedItem);

                if (itemChecked.Equals("Unchecked"))
                {
                    checkedListBox.SetItemCheckState(newIndex, CheckState.Unchecked);
                } 
                else
                {
                    checkedListBox.SetItemCheckState(newIndex, CheckState.Checked);
                }

                checkedListBox.SetSelected(newIndex,true);
            }
        }

        private void DownPictureBox_Click(object sender, EventArgs e)
        {
            if (checkedListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select an item to move", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int newIndex = checkedListBox.SelectedIndex + 1;

                if (newIndex >= checkedListBox.Items.Count)
                {
                    return;
                }

                string itemChecked = checkedListBox.GetItemCheckState(checkedListBox.SelectedIndex).ToString();
                object selectedItem = checkedListBox.SelectedItem;
                checkedListBox.Items.Remove(selectedItem);
                checkedListBox.Items.Insert(newIndex, selectedItem);

                if (itemChecked.Equals("Unchecked"))
                {
                    checkedListBox.SetItemCheckState(newIndex, CheckState.Unchecked);
                }
                else
                {
                    checkedListBox.SetItemCheckState(newIndex, CheckState.Checked);
                }

                checkedListBox.SetSelected(newIndex, true);
            }
        }

        private void btSaveMoves_Click(object sender, EventArgs e)
        {
            //Save changes made to the options order:
            Dictionary<string, string> fieldOptionsDict = new Dictionary<string, string>();
            string custom1 = MainForm.GetCustomField1();
            string custom2 = MainForm.GetCustomField2();

            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                fieldOptionsDict.Add(checkedListBox.Items[i].ToString(), checkedListBox.GetItemChecked(i).ToString());
            }

            if (custom1.Equals(""))
            {
                fieldOptionsDict.Add("Custom1", "False");
            }
            if (custom2.Equals(""))
            {
                fieldOptionsDict.Add("Custom2", "False");
            }

            //Update the dictionary:
            MainForm.SetFieldDictionary(fieldOptionsDict);

            MessageBox.Show("Changes saved for Field Options.");
        }


        //Example of subset of datagridview items
        //private void dataGridView1_Click_BACKUP(object sender, EventArgs e)
        //{
        //    //if (!cbUpdateValues.Checked)
        //    //{
        //    //    return;
        //    //}

        //    string setCommand = "";

        //    //Create a new dictionary of strings for the parameters to use in the sql command:
        //    Dictionary<string, string> sqlPrameters = new Dictionary<string, string>();

        //    int rowindex = dataGridView1.CurrentCell.RowIndex;
        //    int columnindex = dataGridView1.CurrentCell.ColumnIndex;

        //    RideDataEntry rideDataEntryForm = new RideDataEntry();
        //    //rideDataEntryForm.cbBikeDataEntrySelection.SelectedIndex = Convert.ToInt32(GetLastBikeSelected());

        //    //int logIndex = -1;
        //    int bikeIndex = -1;
        //    int routeIndex = -1;
        //    int logNameIndex = cbLogYearFilter.SelectedIndex;


        //    string weekNumber = "";
        //    string id = "";
        //    string date = "";
        //    string movingTime = "";
        //    string rideDistance = "";
        //    string avgSpeed = "";
        //    string bike = "";
        //    string rideType = "";
        //    string wind = "";
        //    string temp = "";
        //    string avgCadence = "";
        //    string maxCadence = "";
        //    string avgHeartRate = "";
        //    string maxHeartRate = "";
        //    string calories = "";
        //    string totalAscent = "";
        //    string totalDescent = "";
        //    string route = "";
        //    string location = "";
        //    string comments = "";
        //    string effort = "";
        //    string maxSpeed = "";
        //    string avgPower = "";
        //    string maxPower = "";
        //    string comfort = "";
        //    string custom1 = "";
        //    string custom2 = "";


        //    int index;

        //    if (checkedListBox.GetItemChecked(0))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["WeekNumber"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        weekNumber = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "WeekNumber=@weekNumber,";
        //        sqlPrameters.Add("@weekNumber", weekNumber);
        //    }

        //    if (checkedListBox.GetItemChecked(1))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Id"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        id = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Id=@id ";
        //        sqlPrameters.Add("@id", id);
        //    }

        //    if (checkedListBox.GetItemChecked(2))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Date"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        date = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Date=@date,";
        //        sqlPrameters.Add("@date", date);
        //    }

        //    if (checkedListBox.GetItemChecked(3))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["MovingTime"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        movingTime = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "MovingTime=@movingTime,";
        //        sqlPrameters.Add("@movingTime", movingTime);
        //    }

        //    if (checkedListBox.GetItemChecked(4))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["RideDistance"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        rideDistance = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "RideDistance=@rideDistance,";
        //        sqlPrameters.Add("@rideDistance", rideDistance);
        //    }

        //    if (checkedListBox.GetItemChecked(5))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["AvgSpeed"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        avgSpeed = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "AvgSpeed=@avgSpeed,";
        //        sqlPrameters.Add("@avgSpeed", avgSpeed);
        //    }

        //    if (checkedListBox.GetItemChecked(6))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Bike"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        bike = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Bike=@bike,";
        //        sqlPrameters.Add("@bike", bike);
        //    }

        //    if (checkedListBox.GetItemChecked(7))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["RideType"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        rideType = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "RideType=@rideType,";
        //        sqlPrameters.Add("@rideType", rideType);
        //    }

        //    if (checkedListBox.GetItemChecked(8))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Wind"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        wind = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Wind=@wind,";
        //        sqlPrameters.Add("@wind", wind);
        //    }

        //    if (checkedListBox.GetItemChecked(9))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Temp"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        temp = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Temp=@temp,";
        //        sqlPrameters.Add("@temp", temp);
        //    }

        //    if (checkedListBox.GetItemChecked(10))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["AvgCadence"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        avgCadence = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "AvgCadence=@avgCadence,";
        //        sqlPrameters.Add("@avgCadence", avgCadence);
        //    }

        //    if (checkedListBox.GetItemChecked(11))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["MaxCadence"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        maxCadence = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "MaxCadence=@maxCadence,";
        //        sqlPrameters.Add("@maxCadence", maxCadence);
        //    }

        //    if (checkedListBox.GetItemChecked(12))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["AvgHeartRate"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        avgHeartRate = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "AvgHeartRate=@avgHeartRate,";
        //        sqlPrameters.Add("@avgHeartRate", avgHeartRate);
        //    }

        //    if (checkedListBox.GetItemChecked(13))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["MaxHeartRate"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        maxHeartRate = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "MaxHeartRate=@maxHeartRate,";
        //        sqlPrameters.Add("@maxHeartRate", maxHeartRate);
        //    }

        //    if (checkedListBox.GetItemChecked(14))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Calories"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        calories = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Calories=@calories,";
        //        sqlPrameters.Add("@calories", calories);
        //    }

        //    if (checkedListBox.GetItemChecked(15))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["TotalAscent"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        totalAscent = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "TotalAscent=@totalAscent,";
        //        sqlPrameters.Add("@totalAscent", totalAscent);
        //    }

        //    if (checkedListBox.GetItemChecked(16))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["TotalDescent"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        totalDescent = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "TotalDescent=@totalDescent,";
        //        sqlPrameters.Add("@totalDescent", totalDescent);
        //    }

        //    if (checkedListBox.GetItemChecked(17))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Route"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        route = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Route=@route,";
        //        sqlPrameters.Add("@route", route);
        //    }

        //    if (checkedListBox.GetItemChecked(18))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Location"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        location = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Location=@location,";
        //        sqlPrameters.Add("@location", location);
        //    }

        //    if (checkedListBox.GetItemChecked(19))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Comments"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        comments = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Comments=@comments,";
        //        sqlPrameters.Add("@comments", comments);
        //    }

        //    if (checkedListBox.GetItemChecked(20))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Effort"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        effort = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Effort=@effort,";
        //        sqlPrameters.Add("@effort", effort);
        //    }

        //    if (checkedListBox.GetItemChecked(21))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["MaxSpeed"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        maxSpeed = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "MaxSpeed=@maxSpeed,";
        //        sqlPrameters.Add("@maxSpeed", maxSpeed);
        //    }

        //    if (checkedListBox.GetItemChecked(22))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["AvgPower"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        avgPower = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "AvgPower=@avgPower,";
        //        sqlPrameters.Add("@avgPower", avgPower);
        //    }

        //    if (checkedListBox.GetItemChecked(23))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["MaxPower"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        maxPower = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "MaxPower=@maxPower,";
        //        sqlPrameters.Add("@maxPower", maxPower);
        //    }

        //    if (checkedListBox.GetItemChecked(24))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Comfort"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        comfort = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Comfort=@comfort,";
        //        sqlPrameters.Add("@comfort", comfort);
        //    }

        //    if (checkedListBox.GetItemChecked(25))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Custom1"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        custom1 = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Custom1=@custom1,";
        //        sqlPrameters.Add("@custom1", custom1);
        //    }

        //    if (checkedListBox.GetItemChecked(26))
        //    {
        //        var dataGridViewColumn = dataGridView1.Columns["Custom2"];
        //        index = dataGridView1.Columns.IndexOf(dataGridViewColumn);
        //        custom2 = dataGridView1.Rows[rowindex].Cells[index].Value.ToString();
        //        setCommand += "Custom2=@custom2,";
        //        sqlPrameters.Add("@custom2", custom2);
        //    }

        //    rideDataEntryForm.SetSqlParameters(sqlPrameters);
        //    setCommand = setCommand.TrimEnd(',');
        //    rideDataEntryForm.SetSetCommand(setCommand);

        //    List<string> bikeList = MainForm.ReadDataNames("Table_Bikes", "Name");

        //    for (int i = 0; i < bikeList.Count; i++)
        //    {
        //        if (bikeList[i].Equals(bike))
        //        {
        //            bikeIndex = i;
        //            break;
        //        }
        //    }

        //    List<string> routeList = MainForm.ReadDataNames("Table_Routes", "Name");

        //    for (int i = 0; i < routeList.Count; i++)
        //    {
        //        if (routeList[i].Equals(bike))
        //        {
        //            routeIndex = i;
        //            break;
        //        }
        //    }

        //    //Populate fields in the entry form:
        //    rideDataEntryForm.SetcbLogYearDataEntryIndex(logNameIndex - 1);
        //    rideDataEntryForm.SetDate(DateTime.Parse(date));
        //    rideDataEntryForm.SettbWeekCountRDE(weekNumber);

        //    rideDataEntryForm.SetRoute(routeIndex);
        //    rideDataEntryForm.SetBike(bikeIndex);
        //    rideDataEntryForm.SetTime(DateTime.Parse(movingTime));
        //    rideDataEntryForm.SetDistance(decimal.Parse(rideDistance));
        //    rideDataEntryForm.SetAvgSpeed(decimal.Parse(avgSpeed));


        //    rideDataEntryForm.SetCalories(calories);

        //    rideDataEntryForm.ShowDialog();
        //}
    }
}
