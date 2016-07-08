define(["require", "exports", 'knockout', 'jquery', 'bootstrap'], function (require, exports, ko, $) {
    "use strict";
    var App = (function () {
        function App() {
            var _this = this;
            this.pageCaption = ko.observable();
            this.currentPageId = ko.observable();
            this.viewsList = [
                { id: 0, linkText: 'Стартовая страница', pageCaption: 'Стартовая страница' },
                { id: 1, linkText: 'Группы', pageCaption: 'Группы', components: ['named-table'] },
                { id: 2, linkText: 'Назначения', pageCaption: 'Назначения', components: ['named-table'] },
                { id: 3, linkText: 'Продукция', pageCaption: 'Продукция', components: ['named-table'] },
                { id: 4, linkText: 'Наборы', pageCaption: 'Наборы', components: ['named-table'] }
            ];
            this.updatePage = function (view) {
                var pageId = view.id;
                var pageCaption = view.pageCaption;
                _this.currentPageId(pageId);
                _this.pageCaption(pageCaption);
            };
        }
        App.prototype.init = function () {
            ko.applyBindings(this);
            this.updatePage(this.viewsList[0]);
            ko.components.register('named-table', { require: 'components/named-table/module' });
        };
        return App;
    }());
    $(function () {
        var app = new App();
        app.init();
    });
});
//# sourceMappingURL=app.js.map