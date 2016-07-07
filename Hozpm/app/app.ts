import ko = require('knockout');
import $ = require('jquery');
import 'bootstrap';

interface ILink {
	id: number;
	text: string;
}

class App {
	public menu = new Menu();

	public init(): void {
		ko.applyBindings(this);
	}
}

class Menu {
	public links: ILink[] = [
		{ id: 0, text: 'Стартовая страница' },
		{ id: 1, text: 'Группы' },
		{ id: 2, text: 'Назначения' },
		{ id: 3, text: 'Продукция' },
		{ id: 4, text: 'Наборы' }
	];

	public current = ko.observable(3);

	public setCurrent = (context: ILink) => {
		this.current(context.id);
	}
}

$(() => {
	const app = new App();
	app.init();
});
