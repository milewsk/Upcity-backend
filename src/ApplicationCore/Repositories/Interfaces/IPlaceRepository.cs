using ApplicationCore.Interfaces;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IPlaceRepository : IRepository<Place>
    {
        Task<List<Place>> GetListBySearchStringAsync(string searchedText);
    }
}
