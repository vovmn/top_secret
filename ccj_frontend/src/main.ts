/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'

// Styles
import 'unfonts.css'
import vuetify from './plugins/vuetify'

const app = createApp(App)

registerPlugins(app)
app.use(vuetify);

app.mount('#app')
