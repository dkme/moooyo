using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a trend(topic).
    /// </summary>
    [Serializable]
    [XmlRoot("userTrend")]
    public class TrendInfo
    {
        /// <summary>
        /// Gets or sets the trend id.
        /// </summary>
        [XmlElement("trend_id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the hotword of the trend.
        /// </summary>
        [XmlElement("hotword")]
        public string HotWord { get; set; }

        /// <summary>
        /// Gets or sets the number of hits the trend has.
        /// </summary>
        [XmlElement("num")]
        public long Hits { get; set; }

    }

    /// <summary>
    /// Represents a trend(topic).
    /// </summary>
    [Serializable]
    [XmlRoot("trend")]
    public class PeriodTrendInfo
    {
        /// <summary>
        /// Gets or sets the name of the trend.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the query of the trend.
        /// </summary>
        [XmlElement("query")]
        public string Query { get; set; }
    }
}
