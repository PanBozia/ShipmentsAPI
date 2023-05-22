import { ref } from 'vue'
import axios from 'axios'

const getShipmentById = (url) =>{

    const shipment = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadShipment = async (id) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'Shipment/' + id, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                shipment.value = resp.data
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }
      }

      return {shipment, error, loadShipment, isPending}
}

export default getShipmentById