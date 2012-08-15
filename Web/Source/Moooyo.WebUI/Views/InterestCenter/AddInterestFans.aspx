<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.Content.AddInterestFansModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
添加兴趣群组
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- 内容-->
<div class="container">
    <div class="box_demo1 w680 fb_left">
        <div class="mt10"></div>
        <div class="Set_title1 border_b1 p40">
            <span style="font-size:22px">更多兴趣群组</span>
            <a class="com_back blue02" href="/Content/IndexContent">返回</a>
        </div>
        <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
            <div class="mt10"></div>
            <div class="fb_box_com w600" style="margin-bottom:30px;">
                <span class="add_xiqu">
                    <a class="blue02" onclick="showCreatedInterest()"><img src="/pics/add_xiqu.gif" width="51" height="51"/>创建兴趣群组</a>
                </span>
            </div>
            <div id="interestmain">
                加载中...
            </div>
        </div>
    </div>
    <%Html.RenderAction("AddRightPanel", "Content", new { contentObj = "" }); %>
</div>

<div id="box_msg_addfans" style="display:none;">
    <div class="msg_box w600">
        <div class="msg_title"><h2>加入兴趣群组</h2><span class="msg_close"><a onclick="$('#box_msg_addfans').css('display','none');$('#box_msg_addfans a.redlink').unbind('click');$('#content').val('');"><em></em></a></span></div>
        <div class="msg_content">
            <div class="msg_c_l">
                <span><img class="image" src="/pics/MM01.gif" width="152" height="152" /></span>
                <span class="title">兴趣标题</span>
            </div>
            <div class="msg_c_r w326">
                <span>和群组里面先到的同学说点什么吧：</span>
                <span><textarea id="content" name="textarea" class="textarea3" style="width:315px; height:120px;"></textarea></span>
                <span class="btn_span"><a class="redlink">加  入</a></span>
            </div>
        </div>
    </div>
</div>

<div id="box_msg" style="display:none;">
    <div class="msg_box w440">
        <div class="msg_title"><h2>离开兴趣群组</h2><span class="msg_close"><a onclick="$('#box_msg').css('display','none');$('#box_msg a.redlink').unbind('click');"><em></em></a></span></div>
        <div class="msg_content">
            <div class="msg_c_l">
                <span><img class="image" src="/pics/MM01.gif" width="152" height="152" /></span>
            </div>
            <div class="msg_c_r w155">
                <span class="span_c"><p><a class="title"></a></p></span>
                <span class="btn_span"><a class="redlink">离  开</a></span>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="userid" name="userid" value="<%=Model.UserID %>"/>
<%
    string topoints = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InterestAddFansToPoints");
    int mypoints = Model.Member.Points;
%>
<input type="hidden" id="topoints" name="topoints" value="<%=topoints %>"/>
<input type="hidden" id="mypoints" name="mypoints" value="<%=mypoints %>" />
<%if (ViewData["nowCreatedInterestToPoints"] != null)
  {
      %>
        <input type="hidden" id="createdtppoints" name="createdtppoints" value="<%=ViewData["nowCreatedInterestToPoints"].ToString() %>" />
      <%
  } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<style type="text/css">
     img { border:0;}
    .w155 { width:155px;}
    .w326 { width:326px;}
    .w440 { width:440px;}
    .w600{ width:600px;}
    #box_msg { }
    .msg_box { overflow:hidden; display:block; border:2px solid #ededed; color:#444;}
    .msg_title { padding:0 0 0 35px;  height:50px; display:block; overflow:hidden; clear:both;  }
    .msg_title h2 { display:block; width:280px; height:45px; line-height:45px; font-size:24px; margin:0; float:left; font-family:"\5FAE\8F6F\96C5\9ED1"; }
    .msg_title span.msg_close { display:block; float:right; width:44px; height:40px;}
    .msg_content { display:block; padding:30px 40px; overflow:hidden;}
    .msg_c_l { width:152px; float:left; padding-right:20px; border-right:1px solid #ededed;}
    .msg_c_l span, .msg_c_r span{ display:block; padding-bottom:10px; line-height:25px; text-align:left; font-size:16px; }
    .msg_c_l span img { width:152px; height:152px;}
    .msg_c_r { display:block; float:left; padding-left:20px; }
    .textarea3 { background:#fff url(/pics/input_long_bg03.png) left top no-repeat; line-height:25px; font-size:14px; color:#666; overflow-y:hidden; border:1px solid #cdcdcd;}
    .msg_c_r span.span_c { padding-bottom:20px;}
    .msg_c_r span.btn_span { line-height:35px; height:35px; padding-right:10px;}
    .msg_c_r span a.redlink { color:#fff; font-size:16px; text-decoration:none; font-weight:600; float:right; width:100px; background:#b40001; line-height:32px; text-align:center; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
    .fb_box_com span.xiqu_t font {  font-family:"Adobe 黑体 Std"; color:#c3c3c3; font-size:16px; font-weight:bolder; padding-right: 15px;}
    .fb_box_com span.xiqu_t { line-height:28px; text-align:left; font-family:"微软雅黑"; color:#333; font-size:16px;}  
    .xiqu_l { width:160px; height:214px; float:left; padding-left:12px; padding-top:30px; position:relative;  }
    .xiqu_c { width:60px; float:left; padding-top:30px; height:214px; }
    .xiqu_r { width:360px; float:left; height:244px;}
    .xiqu_l img { width:152px; display:block;}
    .xiqu_l a.self_create { position:absolute; display:block; width:34px; height:23px; left:12px; top:7px; background:url(/pics/self_bg.png) no-repeat; z-index:5;}
    .xiqu_l a.self_create:hover { background:url(/pics/self_bghover.png) no-repeat;}
    .xiqu_l a.self_err { position:absolute; display:block; width:22px; height:22px; right:0; top:18px; background:url(/pics/error.png) no-repeat; z-index:5; visibility:hidden;}
    .fb_box_com .xiqu_l span {display:block; line-height:28px; text-align:center; padding-bottom:5px;}
    .relation_list { width:362px; list-style:none; padding-top:5px;}
    .relation_list li { display:block; float:left; width:75px; height:115px; padding-right:12px; padding-left:3px; padding-top:5px; position:relative;}
    .relation_list li a { text-align:center; display:block; color:#0099cc;}
    .add_xiqu a{ color:#0099cc;}
    .relation_list li a:hover,.add_xiqu a:hover { color:#b40001;}
    .relation_list li a img { display:block; width:75px; height:75px;}
    .relation_list li em { display:none; background:url(/pics/add_xiqu.png) no-repeat; width:21px; height:21px; position:absolute; right:2px; top:2px; cursor:pointer; }
    
    /* 添加兴趣样式*/
    .fb_box_com a.blue01 { color:#0099cc; font-size:12px; text-align:right; text-decoration:none; display:block; float:right; width:120px; }
    .fb_box_com a.blue0:hover { color:#b40001; font-size:12px;}
    .all-group { padding-left:35px; overflow:hidden }
    .all-group dl.grouplist { list-style:none;  display:block; float:left; width:87px; height:148px; overflow:hidden; padding:0 0 0 3px; position:relative; font-family:"微软雅黑"; z-index:5;}
    .all-group dl.grouplist dt{ width:75px; height:75px; display:block; padding-right:12px; padding-top:23px; position:relative; z-index:10; overflow:hidden; float:left; }
    .all-group dl.grouplist dt a { display:block; width:75px; height:75px;}
    .all-group dl.grouplist dd { line-height:20px; display:block; width:75px; height:40px; float:left;}
    .all-group dl.grouplist dd span { display:block; text-align:center; line-height:20px;}
    .all-group dl.grouplist dd span a { color:#0099cc; text-decoration:none;}
    .all-group dl.grouplist dd span a:hover { color:#b40001;}
    .all-group dl.grouplist dd span.gray02 { color:#ccc; font-size:12px; }
    .all-group dl.grouplist em { position:absolute; display:none; z-index:25;}
    .all-group dl.grouplist em.del { width:22px; height:22px; right:3px; top:11px; background:url(/pics/error.png) no-repeat; }
    .all-group dl.grouplist em.joiner { width:21px; height:21px; right:3px; top:12px; background:url(/pics/add_xiqu.png) no-repeat;}
    .all-group dl.grouplist a.creater { display:block; z-index:20; width:34px; height:23px; position:absolute; left:3px; top:0;  background:url(/pics/self_bg.png) 0 0 no-repeat;}
    .all-group dl.grouplist a.creater:hover { background:url(/pics/self_bghover.png) no-repeat;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/main_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/dialog.js"></script>
<script type="text/javascript" src="/js/msg.js" charset="utf-8"></script>

<script type="text/javascript">
    uploadpath = '<%=Model.UploadPath %>';
    var nowToPoints = 0;//当前添加兴趣所需要的米果数
    var maxInterestCount = 0;//添加或创建兴趣的上限
    var myInterestCount = 0;//当前添加或创建兴趣的数量
    $(document).ready(function () {
        interestCenterProvider.getMyinterest(function (data) {
            var data = $.parseJSON(data);
            var interestList = data.interestList;
            var interestListTo = data.interestListTo;
            var str = getInterestStr(interestList, interestListTo);
            if (str == "") {
                str = "你未添加兴趣！";
            }
            $("#interestmain").html(str);
            showfunction();
        });

        $("#wrap").css("background", "#DEDEE0");
        loadToSth();
    });
    //计算添加兴趣和创建兴趣时所需要的参数
    function loadToSth() {
        nowToPoints = 0;
        var pointslist = $("#topoints").val().split('|');
        var ids = $("#interestIDs").val().split(',');
        var ifmovre = true;
        for (var i = 0; i < pointslist.length; i++) {
            if (ids.length == parseInt(pointslist[i].split(',')[0])) {
                nowToPoints = pointslist[i].split(',')[1];
                ifmovre = false;
            }
        }
        maxInterestCount = parseInt(pointslist[pointslist.length - 1].split(',')[0]);
        myInterestCount = ids.length;
    }
    function showfunction() {
        $('.relation_list').each(function () {
            $(this).find('li').hover(function () {
                $(this).find('em').css({ "display": "block" });
            }, function () {
                $(this).find('em').css({ "display": "none" });
            });
        });
        $('.Notself').hover(function () {
            $(this).find('a.self_err').css({ "visibility": "visible" });
        }, function () {
            $(this).find('a.self_err').css({ "visibility": "hidden" });
        });
        $('.grouplist').each(function () {
            $(this).find('dt').hover(function () {
                $(this).parent().find('em.del').css({ "display": "block" });
                $(this).parent().find('em.joiner').css({ "display": "block" });
            }, function () {
                $(this).parent().find('em.del').css({ "display": "none" });
                $(this).parent().find('em.joiner').css({ "display": "none" });
            });

        });
    }
    //显示删除兴趣加粉的窗口
    function showdeleteinterestfans(id, title, imgurl) {
        $("#content").val("");
        $("#box_msg").css("display", "block");
        $("#box_msg .title").html(title);
        $("#box_msg .redlink").bind("click", function () { deleteinterestfans(id, $(this)); });
        $("#box_msg .image").attr("src", photofunctions.getprofileiconphotoname(imgurl));
    }
    //删除兴趣加粉
    function deleteinterestfans(id, obj) {
        interestCenterProvider.delInterestFans(id, function (data) {
            $("#iinterest" + id).remove();
            //更新发布内容时需要的兴趣等等
            var interestIDs = $("#interestIDs").val();
            var interestidlist = interestIDs.split(',');
            interestIDs = "";
            for (var i = 0; i < interestidlist.length; i++) {
                if (interestidlist[i] != id) {
                    interestIDs += interestidlist[i] + ",";
                }
            }
            interestIDs = interestIDs != "" ? interestIDs.substr(0, interestIDs.length - 1) : "";
            $("#interestIDs").val(interestIDs);
            $("#interestIDshistory").val(interestIDs);
            $("ul.chosepic_list li[data-ID='" + id + "']").remove();

            loadToSth();

            $("#box_msg").css("display", "none");
            obj.unbind("click");
        });
    }
    //显示添加兴趣加粉的窗口
    function showaddinterestfans(id, title, imgurl) {

        loadToSth();

        if (myInterestCount > maxInterestCount) {
            $.jBox.tip("你当前拥有的兴趣组已达到上限", 'error');
        }
        else if ($("#interestIDs").val().indexOf(id) >= 0) {
            $.jBox.tip("不能添加相同的兴趣组", 'error');
        }
        else {
            if (nowToPoints > 0) {
                $.jBox.confirm("此次添加兴趣需要" + nowToPoints + "米果，确定添加吗？", "确认提示", function (data) {
                    if (data == "ok") {
                        var mypoints = $("#mypoints").val();
                        if (parseInt(mypoints) < parseInt(nowToPoints)) {
                            $.jBox.tip("米果不够，还差" + (parseInt(nowToPoints) - parseInt(mypoints)) + "个米果，不能添加兴趣。<br/>如果你确定有这么多米果，请尝试刷新。", 'error');
                        }
                        else {
                            $("#box_msg_addfans").css("display", "block");
                            $("#box_msg_addfans .title").html(title);
                            $("#box_msg_addfans .redlink").bind("click", function () { addinterestfans(id, title, $(this)); });
                            $("#box_msg_addfans .image").attr("src", photofunctions.getprofileiconphotoname(imgurl));
                            $("#content").focus();
                        }
                    }
                });
            }
            else {
                $("#box_msg_addfans").css("display", "block");
                $("#box_msg_addfans .title").html(title);
                $("#box_msg_addfans .redlink").bind("click", function () { addinterestfans(id, title, $(this)); });
                $("#box_msg_addfans .image").attr("src", photofunctions.getprofileiconphotoname(imgurl));
                $("#content").focus();
            }
        }
    }
    //添加兴趣加粉
    function addinterestfans(id, title, obj) {
        if ($("#content").val() == "") {
            $.jBox.tip("和群组里面先到的同学说点什么吧", 'error');
            $("#content").focus();
        }
        else {
            var thisobj = obj;
            thisobj.unbind("click");
            thisobj.attr("click", "");
            thisobj.html("添加中...");
            interestCenterProvider.AddInterestFanss(id, function (data) {
                var data = $.parseJSON(data);
                if (data.toString() == "false" || data.toString() == "False") {
                    $("#content").val("");
                    $("#box_msg_addfans").css("display", "none");
                    thisobj.unbind("click");
                    thisobj.attr("click", "");
                    thisobj.html("加  入");
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                }
                else {
                    var interestList = data.interestList;
                    var interestListTo = data.interestListTo;
                    var str = getInterestStr(interestList, interestListTo);
                    $("#interestmain").html(str);

                    //更新页面上隐藏域所保存的米果数量
                    $("#mypoints").val(parseInt($("#mypoints").val()) - parseInt(nowToPoints));

                    showfunction();

                    //更新发布内容时需要的兴趣编号等等
                    var intereststr = "";
                    var ids = "";
                    for (var i = 0; i < interestList.length; i++) {
                        ids += interestList[i].ID + ",";
                        intereststr += "<li class=\"userInterest\" data-ifopen=\"true\" data-ID=\"" + interestList[i].ID + "\"><a><img src=\"" + photofunctions.geticonphotoname(interestList[i].ICONPath) + "\" height=\"51\" width=\"51\" title=\"" + interestList[i].Title + "\" alt=\"" + interestList[i].Title + "\" /></a><em></em></li>";
                    }
                    ids = ids != "" ? ids.substr(0, ids.length - 1) : "";
                    $("ul.chosepic_list").html(intereststr);
                    $("#interestIDs").val(ids);
                    $("#interestIDshistory").val(ids);

                    //加载兴趣选择的点击事件
                    $("ul.chosepic_list li.userInterest").click(function () {
                        interestClick($(this));
                    });
                    interestHover();

                    loadToSth();

                    //添加说说内容
                    var permissions = $("#permissions").val();
                    var interestIDs = ids;
                    var content = $("#content").val();
                    ContentProvider.InsertInterestContent(permissions, interestIDs, content, id, "添加兴趣组", function (data) {
                        var data = $.parseJSON(data);
                        $("#content").val("");
                        $("#box_msg_addfans").css("display", "none");
                        thisobj.unbind("click");
                        thisobj.attr("click", "");
                        thisobj.html("加  入");
                        $.jBox.tip("加粉已成功", 'info');
                        var url = "http://www.moooyo.com/InterestCenter/ShowInterest/" + id;
                        //分享到微博
                        var iffxwb = ShareToWB(title + " " + content, url);
                    });
                }
            });
        }
    }
    function getInterestStr(interestList, interestListTo) {
        var str = "";
        str += "<div class=\"fb_box_com xqtitle w600\" style=\"margin-top:20px;\">";
        str += "<span class=\"xiqu_t\">";
        str += "<font color=\"#c3c3c3\" style=\"font-weight:800\">▕ </font>你所在的群组（" + interestList.length + "）";
        str += "</span>";
        str += "</div>";
        str += "<div class=\"fb_box_com xqdis w600\">";
        str += "<div class=\"all-group\">";
        for (var i = 0; i < interestList.length; i++) {
            var obj = interestList[i];
            if (obj.Creater.MemberID == $("#userid").val()) {
                str += "<dl class=\"grouplist\">";
                str += "<a href=\"/InterestCenter/ShowMyInterest\" class=\"creater\"></a>";
                str += "<dt><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\"><img title=\"" + obj.Title + "\" src=\"" + photofunctions.getprofileiconphotoname(obj.ICONPath) + "\" width=\"75\" height=\"75\" title=\"\" /></a></dt>";
                str += "<dd><span><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\" title=\"" + obj.Title + "\">" + (obj.Title.length > 5 ? obj.Title.substr(0, 5) + ".." : obj.Title) + "</a></span><span class=\"gray02\">(" + obj.FansCount + ")</span></dd>";
                str += "</dl>";
            }
            else {
                str += "<dl class=\"grouplist\" id=\"iinterest" + obj.ID + "\">";
                str += "<dt><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\"><img title=\"" + obj.Title + "\" src=\"" + photofunctions.getprofileiconphotoname(obj.ICONPath) + "\" width=\"75\" height=\"75\" title=\"\" /></a><em class=\"del\" onclick=\"showdeleteinterestfans('" + obj.ID + "','" + obj.Title + "','" + obj.ICONPath + "')\"></em></dt>";
                str += "<dd><span ><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\" title=\"" + obj.Title + "\">" + (obj.Title.length > 5 ? obj.Title.substr(0, 5) + ".." : obj.Title) + "</a></span><span class=\"gray02\">(" + obj.FansCount + ")</span></dd>";
                str += "</dl>";
            }
        }
        str += "</div>";
        str += "</div>";
        str += "<div class=\"fb_box_com  xqtitle w600\">";
        str += "<span class=\"xiqu_t\">";
        str += "<font color=\"#c3c3c3\" style=\"font-weight:800\">▕ </font>这些群组的用户还喜欢";
        str += "</span>";
//        str += "<a href=\"#\" class=\"blue01\">查看所有群组</a>";
        str += "</div>";
        str += "<div class=\"fb_box_com xqdis w600\">";
        str += "<div class=\"all-group\">";
        for (var j = 0; j < interestListTo.length; j++) {
            var obj = interestListTo[j];
            str += "<dl class=\"grouplist\">";
            str += "<dt><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\"><img title=\"" + obj.Title + "\" src=\"" + photofunctions.getprofileiconphotoname(obj.ICONPath) + "\" width=\"75\" height=\"75\" title=\"\" /></a><em class=\"joiner\" onclick=\"showaddinterestfans('" + obj.ID + "','" + obj.Title + "','" + obj.ICONPath + "')\"></em></dt>";
            str += "<dd><span ><a href=\"/InterestCenter/ShowInterest/" + obj.ID + "\" title=\"" + obj.Title + "\">" + (obj.Title.length > 5 ? obj.Title.substr(0, 5) + ".." : obj.Title) + "</a></span><span class=\"gray02\">(" + obj.FansCount + ")</span></dd>";
            str += "</dl>";
        }
        str += "</div>";
        str += "</div>";
        return str;
    }
    //创建兴趣
    function showCreatedInterest() {
        var createdtppoints = $("#createdtppoints").val();
        var mypoints = $("#mypoints").val();
        if (myInterestCount > maxInterestCount) {
            $.jBox.tip("你当前拥有的兴趣组已达到上限", 'error');
        }
        else if (createdtppoints != null && (parseInt(mypoints) < parseInt(createdtppoints))) {
            $.jBox.tip("米果不够，还差" + (parseInt(createdtppoints) - parseInt(mypoints)) + "个米果，不能创建兴趣组。<br/>如果你确定有这么多米果，请尝试刷新。", 'error');
        }
        else {
            window.location = "/InterestCenter/AddInterest";
        }
    }
</script>
</asp:Content>
