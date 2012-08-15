using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a group of emotions.
    /// </summary>
    [Serializable]
    [XmlRoot("emotions")]
    public class Emotions
    {
        private Collection<EmotionInfo> emotions = new Collection<EmotionInfo>();

        /// <summary>
        /// Gets the emotions.
        /// </summary>
        [XmlElement("emotion")]
        public Collection<EmotionInfo> Items
        {
            get
            {
                return emotions;
            }
        }
    }
}
