<script setup>
    import { RouterView } from 'vue-router'
</script>

<template>
    <div class="main-container">
        <div class="nav-container">
            <div class="top-bar">
                <router-link :to="{name:'home'}">
                    <img id="dsse-logo" src="@/assets/img/logoDSSE.png" alt="">
                </router-link>
                <div id="label">FG Shipments</div>
            </div>
            <div>
                <RouterView />
            </div>
        </div>

    </div>

</template>

<style>

</style>
