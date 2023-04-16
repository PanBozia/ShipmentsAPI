import { ref } from 'vue'
import axios from 'axios'

const changeDepartureTime = (url) =>{

    const error = ref(null)
    

    const changeDeparture =  async (shipmentId, atd) => {

        var requestOptions = {
        method: 'PUT',
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
            let resp = await axios.put( url + 'Shipment/atd/'+shipmentId, atd, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            
        } catch (err) {
            error.value = err.response.data
            console.log(error.value)
        }    
    }
      return {changeDeparture, error}
}
export default changeDepartureTime