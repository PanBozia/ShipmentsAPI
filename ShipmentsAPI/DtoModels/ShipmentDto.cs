using ShipmentsAPI.Entities;
using System.Collections.Generic;
using System;

namespace ShipmentsAPI.DtoModels
{
    public class ShipmentDto
    {
        public Guid Id { get; set; }
        public DateTime ETD { get; set; }
        public DateTime? TimeOfDeparture { get; set; }
        public bool HasPriority { get; set; } = false;
        public string Comment { get; set; }
        public WarehouseAreaDto WarehouseArea { get; set; }

        public ForwarderDto Forwarder { get; set; }
        public virtual StatusDto Status { get; set; }

        public virtual List<PurchaseOrderDto> PurchaseOrders { get; set; }
    }
}
