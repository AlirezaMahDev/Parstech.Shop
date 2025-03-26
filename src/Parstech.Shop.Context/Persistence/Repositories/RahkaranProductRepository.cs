using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class RahkaranProductRepository : GenericRepository<RahkaranProduct>, IRahkaranProductRepository
{
    private readonly DatabaseContext _context;
    public RahkaranProductRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}