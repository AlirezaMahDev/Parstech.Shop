using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IFormCreditRepository : IGenericRepository<FormCredit>
{
    Task<List<FormCredit>> Search(string Filter, string FromDate, string ToDate);
}