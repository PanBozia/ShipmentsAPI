import { ref } from 'vue'
import axios from 'axios'

const changeArea = (url) =>{
    
    const error = ref(null)

    const change =  async (shipmentId, areaId) => {
        
        var requestOptions = {
            method: 'PUT',
            headers: {
                'Accept':'*/*',
                'Content-Type':'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': '*',
                'Access-Control-Allow-Credentials': 'true'
            },
            mode:'cors',
        };
        
        if(areaId === null || areaId === undefined ){
            try {
                let resp = await axios.put( url + 'Shipment/area/'+shipmentId, requestOptions)
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                
            } catch (err) {
                error.value = err.response.data
                console.log(error.value)
            }    
        
        
        }else{
            try {
                let resp = await axios.put( url + 'Shipment/area/'+shipmentId+'/'+ areaId, requestOptions)
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                
            } catch (err) {
                error.value = err.response.data.errors
                console.log(error.value)
            }    

        }
    }
      return {change, error}
}
export default changeArea