import { ref } from 'vue'
import axios from 'axios'

const getIncoterms = (url) =>{

    const incoterms = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadIncoterms = async () => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Incoterms/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                incoterms.value = resp.data
                isPending.value = false

            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadIncoterms, error, incoterms, isPending}
}

export default getIncoterms