using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductCategury;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using Shop.Persistence.Repositories;

namespace Shop.Persistence.Repositories
{
    public class ProductCateguryRepository : GenericRepository<ProductCategury>, IProductCateguryRepository
    {

        private readonly DatabaseContext _context;

        public ProductCateguryRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductCategury>> GetCateguriesByProduct(int productId)
        {
            return await _context.ProductCateguries.Where(u => u.ProductId == productId).OrderBy(u=>u.CateguryId).ToListAsync();
        }

        public async Task<ProductCategury?> GetCateguryByProduct(int productId)
        {
            return await _context.ProductCateguries.FirstOrDefaultAsync(u => u.ProductId == productId);
        }
        public async Task<List<ProductCategury>> GetProductCateguriesByCateguryId(int categuryId)
        {
            return await _context.ProductCateguries.Where(u => u.CateguryId == categuryId).ToListAsync();
        }
        public async Task<bool> ExistProductCategury(ProductCateguryDto productCategury)
        {
            if (await _context.ProductCateguries.AnyAsync(u => u.ProductId == productCategury.ProductId && u.CateguryId == productCategury.CateguryId))
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
        public async Task<bool> ExistProductCateguryForCateguryId(int categuryId)
        {
            if (await _context.ProductCateguries.AnyAsync(u => u.CateguryId == categuryId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> ProductHaveCategury(int productId, int categuryId)
        {
            if (await _context.ProductCateguries.AnyAsync(u => u.ProductId == productId && u.CateguryId == categuryId))
                {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ProductCategury> GetProductCateguryByProductIdAndCateguryName(int productId,string categuryName)
        {
            var categury =await _context.Categuries.FirstOrDefaultAsync(u=>u.GroupTitle==categuryName);
            return await _context.ProductCateguries.FirstOrDefaultAsync(u => u.ProductId == productId && u.CateguryId == categury.GroupId);
        }
    }
}
