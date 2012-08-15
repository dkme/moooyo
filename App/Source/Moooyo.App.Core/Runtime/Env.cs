using System;
using System.Net;

namespace Moooyo.App.Core.Runtime
{
	public class Env
	{
		/// <summary>
		/// The runner device.
		/// </summary>
		public static Moooyo.App.Core.Defs.Device.DeviceDef RunnerDevice;
		/// <summary>
		/// The runner version.
		/// </summary>
		public static Moooyo.App.Core.Defs.App.Version RunnerVersion;
		/// <summary>
		/// The cookies.
		/// </summary>
		public static CookieCollection Cookies;
	}
}

