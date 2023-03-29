<template>
    <h2>Dodaj nową wysyłkę</h2>
    
   <div >
        <form class="form-add" @submit.prevent="handleSubmit">
            <div class="add-shipment-grid">
                <div>
                    <div class="double-ctnr-item">
                        <div>
                            <div>
                                <label class="form-labels">ETD</label>
                            </div>
                            <div>
                                <input type="datetime-local" v-model="etdForm" required>
                            </div>
                        </div>
                        <div>
                            <div>
                                <label class="form-labels"> Wysyłka priorytetowa</label>    
                            </div>
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

                    <div>
                        <div>
                            <label class="form-labels">Ilość palet: {{palletQtyForm}}</label>
                        </div>
                        <div>
                            <input type="range" step="1" name="vol" min="0" max="100" v-model="palletQtyForm">
                        </div>
                        <div class="form-double-ctnr">
                            <div >
                                <div>
                                    <div>
                                        <label class="form-labels">Lokacja</label>
                                    </div>
                                    <div>
                                        <select v-if="!areasError" v-model="warehouseAreaIdForm">
                                            <option :value="area.id" v-for="area in areas" :key="area.id">
                                                {{area.name}}
                                            </option>
                                        </select>
                                    </div>
                                </div>

                            </div>
                            <div>
                                <div>
                                    <div>
                                        <label class="form-labels">Typ kontenera</label>
                                    </div>
                                    <div>
                                        <select v-model="containerTypeForm">
                                            <option value="N/A">N/A</option>
                                            <option value="20ft">20ft</option>
                                            <option value="40ft">40ft</option>
                                            <option value="40ft">40ft HQ</option>
                                        </select>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div>
                    <div>
                        <div class="form-double-ctnr">
                            <div>
                                <div>
                                    <label class="form-labels">Nr kontenera</label>
                                </div>
                                <div>
                                    <input type="text" v-model="containerNumberForm">
                                </div>
                            </div>
                            <div>
                                <div>
                                    <label class="form-labels">Nr plomby</label>
                                </div>
                                <div>
                                    <input type="text" v-model="containerSealNumberForm">
                                </div>
                            </div>
                        </div>
                        <div>
                            <div>
                                <label class="form-labels">Komentarz</label>
                            </div>
                            <div class="comment-ctnr">
                                <textarea class="comment" name="comment" rows="4" cols="1" v-model="commentForm"></textarea>
                            </div>
                        </div>
                    </div>
                    

                </div>


            </div>
            <div v-if="!createdFlag" id="add-btn-container">
                <button>Zapisz</button>
            </div>
            <div v-if="addShipmentError" class="error" >
                <p>{{addShipmentError}}</p>
            </div>
            <div v-if="createdFlag" class="success">
                <p>Dane wysyłki zostały zapisane.</p>
            </div>
        </form>
    </div>

</template>

<script>
import addShipment from '../js-components/addShipment.js'
import getAreas from '../js-components/getAreas.js'
import { onMounted, ref } from 'vue'
import moment from 'moment'
export default {
    emits:["new-shipment-event"],
    setup(props,context){
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
        const search_phrase = ref('')
        const createdFlag = ref(false)

        const { addNewShipment, error:addShipmentError, createdId} = addShipment(url)
        const { loadAreas, error:areasError, areas} = getAreas(url)
        onMounted(()=>{
            createdFlag.value = false
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
            if(warehouseAreaIdForm.value != null){
                shipmentData['warehouseAreaId'] = warehouseAreaIdForm.value
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
                    createdFlag.value = true
                    // setTimeout(()=>{createdFlag.value = false},5000)
                    context.emit('new-shipment-event', createdId.value)
                }
            })
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
            addShipmentError,
            createdFlag,
            areasError,
            areas
         }
    }

}
</script>

<style scoped>
input[type="range" ]{
    width: 100%;
    padding: 0;
}
input[type="text" ]{
    width: 100%;
    padding: 0;
}
select{
    width: 100%;
    padding:0 0px;
}
.add-shipment-grid{
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 5vh;
    
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
}
.comment{
    height: 100%;
    font-family: 'Poppins', sans-serif;
    font-weight: 400;
    font-size: 0.8em;
    padding: 8px 8px;
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

