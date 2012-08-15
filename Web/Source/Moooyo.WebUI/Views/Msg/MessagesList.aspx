<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MsgsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.Member.Name %>的米邮
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
    <div class="Letter_box ">
        <div class="Letter_title p30">
            <h2>
                <%=Model.Member.Name %>
                的米邮</h2>
        </div>
        <div id="lastMsgers" data-pagecounter="1">
        </div>
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
    <script language="javascript" type="text/javascript">
        var you = '<%=Model.You == null ? "" : Model.You.ID %>';
        var me = '<%=Model.UserID %>';
        var yourname = '<%=Model.You==null?"":Model.You.Name %>';
        var pageSize = <%=Model.Pagger.PageSize %>;
        var pageCount = <%=Model.Pagger.PageCount %>;
        var pageNo = <%=Model.Pagger.PageNo %>;
        var pageTotal = <%=Model.pageTotal %>;
        var pageCount2 = Math.ceil(pageTotal / 9);
        var pageSize2 = 9;
        var pageUrl = "/Msg/MessagesList/" + you + "/";

        $(document).ready(function () {
            uploadpath = '<%=Model.UploadPath %>';

            if(pageNo < 2)
                openmsg(you, pageSize2, 1);
            else {
                openmsg(you, pageSize, pageNo);
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                $("div#paging").css("display", "block");
            }

            $('.Letter_box #lastMsgers .Letter_list').each(function () {
                $(this).hover(function () {
                    $(this).css({ "background": "#fafafa" });
                    $(this).find('.let_close').css({ "visibility": "visible" });
                    // $(this).find('#answer').css({"background":"#4B8B25"});

                }, function () {
                    $(this).css({ "background": "#fff" });
                    $(this).find('.let_close').css({ "visibility": "hidden" });
                    //  $(this).find('#answer').css({"background":"#5dab2d"});
                });
                $(this).find('.let_close').click(function () {
                    $(this).parent().css({ "display": "none" });

                }, function () {

                });
            });

            var pageNo2 = $("div#lastMsgers").attr("data-pagecounter");
            if(pageCount2 > 1 && pageNo < 2) $(".loading").css("display", "block");
        });

        function openmsg(you, pageSize, pageNo) {
            var str = "";
            str = getLastMsgers(me, you, $("#lastMsgers"), pageSize, pageNo);
            $("div#lastMsgers").html(str);

            //绑定会员标签
            //MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
        }
        function nextPage() {
            var pageNo2 = $("div#lastMsgers").attr("data-pagecounter");
            var str = "";
            if(pageNo2 < pageCount2 && pageNo2 <= 2 && pageNo < 2) {
                var newPageNo = Number($("div#lastMsgers").attr("data-pagecounter")) + 1;
                if(newPageNo >= pageCount2) { 
                    $(".loading").css("display", "none");
                    $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                    $("div#paging").css("display", "block");
                }
                $("div#lastMsgers").attr("data-pagecounter", newPageNo);
                var refreshYou = "";
                $.ajaxSetup({ async: false });
                MsgProvider.getPrivateAndSysMesges(pageSize2, newPageNo, function (data) {
                    var objs = $.parseJSON(data);
                    $.each(objs, function (i) {
                        refreshYou = objs[0].ToMember == me ? objs[0].FromMember : objs[0].ToMember;
                    });
                });
                $.ajaxSetup({ async: true });
                str = getLastMsgers(me, refreshYou, $("div#lastMsgers"), pageSize2, newPageNo);
                
                $("div#lastMsgers").html($("div#lastMsgers").html() + str);

                //绑定会员标签
                MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
            }
            else { 
                $(".loading").css("display", "none");
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                $("div#paging").css("display", "block");
            }
        }
        function getLastMsgers(me, you, container, pageSize, pageNo) {
            var str = "";
            $.ajaxSetup({ async: false });
            MsgProvider.getPrivateAndSysMesges(pageSize, pageNo, function (result) {
                var objs = $.parseJSON(result);
                str = msg_i_functions.bindlastmsginmsger(me, you, objs, container);
                //                getyounewmsgcount(objs);

                //                //隐藏删除按钮
                //                $(".lastmsger").each(function () {
                //                    $(this).find(".del").hide();
                //                    $(this).hover(function () { $(this).find(".del").show(); }, function () { $(this).find(".del").hide(); });
                //                });
                //绑定用户头像弹窗
                //MemberInfoCenter.BindDataInfo($("#lastMsgers div.infos"));

            });
            $.ajaxSetup({ async: true });
            return str;
        }
    </script>
</asp:Content>
