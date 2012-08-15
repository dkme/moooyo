<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MsgsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
与 <%=Model.You== null?"":Model.You.Name %> 的米邮对话
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Letter_intro">
       <!--个人左面板-->
    <% if (Model.IsOwner) {%>
    <% Html.RenderPartial("~/Views/Member/ProfileLeftPanelHead.ascx");%>  
    <% }
       else { %>
        <% Html.RenderPartial("~/Views/Member/MemberLeftPanel.ascx");%>  
    <% } %>
    <!--endof 个人左面板-->
    </div>
       <div class="Letter_box ">
          <div class="Letter_title p30"><h2 style="float:left;">与 <%=Model.You== null?"":Model.You.Name %> 的米邮对话</h2><span style="float:right; margin-top:20px;"><a href="/Msg/MessagesList" class="blue02" style="color:#0099cc">返回米邮列表</a></span></div>
          <% if (Model.You != null)
             { %>
              <div class="mt15"></div>
             <div class="Letter_info ">
                     <div class="Letter_info_l"><a href="/Content/IContent/" target="_blank" title="<%=Model.Member.Name %>"><img src="<%= Model.Member.ICONPath %>" height="64" width="64" /></a></div>
                        <div class="Letter_info_c send01">
                           <div class="Lerter_in01">
                                <div class="Lerter_in02">
                                  <textarea class="Ler_txtarea" onkeyup="textareasize(this)" id="msgwriter"></textarea><em class="white_bg"></em></div>
                               </div>
                          </div>
                      <div class="Letter_info_r"></div>
            </div>
             <div class="Letter_info mb20"><span class="send_span"><input type="button" class="send_btn" name="Send_btn" value="" onclick="sendmsg()" /></span></div>
             <div id="MessageDetails" data-pagecounter="1">
             
            </div>
            <div class="Letter_info">
              <div class="loading" style="cursor:pointer; display:none;" onclick="nextPage()">
                 <b class="rtop"><b class="r1"></b><b class="r2"></b><b class="r3"></b><b class="r4"></b></b>
                 <div class="loaded"><a href="javascript:;">加载更多</a> </div>
                 <b class="rbottom"><b class="r4"></b><b class="r3"></b><b class="r2"></b><b class="r1"></b></b>
              </div>
              <div style="width:100%; text-align:center; display:none; line-height:30px;" id="paging"></div>
            </div>
            
          <div class="padding_b50"></div>
          <% } %>
        </div> 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/main_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var me = '<%=Model.UserID %>';
        var you = '<%=Model.You == null ? "" : Model.You.ID %>';
        var metype = '<%=Model.Member.MemberType %>';
        var metotalmsgcount = <%=Model.Member.Last24HOutCallsCount %>;
        var nomallevelmaxmsglimit=<%=Model.NomalLevelMaxSendMsgLimit %>;
        var hot = '<%=Model.Member.Hot %>';
        var myicon = '<%=Model.Member.ICONPath %>';
        var myname = '<%=Model.Member.Name %>';
        var youricon = '<%=Model.You==null?"":Model.You.ICONPath %>';
        var yourname = '<%=Model.You==null?"":Model.You.Name %>';
        var pageSize = <%=Model.Pagger.PageSize %>;
        var pageCount = <%=Model.Pagger.PageCount %>;
        var pageNo = <%=Model.Pagger.PageNo %>;
        var pageTotal = <%=Model.pageTotal %>;
        var pageCount2 = Math.ceil(pageTotal / 6);
        var pageSize2 = 6;
        var pageUrl = "/Msg/MessageDetails/" + you + "/";

        $().ready(function () {

            if(pageNo < 2)
                bindMsageList(you, pageSize2, 1);
            else {
                bindMsageList(you, pageSize, pageNo);
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                $("div#paging").css("display", "block");
            }

            if(pageCount2 > 1 && pageNo < 2) $("div.loading").css("display", "block");
        });
        function bindMsageList(you, pageSizeB, pageNoB) {
            var str = "";
            str = getMsgs(you, $("#MessageDetails"), pageSizeB, pageNoB);
            $("div#MessageDetails").html(str);

            //绑定会员标签
            //MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
        }
        function nextPage() {
            var pageNo2 = $("div#MessageDetails").attr("data-pagecounter");
            var str = "";
            if(pageNo2 < pageCount2 && pageNo2 <= 2 && pageNo < 2) {
                var newPageNo = Number($("div#MessageDetails").attr("data-pagecounter")) + 1;
                if(newPageNo >= pageCount2) { 
                    $("div.loading").css("display", "none");
                    $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                    $("div#paging").css("display", "block");
                }
                $("div#MessageDetails").attr("data-pagecounter", newPageNo);
                str = getMsgs(you, $("#MessageDetails"), pageSize2, newPageNo);
                
                $("div#MessageDetails").html($("div#MessageDetails").html() + str);

                //绑定会员标签
                MemberInfoCenter.BindDataInfo($("#activityMemberInfo [name='activityMemberInfoArea']"));
            }
            else { 
                $("div.loading").css("display", "none");
                $("div#paging").html(profileQueryStrPaging(pageNo, pageCount, pageUrl).toString());
                $("div#paging").css("display", "block");
            }
        }
        function textareasize(obj) {
            if (obj.scrollHeight > 70) {
                obj.style.height = obj.scrollHeight + 'px';
            }
        }
        function sendmsg() {
            var msg = $("#msgwriter").val();
            if (trim(msg) == "") { 
                $("#msgwriter").focus(); 
                return; 
            }
            if (!checkLen(msg)) return;

            if (metype == "Level0" & metotalmsgcount > nomallevelmaxmsglimit) {
                actionprovider.openup();
            }

            if (metype == "Level0" & hot != "normal") {
                actionprovider.openup();
            }

            MemberLinkProvider.talk(you, msg, function () {
                $("#msgwriter").val("");
                bindMsageList(you, pageSize, pageNo)
            });
        };
        function getMsgs(you, container, pageSizeA, pageNoA)
        {
            var str = "";
            $.ajaxSetup({ async: false });
            MsgProvider.getMsgs(you, pageSizeA, pageNoA, function (result) {
                str = msg_i_functions.bindmsg($.parseJSON(result), you, true, container);
            });
            $.ajaxSetup({ async: true });
            return str;
        }
    </script>
</asp:Content>
