<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	隐私设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
        <div class="Set_content">
		    <div class="Set_title"> <span>隐私设置</span><em>▲</em></div>
			<div class="Set_box">
                <div class="yinsi">
       		        <div class="yinsi_nr">
            	        <input name="autoAddToFavor" type="checkbox" id="autoAddToFavor" value="1" <%=((bool)ViewData["AutoAddOutCallingToMyFavorList"] ? "checked='checked'" : "") %> style="border:none;"/>
            	        &nbsp;自动关注我发米邮的对象
   			        </div>
                    <div class="yinsi_nr">
            	        <input name="onlySeniorMember" type="checkbox" id="onlySeniorMemberTS" value="1" <%=((bool)ViewData["OnlySeniorMemberCanTalkSaiHiMe"] ? "checked='checked'" : "") %> style="border:none;"/>
   	                &nbsp;高级会员才能给我米邮和打招呼（高级、VIP会员特权
   			        ）<span id="upgradeSenior"></span></div>
        <div class="yinsi_nr">
            	        <input name="onlyVIPMember" type="checkbox" id="onlyVIPMemberTS" value="1" <%=((bool)ViewData["OnlyVIPMemberCanTalkSaiHiMe"] ? "checked='check'" : "") %> style="border:none;"/>
       	              &nbsp;VIP会员才能给我米邮和打招呼
   			        （VIP会员特权）<span id="upgradeVIP"></span></div>
                    <div class="yinsi_anniu">
            	        <div class="right-btn-huiyuan"><a href="javascript:;" class="radius3 reg_btn btn fl" onclick="setPrivacy()">&nbsp;&nbsp;确定&nbsp;&nbsp;</a></div>
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
    <script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em,.txtput');
	 </script>
    <![endif]-->
    <script type="text/javascript">
        $(document).ready(function () {
        <%switch(Model.Member.MemberType) {
            case 0:
                %>$("#onlySeniorMemberTS").attr("disabled", true);
                $("#onlyVIPMemberTS").attr("disabled", true);
                $("#upgradeSenior").html("<a href=\"#\">我要升级</a>");
                $("#upgradeVIP").html("<a href=\"#\">我要升级</a>");<%
                break;
            case 1:
                %>$("#onlyVIPMemberTS").attr("disabled", true);
                $("#upgradeVIP").html("<a href=\"#\">我要升级</a>");<%
                break;
            case 2:
                %><%
                break;
        }%>
        });

        function setPrivacy() {
            var flagAutoAddToFavor = $("#autoAddToFavor").attr("checked");
            flagAutoAddToFavor = flagAutoAddToFavor == "checked" ? true : false;
            var flagOnlySeniorMemberTS = $("#onlySeniorMemberTS").attr("checked");
            flagOnlySeniorMemberTS = flagOnlySeniorMemberTS == "checked" ? true : false;
            var flagOnlyVIPMemberTS = $("#onlyVIPMemberTS").attr("checked");
            flagOnlyVIPMemberTS = flagOnlyVIPMemberTS == "checked" ? true : false;
            MemberLinkProvider.setPrivacy(
                flagAutoAddToFavor, 
                flagOnlySeniorMemberTS, 
                flagOnlyVIPMemberTS, 
                function (data) {
                    var result = $.parseJSON(data);
                    if (result.ok) {
                        $.jBox.tip("隐私设置保存成功！", "success");
                    }
                    else 
                        $.jBox.tip("隐私设置失败，系统维护中，给您带来了不便，请谅解！", "error");
            });
        }
    </script>
</asp:Content>
