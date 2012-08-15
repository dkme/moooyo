<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
举报
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../css/main_0101.css" rel="stylesheet" type="text/css" />
<div>
<div class="jyjbTitle">
<ul>
<li id="jb">标题：我要举报</li>
<li id="jy">标题：我要建议</li>
</ul>
</div>
<div id="jbpanel">
<div>（点击选择后，输入说明并发送）</div>
<div id="listcontainer">
    <div class='skilllabel' m_type="11" onclick="selectjb(11)">色情</div>
    <div class='skilllabel' m_type="12" onclick="selectjb(12)">暴力</div>
    <div class='skilllabel' m_type="13" onclick="selectjb(13)">骚扰</div>
    <div class='skilllabel' m_type="14" onclick="selectjb(14)">谩骂</div>
    <div class='skilllabel' m_type="15" onclick="selectjb(15)">广告</div>
    <div class='skilllabel' m_type="16" onclick="selectjb(16)">欺诈</div>
    <div class='skilllabel' m_type="17" onclick="selectjb(17)">反动</div>
    <div class='skilllabel' m_type="18" onclick="selectjb(18)">政治</div>
    <div class='skilllabel' m_type="19" onclick="selectjb(19)">其他</div>
</div><br /><br />
<div><span class="leftTitle">说明：</span><TextArea id="inputdescription" ></TextArea></div>
</div>
<div id="jypanel">
<span class="leftTitle">内容：</span><TextArea id="inputsuject" ></TextArea>
</div>
<input type="button" value="发送" onclick="save()" class="savebu"/>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var type = '<%=ViewData["type"] %>';
    var toid = '<%=ViewData["mid"] %>';
    var selectjbtype = 0;
    $(document).ready(function () {
        if (type == "1") {
            $("#jb").show();
            $("#jbpanel").show();
            $("#jy").hide();
            $("#jypanel").hide();

            $(".skilllabel").each(function () {
                $(this).hover(function () { $(this).addClass("active"); }, function () { $(this).removeClass("active"); });
            });
        }
        if (type == "2") {
            $("#jb").hide();
            $("#jbpanel").hide();
            $("#jy").show();
            $("#jypanel").show();
        }
    });
    function selectjb(t) {
        selectjbtype = t;
        $(".skilllabel").each(function () {
            if ($(this).attr("m_type") == t.toString()) {
                $(this).hover(null, null);
                $(this).addClass("skilllabelactive");
            }
            else {
                $(this).removeClass("skilllabelactive");
                $(this).hover(function () { $(this).addClass("active"); }, function () { $(this).removeClass("active"); });
            }
        });
    }
    function save() {
        if (type == "1") {
            if (selectjbtype == 0) {
                alert("请先选择举报类型并说明情况。");
                return;
            }
            var content = $("#inputdescription").val();
            if (!checkLen(content)) return;

            CallAdmin.call(toid, selectjbtype, content, function () {
                window.parent.jBox.close(true);
                alert("已收到您的举报，我们将核实情况并进行相应处理，谢谢！");
            });
        }
        if (type == "2") {
            var content = $("#inputsuject").val();
            if (!checkLen(content)) return;

            CallAdmin.call(toid, 21, content, function () {
                window.parent.jBox.close(true);
                alert("已收到您的建议，谢谢您的支持！");
            });
        }
    }
</script>
</asp:Content>
