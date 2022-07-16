using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Table
    {
        public Guid ID { get; set; }
        [Range(0,1)]
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public int ChairsAmount { get; set; }
        public int IsReserved { get; set; }
        public Place Place { get; set; }
        public Guid PlaceID { get; set; }
         
    }
}
