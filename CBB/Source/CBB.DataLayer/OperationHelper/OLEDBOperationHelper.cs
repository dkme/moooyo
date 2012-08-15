using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.OleDb;
using System.Data;

namespace NESCBB.DataLayer
{
    /* 已重构为IDBOperationHelper
    public class OLEDBOperationHelper
    {
        public static OleDbTransaction f_开始OleDbTransaction()//黄河 ACCESS
        {
            OleDbConnection myconn = new OleDbConnection(Comm.ConnectionStr);
            myconn.Open();
            OleDbTransaction st = myconn.BeginTransaction(IsolationLevel.ReadUncommitted);
            return st;
        }
        public static void f_结束OleDbTransaction(OleDbTransaction st)//黄河 ACCESS
        {
            try
            {
                OleDbConnection myconn = st.Connection;
                st.Commit();
                if (myconn != null)
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }

            }
            catch (Exception err)
            {
                OleDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }

                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }
        public static void f_OleDb调用无反回(String sql)//黄河 ACCESS
        {
            try
            {
                OleDbConnection myconn = new OleDbConnection(Comm.ConnectionStr);

                myconn.Open();

                DataLayer.OleDbHelper.ExecuteNonQuery(myconn, CommandType.Text, sql);

                myconn.Close();
            }
            catch (Exception err)
            {
                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }
        public static void f_回滚OleDbTransaction(OleDbTransaction st)//黄河 ACCESS
        {
            if (st != null)
            {
                try
                {
                    st.Rollback();
                    if (st.Connection != null)
                        if (st.Connection.State != ConnectionState.Closed)
                        {
                            st.Connection.Close();
                            st.Connection.Dispose();
                        }
                }
                catch { }
            }
        }
        public static void f_OleDb调用无反回(OleDbTransaction st, String sql, bool AllComplete)//黄河 ACCESS
        {
            if (st == null)
            {
                throw new Exception("本方法调用需要传入一个有效的事务，本次调用传入事务为Null");
                return;
            }

            try
            {
                DataLayer.OleDbHelper.ExecuteNonQuery(st, CommandType.Text, sql);
                if (AllComplete)
                {
                    OleDbConnection myconn = st.Connection;
                    st.Commit();
                    if (myconn != null)
                        if (myconn.State != ConnectionState.Closed)
                        {
                            myconn.Close();
                            myconn.Dispose();
                        }
                }

            }
            catch (Exception err)
            {
                OleDbConnection myconn = st.Connection;
                st.Rollback();
                if (myconn != null)
                    if (myconn.State != ConnectionState.Closed)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }

                throw new Exception(NESCBB.ExpressionHelper.c_错误信息类.f_Sql错误处理(err));
            }
        }
    }
    */
}
