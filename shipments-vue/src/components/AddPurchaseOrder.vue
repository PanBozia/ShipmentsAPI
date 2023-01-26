<template>
   <div class="add-container">
        <form class="form-add" @submit.prevent="handleSubmit">
            <p>Dodaj nowe zamówienie</p>
            <label>Numer zamówienia</label>
            <input type="text" v-model="poNumberForm" required>
            <label>Kategoria</label>
            <select v-model="categoryForm">
                <option value="Sample">Sample</option>
                <option value="Standard">Standard</option>
                <option value="Inne">Inne</option>
            </select>
            
            <label>Data dostawy</label>
            <input type="datetime-local" v-model="deliveryDateForm" required>
            <label>Incoterms</label>
            <select v-if="!incotermsError" v-model="incotermIdForm">
                <option v-for="inco in incoterms" :key="inco.id" :value="inco.id">{{inco.shortName}} - {{inco.name}}</option>
            </select>
            <label>Klient</label>
            <select v-if="!customersError" v-model="customerIdForm">
                <option v-for="customer in customers" :key="customer.id" :value="customer.id">{{customer.shortName}} ({{customer.cityAddress}} - {{customer.countryAddress}})</option>
            </select>
            <div id="add-btn-container">
                <button>Zapisz</button>
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
import { onMounted, ref } from 'vue'
import getIncoterms from '../js-components/getIncoterms.js'
import getAllCustomers from '../js-components/getAllCustomers.js'
import addPurchaseOrder from '../js-components/addPurchaseOrder.js'
import moment from 'moment'
export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const poNumberForm = ref('')
        const categoryForm = ref('Standard')
        const deliveryDateForm = ref()
        const incotermIdForm = ref(1)
        const customerIdForm = ref()
        const createdFlag = ref(false)

        const {loadIncoterms, incoterms, error:incotermsError} = getIncoterms(url)
        const {loadAllCustomers, customers, error:customersError} = getAllCustomers(url)
        const {addNewPO, error:addPoError} = addPurchaseOrder(url)
        
        onMounted(()=>{
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
                    createdFlag.value = true
                    setTimeout(()=>{createdFlag.value = false},5000)
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

</style>