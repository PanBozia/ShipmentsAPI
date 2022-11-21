using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace ShipmentsAPI.Entities
{
    public class CreateForwarderDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Speditor { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string CarPlates { get; set; }
        

    }
}
