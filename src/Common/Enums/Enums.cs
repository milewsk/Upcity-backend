using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public enum DayOfWeek
    {
        Poniedziałek = 1,
        Wtorek = 2,
        Środa = 3,
        Czwartek = 4,
        Piątek = 5,
        Sobota = 6,
        Niedziela = 7
    }

    public enum PaymentStatus
    {
        UnPaid = 0,
        Paid = 1
    }

    public enum TagType
    {
        Place = 1,
        Product = 2
    }
}
