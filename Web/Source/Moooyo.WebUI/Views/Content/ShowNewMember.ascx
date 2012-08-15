<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div id="showMember"></div>
<%--<%
    IList<Moooyo.BiZ.Member.Member> memberList = new List<Moooyo.BiZ.Member.Member>();
    if (ViewData["memberList"] != null)
    {
        memberList = (List<Moooyo.BiZ.Member.Member>)ViewData["memberList"];
        foreach (var obj in memberList)
        {
            %><%=obj.MemberInfomation.IconPath %><br /><%
        }
    }
    %>--%>
<script type="text/javascript">
    var lastTimestr = "";
    $().ready(function () {
        loadmemberList("", "");
    });
    function loadmemberList(interestid,createdtime) {
        ContentProvider.ShowNewMember(interestid, createdtime, function (data) {
            var data = $.parseJSON(data);
            loadmember(data);
        });
    }
    function loadmember(memberlist) {
        var fristtimestr = memberlist.length > 0 ? memberlist[0].CreatedTime : "";
        if (fristtimestr != "") {
            var fristtime = new Date(paserJsonDate(fristtimestr).getTime());
            lastTimestr = fristtime.getFullYear() + "-" + (fristtime.getMonth() + 1) + "-" + fristtime.getDate() + " " + fristtime.getHours() + ":" + fristtime.getMinutes() + ":" + fristtime.getSeconds() + "." + fristtime.getMilliseconds();
        }
        newcomer_render(memberlist);
        var timeout = setTimeout(function () {
            loadmemberList("", lastTimestr);
            clearTimeout(timeout);
        }, 10000);
    }
</script>