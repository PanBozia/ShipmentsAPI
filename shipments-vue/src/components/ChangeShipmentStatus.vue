<template>
    <div class="status-screen">
        <div   class="status-ctnr" >
            <h2>wybierz status</h2>
            <div class="status-ctnr-item" v-for="status in statuses" :key="status.id"  >
                <StatusComponent  :statusName="status.name" @click="handleEmitStatusChangeEvent(status)"/>

            </div>    
            <div class="done-date" >
                <p>Jeżeli wybierasz status "Zrealizowana", musisz wybrać datę i godzinę wyjazdu:</p>
                <input type="datetime-local" v-model="dateOfDepartureForm">
            </div>
            <div v-if="departureError">
                {{departureError}}
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
import { onBeforeMount, ref } from 'vue'
import changeStatus from '../js-components/changeStatus.js'
import changeDepartureTime from '../js-components/changeDepartureTime.js'

import getStatuses from '../js-components/getStatuses.js'
import StatusComponent from '../components/StatusComponent.vue'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'

export default {
props:['shipmentId'],
components:{StatusComponent},
setup(props, context){
    const linksStore = useLinksStore()
    const { change, error} = changeStatus(linksStore.url)
    const { loadStatuses, error:loadError, statuses} = getStatuses(linksStore.url)
    const {changeDeparture, error:departureError} = changeDepartureTime(linksStore.url)
    const dateOfDepartureForm = ref(moment(new Date()).format("YYYY-MM-DDThh:mm"))
    
    onBeforeMount(()=>{
        loadStatuses()
    })

    const handleEmitStatusChangeEvent = (status)=>{
        if(status.name != 'Zrealizowana'){
            changeDeparture(props.shipmentId, null).then(()=>{
                change(props.shipmentId, status.id).then(()=>{
                    context.emit('changeStatusEvent', true)
                })

            })
        }else{
            changeDeparture(props.shipmentId, moment(dateOfDepartureForm.value).format("YYYY-MM-DDThh:mm")).then(()=>{
                change(props.shipmentId, status.id).then(()=>{
                    context.emit('changeStatusEvent', true)
                })
            })
        }
    }
    const handleExit = ()=>{    
        context.emit('changeStatusEvent', false)
    }

    return {
        error, loadError, departureError,
        statuses,
        handleEmitStatusChangeEvent, handleExit,
        dateOfDepartureForm
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
.status-ctnr .done-date{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 2vh;
}
.status-ctnr .done-date p{
    padding: 0 6.0vh;
    font-size: 1.2vh;
    text-align: center;
}
.status-ctnr input{
    font-family: 'Poppins', sans-serif;
    background-color: #585858;
    color: #fff;
    padding: 0.6vh;
    font-size: 1.3vh;
    text-align: center;
    width: 20.8vh;
    
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