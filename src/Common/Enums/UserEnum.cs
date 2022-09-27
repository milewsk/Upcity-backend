using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Enums
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

    public enum UserClaimsEnum
    {
        User = 1,
        Worker = 2,
        Owner = 3,
        Admin = 4
    }
}
