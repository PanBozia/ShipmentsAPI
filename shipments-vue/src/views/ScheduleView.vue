<template>
<NavbarComponent />
  <div class="schedule-container">
    <p>Schedule</p>
  </div>
</template>

<script>
import NavbarComponent from '../components/NavbarComponent.vue'
export default {
    components: {NavbarComponent},
    setup(){
        return{}
    }

}
</script>

<style>
.schedule-container{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr;
}
</style>