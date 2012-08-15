using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Moooyo.BiZ.Member
{
    //用户搜索类别
    public class MemberSearch
    {
        public string Province;
        public int Sex;
        public HasPhotoType HasPhoto;
        public AgeType Age;
        public SearchType Type;

        public IMongoQuery GetSearchQueryObj()
        { 
            QueryComplete qc;
            QueryComplete qccity = null;
            if (Province != "全部") qccity = Query.Matches("MemberInfomation.City","^"+Province);
            QueryComplete qcsex = null;
            if (Sex!=-1)
                qcsex= Query.EQ("Sex", Sex);
            QueryComplete qcage=null;
            QueryComplete qcmasthaveicon = null;
            if (HasPhoto == HasPhotoType.True)
                qcmasthaveicon=Query.NE("MemberPhoto.IconID", "");
            if (HasPhoto == HasPhotoType.False)
                qcmasthaveicon = Query.EQ("MemberPhoto.IconID", "");
            QueryComplete qcfinishedreg = Query.NE("FinishedReg", false);

            DateTime thisyear = DateTime.Parse(DateTime.Now.Year.ToString()+"-1-1");
            if (Age != AgeType.All)
            {
                switch (Age)
                {
                    case  AgeType.Age18:
                        qcage = Query.And(Query.GTE("MemberInfomation.Birthday", thisyear.AddYears(-18)));
                        break;
                    case AgeType.Age18_23:
                        qcage = Query.And(Query.LTE("MemberInfomation.Birthday", thisyear.AddYears(-18)), Query.GTE("MemberInfomation.Birthday", thisyear.AddYears(-23)));
                        break;
                    case AgeType.Age22_28:
                        qcage = Query.And(Query.LTE("MemberInfomation.Birthday", thisyear.AddYears(-22)), Query.GTE("MemberInfomation.Birthday", thisyear.AddYears(-28)));
                        break;
                    case AgeType.Age26_32:
                        qcage = Query.And(Query.LTE("MemberInfomation.Birthday", thisyear.AddYears(-26)), Query.GTE("MemberInfomation.Birthday", thisyear.AddYears(-32)));
                        break;
                    case AgeType.Age30_38:
                        qcage = Query.And(Query.LTE("MemberInfomation.Birthday", thisyear.AddYears(-30)), Query.GTE("MemberInfomation.Birthday", thisyear.AddYears(-38)));
                        break;
                    case AgeType.Age36:
                        qcage = Query.And(Query.LTE("MemberInfomation.Birthday", thisyear.AddYears(-36)));
                        break;
                }
            }
            qc = Query.And(qccity, qcsex, qcage, qcmasthaveicon, qcfinishedreg);

            return qc;
        }
        public SortByBuilder GetOrderByObj()
        {
            SortByBuilder sb=null;
            switch (Type)
            {
                case SearchType.New:
                    sb= SortBy.Descending("LastOperationTime");
                    break;
            }
            return sb;
        }
    }
    public enum SearchType
    {
        All=1, //任意排序
        New=2, //较新的
        Audited=3, //认证的
        VIP=4 //VIP
    }
    public enum AgeType
    {
        All=1,
        Age18=20,
        Age18_23=30,
        Age22_28=40,
        Age26_32=50,
        Age30_38=60,
        Age36=70
    }
    public enum HasPhotoType
    {
        All = -1,
        True = 1,
        False = 0
    }
}
