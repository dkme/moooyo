<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="selfshare" style="width:182px; background:none">
    <div class="share_t white">分享</div>
    <div class="Letter_demo">
        <%if (Model.UserID != null)
          { %>
        <div class="Letter_interview" onclick="window.location='/Content/AddImageContent'"><a title="发布新照片"><em><img src="/pics/pic_btn01.png"></em></a><s></s></div>
        <div class="Letter_interview" onclick="window.location='/Content/AddSuiSuiNianContent'"><a title="发布新说说"><em><img src="/pics/pic_btn02.png"></em></a><s></s></div>
        <div class="Letter_interview" onclick="window.location='/Content/AddInterViewContent'"><a title="更新访谈"><em><img src="/pics/pic_btn03.png"></em></a><s></s ></div>
        <div class="Letter_interview" onclick="window.location='/Content/AddCallForContent'"><a title="发布新号召"><em><img src="/pics/pic_btn04.png"></em></a><s></s></div>
        <%} %>
        <%else {%>
        <div class="Letter_interview" onclick="window.location='/Account/Login'"><a target="_blank" title="登录米柚网"><em><img src="/pics/pic_btn01.png"></em></a><s class="Trans"></s></div>
        <div class="Letter_interview" onclick="window.location='/Account/Login'"><a target="_blank" title="登录米柚网"><em><img src="/pics/pic_btn02.png"></em></a><s class="Trans"></s></div>
        <div class="Letter_interview" onclick="window.location='/Account/Login'"><a target="_blank" title="登录米柚网"><em><img src="/pics/pic_btn03.png"></em></a><s class="Trans"></s></div>
        <div class="Letter_interview" onclick="window.location='/Account/Login'"><a target="_blank" title="登录米柚网"><em><img src="/pics/pic_btn04.png"></em></a><s class="Trans"></s></div>
        <%} %>
    </div>
</div>
<script type="text/javascript">
    $().ready(function () {

        $('.Letter_demo ').each(function () {
            $(this).find('.Letter_interview').hover(function () {

              //  $(this).find('s').css({ "background": "#777", "opacity": "0.6" });  
                $(this).find('s').css({ "display": "block" });
                $(this).find('s').addClass('Trans');

            }, function () {
                //$(this).find('s').css({"background": "none" });
                $(this).find('s').css({ "display": "none" });
                $(this).find('s').removeClass('Trans');
            });
        });
    });
</script>
