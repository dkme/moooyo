<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
米柚内测
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    Html.RenderPartial("~/Views/MicroConn/ShareBu.ascx"); 
    %>
<div id="bigbg"><img id="bgimg" src="" title="" alt="" /></div>
<div id="wrap-l">
  <div class="my-login">
     <div class="my-logo" style="margin-bottom:15px;" >
     <img src="/pics/LOGO-L.png" title="米柚网" alt=""/>
     </div>
     <div class="clear"></div>
     <div class="my-wen"><%=ViewData["choiceContent"]%><a style="cursor:pointer;" href="#" title="分享" data-oldobj="index" onmouseover="shareClick('http://www.moooyo.com','米柚网','<%=ViewData["choiceContent"] %>',$(this),'http://www.moooyo.com/photo/Get/<%=ViewData["choiceImage"] %>')"><img src="/pics/share-bg.png" width="36" height="33"/></a></div>
     <div class="my-txtmo">
        <div class="my-title">合作平台账号登陆</div>
        <div class="share-list">
          <ul>
            <li><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToSinaWeibo');" class="share01 em">新浪微博</a><em class="em1"></em></li>
            <li><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToTXWeibo');" class="share02 em">腾讯微博</a><em class="em2"></em></li>
            <li><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToRenRen');" class="share03">人人</a></li>
            <li><a href="javascript:;" onclick="window.open('/MicroConn/ConnectToDouBan');" class="share04">豆瓣</a></li>
          </ul>
        </div>
     </div>
     <div class="my-btn">
        <a href="/Account/Login" class="Trans em">米柚账户登录</a>
     </div>
  </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link type="text/css" rel="Stylesheet" id="LINK" href="/css/Login1.css"  />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.em img,.my-logo img,.mytxt,em img,.my-wen img');
     </script>
<![endif]-->
<!--[if lt IE 9]>
<script type="text/javascript" src="/js/PIE.js"></script>
<![endif]-->
<script language="javascript" type="text/javascript">
  
    $(document).ready(function () {

        $(function () {
            if (window.PIE) {
                $('.rounded').each(function () {
                    PIE.attach(this);
                });
            }
        });

    });


    //窗口自适背景图设置 ADD BY hulu
    $(document).ready(function () {
      //鼠标悬浮底部文字描述位置
      $('.my-wen,.my-wen a').mouseenter(function () {
        $(this).find('img').attr("src", "/pics/share-bghover.png");
      });
      $('.my-wen,.my-wen a').mouseleave(function () {
        $(this).find('img').attr("src", "/pics/share-bg.png");
      });

      var bg = '/photo/Get/<%=ViewData["choiceImage"] %>';
      var $bg = $("#bgimg").hide().attr('src', bg);

      $bg.load(function () {
        var theWindow = $(window),
                  bgWidth = $bg.width(),
                  bgHeight = $bg.height(),
                  aspectRatio = bgWidth / bgHeight;
        function resizeBg() {
          var winWidth = theWindow.width(),
                      winHeight = theWindow.height(),
                      w = h = 0,
                      offsetX = bgWidth - winWidth > 0 ? (bgWidth - winWidth) / 2 : 0,
                      offsetY = bgHeight - winHeight > 0 ? (bgHeight - winHeight) / 2 : 0;
          if ((winWidth / winHeight) < aspectRatio) {
            h = offsetY + winHeight;
            w = h * aspectRatio;
          } else {
            w = offsetX + winWidth;
            h = w / aspectRatio;
          }
          $bg.css({
            width: w,
            height: h,
            left: -offsetX,
            top: -offsetY
          }).css('visibility', 'visible').fadeIn(500);
        }

        theWindow.resize(function () {
          resizeBg();
        }).trigger("resize");
      });
    });

</script>
</asp:Content>


