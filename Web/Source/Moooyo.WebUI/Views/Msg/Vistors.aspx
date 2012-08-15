<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.VistorModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的访客
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
    <% Html.RenderPartial("~/Views/Msg/MsgTopNav.ascx");%>  
    <div class="bor-r3 clearfix">
    <section class="content fl">
     <% if (Model.vistorlist.Count > 0)
       { %>
        <ul class="like-inter-list">
        <% foreach (var obj in Model.vistorlist)
           { %>
           <% Html.RenderAction("RelationListPanel", "Relation", new { obj = obj });%> 
            <%} %>
        </ul>
        <!--分页-->
        <% if (Model.Pagger != null)
               if (Model.Pagger.PageCount > 1)
               {%> 
           <% Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
        <% } %>
        <!--endof 分页-->
        <% }
       else
       { %>
        <div class="h30">系统管理员：好冷清呀，都没有人过来踩踩，赶快去找人互相踩踩吧。</div>
    <% } %>
    </section>
    <aside class="asidebox-r fr" style="margin-top:10px;">
    <% Html.RenderAction("AppPush", "Push");%> 
    <%Html.RenderAction("SameInterestingMemberOverMe", "Push", new { id = Model.IsOwner ? Model.UserID : Model.MemberID }); %>
    <% Html.RenderAction("GuessYourInterest", "Push");%> 
    </aside>
    </div>
</div>
<div class="c976 bottom-bg pr">
    <i class="cir-icon cir-icon3"></i>
    <i class="cir-icon cir-icon4"></i>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        if(<%=Convert.ToString(Model.IsOwner).ToLower() %>) {
            $("[name='fansInfo']").mouseover(function () { 
                $("[name='toFansOperationArea']", this).show();
            });
            $("[name='fansInfo']").mouseout(function () {
                $("[name='toFansOperationArea']", this).hide();
            });
        }
        //绑定兴趣标签
        interestCenter.bindinterestLabel($("#interestRelationContainer li"));
        //绑定会员标签
        MemberInfoCenter.BindDataInfo($("#relationMemberInfo [name='relationMemberInfoArea']"));
    });
</script>
</asp:Content>
