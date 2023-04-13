import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import moment from "moment/dist/moment"
import pl from 'moment/dist/locale/pl'

import './assets/main.css'
import './assets/forms.css'


moment.locale("pl", pl)
const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(moment)
app.mount('#app')
