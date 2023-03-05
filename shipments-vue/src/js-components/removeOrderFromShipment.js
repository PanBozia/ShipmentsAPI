import { ref } from 'vue'
import axios from 'axios'

const removeOrderToShipment = (url) =>{

    const error = ref(null)

    const removeOrder =  async (shipmentId, orderId) => {

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
            let resp = await axios.post( url + 'Shipment/removePo/' + shipmentId +"/"+ orderId, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {removeOrder, error}
}
export default removeOrderToShipment