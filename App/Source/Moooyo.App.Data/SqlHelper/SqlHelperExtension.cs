///
///ģ���ţ�HLsoft.Dataobject
///�����������������ݷ��ʲ�-��չ����
///��    �ߣ�����
///�޸����ڣ�2003-10-15
///������Ϣ��
///
using System;
using System.Data;
using System.Data.SqlClient;

namespace Moooyo.App.Data
{
	/// <summary>
	/// Sql Helper class����չ��
	/// </summary>
	public sealed class SqlHelperExtension
	{
		private SqlHelperExtension()
		{
		}

		/// <summary>
		/// �������ݼ���ϵͳ�־û���(���ݿ�).�Զ��ı����������ʹ�ø÷���..
		/// ���� 2003-10-15  ������:2003-10-17
		/// </summary>
		/// <param name="transaction">���ݿ�����</param>
		/// <param name="dataSet"> Ҫ���µ����ݼ�</param>
		/// <param name="tableName">Update�����ݼ�,��������ͼҲ�����������Ļ���</param>
		/// <param name="procTableName">�����Ļ���,����Ϊnull,���ڸñ����ɵ����ݿ�������洢����,UPDATE,INSERT,DELETE</param>
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
		/// �������ݼ���ϵͳ�־û���(���ݿ�).�Զ��ı����������ʹ�ø÷���..
		/// chy 2003-10-18
		/// </summary>
		/// <param name="dataSet"></param>
		/// <param name="tableNames">��Ҫ���µ�table�������б�</param>
		/// <param name="procTableNames">��table�����Ӧ�Ĵ洢���̵�ǰ׺(һ���Ǳ���)�����б�</param>
		public static void Save(DataSet dataSet, string[] tableNames,string[] procTableNames)
		{
			if (tableNames.GetLength(0)!=procTableNames.GetLength(0)) throw  new ApplicationException ("����:���µı���Ŀ��洢����ǰ׺��Ŀ��ƥ��!");
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
		/// �������ݼ���ϵͳ�־û���(���ݿ�),ʹ��ϵͳĬ���ṩ�����ݿ�����,�Ѿ���װ֧������.(ע:��������Ƽ�ʹ���������)
		/// ���� 2003-10-15
		/// </summary>
		/// <param name="dataSet"> Ҫ���µ����ݼ�</param>
		/// <param name="tableName">Update�����ݼ�,��������ͼҲ�����������Ļ���</param>
        /// <param name="procTableName">�����Ļ���,����Ϊnull,���ڸñ����ɵ����ݿ�������洢����,UPDATE,INSERT,DELETE</param>
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
        /// ʹ��Datareader�Ľ��������ͻ���dataset,�÷��������ҳ�������ݿ�
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
		/// ʹ��Datareader�Ľ��������ͻ���dataset
		/// ����:���� 2003-09-04
		/// </summary>
		/// <param name="dataReader">Դ����datareader�����</param>
		/// <param name="dataSet">���������ͻ������ݼ�</param>
		/// <param name="tableName">������ı�</param>
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
