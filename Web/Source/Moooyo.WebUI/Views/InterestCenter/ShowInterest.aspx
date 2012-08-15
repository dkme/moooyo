<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.interestObj.Title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form>
<%if (!Model.isFans)
  {
    string addinterestfanstopoints = CBB.ConfigurationHelper.AppSettingHelper.GetConfig("InterestAddFansToPoints");
    int mypoints = Model.Member.Points;
    string iids = "";
    foreach (var obj in Model.interestlist) { iids += obj.ID + ","; }
    iids = iids != "" ? iids.Substring(0, iids.Length - 1) : iids;
    %>
<input type="hidden" id="addinterestfanstopoints" name="addinterestfanstopoints" value="<%=addinterestfanstopoints %>"/>
<input type="hidden" id="mypoints" name="mypoints" value="<%=mypoints %>" />
<input type="hidden" id="interestCount" name="interestCount" value="<%=Model.interestCount %>" />
<input type="hidden" id="interestids" name="interestids" value="<%=iids %>" />
</form>
<%}%>
    <div class="box_demo1 w960" style="background:#fff; width:970px;">
        <div class="mt10"></div>
        <div class="Set_box1 p40" style="padding:0 40px;">
        <div class="mt10"></div>
        <div class="admin_box">
            <div class="group_admin" style="width:160px;  float:left;">
                <span class="admin_pic"><img src="<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle)%>" height="152" width="152" />
                <%if (Model.AlreadyLogon)
                  { 
                      %><em onclick="window.location='/InterestCenter/UpdateInterestFansList/<%=Model.interestObj.ID %>'" class="cursor"><%=Model.interestObj.FansCount%><q>柚子</q></em><%
                  }
                  else
                  {
                      %><em class="cursor"><%=Model.interestObj.FansCount%><q>柚子</q></em><%
                  }
                          %></span>
                <span class="admin_info"></span>
            </div>
            <div class="group_member" style="padding-left:30px; width:670px; float:right;" >
                <div class="join_group">
                        <div class="join_t"><span class="span-l"><%=Model.interestObj.Title.Length > 18 ? Model.interestObj.Title.Substring(0, 18) + ".." : Model.interestObj.Title%></span><span class="span-r"></span></div>
                        <div class="join_g">
                        <%string[] tablist = Model.interestObj.Classes.Split(',');
                          foreach (var str in tablist) { %><nobr><a><%=str %></a></nobr><%}
                           %>
                        </div>
                        <div class="join_d"><%=Comm.replaceToN(Model.interestObj.Content.Length > 300 ? Model.interestObj.Content.Substring(0, 300) + ".." : Model.interestObj.Content)%></div>
                        <div class="join_b">
                                <span class="join-b-l">
                                    <%
                                        if (Model.AlreadyLogon)
                                        {
                                            if (Model.interestObj.Creater.MemberID != Model.UserID)
                                            { 
                                          %>
                                        <span class="interestBu">
                                        <%if (!Model.isFans)
                                          { %><a class="join_a" onclick="showAddFans()">加&nbsp;&nbsp;&nbsp;入</a><%} %>
                                        <%else
                                          {%><a class="quit-out" onclick="delFans('<%=Model.interestObj.ID %>',$(this))">退&nbsp;&nbsp;&nbsp;出</a><%}%>
                                        </span>
                                    <%}
                                            else
                                            {
                                          %>
                                          <span class="admin_group_r" style="width:100px; height:35px; padding:0px; margin:0px;"><span class="g_edit" style="padding:0px; margin:0px;"><a href="/InterestCenter/UpdateInterest/<%=Model.interestObj.ID %>" title="修改"><img src="/pics/group_edit.gif" /></a><a title="分享" data-oldobj="showInterest" onmouseover="shareClick('http://www.moooyo.com/InterestCenter/ShowInterest/<%=Model.interestObj.ID %>','<%=Model.interestObj.Title %>','<%=Model.interestObj.Content.Replace("\n", "") %>',$(this),'http://www.moooyo.com<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle) %>')"><img src="/pics/group_back.gif"/></a></span></span>
                                          <%
                                            }
                                        }
                                        else
                                        {
                                            %><span class="admin_group_r" style="width:100px; height:35px; padding:0px; margin:0px;"><span class="g_edit" style="padding:0px; margin:0px;"><a title="分享" data-oldobj="showInterest" onmouseover="shareClick('http://www.moooyo.com/InterestCenter/ShowInterest/<%=Model.interestObj.ID %>','<%=Model.interestObj.Title %>','<%=Model.interestObj.Content.Replace("\n", "") %>',$(this),'http://www.moooyo.com<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle) %>')"><img src="/pics/group_back.gif"/></a></span></span><%
                                        } %>
                                    <a class="blue01" onclick="history.back()">返回</a>
                                </span>
                            <span class="join-b-r">
                            <font>创建者</font>
                                <dl>
                                    <dt><a href="/Content/TaContent/<%=Model.interestObj.Creater.MemberID %>/all/1" class="blue01" target="_blank"><img src="<%=Comm.getImagePath(Model.interestObj.Creater.ICONPath,ImageType.Icon) %>" height="29" width="29" /></a></dt>
                                    <dd><a href="/Content/TaContent/<%=Model.interestObj.Creater.MemberID %>/all/1" class="blue01" target="_blank"><%=Model.interestObj.Creater.NickName %></a></dd>
                                </dl>
                            </span>
                        </div>
                        <div class="join_border_b"></div>
                    </div>
                    <div class="fan_count" style="padding-top:20px;">活跃用户</div>
                    <div class="member_list" style="padding-top:15px;">
                    <%foreach (var obj in Model.interestHotFans)
                      { %>
                        <dl class="other_fans">
                            <dt><a href="/Content/TaContent/<%=obj.Creater.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.Creater.ICONPath,ImageType.Icon) %>" width="49" height="49" alt="" /></a></dt>
                            <dd class="blue02" title="<%=obj.Creater.NickName %>"><%=obj.Creater.NickName.Length > 4 ? obj.Creater.NickName.Substring(0, 4) : obj.Creater.NickName%></dd>
                        </dl>
                        <%}%>
                    </div>
                    <div class="clear"></div>
                    <div class="fan_count" style="padding-top:20px;">最新加入</div>
                    <div class="member_list" style="padding-top:15px;">
                    <%foreach (var obj in Model.interestFansListObje)
                      { %>
                        <dl class="other_fans">
                            <dt><a href="/Content/TaContent/<%=obj.Creater.MemberID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.Creater.ICONPath,ImageType.Icon) %>" width="49" height="49" alt="" /></a></dt>
                            <dd class="blue02" title="<%=obj.Creater.NickName %>"><%=obj.Creater.NickName.Length > 4 ? obj.Creater.NickName.Substring(0, 4) : obj.Creater.NickName%></dd>
                        </dl>
                        <%}%>
                    </div>
                    <div class="clear"></div>
                    <div class="join_border_b"></div>
                </div>
            </div>
            <div class="padding_b50"></div>
            <div class="loading"></div>
            <div class="padding_b50">
                <div id="over_div"></div>
                <div  id="join_box">
                    <div class="msg_box w600">
                        <div class="msg_box w600">
                            <div class="msg_title">
                                <h2>加入兴趣群组</h2>
                                <span class="msg_close"><a href="#" onclick="cancelDlg()"><em></em></a></span>
                            </div>
                            <div class="msg_content">
                                <div class="msg_c_l"> <span><img src="<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle)%>" width="152" height="152" /></span> <span><%=Model.interestObj.Title %></span></div>
                                <div class="msg_c_r w326">
                                    <span>和群组里面先到的同学说点什么吧：</span>
                                    <span><textarea id="content" name="textarea" class="textarea3 g" style="width:315px; height:120px;"></textarea></span>
                                    <%if (!Model.isFans)
                                      { %>
                                    <span class="btn_span"><a class="redlink" onclick="addFans('<%=Model.interestObj.ID %>',$(this),'<%=Model.interestObj.Title %>')">加  入</a></span>
                                    <%} %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<div style="display:none;">
<%Html.RenderAction("AddRightPanel", "Content", new { contentObj = "" }); %>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<link rel="stylesheet" type="text/css" href="/css/msg.css"/>
<style type="text/css">
    .group_member { display:block; float:right;}
    .group_member .fan_count { height:40px; line-height:40px; text-align:left; color:#333;  font-size:16px; overflow:hidden;}
    .join_group { overflow:hidden;}
    .join_t { height:50px; line-height:50px;}
    .join_t span.span-l{ font-size:30px; font-family:"微软雅黑"; text-align:left; color:#444; }
    .join_t span.span-l a{ width:40px; height:35px; background:#f0f0f0; position:relative; vertical-align:middle; display:inline-block;  margin-left:20px;}
    .join_t span.span-l a em { position:absolute; display:block; width:22px; height:20px; left:8px; top:8px;}
    .join_g { line-height:25px; text-align:left; margin-top:10px; }
    .join_g a { display:block; float:left; text-decoration:none; padding-right:10px; text-align:left; font-size:12px; font-family:"宋体"; color:#999;}
    .join_g a:hover { color:#666; }
    .join_d { width:100%; display:block; overflow:hidden; padding-top:25px; line-height:25px; font-size:14px; font-family:"微软雅黑"; color:#666; text-align:left;  }
    .join_b { width:100%; height:35px; overflow:hidden; margin-top:20px;}
    .join_b .join-b-l { display:block; width:70%; float:left;}
    .join_b .join-b-l a { line-height:35px; text-align:left; color:#0099cc; font-family:"宋体";}
    .join_b .join-b-l a:hvoer { color:#b40001;}
    .join_b .join-b-l a.join_a { display:block; float:left; height:35px; line-height:35px; color:#fff; font-size:14px; font-family:"微软雅黑"; width:60px; margin-right:20px; padding-left:40px; background:#79e48f url(/pics/self_add.png) 15px 10px no-repeat; font-weight:bold;}
    .join_b .join-b-l a.join_a:hover { background-color:#88e98b;}
    .join_b .join-b-l a.quit-out {  display:block; float:left; height:35px; line-height:35px; color:#b40001; font-size:14px; font-family:"微软雅黑"; width:60px; margin-right:20px; padding-left:40px; background:#ededee url(/pics/quit-out.png) 15px 15px no-repeat; font-weight:bold;}
    .join_b .join-b-r { display:block; width:30%; float:right; line-height:35px; color:#444; font-size:14px; font-family:"微软雅黑";}
    .join_b .join-b-r font{ display:block; float:left; padding-right:20px;}
    .join_b .join-b-r dl { display:block; width:130px; float:left; padding:0; overflow:hidden;}
    .join_b .join-b-r dl dt { display:block; width:29px; height:29px; float:left; padding:0; margin-right:10px; margin-top:2px; border-left:1px solid #9da0a4; border-top:1px solid #9da0a4;}
    .join_b .join-b-r dl dt img { width:29px; height:29px; vertical-align:top;}
    .join_b .join-b-r dl dd { display:block; width:85px; float:left;}
    .join_b .join-b-r dl dd a { text-align:left; line-height:35px; color:#0099cc; font-size:14px; font-family:"微软雅黑"; }
    .join_b .join-b-r dl dd a:hover { color:#b40001;}
    .join_border_b { height:30px; border-bottom:1px dashed #ddd;}
    .admin_pic em{ position:absolute; background:#b40001; height:35px; line-height:35px; font-size:18px; font-weight:bold; width:95px; left:0; bottom:15px; font-family:"Arial"; text-align:center; display:block; z-index:10; color:#fff;}
    .admin_pic em q{ font-family:"Adobe 黑体 Std"; font-weight:normal; font-size:18px;}
    .admin_pic { width:152px; height:152px; padding-bottom:10px; position:relative; }
    .admin_box { padding:0 10px;}
    .member_list dl.other_fans { width:68px; height:78px; padding:0 18px 10px 0; display:block; float:left;}
    .member_list dl.other_fans dt { width:49px; height:49px; text-align:center; display:block; margin:8px 22px 0 2px; border-left:1px solid #9da0a4; border-top:1px solid #9da0a4; position:relative; }
    .member_list dl.other_fans dt em { width:21px; height:21px; display:block; position:absolute; background:url(/pics/error.png) no-repeat; right:-8px; top:-8px; visibility:hidden; cursor:pointer;}
    .member_list dl.other_fans dd { text-align:center; display:block; width:52px; line-height:20px; color:#0099cc}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script type="text/javascript" src="/js/base_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/data_<%=Model.JsVersion %>.js"></script>
<script type="text/javascript" src="/js/main_<%=Model.JsVersion %>.js"></script>
<script language="javascript" type="text/javascript" src="/js/msg.js" charset="utf-8"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    function cancelDlg() {
        $('#box_msg').hide(1000);
        $('#over_div , #join_box ').hide();
        $('#zz_box').css({ "display": "none" });
        $(".dialog").remove();
    }
    $(document).ready(function () {
        $('#wrap').css({"background":"none"});
        $('#over_div , #join_box ').hide();
        $('.select_list').hide(); //初始ul隐藏
        $('.select_box span').click(function () { //鼠标点击函数
            $(this).parent().find('ul.select_list').slideDown();  //找到ul.son_ul显示
            $('ul.select_list li').eq(0).addClass('hover');
            $(this).parent().find('li').hover(
                function () {
                    $(this).addClass('hover'); $(this).attr("data-select", "yes");
                },
                function () {
                    $(this).removeClass('hover'); $(this).attr("data-select", "none");
                }
            );
            //li的hover效果
            $(this).parent().hover(function () { }, function () { $(this).parent().find("ul.select_list").slideUp(); });
        });
        $('ul.select_list li:first').each(function () {
            if ($(this).keypressed == true) { if (event.keyCode == '38' || event.keyCode == '33') { alert("chenggng"); } }
        });
        //键盘按下时间
        $('ul.select_list li').click(function () {
            if ($(this).eq(3).click()) { $('.chosepic').css({ "display": "block" }); }
            $(this).parents('li').find('span').html($(this).html());
            $(this).parents('li').find('ul').slideUp();
        });
    });
    //显示兴趣加粉的对话框
    function showAddFans() {
//        var timeout = setTimeout(function () {
            if ($("#addinterestfanstopoints").val() != null && $("#interestCount").val() != null) {
                var pointslist = $("#addinterestfanstopoints").val().split('|');
                var interestCount = parseInt($("#interestCount").val());
                var ifmovre = true; //判断当前用户是否在需要扣除米果的范围中
                for (var i = 0; i < pointslist.length; i++) {
                    if (interestCount == parseInt(pointslist[i].split(',')[0])) {
                        points = pointslist[i].split(',')[1];
                        ifmovre = false;
                    }
                }
                //判断用户添加的兴趣是否超出上限
                if (interestCount > parseInt(pointslist[pointslist.length - 1].split(',')[0])) {
                    $.jBox.tip("已达到添加兴趣组的上限", 'error');
                }
                else {
                    var ifok = false; //判断用户是否能添加兴趣
                    if (!ifmovre) {
                        $.jBox.confirm("此次添加兴趣需要" + points + "米果，确定添加吗？", "确认提示", function (data) {
                            if (data == "ok") {
                                var mypoints = $("#mypoints").val();
                                if (parseInt(mypoints) < parseInt(points)) {
                                    $.jBox.tip("米果不够，还差" + (parseInt(points) - parseInt(mypoints)) + "个米果，不能添加兴趣。<br/>如果你确定有这么多米果，请尝试刷新。", 'error');
                                }
                                else {
                                    openaddfans();
                                }
                            }
                        });
                    }
                    else {
                        ifok = true;
                    }
                    //打开添加兴趣的对话框
                    if (ifok) {
                        openaddfans();
                    }
                }
            }
//            clearTimeout(timeout);
//        }, 500);
    }
    function openaddfans() {
        $('#over_div, #join_box').show();
        var top = $(window).height() / 2 - 150;
        var left = $(window).width() / 2 - 250;
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
        $('#join_box').css({
            "position": "fixed",
            "display": "block",
            "zIndex": "15",
            "left": left + "px",
            "top": top + "px",
            "background": "white"
        });
    }
    //添加粉丝
    function addFans(iid, thisobj, title) {
        var join_a = $('.join_a');
        var iids = $("#interestids").val();
        var content = $("#content").val();
        if (content == "") {
            $.jBox.tip("请填写内容！", 'error');
            thisobj.html("加  入");
        }
        else {
            thisobj.unbind("click");
            thisobj.attr("click", "");
            thisobj.parent().html("<a class=\"redlink\">加入中...</a>");
            //添加兴趣
            interestCenterProvider.AddInterestFanss(iid, function (data) {
                var data = $.parseJSON(data);
                if (data.toString() == "false" || data.toString() == "False") {
                    $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                    setTimeout(function () {
                        window.location.reload();
                    },1000);
                }
                else {
                    //更新发布内容时需要的兴趣编号等等
                    //添加说说内容
                    if (iids != "")
                        iids += "," + iid;
                    else
                        iids = iid;
                    var interestIDs = iids;
                    ContentProvider.InsertInterestContent("0", interestIDs, content, iid, "添加兴趣组", function (data) {
                        var data = $.parseJSON(data);
                        //分享到微博
                        var url = "http://www.moooyo.com/InterestCenter/ShowInterest/" + iid;
                        var iffxwb = ShareToWB(title + " " + content, url);
                        window.location.reload();
                    });
                }
            });
        }
    }
    //删除粉丝
    function delFans(iid, thisobj) {
        thisobj.html("退出中..");
        interestCenterProvider.delInterestFans(iid, function (data) {
            var data = $.parseJSON(data);
            if (data.toString() == "false" || data.toString() == "False") {
                $.jBox.tip("系统维护中，给您带来了不便，请谅解！", 'error');
                thisobj.html("退&nbsp;&nbsp;&nbsp;出");
            }
            else {
                window.location.reload();
            }
        });
    }
</script>
</asp:Content>
