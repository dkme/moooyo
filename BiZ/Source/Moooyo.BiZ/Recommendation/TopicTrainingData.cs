/*************************************************
 * Functional description ：兴趣下话题训练数据提供类
 * Author：Tao Lau
 * Modify the expansion：Tao Lau
 * Modified date：2012/4/18 Wednesday  
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CBB.RecommendationHelper;

namespace Moooyo.BiZ.Recommendation
{
    /// <summary>
    /// 用户对兴趣下话题的喜爱的数据
    /// </summary>
    public class TopicTrainingData : TrainingData
    {
        //被喜欢次数
        public int LikeCount;
        //被管理员喜欢次数
        public int AdminLikeCount;

        public TopicTrainingData() { }
        public TopicTrainingData(String memberId, String objectId, TopicTrainingDataType type)
        {
            //初始赋值
            this.MemberID = memberId;
            this.ObjectID = objectId;
            this.Value = 0;
            this.LikeCount = 0;
            this.AdminLikeCount = 0;

            //提取原有数据
            TopicTrainingData topicTrainingData = TrainingDataProvider<TopicTrainingData>.GetTrainingData(this, memberId, objectId);
            if (topicTrainingData != null)
            {
                this.LikeCount = topicTrainingData.LikeCount;
                this.AdminLikeCount = topicTrainingData.AdminLikeCount;
                this._id = topicTrainingData._id;
                this.Value = topicTrainingData.Value;
            }

            //如果已经达到最高分值，不需要继续增加分值
            if (Value >= 5) return;

            #region 根据TopicTrainingData来影响数据
            switch (type)
            {
                case TopicTrainingDataType.Like:
                    //递增被喜欢次数
                    this.LikeCount++;
                    //被喜欢，记1分
                    this.Value = 1;
                    break;
                case TopicTrainingDataType.AdminLike:
                    //递增被管理员喜欢次数
                    this.AdminLikeCount++;
                    //被管理员喜欢，记2分
                    this.Value = 2;
                    break;
                default:
                    break;
            }
            #endregion

            TrainingDataProvider<TopicTrainingData>.SaveTrainingData(this);
        }

        public override String GetCollectionName() { return "TopicTrainingData"; }

        public override MongoDB.Driver.Builders.QueryComplete AddicitonalQuery()
        {
            return null;
        }
    }

    /// <summary>
    /// 兴趣下话题喜爱训练数据类型
    /// </summary>
    public enum TopicTrainingDataType
    {
        Like = 1,
        AdminLike = 2
    }
}
