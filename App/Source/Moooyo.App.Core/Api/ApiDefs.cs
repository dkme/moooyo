using System;

namespace Moooyo.App.Core.Api
{
	public class CommDefs
	{
		public static string RootUri{get{
				return "http://AppApiV1.moooyo.com";
			}}
	}
	public class AccountsDefs
	{
		public static string CreateStep1{get{
				return CommDefs.RootUri+"/accounts/createStep1";
			}}

		public AccountsDefs ()
		{
		}
	}
}

