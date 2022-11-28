using ShipmentsAPI.Entities;
using System.Collections.Generic;
using System;

namespace ShipmentsAPI.DtoModels
{
    public class PurchaseOrderDto
    {
        public Guid Id { get; set; }
        public string PONumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Category { get; set; }
        public string IncotermName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerShortName { get; set; }
        public List<ShipmentBriefDto> Shipments { get; set; }
    }
}
