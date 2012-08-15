<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.IFavorerContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name %>关注的人
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Letter_intro">
<!--个人左面板-->
<% if (Model.Member != null)
    {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%> 
    <div style="clear:both"></div>
    <div class="mt10"></div> 
<% } %>
<!--endof 个人左面板-->
    <%Html.RenderPartial("AddBu"); %>
    <%Html.RenderPartial("~/Views/Member/ProfileLeftPanelPointsSchedule.ascx"); %>
</div>
<%string contenttype = ViewData["contenttype"].ToString(); %>
<input type="hidden" id="contenttype" name="contenttype" value="<%=contenttype %>"/>
<input type="hidden" id="userid" name="userid" value="<%=Model.UserID %>"/>
<div class="Fans_content alpha60 TransOne">
    <div class="mainppt2 h208">
           <div class="self_con Trans" style="width:365px; padding-right:0px;">
            <span class="care_b">很关注</span>
            <%if (Model.Member.FavorMemberCount > 0)
              {
                  %>
                    <span class="care_t">
                    <% 
                        int j = 0;
                        foreach (var obj in Model.favorerMemberList)
                        {
                            if (j >= 8) { break; } else { j++; }
                            %><a href="/Content/TaContent/<%=obj.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.ICONPath,ImageType.Middle) %>" width="30" height="30" title="<%=obj.NickName %>"/></a><%
                        }%>
                    </span>
                  <%
              } %>
           </div>
           <div class="edit_con"><a class="fans_hit" href="/Relation/Favors" title="我关注的人">关注了<%=Model.Member.FavorMemberCount %>个柚子</a><a class="care_a" href="/Content/IContent" title="我的地盘"><em></em></a></div>
            <div class="chose_city">
                <div class="city_title">
                   <div class="city_con Trans"><span class="s-l w160">{ 关注的柚子 }</span></div>
                </div>
            </div>
            <div class="pptindex">
            <%if (Model.Member.MemberSkin != null){ %>
                <img src="<%=Comm.getImagePath(Model.Member.MemberSkin.PersonalityPicture,ImageType.Original) %>" width="775" height="200"/>
            <%}else { %>
                <img src="/pics/selft_ppt.png" width="775" height="200"/>
            <%}%>
            </div>
        </div>
        <span class="contentLoading">加载中...</span>
        <div id="showcontent" style="position:relative;">
            <%if (Model.Pagger != null && Model.Pagger.PageNo >= 2)
              {
                  foreach (var obj in Model.ContentList)
                  { 
                      %><div class="care_com" style="display:none;"><%Html.RenderAction("ContentItem", "Content", new { contentobj = obj, ifshowmember = true, ifmy = false });%></div><%
                  }
              }%>
        </div>
    <div class="padding_b50"></div>
    <!--分页-->
    <div id="pagediv" <%=Model.Pagger.PageNo==1?"style=\"display:none\"":"" %>>
    <%if (Model.Pagger != null)
      {
          if (Model.Pagger.PageCount > 1)
          {
              Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });
          }
          if (Model.Pagger.PageNo <= 1)
          {
        %><input type="hidden" id="data-pageno" name="data-pageno" value="<%=Model.Pagger.PageNo %>" /><%
          }
      }%>
    </div>
    <!--endof 分页-->
    <%if (Model.Pagger.PageNo <= 1 && Model.contentCount > 10)
      { 
          %><div class="loading" id="ContentLoadMore"></div><%
      }%>
    <div class="padding_b50"></div>
</div>
<!--用户自定义的背景图片-->
<%if (Model.Member.MemberSkin != null){
      if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "")
      { %>
<input type="hidden" id="backimage" name="backimage" value="<%=Model.Member.MemberSkin.PersonalityBackgroundPicture %>" style=""/>
    <% }
  }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css" media="screen"/>
<link href="/css/msg.css" rel="stylesheet" type="text/css" />
<style type="text/css"> 
   .msg-p { width:65px; height:70px; position:absolute;  z-index:500;  display:block; background:url(/pics/msghover.png);}
  .msg-p ul { width:65px; list-style:none; padding:0; margin:0;  }
  .msg-p ul li { line-height:22px;  display:block; height:22px; width:65px; }
  .msg-p ul li.b { border-bottom:1px solid #ddd;}
  .msg-p ul li a{ font:normal 12px/22px "\5FAE\8F6F\96C5\9ED1"; height:22px; width:57px; display:inline-block; font-size:12px; padding-left:8px; color:#999; text-align:left; text-decoration:none;}
  .msg-p ul li a:hover { color:#333;}
  .msg-p .toparrow { width:8; height:8; position:absolute; top:-7px;  left:15px; z-index:505;  }
  .msg-p .arr,.msg-p .arrg  {  border-width:0 8px 8px 8px; border-style:dashed  dashed solid dashed; border-color: transparent transparent #fff transparent; position:absolute; line-height:0; font-size:0;}
  .msg-p .arrg { border-bottom-color:#fff; }

</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.js" type="text/javascript"></script>
<script src="/Scripts/jquery.masonry.min.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.self_con,.Trans,.edit_con a,b');
	 </script>
<![endif]-->
<script type="text/javascript" language="javascript">
   
    uploadpath = '<%=Model.UploadPath %>';
    var noContentMsg = "粉少不能怪父母，怪你分享不刻苦。<br />赶紧<a href=\"/Content/AddImageContent\">发布</a>点新鲜内容，让更多柚子知道你的存在~";
    $(document).ready(function () {
        if ($.browser.msie && ($.browser.version === "6.0")) {
            $(".self_con").each(function () {
                if ($(this)[0].scrollHeight > 56)
                    $(this).css({ "height": "56px", "overflow": "hidden" });
            });
        }
        $('.care_t a').hover(function () {
            $(this).css({ "opacity": "1" });
        });

        //内容类型选择的效果
        $("em.jo1").hover(
            function () { $(this).removeClass().addClass("jo1s"); },
            function () { $(this).removeClass().addClass("jo1"); }
        );
        $("em.jo2").hover(
            function () { $(this).removeClass().addClass("jo2s"); },
            function () { $(this).removeClass().addClass("jo2"); }
        );
        $("em.jo5").hover(
            function () { $(this).removeClass().addClass("jo5s"); },
            function () { $(this).removeClass().addClass("jo5"); }
        );
        $("em.jo6").hover(
            function () { $(this).removeClass().addClass("jo6s"); },
            function () { $(this).removeClass().addClass("jo6"); }
        );
        //左边 导航编辑效果
        $('.share_c ul').find('li').hover(
            function () { $(this).find('a').css({ "z-index": "10", "background": "#000" }); },
            function () { $(this).find('a').css({ "z-index": "-10", "background": "none" }); }
        );
        //当处于第一页时，第一批数据用Ajax加载
        if ($("#data-pageno").val() != null) {
            var data_pageno = $("#data-pageno").val();
            if (parseInt(data_pageno) <= 1) {
                showContent(1, true, false);
            }
            else {
                ifopenmasonrynotimeline = true;
                masonrynotimeline();
            }
        }
        else {
            ifopenmasonrynotimeline = true;
            masonrynotimeline();
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

    $(window).scroll(function () { loadcontent(); });

    function showContent(pageno, ifshowmember, ifmy) {
        ifopenmasonrynotimeline = true;
        var contenttype = $("#contenttype").val();
        var userid = $("#userid").val();
        ContentProvider.MyFavorerContentToAjax(pageno, contenttype, function (data) {
            var data = $.parseJSON(data);
            var ContentList = data.ContentList;
            var top = $("#showcontent .care_com:last").html() != null ? $("#showcontent .care_com:last").offset().top : 5;
            var height = $("#showcontent .care_com:last").html() != null ? $("#showcontent .care_com:last").height() : 0;
            str = "";
            if (data.ContentList.length <= 0 && pageno == 1) {
                str = noContentMsg;
            }
            else {
                for (var i = 0; i < ContentList.length; i++) {
                    str += "<div class=\"care_com\" style=\"position: absolute; top:" + (top + height + 5) + "px;left:5px;display:none;\">" + getContentListStr(data, ContentList[i], ifshowmember, ifmy, pageno, userid) + "</div>";
                }
            }
            if (pageno <= 1) {
                $("#showcontent").html(str).masonry({ itemSelector: '.care_com' });
                getpagestr(pageno);
                if ($(".contentLoading").html() != null) $(".contentLoading").remove();
                $("div.care_com").css("display", "block");
            }
            else {
                var strobj = $(str);
                $("#showcontent").append(strobj).masonry('appended', strobj);
                getpagestr(pageno);
                if ($(".contentLoading").html() != null) $(".contentLoading").remove();
                masonrynotimeline();
                $("div.care_com").css("display", "block");
            }

            loadRightBu();
            loadiconmove();
           
        });
    }
    function getpagestr(pageno) {
        if (pageno < 3) {
            $("#ContentLoadMore").html("<b class=\"rtop\"><b class=\"r1\"></b><b class=\"r2\"></b><b class=\"r3\"></b><b class=\"r4\"></b></b><div class=\"loaded\"><a id=\"ContentLoadMoreClick\" onclick=\"showContent(" + (pageno + 1) + ",true,false)\">点击加载</a></div><b class=\"rbottom\"><b class=\"r4\"></b><b class=\"r3\"></b><b class=\"r2\"></b><b class=\"r1\"></b></b>");
        }
        else {
            $("#ContentLoadMore").remove();
            $("#pagediv").css("display", "block");
        }
    }
 
</script>
<% 
//如果用户第一次登陆，显示引导
if (!(bool)ViewData["guideShowed"])
{%>
<link rel="stylesheet" type="text/css" href="/css/guide.css"/>
<script src="/js/guide.js" type="text/javascript"></script>
<script src="/js/guide.index.js" type="text/javascript"></script>
<%} %>
</asp:Content>
