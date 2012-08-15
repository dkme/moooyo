<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	注册 米柚网-单身欢乐季
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("~/Views/MicroConn/ShareBu.ascx"); %>
   <div id="wrap1">
       <div class="contain2">
           <div class="login_left w480" id="change1">
	         <div class="box_com h60 "><h1></h1></div>
		     <div class="box_ppt demo1">
             <%if (ViewData["ifweibo"] != null)
               {
                   %><a href="/Register/Greeting" class="pic01">图片1</a><%
               }
               else
               {
                   %><a href="/Register/RegStep2" class="pic01">图片1</a><%
               }%>
		         		 <%--<a href="/Register/RegStep2" class="pic01">图片1</a>--%>
                         </div>
		     <div class="box_ppt demo1">
		         <a href="javascript:;" class="pic02">图片2</a>
		     </div>
	      </div>
	       <div class="login_left w480" id="change2">
	         <div class="box_com h60 "><h1></h1></div>
		     <div class="box_ppt demo2" >
		          <img src="/pics/login_pic.gif" width="352" height="176" alt="已有伴侣" />
	         </div>
		     <div class="box_ppt login_z demo2" >
		     </div>
	    	     <div class="box_ppt" >
		         <span class="msg_span">oh，非常抱歉，亲爱的朋友，这里是一个单身的人群分享生活，晒快乐的地方。</span>
		     </div>
		      <div class="box_ppt demo2 zgray" >
		         <span class="zspan">我要吐槽</span>
			     <dl id="LinkList">
			        <dt><a href="javascript:;" onclick="shareTX('','有一种非单身，叫不能进米柚。我很悲剧地进不了米柚，能进去的单身盆友们，赶紧去围观一下，也分我点快乐单身的气息吧~请猛戳：','http://www.moooyo.com','')"><img src="/pics/weibo-2.png" height="25" width="25"/></a></dt>
				    <dt><a href="javascript:;" onclick="shareSina('','有一种非单身，叫不能进米柚。我很悲剧地进不了米柚，能进去的单身盆友们，赶紧去围观一下，也分我点快乐单身的气息吧~请猛戳：','http://www.moooyo.com','')"><img src="/pics/demo15.png" height="25" width="25"/></a></dt>
                    <dt><a href="javascript:;" onclick="shareRenRen('','有一种非单身，叫不能进米柚。我很悲剧地进不了米柚，能进去的单身盆友们，赶紧去围观一下，也分我点快乐单身的气息吧~请猛戳：','http://www.moooyo.com','')"><img id="renren" src="/pics/weibo3.png"/></a></dt>
                    <dt><a href="javascript:;" onclick="shareDouBan('','有一种非单身，叫不能进米柚。我很悲剧地进不了米柚，能进去的单身盆友们，赶紧去围观一下，也分我点快乐单身的气息吧~请猛戳：','http://www.moooyo.com','')"><img id="douban" src="/pics/weibo_4.png"/></a></dt>
		        </dl>
		     </div>
		      <div class="box_ppt demo2 h60" >
		         <a href="javascript:void(0)" class="back"><img src="/pics/back.gif" alt="返回" /></a>
		     </div>
	      </div>
	  
	      <div class="login_right h610">
	         <div class="box_com h90 "><img src="/pics/mylogo.gif" alt="米柚网" /></div>
		     <div class="box_com h320"></div>
	     </div>
       </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('.txtput');
	 </script>
    <![endif]-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("body").css({ "background-color": "#dedee0" });
        });
        $(document).ready(function () {
            var change1 = $('#change1');
            var change2 = $('#change2');
            change2.hide();
            $('.pic02').click(function () {
                change1.hide();
                change2.show();
            });
            $('.back').click(function () {
                change1.show();
                change2.hide();
            });
            return false;
        });
    </script>
</asp:Content>
