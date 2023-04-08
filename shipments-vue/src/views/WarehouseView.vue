<template>
<NavbarComponent />
   <div class="frame">
     <div class="view-container">
        <div class="sub-page-header item-e location-header">
            <p>Lokacje magazynowe</p>
        </div>
         <div class="item-a" >
        </div>
        <div  class="item-v">
            <div class="locations-grid">
                <div class="locations" v-if="locations">
                    <div class="location-ctnr" :class="{otherLocation:location.name[0] != 'T'}" v-for="location in locations" :key="location.id">
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
            
             <!-- add location -->
                <div class="location-form">
                    <form class="form-add add-single-form" @submit.prevent="handleSubmit">
                        <h2>Dodaj nową lokację</h2>
                        <div class="form-set">
                            <label class="form-labels">Nazwa</label>
                            <input type="text" v-model="nameForm" required>
                        </div>
                        <div id="add-btn-container">
                            <button>Zapisz</button>
                        </div>
                        <div v-if="loadError || addError" class="error" >
                            <p>{{loadError}}</p>
                            <p>{{addError}}</p>
                        </div>
                        <div v-if="createdFlag" class="success">
                            <p>Lokacja została zapisana.</p>
                        </div>
                        
                    </form>

                </div>
            </div>

        </div>
        
  </div>
  </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'

import NavbarComponent from '../components/NavbarComponent.vue'
import getWarehouseLocations from '../js-components/getWarehouseLocations.js'
import addWarehouseLocation from '../js-components/addWarehouseLocation.js'
export default {
    components: {NavbarComponent},
    setup(){
        const url = 'https://localhost:44331/api/'
        const {loadLocations, error:loadError, locations} = getWarehouseLocations(url)
        const {addNewLocation, error:addError} = addWarehouseLocation(url)
        const nameForm = ref('')
        const createdFlag = ref(false)
        onBeforeMount(()=>{
            loadLocations()
        })
        
        const handleSubmit = () =>{
            const submitedName = {
                name: nameForm.value
            }
            addNewLocation(submitedName).then(()=>{
                loadLocations()
            })
            createdFlag.value = true
        }

        return{ loadError, addError,  locations, handleSubmit, createdFlag, nameForm }
                
    }

}
</script>

<style>
.location-header{
    margin-top: 1.4vh;
    margin-bottom: 2vh;
}
.locations-grid{
    display: grid;
    grid-template-columns: 40vh 45vh;
}
.locations{
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    align-content: flex-start;
    
}
.location-ctnr{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    width: 10vh;
    background: linear-gradient(to right bottom, #453d25,#211d0e);
    
    margin: 1vh;
    /* box-shadow: 0.5vh 0.5vh 0.8vh #00000050; */
    border: solid 2px #fff;
}
.location-form{
    align-self: start;
}
.otherLocation{
    background: linear-gradient(to right bottom, #253b45,#0e1a21);
    }
.location-ctnr p{
    font-size: 2vh;
    margin: 1vh 0 0 0;
}
.image{
    width: 8vh;
    margin-bottom: 1vh;
}
</style>