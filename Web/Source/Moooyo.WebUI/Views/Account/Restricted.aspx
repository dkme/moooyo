<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.SystemMsgsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	米柚网-欢迎您
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="tcenter">
         <div class="center res res_logo"></div>
         <div class="center res res_info">
           <span style="color:#4b4b4b; font-size:25px; padding-left:50px;">·啊哦，</span><span style="color:#adadad; font-size:20px;">您的账号还没有激活！</span><span style="color:#3ac8f7; font-size:20px;">肿么办？</span>
         </div>
         <div class="center res">
            <div class="method1 method1_a">
                <span class="metitle">邀请3位以上好友注册米柚账号或者通过微博账号登陆米柚一次</span>
                <span class="mecontent">·当您邀请的第三位好友成功登陆或注册米柚后您和您邀请的所有账号将被激活<br />
                ·您邀请的好友必须通过您的专用邀请链接登陆或注册米柚网<br />
                ·目前支持新浪微博和腾讯微博登陆，受邀账号和邀请账号登陆IP不能相同。
                </span>
                <span class="melink">您的专用邀请链接:<span class="link_show" id="linkCopyUrl">http://<%= CBB.ConfigurationHelper.AppSettingHelper.GetConfig("Domain")%>/Account/mylogin?No=<%=ViewData["memberid"]%></span><a href="javascript:;" class="acopy" id="copyToClipboardBtn">&nbsp;&nbsp;复制</a></span>
            </div>
         </div>
          <div class="center res">
            <div class="method1 method1_b">
                <span class="metitle">关注米柚官方微博，成功转发头条微博并@5位好友</span>
                <span class="mecontent">·请您先关注米柚官方微博，再转发头条微博并@5位好友<br />
                ·请在转发内容中加入您的"激活编号"，以便工作人员核对<br />
                </span>
                <span class="melink">您的激活编号：<%=ViewData["activNo"] %></span>
                <span class="meweibo">
                    <iframe width="63" height="24" frameborder="0" allowtransparency="true" marginwidth="0" marginheight="0" scrolling="no" border="0" src="http://widget.weibo.com/relationship/followbutton.php?language=zh_cn&width=63&height=24&uid=2618735567&style=1&btn=red&dpc=1"></iframe>
                </span>
            </div>
         </div>
         <div id="tab" class="center res">
            <span id="msgbtn" class="msgbtn">消息</span><span id="linkbtn" class="linkbtna">联系</span>
            <div id="membermsg" class="taba">
                <%--消息内容水电费乐山大佛--%>
                <div class=" mt32  ">
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
    </div>
            </div>
            <div id="linkwe" class="tabb">
                <div class="block_lit">
                    <span>官方QQ群</span>
                    224102208<br /><br />
                </div>
                <div class="block_lit">
                    <span>官方微博</span>
                  t.qq.com/longshu7213 <br />weibo.com/u/2618735567
                </div>
                <div class="block_lit ml60">
                    <span>联系邮箱</span>
                   info@moooyo.com<br /><br />
                </div>
            </div>
         </div>
         

         <br /><br />
          <div class="chblue2 ">©&nbsp;&nbsp;2011-2012 MoooYo.com,All rights reserved <a href="http://www.miibeian.gov.cn/" target="_blank">湘ICP备11011885号-3</a></div>   
    </div>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/Scripts/exts_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#linkwe").hide();
        $("#msgbtn").bind("click", function () { $("#membermsg").show(); $("#linkwe").hide(); $(this).attr("class", "msgbtn"); $("#linkbtn").attr("class", "linkbtna"); });
        $("#linkbtn").bind("click", function () { $("#linkwe").show(); $("#membermsg").hide(); $(this).attr("class", "linkbtn"); $("#msgbtn").attr("class", "msgbtna"); });

        if (!$.browser.msie) {
            //复制到剪贴板
            copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
        }
        else {
            $("#copyToClipboardBtn").bind("click", function () {
                //复制到剪贴板
                copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").html());
            });
        }
    });
</script>
</asp:Content>
