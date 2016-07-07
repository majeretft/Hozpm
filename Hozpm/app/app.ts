import ko = require('knockout');
import $ = require('jquery');
import 'bootstrap';

interface IView {
	id: number;
	linkText: string;
	pageCaption: string;
}

class App {
	public pageCaption = ko.observable<string>();
	public currentPageId = ko.observable<number>();

	private viewsList: IView[] = [
		{ id: 0, linkText: 'Стартовая страница', pageCaption: 'Стартовая страница' },
		{ id: 1, linkText: 'Группы', pageCaption: 'Группы' },
		{ id: 2, linkText: 'Назначения', pageCaption: 'Назначения' },
		{ id: 3, linkText: 'Продукция', pageCaption: 'Продукция' },
		{ id: 4, linkText: 'Наборы', pageCaption: 'Наборы' }
	];

	public init(): void {
		ko.applyBindings(this);
		this.updatePage(this.viewsList[0]);
	}

	public updatePage = (view: IView): void => {
		const pageId = view.id;
		const pageCaption = view.pageCaption;
		this.currentPageId(pageId);
		this.pageCaption(pageCaption);
	}
}

$(() => {
	const app = new App();
	app.init();
});
