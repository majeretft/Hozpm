define(["require", "exports", 'knockout', 'jquery', 'bootstrap'], function (require, exports, ko, $) {
    "use strict";
    var App = (function () {
        function App() {
        }
        App.prototype.init = function () {
            ko.components.register('navbar-component', {
                template: { require: 'text!components/navbar/template.html' },
                viewModel: null
            });
            ko.applyBindings(null);
        };
        return App;
    }());
    exports.App = App;
    $(function () {
        var app = new App();
        app.init();
    });
});
//# sourceMappingURL=app.js.map