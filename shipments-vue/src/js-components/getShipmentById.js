import { ref } from 'vue'
import axios from 'axios'

const getShipmentById = (url) =>{

    const shipment = ref([])
    const error = ref(null)
    
    const loadShipment = async (id) => {

        try {
                let resp = await axios.get(url + 'Shipment/' + id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                shipment.value = resp.data
                
            } catch (er) {
                error.value = er.response.data
            }
        // console.log(forwarders.value)

      }

      return {shipment, error, loadShipment}
}

export default getShipmentById