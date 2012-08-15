using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.RecommendationHelper;

namespace Moooyo.BiZ.Recommendation
{
    /// <summary>
    /// 用户对用户的喜好训练数据
    /// </summary>
    public class MemberFavorTrainingData : TrainingData
    {
        //点击次数
        public int ClickTimes;
        //是否已加粉
        public bool IsFans;
        //主动互动次数
        public int CallTimes;
        //源用户和目标用户是否相同性别
        public bool SameSex;
        //源用户和目标用户是否相同地区
        public bool SameZone;

        public MemberFavorTrainingData() { }
        public MemberFavorTrainingData(String memberid, String tomemberid, MemberFavorTrainingDataType type)
        {
            //初始赋值
            this.MemberID = memberid;
            this.ObjectID = tomemberid;
            this.IsFans = false;
            this.Value = 0;
            this.ClickTimes = 0;
            this.CallTimes = 0;
            this.SameSex = false;
            this.SameZone = false;

            //提取原有数据
            MemberFavorTrainingData obj = TrainingDataProvider<MemberFavorTrainingData>.GetTrainingData(this, memberid, tomemberid);
            if (obj != null)
            {
                this.ClickTimes = obj.ClickTimes;
                this.CallTimes = obj.CallTimes;
                this._id = obj._id;
                this.Value = obj.Value;
                this.IsFans = obj.IsFans;
                this.SameSex = obj.SameSex;
                this.SameZone = obj.SameZone;
            }
            else
            {
                Member.Member mym = BiZ.MemberManager.MemberManager.GetMember(memberid);
                Member.Member tomym = BiZ.MemberManager.MemberManager.GetMember(tomemberid);

                if (mym.Sex == tomym.Sex) this.SameSex = true;
                if (mym.MemberInfomation.City == tomym.MemberInfomation.City) this.SameZone = true;
            }

            //如果已经达到最高分值，不需要继续增加分值
            if (Value >= 5) return;

            #region 根据InterestTrainingDataType来影响数据
            switch (type)
            {
                case MemberFavorTrainingDataType.Click:
                    this.ClickTimes++;
                    //重复点击，记2分
                    if (ClickTimes > 2 & this.Value < 2)
                    {
                        this.Value = 2;
                        break;
                    }
                    //点击，记1分
                    if (ClickTimes > 0 & this.Value < 1)
                    {
                        this.Value = 1;
                        break;
                    }
                    break;
                case MemberFavorTrainingDataType.AddToFansGroup:
                    this.IsFans = true;
                    //已加粉并超过5次点击，记5分
                    if (ClickTimes >= 5)
                    {
                        this.Value = 5;
                    }
                    else if (this.Value < 3)
                    {
                        //加粉，记3分
                        this.Value = 3;
                    }
                    break;
                case MemberFavorTrainingDataType.Call:
                    this.CallTimes++;
                    //主动发起互动，记3分
                    if (this.Value < 3) this.Value = 3;
                    if (this.CallTimes >= 5) this.Value = 5;
                    break;
                default:
                    break;
            }
            #endregion

            TrainingDataProvider<MemberFavorTrainingData>.SaveTrainingData(this);
        }

        public override String GetCollectionName() { return "MemberTrainingData"; }

        public override MongoDB.Driver.Builders.QueryComplete AddicitonalQuery()
        {
            MongoDB.Driver.Builders.QueryComplete qc =
                MongoDB.Driver.Builders.Query.And(
                MongoDB.Driver.Builders.Query.EQ("SameSex", false),
                MongoDB.Driver.Builders.Query.EQ("SameZone", true)
                );

            return qc;
        }
    }
    /// <summary>
    /// 用户对用户的喜好训练数据类型
    /// </summary>
    public enum MemberFavorTrainingDataType
    {
        Click = 1,
        AddToFansGroup = 2,
        Call = 3
    }
}
