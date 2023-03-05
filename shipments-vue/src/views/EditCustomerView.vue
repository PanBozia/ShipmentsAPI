<template>
   <NavbarComponent />
  <div>
    
    <div v-if="customer">
        <div class="add-container">
            <form class="form-add" @submit.prevent="handleSubmit">
                <p>Edytuj dane klienta</p>
                <label>Nazwa (skrót)</label>
                <input type="text" v-model="shortNameForm" required>
                <label>Pełna nazwa</label>
                <input type="text" v-model="nameForm" required>
                <label>Numer klienta</label>
                <input type="text" v-model="clientNumberForm" required>
                <label>Ulica</label>
                <input type="text" v-model="streetForm">
                <label>Miasto</label>
                <input type="text" v-model="cityForm" required>
                <label>Kod pocztowy</label>
                <input type="text" v-model="zipCodeForm" required>
                <label>Kod kraju</label>
                <input type="text" v-model="countryForm" required>
                <div id="add-btn-container">
                    <button>Zapisz</button>
                </div>
                <div v-if="errorForm || getCustomerError" class="error" >
                    <p>{{errorForm}}</p>
                    <p>{{getCustomerError}}</p>
                </div>
                <div v-if="createdFlag" class="success">
                    <p>Dane klienta zostały zapisane.</p>
                </div>
                
            </form>
        </div>

    </div>
  </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import NavbarComponent from '../components/NavbarComponent.vue'
import editCustomer from '../js-components/editCustomer.js'
import getCustomerById from '../js-components/getCustomerById.js'

export default {
    props:['customerId'],
    components:{NavbarComponent},
    setup(props){
    const url = 'https://localhost:44331/api/'
    const nameForm = ref('')
    const shortNameForm = ref('')
    const clientNumberForm = ref('')
    const streetForm = ref('')
    const cityForm = ref('')
    const zipCodeForm = ref('')
    const countryForm = ref('')
    const createdFlag = ref(false)
    const errorForm = ref('')

    const {edit, error:editCustomerError} = editCustomer(url)
    const {loadCustomer, error:getCustomerError, customer} = getCustomerById(url)

    onBeforeMount(()=>{
        loadCustomer(props.customerId).then(()=>{
            nameForm.value = customer.value.name
            shortNameForm.value = customer.value.shortName
            clientNumberForm.value = customer.value.clientNumber
            streetForm.value = customer.value.streetAddress
            cityForm.value = customer.value.cityAddress
            zipCodeForm.value = customer.value.zipCodeAddress
            countryForm.value = customer.value.countryAddress
        })
    })
    const handleSubmit = ()=>{
        if(countryForm.value.length > 2){
            errorForm.value = "Kod kraju jest nieprawidłowy (tylko 2 znaki)"
        }else{
            let formInputData = {
                name: nameForm.value,
                shortName: shortNameForm.value,
                clientNumber: clientNumberForm.value,
                streetAddress: streetForm.value,
                cityAddress: cityForm.value,
                zipCodeAddress: zipCodeForm.value,
                countryAddress: countryForm.value
            }
            errorForm.value=''
            editCustomerError.value = ''
            getCustomerError.value = ''
            createdFlag.value=true
            edit(props.customerId, formInputData).then(()=>{
                setTimeout(()=>{createdFlag.value = false},5000)
            })
        }
    }

    return {getCustomerError,
            editCustomerError,
            nameForm, 
            shortNameForm, 
            clientNumberForm, 
            streetForm, 
            cityForm,
            zipCodeForm,
            countryForm,
            errorForm,
            handleSubmit,
            customer,
            createdFlag
            }
}
}
</script>

<style>

</style>