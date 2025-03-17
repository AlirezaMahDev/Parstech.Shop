using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class RahkaranUserRepository : GenericRepository<RahkaranUser>, IRahkaranUserRepository
{
    private readonly DatabaseContext _context;

    public RahkaranUserRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}