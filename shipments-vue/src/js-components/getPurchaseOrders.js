import { ref } from 'vue'
import axios from 'axios'

const getPurchaseOrders = (url) =>{

    const orders = ref([])
    const error = ref(null)
    const totalItemsCount = ref()
    const itemsFrom = ref()
    const itemsTo = ref()
    const totalPages = ref()
    const isPending = ref(false)

    
    const loadOrders = async (searchPhrase, pageSize, pageNumber, sortBy, sortDirection) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'PurchaseOrder/search?searchPhrase='+ searchPhrase +'&pageSize='+pageSize+'&pageNumber='+pageNumber+'&sortBy='+sortBy+'&SortDirection='+sortDirection, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                orders.value = resp.data.items
                totalPages.value = resp.data.totalPages
                itemsFrom.value = resp.data.itemsFrom
                itemsTo.value = resp.data.itemsTo
                totalItemsCount.value = resp.data.totalItemsCount
                isPending.value = false
                
            } catch (er) {
                isPending.value = false
                error.value = er.response.data
            }
      }

      return {loadOrders, error, orders, totalPages, itemsFrom, itemsTo, totalItemsCount, isPending}
}

export default getPurchaseOrders