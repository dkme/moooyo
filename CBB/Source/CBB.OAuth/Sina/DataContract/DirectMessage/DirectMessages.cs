using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a set of direct message.
    /// </summary>
    [Serializable]
    [XmlRoot("direct-messages")]
    public class DirectMessages
    {
        private Collection<DirectMessageInfo> items = new Collection<DirectMessageInfo>();

        /// <summary>
        /// Gets the direct messages.
        /// </summary>
        [XmlElement("direct_message")]
        public Collection<DirectMessageInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
