using Microsoft.AspNetCore.Identity;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserRepository : IGenericRepository<User>
{
    Task<List<IUserRoleDto>> GetUserRoles(string UserId);
    bool PaswordsValid(string password, string confirmPassword);
    Task<User?> GetUserByUserName(string userName);
    Task<IdentityUser> GetIUserByUserName(string userName);
    int GetCountOfUsers();

    Task<bool> ExistUserCategury(string userName);
    Task<string?> GetUserCategury(int id);
}