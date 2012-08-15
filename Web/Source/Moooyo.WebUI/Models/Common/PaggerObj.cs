using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moooyo.WebUI.Models
{
    /// <summary>
    /// 用于分页的对象
    /// </summary>
    public class PaggerObj
    {
        //总页数
        public int PageCount;
        //每页记录数
        public int PageSize;
        //页码
        public int PageNo;
        //页地址
        public String PageUrl;
        //附加参数
        public String AdditionParams;
    }
}