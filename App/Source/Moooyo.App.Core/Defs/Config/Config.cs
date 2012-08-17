using System;

namespace Moooyo.App.Core.Defs
{
	public class Config
	{
		public const string MajorVersion = "0";
		public const string SubVersion = "1";
		public static string FullVersion {
			get{
				return "V"+MajorVersion+"."+SubVersion;
			}}
		public const string DatabaseName = "moooyoAppDBV01.db";
		public Config ()
		{
		}
	}
}

