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
		    <div class="Set_title2 admin_b mb20 p40"><img src="/pics/self_bg.png" width="34" height="23" /><span style=" font-family:'\5FAE\8F6F\96C5\9ED1';"><%=Model.interestObj.Title %>的柚子  <font class="cgray" ></font></span> <a class="com_back blue02" onclick="history.back()">返回</a></div>
            <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                <div class="mt10"></div>
			      <div class="admin_box ">
                     <div class="group_admin">
                         <span class="admin_pic"><a href="/InterestCenter/ShowInterest/<%=Model.interestObj.ID %>"><img src="<%=Comm.getImagePath(Model.interestObj.ICONPath,ImageType.Middle)%>" height="152" width="152"/></a></span>
                         <span class="admin_info" title="<%=Model.interestObj.Content %>"><%=Model.interestObj.Content.Length > 50 ? Model.interestObj.Content.Substring(0,50) + ".." : Model.interestObj.Content%></span>
                     </div>
                     <div class="group_member">
                         <div class="fan_count" style=" overflow:hidden"><div class="count-l"><span id="interestFansCount"><%=Model.interestObj.FansCount%></span> 个柚子<font class="gray01">（列表中只会显示有头像的柚子们）</font></div><div class="count-r"><a class="em item-1"></a><a href="/InterestCenter/UpdateInterestFansList/<%=Model.interestObj.ID %>" class="em item-2"></a></div></div>
                        <div class="member_list">
                        <%
                            bool ifmyCreated = Model.interestObj.Creater.MemberID == Model.UserID ? true : false;
                            for (int i = 0; i < Model.memberList.Count; i++)
                            {
                                var obj = Model.memberList[i];
                                //if (obj.ID == Model.interestObj.Creater.MemberID) { continue; }
                                %>
                            <dl class="other_fans" id="<%=i %><%=obj.ID %>">
                                <dt>
                                    <a href="/Content/TaContent/<%=obj.ID %>/all/1" target="_blank"><img src="<%=Comm.getImagePath(obj.MemberInfomation.IconPath,ImageType.Middle) %>" width="49" height="49" alt="" title="<%=obj.MemberInfomation.NickName %>" /></a>
                                    <%
                                if (ifmyCreated && Model.interestObj.Creater.MemberID != obj.ID)
                                {
                                    %>
                                    <em onclick="deletefans('<%=obj.ID %>','<%=Model.interestObj.ID %>','<%=Model.interestObj.Title %>',$('#<%=i %><%=obj.ID %>'))"></em>
                                    <%
                                }%>
                                </dt>
                                <dd class="blue02" title="<%=obj.MemberInfomation.NickName %>"><%=obj.MemberInfomation.NickName.Length > 3 ? obj.MemberInfomation.NickName.Substring(0, 3) + ".." : obj.MemberInfomation.NickName%></dd>
                            </dl>
                        <%
                            }%>
                        </div>
                     </div>
				  </div>
                  <div class="clear"></div>
                  <div class="padding_b50"></div>
                  <%if (Model.pagecount > Model.pageno)
                    { %>
                <div class="loading">
                 <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
                 <div class="loaded"><a onclick="showmore('<%=Model.interestObj.ID %>')">点击加载</a> </div>
                 <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
                </div>
                <%} %>
                 <div class="padding_b50"></div>
			</div>
        </div>
	 </div>
     <input type="hidden" id="pagesize" name="pagesize" value="<%=Model.pagesize %>"/>
     <input type="hidden" id="pagecount" name="pagecount" value="<%=Model.pagecount %>"/>
     <input type="hidden" id="pageno" name="pageno" value="<%=Model.pageno %>"/>
     <input type="hidden" id="username" name="username" value="<%=Model.Member.Name %>" />
     <input type="hidden" id="ifmyCreated" name="ifmyCreated" value="<%=ifmyCreated %>" />
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
    /*.fan_count a.item-1:hover { background:url(/pics/Fans-list.png) 0 0 no-repeat;}*/
    .fan_count a.item-1 { background:url(/pics/Fans-listhover.png) 0 0 no-repeat;}
    .fan_count a.item-2:hover { background:url(/pics/Fans-btn.png) 0 0 no-repeat;}
    .fan_count a.item-2 { background:url(/pics/Fans-btnhover.png) 0 0 no-repeat;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    $().ready(function () {
        $("#wrap").css("background", "#DEDEE0");
        uploadpath = '<%=Model.UploadPath %>';
        $('.other_fans').children('dt').hover(function () {
            $(this).find('em').css({ "display": "block" });

        }, function () {
            $(this).find('em').css({ "display": "none" });
        });
    });
    function showmore(id) {
        var pagesize = $("#pagesize").val();
        var pageno = parseInt($("#pageno").val()) + 1;
        var ifmyCreated = $("#ifmyCreated").val();
        interestCenterProvider.UpdateInterestFansAjax(id, pagesize, pageno, function (data) {
            var data = $.parseJSON(data);
            var str = "";
            for (var i = 0; i < data.memberList.length; i++) {
                var obj = data.memberList[i];
//                if (obj.ID == data.interestObj.Creater.MemberID) { continue; }
                str += "<dl class=\"other_fans\" id=\"" + i + obj.ID + "\">";
                str += "<dt>";
                str += "<a href=\"/Content/TaContent/" + obj.ID + "/all/1\" target=\"_blank\"><img src=\"" + photofunctions.getprofileiconphotoname(obj.MemberInfomation.IconPath) + "\" width=\"49\" height=\"49\" alt=\"\" /></a>";
                if ((ifmyCreated.toString() == "True" || ifmyCreated.toString() == "true") && data.interestObj.Creater.MemberID != obj.ID) {
                    str += "<em onclick=\"deletefans('" + obj.ID + "','" + data.interestObj.ID + "','" + data.interestObj.Title + "',$('#" + i + obj.ID + "'))\"></em>";
                }
                str += "</dt>";
                str += "<dd class=\"blue02\" title=\"" + obj.MemberInfomation.NickName + "\">" + (obj.MemberInfomation.NickName.length > 3 ? obj.MemberInfomation.NickName.substr(0, 3) + ".." : obj.MemberInfomation.NickName) + "</dd>";
                str += "</dl>";
            }
            $("div.member_list").html($("div.member_list").html() + str);
            $("#pageno").val(data.pageno);
            if (data.pageno >= data.pagecount) {
                $("div.loading").remove();
            }
            $('.other_fans').children('dt').hover(function () {
                $(this).find('em').css({ "display": "block" });

            }, function () {
                $(this).find('em').css({ "display": "none" });
            });
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
