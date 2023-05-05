import { ref } from 'vue'
import axios from 'axios'

const getStatuses = (url) =>{

    const statuses = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadStatuses = async () => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Status/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                statuses.value = resp.data
                isPending.value = false

            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadStatuses, error, statuses, isPending}
}

export default getStatuses