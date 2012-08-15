<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<style type="text/css">
    .fb_box_com .textarea3{width:578px; height:208px; }
    .fb_box_com .redlink{cursor:pointer;}
    .fb_box_com .graylink{cursor:pointer;}
</style>
<div class="fb_box_com w600">
    <span>
        <textarea id="content" name="textarea" class="textarea3" style="overflow-y:scroll;"></textarea>
    </span>
</div>
<div class="fb_box_com w600">
    <span class="at_text"><a class="graylink" href="/Content/IndexContent">取  消</a></span>
    <span class="a_text"><a class="redlink" id="submitbu" name="submitbu">提  交</a></span>
</div>
<script type="text/javascript">
    $().ready(function () {
        //加载提交的点击事件
        $("#submitbu").click(function () { contentsubmit(); });
    });
</script>