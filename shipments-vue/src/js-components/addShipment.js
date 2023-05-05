import { ref } from 'vue'
import axios from 'axios'

const addShipment = (url) =>{

    const error = ref(null)
    const createdId = ref('')
    const isPending = ref(false)
    const addNewShipment =  async (shipmentData) => {
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
            let resp = await axios.post( url + 'shipment/', shipmentData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data
            isPending.value = false
        } catch (err) {
            error.value = err.response.data.errors
            console.log(error.value)
        }    
    }
      return {addNewShipment, error, createdId, isPending}
}
export default addShipment