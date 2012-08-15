using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using System.Data;

namespace Moooyo.AppDomain.Data
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
                return OperationDBType.OLEDB;
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
                throw new Exception(CBB.ExceptionHelper..ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
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


        public static int ExecSQL(String sql)
        {
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.OLEDB)
                {
                    IDbConnection myconn = GetIDbConnection(Comm.ConnectionStr);
                    return DataLayer.Core.OleDbHelper.ExecuteNonQuery((OleDbConnection)myconn, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return DataLayer.Core.SqlHelper.ExecuteNonQuery(Comm.ConnectionStr, CommandType.Text, sql);
                }
                return 0;
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));

            }
        }

        public static int ExecSQL(String sql, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.OLEDB)
                {
                    IDbConnection myconn = GetIDbConnection(ConnectionStr);
                    return DataLayer.Core.OleDbHelper.ExecuteNonQuery((OleDbConnection)myconn, commaneType, sql);
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return DataLayer.Core.SqlHelper.ExecuteNonQuery(ConnectionStr, commaneType, sql);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static int ExecSQL(String sql, IDbDataParameter[] sp, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.OLEDB)
                {
                    IDbConnection myconn = GetIDbConnection(ConnectionStr);
                    return DataLayer.Core.OleDbHelper.ExecuteNonQuery((OleDbConnection)myconn, commaneType, sql,(OleDbParameter[])sp);
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return DataLayer.Core.SqlHelper.ExecuteNonQuery(ConnectionStr, commaneType, sql, (SqlParameter[])sp);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static int ExecSQL(IDbTransaction st, String sql, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                int returnvalue=0;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    returnvalue = DataLayer.Core.OleDbHelper.ExecuteNonQuery((OleDbTransaction)st, CommandType.Text, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = DataLayer.Core.SqlHelper.ExecuteNonQuery((SqlTransaction)st, CommandType.Text, sql);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static int ExecSQL(IDbTransaction st, String sql, IDbDataParameter[] sp, CommandType commaneType, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                int returnvalue = 0;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    returnvalue = DataLayer.Core.OleDbHelper.ExecuteNonQuery((OleDbTransaction)st, commaneType, sql, (OleDbParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = DataLayer.Core.SqlHelper.ExecuteNonQuery((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }



        public static object ExecSQLReturnObject(String sql)
        {
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.OLEDB)
                {
                    //myconn.Close();
                    throw new Exception("NESCBB.DataLayer：OLEDB不提供单一对象提取方法。");
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    //myconn.Close();
                    return DataLayer.Core.SqlHelper.ExecuteNonQuery(Comm.ConnectionStr, CommandType.Text, sql);
                }

                return 0;
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));

            }
        }

        public static object ExecSQLReturnObject(String sql, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.OLEDB)
                {
                    throw new Exception("NESCBB.DataLayer：OLEDB不提供单一对象提取方法。");
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    return DataLayer.Core.SqlHelper.ExecuteScalar(ConnectionStr, commaneType, sql);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static object ExecSQLReturnObject(String sql, IDbDataParameter[] sp, CommandType commaneType, String ConnectionStr)
        {
            try
            {
                if (GetDBType(ConnectionStr) == OperationDBType.OLEDB)
                {
                    throw new Exception("NESCBB.DataLayer：OLEDB不提供单一对象提取方法。");
                }
                else if (GetDBType(ConnectionStr) == OperationDBType.SQL)
                {
                    return DataLayer.Core.SqlHelper.ExecuteScalar(ConnectionStr, commaneType, sql, (SqlParameter[])sp);
                }
                return 0;

            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
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
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    throw new Exception("NESCBB.DataLayer：OLEDB不提供单一对象提取方法。");
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = DataLayer.Core.SqlHelper.ExecuteScalar((SqlTransaction)st, CommandType.Text, sql);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static object ExecSQLReturnObject(IDbTransaction st, String sql, IDbDataParameter[] sp, CommandType commaneType, bool AllComplete)
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                object returnvalue = null;
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    throw new Exception("NESCBB.DataLayer：OLEDB不提供单一对象提取方法。");
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    returnvalue = DataLayer.Core.SqlHelper.ExecuteScalar((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }
 
 
 
        public static IDataReader ExecSQLReturnDataReader(String sql)
        {
            IDataReader myr = null;
            try
            {

                if (GetDBType(Comm.ConnectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteReader(Comm.ConnectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteReader(Comm.ConnectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {                         
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static IDataReader ExecSQLReturnDataReader(IDbTransaction st, CommandType commaneType, String sql)
        {
            IDataReader myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteReader((OleDbTransaction)st, commaneType, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteReader((SqlTransaction)st, commaneType, sql);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static IDataReader ExecSQLReturnDataReader(IDbTransaction st, CommandType commaneType, String sql, IDbDataParameter[] sp)
        {
            IDataReader myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteReader((OleDbTransaction)st, commaneType, sql, (OleDbParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteReader((SqlTransaction)st, commaneType, sql, (SqlParameter[])sp);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static IDataReader ExecSQLReturnDataReader(String sql, CommandType commaneType, String connectionStr)
        {
            IDataReader myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteReader(connectionStr, commaneType, sql);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteReader(connectionStr, commaneType, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static IDataReader ExecSQLReturnDataReader(String sql, IDbDataParameter[] sp, CommandType commaneType, String connectionStr)
        {
            IDataReader myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteReader(connectionStr, commaneType, sql,(OleDbParameter[])sp);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteReader(connectionStr, commaneType, sql,(SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }


        public static DataSet ExecSQLReturnDataSet(String sql)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(Comm.ConnectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteDataset(Comm.ConnectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(Comm.ConnectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteDataset(Comm.ConnectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static DataSet ExecSQLReturnDataSet(IDbTransaction st, CommandType commaneType, String sql)
        {
            DataSet myr = null;
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }
            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteDataset((OleDbTransaction)st, commaneType, sql);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteDataset((SqlTransaction)st, commaneType, sql);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static DataSet ExecSQLReturnDataSet(IDbTransaction st, CommandType commaneType, String sql, IDbDataParameter[] sp)
        {
            DataSet myr = null;

            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                if (GetDBType(st.Connection.ConnectionString) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteDataset((OleDbTransaction)st, commaneType, sql,(OleDbParameter[])sp);
                }
                else if (GetDBType(st.Connection.ConnectionString) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteDataset((SqlTransaction)st, commaneType, sql,(SqlParameter[])sp);
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
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static DataSet ExecSQLReturnDataSet(String sql, CommandType commaneType, String connectionStr)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteDataset(connectionStr, CommandType.Text, sql);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteDataset(connectionStr, CommandType.Text, sql);
                }
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }

        public static DataSet ExecSQLReturnDataSet(String sql,IDbDataParameter[] sp, CommandType commaneType, String connectionStr)
        {
            DataSet myr = null;
            try
            {
                if (GetDBType(connectionStr) == OperationDBType.OLEDB)
                {
                    myr = DataLayer.Core.OleDbHelper.ExecuteDataset(connectionStr, commaneType, sql,(OleDbParameter[])sp);
                }
                else if (GetDBType(connectionStr) == OperationDBType.SQL)
                {
                    myr = DataLayer.Core.SqlHelper.ExecuteDataset(connectionStr, commaneType, sql,(SqlParameter[])sp);
                }
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return myr;
        }


        #region 获取数据(用于数据分段传输中的数据分解，彭友，2010-09-25)

        /// <summary>
        /// 获取数据(用于数据分段传输中的数据分解，彭友，2010-09-25)
        /// </summary>
        /// <param name="strSql">SQL语名</param>
        /// <param name="strConnString">连接字符串</param>
        /// <param name="intRowNum">最大传输行数</param>
        /// <param name="intCount">分解的Table数量</param>
        /// <param name="dsCache">保存到缓存中的数据集</param>
        /// <returns>每一次返回的数据集</returns>
        public static DataSet GetDataList(string strSql, string strConnString, int intRowNum,
            ref int intCount,
            ref DataSet dsCache)
        {

            DataSet ds = new DataSet();//每一次返回的数据集
            //intRowNum++;
            intCount = 0;              //分解的Table数量
            dsCache = new DataSet();   //保存到缓存中的数据集

            using (SqlConnection sqlConn = new SqlConnection(strConnString))
            {
                SqlCommand sqlComm = new SqlCommand(strSql, sqlConn);
                sqlConn.Open();

                SqlDataReader sqlReader = sqlComm.ExecuteReader();

                DataTable dtNew = null;//新表
                int intIndex = 0;      //行索引值
                while (sqlReader.Read())
                {
                    intIndex++;

                    //设置列信息
                    if (dtNew == null)
                    {
                        dtNew = new DataTable();
                        for (int i = 0; i < sqlReader.FieldCount; i++)
                        {
                            //string strColName = sqlReader.GetName(i);
                            //Type colType = sqlReader.GetFieldType(i);
                            if (!dtNew.Columns.Contains(sqlReader.GetName(i).ToString()))
                            {
                                dtNew.Columns.Add(sqlReader.GetName(i), sqlReader.GetFieldType(i));
                            }
                        }
                        dtNew.TableName = "tab_" + intCount;
                    }

                    //添加行信息
                    DataRow drNew = dtNew.NewRow();
                    foreach (DataColumn col in dtNew.Columns)
                    {
                        drNew[col.ColumnName] = sqlReader[col.ColumnName];
                    }
                    dtNew.Rows.Add(drNew);

                    //添加表信息
                    if (intIndex % intRowNum == 0)
                    {
                        if (intCount == 0)
                        {
                            ds.Tables.Add(dtNew.Copy());
                        }
                        else
                        {
                            dsCache.Tables.Add(dtNew.Copy());
                        }

                        intCount++;
                        intIndex = 0;
                        dtNew.TableName = "tab_" + intCount;
                        dtNew.Rows.Clear();
                    }
                }
                //第一次获取的数据(没有达到最大传输行数)
                if (intCount == 0 && intIndex > 0)
                {
                    ds.Tables.Add(dtNew.Copy());
                    dsCache.Clear();
                    dsCache.Dispose();
                    dsCache = null;
                }
                //最后一次获取的数据(没有达到最大传输行数)
                if (intCount > 0 && intIndex > 0)
                {
                    dsCache.Tables.Add(dtNew.Copy());
                }

                //是否最后一次获得的行数与最大传输的行数相等
                if (intCount > 0 && intIndex == 0)
                {
                    intCount--;
                }

                //清空表信息
                if (dtNew != null)
                {
                    dtNew.Clear();
                    dtNew.Dispose();
                    dtNew = null;
                }
                sqlReader.Close();
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds;
            }
            ds.Clear();
            ds.Dispose();
            dsCache.Clear();
            dsCache.Dispose();
            dsCache = null;

            return null;

        }

        #endregion

    }
}
