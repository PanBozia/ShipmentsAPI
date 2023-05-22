<template>
  <div v-if="clients">
    <!-- <button @click="getData" >get Data</button> -->
    <button @click="printToPdf"> PDF </button>
    <!-- <p>
        {{clients}}
    </p>
    
    <p>
        {{shipment}}
    </p> -->
    
  </div>
  <div v-if="clients">
    <!-- <div v-if="clients"> -->
    <div v-for="client in clients" :key="client">
        <div id="cmr-container">
            
            <img src="/src/assets/img/cmr.png" alt="" srcset="">
            <div class="address2">
                <p>Daicel Safety Systems Sp. z o.o.</p>
                <p>ul. Strefowa 8</p>
                <p>59-230 Żarów</p>
                <p>PL</p>
            </div>
            <div class="address1">
                <p>
                    {{client.name}}
                </p>
                <p>
                    {{client.streetAddress}}
                </p>
                <p>
                {{client.zipCodeAddress}} {{client.cityAddress}}
                </p>
                <p>
                    {{client.countryAddress}}
                </p>
            </div>
            <div class="address3">
               <p> {{client.cityAddress}} , {{client.countryAddress}}</p>
            </div>
            <div class="address4">
               <p> Żarów , PL, {{moment().format('DD.MM.YYYY')}}</p>
            </div>
            <div class="forwarder1">
               <p> {{shipment.forwarder.firstName}} {{shipment.forwarder.lastName}}</p>
               <p> {{shipment.forwarder.speditor}}</p>
               <p> {{shipment.forwarder.carPlates}}</p>
            </div>
            <div class="cargo0">
               <p> UN 3268</p>
            </div>
            <div class="cargo1">
               <p> {{shipment.palletQty}}</p>
            </div>
            <div class="cargo2">
               <p> EUR PALLETS</p>
            </div>
            <div class="cargo3">
               <p> INFLATORS</p>
            </div>
            <div class="cargo4">
               <p> 4325</p>
            </div>
            <div class="cargo5">
               <p> {{shipment.palletQty * 1.2 * 0.8 * 1.2}}</p>
            </div>
            <div class="cargo6">
               <p> 3268 </p>
            </div>
            <div class="cargo7">
               <p> 9 </p>
            </div>
            <div class="address5">
                <p> Żarów , PL / {{moment().format('DD.MM.YYYY')}}</p>
            </div>

        </div>
        
    </div>
        
    </div>
</template>

<script>
import { onMounted, ref } from 'vue'
import getShipmentById from '../js-components/getShipmentById'
import getAllCustomers from '../js-components/getAllCustomers'
import { useLinksStore } from '../stores/linksStore.js'
import jsPDF  from "jspdf";
import html2canvas from "html2canvas"
import moment from 'moment'
export default {
    props:['shipmentId'],
    setup(props){
        const linksStore = useLinksStore()
        const clients = ref([])
        const { loadShipment, error, shipment, isPending} = getShipmentById(linksStore.url)
        const { loadAllCustomers, error:customersError, customers} = getAllCustomers(linksStore.url)
        let singleCustomer
        
        const getData = ()=>{
             loadShipment(props.shipmentId).then(()=>{
                loadAllCustomers().then(()=>{
                    shipment.value.purchaseOrders.forEach(order => {
                        singleCustomer = customers.value.filter(customer => customer.shortName == order.customerShortName)
                        singleCustomer = singleCustomer.map((customer)=>{
                            return {
                                        id:customer.id, 
                                        name:customer.name,
                                        streetAddress: customer.streetAddress,
                                        cityAddress: customer.cityAddress,
                                        zipCodeAddress: customer.zipCodeAddress,
                                        countryAddress: customer.countryAddress
                                }
                        })
                        clients.value.push(singleCustomer[0])
                    })
                   
                    var tempArray = clients.value.filter((value, index, array)=>{
                        if (index > 0){
                                return array[index] != array[index-1];
                        }else{
                            return array[index]
                        }
                    })
                    clients.value = tempArray

                });
            })
        }
        onMounted(()=>{
           getData()
        })
        
        
        function convertToPdf(){
            
            const doc = new jsPDF();

            var domElement = document.querySelector("#cmr-container")

            doc.html(domElement,{
                callback: function(doc){
                    doc.save('newdoc.pdf');
                },
                margin: [10,10,10,10],
                autoPaging: 'text',
                x: 0,
                y: 0,
                width: 197,
                windowWidth:675
            })

        }

        function printToPdf(){
            html2canvas(document.querySelector('#cmr-container')).then((canvas)=>{
                let base64image = canvas.toDataURL('image/png')
                
                let pdf = new jsPDF('p','px',[1267,896])
                pdf.addImage(base64image, 'PNG',0,0,896,1267)
                pdf.save('canvaspdf.pdf')

            });
        }
        
        return {
                error, shipment, isPending,
                customers, customersError,
                convertToPdf, printToPdf,
                moment, getData,
                clients

            }
    }

}
</script>

<style>
#cmr-container{
    display: block;
    margin: 4vh 6vh ;
    
    width: 64.5vw;
    height: 130vh;
    position: relative;
    
    /* background: #ddd; */
}
#cmr-container img{ 
    position: absolute;
    width: 64.5vw;
    top :0;
}
#cmr-container .address1{
    position: absolute;
    top: 22.5vh;
    left: 10vw;
    font-size: 1.2vh;
}
#cmr-container .address2{
    position: absolute;
    top: 12.0vh;
    left: 10vw;
    font-size: 1.2vh;
}
#cmr-container .address3{
     position: absolute;
    top: 34vh;
    left: 10vw;
    font-size: 1.3vh;
}
#cmr-container .address4{
     position: absolute;
    top: 40vh;
    left: 10vw;
    font-size: 1.3vh;
}
#cmr-container .forwarder1{
     position: absolute;
    top: 22.2vh;
    left: 38.5vw;
    font-size: 1.3vh;
}
#cmr-container .cargo0{
    position: absolute;
    top: 53vh;
    left: 7vw;
    font-size: 1.3vh;
}
#cmr-container .cargo1{
    position: absolute;
    top: 52.5vh;
    left: 16vw;
    font-size: 1.8vh;
}
#cmr-container .cargo2{
     position: absolute;
    top: 53vh;
    left: 21vw;
    font-size: 1.3vh;
}
#cmr-container .cargo3{
     position: absolute;
    top: 53vh;
    left: 29vw;
    font-size: 1.3vh;
}
#cmr-container .cargo4{
     position: absolute;
    top: 53vh;
    left: 44vw;
    font-size: 1.3vh;
}
#cmr-container .cargo5{
     position: absolute;
    top: 53.0vh;
    left: 52vw;
    font-size: 1.3vh;
}
#cmr-container .cargo6{
     position: absolute;
    top: 79.7vh;
    left: 8vw;
    font-size: 1.3vh;
}
#cmr-container .cargo7{
     position: absolute;
    top: 79.7vh;
    left: 21.3vw;
    font-size: 1.3vh;
}
#cmr-container .address5{
     position: absolute;
    top: 105.7vh;
    left: 10.3vw;
    font-size: 1.3vh;
}
#cmr-container p{
    margin: 0;
    color: #000;
}
</style>