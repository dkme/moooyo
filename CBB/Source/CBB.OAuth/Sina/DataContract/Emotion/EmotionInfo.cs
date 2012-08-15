using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the emotion info.
    /// </summary>
    [Serializable]
    [XmlRoot("emotion")]
    public class EmotionInfo
    {
        /*
            <phrase>[嘻嘻]</phrase>
            <type>image</type>
            <url>http://timg.sjs.sinajs.cn/miniblog2style/images/common/face/ext/normal/c2/tooth.gif</url>
            <is_hot>false</is_hot>
            <is_common>true</is_common>
            <order_number>96</order_number>\
            <category>表情</category>
         */

        /// <summary>
        /// Gets or sets the phrase represents the emotion.
        /// </summary>
        [XmlElement("phrase")]
        public string Phrase { get; set; }

        /// <summary>
        /// Gets or sets the type of the emotion.
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the url of the emotion.
        /// </summary>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether the emotion is popular.
        /// </summary>
        [XmlElement("is_hot")]
        public bool IsHot { get; set; }

        /// <summary>
        /// Gets or sets a boolean value indicating whether the emotion is common.
        /// </summary>
        [XmlElement("is_common")]
        public bool IsCommon { get; set; }

        /// <summary>
        /// Gets or sets order numer of the emotion.
        /// </summary>
        [XmlElement("order_number")]
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the category of the emotion.
        /// </summary>
        [XmlElement("category")]
        public string Category { get; set; }
    }
}
