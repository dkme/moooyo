using System;
using System.Net;
using MonoTouch.Foundation;

namespace Moooyo.App.Core.Defs.Device
{
	/// <summary>
	/// Device def.
	/// </summary>
	public class DeviceDef
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public DeviceType Type { get; set; }
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public string Version{ get; set; }
		/// <summary>
		/// Gets the unique ID.
		/// </summary>
		/// <value>
		/// The unique ID.
		/// </value>
		public string UniqueID {
			get{
				return GetDeviceID();
			}}
		/// <summary>
		/// Create GUID of the Device;
		/// </summary>
		public static string KEY_DEVICEID = "moooyoDeviceUnquieID";
		public static string GetDeviceID ()
		{
			string result = NSUserDefaults.StandardUserDefaults.StringForKey (KEY_DEVICEID);
			if (string.IsNullOrEmpty (result)) {
				result = Guid.NewGuid().ToString();
				NSUserDefaults.StandardUserDefaults.SetString(result,KEY_DEVICEID);
			}
			return result;
		}
	}
	/// <summary>
	/// Mobile DeviceType Defs.
	/// </summary>
	public enum DeviceType
	{
		iphone=1,
		ipad=2,
		android=3,
		androidpad=4
	}
}

