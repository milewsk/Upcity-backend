﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.Reservation
{
    public class CreateReservationModel
    {
        public Guid TableID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
