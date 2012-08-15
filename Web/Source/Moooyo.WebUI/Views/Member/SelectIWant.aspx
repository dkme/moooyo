<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="hipanel">
<div class="hitypes">
我想和Ta：<span class="info">（点击选择）</span>
</div>
<div id="listcontainer" class="histrs"></div>
<span class="linkT1 left50" style="margin-left:20px;"><a onclick="getwantslist(0);">换一批</a></span>
<div class="clear"></div>
<div class="inputself">上面没有，我要自己添加<input type="text" id="inputwantsname" style="width:200px;" /><input type="button" class="savebtn" value="确定"  onclick="selfinputsave()"/></div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var containername = '<%=ViewData["containername"] %>';
    $().ready(function () {
        getwantslist(0);
    });
    function getwantslist(type) {
        systemdataprovider.getsystemwants(type, 5, function (result) {
            var objs = $.parseJSON(result);
            var str = "";
            $.each(objs, function (i) {
                str += "<div class='histr mt5 bor-b1 ml20' onclick='window.parent.member_i_functions.setinputskill($(this).html(), containername );window.parent.jBox.close(true);'>" + objs[i].IWantStr + "</div>";
            });
            $("#listcontainer").html(str);
            $(".histr").hover(function () { $(this).addClass("skilllabelactive"); }, function () { $(this).removeClass("skilllabelactive"); });
        });
    }
    function selfinputsave() {
        var wants = $("#inputwantsname").val();
        if (trim(wants) == "") { $("#inputwantsname").focus(); return; }
        if (!checkLen(wants)) return;

        window.parent.member_i_functions.setinputskill(wants, containername);
        window.parent.jBox.close(true);
    }
</script>
</asp:Content>
