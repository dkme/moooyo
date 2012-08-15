using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of tag ids.
    /// </summary>
    [Serializable]
    [XmlRoot("tagids")]
    public class TagIDs
    {
        private Collection<long> items = new Collection<long>();

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [XmlElement("tagid")]
        public Collection<long> Items
        {
            get
            {
                return items;
            }
        }
    }
}
