using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a counter info of a status.
    /// </summary>
    [Serializable]
    public class CounterInfo
    {
        /// <remarks/>
        [XmlElement("id")]
        public long StatusID { get; set; }

        /// <remarks/>
        [XmlElement("comments")]
        public int Comments { get; set; }

        /// <remarks/>
        [XmlElement("rt")]
        public int Forwards { get; set; }
    }
}
