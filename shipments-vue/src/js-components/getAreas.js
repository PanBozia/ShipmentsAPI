import { ref } from 'vue'
import axios from 'axios'

const getAreas = (url) =>{

    const areas = ref([])
    const error = ref(null)
    
    const loadAreas = async () => {

        try {
                let resp = await axios.get(url + 'WarehouseArea/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                areas.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadAreas, error, areas}
}

export default getAreas