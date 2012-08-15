using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the unread counters info.
    /// </summary>
    [Serializable]
    [XmlRoot("count")]
    public class UnreadInfo
    {
        /// <remarks/>
        [XmlElement("new_status")]
        public int? NewStatus { get; set; }

        /// <remarks/>
        [XmlElement("dm")]
        public int Messages { get; set; }

        /// <remarks/>
        [XmlElement("followers")]
        public int Followers { get; set; }

        /// <remarks/>
        [XmlElement("mentions")]
        public int Mentions { get; set; }

        /// <remarks/>
        [XmlElement("comments")]
        public int Comments { get; set; }

    }
}
