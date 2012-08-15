<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.IContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的主页
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="Letter_intro">
    <!--个人左面板-->
    <% if (Model.Member != null)
        {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%>  
    <div style="clear:both"></div>
    <div class="mt10"></div>
    <% }%>
    <!--endof 个人左面板-->
    <%Html.RenderPartial("AddBu"); %>

    <%Html.RenderPartial("~/Views/Member/ProfileLeftPanelPointsSchedule.ascx"); %>
</div>
<div class="Share_box alpha60 TransOne" style="padding-bottom:50px;">
    <div class="share_bg">
        <%Html.RenderPartial("~/Views/Content/ContentTypeSelect.ascx"); %>
        <div class="share_ppt">
            <% if (Model.Member.MemberSkin != null) {
                  if (Model.Member.MemberSkin.PersonalityPicture != "")
                  { %>
                <img src="<%=Comm.getImagePath(Model.Member.MemberSkin.PersonalityPicture, ImageType.Original) %>" width="775" height="200"/>
                <% }
                  else { %>
                  <img src="/pics/selft_ppt.png" width="775" height="200"/>
                  <% }
              }
              else { %>
                <img src="/pics/selft_ppt.png" width="775" height="200"/>
            <% } %>
        </div>
    </div>
    <div id="showcontent" style="margin-top:-50px;">
    <%
        if (Model.MyContentList.Count <= 0)
        {
            %>粉少不能怪父母，怪你分享不刻苦。<br />赶紧<a href="/Content/AddImageContent">发布</a>点新鲜内容，让更多柚子知道你的存在~<%
        }
        else
        {
            int i = 0;
            List<Moooyo.BiZ.Content.PublicContent> listleft = new List<Moooyo.BiZ.Content.PublicContent>();
            List<Moooyo.BiZ.Content.PublicContent> listright = new List<Moooyo.BiZ.Content.PublicContent>();
            foreach (var obj in Model.MyContentList)
            {
                string timestr = Comm.getTimeline(obj.UpdateTime);
                if (i == 0 && timestr != "")
                {
                    %>
                    <div class="clear"></div>
                    <div class="time_center"><div class="time_state1"><span><em><%=timestr%></em></span></div></div>
                    <div class="clear"></div>
                    <%
                }
                else if (i > 0 && timestr != "")
                {
                    %>
                    <div class="share_ajax">
                        <div class="ajax_l">
                            <%foreach (var objleft in listleft) { Html.RenderAction("ContentItem", "Content", new { contentobj = objleft, ifshowmember = false, ifmy = true }); }%>
                        </div>
                        <div class="ajax_r">
                            <%foreach (var objtight in listright) { Html.RenderAction("ContentItem", "Content", new { contentobj = objtight, ifshowmember = false, ifmy = true }); } %>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="time_center"><div class="time_state"><span><em><%=timestr%></em></span></div></div>
                    <div class="clear"></div>
                    <%
                  listleft = new List<Moooyo.BiZ.Content.PublicContent>();
                  listright = new List<Moooyo.BiZ.Content.PublicContent>();
                }
                if (listleft.Count == listright.Count) listleft.Add(obj);
                else listright.Add(obj);
                i++;
            }
            if (i >= Model.MyContentList.Count - 1)
            {
                %>
                <div class="share_ajax">
                    <div class="ajax_l">
                        <%foreach (var objleft in listleft) { Html.RenderAction("ContentItem", "Content", new { contentobj = objleft, ifshowmember = false, ifmy = true }); }%>
                    </div>
                    <div class="ajax_r">
                        <%foreach (var objtight in listright) { Html.RenderAction("ContentItem", "Content", new { contentobj = objtight, ifshowmember = false, ifmy = true }); } %>
                    </div>
                </div>
                <%
            }
        }%>
    </div>

    <div class="padding_b50"></div>
    <!--分页-->
    <div id="pagediv" <%=Model.Pagger.PageNo==1?"style=\"display:none\"":"" %>>
    <%if (Model.Pagger != null)
        {%> 
    <%Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
    <%} %>
    </div>
    <!--endof 分页-->
    <%if (Model.Pagger.PageNo <= 1 && Model.contentCount > 10)
        { %>
    <div class="loading" id="ContentLoadMore">
        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
        <div class="loaded"><a id="ContentLoadMoreClick" onclick="showContent(2,'<%=ViewData["contenttype"].ToString() %>',false,true,'<%=Model.UserID %>')">点击加载</a> </div>
        <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
    </div>
    <%} %>
</div>

<!--用户自定义的背景图片-->
<% if (Model.Member.MemberSkin != null) {
      if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "")
      { %>
<input type="hidden" id="backimage" name="backimage" value="<%=Model.Member.MemberSkin.PersonalityBackgroundPicture %>"/>
    <% }
  } %>
<!--系统加载过的时间标签集合 如果页面需要加载时间标签的话就写，如果不需要就不写-->
<%if (Session["timelist"] != null)
  {
      List<long> timelist = Session["timelist"] as List<long>;
      string times = "";
      foreach (var time in timelist) { times += time + ","; }
      times = times.Substring(0, times.Length - 1);%>
    <input type="hidden" id="timelist" value="<%=times %>"/>
<%}%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link href="/css/msg.css" rel="stylesheet" type="text/css" />

<style type="text/css">
 .Letter_demo{ width:185px; margin-bottom:2px; overflow:hidden; background:none; }
 .Letter_interview a { display:block; width:90px; height:90px;}
 .Letter_interview em{width:78px;height:64px;display:block;position:absolute;left:5px;top:12px;z-index:500;}
 .Letter_demo .Letter_interview,.addview  {display:block; float:left; width:90px; height:90px; margin-right:2px; line-height:32px; background:url(/pics/left_letterbg.png) no-repeat; margin-bottom:2px;}
 .Letter_interview { display:block; width:90px; height:90px;  position:relative; z-index:10;}
 .Letter_interview font { color:#fff; position:absolute; display:none; top:5px; left:5px; text-align:left; line-height:20px; font-size:14px;} 
</style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.min.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.top_angle,.bottom_angle,.Trans,.typediv a');
	 </script>
<![endif]-->
<script type="text/javascript" language="javascript">
    uploadpath = '<%=Model.UploadPath %>';
    $(document).ready(function () {
        loadmaindivleft();

        var imagestr = "";
        if ($("#backimage").val() != null)
            imagestr = photofunctions.getnormalphotoname($("#backimage").val());
        else
            imagestr = "/pics/main_bg.gif";
        $("body").append("<img id='mainbodyimage' src='" + imagestr + "' style='position:fixed; left:0; top:0; width:100%; min-height:100%; height:auto !important; height:100%;visibility: hidden; _display: none;'/>");
        $(window).load(function () {
            // 排除IE 6
            if (!$('html').hasClass('lt-ie7')) {
                var theWindow = $(window),
                    bgWidth = $("#mainbodyimage").width(),
                    bgHeight = $("#mainbodyimage").height(),
                    aspectRatio = bgWidth / bgHeight;
                function resizeBg() {
                    var winWidth = theWindow.width(),
                        winHeight = theWindow.height(),
                        w = h = 0,
                        offsetX = bgWidth - winWidth > 0 ? (bgWidth - winWidth) / 2 : 0,
                        offsetY = bgHeight - winHeight > 0 ? (bgHeight - winHeight) / 2 : 0;
                    if ((winWidth / winHeight) < aspectRatio) { h = offsetY + winHeight; w = h * aspectRatio; }
                    else { w = offsetX + winWidth; h = w / aspectRatio; }
                    $("#mainbodyimage").css({ width: w, height: h, left: -offsetX, top: -offsetY }).css('visibility', 'visible').fadeIn(500);
                }
                theWindow.resize(function () { resizeBg(); }).trigger("resize");
            }
            else {
                $('body').attr('style', '_background: transparent url(' + imagestr + ') no-repeat fixed center center');
            }
        });
    });

    function loadmaindivleft() {
        var windowwidth = $(window).width();
        var windowheight = $(window).height();
        var wrapwidth = $("#wrap").width();
        var left = (windowwidth - wrapwidth) / 2;
        $('#wrap').css({ "background": "none", "position": "absolute", "zIndex": "1", "left": left <= 0 ? 0 : left });
    }

    $(window).scroll(function () { loadcontent(); });
    function showContent(pageno, contenttype, ifshowmember, ifmy, userid) {
        ContentProvider.MyContentToAjax(pageno, contenttype, function (data) {
            var data = $.parseJSON(data);
            var MyContentList = data.MyContentList;
            var str = "";
            var index = 0;
            var listleft = new Array();
            var listright = new Array();
            for (var i = 0; i < MyContentList.length; i++) {
                var contentobj = MyContentList[i];
                var timestr = showtimeclick(paserJsonDate(contentobj.CreatedTime));
                if (timestr != "" && i == 0) {
                    str += "<div class=\"clear\"></div>";
                    str += "<div class=\"time_center\"><div class=\"time_state\"><span><em>" + timestr + "</em></span></div></div>";
                    str += "<div class=\"clear\"></div>";
                }
                else if (timestr != "" && i > 0) {
                    str += "<div class=\"share_ajax\">";
                    str += "<div class=\"ajax_l\">";
                    str += getLeftContent(listleft, data, ifshowmember, ifmy, pageno, userid);
                    str += "</div>";
                    str += "<div class=\"ajax_r\">";
                    str += getRightContent(listright, data, ifshowmember, ifmy, pageno, userid);
                    str += "</div>";
                    str += "</div>";
                    str += "<div class=\"clear\"></div>";
                    str += "<div class=\"time_center\"><div class=\"time_state\"><span><em>" + timestr + "</em></span></div></div>";
                    str += "<div class=\"clear\"></div>";
                    listleft = new Array();
                    listright = new Array();
                }
                if (listleft.length == listright.length) listleft[listleft.length] = contentobj;
                else listright[listright.length] = contentobj;
                index++;
            }
            if (index >= MyContentList.length - 1) {
                str += "<div class=\"share_ajax\">";
                str += "<div class=\"ajax_l\">";
                str += getLeftContent(listleft, data, ifshowmember, ifmy, pageno, userid);
                str += "</div>";
                str += "<div class=\"ajax_r\">";
                str += getRightContent(listright, data, ifshowmember, ifmy, pageno, userid);
                str += "</div>";
                str += "</div>";
                listleft = new Array();
                listright = new Array();
            }
            $("#showcontent").html($("#showcontent").html() + str);
            if (pageno < 3) {
                $("#ContentLoadMore").html("<b class=\"rtop\"><b class=\"r1\"></b><b class=\"r2\"></b><b class=\"r3\"></b><b class=\"r4\"></b></b><div class=\"loaded\"><a id=\"ContentLoadMoreClick\" onclick=\"showContent(3,'" + contenttype + "',false,true,'" + userid + "')\">点击加载</a> </div><b class=\"rbottom\"><b class=\"r4\"></b><b class=\"r3\"></b><b class=\"r2\"></b><b class=\"r1\"></b></b>");
            }
            else {
                $("#ContentLoadMore").remove();
                $("#pagediv").css("display", "block");
            }
            loadRightBu();
        });
    }
    function getLeftContent(listleft, data, ifshowmember, ifmy, pageno, userid) {
        var str = "";
        for (var objlefti = 0; objlefti < listleft.length; objlefti++) {
            str += getContentListStr(data, listleft[objlefti], ifshowmember, ifmy, pageno, userid);
        }
        return str;
    }
    function getRightContent(listright, data, ifshowmember, ifmy, pageno, userid) {
        var str = "";
        for (var objtighti = 0; objtighti < listright.length; objtighti++) {
            str += getContentListStr(data, listright[objtighti], ifshowmember, ifmy, pageno, userid);
        }
        return str;
    }
</script>
</asp:Content>
