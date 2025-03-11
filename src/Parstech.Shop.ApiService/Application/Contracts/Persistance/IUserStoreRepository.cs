using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IUserStoreRepository:IGenericRepository<UserStore>
    {
        Task<UserStore> GetStoreOfUser(int userId);
        Task<UserStore> GetStoreByLatinName(string latinName);
        Task<UserStore> GetStoreByName(string name);
        Task<List<UserStore>> GetStoreList();
        Task<bool> CheckUserStoreExist(int userId);
    }
}
