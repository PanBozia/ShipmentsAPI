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
        public int? PalletQty { get; set; }
        public int IncotermId { get; set; }
        public Guid CustomerId { get; set; }
        public List<ShipmentDto> Shipments { get; set; }
    }
}
