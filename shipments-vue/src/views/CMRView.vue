<template>
    <NavbarWarehouse />
        <form class="cmr-form" @submit.prevent="">
            
            <div class="cmr-container">
            <!-- 1 column -->
            <div>
                <span class="cmr-client-header">
                    <h3 class="top-headers">Klient</h3> <h3 class="top-headers yell">{{formConsigneeName}}</h3>
                </span>
                <!-- <h3>Klient <span>{{formConsigneeName}}</span></h3> -->
               
                    <p class="choose-clinet-head" :class="{greenLabel:formClientIndex == null}">Wybierz klienta</p>
                    <!-- <select v-model="formClientIndex">
                        <option v-for="(client, index) in clients" :key="client.id" :value="index" >
                            {{index+1}} - {{client.name}}
                        </option>
                    </select> -->
                    <div class="clients-icons-ctnr">
                        <div 
                            class="client-icon" 
                            v-for="(client, index) in clients" :key="client.id" 
                            :value="index" 
                            @click="chooseClient(index)" 
                            :class="{notChosenIcon:formClientIndex != index}"
                            >
                            <p>
                                {{client.shortName}}
                            </p>
                        </div>
                    </div>
               
                <h3>1 - Nadawca</h3>
                <div class="cmr-form-set">
                    <label>Nazwa firmy</label>
                    <input class="dis" type="text" v-model="formSenderName">
                </div>
                <div class="cmr-form-set">
                    <label>Ulica</label>
                    <input class="dis" type="text" v-model="formSenderStreet">
                </div>
                    
                <div class="cmr-form-set">
                    <label>Kod pocztowy i miasto</label>
                    <input class="dis" type="text" v-model="formSenderCity">
                </div>
                <div class="cmr-form-set">
                    <label>Kraj</label>
                    <input class="dis" type="text" v-model="formSenderCountry">
                </div>
                <h3>2 - Odbiorca</h3>
                <div class="cmr-form-set">
                    <label>Nazwa firmy</label>
                    <input class="dis" type="text" v-model="formConsigneeName">
                </div>
                <div class="cmr-form-set">
                    <label>Ulica</label>
                    <input class="dis" type="text" v-model="formConsigneeStreet">
                </div>
                    
                <div class="cmr-form-set">
                    <label>Kod pocztowy i miasto</label>
                    <input class="dis" type="text" v-model="formConsigneeCity">
                </div>
                <div class="cmr-form-set">
                    <label>Kraj</label>
                    <input class="dis" type="text" v-model="formConsigneeCountry">
                </div>
                <h3>3 - Miejsce przeznaczenia</h3>
                <div class="cmr-form-set">
                    <label>Miejscowość, kraj</label>
                    <input class="dis" type="text" v-model="formDestination">
                </div>
                <h3>4 - Miejsce i data załadowania</h3>
                <div class="cmr-form-set">
                    <label>Miejscowość, kraj, data</label>
                    <input class="dis" type="text" v-model="formLoadingPlace">
                </div>
                <h3>5 - Załączone dokumenty</h3>
                <div class="cmr-form-set">
                    <label>Numer dokumentu 1</label>
                    <input class="dis" type="text" v-model="formAttachment1">
                </div>
                <div class="cmr-form-set">
                    <label>Numer dokumentu 2</label>
                    <input class="dis" type="text" v-model="formAttachment2">
                </div>
            </div>    
            
            <!-- 2 column -->
            <div>
                <h3>6 - Cechy i numery towaru</h3>
                <div class="cmr-form-set">
                    <label>Linia #1</label>
                    <input class="dis" type="text" v-model="formGoodsMarks1">
                </div>
                <div class="cmr-form-set">
                    <label>Linia #2</label>
                    <input class="dis" type="text" v-model="formGoodsMarks2">
                </div>
                <div class="cmr-form-set">
                    <label>Linia #3</label>
                    <input class="dis" type="text" v-model="formGoodsMarks3">
                </div>
                <div class="cmr-form-set">
                    <label>Linia #4</label>
                    <input class="dis" type="text" v-model="formGoodsMarks4">
                </div>
                <h3>7 - Ilość</h3>
                <div class="cmr-form-set">
                    <label>Iość palet (szt)</label>
                    <input class="dis" type="text" v-model="formGoodsQty">
                </div>
                <h3>11 - Waga</h3>
                <div class="cmr-form-set">
                    <label>Waga brutto (kg)</label>
                    <input class="dis" type="text" v-model="formGoodsWeight">
                </div>
                <h3>12 - Objętość</h3>
                <div class="cmr-form-set">
                    <label>Wymiary palet (mm)</label>
                    <div class="dim">
                        <input class="dim-input dis" type="number" v-model="formGoodsDimX">
                        <input class="dim-input dis" type="number" v-model="formGoodsDimZ">
                        <input class="dim-input dis" type="number" v-model="formGoodsDimY">
                    </div>
                </div>
                <div class="cmr-form-set">
                    <label>Objętość (m<sup>3</sup>)</label>
                    <input class="dis" type="text" v-model="formGoodsCBM">
                </div>
                <h3>16 - Przewoźnik</h3>
                <div class="cmr-form-set">
                    <label>Imię i nazwisko</label>
                    <input class="dis" type="text" v-model="formSpedName">
                </div>
                <div class="cmr-form-set">
                    <label>Firma</label>
                    <input class="dis" type="text" v-model="formSpedCompany">
                </div>
                <div class="cmr-form-set">
                    <label>Numery rejestracyjne</label>
                    <input class="dis" type="text" v-model="formSpedCarPlates">
                </div>
                <h3></h3>
                <div v-if="formClientIndex != null" class="double-btns">
                    <button class="btn btn-cmr" @click="showPdf()"> Pokaż CMR</button>
                    <button class="btn btn-cmr" @click="downloadPdf()">Pobierz CMR </button>
                    <button class="btn btn-cmr reset" @click="resetForm()">Anuluj</button>
                </div>
                <div v-else class="no-btns">
                    <p>Wybierz klienta, dla którego chcesz wypełnić list przewozowy.</p>
                </div>
                
            </div>
            
           
            </div>
        </form>
    
    <!-- <div >
        <iframe id="pdf" title="CMR" name="CMR"> </iframe>
    </div> -->

  
</template>

<script>
import { onMounted, onUnmounted, ref, watch, watchEffect } from 'vue'
import getShipmentById from '../js-components/getShipmentById'
import getAllCustomers from '../js-components/getAllCustomers'
import { useLinksStore } from '../stores/linksStore.js'
import moment from 'moment/dist/moment'
import { PDFDocument, rgb, StandardFonts } from 'pdf-lib';
import download from 'downloadjs'
import NavbarWarehouse from '../components/NavbarWarehouse.vue'
export default {
    components:{NavbarWarehouse},
    props:['shipmentId'],
    setup(props){
        const linksStore = useLinksStore()
        const clients = ref([])
        const { loadShipment, error, shipment, isPending} = getShipmentById(linksStore.url)
        const { loadAllCustomers, error:customersError, customers} = getAllCustomers(linksStore.url)
        let singleCustomer
        const isShown = ref(false)

        const formClientIndex = ref()

        // 1 - Sender
        const formSenderName = ref('Daicel Safety Systems Europe Sp z o.o.')
        const formSenderStreet = ref('ul. Sterefowa 6')
        const formSenderCity = ref('58-130 Żarów')
        const formSenderCountry = ref('PL')
        // 2 - Consignee
        const formConsigneeName = ref('')
        const formConsigneeStreet = ref('')
        const formConsigneeCity = ref('')
        const formConsigneeCountry = ref('')
        
        const formDestination = ref('')
        const formLoadingPlace = ref('')
        const formAttachment1 = ref('SDS, instruction for driver, DGD')
        const formAttachment2 = ref('Delivery Note: ')
        //6 Goods
        const formGoodsMarks1 = ref('UN 3268 SAFETY DEVICES, 9, (E)')
        const formGoodsMarks2 = ref('UN 3268 URZĄDZENIA BEZPIECZEŃSTWA, 9, (E)')
        const formGoodsMarks3 = ref('')
        const formGoodsMarks4 = ref('')
        const formGoodsWeight = ref('')
        const formGoodsDimX = ref(1200)
        const formGoodsDimZ = ref(800)
        const formGoodsDimY = ref(1100)
        const formGoodsCBM = ref(0)
        const formGoodsQty = ref(0)
        
        //16 Speditor
        const formSpedCompany = ref('')
        const formSpedName = ref('')
        const formSpedCarPlates = ref('')

        function replace(sentence) {
            let sentenceCharArray = sentence.split("")
            sentenceCharArray.forEach((char,index) => {
                switch (char) {
                    case 'ą':
                        sentenceCharArray[index] = 'a'
                        break;
                    case 'Ą':
                        sentenceCharArray[index] = 'A'
                        break;
                    case 'ć':
                        sentenceCharArray[index] = 'c'
                        break;
                    case 'Ć':
                        sentenceCharArray[index] = 'C'
                        break;
                    case 'ę':
                        sentenceCharArray[index] = 'e'
                        break;
                    case 'Ę':
                        sentenceCharArray[index] = 'E'
                        break;
                    case 'ł':
                        sentenceCharArray[index] = 'l'
                        break;
                    case 'Ł':
                        sentenceCharArray[index] = 'L'
                        break;
                    case 'ń':
                        sentenceCharArray[index] = 'n'
                        break;
                    case 'Ń':
                        sentenceCharArray[index] = 'N'
                        break;
                    case 'ó':
                        sentenceCharArray[index] = 'o'
                        break;
                    case 'Ó':
                        sentenceCharArray[index] = 'O'
                        break;
                    case 'ś':
                        sentenceCharArray[index] = 'ś'
                        break;
                    case 'Ś':
                        sentenceCharArray[index] = 'S'
                        break;
                    case 'ź':
                        sentenceCharArray[index] = 'z'
                        break;
                    case 'Ź':
                        sentenceCharArray[index] = 'Z'
                        break;
                    case 'ż':
                        sentenceCharArray[index] = 'z'
                        break;
                    case 'Ż':
                        sentenceCharArray[index] = 'Z'
                        break;
                
                    default:
                        break;
                }
            });
            const newSentence = sentenceCharArray.join("");
            return newSentence;
        }

        const getData = ()=>{
             loadShipment(props.shipmentId).then(()=>{
                shipment.value.forwarder.firstName = replace(shipment.value.forwarder.firstName)
                shipment.value.forwarder.lastName = replace(shipment.value.forwarder.lastName)
                shipment.value.forwarder.speditor = replace(shipment.value.forwarder.speditor)

                loadAllCustomers().then(()=>{
                    shipment.value.purchaseOrders.forEach(order => {
                        singleCustomer = customers.value.filter(customer => customer.shortName == order.customerShortName)
                        singleCustomer = singleCustomer.map((customer)=>{
                            return {
                                        id:customer.id, 
                                        name:replace(customer.name),
                                        shortName: replace(customer.shortName),
                                        streetAddress: replace(customer.streetAddress),
                                        cityAddress: replace(customer.cityAddress),
                                        zipCodeAddress: replace(customer.zipCodeAddress),
                                        countryAddress: replace(customer.countryAddress)
                                }
                        })
                        clients.value.push(singleCustomer[0])
                    })
                    var tempArray = clients.value.sort((a, b) => {
                    const nameA = a.id; 
                    const nameB = b.id; 
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }
                    // names must be equal
                    return 0;
                    });
                   
                    
                    tempArray = tempArray.filter((value, index, array)=>{
                        if (index > 0){
                            return array[index].shortName !== array[index-1].shortName
                        }else{
                            return array[index]
                        }
                    })
                    
                    clients.value = tempArray

                });
            })
        }
        const pdfUrl = '/src/assets/documents/cmr.pdf'
        let outputUint8Array = null
        onMounted(()=>{
          getData()
          toggleDisableInputs(true)
            
        })
        
        async function modifyPdf(pdfUrl) {
            // console.log(client)
            const pngUrl = '/src/assets/img/logoDSSEblue.png'
            const pngImageBytes = await fetch(pngUrl).then((res) => res.arrayBuffer())
            const pictogramUrl = '/src/assets/img/ADR9.png'
            const pictogramImageBytes = await fetch(pictogramUrl).then((res) => res.arrayBuffer())

            const existingPdfBytes = await fetch(pdfUrl).then(res => res.arrayBuffer())
            const pdfDoc = await PDFDocument.load(existingPdfBytes)
            const helveticaFont = await pdfDoc.embedFont(StandardFonts.Helvetica)
            const pages = pdfDoc.getPages()
            
            const pngImage = await pdfDoc.embedPng(pngImageBytes)
            const pngPictogram = await pdfDoc.embedPng(pictogramImageBytes)

            fillEachPdfPage(pages, helveticaFont, pngImage, pngPictogram)
            const pdfBytes = await pdfDoc.save().then(res => outputUint8Array = res)
            return pdfBytes
        }
        const fillEachPdfPage = (pages, font, pngImage, pngPictogram)=>{
            const pngDims = pngImage.scale(0.25)
            const pictogramDims = pngPictogram.scale(0.09)
            
            
            pages.forEach(page => {
                page.drawImage(pngImage, {
                    x: page.getWidth() / 2 - pngDims.width / 2 + 250,
                    y: page.getHeight() / 2 - pngDims.height + 410,
                    width: pngDims.width,
                    height: pngDims.height,
                })
                 page.drawImage(pngPictogram, {
                    x: page.getWidth() / 2 - pictogramDims.width / 2 - 224,
                    y: page.getHeight() / 2 - pictogramDims.height + 3,
                    width: pictogramDims.width,
                    height: pictogramDims.height,
                })
                const {  height } = page.getSize()
                const x = 50;
                const y = height;
                //1 Sender
                page.drawText(replace(formSenderName.value), {
                    x: x,
                    y: y - 70,
                    size: 9,
                    font: font,
                    color: rgb(0,0,0),
                    
                    })
                page.moveTo(x, y - 80)
                page.drawText(replace(formSenderStreet.value), { size: 9 })
                page.moveTo(x, y - 90)
                page.drawText(replace(formSenderCity.value), { size: 9 })
                page.moveTo(x, y - 100)
                page.drawText(replace(formSenderCountry.value), { size: 9 })
    
                //2 Consignee
                page.moveTo(x, y - 140)
                page.drawText(replace(formConsigneeName.value), { size: 9 })
                page.moveTo(x, y - 150)
                page.drawText(replace(formConsigneeStreet.value), { size: 9 })
                page.moveTo(x, y - 160)
                page.drawText(replace(formConsigneeCity.value), { size: 9 })
                page.moveTo(x, y - 170)
                page.drawText(replace(formConsigneeCountry.value), { size: 9 })
                //3 Destination
                page.moveTo(x, y - 220)
                page.drawText(replace(formDestination.value), { size: 9 })
                //4 Loading place
                page.moveTo(x, y - 264)
                page.drawText(replace(formLoadingPlace.value), { size: 9 })
                //5 Attachments
                page.moveTo(x, y - 298)
                page.drawText(replace(formAttachment1.value), { size: 9 })
                page.moveTo(x, y - 308)
                page.drawText(replace(formAttachment2.value), { size: 9 })
                //16 Speditor
                page.moveTo(x+280, y - 143)
                page.drawText(replace(formSpedCompany.value), { size: 9 })
                page.moveTo(x+280, y - 156)
                page.drawText(replace(formSpedName.value), { size: 9 })
                page.moveTo(x+280, y - 172)
                page.drawText(replace(formSpedCarPlates.value), { size: 12 })
                //18 Speditor comments
                page.moveTo(x+280, y - 260)
                page.drawText('', { size: 9 })
                page.moveTo(x+280, y - 270)
                page.drawText('', { size: 9 })
                //6 7 8 9 10 Marks and Nos
                page.moveTo(x , y - 350)
                page.drawText(replace(formGoodsMarks1.value), { size: 9 })
                page.moveTo(x , y - 360)
                page.drawText(replace(formGoodsMarks2.value), { size: 9, color: rgb(0.40, 0.40, 0.40) })
                page.moveTo(x, y - 375)
                page.drawText(replace(formGoodsMarks3.value), { size: 9 })
                page.moveTo(x, y - 385)
                page.drawText(replace(formGoodsMarks4.value), { size: 9, color: rgb(0.40, 0.40, 0.40) })
                page.moveTo(x, y - 400)
                page.drawText('OVERPACK used: ' + formGoodsQty.value.toString()  + ' EUR pallets', { size: 9 })
                page.moveTo(x, y - 410)
                page.drawText('Zastosowano opakowanie zbiorcze: ' + formGoodsQty.value.toString()  + ' palety typu EUR', { size: 9, color: rgb(0.40, 0.40, 0.40) })
                if(shipment.value.containerNumber.length > 3){
                    page.moveTo(x, y - 470)
                    page.drawText('CTNR#: '+ replace(shipment.value.containerNumber) + ' / ' + shipment.value.containerType, { size: 9 })
                    page.moveTo(x, y - 482)
                    page.drawText('SEAL#: '+ shipment.value.containerSealNumber, { size: 9 })

                }
                
                
                page.moveTo(x+20, y - 507)
                page.drawText('9', { size: 9 })
                page.moveTo(x+110, y - 507)
                page.drawText('3268', { size: 9 })
                page.moveTo(x+200, y - 507)
                page.drawText('N/A', { size: 9 })
                // 11 Weight
                page.moveTo(x+400, y - 350)
                page.drawText(formGoodsWeight.value.toString() + ' kg', { size: 9 })
                //12 CBM
                page.moveTo(x+470, y - 350)
                page.drawText(formGoodsCBM.value.toString(), { size: 9 })
                //13 Senders instructions
                page.moveTo(x-3, y - 545)
                page.drawText('Przewoz zgodny z 1.1.4.2.1', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 555)
                page.drawText('Carriage in accordance with 1.1.4.2.1', { size: 8 })
                page.moveTo(x-3, y - 560)
                page.drawText('_______________________________________________', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 575)
                page.drawText('1.1.3.6: Calkowita ilosc dla kazdej kategorii transportu', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x+23, y - 585)
                page.drawText('/ Total quantity for each transport category:4:.......', { size: 8 })
                page.moveTo(x-3, y - 600)
                page.drawText('1.1.3.6: Obliczona wartosc dla kazdej kategorii transportu', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x+23, y - 610)
                page.drawText('/ Calculated value for each transport category:4:0', { size: 8 })
                page.moveTo(x-3, y - 625)
                page.drawText('1.1.3.6: Obliczona wartosc punktow ADR', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x+23, y - 635)
                page.drawText('/ Sum of calculeted point value:0', { size: 8 })
                //21 Established in
                page.moveTo(x, y - 724)
                page.drawText(replace(formLoadingPlace.value), { size: 9 })
                //22 Stamp
                page.moveTo(x+10, y - 750)
                page.drawText('Daicel Safety Systems', { size: 11, color: rgb(0.50, 0.60, 0.80) })
                page.moveTo(x+27, y - 761)
                page.drawText('Europe Sp. z o.o.', { size: 11 , color: rgb(0.50, 0.60, 0.80) })
                page.moveTo(x+40, y - 772)
                page.drawText('ul. Strefowa 6', { size: 9 , color: rgb(0.50, 0.60, 0.80) })
                page.moveTo(x+39, y - 781)
                page.drawText('58-130 Zarow', { size: 9 , color: rgb(0.50, 0.60, 0.80) })
                page.moveTo(x+8, y - 790)
                page.drawText('NIP 886-27-53-552 Regon 891525310', { size: 7 , color: rgb(0.50, 0.60, 0.80) })
            });
        }
        const downloadPdf = ()=>{
            // const pdfBytes = modifyPdf(pdfUrl)
            modifyPdf(pdfUrl).then(()=>{
                const pdfBytes = outputUint8Array
                download(pdfBytes, "CMR-list-przewozowy.pdf", "application/pdf");
            })
        }
        const showPdf = ()=>{
            // const pdfBytes = modifyPdf(pdfUrl)
            isShown.value = true
            modifyPdf(pdfUrl).then(()=>{
                const pdfBytes = outputUint8Array
                const bytes  = new Uint8Array( pdfBytes ); 
                const blob   = new Blob( [ bytes ], { type: "application/pdf" } );
                const docUrl = URL.createObjectURL( blob );
                // document.getElementById('pdf').src = docUrl ;
                
                    
                var tabOrWindow = window.open(docUrl, '_blank');
                tabOrWindow.focus();
            })
        }
        const chooseClient = (index) =>{
            formClientIndex.value = index
        }
        const formWatcher = watch((formClientIndex), () => {
            if(formClientIndex.value != null){
                toggleDisableInputs(false)

                formConsigneeName.value = clients.value[formClientIndex.value].name
                formConsigneeStreet.value = clients.value[formClientIndex.value].streetAddress
                formConsigneeCity.value = clients.value[formClientIndex.value].zipCodeAddress + ' ' + clients.value[formClientIndex.value].cityAddress
                formConsigneeCountry.value = clients.value[formClientIndex.value].countryAddress
                formDestination.value = clients.value[formClientIndex.value].cityAddress + ', ' + clients.value[formClientIndex.value].countryAddress 
                formLoadingPlace.value = 'Zarow, ' + formSenderCountry.value + ', ' + moment().format('DD.MM.YYYY')
    
                formSpedCompany.value = shipment.value.forwarder.speditor
                formSpedCarPlates.value = shipment.value.forwarder.carPlates
                formSpedName.value = shipment.value.forwarder.firstName + ' ' + shipment.value.forwarder.lastName
                formGoodsQty.value = shipment.value.palletQty
                formGoodsWeight.value = shipment.value.palletQty * 425
                let cbm = ((formGoodsDimX.value/1000) * (formGoodsDimY.value/1000) * (formGoodsDimZ.value/1000))*shipment.value.palletQty
                formGoodsCBM.value = cbm.toFixed(2)
                formGoodsMarks4.value = `${shipment.value.palletQty * 40} Skrzyn kartonowych (4G)`
                formGoodsMarks3.value = `${shipment.value.palletQty * 40} Cardboard boxes (4G)`
            }
            
        })

        const formWatchPalQty = watchEffect( ()=>{
            formGoodsWeight.value = formGoodsQty.value * 425
             let cbm = ((formGoodsDimX.value/1000) * (formGoodsDimY.value/1000) * (formGoodsDimZ.value/1000))*formGoodsQty.value
            formGoodsCBM.value = cbm.toFixed(2)
            
        })

       const resetForm = ()=>{
         formClientIndex.value = null

        // 1 - Sender
         formSenderName.value = 'Daicel Safety Systems Europe Sp z o.o.'
         formSenderStreet.value = 'ul. Sterefowa 6'
         formSenderCity.value = '58-130 Żarów'
         formSenderCountry.value ='PL'
        // 2 - Consignee
         formConsigneeName.value = ''
         formConsigneeStreet.value = ''
         formConsigneeCity.value = ''
         formConsigneeCountry.value = ''
        
         formDestination.value = ''
         formLoadingPlace.value = ''
         formAttachment1.value = ''
         formAttachment2.value = ''
        //6 Goods
         formGoodsMarks1.value = 'UN 3268 SAFETY DEVICES, CLASS 9, (E)'
         formGoodsMarks2.value = 'UN 3268 URZĄDZENIA BEZPIECZEŃSTWA, KLASA 9, (E)'
         formGoodsMarks3.value = ''
         formGoodsMarks4.value = ''
         formGoodsWeight.value = ''
         formGoodsDimX.value = 1200
         formGoodsDimZ.value = 800
         formGoodsDimY.value = 1100
         formGoodsCBM.value = 0
         formGoodsQty.value = 0
         toggleDisableInputs(true)
       }
       const toggleDisableInputs = (value)=>{
         let domElements = document.getElementsByClassName("dis")
                for (let index = 0; index < domElements.length; index++) {
                    domElements[index].disabled = value
                }
       }
       
        onUnmounted(()=>{
            formWatcher()
            formWatchPalQty()
        })

        return {
            chooseClient, resetForm,
            error, shipment, isPending,
                customers, customersError,
                moment, getData,
                clients, isShown,
                downloadPdf, showPdf,
                formClientIndex,
                formSenderName, formSenderStreet, formSenderCity, formSenderCountry,
                formConsigneeName, formConsigneeStreet, formConsigneeCity, formConsigneeCountry,
                formDestination, formAttachment1, formAttachment2,formLoadingPlace, 
                formGoodsMarks1, formGoodsMarks2, formGoodsMarks3,formGoodsMarks4, 
                formGoodsWeight, formGoodsDimX, formGoodsDimY, formGoodsDimZ, formGoodsCBM, formGoodsQty,
                formSpedCarPlates, formSpedCompany, formSpedName

            }
    }

}
</script>

<style>
.no-btns{
    display: flex;
    justify-content: center;
    font-size:1.0vw;
    font-weight: 200;
    color: #a1ff6a;
    margin: 2vh 1vh;
    text-align: center;
}
.greenLabel{
    color: #a1ff6a;
}
.clients-icons-ctnr{
    display: flex;
}
.choose-clinet-head{
    font-size: 0.7vw;
    margin: 0 0 1vh 0;
    font-weight: 200;
}

.cmr-client-header{
    border-top: solid #fff 0.15vh;
    display: flex;
    justify-content: space-between;
    margin-top: 1.5vh;
}
.cmr-client-header .top-headers{
    border-top: none;
    margin-top:0;
    padding-top:0.5vh;
    font-size: 1.6vw;
}
.cmr-client-header .top-headers.yell{
    margin-left: 1vh;
    color: #ffbb00;
}
.client-icon{
    cursor: pointer;
    display: flex;
    flex-direction: row;
    width: 5vw;
    height: 4vh;
    background: linear-gradient(to right bottom, rgb(24, 76, 174), rgb(16, 41, 82));
    margin: 0.5vh 0.8vw 0.5vh 0;
    justify-content: center;
    align-items: center;
    border: solid #ffffff 0.15vh;
}
.client-icon:hover{
    background: linear-gradient(to right bottom, #ffbb00, #ad7f00);
    /* color: #222; */
    box-shadow: 0.5vh 0.5vh 0.8vh #000000a2;
    transform: scale(1.1);
    transition: 200ms ease-in-out;
}
.notChosenIcon{
    background: linear-gradient(to right bottom, rgb(89, 89, 89), rgb(58, 58, 58));
}
.client-icon p{
    font-size: 0.7vw;
}
.cmr-container{
    display: grid;
    grid-template-columns: 30vw 30vw ;
    gap: 2vw;
    margin: 2vh 2vw;
}

.cmr-form{
    display: flex;
    
    justify-content: center;
    
}
.cmr-form .reset{
    background: linear-gradient(to right bottom, #ff2727, #ae1a1a);
    color: #fff;
}
.cmr-form .reset:hover{
    background: linear-gradient(to right bottom, #ff9375, #e92b2b);

}

.cmr-form .cmr-form-set{
    display: grid;
    grid-template-columns: 10vw 18vw;
    gap: 1vw;
    /* justify-content: space-between; */
}
.cmr-form .cmr-form-set label{
    padding: 0.6vh 0;
    font-size: 0.7vw;
    font-weight: 200;
}
.cmr-form .cmr-form-set input, .cmr-form .cmr-form-set select{
    height: 2.3vh;
    width: 100%;
    padding-left: 0.7vw ;
    font-size: 1.2vh;
}
.cmr-form-set .dim{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 1.1vw;
    
}
.cmr-form h3{
    border-top: solid 1px #fff;
    margin: 1.5vh 0 0.2vh 0;
    padding: 0.5vh 0vw 0.5vh 0;
    font-size: 0.8vw;
    
}

#pdf{
    display: flex;
    justify-content: center;
    align-self: center;
    margin: 0.5vh 0vh;
    padding: 0;
    width: 99%;
    height: 80vh;
    border: none;
    background: linear-gradient(to right bottom, #222, #555);
}
.clients-buttons-ctnr{
    display: flex;
    justify-content: center;
    align-items: center;
    /* flex-direction: column; */
    
}
.double-btns{
    margin-top:3vh;
    display: flex;
    flex-direction: row;
    justify-content: space-around
    
}
.btn-cmr{
    width: 6vw;
    margin: 1vh ;
}

</style>