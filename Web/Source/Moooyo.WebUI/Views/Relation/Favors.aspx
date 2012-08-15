<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberRelationsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	我关注的人 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Letter_intro">
    <!--个人左面板-->
    <% if (Model.IsOwner) {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%>  
    <% }
       else { %>
        <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
    </div>
    <div class="Fans_content ">
          <div class="fans_title">
             <span class="all_fans red2">我关注的人（<%=Model.Member.FavorMemberCount %>）</span>
          </div>
              <div class="mt15"></div>
        
               <div class="fans_box"><span class="fans_other"></span></div>
               <div class="fans_box" id="favoredList" data-pagecounter="1">
                   <% Html.RenderAction("RelationListPanel", "Relation", new { objList = Model.relationObjs });%> 
                 
               </div>
            <div class="padding_b50"></div>
            <% if (Model.relationObjs.Count <= 0)
               { %>
       	        <div class="loading">
                    <div class="rel_fans">还没有关注任何柚子，我要<a href="/Content/IndexContent" class="red2">遇见柚子</a></div>
                </div>
            <%} %>
              
             <div class="loading" style="cursor:pointer; display:none;" onclick="nextPage()">
                 <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
                 <div class="loaded"><a href="javascript:;">加载更多</a> </div>
                 <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
              </div>
              <div style="width:100%; text-align:center; display:none; line-height:30px;" id="paging"></div>
          <div class="padding_b50"></div>
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
    var pageSize = <%=Model.Pagger.PageSize %>;
    var pageCount = <%=Model.Pagger.PageCount %>;
    var pageNo = <%=Model.Pagger.PageNo %>;
    var pageTotal = <%=Model.Member.FavorMemberCount %>;
    var pageCount2 = Math.ceil(pageTotal / 48);
    var pageCount3 = Math.ceil(pageTotal / 144);
    var pageSize2 = 48;
    var pageUrl = "/Relation/Favors/";

    $(document).ready(function () {
        if(pageNo >= 2) {
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
            $("div#paging").css("display", "block");
        }

        bindAddDelFavorBtn();

        if(pageCount2 > 1 && pageNo < 2) $("div.loading").css("display", "block");

        //绑定会员标签
        //bindMemberInfoLabel();
    });
    function bindAddDelFavorBtn() {
        $('.other_fans').children('dt').hover(function () {
            $(this).find('em').css({ "display": "block" });

        }, function () {
            $(this).find('em').css({ "display": "none" });
        });
    }
    function bindFavoredList(pageSize, pageNo) {
        var str = getFavoredList(pageSize, pageNo);
        $("#favoredList").html($("#favoredList").html() + str);

        //绑定会员标签
        //bindMemberInfoLabel();
    }
    function nextPage() {
        var pageNo2 = $("div#favoredList").attr("data-pagecounter");
        var str = "";

        if(pageNo2 < pageCount2 && pageNo2 <= 2 && pageNo < 2) {
            if(pageNo2 == 2 || pageNo2 >= (pageCount2 - 1)) { 
                $("div.loading").css("display", "none");
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
                $("div#paging").css("display", "block");
            }

            var newPageNo = Number($("div#favoredList").attr("data-pagecounter")) + 1;
            $("div#favoredList").attr("data-pagecounter", newPageNo);
                
            str = getFavoredList(pageSize, newPageNo);
                
            $("div#favoredList").html($("div#favoredList").html() + str);
            //绑定会员标签
            //bindMemberInfoLabel();
        }
        else { 
            $("div.loading").css("display", "none");
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
            $("div#paging").css("display", "block");
        }

        bindAddDelFavorBtn();
    }
    function getFavoredList(pageSize, pageNo) { 
        var str = "";
        $.ajaxSetup({ async: false });
        MemberLinkProvider.getFavoredList(pageSize, pageNo, function (data) {
            var objs = $.parseJSON(data);
            $.each(objs, function (i) {

                var meId = "", taId = "";
                if (objs[i].FromMember != objs[i].ToMember)
                {
                    if (objs[i].DisplayFromOrTo == "to") 
                    { 
                        meId = objs[i].FromMember; 
                        taId = objs[i].ToMember; 
                    }
                    else if (objs[i].DisplayFromOrTo == "from") { 
                        meId = objs[i].ToMember; 
                        taId = objs[i].FromMember; 
                    }
                }

                str += "<dl class=\"other_fans\">";
                str += "<dt><a href=\"/Content/TaContent/" + objs[i].ID + "/all/1\" id=\"relationMemberInfo\" target=\"_blank\"><img src=\"" + objs[i].MinICON + "\" data_me_id=\"" + meId + "\" data_member_id=\"" + objs[i].ID + "\" name=\"relationMemberInfoArea\" alt=\"" + objs[i].Name + "\" title=\"" + objs[i].Name + "\" width=\"49\" height=\"49\" /></a>";

                if (objs[i].IsFavor)
                { 
                    str += "<em class=\"delete\" onclick=\"deleteFavoredMember('" + objs[i].ToMember + "', $(this))\"></em>";
                }
                else {
                    str += "<em class=\"add\" onclick=\"member_i_functions.favormember('" + objs[i].ToMember + "', $(this))\"></em>";
                } 

                str += "</dt>";
                str += "<dd class=\"blue02\">" + (objs[i].Name.length > 4 ? (objs[i].Name.substring(0, 4) + "<label class=\"ellipsis\">...</label>") : objs[i].Name) + "</dd>";
                str += "</dl>";

            });
        });
        $.ajaxSetup({ async: true });

        return str;
    }
    function deleteFavoredMember(memberId, container) {
        var delResult = member_i_functions.deletefavormember(memberId, container);

        if(delResult) {
            var str = getFavoredList(pageSize, pageNo);
            $("#favoredList").html(str);

            if(pageNo < 2) {
                $("div#favoredList").attr("data-pagecounter", pageNo);
                $("div#paging").css("display", "none");
                $("div.loading").css("display", "block");
            }
            else if(pageNo >= 2) {
                var favoredCount = 0;
                $.ajaxSetup({ async: false });
                MemberLinkProvider.getFavoredCount(null, function (data) {
                    favoredCount = data; 
                });
                $.ajaxSetup({ async: true });
                favoredCount = Math.ceil(favoredCount / 144);
                if(favoredCount > 1)
                    $("div#paging").html(profileQueryStrPaging(pageNo, favoredCount, pageUrl).toString());
                else
                {
                    $("div#paging").css("display", "none");
                    $("div#favoredList").attr("data-pagecounter", 1);
                    $("div.loading").css("display", "block");
                }
            }

            bindAddDelFavorBtn();
        }
        else {
            
        }
    }
//    function bindMemberInfoLabel() {
//        //绑定会员标签
//        MemberInfoCenter.BindDataInfo($("#relationMemberInfo [name='relationMemberInfoArea']"));
//    }
</script>
</asp:Content>
