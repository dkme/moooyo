using System;
using System.Net;

namespace CBB.NetworkingHelper.HttpHelper
{
	/// <summary>
	/// data of httpRequest returned.
	/// </summary>
	public class APIReturnData
	{
		public string content;
		public CookieCollection cookies;
		public APIReturnData (string content)
		{
			this.content = content;
		}
		public APIReturnData(string content,CookieCollection cookies)
		{
			this.content = content;
			this.cookies = cookies;
		}
	}
}

