import { ref } from 'vue'
import axios from 'axios'

const getWarehouseLocations = (url) =>{

    const locations = ref([])
    const error = ref(null)
    
    const loadLocations = async () => {

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
                
                locations.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadLocations, error, locations}
}

export default getWarehouseLocations