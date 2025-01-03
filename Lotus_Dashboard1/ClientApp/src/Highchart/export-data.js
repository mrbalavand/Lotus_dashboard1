﻿/*
 Highcharts JS v10.0.0 (2022-03-07)

 Exporting module

 (c) 2010-2021 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/export-data", ["highcharts", "highcharts/modules/exporting"], function (g) { a(g); a.Highcharts = g; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function g(a, b, d, n) { a.hasOwnProperty(b) || (a[b] = n.apply(null, d), "function" === typeof CustomEvent && window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded", { detail: { path: b, module: a[b] } }))) }
    a = a ? a._modules : {}; g(a, "Extensions/DownloadURL.js", [a["Core/Globals.js"]], function (a) {
        var b = a.isSafari, d = a.win, n = d.document, k = d.URL || d.webkitURL || d, r = a.dataURLtoBlob = function (a) { if ((a = a.replace(/filename=.*;/, "").match(/data:([^;]*)(;base64)?,([0-9A-Za-z+/]+)/)) && 3 < a.length && d.atob && d.ArrayBuffer && d.Uint8Array && d.Blob && k.createObjectURL) { var b = d.atob(a[3]), m = new d.ArrayBuffer(b.length); m = new d.Uint8Array(m); for (var f = 0; f < m.length; ++f)m[f] = b.charCodeAt(f); a = new d.Blob([m], { type: a[1] }); return k.createObjectURL(a) } };
        a = a.downloadURL = function (a, k) {
            var m = d.navigator, f = n.createElement("a"); if ("string" === typeof a || a instanceof String || !m.msSaveOrOpenBlob) {
                a = "" + a; m = /Edge\/\d+/.test(m.userAgent); if (b && "string" === typeof a && 0 === a.indexOf("data:application/pdf") || m || 2E6 < a.length) if (a = r(a) || "", !a) throw Error("Failed to convert to blob"); if ("undefined" !== typeof f.download) f.href = a, f.download = k, n.body.appendChild(f), f.click(), n.body.removeChild(f); else try {
                    var l = d.open(a, "chart"); if ("undefined" === typeof l || null === l) throw Error("Failed to open window");
                } catch (E) { d.location.href = a }
            } else m.msSaveOrOpenBlob(a, k)
        }; return { dataURLtoBlob: r, downloadURL: a }
    }); g(a, "Extensions/ExportData.js", [a["Core/Axis/Axis.js"], a["Core/Chart/Chart.js"], a["Core/Renderer/HTML/AST.js"], a["Core/Globals.js"], a["Core/DefaultOptions.js"], a["Core/Utilities.js"], a["Extensions/DownloadURL.js"]], function (a, b, d, n, k, r, m) {
        function g(a, p) {
            var c = l.navigator, f = -1 < c.userAgent.indexOf("WebKit") && 0 > c.userAgent.indexOf("Chrome"), b = l.URL || l.webkitURL || l; try {
                if (c.msSaveOrOpenBlob && l.MSBlobBuilder) {
                    var t =
                        new l.MSBlobBuilder; t.append(a); return t.getBlob("image/svg+xml")
                } if (!f) return b.createObjectURL(new l.Blob(["\ufeff" + a], { type: p }))
            } catch (N) { }
        } var I = n.doc, f = n.seriesTypes, l = n.win; n = k.getOptions; k = k.setOptions; var E = r.addEvent, J = r.defined, F = r.extend, K = r.find, C = r.fireEvent, L = r.isNumber, v = r.pick, G = m.downloadURL; k({
            exporting: {
                csv: { annotations: { itemDelimiter: "; ", join: !1 }, columnHeaderFormatter: null, dateFormat: "%Y-%m-%d %H:%M:%S", decimalPoint: null, itemDelimiter: null, lineDelimiter: "\n" }, showTable: !1, useMultiLevelHeaders: !0,
                useRowspanHeaders: !0
            }, lang: { downloadCSV: "Download CSV", downloadXLS: "Download XLS", exportData: { annotationHeader: "Annotations", categoryHeader: "Category", categoryDatetimeHeader: "DateTime" }, viewData: "View data table", hideData: "Hide data table" }
        }); E(b, "render", function () { this.options && this.options.exporting && this.options.exporting.showTable && !this.options.chart.forExport && !this.dataTableDiv && this.viewData() }); b.prototype.setUpKeyToAxis = function () {
            f.arearange && (f.arearange.prototype.keyToAxis = {
                low: "y",
                high: "y"
            }); f.gantt && (f.gantt.prototype.keyToAxis = { start: "x", end: "x" })
        }; b.prototype.getDataRows = function (c) {
            var p = this.hasParallelCoordinates, y = this.time, f = this.options.exporting && this.options.exporting.csv || {}, b = this.xAxis, t = {}, d = [], m = [], n = [], z; var l = this.options.lang.exportData; var k = l.categoryHeader, M = l.categoryDatetimeHeader, w = function (q, e, b) {
                if (f.columnHeaderFormatter) { var d = f.columnHeaderFormatter(q, e, b); if (!1 !== d) return d } return q ? q instanceof a ? q.options.title && q.options.title.text || (q.dateTime ?
                    M : k) : c ? { columnTitle: 1 < b ? e : q.name, topLevelColumnTitle: q.name } : q.name + (1 < b ? " (" + e + ")" : "") : k
            }, H = function (a, c, e) { var q = {}, b = {}; c.forEach(function (c) { var d = (a.keyToAxis && a.keyToAxis[c] || c) + "Axis"; d = L(e) ? a.chart[d][e] : a[d]; q[c] = d && d.categories || []; b[c] = d && d.dateTime }); return { categoryMap: q, dateTimeValueAxisMap: b } }, r = function (a, c) {
                return a.data.filter(function (a) { return "undefined" !== typeof a.y && a.name }).length && c && !c.categories && !a.keyToAxis ? a.pointArrayMap && a.pointArrayMap.filter(function (a) {
                    return "x" ===
                        a
                }).length ? (a.pointArrayMap.unshift("x"), a.pointArrayMap) : ["x", "y"] : a.pointArrayMap || ["y"]
            }, h = []; var x = 0; this.setUpKeyToAxis(); this.series.forEach(function (a) {
                var d = a.xAxis, e = a.options.keys || r(a, d), q = e.length, l = !a.requireSorting && {}, g = b.indexOf(d), B = H(a, e), k; if (!1 !== a.options.includeInDataExport && !a.options.isInternal && !1 !== a.visible) {
                    K(h, function (a) { return a[0] === g }) || h.push([g, x]); for (k = 0; k < q;)z = w(a, e[k], e.length), n.push(z.columnTitle || z), c && m.push(z.topLevelColumnTitle || z), k++; var A = {
                        chart: a.chart,
                        autoIncrement: a.autoIncrement, options: a.options, pointArrayMap: a.pointArrayMap
                    }; a.options.data.forEach(function (c, b) {
                        p && (B = H(a, e, b)); var w = { series: A }; a.pointClass.prototype.applyOptions.apply(w, [c]); c = w.x; var h = a.data[b] && a.data[b].name; k = 0; if (!d || "name" === a.exportKey || !p && d && d.hasNames && h) c = h; l && (l[c] && (c += "|" + b), l[c] = !0); t[c] || (t[c] = [], t[c].xValues = []); t[c].x = w.x; t[c].name = h; for (t[c].xValues[g] = w.x; k < q;)b = e[k], h = w[b], t[c][x + k] = v(B.categoryMap[b][h], B.dateTimeValueAxisMap[b] ? y.dateFormat(f.dateFormat,
                            h) : null, h), k++
                    }); x += k
                }
            }); for (e in t) Object.hasOwnProperty.call(t, e) && d.push(t[e]); var e = c ? [m, n] : [n]; for (x = h.length; x--;) { var A = h[x][0]; var D = h[x][1]; var g = b[A]; d.sort(function (a, c) { return a.xValues[A] - c.xValues[A] }); l = w(g); e[0].splice(D, 0, l); c && e[1] && e[1].splice(D, 0, l); d.forEach(function (a) { var c = a.name; g && !J(c) && (g.dateTime ? (a.x instanceof Date && (a.x = a.x.getTime()), c = y.dateFormat(f.dateFormat, a.x)) : c = g.categories ? v(g.names[a.x], g.categories[a.x], a.x) : a.x); a.splice(D, 0, c) }) } e = e.concat(d); C(this, "exportData",
                { dataRows: e }); return e
        }; b.prototype.getCSV = function (a) { var c = "", d = this.getDataRows(), b = this.options.exporting.csv, f = v(b.decimalPoint, "," !== b.itemDelimiter && a ? (1.1).toLocaleString()[1] : "."), k = v(b.itemDelimiter, "," === f ? ";" : ","), l = b.lineDelimiter; d.forEach(function (a, b) { for (var p, y = a.length; y--;)p = a[y], "string" === typeof p && (p = '"' + p + '"'), "number" === typeof p && "." !== f && (p = p.toString().replace(".", f)), a[y] = p; c += a.join(k); b < d.length - 1 && (c += l) }); return c }; b.prototype.getTable = function (a) {
            var c = function (a) {
                if (!a.tagName ||
                    "#text" === a.tagName) return a.textContent || ""; var b = a.attributes, d = "<" + a.tagName; b && Object.keys(b).forEach(function (a) { d += " " + a + '="' + b[a] + '"' }); d += ">"; d += a.textContent || ""; (a.children || []).forEach(function (a) { d += c(a) }); return d += "</" + a.tagName + ">"
            }; a = this.getTableAST(a); return c(a)
        }; b.prototype.getTableAST = function (a) {
            var c = 0, b = [], d = this.options, f = a ? (1.1).toLocaleString()[1] : ".", k = v(d.exporting.useMultiLevelHeaders, !0); a = this.getDataRows(k); var l = k ? a.shift() : null, g = a.shift(), m = function (a, c, b, d) {
                var h =
                    v(d, ""); c = "text" + (c ? " " + c : ""); "number" === typeof h ? (h = h.toString(), "," === f && (h = h.replace(".", f)), c = "number") : d || (c = "empty"); b = F({ "class": c }, b); return { tagName: a, attributes: b, textContent: h }
            }; !1 !== d.exporting.tableCaption && b.push({ tagName: "caption", attributes: { "class": "highcharts-table-caption" }, textContent: v(d.exporting.tableCaption, d.title.text ? d.title.text : "Chart") }); for (var n = 0, r = a.length; n < r; ++n)a[n].length > c && (c = a[n].length); b.push(function (a, c, b) {
                var f = [], h = 0; b = b || c && c.length; var l = 0, e; if (e = k &&
                    a && c) { a: if (e = a.length, c.length === e) { for (; e--;)if (a[e] !== c[e]) { e = !1; break a } e = !0 } else e = !1; e = !e } if (e) { for (e = []; h < b; ++h) { var g = a[h]; var p = a[h + 1]; g === p ? ++l : l ? (e.push(m("th", "highcharts-table-topheading", { scope: "col", colspan: l + 1 }, g)), l = 0) : (g === c[h] ? d.exporting.useRowspanHeaders ? (p = 2, delete c[h]) : (p = 1, c[h] = "") : p = 1, g = m("th", "highcharts-table-topheading", { scope: "col" }, g), 1 < p && g.attributes && (g.attributes.valign = "top", g.attributes.rowspan = p), e.push(g)) } f.push({ tagName: "tr", children: e }) } if (c) {
                        e = []; h = 0; for (b =
                            c.length; h < b; ++h)"undefined" !== typeof c[h] && e.push(m("th", null, { scope: "col" }, c[h])); f.push({ tagName: "tr", children: e })
                    } return { tagName: "thead", children: f }
            }(l, g, Math.max(c, g.length))); var u = []; a.forEach(function (a) { for (var b = [], d = 0; d < c; d++)b.push(m(d ? "td" : "th", null, d ? {} : { scope: "row" }, a[d])); u.push({ tagName: "tr", children: b }) }); b.push({ tagName: "tbody", children: u }); b = { tree: { tagName: "table", id: "highcharts-data-table-" + this.index, children: b } }; C(this, "aftergetTableAST", b); return b.tree
        }; b.prototype.downloadCSV =
            function () { var a = this.getCSV(!0); G(g(a, "text/csv") || "data:text/csv,\ufeff" + encodeURIComponent(a), this.getFilename() + ".csv") }; b.prototype.downloadXLS = function () {
                var a = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head>\x3c!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>Ark1</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--\x3e<style>td{border:none;font-family: Calibri, sans-serif;} .number{mso-number-format:"0.00";} .text{ mso-number-format:"@";}</style><meta name=ProgId content=Excel.Sheet><meta charset=UTF-8></head><body>' +
                    this.getTable(!0) + "</body></html>"; G(g(a, "application/vnd.ms-excel") || "data:application/vnd.ms-excel;base64," + l.btoa(unescape(encodeURIComponent(a))), this.getFilename() + ".xls")
            }; b.prototype.viewData = function () { this.toggleDataTable(!0) }; b.prototype.hideData = function () { this.toggleDataTable(!1) }; b.prototype.toggleDataTable = function (a) {
                (a = v(a, !this.isDataTableVisible)) && !this.dataTableDiv && (this.dataTableDiv = I.createElement("div"), this.dataTableDiv.className = "highcharts-data-table", this.renderTo.parentNode.insertBefore(this.dataTableDiv,
                    this.renderTo.nextSibling)); this.dataTableDiv && (this.dataTableDiv.style.display = a ? "block" : "none", a && (this.dataTableDiv.innerHTML = d.emptyHTML, (new d([this.getTableAST()])).addToDOM(this.dataTableDiv), C(this, "afterViewData", this.dataTableDiv))); this.isDataTableVisible = a; a = this.exportDivElements; var b = this.options.exporting, c = b && b.buttons && b.buttons.contextButton.menuItems; b = this.options.lang; u && u.menuItemDefinitions && b && b.viewData && b.hideData && c && a && (a = a[c.indexOf("viewData")]) && d.setElementHTML(a,
                        this.isDataTableVisible ? b.hideData : b.viewData)
            }; var u = n().exporting; u && (F(u.menuItemDefinitions, { downloadCSV: { textKey: "downloadCSV", onclick: function () { this.downloadCSV() } }, downloadXLS: { textKey: "downloadXLS", onclick: function () { this.downloadXLS() } }, viewData: { textKey: "viewData", onclick: function () { this.toggleDataTable() } } }), u.buttons && u.buttons.contextButton.menuItems.push("separator", "downloadCSV", "downloadXLS", "viewData")); f.map && (f.map.prototype.exportKey = "name"); f.mapbubble && (f.mapbubble.prototype.exportKey =
                "name"); f.treemap && (f.treemap.prototype.exportKey = "name")
    }); g(a, "masters/modules/export-data.src.js", [], function () { })
});
//# sourceMappingURL=export-data.js.map