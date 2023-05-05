import { ref } from 'vue'
import axios from 'axios'

const changeDepartureTime = (url) =>{

    const error = ref(null)
    const isPending = ref(false)

    const changeDeparture =  async (shipmentId, atd) => {
        isPending.value = true
        var requestOptions = {
        method: 'PUT',
        headers: {
            'Accept':'*/*',
            'Content-Type':'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': '*',
            'Access-Control-Allow-Credentials': 'true'
            
        },
        mode:'cors'
        
        };

        try {
                let resp = await axios.put( url + 'Shipment/atd/' + shipmentId + '/' + atd, requestOptions)
            
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                isPending.value = false
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {changeDeparture, error, isPending}
}
export default changeDepartureTime