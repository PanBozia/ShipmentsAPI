import { ref } from 'vue'
import axios from 'axios'

const getForwarders = (url) =>{

    const forwarders = ref([])
    const error = ref(null)
    const totalItemsCount = ref()
    const itemsFrom = ref()
    const itemsTo = ref()
    const totalPages = ref()
    const isPending = ref(false)
    
    const loadForwarders = async (searchPhrase, pageSize, pageNumber, sortBy, sortDirection) => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'forwarder/search?searchPhrase='+ searchPhrase +'&pageSize='+pageSize+'&pageNumber='+pageNumber+'&sortBy='+sortBy+'&SortDirection='+sortDirection, {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                forwarders.value = resp.data.items
                totalPages.value = resp.data.totalPages
                itemsFrom.value = resp.data.itemsFrom
                itemsTo.value = resp.data.itemsTo
                totalItemsCount.value = resp.data.totalItemsCount
                isPending.value = false

            } catch (er) {
                error.value = er.response.data
            }
      }

      return {loadForwarders, error, forwarders, totalPages, itemsFrom, itemsTo, totalItemsCount, isPending}
}

export default getForwarders