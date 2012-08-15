<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="hipanel">
<div class="hitypes">
<ul>
<li id="dateus" class="hitype" onclick="selecttype('us')">你们想做的事</li>
<li id="dateother" class="hitype" onclick="selecttype('other')">另外选一个</li>
</ul>
</div>
<span class="info">（点击直接发送）</span>
<br />
<br />
<span class="info">我要约Ta：</span>
<div id="listcontainer" class="histrs"></div>
<span class="linkT1 left50" id="changeother"><a onclick="getwantslist(0);">换一批</a></span>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var you = '<%=ViewData["you"] %>';
    var youstr = '<%=ViewData["youstr"] %>';
    var me = '<%=ViewData["me"] %>';
    var mestr = '<%=ViewData["mestr"] %>';

    $().ready(function () {
        selecttype("us");
    });
    function selecttype(t)
    {
        if (t == "us") {
            $("#dateus").addClass("active");
            $("#dateother").removeClass("active");
            $("#changeother").hide();
            getuswants();
        }
        else {
            $("#dateother").addClass("active");
            $("#dateus").removeClass("active");
            $("#changeother").show();
            getwantslist(0);
        }

        $(".histr").each(function () {
            $(this).hover(function () { $(this).addClass("active"); }, function () { $(this).removeClass("active"); });
        });
    }
    function getuswants() {
        var flag = false;
        var str = "<ul>";
        if (youstr != "") {
            str += "<li class='histr' onclick='senddate(\"" + youstr + "\",\"" + you + "\")'>" + youstr + " (Ta想做的事)</li>";
            flag = true;
        }
        if (mestr != "") {
            str += "<li class='histr' onclick='senddate(\"" + mestr + "\",\"" + you + "\")'>" + mestr + " (我想做的事)</li>";
            flag = true;
        }
        str += "</ul>";

        if (!flag) {
            getwantslist(0);
        }
        else {
            $("#listcontainer").html(str);
        }
    }
    function getwantslist(type) {
        systemdataprovider.getsystemwants(type, 5, function (result) {
            var objs = $.parseJSON(result);
            var str = "<ul>";
            $.each(objs, function (i) {
                str += "<li class='histr' onclick='senddate(\"" + objs[i].IWantStr + "\",\"" + you + "\")'>" + objs[i].IWantStr + "</li>";
            });
            str += "</ul>";
            $("#listcontainer").html(str);

            $(".histr").each(function () {
                $(this).hover(function () { $(this).addClass("active"); }, function () { $(this).removeClass("active"); });
            });
        });
    }
    
    function senddate(comment,you) {
        MemberLinkProvider.date(you, comment, function () {
            window.parent.jBox.close(true);
        });
    }
</script>
</asp:Content>