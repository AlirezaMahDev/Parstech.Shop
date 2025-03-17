using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserShippingRepository : IGenericRepository<UserShipping>
{
    Task<List<UserShippingDto>> GetShippingOfUser(int userId);
    Task<UserShipping> GetFirstShippingOfUser(int userId);
}