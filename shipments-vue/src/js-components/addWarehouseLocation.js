import { ref } from 'vue'
import axios from 'axios'

const addWarehouseLocation = (url) =>{

    const error = ref(null)
    const createdId = ref('')

    const addNewLocation =  async (locationName) => {

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
            let resp = await axios.post( url + 'WarehouseArea/', locationName, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data.customerId
        } catch (err) {
            error.value = err.response.data
            console.log(error.value)
        }    
    }
      return {addNewLocation, error, createdId}
}
export default addWarehouseLocation