<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="admintop">
<ul class="admintoplist">
<li><a href="/Admin/Actives?type=activity">活动</a></li>
<li><a href="/Admin/MemberInfo?type=memberinfo">用户状态</a></li>
<%--<li><a href="/admin/newuser">新用户系统评分</a></li>--%>
<li><a href="/admin/photoaudit">图片审核</a></li>
<li><a href="/admin/accountmanager">用户管理</a></li>
<li><a href="/admin/dic/iwants">字典项目</a></li>
<li><a href="/admin/userCall/jb">举报和建议</a></li>
<li><a href="/admin/verify/verifytext">审核管理</a></li>
<li><a href="/Admin/RecommendedData/FeaturedContentTopic">推荐数据</a></li>
<li>| <a href="/account/logout">退出系统</a></li>
</ul></div>