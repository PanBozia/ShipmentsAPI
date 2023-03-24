<template>
  <NavbarComponent />
  <div class="row-container-one">
    <div></div>
    <div>
        <AddShipment />
    </div>
    <div></div>
  </div>
  <div class="row-container-two">
    <div></div>
    <div>
      <ChooseOrder :addedPoNumber="addedPoNumber" />
    </div>
    <div>
      <AddPurchaseOrder @order-added-event="handleAddedOrder" />
    </div>
    <div></div>
  </div>
</template>

<script>
import { ref } from "vue";
import AddPurchaseOrder from "../components/AddPurchaseOrder.vue";
import ChooseOrder from "../components/ChooseOrder.vue";
import NavbarComponent from "../components/NavbarComponent.vue";
import AddShipment from "../components/AddShipment.vue";

export default {
  components: { AddPurchaseOrder, NavbarComponent, ChooseOrder, AddShipment },
  setup() {
    const addedPoNumber = ref("");
    const handleAddedOrder = (orderNumber) => {
      //console.log(orderNumber)
      addedPoNumber.value = orderNumber;
    };

    return {
      handleAddedOrder,
      addedPoNumber,
    };
  },
};
</script>

<style>
.row-container-one {
  display: grid;
  grid-template-columns: 1fr 42.5vw 1fr;
  gap: 5vh;
    /* max-width: 50vw; */
    /* margin: auto; */
}
.row-container-two {
  display: grid;
  grid-template-columns: 1fr 20vw 20vw 1fr;
  gap: 5vh;
  /* max-width: 100vw; */
  /* margin: auto; */
}
</style>