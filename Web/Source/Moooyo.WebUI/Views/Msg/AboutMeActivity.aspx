<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MyActivitysModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    与我相关 米柚网-单身欢乐季
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Letter_intro">
        <!--个人左面板-->
        <% if (Model.IsOwner)
           {%>
        <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%>
        <% }
           else
           { %>
        <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>
        <% } %>
        <!--endof 个人左面板-->
    </div>
    <div class="Fans_content ">
        <%--<div class="fans_title">
            <span class="all_fans fan_tab"><a href="javascript:;"><img src="/pics/Fans_news.gif" title="" /></a><a href="javascript:;"><img src="/pics/Fans_f.gif" title="" /></a></span>
        </div>--%>
        <div class="mt15">
        </div>
        <div class="fans_box">
            <span class="fans_other"></span>
        </div>
        <div id="ActivityList" data-pagecounter="1">
            <% if (Model.activityHolderListObje.Count > 0)
               { %>
            <% Html.RenderAction("AboutMeActivityPanel", "Msg", new { aObjs = Model.activityHolderListObje, rObjs = Model.relationObjs }); %>
        </div>
        <% }
               else
               {%>
        <div class="padding_b50">
        </div>
        <div class="nothing">
            <div class="rel_fans">
                <span>还没有关于你的消息，去<a href="/Content/IndexContent" class="blue02">遇见柚子</a>吧！找找你感兴趣的人！</span>
                <div style="overflow: hidden">
                    <span style="width: 121px; float: left;">或者分享点什么，</span><span class="linkbtn" style="width: 100px;
                        float: left;"><a href="/Content/AddImageContent"><img src="/pics/linkbtn01.gif" width="21" /></a><a
                            href="/Content/AddSuiSuiNianContent"><img src="/pics/linkbtn02.gif" width="21" /></a><a
                                href="/Content/AddCallForContent"><img src="/pics/linkbtn06.gif" width="21" /></a><a
                                    href="/Content/AddInterViewContent"><img src="/pics/linkbtn05.gif" width="21" /></a></span><span
                                        style="float: left;">让大家能看到你</span></div>
            </div>
        </div>
        <% } %>
        <div class="loading" style="cursor: pointer; display: none;" onclick="nextPage()">
            <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4">
            </b></b>
            <div class="loaded">
                <a href="javascript:;">加载更多</a>
            </div>
            <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1">
            </b></b>
        </div>
        <div style="width: 100%; text-align: center; display: none; line-height: 30px;" id="paging">
        </div>
        <div class="padding_b50">
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
    DD_belatedPNG.fix('em');
	 </script>
<![endif]-->
    <script language="javascript" type="text/javascript">
    var uploadpath = '<%=Model.UploadPath %>';
    var pageSize = <%=Model.Pagger.PageSize %>;
    var pageCount = <%=Model.Pagger.PageCount %>;
    var pageNo = <%=Model.Pagger.PageNo %>;
    var pageTotal = <%=Model.pageTotal %>;
    var pageCount2 = Math.ceil(pageTotal / 6);
     var pageCount3 = Math.ceil(pageTotal / 18);
    var pageSize2 = 6;
    var pageUrl = "/Msg/AboutMeActivity/";


   function textareasize(obj) {
             if(obj.scrollHeight > 30) {
               obj.style.height = obj.scrollHeight + 'px';
           }
       }
    $(document).ready(function () {

        if(pageNo >= 2) {
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
            $("div#paging").css("display", "block");
        }

        if(pageCount2 > 1 && pageNo < 2) $("div.loading").css("display", "block");

        $('.Fans_content').find('.fans_box').each(function () {
            var respond = $(this).find('.detail_info').find('.gray01');
            var respondtxt = $(this).find('.fans_detail').find('.detail_ans');

            respond.bind("click", function () {
                $(respondtxt).show(500);
            });
        });

        $(".txtanswer").focus(function () { focusControlCursorLast(); });
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
    });
    function bindActivityList(pageSizeB, pageNoB) {
        var str = "";
        str = getActivityList(pageSizeB, pageNoB);
        $("div#ActivityList").html(str);

        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
    }
    function nextPage() {
        var pageNo2 = $("div#ActivityList").attr("data-pagecounter");
        var str = "";

        if(pageNo2 < pageCount2 && pageNo2 <= 2 && pageNo < 2) {
            if(pageNo2 == 2 || pageNo2 >= (pageCount2 - 1)) { 
                $("div.loading").css("display", "none");
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
                $("div#paging").css("display", "block");
            }

            var newPageNo = Number($("div#ActivityList").attr("data-pagecounter")) + 1;
            $("div#ActivityList").attr("data-pagecounter", newPageNo);
            
            str = getActivityList(pageSize, newPageNo);

            $("div#ActivityList").html($("div#ActivityList").html() + str);

            //绑定会员标签
            MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
        }
        else { 
            $("div.loading").css("display", "none");
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
            $("div#paging").css("display", "block");
        }
    }
    function getActivityList(pageSizeA, pageNoA)
    {
        var str = "";
        $.ajaxSetup({ async: false });
        MsgProvider.getAboutMeActivity(pageSizeA, pageNoA, function (data) {
            var modelObjs = $.parseJSON(data);
            var aObjs = modelObjs.activityHolderList;
            var rObjs = modelObjs.relationObjList;
            
            var aboutMeCount = modelObjs.activityHolderList.length;
            var relatCount = modelObjs.relationObjList.length;
            var flag1 = false;
            var num = 0;
            for(var i = 0; i < aboutMeCount; i++) {
                
                var rObj = rObjs[i];
//                for (var j = 0; j < aboutMeCount; j++) 
//                {
//                    if (rObj.FromMember == aObjs[j].MemberID)
//                    {
//                        flag1 = true;
//                        num = j;
//                    }
//                    else
//                    {
//                        flag1 = false;
//                    }
//                    if (flag1) break;
//                }

                str += "<div class=\"fans_box\">";
                str += "<b class=\"rtop\"><b class=\"r1\"></b><b class=\"r2\"></b><b class=\"r3\"></b><b class=\"r4\"></b></b>";
                str += "<div class=\"with_me\">";
                str += "<div class=\"fans_info\">";
                str += "<dl class=\"clearfix\">";
                str += "<dt><a href=\"/Content/TaContent/" + rObjs[i].ID + "\" target=\"_blank\" id=\"activityMemberInfo1\"><img src=\"" + rObjs[i].MinICON + "\" data_me_id=\"<%=Model.UserID %>\" data_member_id=\"" + rObjs[i].ID + "\" name=\"activityMemberInfoArea\" height=\"30\" width=\"30\" title=\"" + rObjs[i].Name + "\" /></a></dt>";
                str += "<dd><span class=\"blue02\"><a href=\"/Content/TaContent/" + rObjs[i].ID + "\" target=\"_blank\">" + rObjs[i].Name + "</a></span>";
                if (rObjs[i].IsRealPhotoIdentification)
                { 
                    str += "<em><img src=\"/pics/video_pic.png\" height=\"14\" width=\"14\" /></em>";
                } 
                str += "</dd>";
                str += "<dd></dd>";
                str += "</dl>";
                str += "</div>";
                str += "<div class=\"fans_detail\">";

//                if (flag1) {
                    str += modelObjs.aboutMeActivityStrList[i];
//                }

                str += "</div>";
                str += "</div>";
                str += "<b class=\"rbottom\"><b class=\"r4\"></b><b class=\"r3\"></b><b class=\"r2\"></b><b class=\"r1\"></b></b>";
                str += "</div>";

            }
        });
        $.ajaxSetup({ async: true });
        return str;
    }
    function textareasize(obj) {
        if (obj.scrollHeight > 30) {
            obj.style.height = obj.scrollHeight + 'px';
        }
    }
    function showHideReplyArea(id, status) {
        switch(status) {
            case "show":
                $("#reply" + id).show(200);
                break;
            case "hide":
                $("#reply" + id).hide(200);
                break;
        } 
    }
    </script>
</asp:Content>
