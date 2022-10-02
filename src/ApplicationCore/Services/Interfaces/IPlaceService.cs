using Infrastructure.Helpers;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helpers.Enums;
using Common.Dto.Models;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPlaceService
    {
        //fajne jeśli chcemy zwrócić odpowiedź co do konkretnego błędu od razu
        //nazwewnictwo sefix - nazwa metody + result , zwracany obiekt + Dto , nazwa kotrolera + nazwa metody + model
        Task<Tuple<PlaceCreatePlaceStatusResult, PlaceResult>> CreatePlaceAsync(CreatePlaceModel model);
        Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync();
    }
}
