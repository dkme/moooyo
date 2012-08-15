using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a set of status.
    /// </summary>
    [Serializable]
    [XmlRoot("statuses")]
    public class Statuses 
    {
        private Collection<StatusInfo> items = new Collection<StatusInfo>();

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        [XmlElement("status")]
        public Collection<StatusInfo> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Gets or sets the total count. This property is valid only in SearchStatuses API. 
        /// </summary>
        [XmlElement("total_count_maybe")]
        public int TotalCount { get; set; }
    }
}
