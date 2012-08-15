using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.WebUI.Models
{
    public class MsgerDisplayObj : RelationDisplayObj
    {
        public bool InFavor;
        public bool FavorMe;
        public int MsgBetweenMeCount;
        public List<BiZ.Member.Relation.UnRead> UnReads; 

        public String Remark;
    }
}
