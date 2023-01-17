<script setup>
    import { RouterView } from 'vue-router'
</script>

<template>
    <div class="main-container">
        <div class="nav-container">
            <div class="top-bar">
                <img id="dsse-logo" src="@/assets/img/logoDSSE.png" alt="">
                
                <div id="label">FG Shipments</div>
            </div>
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
                            <p>KIEROWCY</p>
                        </router-link>
                    </div>
                    <div>
                        <p>MAGAZYN</p>
                    </div>
                    <div>
                        <p>STATUSY</p>
                    </div>
                    <div>
                        <router-link :to="{name:'ScheduleView'}">
                            <p>HARMONOGRAM</p>
                        </router-link>
                    </div>
                </div>
                <div class="navbar-line"></div>
            </div>
        </div>
        <div>
            <RouterView />
        </div>
    </div>

</template>

<style>

</style>
