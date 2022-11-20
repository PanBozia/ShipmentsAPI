using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Entities
{
    public class PurchaseOrder
    {
        public Guid Id { get; set; }
        public string PONumber { get; set; }
        
        public DateTime DeliveryDate { get; set; }
        
        
        public int IncotermId { get; set; }
        public virtual Incoterm Incoterm { get; set; }
        
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<Shipment> Shipments { get; set; }
    }
}
