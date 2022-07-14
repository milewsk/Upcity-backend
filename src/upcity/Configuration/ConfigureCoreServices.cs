
using ApplicationCore.Repositories;
using ApplicationCore.Repositories.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
     public static class ConfigureCoreServices
    {
        public static void AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<JwtService>();

        }
    }
}
