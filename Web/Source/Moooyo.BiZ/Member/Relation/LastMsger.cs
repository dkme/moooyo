using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    //最后一次信息
    public class LastMsger : BiZ.Member.RelationMember
    {
        public LastMsger() { }
        //曾经由某方主动发起过聊天
        public bool HasTalk;
        public Activity.ActivityType Type;
        public bool FromMemberDeleted;
        public bool ToMemberDeleted;
        public List<UnRead> UnReads;
        public override String GetCollectionName() { return "LastMsger"; }
    }
    public class UnRead
    {
        public string SenderMid;
        public int UnReadCount;
    }
}
