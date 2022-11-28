using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto.User
{
   public class UserShortcutResult
    {
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

    }
}
