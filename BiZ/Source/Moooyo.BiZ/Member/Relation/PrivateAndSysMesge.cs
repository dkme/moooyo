/*************************************************
 * Functional description ：私信和系统信息
 * Author：Lau Tao
 * Modify the expansion：Lau Tao
 * Modified date：2012/6/12 Tuesday  
 * Remarks：
 * *********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Member.Relation
{
    public class PrivateAndSysMesge : LastMsger
    {
        /// <summary>
        /// 已读
        /// </summary>
        public bool Readed
        {
            get { return this.readed; }
            set { this.readed = value; }
        }
        private bool readed;
        /// <summary>
        /// 读取时间
        /// </summary>
        public DateTime ReadTime
        {
            get { return this.readTime; }
            set { this.readTime = value; }
        }
        private DateTime readTime;

        public PrivateAndSysMesge() { }
        public PrivateAndSysMesge(LastMsger lastMsger)
        {
            try
            {
                this._id = lastMsger._id;
                this.FromMember = lastMsger.FromMember;
                this.ToMember = lastMsger.ToMember;
                this.Comment = lastMsger.Comment;
                this.CreatedTime = lastMsger.CreatedTime;
                this.HasTalk = lastMsger.HasTalk;
                this.Type = lastMsger.Type;
                this.FromMemberDeleted = lastMsger.FromMemberDeleted;
                this.ToMemberDeleted = lastMsger.ToMemberDeleted;
                this.UnReads = lastMsger.UnReads;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
        public PrivateAndSysMesge(Sys.SystemMsg.SystemMsg sysMsg)
        {
            try
            {
                this._id = sysMsg._id;
                this.FromMember = sysMsg.FromMember;
                this.ToMember = sysMsg.ToMember;
                this.Comment = sysMsg.Comment;
                this.CreatedTime = sysMsg.CreatedTime;
                this.Readed = sysMsg.Readed;
                this.ReadTime = sysMsg.ReadTime;
            }
            catch (System.Exception err)
            {
                throw new CBB.ExceptionHelper.OperationException(
                    CBB.ExceptionHelper.ErrType.SystemErr,
                    CBB.ExceptionHelper.ErrNo.DBOperationError,
                    err);
            }
        }
    }
}
