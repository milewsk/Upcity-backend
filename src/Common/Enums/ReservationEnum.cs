using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Enums
{
    public enum CreateReservationResult
    {
        Ok= 1, 
        DateIsIncorrect = 2,
        Error = 3
    }

    public enum ReservationStatus
    {
        Confirmed= 0,
        Pending = 1,
        Expired = 3,
        Canceled = 4
    }
}
