import { ref } from 'vue'
import axios from 'axios'

const getIncoterms = (url) =>{

    const incoterms = ref([])
    const error = ref(null)
    
    const loadIncoterms = async () => {

        try {
                let resp = await axios.get(url + 'Incoterms/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                incoterms.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadIncoterms, error, incoterms}
}

export default getIncoterms