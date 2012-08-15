<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.InterestForRegModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	注册,了解你的喜好 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="wrap1" style="padding-top:50px;">
   <div id="Div1">
   <div class="contain1">
      <div class="login_left1 h650" >
        <div class="box_com h90 p70">
          <h1>你是哪种柚子?</h1>
          <div class="small_pic">
          </div>
        </div>
        <div class="box_ppt demo3" style="height:430px;">
		     <ul id="interestlist" data-pagenonow="2" data-classidnow="">
            <%foreach (var obj in Model.interests)
            {
               %>
                <li data-ifopen="false" onclick="interestCenterFunctions.addInterestFans('<%=obj.ID %>');shareInterest('<%=obj.ID %>');"><s ></s><a href="javascript:;" id="<%=obj.ID %>"><img src="<%=Comm.getImagePath(obj.ICONPath, ImageType.Middle) %>" alt="<%=obj.Title %>" width="120" height="120" /></a><em><img src="/pics/hobby_check.png" title="<%=obj.Title %>" alt="<%=obj.Title %>" /></em><font class="alpha50 Trans"><%=obj.Title %></font></li>
            <%} %>

			 </ul> 
   	    </div> 
        <%if (Model.User.BindedPlatforms != null && Model.User.BindedPlatforms.Trim() != "")
          { 
              %>
        <div style="height:30px;">
            <input type="checkbox" id="ifshare" name="ifshare" checked="checked" style="float:left; margin-left:100px; cursor:pointer;"/>
            <span style="color:#aaa; float:left; font-family:宋体; font-size:12px; margin-top:1px;">将我的选择同步到第三方平台</span>
        </div>
        <input type="hidden" id="bindedPlatforms" name="bindedPlatforms" value="<%=Model.User.BindedPlatforms %>"/>
        <%
            } %>
     </div>
	
	  <div class="login_right h610">
	     <div class="box_com h90 "><img src="/pics/mylogo.gif" alt="米柚网" /></div>
		 <div class="box_com h320"></div>
	 </div>
   </div>
</div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <style type="text/css">
        .box_ppt ul li span { display:block; width:100px; line-height:20px; font-family:"\5FAE\8F6F\96C5\9ED1"; height:80px; text-align:center; padding:20px 10px; background-color:#b40001; color:#fff; font-size:14px;}
        .box_ppt ul li span label {  text-align:center; height:20px; }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/main_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
        DD_belatedPNG.fix('em img,em,.Trans');
	 </script>
    <![endif]-->
    <script type="text/javascript">
        uploadpath = '<%=Model.UploadPath %>';

        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });

            registerBusinessProvider.regAddInterestClickEffect();

            $("div.box_ppt ul#interestlist li:eq(3)").after("<li id=\"currentSelect\"></li>");
            $("div.box_ppt ul#interestlist li#currentSelect").html("<div style=\"display:none;\"><span><label id=\"regAddInterestTitle\">选择一个最贴切你的生活习惯的图标</label><br/><b><label id=\"currentSelectCount\">0</label>/3</b></span></div>");
            $("div.box_ppt ul#interestlist li#currentSelect div").show();

        });
        function getinterest() {
            registerBusinessProvider.getRegRecomInterest(
                $("div.box_ppt ul#interestlist").attr("data-pagenonow")
            );
        }
        function shareInterest(id) {
            var ifshare = $("#ifshare").attr("checked");
            var bindedPlatforms = $("#bindedPlatforms").val();
            if (ifshare != null && bindedPlatforms != null) {
                interestCenterProvider.getInterest(id, function (data) {
                    var data = $.parseJSON(data);
                    var content = data.Title + "：" + data.Content;
                    for (var i = 0; i < content.length; i++) {
                        if (content.indexOf("\n") >= 0) { content = content.replace("\n", ""); }
                        else { break; }
                    }
                    content = content.length > 130 ? content.substr(0, 130) + "..." : content;
                    var url = "http://www.moooyo.com/InterestCenter/ShowInterest/" + data.ID;
                    microConnOperation.shareMessage(bindedPlatforms, content, url, function () {
                        if ($("#currentSelectCount").html() == "3") {
                            $("div.box_ppt ul#interestlist").html("正在跳转中...");
                            setTimeout(function () {
                                window.location.href = "/Register/UploadAvatar?pt=1";
                            }, 500);
                        }
                        else if ($("#currentSelectCount").html() != "0") {
                            $("div.box_ppt ul#interestlist").html("努力加载中...");
                            setTimeout(function () {
                                getinterest();
                            }, 500);
                        }
                    });
                });
            }
            else {
                setTimeout(function () {
                    if ($("#currentSelectCount").html() == "3") {
                        $("div.box_ppt ul#interestlist").html("正在跳转中...");
                        window.location.href = "/Register/UploadAvatar?pt=1";
                    }
                    else if ($("#currentSelectCount").html() != "0") {
                        $("div.box_ppt ul#interestlist").html("努力加载中...");
                        getinterest();
                    }
                }, 20);
            }
        }
    </script>
</asp:Content>
