using ApplicationCore.Interfaces;
using ApplicationCore.Logging;
using ApplicationCore.Repositories;
using ApplicationCore.Repositories.Interfaces;
using ApplicationCore.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
     public static class ConfigureCoreServices
    {
        public static void AddWebServices(IServiceCollection services, IConfiguration configuration)
        {
            
            //services.AddSingleton(IPasswordHasher<T>, PasswordHasher<T>);
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();

        }
    }
}
