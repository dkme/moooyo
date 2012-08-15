<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="hipanel">
<div class="hitypes">
<ul>
<li id="hi1" class="hitype" onclick="getskillslist('实用类')">实用类</li>
<li id="hi2" class="hitype" onclick="getskillslist('语言类')">语言类</li>
<li id="hi3" class="hitype" onclick="getskillslist('信息类')">信息类</li>
<li id="hi4" class="hitype" onclick="getskillslist('体育类')">体育类</li>
<li id="hi5" class="hitype" onclick="getskillslist('才艺类')">才艺类</li>
<li id="Li1" class="hitype" onclick="getskillslist('专业类')">专业类</li>
<li id="Li2" class="hitype" onclick="getskillslist('另类')">另类</li>
</ul>
</div>
<%--<span class="info">（点击选择）</span>--%>
<div id="listcontainer" class="histrs"></div>
<div class="clear"></div>
<div class="inputself">上面没有，我要自己添加<input type="text" id="inputskillname" /><input type="button" class="savebtn" value="确定"  onclick="selfinputsave()"/></div>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var containername = '<%=ViewData["containername"] %>';
    $().ready(function () {
        $(".hitype").each(function () {
            $(this).hover(function () { $(this).addClass("active"); }, function () { $(this).removeClass("active"); });
        });
        getskillslist("实用类");
    });
    function getskillslist(type) {
        systemdataprovider.getsystemskills(type, 30, function (result) {
            var objs = $.parseJSON(result);
            var str = "";
            for (var i = 0; j = objs[i]; i++) {
                str += "<div class='skilllabel' onclick='window.parent.member_i_functions.setinputskill($(this).html(), containername );window.parent.jBox.close(true);'>" + objs[i].SkillName + "</div>";
            };
            $("#listcontainer").html(str);
            $(".skilllabel").hover(function () { $(this).addClass("skilllabelactive"); }, function () { $(this).removeClass("skilllabelactive"); });

            return objs;
        });
    }
    function selfinputsave() {
        var knows = $("#inputskillname").val();
        if (trim(knows) == "") { $("#inputskillname").focus(); return; }
        if (!checkLen(knows)) return;
        
        window.parent.member_i_functions.setinputskill($('#inputskillname').val(), containername);
        window.parent.jBox.close(true);
    }
</script>
</asp:Content>
