<template>
  <div>
    <div class="search-header">
        <div class="search-item">
            <p>Lista klientów</p>
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
        <div class="list-item list-header customer-list">
            <p>SKRÓT</p>
            <p>NUMER</p>
            <p>NAZWA</p>
            <p>ADRES</p>
            <p>KRAJ</p>
        </div>
         <div class="list-item list-header customer-list sort-bar">
            <p >
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='ShortName', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='ShortName', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='ClientNumber', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='ClientNumber', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='Name', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Name', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='CityAddress', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='CityAddress', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='CountryAddress', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='CountryAddress', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
        </div>

        <div class="list-item customer-list" v-for="customer in customers" :key=customer.id>
            <p>{{customer.shortName}}</p>
            <p>{{customer.clientNumber}}</p>
            <p>{{customer.name}}</p>
            <p>{{customer.streetAddress}}<br>{{customer.zipCodeAddress}} {{customer.cityAddress}}</p>
            <p>{{customer.countryAddress}}</p>
            <div></div>
            <div></div>
            <button @click="handleEdit(customer.id)">EDYTUJ</button>
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
import {useRouter} from 'vue-router'
import getCustomers from '../js-components/getCustomers.js'

export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const {loadCustomers, error, customers, totalPages, itemsFrom, itemsTo, totalItemsCount} = getCustomers(url)
        const router = useRouter()
        const searchPhrase = ref('')
        const pageSize = ref(10)
        const pageNumber = ref(1)
        const sortBy = ref('ShortName')
        const sortDirection = ref(0)
        
        const updateCustomers = (_pageSize, _pageNumber )=>{
            pageSize.value = _pageSize
            pageNumber.value = _pageNumber
            loadCustomers(searchPhrase.value, pageSize.value, pageNumber.value, sortBy.value, sortDirection.value)
        }

        // pages
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
            updateCustomers(pageSize.value,1)
        })

        const updateTable = ()=>{
            updateCustomers(pageSize.value, pageNumber.value)
            pagesRange.effect.run
        }
        
        const searchWatcher = watch((searchPhrase), () => {
            updateTable()
        })
        const sortDirectionWatcher = watch((sortDirection), () => {
            updateTable();
        })
        const sortByWatcher = watch((sortBy), () => {
            updateTable();
        })
        
        const handleSubmit = ()=>{
            updateCustomers(pageSize.value, pageNumber.value)
        }
        const handleGoToPage = (pageNumber)=>{
            updateCustomers(pageSize.value, pageNumber)
        }
        const handlePageSize = (pageSize)=>{
            updateCustomers(pageSize, 1)
        }
        onUnmounted (()=>{
            searchWatcher(); //invoking the method ends watching
            sortDirectionWatcher();
            sortByWatcher();
        })

        const handleEdit = (id)=>{
             router.push({ name:'EditCustomerView', 
                      params:{ customerId:id } 
        })
        }
        return{ handleEdit,
                pagesRange, 
                handlePageSize, 
                handleGoToPage, 
                handleSubmit, 
                searchPhrase, 
                totalPages, 
                totalItemsCount, 
                itemsTo, 
                itemsFrom, 
                customers, 
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

.list-item.customer-list{
    grid-template-columns: 8% 8% 24% 20% 8% 6% 2% 8% ;
    gap:2%;
}
.customer-add-btn{
    background: linear-gradient(to right, #e6ff9b,#d0ff35); 
}
.customer-add-btn span{
    font-size: 1.4em;
    margin-right: 4px ;
}
.list-item.list-header.customer-list.sort-bar{
    border-radius: 0;
    margin: 0;
    
    padding-top: 4px;
    padding-bottom: 4px;
    height: auto;
    border-top: none;
   
    border-bottom: solid 1px var(--back);
}
.list-item.list-header.customer-list{
    border-bottom: none;
}
</style>