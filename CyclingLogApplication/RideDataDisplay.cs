﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Windows.Threading;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;

namespace CyclingLogApplication
{
    public partial class RideDataDisplay : Form
    {
        private SqlConnection sqlConnection;
        private DatabaseConnection databaseConnection;
        int min = 0;// Minimum value for progress range
        int max = 100;// Maximum value for progress range
        int val = 0;// Current progress
        Color BarColor = Color.Blue;// Color of progress meter
        Boolean formloading = false;

        public RideDataDisplay()
        {
            formloading = true;

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

                List<string> logList = MainForm.ReadDataNamesDESC("Table_Log_year", "Name");
                int checkListBoxIndex = 0;
                
                cbLogYearFilter.Items.Add("--Select Value--");
                cbLogYearFilter.Items.Add("All Logs");
                for (int i = 0; i < logList.Count; i++)
                {
                    cbLogYearFilter.Items.Add(logList[i]);
                }

                cbLogYearFilter.SelectedIndex = MainForm.GetLastLogFilterSelected();

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

                int heightCLB = 409;

                if (numberRemoved == 1)
                {
                    heightCLB = 394;
                }
                else if (numberRemoved == 2)
                {
                    heightCLB = 379;
                }

                string gridOrder = MainForm.GetGridOrder();
                if (gridOrder.Equals("ASC"))
                {
                    rbAscendingOrder.Checked = true;
                    rbDescendingOrder.Checked = false;
                } else
                {
                    rbAscendingOrder.Checked = false;
                    rbDescendingOrder.Checked = true;
                }
                
                checkedListBox.Height = heightCLB;
                //sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to load RideDataDisplay form. " + ex.Message.ToString());
            }

            formloading = false;
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

        private async Task LoadingMessage()
        {
            tbLoadingMessage.Visible = true;
            tbLoadingMessage.Refresh();
        }

        private async Task RunGridUpdate()
        {
            await LoadingMessage();
            RunDataGrid();

            //if (!formloading)
            //{
            //    using (RefreshingForm refreshingForm = new RefreshingForm())
            //    {
            //        // Display form modelessly
            //        refreshingForm.Show();
            //        //  ALlow main UI thread to properly display please wait form.
            //        System.Windows.Forms.Application.DoEvents();
            //        //this.ShowDialog();
            //        RunDataGrid();
            //        refreshingForm.Hide();
            //    }
            //}
        }

        private void RunDataGrid()
        {
            string gridOrder;
            if (rbAscendingOrder.Checked)
            {
                gridOrder = "ASC";
            } else
            {
                gridOrder = "DESC";
            }

            if (cbLogYearFilter.SelectedIndex < 1)
            {
                MessageBox.Show("In order to view data a log must be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = null;

            try
            {
                dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                Logger.LogError("[ERROR]: Exception while trying to Clear ride data." + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred.  Review the log for more information.");
                conn?.Close();

                return;
            }

            string custom1 = MainForm.GetCustomField1();
            string custom2 = MainForm.GetCustomField2();
            bool customDataField1 = false;
            bool customDataField2 = false;
            bool commentField = false;

            bool customDataField1Checked = false;
            bool customDataField2Checked = false;
            bool windFieldChecked = false;
            bool rideDistanceChecked = false;
            bool avgSpeedChecked = false;
            bool weekNumberChecked = false;
            bool avgPowerChecked = false;
            bool maxPowerChecked = false;
            bool avgHeartRateChecked = false;
            bool maxHeartRateChecked = false;
            bool avgCadenceChecked = false;
            bool maxCadenceChecked = false;
            bool caloriesChecked = false;
            bool tempChecked = false;
            bool windChillChecked = false;
            bool maxSpeedChecked = false;

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
            Boolean checkedItem = false;

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
                    checkedItem = true;

                    fieldName = checkedListBox.Items[i].ToString();
                    if (fieldName.Equals("Week Number"))
                    {
                        fieldName = "WeekNumber";
                        weekNumberChecked = true;
                    }
                    else if (fieldName.Equals("Moving Time"))
                    {
                        fieldName = "MovingTime";
                    }
                    else if (fieldName.Equals("Ride Distance"))
                    {
                        fieldName = "RideDistance";
                        rideDistanceChecked = true;
                    }
                    else if (fieldName.Equals("Avg Speed"))
                    {
                        fieldName = "AvgSpeed";
                        avgSpeedChecked = true;
                    }
                    else if (fieldName.Equals("Ride Type"))
                    {
                        fieldName = "RideType";
                    }
                    else if (fieldName.Equals("Temperature"))
                    {
                        tempChecked = true;
                    }
                    else if (fieldName.Equals("Avg Cadence"))
                    {
                        fieldName = "AvgCadence";
                        avgCadenceChecked = true;
                    }
                    else if (fieldName.Equals("Max Cadence"))
                    {
                        fieldName = "MaxCadence";
                        maxCadenceChecked = true;
                    }
                    else if (fieldName.Equals("Avg Heart Rate"))
                    {
                        fieldName = "AvgHeartRate";
                        avgHeartRateChecked = true;
                    }
                    else if (fieldName.Equals("Max Heart Rate"))
                    {
                        fieldName = "MaxHeartRate";
                        maxHeartRateChecked = true;
                    }
                    else if (fieldName.Equals("Total Ascent"))
                    {
                        fieldName = "TotalAscent";
                    }
                    else if (fieldName.Equals("Calories"))
                    {
                        caloriesChecked = true;
                    }
                    else if (fieldName.Equals("Total Descent"))
                    {
                        fieldName = "TotalDescent";
                    }
                    else if (fieldName.Equals("Max Speed"))
                    {
                        fieldName = "MaxSpeed";
                        maxSpeedChecked = true;
                    }
                    else if (fieldName.Equals("Avg Power"))
                    {
                        fieldName = "AveragePower";
                        avgPowerChecked = true;
                    }
                    else if (fieldName.Equals("Max Power"))
                    {
                        fieldName = "MaxPower";
                        maxPowerChecked = true;
                    }
                    else if (fieldName.Equals("Wind Chill"))
                    {
                        fieldName = "Windchill";
                        windChillChecked = true;
                    }
                    else if (fieldName.Equals(custom1))
                    {
                        fieldName = "Custom1";
                    }
                    else if (fieldName.Equals(custom2))
                    {
                        fieldName = "Custom2";
                    }
                    else if (fieldName.Equals("Comments"))
                    {
                        commentField = true;
                    }
                    else if (fieldName.Equals("Max Wind"))
                    {
                        fieldName = "Wind";
                        windFieldChecked = true;
                    }

                    fieldString += ",[" + fieldName + "]";

                }
            }

            if (!checkedItem)
            {
                tbLoadingMessage.Visible = false;
                MessageBox.Show("No data items selected to view.");

                return;
            }

            try
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = null;
                int logYearID = -1;
                string logYearIDQuery = "";

                //Get the Logyear ID:
                if (cbLogYearFilter.SelectedIndex == 1)
                {
                    logYearID = 0;
                }
                else
                {
                    logYearID = MainForm.GetLogYearIndexByName(cbLogYearFilter.SelectedItem.ToString());
                    logYearIDQuery = " and [LogYearID]=@logyearID";
                }

                //WeekNumber, Bike, RideType, route
                if (cbFilterField.SelectedItem.Equals("WeekNumber"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @WeekNumber LIKE WeekNumber " + logYearIDQuery + " ORDER BY[Date] " + gridOrder, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@WeekNumber", SqlDbType.BigInt, 5);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WeekNumber", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }

                    //sqlDataAdapter = new SqlDataAdapter("select [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Comments] from Table_Ride_Information WHERE WeekNumber LIKE " + cbFilter.Text + "%", conn);
                }
                else if (cbFilterField.SelectedItem.Equals("Bike"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Bike LIKE Bike" + logYearIDQuery + " ORDER BY[Date] " + gridOrder, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Bike", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Bike", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.SelectedItem.Equals("RideType"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @RideType LIKE RideType" + logYearIDQuery + " ORDER BY[Date] " + gridOrder, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@RideType", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RideType", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.SelectedItem.Equals("Route"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Route LIKE Route" + logYearIDQuery + " ORDER BY[Date] " + gridOrder, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Route", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.SelectedItem.Equals("Location"))
                {
                    sqlDataAdapter = new SqlDataAdapter
                    {
                        SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE @Location LIKE Location" + logYearIDQuery + " ORDER BY[Date] " + gridOrder, sqlConnection)
                    };
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Location", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.SelectedItem.Equals("Longest"))
                {
                    sqlDataAdapter = new SqlDataAdapter();

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY[RideDistance] " + gridOrder, sqlConnection);
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    } else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information ORDER BY[RideDistance] " + gridOrder, sqlConnection);
                    }
                }
                else if (cbFilterField.SelectedItem.Equals("Temperature"))
                {
                    sqlDataAdapter = new SqlDataAdapter();

                    //index 0 = "Below 30":
                    //index 1 = "30-49":
                    //index 2 = "50-69":
                    //index 3 = "70-89":
                    //index 4 = "Above 90":

                    if (cbFilterValue.SelectedIndex == 0)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] > " + 0 + " and [Temperature] < " + 30 + " " + logYearIDQuery + " ORDER BY[Temperature] " + gridOrder, sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 1)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 30 + " and [Temperature] < " + 50 + " " + logYearIDQuery + " ORDER BY[Temperature] " + gridOrder, sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 2)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 50 + " and [Temperature] < " + 70 + " " + logYearIDQuery + " ORDER BY[Temperature] " + gridOrder, sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 3)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 70 + " and [Temperature] < " + 90 + " " + logYearIDQuery + " ORDER BY[Temperature] " + gridOrder, sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 4)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [Temperature] >= " + 90 + " " + logYearIDQuery + " ORDER BY[Temperature] " + gridOrder, sqlConnection);
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
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] " + gridOrder, sqlConnection);
                        //sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + ", CASE WHEN  AveragePower = 0 THEN '- -' END AS tempCol FROM Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] " + gridOrder, sqlConnection);
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                    else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT " + fieldString + " from Table_Ride_Information ORDER BY [Date] " + gridOrder, sqlConnection);
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
                if (weekNumberChecked)
                {
                    dataTable.Columns["WeekNumber"].ColumnName = "Week#";
                }

                if (windFieldChecked)
                {
                    dataTable.Columns["Wind"].ColumnName = "Max Wind";
                }

                //dataGridView1.AutoGenerateColumns = false;
                //var NameField = new DataGridViewColumn();
                //NameField.HeaderText = "AveragePower";
                //NameField.DataPropertyName = "averagePower";
                //dataGridView1.Columns.Add(NameField);

                dataGridView1.DataSource = dataTable;
                if (maxSpeedChecked)
                {
                    dataGridView1.Columns["MaxSpeed"].DefaultCellStyle.Format = "0.0";
                }
                if (avgSpeedChecked)
                {
                    dataGridView1.Columns["AvgSpeed"].DefaultCellStyle.Format = "0.0";
                }
                if (rideDistanceChecked)
                {
                    dataGridView1.Columns["RideDistance"].DefaultCellStyle.Format = "0.0";
                }
                if (windFieldChecked)
                {
                    dataGridView1.Columns["Max Wind"].DefaultCellStyle.Format = "0.0";
                }
                if (tempChecked)
                {
                    dataGridView1.Columns["Temperature"].DefaultCellStyle.Format = "0.0";
                }
                if (windChillChecked)
                {
                    dataGridView1.Columns["Windchill"].DefaultCellStyle.Format = "0.0";
                }

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

                dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

                int commentColNumber = -1;
                if (commentField)
                {
                    commentColNumber = dataGridView1.Columns["Comments"].Index;
                }

                //****************  This causes the load time to be very long  ************************
                // This is to center align all data:
                //int colCount = dataGridView1.ColumnCount;
                //for (int i = 0; i < colCount; i++)
                //{
                //    if (commentField && i == commentColNumber)
                //    {

                //    } else
                //    {
                //        dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //        dataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //    }                  
                //}

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

                //****************  This causes the load time to be very long  ************************
                //For fields that do not contain values, enter 'DBNull.Value' to know that no value was entered at post time.

                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    if (tempChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["Temperature"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["Temperature"].Value = DBNull.Value;
                //        }
                //    }
                //    if (caloriesChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["Calories"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["Calories"].Value = DBNull.Value;
                //        }
                //    }
                //    if (avgCadenceChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["AvgCadence"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["AvgCadence"].Value = DBNull.Value;
                //        }
                //    }
                //    if (maxCadenceChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["MaxCadence"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["MaxCadence"].Value = DBNull.Value;
                //        }
                //    }
                //    if (maxPowerChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["MaxPower"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["MaxPower"].Value = DBNull.Value;
                //        }
                //    }
                //    if (avgPowerChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["AveragePower"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["AveragePower"].Value = DBNull.Value;
                //        }
                //    }
                //    if (avgHeartRateChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["AvgHeartRate"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["AvgHeartRate"].Value = DBNull.Value;
                //        }
                //    }
                //    if (maxHeartRateChecked)
                //    {
                //        if (dataGridView1.Rows[i].Cells["MaxHeartRate"].Value.ToString().Equals("0"))
                //        {
                //            dataGridView1.Rows[i].Cells["MaxHeartRate"].Value = DBNull.Value;
                //        }
                //    }
                //}

                //dataGridView1.Refresh();
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query ride data: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering ride data.  Review the log for more information.");
                return;
            }
            finally
            {
                // close connection
                sqlConnection?.Close();
            }

            tbLoadingMessage.Visible = false;

            return;
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

            //Enable disable filter list:
            if (cbFilterField.SelectedIndex == 0)
            {
                cbFilterValue.Enabled = false;
            } else
            {
                cbFilterValue.Enabled = true;
            }

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
            if (dataGridView1.RowCount < 1)
            {
                MessageBox.Show("No data available to print.");

                return;
            }

            DGVPrinter printer = new DGVPrinter
            {
                Title = "Cycling Log Report",
                //printer.SubTitle = "An Easy to Use DataGridView Printing Object";
                SubTitleFormatFlags = StringFormatFlags.LineLimit |
                StringFormatFlags.NoClip,
                PageNumbers = true,
                PageNumberInHeader = false,
                PorportionalColumns = true,
                //printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
                ColumnWidth = DGVPrinter.ColumnWidthSetting.CellWidth,
                HeaderCellAlignment = StringAlignment.Near,
                //printer.Footer = "";
                FooterSpacing = 15
            };
            printer.PageNumberFormat.Alignment = StringAlignment.Center;

            MessageBox.Show("For best results set page orientation to LANDSCAPE.");

            if (DialogResult.OK == printer.DisplayPrintDialog()) {  // you may replace 


                // print without redisplaying the printdialog 
                //printer.PrintNoDisplay(dataGridView1);
                printer.PrintPreviewDataGridView(dataGridView1);
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
            //if (rowindex == 0)
            //{
            //    return;
            //}

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
                    //UpPictureBox.Enabled = false;
                    return;
                }

                //UpPictureBox.Enabled = true;
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
                    //DownPictureBox.Enabled=false;
                    return;
                }

                //DownPictureBox.Enabled = true;
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

        protected override void OnResize(EventArgs e)
        {
            // Invalidate the control to get a repaint.
            this.Invalidate();
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    SolidBrush brush = new SolidBrush(BarColor);
        //    float percent = (float)(val - min) / (float)(max - min);
        //    Rectangle rect = this.ClientRectangle;

        //    // Calculate area for drawing the progress.
        //    rect.Width = (int)((float)rect.Width * percent);

        //    // Draw the progress meter.
        //    g.FillRectangle(brush, rect);

        //    // Draw a three-dimensional border around the control.
        //    Draw3DBorder(g);

        //    // Clean up.
        //    brush.Dispose();
        //    g.Dispose();
        //}

        public int Minimum
        {
            get
            {
                return min;
            }

            set
            {
                // Prevent a negative value.
                if (value < 0)
                {
                    value = 0;
                }

                // Make sure that the minimum value is never set higher than the maximum value.
                if (value > max)
                {
                    max = value;
                }

                min = value;

                // Ensure value is still in range
                if (val < min)
                {
                    val = min;
                }

                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }

        public int Maximum
        {
            get
            {
                return max;
            }

            set
            {
                // Make sure that the maximum value is never set lower than the minimum value.
                if (value < min)
                {
                    min = value;
                }

                max = value;

                // Make sure that value is still in range.
                if (val > max)
                {
                    val = max;
                }

                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }

        public int Value
        {
            get
            {
                return val;
            }

            set
            {
                int oldValue = val;

                // Make sure that the value does not stray outside the valid range.
                if (value < min)
                {
                    val = min;
                }
                else if (value > max)
                {
                    val = max;
                }
                else
                {
                    val = value;
                }

                // Invalidate only the changed area.
                float percent;

                Rectangle newValueRect = this.ClientRectangle;
                Rectangle oldValueRect = this.ClientRectangle;

                // Use a new value to calculate the rectangle for progress.
                percent = (float)(val - min) / (float)(max - min);
                newValueRect.Width = (int)((float)newValueRect.Width * percent);

                // Use an old value to calculate the rectangle for progress.
                percent = (float)(oldValue - min) / (float)(max - min);
                oldValueRect.Width = (int)((float)oldValueRect.Width * percent);

                Rectangle updateRect = new Rectangle();

                // Find only the part of the screen that must be updated.
                if (newValueRect.Width > oldValueRect.Width)
                {
                    updateRect.X = oldValueRect.Size.Width;
                    updateRect.Width = newValueRect.Width - oldValueRect.Width;
                }
                else
                {
                    updateRect.X = newValueRect.Size.Width;
                    updateRect.Width = oldValueRect.Width - newValueRect.Width;
                }

                updateRect.Height = this.Height;

                // Invalidate the intersection region only.
                this.Invalidate(updateRect);
            }
        }

        public Color ProgressBarColor
        {
            get
            {
                return BarColor;
            }

            set
            {
                BarColor = value;

                // Invalidate the control to get a repaint.
                this.Invalidate();
            }
        }

        private void Draw3DBorder(Graphics g)
        {
            int PenWidth = (int)Pens.White.Width;

            g.DrawLine(Pens.DarkGray,
            new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
            new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top));
            g.DrawLine(Pens.DarkGray,
            new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
            new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth));
            g.DrawLine(Pens.White,
            new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth),
            new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
            g.DrawLine(Pens.White,
            new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top),
            new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth));
        }

        private void bFilter_Click_1(object sender, EventArgs e)
        {
            RunGridUpdate();

            if (rbAscendingOrder.Checked)
            {
                MainForm.SetGridOrder("ASC");
            }
            else
            {
                MainForm.SetGridOrder("DESC");
            }
        }

        private void btPrintPreview_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 1)
            {
                MessageBox.Show("No data available to print.");

                return; 
            }

            DGVPrinter printer = new DGVPrinter
            {
                Title = "Cycling Log Report",
                //printer.SubTitle = "An Easy to Use DataGridView Printing Object";
                SubTitleFormatFlags = StringFormatFlags.LineLimit |
                StringFormatFlags.NoClip,
                PageNumbers = true,
                PageNumberInHeader = false,
                PorportionalColumns = true,
                //printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
                ColumnWidth = DGVPrinter.ColumnWidthSetting.CellWidth,
                HeaderCellAlignment = StringAlignment.Near,
                //printer.Footer = "";
                FooterSpacing = 15
            };
            printer.PageNumberFormat.Alignment = StringAlignment.Center;

            //if (DialogResult.OK == printer.DisplayPrintDialog()) {  // you may replace 

            MessageBox.Show("For best results set page orientation to LANDSCAPE.");

            // print without redisplaying the printdialog 
            //printer.PrintNoDisplay(dataGridView1);
            printer.PrintPreviewDataGridView(dataGridView1);
            //}
        }
    }
}
