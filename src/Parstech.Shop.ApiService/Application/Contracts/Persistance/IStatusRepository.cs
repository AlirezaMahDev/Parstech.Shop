using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IStatusRepository : IGenericRepository<Status>
{
    Task<Status?> GetStatusByLatinName(string name);
}