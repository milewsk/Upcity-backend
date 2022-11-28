using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class Place : EntityBase
    {
        public Guid OwnerID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte IsActive { get; set; }
        public decimal StandardPrice { get; set; }

        public PlaceDetails PlaceDetails { get; set; }
        public Coordinates Coordinates { get; set; }
        public PlaceMenu PlaceMenu { get; set; }
        public ICollection<PlaceOpeningHours> PlaceOpeningHours { get; set; }


        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<PlaceOpinion> PlaceOpinions { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public IList<PlaceTag> PlaceTags { get; set; }


        public Place(string name, string image, byte isActive)
        {
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            Name = name;
            Image = image;
            IsActive = isActive;
        }
    }
}
