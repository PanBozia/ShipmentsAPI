<template>
<div class="choose-order-container non-symertic">
    <div>

        <h2>Wybierz przewoźnika z listy</h2>
        <form class="form-add" @submit.prevent="handleSearchForwarder()">
            <label class="form-labels">Wybrany przewoźnik</label>
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
                            <div>
                                <p>{{forwarder.speditor}}</p>
                                <p>{{forwarder.carPlates}} - {{forwarder.firstName + ' ' + forwarder.lastName}}</p>
                            </div>
                            <div class="add-driver-btn" @click="handleChoise(forwarder)">
                                
                                <div class="add-item-btn"><span>DODAJ</span></div>
                            </div>
                        </div>
                    </div>
                    <div v-if="totalItemsCountForwarders > 20">
                        <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
                    </div>
            </div>
            
        </form>
    </div>

    <div>

        <!-- <form class="form-add" @submit.prevent="">

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
                <button @click="handleAddNewForwarder">Dodaj</button>
            </div>
            <div v-if="errorForm" class="error" >
                <p>{{errorForm}}</p>
                
            </div>
            <div v-if="createdFlag" class="success">
                <p>Dane kierowcy zostały zapisane.</p>
            </div>
            
        </form> -->
        <AddForwarder @created-forwarder-event="handleAddNewForwarder" />
    </div>
</div>

<div class="btn-ctnr">
    <button class="btn" @click="handleEmitForwarder()">
        Zapisz
    </button>
</div>
  
  
</template>

<script>

import { ref } from 'vue'
import getForwarders from '../js-components/getForwarders.js'
import AddForwarder from '../components/AddForwarder.vue'
import getForwarderById from '../js-components/getForwarderById.js'
export default {
    
    emits:['forwarder-chosen-event'],
    components:{AddForwarder},
    setup(props,context){
        const url = 'https://localhost:44331/api/'
        
        const {loadForwarders, error:forwardersError, forwarders, totalItemsCount:totalItemsCountForwarders} = getForwarders(url)
        const {loadForwarder, error:getForwarderError, forwarder} = getForwarderById(url)
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
            
        }

        const handleAddNewForwarder = (createdId)=>{
            loadForwarder(createdId).then(()=>{
                newForwarder.value = forwarder.value
            })
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
                getForwarderError

        }
    }
}
</script>

<style>

</style>

