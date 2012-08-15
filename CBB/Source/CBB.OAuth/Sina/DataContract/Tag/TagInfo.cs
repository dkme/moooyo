using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of tag.
    /// </summary>
    [Serializable]
    [XmlRoot("tag")]
    public class TagInfo
    {
        /// <summary>
        /// Gets or sets the tag id.
        /// </summary>
        [XmlElement("id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the tag value.
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }

    }
}
