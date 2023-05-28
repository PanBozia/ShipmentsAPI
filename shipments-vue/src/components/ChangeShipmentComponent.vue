<template>
    <div class="edit-shipment-screen">
    <div class="edit-shipment-ctnr" v-if="shipment">
            <h2>Edytuj dane wysyłki</h2>
        
        
            <div class="change-shipment-data-ctnr">
                <div>
                        <div>
                            <form class="form-add" @submit.prevent="handleSubmit">
                                <div class="choose-order-container">
                                    <div>
                                                        <div class="form-set">
                                                            <div>
                                                                <label class="form-labels"> Wysyłka priorytetowa</label>    
                                                            </div>
                                                            <div class="prio-ctnr">
                                                                <div class="userSwitch">
                                                                    <input type="checkbox" name="userSwitch" :checked="hasPriorityForm" class="userSwitch-cb"  :id="hasPriorityForm" v-model="hasPriorityForm">
                                                                    <label class="userSwitch-label" @click="handlePrio()">
                                                                    <div class="userSwitch-inner"></div>
                                                                    <div class="userSwitch-switch"></div>
                                                                    </label>
                                                                
                                                                </div>
                                                                <p><span class="material-symbols-outlined timer">timer</span> </p>
                                                            </div>
                                                        </div>

                                                        <div class="form-set">
                                                            <div>
                                                                <label class="form-labels">ETD</label>
                                                            </div>
                                                            <div class="double-in">
                                                                <input type="datetime-local" v-model="etdForm" required>
                                                            </div>
                                                        </div>
                                                    
                                                        <div class="form-set">
                                                            <div>
                                                                <label class="form-labels">Ilość palet: {{palletQtyForm}}</label>
                                                            </div>
                                                            <div class="double-in">
                                                                <input type="range" step="1" name="vol" min="0" max="100" v-model="palletQtyForm">
                                                            </div>

                                                        </div>
                                                        
                                                        <div class="form-set">
                                                                <div>
                                                                    <label class="form-labels">Typ kontenera</label>
                                                                </div>
                                                                <div class="double-in">
                                                                    <select v-model="containerTypeForm">
                                                                        <option value="N/A">N/A</option>
                                                                        <option value="20ft">20ft</option>
                                                                        <option value="40ft">40ft</option>
                                                                        <option value="40ft">40ft HQ</option>
                                                                    </select>
                                                                </div>
                                                        </div>
                                    </div>
                                    <div>
                                                        <div class="form-set">
                                                                <div>
                                                                    <label class="form-labels">Nr kontenera</label>
                                                                </div>
                                                                <div class="double-in">
                                                                    <input type="text" v-model="containerNumberForm">
                                                                </div>
                                                        </div>

                                                        <div class="form-set">
                                                                <div>
                                                                    <label class="form-labels">Nr plomby *</label>
                                                                </div>
                                                                <div class="double-in">
                                                                    <input type="text" v-model="containerSealNumberForm">
                                                                </div>
                                                        </div>
                                                            
                                                        <div class="form-set"> 
                                                            <div>
                                                                <label class="form-labels">Komentarz *</label>
                                                            </div>
                                                            <div class="comment-ctnr">
                                                                <textarea class="comment" name="comment" rows="4" cols="1" v-model="commentForm"></textarea>
                                                            </div>
                                                            <p class="form-labels" style="color: #ccc">* opcjonalnie</p>
                                                        </div>
                                                        
                                    </div>      
                                </div>
                                <!-- choose-order-ctnr -->
                                                        <div>
                                                            <div v-if="!createdFlag" id="add-btn-container">
                                                                <button>Zapisz</button>
                                                            </div>
                                                            
                                                            <div v-if="createdFlag" class="success">
                                                                <p>Dane wysyłki zostały zapisane.</p>
                                                            </div>
                                                        </div>
                            </form>
                        </div>
                        
                </div>
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

import getAreas from '../js-components/getAreas.js'
import getStatuses from '../js-components/getStatuses.js'
import getShipmentById from '../js-components/getShipmentById.js'
import editShipment from '../js-components/editShipment.js'
import { onBeforeMount, onMounted, ref } from 'vue'
import { useLinksStore } from '../stores/linksStore.js'
import moment from 'moment'
export default {
    emits:['changeShipmentDataEvent'],
    props:['shipmentId'],
    setup(props,context){
        const linksStore = useLinksStore()
        const etdForm = ref()
        const hasPriorityForm = ref()
        const palletQtyForm = ref()
        const containerNumberForm = ref()
        const containerTypeForm = ref()
        const containerSealNumberForm = ref()
        const commentForm = ref()
        
        const search_phrase = ref('')
        const createdFlag = ref(false)

        const { loadShipment, error:loadShipmentError, shipment} = getShipmentById(linksStore.url)
        const {edit, error:editError} = editShipment(linksStore.url,props.shipmentId)
        const { loadAreas, areas} = getAreas(linksStore.url)
        const {loadStatuses, statuses} = getStatuses(linksStore.url)
        onBeforeMount(()=>{
                loadStatuses(),
                loadAreas()
                loadShipment(props.shipmentId).then(()=>{
                    etdForm.value = shipment.value.etd
                    hasPriorityForm.value = shipment.value.hasPriority
                    palletQtyForm.value = shipment.value.palletQty
                    containerNumberForm.value = shipment.value.containerNumber
                    containerTypeForm.value = shipment.value.containerType
                    containerSealNumberForm.value = shipment.value.containerSealNumber
                    commentForm.value = shipment.value.comment
                })
            })
        onMounted(()=>{
            if(loadShipmentError.value){
                console.log(loadShipmentError.value)
                }
            createdFlag.value = false
            // loadAreas()
        })

        const handleSubmit = ()=>{
            const shipmentData = {
                etd :etdForm.value,
                timeOfDeparture : shipment.value.timeOfDeparture,
                hasPriority : hasPriorityForm.value,
                palletQty : palletQtyForm.value,
                containerNumber : containerNumberForm.value,
                containerSealNumber : containerSealNumberForm.value,
                containerType : containerTypeForm.value,
                comment : commentForm.value,
                warehouseAreaId : null,
                statusId: null,
                forwarderId: null,
                createdByUser: shipment.value.createdByUser
                }
                
                if(shipment.value.forwarder != null){
                    shipmentData['forwarderId'] = shipment.value.forwarder.id
                }
                statuses.value.forEach(status => {
                    if(shipment.value.status == status.name){
                        shipmentData['statusId'] = status.id
                    }
                });                
                areas.value.forEach(area => {
                    if(shipment.value.warehouseArea == area.name){
                        shipmentData['warehouseAreaId'] = area.id
                    }
                });
            
            edit(shipmentData).then(()=>{
                context.emit('changeShipmentDataEvent', true)
            })
                console.log(editError)
        }

        const handlePrio = ()=>{
            hasPriorityForm.value = !hasPriorityForm.value
        }
        const handleExit = ()=>{    
            context.emit('changeShipmentDataEvent', false)
        }
       
        return {
            search_phrase,
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
            handleExit,
            createdFlag,
            shipment
         }
    }

}
</script>

<style scoped>
.change-shipment-data-ctnr{
    display: flex;
    justify-content: center;
    
}
.edit-shipment-screen{
     position: absolute;
    width: 100%;
    height: 100%;
    background: #14141480;
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.edit-shipment-ctnr{
    display: block;
    width: 80vh;
    height: 54vh;
    margin:16vh auto 1vh auto;
    padding: 1.5vh 0vh;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
    

}
.edit-shipment-ctnr h2{
    padding: 0;
    margin: 1vh  0 3vh 0;
    text-align: center;
}

.timer{
    margin-left: 2vh;
    align-self: center;
    font-size: 3.5vh;
}

.double-in{
    display: grid;
}


.add-shipment-grid{
    display: grid;
    grid-template-columns: 1fr;
}
.double-ctnr-item{
    display: grid;
    grid-template-columns: 1fr 1fr ;
    gap: 3vh;
    justify-self: stretch;
}
.form-add{
    max-width: none;
}
.comment-ctnr{
    display: grid;
    justify-items: stretch; 
}
.comment{
    
    font-family: 'Poppins', sans-serif;
    font-weight: 400;
    font-size: 0.8em;
    padding: 8px 8px;
    
}

.userSwitch {
    margin: 2px 0;
    position: relative;
    width: 140px;
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
  content: "PRIORYTET";
  padding-left: 36px;
  background-color: #98483f;
  color: #fff;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
  font-weight: 400;
}
.switches .flipswitch-inner:after {
    content: "";

}
.userSwitch-inner:after {
    content: "STANDARD";
    padding-right: 36px;
    background-color: #6d9f27;
    color: #fff;
    text-align: right;
    box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
    font-weight: 400;
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
  right: 116px;
  transition: all 0.2s ease-in 0s;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-inner {
  margin-left: 0;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-switch {
  right: 0;
}



input[type=range] {
  -webkit-appearance: none; /* Hides the slider so that custom slider can be made */
  margin: 10px 0;
  padding: 0;
  width: 100%; /* Specific width is required for Firefox. */
  background: transparent; /* Otherwise white in Chrome */
  box-shadow: none;
}

input[type=range]::-webkit-slider-thumb {
  -webkit-appearance: none;
}

input[type=range]:focus {
  outline: none; /* Removes the blue border. You should probably do some kind of focus styling for accessibility reasons though. */
}

input[type=range]::-ms-track {
  width: 100%;
  cursor: pointer;

  /* Hides the slider so custom styles can be added */
  background: transparent; 
  border-color: transparent;
  color: transparent;
  box-shadow: none;
}

/* Special styling for WebKit/Blink */
input[type=range]::-webkit-slider-thumb {
  -webkit-appearance: none;
  box-shadow: 0 0 0.2vh #0d0d0d;
  height: 2.4vh;
  width: 2.4vh;
  border-radius: 50%;
  background: #ffffff;
  background: #F5B727;
  cursor: pointer;
  margin-top: 0.0vh; /* You need to specify a margin in Chrome, but in Firefox and IE it is automatic */
}

/* All the same stuff for Firefox */
input[type=range]::-moz-range-thumb {
  box-shadow: 0 0 0.6vh #0d0d0d;
  height: 2.4vh;
  width: 2.4vh;
  border-radius: 50%;
  background: #ffffff;
  cursor: pointer;
}

/* All the same stuff for IE */
input[type=range]::-ms-thumb {
  box-shadow: 0 0 0.6vh #0d0d0d;
  height: 2.4vh;
  width: 2.4vh;
  border-radius: 50%;
  background: #ffffff;
  cursor: pointer;
  
}
input[type=range]::-webkit-slider-runnable-track {
  width: 100%;
  height: 2.4vh;
  cursor: pointer;
  box-shadow:inset 0px 0px 6px rgba(0,0,0,0.3);
  /* background: #f5c400; */
    background: #6b75a0;
   
  border-radius: 20px;
  border: none;
    transition: 500ms ease-in-out;
}

input[type=range]:focus::-webkit-slider-runnable-track {
    /* background: #f5c400; */
    
    background: #fff;
    transition: 500ms ease-in-out;
}

input[type=range]::-moz-range-track {
  width: 100%;
  height: 8.4px;
  cursor: pointer;
  box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
  background: #1e2551;
  border-radius: 1.3px;
  border: 0.2px solid #010101;
}

input[type=range]::-ms-track {
  width: 100%;
  height: 8.4px;
  cursor: pointer;
  background: transparent;
  border-color: transparent;
  border-width: 16px 0;
  color: transparent;
}
input[type=range]::-ms-fill-lower {
  background: #2a6495;
  border: 0.2px solid #010101;
  border-radius: 2.6px;
  box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
}
input[type=range]:focus::-ms-fill-lower {
  background: #3071a9;
}
input[type=range]::-ms-fill-upper {
  background: #3071a9;
  border: 0.2px solid #010101;
  border-radius: 2.6px;
  box-shadow: 1px 1px 1px #000000, 0px 0px 1px #0d0d0d;
}
input[type=range]:focus::-ms-fill-upper {
  background: #367ebd;
}
</style>

