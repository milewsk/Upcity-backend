using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class ProductTag
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Product Product { get; set; }
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
