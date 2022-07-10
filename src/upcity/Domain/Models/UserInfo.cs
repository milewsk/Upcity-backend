using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Domain.Models
{
    public class UserInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public string Image { get; set; }
    }
}
