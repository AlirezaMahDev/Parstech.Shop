/* flatpickr v4.5.1, @license MIT */
(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
        typeof define === 'function' && define.amd ? define(['exports'], factory) :
            (factory((global.he = {})));
}(this, (function (exports) {
    'use strict';

    var fp = typeof window !== "undefined" && window.flatpickr !== undefined ? window.flatpickr : {
        l10ns: {}
    };
    var Hebrew = {
        weekdays: {
            shorthand: ["א", "ב", "ג", "ד", "ה", "ו", "ז"],
            longhand: ["ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת"]
        },
        months: {
            shorthand: ["ינו׳", "פבר׳", "מרץ", "אפר׳", "מאי", "יוני", "יולי", "אוג׳", "ספט׳", "אוק׳", "נוב׳", "דצמ׳"],
            longhand: ["ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר", "נובמבר", "דצמבר"]
        }
    };
    fp.l10ns.he = Hebrew;
    var he = fp.l10ns;

    exports.Hebrew = Hebrew;
    exports.default = he;

    Object.defineProperty(exports, '__esModule', {value: true});

})));
