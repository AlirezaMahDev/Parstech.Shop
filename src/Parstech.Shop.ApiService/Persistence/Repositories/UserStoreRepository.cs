using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class UserStoreRepository:GenericRepository<UserStore>,IUserStoreRepository
    {
        private readonly DatabaseContext _Context;
        private readonly IdentityContext _iContext;

        public UserStoreRepository(DatabaseContext context, IdentityContext iContext) : base(context)
        {
            _Context = context;
            _iContext = iContext;
        }

        public async Task<UserStore> GetStoreOfUser(int userId)
        {
            return await _Context.UserStores.FirstOrDefaultAsync(u => u.UserId == userId);
        }
        public async Task<UserStore> GetStoreByLatinName(string latinName)
        {
            return await _Context.UserStores.FirstOrDefaultAsync(u => u.LatinStoreName == latinName);
        }
        public async Task<UserStore> GetStoreByName(string name)
        {
            return await _Context.UserStores.FirstOrDefaultAsync(u => u.StoreName == name);
        }
        public async Task<List<UserStore>> GetStoreList()
        {
            var role =await _iContext.Roles.FirstOrDefaultAsync(u => u.Name == "Store");
            List<UserStore> list = new List<UserStore>();
            var iuserRoles =await _iContext.UserRoles.Where(u => u.RoleId == role.Id).ToListAsync();
            foreach (var item in iuserRoles)
            {
                var user =await _Context.Users.FirstOrDefaultAsync(u => u.UserId == item.UserId);
                if (_Context.UserStores.Any(u=>u.UserId==user.Id))
                {
                    var userStore = await _Context.UserStores.FirstOrDefaultAsync(u => u.UserId == user.Id);
                    list.Add(userStore);
                }
                else
                {
                    UserStore us = new UserStore()
                    {
                        UserId = user.Id
                    };
                    list.Add(us);
                }

                
            }
            return list;
        }

        public async Task<bool> CheckUserStoreExist(int userId)
        {
            if (await _Context.UserStores.AnyAsync(u => u.UserId == userId))
            {
                return true;
            }

            return false;
        }
    }
}
