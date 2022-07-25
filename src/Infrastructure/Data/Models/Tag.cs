using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Tag
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public string Name { get; set; }

        public IList<PlaceTag> PlaceTags { get; set; }
        public IList<ProductTag> ProductTags { get; set; }
    }
}
