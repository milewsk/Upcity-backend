using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Coordinates : EntityBase
    {
        public Point Location { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
    }
}
