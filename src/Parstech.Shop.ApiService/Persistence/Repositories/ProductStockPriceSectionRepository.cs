using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductStockPriceSectionRepository : GenericRepository<ProductStockPriceSection>,
    IProductStockPriceSectionRepository
{
    private readonly DatabaseContext _context;

    public ProductStockPriceSectionRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductStockPriceSection>> GetSectionOfProductStockPrice(int id)
    {
        return await _context.ProductStockPriceSections.Include(u => u.Section)
            .Where(u => u.ProductStockPriceId == id)
            .ToListAsync();
    }
}