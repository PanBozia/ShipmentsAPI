import { ref } from 'vue'
import axios from 'axios'

const getPurchaseOrderById = (url) =>{

    const order = ref([])
    const error = ref(null)
    
    const loadOrder = async (id) => {

        try {
                let resp = await axios.get(url + 'PurchaseOrder/'+id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                order.value = resp.data
                
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadOrder, error, order}
}

export default getPurchaseOrderById