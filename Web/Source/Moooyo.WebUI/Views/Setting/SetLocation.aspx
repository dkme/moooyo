<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/FrontEnd.Master" Inherits="System.Web.Mvc.ViewPage<Moooyo.WebUI.Models.PageModels.MemberProfileModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	位置设置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
	    <% Html.RenderPartial("~/Views/Setting/LeftPanel.ascx");%>
		<div class="Set_content">
		    <div class="Set_title"> <span>位置设置</span><em>▲</em></div>
			<div class="Set_box">
                <div class="bind_form ">
				   <span class="at_text t_1_d" style="color:#666;" >在地图上点选你的位置，然后点击确认。 </span>
				    <span class="bind_map"> <div id="map_canvas"></div>
                    </span>		     
				 </div>
			    <div class="bind_form ">
				   
                    <span class="at_text t_1_d" style="color:#666;" > 您的位置在：  </span>
					<span class="at_text t_1_d" style="color:#666;" > <input type="text" class="txtput" name="addr" id="addr" style="width:374px; background:url(/pics/input_long_bg.png) 0 0 no-repeat;" tabindex="2" /> </span>
					<span class="at_text t_1_d" style="color:#666;" > <input type="checkbox" class="ml20" style="border:0" id="hiddenmyloc" name="hiddenmyloc" text="隐藏我的具体位置" <%=Model.Member.HiddenMyLoc ? "checked=\"checked\"" : "" %> /> 隐藏我的具体位置  </span>
					<span class="at_text t_1_d" style="color:#666; margin-top:10px;" >  <input type="button" class="reg_btn btn" value="提交" tabindex="4" onclick="selectp();" /> </span>
					<div class="bind_com"></div>
               </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="interestIds" value="<%=Model.interestIds %>" />
    <input type="hidden" id="permissions" name="permissions" value="0"/>
    <%--<%string wb = Model.User.BindedPlatforms;
          if (wb == "")
          {
          %>
		    
            <input type="hidden" id="txwb" name="txwb" value="false" />
            <input type="hidden" id="sinawb" name="sinawb" value="false" />
          <%
          }
          else
          {
              wb = wb.Substring(0, wb.Length - 1);
              foreach (string wbtype in wb.Split('|'))
              {
                  switch (wbtype)
                  {
                      case "1":%>
                          
                        <input type="hidden" id="txwb" name="txwb" value="false" />
                        <input type="hidden" id="sinawb" name="sinawb" value="true" />
                      <% break;
                      case "2":%>
		                  
                        <input type="hidden" id="txwb" name="txwb" value="true" />
                        <input type="hidden" id="sinawb" name="sinawb" value="false" />
                      <% break;
                  }
              }
          }%>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderCss" runat="server">
    <style type="text/css">
        #map_canvas{width:380px;height:300px;margin:0px;padding:0px;}
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderJs" runat="server">
    <script src="/js/base_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <script src="/js/data_<%=Model.JsVersion %>.js" type="text/javascript"></script>
    <!--[if IE 6]>
    <script type="text/javascript">
        DD_belatedPNG.fix('em,txtput');
	 </script>
    <![endif]-->
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        var city = "<%=((Model.Member.City == null || Model.Member.City == "") || Model.Member.City.Split('@').Length < 2) ? "" : Model.Member.City.Split('@')[1].ToString() %>";
        var lat = <%=Model.Member.Lat %>;
        var lng = <%=Model.Member.Lng %>;

        $(document).ready(function () {
            initialize();
        });
        function initialize() {
            if (lat == 0)
            {
                getMap(city, document.getElementById("map_canvas"), clickMap);
            }
            else
            {
                getMapByLatLng(lat, lng, document.getElementById("map_canvas"), clickMap);
                var latlng = new google.maps.LatLng(lat, lng);
                clickMap(latlng);
            }
        };
        function clickMap(latlng) {
            if (marker != null) marker.setMap(null);
            map.setCenter(latlng);
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode(
                { 'latLng': latlng }, 
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        if (results[0]) {
                            marker = new google.maps.Marker({ position: latlng, map: map });
                            $("#addr").val(results[0].formatted_address);
                        }
                    }
                    else { $.jBox.tip("地址解析失败: " + status, 'err'); }
                }
            );
        }
        function selectp() {
            if (marker == null) { 
                $.jBox.tip("请在地图上选择你的地点。", 'info'); 
                return; 
            }
            var hiddenmyloc = $("#hiddenmyloc").attr("checked") == "checked" ? true : false;
            memberprovider.setLatLng(
                hiddenmyloc, 
                marker.position.lat(), 
                marker.position.lng(),
                function(){

                    //添加说说内容
                    var permissions = $("#permissions").val();
                    var interestIDs = $("#interestIds").val();
                    if (interestIDs == "") {
                        $.jBox.tip("你没有加粉或创建的兴趣，无法将动态分享到说说", 'error');
                    }
                    else {
                        ContentProvider.InsertMemberContent(permissions, interestIDs, marker.position.lat().toString(), marker.position.lng().toString(), '设置个人位置', function (data) {
                            var data = $.parseJSON(data);

                            //分享到微博
//                            var txwb = $("#txwb"), sinawb = $("#sinawb"); ;
//                            if (txwb.val() != null && txwb.val() != "" && txwb.val() == "true") {
//                                microConnOperation.sendInfo('2', '2', "我刚刚在米柚网上添加了一个兴趣，名字叫“" + title + "”，大家都过来看看，很有意思的哦", '');
//                            }
//                            if (sinawb.val() != null && sinawb.val() != "" && sinawb.val() == "true") {
//                                microConnOperation.sendInfo('1', '1', "我刚刚在米柚网上添加了一个兴趣，名字叫“" + title + "”，大家都过来看看，很有意思的哦", '');
//                            }
                            $.jBox.tip("您的位置已保存", 'success');
                        });
                    }
                }
            );
        }
    </script>
</asp:Content>
