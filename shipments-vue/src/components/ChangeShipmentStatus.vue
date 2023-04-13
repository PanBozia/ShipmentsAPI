<template>
    <div>
        <div class="status-ctnr" v-for="status in statuses" :key="status.id">
            <StatusComponent v-model="status.name" :statusName="status.name" />
        </div>    
    </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import changeStatus from '../js-components/changeStatus.js'
import getStatuses from '../js-components/getStatuses.js'
import StatusComponent from '../components/StatusComponent.vue'
export default {
props:['shipmentId'],
components:{StatusComponent},
setup(props){
    const url = 'https://localhost:44331/api/'
    const { change, error} = changeStatus(url)
    const { loadStatuses, error:loadError, statuses} = getStatuses(url)
    
    const statusIdForm = ref(1)

    onBeforeMount(()=>{
        loadStatuses()
    })

    const handleSubmit = ()=>{
        change(props.shipmentId, statusIdForm.value, statuses, loadError)
    }


    return {
        handleSubmit,
        error,
        statusIdForm,
        statuses
    }
}
}
</script>

<style>

</style>