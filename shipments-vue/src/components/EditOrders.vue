<template>
  <div class="choose-order-container non-symertic">
    <div>
      <h2>Wybierz zamówienia</h2>
      <form class="form-add" @submit.prevent="handleSearchOrders()">
        <label class="form-labels">Wybrane zamówienia</label>
        <div v-if="ordersList.length != 0">
          <div class="chosen-one" v-for="po in ordersList" :key="po.id">
            <div>
              {{ po.poNumber + " - " + po.customerShortName }}
            </div>
            <div class="remove-btn" @click="handleRemoveOrder(po)">
              <span class="material-symbols-outlined">close</span>
            </div>
          </div>
        </div>
        <div class="chosen-one red" v-else>
          <div>Brak zamówień</div>
        </div>

        <label class="form-labels">Wyszukaj i wybierz zamówienia</label>
        <div class="search-ctnr">
          <input class="search-input" type="text" v-model="searchOrderPhrase" />
          <div class="search-button btn" @click="handleSearchOrders">
            Szukaj
          </div>
        </div>
        <div v-if="orders != null">
          <div class="forwarder-list-ctnr">
            <div v-if="orders.length == 0">
              <p id="no-result">Brak wyników wyszukiwania...</p>
            </div>
            <div
              v-else
              class="driver-line"
              v-for="order in orders"
              :key="order.id"
            >
              <p>{{ order.poNumber + " - " + order.customerShortName }}</p>
              <div class="add-driver-btn" @click="handleAddOrder(order)">
                <div class="add-item-btn"><span>DODAJ</span></div>
              </div>
            </div>
          </div>
          <div v-if="totalItemsCountPo > 20">
            <p class="red">
              Znaleziono więcej wyników spełniających kryteria wyszukiwania.
            </p>
          </div>
        </div>
        <div>
          
        </div>
      </form>
    </div>

    <div>
      <h2>Dodaj nowe zamówienie</h2>
      <div >
        <form @submit.prevent="" class="form-add">
            <div class="form-set">
                <label class="form-labels">Numer zamówienia</label>
                <input type="text" v-model="poNumberForm" required />
            </div>
            <div class="form-set">
                <label class="form-labels">Kategoria</label>
                <select v-model="categoryForm" required>
                    <option class="option-para" value="Sample">Sample</option>
                    <option class="option-para" value="Standard">Standard</option>
                    <option class="option-para" value="Inne">Inne</option>
                </select>
            </div>
            <div class="form-set">
                <label class="form-labels">Data dostawy</label>
                <input type="date" v-model="deliveryDateForm" required />
            </div>
            <div class="form-set">
                <label class="form-labels">Incoterms</label>
                <select class="incoterms" v-if="!incotermsError" v-model="incotermIdForm">
                    <option
                    class="option-para"
                    v-for="inco in incoterms"
                    :key="inco.id"
                    :value="inco.id"
                    >
                    {{ inco.shortName }} - {{ inco.name }}
                    </option>
                </select>
            </div>
                <label class="form-labels">Klient</label>
                <select v-if="!customersError" v-model="customerIdForm">
                    <option
                    class="option-para"
                    v-for="customer in customers"
                    :key="customer.id"
                    :value="customer.id"
                    >
                    {{ customer.shortName }} ({{ customer.cityAddress }} -
                    {{ customer.countryAddress }})
                    </option>
                </select>
            <div class="form-set">
                <div v-if="!addPoError" class="btn-ctnr">
                    <div class="btn" @click="handleCreatePo">Dodaj</div>
                </div>
            </div>

          <div
            v-if="addPoError || customersError || incotermsError"
            class="error"
          >
            <p>{{ addPoError }}</p>
            <p>{{ customersError }}</p>
            <p>{{ incotermsError }}</p>
          </div>
          <div v-if="createdFlag" class="success">
            <p>Zamówienie zostało zapisane.</p>
          </div>
        </form>
      </div>
    </div>
  </div>
<div class="btn-ctnr">
    <button class="btn" @click="handleSubmitOrdersList()">
        Zapisz
    </button>
</div>
    <ErrorComponent v-if="addPoError" :msg="addPoError" @reset-error-event="resetErrors" />
    <ErrorComponent v-if="customersError" :msg="customersError" @reset-error-event="resetErrors" />
</template>

<script>
import getPurchaseOrders from "../js-components/getPurchaseOrders.js";
import { onBeforeMount, ref } from "vue";
import getIncoterms from '../js-components/getIncoterms.js'
import getAllCustomers from '../js-components/getAllCustomers.js'
import addPurchaseOrder from '../js-components/addPurchaseOrder.js'
import getPurchaseOrderById from '../js-components/getPurchaseOrderById.js'
import { useLinksStore } from '../stores/linksStore.js'
import getShipmentById from '../js-components/getShipmentById.js'
import moment from 'moment'
import ErrorComponent from './ErrorComponent.vue';

export default {
    emits:['add-orders-event'],
    props:['shipmentId'],
    components:{ErrorComponent},
    setup(props, context) {
    const linksStore = useLinksStore()
    const { loadOrders, error: loadOrdersError, orders, totalItemsCount: totalItemsCountPo } = getPurchaseOrders(linksStore.url);
    const { loadShipment, error:loadShipmentError, shipment } = getShipmentById(linksStore.url)
    const searchOrderPhrase = ref("");
    const ordersList = ref([]);

    onBeforeMount(()=>{
        loadShipment(props.shipmentId).then(()=>{
            shipment.value.purchaseOrders.forEach(order => {
                ordersList.value.push(order)
            });
        })
    })

    const handleSearchOrders = () => {
      loadOrders(searchOrderPhrase.value, 20, 1, "DeliveryDate", 1);
    };
    const handleAddOrder = (order) => {
        let orderExist = false
        ordersList.value.forEach(orderFromList => {
            if(orderFromList.id == order.id ){orderExist = true}
        });
        if (!orderExist) ordersList.value.push(order);
    };
    const handleRemoveOrder = (order) => {
      ordersList.value = ordersList.value.filter((po) => po != order);
        // ordersList.value.pop(order)
    };

    const handleSubmitOrdersList = () => {
      context.emit("add-orders-event", ordersList.value);
    };

    //add order
        const {loadOrder, order:newOrder} = getPurchaseOrderById(linksStore.url)
        const poNumberForm = ref('')
        const categoryForm = ref('Standard')
        const deliveryDateForm = ref()
        const incotermIdForm = ref(1)
        const customerIdForm = ref()
        const createdFlag = ref(false)

        const {loadIncoterms, incoterms, error:incotermsError} = getIncoterms(linksStore.url)
        const {loadAllCustomers, customers, error:customersError} = getAllCustomers(linksStore.url)
        const {addNewPO, error:addPoError, createdId} = addPurchaseOrder(linksStore.url)
        
        onBeforeMount (()=>{
            loadIncoterms();
            loadAllCustomers();
        })

        const handleCreatePo = ()=>{
            const poData = {
                poNumber : poNumberForm.value,
                category : categoryForm.value,
                deliveryDate : deliveryDateForm.value,
                incotermId : incotermIdForm.value,
                customerId : customerIdForm.value
            }
            if(poNumberForm.value == '' || deliveryDateForm.value == null || incotermIdForm.value == null || customerIdForm.value == null ){
                    addPoError.value = 'Wypełnij wymagane pola: Nr zamówienia, Data dostawy, Klient.'
                    setTimeout(()=>{addPoError.value = null},4000)
                }
            else{
            addNewPO(poData).then(()=>{
                console.log(addPoError.value)
                if(addPoError.value == null){
                    poData['id'] = createdId.value
                    
                    // loadOrders(searchOrderPhrase.value, 20, 1, "DeliveryDate", 1)
                    loadOrder(createdId.value).then(()=>{
                        ordersList.value.push(newOrder.value)

                        poNumberForm.value = ''
                        deliveryDateForm.value = null
                        customerIdForm.value = null
                        categoryForm.value = 'Standard'
                        incotermIdForm.value = 1
                        addPoError.value = ''
                        customersError.value = ''
                        createdFlag.value = true
                        setTimeout(()=>{createdFlag.value = false},3000)
                    })
                }
                // else
                // {
                //     setTimeout(()=>{addPoError.value = null},3000)
                // }
            })
            }
        }
         const resetErrors = ()=>{
            addPoError.value = null,
            incotermsError.value = null,
            customersError.value = null
            
        }
    return {
        resetErrors,
        shipment,
        loadShipmentError,

      handleSearchOrders,
      handleAddOrder,
      handleRemoveOrder,
      loadOrdersError,
      totalItemsCountPo,
      orders,
      searchOrderPhrase,
      ordersList,
      handleSubmitOrdersList,

        handleCreatePo,
        moment,
        poNumberForm,
        categoryForm,
        deliveryDateForm,
        incotermIdForm,
        customerIdForm,
        incoterms,
        incotermsError,
        customers,
        customersError,
        addPoError,
        createdFlag
    }
  }
}

</script>

<style>
.btn{
    display: flex;
    justify-content: center;
    align-items: center;
    background: linear-gradient(to right bottom, #42c231,#07531e);
    border: none;
    color: #fff;
    padding: 0px 10px;
    height: 34px;
    margin: 0px;
    cursor: pointer;
    font-size: 12px;
    width: 100px;
    font-family: 'Poppins', sans-serif;
}
.form-set select.incoterms{
    width: 100%;
}
.btn-ctnr{
    padding: 3vh 0;
    display: flex;
    justify-content: center;
}
.choose-order-container {
    display: grid;
  grid-template-columns: 22vh 22vh;
  gap: 6vh;
}
.choose-order-container.non-symertic{
    grid-template-columns: 32vh 22vh;
}
#header-edit-shipment {
  display: flex;
  justify-content: center;
}
.edit-shipment-container {
  display: flex;
  justify-content: center;
}

.form-double-ctnr {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 2vh;
  align-items: end;
}
.form-big-double-ctnr{
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 2vh;
    
}

.chosen-one {
  display: flex;
  align-items: center;
  justify-content: space-between;
 
  margin: 0px 0px 10px 0;
  padding: 8px 10px 8px 24px;
  /* border-bottom: 1px solid #fff; */
  /* border: 1px solid #fff; */
  font-size: 0.8em;
  background-color: #3d496a;
  color:#ffffff
}
.chosen-one .remove-btn {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 16px;
  width: 16px;
  
  color: #ffffff;
  border-radius: 50%;
  cursor: pointer;
}
.chosen-one .remove-btn:hover {
  transform: scale(1.4);
  transition: ease-in-out 100ms;
  color: #ff0000;
}
.chosen-one .remove-btn span {
  font-size: 1.8em;
  font-weight: 300;
  pointer-events: none;
}
.search-ctnr {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}
.search-ctnr .search-button {
  margin: 0px;
}
.search-ctnr .search-input {
  height: 30px;
  margin-right: 2vh;
  padding: 2px 6px;
  width: 100%;
  margin-bottom: 0;
}

.forwarder-list-ctnr {
  min-height: 280px;
  max-height: 244px;
  overflow-y: auto;
  background: #fff;
  box-shadow: inset 0 0 0.9vh #00000080;
}
.forwarder-list-ctnr #no-result {
  padding: 10px 20px;
  color: #666;
  font-size: 0.9em;
}

.driver-line {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #fff;
  color: #333;
  padding: 4px 10px;
  font-size: 1.0vh;
  border-bottom: 1px solid #2e3649;
  cursor: pointer;
}
.driver-line:hover {
  color: #15ac24;
}
.driver-line p {
  padding: 0;
  margin: 0px 0px;
}
.add-item-btn{
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 4px 6px;
    font-size: 0.8vh;
    font-weight: 500;
    color: #505050;
    background-color: #e2e2e2;
    /* border-radius: 2px; */
    /* border:solid 1px #000000; */
    margin: 0;
}
.add-item-btn:hover{
    background-color: #3a901b;
    color:#ffffff;
    
    font-weight: 800;
}
.add-driver-btn {
  display: flex;
  justify-content: center;
  align-items: center;
  border-radius: 50%;
  
  /* pointer-events: none; */
}
.add-driver-btn:hover {
    transition: ease-in-out 100ms;
}
.form-add .red {
  color: rgb(228, 67, 52);
  font-size: 0.8em;
  font-weight: 400;
}
.add-shipment-ctnr {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: 40px;
}


.userSwitch {
  margin: 2px 0;
  position: relative;
  width: 94px;
  align-self: center;
}
.userSwitch input[type="checkbox"] {
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
.userSwitch-inner:before,
.userSwitch-inner:after {
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
  content: "NEW PARTS";
  padding-left: 10px;
  background-color: #fff;
  color: #000000;
  box-shadow: inset 2px 2px 4px rgba(0, 0, 0, 0.4);
}
.switches .flipswitch-inner:after {
  content: "";
}
.userSwitch-inner:after {
  content: "STANDARD";
  padding-right: 8px;
  background-color: #003e6a;
  color: #e8e8e8;
  text-align: right;
  box-shadow: inset 2px 2px 4px rgba(0, 0, 0, 0.4);
}
.userSwitch-switch {
  width: 24px;
  height: 24px;
  margin: 0px;
  background: #f5b727;
  box-shadow: 1px 1px 3px rgba(4, 4, 4, 0.4), -1px -1px 3px rgba(4, 4, 4, 0.4);
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

