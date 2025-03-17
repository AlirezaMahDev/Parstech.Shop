using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductRelatedRepository : GenericRepository<ProductRelated>, IProductRelatedRepository
{
    private readonly DatabaseContext _context;

    public ProductRelatedRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductRelated>> GetRelatedProductsByProductId(int productId)
    {
        List<ProductRelated> list = await _context.ProductRelateds.Where(u => u.ProductId == productId).ToListAsync();
        return list;
    }
}