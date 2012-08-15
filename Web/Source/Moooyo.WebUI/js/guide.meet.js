/*
* show guide
* @author hulu
*/
$(function () {
    var guide_html = $('#j-guide-meet').html();
    var MIN_HEIGHT = 680,
        $wrap = $('#wrap'),
        groupsTop = $wrap.find('.mycare').offset().top,
        backupWrapTop = parseInt($wrap.css('margin-top'));
    // to keep content viewable
    var keepGuideView = function () {
        if ($(window).height() < MIN_HEIGHT) {
            $wrap.css('margin-top', -groupsTop + backupWrapTop + 60);
        }
        else {
            $wrap.css('margin-top', backupWrapTop);
        }
    };
    $(window).bind('resize', keepGuideView);
    $.extend(Mo.guide, {
        gostep: function ($step, step) {
            // set position
            var offset;
            // step start from 0
            if (step === 1) {
                var $wrap = $('#wrap .mycare'),
                    $groups = $('#guide .groups');
                offset = $wrap.offset();
                $step.css({
                    left: offset.left,
                    top: offset.top
                });
                if ($groups.children('img').length === 0) {
                    $groups.append($wrap.find('img').slice(0, 3).clone());
                }
            }
            else if (step === 2) {
                offset = $('#wrap .addview').offset();
                $step.css({
                    left: offset.left - 23,
                    top: offset.top - 16
                });
            }
            else if (step === 3) {
                offset = $('#wrap .selfshare').offset();
                $step.css({
                    left: offset.left - 1,
                    top: offset.top - 200
                });
            }
            else if (step === 4) {
                //For IE6: set newcomer section viewable first
                if ($('html').hasClass('lt-ie7')) {
                    if ($(window).height() < MIN_HEIGHT) {
                        $('#newcomer').css('margin-top', groupsTop);
                    }
                    else {
                        $('#newcomer').css('margin-top', 60);
                    }
                }
                offset = $('#newcomer').offset();
                var $clone = $('#newcomer').clone(false);
                if (offset.left === 0) {
                    offset.left = $(window).width() - 100;
                    offset.top = 80;
                    $clone.css({
                        right: '10px',
                        display: 'block'
                    });
                }
                $step.css({
                    left: offset.left - $step.width() - 15,
                    top: offset.top + 80
                });
                $step.children('.newcomer').remove();
                $step.append($clone);
            }
        }
    });
    Mo.guide.init(guide_html);
    // restore page position
    Mo.guide.$guide.bind('finished', function () {
        $wrap.css('margin-top', backupWrapTop);
        $(window).unbind('resize', keepGuideView);
        //For IE6
        if ($('html').hasClass('lt-ie7')) {
            $('#newcomer').css('margin-top', 60);
        }
    });
});