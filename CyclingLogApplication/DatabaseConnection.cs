using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CyclingLogApplication
{
    /// <summary>
    /// Provides methods for connecting to and interacting with a MSSQL database
    /// </summary>
    public class DatabaseConnection
            : IDisposable
    {
        #region Enums, class variables & Supporting Classes

        /// <summary>
        /// The result of a connection test
        /// </summary>
        public enum ConnectionTestResult
        {
            /// <summary>
            /// The server is reachable
            /// </summary>
            Verified,
            /// <summary>
            /// The server is unreachable
            /// </summary>
            Unreachable
        }

        /// <summary>
        /// The result of a database connection test
        /// </summary>
        public class DatabaseConnectionTestResult
        {
            /// <summary>
            /// The result of a connection test
            /// </summary>
            public ConnectionTestResult Status { get; set; }
            /// <summary>
            /// The exception that was thrown by a failed test
            /// </summary>
            [XmlIgnore]
            public Exception FailureException { get; set; }
            /// <summary>
            /// Constructor
            /// </summary>
            public DatabaseConnectionTestResult() { this.Status = ConnectionTestResult.Unreachable; }
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="Status">The result of a connection test</param>
            public DatabaseConnectionTestResult(ConnectionTestResult Status) { this.Status = Status; }
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="Status">The result of a connection test</param>
            /// <param name="FailureException">The exception that was thrown by a failed test</param>
            public DatabaseConnectionTestResult(ConnectionTestResult Status, Exception FailureException)
            {
                this.Status = Status;
                this.FailureException = FailureException;
            }
        }

        /// <summary>
        /// A read-only view of the connection to the database
        /// </summary>
        private SqlConnection Connection { get; set; }
        private SqlDataReader dataReader = null;
        private readonly string ConnectionString = "";

        #endregion

        #region Constructors
        /// <summary>
        /// Instatiates an instance of MSSQL
        /// </summary>
        /// <param name="ConnectionString">The connection string to be used when connecting to the MS SQL server</param>
        public DatabaseConnection(string ConnectionString)
            : base()
        {
            this.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DatabaseConnection()
        {
            CloseDataReader();
            CloseConnection();
            //Dispose();
        }
        #endregion

        #region Private Methods
        private void OpenConnection(string ConnectionString)
        {
            if (!this.IsConnectionOpen())
            {
                this.Connection = new SqlConnection(this.ConnectionString);

                this.Connection.Open();
            }
        }

        private bool IsConnectionOpen()
        {
            if (this.Connection != null)
            {
                if ((this.Connection.State != ConnectionState.Broken) || (this.Connection.State != ConnectionState.Closed))
                {
                    return true;
                }
                
            } else
            {
                return false;
            }

            //try
            //{

            //    return ((this.Connection.State != ConnectionState.Broken) || (this.Connection.State != ConnectionState.Closed));
            //}
            //catch
            //{
            //    return false;
            //}
            return false;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Closes the connection to the MS SQL server
        /// </summary>
        public void CloseConnection()
        {
            try { 
                CloseDataReader(); 
            }
            catch { }

            try {
                string databaseString = this.Connection.Database.ToString();
                if (databaseString.Equals("")) {
                    string test = "";
                } else
                {
                    if (!object.ReferenceEquals(this.Connection, null) || Connection != null && Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
                
            }
            catch { }

            try { 
                SqlConnection.ClearPool(Connection); 
            }
            catch { }
        }

        /// <summary>
        /// Closes the data reader to the MS SQL server
        /// </summary>
        public void CloseDataReader()
        {
            if (this.dataReader != null)
            {
                try { this.dataReader.Close(); }
                catch { }
            }
        }

        /// <summary>
        /// Disposes the memory used by objects in the class
        /// </summary>
        public void Dispose()
        {
            this.CloseConnection();

            try { this.dataReader.Dispose(); }
            catch { }
            try { this.Connection.Dispose(); }
            catch { }
        }

        /// <summary>
        /// Executes a command that does not retrieve data
        /// </summary>
        /// <param name="ProcedureName">The name of the procedure to call</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <returns>The number of affected rows</returns>
        public int ExecuteSimpleNonQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";
            for (int i = 0; i < _Parameters.Count; i++)
                tmpProcedureName += "@" + i.ToString() + ",";
            tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
            return this.ExecuteNonQueryConnection(tmpProcedureName, _Parameters);
        }

        /// <summary>
        /// Executes a command that does not retrieve data
        /// </summary>
        /// <param name="CommandString">A properly formed command string</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <returns>The number of affected rows</returns>
        /// <remarks>A properly formed CommandString will take the following form: [Command][space][Variables represented in ascending order in the form of @# (@0,@1,@2,...) and delimited by commas]</remarks>
        public int ExecuteNonQueryConnection(string CommandString, List<object> _Parameters)
        {
            this.OpenConnection(this.ConnectionString);

            SqlCommand cmd = new SqlCommand(CommandString, this.Connection);
            cmd.Prepare();
            int count = 0;
            foreach (object _o in _Parameters)
            {
                cmd.Parameters.Add(new SqlParameter("@" + count.ToString(), _o == null ? DBNull.Value : _o) { Value = _o == null ? DBNull.Value : _o });
                count++;
            }
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes a command that retrieves data
        /// </summary>
        /// <param name="ProcedureName">The name of the procedure to call</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <returns>A reader that provides access to the data returned from the query</returns>
        public SqlDataReader ExecuteSimpleQueryConnection(string ProcedureName, List<object> _Parameters)
        {
            string tmpProcedureName = "EXECUTE " + ProcedureName + " ";
            for (int i = 0; i < _Parameters.Count; i++)
                tmpProcedureName += "@" + i.ToString() + ",";
            tmpProcedureName = tmpProcedureName.TrimEnd(',') + ";";
            SqlDataReader ToReturn = ExecuteQueryConnection(tmpProcedureName, _Parameters);

            return ToReturn;
        }

        /// <summary>
        /// Executes a command that retrieves data
        /// </summary>
        /// <param name="CommandString">A properly formed command string</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <remarks>A properly formed CommandString will take the following form: [Command][space][Variables represented in ascending order in the form of @# (@0,@1,@2,...) and delimited by commas]</remarks>
        /// <returns>A reader that provides access to the data returned from the query</returns>
        public SqlDataReader ExecuteQueryConnection(string CommandString, List<object> _Parameters)
        {
            this.OpenConnection(this.ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(CommandString, Connection);
            using (SqlCommand cmd = sqlCommand)
            {
                cmd.CommandTimeout = 400;
                if (_Parameters != null)
                {
                    cmd.Prepare();
                    int count = 0;
                    foreach (object _o in _Parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + count.ToString(), _o == null ? DBNull.Value : _o) { Value = _o == null ? DBNull.Value : _o });
                        count++;
                    }
                }

                //Exception StoredException = null;
                //bool Executed = false;
                //int ConnectionAttempts = 0;
                //do
                //{
                //    try 
                //    {
                this.dataReader = cmd.ExecuteReader();
            }
            //        Executed = true;
            //    }
            //    catch (Exception ex)
            //    {
            //        StoredException = ex;
            //        System.Threading.Thread.Sleep(Properties.Settings.Default.QueryRetryDelayInMilliseconds);
            //    }
            //} while (++ConnectionAttempts < Properties.Settings.Default.QueryRetryCount && !Executed);

            //if (ConnectionAttempts >= Properties.Settings.Default.QueryRetryCount && !Executed)
            //    throw StoredException;

            return this.dataReader;
        }

        /// <summary>
        /// Executes a command that retrieves a scalar object
        /// </summary>
        /// <param name="FunctionName">The name of the function to call</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <returns>An object of the data returned from the function</returns>
        public object ExecuteSimpleScalarFunction(string FunctionName, List<object> _Parameters)
        {
            string tmpFunctionName = "SELECT dbo." + FunctionName + "(";
            for (int i = 0; i < _Parameters.Count; i++)
                tmpFunctionName += "@" + i.ToString() + ",";
            tmpFunctionName = tmpFunctionName.TrimEnd(',') + ");";
            return this.ExecuteScalarFunction(tmpFunctionName, _Parameters);
        }

        /// <summary>
        /// Executes a command that retrieves a scalar object
        /// </summary>
        /// <param name="CommandString">The name of the function to call</param>
        /// <param name="_Parameters">The parameters to use as arguments for the procedure</param>
        /// <remarks>A properly formed CommandString will take the following form: [Command][(][Variables represented in ascending order in the form of @# (@0,@1,@2,...) and delimited by commas][)]</remarks>
        /// <returns>An object of the data returned from the function</returns>
        public object ExecuteScalarFunction(string CommandString, List<object> _Parameters)
        {
            this.OpenConnection(this.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(CommandString, this.Connection))
            {
                if (_Parameters != null)
                {
                    cmd.Prepare();
                    int count = 0;
                    foreach (object _o in _Parameters)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + count.ToString(), _o == null ? DBNull.Value : _o) { Value = _o == null ? DBNull.Value : _o });
                        count++;
                    }
                }
                return cmd.ExecuteScalar();
            }
        }

        //public object ExecuteScalarFunctionTest(string CommandString, List<object> _Parameters)
        //{
        //   this.OpenConnection(this.ConnectionString);

        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        cmd.CommandText = CommandString;
        //        cmd.Parameters.Add("@Id", SqlDbType.Bit);
        //        cmd.Parameters["@Id"].Value = 3;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = new SqlConnection(this.ConnectionString);
        //        cmd.Connection.Open();

        //        return cmd.ExecuteScalar();
        //    }
        //}

        /// <summary>
        /// Tests if a connection is open and valid
        /// </summary>
        /// <returns>A DatabaseConnectionTestResult object that contains the result of the test as well as any exceptions that may have been thrown</returns>
        public DatabaseConnectionTestResult TestConnection()
        {
            try
            {
                CloseConnection();
                OpenConnection(this.ConnectionString);
                return new DatabaseConnectionTestResult(ConnectionTestResult.Verified);
            }
            catch (Exception ex) { return new DatabaseConnectionTestResult(ConnectionTestResult.Unreachable, ex); }
            finally
            {
                try { this.CloseConnection(); }
                catch { }
            }
        }

        /// <summary>
        /// Determine if a column exists in SQL return
        /// </summary>
        /// <param name="reader">SqlDataReader returned</param>
        /// <param name="ColumnName">Name to search for</param>
        /// <returns>True if found, else false</returns>
        public static bool CheckColumnExists(SqlDataReader reader, string ColumnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(ColumnName))
                    return true;

            return false;
        }
        #endregion
    }
}

