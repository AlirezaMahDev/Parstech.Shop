using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class RepresentationRepository : GenericRepository<Representation>, IRepresentationRepository
{
    private readonly DatabaseContext _context;

    public RepresentationRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}