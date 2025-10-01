// Utilities
import type { LoginForm } from '@/pages/auth/types/auth_login_types'
import type { SignupForm } from '@/pages/auth/types/auth_signup_types'
import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    username: '',
    isAuthenticated: false,
    authToken: '',
  }),

  actions: {
    restoreSession () {
      const savedUser = localStorage.getItem('username')
      if (savedUser) {
        this.username = savedUser
        this.isAuthenticated = true
      }
    },

    async login (formData: LoginForm) {
      try {
        // << Здесь запрос >>
        this.username = formData.login
        this.isAuthenticated = true

        // << Токен в куки >>
        // << Желательно httpOnly вместо localStorage >>
        localStorage.setItem('username', this.username)

        return { success: true }
      } catch {
        return { success: false, error: 'Введите логин' }
      }
    },

    async signup (formData: SignupForm) {
      try {
        // << Здесь запрос >>
        console.log('Pinia принял форму:', formData.name, formData.surname, formData.fathername)
        return { success: true }
      } catch {
        return { success: false, error: 'Закончите регистрацию' }
      }
    },

    async logout () {
      this.username = ''
      this.isAuthenticated = false
      localStorage.removeItem('username')
    },
  },
})
