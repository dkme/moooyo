using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the user ids.
    /// </summary>
    [Serializable]
    [XmlRoot("id_list")]
    public class UserIDs
    {
        private Collection<long> ids = new Collection<long>();

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [XmlArray("ids")]
        [XmlArrayItem("id")] // Use both XmlArray and XmlArrayItem to form desired the XML structure: <id_list><ids><id>....
        public Collection<long> IDs
        {
            get
            {
                return ids;
            }
        }

        /// <remarks/>
        [XmlElement("next_cursor")]
        public int NextCursor { get; set; }

        /// <remarks/>
        [XmlElement("previous_cursor")]
        public int PreviousCursor { get; set; }
    }
}
