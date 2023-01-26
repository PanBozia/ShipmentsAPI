<template>
  <NavbarComponent />
  <div class="frame">
    <div style="display:flex; justify-content:center; border-bottom:solid 1px #fff; margin:30px 40px 0px 40px;">
        <h1>NOTATNIK</h1>
        
    </div>
    <div class="img-ctnr">
        <div></div>
        <img class="test-image second" src="../assets/img/note01.jpg" alt="" srcset="">
        <img class="test-image first" src="../assets/img/note02.jpg" alt="" srcset="">
        <img class="test-image second" src="../assets/img/note04.jpg" alt="" srcset="">
        <img class="test-image third" src="../assets/img/note03.jpg" alt="" srcset="">
        <div></div>
    </div>

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
.img-ctnr{
    /* display:grid;
    grid-template-columns: 15% 35% 35% 15%;
    justify-items:center ; */
    display: flex;
    justify-content: center;
}
.test-image{
    height: 340px;
    border-radius: 20px;
    margin: 100px 10px;
    box-shadow: 10px 10px 16px rgba(0,0,0,0.8);
}
.test-image.first:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 300ms;
}
.test-image.first{
    transform: rotateZ(10deg) translateX(-40px);
    transition: ease-in-out 300ms;
}
.test-image.second:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 300ms;
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