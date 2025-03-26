using AutoMapper;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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