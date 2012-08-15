<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="rightPart">
<div class="rightBoxTitle">
<div class="fl">
<%if (!(bool)ViewData["IsMe"])
  { %>
与我相距：<%=ViewData["juli"]%>
<% }
  else
  { %>
我的位置
<% } %>
</div>
<div class="rightBoxTitleRight gray fr"><% if ((bool)ViewData["IsMe"])
    {%><a href="/member/SetMyLocation?keepThis=true&TB_iframe=true&height=450&width=600" title="在地图上标注你的位置" class="thickbox">修改我的地点</a><%} %></div>
</div>

<% if ((bool)ViewData["geoisset"])
   {
       if ((bool)ViewData["IAlreadyMarkMyPosition"])
       {%>
       <% if ((bool)ViewData["HiddenMyLoc"] & !(bool)ViewData["IsMe"])
          { %>
          <div class="rightBoxMsg">
           <p>Ta没有公开我的具体方位，想知道，问Ta吧。</p>
           </div>
        <% }
          else
          { %>
          <div><img class="map" src="http://maps.google.com/maps/api/staticmap?center=<%=ViewData["Lat"] %>,<%=ViewData["Lng"] %>&zoom=13&size=300x150&maptype=roadmap&markers=color:red|label:A|<%=ViewData["Lat"] %>,<%=ViewData["Lng"] %>&sensor=false" /></div>
          <% if ((bool)ViewData["HiddenMyLoc"] & (bool)ViewData["IsMe"])
          { %>
          <div class="rightBoxMsg">
           <p>你已经设置为只有自己可以看到具体位置</p>
           </div>
        <% }%>
        <% } %>
<%      }
       else
       {%>
       <div class="rightBoxMsg">
   <p>还没有标记你的地理位置，无法看到你们之间的距离。</p>
   <p><a href="/member/SetMyLocation?keepThis=true&TB_iframe=true&height=450&width=600" title="在地图上标注你的位置" class="thickbox">马上标注吧！</a></p>
</div>
       <%}
    }
   else
   {%>
   <div class="rightBoxMsg">
   <%if (!(bool)ViewData["IsMe"])
  { %>
   <p>Ta还没有标记地理位置，无法看到你们之间的距离。</p>
   <p>马上邀请Ta标注吧！</p>
<% }
  else
  { %>
   <p>你还没有标记地理位置，将无法看到Ta和你的距离。</p>
<% } %>
</div>
<%} 
%>
</div>
