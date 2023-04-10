<template>
    <NavbarComponent />
   <div class="frame">
     <div class="shipment-view-container" v-if="shipment">
            <div class="item-header">
                <h2>{{moment(shipment.etd).format("YYYY MMMM DD - hh:mm")}}</h2>
            </div>
            <div class="item-main">
                {{shipment}}
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
    grid-template-columns: 10vh 20vh 20vh 20vh 20vh 10vh ;
    grid-template-areas:
    ". header header header header ."
    ". . main main . .";
    margin-top: 20px;
}
.item-header {
    grid-area: header;
    display: flex;
    justify-content: flex-end;
}
.item-main {
    grid-area: main;
    display: flex;
    justify-content: flex-end;
}

</style>