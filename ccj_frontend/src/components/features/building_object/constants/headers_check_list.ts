export const checkListGroupBy = [{ key: 'data', order: 'asc' }] as const;

export const headersCheckList = [
	{ title: 'Характеристики', align: 'start', key: 'data', groupable: false},
	{ title: '№ объекта', align: 'end', key: 'numObject'},
	{ title: 'Дата проведения', align: 'end', key: 'deadline'},
	{ title: 'Статус', align: 'end', key: 'status'},
] as const;