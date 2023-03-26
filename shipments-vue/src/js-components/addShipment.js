import { ref } from 'vue'
import axios from 'axios'

const addShipment = (url) =>{

    const error = ref(null)
    const createdId = ref('')

    const addNewShipment =  async (shipmentData) => {

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
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data
        } catch (err) {
            error.value = err.response.data
            console.log(error.value)
        }    
    }
      return {addNewShipment, error, createdId}
}
export default addShipment