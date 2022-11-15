using System;
using System.Collections.Generic;

namespace ShipmentsAPI.DtoModels
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ClientNumber { get; set; }
        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string ZipCodeAddress { get; set; }
        public List<PurchaseOrderDto> PurchaseOrders { get; set; }
    }
}
