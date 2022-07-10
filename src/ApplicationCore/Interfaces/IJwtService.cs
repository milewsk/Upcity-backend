using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Interfaces
{
    public interface IJwtService
    {
        public string Generate(Guid id);

        public JwtSecurityToken Verify(string jwt);
    }
}
