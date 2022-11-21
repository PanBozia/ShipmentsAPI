using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public DateTime ETD { get; set; }
        public DateTime? TimeOfDeparture { get; set; }
        public bool HasPriority { get; set; } = false;
        public string Comment { get; set; }
        public int? PalletQty { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public string ContainerSealNumber { get; set; }
        public int WarehouseAreaId { get; set; }
        public virtual WarehouseArea WarehouseArea { get; set; }

        public Guid ForwarderId { get; set; }
        public virtual Forwarder Forwarder { get; set; }

        public int StatusId { get; set; } = 1;
        public virtual Status Status { get; set; }

        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
