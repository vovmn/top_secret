/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Composables
import { createApp } from 'vue'

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

import vuetify from './plugins/vuetify'
// Styles
import 'unfonts.css'

const app = createApp(App)

registerPlugins(app)
app.use(vuetify)

app.mount('#app')
