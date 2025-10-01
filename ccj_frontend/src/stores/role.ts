import { defineStore } from 'pinia'

export const useRoleStore = defineStore('role', {
  state: () => ({
    selectedRole: null as string | null,
  }),

  actions: {
    setRole (role: string) {
      this.selectedRole = role
    },

    clearRole () {
      this.selectedRole = null
    },
  },
})
