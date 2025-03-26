using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IFormCreditRepository:IGenericRepository<FormCredit>
{
    Task<List<FormCredit>> Search(string Filter, string FromDate, string ToDate);
}