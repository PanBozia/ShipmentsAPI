<template>
  <NavbarStart />
  <div class="frame">
    
    <div class="img-ctnr">
        <div></div>
        <router-link :to="{name:'ShipmentsView'}" class="test-image first">
            <img src="../assets/img/biuro.jpg">
            <div>
                <h1>Biuro</h1>
                <h3>Awizacja wysyłek</h3>
            </div>
        </router-link>
        <router-link :to="{name:'ShipmentsView'}" class="test-image first">
            <img src="../assets/img/magazyn.jpg">
            <div>
                <h1>Magazyn</h1>
                <h3>Obsługa wysyłek</h3>
            </div>
        </router-link>
        <div></div>
    </div>

  </div>
</template>

<script>
import NavbarStart from '../components/NavbarStart.vue'
export default {
    components: {NavbarStart},
    setup(){
        return{}
    }
}
</script>
<style>
.img-ctnr{
    /* display:grid;
    grid-template-columns: 15% 35% 35% 15%;
    justify-items:center ; */
    display: flex;
    justify-content: center;
    align-items: center;
    height: 60vh;
    
}
.img-ctnr img{
    height: inherit;
    overflow: hidden;
    
}
.test-image{
    display: grid;
    grid-template-columns: auto auto;
    gap: 3vh;
    width: 50vh;
    height: 24vh;
    border-radius: 0vh;
    margin: 10vh 3vh;
    border: solid 0.3vh #ffffff;
    justify-content: start;
    align-items: center;
    background: linear-gradient(rgb(56, 56, 56), rgb(38, 41, 42));
}

.test-image h1{
    margin:0vh 0vh;
    font-family: 'Bebas Neue', cursive;
    background: -webkit-linear-gradient(#ffcc00, #fada27);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    font-size: 6.0vh;
    align-self: center;
    padding: 0;
    z-index: 2;
}
.test-image h3{
    margin:0vh 0vh;
    font-family: 'Bebas Neue', cursive;
    background: -webkit-linear-gradient(#ffffff, #e6e6e6);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    font-size: 3.0vh;
    align-self: center;
    padding: 0;
}
.test-image.first:hover{
    transform:scale(1.05);
    z-index: 2;
    transition: ease-in-out 200ms;
    box-shadow: 1.6vh 1.6vh 1.8vh #00000060;
    cursor: pointer;
}
.test-image.first{
    transition: ease-in-out 200ms;
}
.test-image.second:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 200ms;
}
.test-image.second{
    transform: rotateZ(-10deg) translateX(40px);
    transition: ease-in-out 300ms;
}
.test-image.third:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 300ms;
}
.test-image.third{
    transform: rotateZ(-26deg) translateX(20px);
    transition: ease-in-out 300ms;
}
</style>