using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IRoleRepository : IGenericRepository<Irole>
{
    Task DeleteIdentityRole(string id);
    Task<Irole> GetIdentityRole(string id);
}