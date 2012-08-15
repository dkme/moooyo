<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	内测 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="box_code" style="display:none;">
    <div class="box_top"><em></em></div>
    <div class="code_content"> <span></span></div>
    <div class="box_bottom"><em></em></div>
</div>
<!-- 内容-->
<div class="container">
	<div class="box_demo1 w960" style="background:#fff">
        <div class="Set_box1" >
            <!-- TOP div-->
			    <div class="Set_moyo_t testbg">
                    <div class="moyo_left"><img src="/pics/msg_loop.gif" /></div>
                    <div class="moyo_right">
                    <span>矮油，你怎么就进来了？<br/>人家还没完全装扮好呢，现在是米柚的内测时间，<br/>
参观人数有限呢~</span>
                    <span class="moyo_txt"><input type="text"  class="txtput" id="activationCode"  name="activationCode"  /><b class="code_b" onclick="moveCursor();">输入邀请码</b> <a class="redlink" href="javascript:;" onclick="Activation()">进  入</a></span>
                    </div>
                </div>
            <!-- bottom div-->
                <div class="Set_moyo_t" style="margin-bottom:25px;">
                    <div class="moyo_left"></div>
                    <div class="moyo_right">
                    <span>没有邀请码，既然来都来了，就让我猜猜接下来你会做什么吧。</span>
                    <span>你是：</span>
                    <span class="tab_nav"><font data-eq="1">路过的单身</font><font data-eq="2">闷骚的单身</font><font data-eq="3">奔放的单身</font></span>
                    <!-- tab切换内容部分-->
                        <div class="tab_box" id="tabbox1" style="display:none">
                        <span> 路过的单身一般都会马上点右上角的小叉叉跟我拜拜，然后，
                        你就这样挥手错别了一次认识更多快乐单身的机会……</span>
                    </div>
                    <div class="tab_box" id="tabbox2" style="display:none">
                        <span>你肯定是默默将网址放进你的收藏夹，有事没事点开来看看
                        网站是否开放参观了……</span>
                        
                    </div>
                    <div class="tab_box" id="tabbox3" style="display:none">
                        <span> 你马上就会以超越光的速度跑去@米柚官方微博索要邀请码的！！！</span>

                        <span> 一场专属单身的欢乐之旅就这样开始了……</span>
                        <span><a href="http://weibo.com/moooyo123" target="_blank"><img src="/pics/weibobtn01.gif" alt="" title="" /></a><a href="http://t.qq.com/moooyoweibo" target="_blank"><img src="/pics/weibobtn02.gif" alt="" title="" /></a></span>
                    </div>
                    </div>
                </div>
		</div>
    </div>
	
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css" />
<link rel="stylesheet" type="text/css" href="/css/fabu.css">
<style type="text/css">
.Set_moyo_t { overflow:hidden; padding:40px 95px; display:block; }
.testbg { background:#eee; height:160px; }
.moyo_left { width:60px; padding-right:40px; height:100px; overflow:hidden; float:left;}
.moyo_right { width:660px; overflow:hidden; float:left;}
.moyo_right span { display:block; line-height:30px; font-family:"微软雅黑"; font-size:20px; color:#444;}
.moyo_right span.moyo_txt { margin-top:20px; margin-bottom:10px; overflow:hidden; position:relative;}
.moyo_right span.moyo_txt b{ position:absolute; left:8px; top:4px; z-index:2; display:block; height: 30px; line-height: 30px;  color:#666; font-family:"微软雅黑"; font-size:14px; font-weight:normal; }
.moyo_right .moyo_txt input.txtput  {
	width: 195px; height: 30px; line-height: 30px; float:left; _background:#fff; margin-right:8px; padding:3px 0 0 6px; background:#fff url(/pics/border_bg01.png) 0 0 no-repeat;  color:#666; font-family:"微软雅黑"; font-size:14px; border: 1px solid #d4d4d4; }
.moyo_right .moyo_txt input.txtput:hover { border:1px solid #b50203;}
.moyo_right  .moyo_txt a{ display:block; float:left; height:31px; line-height:31px; font-size:16px;  width:79px;   font-weight:bold; text-align:center; font-family:"微软雅黑"; }
.moyo_right  .moyo_txt a.redlink  { color:#fff;  background:#b40001; margin-left:8px; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
.moyo_right  .moyo_txt a.redlink:hover { background:#b20000;}

.moyo_right span.tab_nav { width:588px; height:65px; overflow:hidden; padding-top:15px;}
.moyo_right span.tab_nav font { width:160px; height:65px; line-height:65px; background-color:#eee; color:#b30000; font-size:20px; text-align:center; display:block; margin-right:20px; float:left; cursor:pointer;}
.moyo_right span.tab_nav font.on { background:#b40102; _background-color:#b40102; color:#fff;}
.moyo_right .tab_box { border:1px solid #b40102; padding:30px 25px;  width:536px; overflow:hidden; line-height:30px; font-family:"微软雅黑"; font-size:20px; color:#444; text-align:left;} 
.moyo_right .tab_box span { margin-bottom:15px; overflow:hidden;}
.moyo_right .tab_box span a { display:block; float:left; margin-right:20px; margin-top:25px;}

.box_code  { width:252px; position:absolute; overflow:hidden; z-index:500; }
.code_content { width:252px; overflow:hidden; line-height:30px; text-align:center;  overflow:hidden; background:#ffcc00; color:#444; font-family:"微软雅黑"; font-size:16px;}
.box_code em{ display:block; width:252px;}
.box_top { width:252px; height:22px; overflow:hidden;}
.box_top em { background:url(/pics/code_msg_top.png) no-repeat; height:22px;}
.box_bottom { width:252px; height:10px; overflow:hidden;}
.box_bottom em { background:url(/pics/code_msg_bottom.png) no-repeat; height:10px;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/admin.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("body").css({ "background-color": "#dedee0" });
        $("div#wrap").css({ "background-color": "#dedee0" });

        setFocusEmptyInput($("span.moyo_txt b"), $("input#activationCode"), "输入邀请码");

        //鼠标移上TAB切换
        $('.tab_nav font').hover(function () {

            if ($(this).attr('data-eq') == "1") {
                $(this).addClass('on');
                $('#tabbox1').css({ "display": "block" });
                $('#tabbox2').css({ "display": "none" });
                $('#tabbox3').css({ "display": "none" });
            }
            if ($(this).attr('data-eq') == "2") {
                $(this).addClass('on');
                $('#tabbox2').css({ "display": "block" });
                $('#tabbox1').css({ "display": "none" });
                $('#tabbox3').css({ "display": "none" });
            }
            if ($(this).attr('data-eq') == "3") {

                $(this).addClass('on');
                $('#tabbox1').css({ "display": "none" });
                $('#tabbox2').css({ "display": "none" });
                $('#tabbox3').css({ "display": "block" });
            }
            $(this).siblings().removeClass('on');
        }, function () {
            // $(this).removeClass('on');
            // var j=$(this).attr('data-eq');
            // $('#tabbox'+ j).css({"display":"none"});
        });

    });
    function moveCursor() {
        var rng = this.code.createTextRange();
        rng.move("character", 0);
        rng.select();
    }

    function Activation() {
        var code = $("input#activationCode").val();
        if (code.length > 6) {
            showActivationCodePrompt("邀请码输入有误，请确认！");
        }
        else {
            AdminProvider.checkActivationCode(code, function (data) {
                var data = $.parseJSON(data);
                if (data == "ok") {
                    setTimeout(function () {
                        window.location = "/Content/IFavorerContent";
                    }, 30);
                }
                else if (data == "no") {
                    showActivationCodePrompt("邀请码已过期！");
                }
                else {
                    showActivationCodePrompt("邀请码输入有误，请确认！");
                }
            });
        }
    }

    function showActivationCodePrompt(prompt) {
        //邀请码错误提示框
        var code = $('input#activationCode');
        $('div.box_code').css({
            "left": code.offset().left - $("div#wrap").offset().left - 20,
            "top": code.offset().top - $("div#wrap").offset().top + 40,
            "display": "block"
        });
        $("div.code_content span").html(prompt);
        var timeoutId = setTimeout(function () {
            $('div.box_code').hide(300);
        }, 4000);
    }

    function moveCursor() {
        var rng = this.activationCode.createTextRange();
        rng.move("character", 0);
        rng.select();
    }
</script>
</asp:Content>
