using System;

namespace ShipmentsAPI.DtoModels
{
    public class UpdateShipmentDto
    {
        public Guid ShipmentId { get; set; }
        public DateTime ETD { get; set; }
        public DateTime? TimeOfDeparture { get; set; }
        public bool HasPriority { get; set; } = false;
        public int PalletQty { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public string ContainerSealNumber { get; set; }
        public string Comment { get; set; }
        public int? WarehouseAreaId { get; set; }
        public Guid? ForwarderId { get; set; }
        public int StatusId { get; set; }
    }
}
