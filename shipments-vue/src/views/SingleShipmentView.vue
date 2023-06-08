<template>
    <NavbarComponent />
    <Spinner v-if="isPending" />
   <div class="frame">
     <div class="shipment-view-container" v-if="shipment != null">
            <div class="item-header">
                <div class="header-img-ctnr">
                        <img src="../assets/img/cargo.png" alt="" srcset="">
                </div>
                <div class="poHearders" v-if="shipment.purchaseOrders.length < 3">
                    <div v-for="po in shipment.purchaseOrders" :key="po.id">
                        <h3>{{po.number}}</h3><h2> {{po.customerShortName}}</h2>
                    </div>
                </div>
                <div v-if="shipment.purchaseOrders.length >= 3" >
                    <div>
                        <h3>
                           Wiele zamówień ( {{shipment.purchaseOrders[0].customerShortName}} ... )
                        </h3>
                    </div>

                </div>
                <div class="tod">
                    <h1 v-if="shipment.timeOfDeparture == null">{{moment(shipment.etd).format("DD MMM YYYY")}}</h1>
                    <h1 v-else class="green">{{moment(shipment.timeOfDeparture).format("DD MMM YYYY")}}</h1>
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
                    <p>{{moment(shipment.etd).format("DD MMM YYYY dddd HH:mm")}}</p>
                </div>
                <div>
                    <p v-if="shipment.timeOfDeparture != null">{{moment(shipment.timeOfDeparture).format("DD MMM YYYY dddd HH:mm")}}</p>
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
                    <span v-for="(order, index) in shipment.purchaseOrders" :key="order.id">
                        <p><span class="order-desc">PO# {{index+1}}: </span>{{order.poNumber}} / {{order.category}} - {{order.incotermName}}</p>
                        <p class="pad"><span class="order-desc">Klient: </span>{{order.customerShortName}} ( {{order.customerCity}} )</p>
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
                        <div class="content-btn" @click="changeFlagData=true">
                        <p>Edytuj DANE</p>    
                        </div>
                    </div>
                    
                    <div class="content">
                        <div class="content-btn" @click="changeFlagOrders=true">
                            <p>Edytuj ZAMÓWIENIA</p> 
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
    <ChangeForwarder @changeForwarderEvent="handleChangeForwarder" :shipmentId="shipmentId" :currentForwarder="shipment.forwarder"/>
  </div>
  <div v-if="changeFlagOrders">
    <ChangeOrders @changeOrdersEvent="handleChangeOrders" :shipmentId="shipmentId"/>
  </div>
</template>

<script>
import { onBeforeMount, ref} from 'vue'

import NavbarComponent from '../components/NavbarComponent.vue'
import ChangeShipmentStatus from '../components/ChangeShipmentStatus.vue'
import ChangeShipmentComponent from '../components/ChangeShipmentComponent.vue'
import ChangeForwarder from '../components/ChangeForwarder.vue'
import ChangeOrders from '../components/ChangeOrders.vue'
import { useLinksStore } from '../stores/linksStore.js'
import getShipmentById from '../js-components/getShipmentById'
import moment from "moment/dist/moment"
import Spinner from '../components/SpinnerComponent.vue'
//import { useRouter} from 'vue-router'
export default {
    components: { NavbarComponent, ChangeShipmentStatus, ChangeShipmentComponent, ChangeForwarder, ChangeOrders, Spinner},
    props:['shipmentId'],
    setup(props){
        const linksStore = useLinksStore()
        const { loadShipment, error, shipment, isPending} = getShipmentById(linksStore.url)
        //const router = useRouter()

        const changeFlagStatus = ref(false)
        const changeFlagData = ref(false)
        const changeFlagForwarder = ref(false)
        const changeFlagOrders = ref(false)

        onBeforeMount(()=>{
            refreshShipments()
        })
        const refreshShipments = ()=>{
            loadShipment(props.shipmentId).then(()=>{
                let count = 1
                shipment.value.purchaseOrders.forEach(order => {
                    order['number']=count++
                });
            })
        }
        const handleChangeStatus = (flag) =>{
            if (flag == true){
                 refreshShipments()
            }
            changeFlagStatus.value = false
        }
        const handleChangeShipmentData = (flag) =>{
            if (flag == true){
                 refreshShipments()
            }
            changeFlagData.value = false
        }
        const handleChangeForwarder = (flag) =>{
            if (flag == true){
                 refreshShipments()
            }
            changeFlagForwarder.value = false
        }
        const handleChangeOrders = (flag) =>{
            if (flag == true){
                 refreshShipments()
            }
            changeFlagOrders.value = false
        }
        
        return{ 
                error, 
                shipment, 
                moment, 
                handleChangeStatus, handleChangeShipmentData, handleChangeForwarder, handleChangeOrders,
                changeFlagStatus, changeFlagData, changeFlagForwarder, changeFlagOrders,
                isPending
                }      
                
    }

}
</script>

<style>
.shipment-view-container{
    display: grid;
    grid-template-columns: 20vh 20vh 20vh 20vh ;
    grid-template-areas:
    "header header header status"
    " desc main main btns ";
    margin-top: 4vh;
    justify-content: center;
    


}
.item-header {
    grid-area: header;
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #fdc600;
    background: linear-gradient(to right, #ffdc60, #fdc600, #fdc600);
    background: linear-gradient(to right, #323232, #323232,#323232,#323232, #2d2d2d);
    /* background: #fcfcfc; */
    color: #3e3e3e;
    color: #dedede;
    border: solid #777 1px;
    padding-bottom: 0;
    z-index: 1;
    margin-bottom: 2vh;
}
.item-header .tod{
    margin-right: 3vh;
    font-size: 1.4vh;
}
.item-header h3{
    margin: 0;
}
.item-header .header-img-ctnr{
    margin: 0;
    padding: 0;
}
.item-header img{
    height: 9vh;
    transform: scale(1.5) translateX(-1.5vh) translateY(-0.2vh);
    
}
.poHearders{
    display: flex;
}
.poHearders div{
    display: flex;
    flex-direction: column;
    font-size: 1.5vh;
    
}
.poHearders h3, h2{
    margin: 0;
    padding: 0 1vh;
    z-index: 2;
}
.poHearders h2{
    transform: translateY(-1vh);
}
.poHearders h3{
    font-size: 1.2vh ;
    z-index: 1;
    padding: 0.6vh 0.8vh;
    width: 1vh;
    border-radius: 50%;
    color: #888;
    background: #454545;
    vertical-align: middle;
    transform: translateY(1.7vh) translateX(-0.7vh);
}
.green{
    color:#89f722
}
.item-status {
    grid-area: status;
    display: flex;
    justify-content: center;
    align-items: center;
    background-color: #fdc600;
    background: linear-gradient(to right,#997801, #fdc600,#fdc600,#fdc600, #fdc600, #997801);
    color: #333;
    border: solid #999 1px;
    font-size: 1.5vh;
    /* border-left: #fdc600 ; */
    z-index: 1;
    transform: scale(1.2)  translateY(0vh) translateX(0vh);
    border-radius: 11%;
    box-shadow: 0vh 1vh 1.2vh #00000080;
    margin-bottom: 2vh;
}

.item-status p{
    margin: 0;
}
.item-header div{
    padding: 0.5vh 2vh;
}
.item-header div span{
    margin: 0;
    padding: 0;
    font-size: 4.5vh;
}
.item-main {
    grid-area: main;
    display: flex;
    flex-direction: column;
    background-color: #282a39;
    background: linear-gradient(to right, #22242b, #282d39);
    border-top: solid #777 1px;
    border-bottom: solid #777 1px;
    font-size: 1.3vh;
    
}
.item-desc{
    font-size: 1.3vh;
    grid-area: desc;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    background-color: #242424;
    background: linear-gradient(to right, #202125, #22242b);
    color: #ffcb0e;
    border-left: solid #777 1px;
    border-top: solid #777 1px;
    
    /* box-shadow: 0.6vh 0vh 1.5vh #00000050; */
}
.item-btns{
    grid-area: btns;
    display: grid;
    width: 100%;
    align-content: space-evenly;
    border: solid #777 1px;
    background: linear-gradient(to right, #282d39, #26272f);

}

.item-btns div{
    padding: 0.6vh 0;
    
}
.item-btns div.content{
    align-self: center;
    justify-self: center;
    border: none;
}
.item-btns div.content .content-btn{
    display: flex;
    padding: 1vh 1.5vh;
    font-size: 1.2vh;
    cursor: pointer;
    width: 13vh;
    height: 4vh;
    justify-content: center;
    align-items: center;
    border: solid #888 1px;
    box-shadow: 0.3vh 0.3vh 0.4vh #00000085;
    
    background: linear-gradient(to right bottom, #414141, #353535);
    transition: 100ms ease-in-out;
    /* border-radius: 1em; */
}
.item-btns div.content .content-btn:hover{
    background: #ffcf0e;
    background: linear-gradient(to right bottom, #13a300, #166a03);
    /* color: #555; */
    box-shadow: 0.8vh 0.8vh 1vh #00000085;
    transition: 100ms ease-in-out;
    

}
.item-btns div.content .content-btn p{
    text-align: center;
    padding: 0.4vh 2vh;
}

.item-btns p{
     margin: 0;
    padding: 0.0vh 2vh;
    font-weight: 400;
}

.item-main div, .item-desc div{
    padding: 0.6vh 0;
    display: flex;    
    align-items: flex-start;
    flex-direction: column;
    height: 2.4vh;
    border-bottom: solid #777 1px;
}
.item-main p, .item-desc p{
    margin: 0;
    padding: 0.0vh 2vh;
    font-weight: 400;
    
}

.item-main .line-forwarder, .item-main .line-comment, .item-main .line-orders, 
.item-desc .line-forwarder, .item-desc .line-comment, .item-desc .line-orders {
    height: 9.2vh;
    overflow: hidden;
    overflow-y: visible;
    border-bottom: solid #777 1px;
}
.item-desc .line-orders, .item-main .line-orders{
    height: 11.4vh;
}
.item-main .line-forwarder{
    border-bottom: solid #777 0px;
}
.item-desc .line-comment, .item-main .line-comment{
    height: 6vh;
}

.order-desc{
    color: #ffcb0e;
    color: #999;
}
.line-orders .pad{
    padding-bottom: 0.6vh;
}

.statusNowa{
    background-color: #fff;
    background: linear-gradient(to right,#c6c6c6, #fff,#fff,#fff, #fff, #c6c6c6);
    color: #008ddf;
}
.statusGotowa{
    background-color: #3fb500;
    background: linear-gradient(to right,#1e5600, #3fb500,#3fb500,#3fb500, #3fb500, #1e5600);
    color: #ffffff;
}
.statusZrealizowana{
    background-color: #5f5f5f;
    background: linear-gradient(to right,#323232, #5d5d5d,#5f5f5f,#5f5f5f, #5d5d5d, #323232);
    color: #89f722;
}
.statusKompletacja{
    background-color: #008ddf;
    background: linear-gradient(to right,#004974, #008ddf,#2daef9,#2daef9, #008ddf, #004974);
    color: #ffffff;
}
.statusWstrzymana{
    background-color: #df3000;
    background: linear-gradient(to right,#8f1f00, #df3000,#df3000,#df3000, #df3000, #8f1f00);
    color: #ffffff;
}
.statusAnulowana{
    background-color: #5f5f5f;
    background: linear-gradient(to right,#323232, #5d5d5d,#5f5f5f,#5f5f5f, #5d5d5d, #323232);
    color: #d3d3d3;
}

.prio-line{
    display: flex;
    align-items: center;
}
.prio-line .material-symbols-outlined{
    font-size: 2.4vh;
    margin: 0 0 0 2vh;
    padding: 0;
    color: #90f01b;
}
</style>