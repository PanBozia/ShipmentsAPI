<template>
    <NavbarWarehouse />
   <div class="frame">
     <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>WYSYŁKI</p>
        </div>
        <div class="item-a" >
            
        </div>
        <div class="item-v">
            <ShipmentsList :userIsOffice="false"/>
        </div>
  </div>
  </div>
</template>

<script>

import NavbarWarehouse from '../components/NavbarWarehouse.vue'
import ShipmentsList from "../components/ShipmentsList.vue"
import { useRouter } from 'vue-router'
export default {
    components: {ShipmentsList, NavbarWarehouse },
    setup(){
        const router = useRouter();
        
        const openAddComponent = ()=>{
            router.push({ name:'AddAllView' })
        }
        return{ openAddComponent }                          
                
    }

}
</script>

<style>

</style>