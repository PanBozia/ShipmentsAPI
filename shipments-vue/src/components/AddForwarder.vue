<template>
   
    <div class="add-container">
        <form class="form-add" @submit.prevent="handleSubmit">
            <p>Dodaj nowego kierowcę</p>
            <label>Imię</label>
            <input type="text" v-model="firstNameForm" required>
            <label>Nazwisko</label>
            <input type="text" v-model="lastNameForm" required>
            <label>Spedycja</label>
            <input type="text" v-model="speditionForm" required>
            <label>Numer telefonu</label>
            <input type="text" v-model="phoneForm">
            <label>Numery rejestracyjne</label>
            <input type="text" v-model="platesForm" required>
            <div id="add-btn-container">
                <button>Zapisz</button>
            </div>
            <div v-if="errorForm || error" class="error" >
                <p>{{errorForm}}</p>
                <p>{{error}}</p>
            </div>
            <div v-if="createdFlag" class="success">
                <p>Dane kierowcy zostały zapisane.</p>
            </div>
            
        </form>
    </div>
</template>

<script>
import { ref } from 'vue'
import addForwarder from '../js-components/addForwarder.js'
export default {
setup(){
    const url = 'https://localhost:44331/api/'
    const firstNameForm = ref('')
    const lastNameForm = ref('')
    const speditionForm = ref('')
    const phoneForm = ref('')
    const platesForm = ref('')
    const createdFlag = ref(false)

    const errorForm = ref('')

    const {addNewForwarder, error, createdId} = addForwarder(url)

    const handleSubmit = ()=>{
        if(firstNameForm.value == '' || lastNameForm.value =='' || speditionForm.value == '' || platesForm.value ==''){
            errorForm.value = "Nie wprowadzono wszystkich danych."
        }else{
            let formInputData = {
                firstName: firstNameForm.value,
                lastName: lastNameForm.value,
                speditor: speditionForm.value,
                phoneNumber: phoneForm.value,
                carPlates: platesForm.value
            }
            errorForm.value = ''
            error.value = ''
            addNewForwarder(formInputData).then(()=>{
                firstNameForm.value = ''
                lastNameForm.value = ''
                speditionForm.value = ''
                phoneForm.value = ''
                platesForm.value = ''
                if(error.value == ''){
                    createdFlag.value = true
                }
                setTimeout(()=>{createdFlag.value = false},5000)
            })
        }
    }

    return {firstNameForm, 
            lastNameForm, 
            speditionForm, 
            phoneForm, 
            platesForm,
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