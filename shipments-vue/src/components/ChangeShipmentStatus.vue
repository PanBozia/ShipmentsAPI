<template>
    <div class="status-screen">
        <div class="status-ctnr">
            <h2>wybierz status</h2>
            <div v-for="status in statuses" :key="status.id" @click="handleEmitStatusChangeEvent(status.id)" >
                <StatusComponent  :statusName="status.name" />
            </div>    
            <div class="exit-btn" @click="handleExit">
                <p>
                    Anuluj
                </p>
            </div>
        </div>
    </div>
</template>

<script>
import { onBeforeMount } from 'vue'
import changeStatus from '../js-components/changeStatus.js'
import getStatuses from '../js-components/getStatuses.js'
import StatusComponent from '../components/StatusComponent.vue'
export default {
props:['shipmentId'],
components:{StatusComponent},
setup(props, context){
    const url = 'https://localhost:44331/api/'
    const { change, error} = changeStatus(url)
    const { loadStatuses, error:loadError, statuses} = getStatuses(url)
    
    onBeforeMount(()=>{
        loadStatuses()
    })

    const handleEmitStatusChangeEvent = (statusId)=>{
        change(props.shipmentId, statusId).then(()=>{
            context.emit('changeStatusEvent', true)
        })
    }
    const handleExit = ()=>{    
        context.emit('changeStatusEvent', false)
    }

    return {
        error, loadError,
        statuses,
        handleEmitStatusChangeEvent, handleExit
    }
}
}
</script>

<style>
.status-screen{
 position: absolute;
    width: 100%;
    height: 100%;
    background: #14141480;
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.status-ctnr{
    display: block;
    width: 36vh;
    margin:16vh auto 1vh auto;
    padding: 1.5vh 0vh;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
    

}
.status-ctnr h2{
    padding: 0;
    margin: 1.4vh  0;
    text-align: center;
}
.exit-btn{
    width: 9.5vh;
    background-color: #ff4141;
    color:#fff;
    margin: 2vh auto;
    cursor: pointer;
}
.exit-btn:hover{
    transform: scale(1.05);
    box-shadow: 0.3vh 0.3vh 0.5vh #00000090  ;
}
.exit-btn p{
    text-align: center;
    padding: 0.6vh 0.4vh;
    font-size: 1.3vh;
}

</style>