<template>
    <div class="error-ctnr">
        <div class="error-window" v-if="props">
            <h3>Coś poszło nie tak..</h3>
            <p>
                <span class="material-symbols-outlined ico-error">
                    error
                </span>
            </p>
            <p>
                {{props.msg}}
            </p>
            <div class="error-btn-ctnr">
                <button class="btn" @click="closeWindow">OK</button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    emits:['reset-error-event'],
    props:['msg'],
    setup(props, context){
        const closeWindow = ()=>{
            context.emit('reset-error-event')
        }

    console.log(props.msg)
    return{
            props,
            closeWindow
        }
    }
}    
    
</script>

<style>
.error-ctnr{
    position: fixed;
    top: 0;
    left: 0;
    z-index: 1000;
    background: #000000c0;
    height: 100%;
    width: 100%;
}
.error-window{
    margin:20vh auto;
    width: 20vw;
    /* height: 20vw; */
    background: #1e2a3d;
    border-radius: 2vh;
    border: 0.2vh solid #ffcc00;
    
    text-align: center;
    padding: 2vh;
}
.error-window .ico-error {
    font-size: 10vh;
    color: #ff6b6b;
    margin: 0;
    padding: 0;
    /* transform: translateY(-6vh); */
    
}
.error-window h3{
    margin: 1vh auto 2vh auto;
    color: #ffcc00;
    font-size: 2vh;
    transform: translateY(1.2vh);
    
    
}
.error-btn-ctnr{
    margin: 3vh 0 1vh 0;
    display: flex;
    justify-content: center;
}
</style>