<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" ContentType="text/html" ResponseEncoding="gb2312" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
用户管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.RenderPartial("~/Views/Admin/AdminTopNav.ascx");%>
<div>
性别　<select id="sex" value="0">
						<option value="0">请选择</option>
                        <option value="1">男</option>
                        <option value="2">女</option>
					</select>
注册　<select id="regtype" value="-1">
						<option value="-1">请选择</option>
                        <option value="1">已完成注册</option>
                        <option value="0">未完成注册</option>
					</select>
屏蔽　<select id="bandtype" value="-1">
						<option value="-1">请选择</option>
                        <option value="1">未屏蔽</option>
                        <option value="0">已屏蔽</option>
					</select>
照片　<select id="phototype" value="-1">
						<option value="-1">请选择</option>
                        <option value="1">有照片</option>
                        <option value="0">无照片</option>
					</select>
ID　<input id="id" type="text" value=""/><input type="button" onclick="search()" value="查找" /> 共<%=ViewData["membercount"] %>个
</div>
<TABLE id="listcontiner" border="0" cellpadding="2" cellspacing="2"></TABLE>
<div id="pagger">
<% if ((int)ViewData["pagecount"] > 1)
   {%> 
   <% Html.RenderAction("pagger", "Shared", new { nowpage = ViewData["pageno"], pagecount = ViewData["pagecount"],additionID="", url = "/Admin/MemberManager" });%> 
<%} %></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
<link href="/css/main_<%=ViewData["cssversion"] %>.css" rel="stylesheet" type="text/css" />
<script src="/js/admin.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
uploadpath = '<%=ViewData["uploadpath"] %>';
var members = <%=ViewData["members"] %>;
var membercount = <%=ViewData["membercount"] %>;
var pagesize = 30;
var nowpage = <%=ViewData["pageno"] %>;
$(document).ready(function(){
    bindmembers();
    bindpagger("?province=&city=&sex=0&beenband=-1&finishedreg=-1&hasphoto=-1&id=");
});
function bindmembers()
{
    var str = "<tr>";
    //str += "<td>全选<input type='checkbox' id='chkall' onclick='selectall()'/></td>";
    str += "<td>ID</td>";
    str += "<td>状态</td>";
    str += "<td>用户级别</td>";
    str += "<td>头像</td>";
    str += "<td>姓名</td>";
    str += "<td>性别</td>";
    str += "<td>年龄</td>";
    str += "<td>城市</td>";
    str += "<td>注册时间</td>";
    str += "<td>登录次数</td>";
    str += "<td>上次登录时间</td>";
    str += "<td>最后登录IP</td>";
    str += "<td>操作</td>";
    str += "</tr>";

    $.each(members,function(i){
        str += bindmember(members[i]);
    });
    str += "";
    $("#listcontiner").html(str);
}
function selectall()
{
    $("input[name='chk']").each(function(){
        $(this).attr("checked",$("#chkall").attr("checked"));
    });
}
function search()
{
    var sex = $("#sex").val();
    var beenband = $("#bandtype").val();
    var regtype = $("#regtype").val();
    var phototype = $("#phototype").val();
    var id = $("#id").val();

    var condi = "?province=&city=&sex="+sex+"&beenband="+beenband+"&finishedreg="+regtype+"&hasphoto="+phototype+"&id="+id;
    window.location = "/admin/MemberManager"+condi;
}
function bindpagger(condi)
{
    $("#pagger A").each(function(){
        $(this).attr("href","/admin/MemberManager/"+$(this).attr("data-i")+"/"+condi);
    });
}
function bindmember(obj){
    if(obj.MemberInfomation==null) return "";
    var str = "<TR>";
    //str += "<td><input type='checkbox' name='chk' ID='" + obj.ID + "'/></td>";
    str += "<td>" + obj.ID + "</td>";
    var alogin = obj.AllowLogin?"正常":"屏蔽";
    str += "<td>"+alogin+"</td>";
    var mtype = (obj.MemberType==0)?"普通":"正式";
    if (obj.MemberType==2) mtype="VIP";
    str += "<td>"+mtype+"</td>";
    str += "<td><a href='/Content/IContent/" + obj.ID + "' target='_blank'><img width='50px' src='" + photofunctions.getprofileiconphotoname(obj.MemberInfomation.IconPath) + "' onerror='$(this).attr(\"src\",\"/pics/noicon.jpg\");'/></a></td>";
    str += "<td><span class='nickname'>" + obj.MemberInfomation.NickName + "</span></td>";
    str += "<td><span class='nickname'>" + obj.Sex + "</span></td>";
    str += "<td><span class='nickname'>" + getAge(paserJsonDate(obj.MemberInfomation.Birthday)) + "</span></td>";
    str += "<td><span class='nickname'>" + obj.MemberInfomation.City + "</span></td>";
    str += "<td><span class='nickname'>" + paserJsonDate(obj.CreatedTime).format("yyyy.m.d h:MM:ss"); + "</span></td>";
    str += "<td><span class='nickname'>" + obj.Status.LoginTimes + "</span></td>";
    str += "<td><span class='nickname'>" + paserJsonDate(obj.LastOperationTime).format("yyyy.m.d h:MM:ss"); + "</span></td>";
    str += "<td><span class='nickname'>" + obj.LastLoginIP + "</span></td>";
    str += "<td><a href='javascript:' onclick='band(\""+obj.ID+"\")'>屏蔽</a> <a href='javascript:' onclick='unband(\""+obj.ID+"\")'>不再屏蔽</a> <a href='javascript:' onclick='upgrade(\""+obj.ID+"\")'>升级</a> <a href='javascript:' onclick='downgrade(\""+obj.ID+"\")'>降级</a></td>";
    str += "</tr>";
    return str;
}
function upgrade(id)
{
    var str = "";
    var nowlevel = 0;
    $.each(members,function(i){
        if (members[i].ID == id)
        {
            nowlevel = members[i].MemberType;
            str = members[i].MemberType==0?"正式用户":"VIP用户";
        }
    });
    if (nowlevel==2) {alert("他已经是VIP用户，不能再升级");return;}
    nowlevel ++;
    if (confirm("将他升级到"+str+"？"))
    {
        AdminProvider.setmembertype(id,nowlevel,function(){
            $.each(members,function(i){
                if (members[i].ID == id)
                    members[i].MemberType=nowlevel;
            });
            bindmembers();
        });
    }
}
function downgrade(id)
{
    var str = "";
    var nowlevel = 0;
    $.each(members,function(i){
        if (members[i].ID == id)
        {
            nowlevel = members[i].MemberType;
            str = members[i].MemberType==2?"正式用户":"普通用户";
        }
    });
    if (nowlevel==0) {alert("他已经是普通用户，不能再降级");return;}
    nowlevel --;
    if (confirm("将他降级到"+str+"？"))
    {
        AdminProvider.setmembertype(id,nowlevel,function(){
            $.each(members,function(i){
                if (members[i].ID == id)
                    members[i].MemberType=nowlevel;
            });
            bindmembers();
        });
    }
}
function band(id)
{
    if (confirm("屏蔽他？"))
    {
        AdminProvider.setAllowLogin(id,false,function(){
            $.each(members,function(i){
                if (members[i].ID == id)
                    members[i].AllowLogin=false;
            });
            bindmembers();
        });
    }
}
function unband(id)
{
    if (confirm("不再屏蔽他？"))
    {
        AdminProvider.setAllowLogin(id,true,function(){
            $.each(members,function(i){
                if (members[i].ID == id)
                {
                    members[i].AllowLogin=true;
                }
            });
            bindmembers();
        });
    }
}
</script>
</asp:Content>
