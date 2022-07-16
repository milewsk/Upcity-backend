using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserByGuid(Guid ID);
        User LoginUser(User user);
        CreateUser();


    }
}
