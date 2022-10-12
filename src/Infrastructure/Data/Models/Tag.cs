using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Tag : EntityBase
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public byte IsActive { get; set; }
        public TagType Type { get; set; }
        public IList<PlaceTag> PlaceTags { get; set; }
        public IList<ProductTag> ProductTags { get; set; }
    }
}
