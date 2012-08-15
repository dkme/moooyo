using System;

namespace Moooyo.App.Core.Defs.App
{
	/// <summary>
	/// App version.
	/// </summary>
	public class Version
	{
		/// <summary>
		/// Gets or sets the major version.
		/// </summary>
		/// <value>
		/// The major version.
		/// </value>
		public string MajorVersion{get;set;}
		/// <summary>
		/// Gets or sets the sub version.
		/// </summary>
		/// <value>
		/// The sub version.
		/// </value>
		public string SubVersion{get;set;}
		/// <summary>
		/// Gets the get version.
		/// </summary>
		/// <value>
		/// Get app total version.
		/// </value>
		public string GetVersion {
			get{return MajorVersion+"."+SubVersion;}
		}
		public Version (){}
		public Version (string MajorVersion,string SubVersion)
		{
			this.MajorVersion= MajorVersion;
			this.SubVersion = SubVersion;
		}
	}
}

