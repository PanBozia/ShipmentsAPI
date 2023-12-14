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
                <div id="label">FG SHIPMENTS</div>
                <img id="hat" src="@/assets/img/hatsanta.png" alt="">
            </div>
            <div>
                <RouterView />
            </div>
        </div>

    </div>

</template>

<style>
#hat{
    height: 4.8vh;
    transform: translateX(-1.7vw) translateY(-1.2vh) rotateZ(-18deg);
}
</style>
