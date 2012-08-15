<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	邀请好友
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>邀请好友</span><em>▲</em></div>
			<div class="Set_box">
                <div class="bind_form  tabbox1"> <span class="at_text t_1_d"> | <font color="#333" style=" width:400px; padding-right:220px;">邀请社交平台好友</font>  </span> <span class="at_text p15">
                 
                 <script type="text/javascript" src="/scripts/share.js"></script>
			     </span> </div>
			   <div class="bind_form tabbox2">
				        <span class="at_text t_1_d">  |   <font color="#333" style=" width:400px; padding-right:300px;">发送链接给QQ好友</font>   </span>
			     <div class="bind_com pr65 mtb20">
					   <span class="at_text t_1_d" style="color:#666;" > <input type="text" class="txtput searchbtn" name="pas" value="http://www.moooyo.com" readonly style="width:374px; background:url(/pics/input_long_bg.png) 0 0 no-repeat; float:left;" tabindex="2" id="linkCopyUrl" />  
                       <a href="javascript:;" class="fl btn reg_btn" id="copyToClipboardBtn">&nbsp;&nbsp;复制</a>
                       </span>  
				 </div>	 
               </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em,.txtput');
	 </script>
    <![endif]-->
    <script type="text/javascript">
        $(document).ready(function (){
            if (!$.browser.msie) {
                //复制到剪贴板
                copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").val());
            }
            else {
                $("#copyToClipboardBtn").bind("click", function () {
                    //复制到剪贴板
                    copyToClipboard($("#copyToClipboardBtn"), $("#linkCopyUrl").val());
                });
            }
        });
    </script>
</asp:Content>
