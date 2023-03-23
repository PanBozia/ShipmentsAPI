<template>
<NavbarComponent />
  <div class="total-container">
    <div></div>
    <div>
        <AddPurchaseOrder/>
    </div>
    <div>
        <ChooseOrder/>
    </div>
    <div></div>
    <div></div>
  </div>
</template>

<script>
import AddPurchaseOrder from '../components/AddPurchaseOrder.vue'
import ChooseOrder from '../components/ChooseOrder.vue'
import NavbarComponent from '../components/NavbarComponent.vue'


export default {
    components:{AddPurchaseOrder, NavbarComponent,ChooseOrder}

}
</script>

<style>
.total-container{
    display: grid;
    grid-template-columns: 1fr 2fr 2fr 2fr 1fr;
}
</style>