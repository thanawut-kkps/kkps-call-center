using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ApplicationBlocks = Microsoft.ApplicationBlocks.Data;
using Phatra.Core.Exceptions;
using Phatra.Core.Logging;

namespace Phatra.Core.Utilities
{
    public class SqlHelper
    {

        static ILog log = new Log4NetLogger(typeof(SqlHelper));

        private string _strConnection = string.Empty;

        public SqlHelper(string connectionString)
        {
            _strConnection = connectionString;
        }

        static SqlHelper()
        {
        }

        private static SqlHelper _sqlHelper;

        public static SqlHelper Instance
        {
            get
            {
                if (_sqlHelper == null)
                {
                    var strConn = ConfigurationHelper.GetAppSettingValue("DATABASE");

                    if (string.IsNullOrWhiteSpace(strConn))
                    {
                        throw new ConfigurationErrorsException("AppSettings name 'DATABASE' was not configured in web.config");
                    }
                    _sqlHelper = new SqlHelper(strConn);
                }

                return _sqlHelper;
            }

        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_strConnection);
        }

        /// <summary>
        ///  Execute the specific storeprocedure
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(SqlTransaction transaction, string storeProcedureName, params object[] parameterValues)
        {
            DataSet ds = null;

            ds = ApplicationBlocks.SqlHelper.ExecuteDataset(transaction, storeProcedureName, parameterValues);

            return ds;
        }

        /// <summary>
        ///  Execute the specific storeprocedure
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string storeProcedureName, params object[] parameterValues)
        {
            DataSet ds = null;
            using (SqlConnection conn = CreateConnection())
            {
                ds = ApplicationBlocks.SqlHelper.ExecuteDataset(conn, storeProcedureName, parameterValues);
            }
            return ds;
        }

        /// <summary>
        ///  Execute the specific storeprocedure and return to DataTable type.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string storeProcedureName, params object[] parameterValues)
        {
            DataSet ds = null;
            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    ds = ApplicationBlocks.SqlHelper.ExecuteDataset(conn, storeProcedureName, parameterValues);
                }
            }
            catch (SqlException sqlEx)
            {
                LogObjectParameter(parameterValues);
                HandleSqlException(sqlEx, storeProcedureName);
            }

            return ds.Tables[0];
        }

        /// <summary>
        ///  Execute the specific storeprocedure and return to DataTable type.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(SqlTransaction transaction, string storeProcedureName, params object[] parameterValues)
        {
            DataSet ds = null;
            ds = ApplicationBlocks.SqlHelper.ExecuteDataset(transaction, storeProcedureName, parameterValues);
            return ds.Tables[0];
        }

        /// <summary>
        ///  Execute the specific storeprocedure and return to DataTable type.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(SqlConnection connection, string storeProcedureName, params object[] parameterValues)
        {
            DataSet ds = null;
            ds = ApplicationBlocks.SqlHelper.ExecuteDataset(connection, storeProcedureName, parameterValues);
            return ds.Tables[0];
        }

        /// <summary>
        /// Execute non query the specific storeprocedure
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string storeProcedureName, params object[] parameterValues)
        {
            int i = -1;
            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    i = ApplicationBlocks.SqlHelper.ExecuteNonQuery(conn, storeProcedureName, parameterValues);
                }
            }
            catch (SqlException sqlEx)
            {
                LogObjectParameter(parameterValues);
                HandleSqlException(sqlEx, storeProcedureName);
            }
            return i;
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            int i = -1;
            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    i = ApplicationBlocks.SqlHelper.ExecuteNonQuery(conn, commandType, commandText, commandParameters);
                }
            }
            catch (SqlException sqlEx)
            {
                LogSqlParameter(commandParameters);
                HandleSqlException(sqlEx, commandText);
            }
            return i;
        }

        /// <summary>
        /// Execute non query the specific storeprocedure
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(SqlTransaction transaction, string storeProcedureName, params object[] parameterValues)
        {
            int i = -1;
            try
            {
                i = ApplicationBlocks.SqlHelper.ExecuteNonQuery(transaction, storeProcedureName, parameterValues);

            }
            catch (SqlException sqlEx)
            {
                LogObjectParameter(parameterValues);
                HandleSqlException(sqlEx, storeProcedureName);
            }
            return i;
        }

        public void ExecuteSqlcommand(string sqlCommand)
        {
            ExecuteSqlcommand(null, sqlCommand);
        }

        public void ExecuteSqlcommand(SqlTransaction transaction, string sqlCommand)
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            if (transaction != null)
            {
                cmd = new System.Data.SqlClient.SqlCommand(sqlCommand, transaction.Connection, transaction);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            else
            {
                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    cmd = new System.Data.SqlClient.SqlCommand(sqlCommand, conn);
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns>First column of the first row</returns>
        public object ExecuteScalar(string storeProcedureName, params object[] parameterValues)
        {
            object returnValue = null;
            try
            {
                using (SqlConnection conn = CreateConnection())
                {
                    returnValue = ApplicationBlocks.SqlHelper.ExecuteScalar(conn, storeProcedureName, parameterValues);
                }

            }
            catch (SqlException sqlEx)
            {
                LogObjectParameter(parameterValues);
                HandleSqlException(sqlEx, storeProcedureName);
            }

            return returnValue;
        }

        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object returnValue = null;
            try
            {
                //bool hasDataTableParameter = false;
                //foreach (var param in commandParameters)
                //{
                //    if (param.Value is DataTable)
                //    {
                //        hasDataTableParameter = true;
                //    }
                //}

                //if (hasDataTableParameter)
                //{
                //    returnValue = ExecuteScalarWithDataTableParameter(commandType, commandText, commandParameters);
                //}
                //else
                //{
                using (SqlConnection conn = CreateConnection())
                {
                    returnValue = ApplicationBlocks.SqlHelper.ExecuteScalar(conn, commandType, commandText, commandParameters);
                }
                //}
            }
            catch (SqlException sqlEx)
            {
                LogSqlParameter(commandParameters);
                HandleSqlException(sqlEx, commandText);
            }

            return returnValue;
        }

        private object ExecuteScalarWithDataTableParameter(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object returnValue = null;
            using (SqlConnection conn = CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(commandText))
                {
                    cmd.CommandType = commandType;
                    cmd.Connection = conn;
                    foreach (SqlParameter param in commandParameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns>First column of the first row</returns>
        public object ExecuteScalar(SqlTransaction transaction, string storeProcedureName, params object[] parameterValues)
        {
            object returnValue = null;
            try
            {
                returnValue = ApplicationBlocks.SqlHelper.ExecuteScalar(transaction, storeProcedureName, parameterValues);
            }
            catch (SqlException sqlEx)
            {
                LogObjectParameter(parameterValues);
                HandleSqlException(sqlEx, storeProcedureName);
            }

            return returnValue;
        }

        #region Helper (Should not use if it is posible

        public static string _s(string s, bool isint, bool isSetNullIfEmpty)
        {
            if (s == null || s.Trim().Length == 0) return isint ? "null" : (isSetNullIfEmpty ? "null" : "''");
            return (isint ? s.Trim().Replace(",", "") : "'" + s.Trim().Replace("'", "''") + "'");
        }

        public static string _s(string s, bool isint)
        {
            return _s(s, isint, true);
        }

        public static string _s(string s)
        {
            return _s(s, false, true);
        }

        #endregion

        private void LogObjectParameter(params object[] parameterValues)
        {
            StringBuilder buiderString = new StringBuilder();
            if (parameterValues != null)
            {
                int count = 0;
                foreach (object obj in parameterValues)
                {

                    if (count != 0)
                    {
                        buiderString.Append(", ");
                    }
                    if (obj == null)
                    {
                        buiderString.AppendFormat("null");
                    }
                    else if (obj is string)
                    {
                        buiderString.AppendFormat("'{0}'", obj);
                    }
                    else if (obj is DateTime)
                    {
                        buiderString.AppendFormat("'{0:yyyy-MM-dd}'", obj);
                    }
                    else
                    {
                        buiderString.AppendFormat("{0}", obj);
                    }
                    count++;                    
                }
            }
            log.Debug("Parameter: {0}", buiderString.ToString());
        }

        private void LogSqlParameter(params SqlParameter[] parameterValues)
        {
            StringBuilder buiderString = new StringBuilder();
            if (parameterValues != null)
            {
                int count = 0;
                foreach (SqlParameter obj in parameterValues)
                {

                    if (count != 0)
                    {
                        buiderString.Append(", ");
                    }
                    if (obj == null || obj.Value == null)
                    {
                        buiderString.AppendFormat("null");
                    }
                    else if (obj.Value is string)
                    {
                        buiderString.AppendFormat("'{0}'", obj.Value);
                    }
                    else if (obj.Value is DateTime)
                    {
                        buiderString.AppendFormat("'{0:yyyy-MM-dd}'", obj.Value);
                    }
                    else
                    {
                        buiderString.AppendFormat("{0}", obj.Value);
                    }
                    count++;
                }
            }
            log.Debug("SqlParameter: {0}", buiderString.ToString());
        }

        private void LogSqlException(SqlException sqlEx)
        {
            log.Warn("sqlEx.ToString(): {0}", sqlEx.ToString());
        }

        private void HandleSqlException(SqlException sqlEx, string commandText)
        {
            log.Warn("HandleSqlException: commandText=>{0}", commandText);
            HandleSqlException(sqlEx);
        }

        private void HandleSqlException(SqlException sqlEx)
        {
            LogSqlException(sqlEx);

            if (sqlEx.Number >= 50000)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < sqlEx.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + sqlEx.Errors[i].Message + "\n" +
                        "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlEx.Errors[i].Source + "\n" +
                        "Number: " + sqlEx.Errors[i].Number + "\n" +
                        "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                }
                log.Warn(errorMessages.ToString());
                throw new SqlServerBusinessException(sqlEx, sqlEx.Message);
            }
            else
            {
                throw sqlEx;
            }

            //if (sqlEx.Class >= 11 && sqlEx.Class <= 16)
            //{

            //}
            //else
            //{
            //    throw sqlEx;
            //}
        }



    }
}
