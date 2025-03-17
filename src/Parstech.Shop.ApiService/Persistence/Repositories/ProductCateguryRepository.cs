using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductCateguryRepository : GenericRepository<ProductCategury>, IProductCateguryRepository
{
    private readonly DatabaseContext _context;

    public ProductCateguryRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProductCategury>> GetCateguriesByProduct(int productId)
    {
        return await _context.ProductCateguries.Where(u => u.ProductId == productId).ToListAsync();
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
        if (await _context.ProductCateguries.AnyAsync(u =>
                u.ProductId == productCategury.ProductId && u.CateguryId == productCategury.CateguryId))
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

    public async Task<ProductCategury> GetProductCateguryByProductIdAndCateguryName(int productId, string categuryName)
    {
        Categury? categury = await _context.Categuries.FirstOrDefaultAsync(u => u.GroupTitle == categuryName);
        return await _context.ProductCateguries.FirstOrDefaultAsync(u =>
            u.ProductId == productId && u.CateguryId == categury.GroupId);
    }
}