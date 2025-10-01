export const activeStatuses = {
  active: { slug: 'active', name: 'Активирован', color: 'info' },
  underControl: { slug: 'underControl', name: 'На контроле', color: 'warning' },
  completed: { slug: 'completed', name: 'Завершен', color: 'success' },
  notStarted: { slug: 'notStarted', name: 'Не начат', color: 'grey-lighten-2' },
} as const

export type StatusKey = keyof typeof activeStatuses
