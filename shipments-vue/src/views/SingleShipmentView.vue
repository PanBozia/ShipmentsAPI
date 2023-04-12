<template>
    <NavbarComponent />
   <div class="frame">
     <div class="shipment-view-container" v-if="shipment">
            <div class="item-header">
                <div>
                    <span class="material-symbols-outlined">
                        inventory
                    </span>
                </div>
                <div>
                    <h2>{{moment(shipment.etd).format("YYYY MMMM DD - hh:mm")}}</h2>
                </div>
                <div v-for="po in shipment.purchaseOrders" :key="po.id">
                    <h2>{{po.customerShortName}} ( {{po.poNumber}} )</h2>
                </div>
            </div>
            <div class="item-main">
                <div :class="{statusNew: shipment.status == 'Nowa'}">
                    <p >{{shipment.status}}</p>
                </div>
                <div>
                    <p>{{moment(shipment.etd).format("YYYY MMMM DD - hh:mm")}}</p>
                </div>
                <div>
                    <p v-if="shipment.timeOfDeparture = null">{{shipment.timeOfDeparture}}</p>
                    <p v-else>Brak</p>
                </div>
                <div>
                    <p>{{shipment.hasPriority}}</p>
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
                <div class="line-forwarder">
                    <p><span class="order-desc">Nr rejestracyjne: </span>{{shipment.forwarder.carPlates}}</p>
                    <p><span class="order-desc">Spedytor: </span>{{shipment.forwarder.speditor}}</p>
                    <p><span class="order-desc">Kierowca: </span>{{shipment.forwarder.firstName}} {{shipment.forwarder.lastName}}</p>
                    <p><span class="order-desc">Telefon: </span>{{shipment.forwarder.phoneNumber}}</p>
                </div>
                
            </div>
            <div class="item-desc">
                <div>
                    <p>STATUS</p>
                </div>
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
                        <div class="content-btn">
                        <p>Edytuj dane</p>    
                        </div>
                    </div>
                    <div class="content">
                        <div class="content-btn">
                            <p>
                                Zmień status
                            </p>
                        </div>
                    </div>
                    
                    <div class="content">
                        <div class="content-btn">
                            <p>Zmień zamówienia</p> 
                        </div>
                    </div>
                    <div class="content">
                        <div class="content-btn">
                            <p>
                                Zmień kierowcę
                            </p>
                        </div>
                    </div>
                

            </div>
    </div>
  </div>
</template>

<script>
import { onBeforeMount} from 'vue'

import NavbarComponent from '../components/NavbarComponent.vue'
import getShipmentById from '../js-components/getShipmentById'
import moment from 'moment'
export default {
    components: { NavbarComponent},
    props:['shipmentId'],
    setup(props){
        const url = 'https://localhost:44331/api/'
        const { loadShipment, error, shipment} = getShipmentById(url)
        
        onBeforeMount(()=>{
            
            loadShipment(props.shipmentId)
        })
        
        return{ error, shipment, moment }      
                
    }

}
</script>

<style>
.shipment-view-container{
    display: grid;
    grid-template-columns: 20vh 20vh 20vh 20vh ;
    grid-template-areas:
    "header header header header"
    " desc main main btns ";
    margin-top: 4vh;
    justify-content: center;
    

}
.item-header {
    grid-area: header;
    display: flex;
    justify-content: flex-start;
    align-items: center;
    background-color: #fdc600;
    background: linear-gradient(to right, #ffdc60, #fdc600, #fdc600);
    color: #333;
    border: solid #444 1px;
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
    background: linear-gradient(to right, #202125, #282d39);
    
}
.item-btns{
    grid-area: btns;
    display: grid;
    width: 100%;
    border-right: solid #444 1px;
    border-bottom: solid #444 1px;
    border-left: solid #444 1px;
    background: linear-gradient(to right, #282d39, #202125);
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
    padding: 1.5vh 1.5vh;
    font-size: 1.3vh;
    cursor: pointer;
    width: 11vh;
    justify-content: center;
    align-items: center;
    border: solid #444 1px;
    box-shadow: 0.5vh 0.5vh 1vh #00000050;
}
.item-btns div.content .content-btn:hover{
    background: linear-gradient(to right bottom, #f3c71a, #a98700);
    background: linear-gradient(to right bottom, #3dc92b, #166a03);

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
    border-bottom: solid #444 1px;
}
.item-main p, .item-desc p{
    margin: 0;
    padding: 0.0vh 2vh;
    font-weight: 400;
}

.item-desc{
    grid-area: desc;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    background-color: #242424;
    background: linear-gradient(to right, #252525, #222);
    color: #ffcb0e;
    border-left: solid #444 1px;
    
    /* box-shadow: 0.6vh 0vh 1.5vh #00000050; */
}



.item-main .line-forwarder, .item-main .line-comment, .item-main .line-orders, 
.item-desc .line-forwarder, .item-desc .line-comment, .item-desc .line-orders {
    height: 10vh;
    overflow: hidden;
    overflow-y: visible;
    border-bottom: solid #444 1px;
}
.item-desc .line-comment, .item-main .line-comment{
    height: 6vh;
}

.order-desc{
    color: #ffcb0e;
    color: #999;
}
  


.statusNew{
    background-color: #888
}
</style>