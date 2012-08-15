using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a geo.
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace="http://www.georss.org/georss")]
    public class Geo
    {
        /// <summary>
        /// Gets or sets the geo point.
        /// </summary>
        [XmlElement("point")]
        public GeoPoint Point { get; set; }
    }
}
