<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.ActivesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    活动管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="Admin_TopNav">
        <% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
    </div>
    <div class="Admin_Dic_LeftNav">
        <ul class="leftlist magT10">
            <li><a href="/Admin/Actives?type=activity">网站活动</a></li>
            <li><a href="/Admin/Actives?type=invitecode">邀请码</a></li>
        </ul>
    </div>
    <div class="content magT10">
        <div class="contenttit">
            <% if (ViewData["type"].ToString() == "activity")
               { %>
            <div style="padding-top: 20px; padding-left: 10px; float: left; width: 900px; text-align: center;">
                dll:<input type="text" id="dllname" name="dllname" style="width: 100px;" />
                spacename:<input type="text" id="spacename" name="spacename" />
                function:<input type="text" id="functionname" name="functionname" style="width: 80px;" />
                type:<input type="text" id="type" name="type" style="width: 100px;" />
                enable:<input type="radio" id="enable1" name="enable" checked="checked" value="true" />true
                <input type="radio" id="enable2" name="enable" value="false" />false
                <input type="button" value="添加" onclick="addactives()" />
            </div>
            <% } %>
            <% if (ViewData["type"].ToString() == "invitecode")
               { %>
            <form id="formInvitationCode">
            生成者编号：<input type="text" id="generatedMemberId" name="generatedMemberId" style="width: 120px;" />
            &nbsp;生成数：<input type="text" id="generateNumb" name="generateNumb" style="width: 50px;" />&nbsp;&nbsp;<input
                type="button" value="生成" onclick="generateInviteCodes()" />
            | 查看
            <select id="viewInvitationCodes" name="viewInvitationCodes" onchange="viewInviteCodes($(this).val())">
                <option value="2" selected="selected">查看所有</option>
                <option value="1">按已使用</option>
                <option value="0">按未使用</option>
            </select>
            </form>
            <% } %>
        </div>
        <% if (ViewData["type"].ToString() == "activity")
           { %>
        <div class="showmessage">
            <div class="dll">
                dll</div>
            <div class="spacename">
                spacename</div>
            <div class="function">
                function</div>
            <div class="type">
                type</div>
            <div class="enable">
                enable</div>
            <div class="del">
                删除</div>
            <div class="update">
                开关</div>
            <div id="showdiv">
                <%foreach (var obj in Model.actives)
                  { %>
                <div class="dll">
                    <%=obj.Dll %></div>
                <div class="spacename">
                    <%=obj.SpaceName %></div>
                <div class="function">
                    <%=obj.FuncTion %></div>
                <div class="type">
                    <%=obj.Type %></div>
                <div class="enable">
                    <%=obj.Enable %></div>
                <div class="del">
                    <a onclick="delactives('<%=obj.ID %>')">删除</a></div>
                <div class="update">
                    <a onclick="upactives('<%=obj.ID %>','<%=obj.Enable %>')">开/关</a></div>
                <%} %>
            </div>
        </div>
        <% } %>
        <div style="padding-bottom: 20px; clear: both; float: none;">
            <ul class="iwantlist magT10" id="listContiner" style="width: 900px;">
            </ul>
        </div>
        <br />
        <br />
        <div class="clearfax">
        </div>
        <div style="width: 900px; padding-bottom: 30px; text-align: center; margin-top: 20px;
            clear: both; float: none;">
            <center>
                <div id="pager" class="verifyPager">
                </div>
            </center>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/scripts/jquery.form.js" type="text/javascript"></script>
    <%--<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>--%>
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/admin.js" type="text/javascript"></script>
    <script type="text/javascript">
    uploadpath = '<%=ViewData["uploadpath"] %>';
    var pageNo = 1;
    var pageCount = 0;
    var pageSize = 25;
    var pageCount2 = 0;
    var mypagecount = 0;
    var usedFlag = 2;

    $(document).ready(function(){
        var category = "<%=ViewData["type"].ToString() %>";
        switch(category.toLowerCase()) {
            case "invitecode": viewInviteCodes(usedFlag);
                break;
            default: 
                break;
        }
    });
    function viewInviteCodes(usedFlag2)
    {
        usedFlag = usedFlag2;
        getContentCount("inviteCode", usedFlag);
        bindInviteCodes(usedFlag); 
    }
    function addactives() {
        var dllname = $("#dllname").val();
        var spacename = $("#spacename").val();
        var functionname = $("#functionname").val();
        var type = $("#type").val();
        var enable = $(":radio:checked").val();
        ActivesProvider.addactive(dllname, spacename, functionname, type, enable, function (date) {
            var objs = $.parseJSON(date);
            var actives = objs.actives;
            var str = bindactive(actives);
            $("#showdiv").html(str);
        });
    }
    function delactives(id) {
        ActivesProvider.delactive(id, function (date) {
            var objs = $.parseJSON(date);
            var actives = objs.actives;
            var str = bindactive(actives);
            $("#showdiv").html(str);
        });
    }
    function upactives(id, enable) {
        if (enable == "true" || enable == "True") {
            ActivesProvider.upactive(id, false, function (date) {
                var objs = $.parseJSON(date);
                var actives = objs.actives;
                var str = bindactive(actives);
                $("#showdiv").html(str);
            });
        }
        else if (enable == "false" || enable == "False") {
            ActivesProvider.upactive(id, true, function (date) {
                var objs = $.parseJSON(date);
                var actives = objs.actives;
                var str = bindactive(actives);
                $("#showdiv").html(str);
            });
        }
    }
    function bindactive(actives) {
        var str = "";
        $.each(actives, function (i) {
            str += "<div class=\"dll\">" + actives[i].Dll + "</div><div class=\"spacename\">" + actives[i].SpaceName + "</div><div class=\"function\">" + actives[i].FuncTion + "</div><div class=\"type\">" + actives[i].Type + "</div><div class=\"enable\">" + actives[i].Enable + "</div><div class=\"del\"><a onclick=\"delactives('" + actives[i].ID + "')\">删除</a></div><div class=\"update\"><a onclick=\"upactives('" + actives[i].ID + "','" + actives[i].Enable + "')\">开/关</a></div>";
        });
        return str;
    }
    function bindInviteCodes(usedFlag) 
    {
        adminActivityDataProvider.getInviteCodes(usedFlag, pageSize, pageNo, function (data) {
            var objs = $.parseJSON(data);
            var str = "";
            var generatedMember = null;
            var usedMember = null;

            $.each(objs, function(i) {
                generatedMember = objs[i].GeneratedMember != null && objs[i].GeneratedMember != undefined ? (objs[i].GeneratedMember.UniqueNumber != null ? objs[i].GeneratedMember.UniqueNumber : null) : null;
                usedMember = objs[i].UsedMember != null && objs[i].UsedMember != undefined ? (objs[i].UsedMember.UniqueNumber != null ? objs[i].UsedMember.UniqueNumber : null) : null;

                generatedMember = generatedMember != null ? objs[i].GeneratedMember.UniqueNumber.ConvertedID : "";
                usedMember = usedMember != null ? objs[i].UsedMember.UniqueNumber.ConvertedID : "";

                str += "<li class='iwantlistart'><span class='markicon'>邀请码：" + objs[i].InviteCode + "</span>";
                str += "<span>创建时间：" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM') + "</span>";
                str += "<span>生成者：" + generatedMember + "</span>";
                str += "<span>生成者编号：" + objs[i].GeneratedMemberId + "</span>";
                str += "<span>是否使用：" + (objs[i].UsedFlag == 0 ? "未使用" : "已使用") + "</span>";
                str += "<span>使用者：" + usedMember + "</span></li>";
            });
            if (str != "")
                $("#listContiner").html(str);
            else
                $("#listContiner").html("没有数据！");
        });
    }
    function generateInviteCodes()
    {
        var generateNumb = $("input#generateNumb").val();
        var generatedMemberId = $("input#generatedMemberId").val();
        if(generatedMemberId != null && generatedMemberId != "")
        {
            if(generateNumb != "0" && generateNumb != "" && generateNumb != null)
            {
                if(!isNaN(generateNumb))
                {
                    if(parseInt(generateNumb) <= 100) 
                    {
                        if(confirm("你确定要生成" + generateNumb + "个邀请码？"))
                        {
                            adminActivityDataProvider.generateInviteCodes(generateNumb, generatedMemberId, function (data) {
                                var obj = $.parseJSON(data);
                                if(obj.ok)
                                {
                                    $.jBox.tip("成功生成" + generateNumb + "个邀请码！", 'success');
                                    viewInviteCodes();
                                    $("input#generateNumb").val("");
                                }
                                else
                                    $.jBox.tip(obj.err, 'error');
                            });
                        }
                    }
                    else
                        $.jBox.tip("最多只能生成100个邀请码！", 'error');
                }
                else
                    $.jBox.tip("生成数必须是数字！", 'error');
            }
            else
                $.jBox.tip("生成数不能为空且不能为0！", 'error');
        }
        else
            $.jBox.tip("生成者编号不能为空！", 'error');  
    }

    function getContentCount(type, usedFlag)
    {
        $.ajaxSetup({ async: false });
        switch (type) {
            case "inviteCode":
                adminActivityDataProvider.getInviteCodesCount(usedFlag, function (data) { 
                    pageCount = $.parseJSON(data);
                });
                break;
            default: 
                break;
        }
        $.ajaxSetup({ async: true });

        pageCount2 = parseInt((parseInt(pageCount) + parseInt(pageSize) - 1) / parseInt(pageSize));
        mypagecount = pageCount2;
        if (pageNo > mypagecount) {
            pageNo = mypagecount;
        }
        setPager(type);
    }

    allInviteCodeClick = function (pageclickednumber) {
        if (pageclickednumber >= mypagecount) { pageNo = mypagecount; }
        else { pageNo = pageclickednumber; }
        $("#pager").pager({ pagenumber: pageNo, pagecount: mypagecount, buttonClickCallback: allInviteCodeClick });
        viewInviteCodes(usedFlag);
    }

    function setPager(type) {
        switch (type) {
            case "inviteCode":
                $("#pager").pager({ pagenumber: pageNo, pagecount: mypagecount, buttonClickCallback: allInviteCodeClick });
                break;
            default: 
                break;
        }
    }

    </script>
</asp:Content>
