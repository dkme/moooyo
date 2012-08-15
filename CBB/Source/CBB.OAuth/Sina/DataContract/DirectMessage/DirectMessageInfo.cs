using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the direct message.
    /// </summary>
    [Serializable]
    public class DirectMessageInfo
    {
        /// <summary>
        /// Gets or sets the creation time of the direct message.
        /// </summary>
        [XmlElement("created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the direct message id.
        /// </summary>
        [XmlElement("id")]
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the direct message's text.
        /// </summary>
        [XmlElement("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the sender ID of the direct message.
        /// </summary>
        [XmlElement("sender_id")]
        public long SenderID { get; set; }

        /// <summary>
        /// Gets or sets the receiver ID of the direct message.
        /// </summary>
        [XmlElement("recipient_id")]
        public long RecipientID { get; set; }

        /// <summary>
        /// Gets or sets the sender's screen name.
        /// </summary>
        [XmlElement("sender_screen_name")]
        public string SenderScreenName { get; set; }

        /// <summary>
        /// Gets or sets the receiver's screen name.
        /// </summary>
        [XmlElement("recipient_screen_name")]
        public string RecipientScreenName { get; set; }

        /// <summary>
        /// Gets or sets the sender of the direct message.
        /// </summary>
        [XmlElement("sender")]
        public UserInfo Sender { get; set; }

        /// <summary>
        /// Gets or sets the recipient of the direct message.
        /// </summary>
        [XmlElement("recipient")]
        public UserInfo Recipient { get; set; }
    }
}
