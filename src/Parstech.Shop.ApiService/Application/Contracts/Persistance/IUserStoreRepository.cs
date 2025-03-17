using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserStoreRepository : IGenericRepository<UserStore>
{
    Task<UserStore> GetStoreOfUser(int userId);
    Task<UserStore> GetStoreByLatinName(string latinName);
    Task<UserStore> GetStoreByName(string name);
    Task<List<UserStore>> GetStoreList();
    Task<bool> CheckUserStoreExist(int userId);
}