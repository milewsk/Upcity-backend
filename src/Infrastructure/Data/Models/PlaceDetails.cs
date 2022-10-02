using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class PlaceDetails : EntityBase
    {
        public string Description { get; set; }       
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisterFirm { get; set; }
        public string NIP { get; set; }
        //menu?

        // opinie
        //
        public ICollection<PlaceOpinion> PlaceOpinions { get; set; }

        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
    }
}
