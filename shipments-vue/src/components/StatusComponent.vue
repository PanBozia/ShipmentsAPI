<template>
  <div class="status-line"> 
    <div :class="{doneStatus:props.statusName == 'Zrealizowana'}">
        <p>
            {{props.statusName}}
        </p>
    </div>
  </div>
</template>
<script>
export default {
    props:['statusName'],
    setup(props) {
     
     return {props}
    }
}
</script>
<style>
.status-line{
    display: flex;
    flex-direction: column;
    
    /* width: 24vh; */
    margin: 0.2vh 4vh;
    padding: 0.5vh 3vh;
    
}
.status-line div{
    padding: 0.4vh 3vh;
    background-color: #ffd500;
    background: linear-gradient(to right bottom, #2eadd7, #0152b5 );
    color: #ffffff;
    cursor: pointer;
    transition: 100ms ease-in-out;
}
.status-line .doneStatus{
    background-color: #bff218;
    background: linear-gradient(to right bottom, #50d72e, #336903 );

}

.status-line div:hover{
    background-color: #92d817;
    background: linear-gradient(to right bottom, #ffd500, #a57901 );
    box-shadow: 0.7vh 0.7vh 1.0vh #00000060;
    transition: 100ms ease-in-out;
    transform: scale(1.03);
}

.status-line div p{
    margin: 0;
    padding: 0;
    text-align: center;
}
</style>