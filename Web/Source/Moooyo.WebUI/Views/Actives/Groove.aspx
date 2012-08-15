<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
吐槽有奖
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="tcenter">
    <div class="center groove">
        <div class="groovemain">
            <img class="titleimg" src="/pics/Active_img/a2title.gif" alt="" title="" />
            <div id="reveal">
                <div class="titletxt">您的“吐槽“一经采纳，将不限量奉送电影票！（4.5~5.25）</div>
                <img class="moimg" src="/pics/Active_img/a2mo.gif" alt="" title="" />
                <div class="tiptxt">&emsp;&emsp;亲，也许您对我们的“米柚”有更好的意见或者建议，有话说？没问题！
    『米柚』感谢您关注的同时，专门为您提供了吐槽的平台，让您一吐为快。</div>
                <div class="pstxt">ps.谏言每采纳一条<span class="bl">奖励电影票</span>一张！抓紧机会了，多劳多得哦~</div>
                <a id="subbtn" class="subbtn" ></a>
            </div>
            <div id="append" class="poper">
                <a id="mycontent_hover" class="mycontent_hover"></a> 
                <a id="submitcontent" class="submitcontent"></a>
                <a id="rollback" class="rollback"></a>
                <div id="list_div" class="list_div">
                    <table class="li_table">
                        <thead>
                            <tr>
                                <th>时间</th>
                                <th>内容</th>
                                <th>状态</th>
                            </tr>
                        </thead>
                        <tbody id="contentbody">
                            
                        </tbody>
                    </table>
                    <div id="pager" class="verifyPager">sfdsfds</div>
                </div> 
                <div id="send_div" class="send_div">
                    <div class="sendpoper"><div class="font_count"><span id="font_count" class="font_count_tip">0</span>/140</div>
</div>
                    <div class="a2paper_bac">
                       <div class="font">
                        <textarea  cols="" rows="" class="inputtxt" id="inputtxt"></textarea>
                        <br /><span class="popertip">(请在信件内容中留下的您的称呼及联系方式，以便米柚工作人员能及时与您取得联系！)</span><a id="submit_btn" class="submit_btn">写好了,提交</a>
                       </div>
                    </div>
                    <div class="a2paper_bottom"></div>
                </div>
            </div>
            <div class="copyrighttxt">米柚网保留对本活动的最终解释权</div>
        </div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    html,body{background:url(/pics/Active_img/a2bac.gif) repeat; height:100%;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/js/a_<%=ViewData["jsversion"] %>.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.pager.pz.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#subbtn").bind("click", function () { $("#reveal").hide(); $("#append").show() });
            $("#rollback").bind("click", function () { $("#append").hide(); $("#reveal").show() });

            $("#mycontent_hover").bind("click", function () { $("#mycontent_hover").attr("class", "mycontent_hover"); $("#submitcontent").attr("class", "submitcontent"); $("#send_div").hide(); $("#list_div").show() });
            $("#submitcontent").bind("click", function () { $("#mycontent_hover").attr("class", "mycontent"); $("#submitcontent").attr("class", "submitcontent_hover"); $("#list_div").hide(); $("#send_div").show() });

            //ie下统计字数
            $("#inputtxt").bind('propertychange', function () {
                var font_count = $.trim($(this).val()).length;
                if (font_count <= 140) {
                    $("#font_count").attr("class", "font_count_tip").html(font_count);
                }
                else {
                    $("#font_count").attr("class", "font_count_tip_warning").html(font_count);
                }

            });
            //其他浏览器统计字数
            $("#inputtxt").bind('input', function () {
                var font_count = $.trim($(this).val()).length;
                if (font_count <= 140) {
                    $("#font_count").attr("class", "font_count_tip").html(font_count);
                }
                else {
                    $("#font_count").attr("class", "font_count_tip_warning").html(font_count);
                }

            });
            //提交建议
            $("#submit_btn").bind("click", function () {
                if (!check()) return;
                var content = $.trim($("#inputtxt").val());
                MyActivesProvider.AddGroove(content, function (data) {
                    var obj = $.parseJSON(data);
                    if (obj.ok) {
                        alert("您的信息已经提交，非常感谢您对米柚网的支持！");
                        count();
                        setPager();
                        bindlist();
                        $("#mycontent_hover").click();
                        $("#inputtxt").val("")
                        $("#font_count").html("0");
                    }
                    else {
                        alert("提交失败");
                    }
                });
            });
            count();
            setPager();
            bindlist();

        });

        function check() {
            var txt_count = $.trim($("#inputtxt").val()).length;
            if ( txt_count > 140) {
                alert("已超过140字限制，请适量删减！");
                return false;
            }
            else if(txt_count <10) {
                alert("您输入的内容太少了~");
                return false;
            }
            return true;
         }
        function bindlist() {
            MyActivesProvider.GetGroove(pagesize, pageno, function (data) {
                var objs = $.parseJSON(data);
                var str = "";
                var className = "";
                $.each(objs, function (i) {
                    className = i % 2 == 0 ? "tr1" : "tr2";
                    str += "<tr class=\""+className +"\"><td class=\"td1\">" + paserJsonDate(objs[i].CreatedTime).format('yyyy-mm-dd HH:MM:ss') + "</td>";
                    str += "<td class=\"td2\">" + objs[i].Content.toString().substr(0,140) + "</td>";
                    str += "<td class=\"td3\">" + (objs[i].IsAudited ? objs[i].Result : "审核中") + "</td>";

                });
                $("#contentbody").html(str);
            });

        }
        var cont; //div里面的内容

        var pagesize = 10;
        var mypagecount = 1;
        var pageno =1;

        function count()
        {    
            var _pagecount=0;
            $.ajaxSetup({  
                    async : false  
            });
            MyActivesProvider.GetGrooveCount(function (result) {
                var allcount = $.parseJSON(result);
                if (allcount <= 0)//如果没有则显示提交div
                { $("#submitcontent").click(); cont = $("#list_div").html();  $("#list_div").html("您还没有提交任何内容！"); }
                else { if (cont != undefined) {$("#list_div").html(cont); }  }
                _pagecount = parseInt((parseInt(allcount) + parseInt(pagesize) - 1) / parseInt(pagesize));
            });
            mypagecount = _pagecount;
            if(pageno > mypagecount)
            {
                pageno = mypagecount;
            }
            $.ajaxSetup({  
                    async : true
            }); 
        }

        PageClick = function (pageclickednumber) {
            if (pageclickednumber > mypagecount) {
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
