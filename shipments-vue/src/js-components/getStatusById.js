import { ref } from 'vue'
import axios from 'axios'

const getStatusById = (url) =>{

    const status = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadStatus = async (id) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Status/' + id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                status.value = resp.data
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }

      }

      return {loadStatus, error, status, isPending}
}

export default getStatusById