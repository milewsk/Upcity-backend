using AutoMapper;
using Infrastructure.Data.Dto;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class MappingHelper
    {
        public static MapperConfiguration MapperConfig { get; set; }
        public static IMapper Mapper
        {
            get
            {
                return MapperConfig.CreateMapper();
            }
        }

        public static void RegisterMappings()
        {
            MapperConfig = new MapperConfiguration(cfg => {

                cfg.CreateMap<UserDto, User>();
            });
        }
    }
}
