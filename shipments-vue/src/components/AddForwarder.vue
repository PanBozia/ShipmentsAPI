<template>
   
    <div>
        <form class="form-add add-single-form" @submit.prevent="handleSubmit">
            <h2>Dodaj nowego przewoźnika</h2>
            <div class="form-set">
                <label class="form-labels">Numery rejestracyjne</label>
                <input type="text" v-model="platesForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Spedycja</label>
                <input type="text" v-model="speditionForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Imię</label>
                <input type="text" v-model="firstNameForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Nazwisko</label>
                <input type="text" v-model="lastNameForm" required>
            </div>
            <div class="form-set">
                <label class="form-labels">Numer telefonu</label>
                <input type="text" v-model="phoneForm">
            </div>
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
emits:['created-forwarder-event'],
setup(props, context){
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
        if(firstNameForm.value == '' || lastNameForm.value == '' || speditionForm.value == '' || platesForm.value ==''){
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
                    context.emit('created-forwarder-event', createdId.value)
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