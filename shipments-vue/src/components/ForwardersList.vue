<template>
  <div>
    <div class="search-header">
        <div class="search-item">
            <p>Lista kierowców</p>
        </div>
        <form class="search-form" @submit.prevent="handleSubmit">
            <input type="search" v-model="searchPhrase">
            <button class="btn-search"><span class="material-symbols-outlined">search</span> Szukaj</button>
        </form>

    </div>
    <div v-if="error != null" style="color:#f61">
        <p>{{error}}</p>
    </div>
    <div v-if="error == null">
        <div class="list-item list-header">
            <p >
                IMIĘ I NAZWISKO 
                <span @click="sortBy='LastName', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='LastName', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                SPEDYTOR
                <span @click="sortBy='Speditor', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Speditor', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                NR TELEFONU
                <span @click="sortBy='PhoneNumber', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='PhoneNumber', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                NR REJESTRACYJNE
                <span @click="sortBy='CarPlates', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='CarPlates', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
        </div>
        

        <div class="list-item" v-for="forwarder in forwarders" :key=forwarder.id>
            <p>{{forwarder.lastName}} {{forwarder.firstName}}</p>
            <p>{{forwarder.speditor}}</p>
            <p>{{forwarder.phoneNumber}}</p>
            <p>{{forwarder.carPlates}}</p>
            <button>EDYTUJ</button>
        </div>
        <div class="list-footer">
            <p>Ilość wszystkich pozycji: {{totalItemsCount}}</p>
            <div class="icon-page-size-ctnr">
                <div><p>Ilość pozycji na stronę</p></div>
                <div class="icon-page-size" @click="handlePageSize(5)">5</div>
                <div class="icon-page-size" @click="handlePageSize(10)">10</div>
                <div class="icon-page-size" @click="handlePageSize(20)">20</div>
            </div>
                <div v-if="totalPages" class="list-footer-pages">
                    <p> Strony:</p>
                    <div v-if="pageNumber > 3" class="first-page-ctnr">
                        <div class="icon-page" :class="{'icon-page-active' : pageNumber==page}" @click="handleGoToPage(1)">
                            1 
                        </div>
                        <div>...</div>
                    </div>
                    <div class="icon-page" :class="{'icon-page-active' : pageNumber==page}" @click="handleGoToPage(page)" v-for="page in pagesRange" :key="page">
                        {{page}}
                    </div>
                    <p>/</p>
                    <div  class="icon-page" @click="handleGoToPage(totalPages)">
                        {{totalPages}}
                    </div>
                </div>
                <div v-else>
                    <p>Brak stron</p>
                </div>
            
        </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, onUnmounted, ref, watch } from 'vue'
import getForwarders from '../js-components/getForwarders.js'
export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const {loadForwarders, error, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCount} = getForwarders(url)

        const searchPhrase = ref('')
        const pageSize = ref(5)
        const pageNumber = ref(1)
        const sortBy = ref('LastName')
        const sortDirection = ref(0)
        
        const updateFowarders = (_pageSize, _pageNumber )=>{
            pageSize.value = _pageSize
            pageNumber.value = _pageNumber
            loadForwarders(searchPhrase.value, pageSize.value, pageNumber.value, sortBy.value, sortDirection.value)
        }

        // stronki
        const pagesRange = computed(()=>{
            let range = []
            if(totalPages.value < 5){
                for (let index = 0; index < totalPages.value; index++) {
                    range[index] = index+1
                }
            }else{
                if (pageNumber.value <= 3){
                    for (let index = 0; index < 5; index++) {
                        range[index] = index+1
                    }
                }else{
                    let firstPage = pageNumber.value-2;
                    let lastPage = totalPages.value
                    for (let index = 0; index < 5; index++) {
                        if(pageNumber.value + 3 > totalPages.value){
                            range[index] = lastPage++ - 4
                        }else{
                            range[index] = firstPage++
                        }
                    }
                }
            }
            
            return range
        })

        onMounted (()=>{
            updateFowarders(5,1)
        })
        const updateTable = ()=>{
            updateFowarders(pageSize.value, pageNumber.value)
            pagesRange.effect.run
        }

        const searchWatcher = watch((searchPhrase), () => {
            updateTable()
        })
        const sortByWatcher = watch((sortBy), () => {
            updateTable()
        })
        const sortDirectionWatcher = watch((sortDirection), () => {
            updateTable()
        })
        
        const handleSubmit = ()=>{
            updateFowarders(pageSize.value, pageNumber.value)
        }
        const handleGoToPage = (pageNumber)=>{
            updateFowarders(pageSize.value, pageNumber)
        }
        const handlePageSize = (pageSize)=>{
            updateFowarders(pageSize, 1)
        }

        onUnmounted(()=>{
            searchWatcher();
            sortDirectionWatcher();
            sortByWatcher();
        })

        return{ pagesRange, 
                handlePageSize, 
                handleGoToPage, 
                handleSubmit, 
                searchPhrase, 
                totalPages, 
                totalItemsCount, 
                itemsTo, 
                itemsFrom, 
                forwarders, 
                error, 
                pageNumber,
                sortBy,
                sortDirection }                          
                
    }
}
</script>

<style scoped>

form{
    margin:  0;
    padding: 0;
}
.search-form{
    display: flex;
    align-items: center;
}

input{
    height: 34px;
    width: 220px;
    border: none;
    font-family: 'Poppins', sans-serif;
}

.btn-search{
    display: flex;
    justify-content: center;
    align-items: center;
    border: none;
    color:#fff;
    padding: 5px 30px;
    margin: 0px 10px;
    cursor: pointer;
    height: 34px;
    font-family: 'Poppins', sans-serif;
    margin-right: 0;
    margin-left: 20px;
    background: linear-gradient(to right bottom, #5bb7f8,#0e409d);
}
.btn-search span{
    font-size: 1.5em;
}

</style>