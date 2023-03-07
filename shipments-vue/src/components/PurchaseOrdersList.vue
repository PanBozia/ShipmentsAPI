<template>
  <div>
    <div class="search-header">
        <div class="search-item">
            <p>Lista zamówień</p>
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
        <div class="list-item list-header po-header po-list">
            <p>TYP</p>   
            <p>INCOTERMS</p>
            <p>NUMER PO</p>
            <p>KLIENT</p>
            <p>DATA DOSTAWY</p>
            <p>WYSYŁKI</p>
        </div>
        <div class="list-item list-header po-header po-list sort-bar">
             <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='Category', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Category', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>   
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='Incoterms', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Incoterms', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='PONumber', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='PONumber', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='CustomerShortName', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='CustomerShortName', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='DeliveryDate', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='DeliveryDate', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                
            </p>
        </div>

        <div class="list-item po-list" v-for="order in orders" :key="order.id">
            <p>{{order.category}}</p>
            <p>{{order.incotermName}}</p>
            <p>{{order.poNumber}}</p>
            <p>{{order.customerShortName}} - {{order.customerName}}</p>
            <p>{{moment(order.deliveryDate).format("YYYY MMMM DD")}}</p>
            <div>
                <div v-if="order.shipments.length == 0"></div>
                <div v-else v-for="shipment in order.shipments" :key="shipment.id" class="order-shipment">
                    <div>
                        <p>ETD:</p>
                        <p>{{moment(shipment.etd).format("YYYY-MM-DD")}}</p>
                    </div>
                    <div>
                        <p>TD:</p>
                        <p>{{moment(shipment.timeOfDeparture).format("YYYY-MM-DD")}}</p>
                    </div>
                </div>
            </div>
            
            <div></div>
            <button class="customer-add-btn" @click="handleEdit(order.id)">EDYTUJ</button>
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
import getPurchaseOrders from '../js-components/getPurchaseOrders.js'
import moment from 'moment'

import {useRouter} from 'vue-router'
export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const {loadOrders, error, orders, totalPages, itemsFrom, itemsTo, totalItemsCount} = getPurchaseOrders(url)
        const router = useRouter()
        const searchPhrase = ref('')
        const pageSize = ref(10)
        const pageNumber = ref(1)
        const sortBy = ref('DeliveryDate')
        const sortDirection = ref(0)
        
        const updateOrders = (_pageSize, _pageNumber )=>{
            pageSize.value = _pageSize
            pageNumber.value = _pageNumber
            loadOrders(searchPhrase.value, pageSize.value, pageNumber.value, sortBy.value, sortDirection.value)
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
            updateOrders(pageSize.value,1)
        })

        const updateTable = ()=>{
            updateOrders(pageSize.value, pageNumber.value)
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
            updateOrders(pageSize.value, pageNumber.value)
        }
        const handleGoToPage = (pNumber)=>{
            updateOrders(pageSize.value, pNumber)
        }
        const handlePageSize = (pSize)=>{
            updateOrders(pSize, 1)
        }
        const handleEdit =(orderId)=>{
             router.push({ name:'EditOrderView', 
                           params:{ orderId:orderId } 
                        })
        }

        onUnmounted (()=>{
            searchWatcher(); //invoking the method ends watching
            sortDirectionWatcher();
            sortByWatcher();
        })
        return{ pagesRange, 
                handleEdit,
                handlePageSize, 
                handleGoToPage, 
                handleSubmit, 
                searchPhrase, 
                totalPages, 
                totalItemsCount, 
                itemsTo, 
                itemsFrom, 
                orders, 
                error, 
                pageNumber,
                sortBy,
                sortDirection,
                moment }                          
                
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

.list-item.po-list{
    grid-template-columns: 9% 11% 12% 20% 18% 10% 2% 11%;
    gap: 1%;
}
.list-header.sort-icon.po-header{
    font-size: 0.1em;
}
.sort-icon p span{
    font-size: 0.3em;
}
.customer-add-btn:hover{
    background: linear-gradient(to right, #e6ff9b,#d0ff35); 
}
.customer-add-btn span{
    font-size: 1.4em;
    margin-right: 4px ;
}
.list-item.list-header.po-header.po-list.sort-bar{
    border-radius: 0;
    margin: 0;
    
    padding-top: 4px;
    padding-bottom: 4px;
    height: auto;
    border-top: none;
   
    border-bottom: solid 1px var(--back);
}
.list-item.list-header.po-header.po-list{
    border-bottom: none;
}
.order-shipment{
    display: flex;
    flex-direction: column;
    border-left:solid 1px #888; 
    border-right: solid 1px #888; 
    
    
    border-radius: 5%;
    margin-top: 5px;
    margin-bottom: 5px;
    padding: 4px;
    font-size: 0.8em;
    
}
.order-shipment div{
    display: flex;
    justify-content: space-between;
}
.order-shipment div p{
    font-size: 0.6vw;

}
</style>