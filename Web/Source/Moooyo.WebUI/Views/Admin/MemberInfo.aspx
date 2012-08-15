<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.SystemInfoModels>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户状态
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav">
        <% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
    </div>
    <div class="Admin_Dic_LeftNav">
        <ul class="leftlist magT10">
            <li><a href="/Admin/MemberInfo?type=memberinfo">用户状态</a></li>
            <li><a href="/Admin/MemberInfo?type=daymemberinfo">当天用户状态</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div class="contenttit">
            <% if (Model.type == "memberinfo")
               { %>
            <div style="line-height: 25px;">
                <font color="#ff0000">鼠标悬停某项上可显示附带信息</font></div>
            <div>
                <div class="fl">
                    <form id="formMemberInfo">
                    按操作类别
                    <select id="viewTypeMemberInfos" name="viewTypeMemberInfos" onchange="viewMemberInfos($(this).val())">
                        <option value="memberinfo" selected="selected">查看所有</option>
                        <option value="type0">上传头像</option>
                        <option value="type1">发布图片</option>
                        <option value="type2">评论</option>
                        <option value="type3">新浪微博登录</option>
                        <option value="type18">腾讯微博登录</option>
                        <option value="type4">豆瓣登录</option>
                        <option value="type19">人人登录</option>
                        <option value="type5">视频认证</option>
                        <option value="type6">删除内容</option>
                        <option value="type7">私信</option>
                        <option value="type8">资料修改</option>
                        <option value="type9">加入兴趣</option>
                        <option value="type10">访谈</option>
                        <option value="type11">新说说</option>
                        <option value="type12">mo别人</option>
                        <option value="type13">关注别人</option>
                        <option value="type14">提交建议</option>
                        <option value="type15">登出</option>
                        <option value="type16">登录</option>
                        <option value="type17">创建兴趣</option>
                        <option value="type20">未知</option>
                        <option value="type21">注册</option>
                        <option value="type22">允许登录</option>
                    </select>
                    </form>
                </div>
                <div class="fl">
                    &nbsp;&nbsp; 按时间：<input type="text" id="viewStartTimeMemberInfos" name="viewStartTimeMemberInfos"
                        style="margin-bottom: 30px; width: 80px;" />&nbsp;<input type="text" id="viewEndTimeMemberInfos"
                            name="viewEndTimeMemberInfos" style="width: 80px;" />&nbsp;<input type="button" onclick="viewMemberInfos('activitytime')"
                                value="确定" />
                </div>
                <div class="fl">
                    &nbsp;&nbsp;按源用户：<input type="text" id="viewFromMemberInfos" name="viewFromMemberInfos"
                        style="margin-bottom: 30px; width: 120px;" />&nbsp;<input type="button" onclick="viewMemberInfos('frommember')"
                                value="搜索" /></div>
            </div>
            <% } %>
            <% if (Model.type == "daymemberinfo")
               { %>
            <div style="width: 400px; float: left; line-height: 30px; font-size: 12px; padding-left: 20px;">
                按时间：<input type="text" id="timechang1" name="timechang1" style="margin-bottom: 30px;
                    width: 80px;" />&nbsp;<input type="text" id="timechang2" name="timechang2" style="width: 80px;" />&nbsp;<input
                        type="button" onclick="ssclick()" value="确定" />
            </div>
            <% } %>
        </div>
        <div style="padding-bottom: 20px; clear: both; float: none;">
            <% if (Model.type == "daymemberinfo")
               { %>
            <div style="width: 700px; line-height: 20px; text-align: center;">
                <div style="width: 220px; float: left; border-bottom: solid 1px #ddd;">
                    时间</div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    新增的用户</div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    新增的兴趣</div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    新增的话题</div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    新增的回复</div>
                <%for (int i = 0; i < Model.NewMembers.Count; i++)
                  { %>
                <div style="width: 220px; float: left; border-bottom: solid 1px #ddd;">
                    <%=Model.time%></div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    <%=Model.NewMembers[i]%></div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    <%=Model.NewInterests[i]%></div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    <%=Model.NewWenWens[i]%></div>
                <div style="width: 120px; float: left; border-bottom: solid 1px #ddd;">
                    <%=Model.NewAnswers[i]%></div>
                <%} %>
            </div>
            <% } %>
            <ul class="iwantlist magT10" id="listContiner" style="width: 1000px;">
            </ul>
        </div>
        <br />
        <br />
        <div class="clearfax">
        </div>
        <div style="width: 900px; padding-bottom: 30px; text-align: center; margin-top: 20px;
            clear: both; float: none;">
            <center>
                <div id="pager" class="verifyPager">
                </div>
            </center>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
#datepick-div, .datepick-inline {font-family: Arial,Helvetica,sans-serif;font-size: 14px;padding: 0;margin: 0;background: #ddd;color: #000;width: 185px;}
#datepick-div {display: none;border: 1px solid #777;z-index: 100; }
.datepick-inline {float: left;display: block;border: 0;}
.datepick-rtl {direction: rtl;}
.datepick-dialog {padding: 5px !important;border: 4px ridge #ddd !important;}
.datepick-disabled {position: absolute;z-index: 100;background-color: white;opacity: 0.5;filter: alpha(opacity=50);}
button.datepick-trigger {width: 25px;padding: 0px;}
img.datepick-trigger {margin: 2px;vertical-align: middle;}
.datepick-prompt {float: left;padding: 2px;}
* html .datepick-prompt {width: 185px;}
.datepick-control, .datepick-links, .datepick-header, .datepick {clear: both;float: left;width: 100%;color: #fff;}
.datepick-control {background: #400;padding: 2px 0px;}
.datepick-links {background: #000;padding: 2px 0px;}
.datepick-control, .datepick-links {font-weight: bold;font-size: 80%;}
.datepick-links label {padding: 2px 5px;color: #888;}
.datepick-clear, .datepick-prev {float: left;width: 34%;}
.datepick-rtl .datepick-clear, .datepick-rtl .datepick-prev {float: right;text-align: right;}
.datepick-current {float: left;width: 30%;text-align: center;}
.datepick-close, .datepick-next {float: right;width: 34%;text-align: right;}
.datepick-rtl .datepick-close, .datepick-rtl .datepick-next {float: left;text-align: left;}
.datepick-header {background: #333;text-align: center;font-weight: bold;height: 1.6em;}
.datepick-header select {background: #333;color: #fff;border: 0px;font-weight: bold;}
.datepick-header span {position: relative;top: 3px;}
.datepick {background: #ccc;text-align: center;font-size: 100%;}
.datepick a {display: block;width: 100%;}
.datepick-title-row {background: #777;}
.datepick-title-row th {text-align: center;font-weight: normal;}
.datepick-days-row {background: #eee;color: #666;}
.datepick-week-col {background: #777;color: #fff;}
.datepick-days-cell {color: #000;border: 1px solid #ddd;}
.datepick-days-cell a {display: block;}
.datepick-other-month {background: #fff;}
.datepick-week-end-cell {background: #ddd;}
.datepick-title-row .datepick-week-end-cell {background: #777;}
.datepick-week-over {background: #ddd;}
.datepick-unselectable {color: #888;}
.datepick-today {background: #fcc;}
.datepick-current-day {background: #777;color: #fff;}
.datepick-days-cell-over {background: #fff;color: #000;border: 1px solid #777;}
.datepick-status {background: #ddd;width: 100%;font-size: 80%;text-align: center;}
#datepick-div a, .datepick-inline a {cursor: pointer;margin: 0;padding: 0;}
.datepick-inline .datepick-links a {padding: 0 5px !important;}
.datepick-control a, .datepick-links a {padding: 2px 5px !important;color: #eee;}
.datepick-title-row a {color: #eee;}
.datepick-control a:hover {background: #fdd;color: #333;}
.datepick-links a:hover, .datepick-title-row a:hover {background: #ddd;color: #333;}
.datepick-multi .datepick {border: 1px solid #777;}
.datepick-one-month {float: left;width: 185px;}
.datepick-new-row {clear: left;}
.datepick-cover {display: none;display/**/: block;position: absolute;z-index: -1;filter: mask();top: -4px;left: -4px;width: 200px;height: 200px;}
.fl{float:left;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/admin.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery.datepick.js"></script>
    <script type="text/javascript" src="/js/jquery.datepick-zh-CN.js"></script>
    <script type="text/javascript">
        uploadpath = '<%=ViewData["uploadpath"] %>';

        var pageNo = 1;
        var pageCount = 0;
        var pageSize = 45;
        var pageCount2 = 0;
        var mypagecount = 0;

        var showType = "memberinfo";

        $(document).ready(function () {
            var type = "<%=Model.type %>";

            switch (type.toLowerCase()) {
                case "memberinfo": viewMemberInfos("memberinfo");
                    break;
                default:
                    break;
            }

            $("#timechang1").datepick({ dateFormat: 'yy-mm-dd' });
            $("#timechang2").datepick({ dateFormat: 'yy-mm-dd' });
            $("input#viewStartTimeMemberInfos").datepick({ dateFormat: 'yy-mm-dd' });
            $("input#viewEndTimeMemberInfos").datepick({ dateFormat: 'yy-mm-dd' });
        });

        function viewMemberInfos(type) {
            memberInfoCount(type);
            bindMemberInfos(type);
            showType = type;
        }

        function memberInfoCount(type) {
            if (type.toLowerCase().indexOf("type") >= 0) {
                var typeNum = type.substring(4, type.length);
                type = "activitytype";
            }

            $.ajaxSetup({ async: false });
            switch (type) {
                case "memberinfo":
                    adminMemberInfoDataProvider.getMemberInfoCount(function (data) {
                        pageCount = $.parseJSON(data);
                    });
                    break;
                case "activitytype":
                    adminMemberInfoDataProvider.getTypeMemberInfoCount(typeNum, function (data) {
                        pageCount = $.parseJSON(data);
                    });
                    break;
                case "activitytime":
                    var startTime = $("input#viewStartTimeMemberInfos").val(),
                    endTime = $("input#viewEndTimeMemberInfos").val();
                    if (startTime != "" && endTime != "") {
                        adminMemberInfoDataProvider.getTimeMemberInfoCount(startTime, endTime, function (data) {
                            pageCount = $.parseJSON(data);
                        });
                    }
                    else {
                        $.jBox.tip("请选择起止时间", 'error');
                        return;
                    }
                    break;
                case "frommember":
                    var fromMember = $("input#viewFromMemberInfos").val();
                    if (fromMember != "") {
                        if (!isNaN(fromMember)) {
                            adminMemberInfoDataProvider.getFromMemberInfoCount(fromMember, function (data) {
                                pageCount = $.parseJSON(data);
                            });
                        }
                        else {
                            $.jBox.tip("源用户短号必须为数字", 'error');
                            return;
                        }
                    }
                    else {
                        $.jBox.tip("请输入源用户短号", 'error');
                        return;
                    }
                    break;
                default:
                    break;
            }
            $.ajaxSetup({ async: true });
            
            pageCount2 = parseInt((parseInt(pageCount) + parseInt(pageSize) - 1) / parseInt(pageSize));
            mypagecount = pageCount2;
            if (mypagecount >= 1 && pageNo > mypagecount) {
                pageNo = mypagecount;
            }
            setPager(type);
        }

        function bindMemberInfos(type) {
            if (type.toLowerCase().indexOf("type") >= 0) {
                var typeNum = type.substring(4, type.length);
                type = "activitytype";
            }

            var objs;
            $.ajaxSetup({ async: false });
            switch (type) {
                case "memberinfo":
                    adminMemberInfoDataProvider.getMemberInfos(pageSize, pageNo, function (data) {
                        objs = $.parseJSON(data);
                    });
                    break;
                case "activitytype":
                    adminMemberInfoDataProvider.getTypeMemberInfos(typeNum, pageSize, pageNo, function (data) {
                        objs = $.parseJSON(data);
                    });
                    break;
                case "activitytime":
                    var startTime = $("input#viewStartTimeMemberInfos").val(),
                    endTime = $("input#viewEndTimeMemberInfos").val();
                    if (startTime != "" && endTime != "") {
                        adminMemberInfoDataProvider.getTimeMemberInfos(startTime, endTime, pageSize, pageNo, function (data) {
                            objs = $.parseJSON(data);
                        });
                    }
                    else {
                        $.jBox.tip("请选择起止时间", 'error');
                        return;
                    }
                    break;
                case "frommember":
                    var fromMember = $("input#viewFromMemberInfos").val();
                    if (fromMember != "") {
                        if (!isNaN(fromMember)) {
                            adminMemberInfoDataProvider.getFromMemberInfos(fromMember, pageSize, pageNo, function (data) {
                                objs = $.parseJSON(data);
                            });
                        }
                        else {
                            $.jBox.tip("源用户短号必须为数字", 'error');
                            return;
                        }
                    }
                    else {
                        $.jBox.tip("请输入源用户短号", 'error');
                        return;
                    }
                    break;
                default:
                    break;
            }
            $.ajaxSetup({ async: true });

            var str = "";
            var fromMember, toMember, fromMember2, toMember2;
            if (objs != null && objs != "") {
                $.each(objs, function (i) {
                    fromMember = objs[i].FromMember != null && objs[i].FromMember != undefined ? (objs[i].FromMember.UniqueNumber != null ? objs[i].FromMember.UniqueNumber : null) : null;
                    toMember = objs[i].ToMember != null && objs[i].ToMember != undefined ? (objs[i].ToMember.UniqueNumber != null ? objs[i].ToMember.UniqueNumber : null) : null;
                    fromMember = fromMember != null ? objs[i].FromMember.UniqueNumber.ConvertedID : "";
                    toMember = toMember != null ? objs[i].ToMember.UniqueNumber.ConvertedID : "";
                    var operateType = "";
                    fromMember2 = objs[i].FromMember != null ? objs[i].FromMember : null;
                    toMember2 = objs[i].ToMember != null ? objs[i].ToMember : null;
                    switch (objs[i].ActivityType) {
                        case 0: operateType = "上传头像";
                            break;
                        case 1: operateType = "发布图片";
                            break;
                        case 2: operateType = "评论";
                            break;
                        case 3: operateType = "新浪微博登录";
                            break;
                        case 4: operateType = "豆瓣登录";
                            break;
                        case 5: operateType = "视频认证";
                            break;
                        case 6: operateType = "删除内容";
                            break;
                        case 7: operateType = "私信";
                            break;
                        case 8: operateType = "资料修改";
                            break;
                        case 9: operateType = "加入兴趣";
                            break;
                        case 10: operateType = "访谈";
                            break;
                        case 11: operateType = "新说说";
                            break;
                        case 12: operateType = "mo别人";
                            break;
                        case 13: operateType = "关注别人";
                            break;
                        case 14: operateType = "提交建议";
                            break;
                        case 15: operateType = "登出";
                            break;
                        case 16: operateType = "登录";
                            break;
                        case 17: operateType = "创建兴趣";
                            break;
                        case 18: operateType = "腾讯微博登录";
                            break;
                        case 19: operateType = "人人登录";
                            break;
                        case 20: operateType = "未知";
                            break;
                        case 21: operateType = "注册";
                            break;
                        case 22: operateType = "允许登录";
                            break;
                        default:
                            break;
                    }

                    str += "<li class='iwantlistart'><span class='markicon' title=\"目标用户：" + toMember + "\">源用户：" + fromMember + "</span>";
                    str += "<span>操作类别：" + operateType + "</span>";
                    str += "<span title=\"城市：" + (fromMember2 != null ? fromMember2.City : "") + "\">名称：" + (fromMember2 != null ? fromMember2.NickName : "") + "</span>";
                    str += "<span>性别：" + ((fromMember2 != null ? (fromMember2.Sex == 1 ? "男" : "女") : "")) + "</span>";
                    str += "<span title=\"视频认证：" + (fromMember2 != null ? ((fromMember2.MemberPhoto.IsRealPhotoIdentification) ? "是" : "否") : "") + "\">头像：" + (fromMember2 != null ? ((fromMember2.ICONPath) ? "有" : "没有") : "") + "</span>";
                    str += "<span>时间：" + paserJsonDate(objs[i].CreatedTime).format('yyyy-m-d H:M') + "</span>";
                    str += "<span>操作链接：" + objs[i].OperateUrl + "</span>";
                });
            }
            if (str != "")
                $("#listContiner").html(str);
            else
                $("#listContiner").html("没有数据！");
        }

        memberInfoClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) {
                pageNo = mypagecount;
            }
            else {
                pageNo = pageclickednumber;
            }
            $("#pager").pager({ pagenumber: pageNo, pagecount: mypagecount, buttonClickCallback: memberInfoClick });
            viewMemberInfos(showType);
        }

        function setPager(type) {
//            switch (type) {
//                case "memberinfo":
                    $("#pager").pager({ pagenumber: pageNo, pagecount: mypagecount, buttonClickCallback: memberInfoClick });
//                    break;
//                default:
//                    break;
//            }
        }

        function ssclick() {
            if ($("#timechang1").val() != "" || $("#timechang2").val()) {
                window.location = "/Admin/MemberInfo?type=daymemberinfo&time1=" + encodeURI($("#timechang1").val()) + "&time2=" + encodeURI($("#timechang2").val());
            }
            else {
                alert("“确定”前请选择时间！");
            }
        }

    </script>
</asp:Content>
