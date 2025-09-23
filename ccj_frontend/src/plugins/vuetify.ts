/**
 * plugins/vuetify.ts
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Composables
import { createVuetify } from 'vuetify'
// Styles
import '@mdi/font/css/materialdesignicons.css'

import 'vuetify/styles'

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
export default createVuetify({
  theme: {
    defaultTheme: 'light',
    themes: {
      light: {
        dark: false,
        colors: {
          background: '#F8F9FC',
          surface: '#FFFFFF',
          primary: '#2A4BA0',
          secondary: '#5C6BC0',
          accent: '#FF6D00',
          error: '#C62828',
          info: '#1E88E5',
          success: '#2E7D32',
          warning: '#ED6C02',
        },
      },
      dark: {
        dark: true,
        colors: {
          background: '#121827',
          surface: '#1e293b',
          onSurface: '#FFFFFF',
          primary: '#1E293B',
          secondary: '#4B5563',
          text: '#E5E7EB',
          accent: '#D97706',
          error: '#EF4444',
          info: '#29B6F6',
          success: '#10B981',
          warning: '#F59E0B',
        },
      },
    },
  },
})
