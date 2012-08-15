<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/IndexEnter.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="C3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" href="/css/reg_<%=ViewData["cssversion"] %>.css" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	是否单身
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section class="clearfix mt85 mb82">
<h2><img src="/pics/login/issingle.png" alt="是否单身"/></h2>
<div class="is_single clearfix"><a href="#" id="no_single" class="no_single fl">不是单身</a><a href="/register/reg" class="single fr">是单身</a></div>
</section>
<!--已有伴侣  弹窗-->
<div id="singletips">
<div class="top"><a href="#" class="close">X</a></div>
<div class="singlebox clearfix">
    <div class="sing_lbg"></div>
    <div  class="singletips cwhite f18 fl">
    <div class="sing_up">
        <p class="chblue f30">很抱歉</p>
        <h3>亲爱的朋友，这里是一个<span class="fb">专为单身的TA们寻找恋人的网站</span><br/>您还是去别处看看吧。</h3>
    </div>
    <div class="sing_down">
    <form name="enjoy">
        <label class="cwhite f16">推荐给我的单身好友</label>
        <input type="text" id="txtCopyUrl" value="www.Moooyo.com" class="sent_input"/>
        <a href="javascript:;" class="fl" onclick="copyToClipboard($('#linkCopyUrl').html())">&nbsp;&nbsp;复制</a>
        <label class="chblue2 f12 ml193">复制后可直接粘贴在QQ、MSN等聊天软件中</label>
    </form>
    <div class="enjoy clearfix">
    <p><span class="fl mr8">或分享到</span><!-- JiaThis Button BEGIN -->
<div id="jiathis_style_32x32">
	<a class="jiathis_button_qzone"></a>
	<a class="jiathis_button_tsina"></a>
	<a class="jiathis_button_tqq"></a>
	<a class="jiathis_button_renren"></a>
	<a class="jiathis_button_kaixin001"></a>
	<a href="http://www.jiathis.com/share" class="jiathis jiathis_txt jtico jtico_jiathis" target="_blank"></a>
	<a class="jiathis_counter_style"></a>
</div>
<script type="text/javascript" src="http://v2.jiathis.com/code/jia.js" charset="utf-8"></script>
<!-- JiaThis Button END --></p>
    </div>
    </div>
    </div>
    <div class="sing_rbg"></div>
</div>
<div class="bot"></div>
<a class="no_single mt13" href="#">不是单身</a>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<!--已有伴侣  弹窗结束-->
<script type="text/javascript">
    $().ready(function () {
        $("#no_single").bind("click", function () {
            var dw = document.body.clientWidth;
            var left = document.body.clientWidth > 976 ? (parseInt(dw / 2) - 427) : 61;
            var h = parseInt(window.screen.height);
            $("body").append("<div id='fadebg'></div>");
            $("#singletips").fadeIn().show().css("left", left + "px");
            $("#fadebg").fadeIn().css("height", h + "px");
            if ($("#fadebg")) {
                $(window).resize(function () {
                    dw = document.body.clientWidth;
                    left = document.body.clientWidth > 976 ? (parseInt(dw / 2) - 427) : 61;
                    h = parseInt(window.screen.height);
                    setposition(left, h);
                });
            }
        });
        $("#fadebg,.close").live("click", function () {
            $("#fadebg,#singletips").fadeOut(function () {
                $("#singletips").hide();
                $("#fadebg").remove();
            });
        });
    });
    function setposition(left, h) {
        $("#singletips").css("left", left + "px");
        $("#fadebg").css("height", h + "px");
    }
    
</script>
</asp:Content>
