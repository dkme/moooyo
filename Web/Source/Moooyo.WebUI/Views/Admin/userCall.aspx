<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Admin_TopNav"><% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%> </div>
<div class="Admin_Dic_LeftNav">
    <ul  class="leftlist magT10">
    <li><a href="javascript:undefined;" onclick="rebind('jb')">举报</a></li>
    <li><a href="javascript:undefined;" onclick="rebind('jy')">建议</a></li>
    <li><a href="javascript:undefined;" onclick="rebind('yj')">bug</a></li>
    </ul>
</div>
<div class="content magT10">
类别：
   <select id="isaudited" name="isaudited" tabindex="10" onchange="bind()">
        <option value="1" selected="1">未处理</option>
        <option value="2">已处理</option>
   </select>
<ul class="iwantlist magT10" id="listcontiner"></ul>

</div>
<br />
<div id="pager" class="verifyPager" ></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<%--<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>--%>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/admin.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
$(document).ready(function(){
    settype("jb");
    bind();
});
function rebind(_type) {
    settype(_type);
    bind();
 }
function bind()
{
    if (type == "jb") {
        count();
        setPager();
        bindjbcontent(getisaudited());
    }
    if (type == "jy") {
        count();
        setPager();
        bindjycontent(getisaudited());
    }
    if (type == "yj") {
        count();
        setPager();
        bindyjcontent(getisaudited());
    }
}
function getisaudited()
{
    if ($("#isaudited").val()=="1")
        return false;
    else
        return true;
}
function bindjbcontent(isautited) {
    AdminProvider.getJb(isautited, pagesize, pageno, function (result) {
        var objs = $.parseJSON(result);
        var str = "";
        $.each(objs, function (i) {
            str += "<li><span>举报人：" + getuserminid(objs[i].Writter) + "<br/>被举报人：" + getuserminid(objs[i].MemberID) + "</span><br/>";
            str += "<span>类别：" + gettypestr(objs[i].type) + " <br/>举报内容：" + objs[i].Content + "</span><br/>";
            str += "<span>时间：" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM:ss');
            if (objs[i].IsAudited)
                str += "<span>处理结果：" + objs[i].Result + "</span><br/>";
            else
                str += "<a onclick='audit(\"" + objs[i].ID + "\"," + objs[i].type + ")' href='#'>处理</a></li>";
        });
        $("#listcontiner").html(str);
    });
}
function bindjycontent(isautited) {
    AdminProvider.getJy(isautited, pagesize, pageno, function (result) {
        var objs = $.parseJSON(result);
        var str = "";
        $.each(objs, function (i) {
            str += "<li><span>建议人：" + getuserminid(objs[i].Writter) + "</span><br/>";
            str += "<span>类别：" + gettypestr(objs[i].type) + " <br/>建议内容：" + objs[i].Content + "</span><br/>";
            str += "<span>时间：" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM:ss');
            if (objs[i].IsAudited)
                str += "<span>处理结果：" + objs[i].Result + "</span><br/>";
            else
                str += "<a onclick='audit(\"" + objs[i].ID + "\"," + objs[i].type + ")' href='#'>处理</a></li>";
        });
        $("#listcontiner").html(str);
    });
}
function bindyjcontent(isautited)
{
    AdminProvider.getYj(isautited, pagesize, pageno, function (result) {
        var objs = $.parseJSON(result);
        var str = "";
        $.each(objs, function (i) {
            str += "<li><span>建议人：" + getuserminid(objs[i].Writter) + "</span><br/>";
            str += "<span>类别：" + gettypestr(objs[i].type) + " <br/>建议内容：" + objs[i].Content + "</span><br/>";
            str += "<span>时间：" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM:ss') + "&nbsp;&nbsp;&nbsp;&nbsp;";
            if (objs[i].IsAudited) {
                str += "<span>处理结果：" + objs[i].Result + "</span><br/>";
            }
            else {
                str += "<select id=\"txtresult" + objs[i].ID + "\" ><option selected=\"1\">已采纳</option><option>未采纳</option><option>重复提交</option></select>";
                str += "<a onclick='audit22(\"" + objs[i].ID + "\",\"" + objs[i].Writter + "\")' href='#'>处理</a><br/>";
                str += "<span>回复：<textarea id=\"textarearesult" + objs[i].ID + "\" rows=\"6\" ></textarea></span></li>"
                str += "<input type=\"hidden\" id=\"hid" + objs[i].ID + "\" value=\"" + objs[i].Content + "\"  />";
            }
        });
        $("#listcontiner").html(str);
    });
}

function getuserminid(mid) {

    var url = "";
    $.ajaxSetup({ async: false });
    AdminProvider.getUserMin_Id(mid, function (result) {
        var id = $.parseJSON(result);
        if ($.trim(id).length > 0) {
            url = "http://www.moooyo.com/u/" + id;
        }
        else {
            url= "empty";
        }
    });
    $.ajaxSetup({ async: true });
    return url;
}

function audit22(_id,uid)
{
    var tempresult = ($("#txtresult" + _id).val());
    var temprs = ($("#textarearesult" + _id).val());
    var temphid = ($("#hid" + _id).val());
    AdminProvider.setYj(_id, uid, tempresult, temprs, temphid, function (data) {
        var bacdata = $.parseJSON(data);
        if (bacdata.ok) {
            alert("处理成功！");
            bind();
        }
        else {
            alert("处理失败,请参考异常记录!");
        }
    });
}
function audit() {alert("啥事");}

function gettypestr(type)
{
    switch (type)
    {
        case 11:
            return "色情";
        case 12:
            return "暴力";
        case 13:
            return "骚扰";
        case 14:
            return "谩骂";
        case 15:
            return "广告";
        case 16:
            return "欺诈";
        case 17:
            return "反动";
        case 18:
            return "政治";
        case 19:
            return "其他";
        case 21:
            return "小编访谈建议";
        case 22:
            return "意见、建议、bug";
        default:
            return "未知";
    }
}

var pagesize = 10;
var mypagecount = 1;
var pageno =1;
var type = "jb";

function settype(_type)
{
    if(type == _type)
    {
        return;
    }
    else{
        pageno = 1;
        type = _type;
    }
}
function count()
{    
    var _pagecount=0;
    $.ajaxSetup({  
            async : false  
    });
    AdminProvider.getCallCount(type, getisaudited(), function (result) {
        var allcount = $.parseJSON(result).toString();
        _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) - 1) / parseInt(pagesize));
    });
    mypagecount = _pagecount;
    if(pageno > mypagecount)
    {
        pageno = mypagecount;
    }
    $.ajaxSetup({  
            async : true
        });
}

PageClick = function (pageclickednumber) {
            if(pageclickednumber >mypagecount)
            {
                pageno = mypagecount;
            }
            else{
                pageno = pageclickednumber;
            }
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: mypagecount, buttonClickCallback: PageClick });
            bind();
        }
function setPager()
{
    $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: PageClick });
}

</script>
</asp:Content>
