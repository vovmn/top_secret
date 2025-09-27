// Composables
import { createApp } from 'vue'

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Styles
import 'unfonts.css'
import '@/../node_modules/frappe-gantt/dist/frappe-gantt.css'

const app = createApp(App)

registerPlugins(app)

app.mount('#app')
