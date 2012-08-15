<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberRelationsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	我的粉丝 米柚网-单身欢乐季
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
             <span class="all_fans red2">所有<% if (Model.Member.FansGroupName != null) { Response.Write(Model.Member.FansGroupName.Name); } else { Response.Write("粉丝"); } %>（<%=Model.Member.MemberFavoredMeCount%>）</span>
          </div>
          <% if(Model.relationObjs.Count > 0) { %>
              <div class="mt15"></div>
              <% if (Model.relationObjs.Count > 0)
                 { %>
         <%--<div class="fans_box"><span class="makename"><a href="javascript:;" class="blue02" onclick="actionprovider.openFansGroupName();">[给Ta们取个更个性的名称]</a></span></div>--%>
         <% } %>
         
             
                     <% Html.RenderAction("FansGlamourToMeRank", "Relation", new { skin = "fans" });%> 
               <% if (Model.relationObjs.Count > 0)
                  { %>
               <div class="fans_box"><span class="fans_other">其他<% if (Model.Member.FansGroupName != null) { Response.Write(Model.Member.FansGroupName.Name); } else { Response.Write("粉丝"); } %>们</span></div>
               <div class="fans_box" id="fansList" data-pagecounter="1">
                   <% Html.RenderAction("RelationListPanel", "Relation", new { objList = Model.relationObjs });%> 
                 
               </div>
               <% } %>
            <div class="padding_b50"></div>

              <%
                  }
                  else if (Model.relationObjs.Count <= 0)
               { %>
       	        <div class="loading">
                  <div class="rel_fans">你还没有粉丝，去<a href="/Content/IndexContent" class="blue02">单身聚居地</a>找找感兴趣的人，或者分享点什么，让大家能看到你。</div>
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
<script type="text/javascript">
    var pageSize = <%=Model.Pagger.PageSize %>;
    var pageCount = <%=Model.Pagger.PageCount %>;
    var pageNo = <%=Model.Pagger.PageNo %>;
    var pageTotal = <%=Model.Member.MemberFavoredMeCount %>;
    var pageCount2 = Math.ceil(pageTotal / 32);
    var pageCount3 = Math.ceil(pageTotal / 96);
    var pageSize2 = 32;
    var pageUrl = "/Relation/Fans/";
    
    $(document).ready(function () {
   
        if(pageNo >= 2) {
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
            $("div#paging").css("display", "block");
        }

        bindAddDelFansBtn();

        if(pageCount2 > 1 && pageNo < 2) $("div.loading").css("display", "block");

        //绑定会员标签
        //bindMemberInfoLabel();
    });
    function bindAddDelFansBtn() {
        $('.fans_list').children('dt').hover(function () {
            $(this).find('em').css({ "display": "block" });

        }, function () {
            $(this).find('em').css({ "display": "none" });
        });

        $('.other_fans').find('dt').hover(function () {
            $(this).find('em').css({ "display": "block" });

        }, function () {
            $(this).find('em').css({ "display": "none" });
        });
    }
    function bindFansList(pageSize, pageNo) {
        var str = getFansList(pageSize, pageNo);
        $("#fansList").html($("#fansList").html() + str);
        //绑定会员标签
        //bindMemberInfoLabel();
    }
    function nextPage() {
        var pageNo2 = $("div#fansList").attr("data-pagecounter");
        var str = "";

        if(pageNo2 < pageCount2 && pageNo2 <= 2 && pageNo < 2) {
            if(pageNo2 == 2 || pageNo2 >= (pageCount2 - 1)) { $("div.loading").css("display", "none");
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
                $("div#paging").css("display", "block");
            }

            var newPageNo = Number($("div#fansList").attr("data-pagecounter")) + 1;
            $("div#fansList").attr("data-pagecounter", newPageNo);
                
            str = getFansList(pageSize, newPageNo);
                
            $("div#fansList").html($("div#fansList").html() + str);
            //绑定会员标签
            //bindMemberInfoLabel();
        }
        else { 
            $("div.loading").css("display", "none");
            $("div#paging").html(profileQueryStrPaging(pageNo, pageCount3, pageUrl).toString());
            $("div#paging").css("display", "block");
        }

        bindAddDelFansBtn();
    }
    function getFansList(pageSize, pageNo) { 
        var str = "";
        $.ajaxSetup({ async: false });
        MemberLinkProvider.getListWhoFavoredMe(pageSize, pageNo, function (data) {
            
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
                str += "<dt><a href=\"/Content/TaContent/" + objs[i].ID + "/all/1\" target=\"_blank\"><img src=\"" + objs[i].MinICON + "\" data_me_id=\"" + meId + "\" data_member_id=\"" + objs[i].ID + "\" name=\"relationMemberInfoArea\" alt=\"" + objs[i].Name + "\" title=\"" + objs[i].Name + "\" width=\"50\" height=\"50\" /></a>";

                if (objs[i].FromMember != objs[i].ToMember) {
                    if (objs[i].DisplayFromOrTo == "to") {
                        var isInFavor = false;
                        memberprovider.getMemberIsInFavor(objs[i].FromMember, objs[i].ToMember, function (data) {
                            isInFavor = data; 
                        });
                       if (isInFavor)
                       { 
                            str += "<em class=\"delete\" onclick=\"member_i_functions.deletefavormember('" + objs[i].ToMember + "', $(this))\"></em>";
                        }
                       else {
                            str += "<em class=\"add\" onclick=\"member_i_functions.favormember('" + objs[i].ToMember + "', $(this))\"></em>";
                        } 
                    }
                 }

                str += "</dt>";
                str += "<dd class=\"blue02\">" + (objs[i].Name.length > 4 ? objs[i].Name.substring(0, 4) + "<label class=\"ellipsis\">...</label>" : objs[i].Name) + "</dd>";
                str += "</dl>";

            });
        });
        $.ajaxSetup({ async: true });

        return str;
    }
//    function bindMemberInfoLabel() {
//        //绑定会员标签
//        MemberInfoCenter.BindDataInfo($("#relationMemberInfo [name='relationMemberInfoArea']"));
//    }
</script>
</asp:Content>
