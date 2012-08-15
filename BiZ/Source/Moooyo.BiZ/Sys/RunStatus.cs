using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moooyo.BiZ.Sys
{
    /// <summary>
    /// 运行时状态
    /// </summary>
    public class RunStatus
    {
        #region 版本管理
        //js
        public static String JsVersion {
            get {
                if (jsversion == null) jsversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("JsVersion");
                return jsversion;
            }
            set {
                jsversion = value;
            }
        }
        private static String jsversion;
        public static String JsDebugVersion
        {
            get
            {
                if (jsdebugversion == null) jsdebugversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("JsDebugVersion");
                return jsdebugversion;
            }
            set
            {
                jsdebugversion = value;
            }
        }
        private static String jsdebugversion;
        //image
        public static String ImageVersion
        {
            get
            {
                if (imageversion == null) imageversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ImageVersion");
                return jsversion;
            }
            set
            {
                imageversion = value;
            }
        }
        private static String imageversion;
        public static String ImageDebugVersion
        {
            get
            {
                if (imagedebugversion == null) imagedebugversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("ImageDebugVersion");
                return imagedebugversion;
            }
            set
            {
                imagedebugversion = value;
            }
        }
        private static String imagedebugversion;
        //css
        public static String CSSVersion
        {
            get
            {
                if (cssversion == null) cssversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("CSSVersion");
                return cssversion;
            }
            set
            {
                cssversion = value;
            }
        }
        private static String cssversion;
        public static String CSSDebugVersion
        {
            get
            {
                if (cssdebugversion == null) cssdebugversion = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("CSSDebugVersion");
                return cssdebugversion;
            }
            set
            {
                cssdebugversion = value;
            }
        }
        private static String cssdebugversion;
        //debugpassword
        public static String DebugPassword
        {
            get
            {
                if (debugpassword == null) debugpassword = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("DebugPassword");
                return debugpassword;
            }
            set
            {
                debugpassword = value;
            }
        }
        private static String debugpassword;
        #endregion

    }
}
