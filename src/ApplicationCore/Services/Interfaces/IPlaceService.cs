using Infrastructure.Helpers;
using Infrastructure.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Interfaces
{
    public interface IPlaceService
    {

        //fajne jeśli chcemy zwrócić odpowiedź co do konkretnego błędu od razu
        //nazwewnictwo sefix - nazwa metody + result , zwracany obiekt + Dto , nazwa kotrolera + nazwa metody + model
        Task<Tuple<PlaceCreatePlaceStatusResult, PlaceDto>> CreatePlaceAsync();

        Task<Tuple<PlaceEditPlaceStatusResult, bool>> EditPlaceAsync();
    }
}
