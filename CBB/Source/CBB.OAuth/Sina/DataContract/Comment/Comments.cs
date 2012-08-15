using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a set of comment.
    /// </summary>
    [Serializable]
    [XmlRoot("comments")]
    public class Comments 
    {
        private Collection<CommentInfo> items = new Collection<CommentInfo>();

        /// <summary>
        /// Gets the comments.
        /// </summary>
        [XmlElement("comment")]
        public Collection<CommentInfo> Items
        {
            get
            {
                return items;
            }
        }
    }
}
