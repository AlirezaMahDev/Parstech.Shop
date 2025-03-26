using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IUserStoreRepository:IGenericRepository<UserStore>
{
    Task<UserStore> GetStoreOfUser(int userId);
    Task<UserStore> GetStoreByLatinName(string latinName);
    Task<UserStore> GetStoreByName(string name);
    Task<List<UserStore>> GetStoreList();
    Task<bool> CheckUserStoreExist(int userId);
}