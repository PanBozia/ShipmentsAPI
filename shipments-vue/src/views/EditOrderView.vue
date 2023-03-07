<template>
    <NavbarComponent/>
    
  <div class="add-container">
        <form class="form-add" @submit.prevent="handleSubmit">
            <p>Edytuj dane zamówienia</p>
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
            <div v-if="!editOrderError" id="add-btn-container">
                <button>Zapisz</button>
            </div>
            <div v-if="loadOrderError || editOrderError || customersError || incotermsError" class="error" >
                <p>{{loadOrderError}}</p>
                <p>{{customersError}}</p>
                <p>{{incotermsError}}</p>
                <p>{{editOrderError}}</p>
            </div>
            <div v-if="createdFlag" class="success">
                <p>Zamówienie zostało zapisane.</p>
            </div>
            
        </form>
    </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import getPurchaseOrderById from '../js-components/getPurchaseOrderById.js'
import NavbarComponent from '../components/NavbarComponent.vue'
import getIncoterms from '../js-components/getIncoterms.js'
import getAllCustomers from '../js-components/getAllCustomers.js'
import editPurchaseOrder from '../js-components/editPurchaseOrder.js'
import moment from 'moment'
export default {
    props:['orderId'],
    components:{NavbarComponent},
    setup(props){
        const url = 'https://localhost:44331/api/'
        const {loadOrder, error:loadOrderError, order} = getPurchaseOrderById(url)
        const poNumberForm = ref('')
        const categoryForm = ref('')
        const deliveryDateForm = ref()
        const incotermIdForm = ref(1)
        const customerIdForm = ref()
        const createdFlag = ref(false)
        const {loadIncoterms, incoterms, error:incotermsError} = getIncoterms(url)
        const {loadAllCustomers, customers, error:customersError} = getAllCustomers(url)
        const {editOrder, error:editOrderError} = editPurchaseOrder(url)
       
        onBeforeMount(()=>{
            loadOrder(props.orderId).then(()=>{
                loadIncoterms().then(()=>{
                    let incoId = incoterms.value.find(x => x.shortName == order.value.incotermName).id
                    incotermIdForm.value = incoId
                    
                })
                loadAllCustomers().then(()=>{
                    let customerId = customers.value.find(x => x.name == order.value.customerName).id
                    customerIdForm.value = customerId
                });
                poNumberForm.value = order.value.poNumber
                categoryForm.value = order.value.category
                deliveryDateForm.value = moment(order.value.deliveryDate).format("YYYY-MM-DDTHH:mm")
            })
        })

         const handleSubmit = ()=>{
            const poData = {
                poNumber : poNumberForm.value,
                category : categoryForm.value,
                deliveryDate : deliveryDateForm.value,
                incotermId : incotermIdForm.value,
                customerId : customerIdForm.value
            }
            editOrder(props.orderId, poData).then(()=>{
                if(editOrderError.value == null){
                    createdFlag.value = true
                    setTimeout(()=>{createdFlag.value = false},5000)
                }else{
                    setTimeout(()=>{editOrderError.value = null},4000)
                }
            })
           
        }

        return{
            handleSubmit,
            loadOrderError,
            order,
            poNumberForm, categoryForm, deliveryDateForm, incotermIdForm, customerIdForm, 
            createdFlag,
            incoterms, incotermsError,
            customers, customersError,
            editOrderError,
        }
    }
}
</script>

<style>

</style>