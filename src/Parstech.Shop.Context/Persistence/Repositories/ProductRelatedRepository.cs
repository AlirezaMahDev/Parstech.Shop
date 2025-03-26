using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ProductRelatedRepository : GenericRepository<ProductRelated>, IProductRelatedRepository
{
    private readonly DatabaseContext _context;
    public ProductRelatedRepository(DatabaseContext context):base(context) 
    {
        _context = context;
    }
    public async Task<List<ProductRelated>> GetRelatedProductsByProductId(int productId)
    {
        var list=await _context.ProductRelateds.Where(u=>u.ProductId == productId).ToListAsync();
        return list;
    }
}