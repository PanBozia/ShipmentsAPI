<template>
<NavbarComponent />
  <div class="total-container">
    <div></div>
    <div>
        <ChooseOrder :addedPoNumber="addedPoNumber" />
    </div>
    <div>
        <AddPurchaseOrder @order-added-event="handleAddedOrder" />
    </div>
    <div>
        <div v-if="addedPoNumber"></div>
    </div>
    <div></div>
  </div>
</template>

<script>
import { ref } from 'vue'
import AddPurchaseOrder from '../components/AddPurchaseOrder.vue'
import ChooseOrder from '../components/ChooseOrder.vue'
import NavbarComponent from '../components/NavbarComponent.vue'


export default {
    
    components:{AddPurchaseOrder, NavbarComponent,ChooseOrder},
    setup(){
        const addedPoNumber = ref('')
        const handleAddedOrder = (orderNumber)=>{
            //console.log(orderNumber)
            addedPoNumber.value = orderNumber
        }
        
        return{
            handleAddedOrder,
            addedPoNumber
            }
    }

}
</script>

<style>
.total-container{
    display: grid;
    grid-template-columns: 1fr 2fr 2fr 2fr 1fr;
    gap: 5vh;
}
</style>