using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of urls.
    /// </summary>
    [Serializable]
    [XmlRoot("urls")]
    public class Urls
    {
        private Collection<UrlInfo> urls = new Collection<UrlInfo>();

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [XmlElement("url")]
        public Collection<UrlInfo> Items
        {
            get
            {
                return urls;
            }
        }
    }
}
