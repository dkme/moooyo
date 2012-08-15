<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding-left:20px;">
<ul class="fansGroup">
    <li>粉丝团名称：<input type="text" name="name" id="name" size="20" maxlength="10" /></li>
    <li>第一名名称：<input type="text" name="firstName" id="firstName" size="20" maxlength="10" /></li>
    <li>第二名名称：<input type="text" name="second" id="second" size="20" maxlength="10" /></li>
    <li>第三名名称：<input type="text" name="theThird" id="theThird" size="20" maxlength="10" /></li>
    <li><input type="button" value="保存" onclick="updateFansGroupName()" /></li>
</ul>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
.fansGroup li{line-height:32px;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $().ready(function () {
            getFansGroupName();
        });
        function getFansGroupName() {
            memberprovider.getFansGroupName("", function (data) {
                if (data != "null") {
                    var objs = $.parseJSON(data);
                    $("#name").val(objs.Name);
                    $("#firstName").val(objs.FirstName);
                    $("#second").val(objs.Second);
                    $("#theThird").val(objs.TheThird);
                }
            });
        }
        function updateFansGroupName() {
            memberprovider.updateFansGroupName("", $("#name").val(), $("#firstName").val(), $("#second").val(), $("#theThird").val(), function (data) {
                var result = $.parseJSON(data);
                if (result.ok) {
                    $.jBox.tip("保存成功", 'info');
                    window.setTimeout(function () {
                        window.parent.jBox.close(true);
                    }, 3000);
                }
                else {
                    $.jBox.tip("保存失败，系统维护中，给您带来了不便，请谅解！", 'error');
                    window.setTimeout(function () {
                        window.parent.jBox.close(true);
                    }, 3000);
                }
            });
        }
    </script>
</asp:Content>
