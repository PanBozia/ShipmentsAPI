using ShipmentsAPI.Entities;
using System;

namespace ShipmentsAPI.DtoModels
{
    public class QueryShipments
    {
        public DateTime TimeOfDeparture { get; set; }
        public bool HasPriority { get; set; }
        public string Comment { get; set; }
        public int WarehouseAreaId { get; set; }
        public int PalletQty { get; set; }
        public string CarPlates { get; set; }
        public int StatusId { get; set; }
        public string ContainerNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
