<template>
  <NavbarComponent />
  <div class="row-container-one" v-if="chosenShipment == null">
    <div class="description">
        <h1>1 - WYSYŁKA</h1>
        <p>
            Wprowadź dane nowej wysyłki/transportu.
            Podaj estymowaną datę wyjazdu. 
            Jeżeli nie jest to transport kontenerowy, to wybierz opcję N/A jako typ kontenera.
            Pola numer kontenera, numer plomby, komentarz, to pola opcjonalne - ich uzupełnienie nie jest wymagane.
        </p>
    </div>
    <div>
        <AddShipment @new-shipment-event="handleNewShipment"/>
    </div>
  </div>
  <div v-else class="row-container-three">
    <div>
        <div>
                <div class="chosen-summary">
                    <h2>DANE NOWEJ WYSYŁKI</h2>
                    <p v-if="chosenShipment.hasPriority"> PRIORYTETOWA</p>
                    <p><span class="yellow">ETD:</span> {{moment(chosenShipment.etd).format("YYYY-MM-DD / hh:mm")}}</p>
                    <p v-if="chosenShipment.containerNumber"><span class="yellow">Nr kontenera: </span>{{chosenShipment.containerNumber}}</p>
                    <p v-if="chosenShipment.containerType"><span class="yellow">Typ kontenera: </span>{{chosenShipment.containerType}}</p>
                    <p v-if="chosenShipment.warehouseArea != 'N/A'"><span class="yellow">TPA: </span>{{chosenShipment.warehouseArea}} ( {{chosenShipment.palletQty}} palet )</p>
                    
                    
                </div>
        </div>
    </div>
    <div>
        <div v-if="chosenOrders != null" class="chosen-summary">
            <h2>WYBRANE ZAMÓWIENIA</h2>
            <div v-for="order in chosenOrders" :key="order.id">
                <p><span class="yellow">Klient: </span>{{order.customerName}}</p>
                <p><span class="yellow">Nr zamówienia: </span>{{order.poNumber}}</p>
                <p><span class="yellow">Kategoria: </span> {{order.category}} ({{order.incotermName}})</p>
            </div>
            
        </div>
    </div>
    <div>
        <div v-if="chosenForwarder != null">
            <div class="chosen-summary">
                
                <h2>Wybrany przwoźnik</h2>
                <p><span class="yellow">Tablice rej.: </span>{{chosenForwarder.carPlates}}</p>
                <p><span class="yellow">Firma: </span>{{chosenForwarder.speditor}}</p>
                <p><span class="yellow">Kierowca: </span>{{chosenForwarder.firstName}} {{chosenForwarder.lastName}}</p>
                <p><span class="yellow">Telefon: </span>{{chosenForwarder.phoneNumber}}</p>
            </div>
        </div>

    </div>
    <div></div>
  </div> 






  <div class="row-container-one" v-if="chosenShipment !=null && chosenOrders == null">
    <div class="description">
        <h1>2 - ZAMÓWIENIA</h1>
        <p>
            Wyszukaj i wybierz z listy zamówienia, które chcesz dodać do wysyłki, lub wprowadź dane nowego zamówienia.
            <br>
            Jeżeli nie dodasz zamówienia do wysyłki, możesz to zrobić później, edytując dane wysyłki.
        </p>
    </div>
    <div >
      <ChooseOrder @add-orders-event="handleAddOrders"/>
    </div>
    <div></div>
  </div>

   <div class="row-container-two" v-if="chosenShipment !=null">
     <div class="description">
        <h1>3 - PRZEWOŹNIK</h1>
        <p>
            Wyszukaj i wybierz z listy kierowcę, który realizuje transport/wysyłkę, lub wprowadź dane nowego kierowcy.
            <br>
            Jeżeli nie dodasz kierowcy do wysyłki, możesz to zrobić później, edytując dane wysyłki.
        </p>
    </div>
    <div>
      <ChooseForwarder @forwarder-chosen-event="handleChoosenForwarder"/>
    </div>
    <div>
      <AddForwarder @forwarder-chosen-event="handleChoosenForwarder" />
    </div>
    <div>
        <div v-if="chosenForwarder != null">
            <div class="chosen-summary">
                
                <h2>Wybrany przwoźnik</h2>
                
                <p>{{chosenForwarder.carPlates}}</p>
                <p>{{chosenForwarder.speditor}}</p>
                <p>{{chosenForwarder.firstName}} {{chosenForwarder.lastName}}</p>
                <p>Telefon: {{chosenForwarder.phoneNumber}}</p>
                
                
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";

import ChooseOrder from "../components/ChooseOrder.vue";
import NavbarComponent from "../components/NavbarComponent.vue";
import AddShipment from "../components/AddShipment.vue";
import ChooseForwarder from "../components/ChooseForwarder.vue";
import AddForwarder from "../components/AddForwarder.vue";
import getForwarderById from "../js-components/getForwarderById.js"
import getShipmentById from "../js-components/getShipmentById.js"
import moment from 'moment'
export default {
  components: { NavbarComponent, ChooseOrder, AddShipment, ChooseForwarder, AddForwarder },
  setup() {
    const url = 'https://localhost:44331/api/'
    const {loadForwarder, error:loadForwarderError, forwarder} = getForwarderById(url)
    const {shipment, error:shipmentError, loadShipment} = getShipmentById(url)
    const chosenForwarder = ref(null)
    const chosenShipment = ref(null)
    const chosenOrders = ref(null)
    
    const addedPoNumber = ref("");
    const handleAddOrders = (ordersList) => {
        console.log(ordersList)
      chosenOrders.value = ordersList
    };

    const handleNewShipment = (shipmentId) =>{
        console.log('shipmentId')
        console.log(shipmentId)
        loadShipment(shipmentId).then(()=>{
            chosenShipment.value = shipment.value
            // console.log(chosenShipment.value)
        })
    }

    const handleChoosenForwarder = (forwarderId)=>{
        console.log('Added forwarder was emmited')
        console.log(forwarderId)
        loadForwarder(forwarderId).then(()=>{
            chosenForwarder.value = forwarder.value
        })
    }

    // watch(forwarder,()=>{
    //     console.log(forwarder)
    // })

    return {
      handleNewShipment, shipmentError, chosenShipment,
      handleChoosenForwarder, loadForwarderError, chosenForwarder,
      addedPoNumber,
      moment,
      chosenOrders, handleAddOrders
    };
  },
};
</script>

<style>
.chosen-summary{
    padding:0 0 0 6vh;
}
.chosen-summary p{
    font-size: 1.4vh;
    font-weight: 300;
    padding: 0;
    margin:0;

}
.yellow{
    color: #e6b800;
}
.chosen-summary h2{
    color: #e6b800;
    margin-top: 0;
}
.description{
    font-weight: 200;
    padding:0vh 6vh;
    text-align: justify;
}

.row-container-one {
  display: grid;
  grid-template-columns: 1fr 42.5vw 1fr;
  gap: 5vh;
    /* max-width: 50vw; */
    /* margin: auto; */
    border-bottom: solid 1px #fff; 
    margin: 40px 0;
    padding-bottom: 40px;
}
.row-container-two {
    display: grid;
  grid-template-columns: 1fr 20vw 20vw 1fr;
  gap: 5vh;
  /* max-width: 100vw; */
  /* margin: auto; */
  border-bottom: solid 1px #fff; 
    margin: 40px 0;
    padding-bottom: 40px;
}
.row-container-three{
    display: grid;
    grid-template-columns: 30vh 30vh 30vh;
    
    /* max-width: 50vw; */
    /* margin: auto; */
    border-bottom: solid 1px #fff; 
    margin: 25px 0;
    padding-bottom: 25px;
}
</style>