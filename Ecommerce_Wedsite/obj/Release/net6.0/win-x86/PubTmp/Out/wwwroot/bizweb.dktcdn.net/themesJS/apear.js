(function (e) { e.fn.appear = function (t, n) { var r = e.extend({ data: undefined, one: true, accX: 0, accY: 0 }, n); return this.each(function () { var n = e(this); n.appeared = false; if (!t) { n.trigger("appear", r.data); return } var i = e(window); var s = function () { if (!n.is(":visible")) { n.appeared = false; return } var e = i.scrollLeft(); var t = i.scrollTop(); var s = n.offset(); var o = s.left; var u = s.top; var a = r.accX; var f = r.accY; var l = n.height(); var c = i.height(); var h = n.width(); var p = i.width(); if (u + l + f >= t && u <= t + c + f && o + h + a >= e && o <= e + p + a) { if (!n.appeared) n.trigger("appear", r.data) } else { n.appeared = false } }; var o = function () { n.appeared = true; if (r.one) { i.unbind("scroll", s); var o = e.inArray(s, e.fn.appear.checks); if (o >= 0) e.fn.appear.checks.splice(o, 1) } t.apply(this, arguments) }; if (r.one) n.one("appear", r.data, o); else n.bind("appear", r.data, o); i.scroll(s); e.fn.appear.checks.push(s); s() }) }; e.extend(e.fn.appear, { checks: [], timeout: null, checkAll: function () { var t = e.fn.appear.checks.length; if (t > 0) while (t--) e.fn.appear.checks[t]() }, run: function () { if (e.fn.appear.timeout) clearTimeout(e.fn.appear.timeout); e.fn.appear.timeout = setTimeout(e.fn.appear.checkAll, 20) } }); e.each(["append", "prepend", "after", "before", "attr", "removeAttr", "addClass", "removeClass", "toggleClass", "remove", "css", "show", "hide"], function (t, n) { var r = e.fn[n]; if (r) { e.fn[n] = function () { var t = r.apply(this, arguments); e.fn.appear.run(); return t } } }) })(jQuery);


/*
 jQuery placeholderTypewriter plugin
 ===================================
 Author: Bjoern Diekert <https://github.com/bdiekert>
 Version: 1.0
 License: Unlicense <http://unlicense.org>
 */
(function ($) {
    "use strict";

    $.fn.placeholderTypewriter = function (options) {

        // Plugin Settings
        var settings = $.extend({
            delay: 100,
            pause: 1000,
            text: []
        }, options);

        // Type given string in placeholder
        function typeString($target, index, cursorPosition, callback) {

            // Get text
            var text = settings.text[index];

            // Get placeholder, type next character
            var placeholder = $target.attr('placeholder');
            $target.attr('placeholder', placeholder + text[cursorPosition]);

            // Type next character
            if (cursorPosition < text.length - 1) {
                setTimeout(function () {
                    typeString($target, index, cursorPosition + 1, callback);
                }, settings.delay);
                return true;
            }

            // Callback if animation is finished
            callback();
        }

        // Delete string in placeholder
        function deleteString($target, callback) {

            // Get placeholder
            var placeholder = $target.attr('placeholder');
            var length = placeholder.length;

            // Delete last character
            $target.attr('placeholder', placeholder.substr(0, length - 1));

            // Delete next character
            if (length > 1) {
                setTimeout(function () {
                    deleteString($target, callback)
                }, settings.delay);
                return true;
            }

            // Callback if animation is finished
            callback();
        }

        // Loop typing animation
        function loopTyping($target, index) {

            // Clear Placeholder
            $target.attr('placeholder', '');

            // Type string
            typeString($target, index, 0, function () {

                // Pause before deleting string
                setTimeout(function () {

                    // Delete string
                    deleteString($target, function () {
                        // Start loop over
                        loopTyping($target, (index + 1) % settings.text.length)
                    })

                }, settings.pause);
            })

        }

        // Run placeholderTypewriter on every given field
        return this.each(function () {

            loopTyping($(this), 0);
        });

    };

}(jQuery));

/*!
 * The Final Countdown for jQuery v2.0.4 (http://hilios.github.io/jQuery.countdown/)
 * Copyright (c) 2014 Edson Hilios
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
 * the Software, and to permit persons to whom the Software is furnished to do so,
 * subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
!function (a) { "use strict"; "function" == typeof define && define.amd ? define(["jquery"], a) : a(jQuery) }(function (a) { "use strict"; function b(a) { if (a instanceof Date) return a; if (String(a).match(g)) return String(a).match(/^[0-9]*$/) && (a = Number(a)), String(a).match(/\-/) && (a = String(a).replace(/\-/g, "/")), new Date(a); throw new Error("Couldn't cast `" + a + "` to a date object.") } function c(a) { return function (b) { var c = b.match(/%(-|!)?[A-Z]{1}(:[^;]+;)?/gi); if (c) for (var e = 0, f = c.length; f > e; ++e) { var g = c[e].match(/%(-|!)?([a-zA-Z]{1})(:[^;]+;)?/), i = new RegExp(g[0]), j = g[1] || "", k = g[3] || "", l = null; g = g[2], h.hasOwnProperty(g) && (l = h[g], l = Number(a[l])), null !== l && ("!" === j && (l = d(k, l)), "" === j && 10 > l && (l = "0" + l.toString()), b = b.replace(i, l.toString())) } return b = b.replace(/%%/, "%") } } function d(a, b) { var c = "s", d = ""; return a && (a = a.replace(/(:|;|\s)/gi, "").split(/\,/), 1 === a.length ? c = a[0] : (d = a[0], c = a[1])), 1 === Math.abs(b) ? d : c } var e = 100, f = [], g = []; g.push(/^[0-9]*$/.source), g.push(/([0-9]{1,2}\/){2}[0-9]{4}( [0-9]{1,2}(:[0-9]{2}){2})?/.source), g.push(/[0-9]{4}([\/\-][0-9]{1,2}){2}( [0-9]{1,2}(:[0-9]{2}){2})?/.source), g = new RegExp(g.join("|")); var h = { Y: "years", m: "months", w: "weeks", d: "days", D: "totalDays", H: "hours", M: "minutes", S: "seconds" }, i = function (b, c, d) { this.el = b, this.$el = a(b), this.interval = null, this.offset = {}, this.instanceNumber = f.length, f.push(this), this.$el.data("countdown-instance", this.instanceNumber), d && (this.$el.on("update.countdown", d), this.$el.on("stoped.countdown", d), this.$el.on("finish.countdown", d)), this.setFinalDate(c), this.start() }; a.extend(i.prototype, { start: function () { null !== this.interval && clearInterval(this.interval); var a = this; this.update(), this.interval = setInterval(function () { a.update.call(a) }, e) }, stop: function () { clearInterval(this.interval), this.interval = null, this.dispatchEvent("stoped") }, pause: function () { this.stop.call(this) }, resume: function () { this.start.call(this) }, remove: function () { this.stop(), f[this.instanceNumber] = null, delete this.$el.data().countdownInstance }, setFinalDate: function (a) { this.finalDate = b(a) }, update: function () { return 0 === this.$el.closest("html").length ? void this.remove() : (this.totalSecsLeft = this.finalDate.getTime() - (new Date).getTime(), this.totalSecsLeft = Math.ceil(this.totalSecsLeft / 1e3), this.totalSecsLeft = this.totalSecsLeft < 0 ? 0 : this.totalSecsLeft, this.offset = { seconds: this.totalSecsLeft % 60, minutes: Math.floor(this.totalSecsLeft / 60) % 60, hours: Math.floor(this.totalSecsLeft / 60 / 60) % 24, days: Math.floor(this.totalSecsLeft / 60 / 60 / 24) % 7, totalDays: Math.floor(this.totalSecsLeft / 60 / 60 / 24), weeks: Math.floor(this.totalSecsLeft / 60 / 60 / 24 / 7), months: Math.floor(this.totalSecsLeft / 60 / 60 / 24 / 30), years: Math.floor(this.totalSecsLeft / 60 / 60 / 24 / 365) }, void (0 === this.totalSecsLeft ? (this.stop(), this.dispatchEvent("finish")) : this.dispatchEvent("update"))) }, dispatchEvent: function (b) { var d = a.Event(b + ".countdown"); d.finalDate = this.finalDate, d.offset = a.extend({}, this.offset), d.strftime = c(this.offset), this.$el.trigger(d) } }), a.fn.countdown = function () { var b = Array.prototype.slice.call(arguments, 0); return this.each(function () { var c = a(this).data("countdown-instance"); if (void 0 !== c) { var d = f[c], e = b[0]; i.prototype.hasOwnProperty(e) ? d[e].apply(d, b.slice(1)) : null === String(e).match(/^[$A-Z_][0-9A-Z_$]*$/i) ? (d.setFinalDate.call(d, e), d.start()) : a.error("Method %s does not exist on jQuery.countdown".replace(/\%s/gi, e)) } else new i(this, b[0], b[1]) }) } });
