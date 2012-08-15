/*
 * Render newcomer's uploaded avatar
 * @param {Array} users
 * @author hulu
 */
var newcomer_render = (function () {
    var $el,
        max_count = MAX_COUNT = 8,
    // 维护一份新用户池
        user_pool = [],
        binded = false;
    // set max_count
    var updateMaxCount = function () {
        if ($el) {
            // gotop.height = 65, newcomer.title.height = 68, newcomer.top = 75
            var count = Math.round(($(window).height() - 65 - 68 - 75) / 60);
            max_count = count < MAX_COUNT ? count : MAX_COUNT;
        }
    }

    var toggleCollapse = function (e) {
        $el.toggleClass('newcomer-collapse');
    };
    var setpos = function () {
        if ($el) {
            var offsetLeft = ($(window).width() - $('#wrap').width()) / 2,
            //minWidth = $el.width() + 10;
            // Because window.resize trigger only once in IE9, so $el.width() will be smaller when $el is collapsed
            // so we should set the width to a constant
                minWidth = 68 + 10;
            // horizonal
            if (offsetLeft > minWidth) {
                $el.css('right', offsetLeft - minWidth).removeClass('newcomer-collapse').off('mouseenter mouseleave', toggleCollapse);
                binded = false;
                // For IE6
                if ($('html').hasClass('lt-ie7')) {
                    $el.css('right', -minWidth);
                }
            }
            else {
                // $el.hide();
                $el.css('right', 0).addClass('newcomer-collapse');
                if (!binded) {
                    $el.on('mouseenter mouseleave', toggleCollapse);
                    binded = true;
                }
                // For IE6
                if ($('html').hasClass('lt-ie7')) {
                    $el.css('right', -30);
                }
            }
            // vertical
            updateMaxCount();
            var $items = $el.children('.newcomer-list').children('.newcomer-item');
            var extraCount = $items.length - max_count;
            if (extraCount > 0) {
                // 缩小窗口
                $items.slice(-extraCount).remove();
            }
            else if (extraCount < 0) {
                // 放大窗口
                var item_html = $('#j-newcomer-item').html(),
                    $list = $el.children('.newcomer-list'),
                    users = user_pool.slice(extraCount),
                    len = users.length;
                for (var i = max_count - user_pool.length; i < len; i++) {
                    users[i].Creater.ICONPath = photofunctions.getprofileiconphotoname(users[i].Creater.ICONPath);
                    $(Mustache.render(item_html, users[i])).appendTo($list);
                }
            }
        }
    };
    $(window).bind('resize', setpos);

    var popupDetail = function () {
        var thisobj = $(this);
        // setTimeout for IE6
        if ($('html').hasClass('lt-ie7')) {
            setTimeout(function () { thisobj.siblings().children('.detail').hide(); }, 0);
        }
        else {
            thisobj.siblings().children('.detail').hide();
        }
        thisobj.children('.detail').show();
        thisobj.children('.nickname').hide();
        var toObjectID = thisobj.attr("data-toobjectid");
        var type = thisobj.attr("data-type");
        var action = thisobj.children('.detail').children('.cnt').children('.action');
        ContentProvider.getLastMemberContent(toObjectID, type, function (data) {
            var data = $.parseJSON(data);
            action.children('.action-1').children('.mo').unbind("click");
            action.children('.action-2').children('.comment').unbind("click");
            if (data == null) {
                action.children('.action-1').children('.mo').bind("click", function () { window.open("/Content/TaContent/" + data.MemberID + "/all/1"); });
                action.children('.action-2').children('.comment').bind("click", function () { window.open("/Content/TaContent/" + data.MemberID + "/all/1"); });
            }
            else {
                action.children('.action-1').children('.mo').click(function () {
                    var nowcount = action.children('.action-1').children('.count').html() == "" ? 0 : parseInt(action.children('.action-1').children('.count').html());
                    action.children('.action-1').children('.count').html(nowcount + 1);
                    ContentProvider.AddContentLike(data.ID, data.ContentType, function (data) {
                        var data = $.parseJSON(data);
                        if (data.toString() == "False" || data.toString() == "false") {
                            action.children('.action-1').children('.count').html(nowcount == 0 ? "" : nowcount);
                            $.jBox.tip("只能mo一次的。", 'error');
                        }
                        else {
                            $.jBox.tip("+1", 'info');
                        }
                    });
                });
                action.children('.action-1').children('.count').html(data.LikeCount == 0 ? "" : data.LikeCount);
                action.children('.action-2').children('.comment').bind("click", function () { window.open("/Content/TaContent/" + data.MemberID + "/all/1"); });
                action.children('.action-2').children('.count').html(data.AnswerCount == 0 ? "" : data.AnswerCount);
            }
        });
    };

    return function (users) {
        var wrap_html = $('#j-newcomer').html(),
            item_html = $('#j-newcomer-item').html(),
            first_load = false,
            len = 0,
            user, city,
            $list,
            $items = $(),
            $item;
        if (!$el) {
            first_load = true;
            $el = $(wrap_html).appendTo('#wrap');
            setpos();

            // bind events
            $el.on('mouseenter', '.newcomer-item', popupDetail).on('mouseleave', '.newcomer-list', function () {
                ifmove = false;
                $(this).find('.detail').hide();
            });
        }

        if (users && (len = users.length) > 0) {
            $list = $el.children('.newcomer-list');
            var $preItems = $list.children('.newcomer-item');
            if (len > max_count) {
                len = max_count;
                user_pool = users.slice(0, MAX_COUNT);
                users = users.slice(0, len);
            }
            else if (user_pool.length > 0) {
                // 排在前面的是最新的用户
                user_pool = users.concat(user_pool.slice(0, -len));
            }
            else {
                user_pool = users;
            }

            var extraCount = $preItems.length + len - max_count;
            if (extraCount > 0) {
                // remove extra items
                $preItems.slice(-extraCount).remove();
            }
            for (var i = len - 1; i >= 0; i--) {
                user = users[i];
                user.Creater.ICONPath = photofunctions.getprofileiconphotoname(user.Creater.ICONPath);
                user.Creater.Sex = user.Creater.Sex === 1 ? '男' : '女';
                if (user.Creater.City) {
                    city = user.Creater.City.split('|');
                    user.Creater.City = city[0] === city[1] ? city[0] : city.join('');
                }
                $item = $(Mustache.render(item_html, users[i])).prependTo($list);
                $items = $items.add($item);
            }
            // for IE6
            if ($('html').hasClass('lt-ie7')) {
                DD_belatedPNG.fix('#newcomer .popover.nickname, #newcomer .detail');
            }
            else {
                // slidedown in IE6 will set the last item overflow as hidden, don't be surprised, WTF
                // slide effect, delay for IE7
                setTimeout(function () { $items.last().hide().slideDown(); }, 0);
            }
            if (!first_load) {
                // first load just don't show the nickname
                var $names = $list.children('.newcomer-item').find('.popover.nickname');
                $names.first().fadeIn();
                $names.slice(1).fadeOut();
            }
        }
    };
})();
