using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a relationship part.
    /// </summary>
    [Serializable]
    public class RelationshipPart
    {
        /// <remarks/>
        [XmlElement("id")]
        public long UserID { get; set; }

        /// <remarks/>
        [XmlElement("screen_name")]
        public string ScreenName { get; set; }

        /// <remarks/>
        [XmlElement("following")]
        public bool Following { get; set; }

        /// <remarks/>
        [XmlElement("followed_by")]
        public bool FollowedBy { get; set; }

        /// <remarks/>
        [XmlElement("notifications_enabled")]
        public bool NotificationsEnabled { get; set; }

    }
}
