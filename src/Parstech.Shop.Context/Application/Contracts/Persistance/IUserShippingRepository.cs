using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IUserShippingRepository:IGenericRepository<UserShipping>
{
    Task<List<UserShippingDto>> GetShippingOfUser(int userId);
    Task<UserShipping> GetFirstShippingOfUser(int userId);
        
}