using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Tag
{
    public class CreateTagModel
    {
        public string Name { get; set; }

        public string Color { get; set; }
        public TagType Type { get; set; }
    }
}
