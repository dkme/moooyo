using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Mono.Data.Sqlite;
using Mono.Data.SqlExpressions;
using System.Data;
using Moooyo.App.Data;

namespace Moooyo.App.Core.Preparation
{
	public class Database
	{
		//Full of database path (include db name)
		public static string DatabasePath {
			get {
				string pathStr = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				return Path.Combine(pathStr,Defs.Config.DatabaseName);
			}
		}
		/// <summary>
		/// Preparations the of database.
		/// </summary>
		public static void PreparationOfDatabase ()
		{
			//if database isn't exists ,creating it.
			if (!DetectingDatabase()) {
				CreatingDatabase ();
				InitializationTables();
			} else {
				UpdatingDatabase();
			}
		}
		/// <summary>
		/// Creating the database.
		/// </summary>
		private static void CreatingDatabase()
		{
			SqliteConnection.CreateFile(DatabasePath);
		}
		/// <summary>
		/// Detecting the database if it is exists.
		/// </summary>
		private static bool DetectingDatabase()
		{
			bool exists = File.Exists(DatabasePath);
			return exists;
		}
		/// <summary>
		/// Updatings the database.
		/// </summary>
		private static void UpdatingDatabase()
		{
		}
		/// <summary>
		/// Initializations the tables.
		/// </summary>
		private static void InitializationTables ()
		{
			//Defs SQL.
			string[] commands = new string[]{
			//Table "EnvSetting"
				"drop table if exists EnvSetting",
				@"create table EnvSetting(
					MajorVersion varchar(20),
					SubVersion varchar(20),
					AccountID varchar(20),
					MemberID varchar(20),
					LastOperationTime datetime,
					AutoLogin bit
				)",
			//Table "UpdatingHistory"
				"drop table if exists UpdatedHistory",
				@"create table UpdatingHistory(
					InstallTime datetime,
					UpdateTimes int,
					StartedAtVersion varchar(50),
					NowVersion varchar(50),
					RunnedTimes int,
					LastRunnedTime datetime
				)",
			//Table "CachedImage"
				"drop table if exists CachedImage",
				@"create table CachedImage(
					Name varchar(50) primary key,
					TimeStamp varchar(20),
					ImgData binary
				)"
			};


			//Start DB Transaction
			IDbTransaction st = App.Data.IDBOperationHelper.BeginIDbTransaction ("Data Source=" + DatabasePath);
			//Exec SQL
			foreach (var cmd in commands) {
				App.Data.IDBOperationHelper.ExecuteNonQuery (st, cmd, false);
			}
			//Commit DB Transaction
			App.Data.IDBOperationHelper.CommitIDbTransaction (st);

		}
		private static void InitializationDatas ()
		{
			//Start DB Transaction
			IDbTransaction st = App.Data.IDBOperationHelper.BeginIDbTransaction ("Data Source=" + DatabasePath);
			//Exec SQL
		    App.Data.IDBOperationHelper.ExecuteNonQuery (st, BuildInitEnvSettingSql(), false);
		    App.Data.IDBOperationHelper.ExecuteNonQuery (st, BuildInitUpdatedHistorySql(), false);
			//Commit DB Transaction
			App.Data.IDBOperationHelper.CommitIDbTransaction (st);
		}
		private static string BuildInitEnvSettingSql()
		{
			//Defs SQL.
			string sql = 
				  @"insert into EnvSetting
					(
						MajorVersion,
						SubVersion,
					    AccountID,
					    MemberID,
	                    LastOperationTime,
	                    AutoLogin
					)
					values
					(
					   @MajorVersion,
					   @SubVersion,
				       @AccountID,
                       @MemberID,
                       @LastOperationTime,
                       @AutoLogin
					)";
					

			List<App.Data.SqlParam> sqlParams = new List<SqlParam>(){
				new SqlParam("@MajorVersion",Defs.Config.MajorVersion,typeof(String)),
				new SqlParam("@SubVersion",Defs.Config.SubVersion,typeof(String)),
				new SqlParam("@AccountID","",typeof(String)),
				new SqlParam("@MemberID","",typeof(String)),
				new SqlParam("@LastOperationTime",DateTime.Now,typeof(DateTime)),
				new SqlParam("@AutoLogin",false,typeof(Boolean))
			};

			//build sql
			sql = SqlBuilder.GetSql(sql,sqlParams);
			return sql;
		}
		private static string BuildInitUpdatedHistorySql ()
		{
			//Defs SQL.
			string sql = 
				  @"insert into UpdatedHistory
					(
						InstallTime,
						UpdateTimes,
					    StartedAtVersion,
					    NowVersion,
	                    RunnedTimes,
	                    LastRunnedTime
					)
					values
					(
					   @InstallTime,
					   @UpdateTimes,
				       @StartedAtVersion,
                       @NowVersion,
                       @RunnedTimes,
                       @LastRunnedTime
					)";
					

			List<App.Data.SqlParam> sqlParams = new List<SqlParam>(){
				new SqlParam("@InstallTime",DateTime.Now,typeof(DateTime)),
				new SqlParam("@UpdateTimes",DateTime.Now,typeof(DateTime)),
				new SqlParam("@StartedAtVersion",Defs.Config.FullVersion,typeof(String)),
				new SqlParam("@NowVersion",Defs.Config.FullVersion,typeof(String)),
				new SqlParam("@RunnedTimes",1,typeof(int)),
				new SqlParam("@LastRunnedTime",DateTime.Now,typeof(DateTime))
			};

			//build sql
			sql = SqlBuilder.GetSql(sql,sqlParams);
			return sql;
		}
		public Database ()
		{

		}
	}
}

