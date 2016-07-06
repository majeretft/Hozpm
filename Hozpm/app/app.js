define(["require", "exports"], function (require, exports) {
    "use strict";
    var App = (function () {
        function App() {
        }
        App.prototype.init = function () {
            $(document.body).append('<div>Hello World !</div>');
        };
        return App;
    }());
    exports.App = App;
});
//# sourceMappingURL=app.js.map