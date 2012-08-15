<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div  class="photoAlbumButtons">
<ul>
<li class="button-list first"><a href="/photo/mplist/<%= ViewData["mid"] %>?t=-1"> < 前一张 </a></li>
<li class="button-list "><a href="/photo/mplist/<%= ViewData["mid"] %>?t=1"> + </a></li>
<li class="button-list last"><a href="/photo/mplist/<%= ViewData["mid"] %>?t=2"> 后一张 > </a></li>
</ul>
</div>