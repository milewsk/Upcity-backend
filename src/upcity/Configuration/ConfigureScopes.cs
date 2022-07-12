
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    class ConfigureScopes
    {
        private IServiceCollection _services;

        ConfigureScopes(IServiceCollection services)
        {
            _services = services;
        }

        public void InitializeScopes()
        {
            _services.AddScoped<IUserRepository, UserRepository>();
            _services.AddScoped<JwtService>();

        }
    }
}
