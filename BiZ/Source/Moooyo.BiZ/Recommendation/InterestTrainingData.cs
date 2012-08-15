using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.RecommendationHelper;

namespace Moooyo.BiZ.Recommendation
{
    /// <summary>
    /// 用户对兴趣的喜好数据
    /// </summary>
    public class InterestTrainingData : TrainingData
    {
        //点击次数
        public int ClickTimes;
        //是否已加粉
        public bool IsFans;

        public InterestTrainingData() { }
        public InterestTrainingData(String memberid, String objectid, InterestTrainingDataType type)
        {
            //初始赋值
            this.MemberID = memberid;
            this.ObjectID = objectid;
            this.IsFans = false;
            this.Value = 0;
            this.ClickTimes = 0;

            //提取原有数据
            InterestTrainingData obj = TrainingDataProvider<InterestTrainingData>.GetTrainingData(this, memberid, objectid);
            if (obj != null)
            {
                this.ClickTimes = obj.ClickTimes;
                this._id = obj._id;
                this.Value = obj.Value;
                this.IsFans = obj.IsFans;
            }

            //如果已经达到最高分值，不需要继续增加分值
            if (Value >= 5) return;

            #region 根据InterestTrainingDataType来影响数据
            switch (type)
            {
                case InterestTrainingDataType.Click:
                    this.ClickTimes++;
                    //已加粉并超过5次点击，记5分
                    if (ClickTimes >= 5 && IsFans)
                    {
                        this.Value = 5;
                        break;
                    }
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
                case InterestTrainingDataType.AddToFansGroup:
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
                case InterestTrainingDataType.CreateWenWen:
                    //发布问问，记4分
                    if (this.Value >= 4) return;
                    this.Value = 4;
                    break;
                case InterestTrainingDataType.CreateInterest:
                    this.Value = 5;
                    this.IsFans = true;
                    break;
                default:
                    break;
            }
            #endregion

            TrainingDataProvider<InterestTrainingData>.SaveTrainingData(this);
        }

        public override String GetCollectionName() { return "InterestTrainingData"; }

        public override MongoDB.Driver.Builders.QueryComplete AddicitonalQuery()
        {
            return null;
        }

    }
    /// <summary>
    /// 兴趣喜好训练数据类型
    /// </summary>
    public enum InterestTrainingDataType
    {
        Click = 1,
        AddToFansGroup = 2,
        CreateWenWen = 3,
        CreateInterest = 4,
    }
}
