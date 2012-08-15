using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of tag.
    /// </summary>
    [Serializable]
    [XmlRoot("tags")]
    public class Tags
    {
        private Collection<TagInfo> items = new Collection<TagInfo>();

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [XmlElement("tag")]
        public Collection<TagInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
