define(["require", "exports", 'knockout', 'jquery', 'bootstrap'], function (require, exports, ko, $) {
    "use strict";
    var App = (function () {
        function App() {
            this.menu = new Menu();
        }
        App.prototype.init = function () {
            ko.applyBindings(this);
        };
        return App;
    }());
    var Menu = (function () {
        function Menu() {
            var _this = this;
            this.links = [
                { id: 0, text: 'Стартовая страница' },
                { id: 1, text: 'Группы' },
                { id: 2, text: 'Назначения' },
                { id: 3, text: 'Продукция' },
                { id: 4, text: 'Наборы' }
            ];
            this.current = ko.observable(3);
            this.setCurrent = function (context) {
                _this.current(context.id);
            };
        }
        return Menu;
    }());
    $(function () {
        var app = new App();
        app.init();
    });
});
//# sourceMappingURL=app.js.map