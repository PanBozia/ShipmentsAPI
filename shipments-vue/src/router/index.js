import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ShipmentsView from '../views/ShipmentsView.vue'
import ShipmentsWarehouseView from '../views/ShipmentsWarehouseView.vue'
import SingleShipmentView from '../views/SingleShipmentView.vue'
import SingleShipmentWarehouseView from '../views/SingleShipmentWarehouseView.vue'
import ScheduleView from '../views/ScheduleView.vue'
import ForwarderView from '../views/ForwarderView.vue'
import CustomerView from '../views/CustomerView.vue'
import PurchaseOrderView from '../views/PurchaseOrderView.vue'
import EditShipmentView from '../views/EditShipmentView.vue'
import EditOrderView from '../views/EditOrderView.vue'
import EditCustomerView from '../views/EditCustomerView.vue'
import AddAllView from '../views/AddAllView.vue'
import EditForwarderView from '../views/EditForwarderView.vue'
import WarehouseView from '../views/WarehouseView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/office/customers',
      name: 'CustomerView',
      component: CustomerView
    },
    {
      path: '/office/orders',
      name: 'PurchaseOrderView',
      component: PurchaseOrderView
    },
    {
      path: '/schedule/:userIsOffice',
      name: 'ScheduleView',
      component: ScheduleView,
      props: true
    },
    {
      path: '/office/forwarder',
      name: 'ForwarderView',
      component: ForwarderView
    },
    {
        path: '/office/shipments',
        name: 'ShipmentsView',
        component: ShipmentsView,
        props: true
    },
    {
        path: '/warehouse/shipments',
        name: 'ShipmentsWarehouseView',
        component: ShipmentsWarehouseView,
        props: true
    },
    {
        path: '/office/shipment/:shipmentId',
        name: 'SingleShipmentView',
        component: SingleShipmentView,
        props: true
    },
    {
        path: '/warehouse/shipment/:shipmentId',
        name: 'SingleShipmentWarehouseView',
        component: SingleShipmentWarehouseView,
        props: true
    },
    {
        path: '/warehouse/locations',
        name: 'WarehouseView',
        component: WarehouseView,
        props: true
    },
    {
        path: '/office/addall',
        name: 'AddAllView',
        component: AddAllView,
        props: true
    },
    {
        path: '/office/shipment/edit/:shipmentId',
        name: 'EditShipmentView',
        component: EditShipmentView,
        props: true
        
    },
    {
        path: '/office/order/edit/:orderId',
        name: 'EditOrderView',
        component: EditOrderView,
        props: true
        
    },
    {
        path: '/office/customer/edit/:customerId',
        name: 'EditCustomerView',
        component: EditCustomerView,
        props: true
        
    },
    {
        path: '/office/forwarder/edit/:forwarderId',
        name: 'EditForwarderView',
        component: EditForwarderView,
        props: true
        
    }
    
  ]
})

export default router
