using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    private DatabaseContext _context;

    public StatusRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Status?> GetStatusByLatinName(string name)
    {
        return await _context.Statuses.FirstOrDefaultAsync(u => u.StatusLatinName == name);
    }
}