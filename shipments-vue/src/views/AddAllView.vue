<template>
  <NavbarComponent />
  <Spinner v-if="isPending1 || isPending2" />
  <div class="row-container-seven" >
    <!-- 1st column -->
    <div> 
        <div>
            <div class="chosen-summary">
                <h3>DANE NOWEJ WYSYŁKI</h3>
                <div v-if="chosenShipment != null">
                    <p v-if="chosenShipment.hasPriority"> PRIORYTETOWA</p>
                    <p><span class="yellow">ETD:</span> {{moment(chosenShipment.etd).format("YYYY-MM-DD / HH:mm")}}</p>
                    <p v-if="chosenShipment.containerNumber"><span class="yellow">Nr kontenera: </span>{{chosenShipment.containerNumber}}</p>
                    <p v-if="chosenShipment.containerType"><span class="yellow">Typ kontenera: </span>{{chosenShipment.containerType}}</p>
                    <p v-if="chosenShipment.warehouseArea != null"><span class="yellow">TPA: </span>{{chosenShipment.warehouseArea}}</p>
                    <p><span class="yellow">Ilość palet: </span>{{chosenShipment.palletQty}}</p>
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
    <div v-if="chosenShipment != null" class="forward-arrow done" >
        <span class="material-symbols-outlined edit" >
            task_alt
        </span>
        <span v-if="createdId == ''" class="edit-label" @click="handleEdit(1,0,0)">
            EDYTUJ
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
            <h3>WYBRANE ZAMÓWIENIA</h3>
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
     <div v-if="chosenOrders != null" class="forward-arrow done seven-item" >
        <span class="material-symbols-outlined edit" >
            task_alt
        </span>
        <span v-if="createdId ==''" class="edit-label" @click="handleEdit(0,1,0)">
            EDYTUJ
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
                
                <h3>Wybrany przwoźnik</h3>
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
     <div v-if="chosenForwarder != null" class="forward-arrow done seven-item" >
        <span class="material-symbols-outlined edit" >
            task_alt
        </span>
        <span v-if="createdId == ''" class="edit-label" @click="handleEdit(0,0,1)">
            EDYTUJ
        </span>
    </div>
    <div v-if="chosenForwarder == null" class="forward-arrow undone seven-item">
        <span class="material-symbols-outlined">
            unpublished
        </span>
        {{chosenForwarder}}
    </div>
    <!-- 7th column -->
    <div v-if="createdId ==''" class="button-ctnr">
        <div class="save-all-btn seven-item" v-if="chosenShipment != null && chosenOrders != null && chosenOrders.length != 0" @click="handleSaveShipmentData">
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

  <div class="row-container-one" v-if="editShipment">
    <div>
    </div>
    <div>
        <AddShipmentData @new-shipment-event="handleNewShipment"/>
    </div>
  </div>


  <div class="row-container-one" v-if="!editShipment && editOrders">
    <div>
    </div>
    <div>
      <ChooseOrder @add-orders-event="handleAddOrders" :currentOrders="chosenOrders" />
    </div>
    <div></div>
  </div>

   <div class="row-container-one" v-if="!editShipment && !editOrders && editForwarder">
        <div>
        </div>
        <div>
        <ChooseForwarder @forwarder-chosen-event="handleChoosenForwarder" :currentForwarder="chosenForwarder" />
        </div>
        <div></div>
  </div>
    <div class="row-container-two larger" v-if="chosenShipment !=null && chosenOrders != null && chosenForwarder != null">
        <div></div>
        <div v-if="!createdId && !editShipment && !editOrders && !editForwarder">
            <p>Dane zostały wprowadzone.</p>
            <p> Zapisz wysyłkę lub edytuj.</p>
        </div>
        <div v-if="createdId && !editShipment && !editOrders && !editForwarder">
            <div class="chosen-summary bigger">
                <div>
                    <h2>Podsumowanie:</h2>
                </div>
                <div>
                    <p>
                        <span class="yellow">ETD: </span>
                        {{moment(chosenShipment.etd).format("YYYY-MM-DD / HH:mm")}}
                    </p>
                    <p>
                        <span class="yellow">Nr kontenera: </span>
                        {{chosenShipment.containerNumber}}
                    </p>
                    <p>
                        <span class="yellow">Nr plomby: </span>
                        {{chosenShipment.containerSealNumber}}
                    </p>
                    <p>
                        <span class="yellow">Typ kontenera: </span>
                        {{chosenShipment.containerType}}
                    </p>
                    <p>
                        <span class="yellow">Komentarz: </span>
                        <span v-if="chosenShipment.comment != ''"> {{chosenShipment.comment}}</span>
                                    <span v-else> Brak</span>
                    </p>
                    <p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p>
                    <h3>Zamówienia:</h3>
                    <div v-for="order in chosenOrders" :key="order.id">
                        <p></p>
                        <p>
                            <span class="yellow">PO#: </span>
                            {{order.poNumber}}
                        </p>
                        <p>
                            <span class="yellow">Kategoria: </span>
                            {{order.category}}
                        </p>
                        <p>
                            <span class="yellow">Klient: </span>
                            {{order.customerName}}
                        </p>
                       
                       
                    </div>
                    <div>
                        <p></p><p></p><p></p><p></p><p></p><p></p><p></p><p></p>
                        <h3>Przewoźnik:</h3>
                        <p>
                            <span class="yellow">Spedycja: </span>
                            {{chosenForwarder.speditor}}
                        </p>
                        <p>
                            <span class="yellow">Tablice rejestracyjne: </span>
                            {{chosenForwarder.carPlates}}
                        </p>
                        <p>
                            <span class="yellow">Kierowca: </span>
                            {{chosenForwarder.firstName}} {{chosenForwarder.lastName}}
                        </p>
                        <p>
                            <span class="yellow">Nr telefonu: </span>
                            {{chosenForwarder.phoneNumber}}
                        </p>
                    </div>

                </div>
            </div>
        </div>
        <div class="chosen-summary bigger" v-if="chosenOrders != null && chosenOrders.length != 0 && !editShipment && !editOrders && !editForwarder">
            <div>
                <h2>Transporty wybranych zamówień</h2>
                <div v-for="order in chosenOrders" :key="order.id">
                    <p ></p>
                    
                    <div class="summary-head">
                        <span class="material-symbols-outlined">
                            description   
                        </span>
                        <h4>Zamówienie</h4>
                    </div>
                    <p>
                        <span class="yellow">PO#: </span>
                        {{order.poNumber}}
                    </p>
                    <p>
                        <span class="yellow">Klient: </span>
                        {{order.customerName}}
                    </p>
                        <div v-if="order.shipments.length != 0" class="summary-head">
                            <span class="material-symbols-outlined">
                                local_shipping
                            </span>
                            <h4>Wysyłki</h4>
                        </div>
                        <div v-else class="summary-head">
                            <span class="material-symbols-outlined">
                                block
                            </span>
                            <h4>brak wysyłek</h4>
                        </div>
                    <div v-for="poShipment in order.shipments" :key="poShipment.id">
                        <p>
                            <span class="yellow">ETD: </span>
                            {{moment(poShipment.etd).format("YYYY-MM-DD / HH:mm")}}
                        </p>
                        <p>
                            <span class="yellow">ATD: </span>
                            {{moment(poShipment.atd).format("YYYY-MM-DD / HH:mm")}}
                        </p>
                        <p>
                            <span class="yellow">Nr kontenera: </span>
                            {{poShipment.containerNumber}}
                        </p>
                        <p style="padding-bottom: 1vh">
                            <span class="yellow">Ilość palet: </span>
                            {{poShipment.palletQty}}
                        </p>
                    </div>
                    
                    
                </div>
            </div>
        </div>
        
        <div v-if="addShipmentError">
            {{addShipmentError}}
        </div>
        <div v-if="addOrdersError">
            {{addOrdersError}}
        </div>
        <div></div>
    </div>
</template>

<script>
import { ref } from "vue";
import { useLinksStore } from '../stores/linksStore.js'
import ChooseOrder from "../components/ChooseOrder.vue";
import NavbarComponent from "../components/NavbarComponent.vue";
import AddShipmentData from "../components/AddShipmentData.vue";
import ChooseForwarder from "../components/ChooseForwarder.vue";
import addShipment from '../js-components/addShipment.js'
import addManyOrdersToShipment from '../js-components/addManyOrdersToShipment.js'
import moment from 'moment'
import Spinner from '../components/SpinnerComponent.vue';
export default {
  components: { NavbarComponent, ChooseOrder, AddShipmentData, ChooseForwarder, Spinner },
  setup() {
    const linksStore = useLinksStore()

    //const {shipment, error:shipmentError, loadShipment} = getShipmentById(url)
    const chosenForwarder = ref(null)
    const chosenShipment = ref(null)
    const chosenOrders = ref(null)
    const { addNewShipment, error:addShipmentError, createdId, ispending:isPending1} = addShipment( linksStore.url)
    // const {addOrder, error:addOrderError, isPending:isPending2} = addOrderToShipment(linksStore.url)
    const {addOrders, error:addOrdersError, isPending:isPending2} = addManyOrdersToShipment(linksStore.url)
    const addedPoNumber = ref("");
    
    const editShipment = ref(true)
    const editOrders = ref(false)
    const editForwarder = ref(false)

    const handleNewShipment = (shipmentData) =>{
        chosenShipment.value = shipmentData
        editShipment.value = false
        console.log(chosenOrders.value)
        if(chosenOrders.value == null){editOrders.value = true}
        if(chosenForwarder.value == null && chosenOrders.value != null){editForwarder.value = true}
    }

    const handleAddOrders = (ordersList) => {
      chosenOrders.value = ordersList
      editOrders.value = false
      if(chosenForwarder.value == null){editForwarder.value = true}
    };

    const handleChoosenForwarder = (newForwarder)=>{
        chosenForwarder.value = newForwarder
        editForwarder.value = false
    }

    const handleEdit = (shipment, order, forwarder)=>{
        editShipment.value = shipment
        editOrders.value = order
        editForwarder.value = forwarder
    }
    
    const handleSaveShipmentData = ()=>{
        
        if(chosenForwarder.value){
                chosenShipment.value['forwarderId'] = chosenForwarder.value['id']
            }
        chosenShipment.value.etd = moment(chosenShipment.value.etd).format("YYYY-MM-DDTHH:mm")
        if(chosenShipment.value.timeOfDeparture != null){
            chosenShipment.value.timeOfDeparture = moment(chosenShipment.value.timeOfDeparture).format("YYYY-MM-DDTHH:mm")
        }
        
        addNewShipment(chosenShipment.value)
        .then(()=>{
            if(chosenOrders.value.length != 0){
                        // chosenOrders.value = chosenOrders.value.toReversed()
                        var newOrdersIds = chosenOrders.value.map(order => order.id)
                        addOrders(createdId.value, newOrdersIds)
            }
        })
            
    }


    // watch(forwarder,()=>{
    //     console.log(forwarder)
    // })

    return {
      isPending1, isPending2,
      handleNewShipment, chosenShipment,
      handleChoosenForwarder, chosenForwarder,
      addedPoNumber,
      moment,
      chosenOrders, handleAddOrders,
      addShipmentError, createdId, handleSaveShipmentData,
      addOrdersError,
      editShipment, editOrders, editForwarder, handleEdit
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
.material-symbols-outlined.edit{
        display: flex;
        font-size: 4.5vh;
        color: #42c231 ;
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
    flex-direction: column;
    justify-content: center;
    align-items: center;
    font-size: 3vh; 
}
.forward-arrow .edit-label:hover{
    color: #f8f8f8;
    cursor: pointer;
}

.done{
    color: #42c231;
    }

.edit-label{
    font-size: 1.2vh;
    border: solid 0.25vh;
    border-radius: 1vh;
    padding: 0.5vh 1.0vh;
    margin-top: 1vh;
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
.chosen-summary .summary-head{
    display: flex;
    align-items: center;
    margin: 1vh 0;
}
.chosen-summary .summary-head h4{
    margin: 0 0 0 1vh;
    color: #eeeeee
}
.chosen-summary .summary-head span{
    font-size: 2vh;
}

.chosen-summary .underlined-para{
    border-bottom: solid #fff 1px;
    
}
.chosen-summary.bigger p{
    font-size: 1.4vh;
    padding: 0.1vh 0;
}
.chosen-orders{
    margin-bottom: 0.8vh ;
}
.yellow{
    color: #e6b800;
    font-weight: 400;
}
.chosen-summary h2, .chosen-summary h3{
    color: #f0f0f0;
    margin-top: 0;
    margin-bottom: 1vh;
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
    justify-items: center;
    margin: 2vh 0;
    padding-bottom: 40px;
}
.row-container-two {
    display: grid;
    grid-template-columns: 1fr 20vw 20vw 1fr;
    gap: 5vh;
    margin: 40px 0;
    padding-bottom: 40px;
}
.row-container-two.larger{
    grid-template-columns: 1fr 30vw 30vw 1fr;
}
.row-container-three{
    display: grid;
    grid-template-columns: 30vh 30vh 30vh;
    margin: 25px 0;
    padding-bottom: 25px;
    min-height: 12vh;
}
.row-container-seven{
    display: grid;
    justify-content: stretch;
    justify-items: stretch;
    grid-template-columns: 34vh 7vh 34vh 7vh 34vh 7vh auto;
    margin: 0;
    padding: 2vh 0vh;
    min-height: 10.3vh;
    /* background: #00000085 */
    transition: 300ms ease-in-out;
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