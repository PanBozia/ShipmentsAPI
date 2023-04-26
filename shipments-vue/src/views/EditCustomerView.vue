<template>
   <NavbarComponent />
  
  <div class="frame">
     <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>KLIENCI</p>
        </div>
        <div class="item-a" >
            <router-link :to="{name:'CustomerView'}" class="item-container main-list-btn">
                <p><span class="material-symbols-outlined">list_alt</span></p>
            </router-link>
           
            <div class="item-container main-add-btn" @click="openAddComponent">
                <p><span class="material-symbols-outlined">add</span></p>
            </div>
        </div>
        
        <div class="item-v">
            <div v-if="customer && !goToAddFlag">
                <div class="add-container">
                    <form class="form-add add-single-form" @submit.prevent="handleSubmit">
                        <h2>Edytuj dane klienta</h2>
                        <div class="form-set">
                            <label>Nazwa (skrót)</label>
                            <input type="text" v-model="shortNameForm" required>
                        </div>
                        <div class="form-set">
                            <label>Pełna nazwa</label>
                            <input type="text" v-model="nameForm" required>
                        </div>
                        <div class="form-set">
                            <label>Numer klienta</label>
                            <input type="text" v-model="clientNumberForm" required>
                        </div>
                        <div class="form-set">
                            <label>Ulica</label>
                            <input type="text" v-model="streetForm">
                        </div>
                        <div class="form-set">
                            <label>Miasto</label>
                            <input type="text" v-model="cityForm" required>
                        </div>
                        <div class="form-set">
                            <label>Kod pocztowy</label>
                            <input type="text" v-model="zipCodeForm" required>
                        </div>
                        <div class="form-set">
                            <label>Kod kraju</label>
                            <input type="text" v-model="countryForm" required>
                        </div>
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
            <div v-if="goToAddFlag">
                <AddCustomer />
            </div>
        </div>
    </div>
  </div>
 
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import NavbarComponent from '../components/NavbarComponent.vue'
import editCustomer from '../js-components/editCustomer.js'
import getCustomerById from '../js-components/getCustomerById.js'
import AddCustomer from '../components/AddCustomer.vue'
import { useLinksStore } from '../stores/linksStore.js'
export default {
    props:['customerId'],
    components:{NavbarComponent, AddCustomer},
    setup(props){
    const linksStore = useLinksStore()

    const nameForm = ref('')
    const shortNameForm = ref('')
    const clientNumberForm = ref('')
    const streetForm = ref('')
    const cityForm = ref('')
    const zipCodeForm = ref('')
    const countryForm = ref('')
    const createdFlag = ref(false)
    const errorForm = ref('')
    const goToAddFlag = ref(false)

    const {edit, error:editCustomerError} = editCustomer(linksStore.url)
    const {loadCustomer, error:getCustomerError, customer} = getCustomerById(linksStore.url)

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

    const openAddComponent = ()=>{
        goToAddFlag.value = true
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
            createdFlag,
            openAddComponent,
            goToAddFlag
            }
}
}
</script>

<style>

</style>