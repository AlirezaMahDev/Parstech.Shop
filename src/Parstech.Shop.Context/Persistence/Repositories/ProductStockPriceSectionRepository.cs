using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ProductStockPriceSectionRepository:GenericRepository<ProductStockPriceSection>,IProductStockPriceSectionRepository
{
    private readonly DatabaseContext _context;
    public ProductStockPriceSectionRepository(DatabaseContext context):base(context)
    {
        _context = context;
    }

    public async Task<List<ProductStockPriceSection>> GetSectionOfProductStockPrice(int id)
    {
        return await _context.ProductStockPriceSections.Include(u=>u.Section).Where(u=>u.ProductStockPriceId==id).ToListAsync();
    }
}