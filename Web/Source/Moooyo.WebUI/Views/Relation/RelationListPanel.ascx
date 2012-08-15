<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.MemberRelationsModel>" %>

<% foreach(Moooyo.WebUI.Models.RelationDisplayObj obj in Model.relationObjs) 
   { 
        String meId = "", taId = "";
       if (obj.FromMember != obj.ToMember)
       {
           if (obj.DisplayFromOrTo == "to") 
           { 
               meId = obj.FromMember; 
               taId = obj.ToMember; 
           }
           else if (obj.DisplayFromOrTo == "from") { 
               meId = obj.ToMember; 
               taId = obj.FromMember; 
           }
       }%>
<dl class="other_fans">
    <dt><a href="/Content/TaContent/<%=obj.ID %>/all/1" target="_blank" id="relationMemberInfo"><img  src="<%=obj.MinICON %>" data_me_id="<%=meId %>" data_member_id="<%=obj.ID %>" name="relationMemberInfoArea" alt="<%=obj.Name%>" title="<%=obj.Name%>" width="49" height="49" /></a>
    <% if (obj.FromMember != obj.ToMember) {
                   if (obj.DisplayFromOrTo == "to") {
                       if (Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(obj.FromMember, obj.ToMember))
                       { %>
                       <em class="delete" onclick="deleteFavoredMember('<%=obj.ToMember%>',$(this))"></em>
                    <% }
                       else { %>
                       <em class="add" onclick="member_i_functions.favormember('<%=obj.ToMember%>',$(this))"></em>
                    <% } %>
                 <% }
                   else if (obj.DisplayFromOrTo == "from") {
                       if (Moooyo.WebUI.Models.DisplayObjProvider.IsInFavor(obj.ToMember, obj.FromMember))
                       { %>
                       <em class="delete" onclick="member_i_functions.deletefavormember('<%=obj.FromMember%>',$(this))"></em>
                    <% }
                       else { %>
                       <em class="add" onclick="member_i_functions.favormember('<%=obj.FromMember%>',$(this))"></em>
                    <% } %>
                <% } %>
             <% } %>
    </dt>
    <dd class="blue02"><%=obj.Name.Length > 4 ? obj.Name.Substring(0, 4) + "<label class=\"ellipsis\">...</label>" : obj.Name%></dd>
</dl>
<% } %>