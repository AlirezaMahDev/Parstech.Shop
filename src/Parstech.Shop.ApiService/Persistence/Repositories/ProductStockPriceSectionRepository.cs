using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
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
}
