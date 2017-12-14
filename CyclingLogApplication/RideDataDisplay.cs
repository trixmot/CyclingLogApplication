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
            sqlConnection = mainForm.getsqlConnectionString();
        }

        private void CloseForm(object sender, EventArgs e)
        {
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

        private void bFilter_Click(object sender, EventArgs e)
        {
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
                } else
                {
                    MainForm mainForm = new MainForm();
                    logYearID = mainForm.getLogYearIndex(cbLogYearFilter.SelectedItem.ToString());
                    logYearIDQuery = " and [LogYearID]=@logyearID";
                }

                //WeekNumber, Bike, RideType, route
                if (cbFilterField.Text.Equals("WeekNumber"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @WeekNumber LIKE WeekNumber" + logYearIDQuery, sqlConnection);
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
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Bike LIKE Bike" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Bike", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Bike", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("RideType"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @RideType LIKE RideType" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@RideType", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RideType", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("Route"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Route LIKE Route" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Route", cbFilterValue.Text);

                    if (logYearID != 0)
                    {
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                    }
                }
                else if (cbFilterField.Text.Equals("Location"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Location LIKE Location" + logYearIDQuery, sqlConnection);
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
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY[RideDistance] DESC", sqlConnection);
                    } else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information ORDER BY[RideDistance] DESC", sqlConnection);
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
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] < " + 30 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 1)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] >= " + 30 + " and [Temperature] < " + 50 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 2)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] >= " + 50 + " and [Temperature] < " + 70 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 3)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] >= " + 70 + " and [Temperature] < " + 90 + " " + logYearIDQuery + " ORDER BY[Temperature] ASC", sqlConnection);
                    }
                    else if (cbFilterValue.SelectedIndex == 4)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [Temperature] >= " + 90 + " " + logYearIDQuery, sqlConnection);
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
                        sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] ASC", sqlConnection);            
                    } else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information ORDER BY [Date] ASC", sqlConnection);
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
                
                Logger.LogError("[ERROR]: Exception while trying to run query ride data." + ex.Message.ToString());
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
            mainForm.setLastLogFilterSelected(cbLogYearFilter.SelectedIndex);
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
                bikeList = mainForm.readDataNames("Table_Bikes", "Name");

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
                List<string> routeList = mainForm.readDataNames("Table_Routes", "Name");
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
    }
}
