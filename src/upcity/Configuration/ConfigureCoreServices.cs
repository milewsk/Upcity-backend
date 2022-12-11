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
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPlaceCategoryRepository, PlaceCategoryRepository>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPlaceMenuService, PlaceMenuSerivce>();


            services.AddScoped<IJwtService, JwtService>();

        }
    }
}
