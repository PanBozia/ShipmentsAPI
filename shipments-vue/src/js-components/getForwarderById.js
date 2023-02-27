import { ref } from 'vue'
import axios from 'axios'

const getForwarderById = (url) =>{

    const forwarder = ref([])
    const error = ref(null)
    
    const loadForwarder = async (id) => {

        try {
                let resp = await axios.get(url + 'forwarder/'+ id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                forwarder.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }
        // console.log(forwarders.value)

      }

      return {loadForwarder, error, forwarder}
}

export default getForwarderById