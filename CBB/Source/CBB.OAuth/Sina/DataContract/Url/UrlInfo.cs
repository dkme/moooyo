using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a short and long url mapping.
    /// </summary>
    [Serializable]
    [XmlRoot("url")]
    public class UrlInfo
    {
        /// <summary>
        /// Gets or sets the short url.
        /// </summary>
        [XmlElement("url_short")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// Gets or sets the long url.
        /// </summary>
        [XmlElement("url_long")]
        public string LongUrl { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [XmlElement("type")]
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the share count of the short url.
        /// </summary>
        [XmlElement("share_counts")]
        public int SharedCount { get; set; }

        /// <summary>
        /// Gets or sets the comment count of the short url.
        /// </summary>
        [XmlElement("comment_counts")]
        public int CommentCount { get; set; }
    }
}
