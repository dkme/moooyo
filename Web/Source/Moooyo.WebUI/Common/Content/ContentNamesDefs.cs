using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Common.Content
{
    /// <summary>
    /// 获取内容类别名称和喜欢类别名称
    /// </summary>
    /// <returns></returns>
    public class ContentNamesDefs{
        private static TypeNameAndLikeNameModel obj = null;
        public static TypeNameAndLikeNameModel GetDefs(){
            if (obj!=null) return obj;
            obj = new TypeNameAndLikeNameModel();
            obj.interestTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InterestType").Split('|').ToList();
            obj.memberTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MemberType").Split('|').ToList();
            obj.imageTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ImageType").Split('|').ToList();
            obj.suisuinianTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("SuiSuiNianType").Split('|').ToList();
            //obj.iwantTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("IWantType").Split('|').ToList();
            //obj.moodTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("MoodType").Split('|').ToList();
            obj.callforTypes = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("CallForType").Split('|').ToList();
            return obj;
        }
    }
    public class TypeNameAndLikeNameModel
    {
        public List<string> interestTypes;
        public List<string> memberTypes;
        public List<string> imageTypes;
        public List<string> suisuinianTypes;
        //public List<string> iwantTypes;
        //public List<string> moodTypes;
        public List<string> callforTypes;
        public TypeNameAndLikeNameModel() { }
    }
}