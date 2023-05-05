import { ref } from 'vue'
import axios from 'axios'

const getCustomerById = (url) =>{

    const customer = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadCustomer = async (id) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Customer/'+id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                customer.value = resp.data
                isPending.value = false
                
            } catch (er) {
                error.value = er.response.data
            }
      }
      return {loadCustomer, error, customer, isPending}
}

export default getCustomerById