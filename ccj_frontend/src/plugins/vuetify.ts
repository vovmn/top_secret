/**
 * plugins/vuetify.ts
 *
 * Framework documentation: https://vuetifyjs.com`
 */

// Styles
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'

// Composables
import { createVuetify } from 'vuetify'

// https://vuetifyjs.com/en/introduction/why-vuetify/#feature-guides
export default createVuetify({
	theme: {
		defaultTheme: 'light',
		themes: {
			light: {
				dark: false,
				colors: {
					background: '#FAFAFF',
					surface: '#FAFAFF',
					primary: '#273469',
					secondary: '#30343F',
					accent: '#FF9800',
					error: '#D32F2F',
					info: '#0288D1',
					success: '#2E7D32',
					warning: '#ED6C02',
				}
			},
			dark: {
				dark: true,
				colors: {
					background: '#30343F',
					surface: '#95979F',
					primary: '#273469',
					accent: '#FFB74D',
					error: '#EF5350',
					info: '#29B6F6',
					success: '#66BB6A',
					warning: '#FF9800',
				},
			}
		}
	},
})
