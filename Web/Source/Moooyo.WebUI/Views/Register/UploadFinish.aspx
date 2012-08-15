<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    注册 米柚网-单身欢乐季
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrap1" style="padding-top:50px;">
        <div class="contain2">
            <div class="login_left w480" id="change1">
                <div class="upload">
                    <span style="padding-left:20px;"><img src="<%=ViewData["imagepath"].ToString() %>" width="362" height="520" /></span>
                </div>
            </div>
            <div class="login_right h610">
                <div class="box_com h90 ">
                    <img src="/pics/mylogo.gif" alt="米柚网" />
                </div>
                <div class="box_com h320">
                </div>
                <div class="box_com h90">
                   <span><a href="#" class="redlink" onclick="shareMessage()">进 入</a></span><br /><br /><br />
                   
        <%if (Model.User.BindedPlatforms != null && Model.User.BindedPlatforms.Trim() != "")
          { 
              %>
                <span>
                    <input type="checkbox" id="remember" class="chekbox" name="remember" tabindex="4" checked="checked" style="float:left; cursor:pointer; line-height:12px; height:12px; margin-top:0px;"/>
                    <span style="color:#aaa; font-size:12px; font-family:宋体; line-height:12px; height:12px;">分享到我的第三方平台</span>
                </span>
                <input type="hidden" id="bindedPlatforms" name="bindedPlatforms" value="<%=Model.User.BindedPlatforms %>"/>
                   <%
          } %>
                </div>
            </div>
        </div>
    </div>
    
<input type="hidden" id="userName" name="userName" value="<%=Model.User.Name %>" />
<input type="hidden" id="userID" name="userID" value="<%=Model.UserID %>" />
<input type="hidden" id="interestTitle" name="interestTitle" value="<%=ViewData["interestTitles"] %>" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" href="/css/jquery.Jcrop.css" type="text/css" media="screen"/>
<style type="text/css">
    .upload { padding:50px 20px 0 45px; font: normal 14px/28px "微软雅黑";}
    .upload span { display:block;}
    .box_com span { display:block; padding-left:20px;}
    .box_com span a.redlink {color:#fff; font-size:16px; text-decoration:none; font-weight:600; float:left; width:100px; background:#b40001; line-height:32px; text-align:center; border-bottom:#9e0203 3px solid; border-right:#9e0203 3px solid;}
    .chekbox { vertical-align:middle;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script type="text/javascript">
        //*** 禁止浏览器后退 *****************
        //禁用后退按钮
        window.history.forward(1);
        //禁用后退键，作用于Firefox、Opera  
        document.onkeypress = banBackSpace;
        //禁用后退键，作用于IE、Chrome  
        document.onkeydown = banBackSpace;
        //************************************ 
        //    var settings;
     
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });
            $("#uploadPhoto").css({opacity:0});
        });

        function shareMessage() {
            var remember = $("#remember").attr("checked");
            var bindedPlatforms = $("#bindedPlatforms").val();
            if (remember != null && bindedPlatforms != null) {
                var content = "我加入了：" + $("#interestTitle").val();
                var url = "http://www.moooyo.com/Content/TaContent/" + $("#userID").val() + "/all/1";
                microConnOperation.shareMessage(bindedPlatforms, content, url, function () {
                    window.location = "/Content/IFavorerContent";
                });
            }
            else {
                window.location = "/Content/IFavorerContent";
            }
        }
       
    </script>
</asp:Content>
