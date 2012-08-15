using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents an error response from remote server.
    /// </summary>
    [Serializable]
    [XmlRoot("hash")]
    public class ErrorResponse
    {
        /// <remarks/>
        [XmlElement("request")]
        public string Uri { get; set; }

        /// <remarks/>
        [XmlElement("error_code")]
        public int ErrorCode { get; set; }

        /// <remarks/>
        [XmlElement("error")]
        public string ErrorMessage { get; set; }
    }
}
