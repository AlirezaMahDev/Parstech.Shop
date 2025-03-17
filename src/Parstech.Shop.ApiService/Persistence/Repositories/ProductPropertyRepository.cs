using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductPropertyRepository : GenericRepository<ProductProperty>, IProductPropertyRepository
{
    private readonly DatabaseContext _context;

    public ProductPropertyRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductProperty>> GetPropertiesByProduct(int productId)
    {
        return await _context.ProductProperties.Where(u => u.ProductId == productId).ToListAsync();
    }

    public async Task<ProductProperty?> GetpropertyByProduct(int productId)
    {
        return await _context.ProductProperties.FirstOrDefaultAsync(u => u.ProductId == productId);
    }

    public async Task<bool> ExistPropertyForProduct(int productId, int propertyId)
    {
        if (await _context.ProductProperties.AnyAsync(u => u.ProductId == productId && u.PropertyId == propertyId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}