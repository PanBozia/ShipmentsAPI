<template>
   
    <div class="add-container">
        <form class="form-add" @submit.prevent="handleSubmit">
            <p>Dodaj nowego klienta</p>
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
export default {
setup(){
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

    const {addNewCustomer, error, createdId} = addCustomer(url)

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

    return {nameForm, 
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