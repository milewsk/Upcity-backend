using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Inbox
{
    public class MessageResult
    {
        public Guid? PlaceID { get; set; }
        public string PlaceName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
