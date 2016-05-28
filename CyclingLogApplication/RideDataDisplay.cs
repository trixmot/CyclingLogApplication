using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@WeekNumber", tbFilterText.Text);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);

                    //sqlDataAdapter = new SqlDataAdapter("select [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Comments] from Table_Ride_Information WHERE WeekNumber LIKE " + cbFilter.Text + "%", conn);
                }
                else if (cbFilterField.Text.Equals("Bike"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Bike LIKE Bike" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Bike", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Bike", tbFilterText.Text);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                }
                else if (cbFilterField.Text.Equals("RideType"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @RideType LIKE RideType" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@RideType", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@RideType", tbFilterText.Text);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                }
                else if (cbFilterField.Text.Equals("Route"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Route LIKE Route" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Route", tbFilterText.Text);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                }
                else if (cbFilterField.Text.Equals("Location"))
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE @Location LIKE Location" + logYearIDQuery, sqlConnection);
                    //sqlDataAdapter.SelectCommand.Parameters.Add("@Route", SqlDbType.NVarChar, 50);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Location", tbFilterText.Text);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                }
                else
                {
                    sqlDataAdapter = new SqlDataAdapter();
                    if (logYearID == 0)
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information ORDER BY [Date] ASC", sqlConnection);
                    } else
                    {
                        sqlDataAdapter.SelectCommand = new SqlCommand("SELECT [WeekNumber],[Date],[MovingTime],[RideDistance],[AvgSpeed],[Bike],[RideType],[Wind],[Temperature],[AvgCadence],[AvgHeartRate],[MaxHeartRate],[Calories],[TotalAscent],[TotalDescent],[Route],[Location],[Comments] from Table_Ride_Information WHERE [LogYearID]=@logyearID ORDER BY [Date] ASC", sqlConnection);
                    }
                    
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@logyearID", logYearID);
                }

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["AvgSpeed"].DefaultCellStyle.Format = "0.00";
                dataGridView1.Columns["RideDistance"].DefaultCellStyle.Format = "0.0";
            }
            catch (Exception ex)
            {
                
                Logger.LogError("[ERROR]: Exception while trying to Clear ride data." + ex.Message.ToString());
                MessageBox.Show("An exception error has occurred.  Review the log for more information.");
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
            tbFilterText.Text = "";
            cbFilterField.SelectedIndex = 0;

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
    }
}
