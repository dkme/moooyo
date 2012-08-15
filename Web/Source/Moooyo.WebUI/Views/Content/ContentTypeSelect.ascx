<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style type="text/css">
    .typebackground{z-index:10; background-color:#888; width:135px; height:25px;}
    .left_tools{ position:absolute; left:20px; width:250px; top:10px; color:#FFF; height:35px; line-height:35px; z-index:100; font-family:宋体; font-size:12px;}
    .left_tools a{height:35px; line-height:35px;}
    .typediv{z-index:11; background-color:none; width:135px; height:25px;}
    .typediv a{float:left; margin-right:5px; width:28px; height:25px; line-height:25px; text-align:center; background-image:url('/pics/typedaohan.png'); background-repeat:no-repeat;}
    .typediv .image{background-position:4px 0px;}
    .typediv .images{background-position:4px -25px;}
    .typediv .shuoshuo{background-position:-22px 0px;}
    .typediv .shuoshuos{background-position:-22px -25px;}
    /*.typediv .iwant{background-position:-46px 0px;}
    .typediv .iwants{background-position:-46px -25px;}
    .typediv .mood{background-position:-70px 0px;}
    .typediv .moods{background-position:-70px -25px;}*/
    .typediv .interview{background-position:-100px 0px;}
    .typediv .interviews{background-position:-100px -25px;}
    .typediv .callfor{background-position:-130px 0px;}
    .typediv .callfors{background-position:-130px -25px;}
</style>
<div class="left_tools">
    <%string urlStr = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
      urlStr = urlStr.Substring(0, urlStr.IndexOf('/', 7));%>
    <a href="http://<%=HttpContext.Current.Request.Url.Host.ToString()%>/u/<%=ViewData["memberUrl"].ToString() %>" target="_blank" style="color:#FFF;" title="空间个性地址"><%=urlStr%>/u/<%=ViewData["memberUrl"].ToString()%></a>
</div>
<div class="typebackground" style="position:absolute; right:7px; top:12px;">&nbsp;</div>
<% bool isMe = true;
   isMe = HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf("/Content/IContent") >= 0 ? true : false; %>
<div class="typediv" style="position:absolute; right:7px; top:12px;">
    <a href="/Content/<%=isMe ? "IContent" : "TaContent/" + ViewData["memberId"] %>/<%=Moooyo.BiZ.Content.ContentType.Image.GetHashCode() %>/1" <%=ViewData["contenttype"].ToString() == "0" ? "class=\"images\"" : "class=\"image\""%> title="点击筛选照片内容">&nbsp;&nbsp;&nbsp;&nbsp;</a>
    <a href="/Content/<%=isMe ? "IContent" : "TaContent/" + ViewData["memberId"] %>/<%=Moooyo.BiZ.Content.ContentType.SuiSuiNian.GetHashCode() %>/1" <%=ViewData["contenttype"].ToString() == "1" ? "class=\"shuoshuos\"" : "class=\"shuoshuo\""%> title="点击筛选说说内容">&nbsp;&nbsp;&nbsp;&nbsp;</a>
    <a href="/Content/<%=isMe ? "IContent" : "TaContent/" + ViewData["memberId"] %>/<%=Moooyo.BiZ.Content.ContentType.InterView.GetHashCode() %>/1" <%=ViewData["contenttype"].ToString() == "4" ? "class=\"interviews\"" : "class=\"interview\""%> title="点击筛选访谈内容">&nbsp;&nbsp;&nbsp;&nbsp;</a>
    <a href="/Content/<%=isMe ? "IContent" : "TaContent/" + ViewData["memberId"] %>/<%=Moooyo.BiZ.Content.ContentType.CallFor.GetHashCode() %>/1" <%=ViewData["contenttype"].ToString() == "5" ? "class=\"callfors\"" : "class=\"callfor\""%> title="点击筛选号召内容">&nbsp;&nbsp;&nbsp;&nbsp;</a>
</div>
<script type="text/javascript">
    $().ready(function () {
//        $('.left_tools').css({
//            top: $('.share_bg').offset().top - 65 - $('#top_target').heigth + "px"
//        });
        $("div.typebackground").css({ opacity: .3 });
        $("a.image").hover(function () { $(this).removeClass().addClass("images"); }, function () { $(this).removeClass().addClass("image"); });
        $("a.shuoshuo").hover(function () { $(this).removeClass().addClass("shuoshuos"); }, function () { $(this).removeClass().addClass("shuoshuo"); });
        $("a.interview").hover(function () { $(this).removeClass().addClass("interviews"); }, function () { $(this).removeClass().addClass("interview"); });
        $("a.callfor").hover(function () { $(this).removeClass().addClass("callfors"); }, function () { $(this).removeClass().addClass("callfor"); });
    });
</script>
