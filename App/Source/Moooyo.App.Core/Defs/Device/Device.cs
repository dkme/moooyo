using System;

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
		/// Gets or sets the unique ID.
		/// </summary>
		/// <value>
		/// The unique ID.
		/// </value>
		public string UniqueID{ get; set; }
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

