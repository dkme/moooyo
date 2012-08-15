using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents the type of counters.
    /// </summary>
    [Serializable]
    public enum CounterType
    {
        /// <summary>
        /// Represents comment counter.
        /// </summary>
        Comment,
        /// <summary>
        /// Represents mention counter.
        /// </summary>
        Mention,
        /// <summary>
        /// Represents message counter.
        /// </summary>
        Message,
        /// <summary>
        /// Represents follower counter.
        /// </summary>
        Follower
    }
}
