<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LoginIndexContent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="Msgbox1" data-ifopen="false" style="position:absolute; left:0px; top::0px; display:none; background-color:#FFF; border:0; z-index:500;">
<div class="pup_upbox boxg">
    <div class="top_arrow arrow_left"><div class="top_al g"></div><div class="arrowbg g"></div></div>
    <div class="pup_content">
       <div class="pup_com"><span class="pup_l"><img src="/pics/huiying.gif" /></span><span class="pup_r">已经帮你告诉Ta了，再给Ta说点啥吧</span></div>
       <div class="pup_com"><textarea class="pup_txta g" onkeyup="textareasize(this)"></textarea></div>
       <div class="pup_com"><span class="pup_send"><a href="#">发送</a></span></div>
     
    </div>
  </div>
</div>
<div id="Msgbox2" data-ifopen="false" style="position:absolute; left:0px; top::0px; display:none; background-color:#FFF; border:0; z-index:500;">
<div class="pup_upbox boxg">
    <div class="top_arrow arrow_right"><div class="top_al g"></div><div class="arrowbg g"></div></div>
    <div class="pup_content">
        <div class="pup_com"><input type="text" class="pup_txt g" name=""/></div>
       <div class="pup_com"><span class="puplist"><a href="#"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="#" target="_blank" class="blue02">安七</a><font>郭美美</font></span></div>
       <div class="pup_com"><span class="puplist"><a href="#"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="#" target="_blank" class="blue02">安七</a><font>郭美美</font></span></div>
       <div class="pup_com"><span class="puplist"><a href="#"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="#" target="_blank" class="blue02">安七</a><font>郭美美</font></span></div>
       <div class="pup_com"><span class="puplist"><a href="#"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="#" target="_blank" class="blue02">安七</a><font>郭美美</font></span></div>
       <div class="pup_com"><span class="pup_more"><a href="#">更多回应</a></span></div>
    </div>
  </div>
</div>

  <div id="wrap">
    <div class="login_pic"><div>
      <div class="login_pic">
        <div><img src="/pics/logopic.gif" alt="米柚网" width="970" /></div>
        <div class="login_close"><img src="/pics/login_close.gif" title="关闭" /></div>
      </div>
    </div>
    </div>
    <div class="mt15 boxbg"></div>
    <div class="mainppt h357">
        <div class="ifocus_btn" style="left:0px; top:0px; width:500px;">
            <div class="nav_right" style="width:auto; float:left; text-align:left; padding-left:20px;">
            <%if (Model.imagePush != null)
                { %><span><a href="/Content/TaContent/<%=Model.imagePush.MemberID %>/all/1" onclick="clickImagePush('<%=Model.imagePush.ID %>')" style="color:#FFF;" target="_blank">独自一个人的旅行，我的Ta在哪里？</a></span><span class="huise" style="color:#FFF;">by <%=Model.imagePush.Creater.NickName%></span><%}%>%>
            </div>
        </div>
      <div class="pptpic"><img src="/pics/ppt.gif" height="357" width="970" /></div>
      <div class="login_main">
	     <div class="login_com"><div class="log_l"><a href="#"><img src="/pics/index_reg.gif" width="140" height="54" /></a></div><div class="log_r"><font>第三方账号</font><span class="other_name"><a href="#"><img src="/pics/demo14.png" height="24" width="25"/></a><a href="#"><img src="/pics/demo15.png" height="24" width="25"/></a></span></div></div>
          <div class="mt10"></div>
		  <div class="login_com"><input type="text"  class="txtput" id="email" name="email" /></div>
		  <div class="login_com"><input type="text"  class="txtput" id="pwd" name="pwd" /></div>
		  <div class="login_com"><input type="button" class="login_btn " value="登入" tabindex="4" /></div>
		  <div class="login_com"><span> <input type="checkbox" class="chekbox" />  14天内访问自动登录</span></div>
	  </div>
    </div>
<div class="mainppt h50 boxbg">
    <div class="jump_share"><span class="main_title" >大家正在分享</span></div>
    <div style="padding:10px 0; height:30px;">
        <div class="jump_list">
            <a href="/Content/AddImageContent" target="_blank"><em class="me1"></em></a>
            <a href="/Content/AddSuiSuiNianContent" target="_blank"><em class="me2"></em></a>
            <a href="/Content/AddIWantContent" target="_blank"><em class="me3"></em></a>
            <a href="/Content/AddMoodContent" target="_blank"><em class="me4"></em></a>
            <a href="/Content/AddInterViewContent" target="_blank"><em class="me5"></em></a>
            <a href="/Content/AddCallForContent" target="_blank"><em class="me6"></em></a>
        </div>
    </div>
</div>
    <div class="mainppt h266 boxbg hoverpic">
       <div class="happy_l"><a href="#"><img src="/pics/special01.gif" title="" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a></div>
       <div class="happy_z"><a href="#"><img src="/pics/special02.gif" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a><a href="#"><img src="/pics/special03.gif" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a><a href="#"><img src="/pics/special04.gif" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a><a href="#"><img src="/pics/special04.gif" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a><a href="#"><img src="/pics/special03.gif" /></a><a href="#"><img src="/pics/special02.gif" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a></div>
       <div class="happy_r"><a href="#"><img src="/pics/special05.gif" title="" /><div class="info_title">@ 柚子：下午的植物园</div><div class="time_set">今天</div></a></div>
    </div>
    <!--start 中间内容部分 -->
<div class="mainbox">
  <!--<div style="position:absolute; display:block; top:241px; width:200px; height:200px; left:20; z-index:500; background:#060; color:#fff;">vv</div>-->
       <div class="time_statebox1 h32"><span class="main_title">快乐单身时光</span></div>
    <div class="time_statebox1 h50"><div class="time_state_i"><span><em>2分钟前</em></span> </div></div>
       <!-- 中间内容通用部分 style two -->
      <div class="boxindex h250">
        <!-- 两个圆角矩形STYLE通用部分 -->
        <div class="box_demo w412"> 
		   <!-- <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b> -->
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info">
                <div class="info_left">
                  <h3 class="h3 hleft"><img src="/pics/left_marks.gif" /></h3>
                  <span class="h80">什么嘛，迟到一下会死啊！骂我那么久，内分泌失调啊！！！</span>
                  <h3 class="h3 hright"><img src="/pics/right_marks.gif" /></h3>
                <span class="red1" >8人安慰<em onClick="showlike($(this),1)">+1</em></span> <span class="h25"><a href="javascript:void(0)" onclick="showlike($(this),1)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a onclick="showlike($(this),1)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a ><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a onclick="showlike($(this),1)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a onclick="showlike($(this),1)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a></span> </div>
                <div class="info_right"><a href="#"><img src="/pics/MM01.gif" /></a></div>
              </div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
			</div>
			<div class="rbottom"></div>
         <!--  <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b> -->
	    </div>
        <div class="w12"></div>
        <!-- 一个圆角矩形STYLE通用部分 -->
        <div class="box_demo w412">
            <div class="box_content">
              <div class="box_top">
                <div class="contetn_l"><span><font class="blue">野百合</font></span><span>心情不好</span><span class="userpic"><a href="#"><img src="/pics/MM04.gif" /></a></span></div>
                <div class="content_z"><a href="#"><img src="/pics/MM03.gif" /></a></div>
                <div class="content_r"> <a href="#" class="mb12"><img src="/pics/MM02.gif" height="88" width="115px" /></a> <a href="#" ><img src="/pics/MM02.gif" height="88" width="115px" /></a> </div>
              </div>
              <div style="clear:both"></div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
            </div>
			<div class="rbottom"></div>
        </div>
      </div>
      
       <div class="time_statebox1 h50"><div class="time_state_i"><span><em>2分钟前</em></span></div></div>
      <!-- 中间内容通用部分 style three -->
      <div class="boxindex h250">
        <!-- 一个圆角矩形STYLE通用部分 -->
        <div class="box_demo w201"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info"> <a href="#"><img src="/pics/MM01.gif" /></a></div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
            </div>
		   <div class="rbottom"></div>
        </div>
        <div class="w12"></div>
        <!-- 两个圆角矩形STYLE通用部分 -->
        <div class="box_demo w623"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font></div>
              <div class="box_info">
                <div class="contetn_l"><span>心情不好</span><span class="userpic"><a href="#"><img src="/pics/MM04.gif" /></a></span></div>
                <div class="content_com"> <span>小编：如果只要你一出门就有瘸子在你附近走，而且周围全是这样的人，都是这样的。你时间长了也会瘸吗 ？</span> <span><font class="blue1" >Ta：10岁了还尿床！</font></span> <span class="spanbottom"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
                <div class="content_com"> <span>小编：如果只要你一出门就有瘸子在你附近走，而且周围全是这样的人，都是这样的。你时间长了也会瘸吗 ？</span> <span><font class="blue1" >Ta：10岁了还尿床！</font></span> <span class="spanbottom"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
                <div class="content_com"> <span>小编：如果只要你一出门就有瘸子在你附近走，而且周围全是这样的人，都是这样的。你时间长了也会瘸吗 ？</span> <span><font class="blue1" >Ta：10岁了还尿床！</font></span> <span class="spanbottom"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
              </div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"></span></div>
            </div>
			<div class="rbottom"></div>
        </div>
    </div>
     
	 
	   <div class="time_statebox1 h50"><div class="time_state_i"><span><em>2分钟前</em></span></div></div>
      
      <!-- 中间内容通用部分 style  one<div class="time_statebox"></div> -->
      <div class="boxindex h250">
        <!-- 两个圆角矩形STYLE通用部分 -->
        <div class="box_demo w412"> 
		   <!-- <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b> -->
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info">
                <div class="info_left">
                  <h3 class="h3 hleft"><img src="/pics/left_marks.gif" /></h3>
                  <span class="h80">什么嘛，迟到一下会死啊！骂我那么久，内分泌失调啊！！！</span>
                  <h3 class="h3 hright"><img src="/pics/right_marks.gif" /></h3>
                  <span class="red1">8人安慰<em onClick="showlike($(this),1)">+1</em></span> <span class="h25"><a href="javascript:void(0)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="javascript:void(0)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="javascript:void(0)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="javascript:void(0)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a><a href="javascript:void(0)"><img src="/pics/Fans_other01.png" height="25" width="25" /></a></span> </div>
                <div class="info_right"><a href="#"><img src="/pics/MM01.gif" /></a></div>
              </div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
			</div>
			<div class="rbottom"></div>
         <!--  <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b> -->
	    </div>
        <div class="w12"></div>
        <!-- 一个圆角矩形STYLE通用部分 -->
        <div class="box_demo w201"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info"><a href="#"><img src="/pics/MM01.gif" /></a> </div>
              <div class="box_bottom"><span class="bottom_left">距离你 0.5 km</span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
            </div> 
			<div class="rbottom"></div>
		</div>
        <div class="w12"></div>
        <!-- 一个圆角矩形STYLE通用部分 -->
        <div class="box_demo w201"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info"><a href="#"><img src="/pics/MM01.gif" /></a> </div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
            </div> 
			<div class="rbottom"></div>
	    </div>
      </div>
	   
	   <!-- 两变  一个一个小BOX style two -->
	   <div class="boxindex h250">
         <!-- 一个圆角矩形STYLE通用部分 -->
          <div class="box_demo w201"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info"> <a href="#"><img src="/pics/MM01.gif" /></a></div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
            </div>
		   <div class="rbottom"></div>
        </div>
	     <div class="w12"></div>
	     <!-- 两个圆角矩形STYLE通用部分 -->
         <div class="box_demo w412">
           <div class="box_content">
             <div class="box_top">
               <div class="contetn_l"><span><font class="blue">野百合</font></span><span>心情不好</span><span class="userpic"><a href="#"><img src="/pics/MM04.gif" /></a></span></div>
               <div class="content_z"><a href="#"><img src="/pics/MM03.gif" /></a></div>
               <div class="content_r"> <a href="#" class="mb12"><img src="/pics/MM02.gif" height="88" width="115px" /></a> <a href="#" ><img src="/pics/MM02.gif" height="88" width="115px" /></a> </div>
             </div>
             <div style="clear:both"></div>
             <div class="box_bottom"><span class="bottom_left"></span> <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span> </div>
           </div>
           <div class="rbottom"></div>
         </div>
		 <div class="w12"></div>
		   <!-- 一个圆角矩形STYLE通用部分 -->
          <div class="box_demo w201"> 
            <div class="box_content">
              <div class="box_title"><font class="blue">野百合</font>心情不好</div>
              <div class="box_info"> <a href="#"><img src="/pics/MM01.gif" /></a></div>
              <div class="box_bottom"><span class="bottom_left"></span>
			  <span class="bottom_right"><b class="love_hit">18</b><b class="msg_hit" onMouseOver="showlike($(this),2)">25</b></span></div>
            </div>
		   <div class="rbottom"></div>
        </div>
    </div>
	   <!-- 地底部加载 style four -->

      <div class="boxindex  h110" ><span class="box_footer"></span></div>
    </div>
    <!--end中间内容部分 -->
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<link rel="stylesheet" type="text/css" href="/css/style.css"/>
<link rel="stylesheet" type="text/css" href="/css/msg.css"/>
<style type="text/css">
.contetn_l{ width:84px; padding:10px; margin-right:10px; float:left; height: 150px; position:relative; text-align:center; color:#d6d6d6}
.mainppt .happy_l ,.mainppt .happy_r{ width:290px; height:265px; float:left; position:relative;}
.hoverpic a { display:block; position:relative;}
.info_title ,.time_set{ line-height:20px; color:#fff; font-size:12px; font-family:"\5FAE\8F6F\96C5\9ED1"; position:absolute; z-index:2}
.info_title { top:10px; left:5px; visibility:hidden;}
.time_set {bottom:10px; left:5px;visibility:hidden; padding-left:20px; background:url(/pics/time_pic.png) left center no-repeat;}
</style>

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
    DD_belatedPNG.fix('em');
	 </script>
<![endif]-->
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('.hoverpic').find('a').hover(function () {
            $(this).css({
                "background": "#000",
                "opacity": "0.5"
            });
            $(this).children('div').css({ "visibility": "visible" });

        }, function () {

            $(this).css({
                "background": "none",
                "opacity": "1"
            });
            $(this).children("div").css({ "visibility": "hidden" });
        });


        $('.info_left').find('.red1').children('em').hover(function () {
            $(this).css({ "background-position": "-756px 0" });
        }, function () {

            $(this).css({ "background-position": "-728px 0" });
        });

        $('.bottom_right').find('.love_hit').hover(function () {
            $(this).css({ "background-position": "0 0" });
        }, function () {
            $(this).css({ "background-position": "0 -30px" });
        });
        $('.bottom_right').find('.msg_hit').hover(function () {
            $(this).css({ "background-position": "0 -15px" });
        }, function () {
            $(this).css({ "background-position": "0 -45px" });
        });
        $('.red1').hover(function () {
            $(this).css({ "color": "#b40001" });
        }, function () {
            $(this).css({ "color": "#d5836f" });
        });
        $('.login_close').click(function () {
            $('.login_pic').slideUp(1000);
        });

    });

    function textareasize(obj) {
        if (obj.scrollHeight > 30) {
            obj.style.height = obj.scrollHeight + 'px';
        }
    }

    //提示框脚本
    var msgobj = null;
    function hidelike(str) {
        str.attr("data-ifopen", "false");
        str.slideUp(0);
    }
    function showlike(obj, type) {
        msgobj = obj;
        if (type == 1) {
            var msgdiv = $("#Msgbox1");
            msgdiv.css("left", msgobj.offset().left - 20 + "px");
            msgdiv.css("top", msgobj.offset().top + msgobj.height() + 10 + "px");
            if (msgdiv.attr("data-ifopen") == "false") {
                var Msgbox2 = $("#Msgbox2");
                Msgbox2.attr("data-ifopen", "false");
                Msgbox2.slideUp(0);
                msgdiv.attr("data-ifopen", "true");
                msgdiv.slideDown(500);
            }
            else {
                msgdiv.attr("data-ifopen", "false");
                msgdiv.slideUp(0);
            }
        } else if (type == 2) {
            var msgdiv = $("#Msgbox2");
            msgdiv.css("left", msgobj.offset().left - 148 + "px");
            msgdiv.css("top", msgobj.offset().top + msgobj.height() + 10 + "px");
            if (msgdiv.attr("data-ifopen") == "false") {
                var Msgbox1 = $("#Msgbox1");
                Msgbox1.attr("data-ifopen", "false");
                Msgbox1.slideUp(0);
                msgdiv.attr("data-ifopen", "true");
                msgdiv.slideDown(500);
            }
            else {
                msgdiv.attr("data-ifopen", "false");
                msgdiv.slideUp(0);
            }
        }
    }
    $(window).resize(function () {
        if (msgobj != null) {
            var Msgbox1 = $("#Msgbox1");
            Msgbox1.css("left", msgobj.offset().left + "px");
            Msgbox1.css("top", msgobj.offset().top + msgobj.height() + 10 + "px");
            var Msgbox2 = $("#Msgbox2");
            Msgbox2.css("left", msgobj.offset().left + "px");
            Msgbox2.css("top", msgobj.offset().top + msgobj.height() + 10 + "px");
        }
    });
</script>
</asp:Content>
