using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a hyperlink.
    /// </summary>
    [Serializable]
    [XmlRoot("a")]
    public class Hyperlink
    {
        /// <remarks/>
        [XmlAttribute("href")]
        public string Uri { get; set; }

        /// <remarks/>
        [XmlText]
        public string Text { get; set; }
    }
}
