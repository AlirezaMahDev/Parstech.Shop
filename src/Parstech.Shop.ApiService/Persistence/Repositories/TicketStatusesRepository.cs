using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class TicketStatusesRepository : GenericRepository<TicketStatus>, ITicketStatusesRepository
{
    private DatabaseContext _context;

    public TicketStatusesRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}