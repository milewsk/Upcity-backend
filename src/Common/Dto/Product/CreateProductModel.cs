﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Models
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Guid CategoryID { get; set; }
    }
}
