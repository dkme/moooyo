using System;
using System.Data;
using System.Xml;
using Mono.Data.Sqlite;
using Mono.Data.SqlExpressions;
using System.Collections;

namespace Moooyo.App.Data
{
    public sealed class SqliteHelper
    {
        /// <summary>
        /// ���ݿ���������ö��
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,
            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        /// <summary>
        /// SqliteDataReader(��������)
        /// </summary>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(SqliteTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, (SqliteParameter[])null); 
        }

        /// <summary>
        /// SqliteDataReader(������)
        /// </summary>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(SqliteTransaction transaction, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
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
        /// ˽�з���--SqliteDataReader
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <param name="connectionOwnership"></param>
        /// <returns></returns>
        private static SqliteDataReader ExecuteReader(SqliteConnection connection, SqliteTransaction transaction, CommandType commandType, string commandText, SqliteParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            bool mustCloseConnection = false;

            SqliteCommand cmd = new SqliteCommand();
            try
            {
                PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

                SqliteDataReader dataReader;

                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    dataReader = cmd.ExecuteReader();
                }
                else
                {
                    dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool canClear = true;
                foreach (SqliteParameter commandParameter in cmd.Parameters)
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
        /// ˽�з���--PrepareCommand
        /// </summary>
        /// <param name="command">����</param>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <param name="mustCloseConnection"></param>
        private static void PrepareCommand(SqliteCommand command, SqliteConnection connection, SqliteTransaction transaction, CommandType commandType, string commandText, SqliteParameter[] commandParameters, out bool mustCloseConnection)
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
        /// ˽�з���--AttachParameters
        /// </summary>
        /// <param name="command">����</param>
        /// <param name="commandParameters">�������</param>
        private static void AttachParameters(SqliteCommand command, SqliteParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqliteParameter p in commandParameters)
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
        /// ִ�����(��������)
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ������</param>
        /// <returns>Ӱ���¼��</returns>
        public static int ExecuteNonQuery(SqliteConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ִ�����(��������)
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>Ӱ���¼��</returns>
        public static int ExecuteNonQuery(SqliteConnection connection, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            SqliteCommand cmd = new SqliteCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqliteTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        /// <summary>
        /// ִ�����(��������)
        /// </summary>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>Ӱ���¼��</returns>
        public static int ExecuteNonQuery(SqliteTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ִ�����(��������)
        /// </summary>
        /// <param name="transaction">����</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>Ӱ���¼��</returns>
        public static int ExecuteNonQuery(SqliteTransaction transaction, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            SqliteCommand cmd = new SqliteCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            return retval;
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(SqliteConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(SqliteConnection connection, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            return ExecuteReader(connection, (SqliteTransaction)null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connectionString">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="connectionString">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>SqliteDataReader</returns>
        public static SqliteDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            SqliteConnection connection = null;
            try
            {
                connection = new SqliteConnection(connectionString);
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
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(SqliteConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="connection">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(SqliteConnection connection, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            SqliteCommand cmd = new SqliteCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqliteTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            using (SqliteDataAdapter da = new SqliteDataAdapter(cmd))
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
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="transaction">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(SqliteTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="transaction">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(SqliteTransaction transaction, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            SqliteCommand cmd = new SqliteCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

            using (SqliteDataAdapter da = new SqliteDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="connectionString">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, (SqliteParameter[])null);
        }

        /// <summary>
        /// ���ݿ�ִ��(����DataSet)
        /// </summary>
        /// <param name="connectionString">���ݿ����Ӵ�</param>
        /// <param name="commandType">ָ������</param>
        /// <param name="commandText">ִ���ı�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqliteParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }
     }
}
