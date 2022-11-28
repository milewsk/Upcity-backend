using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
   public class PrivateMessage : EntityBase
    {
        public Guid UserID { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
