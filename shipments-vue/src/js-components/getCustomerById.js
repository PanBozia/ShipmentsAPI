import { ref } from 'vue'
import axios from 'axios'

const getCustomerbyId = (url) =>{

    const customer = ref([])
    const error = ref(null)
    
    const loadCustomer = async (id) => {

        try {
                let resp = await axios.get(url + 'Customer/'+id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                customer.value = resp.data
                
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadCustomer, error, customer}
}

export default getCustomerbyId