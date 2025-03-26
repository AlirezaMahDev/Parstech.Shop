using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IRoleRepository:IGenericRepository<Irole>
{
    Task DeleteIdentityRole(string id);
    Task<Irole> GetIdentityRole(string id);
}