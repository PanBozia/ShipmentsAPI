import { ref } from 'vue'
import axios from 'axios'

const getAllCustomers = (url) =>{

    const customers = ref([])
    const error = ref(null)
    
    const loadAllCustomers = async () => {

        try {
                let resp = await axios.get(url + 'Customer/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                customers.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return { loadAllCustomers, error, customers }
}

export default getAllCustomers