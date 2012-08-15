<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="hipanel">
<div class="hitypes">
<ul>
<li id="hi1" class="hitype" m_type="11" f_type="21" onclick="selecttype(1)">短语</li>
<li id="hi2" class="hitype" m_type="12" f_type="22" onclick="selecttype(2)">幽默</li>
<li id="hi3" class="hitype" m_type="13" f_type="23" onclick="selecttype(3)">搞怪</li>
<li id="hi4" class="hitype" m_type="14" f_type="24" onclick="selecttype(4)">赞美</li>
<li id="hi5" class="hitype" m_type="15" f_type="25" onclick="selecttype(5)">耍酷</li>
</ul>
</div>
<div id="listcontainer" class="histrs">
</div>
<span class="linkT1 left50"><a onclick="getothers()">换一批</a></span>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    var you = '<%=ViewData["you"] %>';
    var type = <%=ViewData["type"] %>;
    var sexnum = <%=ViewData["sexnum"] %>;

    $().ready(function () {
        getothers();
        $(".hitype").each(function(){
            $(this).hover(function(){ $(this).addClass("active"); },function(){ $(this).removeClass("active");});
        });
    });
    function selecttype(t)
    {
        type = sexnum*10+t;
        getothers()
    }
    function getothers() {

        gethi(type,$("#listcontainer"));
        $(".hitype").each(function(){

             $(this).removeClass("selected");
             
             if ($(this).attr("m_type")==type.toString() || $(this).attr("f_type").toString()==type.toString())
             {
                $(this).addClass("selected");
             }
        });
    }
    function gethi(type,container) {
        systemdataprovider.getsystemhi(type, function (result) {
            var his = $.parseJSON(result);
            bindhi(his, container);
            $(".histr").each(function(){
            $(this).hover(function(){ $(this).addClass("active"); },function(){ $(this).removeClass("active");});
        });
        });
    }
    function bindhi(objs, container) {
        var str = "<ul>";
        $.each(objs, function (i) {
            str += "<li id='" + objs[i].ID + "' class='histr' onclick='sendhi(\""+objs[i].Comment+"\",\""+you+"\")'>" + objs[i].Comment + "</li>";
        });
        str += "</ul>";
        container.html(str);
    }
    function sendhi(comment,you)
    {
        window.parent.jBox.tip("成功发送信息", 'info');
        MemberLinkProvider.sayhi(you, comment, function () {
            window.setTimeout(function () {
                window.parent.jBox.close(true);
            }, 10);  
        });
    }
</script>
</asp:Content>
