<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	账号管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="Admin_TopNav"><% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%> </div>
    <div class="Admin_Dic_LeftNav">
        <ul  class="leftlist magT10">
        <li><a href="/admin/accountmanager">账号管理</a></li>
        <li><a href="/Admin/SystemManager">系统账号管理</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div class="contenttit">
            是否允许登录：
            <select id="typesel" onchange="$('#stext').val('');searchlist()">
                <option value="1">允许登录</option>
                <option value="0">禁止登录</option>
            </select>
            账户类型：
            <select id="usersel" onchange="$('#stext').val('');searchlist()">
                <option value="0">所有</option>
                <option value="1" selected="selected">本地用户</option>
                <option value="2">腾讯微博</option>
                <option value="3">新浪微博</option>
                <option value="4">人人网</option>
                <option value="5">豆瓣网</option>
            </select><br /><br />
            搜索：
            <select id="ssel">
                <option value="1">Email</option>
                <option value="2">ID</option>
            </select>
            <input type="text" id="stext" name="stext" />
            <input type="button" id="btnsearch" onclick="searchlist()" value="搜索" />&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="Button1" onclick="updatetype(0)" value="禁用账户" />
            <input type="button" id="Button2" onclick="updatetype(1)" value="启用账户" />
            <input type="button" id="Button3" onclick="updaphotoisreal(0)" value="取消视频认证" />
            <input type="button" id="Button4" onclick="updaphotoisreal(1)" value="通过视频认证" />
            <br /><br />
            <div id="listcontiner" class="account">
                
            </div>   
            <div id="pager" class="verifyPager"></div>     
        </div>
    </div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pagesize = 40;
        var mypagecount = 1;
        var pageno = 1;
        var type = 1;
        var usersel = 1;
        var ssel = 1;
        var scontent = "";
        $(document).ready(function () {
            settype(1, 1, $("#ssel").val(), $("#stext").val());
            count();
            bindlist();
        });

        function updatetype(type) {
            var str = "";
            $.each($("ul.list .mycheck"), function (i) {
                if ($(this).attr("checked")) {
                    str += $(this).attr("data-memberid") + ",";
                }
            });
            str = str.substr(0, str.length - 1);
            if (str.length <= 0) { alert("请先选择用户！"); return; }
            AdminProvider.UpdateAllowLogin(str, type, function (data) {
                var obj = $.parseJSON(data);
                count();
                bindlist();
            });
        }

        function updaphotoisreal(type) {
              
            var str = "";
            $.each($("ul.list .mycheck"), function (i) {
                if ($(this).attr("checked")) {
                    str += $(this).attr("data-memberid") + ",";
                }
            });
            str = str.substr(0, str.length - 1);
            if (str.length <= 0) { alert("请先选择用户！");return; }
            if (!confirm("您确定要修改所选用户的视频认证状态吗？")) return;         
            AdminProvider.UpdateUserPhotoisReal(type, str, function (data) {
                var obj = $.parseJSON(data);
                if (obj) {
                    alert("修改成功！");
                }
                else {
                    alert("更新失败，请查看异常日志！");
                 }
                count();
                bindlist();
            });
        }

        function allcheck() {
            // $("ul.list .mycheck").attr("checked", ($(".mycheckparent").attr("checked") == "checked"? true:false));
            $("ul.list .mycheck").prop("checked", function (i, val) { return !val});
        }

        function bindlist() {
            AdminProvider.GetAllAccount(type, usersel, ssel, scontent, pageno, pagesize, function (result) {
                var objs = $.parseJSON(result);
                var str = "";
                str += "<ul class=\"list\">";
                str += "<li class=\"cur w60\"><input class=\"mycheckparent\" type=\"checkbox\" name=\"ckcall\" onclick=\"allcheck()\">" + "全选</li><li class=\"cur w160\">邮箱</li><li class=\"cur w160\">注册时间</li><li class=\"cur w60\">状态</li><li class=\"cur w100\">最后登录IP</li><li class=\"cur w60\">邀请数</li><li class=\"cur w220\">账户地址</li><li class=\"cur w60\">视频认证</li><br/><br/>";
                $.each(objs, function (i) {
                    var li = "";
                    var endli = "</li>";
                    var num = (pageno - 1) * pagesize + i + 1;
                    if (i % 2 == 0) li = "cur";
                    else li = "";
                    str += "<li class=\"" + li + " w60\">" + num + ".<input class=\"mycheck\" type=\"checkbox\" data-memberid=\"" + objs[i].ID + "\" name=\"listckc\">" + endli;
                    str += "<li class=\"" + li + " w160\">" + (objs[i].Email == "" ? "empty" : objs[i].Email) + endli;
                    str += "<li class=\"" + li + " w160\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + endli;
                    str += "<li class=\"" + li + " w60\">" + (objs[i].AllowLogin ? "允许" : "禁止") + endli;
                    str += "<li class=\"" + li + " w100\">" + (objs[i].LastLoginIP == "" ? "empty" : objs[i].LastLoginIP) + endli;
                    str += "<li class=\"" + li + " w60\">" + "<span style=\"cursor:pointer;\" onclick=\"showtomember(" + i + ")\">" + (objs[i].ToRegInviterMember.length) + "</span>" + endli;
                    var memberID = objs[i].UniqueNumber != null ? "/u/" + objs[i].UniqueNumber.ConvertedID : objs[i].ID;
                    str += "<li class=\"" + li + " w220\">" + memberID + endli;
                    str += "<li class=\"" + li + " w60\">" + (objs[i].MemberPhoto.IsRealPhotoIdentification ? "是" : "否") + endli;
                    str += "<br/>";
                    str += "<div class=\"tomember" + i + "\" style=\"display:none;\">";
                    $.each(objs[i].ToRegInviterMember, function (j) {
                        var li2 = "";
                        var endli = "</li>";
                        if (j % 2 == 0) li2 = "li2";
                        else li2 = "";
                        var objss = objs[i].ToRegInviterMember[j];
                        str += "<li class=\"" + li2 + " w60\">" + "#" + endli;
                        str += "<li class=\"" + li2 + " w160\">" + (objss.Email == "" ? "empty" : objss.Email) + endli;
                        str += "<li class=\"" + li2 + " w160\">" + paserJsonDate(objss.CreatedTime).format('yyyy-mm-dd HH:MM') + endli;
                        str += "<li class=\"" + li2 + " w60\">" + (objss.AllowLogin ? "允许" : "禁止") + endli;
                        str += "<li class=\"" + li2 + " w100\">" + (objss.LastLoginIP == "" ? "empty" : objss.LastLoginIP) + endli;
                        str += "<li class=\"" + li2 + " w60\">" + "#" + endli;
                        str += "<li class=\"" + li2 + " w220\">" + memberID + endli;
                        str += "<li class=\"" + li2 + " w60\">" + (objss.MemberPhoto.IsRealPhotoIdentification ? "是" : "否") + endli;
                        str += "<br/>";
                    });
                    str += "</div>";
                });
                str += "</ul>";
                if (result == "[]") {
                    str = "<center>没有数据！</center>";
                }
                $("#listcontiner").html(str);
            });
        }
        
        function showtomember(index) {
            if ($("div.tomember" + index).css("display") == "none")
                $("div.tomember" + index).css("display", "block");
            else
                $("div.tomember" + index).css("display", "none");
        }

        function searchlist() {
            settype($("#typesel").val(), $("#usersel").val(), $("#ssel").val(), $("#stext").val());
            count();
            bindlist();
        }
//        function typeselchange() {
//            settype($("#typesel").val(), "", "", "");
//            count();
//            bindlist();
//        }
//        function userselchange() {
//            settype("", $("#usersel").val(), "", "");
//            count();
//            bindlist();
//        }
        function settype(_type, _usersel, _ssel, _scontent) {
            pageno = 1;
            type = _type;
            usersel = _usersel;
            ssel = _ssel;
            scontent = _scontent;
        }
        function count() {
            var _pagecount = 0;
            $.ajaxSetup({
                async: false
            });
            AdminProvider.GetAllAcountCount(type, usersel, ssel, scontent, function (result) {
                var allcount = $.parseJSON(result);
                _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) - 1) / parseInt(pagesize));
            });
            mypagecount = _pagecount;
            if (pageno > mypagecount) {
                pageno = mypagecount;
            }
            $.ajaxSetup({
                async: true
            });
            setPager();
        }

        PageClick = function (pageclickednumber) {
            if (pageclickednumber >= mypagecount) {
                pageno = mypagecount;
            }
            else {
                pageno = pageclickednumber;
            }
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: mypagecount, buttonClickCallback: PageClick });
            bindlist();
        }
        function setPager() {
            $("#pager").pager({ pagenumber: pageno, pagecount: mypagecount, buttonClickCallback: PageClick });
        }

    </script>
</asp:Content>
