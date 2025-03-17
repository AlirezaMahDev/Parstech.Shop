using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserShippingRepository : IGenericRepository<UserShipping>
{
    Task<List<UserShippingDto>> GetShippingOfUser(int userId);
    Task<UserShipping> GetFirstShippingOfUser(int userId);
}