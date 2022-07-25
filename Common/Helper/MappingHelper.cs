using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helper
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


            });
        }
    }
}
