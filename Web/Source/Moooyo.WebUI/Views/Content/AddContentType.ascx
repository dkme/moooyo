<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Moooyo.WebUI.Models.PageModels.Content.AddContentModel>" %>
<%@ Import Namespace="Moooyo.WebUI.Common" %>
<div class="fb_box_com w600">
	<ul id="piclist">
    <%
    string contenttype = "";
    if (Model.contentObj != null)
    {
        switch (Model.contentObj.ContentType)
        {
            case Moooyo.BiZ.Content.ContentType.Image: contenttype = ((Moooyo.BiZ.Content.ImageContent)Model.contentObj).Type; break;
            case Moooyo.BiZ.Content.ContentType.SuiSuiNian: contenttype = ((Moooyo.BiZ.Content.SuiSuiNianContent)Model.contentObj).Type; break;
            case Moooyo.BiZ.Content.ContentType.CallFor: contenttype = ((Moooyo.BiZ.Content.CallForContent)Model.contentObj).Type; break;
        }
    }
    foreach (var type in Model.type)
    {
        List<string> types = type.Split(',').ToList();
        if (types[1] == contenttype)
        {
                    %><li class="contenttype" data-type="<%=types[1] %>" <%="style=\"background-image:url('"+types[3]+"');\"" %> title="点击勾选内容类别"><%=types[1]%><em></em></li><%
        }
        else
        {
                    %><li class="contenttype" data-type="<%=types[1] %>" <%="style=\"background-image:url('"+types[3]+"');\"" %> title="点击勾选内容类别"><%=types[1]%></li><%
        }
    }
    if (contenttype != "")
    {
        %><input type="hidden" id="type" name="type" value="<%=contenttype %>"/><%
    }
    else
    {
        %><input type="hidden" id="type" name="type"/><%
    }%>
    </ul>
</div>
<script type="text/javascript">
    $().ready(function () {
        $(".contenttype").click(function () {
            $("#type").val($(this).attr("data-type"));
            $("#piclist li em").remove();
            $(this).html($(this).html() + "<em></em>")
        });
        if ($("#updateContent").val() == null) {
            $(".contenttype:first").click();
        }
    });
</script>