using AutoMapper;
using Common.Dto;
using Common.Dto.Place;
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

                //  cfg.CreateMap<List<Place>, List<PlaceShortcutResult>>();

                cfg.CreateMap<Place, PlaceShortcutResult>()
                        .ForMember(dest => dest.CloseHour , opt => opt.MapFrom(src => src.PlaceOpeningHours.Closes.ToString()))
                        .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => src.PlaceOpeningHours.Opens.ToString()))
                        .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => src.PlaceOpeningHours.Opens.ToString()))
                        ;
            });
        }
    }
}
