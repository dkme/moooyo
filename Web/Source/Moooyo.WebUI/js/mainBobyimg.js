$(document).ready(function () { 
var imagestr = "";
if ($("#backimage").val() != null)
    imagestr = photofunctions.getnormalphotoname($("#backimage").val());
else
    imagestr = "/pics/main_bg.gif";
$("body").append("<img id='mainbodyimage' src='" + imagestr + "' style='position:fixed; left:0; top:0; width:100%; min-height:100%; height:auto !important; height:100%;visibility: hidden; _display: none;'/>");
$(window).load(function () {
    // 排除IE 6
    if (!$('html').hasClass('lt-ie7')) {
        var theWindow = $(window),
                    bgWidth = $("#mainbodyimage").width(),
                    bgHeight = $("#mainbodyimage").height(),
                    aspectRatio = bgWidth / bgHeight;
        function resizeBg() {
            var winWidth = theWindow.width(),
                        winHeight = theWindow.height(),
                        w = h = 0,
                        offsetX = bgWidth - winWidth > 0 ? (bgWidth - winWidth) / 2 : 0,
                        offsetY = bgHeight - winHeight > 0 ? (bgHeight - winHeight) / 2 : 0;
            if ((winWidth / winHeight) < aspectRatio) { h = offsetY + winHeight; w = h * aspectRatio; }
            else { w = offsetX + winWidth; h = w / aspectRatio; }
            $("#mainbodyimage").css({ width: w, height: h, left: -offsetX, top: -offsetY }).css('visibility', 'visible').fadeIn(500);
        }
        theWindow.resize(function () { resizeBg(); }).trigger("resize");
    }
    else {
        $('body').attr('style', '_background: transparent url(' + imagestr + ') no-repeat fixed center center');
    }
});
});