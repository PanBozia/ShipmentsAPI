import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ShipmentsView from '../views/ShipmentsView.vue'
import ScheduleView from '../views/ScheduleView.vue'
import ForwarderView from '../views/ForwarderView.vue'
import CustomerView from '../views/CustomerView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/customers',
      name: 'CustomerView',
      component: CustomerView
    },
    {
      path: '/schedule',
      name: 'ScheduleView',
      component: ScheduleView
    },
    {
      path: '/forwarder',
      name: 'ForwarderView',
      component: ForwarderView
    },
    {
        path: '/shipment',
        name: 'ShipmentsView',
        component: ShipmentsView
    }
    
  ]
})

export default router
