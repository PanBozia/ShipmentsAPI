import { ref } from 'vue'
import axios from 'axios'

const addOrderToShipment = (url) =>{

    const error = ref(null)
    const isPending = ref(false)
    const addOrder =  async (shipmentId, orderId) => {
        isPending.value = true
        var requestOptions = {
            method: 'POST',
            headers: {
                'Accept':'*/*',
                'Content-Type':'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': '*',
                'Access-Control-Allow-Credentials': 'true'
            },
            mode:'cors',
        };
        
        try {
            let resp = await axios.post( url + 'Shipment/addPo/' + shipmentId +"/"+ orderId, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false
                throw Error('Coś poszło nie tak..')
            }
            isPending.value = false
            
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {addOrder, error, isPending}
}
export default addOrderToShipment