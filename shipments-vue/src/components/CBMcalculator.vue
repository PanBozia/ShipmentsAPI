<template>
    <div class="area-screen">
        <div   class="calc-ctnr" >
            <h2>Kalkulator objętości</h2>
            
            <div>
                <form class="cbmform" @submit.prevent="">
                    
                    <div class="dim labels">

                            <label>LP</label>
                            <label>Długość</label>
                            <label>Szerokość</label>
                            <label>Wysokość</label>
                            <label>Ilość palet</label>
                    </div>
                        
                    <div class="dim" v-for="(pal, index) in listOfPallets" :key="pal">
                        <p>{{ index + 1 }}</p>
                        <input class="dis" type="number" v-model="pal.dimX">
                        <input class="dis" type="number" v-model="pal.dimY">
                        <input class="dis" type="number" v-model="pal.dimH">
                        <input class="dis" type="number" v-model="pal.qty">
                    </div>
                    <div>
                    </div>
                    <div class="cbm-ctnr" v-if="totalCbm > 0">
                        <p class="cbmsum">
                            <span class="material-symbols-outlined">deployed_code</span>
                            <span class="txt">{{ totalCbm }} m</span>
                            <span class="txt upper">3</span>
                        </p>
                    </div>
                    <div v-else class="cbm-ctnr">
                        <p class="cbmsum"></p>
                    </div>
                    <div class="dimbtn">
                        <div class="btn" @click="addToList">+</div>
                        <div class="btn btnr" @click="removeFromList">-</div>
                    </div>
                    
                        
                </form>
            </div>
            <div class="bottombuttons">
                
                <div class="btn  btn-save" @click="handleEmitCalculation">
                    <p>
                        Zapisz wynik
                    </p>
                </div>
                <div class=" btn exit-btn" @click="handleExit">
                    <p>
                        Anuluj
                    </p>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import { useLinksStore } from '../stores/linksStore.js'
import { watchEffect,onBeforeMount, ref, onUnmounted} from 'vue'
export default {
emits:['calculateCbmEvent'],
setup(props, context){
    const linksStore = useLinksStore()
    
    const totalCbm = ref(0)
    const listOfPallets = ref([])
    
    class Pallet {
        constructor(dimX,dimY,dimH,qty){
            this.dimX = dimX
            this.dimY = dimY
            this.dimH = dimH
            this.qty = qty

        }
    }
       

    const addToList = ()=>{
        listOfPallets.value.push(new Pallet(1200,800,1000,1))
        totalCbm.value = Number(0).toFixed(2)
    }
    const removeFromList = ()=>{
        listOfPallets.value.pop()
        totalCbm.value = Number(0).toFixed(2)
    }

    onBeforeMount(()=>{
        addToList()
        })

    const handleCalculation = ()=>{
        var cbm = 0
        listOfPallets.value.forEach(pal => {
            cbm = cbm + (pal.dimX/1000)*(pal.dimY/1000)*(pal.dimH/1000)*(pal.qty)
        });
        totalCbm.value = Number(cbm).toFixed(2)
    }
    
    const handleExit = ()=>{    
        context.emit('calculateCbmEvent', false)
    }
    const handleEmitCalculation = ()=>{   
        handleCalculation() 
        context.emit('calculateCbmEvent', totalCbm.value)
    }
    const formWatchCbm = watchEffect( ()=>{
            handleCalculation()
        })

    onUnmounted(()=>{
            formWatchCbm()
        })

    return {
        handleExit,
        totalCbm,
        listOfPallets, addToList, removeFromList,
        handleCalculation, handleEmitCalculation
        
    }
}
}
</script>

<style>

.calc-ctnr{
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 400px;
    margin:16vh auto 1vh auto;
    padding: 1.5vh 2vw;
    background: linear-gradient(to right bottom, #232323, #383838);
    border:solid 0.3vh #ebebeb;
    box-shadow: 0.6vh 0.6vh 1.0vh #000000dd;
}

.calc-ctnr h2{
    padding: 0;
    margin: 1.4vh  0;
    text-align: center;
}
.calc-ctnr .cbmform{
    margin: 2vh 0vh;
}
.calc-ctnr .dim{
    display: grid;
    grid-template-columns: 24px 70px 70px 70px 70px;
    justify-content: center;
}
.dim p{
    font-size: 11px;
    margin-top: 0.4vh;
    font-weight: 300;
    
}
.calc-ctnr .labels{
    margin-bottom: 0.4vw;
    
}
.calc-ctnr .dim-ctnr{
    display: flex;
    flex-direction: column;
}
.calc-ctnr label{
    padding: 0;
    margin: 0.8vh 0;
    font-size: 11px;
    text-align: left;
    font-weight: 300;
}
.calc-ctnr input{
    height: 2.3vh;
    width: 44px;
    padding-left: 10px ;
    font-size: 12px;
    
}
.dimbtn{
    display: grid;
    grid-template-columns: 70px 70px;
    justify-items: center;
    justify-content: center;
    align-items: center;
    margin: 30px 0px;
}
.dimbtn .btn{
    width: 16px;
    height: 34px;
    font-size: 30px;
    font-weight: 800;
    background: #333;
    border: 1px solid #ddd;

    /* border-radius: 50%; */
}
.dimbtn .btn:hover{
    box-shadow: 8px 8px 10px #00000050;
    transform: scale(1.1);
}
.bottombuttons{
    display: flex;
}
/* .bottombuttons .btn p{
    padding: 0;
    margin: 0;
} */
.bottombuttons .exit-btn, .bottombuttons .btn-save{
    width: 90px;
    color: #fff;
    margin: 10px 10px;
    cursor: pointer;
    padding: 0px 6px;
}


.bottombuttons .exit-btn{
    background: rgb(214, 15, 15);
}
.bottombuttons .btn-save:hover{
    box-shadow: 0.6vh 0.6vw 1.2vh #00000050;
    transform: scale(1.08);
}
.cbm-ctnr{
    height: 48px;
    margin: 20px auto;
}

.cbmsum{
    display: flex;
    align-self: center;
    justify-content: center;
    font-size: 20px;
    padding-top: 7px;
}
.cbmsum .material-symbols-outlined{
    margin-right: 12px;
}
.cbmsum .txt{
    font-size: 26px;
    padding-top: 2px ;
}

.cbmsum .upper{
    font-size: 16px;
    font-variant: small-caps;
    vertical-align: super;
    
}


</style>