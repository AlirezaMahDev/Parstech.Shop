using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductLogTypeRepository : GenericRepository<ProductLogType>, IProductLogTypeRepository
{
    private readonly DatabaseContext _context;

    public ProductLogTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}