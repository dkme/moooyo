<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MyInterestToWenWenModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
我的兴趣话题
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    #maindiv .tab{width:630px;font-size:25px;color:#727985;height:50px;line-height:50px; font-family:宋体;}
    #maindiv .tab .fgaol{font-size:12px; cursor:pointer; font-family:\5FAE\8F6F\96C5\9ED1;color:#999999;}
    #maindiv .tab .gaolall{font-size:12px; cursor:pointer; font-family:\5FAE\8F6F\96C5\9ED1;color:#CA6445;}
    #maindiv .tab .gaolboy{font-size:12px; cursor:pointer; font-family:\5FAE\8F6F\96C5\9ED1;color:#2ba4cd;}
    #maindiv .tab .gaolgirl{font-size:12px; cursor:pointer; font-family:\5FAE\8F6F\96C5\9ED1;color:#85bf52;}
    #maindiv .interestitem{width:630px;height:30px;line-height:30px; clear:both; background-image:url('/pics/myinterest.png'); background-repeat:repeat-x; background-position:left top;}
    #maindiv .interestitem a{color:#85c054;margin-left:10px;}
    #maindiv .tointerest{width:630px; text-align:right; height:30px; line-height:25px;}
    #maindiv .wenwenitem{width:570px;padding-left:30px;height:30px;line-height:30px;color:#ccc; clear:both;}
    #maindiv .wenwenitem a{color:#336699;float:left;margin-left:10px;}
    #maindiv .wenwenitem span{margin-left:10px;float:left;}
    #maindiv .wenwenitem img{margin-left:10px;margin-top:5px;float:left;}
    #maindiv .machmoer{width:630px; margin-top:10px; height:30px; line-height:30px; text-align:center; cursor:pointer; clear:both; background-image:url('/pics/myinterest.png'); background-repeat:repeat-x; background-position:left top;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="c976 clearfix">
<section class="inter-conbox mr15 mt32 fl" style="width:610px;" id="maindiv">
<div class="tab">我的兴趣组...
<%
    string aclick = "";
    string topictoboystr = ViewData["topictoboy"].ToString();
    string topictogirlstr = ViewData["topictogirl"].ToString();
    if (topictoboystr == "1" && topictogirlstr == "0")
    {
        aclick = "<a class=\"gaolboy\">【大米】</a><a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=0&topictogirl=1\">【柚子】</a><a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=1&topictogirl=1\">【所有】</a>";
    }
    if (topictoboystr == "0" && topictogirlstr == "1")
    {
        aclick = "<a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=1&topictogirl=0\">【大米】</a><a class=\"gaolgirl\">【柚子】</a><a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=1&topictogirl=1\">【所有】</a>";
    }
    if (topictoboystr == "1" && topictogirlstr == "1")
    {
        aclick = "<a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=1&topictogirl=0\">【大米】</a><a class=\"fgaol\" href=\"/Member/MyInterest?topictoboy=0&topictogirl=1\">【柚子】</a><a class=\"gaolall\">【所有】</a>";
    }
%><%=aclick%></div>
<%--提示--%>
<div class="littip_div">
    <div class="littip meinterest"></div>
    <img id="meinterestlittip1del" src="/pics/tip/littip_del.png" alt="" title="知道了，关闭" class="meinterestlittip1del" />
</div>
<%--提示结束--%>
<%
    foreach (var obj in Model.interests)
    {
        bool topictoboy = ViewData["topictoboy"].ToString() == "1" ? true : false;
        bool topictogirl = ViewData["topictogirl"].ToString() == "1" ? true : false;
        IList<Moooyo.BiZ.WenWen.WenWen> wenwenlist = new List<Moooyo.BiZ.WenWen.WenWen>();
        if (topictoboy && topictogirl)
            wenwenlist = obj.WenWens;
        else if (topictoboy)
            wenwenlist = obj.WenWensToBoy;
        else if (topictogirl)
            wenwenlist = obj.WenWensToGirl;
        if (wenwenlist != null && wenwenlist.Count > 0)
        {
    %>
    <div class="interestitem"><img class="myinterest" data-interestid="<%=obj.ID %>" src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" width="30" height="30" style="float:left;"/><a href="/InterestCenter/InterestFans?iid=<%=obj.ID %>"><%=obj.Title.Length > 20 ? "<span style='margin:0px;'>" + obj.Title.Substring(0, 20) + "</span>" + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></div>
<div style="width:600px; padding-bottom:10px;">
    <% Html.RenderAction("MyInterestTowwItem", "Push", new { topiclist = wenwenlist, showcount = 6 }); %>
    </div>
    <%}
    }
        if (Model.interests == null || Model.interests.Count <= 0)
        {
            %><div style="width:100%;padding-top:100px;padding-bottom:50px;text-align:center;font-size:20px; font-family:\5FAE\8F6F\96C5\9ED1;">啊哦，你还没有关注任何兴趣，<a href="/InterestCenter/FeaturedInterestTopic">先看看有料的东东</a></div><%
        } %>
<%if (Model.interestpagecount > Model.interestpageno) { %>
    <div class="machmoer" onclick="showforinterest(<%=Model.interestpageno %>,'<%=Model.UserID %>')">加载更多...</div>
<%} %>
</section>
<aside class="asidebox-r mt32 fr" style="width:335px;">
<h3 class="caption-tit mt18" style="color:#006600;text-indent:5px; height:30px; line-height:30px;"><span class="fl">我的兴趣...</span><a href="/InterestCenter/Interests" target="_blank" style="float:right;">所有(<%=Model.interestcount %>)</a></h3>
<ul class="pic-list clearfix" id="myinterestul">
    <% int j = 0;
       foreach (var obj in Model.myinterest)
        {
            if (j >= 8) { break; } %>
        <li class="interestli" data-interestid="<%=obj.ID %>" style="height:90px; width:60px; line-height:15px; text-align:center; margin-left:15px; margin-top:15px;"><a href="/InterestCenter/InterestFans?iid=<%=obj.ID%>" target="_blank"><img src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Icon) %>" width="50" height="50" alt="" border="0" /><%=obj.Title.Length > 6 ? obj.Title.Substring(0, 6) + "<span class=\"letspa--3\">...</span>" : obj.Title%></a></li>
    <%j++;
        } %>
</ul>
<div style="border:solid 1px #FFF5EC; width:333px; height:30px; line-height:32px; text-indent:10px;">
    <a href="/InterestCenter/AddInterest" target="_blank">+　创建自己的兴趣组</a>
</div>
<% Html.RenderAction("MyWenWen", "Push"); %>
<% Html.RenderAction("GuessYourInterests", "Push"); %>
</aside>
</div>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script language="javascript" type = "text/javascript">
    var topictoboy = '<%=ViewData["topictoboy"].ToString() %>';
    var topictogirl = '<%=ViewData["topictogirl"].ToString() %>';
    $().ready(function () {
        interestCenter.bindinterestLabel($("#maindiv div.interestitem img.myinterest"));
        MemberInfoCenter.BindDataInfo($("div.myinterestuser img"));
        interestCenter.bindinterestLabel($("#myinterestul li.interestli"));

        //提示相关
        var logintimes = <%=(Session["logintimes"]==null || Session["logintimes"]=="")?0:Session["logintimes"] %>;
        if(logintimes <=2 && $.cookie("<%=Model.MemberID%>meinterestlittip1del") == null)
        {
            $("#meinterestlittip1del").bind("click", function () { $(this).parent().fadeOut(1000);$.cookie("<%=Model.MemberID%>meinterestlittip1del", "false", { expires: 1 }); });
            setTimeout(function () { $("#meinterestlittip1del").parent().fadeIn(1000) }, 2000);
        }
    });

    function showforinterest(pageno, userid) {
        $("div.machmoer").remove();
        WenWenLinkProvider.getwenwenforinterest(pageno, topictoboy == "1" ? true : false, topictogirl == "1" ? true : false, function (result) {
            var model = $.parseJSON(result);
            var interests = model.interests;
            var xqpagecount = model.interestpagecount;
            var xqpageno = model.interestpageno;
            var str = "";
            for (var i = 0; i < interests.length; i++) {
                var topiclist = null;
                if (topictoboy == "1" && topictogirl == "0") {
                    topiclist = interests[i].WenWensToBoy;
                }
                else if (topictoboy == "0" && topictogirl == "1") {
                    topiclist = interests[i].WenWensToGirl;
                }
                else if (topictoboy == "1" && topictogirl == "1") {
                    topiclist = interests[i].WenWens;
                }
                var obj = interests[i];
                if (topiclist.length > 0) {
                    str += "<div class=\"interestitem\"><img class=\"myinterest\" data-interestid=\"" + obj.ID + "\" src=\"" + photofunctions.geticonphotoname(obj.ICONPath) + "\" width=\"30\" height=\"30\" style=\"float:left;\"/><a href=\"/InterestCenter/InterestFans?iid=" + obj.ID + "\" target=\"_blank\">" + (obj.Title.Length > 20 ? "<span style='margin:0px;'>" + obj.Title.Substring(0, 20) + "</span>" + "<span class=\"letspa--3\">...</span>" : obj.Title) + "</a></div><div style=\"width:600px; padding-bottom:10px;\"><div class=\"interesttowwitemdiv\">";
                    str += showmytopic(topiclist, model.UserID);
                    str += "</div></div>";
                }
            }
            if (xqpagecount > xqpageno) { str += "<div class=\"machmoer\" onclick=\"showforinterest(" + xqpageno + ",'" + userid + "')\">加载更多...</div>"; }
            $("#maindiv").html($("#maindiv").html() + str);
            interestCenter.bindinterestLabel($("#maindiv div.interestitem img"));
            MemberInfoCenter.BindDataInfo($("div.myinterestuser img"));

        });
    }
    function showmytopic(list,userid) {
        var str = "";
        var llist = new Array();
        var rlist = new Array();
        var lcount = 0, rcount = 0;
        for (var i = 0; i < list.length; i++) {
            var obj = list[i];
            if (obj != null && obj.Content != null && obj.InterestID != null) {
                if (lcount - rcount == 0) {
                    if (obj.ContentImage != null && obj.ContentImage != "")
                        lcount += 2;
                    else
                        lcount += 1;
                    llist.push(obj);
                }
                else if (lcount - rcount > 0) {
                    if (obj.ContentImage != null && obj.ContentImage != "")
                        rcount += 2;
                    else
                        rcount += 1;
                    rlist.push(obj);
                }
                else if (lcount - rcount < 0) {
                    if (obj.ContentImage != null && obj.ContentImage != "")
                        lcount += 2;
                    else
                        lcount += 1;
                    llist.push(obj);
                }
            }
        }
        if (rcount > lcount)
        {
            rilist = rlist.pop();
        }
        var ltopicstr = "<div class=\"leftdiv\">";
        var rtopicstr = "<div class=\"rightdiv\">";
        for (var j = 0; j < llist.length; j++) {
            var obj = llist[j];
            if (obj.ContentImage != null && obj.ContentImage != "") {
                var content = obj.Content.length > 30 ? obj.Content.substr(0, 30) + "..." : obj.Content;
                content = getExpression(content);
                var replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
                var imagestr = obj.ContentImage;
                var imgstr = "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + getImageToTopic(imagestr, 1) + "</a>";
                var classname = obj.Creater.Sex == 1 ? "class=\"itemboybig\"" : "class=\"tiemgirlbig\"";
                ltopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Content/TaContent/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + userid + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + photofunctions.geticonphotoname(obj.Creater.ICONPath) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span>" + imgstr + "</div><div class=\"manadiv\">" + replay + "&nbsp;" + (imagestr.split(',').length - 1) + "图</div></div>";
            }
            else {
                var content = obj.Content.length > 30 ? obj.Content.substr(0, 30) + "..." : obj.Content;
                content = getExpression(content);
                var replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
                var classname = obj.Creater.Sex == 1 ? "class=\"itemboysmal\"" : "class=\"itemgirlsmal\"";
                ltopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Content/TaContent/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + userid + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + photofunctions.geticonphotoname(obj.Creater.ICONPath) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span></div><div class=\"manadiv\">" + replay + "</div></div>";
            }
        }
        for (var k = 0; k < rlist.length; k++) {
            var obj = rlist[k];
            if (obj.ContentImage != null && obj.ContentImage != "") {
                var content = obj.Content.length > 30 ? obj.Content.substr(0, 30) + "..." : obj.Content;
                content = getExpression(content);
                var replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
                var imagestr = obj.ContentImage;
                var imgstr = "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + getImageToTopic(imagestr, 1) + "</a>";
                var classname = obj.Creater.Sex == 1 ? "class=\"itemboybig\"" : "class=\"tiemgirlbig\"";
                rtopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Content/TaContent/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + userid + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + photofunctions.geticonphotoname(obj.Creater.ICONPath) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span>" + imgstr + "</div><div class=\"manadiv\">" + replay + "&nbsp;" + (imagestr.split(',').length - 1) + "图</div></div>";
            }
            else {
                var content = obj.Content.length > 30 ? obj.Content.substr(0, 30) + "..." : obj.Content;
                content = getExpression(content);
                var replay = obj.AnswerCount > 0 ? "<a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + obj.AnswerCount + "回应</a>" : "";
                var classname = obj.Creater.Sex == 1 ? "class=\"itemboysmal\"" : "class=\"itemgirlsmal\"";
                rtopicstr += "<div " + classname + "><div class=\"imgdiv myinterestuser\"><a href=\"/Content/TaContent/" + obj.Creater.MemberID + "\" target=\"_blank\"><img data_me_id=\"" + userid + "\" data_member_id=\"" + obj.Creater.MemberID + "\" src=\"" + photofunctions.geticonphotoname(obj.Creater.ICONPath) + "\"/>" + obj.Creater.NickName + "</a></div><div class=\"contentdiv\"><span><a href=\"/WenWen/ShowWenWen?wwid=" + obj.ID + "\" target=\"_blank\" title=\"点击查看详情\">" + content + "</a></span></div><div class=\"manadiv\">" + replay + "</div></div>";
            }
        }
        ltopicstr += "</div>";
        rtopicstr += "</div>";
        str = ltopicstr + rtopicstr;
        return str;
    }
</script>
</asp:Content>
