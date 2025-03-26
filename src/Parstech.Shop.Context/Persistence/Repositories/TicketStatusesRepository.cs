using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class TicketStatusesRepository : GenericRepository<TicketStatus>, ITicketStatusesRepository
{
    private DatabaseContext _context;
    public TicketStatusesRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}