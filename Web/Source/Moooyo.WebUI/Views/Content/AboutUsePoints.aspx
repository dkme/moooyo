<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
怎样使用米果
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container">
    <div class="box_demo1 w960" style="background:#fff; width:970px;">
        <div class="mt10"></div>
        <div style="height:20px;"></div>
        <div class="Set_title2 admin_b  p40" style="padding-bottom:20px; overflow:hidden;"><span class="miguo" ><em></em><font class="miguo_t">你的米果</font><font class="miguo_a"><%=Model.UserID==null?0:Model.Member.Points %></font></span><span style=" font-family:'微软雅黑'; font-size:30px; color:#444; ">使用你的米果  </span> <a class="com_back blue02" onclick="history.back()">返回</a></div>
        <div class="Set_box1 p40" style="padding:0 40px;">
            <div class="mt10"></div>
            <div class="admin_box ">
                <div class="miguo_about">
                    <span class="miguo_c">目前，你可以这样使用米果</span>
                    <span class="miguo_title"><font>创建兴趣群组</font><a href="/InterestCenter/AddInterest" class="blue01">[传送门]</a></span>
                    <span class="miguo_c">
                    当你创建第 1 个兴趣群组时，将消耗 100 个米果<br/>
                    创建第 2个兴趣群组时，将消耗200个米果<br/>
                    创建第3个兴趣群组时，将消耗300个米果<br/>
                    </span>
                    <span class="miguo_title"><font>添加更多的兴趣群组</font><a href="/InterestCenter/AddInterestFans" class="blue01">[传送门]</a></span>
                    <span class="miguo_c">
                    目前的兴趣群组设定上限为7个，当你想加入第8个群组时，需消耗50个米果<br/>
                    当你想加入第9个群组时，需消耗100个米果<br/>
                    当你想加入第10个群组时，需消耗150个米果<br/>
                    </span>
                    <span class="miguo_title"><font>发布号召</font><a href="/Content/AddCallForContent" class="blue01">[传送门]</a></span>
                    <span class="miguo_c">
                    每发布一次号召，将消耗50个米果<br/>
                    </span>
                    <span class="miguo_title" style="margin-top:50px;"><a href="/Content/AboutEarnPoints" class="blue01">如何获得米果？</a></span>
                </div>
            </div>
            <div class="padding_b50"></div>
            <div class="padding_b50"></div>
        </div>
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
