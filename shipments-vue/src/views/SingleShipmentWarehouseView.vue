<template>
    <NavbarWarehouse />
   <div class="frame">
     <div class="shipment-view-container" v-if="shipment != null">
            <div class="item-header">
                <div>
                    <span class="material-symbols-outlined">
                        inventory
                    </span>
                </div>
                <div class="poHearders" v-if="shipment.purchaseOrders.length < 4">
                    <div v-for="po in shipment.purchaseOrders" :key="po.id">
                        <h3>{{po.customerShortName}}</h3><h3> ({{po.poNumber}})</h3>
                    </div>
                </div>
                <div v-if="shipment.purchaseOrders.length >= 4" >
                    <div>
                        <h3>
                           Wiele zamówień ( {{shipment.purchaseOrders[0].customerShortName}} ... )
                        </h3>
                    </div>

                </div>
                <div class="tod">
                    <h1 v-if="shipment.timeOfDeparture == null">{{moment(shipment.etd).format("YYYY MMM DD")}}</h1>
                    <h1 v-else class="green">{{moment(shipment.timeOfDeparture).format("YYYY MMM DD")}}</h1>
                </div>
            </div>
            <div class="item-status" :class="{
                                                statusNowa: shipment.status == 'Nowa',
                                                statusKompletacja:shipment.status == 'Kompletacja',
                                                statusGotowa:shipment.status == 'Gotowa',
                                                statusZrealizowana:shipment.status == 'Zrealizowana',
                                                statusWstrzymana:shipment.status == 'Wstrzymana',
                                                statusAnulowana:shipment.status == 'Anulowana'
                                            }">
                <div>
                    <h2 >{{shipment.status}}</h2>
                </div>
            </div>
            <div class="item-main">
                <div>
                    <p>{{moment(shipment.etd).format("YYYY-MM-DD dddd hh:mm")}}</p>
                </div>
                <div>
                    <p v-if="shipment.timeOfDeparture != null">{{moment(shipment.timeOfDeparture).format("YYYY-MM-DD dddd hh:mm")}}</p>
                    <p v-else>Brak</p>
                </div>
                <div>
                    <p class="prio-line" v-if="shipment.hasPriority">
                        <span>TAK</span>
                        <span class="material-symbols-outlined">
                            timer
                        </span>
                    </p>
                    <p v-else>
                        NIE
                    </p>
                </div>
                <div>
                    <p>{{shipment.palletQty}}</p>
                </div>
                <div>
                    <p>{{shipment.createdByUser}}</p>
                </div>
                <div>
                    <p>{{shipment.containerNumber}}</p>
                </div>
                <div>
                    <p>{{shipment.containerType}}</p>
                </div>
                <div>
                    <p>{{shipment.containerSealNumber}}</p>
                </div>
                <div>
                    <p>{{shipment.warehouseArea}}</p>
                </div>
                <div class="line-comment">
                    <p >{{shipment.comment}}</p>
                </div>
                <div class="line-orders">
                    <span v-for="order in shipment.purchaseOrders" :key="order.id">
                        <p><span class="order-desc">Nr zamówienia: </span>{{order.poNumber}}</p>
                        <p><span class="order-desc">Klient: </span>{{order.customerShortName}} ( {{order.customerCity}} )</p>
                        <p><span class="order-desc">Kategoria: </span>{{order.category}} - {{order.incotermName}}</p>
                        <br>
                    </span>
                </div>
                <div v-if="shipment.forwarder" class="line-forwarder">
                    <p><span class="order-desc">Nr rejestracyjne: </span>{{shipment.forwarder.carPlates}}</p>
                    <p><span class="order-desc">Spedytor: </span>{{shipment.forwarder.speditor}}</p>
                    <p><span class="order-desc">Kierowca: </span>{{shipment.forwarder.firstName}} {{shipment.forwarder.lastName}}</p>
                    <p><span class="order-desc">Telefon: </span>{{shipment.forwarder.phoneNumber}}</p>
                </div>
                
            </div>
            <div class="item-desc">
             
                <div>
                    <p>ETD</p>
                </div>
                <div>
                    <p>ATD</p>
                </div>
                <div>
                    <p>PRIORYTET</p>
                </div>
                <div>
                    <p>ILOŚĆ PALET</p>
                </div>
                <div>
                    <p>UŻYTKOWNIK</p>
                </div>
                <div>
                    <p>NUMER KONTENERA</p>
                </div>
                <div>
                    <p>TYP KONTENERA</p>
                </div>
                <div>
                    <p>NUMER PLOMBY</p>
                </div>
                <div>
                    <p>LOKACJA</p>
                </div>
                <div class="line-comment">
                    <p >KOMENTARZ</p>
                </div>
                <div class="line-orders">
                    <p>ZAMÓWIENIA</p>
                </div>
                <div class="line-forwarder">
                    <p>PRZEWOŹNIK</p>
                </div>
            </div>
            <div class="item-btns">
                
                    <div class="content">
                        <div class="content-btn" @click="changeFlagStatus=true">
                            <p>
                                Zmień STATUS
                            </p>
                        </div>
                    </div>
                    <div class="content">
                        <div class="content-btn" @click="changeFlagLocation=true">
                            <p>
                                Zmień LOKACJĘ
                            </p>
                        </div>
                    </div>
                    <div class="content">
                        <div class="content-btn" @click="changeFlagData=true">
                        <p>Edytuj DANE</p>    
                        </div>
                    </div>
                    
                    <div class="content">
                        <div class="content-btn"  @click="changeFlagForwarder=true">
                            <p>
                                Edytuj PRZEWOŹNIKA
                            </p>
                        </div>
                    </div>
                

            </div>
    </div>
  </div>
  
  <div v-if="changeFlagStatus">
    <ChangeShipmentStatus @changeStatusEvent="handleChangeStatus" :shipmentId="shipment.id" />
  </div>
  <div v-if="changeFlagData">
    <ChangeShipmentComponent @changeShipmentDataEvent="handleChangeShipmentData" :shipmentId="shipment.id" />
  </div>
  <div v-if="changeFlagForwarder">
    <ChangeForwarder @changeForwarderEvent="handleChangeForwarder" :shipmentId="shipmentId"/>
  </div>
  <div v-if="changeFlagLocation">
    <ChangeShipmentLocation @changeLocationEvent="handleChangeLocation" :shipmentId="shipmentId"/>
  </div>
  
</template>

<script>
import { onBeforeMount, ref} from 'vue'

import NavbarWarehouse from '../components/NavbarWarehouse.vue'
import ChangeShipmentStatus from '../components/ChangeShipmentStatus.vue'
import ChangeShipmentLocation from '../components/ChangeShipmentLocation.vue'
import ChangeShipmentComponent from '../components/ChangeShipmentComponent.vue'
import ChangeForwarder from '../components/ChangeForwarder.vue'

import { useLinksStore } from '../stores/linksStore.js'
import getShipmentById from '../js-components/getShipmentById'
import moment from "moment/dist/moment"

//import { useRouter} from 'vue-router'
export default {
    components: { NavbarWarehouse, ChangeShipmentStatus, ChangeShipmentComponent, ChangeForwarder, ChangeShipmentLocation },
    props:['shipmentId'],
    setup(props){
        const linksStore = useLinksStore()
        const { loadShipment, error, shipment} = getShipmentById(linksStore.url)
        //const router = useRouter()

        const changeFlagStatus = ref(false)
        const changeFlagData = ref(false)
        const changeFlagForwarder = ref(false)
        const changeFlagLocation = ref(false)

        onBeforeMount(()=>{
            loadShipment(props.shipmentId)
            
        })

        const handleChangeStatus = (flag) =>{
            if (flag == true){
                loadShipment(props.shipmentId)
            }
            changeFlagStatus.value = false
        }
        const handleChangeShipmentData = (flag) =>{
            if (flag == true){
                loadShipment(props.shipmentId)
            }
            changeFlagData.value = false
        }
        const handleChangeForwarder = (flag) =>{
            if (flag == true){
                loadShipment(props.shipmentId)
            }
            changeFlagForwarder.value = false
        }
        const handleChangeLocation = (flag) =>{
            if (flag == true){
                loadShipment(props.shipmentId)
            }
            changeFlagLocation.value = false
        }
        
        
        return{ 
                error, 
                shipment, 
                moment, 
                handleChangeStatus, handleChangeShipmentData, handleChangeForwarder, handleChangeLocation,
                changeFlagStatus, changeFlagData, changeFlagForwarder, changeFlagLocation
                
                }      
                
    }

}
</script>

<style>

</style>