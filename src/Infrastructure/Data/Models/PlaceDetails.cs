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
        //menu?

        //
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
    }
}
