<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.interestObj.Title %>的柚子(<%=Model.interestObj.FansCount%>)
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 内容-->
     <div class="container">
	    <div class="box_demo1 w960" style="background:#fff">
		    <div class="mt10"></div>
            <div style="height:10px;"></div>
		    <div class="Set_title2 admin_b mb20  p40"><img src="/pics/self_bg.png" width="34" height="23" /><span style=" font-family:'\5FAE\8F6F\96C5\9ED1';"><%=Model.interestObj.Title %>的柚子  <font class="cgray" ></font></span> <a class="com_back blue02" onclick="history.back()">返回</a></div>
            <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                <div class="mt10"></div>
			      <div class="admin_box ">
                     <div class="group_admin">
                         <span class="admin_pic"><a href="/InterestCenter/ShowInterest/<%=Model.interestObj.ID %>"><img src="<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle)%>" height="152" width="152"/></a></span>
                         <span class="admin_info" title="<%=Model.interestObj.Content %>"><%=Model.interestObj.Content.Length > 50 ? Model.interestObj.Content.Substring(0,50) + ".." : Model.interestObj.Content%></span>
                     </div>
                     <div class="group_member">
                        <div class="fan_count" style=" overflow:hidden"><div class="count-l"><span id="interestFansCount"><%=Model.interestObj.FansCount%></span> 个柚子<font class="gray01">（列表中只会显示有头像的柚子们）</font></div><div class="count-r"><a href="/InterestCenter/UpdateInterestFansIcon/<%=Model.interestObj.ID %>" class="em item-1"></a><a class="em item-2"></a></div></div>
                        <div class="member_list">
                        <%
                            bool ifmyCreated = Model.interestObj.Creater.MemberID == Model.UserID ? true : false;
                            for (var i = 0; i < Model.memberList.Count; i++)
                            {
                                var obj = Model.memberList[i];
                                String memberid = obj.ID;
                                String iconpath = Comm.getImagePath(obj.MemberInfomation.IconPath, ImageType.Middle);
                                String name = obj.MemberInfomation.NickName;
                                String sex = obj.Sex == 1 ? "男" : "女";
                                String online = obj.OnlineStr;
                                String age = obj.MemberInfomation.Age == "" || obj.MemberInfomation.Age == "?" ? "" : obj.MemberInfomation.Age + "岁";
                                String height = obj.MemberInfomation.Height == "" ? "" : obj.MemberInfomation.Height + "cm";
                                String career = obj.MemberInfomation.Career;
                                String wodistance = Moooyo.WebUI.Models.DisplayObjProvider.GetWeDistance(Model.UserID, obj.ID);
                                if (wodistance == "" || wodistance == "? 米")
                                {
                                    wodistance = "未知";
                                }
                                %>
                            <div class="fans-item" id="<%=i %><%=memberid %>">
                                <div class="mol">
                                    <div class="mol-l">
                                    <%
                                if (ifmyCreated && Model.interestObj.Creater.MemberID != memberid)
                                {
                                        %><em onclick="deletefans('<%=memberid %>','<%=Model.interestObj.ID %>','<%=Model.interestObj.Title %>',$('#<%=i %><%=memberid %>'))"></em><%
                                }%>
                                        <a href="/Content/TaContent/<%=memberid %>/all/1" target="_blank"><img src="<%=iconpath %>" width="49" height="49" title="<%=name %>"/></a>
                                    </div>
                                    <div class="mol-r">
                                        <span><a href="/Content/TaContent/<%=memberid %>/all/1" target="_blank" class="blue02"><%=name%></a><font class="gre"><%=online%></font><%--<font class="gra">10个单身分享</font>--%></span>
                                        <span><s><%=sex%></s><s><%=age%></s><s><%=height%> </s><s><%=career%></s></span>
                                        <span><font class="gra">距离</font><font class="gra"><%=wodistance %></font></span>
                                    </div>
                                </div>
                                <div class="mor">
                                    <a class="on list-1" onclick="window.open('/Msg/Messagedetails/<%=memberid%>')">米邮</a>
                                <%if (!Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(Model.UserID, memberid) && Model.UserID != memberid)
                                  {
                                        %><a class="on list-2" onclick="member_i_functions.favormembertow('<%=memberid%>',$(this),true);">关注</a><%
                                  } %>
                                </div>
                            </div>
                        <%
                            }
                            %>
                        </div>
                     </div>
				  </div>
                  <div class="clear"></div>
                  <div class="padding_b50"></div>
                  <%if (Model.pagecount > Model.pageno)
                    { 
                        %>
                    <div class="loading">
                        <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
                        <div class="loaded"><a onclick="showmore()">点击加载</a></div>
                        <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
                    </div>
                <%
                    }
                  %>
                 <div class="padding_b50"></div>
			</div>
        </div>
	 </div>
     <input type="hidden" id="pagesize" name="pagesize" value="<%=Model.pagesize %>"/>
     <input type="hidden" id="pagecount" name="pagecount" value="<%=Model.pagecount %>"/>
     <input type="hidden" id="pageno" name="pageno" value="<%=Model.pageno %>"/>
     <input type="hidden" id="username" name="username" value="<%=Model.Member.Name %>" />
     <input type="hidden" id="ifmyCreated" name="ifmyCreated" value="<%=ifmyCreated %>" />
     <input type="hidden" id="interestId" name="interestId" value="<%=Model.interestObj.ID %>" />
     <input type="hidden" id="userID" name="userID" value="<%=Model.UserID %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<style type="text/css">
    .admin_box { padding:8px 0;  text-align:left; overflow:hidden;}
    .group_admin { display:block; float:left; width:160px;  margin-top:30px; }
    .group_member { float:right; width:670px;padding-left:50px;  margin-top:30px; overflow:hidden;}
    .group_admin span{ display:block;}
    .admin_pic { width:152px; height:152px; padding-bottom:10px; }
    .group_admin span.admin_info { padding:0 10px; text-align:left; color:#0099cc; font-size:16px; }
    .group_member .fan_count { height:40px; line-height:40px; text-align:left; color:#333;  font-size:16px; }
    .group_member .fan_count span{ height:32px; line-height:32px; padding:0 15px; margin-right:10px; display:block; float:left; font-weight:600;  background:#b40001; text-align:center; color:#fff;}
    .group_member .member_list { padding-top:30px; overflow:hidden;}

    .member_list dl.other_fans { width:68px; height:78px; padding:0 18px 10px 0; display:block; float:left; margin:0;}
    .member_list dl.other_fans dt { width:49px; height:49px; text-align:center; display:block; margin:8px 22px 0 2px; border-left:1px solid #9da0a4; border-top:1px solid #9da0a4; position:relative; }
    .member_list dl.other_fans dt em { width:21px; height:21px; display:none; position:absolute; background:url(/pics/error.png) no-repeat; right:-8px; top:-8px;  cursor:pointer;}
    .member_list dl.other_fans dd { text-align:center; display:block; width:52px; line-height:20px;}
    
    .count-l { width:615px; float:left; overflow:hidden;}
    .count-r { width:50px; float:right; overflow:hidden;}
    .fan_count font.gray01 { font-size:12px; color:#aaa;}
    .fan_count a.em { width:15px; height:14px; display:inline-block; float:right; text-align:right; margin-left:5px;}
    .fan_count a.item-1 { background:url(/pics/Fans-list.png) 0 0 no-repeat;}
    .fan_count a.item-1:hover { background:url(/pics/Fans-listhover.png) 0 0 no-repeat;}
    .fan_count a.item-2 { background:url(/pics/Fans-btn.png) 0 0 no-repeat;}
    /*.fan_count a.item-2:hover { background:url(/pics/Fans-btnhover.png) 0 0 no-repeat;}*/

    .fans-item { width:100%; height:111px; overflow:hidden; padding-bottom:1px; border-bottom:1px dashed #dbdbdb;}
    .fans-item .mol { width:75%; display:block; float:left; padding-top:8px; overflow:hidden}
    .fans-item .mol .mol-l{ width:50px; padding-right:20px; padding-top:12px; padding-left:5px; float:left; position:relative;}
    .fans-item .mol .mol-l em { width:22px; height:22px; display:none; position:absolute; z-index:10; right:10px; top:2px; background:url(/pics/error.png) no-repeat;}
    .fans-item .mol .mol-l a { display:block; width:49px; height:49px; border-top:1px solid #9DA0A4; border-left:1px solid #9DA0A4;}
    .fans-item .mol .mol-r{ width:420px;  float:left; padding-top:2px; overflow:hidden; text-align:left; font: normal 12px/28px "宋体"; color:#666;}
    .fans-item .mol .mol-r span { display:block;}
    .fans-item .mol .mol-r a ,.fans-item .mol .mol-r font { padding-right:10px; font-size:12px;}
    .fans-item .mol .mol-r s { padding-right:5px; font-weight:normal; font-size:12px;}
    .fans-item .mol .mol-r a.blue02 { color:#0099cc; text-decoration:none; font-family:"微软雅黑";} 
    .fans-item .mol .mol-r a.blue02:hover { color:#b40001} 
    .fans-item .mol .mol-r font.gre { color:#65d681;}
    .fans-item .mol .mol-r font.gra { color:#aaa;}
    .fans-item .mor { width:25%; display:block; float:right; padding-top:20px; overflow:hidden}
    .fans-item .mor a {  float:right; display:block;  width:35px; padding-left:25px; height:30px; margin-right:3px; text-align:left; font: normal 14px/30px "微软雅黑"; color:#fff; text-decoration:none;}
    .fans-item .mor a.list-1 { background:#78E3E7 url(/pics/letter_msg.png) 5px 10px no-repeat;}
    .fans-item .mor a.list-1:hover { background:#7FE0E3 url(/pics/letter_msg.png) 5px 10px no-repeat;}
    .fans-item .mor a.list-2 { background:#78E48E url(/pics/self_add.png) 5px 9px no-repeat; }
    .fans-item .mor a.list-2:hover { background:#8AE99D url(/pics/self_add.png) 5px 9px no-repeat;}
    
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>

<script type="text/javascript">
    $().ready(function () {
        $("#wrap").css("background", "#DEDEE0");
        uploadpath = '<%=Model.UploadPath %>';
        mainload();
    });
    function mainload() {
        $('.other_fans').children('dt').hover(function () {
            $(this).find('em').css({ "display": "block" });

        }, function () {
            $(this).find('em').css({ "display": "none" });
        });
        $('.fans-item:last').css({ "border": "0" });
        $('.fans-item').each(function () {
            $(this).hover(function () {
                $(this).css({ "background": "#fafafa" });
             //   $(this).children('.mor').find('a.list-2').css({ "display": "block" });
            }, function () {
                $(this).css({ "background": "none" });
               // $(this).children('.mor').find('a.list-2').css({ "display": "none" });
            });
            $(this).find('.mol-l').hover(function () {
                $(this).find('em').css({ "display": "block" });
            }, function () {
                $(this).find('em').css({ "display": "none" });
            });

        });
    }
    $(window).scroll(function () {
        if ($("div.loading").html() != null && $("div.loading").html() != "") {
            if ($("div.loaded a").html() != null && $("div.loaded a").html() != "") {
                $("div.loaded a").html("努力加载中…");
                var clientheight = document.documentElement.clientHeight;
                var scrolltop = document.body.scrollTop + document.documentElement.scrollTop;
                var offsetheight = document.body.offsetHeight;
                if (clientheight + scrolltop > $("div.loading").offset().top - (clientheight / 2)) {
//                    $("div.loaded a").html("<img src=\"/pics/Ajax_loading.gif\"/>");
                    $("div.loaded a").click();
                    $("div.loaded a").parent().html("<a>努力加载中…</a>");
                }
            }
        }
    });
    function showmore() {
        var id = $("#interestId").val();
        var userid = $("#userID").val();
        var pagesize = $("#pagesize").val();
        var pageno = parseInt($("#pageno").val()) + 1;
        var ifmyCreated = $("#ifmyCreated").val();
        interestCenterProvider.UpdateInterestFansAjax(id, pagesize, pageno, function (data) {
            var data = $.parseJSON(data);
            var str = "";
            for (var i = 0; i < data.memberList.length; i++) {
                var obj = data.memberList[i];
                //                if (obj.ID == data.interestObj.Creater.MemberID) { continue; }
                var memberid = obj.ID;
                var iconpath = photofunctions.getprofileiconphotoname(obj.MemberInfomation.IconPath);
                var name = obj.MemberInfomation.NickName;
                var sex = obj.Sex == 1 ? "男" : "女";
                var online = obj.OnlineStr;
                var age = obj.MemberInfomation.Age == "" || obj.MemberInfomation.Age == "?" ? "" : obj.MemberInfomation.Age + "岁";
                var height = obj.MemberInfomation.Height == "" ? "" : obj.MemberInfomation.Height + "cm";
                var career = obj.MemberInfomation.Career;
                $.ajaxSetup({ async: false });
                var wodistance = "";
                memberprovider.getMemberDistance(userid, memberid, function (data) {
                    wodistance = $.parseJSON(data);
                });
                if (wodistance == "" || wodistance == "? 米") {
                    wodistance = "未知";
                }
                var ifmyfans = "";
                memberprovider.getMemberIsInFavor(userid, memberid, function (data) {
                    ifmyfans = $.parseJSON(data);
                });
                $.ajaxSetup({ async: true });
                if (i <= 0) {
                    str += "<div class=\"fans-item\" style=\"border-top:1px dashed #dbdbdb;\" id=\"" + i + "" + memberid + "\">";
                }
                else {
                    str += "<div class=\"fans-item\" id=\"" + i + "" + memberid + "\">";
                }
                str += "<div class=\"mol\">";
                str += "<div class=\"mol-l\">";
                if ((ifmyCreated == "true" || ifmyCreated == "True") && data.interestObj.Creater.MemberID != memberid) {
                    str += "<em onclick=\"deletefans('" + memberid + "','" + data.interestObj.ID + "','" + data.interestObj.Title + "',$('#" + i + "" + memberid + "'))\"></em>";
                }
                str += "<a href=\"/Content/TaContent/" + memberid + "/all/1\" target=\"_blank\"><img src=\"" + iconpath + "\" width=\"49\" height=\"49\" title=\"\" /></a>";
                str += "</div>";
                str += "<div class=\"mol-r\">";
                str += "<span><a href=\"/Content/TaContent/" + memberid + "/all/1\" target=\"_blank\" class=\"blue02\">" + name + "</a><font class=\"gre\">" + online + "</font></span>";
                str += "<span><s>" + sex + "</s><s>" + age + "</s><s>" + height + " </s><s>" + career + "</s></span>";
                str += "<span><font class=\"gra\">距离</font><font class=\"gra\">" + wodistance + "</font></span>";
                str += "</div>";
                str += "</div>";
                str += "<div class=\"mor\">";
                str += "<a class=\"on list-1\" onclick=\"window.open('/Msg/Messagedetails/" + memberid + "')\">米邮</a>";
                if (ifmyfans.toString() == "False" || ifmyfans.toString() == "false" && userid != memberid) {
                    str += "<a class=\"on list-2\" onclick=\"member_i_functions.favormembertow('" + memberid + "',$(this),true);\">关注</a>";
                }
                str += "</div>";
                str += "</div>";
            }
            $("div.member_list").html($("div.member_list").html() + str);
            $("#pageno").val(data.pageno);
            if (data.pageno >= data.pagecount) {
                $("div.loading").remove();
            }
            else {
                $("div.loaded").html("<a onclick=\"showmore()\">点击加载</a>");
            }
            mainload();
        });
    }
    function deletefans(userid, interestid, interestname, obj) {
        var username = $("#username").val();
        interestCenterProvider.delMemberInterestFans(userid, interestid, function (data) {
            //发送私信
            interestCenterProvider.delMemberInterestFansToMsg(username, interestname, userid, obj);
            $("#interestFansCount").html(parseInt($("#interestFansCount").html()) - 1);
        });
    }
</script>
</asp:Content>
