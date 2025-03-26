using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    private DatabaseContext _context;
    public StatusRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Status?> GetStatusByLatinName(string name)
    {
        return await _context.Statuses.FirstOrDefaultAsync(u=>u.StatusLatinName == name);	
    }
}