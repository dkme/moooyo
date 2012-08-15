using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace NESCBB.DataLayer
{
    /* 已重构为IDBOperationHelper
    public class SQLOperationHelper
    {
        public static SqlTransaction f_开始SqlTransaction()
        {
            SqlConnection myconn = new SqlConnection(Comm.ConnectionStr);

            myconn.Open();

            SqlTransaction st = myconn.BeginTransaction(IsolationLevel.ReadUncommitted);
            return st;
        }

        public static void f_结束SqlTransaction(SqlTransaction st)
        {
            try
            {
                SqlConnection myconn = st.Connection;

                if (myconn != null)
                {
                    st.Commit();
                    if (myconn.State != ConnectionState.Closed)
                    {
                        SqlConnection.ClearPool(myconn);
                        myconn.Close();
                        myconn.Dispose();
                    }

                }
                st.Dispose();
                st = null;
            }
            catch (Exception err)
            {
                SqlConnection myconn = st.Connection;

                if (myconn != null)
                {
                    st.Rollback();
                    if (myconn.State != ConnectionState.Closed)
                    {
                        SqlConnection.ClearPool(myconn);
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }

        public static void f_回滚SqlTransaction(SqlTransaction st)
        {
            if (st != null)
            {
                try
                {

                    if (st.Connection != null)
                    {
                        st.Rollback();
                        if (st.Connection.State != ConnectionState.Closed)
                        {
                            SqlConnection.ClearPool(st.Connection);
                            st.Connection.Close();
                            st.Connection.Dispose();
                        }
                    }
                    st.Dispose();
                    st = null;
                }
                catch { }
            }
        }

        public static int f_Sql调用无反回(String sql)
        {
            return f_Sql调用无反回(sql, Comm.ConnectionStr);
        }

        public static int f_Sql调用无反回(String sql, String connectionStr)
        {
            int intReturn = 0;
            try
            {

                //SqlConnection.ClearAllPools(); 
                SqlConnection myconn = new SqlConnection(connectionStr);

                myconn.Open();

                intReturn = NESCBB.DataLayer.SqlHelper.ExecuteNonQuery(myconn, CommandType.Text, sql);

                SqlConnection.ClearPool(myconn);
                myconn.Close();
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }

            return intReturn;
        }

        public static int f_Sql调用无反回(SqlTransaction st, String sql, bool AllComplete)
        {
            int intReturn = 0;
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
            }

            try
            {
                intReturn = DataLayer.SqlHelper.ExecuteNonQuery(st, CommandType.Text, sql);

                if (AllComplete)
                {
                    SqlConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                        if (myconn.State != ConnectionState.Closed)
                        {
                            SqlConnection.ClearPool(myconn);
                            myconn.Close();
                            myconn.Dispose();
                        }
                }

            }
            catch (Exception err)
            {
                SqlConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                    if (myconn.State != ConnectionState.Closed)
                    {
                        SqlConnection.ClearPool(myconn);
                        myconn.Close();
                        myconn.Dispose();
                    }

                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
            return intReturn;
        }

        public static SqlDataReader f_读取DataReader(String sql)
        {
            SqlDataReader myr = SqlHelper.ExecuteReader(Comm.ConnectionStr, CommandType.Text, sql);
            return myr;
        }

        public static SqlDataReader f_读取DataReader(String sql, CommandType commaneType, String connectionStr)
        {
            SqlDataReader myr = SqlHelper.ExecuteReader(connectionStr, commaneType, sql);
            return myr;
        }

        public static SqlDataReader f_读取DataReader(SqlTransaction st, CommandType commaneType, String sql)
        {
            SqlDataReader myr = SqlHelper.ExecuteReader(st, commaneType, sql);
            return myr;
        }

    }
    */
}
