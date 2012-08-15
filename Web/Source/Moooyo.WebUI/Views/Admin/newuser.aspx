<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
用户照片审核
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
<div class="selectbtn">
    <a onclick="audltokall(6);">6</a>
    <a onclick="audltokall(7);">7</a>
    <a onclick="audltokall(8);">8</a>
    <a onclick="audltokall(9);">9</a>
    <a onclick="audltokall(10);">10</a>
</div>
<div class="selectboy">
<div id="malemarks"></div>
<div class="clr"></div>
<div id="selectedmalemarks" class="photolist magT10"></div>
</div>
<div class="selectgirl">
<div id="femalemarks"></div>
<div class="clr"></div>
<div id="selectedfemalemarks" class="photolist magT10"></div>
</div>
<div class="clr"></div>
<ul id="listcontiner" class="impresslist"></ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<%--<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>--%>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<%--<link href="/css/main_<%=ViewData["cssversion"] %>.css" rel="stylesheet" type="text/css" />--%>
<script src="/js/admin.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    uploadpath = '<%=ViewData["uploadpath"] %>';
    var newusers = [];
    var malemarks = [];
    var femalemarks = [];
    var selectedmalemarks = [];
    var selectedfemalemarks = [];

    $(document).ready(function () {
        bindnewusers();
        getsystemmarks(1, $("#femalemarks"));
        getsystemmarks(2, $("#malemarks"));
    });
    function bindnewusers() {
        AdminProvider.getauditnewusers(100000, 1, function (objs) {
            newusers = objs;
            binding();
        });
    }
    function getsystemmarks(sex, continer) {
        systemdataprovider.getsystemmarks(sex, function (result) {
            if (sex == 1)
            { malemarks = $.parseJSON(result); bindmarks(malemarks, continer); }
            if (sex == 2)
            { femalemarks = $.parseJSON(result); bindmarks(femalemarks, continer); }
        });
    };
    function bindmarks(objs, continer) {
        for (i = 0; i < objs.length; i++) {
            continer.append(getdiv(objs[i].Content, objs[i].Sex));
        }
    }
    function getdiv(content, sex) {
        var str = "<div class='markdiv' onclick='selectmark(\"" + content + "\"," + sex + ",$(this));'>";
        str += content;
        str += "</div>";

        return str;
    }
    function selectmark(mark, sex, continer) {
        var has = false;
        if (sex == 1) {
            has = changeselectmark(mark, selectedmalemarks);
        }
        if (sex == 2) {
            has = changeselectmark(mark, selectedfemalemarks);
        }
        if (has)
            continer.removeClass("selectedmark");
        else
            continer.addClass("selectedmark");
    }
    function changeselectmark(mark, objs) {
        var has = false;
        $.each(objs, function (i) {
            if (objs[i] == mark) {
                objs[i] = null;
                has = true;
            }
        });
        if (!has) objs.push(mark);
        return has;
    }
    function audltokall(score) {
        var psmale = "";
        var psfemale = "";
        var malemark = "";
        var femalemark = "";

        $.each(selectedmalemarks, function (i) {
            malemark += selectedmalemarks[i] + "|";
        });
        $.each(selectedfemalemarks, function (i) {
            femalemark += selectedfemalemarks[i] + "|";
        });

        $.each(newusers, function (i) {
            if (newusers[i] != null) {
                if (newusers[i].Sex == 2)
                    psfemale += newusers[i].ID + "|";
                else
                    psmale += newusers[i].ID + "|";
            }
        });
        if (psmale != "" & malemark == "") {
            alert("里面有男性用户，却没有选择男性用户对应的印象，操作已取消！");
            return;
        }
        if (psfemale != "" & femalemark == "") {
            alert("里面有女性用户，却没有选择女性用户对应的印象，操作已取消！");
            return;
        }

        if (psmale != "") {
            AdminProvider.setmembersauditresult(psmale, 1, malemark, score,1, function (result) {
                bindnewusers();
            });
        }
        if (psfemale != "") {
            AdminProvider.setmembersauditresult(psfemale, 1, femalemark, score,2, function (result) {
                bindnewusers();
            });
        }
    }
    function binding() {
        var str = "";
        $.each(newusers, function (i) {
            if (newusers[i] != null) {
                var sexstr = "男";
                if (newusers[i].Sex == 2) sexstr = "女";
                str += "<li><img src='" + photofunctions.getprofileiconphotoname(newusers[i].MemberInfomation.IconPath) + "'/><br/>";
                str += "<a onclick='deluser(\"" + newusers[i].ID + "\")' href='javascript:;'>拒绝</a>  |  <a onclick='changesex(\"" + newusers[i].ID + "\",$(this))' href='javascript:;'>" + sexstr + "</a>";
            }
        });
        $("#listcontiner").html(str);
    }
    function changesex(id,continer) {
        $.each(newusers, function (i) {
            if (newusers[i] != null)
                if (newusers[i].ID == id) {
                    if (newusers[i].Sex == 0) {
                        newusers[i].Sex = 2;
                        continer.html("女");
                    }
                    else {
                        newusers[i].Sex = 0;
                        continer.html("男");
                    }
                }
        });
    }
    function getsex(id) {
        var sex = 0;
        $.each(newusers, function (i) {
            if (newusers[i] != null)
                if (newusers[i].ID == id) {
                    sex = newusers[i].Sex == 0 ? 1 : 2;
                }
            });
            return sex;
    }
    function deluser(id) {
        var sex = getsex(id);
            AdminProvider.setmembersauditresult(id, -1, "", 0, sex, function (result) {
            if ($.parseJSON(result).ok) {
                $.each(newusers, function (i) {
                    if (newusers[i] != null)
                        if (newusers[i].ID == id) newusers[i] = null;
                });
                binding();
            }
            else {
                alert("删除失败");
            }
        });
    }
</script>
</asp:Content>
