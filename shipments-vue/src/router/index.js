import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ShipmentsView from '../views/ShipmentsView.vue'
import ScheduleView from '../views/ScheduleView.vue'
import ForwarderView from '../views/ForwarderView.vue'
import CustomerView from '../views/CustomerView.vue'
import PurchaseOrderView from '../views/PurchaseOrderView.vue'
import EditShipmentView from '../views/EditShipmentView.vue'
import EditOrderView from '../views/EditOrderView.vue'
import EditCustomerView from '../views/EditCustomerView.vue'
import AddAllView from '../views/AddAllView.vue'

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
      path: '/orders',
      name: 'PurchaseOrderView',
      component: PurchaseOrderView
    },
    {
      path: '/schedule',
      name: 'ScheduleView',
      component: ScheduleView,
      props: true
    },
    {
      path: '/forwarder',
      name: 'ForwarderView',
      component: ForwarderView
    },
    {
        path: '/shipment',
        name: 'ShipmentsView',
        component: ShipmentsView,
        props: true
    },
    {
        path: '/addall',
        name: 'AddAllView',
        component: AddAllView,
        props: true
    },
    {
        path: '/shipment/edit/:shipmentId',
        name: 'EditShipmentView',
        component: EditShipmentView,
        props: true
        
    },
    {
        path: '/order/edit/:orderId',
        name: 'EditOrderView',
        component: EditOrderView,
        props: true
        
    },
    {
        path: '/customer/edit/:customerId',
        name: 'EditCustomerView',
        component: EditCustomerView,
        props: true
        
    }
    
  ]
})

export default router
