///
/// 功能描述：系统状态的页面数据模型
/// 作   者：彭卓
/// 修改扩展者:彭卓
/// 修改日期：2012/3/31
/// 附加信息：
///  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models.PageModels
{
    public class SystemInfoModels : PageModelBase
    {
        public List<long> NewMembers;
        public List<long> NewInterests;
        public List<long> NewWenWens;
        public List<long> NewAnswers;
        public List<string> times;
        public string time, type;
        public SystemInfoModels() { }
        public SystemInfoModels(List<long> NewMembers, List<long> NewInterests, List<long> NewWenWens, List<long> NewAnswers, List<string> times)
        {
            this.NewMembers = NewMembers;
            this.NewInterests = NewInterests;
            this.NewWenWens = NewWenWens;
            this.NewAnswers = NewAnswers;
            this.times = times;
        }
    }
}