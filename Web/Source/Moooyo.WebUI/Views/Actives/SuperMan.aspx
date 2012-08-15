<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.SuperManModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	变身超人—点亮超人图标，免费赢取电影票
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tcenter">
        <div>
            <div class="sm_diswin_div">
                <div class="sm_content_div">
                    <div class="page1">
                        <span class="title"><a id="index" href="javascript:undefined;">超人在集结</a></span>
                        <span class="link"><a id="nextpage" href="javascript:undefined;"><%=Model.everydaysupercount %>个超人已现身&gt;</a></span>
                        <span class="link"><a id="next2page" href="javascript:undefined;">今日排行榜&gt;</a></span>
                    </div>
                    <div class="page2">
                        <span class="title">Ta们的华丽变身</span>
                        <div class="clickpage" id="everydaypagediv"><img id="Img2" src="/pics/Active_img/up.gif" onclick="everydaysuperpage(<%=Model.everydaysuperpageno-1 %>)" alt="上一页" title="上一页"/>&nbsp;&nbsp;<img id="Img3" src="/pics/Active_img/next.gif" onclick="everydaysuperpage(<%=Model.everydaysuperpageno+1 %>)" alt="下一页" title="下一页" /></div>
                        <div class="head">
                            <ul id="everydaysuperul">
                            <%foreach (var obj in Model.everydaysuperobj)
                              {
                                   %>
                                <li><div><a href="/Content/TaContent/<%=obj.superobj.ToMemberID %>" target="_blank"><img data_me_id="<%=Model.UserID %>" data_member_id="<%=obj.superobj.ToMemberID %>" src="<%=Comm.getImagePath(obj.memberobj.MemberInfomation.IconPath,ImageType.Middle) %>" alt="" /></a><br /><span><%=obj.superobj.CreatedTime.Month+"月" + obj.superobj.CreatedTime.Day+"日"%></span></div></li>
                                <%} %>
                            </ul>    
                        </div>
                        <span class="title">超人权力</span>
                        <div class="have">
                            <span class="span1">点亮超人图标</span>
                            <img src="/pics/Active_img/zs.gif" alt="" />
                            <span class="span2">全免电影票一张</span>
                        </div>
                        <span class="title">超人的条件</span>
                        <div class="condition">
                            <ul>
                                <li>他人第一次对您的兴趣发表话题即视为您当日的有效兴趣粉丝</li>
                                <li>活动期间兴趣粉丝不能累计不能重复</li>
                                <li>当日新增兴趣粉丝数最多的会员即可点亮米柚超人图标</li>
                                <li>评选活动每日重新排名，得奖机率人人平等</li>
                            </ul>
                        </div>
                        <div class="link"><a id="goto3" href="javascript:undefined;">今日战况&gt;&gt;</a></div>
                    </div>
                    <div class="page3">
                        <div class="head">冲刺中的准超人们</div>
                        <div class="list" id="nowsuperul">
                        <%int i = 0;
                          foreach (var nowsuperobj in Model.nowsuperobj)
                          {%>
                            <div class="usershow">
                                <span class="top">top<span><%=(Model.nowsuperpageno-1) * Model.nowsuperpagesize + i+1%></span></span>
                                <span class="headimg"><a href="/Content/TaContent/<%=nowsuperobj.superobj.ToMemberID %>" target="_blank"><img  data_me_id="<%=Model.UserID %>" data_member_id="<%=nowsuperobj.superobj.ToMemberID %>" src="<%=Comm.getImagePath(nowsuperobj.memberobj.MemberInfomation.IconPath,ImageType.Middle) %>" alt="" /></a></span>
                                <div class="interest">
                                    <a href="/Content/TaContent/<%=nowsuperobj.superobj.ToMemberID %>" target="_blank"><span class="nick"><%=nowsuperobj.memberobj.MemberInfomation.NickName %></span></a>&nbsp;创建了<br />
                                    <span>
                                    <%foreach (var interestobj in Model.nowsuperinterest[i]) { %>
                                        <img data-interestid="<%=interestobj.ID %>" src="<%=Comm.getImagePath(interestobj.ICONPath,ImageType.Middle) %>" alt="" />
                                    <%} %>
                                    </span>
                                </div> 
                                <span class="append">个</span><span class="count"><%=nowsuperobj.superobj.FromMemberCount %></span><span class="append">今日新增有效粉丝</span>
                            </div>
                        <%i++;
                          } %>
                        </div>
                        <div class="clickpage" id="nowpagediv"><img id="up_img" src="/pics/Active_img/up.gif" onclick="nowsuperpage(<%=Model.nowsuperpageno-1 %>)" alt="上一页" title="上一页"/>&nbsp;&nbsp;<img id="Img1" src="/pics/Active_img/next.gif" onclick="nowsuperpage(<%=Model.nowsuperpageno+1 %>)" alt="下一页" title="下一页" /></div>
                        <div class="tip"><a id="back2" href="javascript:void(0);">[返回]</a>&nbsp;&nbsp;&nbsp;&nbsp;有效粉丝—创建的兴趣中第一次新增话题或回应话题的粉丝数量。</div>
                        <div class="copyright">米柚网对该活动保留最终解释权</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script type="text/javascript">
    uploadpath = '<%=Model.UploadPath %>';
    $(document).ready(function () {
        MemberInfoCenter.BindDataInfo($("#everydaysuperul img"));
        MemberInfoCenter.BindDataInfo($("#nowsuperul div.usershow span.headimg img"));
        interestCenter.bindinterestLabel($("#nowsuperul div.usershow div.interest img"));
        $("#nextpage").bind("click", function () { $(".sm_content_div").animate({ "left": "-820px" }, "slow", function () { setbak(); }); });
        $("#goto3").bind("click", function () { $(".sm_content_div").animate({ "left": "-1800px" }, "slow"); });
        $("#back2").bind("click", function () { $(".sm_content_div").animate({ "left": "-820px" }, "slow", function () { setbak(); }); });
        $("#index").bind("click", function () { $(".sm_content_div").animate({ "left": "0px" }, "slow", function () { setindex(); }); });
        $("#next2page").bind("click", function () { $(".sm_content_div").animate({ "left": "-1800px" }, "slow"); });
    });

    function setbak() {
        $("#nextpage").html("<a href=\"javascript:undefined;\">&lt;返回</a>").unbind().bind("click", function () { $(".sm_content_div").animate({ "left": "0px" }, "slow", function () { setindex(); }); });
    }
    function setindex() {
        $("#nextpage").html("<a href=\"javascript:undefined;\"><%=Model.everydaysupercount %>个超人已现身&gt;</a>").unbind().bind("click", function () { $(".sm_content_div").animate({ "left": "-820px" }, "slow", function () { setbak(); }); });
    }
    function everydaysuperpage(everydaypageno) {
        var pagecount = "<%=Model.everydaysuperpagecount %>";
        if (everydaypageno > 0 && everydaypageno <= parseInt(pagecount)) {
            ActivesProvider.geteverydaysuper(everydaypageno, function (date) {
                var obj = $.parseJSON(date);
                var everydaysuper = obj.everydaysuperobj;
                var everydaysuperpagecount = obj.everydaysuperpagecount;
                var everydaysuperpageno = obj.everydaysuperpageno;
                var everydaysuperpagesize = obj.everydaysuperpagesize;
                var everydaysupercount = obj.everydaysupercount;
                var str = "";
                var everydaysuperul = $("#everydaysuperul");
                $.each(everydaysuper, function (i) {
                    str += "<li><div><a href=\"/Content/TaContent/" + everydaysuper[i].superobj.ToMemberID + "\" target=\"_blank\"><img data_me_id=\"<%=Model.UserID %>\" data_member_id=\"" + everydaysuper[i].superobj.ToMemberID + "\" src=\"" + photofunctions.geticonphotoname(everydaysuper[i].memberobj.MemberInfomation.IconPath) + "\" alt=\"\" /></a><br /><span>" + (parseInt(paserJsonDate(everydaysuper[i].superobj.CreatedTime).format("mm")) + "月" + parseInt(paserJsonDate(everydaysuper[i].superobj.CreatedTime).format("dd")) + "日") + "</span></div></li>";
                });
                everydaysuperul.html(str);
                MemberInfoCenter.BindDataInfo($("#everydaysuperul img"));
                $("#everydaypagediv").html("<img id=\"Img2\" src=\"/pics/Active_img/up.gif\" onclick=\"everydaysuperpage(" + (everydaysuperpageno - 1) + ")\" alt=\"上一页\" title=\"上一页\"/>&nbsp;&nbsp;<img id=\"Img3\" src=\"/pics/Active_img/next.gif\" onclick=\"everydaysuperpage(" + (everydaysuperpageno + 1) + ")\" alt=\"下一页\" title=\"下一页\" />");
            });
        }
    }
    function nowsuperpage(nowpageno) {
        var pagecount = "<%=Model.nowsuperpagecount %>";
        if (nowpageno > 0 && nowpageno <= parseInt(pagecount)) {
            ActivesProvider.getnowsuper(nowpageno, function (date) {
                var obj = $.parseJSON(date);
                var nowsuper = obj.nowsuperobj;
                var nowintexts = obj.nowsuperinterest;
                var nowsuperpagecount = obj.nowsuperpagecount;
                var nowsuperpageno = obj.nowsuperpageno;
                var nowsuperpagesize = obj.nowsuperpagesize;
                var nowsupercount = obj.nowsupercount;
                var str = "";
                var nowsuperul = $("#nowsuperul");
                $.each(nowsuper, function (i) {
                    str += "<div class=\"usershow\"><span class=\"top\">top<span>" + ((nowsuperpageno - 1) * nowsuperpagesize + i + 1) + "</span></span><span class=\"headimg\"><a href=\"/Content/TaContent/" + nowsuper[i].superobj.ToMemberID + "\" target=\"_blank\"><img  data_me_id=\"<%=Model.UserID %>\" data_member_id=\"" + nowsuper[i].superobj.ToMemberID + "\" src=\"" + photofunctions.geticonphotoname(nowsuper[i].memberobj.MemberInfomation.IconPath) + "\" alt=\"\" /></a></span><div class=\"interest\"><a href=\"/Member/Ta/" + nowsuper[i].superobj.ToMemberID + "\" target=\"_blank\"><span class=\"nick\">" + nowsuper[i].memberobj.MemberInfomation.NickName + "</span></a>&nbsp;创建了<br /><span>";
                    var interests = nowintexts[i];
                    $.each(interests, function (j) {
                        str += "<img data-interestid=\"" + interests[j].ID + "\" src=\"" + photofunctions.geticonphotoname(interests[j].ICONPath) + "\" alt=\"\" />";
                    });
                    str += "</span></div> <span class=\"append\">个</span><span class=\"count\">" + nowsuper[i].superobj.FromMemberCount + "</span><span class=\"append\">今日新增兴趣粉丝</span></div>";
                });
                nowsuperul.html(str);
                MemberInfoCenter.BindDataInfo($("#nowsuperul div.usershow span.headimg img"));
                interestCenter.bindinterestLabel($("#nowsuperul div.usershow div.interest img"));
                $("#nowpagediv").html("<img id=\"up_img\" src=\"/pics/Active_img/up.gif\" onclick=\"nowsuperpage(" + (nowsuperpageno - 1) + ")\" alt=\"上一页\" title=\"上一页\"/>&nbsp;&nbsp;<img id=\"Img1\" src=\"/pics/Active_img/next.gif\" onclick=\"nowsuperpage(" + (nowsuperpageno + 1) + ")\" alt=\"下一页\" title=\"下一页\" />");
            });
        }
    }
</script>
</asp:Content>
