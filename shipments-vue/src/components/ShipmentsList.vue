<template>
    <SpinnerComponent v-if="isPending" />
  <div>
   <div>
    <form class="shipment-search-form">
        <div class="search-item">
            <h2 class="filter-header">Filtrowanie</h2>
        </div>
        <div class="search-item">
            <label>Data wysyłki</label>
            <div class="item-row">
                <input type="date" v-model="timeOfDeparture">
                <span class="material-symbols-outlined cancel-btn" @click="timeOfDeparture = null">
                    backspace
                </span>

            </div>
        </div>
        <div class="search-item">
            <label>Nr rejestracyjny</label>
            <div class="item-row">
                <input type="text" v-model="carPlates">
                 <span class="material-symbols-outlined cancel-btn" @click="carPlates = ''">
                    backspace
                </span>
            </div>
        </div>
        <div class="search-item">
            <label>Nr kontenera</label>
            <div class="item-row">
                <input type="text" v-model="containerNumber">
                <span class="material-symbols-outlined cancel-btn" @click="containerNumber = ''">
                    backspace
                </span>
            </div>
        </div>
        <div class="search-item">
            <label>TPA</label>
            <div class="item-row">
                <!-- <input type="text" v-model="warehouseAreaId"> -->
                <select v-if="!areaError" v-model="warehouseAreaId">
                    <option value="0"></option>
                    <option v-for="area in areas " :key="area.id" :value="area.id">{{area.name}}</option>
                    
                </select>
                <span class="material-symbols-outlined cancel-btn" @click="warehouseAreaId = 0">
                        backspace
                </span>
            </div>
        </div>
        <div class="search-item">
            <label>Nr zamówienia</label>
            <div class="item-row">
            <input type="text" v-model="purchaseOrderNumber">
            <span class="material-symbols-outlined cancel-btn" @click="purchaseOrderNumber = ''">
                    backspace
            </span>
            </div>
        </div> 
        <div class="search-item">
            <label>Ilość palet</label>
            <div class="item-row">
                <!-- <input type="number" v-model="palletQty"> -->
                <select v-model="palletQty">
                    <option v-for="number in palletsArray" :key="number" :value="number">{{number}}</option>
                </select>
                <span class="material-symbols-outlined cancel-btn" @click="palletQty = 0">
                        backspace
                </span>
            </div>
        </div>
        <div class="search-item">
            <label>Status</label>
            <div class="item-row">
                <select v-model="statusId">
                    <option v-for="status in statuses" :key="status.id" :value="status.id">{{status.name}}</option>
                </select>
                <span class="material-symbols-outlined cancel-btn" @click="statusId = 0">
                        backspace
                </span>
            </div>
        </div>
        <div class="search-item">
            <label>Komentarz</label>
            <div class="item-row">
                <input type="text" v-model="comment">
                <span class="material-symbols-outlined cancel-btn" @click="comment = ''">
                        backspace
                </span>
            </div>
        </div>
        <div class="prio-item">
            <!-- <input type="checkbox"  v-model="hasPriority">
            <label>PRIO</label> -->

            <p><span class="material-symbols-outlined">timer</span></p>
            <div class="userSwitch">
                                <input type="checkbox" name="userSwitch" :checked="hasPriority" class="userSwitch-cb"  :id="hasPriority" v-model="hasPriority">
                                <label class="userSwitch-label" @click="handlePrio()">
                                <div class="userSwitch-inner"></div>
                                <div class="userSwitch-switch"></div>
                                </label>
                            </div>
        </div>
        
    </form>

   </div>



    <div v-if="error != null" style="color:#f61">
        <p>{{error}}</p>
    </div>
    <div v-if="error == null">
        <div class="list-item list-header shipment-header shipment-list">
            
            <p>NR PO</p>
            <p>ETD</p>
            <p>KONTENER</p>
            <p>TPA</p>
            <p>ILOŚĆ</p>
            <p>KIEROWCA</p>
            <!-- <p>UŻYTKOWNIK</p> -->
            <p>STATUS</p>
        </div>
        <div class="list-item list-header shipment-header shipment-list sort-bar">
             <p>
               
            </p>   
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='ETD', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='ETD', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>
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
            
            <p>
                <span class="sort-icon-label">SORTUJ:</span>
                <span @click="sortBy='Name', sortDirection=0" class="material-symbols-outlined sort-icon">keyboard_double_arrow_up</span>
                <span @click="sortBy='Name', sortDirection=1" class="material-symbols-outlined sort-icon">keyboard_double_arrow_down</span>                
            </p>
        </div>
        <!-- :class="{'prio-shipment' : shipment.hasPriority}" -->
        <div class="list-item shipment-list "  v-for="shipment in shipments" :key="shipment.id" @click="gotoShipment(shipment.id)">
                <div>
                    <div v-if="shipment.purchaseOrders.length == 0">
                        <p>N/A</p>
                    </div>
                    <div v-else v-for="order in shipment.purchaseOrders" :key="order.id" class="order-shipment">
                        <div>
                            <!-- <p>PO#: </p> -->
                            <p>{{order.poNumber}} - {{order.customerShortName}} / {{order.customerCity[0]}}{{order.customerCity[1]}}</p>
                        </div>
                    </div>
                </div>

                <p>ETD: {{moment(shipment.etd).format("YYYY-MM-DD")}}
                    <br>
                    <span v-if="shipment.timeOfDeparture">ATD: {{moment(shipment.timeOfDeparture).format("YYYY-MM-DD")}}</span>
                    
                </p>
                <p>TYP: {{shipment.containerType}} <br>CTNR#: {{shipment.containerNumber}}<br>SEAL#: {{shipment.containerSealNumber}}</p>
                <p>{{shipment.warehouseArea}}</p>
                <p>{{shipment.palletQty}}</p>
                <p v-if="shipment.forwarder">{{shipment.forwarder.carPlates}}<br>{{shipment.forwarder.firstName +' '+ shipment.forwarder.lastName}}<br>{{shipment.forwarder.speditor}}</p>
                <p v-else>N/A</p>
                <!-- <p>{{shipment.createdByUser}}</p> -->
                <p>{{shipment.status}}</p>
                <div v-if="shipment.hasPriority"><p><span class="material-symbols-outlined">timer</span></p></div>
                <div v-else></div>
                
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
                        <div class="icon-page" :class="{'icon-page-active' : pageNumber==1}" @click="handleGoToPage(1)">
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
import getAreas from '../js-components/getAreas.js'
import getStatuses from '../js-components/getStatuses.js'
import moment from 'moment'
import 'moment/min/locales.min'
import 'moment/locale/pl'
import {useRouter} from 'vue-router'
import { useLinksStore } from '../stores/linksStore.js'
import SpinnerComponent from './SpinnerComponent.vue'
export default {
    components:{SpinnerComponent},
    props:['userIsOffice'],
    setup(props){
        const linksStore = useLinksStore()
        const router = useRouter()
        const {loadShipments, error, shipments, totalPages, itemsFrom, itemsTo, totalItemsCount, isPending} = getShipments(linksStore.url)
        const {loadAreas, error:areaError, areas} = getAreas(linksStore.url)
        const {loadStatuses, error:statusError, statuses} = getStatuses(linksStore.url)

        const pageSize = ref(10)
        const pageNumber = ref(1)
        const sortBy = ref('')
        const sortDirection = ref(0)

        const timeOfDeparture = ref(null)
        const hasPriority = ref(false)
        const comment =ref('')
        const warehouseAreaId = ref()
        const palletQty = ref()
        const carPlates =ref("")
        const statusId = ref()
        const containerNumber = ref("")
        const purchaseOrderNumber = ref()
        
        const palletsArray = ref([])
        

        const queryData = computed(()=>{
            
            let query = {
                
                hasPriority: hasPriority.value,
                comment: comment.value,
                warehouseAreaId: warehouseAreaId.value,
                carPlates: carPlates.value,
                statusId: statusId.value,
                containerNumber: containerNumber.value,
                purchaseOrderNumber: purchaseOrderNumber.value,
                pageNumber: pageNumber.value,
                pageSize: pageSize.value,
                sortBy: sortBy.value,
                sortDirection: sortDirection.value
            };
                if(palletQty.value != 0){query['palletQty'] = palletQty.value}
                //console.log(timeOfDeparture.value)
                if (moment(timeOfDeparture.value).format("YYYY-MM-DDTHH:mm") != "Invalid date") {query['timeOfDeparture'] = timeOfDeparture.value}

            return query
        })

        
        
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
            
            loadAreas();
            loadStatuses();
            loadShipments(queryData.value);
            let pallets = [];
            for(let index = 0; index < 100; index++) {
                pallets[index] = index +1;
            }
            palletsArray.value = pallets
        })

        const updateTable = ()=>{
            //pageNumber.value = 1
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
            queryData.value.pageNumber = 1
            loadShipments(queryData.value)
        }
        const handlePrio = ()=>{
            hasPriority.value = !hasPriority.value
        }

        onUnmounted (()=>{
            searchWatcher(); //invoking the method ends watching
        })

        

        const gotoShipment = (shipmentId) =>{
            
            if(props.userIsOffice){
                router.push({ name:'SingleShipmentView', 
                          params:{ shipmentId }
                          }) 
            }
            if(!props.userIsOffice){
                router.push({ name:'SingleShipmentWarehouseView', 
                          params:{ shipmentId }
                          }) 
            }
            
        }

        return{ isPending,
                pagesRange, 
                palletsArray,
                handlePageSize, 
                handleGoToPage, 
                handleSubmit, 
                handlePrio,
                totalPages, 
                totalItemsCount, 
                itemsTo, 
                itemsFrom, 
                shipments, 
                error, 
                areaError,
                areas,
                statusError,
                statuses,
                pageNumber,
                sortBy,
                sortDirection,
                moment,
                timeOfDeparture,
                hasPriority,
                comment,
                warehouseAreaId,
                palletQty,
                carPlates,
                statusId,
                containerNumber,
                purchaseOrderNumber,
                gotoShipment }                          
                
    }
}
</script>

<style scoped>

form{
    margin:  0;
    padding: 0;
}
input{
    font-size: 0.7em;
    border: none;
    font-family: 'Poppins', sans-serif;
}
.list-item.shipment-list{
    grid-template-columns: 14% 14% 16% 6% 8% 16% 16% 2% 0% ;
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
.shipment-add-btn:hover{
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

.shipment-search-form{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: auto;
    gap: 12px;
    align-items: end;
    border: solid 2px #eee;
    border-radius: 10px 10px 0 0;
    color:#fff;
    padding: 20px 30px 30px 30px;
    transform: translateY(10px);
    border-bottom: none;
    /* background: linear-gradient(to right bottom, #5bb7f8,#0e409d); */
    background: #0064ac;
    background: linear-gradient(to right bottom, #0178cd, var(--back));
}

.shipment-search-form .search-item{
    display: grid;
    justify-items: stretch;
    max-width: 14vh;
}
.shipment-search-form .search-item input, select{
    padding: 5px 8px;
    border: none;
    min-width: 8vh;
    margin: 0;
}
.shipment-search-form option{
    font-size: 0.8em;
    margin: 0;
}
.shipment-search-form .prio-item{
    align-self: end;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    font-size: 1.3em;
}
.shipment-search-form .prio-item p{
    display: flex;
    padding: 0px 4px;
    margin: 0 16px 0 0;
}

.shipment-search-form .prio-item .material-symbols-outlined{
    font-size: 1.4em;
}
.shipment-search-form  .item-row{
    display: grid;
    grid-template-columns: 1fr 26px;
    justify-items: stretch;
    margin:0px;
}
.shipment-search-form  .item-row input, select{
    box-shadow: none;
}
.cancel-btn{
    background: #fff;
    font-size: 1.5em;
    border: 0;
    border-radius: 0 4px 4px 0;
    
    cursor: pointer;
    color: #aaa;
    display: flex;
    align-items: center;
    justify-content: stretch;
    margin: 0;
    transform: translateX(-3px);

}
.cancel-btn:hover{
    background: var(--back);
    color: #fff;

}
.filter-header{
    padding: 0px;
    margin: 0;
    justify-self: flex-start;
    align-self: flex-start;
}

label{
    font-weight: 300;
    font-size: 0.8em;
}
.shipment-search-form input[type="checkbox"]{
    height: 26px;
    width: 26px;
    
}

.shipment-search-form .btn-search{
    display: flex;
    padding: 5px 30px;
    margin: 0px 10px;
    cursor: pointer;
    height: 34px;
    font-family: 'Poppins', sans-serif;
    margin-right: 0;
    margin-left: 20px;
    background: linear-gradient(to right bottom, #5bb7f8,#0e409d);
}



.userSwitch {
    margin: 2px 0;
    position: relative;
    width: 94px;
}
.userSwitch input[type=checkbox] {
  display: none;
}
.userSwitch-label {
  display: block;
  overflow: hidden;
  cursor: pointer;
  border: 0px solid #000c20;
  border-radius: 50px;
}
.userSwitch-inner {
  width: 200%;
  margin-left: -100%;
  transition: margin 0.2s ease-in 0s;
  
}
.userSwitch-inner:before, .userSwitch-inner:after {
  float: left;
  width: 50%;
  height: 24px;
  padding: 0;
  line-height: 24px;
  font-size: 11px;
  color: white;
  /* font-family: Trebuchet, Arial, sans-serif; */
  /* font-weight: bold; */
  box-sizing: border-box;
  
}
.userSwitch-inner:before {
  content: "PRIO";
  padding-left: 10px;
  background-color: #fff;
  color: #000000;
  box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.switches .flipswitch-inner:after {
    content: "";

}
.userSwitch-inner:after {
    content: "WSZYSTKIE";
    padding-right: 8px;
    background-color: #003e6a;
    color: #E8E8E8;
    text-align: right;
    box-shadow: inset 2px 2px 4px rgba(0,0,0,0.4);
}
.userSwitch-switch {
  width: 24px;
  height: 24px;
  margin: 0px;
  background: #F5B727;
  box-shadow: 1px 1px 3px rgba(4,4,4,0.4), -1px -1px 3px rgba(4,4,4,0.4);
  border-radius: 50px;
  position: absolute;
  top: 0;
  bottom: 0;
  right: 70px;
  transition: all 0.2s ease-in 0s;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-inner {
  margin-left: 0;
}
.userSwitch-cb:checked + .userSwitch-label .userSwitch-switch {
  right: 0;
}


</style>