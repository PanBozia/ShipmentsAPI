<template>
  <h2>Wybierz przewoźnika z listy</h2>
  <form class="form-add" @submit.prevent="handleSearchForwarder()">
    <label class="form-labels">Aktualny przewoźnik</label>
    <div class="chosen-one" v-if="newForwarder">
        <div  >{{newForwarder.lastName + ' ' + newForwarder.firstName + ' - ' + newForwarder.carPlates}}</div>
        <div class="remove-btn" @click="handleRemoveDriver()">
            <span class="material-symbols-outlined">
                close
            </span>
        </div>
    </div>
    <div class="chosen-one red" v-else>
        <div >Brak przewoźnika</div>
    </div >

    <label class="form-labels">Wyszukaj</label>
    <div class="search-ctnr">
        <input class="search-input" type="text" v-model="search_phrase">
        <div class="btn search-button" @click="handleSearchForwarder()">Szukaj</div>
    </div>
    <div v-if="forwarders != null">
            <div class="forwarder-list-ctnr">
                <div v-if="forwarders.length == 0">
                    <p id="no-result">Brak wyników wyszukiwania...</p>
                </div>
                <div v-else class="driver-line" v-for="forwarder in forwarders" :key="forwarder.id">
                    <p>{{forwarder.lastName + ' ' + forwarder.firstName + ' - ' + forwarder.carPlates}}</p>
                    <div class="add-driver-btn" @click="handleChoise(forwarder)">
                        
                         <div class="add-item-btn"><span>DODAJ</span></div>
                    </div>
                </div>
            </div>
            <div v-if="totalItemsCountForwarders > 20">
                <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
            </div>
    </div>
    <div class="btn-ctnr">
</div>
</form>


 <form class="form-add" @submit.prevent="">
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
                <button @click="handleAddNewForwarder">Zapisz</button>
            </div>
            <div v-if="errorForm" class="error" >
                <p>{{errorForm}}</p>
                
            </div>
            <div v-if="createdFlag" class="success">
                <p>Dane kierowcy zostały zapisane.</p>
            </div>
            
        </form>


<button class="btn" @click="handleEmitForwarder()">
    Zapisz
</button>
  
  
</template>

<script>

import { ref } from 'vue'
import getForwarders from '../js-components/getForwarders.js'

export default {
    
    emits:['forwarder-chosen-event'],
    setup(props,context){
        const url = 'https://localhost:44331/api/'
        
        const {loadForwarders, error:forwardersError, forwarders, totalItemsCount:totalItemsCountForwarders} = getForwarders(url)
        const search_phrase = ref('')
        const newForwarder = ref('')

        const handleSearchForwarder = ()=>{
            loadForwarders(search_phrase.value,20,1,'',0)
        }
        const handleChoise = (chosenForwarder)=>{
            newForwarder.value = chosenForwarder
        }
        const handleRemoveDriver = ()=>{
            newForwarder.value = null
        }

        const handleEmitForwarder = ()=>{
            context.emit('forwarder-chosen-event',  newForwarder.value)
            console.log(newForwarder.value)
        }

        //Add new forwarder

        const firstNameForm = ref('')
        const lastNameForm = ref('')
        const speditionForm = ref('')
        const phoneForm = ref('')
        const platesForm = ref('')
        const createdFlag = ref(false)

        const errorForm = ref('')

        const handleAddNewForwarder = ()=>{
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
                
                newForwarder.value =  formInputData
                firstNameForm.value = ''
                lastNameForm.value = ''
                speditionForm.value = ''
                phoneForm.value = ''
                platesForm.value = ''
                createdFlag.value = true
                setTimeout(()=>{createdFlag.value = false},5000)
            }
        }


        return {
                handleEmitForwarder,
                handleSearchForwarder, 
                handleChoise, handleRemoveDriver,
                forwardersError,
                totalItemsCountForwarders,
                forwarders,
                search_phrase,
                newForwarder,
                
                handleAddNewForwarder,
                firstNameForm, 
                lastNameForm, 
                speditionForm, 
                phoneForm, 
                platesForm,
                errorForm,
                createdFlag

        }
    }
}
</script>

<style>

</style>

