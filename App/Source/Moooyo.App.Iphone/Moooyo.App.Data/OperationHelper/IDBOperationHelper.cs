using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using Mono.Data.SqlExpressions;
using System.Data;

namespace Moooyo.App.Data
{
    public enum OperationDBType
    {
        SQL, SQLLite, Unknow
    }
    public class IDBOperationHelper
    {
        public static OperationDBType GetDBType(string ConnectionStr)
        {
            //按照ConnectionStr中OLEDB Access访问和SQL访问的不同点，区分是哪种数据连接
            if (ConnectionStr.ToLower().Contains("server="))
            {
                return OperationDBType.SQL;
            }
            else if (ConnectionStr.ToLower().Contains("microsoft.jet.oledb"))
            {
                return OperationDBType.SQLLite;
            }
            return OperationDBType.Unknow;
        }

        public static IDbConnection GetIDbConnection(string ConnectionStr)
        {
            IDbConnection myconn = null;

            if (GetDBType(ConnectionStr) == OperationDBType.SQLLite)
            {
                myconn = new Mono.Data.Sqlite.SqliteConnection(ConnectionStr);
            }
            else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
            {
                myconn = new SqlConnection(ConnectionStr);
            }
            else if (GetDBType(ConnectionStr) == OperationDBType.Unknow)
            {
                throw new Exception("IDBOperationHelper.GetIDbConnection 无法检测连接字符串的数据库类型！");
            }

            return myconn;
        }


        public static IDbTransaction BeginIDbTransaction()
        {
            IDbConnection myconn = GetIDbConnection(Comm.ConnectionStr);
            myconn.Open();
            IDbTransaction st = myconn.BeginTransaction(IsolationLevel.ReadUncommitted);
            return st;
        }
        public static IDbTransaction BeginIDbTransaction(String ConnectionStr)
        {
            IDbConnection myconn = GetIDbConnection(ConnectionStr);
            myconn.Open();
            IDbTransaction st = myconn.BeginTransaction(IsolationLevel.ReadUncommitted);
            return st;
        }
        public static void CommitIDbTransaction(IDbTransaction st)
        {
            try
            {
                IDbConnection myconn = st.Connection;
                st.Commit();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static void RollbackIDbTransaction(IDbTransaction st)
        {
            if (st != null)
            {
                try
                {
                    st.Rollback();
                    if (st.Connection != null)
                    {
                        if (st.Connection.State != ConnectionState.Closed)
                        {
                            st.Connection.Close();
                            st.Connection.Dispose();
                        }
                    }
                }
                catch { }
            }
        }


        public static int ExecuteNonQuery(String sql)
        {
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQLLite)
                {
                    IDbConnection myconn = GetIDbConnection(Comm.ConnectionStr);
                    return SqliteHelper.ExecuteNonQuery((SqliteConnection)myconn, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return SqlHelper.ExecuteNonQuery(Comm.ConnectionStr, CommandType.Text, sql);
                }
                return 0;
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));

            }
        }

        public static int ExecuteNonQuery(String sql, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.SQLLite)
                {
                    IDbConnection myconn = GetIDbConnection(ConnectionStr);
                    return SqliteHelper.ExecuteNonQuery((SqliteConnection)myconn, commaneType, sql);
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return SqlHelper.ExecuteNonQuery(ConnectionStr, commaneType, sql);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static int ExecuteNonQuery(String sql, IDbDataParameter[] sp, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.SQLLite)
                {
                    IDbConnection myconn = GetIDbConnection(ConnectionStr);
                    return SqliteHelper.ExecuteNonQuery((SqliteConnection)myconn, commaneType, sql,(SqliteParameter[])sp);
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return SqlHelper.ExecuteNonQuery(ConnectionStr, commaneType, sql, (SqlParameter[])sp);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static int ExecuteNonQuery(IDbTransaction st, String sql, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                int returnvalue=0;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    returnvalue = SqliteHelper.ExecuteNonQuery((SqliteTransaction)st, CommandType.Text, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = SqlHelper.ExecuteNonQuery((SqlTransaction)st, CommandType.Text, sql);
                }
                if (AllComplete)
                {
                    IDbConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                    {
                        if (myconn.State != ConnectionState.Closed)
                        {
                            myconn.Close();
                            myconn.Dispose();
                        }
                    }
                }
                return returnvalue;

            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static int ExecuteNonQuery(IDbTransaction st, String sql, IDbDataParameter[] sp, CommandType commaneType, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                int returnvalue = 0;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    returnvalue = SqliteHelper.ExecuteNonQuery((SqliteTransaction)st, commaneType, sql, (SqliteParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = SqlHelper.ExecuteNonQuery((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
                }
                if (AllComplete)
                {
                    IDbConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                    {
                        if (myconn.State != ConnectionState.Closed)
                        {
                            myconn.Close();
                            myconn.Dispose();
                        }
                    }
                }
                return returnvalue;

            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }



        public static object ExecuteScalar(String sql)
        {
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQLLite)
                {
                    //myconn.Close();
                    throw new Exception("Sqlite不提供单一对象提取方法。");
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return SqlHelper.ExecuteNonQuery(Comm.ConnectionStr, CommandType.Text, sql);
                }

                return 0;
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));

            }
        }

        public static object ExecuteScalar(String sql, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.SQLLite)
                {
                    throw new Exception("Sqlite不提供单一对象提取方法。");
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    return SqlHelper.ExecuteScalar(ConnectionStr, commaneType, sql);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static object ExecSQLReturnObject(String sql, IDbDataParameter[] sp, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.SQLLite)
                {
                    throw new Exception("Sqlite不提供单一对象提取方法。");
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    return SqlHelper.ExecuteScalar(ConnectionStr, commaneType, sql, (SqlParameter[])sp);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static object ExecSQLReturnObject(IDbTransaction st, String sql, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                object returnvalue=null;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    throw new Exception("Sqlite不提供单一对象提取方法。");
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = SqlHelper.ExecuteScalar((SqlTransaction)st, CommandType.Text, sql);
                }
                if (AllComplete)
                {
                    IDbConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                    {
                        if (myconn.State != ConnectionState.Closed)
                        {
                            myconn.Close();
                            myconn.Dispose();
                        }
                    }
                }
                return returnvalue;

            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }

        public static object ExecuteScalar(IDbTransaction st, String sql, IDbDataParameter[] sp, CommandType commaneType, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                object returnvalue = null;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    throw new Exception("Sqlite不提供单一对象提取方法。");
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = SqlHelper.ExecuteScalar((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
                }
                if (AllComplete)
                {
                    IDbConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                    {
                        if (myconn.State != ConnectionState.Closed)
                        {
                            myconn.Close();
                            myconn.Dispose();
                        }
                    }
                }
                return returnvalue;

            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
        }
 
 
 
        public static IDataReader ExecuteReader(String sql)
        {
            IDataReader myr = null;
            try
            {

                if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteReader(Comm.ConnectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteReader(Comm.ConnectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {                         
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static IDataReader ExecuteReader(IDbTransaction st, CommandType commaneType, String sql)
        {
            IDataReader myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteReader((SqliteTransaction)st, commaneType, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteReader((SqlTransaction)st, commaneType, sql);
                }
            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static IDataReader ExecuteReader(IDbTransaction st, CommandType commaneType, String sql, IDbDataParameter[] sp)
        {
            IDataReader myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteReader((SqliteTransaction)st, commaneType, sql, (SqliteParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteReader((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static IDataReader ExecuteReader(String sql, CommandType commaneType, String connectionStr)
        {
            IDataReader myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteReader(connectionStr, commaneType, sql);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteReader(connectionStr, commaneType, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static IDataReader ExecuteReader(String sql, IDbDataParameter[] sp, CommandType commaneType, String connectionStr)
        {
            IDataReader myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteReader(connectionStr, commaneType, sql,(SqliteParameter[])sp);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteReader(connectionStr, commaneType, sql,(SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }


        public static DataSet ExecuteDataset(String sql)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteDataset(Comm.ConnectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteDataset(Comm.ConnectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static DataSet ExecuteDataset(IDbTransaction st, CommandType commaneType, String sql)
        {
            DataSet myr = null;
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }
            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteDataset((SqliteTransaction)st, commaneType, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteDataset((SqlTransaction)st, commaneType, sql);
                }
            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static DataSet ExecuteDataset(IDbTransaction st, CommandType commaneType, String sql, IDbDataParameter[] sp)
        {
            DataSet myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteDataset((SqliteTransaction)st, commaneType, sql,(SqliteParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteDataset((SqlTransaction)st, commaneType, sql,(SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                IDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                {
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static DataSet ExecuteDataset(String sql, CommandType commaneType, String connectionStr)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteDataset(connectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteDataset(connectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }

        public static DataSet ExecuteDataset(String sql,IDbDataParameter[] sp, CommandType commaneType, String connectionStr)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.SQLLite)
                {
                    myr = SqliteHelper.ExecuteDataset(connectionStr, commaneType, sql,(SqliteParameter[])sp);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = SqlHelper.ExecuteDataset(connectionStr, commaneType, sql,(SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                throw new Exception(CBB.ExceptionHelper.ExpressionPaser.ErrTrim(err));
            }
            return myr;
        }
    }
}
