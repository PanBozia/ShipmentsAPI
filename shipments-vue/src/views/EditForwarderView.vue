<template>
   <NavbarComponent />
  
  <div class="frame">
     <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>PRZWOŹNICY</p>
        </div>
        <div class="item-a" >
            <router-link :to="{name:'ForwarderView'}" class="item-container main-list-btn">
                <p><span class="material-symbols-outlined">list_alt</span></p>
            </router-link>
           
            <div class="item-container main-add-btn" @click="openAddComponent">
                <p><span class="material-symbols-outlined">add</span></p>
            </div>
        </div>
        
        <div class="item-v">
            <div v-if="forwarder && !goToAddFlag">
                <div class="add-container">
                    <form class="form-add add-single-form" @submit.prevent="handleSubmit">
                        <h2>Edytuj dane przewoźnika</h2>
                        <div class="form-set">
                            <label class="form-labels">Numery rejestracyjne</label>
                            <input type="text" v-model="platesForm" required>
                        </div>
                        <div class="form-set">
                            <label class="form-labels">Spedycja</label>
                            <input type="text" v-model="speditionForm" required>
                        </div>
                        <div class="form-set">
                            <label class="form-labels">Imię</label>
                            <input type="text" v-model="firstNameForm" required>
                        </div>
                        <div class="form-set">
                            <label class="form-labels">Nazwisko</label>
                            <input type="text" v-model="lastNameForm" required>
                        </div>
                        <div class="form-set">
                            <label class="form-labels">Numer telefonu</label>
                            <input type="text" v-model="phoneForm">
                        </div>
                        <div id="add-btn-container">
                            <button>Zapisz</button>
                        </div>
                        <div v-if="errorForm || getForwarderError" class="error" >
                            <p>{{errorForm}}</p>
                            <p>{{getForwarderError}}</p>
                        </div>
                        <div v-if="createdFlag" class="success">
                            <p>Dane kierowcy zostały zapisane.</p>
                        </div>
                        
                    </form>
                </div>

            </div>
            <div v-if="goToAddFlag">
                <AddForwarder />
            </div>
        </div>
    </div>
  </div>
 
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import NavbarComponent from '../components/NavbarComponent.vue'
import editForwarder from '../js-components/editForwarder.js'
import getForwarderById from '../js-components/getForwarderById.js'
import AddForwarder from '../components/AddForwarder.vue'
export default {
    props:['forwarderId'],
    components:{NavbarComponent, AddForwarder},
    setup(props){
    const url = 'https://localhost:44331/api/'
    const firstNameForm = ref('')
    const lastNameForm = ref('')
    const speditionForm = ref('')
    const phoneForm = ref('')
    const platesForm = ref('')
    const createdFlag = ref(false)

    const errorForm = ref('')
    const goToAddFlag = ref(false)

    const {edit, error:editForwarderError} = editForwarder(url)
    const {loadForwarder, error:getForwarderError, forwarder} = getForwarderById(url)

    onBeforeMount(()=>{
        loadForwarder(props.forwarderId).then(()=>{
            firstNameForm.value = forwarder.value.firstName
            lastNameForm.value = forwarder.value.lastName
            speditionForm.value = forwarder.value.speditor
            phoneForm.value = forwarder.value.phoneNumber
            platesForm.value = forwarder.value.carPlates
        })
    })
    const handleSubmit = ()=>{
         if(firstNameForm.value == '' || lastNameForm.value == '' || speditionForm.value == '' || platesForm.value ==''){
            errorForm.value = "Nie wprowadzono wszystkich danych."
        }else{
            let formInputData = {
                firstName: firstNameForm.value,
                lastName: lastNameForm.value,
                speditor: speditionForm.value,
                phoneNumber: phoneForm.value,
                carPlates: platesForm.value
            }
            errorForm.value = ''
            getForwarderError.value = ''
            edit(props.forwarderId, formInputData).then(()=>{
                
                console.log(editForwarderError.value)
                if(editForwarderError.value == null){
                    createdFlag.value = true
                }
                setTimeout(()=>{createdFlag.value = false},5000)
            })
        }
    }

    const openAddComponent = ()=>{
        goToAddFlag.value = true
    }

    return {getForwarderError,
            editForwarderError,

            firstNameForm,
            lastNameForm,
            speditionForm,
            phoneForm,
            platesForm,

            errorForm,
            handleSubmit,
            forwarder,
            createdFlag,
            openAddComponent,
            goToAddFlag
            }
}
}
</script>

<style>

</style>