<template>
  <div class="navbar">
        <div class="navbar-line"></div>
        <div class="navbar-content">
            <div>
                <router-link :to="{name:'CustomerView'}">
                    <p>KLIENCI</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name: 'PurchaseOrderView'}">
                    <p>ZAMÓWIENIA</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name: 'ShipmentsView'}">
                    <p>WYSYŁKI</p>
                </router-link>
            </div>
            <div>
                    <router-link :to="{name:'ForwarderView'}">
                    <p>PRZEWOŹNICY</p>
                </router-link>
            </div>
            <div>
                <router-link :to="{name:'WarehouseView'}">
                    <p>MAGAZYN</p>
                </router-link>
            </div>
            
            <div>
                <router-link :to="{name:'ScheduleView'}">
                    <p>HARMONOGRAM</p>
                </router-link>
            </div>
             <div>
                <router-link :to="{name: 'AddAllView'}">
                    <span class="material-symbols-outlined">
                        add_circle_outline
                    </span>
                </router-link>
            </div>
        </div>
        <div class="navbar-line"></div>
    </div>
</template>

<script>
export default {
    // setup(){
    //     const reloadPage = ()=>{
    //         location.reload()
    //     }
    //     return { reloadPage}
    // }

}
</script>



<style scoped>
.material-symbols-outlined{
    font-size: 2.2vh;
    padding: 0;
    margin: 0;
}

</style>