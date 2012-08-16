using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Core.DisplayObjs
{
    public class MsgerDisplayObj : RelationDisplayObj
    {
        public bool InFavor;
        public bool FavorMe;
        public int MsgBetweenMeCount;
        public List<BiZ.Core.Member.Relation.UnRead> UnReads; 

        public String Remark;
    }
}
