using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace ShipmentsAPI.Entities
{
    public class Customer
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ClientNumber { get; set; }
        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string ZipCodeAddress { get; set; }

        public virtual List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
