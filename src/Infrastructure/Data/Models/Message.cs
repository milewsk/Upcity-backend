using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class Message : EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
