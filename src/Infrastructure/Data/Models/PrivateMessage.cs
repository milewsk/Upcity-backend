﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Models
{
    public class PrivateMessage : Message
    {
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
