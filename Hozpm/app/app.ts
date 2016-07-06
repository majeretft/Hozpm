import ko = require('knockout');
import $ = require('jquery');
import 'bootstrap';

export class App {
	init(): void {
		ko.components.register('navbar-component', {
			template: { require: 'text!components/navbar/template.html' },
			viewModel: null
		});

		ko.applyBindings(null);
	}
}

$(() => {
	const app = new App();
	app.init();
});
