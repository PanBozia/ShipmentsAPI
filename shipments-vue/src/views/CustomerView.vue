<template>
   <div>
     <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>KLIENCI</p>
        </div>
        <div class="item-a item-container" @click="openAddComponent">
            <p><span class="material-symbols-outlined">add_box</span></p>
            <p>Dodaj klienta</p>
        </div>
        <div class="item-d item-container" @click="openListComponent">
            <p><span class="material-symbols-outlined">list_alt</span></p>
            <p>Lista klientów</p>
        </div>
        <div v-if="listFlag" class="item-v">
            <CustomersList/>
        </div>
        <div v-if="addFlag" class="item-v">
            <AddCustomer/>
        </div>
  </div>
  </div>
</template>

<script>
import { ref } from 'vue'

import CustomersList from "../components/CustomersList.vue"
import AddCustomer from "../components/AddCustomer.vue"

export default {
    components: {CustomersList, AddCustomer},
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