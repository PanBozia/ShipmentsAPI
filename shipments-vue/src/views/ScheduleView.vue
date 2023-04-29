<template class="schedule-ctnr">
    <div class="navbar" @click="refreshScreenOn">
        <div class="navbar-line"></div>
        <div class="schedule-container schedule-bar">
            <div>
                <div class="schedule-header">
                    <p>ZAMÓWIENIE</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>KLIENT</p>
                </div>
            </div>
            <div>
               <div class="schedule-header">
                    <p>MIEJSCE</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>LOKACJA</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>PAL #</p>
                </div>
            </div>
            
            <div>
                <div class="schedule-header">
                    <p>ETD</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>NR KONTENERA</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>NR REJESTRACJI</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>STATUS</p>
                </div>
            </div>
        </div>
        <div class="navbar-line"></div>
    </div>



    <div class="frame">
        
        <div v-if="!error" >
            <div v-for="shipment in shipments" :key="shipment.id" class="schedule-container scheduleRow" 
                :class="{
                    statusCompleated:shipment.status == 'Zrealizowana',
                    
                    secondRow: shipment.counter % 2 == 0,
                    prioRow: shipment.hasPriority
                    }">
                <!-- zamówienie -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="order in shipment.purchaseOrders" :key="order.id" :class="{doubleline:shipment.purchaseOrders.length>1}">
                        {{order.category}}: {{order.poNumber}} 
                    </div>
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>

                <!-- klient -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="order in shipment.purchaseOrders" :key="order.id" :class="{doubleline:shipment.purchaseOrders.length>1}">
                        {{order.customerShortName}}
                    </div>
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>
<!-- :class="{statusDelivered:delivery.statusId==3, statusInTransit:delivery.statusId==2, statusCreated:delivery.statusId==1}" -->
                <!-- miejsce -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="order in shipment.purchaseOrders" :key="order.id" :class="{doubleline:shipment.purchaseOrders.length>1}">
                        {{order.customerCity}}
                    </div>
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>
                <!-- TPA -->
                <div v-if="shipment.warehouseArea">{{shipment.warehouseArea}}</div>
                <div v-else>N/A</div>
                <!-- PAL# -->
                <div v-if="shipment.palletQty">{{shipment.palletQty}}</div>
                <div v-else>N/A</div>
                <!-- ETD -->
                <div v-if="shipment.etd">
                    <span v-if="shipment.status != 'Zrealizowana'">
                        {{moment(shipment.etd).format("YY-MMM-DD / HH:mm")}}
                    </span>
                    <span v-else>
                        {{moment(shipment.timeOfDeparture).format("YY-MMM-DD / HH:mm")}}
                    </span>
                </div>
                <div v-else>N/A</div>
                <!-- CTNR# -->
                <div v-if="shipment.containerNumber">{{shipment.containerNumber}}</div>
                <div v-else>N/A</div>
                <!-- NR REJ. -->
                <div v-if="shipment.forwarder != null">{{shipment.forwarder.carPlates}}</div>
                <div v-else>N/A</div>
                <!-- STATUS -->
                <div class="status" v-if="shipment.status != null" 
                    :class="{
                        statusDone:shipment.status == 'Zrealizowana' || shipment.status == 'Gotowa',
                        statusBlocked:shipment.status == 'Wstrzymana' || shipment.status == 'Wstrzymana QA' || shipment.status == 'Wstrzymana LP',
                        statusCanceled:shipment.status == 'Anulowana'
                        }">
                    <span>
                        {{shipment.status}}
                    </span>
                    <span v-if="shipment.hasPriority" class="material-symbols-outlined timer">timer</span>
                </div>
                <div v-else>N/A</div>

            </div>
        </div>

    </div>
</template>

<script>
import { onBeforeMount } from 'vue'
import getShipments from '../js-components/getShipments'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'
export default {
    
    setup(){
        const linksStore = useLinksStore()
        const {loadShipments, error, shipments, totalPages, itemsFrom, itemsTo, totalItemsCount} = getShipments(linksStore.url)
        
        const queryData = {
                //hasPriority: hasPriority.value,
                //comment: comment.value,
                //warehouseAreaId: warehouseAreaId.value,
                //carPlates: carPlates.value,
                //statusId: statusId.value,
                //containerNumber: containerNumber.value,
                //purchaseOrderNumber: purchaseOrderNumber.value,
                pageNumber: 1,
                pageSize: 20,
                sortBy: "ETD"
                //sortDirection: sortDirection.value
                // palletQty: 14
                //timeOfDeparture.value
        }
        

        onBeforeMount(()=>{
            loadScheduleShipments()
        })

        const loadScheduleShipments = ()=>{
            loadShipments(queryData).then(()=>{
                let counter = 0
                shipments.value.forEach(shipment => {
                    shipment['counter']=counter++
                });
            })
        }
       
        // const handleFullScreen = ()=>{
            
        //     var elem = document.getElementById("app")
        //     if (elem.requestFullscreen) {
        //         elem.requestFullscreen();
        //     } else if (elem.webkitRequestFullscreen) { /* Safari */
        //         elem.webkitRequestFullscreen();
        //     } else if (elem.msRequestFullscreen) { /* IE11 */
        //         elem.msRequestFullscreen();
        //     }

            
        // }    
          function requestFullScreen(element) {
                // Supports most browsers and their versions.
                var requestMethod = element.requestFullScreen || element.webkitRequestFullScreen || element.mozRequestFullScreen || element.msRequestFullScreen;

                if (requestMethod) { // Native full screen.
                    requestMethod.call(element);
                    } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
                        var wscript = new window.ActiveXObject("WScript.Shell");
                        if (wscript !== null) {
                            wscript.SendKeys("{F11}");
                        }
                    }
                }

        const refreshScreenOn = ()=>{
            var elem = document.getElementById("app") // Make the body go full screen.
            requestFullScreen(elem);
            setInterval(()=>{
                loadScheduleShipments()
            }, 1000*60)
        }  
        
     

        return{
             error, shipments, totalPages, itemsFrom, itemsTo, totalItemsCount, moment, refreshScreenOn
        }
    }

}
</script>

<style scoped>



.scheduleRow{

    background: linear-gradient(to right, #232938, #213D67);
    border-bottom: solid 0.1vh #ddd;
    /* border-color: #000000; */
    min-height: 7.0vh;
    box-shadow: inset 0 1vh 2vh #00000030;
    color: #ddd;

}
.secondRow{
    background: linear-gradient(to right, #252d4e, #1d488a);
    color: #eee;
    }
.prioRow{
    /* background: linear-gradient(to right, #38254e, #7e22af); */
}
.status{
    display: flex;
    justify-content: space-between;
}
.timer{
    font-size: 3vh;
    margin: 0 0 0 1vh;
    padding: 0;
    color: #fdc700;

}
.schedule-container{
    font-family: 'Bebas Neue';
    
    font-weight: 200;
    font-size: 2.6vh;
    display: grid;
    align-items: center;
    grid-template-columns:2.5fr 1fr 3fr 1.2fr 1fr 2.5fr 2fr 3fr 2fr ;
    column-gap: 1vh;
    padding: 0.3vh 3vh;
}
.schedule-container div{
    word-wrap: none;
}
.schedule-header p{
    font-family: 'Bebas Neue';
    font-weight: 500;
    font-size: 2.8vh;
}
.schedule-bar{
    height: 6.0vh;
    align-content: center
}

.schedule-container .doubleline{
    font-size: 2.0vh;
}
.statusDone{
    color: #b0f25a;
}
.statusBlocked{
    color: #f8b052;
}
.statusCompleated{
    color: #89cd2f;
}
.statusCanceled{
    color: #f86262;

}
</style>