using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class RahkaranOrderRepository : GenericRepository<RahkaranOrder>, IRahkaranOrderRepository
{
    private readonly DatabaseContext _context;

    public RahkaranOrderRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}