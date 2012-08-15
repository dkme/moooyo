<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
视频认证
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>视频认证</span><em>▲</em></div>
			<div class="Set_box">
                <div class="bind_form"><span class="at_text t_1_d" style="color:#666;" >
			      通过与客服短暂视频连接，来确认上传的头像照是否与您本人相符，认证<br/>
              通过后，将获得重点推荐，视频认证徽章将点亮！ 
			    </span>
				<span><a href="#" class="red">完成视频认证，将获得视频认证徽章。</a> <img src="/pics/pic_video.gif" style="vertical-align:middle" alt="视频认证" /></span>
				</div>
			    <div class="bind_form ">
					<span class="bind_video" style="height:260px; width:300px;">
                    <% string status = ViewData["isAuth"].ToString();
                    if (status == "pass")
                    {
                    %>
                        <div style=" text-align:center; margin-top:100px; font-size:18px; font-weight:bold; color:#999999;">认证已经通过</div>
                    <%}
                    else if (status == "notIcon")
                    {%>
                        <div style=" text-align:center; margin-top:85px; font-size:18px; font-weight:bold; color:#999999;">您还没有头像照，请上传一<br />张满意的照片！</div>
                    <% }
                    else if (status == "Auth")
                    {%>
                    <div id="anniu">
                        <div class="right-btn-huiyuan" style="text-align:center; margin-top:100px;">
                            <a href="javascript:;" onclick="shipin()"><img src="/pics/pic_video01.png" alt="视频认证" /></a></div>
                        </div> 
                        <object id="Object1" style="display:none;" align="middle" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
                        codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
                        height="260" width="300">
                        <param name="allowScriptAccess" value="sameDomain" />
                        <param name="movie" value="/swf/kjk.swf" />
                        <param name="quality" value="high" />
                        <param name="bgcolor" value="#ffffff" />
                        <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" height="260"
                            name="My_Cam" pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high"
                            src="/swf/kjk.swf" type="application/x-shockwave-flash" width="300"></embed>
                        </object>
                    <%}
                    else if (status == "wait")
                    { %>
                        <div style=" text-align:center; margin-top:100px; font-size:18px; font-weight:bold; color:#999999;">正在审核中</div>
                
                    <%}
                    else if (status == "lose")
                    { %>
                        <div id="anniu" style=" text-align:center; margin-top:100px; font-size:18px; line-height:20px; font-weight:bold; color:#999999;">您上次视频认证失败了。<br /><a onclick="shipin()" href="javascript:;">重新认证</a></div>
                            <object id="Object1" style="display:none;" align="middle" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000"
                        codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0"
                        height="260" width="300">
                        <param name="allowScriptAccess" value="sameDomain" />
                        <param name="movie" value="/swf/kjk.swf" />
                        <param name="quality" value="high" />
                        <param name="bgcolor" value="#ffffff" />
                        <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" height="260"
                            name="My_Cam" pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high"
                            src="/swf/kjk.swf" type="application/x-shockwave-flash" width="300"></embed>
                        </object>
                    <%} %>
                    </span>
						
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
    <script type="text/javascript">
        function shipin() {
            document.getElementById("anniu").style.display = "none";
            document.getElementById("Object1").style.display = "block";
        }
    </script>
</asp:Content>
