using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IUserProductRepository:IGenericRepository<UserProduct>
{

    Task<List<UserProduct?>>GetUserProductsByUsername(string userName,string type);
    Task<bool>ExistFourUserProductByUserName(string userName ,string type);
        
}