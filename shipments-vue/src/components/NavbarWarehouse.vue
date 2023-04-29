<template>
  <div class="navbar">
        <div class="navbar-line"></div>
        <div class="navbar-content">
            <div>
                <router-link :to="{name: 'ShipmentsWarehouseView'}">
                    <p>WYSYŁKI</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name:'WarehouseView'}">
                    <p>LOKACJE</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name:'ScheduleView'}">
                    <p>HARMONOGRAM</p>
                </router-link>
            </div>
             
        </div>
        <div class="navbar-line"></div>
    </div>
</template>

<script>
export default {

}
</script>



<style scoped>
.material-symbols-outlined{
    font-size: 2.2vh;
    padding: 0;
    margin: 0;
}

</style>