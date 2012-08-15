using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moooyo.BiZ.Member.MemberSkin;

namespace Moooyo.WebUI.Models.PageModels.Setting
{
    public class MemberSkinModel : MemberPageModel
    {
        public IList<MemberSkin> memberSkinList;

        public MemberSkinModel()
        {
        }
        public MemberSkinModel(
            MemberFullDisplayObj memberFullDisplayObj,
            IList<MemberSkin> memberSkinList)
        {
            this.Member = memberFullDisplayObj;
            this.memberSkinList = memberSkinList;
        }
    }
}