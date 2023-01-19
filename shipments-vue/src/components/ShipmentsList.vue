<template>
  <div>
    <div class="search-header">
        <div class="search-item">
            <p>Lista wysyłek</p>
        </div>
        <form class="search-form" @submit.prevent="handleSubmit">
            <input type="search" v-model="queryData.carPlates">
            <button class="btn-search"><span class="material-symbols-outlined">search</span> Szukaj</button>
        </form>

    </div>
    <div v-if="error != null" style="color:#f61">
        <p>{{error}}</p>
    </div>
    <div v-if="error == null">
        <div class="list-item list-header shipment-header shipment-list">
            
            <p>NR PO</p>
            <p>WYJAZD</p>
            <p>KONTENER</p>
            <p>TPA</p>
            <p>ILOŚĆ</p>
            <p>KIEROWCA</p>
            <p>UŻYTKOWNIK</p>
            <p>STATUS</p>
        </div>
        <div class="list-item list-header shipment-header shipment-list sort-bar">
             <p>
               
            </p>   
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='TimeOfDeparture', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='TimeOfDeparture', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='ContainerType', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='ContainerType', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p></p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='PalletQty', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='PalletQty', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
            </p>
            <p></p>
            <p></p>
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='Name', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Name', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>                
            </p>
        </div>

        <div class="list-item shipment-list " :class="{'prio-shipment' : shipment.hasPriority}" v-for="shipment in shipments" :key=shipment.id>
            <div v-if="shipment.purchaseOrders.length == 0">
                <p>N/A</p>
            </div>
            <div v-else v-for="order in shipment.purchaseOrders" :key="order.id" class="order-shipment">
                <div>
                    <!-- <p>PO#: </p> -->
                    <p>{{order.poNumber}}</p>
                </div>
            </div>
            <p>{{moment(shipment.etd).locale("pl").format("YYYY-MM-DD")}} ETD<br>{{moment(shipment.timeOfDeparture).locale("pl").format("YYYY-MM-DD")}} ATD</p>
            <p>{{shipment.containerType}} <br>CTNR#: {{shipment.containerNumber}}<br>SEAL#: {{shipment.containerSealNumber}}</p>
            <p>{{shipment.warehouseArea}}</p>
            <p>{{shipment.palletQty}}</p>
            <p v-if="shipment.forwarder">{{shipment.forwarder.carPlates}}<br>{{shipment.forwarder.firstName +' '+ shipment.forwarder.lastName}}<br>{{shipment.forwarder.speditor}}</p>
            <p v-else>N/A</p>
            <p>{{shipment.createdByUser}}</p>
            <p>{{shipment.status}}</p>
            <div v-if="shipment.hasPriority"><p><span class="material-symbols-outlined">timer</span></p></div>
            <div v-else></div>
            <button class="shipment-add-btn"><span class="material-symbols-outlined">edit</span>EDYTUJ</button>
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
import getShipments from '../js-components/getShipments.js'
import moment from 'moment'
import 'moment/min/locales.min'
import 'moment/locale/pl'
export default {
    setup(){
        const url = 'https://localhost:44331/api/'
        const {loadShipments, error, shipments, totalPages, itemsFrom, itemsTo, totalItemsCount} = getShipments(url)

        const pageSize = ref(5)
        const pageNumber = ref(1)
        const sortBy = ref('')
        const sortDirection = ref(0)
        moment.locale('pl')
        const queryData = computed(()=>{
            let query = {
                // timeOfDeparture : "",
                hasPriority: false,
                comment: "",
                warehouseAreaId: 0,
                palletQty: 0,
                carPlates: "",
                statusId: 0,
                containerNumber: "",
                purchaseOrderNumber: "",
                pageNumber: pageNumber.value,
                pageSize: pageSize.value,
                sortBy: sortBy.value,
                sortDirection: sortDirection.value
            }
            return query
        })
        // const updateShipments = (_pageSize, _pageNumber )=>{
        //     pageSize.value = _pageSize
        //     pageNumber.value = _pageNumber
        //     loadShipments(searchPhrase.value, pageSize.value, pageNumber.value, sortBy.value, sortDirection.value)
        // }

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
            loadShipments(queryData.value)
        })

        const updateTable = ()=>{
            loadShipments(queryData.value)
            pagesRange.effect.run
        }
        
        const searchWatcher = watch((queryData), () => {
            updateTable()
        })
        
        
        const handleSubmit = ()=>{
            loadShipments(queryData.value)
        }
        const handleGoToPage = (pageNo)=>{
            pageNumber.value = pageNo
            queryData.value.pageNumber = pageNo
            loadShipments(queryData.value)
        }
        const handlePageSize = (pageS)=>{
            pageSize.value = pageS
            queryData.value.pageSize = pageS
            loadShipments(queryData.value)
        }
        onUnmounted (()=>{
            searchWatcher(); //invoking the method ends watching
        })
        return{ pagesRange, 
                handlePageSize, 
                handleGoToPage, 
                handleSubmit, 
                totalPages, 
                totalItemsCount, 
                itemsTo, 
                itemsFrom, 
                shipments, 
                error, 
                pageNumber,
                sortBy,
                sortDirection,
                moment,
                queryData }                          
                
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
    font-size: 1.1vw;
}

.list-item.shipment-list{
    grid-template-columns: 8% 11% 14% 6% 6% 14% 12% 10% 2% 8% ;
    column-gap: 1%;
}
.list-item.shipment-list p{
    font-size: 0.7em;
    padding: 4px;

}
.prio-shipment{
    background: #ffe46c;
    background: linear-gradient(to right,#fff, #fff,#fff,#ffe46c); 
}
.sort-icon p span{
    font-size: 0.3em;
}
.shipment-add-btn{
    background: linear-gradient(to right, #e6ff9b,#d0ff35); 
    
}
.shipment-add-btn span{
    font-size: 1.2em;
    margin-right: 4px ;
}
.list-item.list-header.shipment-header.shipment-list.sort-bar{
    border-radius: 0;
    margin: 0;
    
    padding-top: 4px;
    padding-bottom: 4px;
    height: auto;
    border-top: none;
   
    border-bottom: solid 1px var(--back);
}
.list-item.list-header.shipment-header.shipment-list{
    border-bottom: none;
}
.list-item.list-header.shipment-header.shipment-list p{
    padding: 0 5px;
    font-size: 0.8em;
}
/* .order-shipment{
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
} */
</style>