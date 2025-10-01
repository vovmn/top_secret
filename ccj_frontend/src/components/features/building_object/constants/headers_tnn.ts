export const tnnGroupBy = [{ key: 'product', order: 'asc' }] as const

export const headersTnn = [
  { title: 'Товар', sortable: false, key: 'product' },
  { title: '№', key: 'number', align: 'end' },
  { title: 'Объем', key: 'volume', align: 'end' },
  { title: 'Единица измерения', key: 'unit', align: 'end' },
  { title: 'Дата поступления', key: 'date', align: 'end' },
  { title: 'Паспорт', key: 'passport', align: 'end' },
] as const
