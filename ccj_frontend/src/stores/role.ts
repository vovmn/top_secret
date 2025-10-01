import { defineStore } from 'pinia'

export const useRoleStore = defineStore('role', {
  state: () => ({
    selectedRole: null as string | null,
  }),
})
