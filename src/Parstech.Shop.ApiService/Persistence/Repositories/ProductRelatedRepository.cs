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
}
