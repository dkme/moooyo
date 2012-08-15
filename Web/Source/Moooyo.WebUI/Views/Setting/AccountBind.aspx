<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberBindingPlatformModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	绑定设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>绑定设置</span><em>▲</em></div>
			<div class="Set_box">
			    <%--<div class="bind_form border_b">
				        <span class="at_text t_1_d">  <strong>|   绑定手机 </strong> </span>
				            <span class="at_text t_1_d">  你还没有绑定手机  <a href="#" class="red">马上去绑定手机</a>  </span> 
                </div>--%>
			    <div class="bind_form ">
				    <span class="at_text t_1_d">  |   <font color="#333">绑定其他网站</font>    </span>
						
					<div class="bind_com">
						<ul>
						<li>
                        <% 
                            if (!Model.IsBinded(Moooyo.BiZ.Member.Connector.Platform.SinaWeibo))
                            {
                                %>
                            <b><em><img src="/pics/weibo-01.png" alt="新浪微博" /></em> 新浪微博</b><a href="javascript:;" class="red" onclick="window.open('/MicroConn/ConnectToSinaWeibo?isbinding=true')">点击绑定</a>
                        <%
                            }
                            else
                            { 
                               %>
                            <b><em><img src="/pics/weibo-01.png" alt="新浪微博" /></em> 新浪微博</b><a href="javascript:;" class="huise" onclick="unbind(<%=(int)Moooyo.BiZ.Member.Connector.Platform.SinaWeibo %>)">取消绑定</a>
                        <%
                            }
                           %>
                        </li>
						<li>
                        <% 
                            if (!Model.IsBinded(Moooyo.BiZ.Member.Connector.Platform.TencentWeibo))
                            { 
                               %>
                            <b><em><img src="/pics/weibo-2.png" alt="腾讯微博" /></em> 腾讯微博</b><a href="javascript:;" class="red" onclick="window.open('/MicroConn/ConnectToTXWeibo?isbinding=true')">点击绑定</a>
                        <%
                            }
                            else
                            {
                                %>
                            <b><em><img src="/pics/weibo-2.png" alt="腾讯微博" /></em> 腾讯微博</b><a href="javascript:;" class="huise" onclick="unbind(<%=(int)Moooyo.BiZ.Member.Connector.Platform.TencentWeibo %>)">取消绑定</a>
                        <% 
                            }
                           %>
                        </li>
						<li>
                        <% 
                            if (!Model.IsBinded(Moooyo.BiZ.Member.Connector.Platform.RenRen))
                            { 
                            %>
                            <b><em><img src="/pics/weibo3.png" alt="人人帐号" /></em> 人人</b><a href="javascript:;" class="red" onclick="window.open('/MicroConn/ConnectToRenRen?isbinding=true')">点击绑定</a>
                        <%
                            }
                            else
                            { 
                            %>
                            <b><em><img src="/pics/weibo3.png" alt="人人帐号" /></em> 人人</b><a href="javascript:;" class="huise" onclick="unbind(<%=(int)Moooyo.BiZ.Member.Connector.Platform.RenRen %>)">取消绑定</a>
                        <% 
                            }
                            %>
                        </li>
						<li>
                        <% 
                            if (!Model.IsBinded(Moooyo.BiZ.Member.Connector.Platform.Douban))
                            { 
                            %>
                            <b><em><img src="/pics/weibo_4.png" alt="豆瓣帐号" /></em> 豆瓣</b><a href="javascript:;" class="red" onclick="window.open('/MicroConn/ConnectToDouBan?isbinding=true')">点击绑定</a>
                        <%
                            }
                            else
                            { 
                            %>
                            <b><em><img src="/pics/weibo_4.png" alt="豆瓣帐号" /></em> 豆瓣</b><a href="javascript:;" class="huise" onclick="unbind(<%=(int)Moooyo.BiZ.Member.Connector.Platform.Douban %>)">取消绑定</a>
                        <% 
                            }
                            %>
                        </li>
						</ul>
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
    <script type="text/javascript">
        function unbind(t) {
            MicroConn.setConnectorEnableStatusFalse(t, function (result) {
                $.jBox.tip("成功取消绑定！", "success");
                setTimeout(function () {
                    window.location.reload();
                }, 4000);
            });
        }
    </script>
</asp:Content>
