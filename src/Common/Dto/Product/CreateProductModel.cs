using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Models
{
    //idk if we wanna add discount 
    public class CreateProductModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Guid CategoryID { get; set; }
        public List<Guid> TagsID { get; set; }

        public Guid PlaceID { get; set; }
    }
}
