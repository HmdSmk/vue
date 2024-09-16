import { createApp } from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import {createPinia} from 'pinia'

const pinia = createPinia(); // Create a Pinia instance

createApp(App).use(pinia).use(router).mount('#app')
