using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the relationship info of two users.
    /// </summary>
    [Serializable]
    [XmlRoot("relationship")]
    public class RelationshipInfo
    {
        /// <remarks/>
        [XmlElement("source")]
        public RelationshipPart Source { get; set; }

        /// <remarks/>
        [XmlElement("target")]
        public RelationshipPart Target { get; set; }
    }
}
