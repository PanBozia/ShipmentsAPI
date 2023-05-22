import { ref } from 'vue'
import axios from 'axios'

const addForwarder = (url) =>{

    const error = ref(null)
    const createdId = ref('')
    const isPending = ref(false)
    const addNewForwarder =  async (forwarderData) => {
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
            let resp = await axios.post( url + 'forwarder/', forwarderData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data
            isPending.value = false
        } catch (err) {
            error.value = err.response.data.errors
            isPending.value = false
            console.log(error.value)
        }    
    }
      return {addNewForwarder, error, createdId, isPending}
}
export default addForwarder