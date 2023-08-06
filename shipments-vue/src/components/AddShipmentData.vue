<template>
    <h2 class="order-h2">Dodaj nową wysyłkę</h2>
    
   <div>
        <form class="form-add" @submit.prevent="handleSubmit">
            <div class="choose-order-container">
                 <div>
                                    <div class="form-set">
                                        <div>
                                            <label class="form-labels">Nowe części</label>    
                                        </div>
                                        <div class="prio-ctnr">
                                            <div class="userSwitch">
                                                <input type="checkbox" name="userSwitch" :checked="hasPriorityForm" class="userSwitch-cb"  :id="hasPriorityForm" v-model="hasPriorityForm">
                                                <label class="userSwitch-label" @click="handlePrio()">
                                                <div class="userSwitch-inner"></div>
                                                <div class="userSwitch-switch"></div>
                                                </label>
                                            
                                            </div>
                                            <p><span class="material-symbols-outlined timer">new_label</span> </p>
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
                                            <label class="form-labels">Lokacja</label>
                                        </div>
                                        <div class="double-in">
                                            <select  v-if="!areasError" v-model="warehouseAreaIdForm">
                                                <option :value="area.id" v-for="area in areas" :key="area.id">
                                                    {{area.name}}
                                                </option>
                                            </select>
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
                                                <label class="form-labels">Nr plomby</label>
                                            </div>
                                            <div class="double-in">
                                                <input type="text" v-model="containerSealNumberForm">
                                            </div>
                                    </div>
                                        
                                    <div class="form-set"> 
                                        <div>
                                            <label class="form-labels">Komentarz</label>
                                        </div>
                                        <div class="comment-ctnr">
                                            <textarea class="comment" name="comment" rows="4" cols="1" v-model="commentForm"></textarea>
                                        </div>
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
</template>

<script>

import getAreas from '../js-components/getAreas.js'
import { onMounted, ref } from 'vue'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'
export default {
    emits:["new-shipment-event"],
    setup(props,context){
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
        const search_phrase = ref('')
        const createdFlag = ref(false)

        
        const { loadAreas, error:areasError, areas} = getAreas(linksStore.url)
        onMounted(()=>{
            createdFlag.value = false
            loadAreas()
            etdForm.value = moment(new Date()).format("YYYY-MM-DD HH:mm")
        })

        const handleSubmit = ()=>{
            const shipmentData = {
                etd : etdForm.value,
                timeOfDeparture : null,
                hasPriority : hasPriorityForm.value,
                palletQty : palletQtyForm.value,
                containerNumber : containerNumberForm.value,
                containerSealNumber : containerSealNumberForm.value,
                containerType : containerTypeForm.value,
                comment : commentForm.value
                }
            if(warehouseAreaIdForm.value != null){
                shipmentData['warehouseAreaId'] = warehouseAreaIdForm.value
            }
            context.emit('new-shipment-event', shipmentData)
        
        // addNewShipment(shipmentData).then(()=>{
        //         if(addShipmentError.value == null){
        //             etdForm.value = null
        //             hasPriorityForm.value = false
        //             palletQtyForm.value = 0
        //             containerNumberForm.value = ''
        //             containerSealNumberForm.value =''
        //             containerTypeForm.value = '' 
        //             commentForm.value = ''
        //             warehouseAreaIdForm.value = null
        //             forwarderIdForm.value = null
        //             createdFlag.value = true
        //             // setTimeout(()=>{createdFlag.value = false},5000)
        //         }
        //     })
        }

        const handlePrio = ()=>{
            hasPriorityForm.value = !hasPriorityForm.value
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
            warehouseAreaIdForm,
            forwarderIdForm,
            createdFlag,
            areasError,
            areas
         }
    }

}
</script>

<style scoped>

.timer{
    margin-left: 2vh;
    align-self: center;
    font-size: 3.5vh;
}

.double-in{
    display: grid;
}
.order-h2{
    margin-top:2vh;
    padding: 0;
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
  content: "NEW PARTS";
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

