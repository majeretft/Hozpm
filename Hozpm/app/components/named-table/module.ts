import ko = require('knockout');
import 'jquery';
import 'bootstrap';

export class NamedTable {
	public data: string[] = ['1', '2', '3'];
}

ko.components.register('named-table', {
	viewModel: NamedTable,
	template: { require: 'text!components/named-table/view.html' }
});
