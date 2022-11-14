using System;
using System.Reflection.PortableExecutable;

namespace ShipmentsAPI.Entities
{
    public class Forwarder
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speditor { get; set; }
        public string PhoneNumber { get; set; }
        public string CarPlates { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public string ContainerSealNumber { get; set; }

    }
}
