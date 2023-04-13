<template>
  <div class="status-ctnr"> 
    <div>
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
.status-ctnr{
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 24vh;
    margin: 0.2vh;
    
}
.status-ctnr div{
    padding: 0.4vh 3vh;
    background-color: #25507c;
}
.status-ctnr div p{
    margin: 0;
    padding: 0;
}
</style>