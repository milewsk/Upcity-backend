using Common.Dto.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.User
{
    public class CreateOwnerWithPlaceModel : CreateUserModel
    {
        public string Name { get; set; }
        public string PlaceImage { get; set; }
        public IFormFile PlaceImageFile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterFirm { get; set; }
        public string NIP { get; set; }
        public int MaxSeatNumber { get; set; }

        public List<Guid> TagIDs { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
