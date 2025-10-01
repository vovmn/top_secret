export const violationsGroupBy = [{ key: 'category', order: 'asc' }] as const

export const headersViolations = [
  { title: 'Категория', sortable: false, key: 'category' },
  { title: '№ объекта', key: 'id', align: 'end' },
  { title: 'Вид', key: 'view', align: 'end' },
  { title: 'Тип', key: 'type', align: 'end' },
  { title: 'Наименование ', key: 'name', align: 'end' },
  { title: 'Срок устранения', key: 'deadline', align: 'end' },
  { title: 'Дата проверки', key: 'dateReview', align: 'end' },
  { title: 'Остановочное', key: 'workStoppage', align: 'end' },
  { title: 'Дополнение', key: 'addition', align: 'end' },
  { title: 'Данные фиксации', key: 'commitData', align: 'end' },
] as const
