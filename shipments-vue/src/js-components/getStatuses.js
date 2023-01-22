import { ref } from 'vue'
import axios from 'axios'

const getStatuses = (url) =>{

    const statuses = ref([])
    const error = ref(null)
    
    const loadStatuses = async () => {

        try {
                let resp = await axios.get(url + 'Status/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                statuses.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadStatuses, error, statuses}
}

export default getStatuses