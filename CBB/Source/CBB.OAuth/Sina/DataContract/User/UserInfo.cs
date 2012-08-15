using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

namespace CBB.OAuth.Sina.DataContract
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    [Serializable]
    [XmlRoot("user")]
    [DebuggerDisplay("User:{ScreenName}")]
    public class UserInfo
    {
        /// <remarks/>
        [XmlElement("id")]
        public long ID { get; set; }

        /// <remarks/>
        [XmlElement("screen_name")]
        public string ScreenName { get; set; }

        /// <remarks/>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the markup(note) you set for this user.
        /// </summary>
        [XmlElement("define_as")]
        public string DefineAs { get; set; }

        /// <remarks/>
        [XmlElement("province")]
        public string Province { get; set; }

        /// <remarks/>
        [XmlElement("city")]
        public string City { get; set; }

        /// <remarks/>
        [XmlElement("location")]
        public string Location { get; set; }

        /// <remarks/>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <remarks/>
        [XmlElement("url")]
        public string Url { get; set; }

        /// <remarks/>
        [XmlElement("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        /// <remarks/>
        [XmlElement("domain")]
        public string Domain { get; set; }

        /// <remarks/>
        [XmlElement("gender")]
        public string Gender { get; set; }

        /// <remarks/>
        [XmlElement("followers_count")]
        public int FollowersCount { get; set; }

        /// <remarks/>
        [XmlElement("friends_count")]
        public int FriendsCount { get; set; }

        /// <remarks/>
        [XmlElement("statuses_count")]
        public int StatusesCount { get; set; }

        /// <remarks/>
        [XmlElement("favourites_count")]
        public int FavouritesCount { get; set; }

        /// <remarks/>
        [XmlElement("created_at")]
        public string CreatedAt { get; set; }

        /// <remarks/>
        [XmlElement("geo_enabled")]
        public bool GeoEnabled { get; set; }

        /// <remarks/>
        [XmlElement("allow_all_act_msg")]
        public bool AllowAllActMsg { get; set; }

        /// <remarks/>
        [XmlElement("following")]
        public bool Following { get; set; }

        /// <remarks/>
        [XmlElement("verified")]
        public bool Verified { get; set; }

        /// <remarks/>
        [XmlElement("status")]
        public StatusInfo LatestStatus { get; set; }

    }
}
