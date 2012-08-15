///
/// 功能描述：超人活动的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/4/7
/// 附加信息：
///   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class SuperManModel : PageModelBase
    {
        public IList<IList<BiZ.InterestCenter.Interest>> nowsuperinterest;
        public Active.SuperMan.Super superobj;
        public BiZ.Member.Member memberobj;
        public List<SuperManModel> nowsuperobj;
        public List<SuperManModel> everydaysuperobj;
        public int nowsuperpagecount;
        public int nowsuperpageno;
        public int nowsuperpagesize;
        public int nowsupercount;
        public int everydaysuperpagecount;
        public int everydaysuperpageno;
        public int everydaysuperpagesize;
        public int everydaysupercount;
        public SuperManModel() { }
        public SuperManModel(
            IList<IList<BiZ.InterestCenter.Interest>> nowsuperinterest, 
            int nowsuperpagecount, 
            int nowsuperpageno, 
            int nowsuperpagesize, 
            int everydaysuperpagecount, 
            int everydaysuperpageno, 
            int everydaysuperpagesize)
        {
            this.nowsuperinterest = nowsuperinterest;
            this.nowsuperpagecount = nowsuperpagecount;
            this.nowsuperpageno = nowsuperpageno;
            this.nowsuperpagesize = nowsuperpagesize;
            this.everydaysuperpagecount = everydaysuperpagecount;
            this.everydaysuperpageno = everydaysuperpageno;
            this.everydaysuperpagesize = everydaysuperpagesize;
        }
        public SuperManModel(
            Active.SuperMan.Super superobj, 
            BiZ.Member.Member memberobj)
        {
            this.superobj = superobj;
            this.memberobj = memberobj;
        }
        public SuperManModel(List<SuperManModel> nowsuperobj, String text1)
        {
            this.nowsuperobj = nowsuperobj;
        }
        public SuperManModel(List<SuperManModel> everydaysuperobj, Boolean text2)
        {
            this.everydaysuperobj = everydaysuperobj;
        }
    }
}