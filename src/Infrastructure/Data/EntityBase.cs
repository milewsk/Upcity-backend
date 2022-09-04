using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class EntityBase
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
