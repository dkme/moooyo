<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.ContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的主页
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="clear:both"></div>
<div class="mt15 clear"></div>


<div class="Letter_intro">
    <!--个人左面板-->
    <% if (Model.Member != null)
       {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%>  
    <div style="clear:both"></div>
    <div class="mt10"></div>
    <% }%>
    <!--endof 个人左面板-->
    <%
        if (Model.IsOwner)
        {
            Html.RenderPartial("AddBu");
        }
        else
        {
            %>

            <%
        }
        %>
</div>
<div class="Share_box main" style="background:#f9f8f9;">
 <%
    if (Model.contenttype == Moooyo.BiZ.Content.ContentType.Image.ToString())
    {
        Html.RenderPartial("ContentItemDetail_Image");
    }
    if (Model.contenttype == Moooyo.BiZ.Content.ContentType.SuiSuiNian.ToString())
    {
        Html.RenderPartial("ContentItemDetail_ShuoShuo");
    }
    if (Model.contenttype == Moooyo.BiZ.Content.ContentType.InterView.ToString())
    {
        Html.RenderPartial("ContentItemDetail_InterView");
    }
    if (Model.contenttype == Moooyo.BiZ.Content.ContentType.CallFor.ToString())
    {
        Html.RenderPartial("ContentItemDetail_CallFor");
    }
%>
<% if (Model.AlreadyLogon)
   { %>
         <div class="comment" style="margin-bottom:30px; margin-top:30px;" >
           <div class="comment-form">
             <dl class="clearfix">
               <dt>
                 <label for="comment-input" class="avatar"><img src="<%=Model.AlreadyLogon ? Model.User.ICONPath : "" %>" alt="avatar" width="120px" height="120px" /></label>
               </dt>
               <dd>
                  <span class="icon-say"></span>
                 <textarea name="" rows="10" cols="30" id="content<%=Model.contentobj.ID %>"></textarea>
                 <input class="btn" type="submit" value="评 论" onclick="addcomment()"/>
               </dd>
             </dl>
           </div>
           <div id="comment-list-box">
           <div class="comment-content">
             <div class="header">
               <div class="summary"><span id="comment-count"></span>个评论</div>
             </div>
             <div class="comment-list" id="comment-list">
             </div>
           </div>
           <div class="loading1" id="comment-more"><b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
           <div class="loaded1"><a id="loadMoreBtn" onclick="getcomment()">努力加载中…</a> </div>
           <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b></div>
           </div>
         </div>
<% } %>
</div>

<!--用户自定义的背景图片-->
<%if (Model.Member.MemberSkin != null){
      if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "")
      { %>
<input type="hidden" id="backimage" name="backimage" value="<%=Model.Member.MemberSkin.PersonalityBackgroundPicture %>"/>
<%}
  }%>
  <input type="hidden" id="answersMemberid" name="answersMemberid" value=""/>
  <input type="hidden" id="answersID" name="answersID" value=""/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link href="/css/person.photo.css" rel="stylesheet" type="text/css" media="screen" />
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
<script type="text/javascript" src="/scripts/exts_<%=Model.JsVersion %>.js"></script>
<script src="/Scripts/jquery.masonry.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var objid = "<%=Model.contentobj.ID %>";
    var uid = "<%=Model.UserID%>";
    var uploadpath = "<%=Model.UploadPath%>";
    var currentcommentpage = 1;
    $(document).ready(function () {
        getcomment();
        if (!$.browser.msie) {
            //复制到剪贴板
            copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").val());
        }
        else {
            $("#copyToClipboardBtn").bind("click", function () {
                //复制到剪贴板
                copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").val());
            });
        }

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

    $(window).scroll(function () { loadcomment($("#comment-more")); });
    function imageonload(img, width) {
        if (img.clientWidth > width) img.width = width;
    }
    function getcomment() {
        showcontentanswerinContentDetail(objid, $("#comment-list-box"), $("#comment-list"), $("#comment-count"), $("#comment-more"), 20, currentcommentpage, uid);
        currentcommentpage = currentcommentpage + 1;

    }
    function addcomment() {
        var memberid = "";
        if ($("#answersMemberid").val() != null && $("#answersMemberid").val() != "") {
            memberid = $("#answersMemberid").val();
        }
        var answersID = "";
        if ($("#answersID").val() != null && $("#answersID").val() != "") {
            answersID = $("#answersID").val();
        }
        addcontentanswer('<%=Model.contentobj.ID %>', answersID, '<%=Model.UserID %>', memberid);
        var timeout = setTimeout(function () {
            showcontentanswerinContentDetail(objid, $("#comment-list-box"), $("#comment-list"), $("#comment-count"), $("#comment-more"), 20, 1, uid);
            $("#answersMemberid").val("");
            $("#answersID").val("");
        }, 500);
    }
    function showcommentcomment(nickname, memberid, answersID) {
        $(document).scrollTop($("#content<%=Model.contentobj.ID %>").offset().top - 50);
        $("#content<%=Model.contentobj.ID %>").val("回应" + nickname + "：");
        $("#content<%=Model.contentobj.ID %>").focus(function () { focusControlCursorLast(); });
        $("#answersMemberid").val(memberid);
        $("#answersID").val(answersID);
    }
    function loadcomment(morebtncontiner) {
        var clientheight = document.documentElement.clientHeight;
        var scrolltop = document.body.scrollTop + document.documentElement.scrollTop;
        var offsetheight = document.body.offsetHeight;
        if (morebtncontiner.html() != null && morebtncontiner.html() != "") {
            $("#loadMoreBtn").html("努力加载中…");
            if (clientheight + scrolltop > morebtncontiner.offset().top) {
                $("#loadMoreBtn").html("<img src=\"/pics/Ajax_loading.gif\"/>");
                $("#loadMoreBtn").click();
            }
        }
    }
</script>
</asp:Content>
