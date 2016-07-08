define(["require", "exports", 'knockout', 'jquery', 'bootstrap'], function (require, exports, ko) {
    "use strict";
    var NamedTable = (function () {
        function NamedTable() {
            this.data = ['1', '2', '3'];
        }
        return NamedTable;
    }());
    exports.NamedTable = NamedTable;
    ko.components.register('named-table', {
        viewModel: NamedTable,
        template: { require: 'text!components/named-table/view.html' }
    });
});
//# sourceMappingURL=module.js.map