<template>
<NavbarComponent />
<div class="frame">
    <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>ZAMÓWIENIA</p>
        </div>
        <div class="item-a" >
            <div class="item-container main-list-btn" @click="openListComponent">
                <p><span class="material-symbols-outlined">list_alt</span></p>
            </div>
            <div class="item-container main-add-btn" @click="openAddComponent">
                <p><span class="material-symbols-outlined">add</span></p>
            </div>
        </div>
        <div v-if="listFlag" class="item-v">
            <PurchaseOrdersList/>
        </div>
        <div v-if="addFlag" class="item-v">
            <AddPurchaseOrder/>
        </div>
    </div>
</div>
</template>

<script>
import { ref } from 'vue'
import NavbarComponent from '../components/NavbarComponent.vue'
import PurchaseOrdersList from "../components/PurchaseOrdersList.vue"
import AddPurchaseOrder from "../components/AddPurchaseOrder.vue"

export default {
    components: {PurchaseOrdersList, AddPurchaseOrder, NavbarComponent},
    setup(){
        const listFlag = ref(true);
        const addFlag = ref(false);
        
        const openListComponent = ()=>{
            addFlag.value = false
            listFlag.value = true
        }
        const openAddComponent = ()=>{
            listFlag.value = false
            addFlag.value = true
        }
        return{ listFlag, addFlag, openListComponent, openAddComponent }                          
                
    }

}
</script>

<style>

</style>