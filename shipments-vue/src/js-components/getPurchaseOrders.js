import { ref } from 'vue'
import axios from 'axios'

const getPurchaseOrders = (url) =>{

    const orders = ref([])
    const error = ref(null)
    const totalItemsCount = ref()
    const itemsFrom = ref()
    const itemsTo = ref()
    const totalPages = ref()

    
    const loadOrders = async (searchPhrase, pageSize, pageNumber, sortBy, sortDirection) => {

        try {
                let resp = await axios.get(url + 'PurchaseOrder/search?searchPhrase='+ searchPhrase +'&pageSize='+pageSize+'&pageNumber='+pageNumber+'&sortBy='+sortBy+'&SortDirection='+sortDirection, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                
                orders.value = resp.data.items
                totalPages.value = resp.data.totalPages
                itemsFrom.value = resp.data.itemsFrom
                itemsTo.value = resp.data.itemsTo
                totalItemsCount.value = resp.data.totalItemsCount
                
            } catch (er) {
                error.value = er.response.data
            }
        // console.log(forwarders.value)

      }

      return {loadOrders, error, orders, totalPages, itemsFrom, itemsTo, totalItemsCount}
}

export default getPurchaseOrders