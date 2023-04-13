import { ref } from 'vue'
import axios from 'axios'

const changeStatus = (url) =>{

    const error = ref(null)
    

    const change =  async (shipmentId, statusId) => {

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
            let resp = await axios.put( url + 'Shipment/'+shipmentId+'/'+ statusId, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            
        } catch (err) {
            error.value = err.response.data
            console.log(error.value)
        }    
    }
      return {change, error}
}
export default changeStatus