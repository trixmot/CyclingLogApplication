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

namespace CyclingLogApplication
{
    public partial class RideDataDisplay : Form
    {
        static int logYearFilterIndex = -1;
        private SqlConnection sqlConnection;// = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=""\\Mac\Home\Documents\Visual Studio 2015\Projects\CyclingLogApplication\CyclingLogApplication\CyclingLogDatabase.mdf"";Integrated Security=True");


        public RideDataDisplay(MainForm mainForm)
        {
            InitializeComponent();
            cbFilterField.SelectedIndex = 0;
            //MainForm mainForm = new MainForm("");
            sqlConnection = mainForm.GetsqlConnectionString();
        }

        private void CloseForm(object sender, EventArgs e)
        {
            cbFilterField.SelectedIndex = 0;
            cbFilterValue.Items.Clear();
            cbFilterValue.Items.Add("");
            cbFilterValue.SelectedIndex = 0;
            dataGridView1.DataSource = null;

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

        public void setLogYearFilterIndex(int index)
        {
            cbLogYearFilter.SelectedIndex = index;
        }

        public int getLogYearFilterIndex()
        {
            return logYearFilterIndex;
        }

        public void setCheckedValues()
        {
            MainForm mainForm = new MainForm();

            // Set CheckedListBox values:
            if (mainForm.GetCheckedListBoxItem0().Equals("1"))
            {
                checkedListBox.SetItemCheckState(0, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(0, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem1().Equals("1"))
            {
                checkedListBox.SetItemCheckState(1, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(1, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem2().Equals("1"))
            {
                checkedListBox.SetItemCheckState(2, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(2, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem3().Equals("1"))
            {
                checkedListBox.SetItemCheckState(3, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(3, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem4().Equals("1"))
            {
                checkedListBox.SetItemCheckState(4, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(4, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem5().Equals("1"))
            {
                checkedListBox.SetItemCheckState(5, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(5, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem6().Equals("1"))
            {
                checkedListBox.SetItemCheckState(6, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(6, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem7().Equals("1"))
            {
                checkedListBox.SetItemCheckState(7, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(7, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem8().Equals("1"))
            {
                checkedListBox.SetItemCheckState(8, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(8, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem9().Equals("1"))
            {
                checkedListBox.SetItemCheckState(9, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(9, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem10().Equals("1"))
            {
                checkedListBox.SetItemCheckState(10, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(10, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem11().Equals("1"))
            {
                checkedListBox.SetItemCheckState(11, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(11, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem12().Equals("1"))
            {
                checkedListBox.SetItemCheckState(12, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(12, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem13().Equals("1"))
            {
                checkedListBox.SetItemCheckState(13, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(13, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem14().Equals("1"))
            {
                checkedListBox.SetItemCheckState(14, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(14, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem15().Equals("1"))
            {
                checkedListBox.SetItemCheckState(15, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(15, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem16().Equals("1"))
            {
                checkedListBox.SetItemCheckState(16, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(16, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem17().Equals("1"))
            {
                checkedListBox.SetItemCheckState(17, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(17, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem18().Equals("1"))
            {
                checkedListBox.SetItemCheckState(18, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(18, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem19().Equals("1"))
            {
                checkedListBox.SetItemCheckState(19, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(19, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem20().Equals("1"))
            {
                checkedListBox.SetItemCheckState(20, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(20, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem21().Equals("1"))
            {
                checkedListBox.SetItemCheckState(21, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(21, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem22().Equals("1"))
            {
                checkedListBox.SetItemCheckState(22, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(22, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem23().Equals("1"))
            {
                checkedListBox.SetItemCheckState(23, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(23, CheckState.Unchecked);
            }

            if (mainForm.GetCheckedListBoxItem24().Equals("1"))
            {
                checkedListBox.SetItemCheckState(24, CheckState.Checked);
            }
            else
            {
                checkedListBox.SetItemCheckState(24, CheckState.Unchecked);
            }
        }

        private void bFilter_Click(object sender, EventArgs e)
        {
            string fieldString = "";
            if (checkedListBox.GetItemChecked(0))
            {
                fieldString = fieldString + "[WeekNumber]";
            }

            if (checkedListBox.GetItemChecked(1))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Id]";
                }
                else
                {
                    fieldString = fieldString + ",[Id]";
                }
            }

            if (checkedListBox.GetItemChecked(2))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Date]";
                }
                else
                {
                    fieldString = fieldString + ",[Date]";
                }
            }

            if (checkedListBox.GetItemChecked(3))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[MovingTime]";
                }
                else
                {
                    fieldString = fieldString + ",[MovingTime]";
                }
            }

            if (checkedListBox.GetItemChecked(4))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[RideDistance]";
                }
                else
                {
                    fieldString = fieldString + ",[RideDistance]";
                }
            }

            if (checkedListBox.GetItemChecked(5))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[AvgSpeed]";
                }
                else
                {
                    fieldString = fieldString + ",[AvgSpeed]";
                }
            }

            if (checkedListBox.GetItemChecked(6))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Bike]";
                }
                else
                {
                    fieldString = fieldString + ",[Bike]";
                }
            }

            if (checkedListBox.GetItemChecked(7))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[RideType]";
                }
                else
                {
                    fieldString = fieldString + ",[RideType]";
                }
            }

            if (checkedListBox.GetItemChecked(8))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Wind]";
                }
                else
                {
                    fieldString = fieldString + ",[Wind]";
                }
            }

            if (checkedListBox.GetItemChecked(9))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Temperature]";
                }
                else
                {
                    fieldString = fieldString + ",[Temperature]";
                }
            }

            if (checkedListBox.GetItemChecked(10))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[AvgCadence]";
                }
                else
                {
                    fieldString = fieldString + ",[AvgCadence]";
                }
            }

            if (checkedListBox.GetItemChecked(11))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[AvgHeartRate]";
                }
                else
                {
                    fieldString = fieldString + ",[AvgHeartRate]";
                }
            }

            if (checkedListBox.GetItemChecked(12))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[MaxHeartRate]";
                }
                else
                {
                    fieldString = fieldString + ",[MaxHeartRate]";
                }
            }

            if (checkedListBox.GetItemChecked(13))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Calories]";
                }
                else
                {
                    fieldString = fieldString + ",[Calories]";
                }
            }

            if (checkedListBox.GetItemChecked(14))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[TotalAscent]";
                }
                else
                {
                    fieldString = fieldString + ",[TotalAscent]";
                }
            }

            if (checkedListBox.GetItemChecked(15))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[TotalDescent]";
                }
                else
                {
                    fieldString = fieldString + ",[TotalDescent]";
                }
            }

            if (checkedListBox.GetItemChecked(16))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Route]";
                }
                else
                {
                    fieldString = fieldString + ",[Route]";
                }
            }

            if (checkedListBox.GetItemChecked(17))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Location]";
                }
                else
                {
                    fieldString = fieldString + ",[Location]";
                }
            }

            if (checkedListBox.GetItemChecked(18))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Comments]";
                }
                else
                {
                    fieldString = fieldString + ",[Comments]";
                }
            }

            if (checkedListBox.GetItemChecked(19))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Effort]";
                }
                else
                {
                    fieldString = fieldString + ",[Effort]";
                }
            }

            if (checkedListBox.GetItemChecked(20))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[MaxSpeed]";
                }
                else
                {
                    fieldString = fieldString + ",[MaxSpeedId]";
                }
            }

            if (checkedListBox.GetItemChecked(21))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[AveragePower]";
                }
                else
                {
                    fieldString = fieldString + ",[AveragePower]";
                }
            }

            if (checkedListBox.GetItemChecked(22))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[MaxPower]";
                }
                else
                {
                    fieldString = fieldString + ",[MaxPower]";
                }
            }

            if (checkedListBox.GetItemChecked(23))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Comfort]";
                }
                else
                {
                    fieldString = fieldString + ",[Comfort]";
                }
            }

            if (checkedListBox.GetItemChecked(24))
            {
                if (fieldString.Equals(""))
                {
                    fieldString = fieldString + "[Custom]";
                }
                else
                {
                    fieldString = fieldString + ",[Custom]";
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
                    MainForm mainForm = new MainForm();
                    //logYearID = mainForm.GetLogYearIndex(cbLogYearFilter.SelectedItem.ToString());

                    logYearID = mainForm.GetLogYearIndexByName(cbLogYearFilter.SelectedItem.ToString());

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
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["AvgSpeed"].DefaultCellStyle.Format = "0.00";
                dataGridView1.Columns["RideDistance"].DefaultCellStyle.Format = "0.0";

                // Resize the master DataGridView columns to fit the newly loaded data.
                dataGridView1.AutoResizeColumns();
                dataGridView1.AllowUserToOrderColumns = true;
                // Configure the details DataGridView so that its columns automatically adjust their widths when the data changes.
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {

                Logger.LogError("[ERROR]: Exception while trying to run query ride data: " + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred while quering ride data.  Review the log for more information.");
            }
            finally
            {
                // close connection
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void btClear_Click(object sender, EventArgs e)
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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        private void cbLogYearFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm("");
            mainForm.SetLastLogFilterSelected(cbLogYearFilter.SelectedIndex);
        }

        private void cbFilterFieldChanged(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm("");
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
                List<string> bikeList = new List<string>();
                bikeList = mainForm.ReadDataNames("Table_Bikes", "Name");

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
                List<string> routeList = mainForm.ReadDataNames("Table_Routes", "Name");
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

        private void btUpdateFields_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();

            if (checkedListBox.GetItemChecked(0))
            {
                mainForm.SetCheckedListBoxItem0("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem0("0");
            }

            if (checkedListBox.GetItemChecked(1))
            {
                mainForm.SetCheckedListBoxItem1("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem1("0");
            }

            if (checkedListBox.GetItemChecked(2))
            {
                mainForm.SetCheckedListBoxItem2("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem2("0");
            }

            if (checkedListBox.GetItemChecked(3))
            {
                mainForm.SetCheckedListBoxItem3("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem3("0");
            }

            if (checkedListBox.GetItemChecked(4))
            {
                mainForm.SetCheckedListBoxItem4("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem4("0");
            }

            if (checkedListBox.GetItemChecked(5))
            {
                mainForm.SetCheckedListBoxItem5("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem5("0");
            }

            if (checkedListBox.GetItemChecked(6))
            {
                mainForm.SetCheckedListBoxItem6("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem6("0");
            }

            if (checkedListBox.GetItemChecked(7))
            {
                mainForm.SetCheckedListBoxItem7("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem7("0");
            }

            if (checkedListBox.GetItemChecked(8))
            {
                mainForm.SetCheckedListBoxItem8("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem8("0");
            }

            if (checkedListBox.GetItemChecked(9))
            {
                mainForm.SetCheckedListBoxItem9("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem9("0");
            }

            if (checkedListBox.GetItemChecked(10))
            {
                mainForm.SetCheckedListBoxItem10("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem10("0");
            }

            if (checkedListBox.GetItemChecked(11))
            {
                mainForm.SetCheckedListBoxItem11("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem11("0");
            }

            if (checkedListBox.GetItemChecked(12))
            {
                mainForm.SetCheckedListBoxItem12("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem12("0");
            }

            if (checkedListBox.GetItemChecked(13))
            {
                mainForm.SetCheckedListBoxItem13("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem13("0");
            }

            if (checkedListBox.GetItemChecked(14))
            {
                mainForm.SetCheckedListBoxItem14("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem14("0");
            }

            if (checkedListBox.GetItemChecked(15))
            {
                mainForm.SetCheckedListBoxItem15("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem15("0");
            }

            if (checkedListBox.GetItemChecked(16))
            {
                mainForm.SetCheckedListBoxItem16("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem16("0");
            }

            if (checkedListBox.GetItemChecked(17))
            {
                mainForm.SetCheckedListBoxItem17("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem17("0");
            }

            if (checkedListBox.GetItemChecked(18))
            {
                mainForm.SetCheckedListBoxItem18("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem18("0");
            }

            if (checkedListBox.GetItemChecked(19))
            {
                mainForm.SetCheckedListBoxItem19("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem19("0");
            }

            if (checkedListBox.GetItemChecked(20))
            {
                mainForm.SetCheckedListBoxItem20("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem20("0");
            }

            if (checkedListBox.GetItemChecked(21))
            {
                mainForm.SetCheckedListBoxItem21("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem21("0");
            }

            if (checkedListBox.GetItemChecked(22))
            {
                mainForm.SetCheckedListBoxItem22("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem22("0");
            }

            if (checkedListBox.GetItemChecked(23))
            {
                mainForm.SetCheckedListBoxItem23("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem23("0");
            }

            if (checkedListBox.GetItemChecked(24))
            {
                mainForm.SetCheckedListBoxItem24("1");
            }
            else
            {
                mainForm.SetCheckedListBoxItem24("0");
            }

            MessageBox.Show("The Display field options have been updated.");
        }

        private void btSelectAll_Click(object sender, EventArgs e)
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
        }

        private void btDeselectAll_Click(object sender, EventArgs e)
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
        }
    }
}
