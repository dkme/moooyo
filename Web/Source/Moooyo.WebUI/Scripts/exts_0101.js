//js圆角 Begin ******************************************************************************************
/**
*  corner() takes a single string argument:  $('#myDiv').corner("effect corners width")
*
*  effect:  name of the effect to apply, such as round, bevel, notch, bite, etc (default is round). 
*  corners: one or more of: top, bottom, tr, tl, br, or bl.  (default is all corners)
*  width:   width of the effect; in the case of rounded corners this is the radius. 
*           specify this value using the px suffix such as 10px (yes, it must be pixels).
*/
; (function ($) {

    var style = document.createElement('div').style,
    moz = style['MozBorderRadius'] !== undefined,
    webkit = style['WebkitBorderRadius'] !== undefined,
    radius = style['borderRadius'] !== undefined || style['BorderRadius'] !== undefined,
    mode = document.documentMode || 0,
    noBottomFold = $.browser.msie && (($.browser.version < 8 && !mode) || mode < 8),

    expr = $.browser.msie && (function () {
        var div = document.createElement('div');
        try { div.style.setExpression('width', '0+0'); div.style.removeExpression('width'); }
        catch (e) { return false; }
        return true;
    })();

    $.support = $.support || {};
    $.support.borderRadius = moz || webkit || radius; // so you can do:  if (!$.support.borderRadius) $('#myDiv').corner();

    function sz(el, p) {
        return parseInt($.css(el, p)) || 0;
    };
    function hex2(s) {
        s = parseInt(s).toString(16);
        return (s.length < 2) ? '0' + s : s;
    };
    function gpc(node) {
        while (node) {
            var v = $.css(node, 'backgroundColor'), rgb;
            if (v && v != 'transparent' && v != 'rgba(0, 0, 0, 0)') {
                if (v.indexOf('rgb') >= 0) {
                    rgb = v.match(/\d+/g);
                    return '#' + hex2(rgb[0]) + hex2(rgb[1]) + hex2(rgb[2]);
                }
                return v;
            }
            if (node.nodeName.toLowerCase() == 'html')
                break;
            node = node.parentNode; // keep walking if transparent
        }
        return '#ffffff';
    };

    function getWidth(fx, i, width) {
        switch (fx) {
            case 'round': return Math.round(width * (1 - Math.cos(Math.asin(i / width))));
            case 'cool': return Math.round(width * (1 + Math.cos(Math.asin(i / width))));
            case 'sharp': return width - i;
            case 'bite': return Math.round(width * (Math.cos(Math.asin((width - i - 1) / width))));
            case 'slide': return Math.round(width * (Math.atan2(i, width / i)));
            case 'jut': return Math.round(width * (Math.atan2(width, (width - i - 1))));
            case 'curl': return Math.round(width * (Math.atan(i)));
            case 'tear': return Math.round(width * (Math.cos(i)));
            case 'wicked': return Math.round(width * (Math.tan(i)));
            case 'long': return Math.round(width * (Math.sqrt(i)));
            case 'sculpt': return Math.round(width * (Math.log((width - i - 1), width)));
            case 'dogfold':
            case 'dog': return (i & 1) ? (i + 1) : width;
            case 'dog2': return (i & 2) ? (i + 1) : width;
            case 'dog3': return (i & 3) ? (i + 1) : width;
            case 'fray': return (i % 2) * width;
            case 'notch': return width;
            case 'bevelfold':
            case 'bevel': return i + 1;
            case 'steep': return i / 2 + 1;
            case 'invsteep': return (width - i) / 2 + 1;
        }
    };

    $.fn.corner = function (options) {
        // in 1.3+ we can fix mistakes with the ready state
        if (this.length == 0) {
            if (!$.isReady && this.selector) {
                var s = this.selector, c = this.context;
                $(function () {
                    $(s, c).corner(options);
                });
            }
            return this;
        }

        return this.each(function (index) {
            var $this = $(this),
            // meta values override options
            o = [$this.attr($.fn.corner.defaults.metaAttr) || '', options || ''].join(' ').toLowerCase(),
            keep = /keep/.test(o),                       // keep borders?
            cc = ((o.match(/cc:(#[0-9a-f]+)/) || [])[1]),  // corner color
            sc = ((o.match(/sc:(#[0-9a-f]+)/) || [])[1]),  // strip color
            width = parseInt((o.match(/(\d+)px/) || [])[1]) || 10, // corner width
            re = /round|bevelfold|bevel|notch|bite|cool|sharp|slide|jut|curl|tear|fray|wicked|sculpt|long|dog3|dog2|dogfold|dog|invsteep|steep/,
            fx = ((o.match(re) || ['round'])[0]),
            fold = /dogfold|bevelfold/.test(o),
            edges = { T: 0, B: 1 },
            opts = {
                TL: /top|tl|left/.test(o), TR: /top|tr|right/.test(o),
                BL: /bottom|bl|left/.test(o), BR: /bottom|br|right/.test(o)
            },
            // vars used in func later
            strip, pad, cssHeight, j, bot, d, ds, bw, i, w, e, c, common, $horz;

            if (!opts.TL && !opts.TR && !opts.BL && !opts.BR)
                opts = { TL: 1, TR: 1, BL: 1, BR: 1 };

            // support native rounding
            if ($.fn.corner.defaults.useNative && fx == 'round' && (radius || moz || webkit) && !cc && !sc) {
                if (opts.TL)
                    $this.css(radius ? 'border-top-left-radius' : moz ? '-moz-border-radius-topleft' : '-webkit-border-top-left-radius', width + 'px');
                if (opts.TR)
                    $this.css(radius ? 'border-top-right-radius' : moz ? '-moz-border-radius-topright' : '-webkit-border-top-right-radius', width + 'px');
                if (opts.BL)
                    $this.css(radius ? 'border-bottom-left-radius' : moz ? '-moz-border-radius-bottomleft' : '-webkit-border-bottom-left-radius', width + 'px');
                if (opts.BR)
                    $this.css(radius ? 'border-bottom-right-radius' : moz ? '-moz-border-radius-bottomright' : '-webkit-border-bottom-right-radius', width + 'px');
                return;
            }

            strip = document.createElement('div');
            $(strip).css({
                overflow: 'hidden',
                height: '1px',
                minHeight: '1px',
                fontSize: '1px',
                backgroundColor: sc || 'transparent',
                borderStyle: 'solid'
            });

            pad = {
                T: parseInt($.css(this, 'paddingTop')) || 0, R: parseInt($.css(this, 'paddingRight')) || 0,
                B: parseInt($.css(this, 'paddingBottom')) || 0, L: parseInt($.css(this, 'paddingLeft')) || 0
            };

            if (typeof this.style.zoom != undefined) this.style.zoom = 1; // force 'hasLayout' in IE
            if (!keep) this.style.border = 'none';
            strip.style.borderColor = cc || gpc(this.parentNode);
            cssHeight = $(this).outerHeight();

            for (j in edges) {
                bot = edges[j];
                // only add stips if needed
                if ((bot && (opts.BL || opts.BR)) || (!bot && (opts.TL || opts.TR))) {
                    strip.style.borderStyle = 'none ' + (opts[j + 'R'] ? 'solid' : 'none') + ' none ' + (opts[j + 'L'] ? 'solid' : 'none');
                    d = document.createElement('div');
                    $(d).addClass('jquery-corner');
                    ds = d.style;

                    bot ? this.appendChild(d) : this.insertBefore(d, this.firstChild);

                    if (bot && cssHeight != 'auto') {
                        if ($.css(this, 'position') == 'static')
                            this.style.position = 'relative';
                        ds.position = 'absolute';
                        ds.bottom = ds.left = ds.padding = ds.margin = '0';
                        if (expr)
                            ds.setExpression('width', 'this.parentNode.offsetWidth');
                        else
                            ds.width = '100%';
                    }
                    else if (!bot && $.browser.msie) {
                        if ($.css(this, 'position') == 'static')
                            this.style.position = 'relative';
                        ds.position = 'absolute';
                        ds.top = ds.left = ds.right = ds.padding = ds.margin = '0';

                        // fix ie6 problem when blocked element has a border width
                        if (expr) {
                            bw = sz(this, 'borderLeftWidth') + sz(this, 'borderRightWidth');
                            ds.setExpression('width', 'this.parentNode.offsetWidth - ' + bw + '+ "px"');
                        }
                        else
                            ds.width = '100%';
                    }
                    else {
                        ds.position = 'relative';
                        ds.margin = !bot ? '-' + pad.T + 'px -' + pad.R + 'px ' + (pad.T - width) + 'px -' + pad.L + 'px' :
                                        (pad.B - width) + 'px -' + pad.R + 'px -' + pad.B + 'px -' + pad.L + 'px';
                    }

                    for (i = 0; i < width; i++) {
                        w = Math.max(0, getWidth(fx, i, width));
                        e = strip.cloneNode(false);
                        e.style.borderWidth = '0 ' + (opts[j + 'R'] ? w : 0) + 'px 0 ' + (opts[j + 'L'] ? w : 0) + 'px';
                        bot ? d.appendChild(e) : d.insertBefore(e, d.firstChild);
                    }

                    if (fold && $.support.boxModel) {
                        if (bot && noBottomFold) continue;
                        for (c in opts) {
                            if (!opts[c]) continue;
                            if (bot && (c == 'TL' || c == 'TR')) continue;
                            if (!bot && (c == 'BL' || c == 'BR')) continue;

                            common = { position: 'absolute', border: 'none', margin: 0, padding: 0, overflow: 'hidden', backgroundColor: strip.style.borderColor };
                            $horz = $('<div/>').css(common).css({ width: width + 'px', height: '1px' });
                            switch (c) {
                                case 'TL': $horz.css({ bottom: 0, left: 0 }); break;
                                case 'TR': $horz.css({ bottom: 0, right: 0 }); break;
                                case 'BL': $horz.css({ top: 0, left: 0 }); break;
                                case 'BR': $horz.css({ top: 0, right: 0 }); break;
                            }
                            d.appendChild($horz[0]);

                            var $vert = $('<div/>').css(common).css({ top: 0, bottom: 0, width: '1px', height: width + 'px' });
                            switch (c) {
                                case 'TL': $vert.css({ left: width }); break;
                                case 'TR': $vert.css({ right: width }); break;
                                case 'BL': $vert.css({ left: width }); break;
                                case 'BR': $vert.css({ right: width }); break;
                            }
                            d.appendChild($vert[0]);
                        }
                    }
                }
            }
        });
    };

    $.fn.uncorner = function () {
        if (radius || moz || webkit)
            this.css(radius ? 'border-radius' : moz ? '-moz-border-radius' : '-webkit-border-radius', 0);
        $('div.jquery-corner', this).remove();
        return this;
    };

    // expose options
    $.fn.corner.defaults = {
        useNative: true, // true if plugin should attempt to use native browser support for border radius rounding
        metaAttr: 'data-corner' // name of meta attribute to use for options
    };

})(jQuery);

//js圆角 End ******************************************************************************************


//html5标签支持 Begin *******************************************************************************

(function () { var html5_tag = 'section,article,aside,header,footer,nav,dialog,figure', arr = html5_tag.split(','); for (var i = arr.length; i--; ) { document.createElement(arr[i]); } })();

//html5标签支持 End **********************************************************************************


/* 复制到剪贴版处理代码开始 ****************************************************************************/

/*
* zClip :: jQuery ZeroClipboard v1.1.1
* http://steamdev.com/zclip
*
* Copyright 2011, SteamDev
* Released under the MIT license.
* http://www.opensource.org/licenses/mit-license.php
*
* Date: Wed Jun 01, 2011
*/

(function ($) {

    $.fn.zclip = function (params) {

        if (typeof params == "object" && !params.length) {

            var settings = $.extend({

                path: 'ZeroClipboard.swf',
                copy: null,
                beforeCopy: null,
                afterCopy: null,
                clickAfter: true,
                setHandCursor: true,
                setCSSEffects: true

            }, params);
            return this.each(function () {

                var o = $(this);

                if (o.is(':visible') && (typeof settings.copy == 'string' || $.isFunction(settings.copy))) {

                    ZeroClipboard.setMoviePath(settings.path);
                    var clip = new ZeroClipboard.Client();

                    if ($.isFunction(settings.copy)) {
                        o.bind('zClip_copy', settings.copy);
                    }
                    if ($.isFunction(settings.beforeCopy)) {
                        o.bind('zClip_beforeCopy', settings.beforeCopy);
                    }
                    if ($.isFunction(settings.afterCopy)) {
                        o.bind('zClip_afterCopy', settings.afterCopy);
                    }

                    clip.setHandCursor(settings.setHandCursor);
                    clip.setCSSEffects(settings.setCSSEffects);
                    clip.addEventListener('mouseOver', function (client) {
                        o.trigger('mouseenter');
                    });
                    clip.addEventListener('mouseOut', function (client) {
                        o.trigger('mouseleave');
                    });
                    clip.addEventListener('mouseDown', function (client) {

                        o.trigger('mousedown');

                        if (!$.isFunction(settings.copy)) {
                            clip.setText(settings.copy);
                        } else {
                            clip.setText(o.triggerHandler('zClip_copy'));
                        }

                        if ($.isFunction(settings.beforeCopy)) {
                            o.trigger('zClip_beforeCopy');
                        }

                    });

                    clip.addEventListener('complete', function (client, text) {

                        if ($.isFunction(settings.afterCopy)) {

                            o.trigger('zClip_afterCopy');

                        } else {
                            if (text.length > 500) {
                                text = text.substr(0, 500) + "...\n\n(" + (text.length - 500) + " characters not shown)";
                            }

                            o.removeClass('hover');
                            alert("Copied text to clipboard:\n\n " + text);
                        }

                        if (settings.clickAfter) {
                            o.trigger('click');
                        }

                    });
                    clip.glue(o[0], o.parent()[0]);

                    $(window).bind('load resize', function () { clip.reposition(); });
                }

            });

        } else if (typeof params == "string") {

            return this.each(function () {

                var o = $(this);

                params = params.toLowerCase();
                var zclipId = o.data('zclipId');
                var clipElm = $('#' + zclipId + '.zclip');

                if (params == "remove") {

                    clipElm.remove();
                    o.removeClass('active hover');

                } else if (params == "hide") {

                    clipElm.hide();
                    o.removeClass('active hover');

                } else if (params == "show") {

                    clipElm.show();

                }

            });
        }
    }
})(jQuery);


// ZeroClipboard
// Simple Set Clipboard System
// Author: Joseph Huckaby
var ZeroClipboard = {

    version: "1.0.7",
    clients: {},
    // registered upload clients on page, indexed by id
    moviePath: 'ZeroClipboard.swf',
    // URL to movie
    nextId: 1,
    // ID of next movie
    $: function (thingy) {
        // simple DOM lookup utility function
        if (typeof (thingy) == 'string') thingy = document.getElementById(thingy);
        if (!thingy.addClass) {
            // extend element with a few useful methods
            thingy.hide = function () {
                this.style.display = 'none';
            };
            thingy.show = function () {
                this.style.display = '';
            };
            thingy.addClass = function (name) {
                this.removeClass(name);
                this.className += ' ' + name;
            };
            thingy.removeClass = function (name) {
                var classes = this.className.split(/\s+/);
                var idx = -1;
                for (var k = 0; k < classes.length; k++) {
                    if (classes[k] == name) {
                        idx = k;
                        k = classes.length;
                    }
                }
                if (idx > -1) {
                    classes.splice(idx, 1);
                    this.className = classes.join(' ');
                }
                return this;
            };
            thingy.hasClass = function (name) {
                return !!this.className.match(new RegExp("\\s*" + name + "\\s*"));
            };
        }
        return thingy;
    },

    setMoviePath: function (path) {
        // set path to ZeroClipboard.swf
        this.moviePath = path;
    },

    dispatch: function (id, eventName, args) {
        // receive event from flash movie, send to client		
        var client = this.clients[id];
        if (client) {
            client.receiveEvent(eventName, args);
        }
    },

    register: function (id, client) {
        // register new client to receive events
        this.clients[id] = client;
    },

    getDOMObjectPosition: function (obj, stopObj) {
        // get absolute coordinates for dom element
        var info = {
            left: 0,
            top: 0,
            width: obj.width ? obj.width : obj.offsetWidth,
            height: obj.height ? obj.height : obj.offsetHeight
        };

        if (obj && (obj != stopObj)) {
            info.left += obj.offsetLeft;
            info.top += obj.offsetTop;
        }

        return info;
    },

    Client: function (elem) {
        // constructor for new simple upload client
        this.handlers = {};

        // unique ID
        this.id = ZeroClipboard.nextId++;
        this.movieId = 'ZeroClipboardMovie_' + this.id;

        // register client with singleton to receive flash events
        ZeroClipboard.register(this.id, this);

        // create movie
        if (elem) this.glue(elem);
    }
};

ZeroClipboard.Client.prototype = {

    id: 0,
    // unique ID for us
    ready: false,
    // whether movie is ready to receive events or not
    movie: null,
    // reference to movie object
    clipText: '',
    // text to copy to clipboard
    handCursorEnabled: true,
    // whether to show hand cursor, or default pointer cursor
    cssEffects: true,
    // enable CSS mouse effects on dom container
    handlers: null,
    // user event handlers
    glue: function (elem, appendElem, stylesToAdd) {
        // glue to DOM element
        // elem can be ID or actual DOM element object
        this.domElement = ZeroClipboard.$(elem);

        // float just above object, or zIndex 99 if dom element isn't set
        var zIndex = 99;
        if (this.domElement.style.zIndex) {
            zIndex = parseInt(this.domElement.style.zIndex, 10) + 1;
        }

        if (typeof (appendElem) == 'string') {
            appendElem = ZeroClipboard.$(appendElem);
        } else if (typeof (appendElem) == 'undefined') {
            appendElem = document.getElementsByTagName('body')[0];
        }

        // find X/Y position of domElement
        var box = ZeroClipboard.getDOMObjectPosition(this.domElement, appendElem);

        // create floating DIV above element
        this.div = document.createElement('div');
        this.div.className = "zclip";
        this.div.id = "zclip-" + this.movieId;
        $(this.domElement).data('zclipId', 'zclip-' + this.movieId);
        var style = this.div.style;
        //style.position = 'absolute';
        //style.left = '' + box.left + 'px';
        //style.top = '' + box.top + 'px';
        style.width = '' + box.width + 'px';
        style.height = '' + box.height + 'px';
        //style.zIndex = zIndex;
        if ($.browser.msie) { style.styleFloat = "left"; } else { style.cssFloat = "left"; }
        style.marginLeft = "-" + box.width + "px";

        if (typeof (stylesToAdd) == 'object') {
            for (addedStyle in stylesToAdd) {
                style[addedStyle] = stylesToAdd[addedStyle];
            }
        }

        // style.backgroundColor = '#f00'; // debug
        appendElem.appendChild(this.div);

        this.div.innerHTML = this.getHTML(box.width, box.height);
    },
    getHTML: function (width, height) {
        // return HTML for movie
        var html = '';
        var flashvars = 'id=' + this.id + '&width=' + width + '&height=' + height;
        if (navigator.userAgent.match(/MSIE/)) {
            // IE gets an OBJECT tag
            var protocol = location.href.match(/^https/i) ? 'https://' : 'http://';
            html += '<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="' + protocol + 'download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0" width="' + width + '" height="' + height + '" id="' + this.movieId + '" align="middle"><param name="allowScriptAccess" value="always" /><param name="allowFullScreen" value="false" /><param name="movie" value="' + ZeroClipboard.moviePath + '" /><param name="loop" value="false" /><param name="menu" value="false" /><param name="quality" value="best" /><param name="bgcolor" value="#ffffff" /><param name="flashvars" value="' + flashvars + '"/><param name="wmode" value="transparent"/></object>';
        } else {
            // all other browsers get an EMBED tag
            html += '<embed id="' + this.movieId + '" src="' + ZeroClipboard.moviePath + '" loop="false" menu="false" quality="best" bgcolor="#ffffff" width="' + width + '" height="' + height + '" name="' + this.movieId + '" align="middle" allowScriptAccess="always" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="' + flashvars + '" wmode="transparent" />';
        }
        return html;
    },
    hide: function () {
        // temporarily hide floater offscreen
        if (this.div) {
            this.div.style.left = '-2000px';
        }
    },
    show: function () {
        // show ourselves after a call to hide()
        this.reposition();
    },
    destroy: function () {
        // destroy control and floater
        if (this.domElement && this.div) {
            this.hide();
            this.div.innerHTML = '';

            var body = document.getElementsByTagName('body')[0];
            try {
                body.removeChild(this.div);
            } catch (e) {
                ;
            }

            this.domElement = null;
            this.div = null;
        }
    },
    reposition: function (elem) {
        // reposition our floating div, optionally to new container
        // warning: container CANNOT change size, only position
        if (elem) {
            this.domElement = ZeroClipboard.$(elem);
            if (!this.domElement) this.hide();
        }
        if (this.domElement && this.div) {
            var box = ZeroClipboard.getDOMObjectPosition(this.domElement);
            var style = this.div.style;
            style.left = '' + box.left + 'px';
            style.top = '' + box.top + 'px';
        }
    },
    setText: function (newText) {
        // set text to be copied to clipboard
        this.clipText = newText;
        if (this.ready) {
            this.movie.setText(newText);
        }
    },
    addEventListener: function (eventName, func) {
        // add user event listener for event
        // event types: load, queueStart, fileStart, fileComplete, queueComplete, progress, error, cancel
        eventName = eventName.toString().toLowerCase().replace(/^on/, '');
        if (!this.handlers[eventName]) {
            this.handlers[eventName] = [];
        }
        this.handlers[eventName].push(func);
    },
    setHandCursor: function (enabled) {
        // enable hand cursor (true), or default arrow cursor (false)
        this.handCursorEnabled = enabled;
        if (this.ready) {
            this.movie.setHandCursor(enabled);
        }
    },
    setCSSEffects: function (enabled) {
        // enable or disable CSS effects on DOM container
        this.cssEffects = !!enabled;
    },
    receiveEvent: function (eventName, args) {
        // receive event from flash
        eventName = eventName.toString().toLowerCase().replace(/^on/, '');

        // special behavior for certain events
        switch (eventName) {
            case 'load':
                // movie claims it is ready, but in IE this isn't always the case...
                // bug fix: Cannot extend EMBED DOM elements in Firefox, must use traditional function
                this.movie = document.getElementById(this.movieId);
                if (!this.movie) {
                    var self = this;
                    setTimeout(function () {
                        self.receiveEvent('load', null);
                    }, 1);
                    return;
                }
                // firefox on pc needs a "kick" in order to set these in certain cases
                if (!this.ready && navigator.userAgent.match(/Firefox/) && navigator.userAgent.match(/Windows/)) {
                    var self = this;
                    setTimeout(function () {
                        self.receiveEvent('load', null);
                    }, 100);
                    this.ready = true;
                    return;
                }
                this.ready = true;
                try {
                    this.movie.setText(this.clipText);
                } catch (e) { }
                try {
                    this.movie.setHandCursor(this.handCursorEnabled);
                } catch (e) { }
                break;
            case 'mouseover':
                if (this.domElement && this.cssEffects) {
                    this.domElement.addClass('hover');
                    if (this.recoverActive) {
                        this.domElement.addClass('active');
                    }


                }
                break;
            case 'mouseout':
                if (this.domElement && this.cssEffects) {
                    this.recoverActive = false;
                    if (this.domElement.hasClass('active')) {
                        this.domElement.removeClass('active');
                        this.recoverActive = true;
                    }
                    this.domElement.removeClass('hover');
                }
                break;
            case 'mousedown':
                if (this.domElement && this.cssEffects) {
                    this.domElement.addClass('active');
                }
                break;
            case 'mouseup':
                if (this.domElement && this.cssEffects) {
                    this.domElement.removeClass('active');
                    this.recoverActive = false;
                }
                break;
        } // switch eventName
        if (this.handlers[eventName]) {
            for (var idx = 0, len = this.handlers[eventName].length; idx < len; idx++) {
                var func = this.handlers[eventName][idx];

                if (typeof (func) == 'function') {
                    // actual function reference
                    func(this, args);
                } else if ((typeof (func) == 'object') && (func.length == 2)) {
                    // PHP style object + method, i.e. [myObject, 'myMethod']
                    func[0][func[1]](this, args);
                } else if (typeof (func) == 'string') {
                    // name of function
                    window[func](this, args);
                }
            } // foreach event handler defined
        } // user defined handler for event
    }
};

/* 复制到剪贴版处理代码结束 *******************************************************************************************/


/* Jquery 图片旋转插件开始 jquery.rotate.js ****************************************************************************/

jQuery.fn.rotate = function (angle, whence) {
    var p = this.get(0);

    // we store the angle inside the image tag for persistence
    if (!whence) {
        p.angle = ((p.angle == undefined ? 0 : p.angle) + angle) % 360;
    } else {
        p.angle = angle;
    }

    if (p.angle >= 0) {
        var rotation = Math.PI * p.angle / 180;
    } else {
        var rotation = Math.PI * (360 + p.angle) / 180;
    }
    var costheta = Math.round(Math.cos(rotation) * 1000) / 1000;
    var sintheta = Math.round(Math.sin(rotation) * 1000) / 1000;
    //alert(costheta+","+sintheta);

    if (document.all && !window.opera) {
        var canvas = document.createElement('img');

        canvas.src = p.src;
        canvas.height = p.height;
        canvas.width = p.width;

        canvas.style.filter = "progid:DXImageTransform.Microsoft.Matrix(M11=" + costheta + ",M12=" + (-sintheta) + ",M21=" + sintheta + ",M22=" + costheta + ",SizingMethod='auto expand')";
    } else {
        var canvas = document.createElement('canvas');
        if (!p.oImage) {
            canvas.oImage = new Image();
            canvas.oImage.src = p.src;
        } else {
            canvas.oImage = p.oImage;
        }

        canvas.style.width = canvas.width = Math.abs(costheta * canvas.oImage.width) + Math.abs(sintheta * canvas.oImage.height);
        canvas.style.height = canvas.height = Math.abs(costheta * canvas.oImage.height) + Math.abs(sintheta * canvas.oImage.width);

        var context = canvas.getContext('2d');
        context.save();
        if (rotation <= Math.PI / 2) {
            context.translate(sintheta * canvas.oImage.height, 0);
        } else if (rotation <= Math.PI) {
            context.translate(canvas.width, -costheta * canvas.oImage.height);
        } else if (rotation <= 1.5 * Math.PI) {
            context.translate(-costheta * canvas.oImage.width, canvas.height);
        } else {
            context.translate(0, -sintheta * canvas.oImage.width);
        }
        context.rotate(rotation);
        context.drawImage(canvas.oImage, 0, 0, canvas.oImage.width, canvas.oImage.height);
        context.restore();
    }
    canvas.id = p.id;
    canvas.angle = p.angle;
    p.parentNode.replaceChild(canvas, p);
}

jQuery.fn.rotateRight = function (angle) {
    this.rotate(angle == undefined ? 90 : angle);
}

jQuery.fn.rotateLeft = function (angle) {
    this.rotate(angle == undefined ? -90 : -angle);
}

/* Jquery 图片旋转插件结束 jquery.rotate.js ****************************************************************************/


/* Jquery Cookie Begin jquery.cookie.js ****************************************************************************/
/**
* Cookie plugin
*
* Copyright (c) 2006 Klaus Hartl (stilbuero.de)
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
*
*/

/**
* Create a cookie with the given name and value and other optional parameters.
*
* @example $.cookie('the_cookie', 'the_value');
* @desc Set the value of a cookie.
* @example $.cookie('the_cookie', 'the_value', { expires: 7, path: '/', domain: 'jquery.com', secure: true });
* @desc Create a cookie with all available options.
* @example $.cookie('the_cookie', 'the_value');
* @desc Create a session cookie.
* @example $.cookie('the_cookie', null);
* @desc Delete a cookie by passing null as value. Keep in mind that you have to use the same path and domain
*       used when the cookie was set.
*
* @param String name The name of the cookie.
* @param String value The value of the cookie.
* @param Object options An object literal containing key/value pairs to provide optional cookie attributes.
* @option Number|Date expires Either an integer specifying the expiration date from now on in days or a Date object.
*                             If a negative value is specified (e.g. a date in the past), the cookie will be deleted.
*                             If set to null or omitted, the cookie will be a session cookie and will not be retained
*                             when the the browser exits.
* @option String path The value of the path atribute of the cookie (default: path of page that created the cookie).
* @option String domain The value of the domain attribute of the cookie (default: domain of page that created the cookie).
* @option Boolean secure If true, the secure attribute of the cookie will be set and the cookie transmission will
*                        require a secure protocol (like HTTPS).
* @type undefined
*
* @name $.cookie
* @cat Plugins/Cookie
* @author Klaus Hartl/klaus.hartl@stilbuero.de
*/

/**
* Get the value of a cookie with the given name.
*
* @example $.cookie('the_cookie');
* @desc Get the value of a cookie.
*
* @param String name The name of the cookie.
* @return The value of the cookie.
* @type String
*
* @name $.cookie
* @cat Plugins/Cookie
* @author Klaus Hartl/klaus.hartl@stilbuero.de
*/
jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        options = options || {};
        if (value === null) {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        // CAUTION: Needed to parenthesize options.path and options.domain
        // in the following expressions, otherwise they evaluate to undefined
        // in the packed version for some reason...
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

/* Jquery Cookie End jquery.cookie.js ****************************************************************************/


/* jquery.pager.js begin ****************************************************************************/

/*
* jQuery pager plugin
* Version 1.0 (12/22/2008)
* @requires jQuery v1.2.6 or later
*
* Example at: http://jonpauldavies.github.com/JQuery/Pager/PagerDemo.html
*
* Copyright (c) 2008-2009 Jon Paul Davies
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
* 
* Read the related blog post and contact the author at http://www.j-dee.com/2008/12/22/jquery-pager-plugin/
*
* This version is far from perfect and doesn't manage it's own state, therefore contributions are more than welcome!
*
* Usage: .pager({ pagenumber: 1, pagecount: 15, buttonClickCallback: PagerClickTest });
*
* Where pagenumber is the visible page number
*       pagecount is the total number of pages to display
*       buttonClickCallback is the method to fire when a pager button is clicked.
*
* buttonClickCallback signiture is PagerClickTest = function(pageclickednumber) 
* Where pageclickednumber is the number of the page clicked in the control.
*
* The included Pager.CSS file is a dependancy but can obviously tweaked to your wishes
* Tested in IE6 IE7 Firefox & Safari. Any browser strangeness, please report.
*/
(function ($) {

    $.fn.pager = function (options) {

        var opts = $.extend({}, $.fn.pager.defaults, options);

        return this.each(function () {

            // empty out the destination element and then render out the pager with the supplied options
            $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagecount), options.buttonClickCallback));

            // specify correct cursor activity
            $('.pages li').mouseover(function () { document.body.style.cursor = "pointer"; }).mouseout(function () { document.body.style.cursor = "auto"; });
        });
    };

    // render and return the pager with the supplied options
    function renderpager(pagenumber, pagecount, buttonClickCallback) {

        // setup $pager to hold render
        var $pager = $('<ul class="pages"></ul>');

        // add in the previous and next buttons
        $pager.append(renderButton('首页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('上一页', pagenumber, pagecount, buttonClickCallback));

        // pager currently only handles 10 viewable pages ( could be easily parameterized, maybe in next version ) so handle edge cases
        var startPoint = 1;
        var endPoint = 9;

        if (pagenumber > 4) {
            startPoint = pagenumber - 4;
            endPoint = pagenumber + 4;
        }

        if (endPoint > pagecount) {
            startPoint = pagecount - 8;
            endPoint = pagecount;
        }

        if (startPoint < 1) {
            startPoint = 1;
        }

        // loop thru visible pages and render buttons
        for (var page = startPoint; page <= endPoint; page++) {

            var currentButton = $('<li class="page-number">' + (page) + '</li>');

            page == pagenumber ? currentButton.addClass('pgCurrent') : currentButton.click(function () { buttonClickCallback(this.firstChild.data); });
            currentButton.appendTo($pager);
        }

        // render in the next and last buttons before returning the whole rendered control back.
        $pager.append(renderButton('下一页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('尾页', pagenumber, pagecount, buttonClickCallback));

        return $pager;
    }

    // renders and returns a 'specialized' button, ie 'next', 'previous' etc. rather than a page number button
    function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback) {

        var $Button = $('<li class="pgNext">' + buttonLabel + '</li>');

        var destPage = 1;

        // work out destination page for required button type
        switch (buttonLabel) {
            case "首页":
                destPage = 1;
                break;
            case "上一页":
                destPage = pagenumber - 1;
                break;
            case "下一页":
                destPage = pagenumber + 1;
                break;
            case "尾页":
                destPage = pagecount;
                break;
        }

        // disable and 'grey' out buttons if not needed.
        if (buttonLabel == "首页" || buttonLabel == "上一页") {
            pagenumber <= 1 ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }
        else {
            pagenumber >= pagecount ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }

        return $Button;
    }

    // pager defaults. hardly worth bothering with in this case but used as placeholder for expansion in the next version
    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1
    };

})(jQuery);

/* jquery.pager.js end ****************************************************************************/


/* jquery.pager.pz.js begin ****************************************************************************/

(function ($) {
    $.fn.pager = function (options) {
        var opts = $.extend({}, $.fn.pager.defaults, options);
        return this.each(function () {
            $(this).empty().append(renderpager(parseInt(options.pagenumber), parseInt(options.pagecount), options.buttonClickCallback));
            $('.pages li').mouseover(function () { document.body.style.cursor = "pointer"; }).mouseout(function () { document.body.style.cursor = "auto"; });
        });
    };
    function renderpager(pagenumber, pagecount, buttonClickCallback) {
        var $pager = $('<ul class="pages"></ul>');
        $pager.append(renderButton('首页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('上一页', pagenumber, pagecount, buttonClickCallback));
        var startPoint = 1;
        var endPoint = 9;
        if (pagenumber > 4) {
            startPoint = pagenumber - 4;
            endPoint = pagenumber + 4;
        }
        if (endPoint > pagecount) {
            startPoint = pagecount - 8;
            endPoint = pagecount;
        }
        if (startPoint < 1) {
            startPoint = 1;
        }
        for (var page = startPoint; page <= endPoint; page++) {
            var currentButton = $('<li class="page-number">' + (page) + '</li>');
            page == pagenumber ? currentButton.addClass('pgCurrent') : currentButton.click(function () { buttonClickCallback(this.firstChild.data); });
            currentButton.appendTo($pager);
        }
        $pager.append(renderButton('下一页', pagenumber, pagecount, buttonClickCallback)).append(renderButton('尾页', pagenumber, pagecount, buttonClickCallback));
        return $pager;
    }
    function renderButton(buttonLabel, pagenumber, pagecount, buttonClickCallback) {
        var $Button = $('<li class="pgNext">' + buttonLabel + '</li>');
        var destPage = 1;
        switch (buttonLabel) {
            case "首页":
                destPage = 1;
                break;
            case "上一页":
                destPage = pagenumber - 1;
                break;
            case "下一页":
                destPage = pagenumber + 1;
                break;
            case "尾页":
                destPage = pagecount;
                break;
        }
        if (buttonLabel == "首页" || buttonLabel == "上一页") {
            pagenumber <= 1 ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }
        else {
            pagenumber >= pagecount ? $Button.addClass('pgEmpty') : $Button.click(function () { buttonClickCallback(destPage); });
        }
        return $Button;
    }
    $.fn.pager.defaults = {
        pagenumber: 1,
        pagecount: 1
    };
})(jQuery);

/* jquery.pager.pz.js end ****************************************************************************/


/* jquery.scrollLoading.js begin ****************************************************************************/

/*
* jquery.scrollLoading.js
* by zhangxinxu  http://www.zhangxinxu.com
* 2010-11-19 v1.0
*/
(function ($) {
    $.fn.scrollLoading = function (options) {
        var defaults = {
            attr: "data-url"
        };
        var params = $.extend({}, defaults, options || {});
        params.cache = [];
        $(this).each(function () {
            var node = this.nodeName.toLowerCase(), url = $(this).attr(params["attr"]);
            if (!url) { return; }
            //重组
            var data = {
                obj: $(this),
                tag: node,
                url: url
            };
            params.cache.push(data);
        });

        //动态显示数据
        var loading = function () {
            var st = $(window).scrollTop(), sth = st + $(window).height();
            $.each(params.cache, function (i, data) {
                var o = data.obj, tag = data.tag, url = data.url;
                if (o) {
                    post = o.position().top; posb = post + o.height();
                    if ((post > st && post < sth) || (posb > st && posb < sth)) {
                        //在浏览器窗口内
                        if (tag === "img") {
                            //图片，改变src
                            o.attr("src", url);
                        } else {
                            o.load(url);
                        }
                        data.obj = null;
                    }
                }
            });
            return false;
        };

        //事件触发
        //加载完毕即执行
        loading();
        //滚动执行
        $(window).bind("scroll", loading);
    };
})(jQuery);

/* jquery.scrollLoading.js end ****************************************************************************/