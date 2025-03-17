using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class FormCreditRepository : GenericRepository<FormCredit>, IFormCreditRepository
{
    private readonly DatabaseContext _context;

    public FormCreditRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<FormCredit>> Search(string Filter, string FromDate, string ToDate)
    {
        IQueryable<FormCredit> Result = _context.FormCredits;
        if (Filter != null && Filter != "")
        {
            Result = Result.Where(u =>
                u.Name.Contains(Filter) ||
                u.Family.Contains(Filter));
        }

        return await Result.OrderByDescending(u => u.Id).ToListAsync();
    }
}