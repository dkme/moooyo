using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a set of user suggestion.
    /// </summary>
    [Serializable]
    [XmlRoot("suggestions")]
    public class UserSuggestions
    {
        private Collection<UserSuggestionInfo> items = new Collection<UserSuggestionInfo>();

        /// <summary>
        /// Gets the suggestions.
        /// </summary>
        [XmlElement("suggestion")]
        public Collection<UserSuggestionInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
