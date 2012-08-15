<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
我创建的兴趣群组
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- 内容-->
<div class="container">
	    <div class="box_demo1 w960" style="background:#fff">
		    <div class="mt10"></div>
            <div style="height:20px;"></div>
		    <div class="Set_title2 admin_b  p40"><img src="/pics/self_bg.png" width="34" height="23" /><span style=" font-family:'\5FAE\8F6F\96C5\9ED1; ">我创建的兴趣群组  <font class="cgray"></font></span> <a class="com_back blue02" href="/InterestCenter/AddInterestFans">返回</a></div>
            <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                <div class="mt10"></div>
			      <div class="admin_box ">
                    <%foreach (var obj in Model.interestlist)
                        {
                            %>
                       <div class="admin_group">
                         <div class="admin_group_l"><img src="<%=Comm.getImagePath(obj.ICONPath,ImageType.Middle) %>" height="152" width="152" title="某某群组" /></div>
                         <div class="admin_group_r">
                             <span class="g_name" style="height:20px;">
                                 <font><%=obj.Title %></font>
                                 <%if (obj.Classes != null && obj.Classes != "")
                                   {
                                       foreach (string classstr in obj.Classes.Split(','))
                                       {
                                           %><a class="gray01"><%=classstr %></a><%
                                       }
                                   } %>
                             </span>
                             <span class="g_disc"><%=obj.Content %></span>
                             <span class="g_edit"><a class="fans_hit" href="/InterestCenter/UpdateInterestFansList/<%=obj.ID %>" target="_blank"><%=obj.FansCount %></a><a href="/InterestCenter/UpdateInterest/<%=obj.ID %>" title="修改"><img src="/pics/group_edit.gif" /></a><a title="分享"><img src="/pics/group_back.gif" data-oldobj="showInterest<%=obj.ID %>" onmouseover="shareClick('http://www.moooyo.com/InterestCenter/ShowInterest/<%=obj.ID %>','<%=obj.Title %>','<%=obj.Content.Replace("\n", "") %>',$(this),'http://www.moooyo.com<%=Comm.getImagePath(obj.ICONPath,ImageType.Middle) %>')"/></a></span>
                         </div>
                       </div>
                    <%}%>
				  </div>
                 <div class="padding_b50"></div>
			</div>
        </div>
	 </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
<style type="text/css">
.admin_group { width:790px; height:160px; padding:40px; border-bottom:1px dashed #ebebeb; overflow:hidden;}
.admin_group_l { width:160px; height:160px; float:left;}
.admin_group_r { width:604px; height:160px; padding-left:20px; float:left;}
.admin_group_r span { display:block;  padding-bottom:10px; text-align:left; font-family:"\5FAE\8F6F\96C5\9ED1";}
.admin_group_r span.g_name { line-height:25px;  }
.admin_group_r span.g_name font { color:#0099cc;  font-size:16px; padding-right:20px;}
.admin_group_r span.g_name a { display:inline; padding-right:10px; font-size:12px;}
.admin_group_r span.g_disc { color:#666; font-size:12px; line-height:20px; padding-right:20px;}
.admin_group_r span.g_edit { padding-top:10px; height:34px; line-height:34px;}

.admin_group_r span.g_edit a{ display:block; float:left; margin-left:3px; height:34px; line-height:34px;}
.admin_group_r span.g_edit a img{ vertical-align:top;}
.admin_group_r span.g_edit a.fans_hit { display:block;  font-size:16px; width:60px; margin-left:3px; float:left; text-align:center; background:#b40001; color:#fff; font-family:"arial";}
.parentframe { position:relative; z-index:9999px; background:#fff; padding-left:10px;}
.Framediv { background:#f3f3f3;float:left;position:relative; margin-right:20px; cursor:pointer;}
.childFrame { background:#ccc; float:left; position:absolute;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script type="text/javascript">
    $().ready(function () { 
        $("#wrap").css("background", "#DEDEE0");
    });
</script>
</asp:Content>
