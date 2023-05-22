import { ref } from 'vue'
import axios from 'axios'

const getPurchaseOrderById = (url) =>{

    const order = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadOrder = async (id) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'PurchaseOrder/'+id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                order.value = resp.data
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }

      }

      return {loadOrder, error, order, isPending}
}

export default getPurchaseOrderById