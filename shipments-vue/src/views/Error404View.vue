<template>
<div class="whiteline"></div>
<div class="main-error-container">

    <div class="error-container">
    <h3 class="error-item"><span class="material-symbols-outlined">error</span></h3>
    <h1 class="error-item">NIE ZNALEZIONO STRONY</h1>
    <h3 class="error-item">..coś poszło nie tak.. </h3>
    <h2 class="error404">404</h2>
    
    </div>
  
</div>
</template>

<script>
export default {

}
</script>

<style>
.whiteline{
    width: 100%;
    height: 0.4vh;
    background: #fff;
}
.main-error-container{
    height: 60vh;
    overflow: hidden;
    /* position: absolute; */
    
}
.main-error-container .error404{
    padding: 0;
    margin: 0;
    font-size: 54vh;
    transform: rotateZ(0deg) translateY(-46vh);
    color: #ffffff0c;
    z-index: 1;
}
.main-error-container h1{
    font-size: 5vh;
    text-shadow: 0.6vh 0.6vh 0vh rgba(15, 15, 15, 0.766);
    z-index: 2;
}
.error-container{
    display: flex;
    flex-flow: column;
    margin:12vh auto 0 auto;
    align-items: center;
    padding: 4vh;
}
.error-container .error-item{
    margin: 15px 0;
    z-index: 2;
}
.error-container h3.error-item{
    color: #aaa;
    text-shadow: 0.6vh 0.6vh 0vh rgba(15, 15, 15, 0.766);
}
.error-container .error-item span{
    font-size: 15vh;
    color: #dd0025;
}


</style>