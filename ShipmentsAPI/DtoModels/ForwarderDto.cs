﻿using System;
using System.Reflection.PortableExecutable;

namespace ShipmentsAPI.Entities
{
    public class ForwarderDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Speditor { get; set; }
        public string PhoneNumber { get; set; }
        public string CarPlates { get; set; }
       

    }
}
