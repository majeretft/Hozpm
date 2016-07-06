define(["require", "exports", 'app'], function (require, exports, app_1) {
    "use strict";
    requirejs(['jquery', 'bootstrap', 'knockout', 'sammy', 'app'], function () {
        $(function () {
            var app = new app_1.App();
            app.init();
        });
    });
});
//# sourceMappingURL=app.loader.js.map