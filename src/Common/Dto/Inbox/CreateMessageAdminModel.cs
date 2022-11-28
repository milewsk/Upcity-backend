using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Inbox
{
   public  class CreateMessageAdminModel
    {
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
