/*
 * @author hulu
 */
window.Mo = window.Mo || {};
Mo.guide = {
    timeId: 0,
    effect: function ($step) {
        if (this.timeId) {
            clearInterval(this.timeId);
        }
        var $next = $step.find('.next');
        this.timeId = setInterval(function () {
            $next.fadeToggle();
        }, 800);
    },
    center: function ($step) {
        var $mask = $(window);
        $step.css({
            left: ($mask.width() - $step.width()) / 2,
            top: ($mask.height() - $step.height()) / 2
        });
    },
    init: function (html) {
        this.$guide = $(html).appendTo('body');
        this.currentStep = 0;
        var $finish = this.$guide.find('.finish'),
            reserveTop = parseFloat($finish.css('margin-top')),
            $steps = this.$guide.find('.step'),
            $next = this.$guide.find('.next'),
            $dots = this.$guide.find('.dot'),
            $close = $('<a class="close"></a>').appendTo(this.$guide),
            that = this,
            $step1 = $steps.eq(0).show();

        $('html').css({
            overflow: 'hidden'
        });

        // set step 1 position
        this.center($step1);
        this.effect($step1);

        // bind next event
        this.$guide.click(function () {
            var lastStep = $steps.length - 1,
                nextStep = that.currentStep + 1,
                step = that.currentStep = nextStep > lastStep ? lastStep : nextStep,
                $step = $steps.hide().eq(nextStep).show();
            $dots.eq(step).addClass('current').siblings().removeClass('current');
            that.effect($step);
            if (nextStep < lastStep) {
                that.gostep($step, step);
            }
            else if (nextStep === lastStep) {
                // go to last step
                //that.center($step);
                var toggle = true;
                var fx = function () {
                    $finish.animate({
                        'margin-top': toggle ? reserveTop - 20 : reserveTop
                    }, 1000);
                    toggle = !toggle;
                };
                fx();
                that.timeId = setInterval(fx, 1500);
                // fix for IE 7
                that.$guide.children('.mask').width('1px').width('100%');
                setTimeout(function () { that.center($step) }, 0);
            }
            else {
                // click on last step, close the guide
                $finish.click();
            }
        });
        this.$guide.on('click', '.dot', function (e) {
            var i = $(this).index();
            if (i === 0) {
                that.currentStep = 0;
                $(this).addClass('current').siblings().removeClass('current');
                var $step = $steps.hide().eq(0).show();
                //that.center($step);
                that.effect($step);
                // fix for IE 7
                that.$guide.children('.mask').width('1px').width('100%');
                setTimeout(function () { that.center($step) }, 0);
                e.stopPropagation();
            }
            else {
                that.currentStep = i - 1;
            }
        });
        $finish.add($close).click(function (e) {
            that.$guide.hide();
            $('html').css({
                overflow: 'auto'
            });
            if (that.timeId) {
                clearInterval(that.timeId);
            }
            e.stopPropagation();
            that.$guide.trigger('finished');
        });
        $(window).bind('resize', function () {
            var currentStep = that.currentStep,
                $step = $steps.eq(currentStep);
            if ($(window).height() < 538) {
                $dots.hide();
            }
            else {
                $dots.show();
            }
            if (currentStep === 0 || currentStep === $steps.length - 1) {
                // first and last step
                that.center($step);
            }
            else {
                // setTimeout for IE 7
                setTimeout(function () { that.gostep($step, currentStep); }, 0);
            }
        }).trigger('resize');

        // For IE£¶
        if ($('html').hasClass('lt-ie7')) {
            $(window).load(function () {
                DD_belatedPNG.fix('#guide img, .next, .text, .close');
            });
        }
    }
};