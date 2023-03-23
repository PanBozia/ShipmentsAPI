<template>
  <h3>Aktualnie wysyłane zamówienia</h3>
  <form class="form-add" @submit.prevent="handleSearchOrders()">
    <label class="form-labels">Wybrane zamówienia</label>
    <div v-if="shipment.purchaseOrders != 0">
      <div
        class="chosen-one"
        v-for="po in shipment.purchaseOrders"
        :key="po.id"
      >
        <div>
          {{ po.poNumber + " - " + po.customerShortName }}
        </div>
        <div class="remove-btn" @click="handleRemoveOrder(po.id)">
          <span class="material-symbols-outlined"> remove_circle </span>
        </div>
      </div>
    </div>
    <div class="chosen-one red" v-else>
      <div>Brak zamówień</div>
    </div>

    <label class="form-labels">Wyszukaj i wybierz zamówienia</label>
    <div class="search-ctnr">
      <input class="search-input" type="text" v-model="searchOrderPhrase" />
      <button class="search-button">Szukaj</button>
    </div>
    <div v-if="orders != null">
      <div class="forwarder-list-ctnr">
        <div v-if="orders.length == 0">
          <p id="no-result">Brak wyników wyszukiwania...</p>
        </div>
        <div v-else class="driver-line" v-for="order in orders" :key="order.id">
          <p>{{ order.poNumber + " - " + order.customerShortName }}</p>
          <div class="add-driver-btn" @click="handleAddOrder(order.id)">
            <span class="material-symbols-outlined"> add_circle </span>
          </div>
        </div>
      </div>
      <div v-if="totalItemsCountPo > 20">
        <p class="red">
          Znaleziono więcej wyników spełniających kryteria wyszukiwania.
        </p>
      </div>
      <div v-if="addOrderError">
        <p class="red">
          {{addOrderError}}
        </p>
      </div>
    </div>
  </form>
</template>

<script>
import getPurchaseOrders from '../js-components/getPurchaseOrders.js'
import addOrderToShipment from '../js-components/addOrderToShipment.js'
import removeOrderFromShipment from '../js-components/removeOrderFromShipment.js'
import getShipmentById from '../js-components/getShipmentById.js'
import { onBeforeMount, ref, watch } from 'vue'

export default {
    props:['shipmentId', 'addedPoNumber'],
    setup(props){
        const url = 'https://localhost:44331/api/'
        const {loadShipment, error:loadShipmentError, shipment} = getShipmentById(url)
        const {addOrder, error:addOrderError} = addOrderToShipment(url)
        const {removeOrder, error:removeOrderError} = removeOrderFromShipment(url)
        const {loadOrders, error:loadOrdersError, orders, totalItemsCount:totalItemsCountPo} = getPurchaseOrders(url)
        const searchOrderPhrase = ref('')
        const firstOrderFromListId = ref('')
        const hardCodedId = '37747698-82b5-4c36-b524-dc5b5327bc88'
        onBeforeMount(()=>{
           //loadShipment(props.shipmentId)
            loadShipment(hardCodedId)
        })

        const handleSearchOrders = () =>{
            loadOrders(searchOrderPhrase.value,20,1,'DeliveryDate',1)
            // .then(()=>{
            //     if(orders.value){
            //         //console.log(orders.value[0].id)
            //         firstOrderFromListId.value = orders.value[0].id
            //     }
            // })
        }
        const handleAddOrder = (orderId) =>{
            //addOrder(props.shipmentId, orderId)
            addOrder(hardCodedId, orderId).then(()=>{
                loadShipment(hardCodedId)
            })
        }
        const handleRemoveOrder = (orderId) =>{
            //removeOrder(props.shipmentId, orderId)
            removeOrder(hardCodedId, orderId).then(()=>{
                loadShipment(hardCodedId)
            })
        }

        watch (orders,()=>{
                if(orders.value){
                    console.log(orders.value[0].id)
                    firstOrderFromListId.value = orders.value[0].id
                }
        })

        watch(props, ()=>{
            if(props.addedPoNumber != null){
                searchOrderPhrase.value = props.addedPoNumber
                loadOrders(searchOrderPhrase.value,20,1,'DeliveryDate',1).then(()=>{
                    addOrder(hardCodedId, firstOrderFromListId.value).then(()=>{
                        loadShipment(hardCodedId)
                    })
                })
            }
        })

        return {
                handleSearchOrders, handleAddOrder, handleRemoveOrder,
                addOrderError, removeOrderError, loadOrdersError, loadShipmentError,
                totalItemsCountPo,
                shipment,
                orders,
                searchOrderPhrase
        }
    }
}
</script>

<style>
.comment{
    font-family: 'Poppins', sans-serif;
    font-weight: 400;
    font-size: 0.8em;
    padding: 8px 12px;
}

#header-edit-shipment{
    display: flex;
    justify-content: center;
}
.edit-shipment-container{
    display: flex;
    justify-content: center;
}



.form-double-ctnr{
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
    align-items: end;
}
.form-double-ctnr .double-ctnr-item{
    display: flex;
    flex-flow: column;
}

.chosen-one{
    display: flex;
    align-items: center;
    justify-content: space-between;
    
    height: 34px;
    margin: 0px 0px 10px 0;
    padding: 2px 10px;
    /* border-bottom: 1px solid #fff; */
    border: 1px solid #fff;
    font-size: 0.8em;
    background: #1f3659;
    border-radius: 4px;
}
.chosen-one .remove-btn{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 16px;
    width: 16px;
    background-color: #e14c27;
    border-radius: 50%;
    cursor: pointer;
}
.chosen-one .remove-btn:hover{
    transform: scale(1.4);
    transition: ease-in-out 100ms;

}
.chosen-one .remove-btn span{
    font-size: 1.8em;
    font-weight: 200;
    pointer-events: none;
}
.search-ctnr{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px;
}
.search-ctnr .search-button{
    height: 34px;
    margin: 0px;
    padding: 2px 6px;
}
.search-ctnr .search-input{
    height: 30px;
    margin: 0px;
    padding: 2px 6px;
}

.forwarder-list-ctnr{
    min-height: 244px;
    max-height: 244px;
    overflow-y: auto;
    background: #fff;
    border-radius: 4px;
}
.forwarder-list-ctnr #no-result{
    padding: 10px 20px;
    color: #666;
    font-size: 0.9em;

}

.driver-line{
    display: flex;
    align-items: center;
    justify-content: space-between;
    background-color: #fff;
    color: #333;
    padding:2px 10px;
    font-size: 0.8em;
    border-bottom: 1px solid #333;
    cursor: pointer;
}
.driver-line:hover{
    background-color: #eee;
}
.driver-line p{
    padding: 0;
    margin: 4px 0px;
}
.add-driver-btn{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 16px;
    width: 16px;
    background-color: #ffffff;
    border-radius: 50%;
    /* pointer-events: none; */
}
.add-driver-btn:hover{
    transform: scale(1.4);
    transition: ease-in-out 100ms;
    background-color: #b3ff00;
}


.add-driver-btn span{
    font-size: 1.8em;
    font-weight: 300;
}

.form-add .red{
    color: rgb(228, 67, 52);
    font-size: 0.8em;
    font-weight: 400;
}
.add-shipment-ctnr{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 40px;
}

.prio-ctnr{
    display: flex;
    margin-bottom: 10px;
    align-content: center;
}
.prio-ctnr p{
    display: flex;
    align-items: center;
    margin: 0 14px 0 0;
    padding: 0;
    
    font-size: 1.2em;
}

.userSwitch {
    margin: 2px 0;
    position: relative;
    width: 94px;
    align-self: center;
}
.userSwitch input[type=checkbox] {
  display: none;
}
.userSwitch-label {
  display: block;
  overflow: hidden;
  cursor: pointer;
  border: 0px solid #000c20;
  border-radius: 50px;
}
.userSwitch-inner {
  width: 200%;
  margin-left: -100%;
  transition: margin 0.2s ease-in 0s;
  
}
.userSwitch-inner:before, .userSwitch-inner:after {
  float: left;
  width: 50%;
  height: 24px;
  padding: 0;
  line-height: 24px;
  font-size: 11px;
  color: white;
  /* font-family: Trebuchet, Arial, sans-serif; */
  /* font-weight: bold; */
  box-sizing: border-box;
  
}
.userSwitch-inner:before {
  content: "PRIO";
  padding-left: 10px;
  background-color: #fff;
  color: #000000;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.switches .flipswitch-inner:after {
    content: "";

}
.userSwitch-inner:after {
    content: "STANDARD";
    padding-right: 8px;
    background-color: #003e6a;
    color: #E8E8E8;
    text-align: right;
    box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.userSwitch-switch {
  width: 24px;
  height: 24px;
  margin: 0px;
  background: #F5B727;
  box-shadow: 1px 1px 3px rgba(4,4,4,0.4), -1px -1px 3px rgba(4,4,4,0.4);
  border-radius: 50px;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 70px;
  transition: all 0.2s ease-in 0s;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-inner {
  margin-left: 0;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-switch {
  right: 0;
}
</style>

