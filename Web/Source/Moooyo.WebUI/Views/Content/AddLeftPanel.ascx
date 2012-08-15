<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="left_menu" style="display:none;">
	<ul>
	    <li ><a href="/Content/AddImageContent" class="on1" title="发布新照片">照片</a></li>
	    <li ><a href="/Content/AddSuiSuiNianContent" class="on2" title="发布新说说">说说</a></li>
	    <%--<li ><a href="/Content/AddIWantContent" class="on3">计划</a></li>--%>
	    <%--<li ><a href="/Content/AddMoodContent" class="on4">心情</a></li>--%>
	    <li ><a href="/Content/AddInterViewContent" class="on5" title="更新访谈">访谈</a></li>
	    <li ><a href="/Content/AddCallForContent" class="on6" title="发布新号召">号召</a></li>
    </ul>
</div>
<script type="text/javascript">
    $().ready(function () {
        var nowurl = window.location.toString();
        if (nowurl.toLowerCase().indexOf("addimagecontent") >= 0) {
            $("div.left_menu ul li a.on1").attr("class", "dmo1");
        }
        if (nowurl.toLowerCase().indexOf("addsuisuiniancontent") >= 0) {
            $("div.left_menu ul li a.on2").attr("class", "dmo2");
        }
        //if (nowurl.toLowerCase().indexOf("addiwantcontent") >= 0) {
        //    $("div.left_menu ul li a.on3").attr("class", "dmo3");
        //}
        //if (nowurl.toLowerCase().indexOf("addmoodcontent") >= 0) {
        //    $("div.left_menu ul li a.on4").attr("class", "dmo4");
        //}
        if (nowurl.toLowerCase().indexOf("addinterviewcontent") >= 0) {
            $("div.left_menu ul li a.on5").attr("class", "dmo5");
        }
        if (nowurl.toLowerCase().indexOf("addcallforcontent") >= 0) {
            $("div.left_menu ul li a.on6").attr("class", "dmo6");
        }
    });
</script>