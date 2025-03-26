using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class RepresentationRepository:GenericRepository<Representation>,IRepresentationRepository
{
    private readonly DatabaseContext _context;

    public RepresentationRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}