<template>
  <div class="navbar">
        <div class="navbar-line"></div>
        <div class="navbar-content">
            <div>
                <router-link :to="{name: 'ShipmentsView'}">
                    <p>BIURO</p>
                </router-link>
            </div>
            <div>
                    <router-link :to="{name:'ShipmentsWarehouseView'}">
                    <p>MAGAZYN</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name:'ScheduleView'}" >
                    <p>HARMONOGRAM</p>
                </router-link>
            </div>
            
        </div>
        <div class="navbar-line"></div>
    </div>
</template>

<script>
// export default {
//     setup(){
//         const reloadPage = ()=>{
//             location.reload()
//         }
//         return { reloadPage }
//     }

// }
</script>



<style scoped>
.material-symbols-outlined{
    font-size: 2.2vh;
    padding: 0;
    margin: 0;
}

</style>