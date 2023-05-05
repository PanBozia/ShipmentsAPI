import { ref } from 'vue'
import axios from 'axios'

const addCustomer = (url) =>{

    const error = ref(null)
    const createdId = ref('')
    const isPending = ref(false)

    const addNewCustomer =  async (customerData) => {
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
            let resp = await axios.post( url + 'customer/', customerData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data.customerId
            isPending.value = false
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {addNewCustomer, error, createdId, isPending}
}
export default addCustomer