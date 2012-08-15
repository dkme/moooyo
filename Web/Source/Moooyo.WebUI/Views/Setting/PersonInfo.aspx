<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    个人信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
        <div class="Set_content">
            <div class="Set_title">
                <span>个人信息</span><em>▲</em></div>
            <div class="Set_box">
                <form action="" method="post" id="profileform" name="profileform">
                <div class="update_form">
                    <dl>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab" id="registEmail">注册邮箱:</em> <span class="at_text" style="color: #b6a8a1;"
                                id="emailArea">
                                <% string memberLowerEmail = Model.Member.Email.ToLower();
                                   bool isExternalPlatform = false;
                                   if (memberLowerEmail.IndexOf("@sinaweibo") >= 0 || memberLowerEmail.IndexOf("@tencentweibo") >= 0 || memberLowerEmail.IndexOf("@renren") >= 0 || memberLowerEmail.IndexOf("@douban") >= 0)
                                   {
                                       isExternalPlatform = true;
                                   } %>
                                <span id="oldEmail">
                                    <%=(!isExternalPlatform) ? Model.Member.Email : "合作平台账户"%></span>
                                <% if (!isExternalPlatform)
                                   { %>
                                <font color="#f89623">[<%=Model.Member.EmailIsVaild ? "已" : "未"%>认证]
                                    <% } %>
                                    <% if (!Model.Member.EmailIsVaild)
                                       { %>
                                    <a href="javascript:;" onclick="modifyEmail3('<%=(!isExternalPlatform) ? "modify" : "modifyEmailPasswd" %>');">
                                        [修改]</a>
                                    <% } %>
                                </font></span><span class="at_text" style="color: #b6a8a1; display: none;" id="modifyEmailArea">
                                    <input type="text" class="txtput black" name="modifyEmail" id="newEmail" tabindex="0"
                                        value="<%=Model.Member.Email %>" /><a href="javascript:;" onclick="modifyEmail3('save')">保存</a>
                                    <a href="javascript:;" onclick="modifyEmail3('cancel')">取消</a> </span><span class="at_text"
                                        style="color: #b6a8a1; display: none;" id="modifyEmailPasswdArea">
                                        <input type="text" class="txtput black" name="modifyEmail2" id="newEmail2" tabindex="0"
                                            value="" style="width: 150px;" />
                                        &nbsp;<span style="color: #666666">密码</span>：<input type="password" class="txtput black"
                                            name="modifyPasswd2" id="newPasswd2" tabindex="0" value="" style="width: 150px;" />
                                        &nbsp;<a href="javascript:;" onclick="modifyEmailPasswd2('saveEmailPasswd')">保存</a>
                                        <a href="javascript:;" onclick="modifyEmailPasswd2('cancelEmailPasswd')">取消</a>
                                    </span>
                        </dd>
                        <dd class="clearfix">
                            <em class="lab">昵称:</em> <span class="at_text t_1_d">
                                <input type="text" class="txtput black" name="nickName" id="nickName" tabindex="0"
                                    value="<%=Model.Member.Name %>" />
                            </span>
                        </dd>
                        <dd class="clearfix">
                            <em class="lab">出生日期:</em> <span class="at_text t_1_d">
                                <select id="year" name="year" tabindex="1">
                                </select>&nbsp;年&nbsp;
                                <select id="month" name="month" tabindex="2">
                                </select>&nbsp;月&nbsp;
                                <select id="day" name="day" tabindex="3">
                                </select>&nbsp;日&nbsp; </span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">性别:</em> <span class="at_text">
                                <%=Model.Member.Sex == 1 ? "男" : "女" %></span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">城市:</em> <span class="at_text t_1_d">
                                <select id="province" name="province" tabindex="6">
                                </select>
                                <select id="city" name="city" tabindex="7">
                                </select></span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">身高:</em> <span class="at_text t_1_d">
                                <select id="height" name="height" tabindex="8">
                                    <option value="">请选择</option>
                                    <option value="150">150cm&nbsp;&nbsp;</option>
                                    <option value="151">151cm&nbsp;&nbsp;</option>
                                    <option value="152">152cm&nbsp;&nbsp;</option>
                                    <option value="153">153cm&nbsp;&nbsp;</option>
                                    <option value="154">154cm&nbsp;&nbsp;</option>
                                    <option value="155">155cm&nbsp;&nbsp;</option>
                                    <option value="156">156cm&nbsp;&nbsp;</option>
                                    <option value="157">157cm&nbsp;&nbsp;</option>
                                    <option value="158">158cm&nbsp;&nbsp;</option>
                                    <option value="159">159cm&nbsp;&nbsp;</option>
                                    <option value="160">160cm&nbsp;&nbsp;</option>
                                    <option value="161">161cm&nbsp;&nbsp;</option>
                                    <option value="162">162cm&nbsp;&nbsp;</option>
                                    <option value="163">163cm&nbsp;&nbsp;</option>
                                    <option value="164">164cm&nbsp;&nbsp;</option>
                                    <option value="165">165cm&nbsp;&nbsp;</option>
                                    <option value="166">166cm&nbsp;&nbsp;</option>
                                    <option value="167">167cm&nbsp;&nbsp;</option>
                                    <option value="168">168cm&nbsp;&nbsp;</option>
                                    <option value="169">169cm&nbsp;&nbsp;</option>
                                    <option value="170">170cm&nbsp;&nbsp;</option>
                                    <option value="171">171cm&nbsp;&nbsp;</option>
                                    <option value="172">172cm&nbsp;&nbsp;</option>
                                    <option value="173">173cm&nbsp;&nbsp;</option>
                                    <option value="174">174cm&nbsp;&nbsp;</option>
                                    <option value="175">175cm&nbsp;&nbsp;</option>
                                    <option value="176">176cm&nbsp;&nbsp;</option>
                                    <option value="177">177cm&nbsp;&nbsp;</option>
                                    <option value="178">178cm&nbsp;&nbsp;</option>
                                    <option value="179">179cm&nbsp;&nbsp;</option>
                                    <option value="180">180cm&nbsp;&nbsp;</option>
                                    <option value="181">181cm&nbsp;&nbsp;</option>
                                    <option value="182">182cm&nbsp;&nbsp;</option>
                                    <option value="183">183cm&nbsp;&nbsp;</option>
                                    <option value="184">184cm&nbsp;&nbsp;</option>
                                    <option value="185">185cm&nbsp;&nbsp;</option>
                                    <option value="186">186cm&nbsp;&nbsp;</option>
                                    <option value="187">187cm&nbsp;&nbsp;</option>
                                    <option value="188">188cm&nbsp;&nbsp;</option>
                                    <option value="189">189cm&nbsp;&nbsp;</option>
                                    <option value="190">190cm&nbsp;&nbsp;</option>
                                    <option value="191">191cm&nbsp;&nbsp;</option>
                                    <option value="192">192cm&nbsp;&nbsp;</option>
                                    <option value="193">193cm&nbsp;&nbsp;</option>
                                    <option value="194">194cm&nbsp;&nbsp;</option>
                                    <option value="195">195cm&nbsp;&nbsp;</option>
                                    <option value="196">196cm&nbsp;&nbsp;</option>
                                    <option value="197">197cm&nbsp;&nbsp;</option>
                                    <option value="198">198cm&nbsp;&nbsp;</option>
                                    <option value="199">199cm&nbsp;&nbsp;</option>
                                    <option value="200">200cm&nbsp;&nbsp;</option>
                                    <option value="201">201cm&nbsp;&nbsp;</option>
                                    <option value="202">202cm&nbsp;&nbsp;</option>
                                    <option value="203">203cm&nbsp;&nbsp;</option>
                                    <option value="204">204cm&nbsp;&nbsp;</option>
                                    <option value="205">205cm&nbsp;&nbsp;</option>
                                    <option value="206">206cm&nbsp;&nbsp;</option>
                                    <option value="207">207cm&nbsp;&nbsp;</option>
                                    <option value="208">208cm&nbsp;&nbsp;</option>
                                    <option value="209">209cm&nbsp;&nbsp;</option>
                                    <option value="210">210cm&nbsp;&nbsp;</option>
                                </select>
                            </span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">星座:</em> <span class="at_text t_1_d">
                                <select id="star" name="star" tabindex="9">
                                    <option value="">请选择</option>
                                    <option value="水瓶座">水瓶座</option>
                                    <option value="双鱼座">双鱼座</option>
                                    <option value="白羊座">白羊座</option>
                                    <option value="金牛座">金牛座</option>
                                    <option value="双子座">双子座</option>
                                    <option value="巨蟹座">巨蟹座</option>
                                    <option value="狮子座">狮子座</option>
                                    <option value="处女座">处女座</option>
                                    <option value="天秤座">天秤座</option>
                                    <option value="天蝎座">天蝎座</option>
                                    <option value="射手座">射手座</option>
                                    <option value="摩羯座">摩羯座</option>
                                </select></span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">学历:</em> <span class="at_text t_1_d">
                                <select id="educationalBackground" name="educationalBackground" tabindex="10">
                                    <option value="">请选择</option>
                                    <option value="初中及以下">初中及以下</option>
                                    <option value="高中">高中</option>
                                    <option value="中专">中专</option>
                                    <option value="大专">大专</option>
                                    <option value="本科">本科</option>
                                    <option value="硕士">硕士</option>
                                    <option value="博士及以上">博士及以上</option>
                                </select>
                            </span>
                        </dd>
                        <dd class="clearfix" style="height: auto;">
                            <em class="lab">职业:</em> <span class="at_text t_1_d">
                                <select id="career" name="career" tabindex="13">
                                    <option value="">请选择</option>
                                    <option value="中层管理">中层管理</option>
                                    <option value="企业高管">企业高管</option>
                                    <option value="艺术类">艺术类</option>
                                    <option value="私营业主">私营业主</option>
                                    <option value="学生">学生</option>
                                    <option value="公司职员">公司职员</option>
                                    <option value="教师">教师</option>
                                    <option value="党政军机关">党政军机关</option>
                                    <option value="医务工作者">医务工作者</option>
                                    <option value="其他">其他</option>
                                    <%--<option value="自己填写"  >自己填写</option>--%>
                                </select>
                            </span>
                        </dd>
                        <%--<dd class="clearfix" style="height: auto;">
            <em class="lab">置业状况:</em>
            <span class="at_text t_1_d">
                <select id="propertySituation" name="propertySituation" tabindex="13">
                <option value="">请选择</option>
                <option value="有房">有房</option>
                <option value="有车">有车</option>
                </select>
            </span>			</dd>--%>
                        <dd class="clearfix">
                            <em class="lab">个性域名:</em> <span class="at_text t_1_d domainNameID" style="white-space: nowrap;">
                                http://www.moooyo.com/u/<input type="text" class="txtput mytxt" name="domainNameID"
                                    id="domainNameID" tabindex="12" style="width: 135px;" value="" />
                            </span>
                        </dd>
                        <dd class="clearfix" style="height: 100px; margin-bottom: 20px;">
                            <em class="lab">个人介绍:</em> <span class="at_text t_1_d personalIntroduction">
                                <textarea class="txtarea" name="personalIntroduction" id="personalIntroduction" tabindex="13"></textarea>
                            </span>
                        </dd>
                        <dd class="clearfix">
                            <em class="lab"></em><span class="at_text">
                                <input type="button" onclick="saveBtnClick();" class="reg_btn btn" value="提交" tabindex="14" />
                            </span>
                        </dd>
                    </dl>
                </div>
                </form>
                <div style="height: 100px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
<style type="text/css">
    .domainNameID label.error{float:none; clear:both; background-color:Red;}
    .personalIntroduction label.error{float:none; clear:both; background-color:Red;}
    
    .parentframe { position:relative; z-index:9999px; left:50px; top:50px; width:700px; height:500px; background:#fff; border:1px solid #ddd;}
    .Framediv { background:#f3f3f3;float:left;position:relative; margin-right:20px; cursor:pointer; }
    .childFrame { background:#ccc; float:left; position:absolute;}
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.validate.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em,.txtput');
	 </script>
    <![endif]-->
    <script language="javascript" type="text/javascript">
       $(document).ready(function () {
            <%
            var day = Model.Member.Birthday.Day;
            var month = Model.Member.Birthday.Month;
            var year = Model.Member.Birthday.Year;
            %>
            setYMD(<%=year %>, <%=month %>, <%=day %>);

            var sex = <%=Model.Member.Sex %>;
            switch(sex) {
                case 1: 
                    $("#sexBoy").attr("checked", true); 
                    break;
                case 2: 
                    $("#sexGirl").attr("checked", true); 
                    break;
            }

            //城市
            <% if (Model.Member.City!=null) { 
                %>
                var prov = "<%=Model.Member.City.Split('@')[0] %>";
                var city = "<%=Model.Member.City.Split('@').Length == 2 ? Model.Member.City.Split('@')[1] : Model.Member.City.Split('@')[0] %>";
                setZone($("#province"), $("#city"), prov, city);
            <% }else{ %>
                setZone($("#province"), $("#city"), '', '');
            <% } %>

             //身高
            $("#height").attr("value", "<%=Model.Member.Height %>");
            //学历
            $("#educationalBackground").attr("value", "<%=Model.Member.EducationalBackground %>");
            //职业
            $("#career").attr("value", "<%=Model.Member.Career %>");
            //置业状况
            $("#propertySituation").attr("value", "<%=Model.Member.PropertySituation %>");
            //个性域名
            var domainNameID = $("#domainNameID");
            var domainNameIDValue = "<%=Model.Member.UniqueNumber != null ? (Model.Member.UniqueNumber.DomainNameID == "" ? "" : Model.Member.UniqueNumber.DomainNameID) : "" %>";
            if(domainNameIDValue != "") {
                domainNameID.attr("value", domainNameIDValue);
            }
            //个人介绍
            var personalIntroduction = $("#personalIntroduction");
            var personalIntroductionValue = "<%=Model.Member.PersonalIntroduction == "" ? "" : Model.Member.PersonalIntroduction %>";
            if(personalIntroduction != "") {
                personalIntroduction.attr("value", personalIntroductionValue);
            }

            setValidate();
        });

        function setValidate() {
            $("#profileform").validate({
                rules: {
                    nickName: {
                        required: true,
                        chineseEnglishMaxLength: 18,
                        minlength: 2,
                        nicknameCheck: true,
                        userNameFliterCheck:true
                    },
                    province: {
                        required: true
                    },
                    city: {
                        required: true
                    },
                    domainNameID: {
                        minlength: 2,
                        maxlength: 18,
                        nameCheck: true,
                        remote: "/Setting/IsDomainNameIDUsed"
                    },
                    personalIntroduction: {
                        minlength: 5,
                        maxlength: 160
                    }
                },
                messages: {
                    nickName: {
                        required: "必填",
                        chineseEnglishMaxLength: "昵称不能超过9个中文或18个英文字符",
                        minlength: "昵称不能少于2个字",
                        nicknameCheck: "只能包括中文字、英文字母、数字和下划线"
                    },
                    province: {
                        required: "必填"
                    },
                    city: {
                        required: "必填"
                    },
                    domainNameID: {
                        minlength: "长度不能少于2个字符",
                        maxlength: "长度不能多于18个字符",
                        nameCheck: "只能英文字母、数字和下划线",
                        remote: "该域名已使用"
                    },
                    personalIntroduction: {
                        minlength: "长度不能低于5个字",
                        maxlength: "长度不能多于160个字"
                    }
                }
            });
        }
        function saveBtnClick() {
            var form = $("#profileform");
            if (form.valid()) {
                saveProfile(form, '/Setting/updateProfile', '档案已成功保存', '档案保存失败，系统维护中，给您带来了不便，请谅解！');
            }
            else
                showMsg('请将必要信息填写完整', "error");
        }
        function showMsg(msg, type) {
            $("html, body").animate({ scrollTop: 0 }, 120);
            $.jBox.tip(msg, type);
        }
        function saveProfile(form, url, successmsg, faultmsg) {
            $.ajax({
                type: 'POST',
                url: url,
                data: form.serialize(), //序列化表单里所有的内容
                success: function (data) {
                    showMsg(successmsg, "success");
                },
                error: function (data) {
                    showMsg(faultmsg, "error");
                }
            });
        }
      
        function modifyEmail3(operating) {
            switch(operating) {
                case "modify":
                    $("span#emailArea").hide();
                    $("span#modifyEmailArea").show();
                    break;
                case "save": 
                    var email = $("input#newEmail").val();
                    if(email != "" && email != null) {
                        if (isEmail(email)) {
                            memberprovider.modifyEmail(email, function (data) {
                                var obj = $.parseJSON(data);
                                if(obj.ok) {
                                    $.jBox.tip("成功修改注册邮箱", "success");
                                    $("span#emailArea").show();
                                    $("span#modifyEmailArea").hide();
                                    $("span#oldEmail").html(email);
                                }
                                else {
                                    $.jBox.tip(obj.err, "error");
                                }
                            });
                        }
                        else
                            $.jBox.tip("邮箱格式错误", "error");    
                    }
                    else
                        $.jBox.tip("注册邮箱不能为空", "error"); 
                    break;
                case "cancel":
                    $("span#emailArea").show();
                    $("span#modifyEmailArea").hide();
                    break;
                case "modifyEmailPasswd":
                    $("span#emailArea").hide();
                    $("span#modifyEmailPasswdArea").show();
                    //$("em#registEmail").html("邮箱:");
                    break;
                default:
                    break;
            }
        }

         function modifyEmailPasswd2(operating) {
            switch(operating) {
                 case "saveEmailPasswd":
                    var email = $("input#newEmail2").val();
                    var passwd = $("input#newPasswd2").val();
                    if(email != "" && email != null && passwd != "" && passwd != null) {
                        if (isEmail(email)) {
                            if(passwd.length >= 6) {
                                memberprovider.modifyEmailPasswd(email, passwd, function (data) {
                                    var obj = $.parseJSON(data);
                                    if(obj.ok) {
                                        $.jBox.tip("成功修改邮箱密码", "success");
                                        $("span#emailArea").show();
                                        $("span#modifyEmailPasswdArea").hide();
                                        //$("em#registEmail").html("注册邮箱:");
                                        $("span#oldEmail").html(email);
                                    }
                                    else {
                                        $.jBox.tip(obj.err, "error");
                                    }
                                });
                            }
                            else
                                $.jBox.tip("密码太短", "error"); 
                        }
                        else
                            $.jBox.tip("邮箱格式错误", "error");    
                    }
                    else
                        $.jBox.tip("邮箱或密码不能为空", "error"); 
                    break;
                case "cancelEmailPasswd":
                    $("span#emailArea").show();
                    $("span#modifyEmailPasswdArea").hide();
                    $("em#registEmail").html("注册邮箱:");
                    break;
                default: 
                    break;
            }
        }

    </script>
</asp:Content>
