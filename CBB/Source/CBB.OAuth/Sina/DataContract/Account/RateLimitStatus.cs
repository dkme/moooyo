using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the API rate limit status.
    /// </summary>
    [Serializable]
    [XmlRoot("rate-limit-status")]
    public class RateLimitStatus
    {
        /*
         <?xml version="1.0" encoding="UTF-8"?><hash><remaining-hits type="integer">1000</remaining-hits><hourly-limit type="integer">1000</hourly-limit><reset-time-in-seconds type="integer">1539</reset-time-in-seconds><reset-time type="datetime">Sat Aug 27 10:00:00 +0800 2011</reset-time></hash>
         */

        /// <remarks/>
        [XmlElement("remaining-hits")]
        public int RemainingHits { get; set; }

        /// <remarks/>
        [XmlElement("hourly-limit")]
        public int HourlyHits { get; set; }

        /// <remarks/>
        [XmlElement("reset-time-in-seconds")]
        public int ResetTimeInSeconds { get; set; }

        /// <remarks/>
        [XmlElement("reset-time")]
        public string ResetTime { get; set; }
    }
}
