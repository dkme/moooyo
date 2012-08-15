using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the status info.
    /// </summary>
    [Serializable]
    [XmlRoot("status")]
    public class StatusInfo
    {
        /// <summary>
        /// Gets or sets the creation time of the status.
        /// </summary>
        [XmlElement("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the stutus id.
        /// </summary>
        [XmlElement("id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        [XmlElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the source of the status.
        /// </summary>
        [XmlElement("source")]
        public Source Source { get; set; }

        /// <summary>
        /// Gets or sets a boolead indicates whether the status is favarited.
        /// </summary>
        [XmlElement("favorited")]
        public bool Favorited { get; set; }

        /// <summary>
        /// Gets or sets a boolead indicates whether the status is truncated.
        /// </summary>
        [XmlElement("truncated")]
        public bool Truncated { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [XmlElement("in_reply_to_status_id")]
        public string ReplyTo { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [XmlElement("in_reply_to_user_id")]
        public string ReplyToUserId { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [XmlElement("in_reply_to_screen_name")]
        public string ReplyToUserScreenName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail_pic.
        /// </summary>
        [XmlElement("thumbnail_pic")]
        public string ThumbnailPic { get; set; }

        /// <summary>
        /// Gets or sets the bmiddle_pic.
        /// </summary>
        [XmlElement("bmiddle_pic")]
        public string MiddlePic { get; set; }

        /// <summary>
        /// Gets or sets the original_pic.
        /// </summary>
        [XmlElement("original_pic")]
        public string OriginalPic { get; set; }

        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        [XmlElement("mid")]
        public string Mid { get; set; }

        /// <summary>
        /// Gets or sets the user who posts this status.
        /// </summary>
        [XmlElement("user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the user who posts this status.
        /// </summary>
        [XmlElement("geo")]
        public Geo Geo { get; set; }

        /// <summary>
        /// Gets or sets the status that current status is reposted with.
        /// </summary>
        [XmlElement("retweeted_status")]
        public StatusInfo RetweetedStatus { get; set; }
    }
}
