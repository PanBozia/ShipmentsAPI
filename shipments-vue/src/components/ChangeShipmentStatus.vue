<template>
  <div>
        <form class="form-add" @submit.prevent="handleSubmit">
            <div class="choose-order-container">
                <div class="form-set"> 
                    <div>
                        <label class="form-labels">Status</label>
                    </div>
                    <div class="double-in">
                        <select v-model="statusIdForm">
                            <option :value="status.id" v-for="status in statuses" :key="status.id">
                                {{status.name}}
                            </option>
                        </select>
                    </div>
               
                </div>
           </div>
        </form>
    </div>
</template>

<script>
import { ref } from 'vue'
import changeStatus from '../js-components/changeStatus.js'
export default {
props:['shipmentId'],
setup(props){
    const url = 'https://localhost:44331/api/'
    const { change, error} = changeStatus(url)
    const statusIdForm = ref(1)

    const handleSubmit = ()=>{
        change(props.shipmentId, statusIdForm.value)
    }


    return {
        handleSubmit,
        error,
        statusIdForm
    }
}
}
</script>

<style>

</style>