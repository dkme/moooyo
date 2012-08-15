<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.SystemMsgsModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%=Model.Member.Name%>的系统消息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="c976 clearfix">
    <% Html.RenderPartial("~/Views/Msg/MsgTopNav.ascx");%>  
  <div class="clearfix bgf7">
    <section class="w700 mt32 ml65 fl">
          <% if (Model.systemMsglist.Count == 0)
       { %>
       	<div class="tcenter f18 cgreen">还没有任何系统消息。</div>
    <%} %>
        <ul class="like-inter-list">
        <% foreach (var obj in Model.systemMsglist)
           { %>
           <div class="head50"><img src="/pics/defultpic.png"/></div>
      <div class="ml65">
        <h3 class="cblue">系统管理员</h3>
        <p>
            <%=obj.Comment %>
        </p>
        <p class="cgray txtfr"><%= Moooyo.WebUI.Common.Comm.getTimeSpan(obj.CreatedTime)%></p>
      </div>
        <%} %>
        </ul>
        <!--分页-->
        <% if (Model.Pagger!=null)
               if (Model.Pagger.PageCount>1)
           {%> 
           <% Html.RenderAction("pagger", "Shared", new { nowpage = Model.Pagger.PageNo, pagecount = Model.Pagger.PageCount, additionID = Model.Pagger.AdditionParams, url = Model.Pagger.PageUrl });%> 
        <%} %>
        <!--endof 分页-->
    </section>
    
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
<script src="/js/a_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
</asp:Content>
