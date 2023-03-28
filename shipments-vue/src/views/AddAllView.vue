<template>
  <NavbarComponent />
  <div class="row-container-seven">
    <!-- 1st column -->
    <div> 
        <div>
            <div class="chosen-summary">
                <h2>DANE NOWEJ WYSYŁKI</h2>
                <div v-if="chosenShipment != null">
                    <p v-if="chosenShipment.hasPriority"> PRIORYTETOWA</p>
                    <p><span class="yellow">ETD:</span> {{moment(chosenShipment.etd).format("YYYY-MM-DD / hh:mm")}}</p>
                    <p v-if="chosenShipment.containerNumber"><span class="yellow">Nr kontenera: </span>{{chosenShipment.containerNumber}}</p>
                    <p v-if="chosenShipment.containerType"><span class="yellow">Typ kontenera: </span>{{chosenShipment.containerType}}</p>
                    <p v-if="chosenShipment.warehouseArea != 'N/A'"><span class="yellow">TPA: </span>{{chosenShipment.warehouseArea}} ( {{chosenShipment.palletQty}} palet )</p>
                </div>
                <div v-else>
                    <div>
                        <p>
                            Nie wprowadzono danych
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- 2nd column -->
    <div v-if="chosenShipment != null" class="forward-arrow done">
        <span class="material-symbols-outlined">
            task_alt
        </span>
    </div>
    <div v-else class="forward-arrow undone">
        <span class="material-symbols-outlined">
            unpublished
        </span>
    </div>
    <!-- 3rd column -->
    <div>
        <div class="chosen-summary">
            <h2>WYBRANE ZAMÓWIENIA</h2>
            <div v-if="chosenOrders != null">
                <div v-if="chosenOrders.length == 0">
                    <p>Wysyłka bez zamówień</p>
                    <p>(edytuj później)</p>
                </div>
                <div v-else v-for="order in chosenOrders" :key="order.id" class="chosen-orders">
                    <p><span class="yellow">Nr: </span>{{order.poNumber}}</p>
                    <p><span class="yellow">Klient: </span>{{order.customerName}}</p>
                    <p><span class="yellow">Kategoria: </span> {{order.category}} ({{order.incotermName}})</p>
                    
                </div>
            </div>
            <div v-else>
                <div>
                    <p>
                        Nie wybrano zamówień
                    </p>
                </div>
            </div>
            
        </div>
    </div>
    <!-- 4th column -->
     <div v-if="chosenOrders != null" class="forward-arrow done seven-item">
        <span class="material-symbols-outlined">
            task_alt
        </span>
    </div>
    <div v-else class="forward-arrow undone seven-item">
        <span class="material-symbols-outlined">
            unpublished
        </span>
    </div>
    <!-- 5th column -->
    <div class="seven-item">
        <div>
            <div class="chosen-summary">
                
                <h2>Wybrany przwoźnik</h2>
                <div v-if="chosenForwarder != null">
                    <div v-if="chosenOrders.length == 0">
                        <p>Wysyłka bez przewoźnika</p>
                        <p>(edytuj później)</p>
                    </div>
                    <div v-else>
                        <p><span class="yellow">Tablice rej.: </span>{{chosenForwarder.carPlates}}</p>
                        <p><span class="yellow">Firma: </span>{{chosenForwarder.speditor}}</p>
                        <p><span class="yellow">Kierowca: </span>{{chosenForwarder.firstName}} {{chosenForwarder.lastName}}</p>
                        <p><span class="yellow">Telefon: </span>{{chosenForwarder.phoneNumber}}</p>
                    </div>
                </div>
                <div v-else>
                <div>
                    <p>
                        Nie wybrano przewoźnika
                    </p>
                </div>
            </div>
            </div>
        </div>
    </div>
    <!-- 6th column -->
     <div v-if="chosenForwarder != null" class="forward-arrow done seven-item">
       <span class="material-symbols-outlined">
            task_alt
        </span>
    </div>
    <div v-else class="forward-arrow undone seven-item">
        <span class="material-symbols-outlined">
            unpublished
        </span>
    </div>
    <!-- 7th column -->
    <div v-if="createdId ==''" class="button-ctnr">
        <div class="save-all-btn seven-item" v-if="chosenShipment != null && chosenOrders != null" @click="handleSaveShipmentData">
                Zapisz wysyłkę
        </div>

    </div>
    <div v-else class="button-ctnr">
        <div >
            <p>
                Dane wysyłki zostały zapisane.

            </p>
            
        </div>
    </div>
  </div>
    
    <!-- <div class="row-container-seven bar">
        <div class="uncompleatedbar" :class="{compleatedbar:chosenShipment != null}">
            <span class="material-symbols-outlined lifted">
                forklift
            </span>
        </div>
        <div class="uncompleatedbar" :class="{compleatedbar:chosenShipment != null}"></div>
        <div class="uncompleatedbar" :class="{compleatedbar:chosenOrders != null}"></div>
        <div class="uncompleatedbar" :class="{compleatedbar:chosenOrders != null}"></div>
        <div class="uncompleatedbar" :class="{compleatedbar:chosenForwarder != null}"></div>
        <div class="uncompleatedbar" :class="{compleatedbar:chosenForwarder != null}"></div>
        <div class="uncompleatedbar" :class="{compleatedbar:createdId != ''}"></div>
    </div> -->
    <div class="row-container-seven bar">
        <div v-if="chosenShipment == null" class="uncompleatedbar">
            <span class="material-symbols-outlined lifted">
                forklift
            </span>
        </div>
        <div v-else class="compleatedbar"></div>
        <div v-if="chosenShipment == null" class="uncompleatedbar"></div>
        <div v-else class="compleatedbar endline">
            <span v-if="chosenShipment != null && chosenOrders == null & chosenForwarder == null " class="material-symbols-outlined lifted">
                forklift
            </span>
        </div>


        <div v-if="chosenOrders == null" class="uncompleatedbar">
        </div>
        <div v-else class="compleatedbar"></div>
        <div v-if="chosenOrders == null" class="uncompleatedbar"></div>
        <div v-else class="compleatedbar endline">
            <span v-if="chosenShipment != null && chosenOrders != null && chosenForwarder == null " class="material-symbols-outlined lifted">
                forklift
            </span>
        </div>


        <div v-if="chosenForwarder == null" class="uncompleatedbar">
        </div>
        <div v-else class="compleatedbar"></div>
        <div v-if="chosenForwarder == null" class="uncompleatedbar"></div>
        <div v-else class="compleatedbar endline">
            <span v-if="chosenShipment != null && chosenOrders != null && chosenForwarder !== null && createdId == '' " class="material-symbols-outlined lifted">
                forklift
            </span>
        </div>
        
        <div v-if="createdId == ''" class="uncompleatedbar"></div>
        <div v-else class="compleatedbar endline">
            <span v-if="chosenShipment != null && chosenOrders != null && chosenForwarder !== null && createdId != '' " class="material-symbols-outlined lifted endline">
                local_shipping
            </span>
        </div>
    </div>

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
        <AddShipmentData @new-shipment-event="handleNewShipment"/>
    </div>
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

   <div class="row-container-two" v-if="chosenShipment !=null && chosenOrders != null && chosenForwarder == null">
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
        <div></div>
  </div>
    <div class="row-container-one" v-if="chosenShipment !=null && chosenOrders != null && chosenForwarder != null">
        <div></div>
        <div v-if="!createdId">
            <p>Dane zostały wprowadzone. Zapisz wysyłkę lub wróć do edycji.</p>
        </div>
        
        <div v-if="addShipmentError">
            {{addShipmentError}}
        </div>
        <div v-if="addOrderError">
            {{addOrderError}}
        </div>
        <div></div>
    </div>
</template>

<script>
import { ref } from "vue";

import ChooseOrder from "../components/ChooseOrder.vue";
import NavbarComponent from "../components/NavbarComponent.vue";
import AddShipmentData from "../components/AddShipmentData.vue";
import ChooseForwarder from "../components/ChooseForwarder.vue";
import AddForwarder from "../components/AddForwarder.vue";
import getForwarderById from "../js-components/getForwarderById.js"
//import getShipmentById from "../js-components/getShipmentById.js"
import addShipment from '../js-components/addShipment.js'
import addOrderToShipment from '../js-components/addOrderToShipment.js'
import moment from 'moment'
export default {
  components: { NavbarComponent, ChooseOrder, AddShipmentData, ChooseForwarder, AddForwarder },
  setup() {
    const url = 'https://localhost:44331/api/'
    const {loadForwarder, error:loadForwarderError, forwarder} = getForwarderById(url)
    //const {shipment, error:shipmentError, loadShipment} = getShipmentById(url)
    const chosenForwarder = ref(null)
    const chosenShipment = ref(null)
    const chosenOrders = ref(null)
    const { addNewShipment, error:addShipmentError, createdId} = addShipment(url)
    const {addOrder, error:addOrderError} = addOrderToShipment(url)
    const addedPoNumber = ref("");

    const handleAddOrders = (ordersList) => {
        console.log(ordersList)
      chosenOrders.value = ordersList
    };

    const handleNewShipment = (shipmentData) =>{
        console.log(shipmentData)
        chosenShipment.value = shipmentData
    }

    const handleSaveShipmentData = ()=>{
        
        if(chosenForwarder.value['id']){
                chosenShipment.value['forwarderId'] = chosenForwarder.value['id']
            }
        addNewShipment(chosenShipment.value)
        .then(()=>{
            if(chosenOrders.value.length != 0){
                    chosenOrders.value.forEach(order => {
                        addOrder(createdId.value, order.id)
                    });
            }
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
      handleNewShipment, chosenShipment,
      handleChoosenForwarder, loadForwarderError, chosenForwarder,
      addedPoNumber,
      moment,
      chosenOrders, handleAddOrders,

      addShipmentError, createdId, handleSaveShipmentData,
      addOrderError
    };
  },
};
</script>

<style>
.lifted{
    /* transform: translateY(-1.5vh); */
    
    color: #fff;
    padding: 0.6vh 0.6vh;
    border-radius: 50%;
    border: solid 0.2vh #fff;
    background-color: #14224f;
    box-shadow: 0 0 1vh rgba(0, 0, 0, 0.8);
    transition: 500ms ease-in;
}
.lifted.material-symbols-outlined{
    font-size: 2vh;
}
.endline{
    text-align: right;
}
.uncompleatedbar{
    min-height: 0.8vh;
    max-height: 0.8vh;
    margin: 0;
    background: #364b6c;
    transition: 500ms ease-in;
    /* justify-self: stretch;     */
    overflow: visible;
     display: flex;
    align-items: center;
}
.compleatedbar{
    display: flex;
    align-items: center;
    justify-content: flex-end;
    min-height: 0.8vh;
    max-height: 0.8vh;
    margin: 0;
    background: #42c231;
    transition: 500ms ease-in;
    /* justify-self: stretch; */
}
.color-done{
    background-color: #42c231;
}
.forward-arrow{
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 4vh; 
}
.done{
    color: #42c231;
    }
.undone{
    color: #364b6c;
}
.button-ctnr{
    display:flex;
    justify-content: center;
    align-self: center;
}
.save-all-btn{
    display: flex;
    box-shadow: 0.2vh 0.2vh 0.8vh rgba(0,0,0,0.5);
    border-radius: 0.8vh;
    justify-content: center;
    align-items: center;
    padding: 1.2vh 3vh;
    /* margin: auto; */
    background: linear-gradient(to right bottom, #42c231,#07531e);
    align-self: center;
    cursor: pointer;
}
.save-all-btn:hover{
    background: linear-gradient(to right bottom, #fdd700,#c58001);
    transform: scale(1.05);
    box-shadow: 1vh 1vh 2vh rgba(0,0,0,0.8);
}

.chosen-summary{
    padding:0 6vh 0 6vh;
}
.chosen-summary p{
    font-size: 1.2vh;
    font-weight: 300;
    padding: 0;
    margin:0;

}
.chosen-orders{
    margin-bottom: 0.8vh ;
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
    
    margin: 40px 0;
    padding-bottom: 40px;
}
.row-container-two {
    display: grid;
  grid-template-columns: 1fr 20vw 20vw 1fr;
  gap: 5vh;
  /* max-width: 100vw; */
  /* margin: auto; */
  
    margin: 40px 0;
    padding-bottom: 40px;
}
.row-container-three{
    display: grid;
    grid-template-columns: 30vh 30vh 30vh;
    /* max-width: 50vw; */
    /* margin: auto; */
    
    margin: 25px 0;
    padding-bottom: 25px;
    min-height: 12vh;
    position: relative;
    
}
.row-container-seven{
    display: grid;
    justify-content: stretch;
    justify-items: stretch;
    grid-template-columns: 34vh 7vh 34vh 7vh 34vh 7vh auto;
    /* max-width: 50vw; */
    /* margin: auto; */
    /* border-bottom: solid 1px #fff;  */
    margin: 0;
    padding: 2vh 0vh;
    min-height: 16vh;
    /* position: relative; */
    background: linear-gradient(to right bottom, #272d36,#13182c);
    
}
.seven-item{
    margin: 0 2vh;
}
.bar{
    margin:0;
    min-height: 0;
    padding: 0;

    }
</style>