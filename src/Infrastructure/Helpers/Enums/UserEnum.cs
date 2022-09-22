﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infrastructure.Helpers.Enums
{

    public enum UserLoginResult
    {
        Ok = 1,
        Error = 2,
        [Description("User is not found in database")]
        UserNotFound = 3,
        [Description("Password is incorrect")]
        WrongPassword = 4
    }

    public enum UserRegisterResult
    {
        Ok = 1,
        Error = 2,
        EmailAlreadyTaken = 3,

    }
}