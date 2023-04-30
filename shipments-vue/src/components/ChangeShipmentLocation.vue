<template>
    <div class="area-screen">
        <div   class="area-ctnr" >
            <h2>wybierz lokację</h2>
            <!-- <div class="area-ctnr-items">
                <div class="area-ctnr-item" v-for="area in areas" :key="area.id"  >
                    <div>
                        <p>{{area.name}}</p>
                    </div>
                </div>    
            </div> -->
            <div class="locations" v-if="areas">
                    <div class="location-ctnr noarea" @click="handleChangeArea(null)">
                        <div><p>BRAK</p></div>
                        <div>
                            <img class="image" src="../assets/img/EUROPAL.png" alt="" srcset="">
                        </div>
                    </div>
                    <div 
                        class="location-ctnr" 
                        :class="{otherLocation:location.name[0] != 'T'}" 
                        v-for="location in areas" :key="location.id" 
                        @click="handleChangeArea(location.id)">
                        
                        <div>
                            <p>
                                {{location.name}}
                            </p>
                        </div>
                        <div>
                            <img class="image" src="../assets/img/EUROPAL.png" alt="" srcset="">
                        </div>
                    </div>
                </div>


            <div v-if="areasError">
                {{areasError}}
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
import getAreas from '../js-components/getAreas.js'
import { useLinksStore } from '../stores/linksStore.js'
import changeArea from '../js-components/changeArea.js'
export default {
props:['shipmentId'],
emits:['changeLocationEvent'],
setup(props, context){
    const linksStore = useLinksStore()
    const { loadAreas, areas, error:areasError} = getAreas(linksStore.url)
    const {change, error:changeError} = changeArea(linksStore.url)
    
    onBeforeMount(()=>{
        loadAreas()
    })

   const handleChangeArea = (areaId)=>{
        change(props.shipmentId, areaId).then(()=>{
            if (!changeError.value || changeError.value !=''){
                context.emit('changeLocationEvent', true)
            }
        })
   }
    const handleExit = ()=>{    
        context.emit('changeLocationEvent', false)
    }

    return {
        //handleEmitAreasChangeEvent
        handleChangeArea, handleExit,
        areas, areasError
    }
}
}
</script>

<style>
.area-screen{
 position: absolute;
    width: 100%;
    height: 100%;
    background: #14141480;
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.area-ctnr{
    display: block;
    width: 68vh;
    margin:16vh auto 1vh auto;
    padding: 1.5vh 0vh;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
}
.area-ctnr .locations{
    justify-content: center;
}
.area-ctnr .locations .location-ctnr{
    cursor: pointer;
    transition: 100ms ease-in-out;
}
.area-ctnr .locations .location-ctnr.noarea{
    background: linear-gradient(to bottom right, #2e2e2e, #1f1f1f);
}
.area-ctnr .locations .location-ctnr:hover{
    cursor: pointer;
    box-shadow: 0.8vh 0.8vh 1.2vh #00000080;
    transform: scale(1.1);
    transition: 100ms ease-in-out;
}
.area-ctnr-items{
    display: flex;
    flex-wrap: wrap;
    margin: 1vh 3vh;
}
.area-ctnr-items .area-ctnr-item{
    width: 4vh;
    height: 4vh;
    margin: 0.3vh 0.5vh;
    padding: 1vh;
    border: solid #ddd 2px;
    background: linear-gradient(to bottom right, #07294a, #192633);
    cursor: pointer;
}
.area-ctnr-items .area-ctnr-item p{
    margin: 0;
    padding: 0;
    font-size: 1.1vh;
}


.area-ctnr h2{
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