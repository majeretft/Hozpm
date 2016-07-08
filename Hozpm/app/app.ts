import ko = require('knockout');
import $ = require('jquery');
import 'bootstrap';
//import 'components/named-table/module';

interface IView {
	id: number;
	linkText: string;
	pageCaption: string;
	components?: string[];
}

class App {
	public pageCaption = ko.observable<string>();
	public currentPageId = ko.observable<number>();

	private viewsList: IView[] = [
		{ id: 0, linkText: 'Стартовая страница', pageCaption: 'Стартовая страница' },
		{ id: 1, linkText: 'Группы', pageCaption: 'Группы', components: ['named-table'] },
		{ id: 2, linkText: 'Назначения', pageCaption: 'Назначения', components: ['named-table'] },
		{ id: 3, linkText: 'Продукция', pageCaption: 'Продукция', components: ['named-table'] },
		{ id: 4, linkText: 'Наборы', pageCaption: 'Наборы', components: ['named-table'] }
	];

	public init(): void {
		ko.applyBindings(this);
		this.updatePage(this.viewsList[0]);

		ko.components.register('named-table', { require: 'components/named-table/module' });
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
