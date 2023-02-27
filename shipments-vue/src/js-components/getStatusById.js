import { ref } from 'vue'
import axios from 'axios'

const getStatusById = (url) =>{

    const status = ref([])
    const error = ref(null)
    
    const loadStatus = async (id) => {

        try {
                let resp = await axios.get(url + 'Status/' + id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                status.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadStatus, error, status}
}

export default getStatusById