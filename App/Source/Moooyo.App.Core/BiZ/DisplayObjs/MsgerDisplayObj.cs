using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.App.Core.BiZ.DisplayObjs
{
    public class MsgerDisplayObj : RelationDisplayObj
    {
        public bool InFavor;
        public bool FavorMe;
        public int MsgBetweenMeCount;
        public List<Core.BiZ.Member.Relation.UnRead> UnReads; 

        public String Remark;
    }
}
