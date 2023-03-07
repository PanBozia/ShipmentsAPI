import { ref } from 'vue'
import axios from 'axios'

const editPurchaseOrder = (url) =>{

    const error = ref(null)
    

    const editOrder =  async (id, orderData) => {

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
                throw Error('Coś poszło nie tak..')
            }
            
        } catch (err) {
            error.value = err.response.data
        }    
    }
      return {editOrder, error}
}
export default editPurchaseOrder