using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Application.DTOs.IUserRole;
using Shop.Application.DTOs.User;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IUserRepository:IGenericRepository<User>
    {

        Task<List<IUserRoleDto>> GetUserRoles(string UserId);
        bool PaswordsValid(string password, string confirmPassword);
        Task<User?> GetUserByUserName(string userName);
        Task<IdentityUser> GetIUserByUserName(string userName);
        int GetCountOfUsers();

        Task<bool> ExistUserCategury(string userName);
        Task<string?> GetUserCategury(int id);

        
    }
}
