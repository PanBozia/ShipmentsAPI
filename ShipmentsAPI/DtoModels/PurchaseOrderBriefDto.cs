using System;

namespace ShipmentsAPI.DtoModels
{
    public class PurchaseOrderBriefDto
    {
        public Guid Id { get; set; }
        public string PONumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Category { get; set; }
        public string IncotermName { get; set; }
        public string CustomerShortName { get; set; }
        public string CustomerCity { get; set; }
    }
}
