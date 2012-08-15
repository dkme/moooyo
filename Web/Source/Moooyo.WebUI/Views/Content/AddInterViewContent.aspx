<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.AddInterViewContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
小编专访
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("AddLeftPanel", "Content");  %>
<div class="container">
    <div class="box_demo1 fb_left">
      <div class="box_left2" >
        <div class="mt10"></div>
        <div class="Set_title1 border_b1 p40">
            <span class="large_font">小编专访</span>
            <font class="cgray" >（做一次最有趣、最搞怪、最新奇的访谈，让更多柚子了解最原始的你。）</font>
        </div>
        <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
            <div class="fb_box_com w600 ">
				<div class="box_open">
				    <div class="box_neirong"><font><%=Model.myinterviewList.Count%>个回答</font>   <a href="#" onclick="myinterestdivclick($(this))">展开</a></div>
				</div>
                <!--已回答的小编访问列表-->
                <div id="listcontainer" class="fb_box_com w600" data-ifopen="false" style="display:none;">
                <% foreach (var obj in Model.myinterviewList)
                { %>
                <div id='interview<%=obj.ID %>' class="talk_list clearfix">
                    <ul class="talk_ul clearfix">
	                    <li>
                            <span class="span_q">
                            小编：<span id='updatequestion<%=obj.ID %>'><%=obj.Question%></span><br />
                            <%=Model.Member.Name%>：<span id='answer<%=obj.ID %>'><%=obj.Answer%></span>
                        　    <a onclick='deleteinterview("<%=obj.ID %>")'>删除</a>
                            </span>
                            <a class="link_btn" id="answer">修改问题</a>
                        </li>
	                    <li class="li_txt">
                            <div class="txt_content"> 
                                <em>我：</em>
                                <span class="span_txt">
		                            <textarea id='update<%=obj.ID %>' name="textarea" class="textareaOne" style="width:525px; float:right; height:50px; color:#444; line-height:22px; padding:5px; "></textarea>
		                        </span>
                            </div>
	                    </li>
	                    <li class="li_btn">
                            <div class="content_btn">
                                <a class="link_btn" onclick='updateinterview("<%=obj.ID %>")'>保存答案</a>
                                <a class="close">取消</a> 
                            </div>
	                    </li>		       
                    </ul>
                </div>
                <%} %>
                </div>
                <!--endof 已回答的小编访问列表-->
			</div>
            <div style="clear:both"></div>
            <style type="text/css">
                #listcontainer2 .link_btn{cursor:pointer;}
                #listcontainer2 .close{cursor:pointer;}
                #listcontainer2 .textareaOne{width:525px; float:right; height:50px; color:#444; line-height:22px; padding:5px;}
            </style>
            <div id="listcontainer2" class="fb_box_com w600">
                <!--系统中随机提取的问题列表-->
                <%foreach (var obj in Model.sysinterviewList){%>
                <div id='sysinterview<%=obj.ID %>' class="talk_list clearfix">
                    <ul class="talk_ul clearfix">
                        <li>
                            <span class="span_q">小编：<span id='insertquestion<%=obj.ID %>'><%=obj.Question%></span></span>
                            <a class="link_btn" id="answer">回答问题</a>
                        </li>
                        <li class="li_txt">
                            <div class="txt_content">
                            <em>我：</em>
                            <span class="span_txt"><textarea id='insert<%=obj.ID %>' name="textarea" class="textareaOne"></textarea></span>
                            </div>
                        </li>
                        <li class="li_btn">
                            <div class="content_btn">
                                <a class="link_btn" onclick='insertinterview("<%=obj.ID%>")'>回答问题</a>
                                <a class="close">取消</a>
                            </div>
                        </li>		       
                    </ul>
                </div>
                <%}%>
                <!--endof 系统中随机提取的问题列表-->
            </div>
            <div style="width:100%; height:30px; line-height:30px; padding-left:15px;"><a onclick="reloadInterView()">换一批</a></div>
            <div class="fb_box_com w600"><span class="at_text"></span><span class="a_text"></span></div>
        </div>
      </div>
    </div>
    <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = "" }); %>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="/js/data_0101.js"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.left_menu ul li a,.textareaOne');
	 </script>
<![endif]-->
<script type="text/javascript">
    $().ready(function () {
        $("#wrap").css({ "background": "#DEDEE0", "width": "1056px" });
        $(".left_menu").css("display", "block");
    });
    //禁止浏览器后退
    //禁用后退按钮
    window.history.forward(1);
    //禁用后退键，作用于Firefox、Opera  
    document.onkeypress = banBackSpace;
    //禁用后退键，作用于IE、Chrome  
    document.onkeydown = banBackSpace;

    $().ready(function () {
        loadinterview();
    });
    function loadinterview() {
        $('.talk_list').find('ul').eq(0).css('border-top', 'none');
        $('.talk_list').each(function () {
            var li_txt = $(this).children('.talk_ul').children('li.li_txt');
            var li_btn = $(this).children('.talk_ul').children('li.li_btn');
            li_txt.hide();
            li_btn.hide();
            $(this).find('#answer').click(function () {
                $(this).hide();
                li_txt.show();
                li_btn.show();
                $(this).parent().find('.span_q').css({ "color": "#0099cc" });
            });
            $(this).find('.close').click(function () {
                $(this).parent().parent().parent().find('#answer').show();
                li_txt.hide();
                li_btn.hide();
                $(this).parent().parent().parent().find('.span_q').css({ "color": "" });
            });
        });
    }
    $(function () {
        $('.talk_list').each(function () {
            $('.talk_ul').hover(function () {
                $(this).css({ "background": "#fafafa" });
            }, function () {
                $(this).css({ "background": "#fff" });
            });
        });
    });
    //刷新访谈
    function reloadInterView() {
        ContentProvider.reloadInterView(function (data) {
            var list = $.parseJSON(data);
            var str = "";
            $.each(list, function (i) {
                var obj = list[i];
                str += "<div id='sysinterview" + obj.ID + "' class=\"talk_list clearfix\">";
                str += "<ul class=\"talk_ul clearfix\">";
                str += "<li>";
                str += "<span class=\"span_q\">小编：<span id='insertquestion" + obj.ID + "'>" + obj.Question + "</span></span>";
                str += "<a class=\"link_btn\" id=\"answer\">回答问题</a>";
                str += "</li>";
                str += "<li class=\"li_txt\">";
                str += "<div class=\"txt_content\">";
                str += "<em>我：</em>";
                str += "<span class=\"span_txt\"><textarea id='insert" + obj.ID + "' name=\"textarea\" class=\"textareaOne\"></textarea></span>";
                str += "</div>";
                str += "</li>";
                str += "<li class=\"li_btn\">";
                str += "<div class=\"content_btn\">";
                str += "<a class=\"link_btn\" onclick='insertinterview(\"" + obj.ID + "\")'>回答问题</a>";
                str += "<a class=\"close\">取消</a>";
                str += "</div>";
                str += "</li>";
                str += "</ul>";
                str += "</div>";
            });
            $("#listcontainer2").html(str);
            loadinterview();
        });
    }
    //添加访谈
    function insertinterview(id) {
        var mid = '<%=Model.UserID %>';
        var mname = '<%=Model.Member.Name%>';
        var question = $("#insertquestion" + id).html();
        var answer = $("#insert" + id).val();
        var interestIDs = $("#interestIDs").val();
        if (interestIDs == "") {
            $.jBox.tip("请至少至少选择一个兴趣", 'error');
        }
        else if (answer == "") {
            $.jBox.tip("必须填写答案", 'error');
        }
        else {
            memberprovider.addinterview(mid, id, question, answer, function (data) {
                var data = $.parseJSON(data);
                $("#interviewIDs").val($("#interviewIDs").val() + "," + data.ID);
                $("#sysinterview" + id).remove();
                var str = "";
                str += "<div id=\"interview" + data.ID + "\" class=\"talk_list clearfix\">";
                str += "<ul class=\"talk_ul clearfix\">";
                str += "<li>";
                str += "<span class=\"span_q\">";
                str += "小编：<span id=\"updatequestion" + data.ID + "\">" + data.Question + "</span><br />";
                str += "" + mname + "：<span id=\"answer" + data.ID + "\">" + data.Answer + "</span>";
                str += "  <a onclick=\"deleteinterview('" + data.ID + "')\">删除</a>";
                str += "</span>";
                str += "<a class=\"link_btn\" id=\"answer\">修改问题</a></li>";
                str += "<li class=\"li_txt\">";
                str += "<div class=\"txt_content\">";
                str += "<em>我：</em>";
                str += "<span class=\"span_txt\">";
                str += "<textarea id=\"update" + data.ID + "\" name=\"textarea\" class=\"textareaOne\" style=\"width:525px; float:right; height:50px; color:#444; line-height:22px; padding:5px; \"></textarea>";
                str += "</span>";
                str += "</div>";
                str += "</li>";
                str += "<li class=\"li_btn\">";
                str += "<div class=\"content_btn\"><a class=\"link_btn\" onclick=\"updateinterview('" + data.ID + "')\">保存答案</a><a class=\"close\">取消</a></div>";
                str += "</li>";
                str += "</ul>";
                str += "</div>";
                $("div.box_neirong font").html(parseInt($(".box_neirong font").html()) + 1 + "");
                $("#listcontainer").html($("#listcontainer").html() + str);
                loadinterview();
                $("#insert" + id).val("");
                contentsubmit(data.ID, false, false, question, answer);
            });
        }
    }
    //修改访谈
    function updateinterview(id) {
        var mid = '<%=Model.UserID %>';
        var answer = $("#update" + id).val();
        if (answer == "") {
            $.jBox.tip("必须填写答案", 'error');
        }
        else {
            memberprovider.updateinterview(id, mid, answer, function (data) {
                var data = $.parseJSON(data);
                if (data == "false" || data == "False") {
                    $.jBox.tip("修改失败，系统维护中，给您带来了不便，请谅解！", 'error');
                }
                else {
                    $("#interviewIDs").val($("#interviewIDs").val() + "," + id);
                    $("#answer" + id).html(answer);
                    $("#update" + id).val("");
                    $("#interview" + id).children('.talk_ul').children('li.li_txt').hide();
                    $("#interview" + id).children('.talk_ul').children('li.li_btn').hide();
                    $("#interview" + id).find('#answer').show();
                    contentsubmit(id, true, false, '', '');
                }
            });
        }
    }
    //删除访谈
    function deleteinterview(id) {
        var mid = '<%=Model.UserID %>';
        memberprovider.deleteinterview(id, mid, function (data) {
            var data = $.parseJSON(data);
            if (data == "false" || data == "False") {
                $.jBox.tip("删除，系统维护中，给您带来了不便，请谅解！", 'error');
            }
            else {
                $("div.box_neirong font").html(parseInt($(".box_neirong font").html()) - 1 + "");
                $("#interview" + id).remove();
                contentsubmit(id, false, true);
            }
        });
    }
    //添加访谈内容
    function contentsubmit(id, ifupdate, ifdelete, question, answer) {
        var permissions = $("#permissions").val();
        var interestIDs = $("#interestIDs").val();
        var userid = '<%=Model.UserID %>';
        if (interestIDs == "") {
            $.jBox.tip("请至少至少选择一个兴趣", 'error');
        }
        else {
            ContentProvider.InsertInterViewContent(permissions, interestIDs, id, ifdelete, function (data) {
                var data = $.parseJSON(data);
                if (data != null) {
                    if (!ifupdate && !ifdelete) {
                        //分享到微博，是否存在需要分享的微博，且是否分享成功
                        var url = "http://www.moooyo.com/Content/ContentDetail/" + data.ID;
                        var ifShareToWB = ShareToWB(question + ":" + answer, url);
                        if (ifShareToWB) {
                            $.jBox.tip("访谈更新成功，并且已分享到你勾选的微博中。", 'info');
                        }
                        else {
                            $.jBox.tip("访谈更新成功。", 'info');
                        }
                    }
                }
                else {
                    $.jBox.tip("更新失败，系统维护中，给您带来了不便，请谅解！", 'error');
                }
            });
        }
    }
    //展开或关闭我当前回答过的访谈
    function myinterestdivclick(obj) {
        var ifopen = $("#listcontainer").attr("data-ifopen");
        if (ifopen == "false") {
            $("#listcontainer").attr("data-ifopen", "true");
            $("#listcontainer").stop();
            $("#listcontainer").slideDown(500, function () { obj.html("关闭"); });
        }
        else {
            $("#listcontainer").attr("data-ifopen", "false");
            $("#listcontainer").stop();
            $("#listcontainer").slideUp(500, function () { obj.html("展开"); });
        }
    }
</script>
</asp:Content>
