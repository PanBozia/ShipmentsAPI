<template>
  <h2>Wybierz przewoźnika</h2>
  <form class="form-add" @submit.prevent="handleEmitForwarder()">
    <label class="form-labels">Aktualny kierowca</label>
    <div class="chosen-one" v-if="newForwarder">
        <div  >{{newForwarder.lastName + ' ' + newForwarder.firstName + ' - ' + newForwarder.carPlates}}</div>
        <div class="remove-btn" @click="handleRemoveDriver()">
            <span class="material-symbols-outlined">
                remove_circle
            </span>
        </div>
    </div>
    <div class="chosen-one red" v-else>
        <div >Brak kierowcy</div>
    </div >

    <label class="form-labels">Wybierz nowego kierowcę</label>
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
                        <span class="material-symbols-outlined">
                            add_circle
                        </span>
                    </div>
                </div>
            </div>
            <div v-if="totalItemsCountForwarders > 20">
                <p class="red">Znaleziono więcej wyników spełniających kryteria wyszukiwania.</p>
            </div>
    </div>
    <div>
        <button>Zapisz</button>
    </div>
</form>
  
  
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
            context.emit('forwarder-chosen-event',  newForwarder.value.id)
        }

        return {
                handleEmitForwarder,
                handleSearchForwarder, 
                handleChoise, handleRemoveDriver,
                forwardersError,
                totalItemsCountForwarders,
                forwarders,
                search_phrase,
                newForwarder
        }
    }
}
</script>

<style>

</style>

