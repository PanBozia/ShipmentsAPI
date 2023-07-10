<template>
   <Spinner v-if="isPending" />
    <div class="add-container">
        <form class="form-add add-single-form" @submit.prevent="handleSubmit">
            <h2>Dodaj nowego klienta</h2>
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
            <div v-if="errorForm || error" class="error" >
                <p>{{errorForm}}</p>
                <p>{{error}}</p>
            </div>
            <div v-if="createdFlag" class="success">
                <p>Dane klienta zostały zapisane.</p>
            </div>
            
        </form>
    </div>
</template>

<script>
import { ref } from 'vue'
import addCustomer from '../js-components/addCustomer.js'
import { useLinksStore } from '../stores/linksStore.js'

import Spinner from './SpinnerComponent.vue'
export default {
components:{Spinner},
setup(){
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

    const {addNewCustomer, error, createdId, isPending} = addCustomer(linksStore.url)

    const handleSubmit = ()=>{
        errorForm.value = ''
        
        if(shortNameForm.value.includes(" ") || shortNameForm.value.length > 6 ){
            errorForm.value = 'Nazwa (skrót) nie powinna zawierać spacji i musi mieć od 3 do 6 znaków. '
        }
        console.log(shortNameForm.value.length)
        console.log(errorForm.value)

        if(countryForm.value.length > 2){
            errorForm.value = "Kod kraju jest nieprawidłowy (tylko 2 znaki). "
        }
        if(errorForm.value == ''){
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
            error.value = ''
            addNewCustomer(formInputData).then(()=>{
                if(error.value == ''){
                    nameForm.value = ''
                    shortNameForm.value = ''
                    clientNumberForm.value = ''
                    streetForm.value = ''
                    cityForm.value = ''
                    zipCodeForm.value = ''
                    countryForm.value = ''
                    createdFlag.value = true
                }
                setTimeout(()=>{createdFlag.value = false},5000)
            })

        }
        
    }

    return {
            isPending,
            nameForm, 
            shortNameForm, 
            clientNumberForm, 
            streetForm, 
            cityForm,
            zipCodeForm,
            countryForm,
            errorForm,
            handleSubmit,
            error,
            createdId,
            createdFlag
            }
}
}
</script>

<style>

</style>