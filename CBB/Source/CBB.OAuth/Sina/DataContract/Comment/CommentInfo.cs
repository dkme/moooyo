using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a comment.
    /// </summary>
    [Serializable]
    [XmlRoot("comment")]
    public class CommentInfo
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
        /// Gets or sets the user who posts this comment.
        /// </summary>
        [XmlElement("user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the status which this comment comments to.
        /// </summary>
        [XmlElement("status")]
        public StatusInfo Status{ get; set; }
    }
}
