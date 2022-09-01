using ApplicationCore.Interfaces;
using ApplicationCore.Logging;
using ApplicationCore.Repositories;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
     public static class ConfigureCoreServices
    {
        public static void AddWebServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();

        }
    }
}
