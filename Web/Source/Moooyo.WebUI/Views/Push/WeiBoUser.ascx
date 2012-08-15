<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<section class="Msectionbox fr mt32 clearfix">
    <div class="memb-info-cont-t">你当前的登陆账号为微博账号，请先设置一个你的邮箱作为米柚登陆账号！</div>
    <div class="memb-info-cont-b mt11" style="color:#666">
    <form id="weibouserform" name="weibouserform" method="post">
    <table width="500" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="40" height="50" align="right" class="editformtitle">邮箱：</td>
            <td align="left"><input type="text" name="email" id="email"></td>
        </tr>
        <tr>
            <td height="50" align="right" class="editformtitle">密码：</td>
            <td align="left"><input type="password" name="pwd" id="pwd"></td>
        </tr>
        <tr>
            <td height="50" colspan="2" align="left" class="editformtitle"><input type="button" onclick="weibouserupdate()" value="确定" tabindex="20" class="radius3 btn"/></td>
        </tr>
    </table>
    </form>
    </div>
</section>
<script type="text/javascript">
    $().ready(function () {
        setweiboValidate();
    });
    function setweiboValidate() {
        $("#weibouserform").validate({
            rules: {
                pwd: {
                    required: true, maxlength: 16, minlength: 6
                },
                email: {
                    required: true, email: true, maxlength: 100, remote: "/Register/IsEmailUsed"
                }
            },
            messages: {
                pwd: {
                    required: "密码必填", maxlength: "密码不能超过16个英文的长度", minlength: "密码长度不能少于6字符"
                },
                email: {
                    required: "Email必须输入。", email: "请输入一个有效的Email地址。", maxlength: "Email输入不能超过100个字符。", remote: "这个Email已经注册。"
                }
            }
        });
    }
    function weibouserupdate() {
        var weibouserform = $("#weibouserform");
        if (weibouserform.valid()) {
            memberprovider.setWeiBoUser($("#email").val(), $("#pwd").val(), function (data) {
                var data = $.parseJSON(data);
                window.location = window.location;
            });
        }
    }
</script>