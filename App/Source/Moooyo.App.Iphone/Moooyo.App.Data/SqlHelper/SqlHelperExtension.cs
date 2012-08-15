///
///模块编号：HLsoft.Dataobject
///功能描述：抽象数据访问层-扩展方法
///作    者：畅雨
///修改日期：2003-10-15
///附加信息：
///
using System;
using System.Data;
using System.Data.SqlClient;

namespace Moooyo.App.Data
{
	/// <summary>
	/// Sql Helper class的扩展类
	/// </summary>
	public sealed class SqlHelperExtension
	{
		private SqlHelperExtension()
		{
		}

		/// <summary>
		/// 更新数据集到系统持久化层(数据库).对多表的保存操作建议使用该方法..
		/// 畅雨 2003-10-15  最后更新:2003-10-17
		/// </summary>
		/// <param name="transaction">数据库事务</param>
		/// <param name="dataSet"> 要更新的数据集</param>
		/// <param name="tableName">Update的数据集,可能是视图也可能是真正的基表</param>
		/// <param name="procTableName">真正的基表,可以为null,基于该表生成的数据库的三个存储过程,UPDATE,INSERT,DELETE</param>
		public static void Save(SqlTransaction transaction,DataSet dataSet, string tableName,string procTableName)
		{
			string BaseTable = null;
			if (procTableName == null) 
				BaseTable=tableName;
			else
				BaseTable = procTableName;
			try
			{
				SqlCommand insertCommand = SqlHelper.CreateCommand(transaction.Connection,BaseTable+"_INSERT") ;
				SqlCommand deleteCommand = SqlHelper.CreateCommand(transaction.Connection,BaseTable+"_DELETE") ;				
				SqlCommand updateCommand = SqlHelper.CreateCommand(transaction.Connection,BaseTable+"_UPDATE") ;
				
				if (transaction!=null)
				{
					insertCommand.Transaction = transaction;
					deleteCommand.Transaction = transaction;
					updateCommand.Transaction = transaction;
				}
                
				SqlHelper.UpdateDataset(insertCommand,deleteCommand,updateCommand,dataSet,tableName);
			}
			catch(Exception e)
			{
				throw e;
			}
				
		}

		/// <summary>
		/// 更新数据集到系统持久化层(数据库).对多表的保存操作建议使用该方法..
		/// chy 2003-10-18
		/// </summary>
		/// <param name="dataSet"></param>
		/// <param name="tableNames">需要更新的table的数组列表</param>
		/// <param name="procTableNames">与table数组对应的存储过程的前缀(一般是表名)数组列表</param>
		public static void Save(DataSet dataSet, string[] tableNames,string[] procTableNames)
		{
			if (tableNames.GetLength(0)!=procTableNames.GetLength(0)) throw  new ApplicationException ("错误:更新的表数目与存储过程前缀数目不匹配!");
			string TranName =System.Convert.ToString(System.DateTime.Now);
			ConnectionProvider.connectionPrivider().OpenConnection();
			ConnectionProvider.connectionPrivider().BeginTransaction(TranName);
			try
			{
				try
				{
					for (int i=0;i<tableNames.GetLength(0);i++)
					{
						Save(ConnectionProvider.connectionPrivider().stCurrentTransaction,dataSet,tableNames[i],procTableNames[i]);
					}
					ConnectionProvider.connectionPrivider().CommitTransaction();
				}
				catch(Exception)
				{
					ConnectionProvider.connectionPrivider().RollbackTransaction(TranName);
				}
			}
			finally
			{
				ConnectionProvider.connectionPrivider().CloseConnection(false);
			
			}

		}

		/// <summary>
		/// 更新数据集到系统持久化层(数据库),使用系统默认提供的数据库连接,已经封装支持事务.(注:单表操作推荐使用这个方法)
		/// 畅雨 2003-10-15
		/// </summary>
		/// <param name="dataSet"> 要更新的数据集</param>
		/// <param name="tableName">Update的数据集,可能是视图也可能是真正的基表</param>
        /// <param name="procTableName">真正的基表,可以为null,基于该表生成的数据库的三个存储过程,UPDATE,INSERT,DELETE</param>
		public static void Save(DataSet dataSet, string tableName,string procTableName)
		{

			string TranName =System.Convert.ToString(System.DateTime.Now);
			string BaseTable = null;
			if (procTableName == null) 
				BaseTable=tableName;
			else
				BaseTable = procTableName;
			ConnectionProvider.connectionPrivider().BeginTransaction(TranName);
			try
			{		
				SqlCommand insertCommand = SqlHelper.CreateCommand(ConnectionProvider.connectionPrivider().scoDBConnection,BaseTable+"_INSERT") ;
				insertCommand.Transaction =ConnectionProvider.connectionPrivider().stCurrentTransaction; 
				
				SqlCommand deleteCommand = SqlHelper.CreateCommand(ConnectionProvider.connectionPrivider().scoDBConnection,BaseTable+"_DELETE") ;
				deleteCommand.Transaction =ConnectionProvider.connectionPrivider().stCurrentTransaction; 
				
				SqlCommand updateCommand = SqlHelper.CreateCommand(ConnectionProvider.connectionPrivider().scoDBConnection,BaseTable+"_UPDATE") ;	 
				updateCommand.Transaction =ConnectionProvider.connectionPrivider().stCurrentTransaction; 
				
				SqlHelper.UpdateDataset(insertCommand,deleteCommand,updateCommand,dataSet,tableName);
				
				ConnectionProvider.connectionPrivider().CommitTransaction();
			}
			catch(Exception e)
			{
				ConnectionProvider.connectionPrivider().RollbackTransaction(TranName);
				throw e;
			}				
		}

        /// <summary>
        /// 使用Datareader的结果填充类型化的dataset,该方法允许分页访问数据库
        /// </summary>
        /// <param name="dataReader">The DataReader used to fetch the values.</param>
        /// <param name="dataSet">The DataSet used to store the values.</param>
        /// <param name="tableName">The name of the DataSet table used to add the 
        /// DataReader records.</param>
        /// <param name="from">The quantity of records skipped before placing
        /// values on the DataReader on the DataSet.</param>
        /// <param name="count">The maximum quantity of records alloed to fill on the
        /// DataSet.</param>
        public static void Fill( IDataReader dataReader, DataSet dataSet, string tableName, int from, int count )
        {
            if( tableName == null)
                tableName = "unknownTable";
			    
            if( dataSet.Tables[ tableName ] == null  )
                dataSet.Tables.Add( tableName );
			
            // Get the DataTable reference
            DataTable fillTable;
            if( tableName == null )
                fillTable = dataSet.Tables[ 0 ];
            else
                fillTable = dataSet.Tables[ tableName ];

            DataRow fillRow;
            string fieldName;
            int recNumber = 0;
            int totalRecords = from + count;
            while( dataReader.Read() )
            {
                if( recNumber++ >= from )
                {
                    fillRow = fillTable.NewRow();
                    for( int fieldIdx = 0; fieldIdx < dataReader.FieldCount; fieldIdx++ )
                    {
                        fieldName = dataReader.GetName( fieldIdx );
                        if( fillTable.Columns.IndexOf( fieldName ) == -1 )
                            fillTable.Columns.Add( fieldName, dataReader.GetValue( fieldIdx ).GetType() );
                        fillRow[ fieldName ] = dataReader.GetValue( fieldIdx );
                    }
                    fillTable.Rows.Add( fillRow );
                }
                if( count != 0 && totalRecords <= recNumber )
                    break;
            }
            dataSet.AcceptChanges();
        }
	
		/// <summary>
		/// 使用Datareader的结果填充类型化的dataset
		/// 作者:畅雨 2003-09-04
		/// </summary>
		/// <param name="dataReader">源数据datareader结果集</param>
		/// <param name="dataSet">待填充的类型化的数据集</param>
		/// <param name="tableName">结果集的表</param>
		public static void Fill(IDataReader dataReader, DataSet dataSet, string tableName)
		{
			if( tableName == null)
						 tableName = "unknownTable";
			    
			if( dataSet.Tables[ tableName ] == null  )
				dataSet.Tables.Add( tableName );
			
			// Get the DataTable reference
			DataTable fillTable;
			if( tableName == null )
				fillTable = dataSet.Tables[ 0 ];
			else
				fillTable = dataSet.Tables[ tableName ];

			DataRow fillRow;
			string fieldName;
			while (dataReader.Read())
			{
				fillRow = fillTable.NewRow();
				for( int fieldIdx = 0; fieldIdx < dataReader.FieldCount; fieldIdx++ )
					{
						fieldName = dataReader.GetName( fieldIdx );
						if( fillTable.Columns.IndexOf( fieldName ) == -1 )
							fillTable.Columns.Add( fieldName, dataReader.GetValue( fieldIdx ).GetType() );
						fillRow[ fieldName ] = dataReader.GetValue( fieldIdx );
					}
					fillTable.Rows.Add( fillRow );
			}
			dataSet.AcceptChanges();
		}
	}
}
