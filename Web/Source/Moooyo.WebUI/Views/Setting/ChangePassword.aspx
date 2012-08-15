<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	修改密码
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
        <div class="Set_content">
		     <div class="Set_title"> <span>修改密码</span><em>▲</em></div>
			 <div class="Set_box">
			 <form action="" method="post" name="frmMemberPasswd" id="frmMemberPasswd">
			    <div class="update_form">
		      <dl>
            <dd class="clearfix" style="height: auto;">
                <em class="lab">旧密码:</em>
                <span class="at_text t_1_d">
                    <input type="password" value="" class="txtput" name="oldpwd" id="oldpwd" />
                </span>
                
            </dd>
            <dd class="clearfix">
                <em class="lab">新密码:</em>
                <span class="at_text t_1_d">
                    <input type="password" name="newpwd" id="newpwd" class="txtput newpasstxt"  />
                </span>	</dd>
            <dd class="clearfix">
                <em class="lab">确认密码:</em>
                <span class="at_text t_1_d">
                    <input type="password" name="confirmPassword" id="confirmPassword" class="txtput" />
                </span>		 </dd>
            <dd class="clearfix">
                <em class="lab"></em><span class="at_text t_1_d">
                <input name="button" type="button" class="reg_btn btn" value="提交" onclick="resetPassword()" />&nbsp;&nbsp;&nbsp;<a href="/Account/ResetPassword?back=/Setting/ChangePassword">忘记密码？</a>
                </span></dd>
			 </dl>
            
			   </div>
			 </form>
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
    <script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em,.link_bg');
	 </script>
    <![endif]-->
    <script type="text/javascript">
        $().ready(function () {
            setValidate();
        });
        function setValidate() {
            $("#frmMemberPasswd").validate({
                rules: {
                    oldpwd: {
                        required: true, maxlength: 16, minlength: 6
                    },
                    newpwd: {
                        required: true, maxlength: 16, minlength: 6
                    },
                    confirmPassword: {
                        required: true, maxlength: 16, minlength: 6
                    }
                },
                messages: {
                    oldpwd: {
                        required: "请输入旧密码", maxlength: "旧密码不能超过16个英文的长度", minlength: "旧密码长度不能少于6字符"
                    },
                    newpwd: {
                        required: "请输入新密码", maxlength: "新密码不能超过16个英文的长度", minlength: "新密码长度不能少于6字符"
                    },
                    confirmPassword: {
                        required: "请再次输入新密码", maxlength: "确认密码不能超过16个英文的长度", minlength: "确认密码长度不能少于6字符"
                    }
                }
            });
        }
        function resetPassword() {
            var frmMemberPasswd = $("#frmMemberPasswd");
            if (frmMemberPasswd.valid()) {
                MemberLinkProvider.isOldPwdRight($("#oldpwd").val(), function (result) {
                    if (result) {
                        MemberLinkProvider.setNewpwd($("#oldpwd").val(), $("#newpwd").val(), function (data) {
                            var result = $.parseJSON(data);
                            if (result.ok) {
                                $.jBox.tip("成功修改密码，下次登录请使用新密码！", "success");
                                $("#oldpwd").val("");
                                $("#newpwd").val("");
                                $("#confirmPassword").val("");
                                $("#oldpwd").focus();
                            }
                        });
                    }
                    else {
                        $.jBox.tip("旧密码输入错误！", "err");
                        $("#oldpwd").val("");
                        $("#oldpwd").focus();
                    }
                });
            }
        }
    </script>
</asp:Content>
