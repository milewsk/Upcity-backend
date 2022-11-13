using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Place
{
    public class PlaceResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterFirm { get; set; }
        public string NIP { get; set; }
        public int MaxSeatNumber { get; set; }
    }
}
