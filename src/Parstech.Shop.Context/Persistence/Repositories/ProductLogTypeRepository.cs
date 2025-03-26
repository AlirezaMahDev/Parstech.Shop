using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ProductLogTypeRepository:GenericRepository<ProductLogType>,IProductLogTypeRepository
{
    private readonly DatabaseContext _context;

    public ProductLogTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}