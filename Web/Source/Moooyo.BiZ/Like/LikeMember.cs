using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Like
{
    public class LikeMember
    {
        public String MemberID
        {
            get { return memberID; }
            set { memberID = value; }
        }
        //喜欢的用户ID
        private String memberID;
        public String MemberName
        {
            get { return memberName; }
            set { memberName = value; }
        }
        //喜欢的ID
        private String memberName;
        public String MemberIcon
        {
            get { return memberIcon; }
            set { memberIcon = value; }
        }
        //喜欢的ID
        private String memberIcon;
        //创建时间
        private DateTime createdTime;
        public DateTime CreatedTime
        {
            get { return createdTime; }
            set { createdTime = value; }
        }

        public LikeMember(String memberID, String memberName, String memberIcon)
        {
            this.memberID = memberID;
            this.memberName = memberName;
            this.memberIcon = memberIcon;
            this.createdTime = DateTime.Now;
        }
    }
}
