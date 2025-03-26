using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ProductGalleryRepository:GenericRepository<ProductGallery>,IProductGallleryRepository
{
    private readonly DatabaseContext _context;

    public ProductGalleryRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductGallery>> GetGalleriesByProduct(int productId)
    {
        return await _context.ProductGalleries.Where(u => u.ProductId == productId).ToListAsync();
    }

    public async Task<ProductGallery?> GetGalleryByProduct(int productId)
    {
        return await _context.ProductGalleries.FirstOrDefaultAsync(u => u.ProductId == productId);
    }

    public async Task<ProductGallery?> GetMainImageOfProduct(int productId)
    {
        return await _context.ProductGalleries.FirstOrDefaultAsync(u => u.ProductId == productId && u.IsMain);
    }
}