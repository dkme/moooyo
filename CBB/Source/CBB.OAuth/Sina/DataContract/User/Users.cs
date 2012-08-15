using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of users.
    /// </summary>
    [Serializable]
    [XmlRoot("users")]
    public class Users 
    {
        private Collection<UserInfo> items = new Collection<UserInfo>();

        /// <summary>
        /// Gets the users.
        /// </summary>
        [XmlElement("user")]
        public Collection<UserInfo> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Gets or sets the next page cursor.
        /// </summary>
        [XmlElement("next_cursor")]
        public int NextCursor { get; set; }

        /// <summary>
        /// Gets or sets the previous page cursor.
        /// </summary>
        [XmlElement("previous_cursor")]
        public int PreviousCursor { get; set; }
    }
}
