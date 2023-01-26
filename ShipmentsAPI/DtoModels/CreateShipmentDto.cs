using ShipmentsAPI.Entities;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreateShipmentDto
    {
        [Required]
        public DateTime ETD { get; set; }
        
        public DateTime? TimeOfDeparture { get; set; }
        public bool HasPriority { get; set; } = false;
        public int PalletQty { get; set; }
        public string CreatedByUser { get; set; } = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public string ContainerSealNumber { get; set; }
        public string Comment { get; set; }
        public int? WarehouseAreaId { get; set; }
        public Guid? ForwarderId { get; set; }

    }
}
