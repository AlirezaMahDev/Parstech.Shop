using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ProductTypeRepository:GenericRepository<ProductType>,IProductTypeRepository
{
    private readonly DatabaseContext _context;

    public ProductTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}