<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    注册
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="msg_box w600 pup-greet" style="display:none;">
        <div class="msg_title">
            <h2>
            </h2>
            <span class="msg_close"></span>
        </div>
        <div class="say-m" style="padding: 0 40px; color: #444; line-height: 35px; font-size: 25px;
            font-family: '微软雅黑'; text-align: left;">
            亲爱的豆瓣用户，米柚网的柚子们都是有分性别的哟，请问你的性别是：</div>
        <div class="say-s" style="text-align: center; padding-top: 20px;">
            <img src="/pics/selectSex.png" width="64" height="46" /></div>
        <div class="msg_content">
            <div class="msg_c_r">
                <span class="btn_span" style="width: 400px"><a class="redlink" href="javascript:void(0)"
                    onclick="cancelDiv(1)" style="font-size: 16px; float: left; margin-left: 120px;">
                    男</a><a class="redlink" href="javascript:void(0)" onclick="cancelDiv(2)" style="font-size: 16px;
                        float: left; margin-left: 20px;">女</a></span>
            </div>
        </div>
        <div class="say-m" style="margin: 20px 40px; color: #666; line-height: 30px; font-size: 12px;
            font-family: '宋体'; text-align: center;">
            (性别一旦选择，将不能更改)</div>
    </div>
    <div id="wrap1">
        <div class="contain1">
            <div class="login_left1 h610">
                <div class="box_com h90" style="padding-left: 30px; margin-top: 15px;">
                    <h2>
                        撒花，欢迎你加入单身柚子队伍~</h2>
                </div>
                <div class="box_ppt welcome">
                    <div class="wel-t">
                        <span class="s-l">
                            <img src="/pics/welcome.gif" /></span> <span class="s-r">当你愿意记录，就有了自己的单身生活电影；<br />
                                当你愿意分享，就有了与你为伍的单身柚子们。</span>
                    </div>
                    <div class="wel-c">
                        <div class="wel-title">
                            在米柚，你可以：</div>
                        <dl class="mr10">
                            <dt>
                                <img src="/pics/wel-01.png" title="" /></dt>
                            <dd>
                                发布照片、心情、计划，与柚子们分享单身时光，品味生活的乐趣。</dd>
                        </dl>
                        <dl class="mr10">
                            <dt>
                                <img src="/pics/wel-02.png" title="" /></dt>
                            <dd>
                                做一次最有趣、最搞怪、最新奇的访谈，让更多柚子了解最原始的你。</dd>
                        </dl>
                        <dl class="mr10">
                            <dt>
                                <img src="/pics/wel-03.png" title="" /></dt>
                            <dd>
                                发个号召，召集当地米柚举办个单身派对。爬山？唱歌？喝酒？一呼百应。</dd>
                        </dl>
                        <dl class="mr10">
                            <dt>
                                <img src="/pics/wel-04.png" title="" /></dt>
                            <dd>
                                通过兴趣群组，发现更多"臭味相投"的单身柚子。</dd>
                        </dl>
                    </div>
                    <div class="wel-b">
                        <div class="wel-b">
                            <span class="l-s">为了让你更容易找到那些与你志趣相投的柚子，先让我来了解一下你是哪种类型柚子吧~</span> <span class="r-s"><a
                                href="/Register/RegAddInterest">继 续</a></span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="login_right h610">
                <div class="box_com h90 ">
                    <img src="/pics/mylogo.gif" alt="米柚网" /></div>
                <div class="box_com h320">
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="isDoubanLogin" name="isDoubanLogin" value="<%=ViewData["IsDoubanLogin"] %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link href="/css/style.css" rel="stylesheet" type="text/css" media="screen" />
<link href="/css/msg.css" rel="stylesheet" type="text/css" media="screen" />
<style type="text/css">
.box_com h2 { color:#444; font-family:"微软雅黑"; font-size:26px; text-align:left; font-weight:normal;}

.welcome { height:485px; overflow:hidden; padding-top:10px; padding:0 30px; font-family:"微软雅黑";}
.wel-t { padding-left:15px; overflow:hidden; height:82px;}
.wel-t span { display:block; float:left;}
.wel-t span.s-l { width:80px; height:80px; overflow:hidden;}
.wel-t span.s-r { width:310px; padding:15px 20px; line-height:25px; margin-bottom:20px; color:#878787; font-size:14px;}
.wel-c{ width:100%; overflow:hidden;}
.wel-title{ padding:10px; text-align:left; line-height:30px; color:#444; font-size:14px; overflow:hidden;}
.wel-c dl { width:230px; height:85px; padding:15px 15px 0 15px; overflow:hidden; list-style:none; display:block; float:left; background:#fff; margin-bottom:10px; }
.mr10 { margin-right:10px;}
.wel-c dl dt { width:67px; height:65px; display:block; float:left;}
.wel-c dl dt img { vertical-align:middle;}
.wel-c dl dd{ width:140px; height:85px; padding-left:20px; display:block; float:left; line-height:18px; color:#444; font-family:"微软雅黑"; text-align:left; font-size:14px;}
.wel-b { width:518px; padding:0 10px; overflow:hidden; margin-top:20px;}
.wel-b span { display:block; float:left;}
.wel-b span.l-s { width:385px; line-height:25px; text-align:left; color:#878787;  font-size:14px;}
.wel-b span.r-s { width:100px; height:35px; padding-left:25px;}
.wel-b span.r-s a { width:100px; height:35px; line-height:35px; color:#fff; font-size:14px;text-align:center; display:block; background:#79e48f;}
.wel-b span.r-s a:hover { background-color:#88e98b;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });

            var isDoubanLogin = $("input#isDoubanLogin").val();
            if (isDoubanLogin != "" && isDoubanLogin != "null") {
                $("div.msg_box").show();
                $("body").append("<div id=\"over_div\"></div>");
                openDiv();
            }
        });
        function openDiv() {
            $('#over_div').show();
            //        $('#join_box').attr("class", "dialog");
            _scrollWidth = Math.max(document.body.scrollWidth, document.documentElement.scrollWidth);
            _scrollHeight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
            $('#over_div').css({
                "width": _scrollWidth + "px",
                "height": _scrollHeight + "px",
                "top": "0px",
                "left": "0px",
                "background": "#33393C",
                "filter": "alpha(opacity=10)",
                "opacity": "0.20",
                "position": "absolute",
                "zIndex": "10"
            });

        }
        function cancelDiv(sex) {
            registerprovider.selectSex(sex, function (data) {
                var result = $.parseJSON(data);
                if (result.ok)
                    $.jBox.tip("成功选择性别", "success");
            });
            $('#over_div, .pup-greet').hide();
        }
    </script>
</asp:Content>
