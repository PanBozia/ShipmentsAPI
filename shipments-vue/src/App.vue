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
            <div>
                <RouterView />
            </div>
        </div>

    </div>

</template>

<style>

</style>
