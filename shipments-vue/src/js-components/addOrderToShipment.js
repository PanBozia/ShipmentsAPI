import { ref } from 'vue'
import axios from 'axios'

const addOrderToShipment = (url) =>{

    const error = ref(null)

    const addOrder =  async (shipmentId, orderId) => {

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
                throw Error('Coś poszło nie tak..')
            }
            
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {addOrder, error}
}
export default addOrderToShipment