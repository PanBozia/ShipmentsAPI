import { ref } from 'vue'
import axios from 'axios'

const getScheduledShipments = (url) =>{

    const shipments = ref([])
    const error = ref(null)
    
    const isPending = ref(false)

    
    const loadShipments = async () => {
        isPending.value = true
        try {
                let resp = await axios.get(url + 'Shipment/scheduled', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    
                    throw Error('Coś poszło nie tak..')
                }
                
                shipments.value = resp.data
               
                // setTimeout(()=>{isPending.value = false}, 100000)
                isPending.value = false
                
            } catch (er) {
                error.value = er.response.data.errors
                isPending.value = false
                console.log(error.value)
            }
      }

      return {shipments, error, loadShipments, isPending}
}

export default getScheduledShipments