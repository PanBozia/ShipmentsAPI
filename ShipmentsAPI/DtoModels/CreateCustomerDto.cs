using System;
using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string CityAddress { get; set; }
        [Required]
        public string ZipCodeAddress { get; set; }
        [Required]
        public string CountryAddress { get; set; }

    }
}
