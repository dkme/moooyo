<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	个性设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>个性设置</span><em>▲</em></div>
			<div class="Set_box">
                <div class="bind_form border_b">
				   <span class="at_text t_1_d">  |   <font color="#333">个性图片</font>  </span>
				    <span class="at_text p15"><input type="button" class="reg_btn btn" value="更改" tabindex="4" onclick=" window.location.href='/Setting/SkinPicture'" />  </span> 
					<span class="at_text mb20">
                    <div style="width:620px; height:162px;" width="620" height="162">
                    <%  String personalityPicture = "/pics/setppt.gif";
                        if(Model.Member.MemberSkin != null) {
                            if (Model.Member.MemberSkin.PersonalityPicture != "" && Model.Member.MemberSkin.PersonalityPicture != null)
                            {
                                personalityPicture = Comm.getImagePath(Model.Member.MemberSkin.PersonalityPicture, ImageType.Original);
                            }
                        }
                    %>
                    <img src="<%=personalityPicture %>" alt="个性图片" onload="ImageZoomToJquery($(this), 620)" width="10" height="10" /> </div></span> 
              </div>
			    <div class="bind_form ">
				   <span class="at_text t_1_d">  |   <font color="#333">个性背景</font>    </span>
						
					<div class="bind_com">
					   <span class="at_text p15"><input type="button" class="reg_btn btn" value="更改" tabindex="4" onclick=" window.location.href='/Setting/SkinBackground'" />  </span> 
				    	<span class="at_text">
                        <div style="width:620px; height:620px;" width="620" height="620">
                        <%  String personalityBackgroundPicture = "/pics/setbgif.gif";
                        if(Model.Member.MemberSkin != null) {
                            if (Model.Member.MemberSkin.PersonalityBackgroundPicture != "" && Model.Member.MemberSkin.PersonalityBackgroundPicture != null)
                            {
                                personalityBackgroundPicture = Comm.getImagePath(Model.Member.MemberSkin.PersonalityBackgroundPicture, ImageType.Original);       
                            }
                        }
                    %>
                        <img src="<%=personalityBackgroundPicture %>" alt="个性背景" onload="ImageZoomToJquery($(this), 620)" width="10" height="10" /> </div> </span>    
					</div>	 
               </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em');
	 </script>
    <![endif]-->
</asp:Content>
