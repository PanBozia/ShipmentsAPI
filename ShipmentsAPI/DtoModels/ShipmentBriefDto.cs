using ShipmentsAPI.Entities;
using System.Collections.Generic;
using System;

namespace ShipmentsAPI.DtoModels
{
    public class ShipmentBriefDto
    {
        public Guid Id { get; set; }
        public DateTime ETD { get; set; }
        public DateTime? TimeOfDeparture { get; set; }
        public int PalletQty { get; set; }
    }
}
