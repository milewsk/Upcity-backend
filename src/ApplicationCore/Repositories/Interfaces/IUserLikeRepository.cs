using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repositories.Interfaces
{
    public interface IUserLikeRepository
    {
        Task<List<UserLike>> GetUserLikeListAsync(Guid userID);
        Task<List<Guid>> GetPlaceIDsAsync(Guid userID);
        Task<bool> CheckExistance(Guid userID, Guid placeID);
        Task<bool> AddUserLikeAsync(Guid userID, Guid placeID);
        Task<bool> RemoveUserLikeAsync(Guid userID, Guid placeID);
    }
}
