using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of counters.
    /// </summary>
    [Serializable]
    [XmlRoot("counts")]
    public class Counters
    {
        private Collection<CounterInfo> items = new Collection<CounterInfo>();

        /// <summary>
        /// Gets the counter.
        /// </summary>
        [XmlElement("count")]
        public Collection<CounterInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
