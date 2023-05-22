import { ref } from 'vue'
import axios from 'axios'

const getAllCustomers = (url) =>{

    const customers = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadAllCustomers = async () => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Customer/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                customers.value = resp.data
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }

      }

      return { loadAllCustomers, error, customers, isPending }
}

export default getAllCustomers