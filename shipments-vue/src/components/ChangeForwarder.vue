<template>
   <div class="forwarder-screen">
        <div class="forwarder-ctnr">
            <h2>wybierz przewoźnika</h2>
            <div class="forwarder-ctnr-inner">
                    <ChooseForwarder @forwarder-chosen-event="getForwarderId" :currentForwarder="props.currentForwarder" />
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
    import changeForwarder from '../js-components/changeForwarder.js'
    import ChooseForwarder from './ChooseForwarder.vue'
    import { useLinksStore } from '../stores/linksStore.js'
    export default {
    emits:['changeForwarderEvent'],
    props:['shipmentId','currentForwarder'],
    components:{ChooseForwarder},
    setup(props, context){
        const linksStore = useLinksStore()
        const {change, error} = changeForwarder(linksStore.url)
       
        const getForwarderId = (newForwarder)=>{
           
            change(props.shipmentId, newForwarder.id).then(()=>{
                context.emit('changeForwarderEvent', true)
            })
        }
        const handleExit = ()=>{    
            context.emit('changeForwarderEvent', false)
        }

        return {
            error, props,
            getForwarderId, handleExit
        }
    }
    }
</script>

<style>
.forwarder-screen{
 position: absolute;
    width: 100%;
    height: 100%;
    background: #14141480;
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.forwarder-ctnr{
    display: block;
    width: 80vh;
    /* height: 72vh; */
    margin:16vh auto 1vh auto;
    padding: 1.5vh 0vh;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
}
.forwarder-ctnr h2{
    padding: 0;
    margin: 1.4vh  0 3vh 0;
    text-align: center;
}
.forwarder-ctnr-inner{
    display: grid;
    justify-items: center;
}
.forwarder-ctnr-inner .add-single-form{
    width: 100%;
}
</style>