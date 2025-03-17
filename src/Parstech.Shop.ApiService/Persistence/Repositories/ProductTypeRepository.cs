using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    private readonly DatabaseContext _context;

    public ProductTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}