using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Inbox
{
   public class CreateMessageModel
    {
        public Guid PlaceID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
