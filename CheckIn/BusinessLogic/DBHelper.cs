using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CheckIn
{
    internal class DBHelper
    {
        //private static string connectionString = @"Data Source=localhost\SQL2008R2;Initial Catalog=TestDB;Integrated Security=SSPI;Connection Timeout=120";
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        internal static SqlConnection DB_CONNECTION = new SqlConnection(connectionString);

        internal static SqlCommand GetSelectCommand(string sqlQuery)
        {
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = sqlQuery;
                command.Connection = DB_CONNECTION;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return command;
        }

        internal static object GetScalarValue(string sqlQuery)
        {
            object scalarValue = null;
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = sqlQuery;
                command.Connection = DB_CONNECTION;
                DB_CONNECTION.Open();
                scalarValue = command.ExecuteScalar();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (DB_CONNECTION.State != System.Data.ConnectionState.Closed)
                {
                    DB_CONNECTION.Close();
                }
            }
            return scalarValue;
        }

        internal static DataSet GetSelectDataSet(string sqlQuery)
        {
            DataSet result = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = sqlQuery;
                command.Connection = DB_CONNECTION;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(result);
                adapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (DB_CONNECTION.State != System.Data.ConnectionState.Closed)
                {
                    DB_CONNECTION.Close();
                }
            }
            return result;
        }

        internal static int ExecuteNonQuery(string sqlQuery)
        {
            int totalRowsAffected = 0;
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = DB_CONNECTION;
                command.CommandText = sqlQuery;
                DB_CONNECTION.Open();
                totalRowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                if (DB_CONNECTION.State != System.Data.ConnectionState.Closed)
                {
                    DB_CONNECTION.Close();
                }
            }
            return totalRowsAffected;
        }

        internal static int ExecuteNonQueryCommand(SqlCommand command)
        {
            int totalRowsAffected = 0;
            try
            {
                command.Connection = DB_CONNECTION;
                DB_CONNECTION.Open();
                totalRowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                if (DB_CONNECTION.State != System.Data.ConnectionState.Closed)
                {
                    DB_CONNECTION.Close();
                }
            }
            return totalRowsAffected;
        }

        internal static DataSet ExecuteStoredProcedure(string procedureName)
        {
            DataSet result = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = DB_CONNECTION;
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(result);
                adapter.Dispose();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            finally
            {
                if (DB_CONNECTION.State != System.Data.ConnectionState.Closed)
                {
                    DB_CONNECTION.Close();
                }
            }
            return result;
        }

        public static DataSet ExecuteStoredProcedure(string procedureName, params object[] parameters)
        {
            DataSet result = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = DB_CONNECTION;
                foreach (SqlParameter currentParameter in parameters)
                {
                    command.Parameters.AddWithValue(currentParameter.ParameterName, currentParameter.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                 da.Fill(result);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        internal static void SetConnectionString(string connectionString)
        {
            
        }
    }
}