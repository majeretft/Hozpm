import { App as Application } from 'app';
requirejs(['jquery', 'bootstrap', 'knockout', 'sammy', 'app'], () => {
    $(() => {
        const app = new Application();
        app.init();
    });
});
//# sourceMappingURL=app.loader.js.map