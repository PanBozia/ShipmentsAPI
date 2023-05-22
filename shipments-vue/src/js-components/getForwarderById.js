import { ref } from 'vue'
import axios from 'axios'

const getForwarderById = (url) =>{

    const forwarder = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadForwarder = async (id) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'forwarder/'+ id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                forwarder.value = resp.data
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }
      }

      return {loadForwarder, error, forwarder, isPending}
}

export default getForwarderById