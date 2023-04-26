<template>

   <div class="add-container">
        <form class="form-add add-single-form" @submit.prevent="handleSubmit">
            <h2>Dodaj nowe zamówienie</h2>
            <div class="form-set">
                <label class="form-labels">Numer zamówienia</label>
                <input type="text" v-model="poNumberForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Kategoria</label>
                <select v-model="categoryForm">
                    <option class="option-para" value="Sample">Sample</option>
                    <option class="option-para" value="Standard">Standard</option>
                    <option class="option-para" value="Inne">Inne</option>
                </select>
            </div>
            <div class="form-set">
                <label class="form-labels">Data dostawy</label>
                <input type="datetime-local" v-model="deliveryDateForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Incoterms</label>
                <select class="incoterms" v-if="!incotermsError" v-model="incotermIdForm">
                    <option class="option-para" v-for="inco in incoterms" :key="inco.id" :value="inco.id">{{inco.shortName}} - {{inco.name}}</option>
                </select>
            </div>
            <div class="form-set">
                <label class="form-labels">Klient</label>
                <select v-if="!customersError" v-model="customerIdForm">
                    <option class="option-para" v-for="customer in customers" :key="customer.id" :value="customer.id">{{customer.shortName}} ({{customer.cityAddress}} - {{customer.countryAddress}})</option>
                </select>
            </div>
            <div v-if="!addPoError" id="add-btn-container">
                <button>Dodaj</button>
            </div>
            <div v-if="addPoError || customersError || incotermsError" class="error" >
                <p>{{addPoError}}</p>
                <p>{{customersError}}</p>
                <p>{{incotermsError}}</p>
            </div>
            <div v-if="createdFlag" class="success">
                <p>Zamówienie zostało zapisane.</p>
            </div>
        </form>
    </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import getIncoterms from '../js-components/getIncoterms.js'
import getAllCustomers from '../js-components/getAllCustomers.js'
import addPurchaseOrder from '../js-components/addPurchaseOrder.js'
import { useLinksStore } from '../stores/linksStore.js'
import moment from 'moment'
export default {
    emits:['order-added-event'],
    setup(props, context){
        const linksStore = useLinksStore()
        const poNumberForm = ref('')
        const categoryForm = ref('Standard')
        const deliveryDateForm = ref()
        const incotermIdForm = ref(1)
        const customerIdForm = ref()
        const createdFlag = ref(false)

        const {loadIncoterms, incoterms, error:incotermsError} = getIncoterms(linksStore.url)
        const {loadAllCustomers, customers, error:customersError} = getAllCustomers(linksStore.url)
        const {addNewPO, error:addPoError} = addPurchaseOrder(linksStore.url)
        
        onBeforeMount (()=>{
            loadIncoterms();
            loadAllCustomers();
        })

        const handleSubmit = ()=>{
            const poData = {
                poNumber : poNumberForm.value,
                category : categoryForm.value,
                deliveryDate : deliveryDateForm.value,
                incotermId : incotermIdForm.value,
                customerId : customerIdForm.value
            }
            addNewPO(poData).then(()=>{
                if(addPoError.value == null){
                    poNumberForm.value = ''
                    deliveryDateForm.value = null
                    customerIdForm.value = null
                    categoryForm.value = 'Standard'
                    incotermIdForm.value = 1
                    addPoError.value = ''
                    customersError.value = ''
                    createdFlag.value = true
                    setTimeout(()=>{createdFlag.value = false},5000)
                }else{
                    setTimeout(()=>{addPoError.value = null},4000)
                }
                if(createdFlag.value){
                    context.emit('order-added-event',  poData.poNumber)
                }
            })
           
        }
       
        return {
            handleSubmit,
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
.option-para{
    font-size: 1.1vh;
}

</style>