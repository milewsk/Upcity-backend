using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infrastructure.Helpers.Enums
{

    public enum PlaceCreatePlaceStatusResult
    {
        Ok = 1,
        Error = 2,
        [Description("Place already exist")]
        PlaceAlreadyExist = 3,
        [Description("Data passed is incorrect")]
        IncorrectData = 4
    }

    public enum PlaceEditPlaceStatusResult {
        Ok = 1,
        Error = 2,
        [Description("Data passed is incorrect")]
        IncorrectData = 3,
        [Description("Place not provided")]
        PlaceNotProvided = 4


    }
}
