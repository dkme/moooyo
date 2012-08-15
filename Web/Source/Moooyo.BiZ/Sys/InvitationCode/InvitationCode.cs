/*******************************************************************
 * Functional description ：邀请码数据实体
 * Author：Lau Tao
 * Modify the expansion：Tao Lau 
 * Modified date：2012/7/3 Tuesday 
 * Remarks：
 * ****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace Moooyo.BiZ.Sys.InvitationCode
{
    /// <summary>
    /// 邀请码数据实体
    /// </summary>
    public class InvitationCode
    {
        /// <summary>
        /// 唯一编号
        /// </summary>
        public String ID
        {
            get { if (_id != null)return _id.ToString(); else return ""; }
        }
        [System.Web.Script.Serialization.ScriptIgnore]
        public ObjectId _id;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get { return this.createdTime; }
            set { this.createdTime = value; }
        }
        private DateTime createdTime;
        /// <summary>
        /// 邀请码
        /// </summary>
        public int InviteCode
        {
            get { return this.inviteCode; }
            set { this.inviteCode = value; }
        }
        private int inviteCode;
        /// <summary>
        /// 使用者
        /// </summary>
        public Creater.Creater UsedMember
        {
            get { return this.usedMember; }
            set { this.usedMember = value; }
        }
        private Creater.Creater usedMember;
        /// <summary>
        /// 生成者
        /// </summary>
        public Creater.Creater GeneratedMember
        {
            get { return this.generatedMember; }
            set { this.generatedMember = value; }
        }
        private Creater.Creater generatedMember;
        /// <summary>
        /// 生成者编码
        /// </summary>
        public string GeneratedMemberId
        {
            get { return this.generatedMemberId; }
            set { this.generatedMemberId = value; }
        }
        private string generatedMemberId;
        /// <summary>
        /// 是否使用
        /// </summary>
        public Comm.UsedFlag UsedFlag
        {
            get { return this.usedFlag; }
            set { this.usedFlag = value; }
        }
        private Comm.UsedFlag usedFlag;
        
        /// <summary>
        /// 表（集合）名
        /// </summary>
        /// <returns></returns>
        public static String GetCollectionName()
        {
            return "InvitationCode";
        }

        public InvitationCode(int inviteCode, String usedMember, String generatedMember, string generatedMemberId, Comm.UsedFlag usedFlag)
        {
            this.CreatedTime = DateTime.Now;
            this.InviteCode = inviteCode;
            if (usedMember != null && usedMember != "")
                this.UsedMember = new Creater.Creater(usedMember);
            else
                this.UsedMember = null;
            if (generatedMember != null && generatedMember != "" && generatedMember != "4eb0fde42101b0824e2b018f") //不等于管理员
                this.GeneratedMember = new Creater.Creater(generatedMember);
            else
                this.GeneratedMember = null;
            this.GeneratedMemberId = generatedMemberId;
            this.UsedFlag = usedFlag;
        }
    }
}
