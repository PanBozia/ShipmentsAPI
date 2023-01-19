import { ref } from 'vue'
import axios from 'axios'

const getShipments = (url) =>{

    const shipments = ref([])
    const error = ref(null)
    const totalItemsCount = ref()
    const itemsFrom = ref()
    const itemsTo = ref()
    const totalPages = ref()

    
    const loadShipments = async ( queryData) => {

        try {
                let resp = await axios.post(url + 'Shipment/search', queryData, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                shipments.value = resp.data.items
                totalPages.value = resp.data.totalPages
                itemsFrom.value = resp.data.itemsFrom
                itemsTo.value = resp.data.itemsTo
                totalItemsCount.value = resp.data.totalItemsCount
                
            } catch (er) {
                error.value = er.response.data
            }
        // console.log(forwarders.value)

      }

      return {shipments, error, loadShipments, totalPages, itemsFrom, itemsTo, totalItemsCount}
}

export default getShipments