/*
* show guide
* @author hulu
*/
$(function () {
    var guide_html = "<div id=\"guide\" class=\"guide index\">\n  <div class=\"mask\"></div>\n  <div class=\"step step-1\">\n    <div class=\"pic\">\n      <img src=\"/pics/guide/tree.png\" alt=\"moooyo\" />\n    	<p class=\"text\">hello</p>\n    </div>\n  	<div class=\"text\">\n  		<p>亲爱的新同学，欢迎来到我们有爱的米柚~</p>\n  		<p>在这片快乐的单身领域，你参与得越多，就会收获越多单身朋友。</p>\n  		<p class=\"last cf\"><span class=\"fn-left\">接下来，请跟随我的脚步，来学会如何玩转米柚。</span><img class=\"next\" src=\"/pics/guide/mouse.png\" alt=\"click\"/></p>\n  	</div>\n  </div>\n  <div class=\"step step-2\">\n  	<p><img src=\"/pics/guide/mask-step-2.png\" alt=\"首页\" /></p>\n  	<p><img class=\"arrow\" src=\"/pics/guide/arrow-right.png\" alt=\"->\" /></p>\n    <p class=\"text cf\"><span class=\"fn-left\">每当你点这里，你就能看到你所关注的柚子们的最新动态了。</span><img class=\"next\" src=\"/pics/guide/mouse.png\" alt=\"click\"/></p>\n  </div>\n  <div class=\"step step-3\">\n  	<p><img src=\"/pics/guide/mask-step-3.png\" alt=\"118\" /></p>\n  	<p><img class=\"arrow\" src=\"/pics/guide/arrow-left.png\" alt=\"->\" /></p>\n    <p class=\"text cf\"><span class=\"fn-left\">当然，你也可以看看你都关注了哪些柚子。</span><img class=\"next\" src=\"/pics/guide/mouse.png\" alt=\"click\"/></p>\n  </div>\n  <div class=\"step step-4\">\n  	<p><img src=\"/pics/guide/mask-step-4.png\" alt=\"118\" /></p>\n  	<p><img class=\"arrow\" src=\"/pics/guide/arrow-right.png\" alt=\"->\" /></p>\n    <p class=\"text cf\"><span class=\"fn-left\">呀，你还木有关注任何柚子？<br/>点这里，就能发现跟你兴趣相投的柚子啦~</span><img class=\"next\" src=\"/pics/guide/mouse.png\" alt=\"click\"/></p>\n  </div>\n  <div class=\"step step-5\">\n    <div class=\"pic\">\n      <img src=\"/pics/guide/tree.png\" alt=\"moooyo\" />\n    	<p class=\"text say\">好啦，现在你来试试看吧</p>\n    	<p class=\"text finish\">好！</p>\n    </div>\n  </div>\n  <div class=\"steps\">\n  	<span class=\"dot current\">·</span>\n  	<span class=\"dot\">·</span>\n  	<span class=\"dot\">·</span>\n  	<span class=\"dot\">·</span>\n  	<span class=\"dot\">·</span>\n  </div>\n</div>";
    $.extend(Mo.guide, {
        gostep: function ($step, step) {
            // set position
            // step start from 0
            if (step === 1) {
                var offset = $('#top .selfPic').offset();
                $step.css({
                    left: offset.left - 41,
                    top: offset.top - 12
                });
            }
            else if (step === 2) {
                var offset = $('.fans_hit').offset();
                $step.css({
                    left: offset.left - 23,
                    top: offset.top - 16
                });
            }
            else if (step === 3) {
                var offset = $('#top .singlePic').offset();
                $step.css({
                    left: offset.left - 23,
                    top: offset.top - 12
                });
            }
        }
    });
    Mo.guide.init(guide_html);
});