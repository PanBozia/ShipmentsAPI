using System;
using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreatePurchaseOrderDto
    {
        [Required]
        public string PONumber { get; set; }
        public string Category { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        
        [Required]
        public int IncotermId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
    }
}
