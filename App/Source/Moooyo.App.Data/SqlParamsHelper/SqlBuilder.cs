using System;
using System.Data;
using System.Collections.Generic;

namespace Moooyo.App.Data
{
	public class SqlParam
	{
		public string Name{ get; set; }
		public object Value{ get; set; }
		public Type TypeOfValue { get;set; }
		public SqlParam (string name, object value, Type typeOfValue)
		{
			this.Name = name;
			this.Value = value;
			this.TypeOfValue = typeOfValue;
		}
		public string GetValue ()
		{
			switch (TypeOfValue.ToString()) {
				case "string":
					return "'"+Value.ToString()+"'";
				case "String":
					return "'"+Value.ToString()+"'";
				case "bool":
					if ((Boolean)Value) return "1";
					else return "0";
				case "Boolean":
					if ((Boolean)Value) return "1";
					else return "0";
				case "DateTime":
					return "'"+Value.ToString()+"'";
				default:
					return Value.ToString();
			}
		}
	} 
	public class SqlBuilder
	{
		public static string GetSql (string orginalSQL, List<SqlParam> sqlParams)
		{
			foreach (SqlParam p in sqlParams) {
				orginalSQL = orginalSQL.Replace(p.Name.Trim(),p.GetValue());
			}
			return orginalSQL;
		}
	}
}

