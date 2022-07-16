using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Models
{
    public class UserInfo
    {
        public Guid ID { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
    }
}
