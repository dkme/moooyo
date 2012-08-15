<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberInterestModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
米果不够，无法创建兴趣群组
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 内容-->
     <div class="container">
	    <div class="box_demo1 w680 fb_left">
		    <div class="mt10"></div>
		    <div class="Set_title1 border_b1 p40"> <img src="/pics/add_xiqu.gif" width="51" height="51" /><span style="font-size:22px">创建兴趣群组  <font class="cgray" >（拍了照片，抓紧秀出来）</font></span> </div>
            <div class="Set_box1 p40" style="padding:40px;padding-top:20px;">
                <div class="mt10"></div>
			      <div class="fb_box_com w600">
				   <span class="add_xiqu"></span>
				  </div>
                 <div class="fb_box_com w600">
                     <div class="noenough">
                        <span><img src="/pics/Not_enough.gif" width="600" /></span>
                        <span class="need_inter"><h2><%=Comm.getInterestInsertToPoints()[Model.interestlist.Count] %></h2>
                        </span>
                        <span class="self_inter"><font> 你的米果</font>
                        <h3><%=Model.Member.Points %></h3></span>
                     </div>
                 </div>
                 <div class="fb_box_com w600">
                     <span class="inter_msg">oh，创建第<%=Model.interestlist.Count + 1 %>个兴趣组，你需要积累<%=Comm.getInterestInsertToPoints()[Model.interestlist.Count] %>个米果<a class="com_back blue02" href="/Content/IndexContent">返回</a></span>              <span><a href="/Content/AboutEarnPoints" class="blue02">如何积累米果？</a></span>
                 </div>
                 <div class="padding_b50"></div>
			</div>
        </div>
	 </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/fabu.css"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
<script type="text/javascript" src="/Scripts/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-2.3.min.js"></script>
<script type="text/javascript" src="/scripts/jquery.jBox-zh-CN.js"></script>
<script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
<!--[if IE 6]>
    <script type="text/javascript" src="/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    DD_belatedPNG.fix('em,.txtput,textarea3');
	 </script>
<![endif]-->
<script type="text/javascript">
    $(document).ready(function () {
        $("#warp").css("background", "#dedee0");
    });
</script>
</asp:Content>
