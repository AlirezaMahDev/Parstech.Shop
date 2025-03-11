using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
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
}
