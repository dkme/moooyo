using System;
using System.Data;
using System.Collections.Generic;
using Moooyo.App.Data;

namespace Moooyo.App.Core.Caches
{
	/// <summary>
	/// Cached image.
	/// </summary>
	public class CachedImage
	{
		public string Name{ get; set; }
		public string TimeStamp{ get; set; }
		public byte[] ImageData{ get; set; }

		public CachedImage ()
		{
		}
		public CachedImage (string filename)
		{
			//Defs SQL.
			string sql = "select * from CachedImage where Name=@Name";
					
			List<App.Data.SqlParam> sqlParams = new List<SqlParam> (){
				new SqlParam("@Name",filename,typeof(string))
			};

			//build sql
			sql = SqlBuilder.GetSql (sql, sqlParams);

			//Exec SQL
			IDataReader idr = App.Data.IDBOperationHelper.ExecuteReader (sql);
			while (idr.Read()) {
				this.Name = idr["Name"].ToString();
				this.TimeStamp = idr["TimeStamp"].ToString();
				this.ImageData = idr["ImageData"] as byte[];
			}
		}
		/// <summary>
		/// Save this instance.
		/// </summary>
		public bool Save ()
		{
			try {
				//Defs SQL.
				string sql = 
					@"insert into CachedImage
						(
							Name,
							TimeStamp,
							ImageData
						)
						values
						(
							@Name,
							@TimeStamp,
							@ImageData						
						)
					";
						
				List<App.Data.SqlParam> sqlParams = new List<SqlParam> (){
					new SqlParam("@Name",Name,typeof(string)),
					new SqlParam("@TimeStamp",DateTime.Now.ToString(),typeof(string)),
					new SqlParam("@ImageData",ImageData,typeof(string))
				};

				//build sql
				sql = SqlBuilder.GetSql (sql, sqlParams);

				//Exec SQL
				App.Data.IDBOperationHelper.ExecuteNonQuery (sql);

				return true;
			} 
			catch (Exception err) 
			{
				return false;
			}
		}
	}
}

