using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IStatusRepository : IGenericRepository<Status>
{
    Task<Status?>GetStatusByLatinName(string name);
}