<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	关于米果的赚取
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="box_demo1 w960" style="background:#fff; width:970px;">
        <div class="mt10"></div>
        <div style="height:20px;"></div>
        <div class="Set_title2 admin_b  p40" style="padding-bottom:20px; overflow:hidden;">
            <span class="miguo" ><em></em><font class="miguo_t">你的米果</font><font class="miguo_a"><%=Model.UserID==null?0:Model.Member.Points %></font></span>
            <span style="font-family:'微软雅黑'; font-size:30px; color:#444;">发布精彩内容，参加米果种植  </span>
            <a class="com_back blue02" onclick="history.back()">返回</a>
        </div>
        <div class="Set_box1 p40" style="padding:0 40px;">
            <div class="mt10"></div>
            <div class="admin_box ">
                <div class="miguo_about">
                    <span class="miguo_s mt40">为了奖励用户分享的众多精彩内容，我们有了米果。</span>
                    <span class="miguo_s mt40">每当你发布的心情、说说、照片等获得来自其他用户的1个  <em><img src="/pics/mo_pic.gif" /></em>   ，显示在你主页的米果树就会成长一步，累计获得5个  <em><img src="/pics/mo_pic.gif" /></em> ，就可以采摘米果。</span>
                    <span class="miguo_s">
                        <dl class="mo_list">
                            <dt class="moyobtn"><em class="mo1"></em></dt>
                            <dd><em class="moyo">0 mo</em></dd>
                        </dl>
                        <dl class="mo_list">
                            <dt class="moyobtn on"><em class="mo2"></em></dt>
                            <dd><em class="moyo">1 mo</em></dd>
                        </dl>
                        <dl class="mo_list">
                            <dt class="moyobtn"><em class="mo3"></em></dt>
                            <dd><em class="moyo">2 mo</em></dd>
                        </dl>
                        <dl class="mo_list">
                            <dt class="moyobtn"><em class="mo4"></em></dt>
                            <dd><em class="moyo">3 mo</em></dd>
                        </dl>
                        <dl class="mo_list">
                            <dt class="moyobtn"><em class="mo5"></em></dt>
                            <dd><em class="moyo">4 mo</em></dd>
                        </dl>
                        <dl class="mo_max">
                            <dt class="moyobtn"><em></em></dt>
                            <dd><em class="moyo">5 mo</em></dd>
                        </dl>
                    </span>
                    <span class="miguo_s mt40">
                        每次采摘米果，获得米果的数量都不一样（这就需要看人品了！）。<br/>
                        当然，米柚会不定时举办一些赠送米果的活动，只要你积极参加，也可以获得米果。
                    </span>
                    <span class="miguo_title" style="margin-top:50px;"><a href="/Content/AddImageContent" class="blue01">去发布内容，赚取米果</a><br/><a href="/Content/AboutUsePoints" class="blue01">米果有什么用?</a></span>
                </div>
            </div>
            <div class="padding_b50"></div>
            <div class="padding_b50"></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<link rel="stylesheet" type="text/css" href="/css/msg.css"/>
<style type="text/css">
    .Set_title2 span.miguo{ width:85px;  height:55px; padding-left:55px; padding-top:5px; position:relative; display:block; float:left; margin-right:30px; background:#b40001;}
    .Set_title2 span.miguo em { width:39px; height:40px; position:absolute; left:5px; top:8px; background:url(/pics/miguoaccount.png) no-repeat;}
    .Set_title2 span.miguo font { display:block; float:left; width:85px; text-align:left; color:#fff; line-height:25px;}
    .Set_title2 span.miguo font.miguo_t { font-family:"微软雅黑"; font-size:14px;}
    .Set_title2 span.miguo font.miguo_a { font-family:"Arial"; font-size:32px; font-weight:600; }
    .miguo_about { padding:20px 80px; overflow:hidden; font-family:"微软雅黑"; font-size:16px; }
    .miguo_about span { display:block; text-align:left; line-height:30px;}
    .miguo_about span.miguo_title { margin-top:30px; color:#000; width:100%; clear:both;}
    .miguo_about span.miguo_c { color:#666;}
    .miguo_about span.miguo_s { color:#333; overflow:hidden; margin-top:20px;}
    .miguo_s img { vertical-align:middle;}
    .miguo_about span.miguo_s dl.mo_list { display:block; width:55px; height:81px; float:left; margin-right:40px; margin-top:5px;overflow:hidden; cursor:pointer;}
    .miguo_about span.miguo_s dl.mo_list em ,.moyobtn ,dl.mo_max em{ background:url(/pics/moguo_sprites.png) no-repeat; }
    .miguo_about span.miguo_s dl.mo_list dt em {  display:block; width:30px;  height:35px; margin:8px auto; }
    .miguo_about span.miguo_s dl.mo_list dt em.mo1{ background-position:-16px -295px;}
    .miguo_about span.miguo_s dl.mo_list dt em.mo2{ background-position:-61px -295px;}
    .miguo_about span.miguo_s dl.mo_list dt em.mo3{ background-position:-108px -294px;}
    .miguo_about span.miguo_s dl.mo_list dt em.mo4{ background-position:-155px -293px;}
    .miguo_about span.miguo_s dl.mo_list dt em.mo5{ background-position:-201px -293px;}
    .miguo_about span.miguo_s dl.mo_list dt { display:block; width:52px;  height:51px; margin:0 auto; float:left; background-position:-440px -7px; margin-bottom:6px;  cursor:pointer;}
    .miguo_about span.miguo_s dl.mo_list dt.on{ background-position:-506px -7px;}
    .miguo_about span.miguo_s dl.mo_list dd {display:block; width:55px; height:21px; float:left; margin:0;}
    .miguo_about span.miguo_s dl.mo_list dd em ,.miguo_about span.miguo_s dl.mo_max dd em{ display:block; width:41px; height:21px; line-height:21px; margin:0 auto; text-align:center; font-size:16px; color:#b20000; font-family:"Arial"; background-position:-78px -28px;}
    .miguo_about span.miguo_s dl.mo_max { width:69px; height:91px;  display:block; float:left;}
    .miguo_about span.miguo_s dl.mo_max dt { width:69px; height:64px; margin-bottom:5px; display:block; float:left; background-position:-661px -6px;  cursor:pointer;}
    .miguo_about span.miguo_s dl.mo_max dt.on{ background-position:-575px -5px;}
    .miguo_about span.miguo_s dl.mo_max dt em { width:39px; height:47px; display:block; margin:8px auto; background-position: -256px -286px; }
    .miguo_about span.miguo_s dl.mo_max dd { width:69px; height:21px; line-height:21px;float:left; margin:0;}
    .mt40 { margin-top:40px;}
    .miguo_about span.miguo_title font{   padding-right:35px;}
    .miguo_about span.miguo_title a { font-family:"宋体"; font-size:12px; color:#0099cc;}
    .miguo_about span.miguo_title a:hover { color:#b40001;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script language="javascript" type="text/javascript" src="/js/msg.js" charset="utf-8"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    $(document).ready(function () {
        $('.select_list').hide(); //初始ul隐藏
        $('.select_box span').click(function () { //鼠标点击函数
            $(this).parent().find('ul.select_list').slideDown();  //找到ul.son_ul显示
            $('ul.select_list li').eq(0).addClass('hover');
            $(this).parent().find('li').hover(function () {
                $(this).addClass('hover'); $(this).attr("data-select", "yes");
            }, function () {
                $(this).removeClass('hover'); $(this).attr("data-select", "none");
            }); //li的hover效果
            $(this).parent().hover(function () { },
						 function () {
						     $(this).parent().find("ul.select_list").slideUp();
						 });
        }, function () { });
        $('ul.select_list li:first').each(function () {
            if ($(this).keypressed == true) {
                if (event.keyCode == '38' || event.keyCode == '33') {
                    alert("chenggng");
                }
                // $(this).keydown(function (){ });
            }
        });
        //键盘按下时间
        $('ul.select_list li').click(function () {
            if ($(this).eq(3).click()) {
                $('.chosepic').css({ "display": "block" });
            }
            $(this).parents('li').find('span').html($(this).html());
            $(this).parents('li').find('ul').slideUp();
        });
    });
</script>
</asp:Content>
