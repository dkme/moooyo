using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the user suggestion info.
    /// </summary>
    [Serializable]
    public class UserSuggestionInfo
    {
        /// <remarks/>
        [XmlElement("uid")]
        public long ID { get; set; }

        /// <remarks/>
        [XmlElement("nickname")]
        public string ScreenName { get; set; }

        /// <remarks/>
        [XmlElement("remark")]
        public string Remark { get; set; }

    }
}
