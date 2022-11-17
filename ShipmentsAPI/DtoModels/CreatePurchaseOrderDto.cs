using System;
using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreatePurchaseOrderDto
    {
        [Required]
        public string PONumber { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public int PalletQty { get; set; }
        public int IncotermId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
    }
}
