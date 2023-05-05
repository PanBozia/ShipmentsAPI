<template>
    <Spinner v-if="isPending1 || isPending2 || isPending3" />
   <div class="orders-screen">
        <div class="orders-ctnr">
            <h2>wybierz zamówienia</h2>
            <div class="orders-ctnr-inner">
                    <EditOrders @add-orders-event="getOrdersList" :shipmentId="shipmentId" />
            </div>
            <div class="exit-btn" @click="handleExit">
                <p>
                    Anuluj
                </p>
            </div>
        </div>
    </div>
</template>

<script>
import { onBeforeMount } from 'vue'
import EditOrders from '../components/EditOrders.vue'
import { useLinksStore } from '../stores/linksStore.js'
import getShipmentById from '../js-components/getShipmentById'
import removeOrderToShipment from '../js-components/removeOrderFromShipment.js'
import addOrderToShipment from '../js-components/addOrderToShipment.js'
import Spinner from './SpinnerComponent.vue'
export default {
    emits:['changeOrdersEvent'],
    props:['shipmentId'],
    components:{EditOrders, Spinner},
    setup(props, context){
        const linksStore = useLinksStore()
        const {loadShipment, error:loadShipmentsError, shipment, isPending:isPending1 } = getShipmentById(linksStore.url)
        const {removeOrder, isPending:isPending2} = removeOrderToShipment(linksStore.url)
        const {addOrder, isPending:isPending3} = addOrderToShipment(linksStore.url)

        onBeforeMount(()=>{
            loadShipment(props.shipmentId)
        })
        const removeCurrentOrders = async (ordersArray)=>{
           await ordersArray.forEach(order => {
                removeOrder(props.shipmentId, order.id)
                });
        }
        const addNewOrders = async (ordersArray) =>{
            await ordersArray.forEach(order => {
                    addOrder(props.shipmentId, order.id)
                });
        }

        const getOrdersList = (ordersList)=>{
            removeCurrentOrders(shipment.value.purchaseOrders).then(()=>{
                setTimeout(() => {
                    console.log("Delayed for 0,3 sec.");
                    addNewOrders(ordersList).then(()=>{
                        setTimeout(() => {
                            console.log("Delayed for 0,3 sec.");
                            // console.log(ordersList)
                            context.emit('changeOrdersEvent', true)
                        }, "300");
                    })
                    }, "300");
            })
            
        }

        const handleExit = ()=>{    
            context.emit('changeOrdersEvent', false)
        }

        return {
            // error,
            loadShipmentsError,
            getOrdersList, handleExit,
            isPending1, isPending2, isPending3
        }
    }
    }
</script>

<style>
.orders-screen{
 position: absolute;
    width: 100%;
    height: 100%;
    background: #14141480;
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.orders-ctnr{
    display: block;
    width: 80vh;
    /* height: 72vh; */
    margin:8vh auto 1vh auto;
    padding: 1.5vh 0vh;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
}
.orders-ctnr h2{
    padding: 0;
    margin: 1.4vh  0 3vh 0;
    text-align: center;
}
.orders-ctnr-inner{
    display: grid;
    justify-items: center;
}
.orders-ctnr-inner .add-single-form{
    width: 100%;
}
</style>