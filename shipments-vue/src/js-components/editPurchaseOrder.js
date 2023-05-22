import { ref } from 'vue'
import axios from 'axios'

const editPurchaseOrder = (url) =>{

    const error = ref(null)
    const isPending = ref(false)
    const editOrder =  async (id, orderData) => {
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
        mode:'cors',
        };
      
        try {
            let resp = await axios.put( url + 'PurchaseOrder/'+ id, orderData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false
                throw Error('Coś poszło nie tak..')
            }
            isPending.value = false
        } catch (err) {
            isPending.value = false
            error.value = err.response.data
        }    
    }
      return {editOrder, error, isPending}
}
export default editPurchaseOrder