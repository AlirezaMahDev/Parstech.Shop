using AutoMapper;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    private DatabaseContext _context;
    private IMapper _mapper;

    public TicketRepository(DatabaseContext context,
        IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }
}