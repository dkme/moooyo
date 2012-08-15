<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.TaContentModel>" %>
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
    <div class="clear1"></div>
    <div class="mt10"></div>
<% }%>
<!--endof 个人左面板-->
    <% Html.RenderPartial("~/Views/Member/TaLeftPanel.ascx");%>  
    <div class="clear1"></div>
    <% Html.RenderPartial("~/Views/Member/TaLeftPanelMes.ascx");%>  
</div>
    
<div class="Share_box alpha60 TransOne" style="padding-bottom:50px;">
    <div class="share_bg">
        <%Html.RenderPartial("~/Views/Content/ContentTypeSelect.ascx"); %>
        <div class="share_ppt">
            <%if (Model.Member.MemberSkin != null){ %>
            <img src="<%=Comm.getImagePath(Model.Member.MemberSkin.PersonalityPicture,ImageType.Original) %>" width="775" height="200"/>
            <%}else { %>
            <img src="/pics/selft_ppt.png" width="775" height="200"/>
            <%}%>
        </div>
    </div>
    <div id="showcontent" style="margin-top:-50px;">
<%
    bool ifshowimageyq = false;
    bool ifshowinterviewyq = false;
    if (Model.TaContentList.Count <= 0)
    {
            %>
<br /><br /><br />
<div class="share_ajax"><div class="ajax_l" style="text-align:center;">哎，白来一趟~这位柚子木有公开的内容哦~</div></div>
<div class="share_ajax">
<div class="ajax_l">
<%
        if (Model.Member.ICONPath.IndexOf("noicon") >= 0)
        {
            %>
<div class="edit_box" onclick="Invert.uploadAvatar('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传头像</a></div>
            <%
        }
        if (!Model.Member.IsRealPhotoIdentification)
        {
            %>
<div class="edit_box" onclick="Invert.Authenticas('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta完成视频认证</a></div>
            <%
        }
        %>
</div>
</div>
        <%
    }
    else
    {
        int i = 0;
        List<Moooyo.BiZ.Content.PublicContent> listleft = new List<Moooyo.BiZ.Content.PublicContent>();
        List<Moooyo.BiZ.Content.PublicContent> listright = new List<Moooyo.BiZ.Content.PublicContent>();
        foreach (var obj in Model.TaContentList)
        {
            string timestr = Comm.getTimeline(obj.UpdateTime);
            if (i == 0 && timestr != "")
            {
                    %>
<div class="clear"></div>
<div class="time_center"><div class="time_state1"><span><em><%=timestr%></em></span></div></div>
<div class="clear"></div>
<div class="share_ajax">
<div class="ajax_l">
<%
        if (Model.Member.ICONPath.IndexOf("noicon") >= 0)
        {
            %>
<div class="edit_box" onclick="Invert.uploadAvatar('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传头像</a></div>
            <%
        }
        if (!Model.Member.IsRealPhotoIdentification)
        {
            %>
<div class="edit_box" onclick="Invert.Authenticas('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta完成视频认证</a></div>
            <%
        }
        %>
</div>
</div>
                    <%
            }
            else if ((i > 0 && timestr != "") || i == Model.TaContentList.Count)
            {
                    %>
<div class="share_ajax">
<div class="ajax_l">
            <%foreach (var objleft in listleft)
              {
                  Html.RenderAction("ContentItem", "Content", new { contentobj = objleft, ifshowmember = false, ifmy = false });
                  if (objleft.ContentType.ToString() == "Image" && !ifshowimageyq)
                  {
                      ifshowimageyq = true;
%><div class="edit_box" onclick="Invert.photo('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传更多照片</a></div><%
                  }
                  if (objleft.ContentType.ToString() == "InterView" && !ifshowinterviewyq)
                  {
                      ifshowinterviewyq = true;
%><div class="edit_box" onclick="Invert.interview('<%=Model.MemberID %>',$(this))"><a class="Pink">邀请Ta完成更多访谈</a></div><%
                  }
              }
              %>
</div>
<div class="ajax_r">
            <%foreach (var objtight in listright)
              {
                  Html.RenderAction("ContentItem", "Content", new { contentobj = objtight, ifshowmember = false, ifmy = false });
                  if (objtight.ContentType.ToString() == "Image" && !ifshowimageyq)
                  {
                      ifshowimageyq = true;
%><div class="edit_box" onclick="Invert.photo('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传更多照片</a></div><%
                  }
                  if (objtight.ContentType.ToString() == "InterView" && !ifshowinterviewyq)
                  {
                      ifshowinterviewyq = true;
%><div class="edit_box" onclick="Invert.interview('<%=Model.MemberID %>',$(this))"><a class="Pink">邀请Ta完成更多访谈</a></div><%
                  }
              }
              %>
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
        if (i >= Model.TaContentList.Count)
        {
                %>
<div class="share_ajax">
<div class="ajax_l">
        <%foreach (var objleft in listleft)
          {
              Html.RenderAction("ContentItem", "Content", new { contentobj = objleft, ifshowmember = false, ifmy = false });
              if (objleft.ContentType.ToString() == "Image" && !ifshowimageyq)
              {
                  ifshowimageyq = true;
%><div class="edit_box" onclick="Invert.photo('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传更多照片</a></div><%
              }
              if (objleft.ContentType.ToString() == "InterView" && !ifshowinterviewyq)
              {
                  ifshowinterviewyq = true;
%><div class="edit_box" onclick="Invert.interview('<%=Model.MemberID %>',$(this))"><a class="Pink">邀请Ta完成更多访谈</a></div><%
              }
          } 
          %>
</div>
<div class="ajax_r">
        <%foreach (var objtight in listright)
          {
              Html.RenderAction("ContentItem", "Content", new { contentobj = objtight, ifshowmember = false, ifmy = false });
              if (objtight.ContentType.ToString() == "Image" && !ifshowimageyq)
              {
                  ifshowimageyq = true;
%><div class="edit_box" onclick="Invert.photo('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传更多照片</a></div><%
              }
              if (objtight.ContentType.ToString() == "InterView" && !ifshowinterviewyq)
              {
                  ifshowinterviewyq = true;
%><div class="edit_box" onclick="Invert.interview('<%=Model.MemberID %>',$(this))"><a class="Pink">邀请Ta完成更多访谈</a></div><%
              }
          }
          %>
</div>
</div>
            <%
          listleft = new List<Moooyo.BiZ.Content.PublicContent>();
          listright = new List<Moooyo.BiZ.Content.PublicContent>();
        }
    }
                if (!ifshowimageyq || !ifshowinterviewyq)
                {
              %>
<div class="share_ajax">
<div class="ajax_l">
        <%
                    if (!ifshowimageyq)
                    {
                        ifshowimageyq = true;
%><div class="edit_box" onclick="Invert.photo('<%=Model.MemberID %>',$(this))"><a class="blue02">邀请Ta上传更多照片</a></div><%
                    }
                    if (!ifshowinterviewyq)
                    {
                        ifshowinterviewyq = true;
%><div class="edit_box" onclick="Invert.interview('<%=Model.MemberID %>',$(this))"><a class="Pink">邀请Ta完成更多访谈</a></div><%
                    }
          %>
</div>
</div>
              <%
                }
                %>
    </div>
    <div class="padding_b50"></div>
    <!--分页-->
    <div id="pagediv" <%=Model.Pagger.PageNo==1?"style=\"display:none\"":"" %>>
    <%if (Model.Pagger != null)
      {
          Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });
      }%>
    </div>
    <!--endof 分页-->

    <%if (Model.Pagger.PageNo <= 1 && Model.contentCount > 10)
      {
          %>
        <div class="loading" id="ContentLoadMore">
            <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
            <div class="loaded"><a id="ContentLoadMoreClick" onclick="showContent(2,'<%=ViewData["contenttype"].ToString() %>','<%=Model.MemberID %>',false,false,'<%=Model.UserID %>')">点击加载</a> </div>
            <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
        </div><%
      }%>
    <div class="padding_b50"></div>
</div>

<!--保存是否以显示邀请上传照片和邀请完成访谈-->
<input type="hidden" id="ifshowimageyq" name="ifshowimageyq" value="<%=ifshowimageyq %>" />
<input type="hidden" id="ifshowinterviewyq" name="ifshowinterviewyq" value="<%=ifshowinterviewyq %>" />
<!--用户自定义的背景图片-->
<%if (Model.Member.MemberSkin != null){
      if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "")
      { %>
<input type="hidden" id="backimage" name="backimage" value="<%=Model.Member.MemberSkin.PersonalityBackgroundPicture %>"/>
<%}
  }%>
<!--系统加载过的时间标签集合 如果页面需要加载时间标签的话就写，如果不需要就不写-->
<%if (Session["timelist"] != null)
  {
      List<long> timelist = Session["timelist"] as List<long>;
      string times = "";
      foreach (var time in timelist) { times += time + ","; }
      times = times.Substring(0, times.Length - 1);%>
  <input type="hidden" id="timelist" value="<%=times %>"/>
  <%} %>
<!--判断当前登录用户是否设置头像-->
<%string ishaveicon = Model.User == null ? "true" : Model.User.ICONPath.IndexOf("noicon") >= 0 ? "false" : "true"; %>
<input type="hidden" id="isHaveIcon" name="isHaveIcon" value="<%=ishaveicon%>"/>
<%string ismyauthentica = Model.User == null ? "true" : Model.User.IsRealPhotoIdentification ? "true" : "false"; %>
<input type="hidden" id="isMyAuthentica" name="isMyAuthentica" value="<%=ismyauthentica%>"/>
<%string istaauthentica = Model.User == null ? "true" : Model.Member.IsRealPhotoIdentification ? "true" : "false"; %>
<input type="hidden" id="isTaAuthentica" name="isTaAuthentica" value="<%=istaauthentica%>"/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<style type="text/css">
    img { border:0;}
    .w155 { width:155px;}
    .w326 { width:326px;}
    .w440 { width:440px;}
    .w600{ width:600px;}
    .msg_box { overflow:hidden; display:block; border:2px solid #ededed; color:#444;}
    .msg_title { padding:0 0 0 35px;  height:45px; }
    .msg_title h2 { display:block; width:280px; height:45px; line-height:45px; margin:0; float:left; font-weight:normal; font-size:30px;font-family:"微软雅黑"; cursor:pointer;}
    .msg_title span.msg_close { display:block; float:right; width:50px; height:40px;}
    .msg_title span.msg_close a:hover{background:rgb(242, 242, 242);}
    .msg_content { display:block; padding:30px 40px; overflow:hidden;}
    .msg_c_l { width:152px; float:left; padding-right:20px; border-right:1px solid #ededed;}
    .msg_c_l span, .msg_c_r span ,.msg_bottom span{ display:block; padding-bottom:10px; line-height:25px; text-align:left; font-size:16px; }
    .msg_c_l span img { width:152px; height:152px;}
    .msg_c_r { display:block; float:left; padding-left:20px; }
    .textarea3 { background:#fff url(/pics/input_long_bg03.png) left top no-repeat; line-height:25px; font-size:14px; color:#666; overflow-y:hidden; border:1px solid #cdcdcd;}
    .msg_c_r span.span_c { padding-bottom:20px;}
    .msg_c_r span.btn_span { line-height:35px; height:35px; padding-right:10px;}
    .msg_c_r span a.redlink,.msg_bottom span a.redlink { color:#fff; font-size:16px; text-decoration:none; font-weight:600; float:right; width:100px; background:#b40001; line-height:32px; text-align:center; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,em img,b,.top_angle,.bottom_angle,.Trans,.typediv a');
	 </script>
<![endif]-->
<script type="text/javascript" language="javascript">
    //获取未设置头像提示
    function getSettingIcon() {
        var str = "";
        str += "<div id=\"noHaveIcon\" class=\"msg_box w600\" style=\"background:#fff; display:none; position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-200px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ \">";
        str += "<div class=\"msg_title\"><h2></h2><span class=\"msg_close\"><a onclick=\"closenowwindow();\"><img src=\"/pics/msg_close.gif\" width=\"50\" height=\"40\" /></a></span></div>";
        str += "<div class=\"say-m\" style=\"padding:0 40px; color:#444; line-height:30px; font-size:18px; font-family:\"微软雅黑\"; text-align:left;\">来者何人？！<br/>米柚江湖规矩：不识庐山真面目，木有头像请移步。柚子们都怕蒙面大侠噢。</div>";
        str += "<div class=\"say-s\" style=\"padding:20px 40px; color:#878787; line-height:30px; font-size:16px; font-family:\"微软雅黑\"; text-align:left;\">亲，上传一张自己的照片作为头像才可以浏览头像用户的主页哟~</div>";
        str += "<div class=\"msg_content\">";
        str += "<div class=\"msg_c_l\" style=\" border:0;\">";
        str += "<span><img src=\"/pics/noicon.jpg\" width=\"152\" height=\"152\" /></span>";
        str += "</div>";
        str += "<div class=\"msg_c_r w326\">";
        str += "<span class=\"btn_span\" style=\"margin-top:120px;\"><a href=\"/Setting/UploadFace\" class=\"redlink\" style=\"float:left; font-size:16px;\">去修改头像</a></span>";
        str += "</div>";
        str += "</div>";
        str += "</div>";
        return str;
    }
    function getAuthentica() {
        var str = "";
        str += "<div id=\"noAuthentica\" class=\"msg_box w600\" style=\"background:#fff; display:none; position:absolute; z-index:9999;left:50%; top:50%; margin-left:-300px!important;/*FF IE7 该值为本身宽的一半 */ margin-top:-200px!important;/*FF IE7 该值为本身高的一半*/ margin-top:0px; position:fixed!important;/* FF IE7*/ position:absolute;/*IE6*/ _top:expression(eval(document.compatMode && document.compatMode=='CSS1Compat') ? documentElement.scrollTop + (document.documentElement.clientHeight-this.offsetHeight)/2 :/*IE6*/document.body.scrollTop + (document.body.clientHeight - this.clientHeight)/2);/*IE5 IE5.5*/ \">";
        str += "<div class=\"msg_title\"><h2></h2><span class=\"msg_close\"><a onclick=\"closenowwindow();\"><img src=\"/pics/msg_close.gif\" width=\"50\" height=\"40\" /></a></span></div>";
        str += "<div class=\"say-m\" style=\"padding:0 40px; color:#444; line-height:30px; font-size:18px; font-family:\"微软雅黑\"; text-align:left;\">阿弥陀佛，众生平等~<br/><div style=\"padding-top:15px;\">米柚江湖规矩：为保证视频认证用户的人身财产各种安全，只有同样通过视频认证才能浏览TA的主页哟~</div></div>";
        str += "<div class=\"msg_content\">";
        str += "<div style=\"width:60%; float:left; height:106px;\"><img src=\"/pics/video-big02.gif\" /></div>";
        str += "<div class=\"btn\" style=\"width:38%; float:left; height:106px; background-image:none;\"><a class=\"redlink\" href=\"/Setting/Authentica\" style=\" color:#fff; font-size:16px; text-decoration:none; font-weight:600; float:right; width:100px; background:#b40001; line-height:32px; text-align:center; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid; margin-top:60px;\">去认证视频</a></div>";
        str += "</div>";
        str += "</div>";
        return str;
    }

    uploadpath = '<%=Model.UploadPath %>';
    var ifshowimageyq = false;
    var ifshowinterviewyq = false;
    $(document).ready(function () {
        //IE 支持圆角插件
        if (window.PIE) {
            $('.rounde').each(function () {
                PIE.attach(this);
            });
        }
        //判断当前登录用户是否设置头像
        if ($("#isHaveIcon").val() == "false") {
            $("body").append(getOpacityDiv());
            $("body").append(getSettingIcon());
            //显示半透明遮层
            $("#opacityDiv").css({ display: "block", opacity: 0.5, height: $(document).height() });
            //隐藏页面滚动条
            $("body").css("overflow", "hidden");
            //打开未设置头像提示
            $("#noHaveIcon").css({ display: "block" });
        }
        else {
            if ($("#isMyAuthentica").val() == "false" && $("#isTaAuthentica").val() == "true") {
                $("body").append(getOpacityDiv());
                $("body").append(getAuthentica());
                //显示半透明遮层
                $("#opacityDiv").css({ display: "block", opacity: 0.5, height: $(document).height() });
                //隐藏页面滚动条
                $("body").css("overflow", "hidden");
                //打开未设置头像提示
                $("#noAuthentica").css({ display: "block" });
            }
        }

        //母版页 wrap 层背景色修改
        $('#wrap').css({ "background": "none" });
        //左边 菜单鼠标效果
        $('.redbg').hover(
            function () { $(this).css({ "background": "#ca0000" }); },
            function () { $(this).css({ "background": "#b40001" }); }
        );
        $('.greenbg').hover(
            function () { $(this).css({ "background": "#8ae99d" }); },
            function () { $(this).css({ "background": "#78e48e" }); }
        );
        $('.l_bulebg').hover(
            function () { $(this).css({ "background": "#7fe0e3" }); },
            function () { $(this).css({ "background": "#78e3e7" }); }
        );

        ifshowimageyq = $("#ifshowimageyq").val() == "true" || $("#ifshowimageyq").val() == "True" ? true : false;
        ifshowinterviewyq = $("#ifshowinterviewyq").val() == "true" || $("#ifshowinterviewyq").val() == "True" ? true : false;

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

    function showContent(pageno, contenttype, memberid, ifshowmember, ifmy, userid) {
        ContentProvider.TaContentToAjax(pageno, contenttype, memberid, function (data) {
            var data = $.parseJSON(data);
            var TaContentList = data.TaContentList;
            var str = "";
            var index = 0;
            var listleft = new Array();
            var listright = new Array();
            for (var i = 0; i < TaContentList.length; i++) {
                var contentobj = TaContentList[i];
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
            if (index >= TaContentList.length) {
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
                $("#ContentLoadMore").html("<b class=\"rtop\"><b class=\"r1\"></b><b class=\"r2\"></b><b class=\"r3\"></b><b class=\"r4\"></b></b><div class=\"loaded\"><a id=\"ContentLoadMoreClick\" onclick=\"showContent(3,'" + contenttype + "','" + memberid + "',false,false,'" + userid + "')\">点击加载</a> </div><b class=\"rbottom\"><b class=\"r4\"></b><b class=\"r3\"></b><b class=\"r2\"></b><b class=\"r1\"></b></b>");
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
            if (listleft[objlefti].ContentType.toString() == "0") {
                if (!ifshowimageyq) {
                    ifshowimageyq = true;
                    str += getPhoto(listleft[objlefti]);
                }
            }
            if (listleft[objlefti].ContentType.toString() == "4") {
                if (!ifshowinterviewyq) {
                    ifshowinterviewyq = true;
                    str += getInterview(listleft[objlefti]);
                }
            }
        }
        return str;
    }
    function getRightContent(listright, data, ifshowmember, ifmy, pageno, userid) {
        var str = "";
        for (var objtighti = 0; objtighti < listright.length; objtighti++) {
            str += getContentListStr(data, listright[objtighti], ifshowmember, ifmy, pageno, userid);
            if (listright[objtighti].ContentType.toString() == "0") {
                if (!ifshowimageyq) {
                    ifshowimageyq = true;
                    str += getPhoto(listright[objtighti]);
                }
            }
            if (listright[objtighti].ContentType.toString() == "4") {
                if (!ifshowinterviewyq) {
                    ifshowinterviewyq = true;
                    str += getInterview(listright[objtighti]);
                }
            }
        }
        return str;
    }
    function getPhoto(contentobj) {
        return "<div class=\"edit_box\"><a class=\"blue02\" onclick=\"Invert.photo('" + contentobj.MemberID + "',$(this))\">邀请Ta上传更多照片</a></div>";
    }
    function getInterview(contentobj) {
        return "<div class=\"edit_box\"><a class=\"Pink\" onclick=\"Invert.interview('" + contentobj.MemberID + "',$(this)\")>邀请Ta完成更多访谈</a></div>";
    }
    //显示更多用户资料
    //function showmoremessaeg(obj) {
    //    var usermessagediv = $("#usermessagediv");
    //    if (usermessagediv.attr("data-ifopen") == "false") {
    //        usermessagediv.attr("data-ifopen", "true");
    //        usermessagediv.slideDown(500);
    //        obj.html("收起");
    //    }
    //    else {
    //        usermessagediv.attr("data-ifopen", "false");
    //        usermessagediv.slideUp(500);
    //        obj.html("更多...");
    //    }
    //}
</script>
</asp:Content>
