<template>
            <p>Dodaj nową wysyłkę</p>
   <div class="add-container add-shipment-ctnr">
        <form class="form-add" @submit.prevent="handleSubmit">
            
            <div class="form-double-ctnr">
                <div class="double-ctnr-item">
                    <label class="form-labels">ETD</label>
                    <input type="datetime-local" v-model="etdForm" required>
                </div>
                <div class="double-ctnr-item">
                    <label class="form-labels"> Wysyłka priorytetowa</label>    
                    <div class="prio-ctnr">
                        <p><span class="material-symbols-outlined timer">timer</span> </p>
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
            <div id="add-btn-container">
                <button>Zapisz</button>
            </div>
            <div v-if="addShipmentError || forwardersError" class="error" >
                <p>{{addShipmentError}}</p>
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
                    <div v-if="totalItemsCount > 20">
                        <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
                    </div>
            </div>
        </form>
    </div>
</template>

<script>
import addShipment from '../js-components/addShipment.js'
import getAreas from '../js-components/getAreas.js'
import getForwarders from '../js-components/getForwarders.js'
import { onMounted, ref } from 'vue'
import moment from 'moment'
export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const etdForm = ref(null)
        const hasPriorityForm = ref(false)
        const palletQtyForm = ref(0)
        const containerNumberForm = ref('N/A')
        const containerTypeForm = ref('20ft')
        const containerSealNumberForm = ref('N/A')
        const commentForm = ref('')
        const warehouseAreaIdForm = ref(null)
        const forwarderIdForm = ref(null)
        const newForwarder = ref(null)
        const search_phrase = ref('')
        const createdFlag = ref(false)

        const { addNewShipment, error:addShipmentError} = addShipment(url)
        const { loadAreas, error:areasError, areas} = getAreas(url)
        const {loadForwarders, error:forwardersError, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCount} = getForwarders(url)
        onMounted(()=>{
            loadAreas()
        })

        const handleSubmit = ()=>{
            const shipmentData = {
                etd : etdForm.value,
                timeOfDeparture : etdForm.value,
                hasPriority : hasPriorityForm.value,
                palletQty : palletQtyForm.value,
                containerNumber : containerNumberForm.value,
                containerSealNumber : containerSealNumberForm.value,
                containerType : containerTypeForm.value,
                comment : commentForm.value
                }
            console.log(warehouseAreaIdForm.value)
            if(warehouseAreaIdForm.value != null){
                shipmentData['warehouseAreaId'] = warehouseAreaIdForm.value
            }
            if(newForwarder.value != null){
                shipmentData['forwarderId'] = newForwarder.value['id']
            }
  
        
        addNewShipment(shipmentData).then(()=>{
                if(addShipmentError.value == null){
                    etdForm.value = null
                    hasPriorityForm.value = false
                    palletQtyForm.value = 0
                    containerNumberForm.value = ''
                    containerSealNumberForm.value =''
                    containerTypeForm.value = '' 
                    commentForm.value = ''
                    warehouseAreaIdForm.value = null
                    forwarderIdForm.value = null
                    handleRemoveDriver()
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
        const handleChoise = (forwarder)=>{
            newForwarder.value = forwarder
        }
        const handleRemoveDriver = ()=>{
            newForwarder.value = null
        }
       
        return {
            handleChoise,
            handleRemoveDriver,
            forwardersError, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCount,
            search_phrase,
            handleSearchForwarder,
            newForwarder,
            handlePrio,
            handleSubmit,
            moment,
            etdForm,
            hasPriorityForm,
            palletQtyForm,
            containerNumberForm,
            containerTypeForm,
            containerSealNumberForm,
            commentForm,
            warehouseAreaIdForm,
            forwarderIdForm,
            addShipmentError,
            createdFlag,
            areasError,
            areas
         }
    }

}
</script>

<style scoped>
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
.search-ctnr{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 6px;
}
.search-ctnr button{
    height: 39px;
    margin: 2px 0px 20px 0;
    padding: 2px 10px;
}
.search-ctnr input{
    /* height: 34px; */
    min-width: 260px;
    margin: 2px 0px 20px 0;
    padding: 2px 10px;
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
    grid-template-columns: 1fr 1fr ;
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

