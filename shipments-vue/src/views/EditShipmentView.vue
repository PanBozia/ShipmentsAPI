<template>
    <NavbarComponent />
    <div v-if="!loadShipmentError">
        
    

    </div>
    <h1 id="header-edit-shipment">Edytuj dane wysyłki</h1>
    <div v-if="!loadShipmentError" class="edit-shipment-container">
        
            <div class="add-container add-shipment-ctnr">

 <form class="form-add" @submit.prevent="handleSearchOrders()">
                        <label class="form-labels">Zamówienia</label>
                        <div  v-if="shipment.purchaseOrders != 0">
                            <div class="chosen-one" v-for="po in shipment.purchaseOrders" :key="po.id">
                                <div>
                                    {{po.poNumber + " - " +po.customerShortName}}
                                </div>
                                <div class="remove-btn" @click="handleRemoveOrder(po.id)">
                                    <span class="material-symbols-outlined">
                                        remove_circle
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="chosen-one red" v-else>
                            <div >Brak zamówień</div>
                        </div >

                        <label class="form-labels">Wybierz zamówienia</label>
                        <div class="search-ctnr">
                            <input class="search-input" type="text" v-model="searchOrderPhrase">
                            <button class="search-button" >Szukaj</button>
                        </div>
                        <div v-if="orders != null">
                                <div class="forwarder-list-ctnr">
                                    <div v-if="orders.length == 0">
                                        <p id="no-result">Brak wyników wyszukiwania...</p>
                                    </div>
                                    <div v-else class="driver-line" v-for="order in orders" :key="order.id">
                                        <p>{{order.poNumber+ " - " +order.customerShortName}}</p>
                                        <div class="add-driver-btn" @click="handleAddOrder(order.id)">
                                            <span class="material-symbols-outlined">
                                                add_circle
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="totalItemsCountPo > 20">
                                    <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
                                </div>
                        </div>

                       
                    </form>



                    <form class="form-add" @submit.prevent="handleSubmit">
                        
                        <div class="form-double-ctnr">
                            <div class="double-ctnr-item">
                                <label class="form-labels">ETD</label>
                                <input type="datetime-local" v-model="etdForm" required>
                            </div>
                            <div class="double-ctnr-item">
                                <label class="form-labels"> Nowe części</label>    
                                <div class="prio-ctnr">
                                    <p><span class="material-symbols-outlined timer">fiber_new</span> </p>
                                    <div class="userSwitch">
                                        <input type="checkbox" name="userSwitch" :checked="hasPriorityForm" class="userSwitch-cb"  :id="hasPriorityForm" v-model="hasPriorityForm">
                                        <label class="userSwitch-label" @click="handlePrio()">
                                        <div class="userSwitch-inner"></div>
                                        <div class="userSwitch-switch"></div>
                                        </label>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <label class="form-labels">Ilość palet: {{palletQtyForm}}</label>
                        <input type="range" step="1" name="vol" min="0" max="100" v-model="palletQtyForm">
                        <div class="form-double-ctnr">
                            <div class="double-ctnr-item">
                                <label class="form-labels">Obszar magazynowy</label>
                                <select v-if="!areasError" v-model="warehouseAreaIdForm">
                                    <option :value="area.id" v-for="area in areas" :key="area.id">
                                        {{area.name}}
                                    </option>
                                </select>
                            </div>
                            <div class="double-ctnr-item">
                                <label class="form-labels">Typ kontenera</label>
                                <select v-model="containerTypeForm">
                                    <option value="N/A">N/A</option>
                                    <option value="20ft">20ft</option>
                                    <option value="40ft">40ft</option>
                                    <option value="40ft">40ft HQ</option>
                                </select>
                            </div>
                        </div>
                        <label class="form-labels">Nr kontenera (opcjonalnie)</label>
                        <input type="text" v-model="containerNumberForm">
                        <label class="form-labels">Nr plomby (opcjonalnie)</label>
                        <input type="text" v-model="containerSealNumberForm">
                        <label class="form-labels">Komentarz (opcjonalnie)</label>
                        <!-- <input type="text-area" class="comment" v-model="commentForm"> -->
                        <textarea class="comment" name="comment" rows="5" cols="1" v-model="commentForm">
                        </textarea>
                        <label class="form-labels">Status</label>
                        <select v-if="!getStatusesError" v-model="statusIdForm">
                            <option :value="status.id" v-for="status in statuses" :key="status.id">
                                        {{status.name}}
                            </option>
                        </select>
                        <div v-if="statusIdForm">
                            <div class="form-double-ctnr">
                                <div class="double-ctnr-item">
                                    <label class="form-labels">Data wysyłki</label>
                                    <input type="datetime-local" v-model="timeOfDepartureForm" required>
                                </div>
                                <div class="double-ctnr-item">
                                    
                                </div>
                            </div>
                        </div>
                        
                        <div id="add-btn-container">
                            <button>Zapisz</button>
                        </div>
                        <div v-if="loadShipmentError || forwardersError" class="error" >
                            <p>{{loadShipmentError}}</p>
                            <p>{{forwardersError}}</p>
                        </div>
                        <div v-if="createdFlag" class="success">
                            <p>Dane wysyłki zostały zapisane.</p>
                        </div>
                    </form>



                    <form class="form-add" @submit.prevent="handleSearchForwarder()">
                        <label class="form-labels">Aktualny kierowca</label>
                        <div class="chosen-one" v-if="newForwarder">
                            <div  >{{newForwarder.lastName + ' ' + newForwarder.firstName + ' - ' + newForwarder.carPlates}}</div>
                            <div class="remove-btn" @click="handleRemoveDriver()">
                                <span class="material-symbols-outlined">
                                    remove_circle
                                </span>
                            </div>
                        </div>
                        <div class="chosen-one red" v-else>
                            <div >Brak kierowcy</div>
                        </div >

                        <label class="form-labels">Wybierz nowego kierowcę</label>
                        <div class="search-ctnr">
                            <input type="text" v-model="search_phrase">
                            <button>Szukaj</button>
                        </div>
                        <div v-if="forwarders != null">
                                <div class="forwarder-list-ctnr">
                                    <div v-if="forwarders.length == 0">
                                        <p id="no-result">Brak wyników wyszukiwania...</p>
                                    </div>
                                    <div v-else class="driver-line" v-for="forwarder in forwarders" :key="forwarder.id">
                                        <p>{{forwarder.lastName + ' ' + forwarder.firstName + ' - ' + forwarder.carPlates}}</p>
                                        <div class="add-driver-btn" @click="handleChoise(forwarder)">
                                            <span class="material-symbols-outlined">
                                                add_circle
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div v-if="totalItemsCountForwarders > 20">
                                    <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
                                </div>
                        </div>
                    </form>

                   
                </div>
    </div>
</template>

<script>
import NavbarComponent from '../components/NavbarComponent.vue'
import getShipmentById from '../js-components/getShipmentById.js'
import editShipment from '../js-components/editShipment.js'
import getAreas from '../js-components/getAreas.js'
import getForwarders from '../js-components/getForwarders.js'
import getForwarderById from '../js-components/getForwarderById.js'
import getStatuses from '../js-components/getStatuses.js'
import getPurchaseOrders from '../js-components/getPurchaseOrders.js'
import addOrderToShipment from '../js-components/addOrderToShipment.js'
import removeOrderFromShipment from '../js-components/removeOrderFromShipment.js'
import { onBeforeMount, ref } from 'vue'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'

export default {
    props:['shipmentId'],
    components:{NavbarComponent},
    setup(props){
        const linksStore = useLinksStore()
        const etdForm = ref(null)
        const hasPriorityForm = ref(false)
        const palletQtyForm = ref(0)
        const containerNumberForm = ref('N/A')
        const containerTypeForm = ref('20ft')
        const containerSealNumberForm = ref('N/A')
        const commentForm = ref('')
        const warehouseAreaIdForm = ref(null)
        const forwarderIdForm = ref(null)
        const statusIdForm = ref(null)
        const timeOfDepartureForm = ref(null)
        const newForwarder = ref(null)
        const search_phrase = ref('')
        const searchOrderPhrase = ref('')
        const createdFlag = ref(false)

        const {edit, error:editShipmentError} = editShipment(linksStore.url,props.shipmentId)
        const {loadShipment, error:loadShipmentError, shipment} = getShipmentById(linksStore.url)
        const {loadAreas, error:areasError, areas} = getAreas(linksStore.url)
        const {loadForwarders, error:forwardersError, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCount:totalItemsCountForwarders} = getForwarders(linksStore.url)
        const {loadForwarder, error:getForwarderByIdError, forwarder} = getForwarderById(linksStore.url)
        const {loadStatuses, error:getStatusesError, statuses} = getStatuses(linksStore.url)
        const {loadOrders, error:loadOrdersError, orders, totalItemsCount:totalItemsCountPo} = getPurchaseOrders(linksStore.url)
        const {addOrder, error:addOrderError} = addOrderToShipment(linksStore.url)
        const {removeOrder, error:removeOrderError} = removeOrderFromShipment(linksStore.url)
        
        const refreshShipmentData = () =>{
            
            loadShipment(props.shipmentId).then(()=>{
                etdForm.value = moment(shipment.value.etd).format("YYYY-MM-DD HH:mm")
                timeOfDepartureForm.value = moment(shipment.value.timeOfDeparture).format("YYYY-MM-DD HH:mm")
                console.log(moment(shipment.value.etd).format("DD.MM.YYYY HH:mm"))
                hasPriorityForm.value = shipment.value.hasPriority
                palletQtyForm.value = shipment.value.palletQty
                containerNumberForm.value = shipment.value.containerNumber
                containerTypeForm.value = shipment.value.containerType
                containerSealNumberForm.value = shipment.value.containerSealNumber
                commentForm.value = shipment.value.comment
                loadAreas().then(()=>{
                    if(shipment.value.warehouseArea != null){
                        warehouseAreaIdForm.value = areas.value.find(x =>x.name === shipment.value.warehouseArea).id
                    }else{
                        warehouseAreaIdForm.value = null
                    }
                    if(shipment.value.forwarder != null){
                        forwarderIdForm.value = shipment.value.forwarder.id
                        loadForwarder(forwarderIdForm.value).then(()=>{
                            newForwarder.value = forwarder.value
                        })
                    }
                })
                loadStatuses().then(()=>{
                    statusIdForm.value = statuses.value.find(x => x.name === shipment.value.status).id
                })


            })
        }
        // onMounted(()=>{
            //    refreshShipmentData()
        // })
        onBeforeMount(()=>{
            refreshShipmentData()
        })

        const handleSearchOrders = () =>{
            loadOrders(searchOrderPhrase.value,20,1,'DeliveryDate',1)
        }
        const handleAddOrder = (orderId) =>{
            addOrder(props.shipmentId, orderId).then(()=>{
                refreshShipmentData()
            })
        }
        const handleRemoveOrder = (orderId) =>{
            removeOrder(props.shipmentId, orderId).then(()=>{
                refreshShipmentData()
            })
        }

        const handleSubmit = ()=>{
            const shipmentData = {
                etd : moment(etdForm.value).format("YYYY-MM-DDTHH:mm"),
                timeOfDeparture : moment(timeOfDepartureForm.value).format("YYYY-MM-DDTHH:mm"),
                statusId : statusIdForm.value,
                // timeOfDeparture to rzeczywisty czas wysyłki, jeżeli status inny niż "wysłane", to przypisz wartość etdForm.value.
                // jeżeli status to "wysłane", to pojawi się pole z opcją wpisania timeOfDeprture (chwilowo etd=td powyżej)
                hasPriority : hasPriorityForm.value,
                palletQty : palletQtyForm.value,
                containerNumber : containerNumberForm.value,
                containerSealNumber : containerSealNumberForm.value,
                containerType : containerTypeForm.value,
                comment : commentForm.value
                }
            //console.log(warehouseAreaIdForm.value)
            if(warehouseAreaIdForm.value != null){
                shipmentData['warehouseAreaId'] = warehouseAreaIdForm.value
            }else{
                shipmentData['warehouseAreaId'] = null
            }
            
            if(newForwarder.value != null){
                shipmentData['forwarderId'] = newForwarder.value['id']
            }
  
        
        edit(shipmentData).then(()=>{
                if(editShipmentError.value == null){
                    
                    createdFlag.value = true
                    setTimeout(()=>{createdFlag.value = false},5000)
                }
            })
        }
        const handlePrio = ()=>{
            hasPriorityForm.value = !hasPriorityForm.value
        }

        const handleSearchForwarder = ()=>{
            loadForwarders(search_phrase.value,20,1,'',0)
        }
        const handleChoise = (chosenForwarder)=>{
            newForwarder.value = chosenForwarder
        }
        const handleRemoveDriver = ()=>{
            newForwarder.value = null
        }

        //  const shipmentWatcher = watch((shipment), () => {
        //     refreshShipmentData()
        // })
        // onUnmounted (()=>{
        //     shipmentWatcher(); //invoking the method ends watching
        // })
       
        return {
            handleRemoveOrder,
            removeOrderError,
            addOrderError,
            handleAddOrder,
            handleSearchOrders,
            orders, totalItemsCountPo,loadOrdersError,
            searchOrderPhrase,
            shipment,
            loadShipmentError,
            handleChoise,
            handleRemoveDriver,
            getForwarderByIdError, 
            statuses,
            statusIdForm,
            getStatusesError,
            forwardersError, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCountForwarders,
            search_phrase,
            handleSearchForwarder,
            newForwarder,
            handlePrio,
            handleSubmit,
            moment,
            etdForm,
            timeOfDepartureForm,
            hasPriorityForm,
            palletQtyForm,
            containerNumberForm,
            containerTypeForm,
            containerSealNumberForm,
            commentForm,
            warehouseAreaIdForm,
            forwarderIdForm,
            editShipmentError,
            createdFlag,
            areasError,
            areas
         }
    }

}
</script>



<style scoped>
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

.form-labels{
    font-size: 0.9em;
    margin: 14px 0 4px 0;
    
}
.form-add{
    margin:14px 0;
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
    min-width: 280;
    height: 34px;
    margin: 0px 0px 20px 0;
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
  content: "NEW PARTS";
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

