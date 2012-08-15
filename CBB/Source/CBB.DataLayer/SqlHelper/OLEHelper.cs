using System;
using System.Data;
using System.Xml;
using System.Data.OleDb;
using System.Collections;

namespace NESCBB.DataLayer.Core
{
    public sealed class OleDbHelper
    {
        /// <summary>
        /// 数据库连接类型枚举
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        /// OleDbDataReader(不带参数)
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, (OleDbParameter[])null); 
        }

        /// <summary>
        /// OleDbDataReader(带参数)
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if (transaction != null && transaction.Connection == null)
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// 私有方法--OleDbDataReader
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <param name="connectionOwnership"></param>
        /// <returns></returns>
        private static OleDbDataReader ExecuteReader(OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, OleDbParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            bool mustCloseConnection = false;

            OleDbCommand cmd = new OleDbCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                OleDbDataReader dataReader;

                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (OleDbParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    cmd.Parameters.Clear();
                }

                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 私有方法--PrepareCommand
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="connection">数据库连接串</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <param name="mustCloseConnection"></param>
        private static void PrepareCommand(OleDbCommand command, OleDbConnection connection, OleDbTransaction transaction, CommandType commandType, string commandText, OleDbParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (commandText == null || commandText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }

            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            command.Connection = connection;

            command.CommandText = commandText;

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            command.CommandType = commandType;

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        /// <summary>
        /// 私有方法--AttachParameters
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="commandParameters">命令参数</param>
        private static void AttachParameters(OleDbCommand command, OleDbParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (OleDbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        /// <summary>
        /// 执行语句(不带返回)
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行类型</param>
        /// <returns>影响记录数</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// 执行语句(不带返回)
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>影响记录数</returns>
        public static int ExecuteNonQuery(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            OleDbCommand cmd = new OleDbCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (OleDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// 执行语句(不带返回)
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>影响记录数</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// 执行语句(不带返回)
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>影响记录数</returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            OleDbCommand cmd = new OleDbCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            return ExecuteReader(connection, (OleDbTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            OleDbConnection connection = null;
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();

                return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
            }
            catch
            {
                if (connection != null) connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(OleDbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="connection">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(OleDbConnection connection, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            OleDbCommand cmd = new OleDbCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (OleDbTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return ds;
            }
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="transaction">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(OleDbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="transaction">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(OleDbTransaction transaction, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            OleDbCommand cmd = new OleDbCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, (OleDbParameter[])null);
        }

        /// <summary>
        /// 数据库执行(返回DataSet)
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="commandText">执行文本</param>
        /// <param name="commandParameters">参数集</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OleDbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }
     }
}
