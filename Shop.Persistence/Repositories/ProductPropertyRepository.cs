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
    public class ProductPropertyRepository:GenericRepository<ProductProperty>,IProductPropertyRepository
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
        
        public async Task<bool> ExistPropertyForProduct(int productId,int propertyId)
        {
            if(await _context.ProductProperties.AnyAsync(u=>u.ProductId==productId&&u.PropertyId== propertyId))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
