using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Defines types of status.
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// Represents all type of status.
        /// </summary>
        All = 0,
        /// <summary>
        /// Represents original type of status.
        /// </summary>
        Original = 1,
        /// <summary>
        /// Represents Picture type of status.
        /// </summary>
        Picture = 2,
        /// <summary>
        /// Represents Video type of status.
        /// </summary>
        Video = 3,
        /// <summary>
        /// Represents Music type of status.
        /// </summary>
        Music = 4
    }
}
