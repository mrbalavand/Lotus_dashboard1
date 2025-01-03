﻿/*
 Highcharts JS v10.0.0 (2022-03-07)

 Accessibility module

 (c) 2010-2021 Highsoft AS
 Author: Oystein Moseng

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/accessibility", ["highcharts"], function (t) { a(t); a.Highcharts = t; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function t(a, h, r, q) { a.hasOwnProperty(h) || (a[h] = q.apply(null, r), "function" === typeof CustomEvent && window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded", { detail: { path: h, module: a[h] } }))) } a = a ? a._modules : {};
    t(a, "Accessibility/A11yI18n.js", [a["Core/FormatUtilities.js"], a["Core/Utilities.js"]], function (a, h) {
        var k = a.format, q = h.pick, m; (function (a) {
            function m(a, c) {
                var e = a.indexOf("#each("), d = a.indexOf("#plural("), b = a.indexOf("["), f = a.indexOf("]"); if (-1 < e) { f = a.slice(e).indexOf(")") + e; d = a.substring(0, e); b = a.substring(f + 1); f = a.substring(e + 6, f).split(","); e = Number(f[1]); a = ""; if (c = c[f[0]]) for (e = isNaN(e) ? c.length : e, e = 0 > e ? c.length + e : Math.min(e, c.length), f = 0; f < e; ++f)a += d + c[f] + b; return a.length ? a : "" } if (-1 < d) {
                    b = a.slice(d).indexOf(")") +
                    d; d = a.substring(d + 8, b).split(","); switch (Number(c[d[0]])) { case 0: a = q(d[4], d[1]); break; case 1: a = q(d[2], d[1]); break; case 2: a = q(d[3], d[1]); break; default: a = d[1] }a ? (c = a, c = c.trim && c.trim() || c.replace(/^\s+|\s+$/g, "")) : c = ""; return c
                } return -1 < b ? (d = a.substring(0, b), b = Number(a.substring(b + 1, f)), a = void 0, c = c[d], !isNaN(b) && c && (0 > b ? (a = c[c.length + b], "undefined" === typeof a && (a = c[0])) : (a = c[b], "undefined" === typeof a && (a = c[c.length - 1]))), "undefined" !== typeof a ? a : "") : "{" + a + "}"
            } function v(a, c, e) {
                var d = function (b, d) {
                    b =
                    b.slice(d || 0); var f = b.indexOf("{"), c = b.indexOf("}"); if (-1 < f && c > f) return { statement: b.substring(f + 1, c), begin: d + f + 1, end: d + c }
                }, b = [], f = 0; do { var u = d(a, f); var y = a.substring(f, u && u.begin - 1); y.length && b.push({ value: y, type: "constant" }); u && b.push({ value: u.statement, type: "statement" }); f = u ? u.end + 1 : f + 1 } while (u); b.forEach(function (b) { "statement" === b.type && (b.value = m(b.value, c)) }); return k(b.reduce(function (b, d) { return b + d.value }, ""), c, e)
            } function g(a, c) {
                a = a.split("."); for (var e = this.options.lang, d = 0; d < a.length; ++d)e =
                    e && e[a[d]]; return "string" === typeof e ? v(e, c, this) : ""
            } var n = []; a.compose = function (a) { -1 === n.indexOf(a) && (n.push(a), a.prototype.langFormat = g); return a }; a.i18nFormat = v
        })(m || (m = {})); return m
    }); t(a, "Accessibility/Utils/HTMLUtilities.js", [a["Core/Globals.js"], a["Core/Utilities.js"]], function (a, h) {
        function k(a) {
            if ("function" === typeof w.MouseEvent) return new w.MouseEvent(a.type, a); if (m.createEvent) {
                var v = m.createEvent("MouseEvent"); if (v.initMouseEvent) return v.initMouseEvent(a.type, a.bubbles, a.cancelable,
                    a.view || w, a.detail, a.screenX, a.screenY, a.clientX, a.clientY, a.ctrlKey, a.altKey, a.shiftKey, a.metaKey, a.button, a.relatedTarget), v
            } return q(a.type)
        } function q(a, g) {
            g = g || { x: 0, y: 0 }; if ("function" === typeof w.MouseEvent) return new w.MouseEvent(a, { bubbles: !0, cancelable: !0, composed: !0, view: w, detail: "click" === a ? 1 : 0, screenX: g.x, screenY: g.y, clientX: g.x, clientY: g.y }); if (m.createEvent) {
                var v = m.createEvent("MouseEvent"); if (v.initMouseEvent) return v.initMouseEvent(a, !0, !0, w, "click" === a ? 1 : 0, g.x, g.y, g.x, g.y, !1, !1, !1,
                    !1, 0, null), v
            } return { type: a }
        } var m = a.doc, w = a.win, C = h.css; return {
            addClass: function (a, g) { a.classList ? a.classList.add(g) : 0 > a.className.indexOf(g) && (a.className += " " + g) }, cloneMouseEvent: k, cloneTouchEvent: function (a) {
                var g = function (a) { for (var g = [], c = 0; c < a.length; ++c) { var e = a.item(c); e && g.push(e) } return g }; if ("function" === typeof w.TouchEvent) return g = new w.TouchEvent(a.type, {
                    touches: g(a.touches), targetTouches: g(a.targetTouches), changedTouches: g(a.changedTouches), ctrlKey: a.ctrlKey, shiftKey: a.shiftKey, altKey: a.altKey,
                    metaKey: a.metaKey, bubbles: a.bubbles, cancelable: a.cancelable, composed: a.composed, detail: a.detail, view: a.view
                }), a.defaultPrevented && g.preventDefault(), g; g = k(a); g.touches = a.touches; g.changedTouches = a.changedTouches; g.targetTouches = a.targetTouches; return g
            }, escapeStringForHTML: function (a) { return a.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#x27;").replace(/\//g, "&#x2F;") }, getElement: function (a) { return m.getElementById(a) }, getFakeMouseEvent: q,
            getHeadingTagNameForElement: function (a) { var g = function (a) { a = parseInt(a.slice(1), 10); return "h" + Math.min(6, a + 1) }, v = function (a) { var c; a: { for (c = a; c = c.previousSibling;) { var e = c.tagName || ""; if (/H[1-6]/.test(e)) { c = e; break a } } c = "" } if (c) return g(c); a = a.parentElement; if (!a) return "p"; c = a.tagName; return /H[1-6]/.test(c) ? g(c) : v(a) }; return v(a) }, removeChildNodes: function (a) { for (; a.lastChild;)a.removeChild(a.lastChild) }, removeClass: function (a, g) {
                a.classList ? a.classList.remove(g) : a.className = a.className.replace(new RegExp(g,
                    "g"), "")
            }, removeElement: function (a) { a && a.parentNode && a.parentNode.removeChild(a) }, reverseChildNodes: function (a) { for (var g = a.childNodes.length; g--;)a.appendChild(a.childNodes[g]) }, stripHTMLTagsFromString: function (a) { return "string" === typeof a ? a.replace(/<\/?[^>]+(>|$)/g, "") : a }, visuallyHideElement: function (a) {
                C(a, {
                    position: "absolute", width: "1px", height: "1px", overflow: "hidden", whiteSpace: "nowrap", clip: "rect(1px, 1px, 1px, 1px)", marginTop: "-3px", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)",
                    filter: "alpha(opacity=1)", opacity: .01
                })
            }
        }
    }); t(a, "Accessibility/Utils/ChartUtilities.js", [a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, r) {
        function k(b, f) { var a = f.type, c = b.hcEvents; n.createEvent && (b.dispatchEvent || b.fireEvent) ? b.dispatchEvent ? b.dispatchEvent(f) : b.fireEvent(a, f) : c && c[a] ? d(b, a, f) : b.element && k(b.element, f) } function m(b) {
            var d = b.chart, a = {}, c = "Seconds"; a.Seconds = ((b.max || 0) - (b.min || 0)) / 1E3; a.Minutes = a.Seconds / 60; a.Hours = a.Minutes / 60;
            a.Days = a.Hours / 24;["Minutes", "Hours", "Days"].forEach(function (b) { 2 < a[b] && (c = b) }); var e = a[c].toFixed("Seconds" !== c && "Minutes" !== c ? 1 : 0); return d.langFormat("accessibility.axis.timeRange" + c, { chart: d, axis: b, range: e.replace(".0", "") })
        } function w(b) {
            var d = b.chart, a = d.options, c = a && a.accessibility && a.accessibility.screenReaderSection.axisRangeDateFormat || ""; a = function (f) { return b.dateTime ? d.time.dateFormat(c, b[f]) : b[f] }; return d.langFormat("accessibility.axis.rangeFromTo", {
                chart: d, axis: b, rangeFrom: a("min"),
                rangeTo: a("max")
            })
        } function C(b) { if (b.points && b.points.length) return (b = e(b.points, function (b) { return !!b.graphic })) && b.graphic && b.graphic.element } function v(b) { var d = C(b); return d && d.parentNode || b.graph && b.graph.element || b.group && b.group.element } function g(b, d) { d.setAttribute("aria-hidden", !1); d !== b.renderTo && d.parentNode && d.parentNode !== n.body && (Array.prototype.forEach.call(d.parentNode.childNodes, function (b) { b.hasAttribute("aria-hidden") || b.setAttribute("aria-hidden", !0) }), g(b, d.parentNode)) } var n =
            a.doc, x = h.stripHTMLTagsFromString, c = r.defined, e = r.find, d = r.fireEvent; return {
                fireEventOnWrappedOrUnwrappedElement: k, getChartTitle: function (b) { return x(b.options.title.text || b.langFormat("accessibility.defaultChartTitle", { chart: b })) }, getAxisDescription: function (b) { return b && (b.userOptions && b.userOptions.accessibility && b.userOptions.accessibility.description || b.axisTitle && b.axisTitle.textStr || b.options.id || b.categories && "categories" || b.dateTime && "Time" || "values") }, getAxisRangeDescription: function (b) {
                    var d =
                        b.options || {}; return d.accessibility && "undefined" !== typeof d.accessibility.rangeDescription ? d.accessibility.rangeDescription : b.categories ? (d = b.chart, b = b.dataMax && b.dataMin ? d.langFormat("accessibility.axis.rangeCategories", { chart: d, axis: b, numCategories: b.dataMax - b.dataMin + 1 }) : "", b) : !b.dateTime || 0 !== b.min && 0 !== b.dataMin ? w(b) : m(b)
                }, getPointFromXY: function (b, d, a) { for (var f = b.length, c; f--;)if (c = e(b[f].points || [], function (b) { return b.x === d && b.y === a })) return c }, getSeriesFirstPointElement: C, getSeriesFromName: function (b,
                    d) { return d ? (b.series || []).filter(function (b) { return b.name === d }) : b.series }, getSeriesA11yElement: v, unhideChartElementFromAT: g, hideSeriesFromAT: function (b) { (b = v(b)) && b.setAttribute("aria-hidden", !0) }, scrollToPoint: function (b) {
                        var a = b.series.xAxis, e = b.series.yAxis, y = a && a.scrollbar ? a : e; if ((a = y && y.scrollbar) && c(a.to) && c(a.from)) {
                            e = a.to - a.from; if (c(y.dataMin) && c(y.dataMax)) { var g = y.toPixels(y.dataMin), x = y.toPixels(y.dataMax); b = (y.toPixels(b["xAxis" === y.coll ? "x" : "y"] || 0) - g) / (x - g) } else b = 0; a.updatePosition(b -
                                e / 2, b + e / 2); d(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMEvent: null })
                        }
                    }
            }
    }); t(a, "Accessibility/Utils/DOMElementProvider.js", [a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h) {
        var k = a.doc, q = h.removeElement; return function () {
            function a() { this.elements = [] } a.prototype.createElement = function () { var a = k.createElement.apply(k, arguments); this.elements.push(a); return a }; a.prototype.destroyCreatedElements = function () {
                this.elements.forEach(function (a) { q(a) }); this.elements =
                    []
            }; return a
        }()
    }); t(a, "Accessibility/Utils/EventProvider.js", [a["Core/Globals.js"], a["Core/Utilities.js"]], function (a, h) { var k = h.addEvent; return function () { function h() { this.eventRemovers = [] } h.prototype.addEvent = function () { var h = k.apply(a, arguments); this.eventRemovers.push(h); return h }; h.prototype.removeAddedEvents = function () { this.eventRemovers.forEach(function (a) { return a() }); this.eventRemovers = [] }; return h }() }); t(a, "Accessibility/AccessibilityComponent.js", [a["Accessibility/Utils/ChartUtilities.js"],
    a["Accessibility/Utils/DOMElementProvider.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, r, q, m) {
        var k = a.fireEventOnWrappedOrUnwrappedElement, C = q.getFakeMouseEvent; a = m.extend; q = function () {
            function a() { this.proxyProvider = this.keyCodes = this.eventProvider = this.domElementProvider = this.chart = void 0 } a.prototype.initBase = function (a, n) {
                this.chart = a; this.eventProvider = new r; this.domElementProvider = new h; this.proxyProvider = n; this.keyCodes =
                    { left: 37, right: 39, up: 38, down: 40, enter: 13, space: 32, esc: 27, tab: 9, pageUp: 33, pageDown: 34, end: 35, home: 36 }
            }; a.prototype.addEvent = function (a, n, x, c) { return this.eventProvider.addEvent(a, n, x, c) }; a.prototype.createElement = function (a, n) { return this.domElementProvider.createElement(a, n) }; a.prototype.fakeClickEvent = function (a) { var g = C("click"); k(a, g) }; a.prototype.destroyBase = function () { this.domElementProvider.destroyCreatedElements(); this.eventProvider.removeAddedEvents() }; return a
        }(); a(q.prototype, {
            init: function () { },
            getKeyboardNavigation: function () { }, onChartUpdate: function () { }, onChartRender: function () { }, destroy: function () { }
        }); return q
    }); t(a, "Accessibility/KeyboardNavigationHandler.js", [a["Core/Utilities.js"]], function (a) {
        var h = a.find; a = function () {
            function a(a, h) { this.chart = a; this.keyCodeMap = h.keyCodeMap || []; this.validate = h.validate; this.init = h.init; this.terminate = h.terminate; this.response = { success: 1, prev: 2, next: 3, noHandler: 4, fail: 5 } } a.prototype.run = function (a) {
                var k = a.which || a.keyCode, w = this.response.noHandler,
                r = h(this.keyCodeMap, function (a) { return -1 < a[0].indexOf(k) }); r ? w = r[1].call(this, k, a) : 9 === k && (w = this.response[a.shiftKey ? "prev" : "next"]); return w
            }; return a
        }(); ""; return a
    }); t(a, "Accessibility/Components/ContainerComponent.js", [a["Accessibility/AccessibilityComponent.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, r, q, m) {
        var k = this && this.__extends || function () {
            var a = function (c, e) {
                a =
                Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b } || function (d, b) { for (var a in b) b.hasOwnProperty(a) && (d[a] = b[a]) }; return a(c, e)
            }; return function (c, e) { function d() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (d.prototype = e.prototype, new d) }
        }(), C = r.unhideChartElementFromAT, v = r.getChartTitle, g = q.doc, n = m.stripHTMLTagsFromString; return function (a) {
            function c() { return null !== a && a.apply(this, arguments) || this } k(c, a); c.prototype.onChartUpdate = function () {
                this.handleSVGTitleElement();
                this.setSVGContainerLabel(); this.setGraphicContainerAttrs(); this.setRenderToAttrs(); this.makeCreditsAccessible()
            }; c.prototype.handleSVGTitleElement = function () { var a = this.chart, d = "highcharts-title-" + a.index, b = n(a.langFormat("accessibility.svgContainerTitle", { chartTitle: v(a) })); if (b.length) { var c = this.svgTitleElement = this.svgTitleElement || g.createElementNS("http://www.w3.org/2000/svg", "title"); c.textContent = b; c.id = d; a.renderTo.insertBefore(c, a.renderTo.firstChild) } }; c.prototype.setSVGContainerLabel =
                function () { var a = this.chart, d = a.langFormat("accessibility.svgContainerLabel", { chartTitle: v(a) }); a.renderer.box && d.length && a.renderer.box.setAttribute("aria-label", d) }; c.prototype.setGraphicContainerAttrs = function () { var a = this.chart, d = a.langFormat("accessibility.graphicContainerLabel", { chartTitle: v(a) }); d.length && a.container.setAttribute("aria-label", d) }; c.prototype.setRenderToAttrs = function () {
                    var a = this.chart; "disabled" !== a.options.accessibility.landmarkVerbosity ? a.renderTo.setAttribute("role", "region") :
                        a.renderTo.removeAttribute("role"); a.renderTo.setAttribute("aria-label", a.langFormat("accessibility.chartContainerLabel", { title: v(a), chart: a }))
                }; c.prototype.makeCreditsAccessible = function () { var a = this.chart, d = a.credits; d && (d.textStr && d.element.setAttribute("aria-label", a.langFormat("accessibility.credits", { creditsStr: n(d.textStr) })), C(a, d.element)) }; c.prototype.getKeyboardNavigation = function () {
                    var a = this.chart; return new h(a, {
                        keyCodeMap: [], validate: function () { return !0 }, init: function () {
                            var d = a.accessibility;
                            d && d.keyboardNavigation.tabindexContainer.focus()
                        }
                    })
                }; c.prototype.destroy = function () { this.chart.renderTo.setAttribute("aria-hidden", !0) }; return c
        }(a)
    }); t(a, "Accessibility/FocusBorder.js", [a["Core/Renderer/SVG/SVGLabel.js"], a["Core/Utilities.js"]], function (a, h) {
        var k = h.addEvent, q = h.pick, m; (function (h) {
            function m() {
                var a = this.focusElement, b = this.options.accessibility.keyboardNavigation.focusBorder; a && (a.removeFocusBorder(), b.enabled && a.addFocusBorder(b.margin, {
                    stroke: b.style.color, strokeWidth: b.style.lineWidth,
                    r: b.style.borderRadius
                }))
            } function v(a, b) { var d = this.options.accessibility.keyboardNavigation.focusBorder; (b = b || a.element) && b.focus && (b.hcEvents && b.hcEvents.focusin || k(b, "focusin", function () { }), b.focus(), d.hideBrowserFocusOutline && (b.style.outline = "none")); this.focusElement && this.focusElement.removeFocusBorder(); this.focusElement = a; this.renderFocusBorder() } function g(a) {
                if (!a.focusBorderDestroyHook) {
                    var b = a.destroy; a.destroy = function () {
                        a.focusBorder && a.focusBorder.destroy && a.focusBorder.destroy();
                        return b.apply(a, arguments)
                    }; a.focusBorderDestroyHook = b
                }
            } function n(b, d) {
                this.focusBorder && this.removeFocusBorder(); var c = this.getBBox(), f = q(b, 3); c.x += this.translateX ? this.translateX : 0; c.y += this.translateY ? this.translateY : 0; var e = c.x - f, u = c.y - f, n = c.width + 2 * f, h = c.height + 2 * f, z = this instanceof a; if ("text" === this.element.nodeName || z) {
                    var E = !!this.rotation; if (z) var D = { x: E ? 1 : 0, y: 0 }; else { var F = D = 0; "middle" === this.attr("text-anchor") ? D = F = .5 : this.rotation ? D = .25 : F = .75; D = { x: D, y: F } } F = +this.attr("x"); var p = +this.attr("y");
                    isNaN(F) || (e = F - c.width * D.x - f); isNaN(p) || (u = p - c.height * D.y - f); z && E && (z = n, n = h, h = z, isNaN(F) || (e = F - c.height * D.x - f), isNaN(p) || (u = p - c.width * D.y - f))
                } this.focusBorder = this.renderer.rect(e, u, n, h, parseInt((d && d.r || 0).toString(), 10)).addClass("highcharts-focus-border").attr({ zIndex: 99 }).add(this.parentGroup); this.renderer.styledMode || this.focusBorder.attr({ stroke: d && d.stroke, "stroke-width": d && d.strokeWidth }); x(this, b, d); g(this)
            } function x(a) {
                for (var d = [], c = 1; c < arguments.length; c++)d[c - 1] = arguments[c]; a.focusBorderUpdateHooks ||
                    (a.focusBorderUpdateHooks = {}, b.forEach(function (b) { b += "Setter"; var c = a[b] || a._defaultSetter; a.focusBorderUpdateHooks[b] = c; a[b] = function () { var b = c.apply(a, arguments); a.addFocusBorder.apply(a, d); return b } }))
            } function c() { e(this); this.focusBorderDestroyHook && (this.destroy = this.focusBorderDestroyHook, delete this.focusBorderDestroyHook); this.focusBorder && (this.focusBorder.destroy(), delete this.focusBorder) } function e(a) {
                a.focusBorderUpdateHooks && (Object.keys(a.focusBorderUpdateHooks).forEach(function (b) {
                    var d =
                        a.focusBorderUpdateHooks[b]; d === a._defaultSetter ? delete a[b] : a[b] = d
                }), delete a.focusBorderUpdateHooks)
            } var d = [], b = "x y transform width height r d stroke-width".split(" "); h.compose = function (a, b) { -1 === d.indexOf(a) && (d.push(a), a = a.prototype, a.renderFocusBorder = m, a.setFocusToElement = v); -1 === d.indexOf(b) && (d.push(b), b = b.prototype, b.addFocusBorder = n, b.removeFocusBorder = c) }
        })(m || (m = {})); return m
    }); t(a, "Accessibility/Utils/Announcer.js", [a["Core/Renderer/HTML/AST.js"], a["Accessibility/Utils/DOMElementProvider.js"],
    a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, r, q, m) {
        var k = r.doc, C = q.addClass, v = q.visuallyHideElement, g = m.attr; return function () {
            function n(a, c) { this.chart = a; this.domElementProvider = new h; this.announceRegion = this.addAnnounceRegion(c) } n.prototype.destroy = function () { this.domElementProvider.destroyCreatedElements() }; n.prototype.announce = function (g) {
                var c = this; a.setElementHTML(this.announceRegion, g); this.clearAnnouncementRegionTimer && clearTimeout(this.clearAnnouncementRegionTimer);
                this.clearAnnouncementRegionTimer = setTimeout(function () { c.announceRegion.innerHTML = a.emptyHTML; delete c.clearAnnouncementRegionTimer }, 1E3)
            }; n.prototype.addAnnounceRegion = function (a) { var c = this.chart.announcerContainer || this.createAnnouncerContainer(), e = this.domElementProvider.createElement("div"); g(e, { "aria-hidden": !1, "aria-live": a }); this.chart.styledMode ? C(e, "highcharts-visually-hidden") : v(e); c.appendChild(e); return e }; n.prototype.createAnnouncerContainer = function () {
                var a = this.chart, c = k.createElement("div");
                g(c, { "aria-hidden": !1, "class": "highcharts-announcer-container" }); c.style.position = "relative"; a.renderTo.insertBefore(c, a.renderTo.firstChild); return a.announcerContainer = c
            }; return n
        }()
    }); t(a, "Accessibility/Components/AnnotationsA11y.js", [a["Accessibility/Utils/HTMLUtilities.js"]], function (a) {
        function h(a) { return (a.annotations || []).reduce(function (a, n) { n.options && !1 !== n.options.visible && (a = a.concat(n.labels)); return a }, []) } function k(a) {
            return a.options && a.options.accessibility && a.options.accessibility.description ||
                a.graphic && a.graphic.text && a.graphic.text.textStr || ""
        } function q(a) {
            var g = a.options && a.options.accessibility && a.options.accessibility.description; if (g) return g; g = a.chart; var n = k(a), h = a.points.filter(function (a) { return !!a.graphic }).map(function (a) { var b = a.accessibility && a.accessibility.valueDescription || a.graphic && a.graphic.element && a.graphic.element.getAttribute("aria-label") || ""; a = a && a.series.name || ""; return (a ? a + ", " : "") + "data point " + b }).filter(function (a) { return !!a }), c = h.length, e = "accessibility.screenReaderSection.annotations.description" +
                (1 < c ? "MultiplePoints" : c ? "SinglePoint" : "NoPoints"); a = { annotationText: n, annotation: a, numPoints: c, annotationPoint: h[0], additionalAnnotationPoints: h.slice(1) }; return g.langFormat(e, a)
        } function m(a) { return h(a).map(function (a) { return (a = w(C(q(a)))) ? "<li>" + a + "</li>" : "" }) } var w = a.escapeStringForHTML, C = a.stripHTMLTagsFromString; return {
            getAnnotationsInfoHTML: function (a) { var g = a.annotations; return g && g.length ? '<ul style="list-style-type: none">' + m(a).join(" ") + "</ul>" : "" }, getAnnotationLabelDescription: q, getAnnotationListItems: m,
            getPointAnnotationTexts: function (a) { var g = h(a.series.chart).filter(function (g) { return -1 < g.points.indexOf(a) }); return g.length ? g.map(function (a) { return "" + k(a) }) : [] }
        }
    }); t(a, "Accessibility/Components/InfoRegionsComponent.js", [a["Accessibility/A11yI18n.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/Components/AnnotationsA11y.js"], a["Core/Renderer/HTML/AST.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/FormatUtilities.js"], a["Core/Globals.js"],
    a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, r, q, m, w, C, v, g, n) {
        function x(a, b) {
            var d = b[0], l = a.series && a.series[0] || {}; l = { numSeries: a.series.length, numPoints: l.points && l.points.length, chart: a, mapTitle: l.mapTitle }; if (!d) return a.langFormat("accessibility.chartTypes.emptyChart", l); if ("map" === d) return l.mapTitle ? a.langFormat("accessibility.chartTypes.mapTypeDescription", l) : a.langFormat("accessibility.chartTypes.unknownMap", l); if (1 < a.types.length) return a.langFormat("accessibility.chartTypes.combinationChart",
                l); b = b[0]; d = a.langFormat("accessibility.seriesTypeDescriptions." + b, l); var c = a.series && 2 > a.series.length ? "Single" : "Multiple"; return (a.langFormat("accessibility.chartTypes." + b + c, l) || a.langFormat("accessibility.chartTypes.default" + c, l)) + (d ? " " + d : "")
        } var c = this && this.__extends || function () {
            var a = function (b, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, b) { a.__proto__ = b } || function (a, b) { for (var d in b) b.hasOwnProperty(d) && (a[d] = b[d]) }; return a(b, d) }; return function (b, d) {
                function l() {
                    this.constructor =
                    b
                } a(b, d); b.prototype = null === d ? Object.create(d) : (l.prototype = d.prototype, new l)
            }
        }(), e = q.getAnnotationsInfoHTML, d = w.getAxisDescription, b = w.getAxisRangeDescription, f = w.getChartTitle, u = w.unhideChartElementFromAT, y = C.format, k = v.doc, G = g.addClass, t = g.getElement, B = g.getHeadingTagNameForElement, I = g.stripHTMLTagsFromString, z = g.visuallyHideElement, E = n.attr, D = n.pick; return function (g) {
            function p() { var a = null !== g && g.apply(this, arguments) || this; a.announcer = void 0; a.screenReaderSections = {}; return a } c(p, g); p.prototype.init =
                function () { var a = this.chart, b = this; this.initRegionsDefinitions(); this.addEvent(a, "aftergetTableAST", function (a) { b.onDataTableCreated(a) }); this.addEvent(a, "afterViewData", function (a) { b.dataTableDiv = a; setTimeout(function () { b.focusDataTable() }, 300) }); this.announcer = new r(a, "assertive") }; p.prototype.initRegionsDefinitions = function () {
                    var a = this; this.screenReaderSections = {
                        before: {
                            element: null, buildContent: function (b) { var d = b.options.accessibility.screenReaderSection.beforeChartFormatter; return d ? d(b) : a.defaultBeforeChartFormatter(b) },
                            insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.renderTo.firstChild) }, afterInserted: function () { "undefined" !== typeof a.sonifyButtonId && a.initSonifyButton(a.sonifyButtonId); "undefined" !== typeof a.dataTableButtonId && a.initDataTableButton(a.dataTableButtonId) }
                        }, after: {
                            element: null, buildContent: function (b) { var d = b.options.accessibility.screenReaderSection.afterChartFormatter; return d ? d(b) : a.defaultAfterChartFormatter() }, insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.container.nextSibling) },
                            afterInserted: function () { a.chart.accessibility && a.chart.accessibility.keyboardNavigation.updateExitAnchor() }
                        }
                    }
                }; p.prototype.onChartRender = function () { var a = this; this.linkedDescriptionElement = this.getLinkedDescriptionElement(); this.setLinkedDescriptionAttrs(); Object.keys(this.screenReaderSections).forEach(function (b) { a.updateScreenReaderSection(b) }) }; p.prototype.getLinkedDescriptionElement = function () {
                    var a = this.chart.options.accessibility.linkedDescription; if (a) {
                        if ("string" !== typeof a) return a; a = y(a,
                            this.chart); a = k.querySelectorAll(a); if (1 === a.length) return a[0]
                    }
                }; p.prototype.setLinkedDescriptionAttrs = function () { var a = this.linkedDescriptionElement; a && (a.setAttribute("aria-hidden", "true"), G(a, "highcharts-linked-description")) }; p.prototype.updateScreenReaderSection = function (a) {
                    var b = this.chart, d = this.screenReaderSections[a], c = d.buildContent(b), A = d.element = d.element || this.createElement("div"), e = A.firstChild || this.createElement("div"); c ? (this.setScreenReaderSectionAttribs(A, a), m.setElementHTML(e,
                        c), A.appendChild(e), d.insertIntoDOM(A, b), b.styledMode ? G(e, "highcharts-visually-hidden") : z(e), u(b, e), d.afterInserted && d.afterInserted()) : (A.parentNode && A.parentNode.removeChild(A), delete d.element)
                }; p.prototype.setScreenReaderSectionAttribs = function (a, b) {
                    var d = this.chart, c = d.langFormat("accessibility.screenReaderSection." + b + "RegionLabel", { chart: d, chartTitle: f(d) }); E(a, { id: "highcharts-screen-reader-region-" + b + "-" + d.index, "aria-label": c }); a.style.position = "relative"; "all" === d.options.accessibility.landmarkVerbosity &&
                        c && a.setAttribute("role", "region")
                }; p.prototype.defaultBeforeChartFormatter = function () {
                    var b = this.chart, d = b.options.accessibility.screenReaderSection.beforeChartFormat; if (!d) return ""; var c = this.getAxesDescription(), L = b.sonify && b.options.sonification && b.options.sonification.enabled, J = "highcharts-a11y-sonify-data-btn-" + b.index, u = "hc-linkto-highcharts-data-table-" + b.index, p = e(b), y = b.langFormat("accessibility.screenReaderSection.annotations.heading", { chart: b }); c = {
                        headingTagName: B(b.renderTo), chartTitle: f(b),
                        typeDescription: this.getTypeDescriptionText(), chartSubtitle: this.getSubtitleText(), chartLongdesc: this.getLongdescText(), xAxisDescription: c.xAxis, yAxisDescription: c.yAxis, playAsSoundButton: L ? this.getSonifyButtonText(J) : "", viewTableButton: b.getCSV ? this.getDataTableButtonText(u) : "", annotationsTitle: p ? y : "", annotationsList: p
                    }; b = a.i18nFormat(d, c, b); this.dataTableButtonId = u; this.sonifyButtonId = J; return b.replace(/<(\w+)[^>]*?>\s*<\/\1>/g, "")
                }; p.prototype.defaultAfterChartFormatter = function () {
                    var b = this.chart,
                    d = b.options.accessibility.screenReaderSection.afterChartFormat; if (!d) return ""; var c = { endOfChartMarker: this.getEndOfChartMarkerText() }; return a.i18nFormat(d, c, b).replace(/<(\w+)[^>]*?>\s*<\/\1>/g, "")
                }; p.prototype.getLinkedDescription = function () { var a = this.linkedDescriptionElement; return I(a && a.innerHTML || "") }; p.prototype.getLongdescText = function () { var a = this.chart.options, b = a.caption; b = b && b.text; var d = this.getLinkedDescription(); return a.accessibility.description || d || b || "" }; p.prototype.getTypeDescriptionText =
                    function () { var a = this.chart; return a.types ? a.options.accessibility.typeDescription || x(a, a.types) : "" }; p.prototype.getDataTableButtonText = function (a) { var b = this.chart; b = b.langFormat("accessibility.table.viewAsDataTableButtonText", { chart: b, chartTitle: f(b) }); return '<button id="' + a + '">' + b + "</button>" }; p.prototype.getSonifyButtonText = function (a) {
                        var b = this.chart; if (b.options.sonification && !1 === b.options.sonification.enabled) return ""; b = b.langFormat("accessibility.sonification.playAsSoundButtonText", {
                            chart: b,
                            chartTitle: f(b)
                        }); return '<button id="' + a + '">' + b + "</button>"
                    }; p.prototype.getSubtitleText = function () { var a = this.chart.options.subtitle; return I(a && a.text || "") }; p.prototype.getEndOfChartMarkerText = function () { var a = this.chart, b = a.langFormat("accessibility.screenReaderSection.endOfChartMarker", { chart: a }); return '<div id="highcharts-end-of-chart-marker-' + a.index + '">' + b + "</div>" }; p.prototype.onDataTableCreated = function (a) {
                        var b = this.chart; if (b.options.accessibility.enabled) {
                            this.viewDataTableButton && this.viewDataTableButton.setAttribute("aria-expanded",
                                "true"); var d = a.tree.attributes || {}; d.tabindex = -1; d.summary = b.langFormat("accessibility.table.tableSummary", { chart: b }); a.tree.attributes = d
                        }
                    }; p.prototype.focusDataTable = function () { var a = this.dataTableDiv; (a = a && a.getElementsByTagName("table")[0]) && a.focus && a.focus() }; p.prototype.initSonifyButton = function (a) {
                        var b = this, d = this.sonifyButton = t(a), c = this.chart, e = function (a) {
                            d && (d.setAttribute("aria-hidden", "true"), d.setAttribute("aria-label", "")); a.preventDefault(); a.stopPropagation(); a = c.langFormat("accessibility.sonification.playAsSoundClickAnnouncement",
                                { chart: c }); b.announcer.announce(a); setTimeout(function () { d && (d.removeAttribute("aria-hidden"), d.removeAttribute("aria-label")); c.sonify && c.sonify() }, 1E3)
                        }; d && c && (d.setAttribute("tabindex", -1), d.onclick = function (a) { (c.options.accessibility && c.options.accessibility.screenReaderSection.onPlayAsSoundClick || e).call(this, a, c) })
                    }; p.prototype.initDataTableButton = function (a) {
                        var b = this.viewDataTableButton = t(a), d = this.chart; a = a.replace("hc-linkto-", ""); b && (E(b, { tabindex: -1, "aria-expanded": !!t(a) }), b.onclick =
                            d.options.accessibility.screenReaderSection.onViewDataTableClick || function () { d.viewData() })
                    }; p.prototype.getAxesDescription = function () { var a = this.chart, b = function (b, d) { b = a[b]; return 1 < b.length || b[0] && D(b[0].options.accessibility && b[0].options.accessibility.enabled, d) }, d = !!a.types && 0 > a.types.indexOf("map"), c = !!a.hasCartesianSeries, e = b("xAxis", !a.angular && c && d); b = b("yAxis", c && d); d = {}; e && (d.xAxis = this.getAxisDescriptionText("xAxis")); b && (d.yAxis = this.getAxisDescriptionText("yAxis")); return d }; p.prototype.getAxisDescriptionText =
                        function (a) { var c = this.chart, e = c[a]; return c.langFormat("accessibility.axis." + a + "Description" + (1 < e.length ? "Plural" : "Singular"), { chart: c, names: e.map(function (a) { return d(a) }), ranges: e.map(function (a) { return b(a) }), numAxes: e.length }) }; p.prototype.destroy = function () { this.announcer && this.announcer.destroy() }; return p
        }(h)
    }); t(a, "Accessibility/Components/MenuComponent.js", [a["Core/Chart/Chart.js"], a["Core/Utilities.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/KeyboardNavigationHandler.js"],
    a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, r, q, m, w) {
        var k = this && this.__extends || function () { var a = function (c, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, d) { a.__proto__ = d } || function (a, d) { for (var b in d) d.hasOwnProperty(b) && (a[b] = d[b]) }; return a(c, d) }; return function (c, d) { function b() { this.constructor = c } a(c, d); c.prototype = null === d ? Object.create(d) : (b.prototype = d.prototype, new b) } }(), v = h.attr, g = m.getChartTitle, n = m.unhideChartElementFromAT,
        x = w.getFakeMouseEvent; h = function (a) {
            function c() { return null !== a && a.apply(this, arguments) || this } k(c, a); c.prototype.init = function () { var a = this.chart, b = this; this.addEvent(a, "exportMenuShown", function () { b.onMenuShown() }); this.addEvent(a, "exportMenuHidden", function () { b.onMenuHidden() }); this.createProxyGroup() }; c.prototype.onMenuHidden = function () { var a = this.chart.exportContextMenu; a && a.setAttribute("aria-hidden", "true"); this.setExportButtonExpandedState("false") }; c.prototype.onMenuShown = function () {
                var a =
                    this.chart, b = a.exportContextMenu; b && (this.addAccessibleContextMenuAttribs(), n(a, b)); this.setExportButtonExpandedState("true")
            }; c.prototype.setExportButtonExpandedState = function (a) { this.exportButtonProxy && this.exportButtonProxy.buttonElement.setAttribute("aria-expanded", a) }; c.prototype.onChartRender = function () {
                var a = this.chart, b = a.focusElement, c = a.accessibility; this.proxyProvider.clearGroup("chartMenu"); this.proxyMenuButton(); this.exportButtonProxy && b && b === a.exportingGroup && (b.focusBorder ? a.setFocusToElement(b,
                    this.exportButtonProxy.buttonElement) : c && c.keyboardNavigation.tabindexContainer.focus())
            }; c.prototype.proxyMenuButton = function () {
                var a = this.chart, b = this.proxyProvider, c = a.exportSVGElements && a.exportSVGElements[0], e = a.options.exporting, y = a.exportSVGElements && a.exportSVGElements[0]; e && !1 !== e.enabled && e.accessibility && e.accessibility.enabled && y && y.element && c && (this.exportButtonProxy = b.addProxyElement("chartMenu", { click: c }, {
                    "aria-label": a.langFormat("accessibility.exporting.menuButtonLabel", {
                        chart: a,
                        chartTitle: g(a)
                    }), "aria-expanded": !1, title: a.options.lang.contextButtonTitle || null
                }))
            }; c.prototype.createProxyGroup = function () { this.chart && this.proxyProvider && this.proxyProvider.addGroup("chartMenu", "div") }; c.prototype.addAccessibleContextMenuAttribs = function () {
                var a = this.chart, b = a.exportDivElements; b && b.length && (b.forEach(function (a) { a && ("LI" !== a.tagName || a.children && a.children.length ? a.setAttribute("aria-hidden", "true") : a.setAttribute("tabindex", -1)) }), (b = b[0] && b[0].parentNode) && v(b, {
                    "aria-hidden": void 0,
                    "aria-label": a.langFormat("accessibility.exporting.chartMenuLabel", { chart: a }), role: "list"
                }))
            }; c.prototype.getKeyboardNavigation = function () {
                var a = this.keyCodes, b = this.chart, c = this; return new q(b, {
                    keyCodeMap: [[[a.left, a.up], function () { return c.onKbdPrevious(this) }], [[a.right, a.down], function () { return c.onKbdNext(this) }], [[a.enter, a.space], function () { return c.onKbdClick(this) }]], validate: function () { return !!b.exporting && !1 !== b.options.exporting.enabled && !1 !== b.options.exporting.accessibility.enabled },
                    init: function () { var a = c.exportButtonProxy, d = c.chart.exportingGroup; a && d && b.setFocusToElement(d, a.buttonElement) }, terminate: function () { b.hideExportMenu() }
                })
            }; c.prototype.onKbdPrevious = function (a) { var b = this.chart, d = b.options.accessibility; a = a.response; for (var c = b.highlightedExportItemIx || 0; c--;)if (b.highlightExportItem(c)) return a.success; return d.keyboardNavigation.wrapAround ? (b.highlightLastExportItem(), a.success) : a.prev }; c.prototype.onKbdNext = function (a) {
                var b = this.chart, d = b.options.accessibility;
                a = a.response; for (var c = (b.highlightedExportItemIx || 0) + 1; c < b.exportDivElements.length; ++c)if (b.highlightExportItem(c)) return a.success; return d.keyboardNavigation.wrapAround ? (b.highlightExportItem(0), a.success) : a.next
            }; c.prototype.onKbdClick = function (a) { var b = this.chart, d = b.exportDivElements[b.highlightedExportItemIx], c = (b.exportSVGElements && b.exportSVGElements[0]).element; b.openMenu ? this.fakeClickEvent(d) : (this.fakeClickEvent(c), b.highlightExportItem(0)); return a.response.success }; return c
        }(r); (function (c) {
            function e() {
                var a =
                    this.exportSVGElements && this.exportSVGElements[0]; if (a && (a = a.element, a.onclick)) a.onclick(x("click"))
            } function d() { var a = this.exportDivElements; a && this.exportContextMenu && this.openMenu && (a.forEach(function (a) { if (a && "highcharts-menu-item" === a.className && a.onmouseout) a.onmouseout(x("mouseout")) }), this.highlightedExportItemIx = 0, this.exportContextMenu.hideMenu(), this.container.focus()) } function b(a) {
                var b = this.exportDivElements && this.exportDivElements[a], d = this.exportDivElements && this.exportDivElements[this.highlightedExportItemIx];
                if (b && "LI" === b.tagName && (!b.children || !b.children.length)) { var c = !!(this.renderTo.getElementsByTagName("g")[0] || {}).focus; b.focus && c && b.focus(); if (d && d.onmouseout) d.onmouseout(x("mouseout")); if (b.onmouseover) b.onmouseover(x("mouseover")); this.highlightedExportItemIx = a; return !0 } return !1
            } function f() { if (this.exportDivElements) for (var a = this.exportDivElements.length; a--;)if (this.highlightExportItem(a)) return !0; return !1 } var u = []; c.compose = function (c) {
                -1 === u.indexOf(c) && (u.push(c), c = a.prototype, c.hideExportMenu =
                    d, c.highlightExportItem = b, c.highlightLastExportItem = f, c.showExportMenu = e)
            }
        })(h || (h = {})); return h
    }); t(a, "Accessibility/KeyboardNavigation.js", [a["Core/Globals.js"], a["Accessibility/Components/MenuComponent.js"], a["Core/Utilities.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, r, q, m) {
        var k = a.doc, C = a.win, v = r.addEvent, g = r.fireEvent, n = m.getElement; r = function () {
            function a(a, e) {
                this.components = this.chart = void 0; this.currentModuleIx = NaN; this.exitAnchor =
                    this.eventProvider = void 0; this.modules = []; this.tabindexContainer = void 0; this.init(a, e)
            } a.prototype.init = function (a, e) {
                var d = this, b = this.eventProvider = new q; this.chart = a; this.components = e; this.modules = []; this.currentModuleIx = 0; this.update(); b.addEvent(this.tabindexContainer, "keydown", function (a) { return d.onKeydown(a) }); b.addEvent(this.tabindexContainer, "focus", function (a) { return d.onFocus(a) });["mouseup", "touchend"].forEach(function (a) { return b.addEvent(k, a, function () { return d.onMouseUp() }) });["mousedown",
                    "touchstart"].forEach(function (c) { return b.addEvent(a.renderTo, c, function () { d.isClickingChart = !0 }) }); b.addEvent(a.renderTo, "mouseover", function () { d.pointerIsOverChart = !0 }); b.addEvent(a.renderTo, "mouseout", function () { d.pointerIsOverChart = !1 })
            }; a.prototype.update = function (a) {
                var c = this.chart.options.accessibility; c = c && c.keyboardNavigation; var d = this.components; this.updateContainerTabindex(); c && c.enabled && a && a.length ? (this.modules = a.reduce(function (a, c) { c = d[c].getKeyboardNavigation(); return a.concat(c) },
                    []), this.updateExitAnchor()) : (this.modules = [], this.currentModuleIx = 0, this.removeExitAnchor())
            }; a.prototype.updateExitAnchor = function () { var a = n("highcharts-end-of-chart-marker-" + this.chart.index); this.removeExitAnchor(); a ? (this.makeElementAnExitAnchor(a), this.exitAnchor = a) : this.createExitAnchor() }; a.prototype.move = function (a) {
                var c = this.modules && this.modules[this.currentModuleIx]; c && c.terminate && c.terminate(a); this.chart.focusElement && this.chart.focusElement.removeFocusBorder(); this.currentModuleIx +=
                    a; if (c = this.modules && this.modules[this.currentModuleIx]) { if (c.validate && !c.validate()) return this.move(a); if (c.init) return c.init(a), !0 } this.currentModuleIx = 0; this.exiting = !0; 0 < a ? this.exitAnchor.focus() : this.tabindexContainer.focus(); return !1
            }; a.prototype.onFocus = function (a) {
                var c = this.chart; a = a.relatedTarget && c.container.contains(a.relatedTarget); this.exiting || this.tabbingInBackwards || this.isClickingChart || a || (a = this.getFirstValidModuleIx(), null !== a && (this.currentModuleIx = a, this.modules[a].init(1)));
                this.exiting = !1
            }; a.prototype.onMouseUp = function () { delete this.isClickingChart; if (!this.keyboardReset) { var a = this.chart; if (!this.pointerIsOverChart) { var e = this.modules && this.modules[this.currentModuleIx || 0]; e && e.terminate && e.terminate(); this.currentModuleIx = 0 } a.focusElement && (a.focusElement.removeFocusBorder(), delete a.focusElement); this.keyboardReset = !0 } }; a.prototype.onKeydown = function (a) {
                a = a || C.event; var c = this.modules && this.modules.length && this.modules[this.currentModuleIx], d; this.exiting = this.keyboardReset =
                    !1; if (c) { var b = c.run(a); b === c.response.success ? d = !0 : b === c.response.prev ? d = this.move(-1) : b === c.response.next && (d = this.move(1)); d && (a.preventDefault(), a.stopPropagation()) }
            }; a.prototype.updateContainerTabindex = function () {
                var a = this.chart.options.accessibility; a = a && a.keyboardNavigation; a = !(a && !1 === a.enabled); var e = this.chart, d = e.container; e.renderTo.hasAttribute("tabindex") && (d.removeAttribute("tabindex"), d = e.renderTo); this.tabindexContainer = d; var b = d.getAttribute("tabindex"); a && !b ? d.setAttribute("tabindex",
                    "0") : a || e.container.removeAttribute("tabindex")
            }; a.prototype.createExitAnchor = function () { var a = this.chart, e = this.exitAnchor = k.createElement("div"); a.renderTo.appendChild(e); this.makeElementAnExitAnchor(e) }; a.prototype.makeElementAnExitAnchor = function (a) { var c = this.tabindexContainer.getAttribute("tabindex") || 0; a.setAttribute("class", "highcharts-exit-anchor"); a.setAttribute("tabindex", c); a.setAttribute("aria-hidden", !1); this.addExitAnchorEventsToEl(a) }; a.prototype.removeExitAnchor = function () {
                this.exitAnchor &&
                this.exitAnchor.parentNode && (this.exitAnchor.parentNode.removeChild(this.exitAnchor), delete this.exitAnchor)
            }; a.prototype.addExitAnchorEventsToEl = function (a) {
                var c = this.chart, d = this; this.eventProvider.addEvent(a, "focus", function (a) {
                    a = a || C.event; var b = !(a.relatedTarget && c.container.contains(a.relatedTarget) || d.exiting); c.focusElement && delete c.focusElement; b ? (d.tabbingInBackwards = !0, d.tabindexContainer.focus(), delete d.tabbingInBackwards, a.preventDefault(), d.modules && d.modules.length && (d.currentModuleIx =
                        d.modules.length - 1, (a = d.modules[d.currentModuleIx]) && a.validate && !a.validate() ? d.move(-1) : a && a.init(-1))) : d.exiting = !1
                })
            }; a.prototype.getFirstValidModuleIx = function () { for (var a = this.modules.length, e = 0; e < a; ++e) { var d = this.modules[e]; if (!d.validate || d.validate()) return e } return null }; a.prototype.destroy = function () { this.removeExitAnchor(); this.eventProvider.removeAddedEvents(); this.chart.container.removeAttribute("tabindex") }; return a
        }(); (function (n) {
            function c() {
                var a = this; g(this, "dismissPopupContent",
                    {}, function () { a.tooltip && a.tooltip.hide(0); a.hideExportMenu() })
            } function e(b) { 27 === (b.which || b.keyCode) && a.charts && a.charts.forEach(function (a) { a && a.dismissPopupContent && a.dismissPopupContent() }) } var d = []; n.compose = function (a) { h.compose(a); -1 === d.indexOf(a) && (d.push(a), a.prototype.dismissPopupContent = c); -1 === d.indexOf(k) && (d.push(k), v(k, "keydown", e)); return a }
        })(r || (r = {})); return r
    }); t(a, "Accessibility/Components/LegendComponent.js", [a["Core/Animation/AnimationUtilities.js"], a["Core/Globals.js"],
    a["Core/Legend/Legend.js"], a["Core/Utilities.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, r, q, m, w, C, v) {
        function g(a) { var b = a.legend && a.legend.allItems, d = a.options.legend.accessibility || {}; return !(!b || !b.length || a.colorAxis && a.colorAxis.length || !1 === d.enabled) } function n(a, d) {
            d.setState(a ? "hover" : "", !0);["legendGroup", "legendItem", "legendSymbol"].forEach(function (c) {
                (c =
                    (c = d[c]) && c.element || c) && b(c, a ? "mouseover" : "mouseout")
            })
        } var x = this && this.__extends || function () { var a = function (b, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, b) { a.__proto__ = b } || function (a, b) { for (var d in b) b.hasOwnProperty(d) && (a[d] = b[d]) }; return a(b, d) }; return function (b, d) { function c() { this.constructor = b } a(b, d); b.prototype = null === d ? Object.create(d) : (c.prototype = d.prototype, new c) } }(), c = a.animObject, e = h.doc, d = q.addEvent, b = q.fireEvent, f = q.isNumber, u = q.pick, y = q.syncTimeout,
            k = C.getChartTitle, t = v.stripHTMLTagsFromString, H = v.addClass, B = v.removeClass; a = function (a) {
                function b() { var b = null !== a && a.apply(this, arguments) || this; b.highlightedLegendItemIx = NaN; b.proxyGroup = null; return b } x(b, a); b.prototype.init = function () {
                    var a = this; this.recreateProxies(); this.addEvent(r, "afterScroll", function () { this.chart === a.chart && (a.proxyProvider.updateGroupProxyElementPositions("legend"), a.updateLegendItemProxyVisibility(), -1 < a.highlightedLegendItemIx && this.chart.highlightLegendItem(a.highlightedLegendItemIx)) });
                    this.addEvent(r, "afterPositionItem", function (b) { this.chart === a.chart && this.chart.renderer && a.updateProxyPositionForItem(b.item) }); this.addEvent(r, "afterRender", function () { this.chart === a.chart && this.chart.renderer && a.recreateProxies() && y(function () { return a.proxyProvider.updateGroupProxyElementPositions("legend") }, c(u(this.chart.renderer.globalAnimation, !0)).duration) })
                }; b.prototype.updateLegendItemProxyVisibility = function () {
                    var a = this.chart, b = a.legend, d = b.currentPage || 1, c = b.clipHeight || 0; (b.allItems ||
                        []).forEach(function (e) { if (e.a11yProxyElement) { var l = e.a11yProxyElement.element, f = !1; if (b.pages && b.pages.length) { f = e.pageIx || 0; var A = e._legendItemPos ? e._legendItemPos[1] : 0; e = e.legendItem ? Math.round(e.legendItem.getBBox().height) : 0; f = A + e - b.pages[f] > c || f !== d - 1 } f ? a.styledMode ? H(l, "highcharts-a11y-invisible") : l.style.visibility = "hidden" : (B(l, "highcharts-a11y-invisible"), l.style.visibility = "") } })
                }; b.prototype.onChartRender = function () { g(this.chart) || this.removeProxies() }; b.prototype.highlightAdjacentLegendPage =
                    function (a) { var b = this.chart, d = b.legend; a = (d.currentPage || 1) + a; var c = d.pages || []; if (0 < a && a <= c.length) { c = d.allItems.length; for (var e = 0; e < c; ++e)if (d.allItems[e].pageIx + 1 === a) { b.highlightLegendItem(e) && (this.highlightedLegendItemIx = e); break } } }; b.prototype.updateProxyPositionForItem = function (a) { a.a11yProxyElement && a.a11yProxyElement.refreshPosition() }; b.prototype.recreateProxies = function () {
                        var a = e.activeElement, b = this.proxyGroup; a = a && b && b.contains(a); this.removeProxies(); return g(this.chart) ? (this.addLegendProxyGroup(),
                            this.proxyLegendItems(), this.updateLegendItemProxyVisibility(), this.updateLegendTitle(), a && this.chart.highlightLegendItem(this.highlightedLegendItemIx), !0) : !1
                    }; b.prototype.removeProxies = function () { this.proxyProvider.removeGroup("legend") }; b.prototype.updateLegendTitle = function () {
                        var a = this.chart, b = t((a.legend && a.legend.options.title && a.legend.options.title.text || "").replace(/<br ?\/?>/g, " ")); a = a.langFormat("accessibility.legend.legendLabel" + (b ? "" : "NoTitle"), { chart: a, legendTitle: b, chartTitle: k(a) });
                        this.proxyProvider.updateGroupAttrs("legend", { "aria-label": a })
                    }; b.prototype.addLegendProxyGroup = function () { this.proxyGroup = this.proxyProvider.addGroup("legend", "ul", { "aria-label": "_placeholder_", role: "all" === this.chart.options.accessibility.landmarkVerbosity ? "region" : null }) }; b.prototype.proxyLegendItems = function () { var a = this; (this.chart.legend && this.chart.legend.allItems || []).forEach(function (b) { b.legendItem && b.legendItem.element && a.proxyLegendItem(b) }) }; b.prototype.proxyLegendItem = function (a) {
                        if (a.legendItem &&
                            a.legendGroup) { var b = this.chart.langFormat("accessibility.legend.legendItem", { chart: this.chart, itemName: t(a.name), item: a }); a.a11yProxyElement = this.proxyProvider.addProxyElement("legend", { click: a.legendItem, visual: (a.legendGroup.div ? a.legendItem : a.legendGroup).element }, { tabindex: -1, "aria-pressed": a.visible, "aria-label": b }) }
                    }; b.prototype.getKeyboardNavigation = function () {
                        var a = this.keyCodes, b = this, d = this.chart; return new w(d, {
                            keyCodeMap: [[[a.left, a.right, a.up, a.down], function (a) {
                                return b.onKbdArrowKey(this,
                                    a)
                            }], [[a.enter, a.space], function (d) { return h.isFirefox && d === a.space ? this.response.success : b.onKbdClick(this) }], [[a.pageDown, a.pageUp], function (d) { b.highlightAdjacentLegendPage(d === a.pageDown ? 1 : -1); return this.response.success }]], validate: function () { return b.shouldHaveLegendNavigation() }, init: function () { d.highlightLegendItem(0); b.highlightedLegendItemIx = 0 }, terminate: function () { b.highlightedLegendItemIx = -1; d.legend.allItems.forEach(function (a) { return n(!1, a) }) }
                        })
                    }; b.prototype.onKbdArrowKey = function (a,
                        b) { var d = this.keyCodes, c = a.response, e = this.chart, l = e.options.accessibility, f = e.legend.allItems.length; b = b === d.left || b === d.up ? -1 : 1; if (e.highlightLegendItem(this.highlightedLegendItemIx + b)) return this.highlightedLegendItemIx += b, c.success; 1 < f && l.keyboardNavigation.wrapAround && a.init(b); return c.success }; b.prototype.onKbdClick = function (a) { var b = this.chart.legend.allItems[this.highlightedLegendItemIx]; b && b.a11yProxyElement && b.a11yProxyElement.click(); return a.response.success }; b.prototype.shouldHaveLegendNavigation =
                            function () { var a = this.chart, b = a.colorAxis && a.colorAxis.length, d = (a.options.legend || {}).accessibility || {}; return !!(a.legend && a.legend.allItems && a.legend.display && !b && d.enabled && d.keyboardNavigation && d.keyboardNavigation.enabled) }; return b
            }(m); (function (a) {
                function b(a) {
                    var b = this.legend.allItems, d = this.accessibility && this.accessibility.components.legend.highlightedLegendItemIx, c = b[a]; return c ? (f(d) && b[d] && n(!1, b[d]), b = this.legend, a = b.allItems[a].pageIx, d = b.currentPage, "undefined" !== typeof a && a + 1 !==
                        d && b.scroll(1 + a - d), a = c.legendItem, b = c.a11yProxyElement && c.a11yProxyElement.buttonElement, a && a.element && b && this.setFocusToElement(a, b), n(!0, c), !0) : !1
                } function c(a) { var b = a.item; this.chart.options.accessibility.enabled && b && b.a11yProxyElement && b.a11yProxyElement.buttonElement.setAttribute("aria-pressed", a.visible ? "true" : "false") } var e = []; a.compose = function (a, f) { -1 === e.indexOf(a) && (e.push(a), a.prototype.highlightLegendItem = b); -1 === e.indexOf(f) && (e.push(f), d(f, "afterColorizeItem", c)) }
            })(a || (a = {})); return a
    });
    t(a, "Accessibility/Components/SeriesComponent/SeriesDescriber.js", [a["Accessibility/Components/AnnotationsA11y.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/FormatUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, r, q, m) {
        function k(a) { var b = a.index; return a.series && a.series.data && A(b) ? D(a.series.data, function (a) { return !!(a && "undefined" !== typeof a.index && a.index > b && a.graphic && a.graphic.element) }) || null : null } function C(a) {
            var b = a.chart.options.accessibility.series.pointDescriptionEnabledThreshold;
            return !!(!1 !== b && a.points && a.points.length >= b)
        } function v(a) { var b = a.options.accessibility || {}; return !C(a) && !b.exposeAsGroupOnly } function g(a) { var b = a.chart.options.accessibility.keyboardNavigation.seriesNavigation; return !(!a.points || !(a.points.length < b.pointNavigationEnabledThreshold || !1 === b.pointNavigationEnabledThreshold)) } function n(a, b) {
            var d = a.series, c = d.chart; a = c.options.accessibility.point || {}; var e = d.options.accessibility && d.options.accessibility.point || {}; d = d.tooltipOptions || {}; c = c.options.lang;
            return F(b) ? I(b, e.valueDecimals || a.valueDecimals || d.valueDecimals || -1, c.decimalPoint, c.accessibility.thousandsSep || c.thousandsSep) : b
        } function x(a) { var b = (a.options.accessibility || {}).description; return b && a.chart.langFormat("accessibility.series.description", { description: b, series: a }) || "" } function c(a, b) { return a.chart.langFormat("accessibility.series." + b + "Description", { name: y(a[b]), series: a }) } function e(a, b, d) {
            var c = b || "", e = d || ""; return a.series.pointArrayMap.reduce(function (b, d) {
                b += b.length ? ", " :
                    ""; var l = n(a, p(a[d], a.options[d])); return b + (d + ": " + c + l + e)
            }, "")
        } function d(a) {
            var b = a.series, d = 1 < b.chart.series.length || b.options.name, c = a.series; var f = c.chart; var l = c.options.accessibility; l = l && l.point && l.point.valueDescriptionFormat || f.options.accessibility.point.valueDescriptionFormat; c = p(c.xAxis && c.xAxis.options.accessibility && c.xAxis.options.accessibility.enabled, !f.angular); if (c) {
                var g = a.series; var y = g.chart; var h = g.options.accessibility && g.options.accessibility.point || {}, k = y.options.accessibility.point ||
                    {}; (g = g.xAxis && g.xAxis.dateTime) ? (g = g.getXDateFormat(a.x || 0, y.options.tooltip.dateTimeLabelFormats), h = h.dateFormatter && h.dateFormatter(a) || k.dateFormatter && k.dateFormatter(a) || h.dateFormat || k.dateFormat || g, y = y.time.dateFormat(h, a.x || 0, void 0)) : y = void 0; h = (a.series.xAxis || {}).categories && A(a.category) && ("" + a.category).replace("<br/>", " "); k = a.id && 0 > a.id.indexOf("highcharts-"); g = "x, " + a.x; y = a.name || y || h || (k ? a.id : g)
            } else y = ""; h = A(a.index) ? a.index + 1 : ""; k = a.series; var x = k.chart.options.accessibility.point ||
                {}, z = k.chart.options.accessibility && k.chart.options.accessibility.point || {}, m = k.tooltipOptions || {}; g = z.valuePrefix || x.valuePrefix || m.valuePrefix || ""; x = z.valueSuffix || x.valueSuffix || m.valueSuffix || ""; z = n(a, a["undefined" !== typeof a.value ? "value" : "y"]); k = a.isNull ? k.chart.langFormat("accessibility.series.nullPointValue", { point: a }) : k.pointArrayMap ? e(a, g, x) : g + z + x; f = B(l, { point: a, index: h, xDescription: y, value: k, separator: c ? ", " : "" }, f); l = (l = a.options && a.options.accessibility && a.options.accessibility.description) ?
                    " " + l : ""; b = d ? " " + b.name + "." : ""; d = a.series.chart; c = u(a); y = { point: a, annotations: c }; d = c.length ? d.langFormat("accessibility.series.pointAnnotationsDescription", y) : ""; a.accessibility = a.accessibility || {}; a.accessibility.valueDescription = f; return f + l + b + (d ? " " + d : "")
        } function b(a) {
            var b = v(a), c = g(a); (b || c) && a.points.forEach(function (c) {
                var e; if (!(e = c.graphic && c.graphic.element) && (e = c.series && c.series.is("sunburst"), e = c.isNull && !e)) {
                    var f = c.series, l = k(c); f = (e = l && l.graphic) ? e.parentGroup : f.graph || f.group; l = l ?
                        { x: p(c.plotX, l.plotX, 0), y: p(c.plotY, l.plotY, 0) } : { x: p(c.plotX, 0), y: p(c.plotY, 0) }; l = c.series.chart.renderer.rect(l.x, l.y, 1, 1); l.attr({ "class": "highcharts-a11y-dummy-point", fill: "none", opacity: 0, "fill-opacity": 0, "stroke-opacity": 0 }); f && f.element ? (c.graphic = l, c.hasDummyGraphic = !0, l.add(f), f.element.insertBefore(l.element, e ? e.element : null), e = l.element) : e = void 0
                } f = c.options && c.options.accessibility && !1 === c.options.accessibility.enabled; e && (e.setAttribute("tabindex", "-1"), a.chart.styledMode || (e.style.outline =
                    "none"), b && !f ? (l = c.series, f = l.chart.options.accessibility.point || {}, l = l.options.accessibility && l.options.accessibility.point || {}, c = E(l.descriptionFormatter && l.descriptionFormatter(c) || f.descriptionFormatter && f.descriptionFormatter(c) || d(c)), e.setAttribute("role", "img"), e.setAttribute("aria-label", c)) : e.setAttribute("aria-hidden", !0))
            })
        } function f(a) {
            var b = a.chart, d = b.types || [], e = x(a), f = function (d) { return b[d] && 1 < b[d].length && a[d] }, l = c(a, "xAxis"), A = c(a, "yAxis"), u = {
                name: a.name || "", ix: a.index + 1, numSeries: b.series &&
                    b.series.length, numPoints: a.points && a.points.length, series: a
            }; d = 1 < d.length ? "Combination" : ""; return (b.langFormat("accessibility.series.summary." + a.type + d, u) || b.langFormat("accessibility.series.summary.default" + d, u)) + (e ? " " + e : "") + (f("yAxis") ? " " + A : "") + (f("xAxis") ? " " + l : "")
        } var u = a.getPointAnnotationTexts, y = h.getAxisDescription, M = h.getSeriesFirstPointElement, t = h.getSeriesA11yElement, H = h.unhideChartElementFromAT, B = r.format, I = r.numberFormat, z = q.reverseChildNodes, E = q.stripHTMLTagsFromString, D = m.find, F =
            m.isNumber, p = m.pick, A = m.defined; return {
                defaultPointDescriptionFormatter: d, defaultSeriesDescriptionFormatter: f, describeSeries: function (a) {
                    var d = a.chart, c = M(a), e = t(a), l = d.is3d && d.is3d(); if (e) {
                        e.lastChild !== c || l || z(e); b(a); H(d, e); l = a.chart; d = l.options.chart; c = 1 < l.series.length; l = l.options.accessibility.series.describeSingleSeries; var A = (a.options.accessibility || {}).exposeAsGroupOnly; d.options3d && d.options3d.enabled && c || !(c || l || A || C(a)) ? e.setAttribute("aria-label", "") : (d = a.chart.options.accessibility,
                            c = d.landmarkVerbosity, (a.options.accessibility || {}).exposeAsGroupOnly ? e.setAttribute("role", "img") : "all" === c && e.setAttribute("role", "region"), e.setAttribute("tabindex", "-1"), a.chart.styledMode || (e.style.outline = "none"), e.setAttribute("aria-label", E(d.series.descriptionFormatter && d.series.descriptionFormatter(a) || f(a))))
                    }
                }
            }
    }); t(a, "Accessibility/Components/SeriesComponent/NewDataAnnouncer.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/Utils/ChartUtilities.js"],
    a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"]], function (a, h, r, q, m, w) {
        function k(a) { var b = a.series.data.filter(function (b) { return a.x === b.x && a.y === b.y }); return 1 === b.length ? b[0] : a } function v(a, b) { var d = (a || []).concat(b || []).reduce(function (a, b) { a[b.name + b.index] = b; return a }, {}); return Object.keys(d).map(function (a) { return d[a] }) } var g = h.addEvent, n = h.defined, x = q.getChartTitle, c = w.defaultPointDescriptionFormatter, e = w.defaultSeriesDescriptionFormatter;
        h = function () {
            function d(a) { this.announcer = void 0; this.dirty = { allSeries: {} }; this.eventProvider = void 0; this.lastAnnouncementTime = 0; this.chart = a } d.prototype.init = function () { var a = this.chart, d = a.options.accessibility.announceNewData.interruptUser ? "assertive" : "polite"; this.lastAnnouncementTime = 0; this.dirty = { allSeries: {} }; this.eventProvider = new m; this.announcer = new r(a, d); this.addEventListeners() }; d.prototype.destroy = function () { this.eventProvider.removeAddedEvents(); this.announcer.destroy() }; d.prototype.addEventListeners =
                function () { var a = this, d = this.chart, c = this.eventProvider; c.addEvent(d, "afterApplyDrilldown", function () { a.lastAnnouncementTime = 0 }); c.addEvent(d, "afterAddSeries", function (b) { a.onSeriesAdded(b.series) }); c.addEvent(d, "redraw", function () { a.announceDirtyData() }) }; d.prototype.onSeriesAdded = function (a) { this.chart.options.accessibility.announceNewData.enabled && (this.dirty.hasDirty = !0, this.dirty.allSeries[a.name + a.index] = a, this.dirty.newSeries = n(this.dirty.newSeries) ? void 0 : a) }; d.prototype.announceDirtyData =
                    function () { var a = this; if (this.chart.options.accessibility.announceNewData && this.dirty.hasDirty) { var d = this.dirty.newPoint; d && (d = k(d)); this.queueAnnouncement(Object.keys(this.dirty.allSeries).map(function (b) { return a.dirty.allSeries[b] }), this.dirty.newSeries, d); this.dirty = { allSeries: {} } } }; d.prototype.queueAnnouncement = function (a, d, c) {
                        var b = this, e = this.chart.options.accessibility.announceNewData; if (e.enabled) {
                            var f = +new Date; e = Math.max(0, e.minAnnounceInterval - (f - this.lastAnnouncementTime)); a = v(this.queuedAnnouncement &&
                                this.queuedAnnouncement.series, a); if (d = this.buildAnnouncementMessage(a, d, c)) this.queuedAnnouncement && clearTimeout(this.queuedAnnouncementTimer), this.queuedAnnouncement = { time: f, message: d, series: a }, this.queuedAnnouncementTimer = setTimeout(function () { b && b.announcer && (b.lastAnnouncementTime = +new Date, b.announcer.announce(b.queuedAnnouncement.message), delete b.queuedAnnouncement, delete b.queuedAnnouncementTimer) }, e)
                        }
                    }; d.prototype.buildAnnouncementMessage = function (b, d, g) {
                        var f = this.chart, u = f.options.accessibility.announceNewData;
                        if (u.announcementFormatter && (b = u.announcementFormatter(b, d, g), !1 !== b)) return b.length ? b : null; b = a.charts && 1 < a.charts.length ? "Multiple" : "Single"; b = d ? "newSeriesAnnounce" + b : g ? "newPointAnnounce" + b : "newDataAnnounce"; u = x(f); return f.langFormat("accessibility.announceNewData." + b, { chartTitle: u, seriesDesc: d ? e(d) : null, pointDesc: g ? c(g) : null, point: g, series: d })
                    }; return d
        }(); (function (a) {
            function b(a) {
                var b = this.chart, d = this.newDataAnnouncer; d && d.chart === b && b.options.accessibility.announceNewData.enabled && (d.dirty.newPoint =
                    n(d.dirty.newPoint) ? void 0 : a.point)
            } function d() { var a = this.chart, b = this.newDataAnnouncer; b && b.chart === a && a.options.accessibility.announceNewData.enabled && (b.dirty.hasDirty = !0, b.dirty.allSeries[this.name + this.index] = this) } a.composedClasses = []; a.compose = function (c) { -1 === a.composedClasses.indexOf(c) && (a.composedClasses.push(c), g(c, "addPoint", b), g(c, "updatedData", d)) }
        })(h || (h = {})); return h
    }); t(a, "Accessibility/ProxyElement.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/EventProvider.js"],
    a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, r, q, m) {
        var k = a.doc, C = h.attr, v = h.css, g = h.merge, n = q.fireEventOnWrappedOrUnwrappedElement, x = m.cloneMouseEvent, c = m.cloneTouchEvent, e = m.getFakeMouseEvent, d = m.removeElement; return function () {
            function a(a, b, d, c) {
                this.chart = a; this.target = b; this.groupType = d; d = "ul" === d; this.eventProvider = new r; var e = d ? k.createElement("li") : null, f = this.buttonElement = k.createElement("button"); a.styledMode || this.hideButtonVisually(f);
                e ? (d && !a.styledMode && (e.style.listStyle = "none"), e.appendChild(f), this.element = e) : this.element = f; this.updateTarget(b, c)
            } a.prototype.click = function () { var a = this.getTargetPosition(); a.x += a.width / 2; a.y += a.height / 2; a = e("click", a); n(this.target.click, a) }; a.prototype.updateTarget = function (a, b) {
                this.target = a; this.updateCSSClassName(); var d = b || {}; Object.keys(d).forEach(function (a) { null === d[a] && delete d[a] }); C(this.buttonElement, g({ "aria-label": this.getTargetAttr(a.click, "aria-label") }, d)); this.eventProvider.removeAddedEvents();
                this.addProxyEventsToButton(this.buttonElement, a.click); this.refreshPosition()
            }; a.prototype.refreshPosition = function () { var a = this.getTargetPosition(); v(this.buttonElement, { width: (a.width || 1) + "px", height: (a.height || 1) + "px", left: (Math.round(a.x) || 0) + "px", top: (Math.round(a.y) || 0) + "px" }) }; a.prototype.remove = function () { this.eventProvider.removeAddedEvents(); d(this.element) }; a.prototype.updateCSSClassName = function () {
                var a = this.chart.legend; a = a.group && a.group.div; a = -1 < (a && a.className || "").indexOf("highcharts-no-tooltip");
                var b = -1 < (this.getTargetAttr(this.target.click, "class") || "").indexOf("highcharts-no-tooltip"); this.buttonElement.className = a || b ? "highcharts-a11y-proxy-button highcharts-no-tooltip" : "highcharts-a11y-proxy-button"
            }; a.prototype.addProxyEventsToButton = function (a, b) {
                var d = this; "click touchstart touchend touchcancel touchmove mouseover mouseenter mouseleave mouseout".split(" ").forEach(function (e) {
                    var f = 0 === e.indexOf("touch"); d.eventProvider.addEvent(a, e, function (a) {
                        var d = f ? c(a) : x(a); b && n(b, d); a.stopPropagation();
                        f || a.preventDefault()
                    }, { passive: !1 })
                })
            }; a.prototype.hideButtonVisually = function (a) { v(a, { borderWidth: 0, backgroundColor: "transparent", cursor: "pointer", outline: "none", opacity: .001, filter: "alpha(opacity=1)", zIndex: 999, overflow: "hidden", padding: 0, margin: 0, display: "block", position: "absolute", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)" }) }; a.prototype.getTargetPosition = function () {
                var a = this.target.click; a = a.element ? a.element : a; a = this.target.visual || a; if (this.chart.renderTo && a && a.getBoundingClientRect) {
                    a =
                    a.getBoundingClientRect(); var b = this.chart.pointer.getChartPosition(); return { x: (a.left - b.left) / b.scaleX, y: (a.top - b.top) / b.scaleY, width: a.right / b.scaleX - a.left / b.scaleX, height: a.bottom / b.scaleY - a.top / b.scaleY }
                } return { x: 0, y: 0, width: 1, height: 1 }
            }; a.prototype.getTargetAttr = function (a, b) { return a.element ? a.element.getAttribute(b) : a.getAttribute(b) }; return a
        }()
    }); t(a, "Accessibility/ProxyProvider.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/DOMElementProvider.js"],
    a["Accessibility/Utils/HTMLUtilities.js"], a["Accessibility/ProxyElement.js"]], function (a, h, r, q, m, w) {
        var k = a.doc, v = h.attr, g = h.css, n = r.unhideChartElementFromAT, x = m.removeElement, c = m.removeChildNodes; return function () {
            function a(a) { this.chart = a; this.domElementProvider = new q; this.groups = {}; this.groupOrder = []; this.beforeChartProxyPosContainer = this.createProxyPosContainer("before"); this.afterChartProxyPosContainer = this.createProxyPosContainer("after"); this.update() } a.prototype.addProxyElement = function (a,
                b, c) { var d = this.groups[a]; if (!d) throw Error("ProxyProvider.addProxyElement: Invalid group key " + a); a = new w(this.chart, b, d.type, c); d.proxyContainerElement.appendChild(a.element); d.proxyElements.push(a); return a }; a.prototype.addGroup = function (a, b, c) {
                    var d = this.groups[a]; if (d) return d.groupElement; d = this.domElementProvider.createElement(b); if (c && c.role && "div" !== b) { var e = this.domElementProvider.createElement("div"); e.appendChild(d) } else e = d; e.className = "highcharts-a11y-proxy-group highcharts-a11y-proxy-group-" +
                        a.replace(/\W/g, "-"); this.groups[a] = { proxyContainerElement: d, groupElement: e, type: b, proxyElements: [] }; v(e, c || {}); "ul" === b && d.setAttribute("role", "list"); this.afterChartProxyPosContainer.appendChild(e); this.updateGroupOrder(this.groupOrder); return e
                }; a.prototype.updateGroupAttrs = function (a, b) { var d = this.groups[a]; if (!d) throw Error("ProxyProvider.updateGroupAttrs: Invalid group key " + a); v(d.groupElement, b) }; a.prototype.updateGroupOrder = function (a) {
                    var b = this; this.groupOrder = a.slice(); if (!this.isDOMOrderGroupOrder()) {
                        var d =
                            a.indexOf("series"), e = -1 < d ? a.slice(0, d) : a, g = -1 < d ? a.slice(d + 1) : []; a = k.activeElement;["before", "after"].forEach(function (a) { var d = b["before" === a ? "beforeChartProxyPosContainer" : "afterChartProxyPosContainer"]; a = "before" === a ? e : g; c(d); a.forEach(function (a) { (a = b.groups[a]) && d.appendChild(a.groupElement) }) }); (this.beforeChartProxyPosContainer.contains(a) || this.afterChartProxyPosContainer.contains(a)) && a && a.focus && a.focus()
                    }
                }; a.prototype.clearGroup = function (a) {
                    var b = this.groups[a]; if (!b) throw Error("ProxyProvider.clearGroup: Invalid group key " +
                        a); c(b.proxyContainerElement)
                }; a.prototype.removeGroup = function (a) { var b = this.groups[a]; b && (x(b.groupElement), delete this.groups[a]) }; a.prototype.update = function () { this.updatePosContainerPositions(); this.updateGroupOrder(this.groupOrder); this.updateProxyElementPositions() }; a.prototype.updateProxyElementPositions = function () { Object.keys(this.groups).forEach(this.updateGroupProxyElementPositions.bind(this)) }; a.prototype.updateGroupProxyElementPositions = function (a) { (a = this.groups[a]) && a.proxyElements.forEach(function (a) { return a.refreshPosition() }) };
            a.prototype.destroy = function () { this.domElementProvider.destroyCreatedElements() }; a.prototype.createProxyPosContainer = function (a) { var b = this.domElementProvider.createElement("div"); b.setAttribute("aria-hidden", "false"); b.className = "highcharts-a11y-proxy-container" + (a ? "-" + a : ""); g(b, { top: "0", left: "0" }); this.chart.styledMode || (b.style.whiteSpace = "nowrap", b.style.position = "absolute"); return b }; a.prototype.getCurrentGroupOrderInDOM = function () {
                var a = this, b = function (b) {
                    var d = []; b = b.children; for (var c = 0; c <
                        b.length; ++c) { a: { var e = b[c]; for (var f = Object.keys(a.groups), g = f.length; g--;) { var h = f[g], n = a.groups[h]; if (n && e === n.groupElement) { e = h; break a } } e = void 0 } e && d.push(e) } return d
                }, c = b(this.beforeChartProxyPosContainer); b = b(this.afterChartProxyPosContainer); c.push("series"); return c.concat(b)
            }; a.prototype.isDOMOrderGroupOrder = function () {
                var a = this, b = this.getCurrentGroupOrderInDOM(), c = this.groupOrder.filter(function (b) { return "series" === b || !!a.groups[b] }), e = b.length; if (e !== c.length) return !1; for (; e--;)if (b[e] !==
                    c[e]) return !1; return !0
            }; a.prototype.updatePosContainerPositions = function () { var a = this.chart; if (!a.renderer.forExport) { var b = a.renderer.box; a.container.insertBefore(this.afterChartProxyPosContainer, b.nextSibling); a.container.insertBefore(this.beforeChartProxyPosContainer, b); n(this.chart, this.afterChartProxyPosContainer); n(this.chart, this.beforeChartProxyPosContainer) } }; return a
        }()
    }); t(a, "Extensions/RangeSelector.js", [a["Core/Axis/Axis.js"], a["Core/Chart/Chart.js"], a["Core/Globals.js"], a["Core/DefaultOptions.js"],
    a["Core/Renderer/SVG/SVGElement.js"], a["Core/Utilities.js"]], function (a, h, r, q, m, w) {
        function k(a) { if (-1 !== a.indexOf("%L")) return "text"; var b = "aAdewbBmoyY".split("").some(function (b) { return -1 !== a.indexOf("%" + b) }), d = "HkIlMS".split("").some(function (b) { return -1 !== a.indexOf("%" + b) }); return b && d ? "datetime-local" : b ? "date" : d ? "time" : "text" } var v = q.defaultOptions, g = w.addEvent, n = w.createElement, x = w.css, c = w.defined, e = w.destroyObjectProperties, d = w.discardElement, b = w.extend, f = w.find, u = w.fireEvent, y = w.isNumber,
            t = w.merge, G = w.objectEach, H = w.pad, B = w.pick, I = w.pInt, z = w.splat; b(v, {
                rangeSelector: {
                    allButtonsEnabled: !1, buttons: void 0, buttonSpacing: 5, dropdown: "responsive", enabled: void 0, verticalAlign: "top", buttonTheme: { width: 28, height: 18, padding: 2, zIndex: 7 }, floating: !1, x: 0, y: 0, height: void 0, inputBoxBorderColor: "none", inputBoxHeight: 17, inputBoxWidth: void 0, inputDateFormat: "%b %e, %Y", inputDateParser: void 0, inputEditDateFormat: "%Y-%m-%d", inputEnabled: !0, inputPosition: { align: "right", x: 0, y: 0 }, inputSpacing: 5, selected: void 0,
                    buttonPosition: { align: "left", x: 0, y: 0 }, inputStyle: { color: "#335cad", cursor: "pointer" }, labelStyle: { color: "#666666" }
                }
            }); b(v.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "", rangeSelectorTo: "\u2192" }); var E = function () {
                function f(a) { this.buttons = void 0; this.buttonOptions = f.prototype.defaultButtons; this.initialButtonGroupWidth = 0; this.options = void 0; this.chart = a; this.init(a) } f.prototype.clickButton = function (b, d) {
                    var e = this.chart, l = this.buttonOptions[b], f = e.xAxis[0], A = e.scroller && e.scroller.getUnionExtremes() ||
                        f || {}, h = A.dataMin, n = A.dataMax, p = f && Math.round(Math.min(f.max, B(n, f.max))), k = l.type; A = l._range; var x, m = l.dataGrouping; if (null !== h && null !== n) {
                            e.fixedRange = A; this.setSelected(b); m && (this.forcedDataGrouping = !0, a.prototype.setDataGrouping.call(f || { chart: this.chart }, m, !1), this.frozenStates = l.preserveDataGrouping); if ("month" === k || "year" === k) if (f) { k = { range: l, max: p, chart: e, dataMin: h, dataMax: n }; var q = f.minFromRange.call(k); y(k.newMax) && (p = k.newMax) } else A = l; else if (A) q = Math.max(p - A, h), p = Math.min(q + A, n); else if ("ytd" ===
                                k) if (f) "undefined" === typeof n && (h = Number.MAX_VALUE, n = Number.MIN_VALUE, e.series.forEach(function (a) { a = a.xData; h = Math.min(a[0], h); n = Math.max(a[a.length - 1], n) }), d = !1), p = this.getYTDExtremes(n, h, e.time.useUTC), q = x = p.min, p = p.max; else { this.deferredYTDClick = b; return } else "all" === k && f && (e.navigator && e.navigator.baseSeries[0] && (e.navigator.baseSeries[0].xAxis.options.range = void 0), q = h, p = n); c(q) && (q += l._offsetMin); c(p) && (p += l._offsetMax); this.dropdown && (this.dropdown.selectedIndex = b + 1); if (f) f.setExtremes(q, p,
                                    B(d, !0), void 0, { trigger: "rangeSelectorButton", rangeSelectorButton: l }); else { var r = z(e.options.xAxis)[0]; var v = r.range; r.range = A; var w = r.min; r.min = x; g(e, "load", function () { r.range = v; r.min = w }) } u(this, "afterBtnClick")
                        }
                }; f.prototype.setSelected = function (a) { this.selected = this.options.selected = a }; f.prototype.init = function (a) {
                    var b = this, d = a.options.rangeSelector, c = d.buttons || b.defaultButtons.slice(), e = d.selected, f = function () { var a = b.minInput, d = b.maxInput; a && a.blur && u(a, "blur"); d && d.blur && u(d, "blur") }; b.chart =
                        a; b.options = d; b.buttons = []; b.buttonOptions = c; this.eventsToUnbind = []; this.eventsToUnbind.push(g(a.container, "mousedown", f)); this.eventsToUnbind.push(g(a, "resize", f)); c.forEach(b.computeButtonRange); "undefined" !== typeof e && c[e] && this.clickButton(e, !1); this.eventsToUnbind.push(g(a, "load", function () {
                            a.xAxis && a.xAxis[0] && g(a.xAxis[0], "setExtremes", function (d) {
                                this.max - this.min !== a.fixedRange && "rangeSelectorButton" !== d.trigger && "updatedData" !== d.trigger && b.forcedDataGrouping && !b.frozenStates && this.setDataGrouping(!1,
                                    !1)
                            })
                        }))
                }; f.prototype.updateButtonStates = function () {
                    var a = this, b = this.chart, d = this.dropdown, c = b.xAxis[0], e = Math.round(c.max - c.min), f = !c.hasVisibleSeries, g = b.scroller && b.scroller.getUnionExtremes() || c, h = g.dataMin, p = g.dataMax; b = a.getYTDExtremes(p, h, b.time.useUTC); var n = b.min, k = b.max, u = a.selected, x = y(u), m = a.options.allButtonsEnabled, z = a.buttons; a.buttonOptions.forEach(function (b, l) {
                        var A = b._range, g = b.type, J = b.count || 1, L = z[l], K = 0, N = b._offsetMax - b._offsetMin; b = l === u; var y = A > p - h, q = A < c.minRange, r = !1, B = !1;
                        A = A === e; ("month" === g || "year" === g) && e + 36E5 >= 864E5 * { month: 28, year: 365 }[g] * J - N && e - 36E5 <= 864E5 * { month: 31, year: 366 }[g] * J + N ? A = !0 : "ytd" === g ? (A = k - n + N === e, r = !b) : "all" === g && (A = c.max - c.min >= p - h, B = !b && x && A); g = !m && (y || q || B || f); J = b && A || A && !x && !r || b && a.frozenStates; g ? K = 3 : J && (x = !0, K = 2); L.state !== K && (L.setState(K), d && (d.options[l + 1].disabled = g, 2 === K && (d.selectedIndex = l + 1)), 0 === K && u === l && a.setSelected())
                    })
                }; f.prototype.computeButtonRange = function (a) {
                    var b = a.type, d = a.count || 1, c = {
                        millisecond: 1, second: 1E3, minute: 6E4,
                        hour: 36E5, day: 864E5, week: 6048E5
                    }; if (c[b]) a._range = c[b] * d; else if ("month" === b || "year" === b) a._range = 864E5 * { month: 30, year: 365 }[b] * d; a._offsetMin = B(a.offsetMin, 0); a._offsetMax = B(a.offsetMax, 0); a._range += a._offsetMax - a._offsetMin
                }; f.prototype.getInputValue = function (a) { a = "min" === a ? this.minInput : this.maxInput; var b = this.chart.options.rangeSelector, d = this.chart.time; return a ? ("text" === a.type && b.inputDateParser || this.defaultInputDateParser)(a.value, d.useUTC, d) : 0 }; f.prototype.setInputValue = function (a, b) {
                    var d =
                        this.options, e = this.chart.time, f = "min" === a ? this.minInput : this.maxInput; a = "min" === a ? this.minDateBox : this.maxDateBox; if (f) { var l = f.getAttribute("data-hc-time"); l = c(l) ? Number(l) : void 0; c(b) && (c(l) && f.setAttribute("data-hc-time-previous", l), f.setAttribute("data-hc-time", b), l = b); f.value = e.dateFormat(this.inputTypeFormats[f.type] || d.inputEditDateFormat, l); a && a.attr({ text: e.dateFormat(d.inputDateFormat, l) }) }
                }; f.prototype.setInputExtremes = function (a, b, d) {
                    if (a = "min" === a ? this.minInput : this.maxInput) {
                        var c =
                            this.inputTypeFormats[a.type], e = this.chart.time; c && (b = e.dateFormat(c, b), a.min !== b && (a.min = b), d = e.dateFormat(c, d), a.max !== d && (a.max = d))
                    }
                }; f.prototype.showInput = function (a) {
                    var b = "min" === a ? this.minDateBox : this.maxDateBox; if ((a = "min" === a ? this.minInput : this.maxInput) && b && this.inputGroup) {
                        var d = "text" === a.type, c = this.inputGroup, e = c.translateX; c = c.translateY; var f = this.options.inputBoxWidth; x(a, { width: d ? b.width + (f ? -2 : 20) + "px" : "auto", height: d ? b.height - 2 + "px" : "auto", border: "2px solid silver" }); d && f ? x(a, {
                            left: e +
                                b.x + "px", top: c + "px"
                        }) : x(a, { left: Math.min(Math.round(b.x + e - (a.offsetWidth - b.width) / 2), this.chart.chartWidth - a.offsetWidth) + "px", top: c - (a.offsetHeight - b.height) / 2 + "px" })
                    }
                }; f.prototype.hideInput = function (a) { (a = "min" === a ? this.minInput : this.maxInput) && x(a, { top: "-9999em", border: 0, width: "1px", height: "1px" }) }; f.prototype.defaultInputDateParser = function (a, b, d) {
                    var c = a.split("/").join("-").split(" ").join("T"); -1 === c.indexOf("T") && (c += "T00:00"); if (b) c += "Z"; else {
                        var e; if (e = r.isSafari) e = c, e = !(6 < e.length && (e.lastIndexOf("-") ===
                            e.length - 6 || e.lastIndexOf("+") === e.length - 6)); e && (e = (new Date(c)).getTimezoneOffset() / 60, c += 0 >= e ? "+" + H(-e) + ":00" : "-" + H(e) + ":00")
                    } c = Date.parse(c); y(c) || (a = a.split("-"), c = Date.UTC(I(a[0]), I(a[1]) - 1, I(a[2]))); d && b && y(c) && (c += d.getTimezoneOffset(c)); return c
                }; f.prototype.drawInput = function (a) {
                    function d() {
                        var b = g.getInputValue(a), d = c.xAxis[0], e = c.scroller && c.scroller.xAxis ? c.scroller.xAxis : d, f = e.dataMin; e = e.dataMax; var l = g.maxInput, A = g.minInput; b !== Number(z.getAttribute("data-hc-time-previous")) && y(b) &&
                            (z.setAttribute("data-hc-time-previous", b), u && l && y(f) ? b > Number(l.getAttribute("data-hc-time")) ? b = void 0 : b < f && (b = f) : A && y(e) && (b < Number(A.getAttribute("data-hc-time")) ? b = void 0 : b > e && (b = e)), "undefined" !== typeof b && d.setExtremes(u ? b : d.min, u ? d.max : b, void 0, void 0, { trigger: "rangeSelectorInput" }))
                    } var c = this.chart, e = this.div, f = this.inputGroup, g = this, A = c.renderer.style || {}, h = c.renderer, p = c.options.rangeSelector, u = "min" === a, m = v.lang[u ? "rangeSelectorFrom" : "rangeSelectorTo"] || ""; m = h.label(m, 0).addClass("highcharts-range-label").attr({
                        padding: m ?
                            2 : 0, height: m ? p.inputBoxHeight : 0
                    }).add(f); h = h.label("", 0).addClass("highcharts-range-input").attr({ padding: 2, width: p.inputBoxWidth, height: p.inputBoxHeight, "text-align": "center" }).on("click", function () { g.showInput(a); g[a + "Input"].focus() }); c.styledMode || h.attr({ stroke: p.inputBoxBorderColor, "stroke-width": 1 }); h.add(f); var z = n("input", { name: a, className: "highcharts-range-selector" }, void 0, e); z.setAttribute("type", k(p.inputDateFormat || "%b %e, %Y")); c.styledMode || (m.css(t(A, p.labelStyle)), h.css(t({ color: "#333333" },
                        A, p.inputStyle)), x(z, b({ position: "absolute", border: 0, boxShadow: "0 0 15px rgba(0,0,0,0.3)", width: "1px", height: "1px", padding: 0, textAlign: "center", fontSize: A.fontSize, fontFamily: A.fontFamily, top: "-9999em" }, p.inputStyle))); z.onfocus = function () { g.showInput(a) }; z.onblur = function () { z === r.doc.activeElement && d(); g.hideInput(a); g.setInputValue(a); z.blur() }; var q = !1; z.onchange = function () { q || (d(), g.hideInput(a), z.blur()) }; z.onkeypress = function (a) { 13 === a.keyCode && d() }; z.onkeydown = function (a) {
                            q = !0; 38 !== a.keyCode &&
                                40 !== a.keyCode || d()
                        }; z.onkeyup = function () { q = !1 }; return { dateBox: h, input: z, label: m }
                }; f.prototype.getPosition = function () { var a = this.chart, b = a.options.rangeSelector; a = "top" === b.verticalAlign ? a.plotTop - a.axisOffset[0] : 0; return { buttonTop: a + b.buttonPosition.y, inputTop: a + b.inputPosition.y - 10 } }; f.prototype.getYTDExtremes = function (a, b, d) { var c = this.chart.time, e = new c.Date(a), f = c.get("FullYear", e); d = d ? c.Date.UTC(f, 0, 1) : +new c.Date(f, 0, 1); b = Math.max(b, d); e = e.getTime(); return { max: Math.min(a || e, e), min: b } }; f.prototype.render =
                    function (a, b) {
                        var d = this.chart, e = d.renderer, f = d.container, l = d.options, g = l.rangeSelector, h = B(l.chart.style && l.chart.style.zIndex, 0) + 1; l = g.inputEnabled; if (!1 !== g.enabled) {
                            this.rendered || (this.group = e.g("range-selector-group").attr({ zIndex: 7 }).add(), this.div = n("div", void 0, { position: "relative", height: 0, zIndex: h }), this.buttonOptions.length && this.renderButtons(), f.parentNode && f.parentNode.insertBefore(this.div, f), l && (this.inputGroup = e.g("input-group").add(this.group), e = this.drawInput("min"), this.minDateBox =
                                e.dateBox, this.minLabel = e.label, this.minInput = e.input, e = this.drawInput("max"), this.maxDateBox = e.dateBox, this.maxLabel = e.label, this.maxInput = e.input)); if (l && (this.setInputValue("min", a), this.setInputValue("max", b), a = d.scroller && d.scroller.getUnionExtremes() || d.xAxis[0] || {}, c(a.dataMin) && c(a.dataMax) && (d = d.xAxis[0].minRange || 0, this.setInputExtremes("min", a.dataMin, Math.min(a.dataMax, this.getInputValue("max")) - d), this.setInputExtremes("max", Math.max(a.dataMin, this.getInputValue("min")) + d, a.dataMax)),
                                    this.inputGroup)) { var A = 0;[this.minLabel, this.minDateBox, this.maxLabel, this.maxDateBox].forEach(function (a) { if (a) { var b = a.getBBox().width; b && (a.attr({ x: A }), A += b + g.inputSpacing) } }) } this.alignElements(); this.rendered = !0
                        }
                    }; f.prototype.renderButtons = function () {
                        var a = this, b = this.buttons, d = this.options, c = v.lang, e = this.chart.renderer, f = t(d.buttonTheme), h = f && f.states, p = f.width || 28; delete f.width; delete f.states; this.buttonGroup = e.g("range-selector-buttons").add(this.group); var k = this.dropdown = n("select",
                            void 0, { position: "absolute", width: "1px", height: "1px", padding: 0, border: 0, top: "-9999em", cursor: "pointer", opacity: .0001 }, this.div); g(k, "touchstart", function () { k.style.fontSize = "16px" });[[r.isMS ? "mouseover" : "mouseenter"], [r.isMS ? "mouseout" : "mouseleave"], ["change", "click"]].forEach(function (d) { var c = d[0], e = d[1]; g(k, c, function () { var d = b[a.currentButtonIndex()]; d && u(d.element, e || c) }) }); this.zoomText = e.label(c && c.rangeSelectorZoom || "", 0).attr({
                                padding: d.buttonTheme.padding, height: d.buttonTheme.height, paddingLeft: 0,
                                paddingRight: 0
                            }).add(this.buttonGroup); this.chart.styledMode || (this.zoomText.css(d.labelStyle), f["stroke-width"] = B(f["stroke-width"], 0)); n("option", { textContent: this.zoomText.textStr, disabled: !0 }, void 0, k); this.buttonOptions.forEach(function (d, c) {
                                n("option", { textContent: d.title || d.text }, void 0, k); b[c] = e.button(d.text, 0, 0, function (b) { var e = d.events && d.events.click, f; e && (f = e.call(d, b)); !1 !== f && a.clickButton(c); a.isActive = !0 }, f, h && h.hover, h && h.select, h && h.disabled).attr({ "text-align": "center", width: p }).add(a.buttonGroup);
                                d.title && b[c].attr("title", d.title)
                            })
                    }; f.prototype.alignElements = function () {
                        var a = this, b = this.buttonGroup, d = this.buttons, c = this.chart, e = this.group, f = this.inputGroup, g = this.options, h = this.zoomText, p = c.options, n = p.exporting && !1 !== p.exporting.enabled && p.navigation && p.navigation.buttonOptions; p = g.buttonPosition; var k = g.inputPosition, u = g.verticalAlign, z = function (b, d) { return n && a.titleCollision(c) && "top" === u && "right" === d.align && d.y - b.getBBox().height - 12 < (n.y || 0) + (n.height || 0) + c.spacing[0] ? -40 : 0 }, x = c.plotLeft;
                        if (e && p && k) {
                            var m = p.x - c.spacing[3]; if (b) { this.positionButtons(); if (!this.initialButtonGroupWidth) { var y = 0; h && (y += h.getBBox().width + 5); d.forEach(function (a, b) { y += a.width; b !== d.length - 1 && (y += g.buttonSpacing) }); this.initialButtonGroupWidth = y } x -= c.spacing[3]; this.updateButtonStates(); h = z(b, p); this.alignButtonGroup(h); e.placed = b.placed = c.hasLoaded } b = 0; f && (b = z(f, k), "left" === k.align ? m = x : "right" === k.align && (m = -Math.max(c.axisOffset[1], -b)), f.align({ y: k.y, width: f.getBBox().width, align: k.align, x: k.x + m - 2 }, !0,
                                c.spacingBox), f.placed = c.hasLoaded); this.handleCollision(b); e.align({ verticalAlign: u }, !0, c.spacingBox); f = e.alignAttr.translateY; b = e.getBBox().height + 20; z = 0; "bottom" === u && (z = (z = c.legend && c.legend.options) && "bottom" === z.verticalAlign && z.enabled && !z.floating ? c.legend.legendHeight + B(z.margin, 10) : 0, b = b + z - 20, z = f - b - (g.floating ? 0 : g.y) - (c.titleOffset ? c.titleOffset[2] : 0) - 10); if ("top" === u) g.floating && (z = 0), c.titleOffset && c.titleOffset[0] && (z = c.titleOffset[0]), z += c.margin[0] - c.spacing[0] || 0; else if ("middle" ===
                                    u) if (k.y === p.y) z = f; else if (k.y || p.y) z = 0 > k.y || 0 > p.y ? z - Math.min(k.y, p.y) : f - b; e.translate(g.x, g.y + Math.floor(z)); p = this.minInput; k = this.maxInput; f = this.dropdown; g.inputEnabled && p && k && (p.style.marginTop = e.translateY + "px", k.style.marginTop = e.translateY + "px"); f && (f.style.marginTop = e.translateY + "px")
                        }
                    }; f.prototype.alignButtonGroup = function (a, b) {
                        var d = this.chart, c = this.buttonGroup, e = this.options.buttonPosition, f = d.plotLeft - d.spacing[3], g = e.x - d.spacing[3]; "right" === e.align ? g += a - f : "center" === e.align && (g -= f /
                            2); c && c.align({ y: e.y, width: B(b, this.initialButtonGroupWidth), align: e.align, x: g }, !0, d.spacingBox)
                    }; f.prototype.positionButtons = function () { var a = this.buttons, b = this.chart, d = this.options, c = this.zoomText, e = b.hasLoaded ? "animate" : "attr", f = d.buttonPosition, g = b.plotLeft, h = g; c && "hidden" !== c.visibility && (c[e]({ x: B(g + f.x, g) }), h += f.x + c.getBBox().width + 5); this.buttonOptions.forEach(function (b, c) { if ("hidden" !== a[c].visibility) a[c][e]({ x: h }), h += a[c].width + d.buttonSpacing; else a[c][e]({ x: g }) }) }; f.prototype.handleCollision =
                        function (a) {
                            var b = this, d = this.chart, c = this.buttonGroup, e = this.inputGroup, f = this.options, g = f.buttonPosition, h = f.dropdown, p = f.inputPosition; f = function () { var a = 0; b.buttons.forEach(function (b) { b = b.getBBox(); b.width > a && (a = b.width) }); return a }; var k = function (b) { if (e && c) { var d = e.alignAttr.translateX + e.alignOptions.x - a + e.getBBox().x + 2, f = e.alignOptions.width, l = c.alignAttr.translateX + c.getBBox().x; return l + b > d && d + f > l && g.y < p.y + e.getBBox().height } return !1 }, n = function () {
                                e && c && e.attr({
                                    translateX: e.alignAttr.translateX +
                                        (d.axisOffset[1] >= -a ? 0 : -a), translateY: e.alignAttr.translateY + c.getBBox().height + 10
                                })
                            }; if (c) { if ("always" === h) { this.collapseButtons(a); k(f()) && n(); return } "never" === h && this.expandButtons() } e && c ? p.align === g.align || k(this.initialButtonGroupWidth + 20) ? "responsive" === h ? (this.collapseButtons(a), k(f()) && n()) : n() : "responsive" === h && this.expandButtons() : c && "responsive" === h && (this.initialButtonGroupWidth > d.plotWidth ? this.collapseButtons(a) : this.expandButtons())
                        }; f.prototype.collapseButtons = function (a) {
                            var b = this.buttons,
                            d = this.buttonOptions, c = this.chart, e = this.dropdown, f = this.options, g = this.zoomText, h = c.userOptions.rangeSelector && c.userOptions.rangeSelector.buttonTheme || {}, p = function (a) { return { text: a ? a + " \u25be" : "\u25be", width: "auto", paddingLeft: B(f.buttonTheme.paddingLeft, h.padding, 8), paddingRight: B(f.buttonTheme.paddingRight, h.padding, 8) } }; g && g.hide(); var k = !1; d.forEach(function (a, d) { d = b[d]; 2 !== d.state ? d.hide() : (d.show(), d.attr(p(a.text)), k = !0) }); k || (e && (e.selectedIndex = 0), b[0].show(), b[0].attr(p(this.zoomText &&
                                this.zoomText.textStr))); d = f.buttonPosition.align; this.positionButtons(); "right" !== d && "center" !== d || this.alignButtonGroup(a, b[this.currentButtonIndex()].getBBox().width); this.showDropdown()
                        }; f.prototype.expandButtons = function () {
                            var a = this.buttons, b = this.buttonOptions, d = this.options, c = this.zoomText; this.hideDropdown(); c && c.show(); b.forEach(function (b, c) {
                                c = a[c]; c.show(); c.attr({
                                    text: b.text, width: d.buttonTheme.width || 28, paddingLeft: B(d.buttonTheme.paddingLeft, "unset"), paddingRight: B(d.buttonTheme.paddingRight,
                                        "unset")
                                }); 2 > c.state && c.setState(0)
                            }); this.positionButtons()
                        }; f.prototype.currentButtonIndex = function () { var a = this.dropdown; return a && 0 < a.selectedIndex ? a.selectedIndex - 1 : 0 }; f.prototype.showDropdown = function () { var a = this.buttonGroup, b = this.buttons, d = this.chart, c = this.dropdown; if (a && c) { var e = a.translateX; a = a.translateY; b = b[this.currentButtonIndex()].getBBox(); x(c, { left: d.plotLeft + e + "px", top: a + .5 + "px", width: b.width + "px", height: b.height + "px" }); this.hasVisibleDropdown = !0 } }; f.prototype.hideDropdown = function () {
                            var a =
                                this.dropdown; a && (x(a, { top: "-9999em", width: "1px", height: "1px" }), this.hasVisibleDropdown = !1)
                        }; f.prototype.getHeight = function () { var a = this.options, b = this.group, d = a.y, c = a.buttonPosition.y, e = a.inputPosition.y; if (a.height) return a.height; this.alignElements(); a = b ? b.getBBox(!0).height + 13 + d : 0; b = Math.min(e, c); if (0 > e && 0 > c || 0 < e && 0 < c) a += Math.abs(b); return a }; f.prototype.titleCollision = function (a) { return !(a.options.title.text || a.options.subtitle.text) }; f.prototype.update = function (a) {
                            var b = this.chart; t(!0, b.options.rangeSelector,
                                a); this.destroy(); this.init(b); this.render()
                        }; f.prototype.destroy = function () { var a = this, b = a.minInput, c = a.maxInput; a.eventsToUnbind && (a.eventsToUnbind.forEach(function (a) { return a() }), a.eventsToUnbind = void 0); e(a.buttons); b && (b.onfocus = b.onblur = b.onchange = null); c && (c.onfocus = c.onblur = c.onchange = null); G(a, function (b, c) { b && "chart" !== c && (b instanceof m ? b.destroy() : b instanceof window.HTMLElement && d(b)); b !== f.prototype[c] && (a[c] = null) }, this) }; return f
            }(); E.prototype.defaultButtons = [{
                type: "month", count: 1,
                text: "1m", title: "View 1 month"
            }, { type: "month", count: 3, text: "3m", title: "View 3 months" }, { type: "month", count: 6, text: "6m", title: "View 6 months" }, { type: "ytd", text: "YTD", title: "View year to date" }, { type: "year", count: 1, text: "1y", title: "View 1 year" }, { type: "all", text: "All", title: "View all" }]; E.prototype.inputTypeFormats = { "datetime-local": "%Y-%m-%dT%H:%M:%S", date: "%Y-%m-%d", time: "%H:%M:%S" }; a.prototype.minFromRange = function () {
                var a = this.range, b = a.type, d = this.max, c = this.chart.time, e = function (a, d) {
                    var e = "year" ===
                        b ? "FullYear" : "Month", f = new c.Date(a), g = c.get(e, f); c.set(e, f, g + d); g === c.get(e, f) && c.set("Date", f, 0); return f.getTime() - a
                }; if (y(a)) { var f = d - a; var g = a } else f = d + e(d, -a.count), this.chart && (this.chart.fixedRange = d - f); var h = B(this.dataMin, Number.MIN_VALUE); y(f) || (f = h); f <= h && (f = h, "undefined" === typeof g && (g = e(f, a.count)), this.newMax = Math.min(f + g, this.dataMax)); y(d) || (f = void 0); return f
            }; if (!r.RangeSelector) {
                var D = [], F = function (a) {
                    function b() {
                        c && (d = a.xAxis[0].getExtremes(), e = a.legend, p = c && c.options.verticalAlign,
                            y(d.min) && c.render(d.min, d.max), e.display && "top" === p && p === e.options.verticalAlign && (h = t(a.spacingBox), h.y = "vertical" === e.options.layout ? a.plotTop : h.y + c.getHeight(), e.group.placed = !1, e.align(h)))
                    } var d, c = a.rangeSelector, e, h, p; c && (f(D, function (b) { return b[0] === a }) || D.push([a, [g(a.xAxis[0], "afterSetExtremes", function (a) { c && c.render(a.min, a.max) }), g(a, "redraw", b)]]), b())
                }; g(h, "afterGetContainer", function () { this.options.rangeSelector && this.options.rangeSelector.enabled && (this.rangeSelector = new E(this)) });
                g(h, "beforeRender", function () { var a = this.axes, b = this.rangeSelector; b && (y(b.deferredYTDClick) && (b.clickButton(b.deferredYTDClick), delete b.deferredYTDClick), a.forEach(function (a) { a.updateNames(); a.setScale() }), this.getAxisMargins(), b.render(), a = b.options.verticalAlign, b.options.floating || ("bottom" === a ? this.extraBottomMargin = !0 : "middle" !== a && (this.extraTopMargin = !0))) }); g(h, "update", function (a) {
                    var b = a.options.rangeSelector; a = this.rangeSelector; var d = this.extraBottomMargin, e = this.extraTopMargin; b &&
                        b.enabled && !c(a) && this.options.rangeSelector && (this.options.rangeSelector.enabled = !0, this.rangeSelector = a = new E(this)); this.extraTopMargin = this.extraBottomMargin = !1; a && (F(this), b = b && b.verticalAlign || a.options && a.options.verticalAlign, a.options.floating || ("bottom" === b ? this.extraBottomMargin = !0 : "middle" !== b && (this.extraTopMargin = !0)), this.extraBottomMargin !== d || this.extraTopMargin !== e) && (this.isDirtyBox = !0)
                }); g(h, "render", function () {
                    var a = this.rangeSelector; a && !a.options.floating && (a.render(), a = a.options.verticalAlign,
                        "bottom" === a ? this.extraBottomMargin = !0 : "middle" !== a && (this.extraTopMargin = !0))
                }); g(h, "getMargins", function () { var a = this.rangeSelector; a && (a = a.getHeight(), this.extraTopMargin && (this.plotTop += a), this.extraBottomMargin && (this.marginBottom += a)) }); h.prototype.callbacks.push(F); g(h, "destroy", function () { for (var a = 0; a < D.length; a++) { var b = D[a]; if (b[0] === this) { b[1].forEach(function (a) { return a() }); D.splice(a, 1); break } } }); r.RangeSelector = E
            } return E
    }); t(a, "Accessibility/Components/RangeSelectorComponent.js",
        [a["Extensions/RangeSelector.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Core/Utilities.js"]], function (a, h, r, q, m, w) {
            var k = this && this.__extends || function () {
                var a = function (c, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, d) { a.__proto__ = d } || function (a, d) { for (var b in d) d.hasOwnProperty(b) && (a[b] = d[b]) }; return a(c, d) }; return function (c, d) {
                    function b() {
                        this.constructor =
                        c
                    } a(c, d); c.prototype = null === d ? Object.create(d) : (b.prototype = d.prototype, new b)
                }
            }(), v = r.unhideChartElementFromAT, g = r.getAxisRangeDescription, n = w.addEvent, x = w.attr; h = function (a) {
                function c() { var d = null !== a && a.apply(this, arguments) || this; d.announcer = void 0; return d } k(c, a); c.prototype.init = function () { this.announcer = new q(this.chart, "polite") }; c.prototype.onChartUpdate = function () {
                    var a = this.chart, b = this, c = a.rangeSelector; c && (this.updateSelectorVisibility(), this.setDropdownAttrs(), c.buttons && c.buttons.length &&
                        c.buttons.forEach(function (a) { b.setRangeButtonAttrs(a) }), c.maxInput && c.minInput && ["minInput", "maxInput"].forEach(function (d, e) { if (d = c[d]) v(a, d), b.setRangeInputAttrs(d, "accessibility.rangeSelector." + (e ? "max" : "min") + "InputLabel") }))
                }; c.prototype.updateSelectorVisibility = function () {
                    var a = this.chart, b = a.rangeSelector, c = b && b.dropdown, e = b && b.buttons || []; b && b.hasVisibleDropdown && c ? (v(a, c), e.forEach(function (a) { return a.element.setAttribute("aria-hidden", !0) })) : (c && c.setAttribute("aria-hidden", !0), e.forEach(function (b) {
                        return v(a,
                            b.element)
                    }))
                }; c.prototype.setDropdownAttrs = function () { var a = this.chart, b = a.rangeSelector && a.rangeSelector.dropdown; b && (a = a.langFormat("accessibility.rangeSelector.dropdownLabel", { rangeTitle: a.options.lang.rangeSelectorZoom }), b.setAttribute("aria-label", a), b.setAttribute("tabindex", -1)) }; c.prototype.setRangeButtonAttrs = function (a) { x(a.element, { tabindex: -1, role: "button" }) }; c.prototype.setRangeInputAttrs = function (a, b) { var c = this.chart; x(a, { tabindex: -1, "aria-label": c.langFormat(b, { chart: c }) }) }; c.prototype.onButtonNavKbdArrowKey =
                    function (a, b) { var c = a.response, d = this.keyCodes, e = this.chart, g = e.options.accessibility.keyboardNavigation.wrapAround; b = b === d.left || b === d.up ? -1 : 1; return e.highlightRangeSelectorButton(e.highlightedRangeSelectorItemIx + b) ? c.success : g ? (a.init(b), c.success) : c[0 < b ? "next" : "prev"] }; c.prototype.onButtonNavKbdClick = function (a) { a = a.response; var b = this.chart; 3 !== b.oldRangeSelectorItemState && this.fakeClickEvent(b.rangeSelector.buttons[b.highlightedRangeSelectorItemIx].element); return a.success }; c.prototype.onAfterBtnClick =
                        function () { var a = this.chart, b = g(a.xAxis[0]); (a = a.langFormat("accessibility.rangeSelector.clickButtonAnnouncement", { chart: a, axisRangeDescription: b })) && this.announcer.announce(a) }; c.prototype.onInputKbdMove = function (a) {
                            var b = this.chart, c = b.rangeSelector, d = b.highlightedInputRangeIx = (b.highlightedInputRangeIx || 0) + a; 1 < d || 0 > d ? b.accessibility && (b.accessibility.keyboardNavigation.tabindexContainer.focus(), b.accessibility.keyboardNavigation.move(a)) : c && (a = c[d ? "maxDateBox" : "minDateBox"], c = c[d ? "maxInput" : "minInput"],
                                a && c && b.setFocusToElement(a, c))
                        }; c.prototype.onInputNavInit = function (a) {
                            var b = this, c = this, d = this.chart, e = 0 < a ? 0 : 1, g = d.rangeSelector, h = g && g[e ? "maxDateBox" : "minDateBox"]; a = g && g.minInput; g = g && g.maxInput; d.highlightedInputRangeIx = e; if (h && a && g) {
                                d.setFocusToElement(h, e ? g : a); this.removeInputKeydownHandler && this.removeInputKeydownHandler(); d = function (a) { (a.which || a.keyCode) === b.keyCodes.tab && (a.preventDefault(), a.stopPropagation(), c.onInputKbdMove(a.shiftKey ? -1 : 1)) }; var k = n(a, "keydown", d), m = n(g, "keydown",
                                    d); this.removeInputKeydownHandler = function () { k(); m() }
                            }
                        }; c.prototype.onInputNavTerminate = function () { var a = this.chart.rangeSelector || {}; a.maxInput && a.hideInput("max"); a.minInput && a.hideInput("min"); this.removeInputKeydownHandler && (this.removeInputKeydownHandler(), delete this.removeInputKeydownHandler) }; c.prototype.initDropdownNav = function () {
                            var a = this, b = this.chart, c = b.rangeSelector, e = c && c.dropdown; c && e && (b.setFocusToElement(c.buttonGroup, e), this.removeDropdownKeydownHandler && this.removeDropdownKeydownHandler(),
                                this.removeDropdownKeydownHandler = n(e, "keydown", function (c) { var d = b.accessibility; (c.which || c.keyCode) === a.keyCodes.tab && (c.preventDefault(), c.stopPropagation(), d && (d.keyboardNavigation.tabindexContainer.focus(), d.keyboardNavigation.move(c.shiftKey ? -1 : 1))) }))
                        }; c.prototype.getRangeSelectorButtonNavigation = function () {
                            var a = this.chart, b = this.keyCodes, c = this; return new m(a, {
                                keyCodeMap: [[[b.left, b.right, b.up, b.down], function (a) { return c.onButtonNavKbdArrowKey(this, a) }], [[b.enter, b.space], function () { return c.onButtonNavKbdClick(this) }]],
                                validate: function () { return !!(a.rangeSelector && a.rangeSelector.buttons && a.rangeSelector.buttons.length) }, init: function (b) { var d = a.rangeSelector; d && d.hasVisibleDropdown ? c.initDropdownNav() : d && (d = d.buttons.length - 1, a.highlightRangeSelectorButton(0 < b ? 0 : d)) }, terminate: function () { c.removeDropdownKeydownHandler && (c.removeDropdownKeydownHandler(), delete c.removeDropdownKeydownHandler) }
                            })
                        }; c.prototype.getRangeSelectorInputNavigation = function () {
                            var a = this.chart, b = this; return new m(a, {
                                keyCodeMap: [], validate: function () {
                                    return !!(a.rangeSelector &&
                                        a.rangeSelector.inputGroup && "hidden" !== a.rangeSelector.inputGroup.element.style.visibility && !1 !== a.options.rangeSelector.inputEnabled && a.rangeSelector.minInput && a.rangeSelector.maxInput)
                                }, init: function (a) { b.onInputNavInit(a) }, terminate: function () { b.onInputNavTerminate() }
                            })
                        }; c.prototype.getKeyboardNavigation = function () { return [this.getRangeSelectorButtonNavigation(), this.getRangeSelectorInputNavigation()] }; c.prototype.destroy = function () {
                            this.removeDropdownKeydownHandler && this.removeDropdownKeydownHandler();
                            this.removeInputKeydownHandler && this.removeInputKeydownHandler(); this.announcer && this.announcer.destroy()
                        }; return c
            }(h); (function (c) {
                function e(a) {
                    var b = this.rangeSelector && this.rangeSelector.buttons || [], c = this.highlightedRangeSelectorItemIx, d = this.rangeSelector && this.rangeSelector.selected; "undefined" !== typeof c && b[c] && c !== d && b[c].setState(this.oldRangeSelectorItemState || 0); this.highlightedRangeSelectorItemIx = a; return b[a] ? (this.setFocusToElement(b[a].box, b[a].element), a !== d && (this.oldRangeSelectorItemState =
                        b[a].state, b[a].setState(1)), !0) : !1
                } function d() { var a = this.chart.accessibility; if (a && a.components.rangeSelector) return a.components.rangeSelector.onAfterBtnClick() } var b = []; c.compose = function (c, g) { -1 === b.indexOf(c) && (b.push(c), c.prototype.highlightRangeSelectorButton = e); -1 === b.indexOf(g) && (b.push(g), n(a, "afterBtnClick", d)) }
            })(h || (h = {})); return h
        }); t(a, "Accessibility/Components/SeriesComponent/ForcedMarkers.js", [a["Core/Utilities.js"]], function (a) {
            var h = a.addEvent, k = a.merge, q; (function (a) {
                function m(a) {
                    k(!0,
                        a, { marker: { enabled: !0, states: { normal: { opacity: 0 } } } })
                } function q(a) { return a.marker.states && a.marker.states.normal && a.marker.states.normal.opacity } function r() {
                    if (this.chart.styledMode) {
                        if (this.markerGroup) this.markerGroup[this.a11yMarkersForced ? "addClass" : "removeClass"]("highcharts-a11y-markers-hidden"); this._hasPointMarkers && this.points && this.points.length && this.points.forEach(function (a) {
                            a.graphic && (a.graphic[a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-hidden"), a.graphic[!1 ===
                                a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-visible"))
                        })
                    }
                } function g(a) { this.resetA11yMarkerOptions = k(a.options.marker || {}, this.userOptions.marker || {}) } function n() {
                    var a = this.options, e = !1 !== (this.options.accessibility && this.options.accessibility.enabled); if (e = this.chart.options.accessibility.enabled && e) e = this.chart.options.accessibility, e = this.points.length < e.series.pointDescriptionEnabledThreshold || !1 === e.series.pointDescriptionEnabledThreshold; if (e) {
                        if (a.marker && !1 ===
                            a.marker.enabled && (this.a11yMarkersForced = !0, m(this.options)), this._hasPointMarkers && this.points && this.points.length) for (a = this.points.length; a--;) { e = this.points[a]; var d = e.options, b = e.hasForcedA11yMarker; delete e.hasForcedA11yMarker; d.marker && (b = b && 0 === q(d), d.marker.enabled && !b ? (k(!0, d.marker, { states: { normal: { opacity: q(d) || 1 } } }), e.hasForcedA11yMarker = !1) : !1 === d.marker.enabled && (m(d), e.hasForcedA11yMarker = !0)) }
                    } else this.a11yMarkersForced && (delete this.a11yMarkersForced, (a = this.resetA11yMarkerOptions) &&
                        this.update({ marker: { enabled: a.enabled, states: { normal: { opacity: a.states && a.states.normal && a.states.normal.opacity } } } }), delete this.resetA11yMarkerOptions)
                } var x = []; a.compose = function (a) { -1 === x.indexOf(a) && (x.push(a), h(a, "afterSetOptions", g), h(a, "render", n), h(a, "afterRender", r)) }
            })(q || (q = {})); return q
        }); t(a, "Accessibility/Components/SeriesComponent/SeriesKeyboardNavigation.js", [a["Core/Series/Point.js"], a["Core/Series/Series.js"], a["Core/Series/SeriesRegistry.js"], a["Core/Globals.js"], a["Core/Utilities.js"],
        a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/ChartUtilities.js"]], function (a, h, r, q, m, w, t, v) {
            function g(a) { var b = a.index, c = a.series.points, d = c.length; if (c[b] !== a) for (; d--;) { if (c[d] === a) return d } else return b } function k(a) {
                var b = a.chart.options.accessibility.keyboardNavigation.seriesNavigation, c = a.options.accessibility || {}, d = c.keyboardNavigation; return d && !1 === d.enabled || !1 === c.enabled || !1 === a.options.enableMouseTracking || !a.visible ||
                    b.pointNavigationEnabledThreshold && b.pointNavigationEnabledThreshold <= a.points.length
            } function x(a) { var b = a.series.chart.options.accessibility, c = a.options.accessibility && !1 === a.options.accessibility.enabled; return a.isNull && b.keyboardNavigation.seriesNavigation.skipNullPoints || !1 === a.visible || !1 === a.isInside || c || k(a.series) } function c(a) { a = a.series || []; for (var b = a.length, c = 0; c < b; ++c)if (!k(a[c])) { a: { var d = a[c].points || []; for (var e = d.length, f = 0; f < e; ++f)if (!x(d[f])) { d = d[f]; break a } d = null } if (d) return d } return null }
            function e(a) { for (var b = a.series.length, c = !1; b-- && !(a.highlightedPoint = a.series[b].points[a.series[b].points.length - 1], c = a.series[b].highlightNextValidPoint());); return c } function d(a) { delete a.highlightedPoint; return (a = c(a)) ? a.highlight() : !1 } var b = r.seriesTypes, f = q.doc, u = m.defined, y = m.fireEvent, C = v.getPointFromXY, G = v.getSeriesFromName, H = v.scrollToPoint; r = function () {
                function b(a, b) { this.keyCodes = b; this.chart = a } b.prototype.init = function () {
                    var b = this, d = this.chart, e = this.eventProvider = new t; e.addEvent(h,
                        "destroy", function () { return b.onSeriesDestroy(this) }); e.addEvent(d, "afterApplyDrilldown", function () { var a = c(this); a && a.highlight(!1) }); e.addEvent(d, "drilldown", function (a) { a = a.point; var c = a.series; b.lastDrilledDownPoint = { x: a.x, y: a.y, seriesName: c ? c.name : "" } }); e.addEvent(d, "drillupall", function () { setTimeout(function () { b.onDrillupAll() }, 10) }); e.addEvent(a, "afterSetState", function () {
                            var a = this.graphic && this.graphic.element, b = f.activeElement, c = b && b.getAttribute("class"); c = c && -1 < c.indexOf("highcharts-a11y-proxy-button");
                            d.highlightedPoint === this && b !== a && !c && a && a.focus && a.focus()
                        })
                }; b.prototype.onDrillupAll = function () { var a = this.lastDrilledDownPoint, b = this.chart, d = a && G(b, a.seriesName), e; a && d && u(a.x) && u(a.y) && (e = C(d, a.x, a.y)); e = e || c(b); b.container && b.container.focus(); e && e.highlight && e.highlight(!1) }; b.prototype.getKeyboardNavigationHandler = function () {
                    var a = this, b = this.keyCodes, f = this.chart, g = f.inverted; return new w(f, {
                        keyCodeMap: [[g ? [b.up, b.down] : [b.left, b.right], function (b) { return a.onKbdSideways(this, b) }], [g ? [b.left,
                        b.right] : [b.up, b.down], function (b) { return a.onKbdVertical(this, b) }], [[b.enter, b.space], function (a, b) { if (a = f.highlightedPoint) b.point = a, y(a.series, "click", b), a.firePointEvent("click"); return this.response.success }], [[b.home], function () { d(f); return this.response.success }], [[b.end], function () { e(f); return this.response.success }], [[b.pageDown, b.pageUp], function (a) { f.highlightAdjacentSeries(a === b.pageDown); return this.response.success }]], init: function () { d(f); return this.response.success }, validate: function () { return !!c(f) },
                        terminate: function () { return a.onHandlerTerminate() }
                    })
                }; b.prototype.onKbdSideways = function (a, b) { var c = this.keyCodes; return this.attemptHighlightAdjacentPoint(a, b === c.right || b === c.down) }; b.prototype.onKbdVertical = function (a, b) {
                    var c = this.chart, d = this.keyCodes; b = b === d.down || b === d.right; d = c.options.accessibility.keyboardNavigation.seriesNavigation; if (d.mode && "serialize" === d.mode) return this.attemptHighlightAdjacentPoint(a, b); c[c.highlightedPoint && c.highlightedPoint.series.keyboardMoveVertical ? "highlightAdjacentPointVertical" :
                        "highlightAdjacentSeries"](b); return a.response.success
                }; b.prototype.onHandlerTerminate = function () { var a = this.chart; a.tooltip && a.tooltip.hide(0); var b = a.highlightedPoint && a.highlightedPoint.series; if (b && b.onMouseOut) b.onMouseOut(); if (a.highlightedPoint && a.highlightedPoint.onMouseOut) a.highlightedPoint.onMouseOut(); delete a.highlightedPoint }; b.prototype.attemptHighlightAdjacentPoint = function (a, b) {
                    var c = this.chart, f = c.options.accessibility.keyboardNavigation.wrapAround; return c.highlightAdjacentPoint(b) ?
                        a.response.success : f && (b ? d(c) : e(c)) ? a.response.success : a.response[b ? "next" : "prev"]
                }; b.prototype.onSeriesDestroy = function (a) { var b = this.chart; b.highlightedPoint && b.highlightedPoint.series === a && (delete b.highlightedPoint, b.focusElement && b.focusElement.removeFocusBorder()) }; b.prototype.destroy = function () { this.eventProvider.removeAddedEvents() }; return b
            }(); (function (a) {
                function c(a) {
                    var b = this.series, c = this.highlightedPoint, d = c && g(c) || 0, e = c && c.series.points || [], f = this.series && this.series[this.series.length -
                        1]; f = f && f.points && f.points[f.points.length - 1]; if (!b[0] || !b[0].points) return !1; if (c) { if (b = b[c.series.index + (a ? 1 : -1)], d = e[d + (a ? 1 : -1)], !d && b && (d = b.points[a ? 0 : b.points.length - 1]), !d) return !1 } else d = a ? b[0].points[0] : f; return x(d) ? (b = d.series, k(b) ? this.highlightedPoint = a ? b.points[b.points.length - 1] : b.points[0] : this.highlightedPoint = d, this.highlightAdjacentPoint(a)) : d.highlight()
                } function d(a) {
                    var b = this.highlightedPoint, c = Infinity, d; if (!u(b.plotX) || !u(b.plotY)) return !1; this.series.forEach(function (e) {
                        k(e) ||
                        e.points.forEach(function (f) { if (u(f.plotY) && u(f.plotX) && f !== b) { var g = f.plotY - b.plotY, h = Math.abs(f.plotX - b.plotX); h = Math.abs(g) * Math.abs(g) + h * h * 4; e.yAxis && e.yAxis.reversed && (g *= -1); !(0 >= g && a || 0 <= g && !a || 5 > h || x(f)) && h < c && (c = h, d = f) } })
                    }); return d ? d.highlight() : !1
                } function e(a) {
                    var b = this.highlightedPoint, c = this.series && this.series[this.series.length - 1], d = c && c.points && c.points[c.points.length - 1]; if (!this.highlightedPoint) return c = a ? this.series && this.series[0] : c, (d = a ? c && c.points && c.points[0] : d) ? d.highlight() :
                        !1; c = this.series[b.series.index + (a ? -1 : 1)]; if (!c) return !1; d = f(b, c, 4); if (!d) return !1; if (k(c)) return d.highlight(), a = this.highlightAdjacentSeries(a), a ? a : (b.highlight(), !1); d.highlight(); return d.series.highlightNextValidPoint()
                } function f(a, b, c, d) {
                    var e = Infinity, f = b.points.length, g = function (a) { return !(u(a.plotX) && u(a.plotY)) }; if (!g(a)) {
                        for (; f--;) { var h = b.points[f]; if (!g(h) && (h = (a.plotX - h.plotX) * (a.plotX - h.plotX) * (c || 1) + (a.plotY - h.plotY) * (a.plotY - h.plotY) * (d || 1), h < e)) { e = h; var k = f } } return u(k) ? b.points[k] :
                            void 0
                    }
                } function h(a) { void 0 === a && (a = !0); var b = this.series.chart; if (!this.isNull && a) this.onMouseOver(); else b.tooltip && b.tooltip.hide(0); H(this); this.graphic && (b.setFocusToElement(this.graphic), !a && b.focusElement && b.focusElement.removeFocusBorder()); b.highlightedPoint = this; return this } function n() { var a = this.chart.highlightedPoint, b = (a && a.series) === this ? g(a) : 0; a = this.points; var c = a.length; if (a && c) { for (var d = b; d < c; ++d)if (!x(a[d])) return a[d].highlight(); for (; 0 <= b; --b)if (!x(a[b])) return a[b].highlight() } return !1 }
                var m = []; a.compose = function (a, f, g) { -1 === m.indexOf(a) && (m.push(a), a = a.prototype, a.highlightAdjacentPoint = c, a.highlightAdjacentPointVertical = d, a.highlightAdjacentSeries = e); -1 === m.indexOf(f) && (m.push(f), f.prototype.highlight = h); -1 === m.indexOf(g) && (m.push(g), f = g.prototype, f.keyboardMoveVertical = !0, ["column", "gantt", "pie"].forEach(function (a) { b[a] && (b[a].prototype.keyboardMoveVertical = !1) }), f.highlightNextValidPoint = n) }
            })(r || (r = {})); return r
        }); t(a, "Accessibility/Components/SeriesComponent/SeriesComponent.js",
            [a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Components/SeriesComponent/ForcedMarkers.js"], a["Accessibility/Components/SeriesComponent/NewDataAnnouncer.js"], a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"], a["Accessibility/Components/SeriesComponent/SeriesKeyboardNavigation.js"], a["Core/Tooltip.js"]], function (a, h, r, q, m, w, t) {
                var k = this && this.__extends || function () {
                    var a = function (c, e) {
                        a = Object.setPrototypeOf || { __proto__: [] } instanceof
                        Array && function (a, b) { a.__proto__ = b } || function (a, b) { for (var c in b) b.hasOwnProperty(c) && (a[c] = b[c]) }; return a(c, e)
                    }; return function (c, e) { function d() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (d.prototype = e.prototype, new d) }
                }(), g = h.hideSeriesFromAT, n = m.describeSeries; return function (a) {
                    function c() { return null !== a && a.apply(this, arguments) || this } k(c, a); c.compose = function (a, c, b) { q.compose(b); r.compose(b); w.compose(a, c, b) }; c.prototype.init = function () {
                        this.newDataAnnouncer = new q(this.chart);
                        this.newDataAnnouncer.init(); this.keyboardNavigation = new w(this.chart, this.keyCodes); this.keyboardNavigation.init(); this.hideTooltipFromATWhenShown(); this.hideSeriesLabelsFromATWhenShown()
                    }; c.prototype.hideTooltipFromATWhenShown = function () { var a = this; this.addEvent(t, "refresh", function () { this.chart === a.chart && this.label && this.label.element && this.label.element.setAttribute("aria-hidden", !0) }) }; c.prototype.hideSeriesLabelsFromATWhenShown = function () {
                        this.addEvent(this.chart, "afterDrawSeriesLabels", function () {
                            this.series.forEach(function (a) {
                                a.labelBySeries &&
                                a.labelBySeries.attr("aria-hidden", !0)
                            })
                        })
                    }; c.prototype.onChartRender = function () { this.chart.series.forEach(function (a) { !1 !== (a.options.accessibility && a.options.accessibility.enabled) && a.visible ? n(a) : g(a) }) }; c.prototype.getKeyboardNavigation = function () { return this.keyboardNavigation.getKeyboardNavigationHandler() }; c.prototype.destroy = function () { this.newDataAnnouncer.destroy(); this.keyboardNavigation.destroy() }; return c
                }(a)
            }); t(a, "Accessibility/Components/ZoomComponent.js", [a["Accessibility/AccessibilityComponent.js"],
            a["Accessibility/Utils/ChartUtilities.js"], a["Core/Globals.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Core/Utilities.js"]], function (a, h, r, q, m) {
                var k = this && this.__extends || function () {
                    var a = function (g, c) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var b in c) c.hasOwnProperty(b) && (a[b] = c[b]) }; return a(g, c) }; return function (g, c) {
                        function e() { this.constructor = g } a(g, c); g.prototype = null === c ? Object.create(c) : (e.prototype = c.prototype,
                            new e)
                    }
                }(), t = h.unhideChartElementFromAT, v = m.attr, g = m.pick; a = function (a) {
                    function h() { var c = null !== a && a.apply(this, arguments) || this; c.focusedMapNavButtonIx = -1; return c } k(h, a); h.prototype.init = function () { var a = this, e = this.chart; this.proxyProvider.addGroup("zoom", "div");["afterShowResetZoom", "afterApplyDrilldown", "drillupall"].forEach(function (c) { a.addEvent(e, c, function () { a.updateProxyOverlays() }) }) }; h.prototype.onChartUpdate = function () {
                        var a = this.chart, e = this; a.mapNavigation && a.mapNavigation.navButtons.forEach(function (c,
                            b) { t(a, c.element); e.setMapNavButtonAttrs(c.element, "accessibility.zoom.mapZoom" + (b ? "Out" : "In")) })
                    }; h.prototype.setMapNavButtonAttrs = function (a, e) { var c = this.chart; e = c.langFormat(e, { chart: c }); v(a, { tabindex: -1, role: "button", "aria-label": e }) }; h.prototype.onChartRender = function () { this.updateProxyOverlays() }; h.prototype.updateProxyOverlays = function () {
                        var a = this.chart; this.proxyProvider.clearGroup("zoom"); a.resetZoomButton && this.createZoomProxyButton(a.resetZoomButton, "resetZoomProxyButton", a.langFormat("accessibility.zoom.resetZoomButton",
                            { chart: a })); a.drillUpButton && a.breadcrumbs && a.breadcrumbs.list && this.createZoomProxyButton(a.drillUpButton, "drillUpProxyButton", a.langFormat("accessibility.drillUpButton", { chart: a, buttonText: a.breadcrumbs.getButtonText(a.breadcrumbs.list[a.breadcrumbs.list.length - 1]) }))
                    }; h.prototype.createZoomProxyButton = function (a, e, d) { this[e] = this.proxyProvider.addProxyElement("zoom", { click: a }, { "aria-label": d, tabindex: -1 }) }; h.prototype.getMapZoomNavigation = function () {
                        var a = this.keyCodes, e = this.chart, d = this; return new q(e,
                            { keyCodeMap: [[[a.up, a.down, a.left, a.right], function (a) { return d.onMapKbdArrow(this, a) }], [[a.tab], function (a, c) { return d.onMapKbdTab(this, c) }], [[a.space, a.enter], function () { return d.onMapKbdClick(this) }]], validate: function () { return !!(e.mapZoom && e.mapNavigation && e.mapNavigation.navButtons.length) }, init: function (a) { return d.onMapNavInit(a) } })
                    }; h.prototype.onMapKbdArrow = function (a, e) { var c = this.keyCodes; this.chart[e === c.up || e === c.down ? "yAxis" : "xAxis"][0].panStep(e === c.left || e === c.up ? -1 : 1); return a.response.success };
                    h.prototype.onMapKbdTab = function (a, e) { var c = this.chart; a = a.response; var b = (e = e.shiftKey) && !this.focusedMapNavButtonIx || !e && this.focusedMapNavButtonIx; c.mapNavigation.navButtons[this.focusedMapNavButtonIx].setState(0); if (b) return c.mapZoom(), a[e ? "prev" : "next"]; this.focusedMapNavButtonIx += e ? -1 : 1; e = c.mapNavigation.navButtons[this.focusedMapNavButtonIx]; c.setFocusToElement(e.box, e.element); e.setState(2); return a.success }; h.prototype.onMapKbdClick = function (a) {
                        this.fakeClickEvent(this.chart.mapNavButtons[this.focusedMapNavButtonIx].element);
                        return a.response.success
                    }; h.prototype.onMapNavInit = function (a) { var c = this.chart, d = c.mapNavigation.navButtons[0], b = c.mapNavigation.navButtons[1]; d = 0 < a ? d : b; c.setFocusToElement(d.box, d.element); d.setState(2); this.focusedMapNavButtonIx = 0 < a ? 0 : 1 }; h.prototype.simpleButtonNavigation = function (a, e, d) {
                        var b = this.keyCodes, c = this, h = this.chart; return new q(h, {
                            keyCodeMap: [[[b.tab, b.up, b.down, b.left, b.right], function (a, c) { return this.response[a === b.tab && c.shiftKey || a === b.left || a === b.up ? "prev" : "next"] }], [[b.space,
                            b.enter], function () { var a = d(this, h); return g(a, this.response.success) }]], validate: function () { return h[a] && h[a].box && c[e].buttonElement }, init: function () { h.setFocusToElement(h[a].box, c[e].buttonElement) }
                        })
                    }; h.prototype.getKeyboardNavigation = function () { return [this.simpleButtonNavigation("resetZoomButton", "resetZoomProxyButton", function (a, e) { e.zoomOut() }), this.simpleButtonNavigation("drillUpButton", "drillUpProxyButton", function (a, e) { e.drillUp(); return a.response.prev }), this.getMapZoomNavigation()] }; return h
                }(a);
                (function (a) { function g(a, e) { var c = e || 3; e = this.getExtremes(); var b = (e.max - e.min) / c * a; c = e.max + b; b = e.min + b; var f = c - b; 0 > a && b < e.dataMin ? (b = e.dataMin, c = b + f) : 0 < a && c > e.dataMax && (c = e.dataMax, b = c - f); this.setExtremes(b, c) } a.composedClasses = []; a.compose = function (c) { -1 === a.composedClasses.indexOf(c) && (a.composedClasses.push(c), c.prototype.panStep = g) } })(a || (a = {})); return a
            }); t(a, "Accessibility/HighContrastMode.js", [a["Core/Globals.js"]], function (a) {
                var h = a.doc, k = a.isMS, q = a.win; return {
                    isHighContrastModeActive: function () {
                        var a =
                            /(Edg)/.test(q.navigator.userAgent); if (q.matchMedia && a) return q.matchMedia("(-ms-high-contrast: active)").matches; if (k && q.getComputedStyle) { a = h.createElement("div"); a.style.backgroundImage = "url(data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==)"; h.body.appendChild(a); var r = (a.currentStyle || q.getComputedStyle(a)).backgroundImage; h.body.removeChild(a); return "none" === r } return q.matchMedia && q.matchMedia("(forced-colors: active)").matches
                    }, setHighContrastTheme: function (a) {
                        a.highContrastModeActive =
                        !0; var h = a.options.accessibility.highContrastTheme; a.update(h, !1); a.series.forEach(function (a) { var k = h.plotOptions[a.type] || {}; a.update({ color: k.color || "windowText", colors: [k.color || "windowText"], borderColor: k.borderColor || "window" }); a.points.forEach(function (a) { a.options && a.options.color && a.update({ color: k.color || "windowText", borderColor: k.borderColor || "window" }, !1) }) }); a.redraw()
                    }
                }
            }); t(a, "Accessibility/HighContrastTheme.js", [], function () {
                return {
                    chart: { backgroundColor: "window" }, title: { style: { color: "windowText" } },
                    subtitle: { style: { color: "windowText" } }, colorAxis: { minColor: "windowText", maxColor: "windowText", stops: [] }, colors: ["windowText"], xAxis: { gridLineColor: "windowText", labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } } }, yAxis: { gridLineColor: "windowText", labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } } }, tooltip: {
                        backgroundColor: "window",
                        borderColor: "windowText", style: { color: "windowText" }
                    }, plotOptions: {
                        series: { lineColor: "windowText", fillColor: "window", borderColor: "windowText", edgeColor: "windowText", borderWidth: 1, dataLabels: { connectorColor: "windowText", color: "windowText", style: { color: "windowText", textOutline: "none" } }, marker: { lineColor: "windowText", fillColor: "windowText" } }, pie: { color: "window", colors: ["window"], borderColor: "windowText", borderWidth: 1 }, boxplot: { fillColor: "window" }, candlestick: { lineColor: "windowText", fillColor: "window" },
                        errorbar: { fillColor: "window" }
                    }, legend: { backgroundColor: "window", itemStyle: { color: "windowText" }, itemHoverStyle: { color: "windowText" }, itemHiddenStyle: { color: "#555" }, title: { style: { color: "windowText" } } }, credits: { style: { color: "windowText" } }, labels: { style: { color: "windowText" } }, drilldown: { activeAxisLabelStyle: { color: "windowText" }, activeDataLabelStyle: { color: "windowText" } }, navigation: { buttonOptions: { symbolStroke: "windowText", theme: { fill: "window" } } }, rangeSelector: {
                        buttonTheme: {
                            fill: "window", stroke: "windowText",
                            style: { color: "windowText" }, states: { hover: { fill: "window", stroke: "windowText", style: { color: "windowText" } }, select: { fill: "#444", stroke: "windowText", style: { color: "windowText" } } }
                        }, inputBoxBorderColor: "windowText", inputStyle: { backgroundColor: "window", color: "windowText" }, labelStyle: { color: "windowText" }
                    }, navigator: { handles: { backgroundColor: "window", borderColor: "windowText" }, outlineColor: "windowText", maskFill: "transparent", series: { color: "windowText", lineColor: "windowText" }, xAxis: { gridLineColor: "windowText" } },
                    scrollbar: { barBackgroundColor: "#444", barBorderColor: "windowText", buttonArrowColor: "windowText", buttonBackgroundColor: "window", buttonBorderColor: "windowText", rifleColor: "windowText", trackBackgroundColor: "window", trackBorderColor: "windowText" }
                }
            }); t(a, "Accessibility/Options/Options.js", [], function () {
                return {
                    accessibility: {
                        enabled: !0, screenReaderSection: {
                            beforeChartFormat: "<{headingTagName}>{chartTitle}</{headingTagName}><div>{typeDescription}</div><div>{chartSubtitle}</div><div>{chartLongdesc}</div><div>{playAsSoundButton}</div><div>{viewTableButton}</div><div>{xAxisDescription}</div><div>{yAxisDescription}</div><div>{annotationsTitle}{annotationsList}</div>",
                            afterChartFormat: "{endOfChartMarker}", axisRangeDateFormat: "%Y-%m-%d %H:%M:%S"
                        }, series: { describeSingleSeries: !1, pointDescriptionEnabledThreshold: 200 }, point: { valueDescriptionFormat: "{index}. {xDescription}{separator}{value}." }, landmarkVerbosity: "all", linkedDescription: '*[data-highcharts-chart="{index}"] + .highcharts-description', keyboardNavigation: {
                            enabled: !0, focusBorder: { enabled: !0, hideBrowserFocusOutline: !0, style: { color: "#335cad", lineWidth: 2, borderRadius: 3 }, margin: 2 }, order: ["series", "zoom", "rangeSelector",
                                "legend", "chartMenu"], wrapAround: !0, seriesNavigation: { skipNullPoints: !0, pointNavigationEnabledThreshold: !1 }
                        }, announceNewData: { enabled: !1, minAnnounceInterval: 5E3, interruptUser: !1 }
                    }, legend: { accessibility: { enabled: !0, keyboardNavigation: { enabled: !0 } } }, exporting: { accessibility: { enabled: !0 } }
                }
            }); t(a, "Accessibility/Options/LangOptions.js", [], function () {
                return {
                    accessibility: {
                        defaultChartTitle: "Chart", chartContainerLabel: "{title}. Highcharts interactive chart.", svgContainerLabel: "Interactive chart", drillUpButton: "{buttonText}",
                        credits: "Chart credits: {creditsStr}", thousandsSep: ",", svgContainerTitle: "", graphicContainerLabel: "", screenReaderSection: { beforeRegionLabel: "Chart screen reader information, {chartTitle}.", afterRegionLabel: "", annotations: { heading: "Chart annotations summary", descriptionSinglePoint: "{annotationText}. Related to {annotationPoint}", descriptionMultiplePoints: "{annotationText}. Related to {annotationPoint}{ Also related to, #each(additionalAnnotationPoints)}", descriptionNoPoints: "{annotationText}" }, endOfChartMarker: "End of interactive chart." },
                        sonification: { playAsSoundButtonText: "Play as sound, {chartTitle}", playAsSoundClickAnnouncement: "Play" }, legend: { legendLabelNoTitle: "Toggle series visibility, {chartTitle}", legendLabel: "Chart legend: {legendTitle}", legendItem: "Show {itemName}" }, zoom: { mapZoomIn: "Zoom chart", mapZoomOut: "Zoom out chart", resetZoomButton: "Reset zoom" }, rangeSelector: { dropdownLabel: "{rangeTitle}", minInputLabel: "Select start date.", maxInputLabel: "Select end date.", clickButtonAnnouncement: "Viewing {axisRangeDescription}" }, table: {
                            viewAsDataTableButtonText: "View as data table, {chartTitle}",
                            tableSummary: "Table representation of chart."
                        }, announceNewData: { newDataAnnounce: "Updated data for chart {chartTitle}", newSeriesAnnounceSingle: "New data series: {seriesDesc}", newPointAnnounceSingle: "New data point: {pointDesc}", newSeriesAnnounceMultiple: "New data series in chart {chartTitle}: {seriesDesc}", newPointAnnounceMultiple: "New data point in chart {chartTitle}: {pointDesc}" }, seriesTypeDescriptions: {
                            boxplot: "Box plot charts are typically used to display groups of statistical data. Each data point in the chart can have up to 5 values: minimum, lower quartile, median, upper quartile, and maximum.",
                            arearange: "Arearange charts are line charts displaying a range between a lower and higher value for each point.", areasplinerange: "These charts are line charts displaying a range between a lower and higher value for each point.", bubble: "Bubble charts are scatter charts where each data point also has a size value.", columnrange: "Columnrange charts are column charts displaying a range between a lower and higher value for each point.", errorbar: "Errorbar series are used to display the variability of the data.",
                            funnel: "Funnel charts are used to display reduction of data in stages.", pyramid: "Pyramid charts consist of a single pyramid with item heights corresponding to each point value.", waterfall: "A waterfall chart is a column chart where each column contributes towards a total end value."
                        }, chartTypes: {
                            emptyChart: "Empty chart", mapTypeDescription: "Map of {mapTitle} with {numSeries} data series.", unknownMap: "Map of unspecified region with {numSeries} data series.", combinationChart: "Combination chart with {numSeries} data series.",
                            defaultSingle: "Chart with {numPoints} data {#plural(numPoints, points, point)}.", defaultMultiple: "Chart with {numSeries} data series.", splineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.", splineMultiple: "Line chart with {numSeries} lines.", lineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.", lineMultiple: "Line chart with {numSeries} lines.", columnSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", columnMultiple: "Bar chart with {numSeries} data series.",
                            barSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", barMultiple: "Bar chart with {numSeries} data series.", pieSingle: "Pie chart with {numPoints} {#plural(numPoints, slices, slice)}.", pieMultiple: "Pie chart with {numSeries} pies.", scatterSingle: "Scatter chart with {numPoints} {#plural(numPoints, points, point)}.", scatterMultiple: "Scatter chart with {numSeries} data series.", boxplotSingle: "Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotMultiple: "Boxplot with {numSeries} data series.",
                            bubbleSingle: "Bubble chart with {numPoints} {#plural(numPoints, bubbles, bubble)}.", bubbleMultiple: "Bubble chart with {numSeries} data series."
                        }, axis: {
                            xAxisDescriptionSingular: "The chart has 1 X axis displaying {names[0]}. {ranges[0]}", xAxisDescriptionPlural: "The chart has {numAxes} X axes displaying {#each(names, -1) }and {names[-1]}.", yAxisDescriptionSingular: "The chart has 1 Y axis displaying {names[0]}. {ranges[0]}", yAxisDescriptionPlural: "The chart has {numAxes} Y axes displaying {#each(names, -1) }and {names[-1]}.",
                            timeRangeDays: "Range: {range} days.", timeRangeHours: "Range: {range} hours.", timeRangeMinutes: "Range: {range} minutes.", timeRangeSeconds: "Range: {range} seconds.", rangeFromTo: "Range: {rangeFrom} to {rangeTo}.", rangeCategories: "Range: {numCategories} categories."
                        }, exporting: { chartMenuLabel: "Chart menu", menuButtonLabel: "View chart menu, {chartTitle}" }, series: {
                            summary: {
                                "default": "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", defaultCombination: "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.",
                                line: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", lineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", spline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", splineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", column: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.",
                                columnCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", bar: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.", barCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", pie: "{name}, pie {ix} of {numSeries} with {numPoints} {#plural(numPoints, slices, slice)}.", pieCombination: "{name}, series {ix} of {numSeries}. Pie with {numPoints} {#plural(numPoints, slices, slice)}.",
                                scatter: "{name}, scatter plot {ix} of {numSeries} with {numPoints} {#plural(numPoints, points, point)}.", scatterCombination: "{name}, series {ix} of {numSeries}, scatter plot with {numPoints} {#plural(numPoints, points, point)}.", boxplot: "{name}, boxplot {ix} of {numSeries} with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotCombination: "{name}, series {ix} of {numSeries}. Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", bubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.",
                                bubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}.", map: "{name}, map {ix} of {numSeries} with {numPoints} {#plural(numPoints, areas, area)}.", mapCombination: "{name}, series {ix} of {numSeries}. Map with {numPoints} {#plural(numPoints, areas, area)}.", mapline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", maplineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.",
                                mapbubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.", mapbubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}."
                            }, description: "{description}", xAxisDescription: "X axis, {name}", yAxisDescription: "Y axis, {name}", nullPointValue: "No value", pointAnnotationsDescription: "{Annotation: #each(annotations). }"
                        }
                    }
                }
            }); t(a, "Accessibility/Options/DeprecatedOptions.js", [a["Core/Utilities.js"]],
                function (a) {
                    function h(a, h, k) { for (var c, e = 0; e < h.length - 1; ++e)c = h[e], a = a[c] = v(a[c], {}); a[h[h.length - 1]] = k } function k(a, k, m, c) { function e(a, b) { return b.reduce(function (a, b) { return a[b] }, a) } var d = e(a.options, k), b = e(a.options, m); Object.keys(c).forEach(function (e) { var f, g = d[e]; "undefined" !== typeof g && (h(b, c[e], g), t(32, !1, a, (f = {}, f[k.join(".") + "." + e] = m.join(".") + "." + c[e].join("."), f))) }) } function q(a) {
                        var g = a.options.chart, h = a.options.accessibility || {};["description", "typeDescription"].forEach(function (c) {
                            var e;
                            g[c] && (h[c] = g[c], t(32, !1, a, (e = {}, e["chart." + c] = "use accessibility." + c, e)))
                        })
                    } function m(a) { a.axes.forEach(function (g) { (g = g.options) && g.description && (g.accessibility = g.accessibility || {}, g.accessibility.description = g.description, t(32, !1, a, { "axis.description": "use axis.accessibility.description" })) }) } function w(a) {
                        var g = {
                            description: ["accessibility", "description"], exposeElementToA11y: ["accessibility", "exposeAsGroupOnly"], pointDescriptionFormatter: ["accessibility", "point", "descriptionFormatter"], skipKeyboardNavigation: ["accessibility",
                                "keyboardNavigation", "enabled"], "accessibility.pointDescriptionFormatter": ["accessibility", "point", "descriptionFormatter"]
                        }; a.series.forEach(function (k) { Object.keys(g).forEach(function (c) { var e, d = k.options[c]; "accessibility.pointDescriptionFormatter" === c && (d = k.options.accessibility && k.options.accessibility.pointDescriptionFormatter); "undefined" !== typeof d && (h(k.options, g[c], "skipKeyboardNavigation" === c ? !d : d), t(32, !1, a, (e = {}, e["series." + c] = "series." + g[c].join("."), e))) }) })
                    } var t = a.error, v = a.pick; return function (a) {
                        q(a);
                        m(a); a.series && w(a); k(a, ["accessibility"], ["accessibility"], {
                            pointDateFormat: ["point", "dateFormat"], pointDateFormatter: ["point", "dateFormatter"], pointDescriptionFormatter: ["point", "descriptionFormatter"], pointDescriptionThreshold: ["series", "pointDescriptionEnabledThreshold"], pointNavigationThreshold: ["keyboardNavigation", "seriesNavigation", "pointNavigationEnabledThreshold"], pointValueDecimals: ["point", "valueDecimals"], pointValuePrefix: ["point", "valuePrefix"], pointValueSuffix: ["point", "valueSuffix"],
                            screenReaderSectionFormatter: ["screenReaderSection", "beforeChartFormatter"], describeSingleSeries: ["series", "describeSingleSeries"], seriesDescriptionFormatter: ["series", "descriptionFormatter"], onTableAnchorClick: ["screenReaderSection", "onViewDataTableClick"], axisRangeDateFormat: ["screenReaderSection", "axisRangeDateFormat"]
                        }); k(a, ["accessibility", "keyboardNavigation"], ["accessibility", "keyboardNavigation", "seriesNavigation"], { skipNullPoints: ["skipNullPoints"], mode: ["mode"] }); k(a, ["lang", "accessibility"],
                            ["lang", "accessibility"], {
                                legendItem: ["legend", "legendItem"], legendLabel: ["legend", "legendLabel"], mapZoomIn: ["zoom", "mapZoomIn"], mapZoomOut: ["zoom", "mapZoomOut"], resetZoomButton: ["zoom", "resetZoomButton"], screenReaderRegionLabel: ["screenReaderSection", "beforeRegionLabel"], rangeSelectorButton: ["rangeSelector", "buttonText"], rangeSelectorMaxInput: ["rangeSelector", "maxInputLabel"], rangeSelectorMinInput: ["rangeSelector", "minInputLabel"], svgContainerEnd: ["screenReaderSection", "endOfChartMarker"], viewAsDataTable: ["table",
                                    "viewAsDataTableButtonText"], tableSummary: ["table", "tableSummary"]
                        })
                    }
                }); t(a, "Accessibility/Accessibility.js", [a["Core/DefaultOptions.js"], a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/A11yI18n.js"], a["Accessibility/Components/ContainerComponent.js"], a["Accessibility/FocusBorder.js"], a["Accessibility/Components/InfoRegionsComponent.js"], a["Accessibility/KeyboardNavigation.js"], a["Accessibility/Components/LegendComponent.js"], a["Accessibility/Components/MenuComponent.js"], a["Accessibility/Components/SeriesComponent/NewDataAnnouncer.js"],
                a["Accessibility/ProxyProvider.js"], a["Accessibility/Components/RangeSelectorComponent.js"], a["Accessibility/Components/SeriesComponent/SeriesComponent.js"], a["Accessibility/Components/ZoomComponent.js"], a["Accessibility/HighContrastMode.js"], a["Accessibility/HighContrastTheme.js"], a["Accessibility/Options/Options.js"], a["Accessibility/Options/LangOptions.js"], a["Accessibility/Options/DeprecatedOptions.js"]], function (a, h, r, q, m, w, t, v, g, n, x, c, e, d, b, f, u, y, M, G) {
                    a = a.defaultOptions; var k = h.doc, B = r.addEvent,
                        C = r.extend, z = r.fireEvent, E = r.merge; h = function () {
                            function a(a) { this.proxyProvider = this.keyboardNavigation = this.components = this.chart = void 0; this.init(a) } a.prototype.init = function (a) { this.chart = a; k.addEventListener && a.renderer.isSVG ? (G(a), this.proxyProvider = new c(this.chart), this.initComponents(), this.keyboardNavigation = new v(a, this.components)) : (this.zombie = !0, this.components = {}, a.renderTo.setAttribute("aria-hidden", !0)) }; a.prototype.initComponents = function () {
                                var a = this.chart, c = this.proxyProvider,
                                f = a.options.accessibility; this.components = { container: new m, infoRegions: new t, legend: new g, chartMenu: new n, rangeSelector: new e, series: new d, zoom: new b }; f.customComponents && C(this.components, f.customComponents); var h = this.components; this.getComponentOrder().forEach(function (b) { h[b].initBase(a, c); h[b].init() })
                            }; a.prototype.getComponentOrder = function () {
                                if (!this.components) return []; if (!this.components.series) return Object.keys(this.components); var a = Object.keys(this.components).filter(function (a) {
                                    return "series" !==
                                        a
                                }); return ["series"].concat(a)
                            }; a.prototype.update = function () {
                                var a = this.components, b = this.chart, c = b.options.accessibility; z(b, "beforeA11yUpdate"); b.types = this.getChartTypes(); c = c.keyboardNavigation.order; this.proxyProvider.updateGroupOrder(c); this.getComponentOrder().forEach(function (c) { a[c].onChartUpdate(); z(b, "afterA11yComponentUpdate", { name: c, component: a[c] }) }); this.keyboardNavigation.update(c); !b.highContrastModeActive && f.isHighContrastModeActive() && f.setHighContrastTheme(b); z(b, "afterA11yUpdate",
                                    { accessibility: this })
                            }; a.prototype.destroy = function () { var a = this.chart || {}, b = this.components; Object.keys(b).forEach(function (a) { b[a].destroy(); b[a].destroyBase() }); this.proxyProvider && this.proxyProvider.destroy(); this.keyboardNavigation && this.keyboardNavigation.destroy(); a.renderTo && a.renderTo.setAttribute("aria-hidden", !0); a.focusElement && a.focusElement.removeFocusBorder() }; a.prototype.getChartTypes = function () { var a = {}; this.chart.series.forEach(function (b) { a[b.type] = 1 }); return Object.keys(a) }; return a
                        }();
                    (function (a) {
                        function c() { this.accessibility && this.accessibility.destroy() } function f() { this.a11yDirty && this.renderTo && (delete this.a11yDirty, this.updateA11yEnabled()); var a = this.accessibility; a && !a.zombie && (a.proxyProvider.updateProxyElementPositions(), a.getComponentOrder().forEach(function (b) { a.components[b].onChartRender() })) } function h(a) {
                            if (a = a.options.accessibility) a.customComponents && (this.options.accessibility.customComponents = a.customComponents, delete a.customComponents), E(!0, this.options.accessibility,
                                a), this.accessibility && this.accessibility.destroy && (this.accessibility.destroy(), delete this.accessibility); this.a11yDirty = !0
                        } function k() { var b = this.accessibility, c = this.options.accessibility; c && c.enabled ? b && !b.zombie ? b.update() : (this.accessibility = b = new a(this), !b.zombie) && b.update() : b ? (b.destroy && b.destroy(), delete this.accessibility) : this.renderTo.setAttribute("aria-hidden", !0) } function m() { this.series.chart.accessibility && (this.series.chart.a11yDirty = !0) } var r = []; a.i18nFormat = q.i18nFormat; a.compose =
                            function (a, l, p, t, u, y, z) {
                                v.compose(l); x.compose(u); g.compose(l, p); n.compose(l); d.compose(l, t, u); b.compose(a); q.compose(l); w.compose(l, y); z && e.compose(l, z); -1 === r.indexOf(l) && (r.push(l), l.prototype.updateA11yEnabled = k, B(l, "destroy", c), B(l, "render", f), B(l, "update", h), ["addSeries", "init"].forEach(function (a) { B(l, a, function () { this.a11yDirty = !0 }) }), ["afterApplyDrilldown", "drillupall"].forEach(function (a) { B(l, a, function () { var a = this.accessibility; a && !a.zombie && a.update() }) })); -1 === r.indexOf(t) && (r.push(t),
                                    B(t, "update", m)); -1 === r.indexOf(u) && (r.push(u), ["update", "updatedData", "remove"].forEach(function (a) { B(u, a, function () { this.chart.accessibility && (this.chart.a11yDirty = !0) }) }))
                            }
                    })(h || (h = {})); E(!0, a, y, { accessibility: { highContrastTheme: u }, lang: M }); return h
                }); t(a, "masters/modules/accessibility.src.js", [a["Core/Globals.js"], a["Accessibility/Accessibility.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Accessibility/KeyboardNavigationHandler.js"],
                a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"]], function (a, h, r, q, m, t, C) { a.i18nFormat = h.i18nFormat; a.A11yChartUtilities = q; a.A11yHTMLUtilities = m; a.AccessibilityComponent = r; a.KeyboardNavigationHandler = t; a.SeriesAccessibilityDescriber = C; h.compose(a.Axis, a.Chart, a.Legend, a.Point, a.Series, a.SVGElement, a.RangeSelector) })
});
//# sourceMappingURL=accessibility.js.map